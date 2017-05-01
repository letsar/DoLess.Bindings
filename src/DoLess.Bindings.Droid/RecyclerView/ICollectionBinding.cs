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
    interface ICollectionBinding<TItem>
    {
        IEnumerable<TItem> ItemsSource { get; set; }

        ICollectionBinding<TItem> WithItemTemplateSelector<T>() 
            where T : IItemTemplateSelector<TItem>, new();

        ICollectionBinding<TItem> WithItemTemplate(int resourceId);
    }
}