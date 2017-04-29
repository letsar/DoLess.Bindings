using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
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
        private readonly ObservedNode sourceRootNode;
        private IConverterFromSource<TSourceProperty, TTargetProperty> converter;

        public OneWayPropertyBinding(IPropertyBinding<TSource, TTarget, TTargetProperty> bindingProperty, Expression<Func<TSource, TSourceProperty>> sourcePropertyExpression) :
            base(bindingProperty)
        {
            this.getSourceProperty = sourcePropertyExpression.Compile();
            this.sourceRootNode = sourcePropertyExpression.AsObservedNode();
            this.sourceRootNode.Observe(this.BindingSet.Source, this.WhenChanged);
        }

        public TSourceProperty SourceProperty => this.getSourceProperty(this.BindingSet.Source);

        public IOneWayPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty> WithConverter<T>()
            where T : IConverterFromSource<TSourceProperty, TTargetProperty>, new()
        {
            this.converter = Cache<T>.Instance;
            return this;
        }

        private void WhenChanged()
        {
            if (this.converter == null)
            {
                Bindings.LogError($"a converter is needed for the types {typeof(TSourceProperty).FullName} and {typeof(TTargetProperty).FullName}");
            }
            else
            {
                try
                {
                    this.TargetProperty = this.converter.ConvertFromSource(this.SourceProperty);
                }
                catch (Exception ex)
                {
                    Bindings.LogError($"when trying to set the property '{this.targetPropertyInfo.Name}' of type '{typeof(TTarget).FullName}': {ex.ToString()}");
                }
            }
        }
    }
}
