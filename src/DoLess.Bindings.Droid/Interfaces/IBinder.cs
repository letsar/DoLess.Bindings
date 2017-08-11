using System;
using Android.Views;

namespace DoLess.Bindings
{
    /// <summary>
    /// Represents an object that holds a view model and allows to binds it.
    /// </summary>
    public partial interface IBinder<TViewModel>
    {
        IBinding<TViewModel, TView> Bind<TView>(int id)
            where TView : View;
    }
}
