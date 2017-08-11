using System;
using System.Linq.Expressions;

namespace DoLess.Bindings
{
    internal class PropertyBindingSource<TSource, TTarget, TTargetProperty> :
        Binding<TSource, TTarget>,
        IPropertyBindingSource<TSource, TTarget, TTargetProperty>
        where TSource : class
        where TTarget : class
    {
        private readonly Expression<Func<TTarget, TTargetProperty>> targetPropertyExpression;
        private readonly IBinding<TSource, TTarget> parent;

        public PropertyBindingSource(IBinding<TSource, TTarget> binding, Expression<Func<TTarget, TTargetProperty>> targetPropertyExpression) :
            base(binding)
        {
            this.parent = binding;
            this.targetPropertyExpression = targetPropertyExpression;
        }

        public IPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty> To<TSourceProperty>(Expression<Func<TSource, TSourceProperty>> sourcePropertyExpression, BindingMode mode = BindingMode.OneWay)
        {
            IPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty> result = null;
            switch (mode)
            {
                case BindingMode.OneWay:
                    result = new OneWayPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty>(this.parent, sourcePropertyExpression, this.targetPropertyExpression);
                    break;
                case BindingMode.TwoWay:
                    result = new TwoWayPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty>(this.parent, sourcePropertyExpression, this.targetPropertyExpression);
                    break;
                default: break; ;   // Not possible. 
            }

            result.WithConverter<DefaultConverter<TSourceProperty, TTargetProperty>>();

            return result;
        }
    }
}
