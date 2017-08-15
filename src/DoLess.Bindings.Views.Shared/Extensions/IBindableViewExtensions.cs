namespace DoLess.Bindings
{
    public static class IBindableViewExtensions
    {
        public static IBinder<TViewModel> ViewModel<TViewModel>(this IBindableView<TViewModel> self, TViewModel vm)
            where TViewModel : class
        {
            self.Binder = new Binder<TViewModel>(vm, self);
            return self.Binder;
        }
    }
}