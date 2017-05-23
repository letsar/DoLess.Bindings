using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Input;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace DoLess.Bindings
{
    public static class ViewExtensions
    {
        internal static IBindableView<TSource> CreateBindableView<TSource>(this View self, TSource viewModel)
            where TSource : class
        {
            return new BindableView<TSource>(self, viewModel);
        }


    }
}