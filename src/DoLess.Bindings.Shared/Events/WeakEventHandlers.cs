using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

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

    internal class CanExecuteChangedWeakEventHandler : WeakEventHandler<ICommand, EventArgs>
    {
        public CanExecuteChangedWeakEventHandler(ICommand eventSource, EventHandler<EventArgs> handler) : base(eventSource, handler)
        {
        }

        protected override void StartListening(ICommand source)
        {
            source.CanExecuteChanged += this.OnEvent;
        }

        protected override void StopListening(ICommand source)
        {
            source.CanExecuteChanged -= this.OnEvent;
        }
    }
}
