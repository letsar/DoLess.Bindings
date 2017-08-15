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
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using static Android.Views.View;

namespace DoLess.Bindings
{
    public static class IBindingCollectionExtensions
    {
        public static ICollectionBinding<TSource, TTarget, TSourceItem> ItemsSourceTo<TSource, TTarget, TSourceItem>(
            this IBinding<TSource, TTarget> self,
            Expression<Func<TSource, IEnumerable<TSourceItem>>> sourceItemsExpression)
            where TSource : class
            where TTarget : RecyclerView
        {
            // TODO.
            return null;
        }
    }
}