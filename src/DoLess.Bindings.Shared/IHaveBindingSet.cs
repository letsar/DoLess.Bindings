namespace DoLess.Bindings
{
    internal interface IHaveBindingSet<TSource, TTarget>
        where TSource : class
        where TTarget : class
    {
        BindingSet<TSource, TTarget> BindingSet { get; }
    }
}
