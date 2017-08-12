using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Input;

namespace DoLess.Bindings
{
    internal class EventBinding<TSource, TTarget, TEventArgs, TCommand> :
        Binding<TSource, TTarget, TCommand>,
        IEventBinding<TSource, TTarget, TEventArgs, TCommand>
        where TSource : class
        where TTarget : class
        where TEventArgs : EventArgs
        where TCommand : ICommand
    {
        private readonly Func<TSource, TCommand> getCommand;
        private Action<TTarget, EventHandler<TEventArgs>> addHandler;
        private Action<TTarget, EventHandler<TEventArgs>> removeHandler;
        private PropertyBindingExpression<TTarget, bool> targetCanExecutePropertyBindingExpression;
        private ICommand command;
        private Expression<Func<TSource, TCommand>> commandExpression;
        private Expression<Func<TTarget, bool>> targetCanExecutePropertyExpression;

        public EventBinding(IBinding<TSource, TTarget> binding, Action<TTarget, EventHandler<TEventArgs>> addHandler, Action<TTarget, EventHandler<TEventArgs>> removeHandler, Expression<Func<TSource, TCommand>> commandExpression, Expression<Func<TTarget, bool>> targetCanExecutePropertyExpression = null) : base(binding, commandExpression)
        {
            if (commandExpression != null)
            {
                this.commandExpression = commandExpression;
                this.getCommand = commandExpression.Compile();
                this.addHandler = addHandler;
                this.removeHandler = removeHandler;

                this.addHandler(this.Target, this.OnEvent);

                if (targetCanExecutePropertyExpression != null)
                {
                    this.targetCanExecutePropertyExpression = targetCanExecutePropertyExpression;
                    this.targetCanExecutePropertyBindingExpression = new PropertyBindingExpression<TTarget, bool>(this.Target, targetCanExecutePropertyExpression);
                    this.OnCommandChanged();
                }
            }
        }

        protected EventBinding(EventBinding<TSource, TTarget, TEventArgs, TCommand> eventBinding) : this((IBinding<TSource, TTarget>)eventBinding.Parent, eventBinding.addHandler, eventBinding.removeHandler, eventBinding.commandExpression, eventBinding.targetCanExecutePropertyExpression)
        {
        }

        public override void Dispose()
        {
            this.removeHandler(this.Target, this.OnEvent);
            this.removeHandler = null;
            this.addHandler = null;
            this.targetCanExecutePropertyBindingExpression = null;
            base.Dispose();
        }

        public IEventBinding<TSource, TTarget, TEventArgs, TCommand, TParameter> WithConverter<TParameter>(Func<TEventArgs, TParameter> converter)
        {
            return new EventBinding<TSource, TTarget, TEventArgs, TCommand, TParameter>(this, converter);
        }

        protected override void OnSourceChanged(object sender, string propertyName)
        {
            this.OnCommandChanged();
        }

        private void OnCommandChanged()
        {
            if (this.CanTrackCanExecuteChanged())
            {
                this.command.CanExecuteChanged -= this.OnCanExecuteChanged;
            }

            this.command = this.GetCommand();

            if (this.CanTrackCanExecuteChanged())
            {
                this.command.CanExecuteChanged += this.OnCanExecuteChanged;
                this.OnCanExecuteChanged(this.command, EventArgs.Empty);
            }
        }

        private void OnCanExecuteChanged(object sender, EventArgs eventArgs)
        {
            if (sender is ICommand command)
            {
                this.targetCanExecutePropertyBindingExpression.Value = this.command.CanExecute(null);
            }
        }

        private bool CanTrackCanExecuteChanged() => this.targetCanExecutePropertyBindingExpression != null && this.command != null;

        private void OnEvent(object sender, TEventArgs eventArgs)
        {
            var parameter = this.GetCommandParameter(eventArgs);
            this.command?.Execute(parameter);
        }

        protected virtual object GetCommandParameter(TEventArgs eventArgs)
        {
            return null;
        }


        private ICommand GetCommand()
        {
            ICommand command = null;
            try
            {
                command = this.getCommand(this.Source);
            }
            catch (Exception ex)
            {
                Bindings.LogError($"when getting command '{this.SourcePropertyBindingExpression.Name}' on type '{typeof(TSource).FullName}'", ex);
            }
            return command;
        }
    }
}
