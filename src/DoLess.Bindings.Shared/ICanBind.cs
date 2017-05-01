namespace DoLess.Bindings
{
    public interface ICanBind<TSource> : IBinding<TSource>
        where TSource : class
    {        
    }
}
