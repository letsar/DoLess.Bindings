using System;
using System.Linq.Expressions;

namespace DoLess.Bindings
{
    internal partial class Binding<TSource, TTarget> :
        Binding,
        IBinding<TSource, TTarget>,
        IHaveLinkedBinding<TSource, TTarget>
        where TSource : class
        where TTarget : class
    {
        private WeakReference<TSource> weakSource;
        private WeakReference<TTarget> weakTarget;
        private WeakReference<object> weakCreator;

        private Binding(TSource source, TTarget target, IBinding linkedBinding = null, long? id = null, object creator = null) :
            base(linkedBinding, id.GetValueOrDefault())
        {
            this.weakSource = new WeakReference<TSource>(source);
            this.weakTarget = new WeakReference<TTarget>(target);
            this.weakCreator = new WeakReference<object>(creator);
        }

        public Binding(TSource source, TTarget target, IHaveLinkedBinding linkedBinding, object creator = null) :
            this(source, target, linkedBinding, linkedBinding?.Id, creator ?? linkedBinding?.Creator)
        { }

        public Binding(IHaveLinkedBinding<TSource, TTarget> binding) :
            this(binding.Source, binding.Target, binding.LinkedBinding, binding.Id, binding.Creator)
        { }

        public TSource Source => this.weakSource.GetOrDefault();

        public TTarget Target => this.weakTarget.GetOrDefault();

        public override object Creator => this.weakCreator.GetOrDefault();

        public override void UnbindInternal()
        {
            this.weakSource = null;
            this.weakTarget = null;
            this.weakCreator = null;
        }

        public sealed override bool CanBePurged()
        {
            return this.Source == null || this.Target == null;
        }

        public IBinding<TSource, TNewTarget> Bind<TNewTarget>(TNewTarget target)
            where TNewTarget : class
        {
            return new Binding<TSource, TNewTarget>(this.Source, target, (IHaveLinkedBinding)this);
        }

        public IPropertyBinding<TSource, TTarget, TTargetProperty> Property<TTargetProperty>(Expression<Func<TTarget, TTargetProperty>> targetPropertyExpression)
        {
            return new PropertyBinding<TSource, TTarget, TTargetProperty>(this, targetPropertyExpression);
        }

        public IEventBinding<TSource, TTarget, TEventArgs> Event<TEventArgs>(string eventName)
            where TEventArgs : EventArgs
        {
            return new EventBinding<TSource, TTarget, TEventArgs>(this, eventName);
        }
    }
}
