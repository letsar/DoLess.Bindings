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
using Java.Lang;

namespace DoLess.Bindings
{
    internal class BindableBaseAdapter<TItem> :
        BaseAdapter<TItem>,
        IBindableAdapter<TItem>,
        INotifyDataSetChanged
        where TItem : class
    {
        private readonly CollectionViewAdapter<TItem> collectionViewAdapter;

        public BindableBaseAdapter()
        {
            this.collectionViewAdapter = new CollectionViewAdapter<TItem>(this);
        }

        public ICollectionViewAdapter<TItem> CollectionViewAdapter => this.collectionViewAdapter;

        public override int Count => this.collectionViewAdapter.ItemCount;

        public override TItem this[int position] => this.collectionViewAdapter[position];

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            return this.collectionViewAdapter.GetView(position, convertView, parent);
        }
    }
}