namespace DoLess.Bindings
{
    internal interface IBindingDescription<TSource, TTarget> :
        IHaveLinkedBinding,
        ICanBind<TSource>
        where TSource : class
        where TTarget : class
    {
        TTarget Target { get; }
    }
}
