using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Input;

namespace DoLess.Bindings
{
    public interface IEventBinding<TSource, TTarget, TEventArgs>
        where TSource : class, INotifyPropertyChanged
        where TTarget : class
        where TEventArgs : EventArgs
    {
    }
}
