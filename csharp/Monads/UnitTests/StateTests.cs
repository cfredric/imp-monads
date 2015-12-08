using System;
using Monads;
using System.Collections.Generic;
using NUnit.Framework;

namespace Monads.UnitTests
{
    [TestFixture]
    public class StateTests
    {
        // Set up some stateful computations to chain together.

        State<Stack<int>, int> pop =
            new State<Stack<int>, int> (s => {
                var x = s.Pop();
                return new Tuple<int, Stack<int>> (x, s);
            });

        Func<int, State<Stack<int>, int>> push =
            x => new State<Stack<int>, int> ((Stack<int> s) => {
                    s.Push (x);
                    return new Tuple<int, Stack<int>> (0, s);
                });

        State<Stack<int>, int> pushCount =
                new State<Stack<int>, int> (s => {
                    s.Push(s.Count);
                    return new Tuple<int, Stack<int>> (0, s);
                });

        Func<int, State<Stack<int>, int>> doubleResult =
            x => new State<Stack<int>, int> (s =>
                    new Tuple<int, Stack<int>> (2*x, s)
                );

        [Test]
        public void State_Stack_pops_pushes()
        {
            State<Stack<int>, int> manipulations = push (3)
                .Void (push(4))
                .Void (push(5))
                .Void (pop);

            Tuple<int, Stack<int>> result = manipulations.runState(new Stack<int> ());
            Assert.AreEqual (5, result.Item1);
            Assert.AreEqual (new Stack<int> (new [] { 3, 4, }), result.Item2);
        }

        [Test]
        public void State_Stack_gets()
        {
            State <Stack<int>, int> man =
                State<Stack<int>, int>.getState()
                .Void (pushCount)
                .Void (pushCount);
            Tuple<int, Stack<int>> result = man.runState(new Stack<int> ());
            Assert.AreEqual (new Stack<int> (new [] { 0, 1, }), result.Item2);
        }

        [Test]
        public void State_Stack_puts()
        {
            State <Stack<int>, Stack<int>> man = push(4)
                .Void (State<Stack<int>, Stack<int>>.putState(new Stack<int>(new [] {1, 2, 3, })));

            var result = man.runState(new Stack<int> ());
            Assert.AreEqual (new Stack<int> (new [] { 1, 2, 3, }), result.Item2);
        }

        [Test]
        public void State_Stack_Binds()
        {
            var man = push(4)
                .Void (pop)
                .Bind (doubleResult);

            var result = man.runState(new Stack<int> ());
            Assert.AreEqual (8, result.Item1);
            Assert.AreEqual (new Stack<int> (), result.Item2);
        }

        [Test]
        public void State_Stack_Bind_Scope()
        {
            State<Stack<int>, int> man = push (4)
                .Void (pop)
                .Bind<int> ((int x) =>
                    push(2*x)
                    .Void (push(3*x))
                );

            var result = man.runState (new Stack<int> ());
            Assert.AreEqual (new Stack<int> (new [] { 8, 12, }), result.Item2);
        }
    }
}
