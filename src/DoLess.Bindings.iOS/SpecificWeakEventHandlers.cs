using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace DoLess.Bindings
{
    internal class ClickWeakEventHandler : WeakEventHandler<UIButton, EventArgs>
    {
        public ClickWeakEventHandler(UIButton eventSource, EventHandler<EventArgs> handler, string eventName = null) : base(eventSource, handler, eventName)
        {
        }

        protected override void StartListening(UIButton source)
        {
            source.TouchUpInside += this.OnEvent;
        }

        protected override void StopListening(UIButton source)
        {
            source.TouchUpInside -= this.OnEvent;
        }
    }
}
