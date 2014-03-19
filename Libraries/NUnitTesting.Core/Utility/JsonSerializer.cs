using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace NUnitTesting.Core.Utility {
	public static class JsonSerializer {

		public static T GetObject<T>(string file) {
			string filePath = string.Format("{0}{1}", Constants.ApplicationRoot, file);
			if (File.Exists(filePath)) {
				StreamReader dataFile = new StreamReader(filePath);
				string data = dataFile.ReadToEnd();
				dataFile.Close();
				T results = new JavaScriptSerializer().Deserialize<T>(data);
				return results;
			} else {
				throw new FileNotFoundException(string.Format("NUnitTesting.Core.Utility.JsonSerializer.GetObject: The file path [{0}] doesn't exist.", filePath));
			}
		}

		/// <summary>
		/// this is used to convert one type of entity into a sub/super class
		/// </summary>
		public static T ConvertTo<T>(object o) {
			string data = new JavaScriptSerializer().Serialize(o);
			T results = new JavaScriptSerializer().Deserialize<T>(data);
			return results;
		}

		public static void SetObject<T>(string file, T contentObj) {
			string filePath = string.Format(@"{0}{1}", Constants.ApplicationRoot, file);
			string data = new JavaScriptSerializer().Serialize(contentObj);
			using (StreamWriter newData = new StreamWriter(filePath, false)) {
				newData.WriteLine(data);
			}
		}
	}
}
