using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnitTesting.Core.Entities;
using NUnitTesting.Core.Utility;

namespace NUnitTesting.Core.Providers {
	public class SiteProvider {

		public static IEnumerable<TestSite> GetSites() {
			return GetSites(Constants.SiteFile);
		}

		public static IEnumerable<TestSite> GetSites(string filePath) {
			IEnumerable<TestSite> sites = JsonSerializer.GetObject<List<TestSite>>(filePath);
			if (sites == null)
				throw new NullReferenceException("NUnitTesting.Core.SystemProvider.GetSites: Check the file path specified exists and that it's not malformed json.");
			return sites;
		}

		public static IEnumerable<TestSite> GetEnabledSites() {
			return GetSites().Where(a => !a.Disabled);
		}

		public static IEnumerable<TestSite> GetEnabledSites(string filePath) {
			return GetSites(filePath).Where(a => !a.Disabled);
		}
	}
}
