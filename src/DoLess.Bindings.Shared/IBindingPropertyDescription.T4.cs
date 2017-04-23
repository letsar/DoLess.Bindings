using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;

namespace DoLess.Bindings
{
    public interface IBindingOneWayPropertyDescription<TSource, TTarget, TTargetProperty, TSourceProperty>
        where TSource : class, INotifyPropertyChanged
        where TTarget : class
    {
        
    }
}
