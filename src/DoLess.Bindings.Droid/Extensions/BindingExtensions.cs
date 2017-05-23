using Android.Views;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Windows.Input;

namespace DoLess.Bindings
{
    public static partial class BindingExtensions
    {
        internal static IEventToCommandBinding<TSource, TTarget, EventArgs<TItem>, TCommand> EventTo<TSource, TTarget, TCommand, TItem>(
            this IBinding<TSource, TTarget> self,
            Expression<Func<TSource, TCommand>> commandExpression,
            Func<TTarget, EventHandler<EventArgs<TItem>>, WeakEventHandler<TTarget, EventArgs<TItem>>> weakEventHandlerFactory,
            Expression<Func<TTarget, bool>> canExecutePropertyExpression = null)
            where TSource : class
            where TTarget : class, ICollectionViewAdapter<TItem>
            where TCommand : ICommand
            where TItem : class
        {
            IEventBinding<TSource, TTarget, EventArgs<TItem>> eventBinding = new EventBinding<TSource, TTarget, EventArgs<TItem>>(self, weakEventHandlerFactory);
            return new EventToCommandBinding<TSource, TTarget, EventArgs<TItem>, TCommand>(eventBinding, commandExpression, canExecutePropertyExpression);
        }

        public static IEventToCommandBinding<TSource, TTarget, EventArgs, TCommand> ViewClickTo<TSource, TTarget, TCommand>(
            this IBinding<TSource, TTarget> self,
            Expression<Func<TSource, TCommand>> commandExpression)
            where TSource : class
            where TTarget : View
            where TCommand : ICommand
        {
            return self.EventTo<TSource, TTarget, EventArgs, TCommand>(commandExpression, (s, e) => new ClickWeakEventHandler<TTarget>(s, e), b => b.Enabled);
        }

        public static IEventToCommandBinding<TSource, TTarget, EventArgs, TCommand> ViewLongClickTo<TSource, TTarget, TCommand>(
            this IBinding<TSource, TTarget> self,
            Expression<Func<TSource, TCommand>> commandExpression)
            where TSource : class
            where TTarget : View
            where TCommand : ICommand
        {
            return self.EventTo<TSource, TTarget, EventArgs, TCommand>(commandExpression, (s, e) => new LongClickWeakEventHandler<TTarget>(s, e));
        }

        public static IEventToCommandBinding<TSource, TTarget, EventArgs<TItem>, TCommand> ItemClickTo<TSource, TTarget, TItem, TCommand>(
            this IBinding<TSource, TTarget> self,
            Expression<Func<TSource, TCommand>> commandExpression)
            where TSource : class
            where TTarget : class, ICollectionViewAdapter<TItem>
            where TItem : class
            where TCommand : ICommand
        {
            return self.EventTo<TSource, TTarget, EventArgs<TItem>, TCommand>(commandExpression, (s, e) => new ItemClickWeakEventHandler<TTarget, TItem>(s, e));
        }

        public static IEventToCommandBinding<TSource, TTarget, EventArgs<TItem>, TCommand> ItemLongClickTo<TSource, TTarget, TItem, TCommand>(
            this IBinding<TSource, TTarget> self,
            Expression<Func<TSource, TCommand>> commandExpression)
            where TSource : class
            where TTarget : class, ICollectionViewAdapter<TItem>
            where TItem : class
            where TCommand : ICommand
        {
            return self.EventTo<TSource, TTarget, EventArgs<TItem>, TCommand>(commandExpression, (s, e) => new ItemLongClickWeakEventHandler<TTarget, TItem>(s, e));
        }

        public static IEventToCommandBinding<TSource, TTarget, EventArgs<TSubItem>, TCommand> SubItemClickTo<TSource, TTarget, TItem, TSubItem, TCommand>(
            this IBinding<TSource, TTarget> self,
            Expression<Func<TSource, TCommand>> commandExpression)
            where TSource : class
            where TTarget : class, ICollectionViewAdapter<TItem, TSubItem>
            where TItem : class, IEnumerable<TSubItem>
            where TSubItem : class
            where TCommand : ICommand
        {
            return self.EventTo<TSource, TTarget, EventArgs<TSubItem>, TCommand>(commandExpression, (s, e) => new SubItemClickWeakEventHandler<TTarget, TItem, TSubItem>(s, e));
        }

        public static IEventToCommandBinding<TSource, TTarget, EventArgs<TSubItem>, TCommand> SubItemLongClickTo<TSource, TTarget, TItem, TSubItem, TCommand>(
            this IBinding<TSource, TTarget> self,
            Expression<Func<TSource, TCommand>> commandExpression)
            where TSource : class
            where TTarget : class, ICollectionViewAdapter<TItem, TSubItem>
            where TItem : class, IEnumerable<TSubItem>
            where TSubItem : class
            where TCommand : ICommand
        {
            return self.EventTo<TSource, TTarget, EventArgs<TSubItem>, TCommand>(commandExpression, (s, e) => new SubItemLongClickWeakEventHandler<TTarget, TItem, TSubItem>(s, e));
        }

        public static IBinding<TSource, TTarget> ConfigureItem<TSource, TTarget, TItem>(
            this IBinding<TSource, TTarget> self,
            Action<IViewBinder<TItem>> configurator)
            where TSource : class
            where TTarget : class, ICollectionViewAdapter<TItem>
            where TItem : class
        {
            configurator?.Invoke(self.Target?.ItemBinder);
            return self;
        }

        public static IBinding<TSource, TTarget> ConfigureSubItem<TSource, TTarget, TItem, TSubItem>(
            this IBinding<TSource, TTarget> self,
            Action<IViewBinder<TSubItem>> configurator)
            where TSource : class
            where TTarget : class, ICollectionViewAdapter<TItem, TSubItem>
            where TItem : class, IEnumerable<TSubItem>
            where TSubItem : class
        {
            configurator?.Invoke(self.Target?.SubItemBinder);
            return self;
        }
    }
}
