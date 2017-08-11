using System.ComponentModel;

namespace DoLess.Bindings
{
    /// <summary>
    /// Represents a view that can be bind.
    /// </summary>
    /// <typeparam name="TViewModel">The type of the view model attached to this view.</typeparam>
    public partial interface IBindableView<TViewModel> 
        where TViewModel : class
    {
        IBinder<TViewModel> Binder { get; set; }        
    }
}
