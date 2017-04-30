using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using DoLess.Bindings.Observation;

namespace DoLess.Bindings
{
    internal static class ExpressionExtensions
    {
        public static ObservedNode AsObservedNode<TViewModel, TPoperty>(this Expression<Func<TViewModel, TPoperty>> self)
        {
            ObservedNodeBuilder builder = new ObservedNodeBuilder();
            builder.Visit(self);
            return builder.Root;
        }        

        public static BindingExpression<T, TProperty> GetBindingExpression<T, TProperty>(this Expression<Func<T, TProperty>> self, T source)
            where T : class
        {
            return new BindingExpression<T, TProperty>(source, self);
        }
    }
}
