<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" 
CodeBehind="LicenseManagement.aspx.cs" 
Inherits="FujifilmUSA.Sync.Web.LicenseManagement" %>
<%@ Register Src="~/UserControl/LicenseManagement.ascx" TagName="LicenseManagement" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<uc:LicenseManagement ID="ucLicenseManagement"  runat="server"/>
</asp:Content>
