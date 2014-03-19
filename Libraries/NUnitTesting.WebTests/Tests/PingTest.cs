using System.Net;
using System.Text;
using System.Linq;
using NUnit.Core;
using NUnit.Framework;
using NUnit.Util;
using NUnitTesting.Core.Entities;
using NUnitTesting.Core.Extensions;
using NUnitTesting.Core.Utility;
using NUnitTesting.Core.Tests;
using System.Collections.Generic;
using System.Threading;
using System;
using NUnitTesting.Core.Providers;

namespace NUnitTesting.WebTests.Tests {
	[TestFixture, RequiresSTA]
	public class PingTest : BaseWebTest, IHandledTest {

		public override string TestName {
			get {
				return "Ping Test";
			}
		}

		public PingTest() { }
		public PingTest(TestMethod tm) : base(tm) { }
		
		public void OnSuccess(TestResult tr, ResultSummarizer rs) {}

		public void OnError(TestResult tr, ResultSummarizer rs) {}

		public void OnFailure(TestResult tr, ResultSummarizer rs) {}

		[Test]
		public void RunTest() {
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(BaseRequestURL);
			try { // catches the 400 and 500 errors by exception
				HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
				HttpStatusCode sc = resp.StatusCode;
				ContextTest.SetProperty(ResponseStatusCodeKey, resp.StatusCode);
				resp.Close();
				Assert.AreEqual(HttpStatusCode.OK, sc);
			} catch (WebException wex) {
				HttpWebResponse resp = (HttpWebResponse)wex.Response;
				ContextTest.SetProperty(ResponseStatusCodeKey, (resp != null) ? resp.StatusCode : HttpStatusCode.BadRequest);
				Assert.Fail(wex.Message);
			}
		}
	}
}
