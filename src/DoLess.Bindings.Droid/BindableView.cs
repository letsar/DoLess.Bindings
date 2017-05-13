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
    internal class BindableView<TViewModel> :
        IBindableView<TViewModel>
        where TViewModel : class
    {
        private readonly WeakReference<View> weakView;
        private readonly WeakReference<TViewModel> weakViewModel;

        public BindableView(View view, TViewModel viewModel)
        {
            this.weakView = new WeakReference<View>(view);
            this.weakViewModel = new WeakReference<TViewModel>(viewModel);
        }
        public TViewModel ViewModel => this.weakViewModel.GetOrDefault();

        public View View => this.weakView.GetOrDefault();

        public IBinding<TViewModel, TTarget> Bind<TTarget>(int resourceId)
                    where TTarget : View
        {
            return Binding<TViewModel, TTarget>.CreateFromBindableView(this, this.GetView<TTarget>(resourceId));
        }

        public TView GetView<TView>(int resourceId)
            where TView : View
        {
            return this.View?.FindViewById<TView>(resourceId);
        }
    }
}