using System;
using System.Collections.Generic;
using System.Linq;

namespace DoLess.Bindings
{
    public static class Bindings
    {
        private static readonly IdentifierPool IdPool;
        private static readonly Dictionary<long, Binding> AllBindings;
        private static readonly Dictionary<long, WeakReference<IBindableView>> BindableViews;

        private static readonly Func<Binding, bool> TrueForAll;
        private static readonly Func<Binding, bool> TrueForCanBePurged;

        static Bindings()
        {
            AllBindings = new Dictionary<long, Binding>();
            BindableViews = new Dictionary<long, WeakReference<IBindableView>>();

            IdPool = new IdentifierPool();
            TrueForAll = x => true;
            TrueForCanBePurged = x => x.CanBePurged();
        }

        public static event EventHandler<BindingTraceEventArgs> Trace = delegate { };

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

        internal static void SetBindableView(Binding binding, IBindableView bindableView)
        {
            BindableViews[binding.Id] = new WeakReference<IBindableView>(bindableView);
        }

        internal static IBindableView GetBindableView(Binding binding)
        {
            WeakReference<IBindableView> weakReference = null;
            BindableViews.TryGetValue(binding.Id, out weakReference);
            return weakReference?.GetOrDefault();
        }

        internal static void Remove(Binding binding)
        {
            if (binding != null && binding.Id > 0)
            {
                var id = binding.Id;
                AllBindings.Remove(id);
                BindableViews.Remove(id);

                IdPool.Recycle(id);
                binding.Id = 0;
            }
        }

        public static void Reset()
        {
            Unbind(TrueForAll);
        }

        public static void Purge()
        {
            Unbind(TrueForCanBePurged);
        }

        internal static void Unbind(Func<Binding, bool> predicate)
        {
            var bindings = AllBindings.Values.ToList();

            foreach (var binding in bindings.Where(predicate))
            {
                // When the unbind reaches the root, it removes itself.
                binding.Unbind();
            }
        }

        internal static void LogError(string message)
        {
            Log(BindingTraceEventType.Error, message);
        }

        internal static void LogWarning(string message)
        {
            Log(BindingTraceEventType.Warning, message);
        }

        private static void Log(BindingTraceEventType type, string message)
        {
            Trace(null, new BindingTraceEventArgs(type, message));
        }
    }
}
