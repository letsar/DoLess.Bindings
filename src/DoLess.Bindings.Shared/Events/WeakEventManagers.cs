using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace DoLess.Bindings
{
    internal class CanExecuteChangedEventManager : WeakEventManager<ICommand, EventArgs> { }
    internal class PropertyChangedEventManager : WeakEventManager<INotifyPropertyChanged, PropertyChangedEventArgs> { }
    internal class CollectionChangedEventManager : WeakEventManager<INotifyCollectionChanged, NotifyCollectionChangedEventArgs> { }
}
