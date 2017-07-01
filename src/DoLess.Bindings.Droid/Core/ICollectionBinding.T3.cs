using System;
using System.Linq.Expressions;
using System.Windows.Input;

namespace DoLess.Bindings
{
    public interface ICollectionBinding<TSource, TTarget, TItem> :
        IBinding,
        ICanBind<TSource>
        where TSource : class
        where TTarget : class
        where TItem : class
    {
        ICollectionBinding<TSource, TTarget, TItem> ConfigureItem(Action<IViewBinder<TItem>> configurator);

        ICollectionBinding<TSource, TTarget, TItem> ItemClickTo<TCommand>(Expression<Func<TSource, TCommand>> commandExpression)
            where TCommand : ICommand;

        ICollectionBinding<TSource, TTarget, TItem> ItemLongClickTo<TCommand>(Expression<Func<TSource, TCommand>> commandExpression)
            where TCommand : ICommand;        
    }
}