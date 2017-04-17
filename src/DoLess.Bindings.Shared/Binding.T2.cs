using System;
using System.Collections.Generic;
using System.Text;

namespace DoLess.Bindings
{
    internal class Binding<TSource, TTarget> : IBinding<TSource, TTarget>
        where TSource : class
        where TTarget : class
    {
        private readonly WeakReference<TSource> weakSource;
        private readonly WeakReference<TTarget> weakTarget;

        public Binding(TSource source, TTarget target)
        {
            this.weakSource = new WeakReference<TSource>(source);
            this.weakTarget = new WeakReference<TTarget>(target);
               
        }
    }
}
