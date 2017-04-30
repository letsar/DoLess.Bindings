using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;

namespace DoLess.Bindings
{
    public interface ITwoWayPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty>
        where TSource : class, INotifyPropertyChanged
        where TTarget : class
    {
        ITwoWayPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty> WithConverter<T>()
            where T : IConverter<TSourceProperty, TTargetProperty>, new();
    }
}
