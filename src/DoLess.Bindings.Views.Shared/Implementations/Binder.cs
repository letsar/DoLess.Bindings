using System;
using System.ComponentModel;

namespace DoLess.Bindings
{
    /// <summary>
    /// Represents an object that holds a view model and allows to binds it.
    /// </summary>
    internal partial class Binder<TViewModel> : IBinder<TViewModel>, IBindingArgs
        where TViewModel : class
    {
        private IBinding binding;

        public Binder(TViewModel viewModel)
        {
            this.ViewModel = viewModel;
        }

        /// <summary>
        /// The view model.
        /// </summary>
        public TViewModel ViewModel { get; private set; }

        public IBinding<TViewModel, TView> Bind<TView>(TView view)
            where TView : class
        {
            DisposerHelper.Release(ref this.binding);
            var binding = new Binding<TViewModel, TView>(this.ViewModel, view, this);
            this.binding = binding;
            return binding;
        }

        public void Dispose()
        {
            this.InternalDispose();
            DisposerHelper.Release(ref this.binding);
            this.ViewModel = null;
        }

        partial void InternalDispose();
    }
}
