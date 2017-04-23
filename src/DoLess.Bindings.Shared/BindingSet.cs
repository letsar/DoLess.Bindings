using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DoLess.Bindings
{
    internal class BindingSet<TSource, TTarget>
        where TSource : class, INotifyPropertyChanged
        where TTarget : class
    {
        private readonly WeakReference<TSource> weakSource;
        private readonly WeakReference<TTarget> weakTarget;
        private readonly List<IBinding> bindings;

        public BindingSet(TSource source, TTarget target)
        {
            this.weakSource = new WeakReference<TSource>(source);
            this.weakTarget = new WeakReference<TTarget>(target);
            this.bindings = new List<IBinding>();
        }

        public TSource Source => this.weakSource.GetOrDefault();

        public TTarget Target => this.weakTarget.GetOrDefault();
    }
}
