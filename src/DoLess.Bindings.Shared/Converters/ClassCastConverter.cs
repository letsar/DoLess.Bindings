using System;

namespace DoLess.Bindings
{
    internal class ClassCastConverter<TSource, TTarget> : IConverter<TSource, TTarget>
    {
        public TTarget ConvertFromSource(TSource value)
        {
            return (TTarget)(object)value;
        }

        public TSource ConvertFromTarget(TTarget value)
        {
            return (TSource)(object)value;
        }
    }
}