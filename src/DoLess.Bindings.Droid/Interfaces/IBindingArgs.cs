using System;
using System.Collections.Generic;
using System.Text;
using Android.Views;

namespace DoLess.Bindings
{
    public partial interface IBindingArgs
    {
        TView FindViewById<TView>(int id) where TView : View;
    }
}
