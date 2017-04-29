using System.ComponentModel;

namespace DoLess.Bindings
{
    /// <summary>
    /// Represents a converter that can convert from a target type to a source type.
    /// </summary>
    /// <typeparam name="TSource">The source type.</typeparam>
    /// <typeparam name="TTarget">The target type.</typeparam>
    public interface IConverterFromTarget<TSource, TTarget>
    {
        /// <summary>
        /// Converts from the target type to the source type.
        /// </summary>
        /// <param name="value">The target value.</param>
        /// <returns></returns>
        TSource ConvertFromTarget(TTarget value);
    }
}
