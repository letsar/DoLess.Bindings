using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DoLess.Bindings.Implementations
{
    internal abstract class PropertyBindingBase<TSource, TTarget, TTargetProperty, TSourceProperty> :
        Binding<TSource, TTarget>,
        IPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty>
        where TSource : class
        where TTarget : class
    {
        private PropertyWatcher sourcePropertyWatcher;

        public PropertyBindingBase(IBinding<TSource, TTarget> parent, Expression<Func<TSource, TSourceProperty>> sourcePropertyExpression, Expression<Func<TTarget, TTargetProperty>> targetPropertyExpression) :
            base(parent)
        {
            // The source is the view model.
            this.SourcePropertyBindingExpression = new PropertyBindingExpression<TSource, TSourceProperty>(this.Source, sourcePropertyExpression, this.IsTwoWayMode);
            this.sourcePropertyWatcher = PropertyWatcher.Create(sourcePropertyExpression);
            this.sourcePropertyWatcher.Watch(this.Source, this.OnSourceChanged);

            // The target is the view. It always has a setter, because it must be a property.
            this.TargetPropertyBindingExpression = new PropertyBindingExpression<TTarget, TTargetProperty>(this.Target, targetPropertyExpression, true);
        }

        public abstract BindingMode Mode { get; }

        public TSourceProperty SourceProperty => this.SourcePropertyBindingExpression.Value;

        public TTargetProperty TargetProperty => this.TargetPropertyBindingExpression.Value;

        public IConverter<TSourceProperty, TTargetProperty> Converter { get; private set; }

        protected PropertyBindingExpression<TSource, TSourceProperty> SourcePropertyBindingExpression { get; private set; }

        protected PropertyBindingExpression<TTarget, TTargetProperty> TargetPropertyBindingExpression { get; private set; }

        private bool IsTwoWayMode => this.Mode == BindingMode.TwoWay;

        public IPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty> WithConverter<TConverter>()
            where TConverter : IConverter<TSourceProperty, TTargetProperty>, new()
        {
            this.Converter = Cache<TConverter>.Instance;

            // The view needs to be updated.
            this.UpdateSourceProperty();
            return this;
        }

        public override void Dispose()
        {
            base.Dispose();
            this.SourcePropertyBindingExpression = null;
            this.TargetPropertyBindingExpression = null;
            this.sourcePropertyWatcher.Dispose();
            this.sourcePropertyWatcher = null;
        }

        protected abstract void OnSourceChanged(object sender, string propertyName);

        protected void UpdateTargetProperty()
        {
            this.TargetPropertyBindingExpression.Value = this.Converter.ConvertFromSource(this.SourceProperty);
        }

        protected void UpdateSourceProperty()
        {
            this.SourcePropertyBindingExpression.Value = this.Converter.ConvertFromTarget(this.TargetProperty);
        }
    }
}
