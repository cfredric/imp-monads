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
				.Void (push(4));

			var result = manipulations.Computation.Invoke (new Stack<int> ());
		}
	}
}