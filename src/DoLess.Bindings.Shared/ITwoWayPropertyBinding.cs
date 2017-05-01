namespace DoLess.Bindings
{
    public interface ITwoWayPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty>
        where TSource : class
        where TTarget : class
    {
        ITwoWayPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty> WithConverter<T>()
            where T : IConverter<TSourceProperty, TTargetProperty>, new();
    }
}
