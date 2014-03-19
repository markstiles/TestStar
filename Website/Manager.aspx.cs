using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using NUnitTesting.Core.Entities;
using NUnitTesting.Core.Providers;
using NUnitTesting.Core.Utility;

namespace NUnitTesting.WebApp {
	public partial class Manager : System.Web.UI.Page {
		
		protected Dictionary<int, TestEnvironment> Environments;
		protected Dictionary<int, TestSystem> Systems;
		protected Dictionary<int, TestSite> Sites;

		protected void Page_Load(object sender, EventArgs e) {
			Environments = EnvironmentProvider.GetEnvironments().OrderBy(a => a.Name).ToDictionary(a => a.ID);
			Systems = SystemProvider.GetSystems().OrderBy(a => a.Name).ToDictionary(a => a.ID);
			Sites = SiteProvider.GetSites().OrderBy(a => a.SystemID).ThenBy(a=>a.Name).ToDictionary(a => a.ID);

			ltlError.Text = string.Empty;

			if (!IsPostBack)
				Init();
		}

		protected void Init() {
			ddlEnvironments.Items.Clear();
			ddlPopEnvs.Items.Clear();
			foreach (KeyValuePair<int, TestEnvironment> kenv in Environments) {
				string envName = string.Format("{0}-{1}", kenv.Value.ID, kenv.Value.Name);
				ddlEnvironments.Items.Add(new ListItem(envName, kenv.Key.ToString()));
				ddlPopEnvs.Items.Add(new ListItem(envName, kenv.Key.ToString()));
			}
			ddlSystems.Items.Clear();
			ddlSiteSystems.Items.Clear();
			foreach (KeyValuePair<int, TestSystem> ksys in Systems) {
				string sysName = string.Format("{0}-{1}", ksys.Value.ID, ksys.Value.Name);
				ddlSystems.Items.Add(new ListItem(sysName, ksys.Key.ToString()));
				ddlSiteSystems.Items.Add(new ListItem(sysName, ksys.Key.ToString()));
			}
			ddlSites.Items.Clear();
			foreach (KeyValuePair<int, TestSite> ksite in Sites)
				ddlSites.Items.Add(new ListItem(string.Format("{0}-{1}-{2}", ksite.Value.ID, ksite.Value.Name, Systems[ksite.Value.SystemID].Name), ksite.Key.ToString()));
		}

		#region environment

		protected void btnShowAddEnv_Click(object sender, EventArgs e) {

			h2Env.InnerText = "Add Environment";
			hdnEnvID.Value = string.Empty;
			txtEnvName.Text = string.Empty;
			txtEnvDomain.Text = string.Empty;
			txtEnvIP.Text = string.Empty;
			
			HideAll();
			pnlEnv.Visible = true;
			btnAddEnv.Visible = true;
		}
		
		protected void btnShowEditEnv_Click(object sender, EventArgs e) {

			h2Env.InnerText = "Edit Environment";
			TestEnvironment env = GetEnvironment(ddlEnvironments.SelectedValue);
			hdnEnvID.Value = ddlEnvironments.SelectedValue;
			txtEnvName.Text = env.Name;
			txtEnvDomain.Text = env.DomainPrefix;
			txtEnvIP.Text = env.IPAddress;
			
			HideAll();
			pnlEnv.Visible = true;
			btnEditEnv.Visible = true;
		}

		protected void btnAddEnv_Click(object sender, EventArgs e) {

			if (Environments.Any(a => a.Value.Name.Equals(txtEnvName.Text))) {
				ltlError.Text = "An environment with this name already exists.";
			} else {
				TestEnvironment env = new TestEnvironment();
				env.Name = txtEnvName.Text;
				env.DomainPrefix = txtEnvDomain.Text;
				env.IPAddress = txtEnvIP.Text;
				env.ID = Environments.Count;
				Environments.Add(env.ID, env);

				UpdateEnv();
				ResetForm();
			}
		}

		protected void btnEditEnv_Click(object sender, EventArgs e) {

			TestEnvironment env = GetEnvironment(hdnEnvID.Value);
			env.Name = txtEnvName.Text;
			env.DomainPrefix = txtEnvDomain.Text;
			env.IPAddress = txtEnvIP.Text;
			
			UpdateEnv();
			ResetForm();
		}

		protected void btnRemEnv_Click(object sender, EventArgs e) {
			TestEnvironment te = GetEnvironment(ddlEnvironments.SelectedValue);
			foreach (TestSite s in Sites.Values.Where(a => a.Environments.Contains(te)))
				s.Environments = s.Environments.Where(a => a.ID != te.ID);
			Environments.Remove(te.ID);
			UpdateSites();
			ResetForm();
		}

		protected TestEnvironment GetEnvironment(string envID) {
			return Environments[int.Parse(envID)];
		}

		protected void UpdateEnv() {
			JsonSerializer.SetObject<List<TestEnvironment>>(Constants.EnvironmentFile, Environments.Values.ToList());
		}

		#endregion environment

		#region system

		protected void btnShowAddSys_Click(object sender, EventArgs e) {

			h2Sys.InnerText = "Add System";
			hdnSysID.Value = string.Empty;
			txtSysName.Text = string.Empty;
			
			HideAll();
			pnlSys.Visible = true;
			btnAddSys.Visible = true;
		}

		protected void btnShowEditSys_Click(object sender, EventArgs e) {

			h2Sys.InnerText = "Edit System";
			TestSystem sys = GetSystem(ddlSystems.SelectedValue);
			hdnSysID.Value = ddlSystems.SelectedValue;
			txtSysName.Text = sys.Name;
			
			HideAll();
			pnlSys.Visible = true;
			btnEditSys.Visible = true;
		}

