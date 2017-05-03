using System;
using System.Linq.Expressions;

namespace DoLess.Bindings
{
    public interface IPropertyBinding<TSource, TTarget, TTargetProperty>
        where TSource : class
        where TTarget : class
    {
        IOneWayPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty> To<TSourceProperty>(Expression<Func<TSource, TSourceProperty>> sourcePropertyExpression);
    }
}
