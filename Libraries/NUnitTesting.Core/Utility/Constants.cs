using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NUnitTesting.Core.Utility {
	public static class Constants {

		#region Contextual Awareness

		/// <summary>
		/// this section switches contexts based on the web app vs the console app
		/// </summary>
		public static bool IsWebApp {
			get {
				return (HttpContext.Current != null && HttpContext.Current.Request != null);
			}
		}

		public static string ApplicationRoot {
			get {
				return (IsWebApp)
					? HttpContext.Current.Request.PhysicalApplicationPath
					: AppDomain.CurrentDomain.BaseDirectory.Split(new string[] {"bin"}, StringSplitOptions.RemoveEmptyEntries)[0];
			}
		}

		public static string ExecutionRoot {
			get {
				return (IsWebApp)
					? string.Format("{0}bin", HttpContext.Current.Request.PhysicalApplicationPath)
					: AppDomain.CurrentDomain.BaseDirectory;
			}
		}

		public static KeyValueConfigurationCollection AppSettings {
			get {
				ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
				
				string webConfigPath = string.Format("{0}{1}", Constants.ApplicationRoot, "web.config");
				string appConfigPath = string.Format("{0}{1}", Constants.ApplicationRoot, "app.config");
				if (File.Exists(webConfigPath))
					configFileMap.ExeConfigFilename = webConfigPath;
				else if (File.Exists(appConfigPath))
					configFileMap.ExeConfigFilename = appConfigPath;
				else
					throw new FileNotFoundException("Could not file either Web.Config or App.Config file");

				Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
				return config.AppSettings.Settings;
			}
		}

		#endregion 

		public static string EnvironmentFile {
			get {
				return TestUtility.GetDataPath(AppSettings["EnvironmentsDataFile"].Value);
			}
		}

		public static string SiteFile {
			get {
				return TestUtility.GetDataPath(AppSettings["SitesDataFile"].Value);
			}
		}
		
		public static string SystemFile {
			get {
				return TestUtility.GetDataPath(AppSettings["SystemsDataFile"].Value);
			}
		}

		public static string DefaultWebTestAssembly {
			get {
				return AppSettings["DefaultWebTestAssembly"].Value;
			}
		}

		public static string DefaultUnitTestAssembly {
			get {
				return AppSettings["DefaultUnitTestAssembly"].Value;
			}
		}

		public static string DefaultTestLauncher {
			get {
				return AppSettings["DefaultTestLauncher"].Value;
			}
		}
	}
}
