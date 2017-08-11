using System;

namespace DoLess.Bindings
{
    /// <summary>
    /// Represents an object that holds a view model and allows to binds it.
    /// </summary>
    public partial interface IBinder<TViewModel> : IDisposable
        where TViewModel : class
    {       
        /// <summary>
        /// The view model.
        /// </summary>
        TViewModel ViewModel { get;  }

        IBinding<TViewModel, TView> Bind<TView>(TView view)
            where TView : class;
    }
}
