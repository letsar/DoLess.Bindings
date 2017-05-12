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
    public interface ICollectionViewAdapter<TItem>
        where TItem : class
    {
        event EventHandler<EventArgs<TItem>> ItemClick;
        event EventHandler<EventArgs<TItem>> ItemLongClick;

        IEnumerable<TItem> ItemsSource { get; set; }
    }
}