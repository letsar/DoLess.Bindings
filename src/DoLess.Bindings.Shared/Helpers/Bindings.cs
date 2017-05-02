using System;
using System.Collections.Generic;

namespace DoLess.Bindings
{
    public static class Bindings
    {
        private static readonly IdentifierPool IdPool;
        private static readonly Dictionary<long, IBinding> AllBindings;

        static Bindings()
        {
            AllBindings = new Dictionary<long, IBinding>();
            IdPool = new IdentifierPool();
        }

        public static event Action<string> Failed = delegate { };

        internal static void Add(Binding binding)
        {
            if (binding != null)
            {
                if (binding.Id == 0)
                {
                    binding.Id = IdPool.Next();
                }

                AllBindings[binding.Id] = binding;
            }
        }

        internal static void LogError(string message)
        {
            Failed($"Binding error: '{message}'.");
        }
    }
}
