using System;
using System.ComponentModel;

namespace DoLess.Bindings
{
    /// <summary>
    /// Represents an object that holds a view model and allows to binds it.
    /// </summary>
    public class Binder<TViewModel> : IBinder<TViewModel>
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

        public IBinding<TViewModel, TView> Bind<TView>(TView view, BindingArgs args = null)
            where TView : class
        {
            DisposerHelper.Release(ref this.binding);
            var binding = new Binding<TViewModel, TView>(this.ViewModel, view, args);
            this.binding = binding;
            return binding;
        }

        public void Dispose()
        {
            DisposerHelper.Release(ref this.binding);
            this.ViewModel = null;
        }
    }
}
