using System;
using System.Linq.Expressions;

namespace DoLess.Bindings
{
    /// <summary>
    /// Represents a binding between a source and a target.
    /// </summary>
    public partial interface IBinding<TSource, TTarget> : 
        IBinding<TSource>
        where TSource : class
        where TTarget : class
    {
        TTarget Target { get; }

        IPropertyBindingSource<TSource, TTarget, TTargetProperty> Property<TTargetProperty>(Expression<Func<TTarget, TTargetProperty>> targetPropertyExpression);

        IEventBindingSource<TSource, TTarget, TEventArgs> Event<TEventArgs>(Action<TTarget, EventHandler<TEventArgs>> addHandler, Action<TTarget, EventHandler<TEventArgs>> removeHandler)
            where TEventArgs : EventArgs;
    }
}
