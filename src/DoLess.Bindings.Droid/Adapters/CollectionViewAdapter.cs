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

namespace DoLess.Bindings
{
    /// <summary>
    /// Represents the adapter used for all collection views (RecycleView, ListView, etc.)
    /// </summary>
    internal class CollectionViewAdapter<TItem> :
        ICollectionViewAdapter<TItem>
        where TItem : class
    {
        private readonly INotifyDataSetChanged adapter;
        private readonly ItemCollection<TItem> itemCollection;

        public CollectionViewAdapter(INotifyDataSetChanged adapter)
        {
            this.adapter = adapter;
            this.itemCollection = new ItemCollection<TItem>(adapter);
        }

        public event EventHandler<EventArgs<TItem>> ItemClick;
        public event EventHandler<EventArgs<TItem>> ItemLongClick;

        public TItem this[int position] => this.itemCollection.ItemsSource.ElementAtOrDefault(position);

        public int ItemCount => this.itemCollection.ItemsSource.Count();

        public IItemTemplateSelector<TItem> ItemTemplateSelector { get; private set; }
                

        public Func<IBindableView<TItem>, IBinding> ItemBinder { get; private set; }

        public IEnumerable<TItem> ItemsSource
        {
            get { return this.itemCollection.ItemsSource; }
            set { this.itemCollection.ItemsSource = value; }
        }

        public ICollectionViewAdapter<TItem> WithItemTemplateSelector<T>()
            where T : IItemTemplateSelector<TItem>, new()
        {
            this.ItemTemplateSelector = Cache<T>.Instance;
            return this;
        }

        public ICollectionViewAdapter<TItem> WithItemTemplate(int resourceId)
        {
            this.ItemTemplateSelector = new SingleItemTemplateSelector<TItem>(resourceId);
            return this;
        }

        public ICollectionViewAdapter<TItem> BindItemTo(Func<IBindableView<TItem>, IBinding> binder)
        {
            this.ItemBinder = binder;
            return this;
        }


        public long GetItemId(int position)
        {
            throw new NotImplementedException();
        }

        public View GetView(int position, View convertView, ViewGroup parent)
        {
            BindableViewHolder<TItem> viewHolder = null;

            if (convertView != null)
            {
                viewHolder = convertView.Tag as BindableViewHolder<TItem>;
            }

            if (viewHolder == null)
            {
                viewHolder = this.CreateViewHolder(parent, this.GetItemViewType(position));
                convertView.Tag = viewHolder;
            }

            this.BindViewHolder(viewHolder, position);
            return convertView;
        }

        public void BindViewHolder(BindableViewHolder<TItem> viewHolder, int position)
        {
            if (this.ItemBinder == null)
            {
                Bindings.LogError("there are no bindings defined for the items of this adapter");
            }
            else
            {
                TItem viewModel = this[position];

                if (viewHolder != null)
                {
                    // Unbinds the previous bindings before setting the new one.
                    viewHolder.Unbind();

                    viewHolder.ViewModel = viewModel;
                    viewHolder.BindEvents();
                    viewHolder.Binding = this.ItemBinder(viewHolder);
                }
            }
        }

        public BindableViewHolder<TItem> CreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(viewType, parent, false);
            var viewHolder = new BindableViewHolder<TItem>(view);
            viewHolder.Click += this.OnViewHolderClick;
            viewHolder.LongClick += this.OnViewHolderLongClick;
            return viewHolder;
        }

        private void OnViewHolderLongClick(object sender, EventArgs<TItem> e)
        {
            this.ItemLongClick?.Invoke(this, e);
        }

        private void OnViewHolderClick(object sender, EventArgs<TItem> e)
        {
            this.ItemClick?.Invoke(this, e);
        }


        public void RecycleViewHolder(Java.Lang.Object holder)
        {
            var viewHolder = holder as BindableViewHolder<TItem>;
            if (viewHolder != null)
            {
                viewHolder.UnbindEvents();
            }
        }

        public int GetItemViewType(int position)
        {
            // The view type will be the layout id.
            return this.ItemTemplateSelector.GetLayoutId(this.ItemsSource.ElementAt(position));
        }
    }
}