using System;

namespace Monads {

	public interface IFunctor<F, A> 
    {
		F FMap<B>(Func<A, B> f);
	}
}