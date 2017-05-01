using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace DoLess.Bindings.Droid.RecyclerView
{
    internal class BindableRecyclerViewAdapter<TItem> :
        Android.Support.V7.Widget.RecyclerView.Adapter,
        ICollectionBinding<TItem>
    {
        private IEnumerable<TItem> itemsSource;
        private IItemTemplateSelector<TItem> itemTemplateSelector;

        public BindableRecyclerViewAdapter()
        {

        }

        public override int ItemCount => this.itemsSource.Count();

        public IEnumerable<TItem> ItemsSource
        {
            get { return this.itemsSource; }
            set { this.SetItemsSource(value); }
        }

        public ICollectionBinding<TItem> WithItemTemplateSelector<T>()
            where T : IItemTemplateSelector<TItem>, new()
        {
            this.itemTemplateSelector = Cache<T>.Instance;
            return this;
        }

        public ICollectionBinding<TItem> WithItemTemplate(int resourceId)
        {
            this.itemTemplateSelector = new SingleItemTemplateSelector<TItem>(resourceId);
            return this;
        }

        public override void OnBindViewHolder(Android.Support.V7.Widget.RecyclerView.ViewHolder holder, int position)
        {
            throw new NotImplementedException();
        }

        public override Android.Support.V7.Widget.RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            throw new NotImplementedException();
        }

        private void Observe(INotifyCollectionChanged source)
        {
            if (source != null)
            {
                source.CollectionChanged += this.OnItemsSourceCollectionChanged;
            }
        }

        private void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    this.NotifyItemRangeInserted(e.NewStartingIndex, e.NewItems.Count);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    this.NotifyItemRangeRemoved(e.OldStartingIndex, e.OldItems.Count);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    this.NotifyItemRangeChanged(e.NewStartingIndex, e.NewItems.Count);
                    break;
                case NotifyCollectionChangedAction.Move:
                    for (var i = 0; i < e.NewItems.Count; i++)
                    {
                        this.NotifyItemMoved(e.OldStartingIndex + i, e.NewStartingIndex + i);
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    this.NotifyDataSetChanged();
                    break;
                default:
                    break;
            }
        }

        private void SetItemsSource(IEnumerable<TItem> itemsSource)
        {
            if (this.itemsSource != null)
            {
                this.Unobserve(this.itemsSource as INotifyCollectionChanged);
            }

            this.itemsSource = itemsSource;
            this.Observe(this.itemsSource as INotifyCollectionChanged);
            this.NotifyDataSetChanged();
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