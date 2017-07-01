using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Windows.Input;

namespace DoLess.Bindings
{
    internal class CollectionBinding<TSource, TTarget, TItem, TSubItem> :
        CollectionBinding<TSource, TTarget, TItem>,
        ICollectionBinding<TSource, TTarget, TItem, TSubItem>
        where TSource : class
        where TTarget : class, ICollectionViewAdapter<TItem, TSubItem>
        where TItem : class, IEnumerable<TSubItem>
        where TSubItem : class
    {
        public CollectionBinding(IPropertyBinding<TSource, TTarget, IEnumerable<TItem>> propertyBinding, Expression<Func<TSource, IEnumerable<TItem>>> itemsSourcePropertyExpression) :
            base(propertyBinding, itemsSourcePropertyExpression)
        {
            this.WithConverter<IdentityConverter<IEnumerable<TItem>>>();
        }

        public ICollectionBinding<TSource, TTarget, TItem, TSubItem> ConfigureSubItem(Action<IViewBinder<TSubItem>> configurator)
        {
            var adapter = this.Target?.SubItemBinder;
            if (adapter != null && configurator != null)
            {
                configurator(adapter);
            }
            return this;
        }

        public ICollectionBinding<TSource, TTarget, TItem, TSubItem> SubItemClickTo<TCommand>(Expression<Func<TSource, TCommand>> commandExpression)
            where TCommand : ICommand
        {
            this.EventTo<TSource, TTarget, EventArgs<TSubItem>, TCommand>(commandExpression, (s, e) => new SubItemClickWeakEventHandler<TTarget, TItem, TSubItem>(s, e))
                .WithConverter<EventArgsConverter<EventArgs<TSubItem>, TSubItem>>();

            return this;
        }

        public ICollectionBinding<TSource, TTarget, TItem, TSubItem> SubItemLongClickTo<TCommand>(Expression<Func<TSource, TCommand>> commandExpression)
            where TCommand : ICommand
        {
            this.EventTo<TSource, TTarget, EventArgs<TSubItem>, TCommand>(commandExpression, (s, e) => new SubItemLongClickWeakEventHandler<TTarget, TItem, TSubItem>(s, e))
                .WithConverter<EventArgsConverter<EventArgs<TSubItem>, TSubItem>>();

            return this;
        }
    }
}