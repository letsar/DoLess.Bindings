using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;

namespace DoLess.Bindings
{
    public static class ViewModelExtensions
    {
        public static IBindingDescription<TSource, TTarget> Bind<TSource, TTarget, TProperty>(this TSource self, TTarget target, Expression<Func<TTarget, TProperty>> targetProperty)
            where TSource : class, INotifyPropertyChanged
            where TTarget : class
        {
            return new Binding<TSource, TTarget>(self, target).SetTargetProperty(targetProperty);
        }
    }
}
