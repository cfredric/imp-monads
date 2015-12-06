using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monads
{
    public interface Maybe<A> 
    {
        Maybe<B> FMap<B>(Func<A, B> f);
        Maybe<B> Bind<B>(Func<A, Maybe<B>> f);
    }

    public class Nothing<A> : Maybe<A>
    {
        public Nothing() { }

        public Maybe<B> FMap<B>(Func<A, B> f)
        {
            return new Nothing<B>();
        }

        public Maybe<B> Bind<B>(Func<A, Maybe<B>> f)
        {
            return new Nothing<B>();
        }
    }

    public class Just<A> : Maybe<A>
    {
        private A a;

        public Just(A a)
        {
            this.a = a;
        }

        public Maybe<B> FMap<B>(Func<A, B> f)
        {
            try
            {
                var result = f.Invoke(a);
                return new Just<B>(result);
            }
            catch (Exception)
            {
                return new Nothing<B>();
            }
        }

        public Maybe<B> Bind<B>(Func<A, Maybe<B>> f)
        {
			try 
			{
				var result = f.Invoke(a);
				return result;
			}
			catch (Exception)
			{
				return new Nothing<B> ();
			}
        }

        public A Value { get { return a; } }
    }
}
