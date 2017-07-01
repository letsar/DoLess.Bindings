using System;
using System.Collections.Generic;
using System.Linq;

namespace DoLess.Bindings
{
    public static class Bindings
    {
        private static readonly IdentifierPool IdPool;
        private static readonly Dictionary<long, BindingGroup> AllBindingGroups;        
        private static readonly Dictionary<long, WeakReference<IBindableView>> BindableViews;

        private static readonly Func<IInternalBinding, bool> TrueForAll;
        private static readonly Func<IInternalBinding, bool> TrueForCanBePurged;

        static Bindings()
        {
            AllBindingGroups = new Dictionary<long, BindingGroup>();            
            BindableViews = new Dictionary<long, WeakReference<IBindableView>>();

            IdPool = new IdentifierPool();
            TrueForAll = x => true;
            TrueForCanBePurged = x => x.CanBePurged();
        }

        public static event EventHandler<BindingTraceEventArgs> Trace = delegate { };

        internal static void Add(BindingGroup group)
        {
            if (group != null)
            {
                if (group.Id == 0)
                {
                    group.Id = IdPool.Next();
                }

                AllBindingGroups[group.Id] = group;
            }
        }

        internal static void SetBindableView(BindingGroup group, IBindableView bindableView)
        {
            BindableViews[group.Id] = new WeakReference<IBindableView>(bindableView);
        }

        internal static IBindableView GetBindableView(BindingGroup group)
        {
            WeakReference<IBindableView> weakReference = null;
            BindableViews.TryGetValue(group.Id, out weakReference);
            return weakReference?.GetOrDefault();
        }

        internal static void Remove(BindingGroup group)
        {
            if (group != null && group.Id > 0)
            {
                var id = group.Id;
                AllBindingGroups.Remove(id);
                BindableViews.Remove(id);

                IdPool.Recycle(id);
                group.Id = 0;
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

        internal static void Unbind(Func<IInternalBinding, bool> predicate)
        {
            var bindings = AllBindingGroups.Values.SelectMany(x => x).ToList();

            foreach (var binding in bindings.Where(predicate))
            {
                // When the unbind reaches the root, it removes itself.
                binding.Dispose();
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
