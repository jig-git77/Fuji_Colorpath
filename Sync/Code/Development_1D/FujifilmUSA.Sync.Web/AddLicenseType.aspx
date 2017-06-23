<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" 
CodeBehind="AddLicenseType.aspx.cs" 
Inherits="FujifilmUSA.Sync.Web.AddLicenseType" %>
<%@ Register Src="~/UserControl/AddLicenseType.ascx" TagName="AddLicenseType" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<uc:AddLicenseType ID="ucAddLicenseType"  runat="server"/>
</asp:Content>
