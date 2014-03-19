using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Core;
using NUnitTesting.Core.Managers;
using NUnitTesting.Core.Utility;
using NUnitTesting.Core.Extensions;

namespace NUnitTesting.UnitTestLauncher {
	public class Program {

		public static List<string> GetStrings(string param) {
			return param.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
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

			//params 1 = test category
			List<string> categories = new List<string>();
			if (args.Length > 1)
				foreach (string s in GetStrings(args[2])) 
					if(!categories.Contains(s))
						categories.Add(s);
	
			//params 2 = test method
			List<string> names = new List<string>();
			if (args.Length > 2)
				foreach (string n in GetStrings(args[2]))
					if (!names.Contains(n))
						names.Add(n);

			//setup for testing
			CoreExtensions.Host.InitializeService();
			//get the test suite
			TestSuite suite = TestUtility.GetTestSuite(testAssembly);

			Dictionary<string, TestMethod> Methods = new Dictionary<string, TestMethod>();
			IEnumerable<TestMethod> allMethods = suite.GetMethods();
			if (!categories.Any() && !names.Any()) { // if nothing selected add all
				Methods = allMethods.ToDictionary(a => a.MethodName);
			} else { // add one at a time
				foreach (string c in categories) 
					foreach (TestMethod ctm in allMethods.Where(a => a.Categories().Any(b => b.Equals(c))))
						Methods.Add(ctm.MethodName, ctm);

				foreach (string n in names)
					foreach (TestMethod ctm in allMethods.Where(a => a.MethodName.Equals(n)))
						if (!Methods.ContainsKey(ctm.MethodName))
							Methods.Add(ctm.MethodName, ctm);
			}

			if (!Methods.Any())
				throw new NullReferenceException("There are no Test Methods found. Make sure the class method has the [Test] attribute.");
			UnitTestManager manager = new UnitTestManager(new UnitConsoleTestHandler());
			foreach(TestMethod tm in Methods.Values)
				manager.RunTest(tm);
		}
	}
}
