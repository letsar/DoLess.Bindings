namespace DoLess.Bindings.Converters
{
    internal class IdentityConverter<T> : IConverter<T, T>
    {
        public T ConvertFromSource(T value)
        {
            return value;
        }

        public T ConvertFromTarget(T value)
        {
            return value;
        }
    }
}
