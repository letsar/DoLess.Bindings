using System;
using System.Linq.Expressions;

namespace DoLess.Bindings
{
    internal class OneWayPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty> :
        PropertyBindingBase<TSource, TTarget, TTargetProperty, TSourceProperty>,
        IPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty>
        where TSource : class
        where TTarget : class
    {
        public OneWayPropertyBinding(IBinding<TSource, TTarget> parent, Expression<Func<TSource, TSourceProperty>> sourcePropertyExpression, Expression<Func<TTarget, TTargetProperty>> targetPropertyExpression):base(parent,sourcePropertyExpression,targetPropertyExpression)
        {
        }

        public override BindingMode Mode => BindingMode.OneWay;

        protected override void OnSourceChanged(object sender, string propertyName)
        {
            this.UpdateTargetProperty();
        }
    }
}
