using System;
using System.ComponentModel;
using Android.App;
using Android.Views;

namespace DoLess.Bindings
{
    /// <summary>
    /// Represents an object that holds a view model and allows to binds it.
    /// </summary>
    internal partial class Binder<TViewModel> : IBinder<TViewModel>
    {
        private View rootView;

        public Binder(TViewModel viewModel, IBindableView<TViewModel> bindableView) :
            this(viewModel)
        {
            this.rootView = GetRootView(bindableView);
        }

        public TView FindViewById<TView>(int id)
            where TView : View
        {
            return this.rootView?.FindViewById<TView>(id);
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
                default:
                    return null;
            }
        }

        partial void InternalDispose()
        {
            this.rootView = null;
        }
    }
}
