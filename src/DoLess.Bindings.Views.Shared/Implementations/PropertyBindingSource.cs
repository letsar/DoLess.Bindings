using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DoLess.Bindings
{
    internal class PropertyBindingSource<TSource, TTarget, TTargetProperty> :
        Binding<TSource, TTarget>,
        IPropertyBindingSource<TSource, TTarget, TTargetProperty>
        where TSource : class
        where TTarget : class
    {
        private readonly Expression<Func<TTarget, TTargetProperty>> targetPropertyExpression;

        public PropertyBindingSource(IBinding<TSource, TTarget> binding, Expression<Func<TTarget, TTargetProperty>> targetPropertyExpression) :
            base(binding)
        {
            this.targetPropertyExpression = targetPropertyExpression;
        }

        public IPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty> To<TSourceProperty>(Expression<Func<TSource, TSourceProperty>> sourcePropertyExpression, BindingMode mode = BindingMode.OneWay)
        {
            throw new NotImplementedException();
        }
    }
}
