namespace DoLess.Bindings
{
    /// <summary>
    /// Contains a cache for the specified type.
    /// This class is static for caching issues.
    /// </summary>
    internal static class Cache<T> 
        where T: new()
    {
        private static readonly T PrivateInstance = new T();

        public static T Instance => PrivateInstance;
    }
}
