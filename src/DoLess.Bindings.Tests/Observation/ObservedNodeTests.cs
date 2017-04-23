using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DoLess.Bindings.Observation;
using FluentAssertions;
using NUnit.Framework;

namespace DoLess.Bindings.Tests.Observation
{
    [TestFixture]
    public class ObservedNodeTests
    {
        private int changeCount = 0;

        [SetUp]
        public void Setup()
        {
            this.changeCount = 0;
        }

        [Test]
        public void PropertyChanged01()
        {
            var vm = new ViewModel1();
            var node = this.Observe(vm, x => x.FirstName);

            node.GetWeakHandlers().Should().HaveCount(1);
            changeCount.Should().Be(0);

            vm.FirstName = "MyFirstName";

            changeCount.Should().Be(1);
        }

        [Test]
        public void PropertyChanged02()
        {
            var vm = new ViewModel1();
            var node = this.Observe(vm, x => x.FirstName + x.LastName);

            node.GetWeakHandlers().Should().HaveCount(1);
            changeCount.Should().Be(0);

            // Was null, so +1;
            vm.FirstName = "MyFirstName";

            // Was null, so +1;
            vm.LastName = "MyLastName";

            // Same
            vm.FirstName = "MyFirstName";

            // Was MyLastName, so +1;
            vm.LastName = "MyLastName2";

            changeCount.Should().Be(3);
        }

        [Test]
        public void PropertyChanged03()
        {
            var vm = new ViewModel1();
            var node = this.Observe(vm, x => x.FirstName + x.LastName + x.ViewModel2.Name);

            // Only 1 because ViewModel2 is null;
            node.GetWeakHandlers().Should().HaveCount(1);
            changeCount.Should().Be(0);

            vm.ViewModel2 = new ViewModel2();

            // ViewModel2 changed and its not null, so now there are 2 weak handlers.
            node.GetWeakHandlers().Should().HaveCount(2);

            vm.ViewModel2.Name = "Name";

            changeCount.Should().Be(2);
        }

        [Test]
        public void PropertyChanged04()
        {
            var vm = new ViewModel1();
            vm.ViewModel2 = new ViewModel2();
            vm.ViewModel2.Name = "hello";

            var node = this.Observe(vm, x => x.FirstName + x.LastName + x.ViewModel2.Name);

            node.GetWeakHandlers().Should().HaveCount(2);
            changeCount.Should().Be(0);

            vm.ViewModel2.Name = "Name";

            changeCount.Should().Be(1);
        }

        [Test]
        public void PropertyChanged05()
        {
            var vm = new ViewModel1();
            vm.ViewModel2 = new ViewModel2();
            vm.ViewModel2.Name = "hello";

            var node = this.Observe(vm, x => x.FirstName + x.LastName + x.ViewModel2.Name);

            node.GetWeakHandlers().Should().HaveCount(2);
            changeCount.Should().Be(0);

            vm.ViewModel2 = null;

            node.GetWeakHandlers().Should().HaveCount(1);
            changeCount.Should().Be(1);
        }

        [Test]
        public void PropertyChanged06()
        {
            var vm = new ViewModel1();
            vm.ViewModel2 = new ViewModel2();
            vm.ViewModel2.Name = "hello";
            vm.NotViewModel1 = new NotViewModel1();
            vm.NotViewModel1.Index = 4;

            var node = this.Observe(vm, x => x.FirstName + x.ViewModel2.Name + x.NotViewModel1.Index);

            node.GetWeakHandlers().Should().HaveCount(2);
            changeCount.Should().Be(0);

            vm.NotViewModel1.Index = 5;

            changeCount.Should().Be(0);
        }

        [Test]
        public void PropertyChanged07()
        {
            var vm = new ViewModel1();
            vm.ViewModel2 = new ViewModel2();
            vm.ViewModel2.Name = "hello";
            vm.ViewModel2.ViewModel3 = new ViewModel3();
            vm.ViewModel2.ViewModel3.Index = 4;

            var node = this.Observe(vm, x => x.FirstName + x.LastName + x.ViewModel2.Name + x.ViewModel2.ViewModel3.Index);

            node.GetWeakHandlers().Should().HaveCount(3);
            changeCount.Should().Be(0);

            vm.ViewModel2.ViewModel3.Index = 5;
            
            changeCount.Should().Be(1);
        }

        private ObservedNode Observe<T, TProperty>(T source, Expression<Func<T, TProperty>> expression)
            where T : INotifyPropertyChanged
        {
            var node = expression.AsObservedNode();
            node.Observe(source, this.OnChange);
            return node;
        }

        private void OnChange()
        {
            this.changeCount++;
        }
    }
}
