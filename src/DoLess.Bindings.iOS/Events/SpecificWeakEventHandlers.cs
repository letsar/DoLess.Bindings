using System;
using UIKit;

namespace DoLess.Bindings
{
    internal class ClickWeakEventHandler<TSource> : WeakEventHandler<TSource, EventArgs>
        where TSource : UIControl
    {
        public ClickWeakEventHandler(TSource eventSource, EventHandler<EventArgs> handler) : 
            base(eventSource, handler, nameof(UIControl.TouchUpInside))
        {
        }

        protected override void StartListening(TSource source)
        {
            source.TouchUpInside += this.OnEvent;
        }

        protected override void StopListening(TSource source)
        {
            source.TouchUpInside -= this.OnEvent;
        }
    }
}
