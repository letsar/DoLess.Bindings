using System.Collections.Generic;

namespace DoLess.Bindings
{
    public interface ICollectionViewAdapter<TItem>
        where TItem : class
    {
        IViewBinder<TItem> ItemBinder { get; }

        /// <summary>
        /// Gets or sets the items source that will be used to populate the collection.
        /// </summary>
        IEnumerable<TItem> ItemsSource { get; set; }
    }
}