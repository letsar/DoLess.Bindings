using System;
using System.Collections.Generic;
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
using static Android.Views.View;

namespace DoLess.Bindings
{
    public static class IBindingExtensions
    {
        public static IEventToCommandBinding<TSource, TTarget, EventArgs, TCommand> ClickTo<TSource, TTarget, TCommand>(
            this IBinding<TSource, TTarget> self,
            Expression<Func<TSource, TCommand>> commandExpression)
            where TSource : class
            where TTarget : View
            where TCommand : ICommand
        {
            EventBindingSource<TSource, TTarget, EventArgs> eventBindingSource = new EventBindingSource<TSource, TTarget, EventArgs>(
                self,
                (x, h) => x.Click += new EventHandler(h),
                (x, h) => x.Click -= new EventHandler(h)
                );
            return eventBindingSource.To<TCommand>(commandExpression, x => x.Enabled);
        }

        public static IEventToCommandBinding<TSource, TTarget, LongClickEventArgs, TCommand> LongClickTo<TSource, TTarget, TCommand>(
            this IBinding<TSource, TTarget> self,
            Expression<Func<TSource, TCommand>> commandExpression)
            where TSource : class
            where TTarget : View
            where TCommand : ICommand
        {
            EventBindingSource<TSource, TTarget, LongClickEventArgs> eventBindingSource = new EventBindingSource<TSource, TTarget, LongClickEventArgs>(
                self,
                (x, h) => x.LongClick += h,
                (x, h) => x.LongClick -= h
                );
            return eventBindingSource.To<TCommand>(commandExpression);
        }
    }
}