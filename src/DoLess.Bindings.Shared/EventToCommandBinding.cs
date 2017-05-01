using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows.Input;
using DoLess.Bindings.Helpers;
using DoLess.Bindings.Observation;

namespace DoLess.Bindings
{
    internal class EventToCommandBinding<TSource, TTarget, TEventArgs, TCommand> :
        EventBinding<TSource, TTarget, TEventArgs>,
        IEventToCommandBinding<TSource, TTarget, TEventArgs, TCommand>
        where TSource : class
        where TTarget : class
        where TEventArgs : EventArgs
        where TCommand : ICommand
    {
        private readonly Func<TSource, TCommand> getCommand;
        private readonly Expression<Func<TSource, TCommand>> commandExpression;
        private readonly ObservedNode sourceRootNode;
        private IConverterFromSource<TEventArgs, object> argsToParameter;

        public EventToCommandBinding(IEventBinding<TSource, TTarget, TEventArgs> binding, Expression<Func<TSource, TCommand>> commandExpression) :
            base(binding)
        {
            Check.NotNull(commandExpression, nameof(commandExpression));

            this.commandExpression = commandExpression;
            this.getCommand = commandExpression.Compile();
            this.sourceRootNode = commandExpression.AsObservedNode();
            this.sourceRootNode.Observe(this.BindingSet.Source, this.WhenCommandChanged);
        }

        protected override void OnEventRaised(object sender, TEventArgs args)
        {
            object parameter = this.GetCommandParameter(args);

            var command = this.GetCommand();

            if (command != null)
            {
                command.Execute(parameter);
            }
        }

        protected ICommand GetCommand()
        {
            ICommand command = null;
            TSource source = this.BindingSet.Source;
            if (source != null)
            {
                try
                {
                    command = this.getCommand(source);
                }
                catch (Exception ex)
                {
                    Bindings.LogError($"when getting command for type '{typeof(TTarget).FullName}' on event '{this.EventName}': {ex.ToString()}");
                }
            }

            return command;
        }

        protected object GetCommandParameter(TEventArgs args)
        {
            object parameter = null;
            if (this.argsToParameter != null)
            {
                parameter = this.argsToParameter.ConvertFromSource(args);
            }
            return parameter;
        }

        protected virtual void WhenCommandChanged() { }
    }
}
