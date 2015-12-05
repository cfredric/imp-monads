using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monads
{
    public class List<A>
    {
        private readonly IEnumerable<A> _values;

        public List(IEnumerable<A> values)
        {
            this._values = values;
        }

        public List<B> FMap<B>(Func<A, B> f)
        {
            return new List<B>(_values.Select(f));
        }

        public List<B> Bind<B>(Func<A, List<B>> f)
        {
            return new List<B>(_values.Select(f).SelectMany(b => b.Values));
        }

        public IEnumerable<A> Values{ get { return _values; } } 
    }
}
