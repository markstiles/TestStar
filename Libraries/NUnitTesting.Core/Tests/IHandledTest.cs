using NUnit.Core;
using NUnit.Util;
using NUnitTesting.Core.Entities;

namespace NUnitTesting.Core.Tests {
	public interface IHandledTest {
		void OnSuccess(TestResult tr, ResultSummarizer rs);
		void OnError(TestResult tr, ResultSummarizer rs);
		void OnFailure(TestResult tr, ResultSummarizer rs);
		void RunTest();
	}
}
