<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddProject.aspx.cs" Inherits="FujifilmUSA.Sync.Web.AddProject"
    MasterPageFile="~/MainMaster.Master" EnableEventValidation="false" %>

<%@ Register Src="~/UserControl/AddProject/ProjectSettingsContainer.ascx" TagName="ProjectSettingsContainer" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <uc1:ProjectSettingsContainer ID="ucProjectSettingsContainer" runat="server" />
</asp:Content>
