using System;
using System.Linq.Expressions;

namespace DoLess.Bindings
{
    internal partial class Binding<TSource, TTarget> :
        Binding,
        IBinding<TSource, TTarget>   
        where TSource : class
        where TTarget : class
    {
        private WeakReference<TSource> weakSource;
        private WeakReference<TTarget> weakTarget;        

        public Binding(TSource source, TTarget target, IBinding linkedBinding = null) :
            base(linkedBinding)
        {
            this.weakSource = new WeakReference<TSource>(source);
            this.weakTarget = new WeakReference<TTarget>(target);            
        }

        public Binding(Binding<TSource, TTarget> binding) :
            this(binding.Source, binding.Target, binding)
        { }

        public static Binding<TSource, TTarget> CreateFromBindableView(IBindableView<TSource> bindableView, TTarget target)
        {
            var binding = new Binding<TSource, TTarget>(bindableView.ViewModel, target);
            Bindings.SetBindableView(binding, bindableView);
            return binding;
        }

        public TSource Source => this.weakSource.GetOrDefault();

        public TTarget Target => this.weakTarget.GetOrDefault();        

        public override void UnbindInternal()
        {
            this.weakSource = null;
            this.weakTarget = null;
            base.UnbindInternal();
        }

        public sealed override bool CanBePurged()
        {
            return this.Source == null || this.Target == null;
        }

        public IBinding<TSource, TNewTarget> Bind<TNewTarget>(TNewTarget target)
            where TNewTarget : class
        {
            return new Binding<TSource, TNewTarget>(this.Source, target, this);
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
