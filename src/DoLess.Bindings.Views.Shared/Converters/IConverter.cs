using System;
using System.Collections.Generic;
using System.Text;

namespace DoLess.Bindings
{
    /// <summary>
    /// Represents a converter that can convert from a target type to a source type and vice-versa.
    /// </summary>
    /// <typeparam name="TSource">The source type.</typeparam>
    /// <typeparam name="TTarget">The target type.</typeparam>
    public interface IConverter<TSource, TTarget>
    {
        /// <summary>
        /// Converts from the source type to the target type.
        /// </summary>
        /// <param name="value">The source value.</param>
        /// <returns></returns>
        TTarget ConvertFromSource(TSource value);

        /// <summary>
        /// Converts from the target type to the source type.
        /// </summary>
        /// <param name="value">The target value.</param>
        /// <returns></returns>
        TSource ConvertFromTarget(TTarget value);
    }
}
