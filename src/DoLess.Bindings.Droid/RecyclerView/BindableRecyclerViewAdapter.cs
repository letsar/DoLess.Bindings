using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace DoLess.Bindings
{
    internal class BindableRecyclerViewAdapter<TItem> :
        Android.Support.V7.Widget.RecyclerView.Adapter
    {
        private IEnumerable<TItem> itemsSource;

        public BindableRecyclerViewAdapter()
        {
        }

        public override int ItemCount => this.itemsSource.Count();

        public IItemTemplateSelector<TItem> ItemTemplateSelector { get; set; }

        public Func<TItem, IViewHolder, IBinding> ItemBinder { get; set; }

        public IEnumerable<TItem> ItemsSource
        {
            get { return this.itemsSource; }
            set { this.SetItemsSource(value); }
        }

        public override void OnBindViewHolder(Android.Support.V7.Widget.RecyclerView.ViewHolder holder, int position)
        {
            if (this.ItemBinder == null)
            {
                Bindings.LogError("there are no bindings defined for the items of this RecyclerView");
            }
            else
            {
                TItem viewModel = this.ItemsSource.ElementAtOrDefault(position);

                BindableViewHolder<TItem> viewHolder = holder as BindableViewHolder<TItem>;
                if (viewHolder != null)
                {
                    // Unbinds the previous bindings before setting the new one.
                    viewHolder.Unbind();
                    viewHolder.Binding = this.ItemBinder(viewModel, viewHolder);
                }
            }
        }

        public override Android.Support.V7.Widget.RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(viewType, parent, false);
            BindableViewHolder<TItem> viewHolder = new BindableViewHolder<TItem>(view);
            return viewHolder;
        }

        public override int GetItemViewType(int position)
        {
            // The view type will be the layout id.
            return this.ItemTemplateSelector.GetLayoutId(this.ItemsSource.ElementAt(position));
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