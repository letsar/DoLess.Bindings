using DoLess.Commands;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoLess.Bindings.Sample.ViewModels
{
    [ImplementPropertyChanged]
    public class TextsViewModel
    {
        public TextsViewModel()
        {                   
        }

        public PersonViewModel Person { get; set; }
    }
}
