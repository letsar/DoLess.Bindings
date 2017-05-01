using System;
using System.Linq.Expressions;
using System.Reflection;
using DoLess.Bindings.Helpers;
using DoLess.Bindings.Observation;

namespace DoLess.Bindings
{
    internal sealed class TwoWayPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty> :
        PropertyBinding<TSource, TTarget, TTargetProperty>,
        ITwoWayPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty>
        where TSource : class
        where TTarget : class
    {
        private static readonly Type TargetType;
        private readonly BindingExpression<TSource, TSourceProperty> sourceProperty;
        private ObservedNode sourceRootNode;
        private DynamicWeakEventHandler<TTarget> targetPropertyChangedWeakEventHandler;
        private IConverter<TSourceProperty, TTargetProperty> converter;
        private bool isBinding;
        private object isBindingLock = new object();

        static TwoWayPropertyBinding()
        {
            TargetType = typeof(TTarget);
        }

        public TwoWayPropertyBinding(IPropertyBinding<TSource, TTarget, TTargetProperty> propertyBinding, Expression<Func<TSource, TSourceProperty>> sourcePropertyExpression) :
            base(propertyBinding)
        {
            Check.NotNull(sourcePropertyExpression, nameof(sourcePropertyExpression));

            this.isBinding = false;
            this.sourceProperty = sourcePropertyExpression.GetBindingExpression(this.Source);
            this.sourceRootNode = sourcePropertyExpression.AsObservedNode();
            this.sourceRootNode.Observe(this.Source, this.OnSourceChanged);

            this.targetPropertyChangedWeakEventHandler = this.CreateHandlerForFirstExistingEvent(this.Target, this.targetProperty.Name + "Changed", "EditingDidEnd", "ValueChanged", "Changed");                                    
        }

        public ITwoWayPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty> WithConverter<T>()
            where T : IConverter<TSourceProperty, TTargetProperty>, new()
        {
            this.converter = Cache<T>.Instance;

            // The result may have changed.
            this.OnSourceChanged();
            return this;
        }

        private bool CheckConverter()
        {
            if (this.converter == null)
            {
                Bindings.LogError($"a converter is needed for the types {typeof(TSourceProperty).FullName} and {typeof(TTargetProperty).FullName}");
                return false;
            }

            return true;
        }

        private DynamicWeakEventHandler<TTarget> CreateHandlerForFirstExistingEvent(TTarget target, params string[] eventNames)
        {
            for (int i = 0; i < eventNames.Length; i++)
            {
                var eventName = eventNames[i];
                var eventInfo = TargetType.GetRuntimeEvent(eventName);

                if (eventInfo != null)
                {
                    return new DynamicWeakEventHandler<TTarget>(target, eventName, this.OnTargetPropertyChanged);
                }
            }

            return null;
        }

        private void DoBinding(Action binding)
        {
            if (this.StartBinding())
            {
                if (this.CheckConverter())
                {
                    try
                    {
                        binding();
                    }
                    catch (Exception ex)
                    {
                        Bindings.LogError($"when trying to set the two-way binding on type'{typeof(TTarget).FullName}': {ex.ToString()}");
                    }
                }
                this.StopBinding();
            }
        }

        private void OnSourceChanged()
        {
            this.DoBinding(() => this.targetProperty.Value = this.converter.ConvertFromSource(this.sourceProperty.Value));
        }

        private void OnTargetPropertyChanged(object sender, EventArgs args)
        {
            this.DoBinding(() => this.sourceProperty.Value = this.converter.ConvertFromTarget(this.targetProperty.Value));
        }

        private bool StartBinding()
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

        private void StopBinding()
        {
            lock (this.isBindingLock)
            {
                this.isBinding = false;
            }
        }

        public override void UnbindInternal()
        {
            base.UnbindInternal();
            this.sourceRootNode.Unobserve();
            this.sourceRootNode = null;
            this.targetPropertyChangedWeakEventHandler.Unsubscribe();
            this.targetPropertyChangedWeakEventHandler = null;
        }
    }
}
