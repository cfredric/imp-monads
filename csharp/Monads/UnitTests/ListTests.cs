using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Monads;
using NUnit.Framework;

namespace Monads.UnitTests
{
    [TestFixture]
    public class ListTests
    {
        [Test]
        public void List_Maps()
        {
            var values = new[] {0, 1, 2, 3, 4};
            var list = new List<int>(values);
            var squares = list.FMap(a => a*a).Values.ToArray();
            for(var i = 0; i < values.Length; i++)
            {
                var value = values[i]*values[i];
                Assert.AreEqual(value, squares[i]);
            }
        }

        [Test]
        public void List_Binds()
        {
            var values = new[] { 1, 2, 3, 4, };
            var list = new List<int>(values);
            var squares = list.Bind<int>(a => new List<int>(Helper(a))).Values.ToList();
            Assert.AreEqual(values.Sum(), squares.Count);

        }

        private IEnumerable<int> Helper(int a)
        {
            while (a > 0)
            {
                yield return 1;
                a--;
            }
        } 
    }
}
