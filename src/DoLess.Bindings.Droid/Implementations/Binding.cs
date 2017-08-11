using Android.Views;

namespace DoLess.Bindings
{
    internal partial class Binding<TSource, TTarget>       
    {
        public IBinding<TSource, TNewTarget> Bind<TNewTarget>(int id)
            where TNewTarget : View
        {
            var view = this.Args.FindViewById<TNewTarget>(id);
            return this.Bind(view);
        }
    }
}
