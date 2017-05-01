namespace DoLess.Bindings
{
    internal interface IHaveLinkedBinding<TSource, TTarget> :
        IHaveLinkedBinding,
        IBinding<TSource, TTarget>
        where TSource : class
        where TTarget : class
    {
    }
}
