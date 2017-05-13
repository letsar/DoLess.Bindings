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

        /// <summary>
        /// Gets or sets the items source that will be used to populate the collection.
        /// </summary>
        IEnumerable<TItem> ItemsSource { get; set; }

        /// <summary>
        /// Sets the <see cref="IItemTemplateSelector{TItem}"/> that will be used to render the items.
        /// </summary>
        /// <typeparam name="T">The type of the ItemTemplateSelector</typeparam>
        /// <returns></returns>
        ICollectionViewAdapter<TItem> WithItemTemplateSelector<T>()
            where T : IItemTemplateSelector<TItem>, new();

        /// <summary>
        /// Sets the layout that will be used to render all the items.
        /// </summary>
        /// <param name="resourceId">The id of the layout.</param>
        /// <returns></returns>
        ICollectionViewAdapter<TItem> WithItemTemplate(int resourceId);

        /// <summary>
        /// Sets the function used to bind the data to the items.
        /// </summary>
        /// <param name="binder">The function used to bind</param>
        /// <returns></returns>
        ICollectionViewAdapter<TItem> BindItemTo(Func<IViewHolder<TItem>, IBinding> binder);
    }
}