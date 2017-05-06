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

    internal class ItemClickWeakEventHandler<TItemProperty> : WeakEventHandler<BindableRecyclerViewAdapter<TItemProperty>, EventArgs<TItemProperty>>        
        where TItemProperty : class
    {
        public ItemClickWeakEventHandler(BindableRecyclerViewAdapter<TItemProperty> eventSource, EventHandler<EventArgs<TItemProperty>> handler) :
            base(eventSource, handler, nameof(BindableRecyclerViewAdapter<TItemProperty>.ItemClick))
        {
        }

        protected override void StartListening(BindableRecyclerViewAdapter<TItemProperty> source)
        {
            source.ItemClick += this.OnEvent;
        }

        protected override void StopListening(BindableRecyclerViewAdapter<TItemProperty> source)
        {
            source.ItemClick -= this.OnEvent;
        }
    }

    internal class ItemLongClickWeakEventHandler<TItemProperty> : WeakEventHandler<BindableRecyclerViewAdapter<TItemProperty>, EventArgs<TItemProperty>>        
        where TItemProperty : class
    {
        public ItemLongClickWeakEventHandler(BindableRecyclerViewAdapter<TItemProperty> eventSource, EventHandler<EventArgs<TItemProperty>> handler) :
            base(eventSource, handler, nameof(BindableRecyclerViewAdapter<TItemProperty>.ItemLongClick))
        {
        }

        protected override void StartListening(BindableRecyclerViewAdapter<TItemProperty> source)
        {
            source.ItemLongClick += this.OnEvent;
        }

        protected override void StopListening(BindableRecyclerViewAdapter<TItemProperty> source)
        {
            source.ItemLongClick -= this.OnEvent;
        }
    }
}