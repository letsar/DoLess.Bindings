using System;
using System.Linq.Expressions;
using System.Windows.Input;

namespace DoLess.Bindings
{
    public static class BindingExtensions
    {
        public static IBinding<TSource, TTarget> Bind<TSource, TTarget>(this IView<TSource> self, TTarget target)
            where TSource : class
            where TTarget : class
        {
            return new Binding<TSource, TTarget>(self.ViewModel, target);
        }

        public static IBinding<TSource, TTarget> BindFromViewModel<TSource, TTarget>(this TSource self, TTarget target)
            where TSource : class
            where TTarget : class
        {
            return new Binding<TSource, TTarget>(self, target);
        }

        public static IPropertyBinding<TSource, TTarget, TTargetProperty> Property<TSource, TTarget, TTargetProperty>(this IBinding<TSource, TTarget> self, Expression<Func<TTarget, TTargetProperty>> targetPropertyExpression)
            where TSource : class
            where TTarget : class
        {
            return new PropertyBinding<TSource, TTarget, TTargetProperty>(self, targetPropertyExpression);
        }

        public static IOneWayPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty> To<TSource, TTarget, TTargetProperty, TSourceProperty>(this IPropertyBinding<TSource, TTarget, TTargetProperty> self, Expression<Func<TSource, TSourceProperty>> sourcePropertyExpression)
            where TSource : class
            where TTarget : class
        {
            return new OneWayPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty>(self, sourcePropertyExpression)
                       .WithConverter<ClassCastConverter<TSourceProperty, TTargetProperty>>();
        }

        public static ITwoWayPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty> TwoWay<TSource, TTarget, TTargetProperty, TSourceProperty>(this IOneWayPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty> self)
            where TSource : class
            where TTarget : class
        {
            return ((OneWayPropertyBinding<TSource, TTarget, TTargetProperty, TSourceProperty>)self)
                    .CreateTwoWay()
                    .WithConverter<ClassCastConverter<TSourceProperty, TTargetProperty>>();
        }

        public static IEventBinding<TSource, TTarget, TEventArgs> Event<TSource, TTarget, TEventArgs>(this IBinding<TSource, TTarget> self, string eventName, TEventArgs defaultEventArgs = default(TEventArgs))
            where TSource : class
            where TTarget : class
            where TEventArgs : EventArgs
        {
            return new EventBinding<TSource, TTarget, TEventArgs>(self, eventName);
        }

        public static IEventToCommandBinding<TSource, TTarget, TEventArgs, TCommand> To<TSource, TTarget, TEventArgs, TCommand>(this IEventBinding<TSource, TTarget, TEventArgs> self, Expression<Func<TSource, TCommand>> commandExpression)
            where TSource : class
            where TTarget : class
            where TEventArgs : EventArgs
            where TCommand : ICommand
        {
            return new EventToCommandBinding<TSource, TTarget, TEventArgs, TCommand>(self, commandExpression);
        }
    }
}
