namespace DoLess.Bindings
{
    public interface ITwoWayPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty> :
        IBinding,
        ICanBind<TSource>
        where TSource : class
        where TTarget : class
    {
        ITwoWayPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty> WithConverter<T>()
            where T : IConverter<TSourceProperty, TTargetProperty>, new();
    }
}
