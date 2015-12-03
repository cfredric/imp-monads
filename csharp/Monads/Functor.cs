using System;
namespace Monads {
	public interface Functor<F, A> {
		F fmap<B>(Func<A, B> f);
	}
}