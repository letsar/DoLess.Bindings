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
    public class ButtonsViewModel
    {
        public ButtonsViewModel()
        {
            this.CancellableCommand = Command.CreateFromTask(this.ExecuteCancellableCommand);            
        }

        public ICancellableCommand CancellableCommand { get; }

        private Task ExecuteCancellableCommand(CancellationToken ct)
        {
            return Task.Delay(3000, ct);
        }
    }
}
