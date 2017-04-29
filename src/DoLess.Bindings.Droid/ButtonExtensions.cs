using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Input;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace DoLess.Bindings
{
    public static class ButtonExtensions
    {
        public static IEventToCommandBinding<TSource, TTarget, EventArgs, TCommand> ClickTo<TSource, TTarget, TCommand>(this IBinding<TSource, TTarget> self, Expression<Func<TSource, TCommand>> commandExpression)
            where TSource : class, INotifyPropertyChanged
            where TTarget : Button
            where TCommand : ICommand
        {
            IEventBinding<TSource, TTarget, EventArgs> eventBinding = self.Event(nameof(Button.Click), EventArgs.Empty);
            return new ClickEventToCommandBinding<TSource, TTarget, EventArgs, TCommand>(eventBinding, commandExpression, b => b.Clickable);
        }
    }
}