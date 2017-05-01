namespace DoLess.Bindings
{
    /// <summary>
    /// Represents a binding between a source and a target on a target property.
    /// </summary>
    public interface IBindingDescription<TSource, TTarget, TTargetProperty>
        where TSource : class
        where TTarget : class
    {
        
    }
}
