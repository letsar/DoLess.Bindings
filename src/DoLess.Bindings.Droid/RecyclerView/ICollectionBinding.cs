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

namespace DoLess.Bindings
{
    public interface ICollectionBinding<TSource, TItemProperty> : 
        IBinding,
        ICanBind<TSource>
        where TSource : class
    {
        ICollectionBinding<TSource, TItemProperty> WithItemTemplateSelector<T>() 
            where T : IItemTemplateSelector<TItemProperty>, new();

        ICollectionBinding<TSource, TItemProperty> WithItemTemplate(int resourceId);

        ICollectionBinding<TSource, TItemProperty> BindItemTo(Func<TItemProperty, IViewHolder, IBinding> itemBinder);
    }
}