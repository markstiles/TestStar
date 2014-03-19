using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using NUnitTesting.Core.Providers;
using NUnitTesting.Core.Utility;

namespace NUnitTesting.Core.Entities {
	
	public class TestSystem {
		
		public int ID = -1; 
		public string Name;
		[ScriptIgnore]
		public virtual IEnumerable<TestSite> Sites {
			get {
				return SiteProvider.GetSites().Where(s => s.SystemID.Equals(this.ID)); 
			}
		}
	}
}
