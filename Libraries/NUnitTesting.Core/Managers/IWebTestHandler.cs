using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Core;
using NUnitTesting.Core.Entities;

namespace NUnitTesting.Core.Managers {
	public interface IWebTestHandler {

		void OnError(TestMethod tm, TestEnvironment te, TestSite ts, TestResult tr);

		void OnFailure(TestMethod tm, TestEnvironment te, TestSite ts, TestResult tr);

		void OnSuccess(TestMethod tm, TestEnvironment te, TestSite ts, TestResult tr);

		void OnSkipped(TestMethod tm, TestEnvironment te, TestSite ts);
	}
}
