using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace DoLess.Bindings
{
    public static class RecyclerViewExtensions
    {
        public static ICollectionBinding<TItemProperty> ItemsSourceTo<TSource, TTarget, TItemProperty>(this IBinding<TSource, TTarget> self, Expression<Func<TSource, IEnumerable<TItemProperty>>> itemsSourcePropertyExpression)
            where TSource : class
            where TTarget : Android.Support.V7.Widget.RecyclerView
        {
            var recyclerView = self.Target;
            var viewModel = self.Source;

            BindableRecyclerViewAdapter<TItemProperty> adapter = null;
            if (recyclerView != null && viewModel != null)
            {
                adapter = recyclerView.GetAdapter() as BindableRecyclerViewAdapter<TItemProperty>;

                if (adapter == null)
                {
                    adapter = new BindableRecyclerViewAdapter<TItemProperty>();
                    recyclerView.SetAdapter(adapter);
                }

                var propertyBinding = new Binding<TSource, BindableRecyclerViewAdapter<TItemProperty>>(viewModel, adapter, self)
                                          .Property(x => x.ItemsSource);

                return new CollectionBinding<TSource, BindableRecyclerViewAdapter<TItemProperty>, TItemProperty>(propertyBinding, itemsSourcePropertyExpression);
            }
            else
            {
                return null;
            }
        }
    }
}