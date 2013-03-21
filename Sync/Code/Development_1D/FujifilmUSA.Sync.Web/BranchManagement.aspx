<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" 
CodeBehind="BranchManagement.aspx.cs" 
Inherits="FujifilmUSA.Sync.Web.BranchManagement" %>
<%@ Register Src="~/UserControl/BranchManagement.ascx" TagName="BranchManagement" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<uc:BranchManagement ID="ucBranchManagement"  runat="server"/>
</asp:Content>
