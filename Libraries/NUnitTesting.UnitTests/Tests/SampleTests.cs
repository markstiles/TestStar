using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NUnitTesting.UnitTests.Tests {
	[TestFixture, Category("Sample Tests")]
	public class SampleTests {

		[SetUp]
		public void SetUp() { }

		[Test]
		public void Sample_TrueTest() {
			Assert.IsTrue(true);
		}
	}
}
