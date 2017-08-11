using System;

namespace DoLess.Bindings
{
    internal class DefaultConverter<TSource, TTarget> :
        IConverter<TSource, TTarget>
    {
        public TTarget ConvertFromSource(TSource value)
        {
            return ChangeType<TSource, TTarget>(value);
        }

        public TSource ConvertFromTarget(TTarget value)
        {
            return ChangeType<TTarget, TSource>(value);
        }

        private static TResult ChangeType<T, TResult>(T value)
        {
            try
            {
                return (TResult)Convert.ChangeType(value, typeof(TResult));
            }
            catch (Exception ex)
            {
                Bindings.LogError($"cannot cast from {typeof(T)} to {typeof(TResult)}", ex);
                return default(TResult);
            }
        }
    }
}
