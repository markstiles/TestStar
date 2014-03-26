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
	public class PingTest : BaseWebTest {

		public PingTest() { }

		[Test]
		public override void RunTest() {
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(RequestURL);
			try { // catches the 400 and 500 errors by exception
				HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
				ResponseStatus = resp.StatusCode;
				resp.Close();
				Assert.AreEqual(HttpStatusCode.OK, ResponseStatus);
			} catch (WebException wex) {
				HttpWebResponse resp = (HttpWebResponse)wex.Response;
				ResponseStatus = (resp != null) ? resp.StatusCode : HttpStatusCode.BadRequest;
				Assert.Fail(wex.Message);
			}
		}
	}
}
