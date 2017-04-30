using System;
using System.ComponentModel;
using System.Linq.Expressions;
using DoLess.Bindings.Observation;

namespace DoLess.Bindings
{
    internal class OneWayPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty> :
        PropertyBinding<TSource, TTarget, TTargetProperty>,
        IOneWayPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty>
        where TSource : class, INotifyPropertyChanged
        where TTarget : class
    {
        private readonly Func<TSource, TSourceProperty> getSourceProperty;
        private readonly Expression<Func<TSource, TSourceProperty>> sourcePropertyExpression;
        private readonly ObservedNode sourceRootNode;
        private IConverterFromSource<TSourceProperty, TTargetProperty> converter;

        public OneWayPropertyBinding(IPropertyBinding<TSource, TTarget, TTargetProperty> bindingProperty, Expression<Func<TSource, TSourceProperty>> sourcePropertyExpression) :
            base(bindingProperty)
        {
            this.sourcePropertyExpression = sourcePropertyExpression;
            this.getSourceProperty = sourcePropertyExpression.Compile();
            this.sourceRootNode = sourcePropertyExpression.AsObservedNode();
            this.sourceRootNode.Observe(this.BindingSet.Source, this.OnSourceChanged);
        }

        public TSourceProperty SourceProperty => this.getSourceProperty(this.BindingSet.Source);

        public ITwoWayPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty> CreateTwoWay()
        {
            this.sourceRootNode.Unobserve();
            return new TwoWayPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty>(this, this.sourcePropertyExpression);
        }

        public IOneWayPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty> WithConverter<T>()
                    where T : IConverterFromSource<TSourceProperty, TTargetProperty>, new()
        {
            this.converter = Cache<T>.Instance;
            return this;
        }

        private void OnSourceChanged()
        {
            if (this.converter == null)
            {
                Bindings.LogError($"a converter is needed for the types {typeof(TSourceProperty).FullName} and {typeof(TTargetProperty).FullName}");
            }
            else
            {
                try
                {
                    this.targetProperty.Value = this.converter.ConvertFromSource(this.SourceProperty);
                }
                catch (Exception ex)
                {
                    Bindings.LogError($"when trying to set the property '{this.targetProperty.Name}' of type '{typeof(TTarget).FullName}': {ex.ToString()}");
                }
            }
        }
    }
}
