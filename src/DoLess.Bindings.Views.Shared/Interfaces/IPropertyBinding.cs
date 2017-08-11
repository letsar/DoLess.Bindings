namespace DoLess.Bindings
{
    public interface IPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty> :
        ICanBind<TSource>
        where TSource : class
        where TTarget : class
    {
        BindingMode Mode { get; }

        IPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty> WithConverter<TConverter>()
            where TConverter : IConverter<TSourceProperty, TTargetProperty>, new();
    }
}
