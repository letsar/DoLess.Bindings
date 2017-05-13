using Android.Views;

namespace DoLess.Bindings
{
    public partial interface IBindableView<T> :
        IBindableView
        where T : class
    {
        IBinding<T, TTarget> Bind<TTarget>(int resourceId)
            where TTarget : View;

        View View { get; }
    }
}