using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;

namespace DoLess.Bindings
{
    public interface IBindingPropertyDescription<TSource, TTarget, TTargetProperty>
        where TSource : class, INotifyPropertyChanged
        where TTarget : class
    {
        
    }
}
