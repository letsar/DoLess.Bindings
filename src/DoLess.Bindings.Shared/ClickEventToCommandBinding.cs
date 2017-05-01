using System;
using System.Linq.Expressions;
using System.Windows.Input;
using DoLess.Bindings.Helpers;

namespace DoLess.Bindings
{
    internal class ClickEventToCommandBinding<TSource, TTarget, TEventArgs, TCommand> :
        EventToCommandBinding<TSource, TTarget, TEventArgs, TCommand>,
        IEventToCommandBinding<TSource, TTarget, TEventArgs, TCommand>        
        where TSource : class
        where TTarget : class
        where TEventArgs : EventArgs
        where TCommand : ICommand
    {
        private CanExecuteChangedWeakEventHandler canExecuteChangedWeakEventHandler;
        private BindingExpression<TTarget, bool> canExecuteTargetProperty;

        public ClickEventToCommandBinding(IEventBinding<TSource, TTarget, TEventArgs> binding, Expression<Func<TSource, TCommand>> commandExpression, Expression<Func<TTarget, bool>> canExecuteTargetPropertyExpression) :
            base(binding, commandExpression)
        {
            Check.NotNull(canExecuteTargetPropertyExpression, nameof(canExecuteTargetPropertyExpression));

            this.canExecuteTargetProperty = canExecuteTargetPropertyExpression.GetBindingExpression(this.Target);
            this.WhenCommandChanged();
        }

        protected override void WhenCommandChanged()
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
            this.canExecuteTargetProperty = null;
            this.canExecuteChangedWeakEventHandler.Unsubscribe();
            this.canExecuteChangedWeakEventHandler = null;
        }
    }
}
