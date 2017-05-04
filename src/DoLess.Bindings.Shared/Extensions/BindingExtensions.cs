using System;
using System.Linq.Expressions;
using System.Windows.Input;

namespace DoLess.Bindings
{
    public static partial class BindingExtensions
    {
        public static IBinding<TSource, TTarget> Bind<TSource, TTarget>(this IView<TSource> self, TTarget target)
            where TSource : class
            where TTarget : class
        {
            return new Binding<TSource, TTarget>(self.ViewModel, target, null, self);
        }
    }
}
