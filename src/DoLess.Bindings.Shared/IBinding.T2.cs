using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DoLess.Bindings
{
    /// <summary>
    /// Represents a binding between a source and a target.
    /// </summary>
    public interface IBinding<TSource, TTarget>
        where TSource : class, INotifyPropertyChanged
        where TTarget : class
    {

    }
}
