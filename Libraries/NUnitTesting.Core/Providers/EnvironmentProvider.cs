using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnitTesting.Core.Entities;
using NUnitTesting.Core.Utility;

namespace NUnitTesting.Core.Providers {
	public class EnvironmentProvider {

		public static IEnumerable<TestEnvironment> GetEnvironments() {
			return GetEnvironments(Constants.EnvironmentFile);
		}

		public static IEnumerable<TestEnvironment> GetEnvironments(string filePath) {
			IEnumerable<TestEnvironment> environments = JsonSerializer.GetObject<List<TestEnvironment>>(filePath);
			if (environments == null)
				throw new NullReferenceException(
					string.Format("NUnitTesting.Core.EnvironmentProvider.GetEnvironments: Check that the file path [{0}] exists and that it's not malformed json.",
					filePath
					)
				);
			return environments;
		}
	}
}
