using Android.Views;

namespace DoLess.Bindings
{
    /// <summary>
    /// Represents a binding that can create another binding.
    /// </summary>
    /// <typeparam name="TSource">The binding's source.</typeparam>
    public partial interface ICanBind<TSource>
    {
        IBinding<TSource, TNewTarget> Bind<TNewTarget>(int id)
            where TNewTarget : View;
    }
}
