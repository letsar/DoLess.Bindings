using Android.Views;

namespace DoLess.Bindings
{
    /// <summary>
    /// Represents a bindable view.
    /// </summary>    
    public partial interface IBindableView
    {
        TView GetView<TView>(int resourceId)
            where TView : View;
    }
}