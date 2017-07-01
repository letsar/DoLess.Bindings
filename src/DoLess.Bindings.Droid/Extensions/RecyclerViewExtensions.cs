using Android.Support.V7.Widget;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DoLess.Bindings
{
    public static class RecyclerViewExtensions
    {
        public static ICollectionBinding<TSource, ICollectionViewAdapter<TItem>, TItem> ItemsSourceTo<TSource, TItem>(
            this IBinding<TSource, RecyclerView> self,
            Expression<Func<TSource, IEnumerable<TItem>>> itemsSourcePropertyExpression,
            ICollectionViewAdapter<TItem> adapter = null)
            where TSource : class
            where TItem : class
        {
            adapter = adapter ?? new BindableRecyclerViewAdapter<TItem>();

            var binding = ((Binding<TSource, RecyclerView>)self).Bind(adapter)
                                                                .Property(x => x.ItemsSource);

            var view = self.Target;
            if (view != null)
            {
                var viewAdapter = adapter as RecyclerView.Adapter;

                if (viewAdapter != null)
                {
                    view.SetAdapter(viewAdapter);
                }
                else
                {
                    Bindings.LogError($"the adapter of type {adapter.GetType().FullName} does not inherit from {nameof(RecyclerView.Adapter)}.");
                }
            }

            return new CollectionBinding<TSource, ICollectionViewAdapter<TItem>, TItem>(binding, itemsSourcePropertyExpression);
        }
    }
}