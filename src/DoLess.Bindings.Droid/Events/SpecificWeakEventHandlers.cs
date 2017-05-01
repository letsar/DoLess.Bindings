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
}