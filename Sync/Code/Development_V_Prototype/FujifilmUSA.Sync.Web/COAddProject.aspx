<%@ Page Title="" Language="C#" MasterPageFile="~/COMaster.Master" AutoEventWireup="true"
    CodeBehind="COAddProject.aspx.cs" Inherits="FujifilmUSA.Sync.Web.COAddProject" %>

<%@ Register Src="~/UserControl/AddProject/ProjectSettingsContainer.ascx" TagName="ProjectSettingsContainer"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ProjectSettingsContainer ID="ucProjectSettingsContainer" runat="server" />
</asp:Content>
