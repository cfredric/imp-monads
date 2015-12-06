using System;
using Monads;
using NUnit.Framework;

namespace Monads.UnitTests
{
	[TestFixture]
	public class WriterTests
	{
		[Test]
		public void Writer_FMaps ()
		{
			var result = new Writer<int> (7)
				.FMap (x => x * 2)
				.FMap (x => x.ToString());
				

			Assert.IsInstanceOf<Writer<string>> (result);
			Assert.AreEqual ("14", result.Value);
			Assert.AreEqual ("", result.Stream);
		}

		[Test]
		public void Writer_Binds ()
		{
			var result = new Writer<int> (7)
				.Bind (x => new Writer<string> (x.ToString (), "Converted the doohickey\n"))
				.Bind (s => new Writer<string> (s + "0", "Amplified to full power\n"))
				.Bind (s => new Writer<int> (int.Parse (s), "Converted back to flubinator format\n"));

			Assert.IsInstanceOf<Writer<int>> (result);
			Assert.AreEqual (70, result.Value);
			Assert.AreEqual ("Converted the doohickey\nAmplified to full power\nConverted back to flubinator format\n", result.Stream);
		}
	}
}

