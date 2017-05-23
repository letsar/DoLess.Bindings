using Android.Views;
using Android.Widget;
using System.Collections.Generic;

namespace DoLess.Bindings
{
    internal class BindableBaseAdapter<TItem> :
        BaseAdapter<TItem>,
        ICollectionViewAdapter<TItem>,        
        INotifyDataSetChanged
        where TItem : class
    {
        private readonly ViewBinder<TItem> itemBinder;
        private readonly ItemCollection<TItem> itemCollection;

        public BindableBaseAdapter()
        {
            this.itemCollection = new ItemCollection<TItem>(this);
            this.itemBinder = new ViewBinder<TItem>();
        }        

        public IViewBinder<TItem> ItemBinder => this.itemBinder;

        public IEnumerable<TItem> ItemsSource
        {
            get { return this.itemCollection.ItemsSource; }
            set { this.itemCollection.ItemsSource = value; }
        }

        public override int Count => this.itemCollection.Count;

        public override TItem this[int position] => this.itemCollection[position];

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            return this.itemBinder.GetView(this[position], convertView, parent);
        }
    }
}