using System;
using System.Collections.Generic;
using System.Linq;

namespace DoLess.Bindings
{
    public static class Bindings
    {
        private static readonly IdentifierPool IdPool;
        private static readonly Dictionary<long, Binding> AllBindings;
        private static readonly Func<Binding, bool> TrueForAll;
        private static readonly Func<Binding, bool> TrueForCanBePurged;

        static Bindings()
        {
            AllBindings = new Dictionary<long, Binding>();
            IdPool = new IdentifierPool();
            TrueForAll = x => true;
            TrueForCanBePurged = x => x.CanBePurged();
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

        internal static void Remove(Binding binding)
        {
            if (binding != null && binding.Id > 0)
            {
                AllBindings.Remove(binding.Id);
                IdPool.Recycle(binding.Id);
                binding.Id = 0;
            }
        }

        internal static void Reset()
        {
            Unbind(TrueForAll);
        }

        internal static void Purge()
        {
            Unbind(TrueForCanBePurged);
        }

        internal static void Unbind(Func<Binding, bool> predicate)
        {
            var bindings = AllBindings.Values.ToList();

            foreach (var binding in bindings.Where(predicate))
            {
                binding.Unbind();
            }
        }

        internal static void LogError(string message)
        {
            Failed($"Binding error: '{message}'.");
        }
    }
}
