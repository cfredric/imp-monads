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

		Func<int,
			State<Stack<int>, int>> push = x =>
			new State<Stack<int>, int> ((Stack<int> s) => {
				s.Push (x);
				return new Tuple<int, Stack<int>> (0, s);
			});

		[Test]
		public void State_Stack()
		{
			State<Stack<int>, int> manipulations = push (3)
				.Void (push(4))
				.Void (push(5))
				.Void (pop);

			Tuple<int, Stack<int>> result = State<Stack<int>, int>.runState(manipulations, new Stack<int> ());
			Assert.AreEqual (5, result.Item1);
			Assert.Contains (3, result.Item2);
			Assert.Contains (4, result.Item2);
		}
	}
}