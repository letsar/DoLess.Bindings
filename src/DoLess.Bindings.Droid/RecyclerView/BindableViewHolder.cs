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
        IViewHolder
    {
        private readonly Dictionary<int, View> views;

        public BindableViewHolder(View itemView) : base(itemView)
        {
            this.views = new Dictionary<int, View>();
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

        public IBinding Binding { get; set; }

        public void Unbind()
        {
            if (this.Binding != null)
            {
                // FIX: There is an binding deleted when unbind.
                this.Binding.Unbind();
                this.Binding = null;
            }
        }
    }
}