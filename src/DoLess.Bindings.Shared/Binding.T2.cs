using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DoLess.Bindings
{
    internal class Binding<TSource, TTarget> : IBinding<TSource, TTarget>, IBindingDescription<TSource, TTarget>
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

        public Binding<TSource, TTarget> SetSourceProperty<TProperty>(Expression<Func<TSource, TProperty>> sourceProperty)
        {
            // TODO.
            return this;
        }

        public Binding<TSource, TTarget> SetTargetProperty<TProperty>(Expression<Func<TTarget, TProperty>> targetProperty)
        {
            // TODO.
            return this;
        }

        public IBinding<TSource, TTarget> To<TProperty>(Expression<Func<TSource, TProperty>> sourceProperty)
        {
            return this.SetSourceProperty(sourceProperty);
        }
    }
}
