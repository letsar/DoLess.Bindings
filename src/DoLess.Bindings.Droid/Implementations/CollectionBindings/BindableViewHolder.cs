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

namespace DoLess.Bindings
{
    internal class BindableViewHolder<TViewModel> :
        RecyclerView.ViewHolder,
        IBindableView<TViewModel>
        where TViewModel : class
    {
        public BindableViewHolder(View itemView) : base(itemView)
        {

        }

        public event EventHandler<ItemEventArgs<TViewModel>> Click;
        public event EventHandler<ItemEventArgs<TViewModel>> LongClick;

        public IBinder<TViewModel> Binder { get; set; }

        private void AddItemViewHandlers()
        {
            this.ItemView.Click += this.OnItemViewClick;
            this.ItemView.LongClick += this.OnItemViewLongClick;
        }

        private void RemoveItemViewHandlers()
        {
            this.ItemView.Click -= this.OnItemViewClick;
            this.ItemView.LongClick -= this.OnItemViewLongClick;
        }

        private void OnItemViewLongClick(object sender, View.LongClickEventArgs e)
        {
            this.Click?.Invoke(this, new ItemEventArgs<TViewModel>(this.Binder.ViewModel));
        }

        private void OnItemViewClick(object sender, EventArgs e)
        {
            this.LongClick?.Invoke(this, new ItemEventArgs<TViewModel>(this.Binder.ViewModel));
        }

    }
}