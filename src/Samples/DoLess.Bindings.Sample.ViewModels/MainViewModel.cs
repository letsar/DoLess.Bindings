using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public ObservableCollection<PersonViewModel> Persons { get; set; }

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
        public PersonViewModel()
        {
            this.ChangeFirstNameCommand = Command.CreateFromAction(this.ChangeFirstName);
        }

        public PersonViewModel(string firstName, string lastName) : this()
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICommand ChangeFirstNameCommand { get; }

        private void ChangeFirstName()
        {
            this.FirstName = this.FirstName + "fy";
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }


}
