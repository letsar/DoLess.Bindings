using System.Collections.Generic;
using Android.App;
using Android.Views;

namespace DoLess.Bindings
{
    /// <summary>
    /// Represents an object that holds a view model and allows to binds it.
    /// </summary>
    internal partial class Binder<TViewModel> : IBinder<TViewModel>
    {
        private Dictionary<int, View> cachedViews;
        private View rootView;

        public Binder(TViewModel viewModel, IBindableView<TViewModel> bindableView) :
            this(viewModel)
        {
            this.rootView = GetRootView(bindableView);
            this.cachedViews = new Dictionary<int, View>();
        }

        public TView FindViewById<TView>(int id)
            where TView : View
        {
            if (!this.cachedViews.TryGetValue(id, out View view))
            {
                view = this.rootView?.FindViewById(id);
                this.cachedViews[id] = view;
            }

            return view as TView;
        }

        public IBinding<TViewModel, TView> Bind<TView>(int id)
            where TView : View
        {
            var view = this.FindViewById<TView>(id);
            return this.Bind(view);
        }

        private static View GetRootView(IBindableView<TViewModel> bindableView)
        {
            switch (bindableView)
            {
                case Activity x:
                    return x.Window.DecorView;
                case Fragment x:
                    return x.View;
                case Android.Support.V7.Widget.RecyclerView.ViewHolder x:
                    return x.ItemView;
                case View x:
                    return x;
                default:
                    return null;
            }
        }

        partial void InternalDispose()
        {
            this.rootView = null;
            this.cachedViews = null;
        }
    }
}
