namespace DoLess.Bindings
{
    /// <summary>
    /// Represents a view holding a viewmodel.
    /// </summary>
    /// <typeparam name="T">The type of the viewmodel.</typeparam>
    public partial interface IBindableView<T> :
        IBindableView
        where T : class
    {
        /// <summary>
        /// Gets the view model.
        /// </summary>
        T ViewModel { get; }
    }
}