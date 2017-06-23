<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" 
CodeBehind="UserManagement.aspx.cs" 
Inherits="FujifilmUSA.Sync.Web.UserManagement" %>
<%@ Register Src="~/UserControl/UserManagement.ascx" TagName="UserManagement" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<uc:UserManagement ID="ucUserManagement"  runat="server"/>
</asp:Content>
