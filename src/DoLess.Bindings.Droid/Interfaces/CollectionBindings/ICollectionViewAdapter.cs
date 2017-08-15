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

namespace DoLess.Bindings.Interfaces.CollectionBindings
{
    public interface ICollectionViewAdapter<TItem>
    {
        /// <summary>
        /// Gets or sets the items source that will be used to populate the collection.
        /// </summary>
        IEnumerable<TItem> ItemsSource { get; set; }
    }
}