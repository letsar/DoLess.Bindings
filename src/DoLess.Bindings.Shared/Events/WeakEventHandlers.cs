using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DoLess.Bindings
{
    internal class INotifyPropertyChangedWeakEventHandler : WeakEventHandler<INotifyPropertyChanged, PropertyChangedEventArgs>
    {
        public INotifyPropertyChangedWeakEventHandler(INotifyPropertyChanged eventSource, EventHandler<PropertyChangedEventArgs> handler) : base(eventSource, handler)
        {
        }

        protected override void StartListening(INotifyPropertyChanged source)
        {
            source.PropertyChanged += this.OnEvent;
        }

        protected override void StopListening(INotifyPropertyChanged source)
        {
            source.PropertyChanged -= this.OnEvent;
        }
    }
}
