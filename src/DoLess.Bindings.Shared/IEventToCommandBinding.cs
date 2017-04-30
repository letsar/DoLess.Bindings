using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Input;

namespace DoLess.Bindings
{
    public interface IEventToCommandBinding<TSource, TTarget, TEventArgs, TCommand>
        where TSource : class, INotifyPropertyChanged
        where TTarget : class
        where TEventArgs : EventArgs
        where TCommand : ICommand
    {
        
    }
}
