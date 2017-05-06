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
using System.Windows.Input;

namespace DoLess.Bindings
{
    internal class CollectionBinding<TSource, TTarget, TItemProperty> :
        OneWayPropertyBinding<TSource, TTarget, IEnumerable<TItemProperty>, IEnumerable<TItemProperty>>,
        ICollectionBinding<TSource, TItemProperty>
        where TSource : class
        where TTarget : BindableRecyclerViewAdapter<TItemProperty>
        where TItemProperty : class
    {
        public CollectionBinding(IPropertyBinding<TSource, TTarget, IEnumerable<TItemProperty>> propertyBinding, Expression<Func<TSource, IEnumerable<TItemProperty>>> itemsSourcePropertyExpression) :
            base(propertyBinding, itemsSourcePropertyExpression)
        {
            this.WithConverter<IdentityConverter<IEnumerable<TItemProperty>>>();
        }

        public ICollectionBinding<TSource, TItemProperty> WithItemTemplateSelector<T>()
            where T : IItemTemplateSelector<TItemProperty>, new()
        {
            var target = this.Target;
            if (target != null)
            {
                target.ItemTemplateSelector = Cache<T>.Instance;
            }
            return this;
        }

        public ICollectionBinding<TSource, TItemProperty> WithItemTemplate(int resourceId)
        {
            var target = this.Target;
            if (target != null)
            {
                target.ItemTemplateSelector = new SingleItemTemplateSelector<TItemProperty>(resourceId);
            }
            return this;
        }

        public ICollectionBinding<TSource, TItemProperty> BindItemTo(Func<IViewHolder<TItemProperty>, IBinding> itemBinder)
        {
            var target = this.Target;
            if (target != null)
            {
                target.ItemBinder = itemBinder;
            }
            return this;
        }

        //public IEventToCommandBinding<TSource, Android.Support.V7.Widget.RecyclerView, EventArgs<TItemProperty>, TCommand> ItemClickTo<TCommand>(Expression<Func<TSource, TCommand>> commandExpression)
        //    where TCommand : ICommand
        //{
        //    return this.EventTo<TSource, Android.Support.V7.Widget.RecyclerView, TCommand, TItemProperty>(commandExpression, (s, e) => new ItemClickWeakEventHandler<TItemProperty>(s, e))
        //               .WithConverter<EventArgsConverter<EventArgs<TItemProperty>, TItemProperty>>();
        //}

        //public IEventToCommandBinding<TSource, Android.Support.V7.Widget.RecyclerView, EventArgs<TItemProperty>, TCommand> ItemLongClickTo<TCommand>(Expression<Func<TSource, TCommand>> commandExpression)
        //    where TCommand : ICommand
        //{
        //    return this.EventTo<TSource, Android.Support.V7.Widget.RecyclerView, TCommand, TItemProperty>(commandExpression, (s, e) => new ItemLongClickWeakEventHandler<TItemProperty>(s, e))
        //               .WithConverter<EventArgsConverter<EventArgs<TItemProperty>, TItemProperty>>();
        //}
    }
}