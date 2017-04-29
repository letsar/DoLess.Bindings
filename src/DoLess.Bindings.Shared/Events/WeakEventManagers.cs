using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;

namespace DoLess.Bindings.EventsOld
{
    internal class CanExecuteChangedEventManager : WeakEventManager<ICommand, EventArgs> { }

    internal class PropertyChangedEventManager : WeakEventManager<INotifyPropertyChanged, PropertyChangedEventArgs>
    {
        protected override void StartListening(INotifyPropertyChanged source)
        {
            source.PropertyChanged += this.DeliverEventFromObject;
        }

        protected override void StopListening(INotifyPropertyChanged source)
        {
            source.PropertyChanged -= this.DeliverEventFromObject;
        }
    }

    internal class CollectionChangedEventManager : WeakEventManager<INotifyCollectionChanged, NotifyCollectionChangedEventArgs>
    {
        protected override void StartListening(INotifyCollectionChanged source)
        {
            source.CollectionChanged += this.DeliverEventFromObject;
        }

        protected override void StopListening(INotifyCollectionChanged source)
        {
            source.CollectionChanged -= this.DeliverEventFromObject;
        }
    }


}
