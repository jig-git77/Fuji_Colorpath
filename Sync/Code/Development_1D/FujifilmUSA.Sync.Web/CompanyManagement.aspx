<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" 
CodeBehind="CompanyManagement.aspx.cs" 
Inherits="FujifilmUSA.Sync.Web.CompanyManagement" %>
<%@ Register Src="~/UserControl/CompanyManagement.ascx" TagName="CompanyManagement" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<uc:CompanyManagement ID="ucCompanyManagement"  runat="server"/>
</asp:Content>
