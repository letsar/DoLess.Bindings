using System;

namespace DoLess.Bindings
{
    internal class Binding<TSource, TTarget> :
        Binding,
        IBinding<TSource, TTarget>,
        IHaveLinkedBinding<TSource, TTarget>
        where TSource : class
        where TTarget : class
    {
        private readonly WeakReference<TSource> weakSource;
        private readonly WeakReference<TTarget> weakTarget;

        private Binding(TSource source, TTarget target, IBinding linkedBinding = null, long? id = null) :
            base(linkedBinding, id.GetValueOrDefault())
        {
            this.weakSource = new WeakReference<TSource>(source);
            this.weakTarget = new WeakReference<TTarget>(target);
        }

        public Binding(TSource source, TTarget target, IHaveLinkedBinding linkedBinding) :
            this(source, target, linkedBinding, linkedBinding?.Id)
        { }

        public Binding(IHaveLinkedBinding<TSource, TTarget> binding) :
            this(binding.Source, binding.Target, binding.LinkedBinding, binding.Id)
        { }

        public TSource Source => this.weakSource.GetOrDefault();

        public TTarget Target => this.weakTarget.GetOrDefault();

        public override void UnbindInternal() { }

        public sealed override bool CanBePurged()
        {
            return this.Source == null || this.Target == null;
        }
    }
}
