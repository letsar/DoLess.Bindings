using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DoLess.Bindings
{
    public static class ExpandableListViewExtensions
    {
        public static ICollectionBinding<TSource, ICollectionViewAdapter<IEnumerable<TSubItem>, TSubItem>, IEnumerable<TSubItem>, TSubItem> ItemsSourceTo<TSource, TSubItem>(
            this IBinding<TSource, ExpandableListView> self,
            Expression<Func<TSource, IEnumerable<IEnumerable<TSubItem>>>> itemsSourcePropertyExpression,
            ICollectionViewAdapter<IEnumerable<TSubItem>, TSubItem> adapter = null)
            where TSource : class            
            where TSubItem : class
        {
            adapter = adapter ?? new BindableBaseExpandableListAdapter<IEnumerable<TSubItem>, TSubItem>();

            var binding = ((Binding<TSource, ExpandableListView>)self).Bind(adapter)
                                                                      .Property(x => x.ItemsSource);

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

            return new CollectionBinding<TSource, ICollectionViewAdapter<IEnumerable<TSubItem>, TSubItem>, IEnumerable<TSubItem>, TSubItem>(binding, itemsSourcePropertyExpression);
        }
    }
}