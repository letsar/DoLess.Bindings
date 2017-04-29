using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using DoLess.Bindings.Helpers;

namespace DoLess.Bindings
{
    internal class PropertyBinding<TSource, TTarget, TTargetProperty> :
        Binding<TSource, TTarget>,
        IPropertyBinding<TSource, TTarget, TTargetProperty>
        where TSource : class, INotifyPropertyChanged
        where TTarget : class
    {
        protected readonly PropertyInfo targetPropertyInfo;

        public PropertyBinding(IBinding<TSource, TTarget> binding, Expression<Func<TTarget, TTargetProperty>> targetPropertyExpression) :
            base((IHaveBindingSet<TSource, TTarget>)binding)
        {
            Check.NotNull(targetPropertyExpression, nameof(targetPropertyExpression));

            this.targetPropertyInfo = targetPropertyExpression.GetPropertyInfo();
        }

        public PropertyBinding(PropertyBinding<TSource, TTarget, TTargetProperty> bindingProperty) :
            base(bindingProperty)
        {
            this.targetPropertyInfo = bindingProperty.targetPropertyInfo;
        }

        public PropertyBinding(IPropertyBinding<TSource, TTarget, TTargetProperty> bindingProperty) :
            this((PropertyBinding<TSource, TTarget, TTargetProperty>)bindingProperty)
        { }

        protected TTargetProperty TargetProperty
        {
            get { return (TTargetProperty)this.targetPropertyInfo.GetValue(this.BindingSet.Target); }
            set { this.targetPropertyInfo.SetValue(this.BindingSet.Target, value); }
        }
    }
}
