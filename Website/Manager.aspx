<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UI/layouts/Main.Master" 
    CodeFile="Manager.aspx.cs" 
    Inherits="NUnitTesting.WebApp.Manager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script src="/UI/js/manager.js"></script>
</asp:Content>
 
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <h1>Web Test Data Manager</h1>
    <div class="manager">
        <div class="error">
            <asp:Literal ID="ltlError" runat="server"></asp:Literal>
        </div>
        <asp:Panel ID="pnlSelect" runat="server">
            <div class="formRow">
                <asp:Label ID="Label1" CssClass="title" runat="server" AssociatedControlID="ddlEnvironments" Text="Environments"></asp:Label>
                <div class="bordered">
                    <asp:DropDownList ID="ddlEnvironments" CssClass="envSelDDL" runat="server"></asp:DropDownList>
                    <div class="clearfix"></div>
                    <asp:Button ID="btnShowAddEnv" runat="server" CssClass="submit" OnClick="btnShowAddEnv_Click" Text="Add" />
                    <asp:Button ID="btnShowEditEnv" runat="server" CssClass="submit" OnClick="btnShowEditEnv_Click" Text="Edit" />
                    <asp:Button ID="btnRemEnv" runat="server" CssClass="submit envSelSub" OnClick="btnRemEnv_Click" Text="Remove" />
                </div>
            </div>
            <div class="formRow">
                <asp:Label ID="Label2" CssClass="title" runat="server" AssociatedControlID="ddlSystems" Text="Systems"></asp:Label>
                <div class="bordered">
                    <asp:DropDownList ID="ddlSystems" CssClass="sysSelDDL" runat="server"></asp:DropDownList>
                    <div class="clearfix"></div>
                    <asp:Button ID="btnShowAddSys" runat="server" CssClass="submit" OnClick="btnShowAddSys_Click" Text="Add" />
                    <asp:Button ID="btnShowEditSys" runat="server" CssClass="submit" OnClick="btnShowEditSys_Click" Text="Edit" />
                    <asp:Button ID="btnRemSys" runat="server" CssClass="submit sysSelSub" OnClick="btnRemSys_Click" Text="Remove" />
                </div>
            </div>
            <div class="formRow">
                <asp:Label ID="Label7" CssClass="title" runat="server" AssociatedControlID="ddlSites" Text="Sites"></asp:Label>
                <div class="bordered">
                    <asp:DropDownList ID="ddlSites" CssClass="siteSelDDL" runat="server"></asp:DropDownList>
                    <div class="clearfix"></div>
                    <asp:Button ID="btnShowAddSite" runat="server" CssClass="submit" OnClick="btnShowAddSite_Click" Text="Add" />
                    <asp:Button ID="btnShowEditSite" runat="server" CssClass="submit" OnClick="btnShowEditSite_Click" Text="Edit" />
                    <asp:Button ID="btnRemSite" runat="server" CssClass="submit siteSelSub" OnClick="btnRemSite_Click" Text="Remove" />
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlEnv" Visible="false" runat="server">
            <h2 id="h2Env" runat="server">Environment</h2>
            <div class="bordered">
                <div>
                    <asp:HiddenField ID="hdnEnvID" runat="server" />
                </div>
                <div class="formRow vertical">
                    <asp:Label ID="Label3" CssClass="title" runat="server" AssociatedControlID="txtEnvName" Text="Name"></asp:Label>
                    <asp:TextBox ID="txtEnvName" runat="server"></asp:TextBox>
                </div>
                <div class="formRow vertical">
                    <asp:Label ID="Label4" CssClass="title" runat="server" AssociatedControlID="txtEnvDomain" Text="Domain Prefix"></asp:Label>
                    <asp:TextBox ID="txtEnvDomain" runat="server"></asp:TextBox>
                </div>
                <div class="formRow vertical">
                    <asp:Label ID="Label5" CssClass="title" runat="server" AssociatedControlID="txtEnvIP" Text="IP Address"></asp:Label>
                    <asp:TextBox ID="txtEnvIP" runat="server"></asp:TextBox>
                </div>
                <div class="formRow vertical">
                    <asp:Button ID="btnEditEnv" CssClass="submit" runat="server" OnClick="btnEditEnv_Click" Text="Save" />
                    <asp:Button ID="btnAddEnv" CssClass="submit" runat="server" OnClick="btnAddEnv_Click" Text="Add" />
                    <asp:Button ID="btnCancelEnv" CssClass="submit" runat="server" OnClick="btnCancel_Click" Text="Cancel" />
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlSys" Visible="false" runat="server">
            <h2 id="h2Sys" runat="server">Systems</h2>
            <div class="bordered">
                <div>
                    <asp:HiddenField ID="hdnSysID" runat="server" />
                </div>
                <div class="formRow vertical">
                    <asp:Label ID="Label6" CssClass="title" runat="server" AssociatedControlID="txtSysName" Text="Name"></asp:Label>
                    <asp:TextBox ID="txtSysName" runat="server"></asp:TextBox>
                </div>
                <div class="formRow vertical">
                    <asp:Button ID="btnEditSys" CssClass="submit" runat="server" OnClick="btnEditSys_Click" Text="Save" />
                    <asp:Button ID="btnAddSys" CssClass="submit" runat="server" OnClick="btnAddSys_Click" Text="Add" />
                    <asp:Button ID="btnCancelSys" CssClass="submit" runat="server" OnClick="btnCancel_Click" Text="Cancel" />
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlSite" Visible="false" runat="server">
            <h2 id="h2Site" runat="server">Sites</h2>
            <div class="bordered">
                <div>
                    <asp:HiddenField ID="hdnSiteID" runat="server" />
                </div>
                <div class="formRow vertical">
                    <asp:Label ID="Label8" CssClass="title" runat="server" AssociatedControlID="txtSiteName" Text="Name"></asp:Label>
                    <asp:TextBox ID="txtSiteName" runat="server"></asp:TextBox>
                </div>
                <div class="formRow vertical">
                    <asp:Label ID="Label10" CssClass="title" runat="server" AssociatedControlID="txtSiteDomain" Text="Domain"></asp:Label>
                    <asp:TextBox ID="txtSiteDomain" runat="server"></asp:TextBox>
                </div>
                <div class="formRow vertical">
                    <asp:Label ID="Label9" CssClass="title" runat="server" AssociatedControlID="ddlSiteSystems" Text="System"></asp:Label>
                    <asp:DropDownList ID="ddlSiteSystems" runat="server"></asp:DropDownList>
                </div>
                <div class="formRow vertical">
                    <label class="title">Properties</label>
                    <div class="sitePropVals">
                        <asp:HiddenField ID="hdnSiteProperties" runat="server"></asp:HiddenField>
                    </div>
                    <div class="sitePropList"></div>
                    <div class="sitePropAdd">
                        <input type="submit" class="submit" value="add" />
                    </div>
                </div>
                <div class="formRow vertical">
                    <label class="title">Environments</label>
                    <div class="siteEnvVals">
                        <asp:HiddenField ID="hdnSiteEnvs" runat="server"></asp:HiddenField>
                    </div>
                    <div class="siteEnvList"></div>
                    <div class="siteEnvAdd">
                        <input type="submit" class="submit" value="add" />
                    </div>
                </div>
                <div class="formRow vertical">
                    <asp:Label ID="ltlDisabled" CssClass="title" runat="server" AssociatedControlID="cbDisabled" Text="Disable"></asp:Label>
                    <asp:CheckBox ID="cbDisabled" runat="server"></asp:CheckBox>
                </div>
                <div class="formRow vertical">
                    <asp:Button ID="btnEditSite" CssClass="submit" runat="server" OnClick="btnEditSite_Click" Text="Save" />
                    <asp:Button ID="btnAddSite" CssClass="submit" runat="server" OnClick="btnAddSite_Click" Text="Add" />
                    <asp:Button ID="btnCancelSite" CssClass="submit" runat="server" OnClick="btnCancel_Click" Text="Cancel" />
                </div>
            </div>
        </asp:Panel>
    </div>
    <div class="envPopup popup">
        <div class="popupInner">
            <div class="ename">
                <label class="title">Name</label>
                <asp:DropDownList ID="ddlPopEnvs" runat="server"></asp:DropDownList>
            </div>
            <div class="edomain">
                <label class="title">Domain</label>
                <input type="text" />
            </div>
            <div class="eip">
                <label class="title">IP</label>
                <input type="text" />
            </div>
            <div class="ebtns">
                <input id="envSubmit" type="submit" class="submit" value="submit" />
                <input id="envCancel" type="submit" class="submit" value="cancel" />
            </div>
        </div>
    </div>
    <div class="propPopup popup">
        <div class="popupInner">
            <div class="ekey">
                <label class="title">Key</label>
                <input type="text" />
            </div>
            <div class="evalue">
                <label class="title">Value</label>
                <input type="text" />
            </div>
            <div class="ebtns">
                <input id="propSubmit" type="submit" class="submit" value="submit" />
                <input id="propCancel" type="submit" class="submit" value="cancel" />
            </div>
        </div>
    </div>
    <div class="remPopup popup">
        <div class="popupInner">
            <div class="message">Are you sure you want to remove this item?</div>
            <div class="ebtns">
                <input id="Submit1" type="submit" class="submit" value="ok" />
                <input id="Submit2" type="submit" class="submit" value="cancel" />
            </div>
        </div>
    </div>
</asp:Content>
