using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace DoLess.Bindings.Implementations.CollectionBindings
{
    internal class BindableRecyclerViewAdapter<TSourceItem> :
        RecyclerView.Adapter,
        INotifyItemChanged
    {
        private readonly ItemsSource<TSourceItem> itemsSource;

        public BindableRecyclerViewAdapter()
        {
            this.itemsSource = new ItemsSource<TSourceItem>(this);
        }

        public override int ItemCount => this.itemsSource.Count;

        public TSourceItem this[int position] => this.itemsSource[position];

        public IEnumerable<TSourceItem> ItemsSource
        {
            get => this.itemsSource.Items;
            set => this.itemsSource.Items = value;
        }         

        public override int GetItemViewType(int position)
        {
            return base.GetItemViewType(position);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            throw new NotImplementedException();
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            throw new NotImplementedException();
        }

        public override void OnViewRecycled(Java.Lang.Object holder)
        {
            base.OnViewRecycled(holder);
        }
    }
}