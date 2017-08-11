using System;

namespace DoLess.Bindings
{
    /// <summary>
    /// Represents an object that holds a view model and allows to binds it.
    /// </summary>
    public interface IBinder<TViewModel> : IDisposable
        where TViewModel : class
    {       
        /// <summary>
        /// The view model.
        /// </summary>
        TViewModel ViewModel { get;  }

        IBinding<TViewModel, TView> Bind<TView>(TView view, BindingArgs args = null)
            where TView : class;
    }
}
