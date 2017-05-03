using System;
using System.Linq.Expressions;
using System.Windows.Input;

namespace DoLess.Bindings
{
    public interface IEventBinding<TSource, TTarget, TEventArgs> : 
        IBinding
        where TSource : class
        where TTarget : class
        where TEventArgs : EventArgs
    {
        IEventToCommandBinding<TSource, TTarget, TEventArgs, TCommand> To<TCommand>(Expression<Func<TSource, TCommand>> commandExpression)
            where TCommand : ICommand;
    }
}
