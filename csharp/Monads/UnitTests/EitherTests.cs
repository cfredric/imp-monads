using System;
using Monads;
using Monads.Either;
using NUnit.Framework;

namespace Monads.UnitTests
{
	[TestFixture]
	public class EitherTests
	{
		[Test]
		public void Either_Maps()
		{
			Func<int, int> f = (x) => x + 3;

			var r = new Either.Right<string, int> (4);
			var res = r.FMap (f);
			Assert.IsTrue (res is Either.Right<string, int>);
			Assert.AreEqual ((res as Either.Right<string, int>).r, 7);

			var l = new Either.Left<string, int> ("blah");
			res = l.FMap (f);
			Assert.IsTrue (res is Either.Left<string, int>);
		}

		[Test]
		public void Either_Binds()
		{
			Func<int, Either<string, int>> f = (x) => {
				if (x < 0) {
					return new Either.Left<string, int>("Cannot take sqrt of negative value");
				}else{
					return new Either.Right<string, int>((int) Math.Sqrt(x));
				}
			};

			var r = new Either.Right<string, int> (4);
			var res = r.Bind (f);
			Assert.IsTrue (res is Either.Right<string, int>);
			Assert.AreEqual ((res as Either.Right<string, int>).r, 2);

			var l = new Either.Left<string, int> ("blah");
			res = l.Bind (f);
			Assert.IsTrue (res is Either.Left<string, int>);
			Assert.AreEqual ((res as Either.Left<string, int>).l, "blah");

			var r2 = new Either.Right<string, int> (-1);
			res = r2.Bind (f);
			Assert.IsTrue (res is Either.Left<string, int>);
			Assert.AreSame ((res as Either.Left<string, int>).l, "Cannot take sqrt of negative value");
		}
	}
}
