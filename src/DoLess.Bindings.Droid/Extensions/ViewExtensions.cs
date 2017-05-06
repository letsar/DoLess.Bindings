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
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace DoLess.Bindings
{
    public static class ViewExtensions
    {
        public static IEventToCommandBinding<TSource, TTarget, EventArgs, TCommand> ClickTo<TSource, TTarget, TCommand>(this IBinding<TSource, TTarget> self, Expression<Func<TSource, TCommand>> commandExpression)
            where TSource : class
            where TTarget : View
            where TCommand : ICommand
        {
            return self.EventTo(commandExpression, (s, e) => new ClickWeakEventHandler<TTarget>(s, e), b => b.Enabled);
        }

        public static IEventToCommandBinding<TSource, TTarget, EventArgs, TCommand> LongClickTo<TSource, TTarget, TCommand>(this IBinding<TSource, TTarget> self, Expression<Func<TSource, TCommand>> commandExpression)
            where TSource : class
            where TTarget : View
            where TCommand : ICommand
        {
            return self.EventTo(commandExpression, (s, e) => new LongClickWeakEventHandler<TTarget>(s, e));
        }
    }
}