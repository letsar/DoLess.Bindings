using System;
using System.Linq.Expressions;

namespace DoLess.Bindings
{
    internal class PropertyBinding<TSource, TTarget, TTargetProperty> :
        Binding<TSource, TTarget>,
        IPropertyBinding<TSource, TTarget, TTargetProperty>
        where TSource : class
        where TTarget : class
    {
        protected BindingExpression<TTarget, TTargetProperty> targetProperty;

        public PropertyBinding(IBinding<TSource, TTarget> binding, Expression<Func<TTarget, TTargetProperty>> targetPropertyExpression) :
            base((Binding<TSource, TTarget>)binding)
        {
            this.targetProperty = targetPropertyExpression?.GetBindingExpression(this.Target);
        }

        public PropertyBinding(PropertyBinding<TSource, TTarget, TTargetProperty> propertyBinding) :
            base(propertyBinding)
        {
            this.targetProperty = propertyBinding.targetProperty;
            propertyBinding.DeleteFromGroup();
        }

        public PropertyBinding(IPropertyBinding<TSource, TTarget, TTargetProperty> propertyBinding) :
            this((PropertyBinding<TSource, TTarget, TTargetProperty>)propertyBinding)
        {
        }

        public override void Dispose()
        {            
            this.targetProperty = null;
            base.Dispose();
        }

        public IOneWayPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty> To<TSourceProperty>(Expression<Func<TSource, TSourceProperty>> sourcePropertyExpression)
        {
            return new OneWayPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty>(this, sourcePropertyExpression)
                      .WithConverter<ClassCastConverter<TSourceProperty, TTargetProperty>>();
        }
    }
}
