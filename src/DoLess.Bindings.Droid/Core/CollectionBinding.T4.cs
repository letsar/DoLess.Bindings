using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Windows.Input;

namespace DoLess.Bindings
{
    //internal class CollectionBinding<TSource, TTarget, TItem, TSubItem> :
    //    CollectionBinding<TSource, TTarget, TItem>,
    //    ICollectionBinding<TSource, TTarget, TItem, TSubItem>
    //    where TSource : class
    //    where TTarget : class, IBindableAdapter<TItem, TSubItem>
    //    where TItem : class, IEnumerable<TSubItem>
    //    where TSubItem : class
    //{
    //    public CollectionBinding(IPropertyBinding<TSource, TTarget, IEnumerable<TItem>> propertyBinding, Expression<Func<TSource, IEnumerable<TItem>>> itemsSourcePropertyExpression) :
    //        base(propertyBinding, itemsSourcePropertyExpression)
    //    {
    //        this.WithConverter<IdentityConverter<IEnumerable<TItem>>>();
    //    }

    //    public ICollectionBinding<TSource, TTarget, TItem, TSubItem> ConfigureSubItem(Action<IViewBinder<TSubItem>> configurator)
    //    {
    //        var adapter = this.Target?.SubItemViewBinder;
    //        if (adapter != null && configurator != null)
    //        {
    //            configurator(adapter);
    //        }
    //        return this;
    //    }        

    //    public IEventToCommandBinding<TSource, TTarget, EventArgs<TSubItem>, TCommand> SubItemClickTo<TCommand>(Expression<Func<TSource, TCommand>> commandExpression)
    //        where TCommand : ICommand
    //    {
    //        return this.EventTo<TSource, TTarget, TCommand, TSubItem>(commandExpression, (s, e) => new SubItemClickWeakEventHandler<TTarget,TItem, TSubItem>(s, e))
    //                   .WithConverter<EventArgsConverter<EventArgs<TSubItem>, TSubItem>>();
    //    }

    //    public IEventToCommandBinding<TSource, TTarget, EventArgs<TSubItem>, TCommand> SubItemLongClickTo<TCommand>(Expression<Func<TSource, TCommand>> commandExpression)
    //        where TCommand : ICommand
    //    {
    //        return this.EventTo<TSource, TTarget, TCommand, TSubItem>(commandExpression, (s, e) => new SubItemLongClickWeakEventHandler<TTarget, TItem, TSubItem>(s, e))
    //                   .WithConverter<EventArgsConverter<EventArgs<TSubItem>, TSubItem>>();
    //    }
    //}
}