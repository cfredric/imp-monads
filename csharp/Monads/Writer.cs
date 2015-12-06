using System;

namespace Monads
{
	public class Writer<A>
	{
		private readonly string _stream;
		private readonly A _value;

		public Writer (A a)
		{
			this._value = a;
			this._stream = "";
		}

		public Writer (A a, string stream)
		{
			this._value = a;
			this._stream = stream;
		}

		public Writer<B> FMap<B> (Func<A, B> f)
		{
			return new Writer<B> (f.Invoke (this.Value));
		}

		public Writer<B> Bind<B> (Func<A, Writer<B>> f)
		{
			Writer<B> w = f.Invoke (this.Value);
			return new Writer<B> (w.Value, this.Stream + w.Stream);
		}

		public A Value{ get { return this._value; } }
		public string Stream{ get { return this._stream; } }
	}
}

