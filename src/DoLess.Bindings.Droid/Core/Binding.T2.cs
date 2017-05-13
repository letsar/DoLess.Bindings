using Android.Views;

namespace DoLess.Bindings
{
    internal partial class Binding<TSource, TTarget>
    {
        public IBinding<TSource, TNewTarget> Bind<TNewTarget>(int resourceId)
            where TNewTarget : View
        {
            var bindableView = Bindings.GetBindableView(this);
            TNewTarget target = null;
            if (bindableView == null)
            {
                Bindings.LogError("the creator does not exists");
            }
            else
            {
                target = bindableView.GetView<TNewTarget>(resourceId);                

                if (target == null)
                {
                    Bindings.LogError($"there is no bindable view associated with the binding n°{this.Id}");
                }
            }
            return new Binding<TSource, TNewTarget>(this.Source, target, this);
        }
    }
}
