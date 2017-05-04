using Android.Views;

namespace DoLess.Bindings
{
    public partial interface ICanBind<TSource>
    { 
        IBinding<TSource, TNewTarget> Bind<TNewTarget>(int resourceId)
            where TNewTarget : View;
    }
}
