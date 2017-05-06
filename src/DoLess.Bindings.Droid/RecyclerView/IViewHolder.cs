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
    public interface IViewHolder<T> :IView<T>
        where T : class
    {
        TView GetView<TView>(int resourceId)
            where TView : View;

        IBinding<T, TTarget> Bind<TTarget>(int resourceId)
            where TTarget : View;

        View ItemView { get;  }
    }
}