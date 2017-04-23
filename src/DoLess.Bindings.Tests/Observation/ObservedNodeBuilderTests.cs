using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DoLess.Bindings.Helpers;
using DoLess.Bindings.Observation;
using FluentAssertions;
using NUnit.Framework;

namespace DoLess.Bindings.Tests.Observation
{
    public class ObservedNodeBuilderTests
    { 
        [Test]
        public void ExpressionsShouldBeAsExpected()
        {
            this.ExpressionShouldBeAsExpected((ViewModel1 vm) => vm.FirstName, "vm(FirstName)");
            this.ExpressionShouldBeAsExpected((ViewModel1 vm) => vm.FirstName + vm.ViewModel2.Name, "vm(FirstName, ViewModel2(Name))");
            this.ExpressionShouldBeAsExpected((ViewModel1 vm) => vm.FirstName + vm.ViewModel2.Name.Length, "vm(FirstName, ViewModel2(Name))");
            this.ExpressionShouldBeAsExpected((ViewModel1 vm) => vm[8], "vm(Item)");
            this.ExpressionShouldBeAsExpected((ViewModel1 vm) => vm[vm.ViewModel2.Name.Length], "vm(Item, ViewModel2(Name))");
            this.ExpressionShouldBeAsExpected((ViewModel1 vm) => vm[vm.ViewModel2.Name, vm.FirstName.Length] + vm.FirstName, "vm(FirstName, Item, ViewModel2(Name))");
            this.ExpressionShouldBeAsExpected((ViewModel1 vm) => vm.ViewModel2.ViewModel3.Name, "vm(ViewModel2(ViewModel3(Name)))");
            this.ExpressionShouldBeAsExpected((ViewModel1 vm) => vm.NotViewModel1.ViewModel2, "vm(NotViewModel1)");
            this.ExpressionShouldBeAsExpected((ViewModel1 vm) => $"{vm.FirstName} {vm.ViewModel2.Name}", "vm(FirstName, ViewModel2(Name))");
        }

        public void ExpressionShouldBeAsExpected<T, TProperty>(Expression<Func<T, TProperty>> expression, string expected)
        {
            ObservedNodeBuilder visitor = new ObservedNodeBuilder();
            visitor.Visit(expression);

            var actual = visitor.ToString();

            actual.Should().Be(expected);
        }
    }
}
