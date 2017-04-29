using System;

namespace DoLess.Bindings
{
    public static class Bindings
    {
        public static event Action<string> Failed = delegate { };

        internal static void LogError(string message)
        {            
            Failed($"Binding error: '{message}'.");
        }
    }
}
