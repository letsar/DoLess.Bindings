using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DoLess.Bindings.Tests.PropertyWatching
{
    public class PropertyWatcherTests
    {
        private int hits;

        [SetUp]
        public void Setup()
        {
            this.hits = 0;
        }

        [Test]
        public void ExpressionsShouldBeAsExpected()
        {
            this.ExpressionShouldBeAsExpected((ViewModel1 x) => x.FirstName, "x(FirstName)");
            this.ExpressionShouldBeAsExpected((ViewModel1 x) => x.FirstName + x.ViewModel2.Name, "x(FirstName, ViewModel2(Name))");
            this.ExpressionShouldBeAsExpected((ViewModel1 x) => x.FirstName + x.ViewModel2.Name.Length, "x(FirstName, ViewModel2(Name))");
            this.ExpressionShouldBeAsExpected((ViewModel1 x) => x[8], "x(Item)");
            this.ExpressionShouldBeAsExpected((ViewModel1 x) => x[x.ViewModel2.Name.Length], "x(Item, ViewModel2(Name))");
            this.ExpressionShouldBeAsExpected((ViewModel1 x) => x[x.ViewModel2.Name, x.FirstName.Length] + x.FirstName, "x(FirstName, Item, ViewModel2(Name))");
            this.ExpressionShouldBeAsExpected((ViewModel1 x) => x.ViewModel2.ViewModel3.Name, "x(ViewModel2(ViewModel3(Name)))");
            this.ExpressionShouldBeAsExpected((ViewModel1 x) => x.NotViewModel1.ViewModel2, "x(NotViewModel1)");
            this.ExpressionShouldBeAsExpected((ViewModel1 x) => $"{x.FirstName} {x.ViewModel2.Name}", "x(FirstName, ViewModel2(Name))");
        }

        [Test]
        public void ProperyWatcherShouldWatchSimpleProperty()
        {
            PropertyWatcher pw = PropertyWatcher.Create((ViewModel1 x) => x.FullName);

            ViewModel1 vm = new ViewModel1();
            pw.Watch(vm, (o, s) =>
            {
                this.hits++;
            });

            this.hits.Should().Be(0);

            vm.FirstName = "Anakin";
            this.hits.Should().Be(1);

            vm.LastName = "Skywalker";
            this.hits.Should().Be(2);
        }

        [Test]
        public void ProperyWatcherShouldWatchComplexProperty()
        {
            PropertyWatcher pw = PropertyWatcher.Create((ViewModel1 x) => x.FirstName + x.ViewModel2.Name);

            ViewModel1 vm = new ViewModel1();
            vm.ViewModel2 = new ViewModel2();

            pw.Watch(vm, (o, s) =>
            {
                this.hits++;
            });

            this.hits.Should().Be(0);

            vm.FirstName = "Anakin";
            this.hits.Should().Be(1);

            vm.ViewModel2.Name = "Skywalker";
            this.hits.Should().Be(2);

            vm.ViewModel2 = new ViewModel2();
            this.hits.Should().Be(3);

            vm.ViewModel2.Name = "Skywalker";
            this.hits.Should().Be(4);
        }

        [Test]
        public void ProperyWatcherWithSimplePropertyShouldNotHaveMemoryLeakWhenDisposed()
        {
            PropertyWatcher pw = PropertyWatcher.Create((ViewModel1 x) => x.FirstName + x.ViewModel2.Name);
            
            ViewModel1 vm = new ViewModel1();
            ViewModel2 vm21 = new ViewModel2();
            ViewModel2 vm22 = new ViewModel2();
            WeakReference<ViewModel1> weakVm1 = new WeakReference<ViewModel1>(vm);
            WeakReference<ViewModel2> weakVm21 = new WeakReference<ViewModel2>(vm21);
            WeakReference<ViewModel2> weakVm22 = new WeakReference<ViewModel2>(vm22);
            vm.ViewModel2 = vm21;

            pw.Watch(vm, (o, s) =>
            {
                this.hits++;
            });            

            vm.FirstName = "Anakin";
            vm.ViewModel2.Name = "Skywalker";
            vm.ViewModel2 = vm22;
            vm.ViewModel2.Name = "Skywalker";           

            vm = null;
            vm21 = null;
            vm22 = null;
            this.TargetShouldBeAliveAfterGarbageCollected(weakVm1);
            this.TargetShouldBeDeadAfterGarbageCollected(weakVm21);
            this.TargetShouldBeAliveAfterGarbageCollected(weakVm22);

            pw.Dispose();
            this.TargetShouldBeDeadAfterGarbageCollected(weakVm1);            
            this.TargetShouldBeDeadAfterGarbageCollected(weakVm22);
        }

        [Test]
        public void ProperyWatcherWithComplexPropertyShouldNotHaveMemoryLeakWhenDisposed()
        {
            PropertyWatcher pw = PropertyWatcher.Create((ViewModel1 x) => x.FullName);

            ViewModel1 vm = new ViewModel1();
            WeakReference<ViewModel1> weak = new WeakReference<ViewModel1>(vm);

            pw.Watch(vm, (o, s) =>
            {
                this.hits++;
            });

            this.hits.Should().Be(0);

            vm.FirstName = "Anakin";

            vm = null;
            this.TargetShouldBeAliveAfterGarbageCollected(weak);

            pw.Dispose();
            this.TargetShouldBeDeadAfterGarbageCollected(weak);
        }


        private void ExpressionShouldBeAsExpected<T, TProperty>(Expression<Func<T, TProperty>> expression, string expected)
            where T : INotifyPropertyChanged
        {
            PropertyWatcher watcher = PropertyWatcher.Create(expression);

            var actual = watcher.ToString();

            actual.Should().Be(expected);
        }

        private void TargetShouldBeDeadAfterGarbageCollected<T>(WeakReference<T> weak)
            where T : class
        {
            CollectGarbage();

            weak.TryGetTarget(out T target).Should().BeFalse();
        }

        private void TargetShouldBeAliveAfterGarbageCollected<T>(WeakReference<T> weak)
            where T : class
        {
            CollectGarbage();

            weak.TryGetTarget(out T target).Should().BeTrue();
        }

        private static void CollectGarbage()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
