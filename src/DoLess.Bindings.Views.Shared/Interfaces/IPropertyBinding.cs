using System;
using System.Collections.Generic;
using System.Text;

namespace DoLess.Bindings
{
    public interface IPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty>
        where TSource : class
        where TTarget : class
    {
        BindingMode Mode { get; }
    }
}
