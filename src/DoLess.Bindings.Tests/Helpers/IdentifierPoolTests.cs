using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace DoLess.Bindings.Tests.Helpers
{
    [TestFixture]
    public class IdentifierPoolTests
    {
        private IdentifierPool identifierPool;

        [SetUp]
        public void Setup()
        {
            this.identifierPool = new IdentifierPool();
        }

        [Test]
        public void FirstIdShouldBeDefault()
        {
            var id1 = this.identifierPool.Next();
            var id2 = this.identifierPool.Next();

            id1.Should().Be(1);
            id2.Should().Be(2);
        }

        [Test]
        public void IdShouldBeRecycled()
        {
            var count = 100;
            for (int i = 0; i < count; i++)
            {
                this.identifierPool.Next();
            }

            int recycle = 50;
            this.identifierPool.Recycle(recycle);

            var id1 = this.identifierPool.Next();
            var id2 = this.identifierPool.Next();

            id1.Should().Be(recycle);
            id2.Should().Be(count + 1);
        }
    }
}
