using System;

namespace DoLess.Bindings
{
    internal class StructCastConverter<TSource, TTarget> : IConverter<TSource, TTarget>
    {
        public TTarget ConvertFromSource(TSource value)
        {
            return (TTarget)Convert.ChangeType(value, typeof(TTarget));
        }

        public TSource ConvertFromTarget(TTarget value)
        {
            return (TSource)Convert.ChangeType(value, typeof(TSource));
        }
    }
}