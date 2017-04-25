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
            return new Binding<TSource, TTarget>(self, target);
        }

        public static IPropertyBinding<TSource, TTarget, TTargetProperty> Property<TSource, TTarget, TTargetProperty>(this IBindingDescription<TSource, TTarget> self, Expression<Func<TTarget, TTargetProperty>> targetPropertyExpression)
            where TSource : class, INotifyPropertyChanged
            where TTarget : class
        {
            return new PropertyBinding<TSource, TTarget, TTargetProperty>(self, targetPropertyExpression);
        }

        public static IOneWayPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty> To<TSource, TTarget, TTargetProperty, TSourceProperty>(this IPropertyBinding<TSource, TTarget, TTargetProperty> self, Expression<Func<TSource, TSourceProperty>> sourcePropertyExpression, Func<TSourceProperty, TTargetProperty> converter)
            where TSource : class, INotifyPropertyChanged
            where TTarget : class
        {
            return new OneWayPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty>(self, sourcePropertyExpression, converter);
        }

        public static IOneWayPropertyBinding<TSource, TTarget, TProperty, TProperty> To<TSource, TTarget, TProperty>(this IPropertyBinding<TSource, TTarget, TProperty> self, Expression<Func<TSource, TProperty>> sourcePropertyExpression)
            where TSource : class, INotifyPropertyChanged
            where TTarget : class
        {
            return new OneWayPropertyBinding<TSource, TTarget, TProperty, TProperty>(self, sourcePropertyExpression, IdentityConverter<TProperty>.Instance);
        }
    }
}
