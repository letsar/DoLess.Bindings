using System.ComponentModel;

namespace DoLess.Bindings
{
    /// <summary>
    /// Represents a converter that can convert from a source type to a target type.
    /// </summary>
    /// <typeparam name="TSource">The source type.</typeparam>
    /// <typeparam name="TTarget">The target type.</typeparam>
    public interface IConverterFromSource<TSource, TTarget>
    {
        /// <summary>
        /// Converts from the source type to the target type.
        /// </summary>
        /// <param name="value">The source value.</param>
        /// <returns></returns>
        TTarget ConvertFromSource(TSource value);
    }
}
