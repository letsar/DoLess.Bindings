using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;

namespace DoLess.Bindings
{
    public interface IPropertyBinding<TSource, TTarget, TTargetProperty>
        where TSource : class, INotifyPropertyChanged
        where TTarget : class
    {
        
    }
}
