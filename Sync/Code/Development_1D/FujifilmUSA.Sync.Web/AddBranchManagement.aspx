<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" 
CodeBehind="AddBranchManagement.aspx.cs" 
Inherits="FujifilmUSA.Sync.Web.AddBranchManagement" %>
<%@ Register Src="~/UserControl/AddBranchManagement.ascx" TagName="AddBranchManagement" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:AddBranchManagement ID="ucAddBranchManagement"  runat="server"/>
</asp:Content>
