using System;

namespace DoLess.Bindings
{
    internal class Binding<TSource, TTarget> :
        IBinding<TSource, TTarget>
        where TSource : class
        where TTarget : class
    {
        public Binding(TSource source, TTarget target, BindingParameters parameters = null)
        {
            this.Source = source;
            this.Target = target;
            this.Parameters = parameters;
        }

        public Binding(IBinding<TSource, TTarget> parent) :
            this(parent?.Source, parent?.Target, parent?.Parameters)
        {
            this.Parent = parent;
        }

        public BindingParameters Parameters { get; private set; }

        public TTarget Target { get; private set; }

        public TSource Source { get; private set; }

        public IBinding Parent { get; protected set; }

        public IBinding<TSource, TNewTarget> Bind<TNewTarget>(TNewTarget newTarget)
            where TNewTarget : class
        {
            var newBinding = new Binding<TSource, TNewTarget>(this.Source, newTarget, this.Parameters);
            newBinding.Parent = this;
            return newBinding;
        }

        public virtual void Dispose()
        {
            this.Parent?.Dispose();
            this.Parent = null;
            this.Parameters = null;
            this.Source = null;
            this.Target = null;
        }
    }
}
