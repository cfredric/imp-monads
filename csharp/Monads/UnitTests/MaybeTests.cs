using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Monads.UnitTests
{
    [TestFixture]
    public class MaybeTests
    {
        [Test]
        public void Nothing_FMap_ReturnsNothing()
        {
            var nothing = new Nothing<int>();
            var result = nothing.FMap<string>(a => a.ToString());
            Assert.IsInstanceOf<Nothing<string>>(result);
        }

        [Test]
        public void Nothing_Bind_ReturnsNothing()
        {
            var nothing = new Nothing<string>();
            var result = nothing.Bind<string>(a => new Just<string>(a));
            Assert.IsInstanceOf<Nothing<string>>(result);
        }

        [Test]
        public void Just_FMap_ReturnsJust()
        {
            var just = new Just<string>("42");
            var result = just.FMap<int>(a => Int32.Parse(a));
            Assert.IsInstanceOf<Just<int>>(result);
            var actualResult = result as Just<int>;
            Assert.AreEqual(42, actualResult.Value);
        }

        [Test]
        public void Just_FMap_ReturnsNothing()
        {
            var just = new Just<string>("Hello");
            var result = just.FMap<int>(a => Int32.Parse(a));
            Assert.IsInstanceOf<Nothing<int>>(result);
        }

        [Test]
        public void Just_Bind_ReturnsJust()
        {
            var just = new Just<string>("4");
            var just2 = just.Bind<string>(a => new Just<string>(a + "2"));
            var result = just2.FMap<int>(a => Int32.Parse(a));
            Assert.IsInstanceOf<Just<int>>(result);
            var actualResult = result as Just<int>;
            Assert.AreEqual(42, actualResult.Value);
        }

        [Test]
        public void Just_Bind_ReturnsNothing()
        {
            var just = new Just<string>("4");
            var just2 = just.Bind<string>(a => new Nothing<string>());
            var result = just2.FMap<int>(a => Int32.Parse(a));
            Assert.IsInstanceOf<Nothing<int>>(result);
        }
    }
}
