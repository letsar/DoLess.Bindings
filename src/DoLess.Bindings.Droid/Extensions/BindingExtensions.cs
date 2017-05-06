using System;
using System.Linq.Expressions;
using System.Windows.Input;

namespace DoLess.Bindings
{
    public static partial class BindingExtensions
    {
        internal static IEventToCommandBinding<TSource, TItemTarget, EventArgs<TItemProperty>, TCommand> EventTo<TSource, TItemTarget, TCommand, TItemProperty>(this IBinding<TSource, TItemTarget> self, Expression<Func<TSource, TCommand>> commandExpression, Func<TItemTarget, EventHandler<EventArgs<TItemProperty>>, WeakEventHandler<TItemTarget, EventArgs<TItemProperty>>> weakEventHandlerFactory, Expression<Func<TItemTarget, bool>> canExecutePropertyExpression = null)
            where TSource : class
            where TItemTarget : BindableRecyclerViewAdapter<TItemProperty>
            where TCommand : ICommand
            where TItemProperty : class
        {
            IEventBinding<TSource, TItemTarget, EventArgs<TItemProperty>> eventBinding = new EventBinding<TSource, TItemTarget, EventArgs<TItemProperty>>(self, weakEventHandlerFactory);
            return new EventToCommandBinding<TSource, TItemTarget, EventArgs<TItemProperty>, TCommand>(eventBinding, commandExpression, canExecutePropertyExpression);
        }
    }
}
