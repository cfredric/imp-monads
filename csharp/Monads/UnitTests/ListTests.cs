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

		[Test]
		public void List_Nondeterminism1()
		{
			var values = new[] { 1, 2, 3, 4, };
			Func<int, List<int>> unAbs = x => new List<int>(new[] { x, -x });
			var list = new List<int> (values);
			var results = list.Bind<int> (unAbs);
			var resultsValues = results.Values.ToArray();

			foreach(var x in values)
			{
				Assert.Contains (x, resultsValues);
				Assert.Contains (-x, resultsValues);
			}
		}

		[Test]
		public void List_Nondeterminism2()
		{
			var values = new[] { 1, 2, };

			var list = new List<int> (values);
			var results = list
				.Bind<int> (x => new List<int>(new [] { x, -x, }))
				.Bind<int> (x => new List<int>(new [] { x, x + 7, }));
			var resultsValues = results.Values.ToArray();

			foreach(var x in new [] { 1, 2, 8, 9, -1, -2, 6, 5, }){
				Assert.Contains (x, resultsValues);
			}
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
