using System;

namespace DoLess.Bindings
{
    public static class DisposerHelper
    {
        public static void Release<T>(ref T disposable)
            where T : class, IDisposable
        {
            disposable?.Dispose();
            disposable = null;
        }
    }
}
