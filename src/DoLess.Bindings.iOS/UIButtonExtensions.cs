using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Input;
using UIKit;

namespace DoLess.Bindings
{
    public static class UIButtonExtensions
    {
        public static IEventToCommandBinding<TSource, UIButton, EventArgs, TCommand> Click<TSource, TCommand>(IBinding<TSource, UIButton> self, Expression<Func<TSource, TCommand>> commandExpression)
            where TSource : class, INotifyPropertyChanged
            where TCommand : ICommand
        {            
            IEventBinding<TSource, UIButton, EventArgs> eventBinding = self.Event<TSource, UIButton, EventArgs>(nameof(UIButton.TouchUpInside));
            return new ClickEventToCommandBinding<TSource, UIButton, EventArgs, TCommand>(eventBinding, commandExpression, b => b.Enabled);
        }
    }
}
