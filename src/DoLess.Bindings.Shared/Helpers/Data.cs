using System;
using System.Collections.Generic;
using System.Text;

namespace DoLess.Bindings
{
    public static class Data
    {
        public static IBinding<TSource, TTarget> Bind<TSource, TTarget>(TSource source, TTarget target)
            where TSource : class
            where TTarget : class
        {
            return new Binding<TSource, TTarget>(source, target, null);
        }
    }
}
