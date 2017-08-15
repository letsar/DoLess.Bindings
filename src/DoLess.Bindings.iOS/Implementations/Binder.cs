namespace DoLess.Bindings
{
    /// <summary>
    /// Represents an object that holds a view model and allows to binds it.
    /// </summary>
    internal partial class Binder<TViewModel> : IBinder<TViewModel>
    {
        public Binder(TViewModel viewModel, IBindableView<TViewModel> bindableView) :
            this(viewModel)
        {        
        }     
    }
}
