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

namespace DoLess.Bindings.Droid.RecyclerView
{
    internal class BindableViewHolder<T>
    {
        private readonly Dictionary<int, View> views;

        public BindableViewHolder()
        {
            this.views = new Dictionary<int, View>();
        }


        public TView GetView<TView>(int resourceId, View parent)
            where TView : View
        {
            View view = null;
            if (!this.views.TryGetValue(resourceId, out view))
            {
                view = parent.FindViewById(resourceId);
                this.views[resourceId] = view;
            }
            return view as TView;
        }

        public void Bind()
        {

        }

        public void Unbind()
        {

        }
    }
}