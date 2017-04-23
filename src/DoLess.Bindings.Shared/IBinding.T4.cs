using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DoLess.Bindings
{
    /// <summary>
    /// Represents a binding between a source and a targe, from the specified source property to the specified target property.
    /// </summary>
    public interface IBinding<TSource, TTarget, TTargetProperty, TSourceProperty>
        where TSource : class, INotifyPropertyChanged
        where TTarget : class        
    {

    }
}
