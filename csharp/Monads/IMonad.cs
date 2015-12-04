using System;

namespace Monads {

	public interface IMonad<M, A> : IFunctor<M, A> 
    {
		M Bind(Func<A, M> f);
	}
}