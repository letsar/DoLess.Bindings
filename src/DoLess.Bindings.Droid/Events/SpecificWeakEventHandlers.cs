using System;
using Android.Views;

namespace DoLess.Bindings
{
    internal class ClickWeakEventHandler<TSource> : WeakEventHandler<TSource, EventArgs>
        where TSource : View
    {
        public ClickWeakEventHandler(TSource eventSource, EventHandler<EventArgs> handler) :
            base(eventSource, handler, nameof(View.Click))
        {
        }

        protected override void StartListening(TSource source)
        {
            source.Click += this.OnEvent;
        }

        protected override void StopListening(TSource source)
        {
            source.Click -= this.OnEvent;
        }
    }

    internal class LongClickWeakEventHandler<TSource> : WeakEventHandler<TSource, EventArgs>
    where TSource : View
    {
        public LongClickWeakEventHandler(TSource eventSource, EventHandler<EventArgs> handler) :
            base(eventSource, handler, nameof(View.LongClick))
        {
        }

        protected override void StartListening(TSource source)
        {
            source.LongClick += this.OnEvent;
        }

        protected override void StopListening(TSource source)
        {
            source.LongClick -= this.OnEvent;
        }
    }

    internal class ItemClickWeakEventHandler<TTarget, TItemProperty> : WeakEventHandler<TTarget, EventArgs<TItemProperty>>      
        where TTarget : class, IRecyclerViewAdapter<TItemProperty>
        where TItemProperty : class
    {
        public ItemClickWeakEventHandler(TTarget eventSource, EventHandler<EventArgs<TItemProperty>> handler) :
            base(eventSource, handler, nameof(IRecyclerViewAdapter<TItemProperty>.ItemClick))
        {
        }

        protected override void StartListening(TTarget source)
        {
            source.ItemClick += this.OnEvent;
        }

        protected override void StopListening(TTarget source)
        {
            source.ItemClick -= this.OnEvent;
        }
    }

    internal class ItemLongClickWeakEventHandler<TTarget, TItemProperty> : WeakEventHandler<TTarget, EventArgs<TItemProperty>>
        where TTarget : class, IRecyclerViewAdapter<TItemProperty>
        where TItemProperty : class
    {
        public ItemLongClickWeakEventHandler(TTarget eventSource, EventHandler<EventArgs<TItemProperty>> handler) :
            base(eventSource, handler, nameof(IRecyclerViewAdapter<TItemProperty>.ItemLongClick))
        {
        }

        protected override void StartListening(TTarget source)
        {
            source.ItemLongClick += this.OnEvent;
        }

        protected override void StopListening(TTarget source)
        {
            source.ItemLongClick -= this.OnEvent;
        }
    }
}