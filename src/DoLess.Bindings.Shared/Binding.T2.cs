namespace DoLess.Bindings
{
    internal class Binding<TSource, TTarget> :
        IBinding<TSource, TTarget>,
        IHaveBindingSet<TSource, TTarget>
        where TSource : class
        where TTarget : class
    {
        public Binding(BindingSet<TSource, TTarget> bindingSet)
        {
            this.BindingSet = bindingSet;
        }

        public Binding(TSource source, TTarget target) :
            this(new BindingSet<TSource, TTarget>(source, target))
        { }

        public Binding(IHaveBindingSet<TSource, TTarget> bindingSetOwner) :
            this(bindingSetOwner?.BindingSet)
        { }

        public BindingSet<TSource, TTarget> BindingSet { get; }
    }
}
