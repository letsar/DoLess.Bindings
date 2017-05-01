namespace DoLess.Bindings
{
    /// <summary>
    /// Represents a binding between a source and a target between the target property and the source one.
    /// </summary>
    public interface IBindingDescription<TSource, TTarget, TTargetProperty, TSourceProperty>
        where TSource : class
        where TTarget : class
    {
        
    }
}
