namespace DoLess.Bindings
{
    public interface ICanBind<TSource> : IBinding
        where TSource : class
    {
        TSource Source { get; }
    }
}
