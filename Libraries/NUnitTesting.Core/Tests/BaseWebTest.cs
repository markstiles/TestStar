using System.Net;
using NUnit.Core;
using NUnitTesting.Core.Entities;
using NUnitTesting.Core.Extensions;

namespace NUnitTesting.Core.Tests {
	public class BaseWebTest {

		public static readonly string BaseRequestURLKey = "BaseRequestURL";
		public static readonly string ResponseStatusCodeKey = "ResponseStatusCode";
		public static readonly string EnvironmentKey = "ContextEnvironment";
		public static readonly string SiteKey = "ContextSite";

		public virtual string TestName {
			get {
				return "Base Web Test";
			}
		}
		
		protected TestMethod CurrentTestMethod;

		protected Test ContextTest {
			get {
				return (CurrentTestMethod == null) ? TestExecutionContext.CurrentContext.CurrentTest : CurrentTestMethod;
			}
		}

		protected TestEnvironment ContextEnvironment {
			get {
				return ContextTest.GetProperty<TestEnvironment>(EnvironmentKey);
			}
		}

		protected TestSite ContextSite {
			get {
				return ContextTest.GetProperty<TestSite>(SiteKey);
			}
		}

		protected HttpStatusCode ResponseStatus {
			get {
				return ContextTest.GetProperty<HttpStatusCode>(ResponseStatusCodeKey);
			}
		}

		protected string BaseRequestURL {
			get {
				return ContextTest.GetProperty<string>(BaseRequestURLKey);
			}
		}
		
		public BaseWebTest() { }

		public BaseWebTest(TestMethod tm) {
			CurrentTestMethod = tm;
		}	
	}
}
