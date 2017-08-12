using System;
using System.Linq.Expressions;

namespace DoLess.Bindings
{
    internal class TwoWayPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty> :
        PropertyBindingBase<TSource, TTarget, TTargetProperty, TSourceProperty>,
        IPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty>
        where TSource : class
        where TTarget : class
    {
        private OnChangedEventSubscription<TTarget> onChangedEventSubscription;
        private bool isBinding;
        private object isBindingLock = new object();


        public TwoWayPropertyBinding(IBinding<TSource, TTarget> parent, Expression<Func<TSource, TSourceProperty>> sourcePropertyExpression, Expression<Func<TTarget, TTargetProperty>> targetPropertyExpression) : base(parent, sourcePropertyExpression, targetPropertyExpression)
        {
            this.isBinding = false;
            this.onChangedEventSubscription = new OnChangedEventSubscription<TTarget>(this.TargetPropertyBindingExpression.Name, this.Target, this.OnTargetChanged);
        }

        public override BindingMode Mode => BindingMode.TwoWay;

        protected override void OnSourceChanged(object sender, string propertyName)
        {
            this.UpdateProperty(() => this.UpdateTargetProperty());
        }

        private void OnTargetChanged()
        {
            this.UpdateProperty(() => this.UpdateSourceProperty());
        }

        private void UpdateProperty(Action updateProperty)
        {
            if (this.StartUpdateProperty())
            {
                updateProperty();
                this.StopUpdateProperty();
            }
        }

        private bool StartUpdateProperty()
        {
            bool canBind = false;
            lock (this.isBindingLock)
            {
                if (!this.isBinding)
                {
                    this.isBinding = true;
                    canBind = true;
                }
            }

            return canBind;
        }

        private void StopUpdateProperty()
        {
            lock (this.isBindingLock)
            {
                this.isBinding = false;
            }
        }

        public override void Dispose()
        {
            DisposerHelper.Release(ref this.onChangedEventSubscription);
            base.Dispose();
        }
    }
}
