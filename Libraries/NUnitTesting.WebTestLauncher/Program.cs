using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using NUnit.Core;
using NUnit.Util;
using NUnitTesting.Core.Entities;
using NUnitTesting.Core.Extensions;
using NUnitTesting.Core.Managers;
using NUnitTesting.Core.Providers;
using NUnitTesting.Core.Utility;
using NUnitTesting.Core.Tests;

namespace NUnitTesting.WebTestLauncher {
	public class Program {
		
		public static List<string> GetStrings(string param) {
			return param.Split(new string[] {","},StringSplitOptions.RemoveEmptyEntries).ToList();
		}

		static void Main(string[] args) {

			//params 0 = test assembly
			string testAssembly = string.Empty;
			if (args.Length > 0) {
				testAssembly = args[0];
			} else {
				Console.WriteLine("You need to specify an assembly.");
				return;
			}

			//params 1 = test name
			string testName = string.Empty;
			if (args.Length > 1) {
				testName = args[1];
			} else {
				Console.WriteLine("You need to specify a test.");
				return;
			}
			
			//params 2 = environments
			Dictionary<int, TestEnvironment> Environments = new Dictionary<int, TestEnvironment>();
			if (args.Length > 2) {
				IEnumerable<TestEnvironment> prEnv = EnvironmentProvider.GetEnvironments();
				foreach (string s in GetStrings(args[2])) {
					foreach(TestEnvironment fenv in prEnv.Where(a => a.ID.Equals(int.Parse(s)))) {
						if (!Environments.ContainsKey(fenv.ID)) {
							Console.WriteLine(string.Format("Adding '{0}' Environment.", fenv.Name));
							Environments.Add(fenv.ID, fenv);
						}
					}
				}
			}

			// params 3 = systems
			// params 4 = sites 
			// will look for sites by system unless systems is an empty string then it looks for them by site
			Dictionary<int, TestSite> Sites = new Dictionary<int, TestSite>();
			IEnumerable<TestSite> prSites = SiteProvider.GetSites();
			if (args.Length > 3 && !string.IsNullOrEmpty(args[3])) {
				foreach (string s in GetStrings(args[3])) {
					foreach (TestSite fsite in prSites.Where(a => a.SystemID.Equals(int.Parse(s)))) {
						if (!Sites.ContainsKey(fsite.ID)) {
							Console.WriteLine(string.Format("Adding '{0}' Site.", fsite.Name));
							Sites.Add(fsite.ID, fsite);
						}
					}
				}
			} 
			if (args.Length > 4) {
				foreach (string s in GetStrings(args[4])) {
					foreach (TestSite fsite in prSites.Where(a => a.ID.Equals(int.Parse(s)))) {
						if (!Sites.ContainsKey(fsite.ID)) {
							Console.WriteLine(string.Format("Adding '{0}' Site.", fsite.Name));
							Sites.Add(fsite.ID, fsite);
						}
					}
				}
			}

			//setup for testing
			CoreExtensions.Host.InitializeService();
			//get the test suite
			TestSuite suite = TestUtility.GetTestSuite(testAssembly);

			IEnumerable<TestFixture> Fixtures = suite.GetFixtures().Where(a => a.ClassName.EndsWith(string.Format(".{0}", testName)));
			if (!Fixtures.Any()) {
				Console.WriteLine("There were no Test Fixtures found. Make sure the class has the [TestFixture] attribute.");
				return;
			}
			TestFixture tf = Fixtures.First();
			WebTestManager manager = new WebTestManager(new WebConsoleTestHandler());
			manager.RunTest(tf, Environments.Values, Sites.Values);
		}
	}
}
