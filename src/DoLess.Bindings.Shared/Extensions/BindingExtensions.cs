using System;
using System.Linq.Expressions;
using System.Windows.Input;

namespace DoLess.Bindings
{
    public static partial class BindingExtensions
    {
        public static IBinding<TSource, TTarget> Bind<TSource, TTarget>(this IView<TSource> self, TTarget target)
            where TSource : class
            where TTarget : class
        {
            return new Binding<TSource, TTarget>(self.ViewModel, target, null, self);
        }

        internal static IEventToCommandBinding<TSource, TTarget, EventArgs, TCommand> EventTo<TSource, TTarget, TCommand>(this IBinding<TSource, TTarget> self, Expression<Func<TSource, TCommand>> commandExpression, Func<TTarget, EventHandler<EventArgs>, WeakEventHandler<TTarget, EventArgs>> weakEventHandlerFactory, Expression<Func<TTarget, bool>> canExecutePropertyExpression = null)
            where TSource : class
            where TTarget : class
            where TCommand : ICommand
        {
            IEventBinding<TSource, TTarget, EventArgs> eventBinding = new EventBinding<TSource, TTarget, EventArgs>(self, weakEventHandlerFactory);
            return new EventToCommandBinding<TSource, TTarget, EventArgs, TCommand>(eventBinding, commandExpression, canExecutePropertyExpression);
        }
    }
}
