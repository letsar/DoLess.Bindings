using System;

namespace DoLess.Bindings
{
    internal class StringCastConverter<TSource> : IConverterFromSource<TSource, string>
    {
        public string ConvertFromSource(TSource value)
        {
            return value?.ToString();
        }
    }
}