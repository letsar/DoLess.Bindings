using System;
using System.Linq.Expressions;

namespace DoLess.Bindings
{
    internal abstract class Binding<TSource, TTarget, TSourceProperty> :
        Binding<TSource, TTarget>
        where TSource : class
        where TTarget : class
    {
        private PropertyWatcher sourcePropertyWatcher;

        public Binding(IBinding<TSource, TTarget> parent, Expression<Func<TSource, TSourceProperty>> sourcePropertyExpression) :
            base(parent)
        {
            if (sourcePropertyExpression != null)
            {
                // The source is the view model.
                this.SourcePropertyBindingExpression = new PropertyBindingExpression<TSource, TSourceProperty>(this.Source, sourcePropertyExpression);
                this.sourcePropertyWatcher = PropertyWatcher.Create(sourcePropertyExpression);
                this.sourcePropertyWatcher.Watch(this.Source, this.OnSourceChanged);
            }
        }

        public TSourceProperty SourceProperty => this.SourcePropertyBindingExpression.Value;

        protected PropertyBindingExpression<TSource, TSourceProperty> SourcePropertyBindingExpression { get; private set; }

        public override void Dispose()
        {
            DisposerHelper.Release(ref this.sourcePropertyWatcher);
            this.SourcePropertyBindingExpression = null;
            base.Dispose();
        }

        protected abstract void OnSourceChanged(object sender, string propertyName);
    }
}
