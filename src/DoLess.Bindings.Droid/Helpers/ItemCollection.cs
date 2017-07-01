using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace DoLess.Bindings
{
    internal class ItemCollection<TItem>
        where TItem : class
    {
        private readonly INotifyDataSetChanged notifier;
        private readonly INotifyItemChanged notifier2;

        private IEnumerable<TItem> itemsSource;

        public ItemCollection(INotifyDataSetChanged owner)
        {
            this.notifier = owner;
            this.notifier2 = owner as INotifyItemChanged;
        }

        public IEnumerable<TItem> ItemsSource
        {
            get { return this.itemsSource; }
            set { this.SetItemsSource(value); }
        }

        public TItem this[int position] => this.ItemsSource.ElementAtOrDefault(position);

        public int Count => (this.ItemsSource?.Count()).GetValueOrDefault();

        private void Observe(INotifyCollectionChanged source)
        {
            if (source != null)
            {
                source.CollectionChanged += this.OnItemsSourceCollectionChanged;
            }
        }

        private void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            try
            {
                if (this.notifier2 != null)
                {
                    switch (e.Action)
                    {
                        case NotifyCollectionChangedAction.Add:
                            this.notifier2.NotifyItemRangeInserted(e.NewStartingIndex, e.NewItems.Count);
                            break;
                        case NotifyCollectionChangedAction.Remove:
                            this.notifier2.NotifyItemRangeRemoved(e.OldStartingIndex, e.OldItems.Count);
                            break;
                        case NotifyCollectionChangedAction.Replace:
                            this.notifier2.NotifyItemRangeChanged(e.NewStartingIndex, e.NewItems.Count);
                            break;
                        case NotifyCollectionChangedAction.Move:
                            for (var i = 0; i < e.NewItems.Count; i++)
                            {
                                this.notifier2.NotifyItemMoved(e.OldStartingIndex + i, e.NewStartingIndex + i);
                            }
                            break;
                        case NotifyCollectionChangedAction.Reset:
                            this.notifier2.NotifyDataSetChanged();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    this.notifier.NotifyDataSetChanged();
                }
            }
            catch (Exception ex)
            {
                Bindings.LogError($"notification from the adapter failed. Are you trying to update your collection from a background task? Details:{ex.ToString()}");
            }
        }

        private void SetItemsSource(IEnumerable<TItem> itemsSource)
        {
            if (this.itemsSource != null)
            {
                this.Unobserve(this.itemsSource as INotifyCollectionChanged);
            }

            this.itemsSource = itemsSource;

            if (this.itemsSource != null && !(this.itemsSource is IList<TItem>))
            {
                Bindings.LogWarning("you are currently binding to an IEnumerable - this can be inefficient, especially for large collections. Binding to IList<TItem> is more efficient.");
            }

            this.Observe(this.itemsSource as INotifyCollectionChanged);
            this.notifier.NotifyDataSetChanged();
        }

        private void Unobserve(INotifyCollectionChanged source)
        {
            if (source != null)
            {
                source.CollectionChanged -= this.OnItemsSourceCollectionChanged;
            }
        }  
    }
}