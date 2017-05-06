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
    public interface ICollectionBinding<TSource, TTarget, TItemProperty> :
        IBinding<TSource, TTarget>,
        ICanBind<TSource>
        where TSource : class
        where TTarget : class
        where TItemProperty : class
    {
        ICollectionBinding<TSource, TTarget, TItemProperty> WithItemTemplateSelector<T>()
            where T : IItemTemplateSelector<TItemProperty>, new();

        ICollectionBinding<TSource, TTarget, TItemProperty> WithItemTemplate(int resourceId);

        ICollectionBinding<TSource, TTarget, TItemProperty> BindItemTo(Func<IViewHolder<TItemProperty>, IBinding> itemBinder);

        IEventToCommandBinding<TSource, TTarget, EventArgs<TItemProperty>, TCommand> ItemClickTo<TCommand>(Expression<Func<TSource, TCommand>> commandExpression)
            where TCommand : ICommand;

        IEventToCommandBinding<TSource, TTarget, EventArgs<TItemProperty>, TCommand> ItemLongClickTo<TCommand>(Expression<Func<TSource, TCommand>> commandExpression)
            where TCommand : ICommand;
    }
}