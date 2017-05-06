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
    internal class BindableViewHolder<T> :
        Android.Support.V7.Widget.RecyclerView.ViewHolder,
        IViewHolder<T>
        where T : class
    {
        private readonly Dictionary<int, View> views;
        private readonly WeakReference<T> weakViewModel;

        public event EventHandler<EventArgs<T>> Click;
        public event EventHandler<EventArgs<T>> LongClick;

        public BindableViewHolder(View itemView) : base(itemView)
        {
            this.views = new Dictionary<int, View>();
            this.weakViewModel = new WeakReference<T>(null);

            this.ItemView.Click += this.OnItemViewClick;
            this.ItemView.LongClick += this.OnItemViewLongClick;
        }

        private void OnItemViewLongClick(object sender, View.LongClickEventArgs e)
        {
            this.LongClick?.Invoke(this, new EventArgs<T>(this.ViewModel));
        }

        private void OnItemViewClick(object sender, EventArgs e)
        {
            this.Click?.Invoke(this, new EventArgs<T>(this.ViewModel));
        }

        public IBinding<T, TTarget> Bind<TTarget>(int resourceId)
            where TTarget : View
        {
            return new Binding<T, TTarget>(this, this.GetView<TTarget>(resourceId), null);
        }

        public TView GetView<TView>(int resourceId)
            where TView : View
        {
            View view = null;
            if (!this.views.TryGetValue(resourceId, out view))
            {
                view = this.ItemView.FindViewById(resourceId);
                this.views[resourceId] = view;
            }
            return view as TView;
        }

        public T ViewModel
        {
            get { return this.weakViewModel.GetOrDefault(); }
            set { this.weakViewModel.SetTarget(value); }
        }

        public IBinding Binding { get; set; }

        public void Unbind()
        {
            if (this.Binding != null)
            {
                this.Binding.Unbind();
                this.Binding = null;

                this.ItemView.Click -= this.OnItemViewClick;
                this.ItemView.LongClick -= this.OnItemViewLongClick;
            }            
        }
    }
}