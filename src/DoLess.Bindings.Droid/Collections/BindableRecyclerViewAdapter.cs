using Android.Views;
using System.Collections.Generic;

namespace DoLess.Bindings
{
    internal class BindableRecyclerViewAdapter<TItem> :
        Android.Support.V7.Widget.RecyclerView.Adapter,
        ICollectionViewAdapter<TItem>,
        INotifyItemChanged
        where TItem : class
    {
        private readonly ViewBinder<TItem> itemBinder;
        private readonly ItemCollection<TItem> itemCollection;

        public BindableRecyclerViewAdapter()
        {
            this.itemCollection = new ItemCollection<TItem>(this);
            this.itemBinder = new ViewBinder<TItem>();
        }

        public IViewBinder<TItem> ItemBinder => this.itemBinder;

        public override int ItemCount => this.itemCollection.Count;

        public IEnumerable<TItem> ItemsSource
        {
            get { return this.itemCollection.ItemsSource; }
            set { this.itemCollection.ItemsSource = value; }
        }

        public TItem this[int position] => this.itemCollection[position];

        public override int GetItemViewType(int position)
        {
            return this.itemBinder.GetLayoutId(this[position]);
        }

        public override void OnBindViewHolder(Android.Support.V7.Widget.RecyclerView.ViewHolder holder, int position)
        {
            this.itemBinder.BindViewHolder(holder as BindableViewHolder<TItem>, this[position]);
        }

        public override Android.Support.V7.Widget.RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            return this.itemBinder.CreateViewHolder(parent, viewType);
        }

        public override void OnViewRecycled(Java.Lang.Object holder)
        {
            this.itemBinder.RecycleViewHolder(holder);
            base.OnViewRecycled(holder);
        }
    }
}