using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Windows.Input;

namespace DoLess.Bindings
{
    public interface ICollectionBinding<TSource, TTarget, TItem, TSubItem> :
        ICollectionBinding<TSource, TTarget, TItem>
        where TSource : class
        where TTarget : class
        where TItem : class, IEnumerable<TSubItem>
        where TSubItem : class
    {
        ICollectionBinding<TSource, TTarget, TItem, TSubItem> ConfigureSubItem(Action<IViewBinder<TSubItem>> configurator);

        ICollectionBinding<TSource, TTarget, TItem, TSubItem> SubItemClickTo<TCommand>(Expression<Func<TSource, TCommand>> commandExpression)
            where TCommand : ICommand;

        ICollectionBinding<TSource, TTarget, TItem, TSubItem> SubItemLongClickTo<TCommand>(Expression<Func<TSource, TCommand>> commandExpression)
            where TCommand : ICommand;
    }
}