namespace DoLess.Bindings
{
    public interface IOneWayPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty> :
        IBinding,
        ICanBind<TSource>
        where TSource : class
        where TTarget : class
    {
        IOneWayPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty> WithConverter<T>()
            where T : IConverterFromSource<TSourceProperty, TTargetProperty>, new();

        ITwoWayPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty> TwoWay();        
    }
}
