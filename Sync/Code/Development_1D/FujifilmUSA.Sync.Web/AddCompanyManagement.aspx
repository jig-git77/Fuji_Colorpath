<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" 
CodeBehind="AddCompanyManagement.aspx.cs" 
Inherits="FujifilmUSA.Sync.Web.AddCompanyManagement" %>
<%@ Register Src="~/UserControl/AddCompanyManagement.ascx" TagName="AddCompanyManagement" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<uc:AddCompanyManagement ID="ucAddCompanyManagement"  runat="server"/>
</asp:Content>
