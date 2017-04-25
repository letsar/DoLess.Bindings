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
        private readonly Func<TSourceProperty, TTargetProperty> converter;

        public OneWayPropertyBinding(IPropertyBinding<TSource, TTarget, TTargetProperty> bindingPropertyDescription, Expression<Func<TSource, TSourceProperty>> sourcePropertyExpression, Func<TSourceProperty, TTargetProperty> converter) :
            base(bindingPropertyDescription)
        {
            this.converter = converter;
            this.getSourceProperty = sourcePropertyExpression.Compile();
            this.sourceRootNode = sourcePropertyExpression.AsObservedNode();
            this.sourceRootNode.Observe(this.BindingSet.Source, this.WhenChanged);
        }

        public TSourceProperty SourceProperty => this.getSourceProperty(this.BindingSet.Source);

        private void WhenChanged()
        {
            this.TargetProperty = this.converter(this.SourceProperty);
        }
    }
}
