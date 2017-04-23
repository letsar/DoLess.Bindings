using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DoLess.Bindings
{
    internal interface IHaveBindingSet<TSource, TTarget>
        where TSource : class, INotifyPropertyChanged
        where TTarget : class
    {
        BindingSet<TSource, TTarget> BindingSet { get; }
    }
}
