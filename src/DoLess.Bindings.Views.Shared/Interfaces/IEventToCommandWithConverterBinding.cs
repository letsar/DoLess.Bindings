using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DoLess.Bindings
{
    public interface IEventToCommandWithConverterBinding<TSource, TTarget, TEventArgs, TCommand, TParameter> :
        ICanBind<TSource>
        where TSource : class
        where TTarget : class
        where TEventArgs : EventArgs
        where TCommand : ICommand
    {

    }
}
