using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
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

        IEventToCommandBinding<TSource, TTarget, EventArgs<TItem>, TCommand> ItemClickTo<TCommand>(Expression<Func<TSource, TCommand>> commandExpression)
            where TCommand : ICommand;

        IEventToCommandBinding<TSource, TTarget, EventArgs<TItem>, TCommand> ItemLongClickTo<TCommand>(Expression<Func<TSource, TCommand>> commandExpression)
            where TCommand : ICommand;        
    }
}