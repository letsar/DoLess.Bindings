using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Windows.Input;

namespace DoLess.Bindings
{
    internal class CollectionBinding<TSource, TTarget, TItem> :
        OneWayPropertyBinding<TSource, TTarget, IEnumerable<TItem>, IEnumerable<TItem>>,
        ICollectionBinding<TSource, TTarget, TItem>
        where TSource : class
        where TTarget : class, ICollectionViewAdapter<TItem>
        where TItem : class
    {
        public CollectionBinding(IPropertyBinding<TSource, TTarget, IEnumerable<TItem>> propertyBinding, Expression<Func<TSource, IEnumerable<TItem>>> itemsSourcePropertyExpression) :
            base(propertyBinding, itemsSourcePropertyExpression)
        {
            this.WithConverter<IdentityConverter<IEnumerable<TItem>>>();
        }

        public ICollectionBinding<TSource, TTarget, TItem> ConfigureItem(Action<IViewBinder<TItem>> configurator)
        {
            var adapter = this.Target?.ItemBinder;
            if (adapter != null && configurator != null)
            {
                configurator(adapter);
            }
            return this;
        }

        public ICollectionBinding<TSource, TTarget, TItem> ItemClickTo<TCommand>(Expression<Func<TSource, TCommand>> commandExpression)
            where TCommand : ICommand
        {

            this.EventTo<TSource, TTarget, TCommand, TItem>(commandExpression, (s, e) => new ItemClickWeakEventHandler<TTarget, TItem>(s, e))
                .WithConverter<EventArgsConverter<EventArgs<TItem>, TItem>>();           
            
            return this;
        }

        public ICollectionBinding<TSource, TTarget, TItem> ItemLongClickTo<TCommand>(Expression<Func<TSource, TCommand>> commandExpression)
            where TCommand : ICommand
        {
            this.EventTo<TSource, TTarget, TCommand, TItem>(commandExpression, (s, e) => new ItemLongClickWeakEventHandler<TTarget, TItem>(s, e))
                .WithConverter<EventArgsConverter<EventArgs<TItem>, TItem>>();

            return this;
        }
    }
}