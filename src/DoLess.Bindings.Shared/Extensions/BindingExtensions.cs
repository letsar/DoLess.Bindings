using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;
using DoLess.Bindings.Converters;

namespace DoLess.Bindings
{
    public static class BindingExtensions
    {
        public static IBindingDescription<TSource, TTarget> Bind<TSource, TTarget>(this TSource self, TTarget target)
            where TSource : class, INotifyPropertyChanged
            where TTarget : class
        {
            return new BindingDescription<TSource, TTarget>(self, target);
        }

        public static IBindingPropertyDescription<TSource, TTarget, TTargetProperty> Property<TSource, TTarget, TTargetProperty>(this IBindingDescription<TSource, TTarget> self, Expression<Func<TTarget, TTargetProperty>> targetPropertyExpression)
            where TSource : class, INotifyPropertyChanged
            where TTarget : class
        {
            return new BindingPropertyDescription<TSource, TTarget, TTargetProperty>(self, targetPropertyExpression);
        }

        public static IBindingOneWayPropertyDescription<TSource, TTarget, TTargetProperty, TSourceProperty> To<TSource, TTarget, TTargetProperty, TSourceProperty>(this IBindingPropertyDescription<TSource, TTarget, TTargetProperty> self, Expression<Func<TSource, TSourceProperty>> sourcePropertyExpression, Func<TSourceProperty, TTargetProperty> converter)
            where TSource : class, INotifyPropertyChanged
            where TTarget : class
        {
            return new BindingOneWayPropertyDescription<TSource, TTarget, TTargetProperty, TSourceProperty>(self, sourcePropertyExpression, converter);
        }

        public static IBindingOneWayPropertyDescription<TSource, TTarget, TProperty, TProperty> To<TSource, TTarget, TProperty>(this IBindingPropertyDescription<TSource, TTarget, TProperty> self, Expression<Func<TSource, TProperty>> sourcePropertyExpression)
            where TSource : class, INotifyPropertyChanged
            where TTarget : class
        {
            return new BindingOneWayPropertyDescription<TSource, TTarget, TProperty, TProperty>(self, sourcePropertyExpression, IdentityConverter<TProperty>.Instance);
        }
    }
}
