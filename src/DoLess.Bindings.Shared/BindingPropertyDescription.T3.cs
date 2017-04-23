using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using DoLess.Bindings.Helpers;

namespace DoLess.Bindings
{
    internal class BindingPropertyDescription<TSource, TTarget, TTargetProperty> :
        BindingDescription<TSource, TTarget>,
        IBindingPropertyDescription<TSource, TTarget, TTargetProperty>
        where TSource : class, INotifyPropertyChanged
        where TTarget : class
    {
        private readonly PropertyInfo targetPropertyInfo;

        public BindingPropertyDescription(IBindingDescription<TSource, TTarget> bindingDescription, Expression<Func<TTarget, TTargetProperty>> targetPropertyExpression) :
            base((IHaveBindingSet<TSource, TTarget>)bindingDescription)
        {
            Check.NotNull(targetPropertyExpression, nameof(targetPropertyExpression));

            this.targetPropertyInfo = targetPropertyExpression.GetPropertyInfo();
        }

        public BindingPropertyDescription(BindingPropertyDescription<TSource, TTarget, TTargetProperty> bindingPropertyDescription) :
            base(bindingPropertyDescription)
        {
            this.targetPropertyInfo = bindingPropertyDescription.targetPropertyInfo;
        }

        public BindingPropertyDescription(IBindingPropertyDescription<TSource, TTarget, TTargetProperty> bindingPropertyDescription) :
            this((BindingPropertyDescription<TSource, TTarget, TTargetProperty>)bindingPropertyDescription)
        { }

        protected TTargetProperty TargetProperty
        {
            get { return (TTargetProperty)this.targetPropertyInfo.GetValue(this.BindingSet.Target); }
            set { this.targetPropertyInfo.SetValue(this.BindingSet.Target, value); }
        }
    }
}
