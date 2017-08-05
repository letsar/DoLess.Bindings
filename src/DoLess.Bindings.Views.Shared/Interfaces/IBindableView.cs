using System.ComponentModel;

namespace DoLess.Bindings.Interfaces
{
    /// <summary>
    /// Represents a view that can be bind.
    /// </summary>
    /// <typeparam name="TViewModel">The type of the view model attached to this view.</typeparam>
    public interface IBindableView<TViewModel> 
        where TViewModel : INotifyPropertyChanged
    {
        IBinding Binding { get; set; }

        TViewModel ViewModel { get; }
    }
}
