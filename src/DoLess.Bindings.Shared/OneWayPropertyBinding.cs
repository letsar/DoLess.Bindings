﻿using System;
using System.Linq.Expressions;
using DoLess.Bindings.Observation;

namespace DoLess.Bindings
{
    internal class OneWayPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty> :
        PropertyBinding<TSource, TTarget, TTargetProperty>,
        IOneWayPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty>
        where TSource : class
        where TTarget : class
    {
        private readonly Func<TSource, TSourceProperty> getSourceProperty;
        private readonly Expression<Func<TSource, TSourceProperty>> sourcePropertyExpression;
        private ObservedNode sourceRootNode;
        private IConverterFromSource<TSourceProperty, TTargetProperty> converter;

        public OneWayPropertyBinding(IPropertyBinding<TSource, TTarget, TTargetProperty> propertyBinding, Expression<Func<TSource, TSourceProperty>> sourcePropertyExpression) :
            base(propertyBinding)
        {
            this.sourcePropertyExpression = sourcePropertyExpression;
            this.getSourceProperty = sourcePropertyExpression.Compile();
            this.sourceRootNode = sourcePropertyExpression.AsObservedNode();
            this.sourceRootNode.Observe(this.Source, this.OnSourceChanged);
        }

        public TSourceProperty SourceProperty => this.getSourceProperty(this.Source);

        public IOneWayPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty> WithConverter<T>()
                    where T : IConverterFromSource<TSourceProperty, TTargetProperty>, new()
        {
            this.converter = Cache<T>.Instance;

            // The result may have changed.
            this.OnSourceChanged();
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

        public override void Dispose()
        {            
            this.sourceRootNode.Unobserve();
            this.sourceRootNode = null;
            base.Dispose();
        }

        public ITwoWayPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty> TwoWay()
        {
            return new TwoWayPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty>(this, this.sourcePropertyExpression)
                      .WithConverter<ClassCastConverter<TSourceProperty, TTargetProperty>>();
        }
    }
}
