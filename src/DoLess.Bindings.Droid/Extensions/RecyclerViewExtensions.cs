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
        public static ICollectionBinding<TSource, IBindableAdapter<TItemProperty>, TItemProperty> ItemsSourceTo<TSource, TTarget, TItemProperty>(this IBinding<TSource, TTarget> self, Expression<Func<TSource, IEnumerable<TItemProperty>>> itemsSourcePropertyExpression)
            where TSource : class
            where TTarget : Android.Support.V7.Widget.RecyclerView
            where TItemProperty : class
        {
            var recyclerView = self.Target;
            var viewModel = self.Source;

            if (recyclerView != null && viewModel != null)
            {
                BindableRecyclerViewAdapter<TItemProperty> adapter = new BindableRecyclerViewAdapter<TItemProperty>();

                var propertyBinding = new Binding<TSource, IBindableAdapter<TItemProperty>>(viewModel, adapter, self)
                                          .Property(x => x.CollectionViewAdapter.ItemsSource);

                var binding = new CollectionBinding<TSource, IBindableAdapter<TItemProperty>, TItemProperty>(propertyBinding, itemsSourcePropertyExpression);

                recyclerView.SetAdapter(adapter);

                return binding;
            }
            else
            {
                return null;
            }
        }
    }
}