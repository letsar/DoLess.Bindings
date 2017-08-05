using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace DoLess.Bindings.Tests
{
    public class NotViewModel1
    {
        public int Index { get; set; }
        public ViewModel2 ViewModel2 { get; set; }
    }

    [ImplementPropertyChanged]
    public class ViewModel1 : INotifyPropertyChanged
    {
        IList<string> range = Enumerable.Range(1, 50).Select(x => x.ToString()).ToList();

        public event PropertyChangedEventHandler PropertyChanged;

        public int[] IntArray { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DependsOn(nameof(FirstName), nameof(LastName))]
        public string FullName => $"{this.FirstName} {this.LastName}";

        public NotViewModel1 NotViewModel1 { get; set; }
        public List<string> StringList { get; set; }
        public ViewModel2 ViewModel2 { get; set; }



        public string this[int index]
        {
            get { return this.range[index]; }
            set { this.range[index] = value; }
        }

        public string this[string tt, int re]
        {
            get { return this.range[re]; }
            set { this.range[re] = value; }
        }
    }

    [ImplementPropertyChanged]
    public class ViewModel2 : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { get; set; }

        public ViewModel3 ViewModel3 { get; set; }
    }

    [ImplementPropertyChanged]
    public class ViewModel3 : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int Index { get; set; }
        public string Name { get; set; }
    }
}
