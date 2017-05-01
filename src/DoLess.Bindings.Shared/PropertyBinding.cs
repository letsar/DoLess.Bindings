using System;
using System.Linq.Expressions;
using DoLess.Bindings.Helpers;

namespace DoLess.Bindings
{
    internal class PropertyBinding<TSource, TTarget, TTargetProperty> :
        Binding<TSource, TTarget>,
        IPropertyBinding<TSource, TTarget, TTargetProperty>
        where TSource : class
        where TTarget : class
    {
        protected readonly BindingExpression<TTarget, TTargetProperty> targetProperty;

        public PropertyBinding(IBinding<TSource, TTarget> binding, Expression<Func<TTarget, TTargetProperty>> targetPropertyExpression) :
            base((IBindingDescription<TSource, TTarget>)binding)
        {
            Check.NotNull(targetPropertyExpression, nameof(targetPropertyExpression));

            this.targetProperty = targetPropertyExpression.GetBindingExpression(this.Target);
        }

        public PropertyBinding(PropertyBinding<TSource, TTarget, TTargetProperty> propertyBinding) :
            base(propertyBinding)
        {
            this.targetProperty = propertyBinding.targetProperty;
            propertyBinding.UnbindInternal();
        }

        public PropertyBinding(IPropertyBinding<TSource, TTarget, TTargetProperty> propertyBinding) :
            this((PropertyBinding<TSource, TTarget, TTargetProperty>)propertyBinding)
        {            
        }        
    }
}
