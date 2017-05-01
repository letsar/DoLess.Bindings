using System;

namespace DoLess.Bindings
{
    internal class Binding<TSource, TTarget> :
        Binding,
        IBinding<TSource, TTarget>,
        IBindingDescription<TSource, TTarget>
        where TSource : class
        where TTarget : class
    {
        private readonly WeakReference<TSource> weakSource;
        private readonly WeakReference<TTarget> weakTarget;

        public Binding(TSource source, TTarget target, IBinding linkedBinding = null) : base(linkedBinding)
        {
            this.weakSource = new WeakReference<TSource>(source);
            this.weakTarget = new WeakReference<TTarget>(target);
        }

        public Binding(IBindingDescription<TSource, TTarget> bindingDescription) :
            this(bindingDescription?.Source, bindingDescription?.Target, bindingDescription?.LinkedBinding)
        { }

        public TSource Source => this.weakSource.GetOrDefault();

        public TTarget Target => this.weakTarget.GetOrDefault();

        public override void UnbindInternal() { }
    }
}
