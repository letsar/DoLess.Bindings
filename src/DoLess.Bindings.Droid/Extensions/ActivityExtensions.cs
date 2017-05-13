using Android.App;
using System;
using System.Linq.Expressions;
using System.Windows.Input;

namespace DoLess.Bindings
{
    public static class ActivityExtensions
    {
        public static IBindableView<TSource> CreateBindableView<TSource>(this Activity self, TSource viewModel)
            where TSource : class
        {
            return new BindableView<TSource>(self.Window.DecorView, viewModel);
        }
    }
}
