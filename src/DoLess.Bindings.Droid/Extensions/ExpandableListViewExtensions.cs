using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DoLess.Bindings
{
    public static class ExpandableListViewExtensions
    {
        public static IOneWayPropertyBinding<TSource, ICollectionViewAdapter<TItem, TSubItem>, IEnumerable<TItem>, IEnumerable<TItem>> ItemsSourceTo<TSource, TItem, TSubItem>(
            this IBinding<TSource, ExpandableListView> self,
            Expression<Func<TSource, IEnumerable<TItem>>> itemsSourcePropertyExpression,
            ICollectionViewAdapter<TItem, TSubItem> adapter = null)
            where TSource : class
            where TItem : class, IEnumerable<TSubItem>
            where TSubItem : class
        {           
            adapter = adapter ?? new BindableBaseExpandableListAdapter<TItem, TSubItem>();

            var binding = ((Binding<TSource, ExpandableListView>)self).Bind(adapter)
                                                                      .Property(x => x.ItemsSource)
                                                                      .To(itemsSourcePropertyExpression);

            var view = self.Target;
            if (view != null)
            {
                var viewAdapter = adapter as IExpandableListAdapter;

                if (viewAdapter != null)
                {
                    view.SetAdapter(viewAdapter);
                }
                else
                {
                    Bindings.LogError($"the adapter of type {adapter.GetType().FullName} does not implement {nameof(IExpandableListAdapter)}.");
                }
            }

            return binding;
        }
    }
}