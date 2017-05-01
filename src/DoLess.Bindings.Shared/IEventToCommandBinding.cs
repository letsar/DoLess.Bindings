using System;
using System.Windows.Input;

namespace DoLess.Bindings
{
    public interface IEventToCommandBinding<TSource, TTarget, TEventArgs, TCommand>
        where TSource : class
        where TTarget : class
        where TEventArgs : EventArgs
        where TCommand : ICommand
    {
        
    }
}
