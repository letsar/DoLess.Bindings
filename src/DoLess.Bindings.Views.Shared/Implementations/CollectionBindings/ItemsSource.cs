using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace DoLess.Bindings
{
    internal class ItemsSource<T>
    {
        private readonly INotifyDataChanged dataChangedNotifier;
        private readonly INotifyItemChanged itemChangedNotifier;
        private IEnumerable<T> items;

        public ItemsSource(INotifyDataChanged notifier)
        {
            this.dataChangedNotifier = notifier;
            this.itemChangedNotifier = notifier as INotifyItemChanged;
        }

        public IEnumerable<T> Items
        {
            get => this.items;
            set => this.SetItems(value);
        }

        public int Count => (this.items?.Count()).GetValueOrDefault();

        public T this[int position] => this.items == null ? default(T) : this.items.ElementAtOrDefault(position);

        private void SetItems(IEnumerable<T> value)
        {
            this.RemoveCollectionChangedHandler();
            this.items = value;
            this.AddCollectionChangedHandler();

            this.dataChangedNotifier.NotifyDataSetChanged();
            if (this.items != null && !(this.items is IList<T>))
            {
                Bindings.LogWarning("you are currently binding to an IEnumerable<T> - this can be inefficient, especially for large collections. Binding to IList<T> is more efficient.");
            }
        }

        private void AddCollectionChangedHandler()
        {
            var collection = this.items as INotifyCollectionChanged;
            if (collection != null)
            {
                collection.CollectionChanged += this.OnCollectionChanged;
            }
        }

        private void RemoveCollectionChangedHandler()
        {
            var collection = this.items as INotifyCollectionChanged;
            if (collection != null)
            {
                collection.CollectionChanged -= this.OnCollectionChanged;
            }
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            try
            {
                if (this.itemChangedNotifier != null)
                {
                    switch (e.Action)
                    {
                        case NotifyCollectionChangedAction.Add:
                            this.itemChangedNotifier.NotifyItemRangeInserted(e.NewStartingIndex, e.NewItems.Count);
                            break;
                        case NotifyCollectionChangedAction.Remove:
                            this.itemChangedNotifier.NotifyItemRangeRemoved(e.OldStartingIndex, e.OldItems.Count);
                            break;
                        case NotifyCollectionChangedAction.Replace:
                            this.itemChangedNotifier.NotifyItemRangeChanged(e.NewStartingIndex, e.NewItems.Count);
                            break;
                        case NotifyCollectionChangedAction.Move:
                            for (var i = 0; i < e.NewItems.Count; i++)
                            {
                                this.itemChangedNotifier.NotifyItemMoved(e.OldStartingIndex + i, e.NewStartingIndex + i);
                            }
                            break;
                        case NotifyCollectionChangedAction.Reset:
                            this.itemChangedNotifier.NotifyDataSetChanged();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    this.dataChangedNotifier.NotifyDataSetChanged();
                }
            }
            catch (Exception ex)
            {
                Bindings.LogError($"notification from the adapter failed. Are you trying to update your collection from a background task?", ex);
            }
        }
    }
}