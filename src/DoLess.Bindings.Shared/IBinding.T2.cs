namespace DoLess.Bindings
{
    /// <summary>
    /// Represents a binding between a source and a target.
    /// </summary>
    public interface IBinding<TSource, TTarget>
        where TSource : class
        where TTarget : class
    {
        
    }
}
