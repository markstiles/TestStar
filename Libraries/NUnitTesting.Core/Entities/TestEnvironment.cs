using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTesting.Core.Entities {
	public class TestEnvironment {

		public int ID = -1;
		public string Name = string.Empty;
		public string DomainPrefix = string.Empty;
		public string IPAddress = string.Empty;

		#region Constructors

		public TestEnvironment() { }

		#endregion Constructors

		public override bool Equals(object obj) {
			return (this.Name.Equals(((TestEnvironment)obj).Name)) ? true : false;
		}
	}
}
