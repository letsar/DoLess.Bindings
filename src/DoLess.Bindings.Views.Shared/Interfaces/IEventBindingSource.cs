using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Input;

namespace DoLess.Bindings
{
    public interface IEventBindingSource<TSource, TTarget, TEventArgs>
        where TSource : class
        where TTarget : class
        where TEventArgs : EventArgs
    {
        IEventToCommandBinding<TSource, TTarget, TEventArgs, TCommand> To<TCommand>(Expression<Func<TSource, TCommand>> commandExpression)
            where TCommand : ICommand;
    }
}
