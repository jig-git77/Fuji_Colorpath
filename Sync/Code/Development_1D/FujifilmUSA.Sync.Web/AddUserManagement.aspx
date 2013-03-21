<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" 
CodeBehind="AddUserManagement.aspx.cs" 
Inherits="FujifilmUSA.Sync.Web.AddUserManagement" %>
<%@ Register Src="~/UserControl/AddUserManagement.ascx" TagName="AddUserManagement" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:AddUserManagement ID="ucAddUserManagement"  runat="server"/>
</asp:Content>
