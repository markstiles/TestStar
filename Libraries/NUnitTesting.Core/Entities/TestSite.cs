using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using NUnitTesting.Core.Utility;

namespace NUnitTesting.Core.Entities {
	public class TestSite {
		#region Properties

		public int ID = -1;
		public string Name = string.Empty;
		public string Domain = string.Empty;
		public int SystemID = -1;

		public Dictionary<string, object> Properties;
		public IEnumerable<TestEnvironment> Environments;
		
		#endregion Properties

		/// <summary>
		/// Concatenates the Domain Prefix and the Domain
		/// </summary>
		/// <param name="env"></param>
		/// <returns></returns>
		public virtual string BaseURL(TestEnvironment env) {
			IEnumerable<TestEnvironment> envs = Environments.Where(e => e.Name.Equals(env.Name));
			if (envs == null || !envs.Any(e => e.Name.Equals(env.Name)))
				return string.Empty;
			TestEnvironment te = envs.First();
			return string.Format("{0}{1}", ((string.IsNullOrEmpty(te.DomainPrefix)) ? env.DomainPrefix : te.DomainPrefix), Domain);
		}

		public T ConvertTo<T>() {
			return JsonSerializer.ConvertTo<T>(this);
		}
	}
}
