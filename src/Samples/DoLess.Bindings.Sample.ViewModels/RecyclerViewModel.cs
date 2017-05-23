using DoLess.Commands;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoLess.Bindings.Sample.ViewModels
{
    [ImplementPropertyChanged]
    public class RecyclerViewModel
    {
        public RecyclerViewModel()
        {
            this.SelectPersonCommand = Command.CreateFromAction<PersonViewModel>(this.SelectPerson);
        }

        public ObservableCollection<PersonViewModel> Persons { get; set; }

        public ICommand<PersonViewModel> SelectPersonCommand { get; }

        public void InitializePersons()
        {
            this.Persons = new ObservableCollection<PersonViewModel>(Enumerable.Range(1, 1000).Select(x => new PersonViewModel(x.ToString(), (x + 1).ToString())));
        }

        private void SelectPerson(PersonViewModel vm)
        {
            vm.LastName = vm.LastName + "fy";
        }
    }
}
