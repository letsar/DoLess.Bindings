using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DoLess.Bindings.Helpers;
using DoLess.Bindings.ObservableProperties;
using FluentAssertions;
using NUnit.Framework;

namespace DoLess.Bindings.Tests.Helpers
{
    public class ObservablePropertiesBuilderTests
    {
        [Test]
        public void ExpressionsShouldBeAsExpected()
        {
            this.ExpressionShouldBeAsExpected((ViewModel1 vm) => vm.Name, "vm(Name)");
            this.ExpressionShouldBeAsExpected((ViewModel1 vm) => vm.Name + vm.ViewModel2.Name, "vm(Name, ViewModel2(Name))");
            this.ExpressionShouldBeAsExpected((ViewModel1 vm) => vm.Name + vm.ViewModel2.Name.Length, "vm(Name, ViewModel2(Name))");
            this.ExpressionShouldBeAsExpected((ViewModel1 vm) => vm[8], "vm(Item)");
            this.ExpressionShouldBeAsExpected((ViewModel1 vm) => vm[vm.ViewModel2.Name.Length], "vm(Item, ViewModel2(Name))");
            this.ExpressionShouldBeAsExpected((ViewModel1 vm) => vm[vm.ViewModel2.Name, vm.Name.Length] + vm.Name, "vm(Item, Name, ViewModel2(Name))");
            this.ExpressionShouldBeAsExpected((ViewModel1 vm) => vm.ViewModel2.ViewModel3.Name, "vm(ViewModel2(ViewModel3(Name)))");
            this.ExpressionShouldBeAsExpected((ViewModel1 vm) => vm.NotViewModel1.ViewModel2, "vm(NotViewModel1)");
        }

        public void ExpressionShouldBeAsExpected<T, TProperty>(Expression<Func<T, TProperty>> expression, string expected)
        {
            ObservablePropertiesBuilder visitor = new ObservablePropertiesBuilder();
            visitor.Visit(expression);

            var actual = visitor.ToString();

            actual.Should().Be(expected);
        }

        public class ViewModel1 : INotifyPropertyChanged
        {

            public ViewModel2 ViewModel2 { get; set; }

            public NotViewModel1 NotViewModel1 { get; set; }

            public string Name { get; set; }


            public int[] IntArray { get; set; }

            public List<string> StringList { get; set; }

            public string this[int index]
            {
                get
                {
                    return this.GetPropertyName();
                }
            }

            public string this[string tt, int re]
            {
                get
                {
                    return this.GetPropertyName();
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            public string GetPropertyName([CallerMemberName] string propertyName = null)
            {
                return propertyName;
            }
        }

        public class ViewModel2 : INotifyPropertyChanged
        {
            public string Name { get; set; }

            public ViewModel3 ViewModel3 { get; set; }

            public event PropertyChangedEventHandler PropertyChanged;
        }

        public class ViewModel3 : INotifyPropertyChanged
        {
            public string Name { get; set; }

            public int Index { get; set; }            

            public event PropertyChangedEventHandler PropertyChanged;
        }

        public class NotViewModel1
        {
            public ViewModel2 ViewModel2 { get; set; }

            public int Index { get; set; }
        }
    }
}
