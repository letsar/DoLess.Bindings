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
        private ObservedNode sourceRootNode;
        private IConverterFromSource<TEventArgs, object> converter;
        private CanExecuteChangedWeakEventHandler canExecuteChangedWeakEventHandler;
        private BindingExpression<TTarget, bool> canExecuteTargetProperty;

        public EventToCommandBinding(IEventBinding<TSource, TTarget, TEventArgs> binding, Expression<Func<TSource, TCommand>> commandExpression, Expression<Func<TTarget, bool>> canExecuteTargetPropertyExpression = null) :
            base(binding)
        {
            Check.NotNull(commandExpression, nameof(commandExpression));

            this.getCommand = commandExpression.Compile();
            this.sourceRootNode = commandExpression.AsObservedNode();
            this.sourceRootNode.Observe(this.Source, this.WhenCommandChanged);

            if (canExecuteTargetPropertyExpression != null)
            {
                this.canExecuteTargetProperty = canExecuteTargetPropertyExpression.GetBindingExpression(this.Target);
                this.WhenCommandChanged();
            }
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
            TSource source = this.Source;
            if (source != null)
            {
                try
                {
                    command = this.getCommand(source);
                }
                catch (Exception ex)
                {
                    Bindings.LogError($"when getting command for type '{typeof(TSource).FullName}' on event '{this.EventName}': {ex.ToString()}");
                }
            }

            return command;
        }

        protected object GetCommandParameter(TEventArgs args)
        {
            object parameter = null;
            if (this.converter != null)
            {
                parameter = this.converter.ConvertFromSource(args);
            }
            return parameter;
        }

        public IEventToCommandBinding<TSource, TTarget, TEventArgs, TCommand> WithConverter<T>()
            where T : IConverterFromSource<TEventArgs, object>, new()
        {
            this.converter = Cache<T>.Instance;
            return this;
        }

        protected void WhenCommandChanged()
        {
            if (this.canExecuteTargetProperty != null)
            {
                if (this.canExecuteChangedWeakEventHandler != null)
                {
                    this.canExecuteChangedWeakEventHandler.Unsubscribe();
                    this.canExecuteChangedWeakEventHandler = null;
                }

                var command = this.GetCommand();
                this.canExecuteChangedWeakEventHandler = new CanExecuteChangedWeakEventHandler(command, this.OnCanExecuteChanged);
                this.OnCanExecuteChanged(command, EventArgs.Empty);
            }
        }

        private void OnCanExecuteChanged(object sender, EventArgs args)
        {
            ICommand command = sender as ICommand;

            if (command != null)
            {
                this.canExecuteTargetProperty.Value = command.CanExecute(null);
            }
        }

        public override void UnbindInternal()
        {
            base.UnbindInternal();
            this.sourceRootNode.Unobserve();
            this.sourceRootNode = null;
            this.canExecuteTargetProperty = null;
            this.canExecuteChangedWeakEventHandler.Unsubscribe();
            this.canExecuteChangedWeakEventHandler = null;
        }
    }
}
