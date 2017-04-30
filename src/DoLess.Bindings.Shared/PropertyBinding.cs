using System;
using System.ComponentModel;
using System.Linq.Expressions;
using DoLess.Bindings.Helpers;

namespace DoLess.Bindings
{
    internal class PropertyBinding<TSource, TTarget, TTargetProperty> :
        Binding<TSource, TTarget>,
        IPropertyBinding<TSource, TTarget, TTargetProperty>
        where TSource : class, INotifyPropertyChanged
        where TTarget : class
    {
        protected readonly BindingExpression<TTarget, TTargetProperty> targetProperty;

        public PropertyBinding(IBinding<TSource, TTarget> binding, Expression<Func<TTarget, TTargetProperty>> targetPropertyExpression) :
            base((IHaveBindingSet<TSource, TTarget>)binding)
        {
            Check.NotNull(targetPropertyExpression, nameof(targetPropertyExpression));

            this.targetProperty = targetPropertyExpression.GetBindingExpression(this.BindingSet.Target);
        }

        public PropertyBinding(PropertyBinding<TSource, TTarget, TTargetProperty> bindingProperty) :
            base(bindingProperty)
        {
            this.targetProperty = bindingProperty.targetProperty;
        }

        public PropertyBinding(IPropertyBinding<TSource, TTarget, TTargetProperty> bindingProperty) :
            this((PropertyBinding<TSource, TTarget, TTargetProperty>)bindingProperty)
        { }
    }
}
