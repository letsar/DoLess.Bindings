using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Windows.Input;

namespace DoLess.Bindings
{
    public static class BindingExtensions
    {
        public static IBinding<TSource, TTarget> Bind<TSource, TTarget>(this TSource self, TTarget target)
            where TSource : class, INotifyPropertyChanged
            where TTarget : class
        {
            return new Binding<TSource, TTarget>(self, target);
        }

        public static IPropertyBinding<TSource, TTarget, TTargetProperty> Property<TSource, TTarget, TTargetProperty>(this IBinding<TSource, TTarget> self, Expression<Func<TTarget, TTargetProperty>> targetPropertyExpression)
            where TSource : class, INotifyPropertyChanged
            where TTarget : class
        {
            return new PropertyBinding<TSource, TTarget, TTargetProperty>(self, targetPropertyExpression);
        }

        public static IOneWayPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty> To<TSource, TTarget, TTargetProperty, TSourceProperty>(this IPropertyBinding<TSource, TTarget, TTargetProperty> self, Expression<Func<TSource, TSourceProperty>> sourcePropertyExpression)
            where TSource : class, INotifyPropertyChanged
            where TTarget : class
        {
            var binding = new OneWayPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty>(self, sourcePropertyExpression);


            return binding;
        }

        public static IOneWayPropertyBinding<TSource, TTarget, string, TSourceProperty> To<TSource, TTarget, TSourceProperty>(this IPropertyBinding<TSource, TTarget, string> self, Expression<Func<TSource, TSourceProperty>> sourcePropertyExpression)
            where TSource : class, INotifyPropertyChanged
            where TTarget : class
        {
            return new OneWayPropertyBinding<TSource, TTarget, string, TSourceProperty>(self, sourcePropertyExpression)
                       .WithConverter<StringCastConverter<TSourceProperty>>();

        }

        public static IOneWayPropertyBinding<TSource, TTarget, TProperty, TProperty> To<TSource, TTarget, TProperty>(this IPropertyBinding<TSource, TTarget, TProperty> self, Expression<Func<TSource, TProperty>> sourcePropertyExpression)
            where TSource : class, INotifyPropertyChanged
            where TTarget : class
        {
            return new OneWayPropertyBinding<TSource, TTarget, TProperty, TProperty>(self, sourcePropertyExpression)
                       .WithConverter<IdentityConverter<TProperty>>();
        }

        public static IEventBinding<TSource, TTarget, TEventArgs> Event<TSource, TTarget, TEventArgs>(this IBinding<TSource, TTarget> self, string eventName, TEventArgs defaultEventArgs = default(TEventArgs))
            where TSource : class, INotifyPropertyChanged
            where TTarget : class
            where TEventArgs : EventArgs
        {
            return new EventBinding<TSource, TTarget, TEventArgs>(self, eventName);
        }


        public static IEventToCommandBinding<TSource, TTarget, TEventArgs, TCommand> To<TSource, TTarget, TEventArgs, TCommand>(this IEventBinding<TSource, TTarget, TEventArgs> self, Expression<Func<TSource, TCommand>> commandExpression)
            where TSource : class, INotifyPropertyChanged
            where TTarget : class
            where TEventArgs : EventArgs
            where TCommand : ICommand
        {
            return new EventToCommandBinding<TSource, TTarget, TEventArgs, TCommand>(self, commandExpression);
        }
    }
}
