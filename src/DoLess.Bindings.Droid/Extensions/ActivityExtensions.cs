using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public static class ActivityExtensions
    {
        public static IBinding<TViewModel, TView> Bind<TViewModel, TView>(this IBindableView<TViewModel> self, TView view, BindingArgs args = null)
            where TViewModel : class
            where TView : View
        {
            return self.Binder.Bind(view, args);
        }
    }
}