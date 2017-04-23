using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;

namespace DoLess.Bindings
{
    public interface IBindingDescription<TSource, TTarget>
        where TSource : class, INotifyPropertyChanged
        where TTarget : class
    {
        
    }
}
