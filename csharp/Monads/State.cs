using System;

namespace Monads
{
    public class State<S, A>
    {
        // This monad models a stateful computation, in a mutable environment
        // (which is passed along to chained computations).  Not super useful
        // in nonpure imperative languages, since everything by default is
        // already a stateful computation in a mutable environment, but it's
        // still interesting. Good to know it exists.
        private readonly Func<S, Tuple<A, S>> _runState;

        // This constructor acts as Haskell's "State" data constructor.
        public State (Func<S, Tuple<A, S>> f)
        {
            this._runState = f;
        }

        public static State<S, A> unit (A a)
        {
            // In Haskell:
            // return x = State $ \s -> (a, s)
            return new State<S, A> (s => new Tuple<A, S> (a, s));
        }

        public State<S, B> Bind<B> (Func<A, State<S, B>> f)
        {
            // In Haskell:
            // (State h) >>= f =
            //      State $ \s ->
            //          let (a, newState) = h s
            //              (State g) = f a
            //          in g newState
            return new State<S, B> (
                    s => {
                    Tuple<A, S> tuple = this.Computation.Invoke(s);
                    State<S, B> g = f.Invoke(tuple.Item1);
                    return g.Computation.Invoke(tuple.Item2);
                    }
                    );
        }

        public State<S, B> Void<B> (State<S, B> s)
        {
            // This is the >> operator in Haskell: same thing as Bind,
            // but it uses a function that ignores its argument.
            return this.Bind (x => s);
        }

        public Func<S, Tuple<A, S>> Computation { get { return this._runState; } }

        // A stateful computation that just returns the current state.
        public static State<S, S> getState()
        {
            // In Haskell:
            // get = \s -> (s, s)
            return new State<S, S> (s => new Tuple<S, S> (s, s));
        }

        // A stateful operation that replaces the current state with the given
        // state.
        public static State<S, S> putState(S newState)
        {
            // In Haskell, this is implemented as:
            // put newState = \s -> ((), newState)
            // However, in C# apparently one can't make something similar to
            // type Tuple <null, S>, so this also returns the state that it
            // just set.
            return new State<S, S> (s => new Tuple<S, S> (newState, newState));
        }

        public Tuple<A, S> runState(S initialState)
        {
            return this.Computation.Invoke (initialState);
        }
    }
}

