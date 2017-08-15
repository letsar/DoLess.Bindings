using System;
using System.Linq.Expressions;

namespace DoLess.Bindings
{
    public interface IPropertyBindingSource<TSource, TTarget, TTargetProperty>
        where TSource : class
        where TTarget : class
    {
        IPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty> To<TSourceProperty>(Expression<Func<TSource, TSourceProperty>> sourceExpression, BindingMode mode = BindingMode.OneWay);
    }
}
