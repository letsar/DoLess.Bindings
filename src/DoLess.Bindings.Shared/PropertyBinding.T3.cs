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
        private readonly PropertyInfo targetPropertyInfo;

        public PropertyBinding(IBindingDescription<TSource, TTarget> bindingDescription, Expression<Func<TTarget, TTargetProperty>> targetPropertyExpression) :
            base((IHaveBindingSet<TSource, TTarget>)bindingDescription)
        {
            Check.NotNull(targetPropertyExpression, nameof(targetPropertyExpression));

            this.targetPropertyInfo = targetPropertyExpression.GetPropertyInfo();
        }

        public PropertyBinding(PropertyBinding<TSource, TTarget, TTargetProperty> bindingPropertyDescription) :
            base(bindingPropertyDescription)
        {
            this.targetPropertyInfo = bindingPropertyDescription.targetPropertyInfo;
        }

        public PropertyBinding(IPropertyBinding<TSource, TTarget, TTargetProperty> bindingPropertyDescription) :
            this((PropertyBinding<TSource, TTarget, TTargetProperty>)bindingPropertyDescription)
        { }

        protected TTargetProperty TargetProperty
        {
            get { return (TTargetProperty)this.targetPropertyInfo.GetValue(this.BindingSet.Target); }
            set { this.targetPropertyInfo.SetValue(this.BindingSet.Target, value); }
        }
    }
}
