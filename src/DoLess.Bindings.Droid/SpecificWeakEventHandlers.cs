using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace DoLess.Bindings.Droid
{
    internal class ClickWeakEventHandler : WeakEventHandler<Button, EventArgs>
    {
        public ClickWeakEventHandler(Button eventSource, EventHandler<EventArgs> handler, string eventName = null) : base(eventSource, handler, eventName)
        {
        }

        protected override void StartListening(Button source)
        {
            source.Click += this.OnEvent;
        }

        protected override void StopListening(Button source)
        {
            source.Click -= this.OnEvent;
        }
    }
}