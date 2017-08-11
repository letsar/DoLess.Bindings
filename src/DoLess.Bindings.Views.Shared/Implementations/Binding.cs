using System;

namespace DoLess.Bindings
{
    internal partial class Binding<TSource, TTarget> :
        IBinding<TSource, TTarget>
        where TSource : class
        where TTarget : class
    {
        public Binding(TSource source, TTarget target, IBindingArgs parameters = null)
        {
            this.Source = source;
            this.Target = target;
            this.Args = parameters;
        }

        public Binding(IBinding<TSource, TTarget> parent) :
            this(parent?.Source, parent?.Target, parent?.Args)
        {
            this.Parent = parent;
        }

        public IBindingArgs Args { get; private set; }

        public TTarget Target { get; private set; }

        public TSource Source { get; private set; }

        public IBinding Parent { get; protected set; }

        public IBinding<TSource, TNewTarget> Bind<TNewTarget>(TNewTarget newTarget)
            where TNewTarget : class
        {
            var newBinding = new Binding<TSource, TNewTarget>(this.Source, newTarget, this.Args);
            newBinding.Parent = this;
            return newBinding;
        }

        public IPropertyBindingSource<TSource, TTarget, TTargetProperty> Property<TTargetProperty>(System.Linq.Expressions.Expression<Func<TTarget, TTargetProperty>> targetPropertyExpression)
        {
            return new PropertyBindingSource<TSource, TTarget, TTargetProperty>(this, targetPropertyExpression);
        }

        public virtual void Dispose()
        {
            this.Parent?.Dispose();
            this.Parent = null;

            this.Source = null;
            this.Target = null;
        }
    }
}
