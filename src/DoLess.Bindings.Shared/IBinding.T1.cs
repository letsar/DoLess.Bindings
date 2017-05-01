namespace DoLess.Bindings
{
    /// <summary>
    /// Represents a binding from a source.
    /// </summary>
    public interface IBinding<TSource> :
        IBinding
        where TSource : class
    {
        TSource Source { get; }
    }
}
