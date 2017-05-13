using Android.App;
using Android.Views;
using System;
using System.Linq.Expressions;

namespace DoLess.Bindings
{
    internal partial class Binding<TSource, TTarget>
    {
        public Binding(IViewHolder<TSource> viewHolder, TTarget target, Binding linkedBinding) :
            this(viewHolder.ViewModel, target, linkedBinding)
        {
            Bindings.SetPayload(this, viewHolder);
        }

        public IBinding<TSource, TNewTarget> Bind<TNewTarget>(int resourceId)
            where TNewTarget : View
        {
            var creator = Bindings.GetPayload(this);
            TNewTarget target = null;
            if (creator == null)
            {
                Bindings.LogError("the creator does not exists");
            }
            else
            {
                var viewHolder = creator as IViewHolder<TSource>;
                if (viewHolder != null)
                {
                    target = viewHolder.GetView<TNewTarget>(resourceId);
                }

                var activity = creator as Activity;
                if (activity != null)
                {
                    target = activity.FindViewById<TNewTarget>(resourceId);
                }

                var view = creator as View;
                if (view != null)
                {
                    target = view.FindViewById<TNewTarget>(resourceId);
                }

                if (target == null)
                {
                    Bindings.LogError("the creator must be a 'IViewHolder', or an 'Activity', or a 'View'");
                }
            }
            return new Binding<TSource, TNewTarget>(this.Source, target, this);
        }
    }
}
