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
    internal class CollectionBinding<TSource, TTarget, TItemProperty> :
        OneWayPropertyBinding<TSource, TTarget, IEnumerable<TItemProperty>, IEnumerable<TItemProperty>>,
        ICollectionBinding<TItemProperty>
        where TSource : class
        where TTarget : BindableRecyclerViewAdapter<TItemProperty>
    {
        public CollectionBinding(IPropertyBinding<TSource, TTarget, IEnumerable<TItemProperty>> propertyBinding, Expression<Func<TSource, IEnumerable<TItemProperty>>> itemsSourcePropertyExpression) :
            base(propertyBinding, itemsSourcePropertyExpression)
        {
            this.WithConverter<IdentityConverter<IEnumerable<TItemProperty>>>();
        }

        public ICollectionBinding<TItemProperty> WithItemTemplateSelector<T>()
            where T : IItemTemplateSelector<TItemProperty>, new()
        {
            var target = this.Target;
            if (target != null)
            {
                target.ItemTemplateSelector = Cache<T>.Instance;
            }
            return this;
        }

        public ICollectionBinding<TItemProperty> WithItemTemplate(int resourceId)
        {
            var target = this.Target;
            if (target != null)
            {
                target.ItemTemplateSelector = new SingleItemTemplateSelector<TItemProperty>(resourceId);
            }
            return this;
        }

        public ICollectionBinding<TItemProperty> BindItemTo(Func<TItemProperty, IViewHolder, IBinding> itemBinder)
        {
            var target = this.Target;
            if (target != null)
            {
                target.ItemBinder = itemBinder;
            }
            return this;
        }
    }
}