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
    internal class BindableViewHolder<TViewModel> :
        Android.Support.V7.Widget.RecyclerView.ViewHolder,
        IBindableView<TViewModel>
        where TViewModel : class
    {
        private readonly Dictionary<int, View> views;
        private readonly WeakReference<TViewModel> weakViewModel;

        public event EventHandler<EventArgs<TViewModel>> Click;
        public event EventHandler<EventArgs<TViewModel>> LongClick;

        public View View => this.ItemView;

        public BindableViewHolder(View itemView) : base(itemView)
        {
            this.views = new Dictionary<int, View>();
            this.weakViewModel = new WeakReference<TViewModel>(null);
        }

        private void OnItemViewLongClick(object sender, View.LongClickEventArgs e)
        {
            this.LongClick?.Invoke(this, new EventArgs<TViewModel>(this.ViewModel));
        }

        private void OnItemViewClick(object sender, EventArgs e)
        {
            this.Click?.Invoke(this, new EventArgs<TViewModel>(this.ViewModel));
        }

        public IBinding<TViewModel, TTarget> Bind<TTarget>(int resourceId)
            where TTarget : View
        {
            return Binding<TViewModel, TTarget>.CreateFromBindableView(this, this.GetView<TTarget>(resourceId));
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

        public TViewModel ViewModel
        {
            get { return this.weakViewModel.GetOrDefault(); }
            set { this.weakViewModel.SetTarget(value); }
        }

        public IBinding Binding { get; set; }


        public void BindEvents()
        {
            this.ItemView.Click += this.OnItemViewClick;
            this.ItemView.LongClick += this.OnItemViewLongClick;
        }

        public void UnbindEvents()
        {
            this.ItemView.Click -= this.OnItemViewClick;
            this.ItemView.LongClick -= this.OnItemViewLongClick;
        }

        public void Unbind()
        {
            if (this.Binding != null)
            {
                this.Binding.Unbind();
                this.Binding = null;
            }
        }
    }
}