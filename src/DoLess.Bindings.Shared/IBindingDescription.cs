using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DoLess.Bindings
{
    public interface IBindingDescription<TSource, TTarget>
        where TSource : class
        where TTarget : class
    {
        IBinding<TSource, TTarget> To<TProperty>(Expression<Func<TSource, TProperty>> sourceProperty);
    }
}
