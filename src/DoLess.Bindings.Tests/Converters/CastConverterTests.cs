using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace DoLess.Bindings.Tests.Converters
{
    [TestFixture]
    public class CastConverterTests
    {
        [TestCase(58, 58.65D)]
        [TestCase(58, 58.65F)]
        [TestCase(58, 58L)]
        [TestCase(58, 58UL)]
        [TestCase(58, 58U)]
        public void TestCastStruct<TSource, TTarget>(TSource source, TTarget target)
        {
            var converter = Cache<StructCastConverter<TSource, TTarget>>.Instance;

            var castFromSource = converter.ConvertFromSource(source);
            var castFromTarget = converter.ConvertFromTarget(target);
        }

        [Test]
        public void TestCastClass()
        {
            this.TestCastClassConvertFromTargetFail(new Dog(), new Animal());
            this.TestCastClassConvertFromTargetFail(new Cat(), new Animal());
            this.TestCastClassConvertFromTargetFail(new Tigri(), new Cat());
            this.TestCastClassConvertFromTargetFail(new Tigri(), new Animal());
            this.TestCastClassConvertFromBothFail(new Dog(), new Cat());
        }

        private void TestCastClassConvertFromTargetFail<TSource, TTarget>(TSource source, TTarget target)
        {
            var converter = Cache<ClassCastConverter<TSource, TTarget>>.Instance;

            var castFromSource = converter.ConvertFromSource(source);
            Assert.Throws<InvalidCastException>(() => converter.ConvertFromTarget(target));
        }

        private void TestCastClassConvertFromBothFail<TSource, TTarget>(TSource source, TTarget target)
        {
            var converter = Cache<ClassCastConverter<TSource, TTarget>>.Instance;

            Assert.Throws<InvalidCastException>(() => converter.ConvertFromSource(source));
            Assert.Throws<InvalidCastException>(() => converter.ConvertFromTarget(target));
        }

        private class Animal { }
        private class Dog : Animal { }
        private class Cat : Animal { }
        private class Tigri : Cat { }
    }
}
