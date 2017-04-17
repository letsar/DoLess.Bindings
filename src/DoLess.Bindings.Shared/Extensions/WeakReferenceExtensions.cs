using System;

namespace DoLess.Bindings
{
    internal static class WeakReferenceExtensions
    {
        /// <summary>
        /// Gets the alive object in this <see cref="WeakReference{T}"/> or the <paramref name="defaultValue"/> if not alive.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="self">The weak reference.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static T GetOrDefault<T>(this WeakReference<T> self, T defaultValue = default(T)) where T : class
        {
            T value = defaultValue;
            self?.TryGetTarget(out value);
            return value;
        }


        /// <summary>
        /// Indicates whether this <see cref="WeakReference{T}"/> is alive.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="self">The weak reference.</param>
        /// <returns></returns>
        public static bool IsAlive<T>(this WeakReference<T> self) where T : class
        {
            return self?.GetOrDefault() != default(T);
        }
    }
}
