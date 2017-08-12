using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DoLess.Bindings
{
    internal abstract class PropertyBindingBase<TSource, TTarget, TTargetProperty, TSourceProperty> :
        Binding<TSource, TTarget, TSourceProperty>,
        IPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty>
        where TSource : class
        where TTarget : class
    {
        public PropertyBindingBase(IBinding<TSource, TTarget> parent, Expression<Func<TSource, TSourceProperty>> sourcePropertyExpression, Expression<Func<TTarget, TTargetProperty>> targetPropertyExpression) :
            base(parent, sourcePropertyExpression)
        {
            // The target is the view.
            this.TargetPropertyBindingExpression = new PropertyBindingExpression<TTarget, TTargetProperty>(this.Target, targetPropertyExpression);
        }

        public abstract BindingMode Mode { get; }

        public TTargetProperty TargetProperty => this.TargetPropertyBindingExpression.Value;

        public IConverter<TSourceProperty, TTargetProperty> Converter { get; private set; }

        protected PropertyBindingExpression<TTarget, TTargetProperty> TargetPropertyBindingExpression { get; private set; }

        public IPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty> WithConverter<TConverter>()
            where TConverter : IConverter<TSourceProperty, TTargetProperty>, new()
        {
            this.Converter = Cache<TConverter>.Instance;

            // The view needs to be updated.
            this.UpdateTargetProperty();
            return this;
        }

        public override void Dispose()
        {
            this.TargetPropertyBindingExpression = null;
            base.Dispose();
        }

        protected void UpdateTargetProperty()
        {
            this.UpdateProperty(() => this.TargetPropertyBindingExpression.Value = this.Converter.ConvertFromSource(this.SourceProperty), this.TargetPropertyBindingExpression);
        }

        protected void UpdateSourceProperty()
        {
            this.UpdateProperty(() => this.SourcePropertyBindingExpression.Value = this.Converter.ConvertFromTarget(this.TargetProperty), this.SourcePropertyBindingExpression);
        }

        protected void UpdateProperty(Action update, IPropertyBindingExpression propertyBindingExpression)
        {
            if (this.HasConverter())
            {
                try
                {
                    update();
                }
                catch (Exception ex)
                {
                    Bindings.LogError($"when trying to set the property '{propertyBindingExpression.RawExpression}'", ex);
                }
            }
        }

        protected bool HasConverter()
        {
            if (this.Converter == null)
            {
                Bindings.LogError($"a converter is needed for the types {typeof(TSourceProperty).FullName} and {typeof(TTargetProperty).FullName}");
                return false;
            }

            return true;
        }
    }
}
