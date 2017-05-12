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
using Java.Lang;

namespace DoLess.Bindings
{
    internal class BindableRecyclerViewAdapter<TItem> :
        Android.Support.V7.Widget.RecyclerView.Adapter,        
        IBindableAdapter<TItem>,
        INotifyItemChanged
        where TItem : class
    {        
        private readonly CollectionViewAdapter<TItem> collectionViewAdapter;        

        public BindableRecyclerViewAdapter()
        {
            this.collectionViewAdapter = new CollectionViewAdapter<TItem>(this);
        }

        public ICollectionViewAdapter<TItem> CollectionViewAdapter => this.collectionViewAdapter;

        public override int ItemCount => this.collectionViewAdapter.ItemCount;

        public override int GetItemViewType(int position)
        {
            return this.collectionViewAdapter.GetItemViewType(position);
        }

        public override void OnBindViewHolder(Android.Support.V7.Widget.RecyclerView.ViewHolder holder, int position)
        {
            this.collectionViewAdapter.BindViewHolder(holder as BindableViewHolder<TItem>, position);
        }

        public override Android.Support.V7.Widget.RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            return this.collectionViewAdapter.CreateViewHolder(parent, viewType);
        }       

        public override void OnViewRecycled(Java.Lang.Object holder)
        {
            this.collectionViewAdapter.RecycleViewHolder(holder);
            base.OnViewRecycled(holder);
        }
    }
}