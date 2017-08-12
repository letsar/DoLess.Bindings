using System;
using System.Windows.Input;

namespace DoLess.Bindings
{
    internal class EventBinding<TSource, TTarget, TEventArgs, TCommand, TParameter> :
        EventBinding<TSource, TTarget, TEventArgs, TCommand>,
        IEventBinding<TSource, TTarget, TEventArgs, TCommand, TParameter>
        where TSource : class
        where TTarget : class
        where TEventArgs : EventArgs
        where TCommand : ICommand
    {
        private readonly Func<TEventArgs, TParameter> converter;

        public EventBinding(EventBinding<TSource, TTarget, TEventArgs, TCommand> eventBinding, Func<TEventArgs, TParameter> converter) : base(eventBinding)
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
