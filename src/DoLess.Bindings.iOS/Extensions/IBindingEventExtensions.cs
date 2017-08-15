using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Input;
using UIKit;

namespace DoLess.Bindings
{
    public static class IBindingEventExtensions
    {
        public static IEventToCommandBinding<TSource, TTarget, EventArgs, TCommand> ClickTo<TSource, TTarget, TCommand>(
            this IBinding<TSource, TTarget> self,
            Expression<Func<TSource, TCommand>> commandExpression)
            where TSource : class
            where TTarget : UIControl
            where TCommand : ICommand
        {
            EventBindingSource<TSource, TTarget, EventArgs> eventBindingSource = new EventBindingSource<TSource, TTarget, EventArgs>(
                self,
                (x, h) => x.TouchUpInside += new EventHandler(h),
                (x, h) => x.TouchUpInside -= new EventHandler(h)
                );
            return eventBindingSource.To<TCommand>(commandExpression, x => x.Enabled);
        }       
    }
}