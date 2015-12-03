using System;
namespace Monads {
	public interface Monad<M, A> : Functor<M, A> {
		M bind(Func<A, M> f);
	}
}