		protected void btnAddSys_Click(object sender, EventArgs e) {
			if (Systems.Any(a => a.Value.Name.Equals(txtSysName.Text))) {
				ltlError.Text = "A system with this name already exists.";
			} else {
				TestSystem sys = new TestSystem();
				sys.Name = txtSysName.Text;
				sys.ID = Systems.Count;
				Systems.Add(sys.ID, sys);
				UpdateSys();
				ResetForm();
			}
		}

		protected void btnEditSys_Click(object sender, EventArgs e) {

			TestSystem sys = GetSystem(hdnSysID.Value);
			sys.Name = txtSysName.Text;
						
			UpdateSys();
			ResetForm();
		}

		protected void btnRemSys_Click(object sender, EventArgs e) {
			TestSystem ts = GetSystem(ddlSystems.SelectedValue);
			if (Sites.Values.Any(a => a.SystemID.Equals(ts.ID))) {
				ltlError.Text = string.Format("Could not remove the '{0}' system since it is still used by some sites.", ts.Name);
				return;
			}
			Systems.Remove(ts.ID);
			UpdateSys();
			ResetForm();
		}

		protected TestSystem GetSystem(string sysID) {
			return Systems[int.Parse(sysID)];
		}

		protected void UpdateSys() {
			JsonSerializer.SetObject<List<TestSystem>>(Constants.SystemFile, Systems.Values.ToList());
		}

		#endregion system

		#region site

		protected void btnShowAddSite_Click(object sender, EventArgs e) {

			h2Site.InnerText = "Add Site";
			hdnSiteID.Value = string.Empty;
			txtSiteName.Text = string.Empty;
			txtSiteDomain.Text = string.Empty;
			ddlSiteSystems.SelectedValue = "0";
			hdnSiteProperties.Value = string.Empty;
			hdnSiteEnvs.Value = string.Empty;

			HideAll();
			pnlSite.Visible = true;
			btnAddSite.Visible = true;
		}

		protected void btnShowEditSite_Click(object sender, EventArgs e) {

			h2Site.InnerText = "Edit Site";
			TestSite site = GetSite(ddlSites.SelectedValue);
			hdnSiteID.Value = ddlSites.SelectedValue;
			txtSiteName.Text = site.Name;
			txtSiteDomain.Text = site.Domain;
			ddlSiteSystems.SelectedValue = site.SystemID.ToString();
			hdnSiteProperties.Value = new JavaScriptSerializer().Serialize(site.Properties);
			hdnSiteEnvs.Value = new JavaScriptSerializer().Serialize(site.Environments);
			
			HideAll();
			pnlSite.Visible = true;
			btnEditSite.Visible = true;
		}

		protected void btnAddSite_Click(object sender, EventArgs e) {
			if (Sites.Any(a => a.Value.SystemID.Equals(ddlSiteSystems.SelectedValue) && a.Value.Name.Equals(txtSiteName.Text))) {
				ltlError.Text = "A site with this name and system already exists.";
			} else {
				TestSite site = new TestSite();
				site.Name = txtSiteName.Text;
				txtSiteDomain.Text = site.Domain;
				site.SystemID = int.Parse(ddlSiteSystems.SelectedValue);
				site.Properties = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(hdnSiteProperties.Value);
				site.Environments = new JavaScriptSerializer().Deserialize<IEnumerable<TestEnvironment>>(hdnSiteEnvs.Value);
				site.ID = Sites.Count;
				Sites.Add(site.ID, site);
				UpdateSites();
				ResetForm();
			}
		}

		protected void btnEditSite_Click(object sender, EventArgs e) {

			TestSite site = GetSite(hdnSiteID.Value);
			site.Name = txtSiteName.Text;
			site.Domain = txtSiteDomain.Text;
			site.SystemID = int.Parse(ddlSiteSystems.SelectedValue);
			site.Properties = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(hdnSiteProperties.Value);
			site.Environments = new JavaScriptSerializer().Deserialize<IEnumerable<TestEnvironment>>(hdnSiteEnvs.Value);	
						
			UpdateSites();
			ResetForm();
		}

		protected void btnRemSite_Click(object sender, EventArgs e) {
			TestSite ts = GetSite(ddlSites.SelectedValue);
			Sites.Remove(ts.ID);
			UpdateSites();
			ResetForm();
		}

		protected TestSite GetSite(string siteID) {
			return Sites[int.Parse(siteID)];
		}

		protected void UpdateSites() {
			JsonSerializer.SetObject<List<TestSite>>(Constants.SiteFile, Sites.Values.ToList());
		}

		#endregion site

		/// <summary>
		/// TODO need to make sure that when the sites get saved they aren't altered by the local settings like the domain prefix or system. 
		/// better yet, how to serialize the sites since the sites property is internal. might need to store the system name on the sites after all
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>

		protected void btnCancel_Click(object sender, EventArgs e) {
			ResetForm();
		}

		protected void ResetForm() {
			Init();
			HideAll();
			pnlSelect.Visible = true;
			h2Env.InnerText = "Environments";
			h2Sys.InnerText = "Systems";
		}
		
		protected void HideAll() {
			pnlSelect.Visible = false;
			pnlEnv.Visible = false;
			pnlSys.Visible = false;
			pnlSite.Visible = false;
			btnAddEnv.Visible = false;
			btnEditEnv.Visible = false;
			btnAddSys.Visible = false;
			btnEditSys.Visible = false;
			btnAddSite.Visible = false;
			btnEditSite.Visible = false;
		}
	}
}