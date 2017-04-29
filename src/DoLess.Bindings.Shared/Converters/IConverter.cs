using System.ComponentModel;

namespace DoLess.Bindings
{
    /// <summary>
    /// Represents a converter that can convert from a target type to a source type and vice-versa.
    /// </summary>
    /// <typeparam name="TSource">The source type.</typeparam>
    /// <typeparam name="TTarget">The target type.</typeparam>
    public interface IConverter<TSource, TTarget> : 
        IConverterFromSource<TSource, TTarget>, 
        IConverterFromTarget<TSource, TTarget>
    {
    }
}
