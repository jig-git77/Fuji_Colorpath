<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" 
CodeBehind="AddLicenseManagement.aspx.cs" 
Inherits="FujifilmUSA.Sync.Web.AddLicenseManagement" %>
<%@ Register Src="~/UserControl/AddLicenseManagement.ascx" TagName="AddLicenseManagement" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<uc:AddLicenseManagement ID="ucAddLicenseManagement"  runat="server"/>
</asp:Content>
