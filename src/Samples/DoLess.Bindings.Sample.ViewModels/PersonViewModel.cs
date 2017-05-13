using DoLess.Commands;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoLess.Bindings.Sample.ViewModels
{
    [ImplementPropertyChanged]
    public class PersonViewModel
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
    }
}
