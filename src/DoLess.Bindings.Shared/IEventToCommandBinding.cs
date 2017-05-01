using System;
using System.Windows.Input;

namespace DoLess.Bindings
{
    public interface IEventToCommandBinding<TSource, TTarget, TEventArgs, TCommand> :
        IBinding,
        ICanBind<TSource>
        where TSource : class
        where TTarget : class
        where TEventArgs : EventArgs
        where TCommand : ICommand
    {
        IEventToCommandBinding<TSource, TTarget, TEventArgs, TCommand> WithConverter<T>()
            where T : IConverterFromSource<TEventArgs, object>, new();
    }
}
