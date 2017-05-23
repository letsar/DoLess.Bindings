using System;
using Android.Views;
using System.Collections.Generic;

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

    internal class ItemClickWeakEventHandler<TTarget, TItem> : WeakEventHandler<TTarget, EventArgs<TItem>>
        where TTarget : class, ICollectionViewAdapter<TItem>
        where TItem : class
    {
        public ItemClickWeakEventHandler(TTarget eventSource, EventHandler<EventArgs<TItem>> handler) :
            base(eventSource, handler, nameof(IViewBinder<TItem>.Click))
        {
        }

        protected override void StartListening(TTarget source)
        {
            source.ItemBinder.Click += this.OnEvent;
        }

        protected override void StopListening(TTarget source)
        {
            source.ItemBinder.Click -= this.OnEvent;
        }
    }

    internal class ItemLongClickWeakEventHandler<TTarget, TItem> : WeakEventHandler<TTarget, EventArgs<TItem>>
        where TTarget : class, ICollectionViewAdapter<TItem>
        where TItem : class
    {
        public ItemLongClickWeakEventHandler(TTarget eventSource, EventHandler<EventArgs<TItem>> handler) :
            base(eventSource, handler, nameof(IViewBinder<TItem>.LongClick))
        {
        }

        protected override void StartListening(TTarget source)
        {
            source.ItemBinder.LongClick += this.OnEvent;
        }

        protected override void StopListening(TTarget source)
        {
            source.ItemBinder.LongClick -= this.OnEvent;
        }
    }

    internal class SubItemClickWeakEventHandler<TTarget, TItem, TSubItem> : WeakEventHandler<TTarget, EventArgs<TSubItem>>
        where TTarget : class, ICollectionViewAdapter<TItem, TSubItem>
        where TItem : class, IEnumerable<TSubItem>
        where TSubItem : class
    {
        public SubItemClickWeakEventHandler(TTarget eventSource, EventHandler<EventArgs<TSubItem>> handler) :
            base(eventSource, handler, nameof(IViewBinder<TSubItem>.Click))
        {
        }

        protected override void StartListening(TTarget source)
        {
            source.SubItemBinder.Click += this.OnEvent;
        }

        protected override void StopListening(TTarget source)
        {
            source.SubItemBinder.Click -= this.OnEvent;
        }
    }

    internal class SubItemLongClickWeakEventHandler<TTarget, TItem, TSubItem> : WeakEventHandler<TTarget, EventArgs<TSubItem>>
        where TTarget : class, ICollectionViewAdapter<TItem, TSubItem>
        where TItem : class, IEnumerable<TSubItem>
        where TSubItem : class
    {
        public SubItemLongClickWeakEventHandler(TTarget eventSource, EventHandler<EventArgs<TSubItem>> handler) :
            base(eventSource, handler, nameof(IViewBinder<TSubItem>.Click))
        {
        }

        protected override void StartListening(TTarget source)
        {
            source.SubItemBinder.LongClick += this.OnEvent;
        }

        protected override void StopListening(TTarget source)
        {
            source.SubItemBinder.LongClick -= this.OnEvent;
        }
    }
}