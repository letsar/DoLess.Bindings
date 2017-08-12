using System;
using System.Windows.Input;

namespace DoLess.Bindings
{
    internal class EventToCommandWithConverterBinding<TSource, TTarget, TEventArgs, TCommand, TParameter> :
        EventToCommandBinding<TSource, TTarget, TEventArgs, TCommand>,
        IEventToCommandWithConverterBinding<TSource, TTarget, TEventArgs, TCommand, TParameter>
        where TSource : class
        where TTarget : class
        where TEventArgs : EventArgs
        where TCommand : ICommand
    {
        private readonly Func<TEventArgs, TParameter> converter;

        public EventToCommandWithConverterBinding(EventToCommandBinding<TSource, TTarget, TEventArgs, TCommand> eventBinding, Func<TEventArgs, TParameter> converter) : base(eventBinding)
        {
            this.converter = converter;
        }

        protected override object GetCommandParameter(TEventArgs eventArgs)
        {
            TParameter parameter = default(TParameter);
            if (this.converter != null)
            {
                parameter = this.converter(eventArgs);
            }
            return parameter;
        }
    }
}
