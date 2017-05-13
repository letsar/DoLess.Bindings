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
    public class ExpandableViewModel
    {
        public ExpandableViewModel()
        {            
        }

        public ObservableCollection<PersonViewModel> Persons { get; set; }
               

        public void InitializePersones()
        {
            this.Persons = new ObservableCollection<PersonViewModel>(Enumerable.Range(1, 1000).Select(x => new PersonViewModel(x.ToString(), (x + 1).ToString())));
        }
    }
}
