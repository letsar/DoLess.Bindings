﻿using System;
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
    public static class IBindableViewExtensions
    {
        public static IBinder<TViewModel> Setup<TViewModel>(this IBindableView<TViewModel> self, TViewModel vm)
            where TViewModel : class
        {
            self.Binder = new Binder<TViewModel>(vm, self);
            return self.Binder;
        }
    }
}