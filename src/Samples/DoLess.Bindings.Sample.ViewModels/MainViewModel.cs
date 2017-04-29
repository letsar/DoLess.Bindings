using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DoLess.Commands;
using PropertyChanged;

namespace DoLess.Bindings.Sample.ViewModels
{
    [ImplementPropertyChanged]
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.CancellableCommand = Command.CreateFromTask(this.ExecuteCancellableCommand);
        }

        public PersonViewModel Person { get; set; }

        public ICancellableCommand CancellableCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private Task ExecuteCancellableCommand(CancellationToken ct)
        {
            return Task.Delay(3000, ct);
        }
    }

    [ImplementPropertyChanged]
    public class PersonViewModel : INotifyPropertyChanged
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }


}
