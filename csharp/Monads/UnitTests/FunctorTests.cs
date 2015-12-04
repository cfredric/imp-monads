using System;
using Monads;
using NUnit.Framework;

namespace Monads.UnitTests
{
    [TestFixture]
    public class FunctorTests
    {
        [Test]
        public void Functor_Maps()
        {
            var functor = new Functor<string, int>();
            Func<int, int> func = delegate(int a) { return a + 2 ; };
            Assert.Throws<NotImplementedException>(() => functor.FMap<int>(func));
        }

        private class Functor<F,A> : IFunctor<F,A>
        {
            public F FMap<B>(Func<A, B> f)
            {
                throw new NotImplementedException();
            }
        }
    }
}
