using System;

namespace Monads
{
	namespace Either
	{
		public abstract class Either<L, R>
		{
			public abstract Either<L, X> FMap<X>(Func<R, X> f);
			public abstract Either<L, X> Bind<X>(Func<R, Either<L, X>> f);
		}

		public class Right<L, R> : Either<L, R>
		{
			public readonly R r;
			public Right(R r)
			{
				this.r = r;
			}
			public override Either<L, X> FMap<X>(Func<R, X> f){
				return new Right<L, X>(f.Invoke (this.r));
			}
			public override Either<L, X> Bind<X>(Func<R, Either<L, X>> f)
			{
				return f.Invoke (this.r);
			}
		}

		public class Left<L, R> : Either<L, R>
		{
			public readonly L l;
			public Left(L l)
			{
				this.l = l;
			}
			public override Either<L, X> FMap<X>(Func<R, X> f)
			{
				return new Left<L, X> (this.l);
			}
			public override Either<L, X> Bind<X>(Func<R, Either<L, X>> f)
			{
				return new Left<L, X> (this.l);
			}
		}
	}
}

