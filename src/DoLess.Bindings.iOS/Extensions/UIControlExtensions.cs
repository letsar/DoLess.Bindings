using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Input;
using UIKit;

namespace DoLess.Bindings
{
    public static class UIControlExtensions
    {
        public static IEventToCommandBinding<TSource, TTarget, EventArgs, TCommand> ClickTo<TSource, TTarget, TCommand>(IBinding<TSource, TTarget> self, Expression<Func<TSource, TCommand>> commandExpression)
            where TSource : class
            where TTarget : UIControl
            where TCommand : ICommand
        {
            IEventBinding<TSource, TTarget, EventArgs> eventBinding = new EventBinding<TSource, TTarget, EventArgs>(self, (s, e) => new ClickWeakEventHandler<TTarget>(s, e));
            return new EventToCommandBinding<TSource, TTarget, EventArgs, TCommand>(eventBinding, commandExpression, b => b.Enabled);
        }
    }
}
