using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnitTesting.Core.Entities;
using NUnitTesting.Core.Utility;

namespace NUnitTesting.Core.Providers {
	public class SystemProvider {

		public static IEnumerable<TestSystem> GetSystems() {
			return GetSystems(Constants.SystemFile);
		}

		public static IEnumerable<TestSystem> GetSystems(string filePath) {
			IEnumerable<TestSystem> systems = JsonSerializer.GetObject<List<TestSystem>>(filePath);
			if (systems == null)
				throw new NullReferenceException("NUnitTesting.Core.SystemProvider.GetSystems: Check the file path specified exists and that it's not malformed json.");
			return systems;
		}
	}
}
