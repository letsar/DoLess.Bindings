using System;
using System.Windows.Input;

namespace DoLess.Bindings
{
    public interface IEventToCommandBinding<TSource, TTarget, TEventArgs, TCommand> :
        ICanBind<TSource>
        where TSource : class
        where TTarget : class
        where TEventArgs : EventArgs
        where TCommand : ICommand
    {
        IEventToCommandWithConverterBinding<TSource, TTarget, TEventArgs, TCommand, TParameter> WithConverter<TParameter>(Func<TEventArgs, TParameter> converter);
    }
}
