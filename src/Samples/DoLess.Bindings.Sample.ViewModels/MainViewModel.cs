using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace DoLess.Bindings.Sample.ViewModels
{
    [ImplementPropertyChanged]
    public class MainViewModel : INotifyPropertyChanged
    {
        public PersonViewModel Person { get; set; }   

        public event PropertyChangedEventHandler PropertyChanged;
    }

    [ImplementPropertyChanged]
    public class PersonViewModel : INotifyPropertyChanged
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
