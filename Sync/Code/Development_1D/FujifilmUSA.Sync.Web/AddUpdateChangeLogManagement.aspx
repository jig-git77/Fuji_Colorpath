<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddUpdateChangeLogManagement.aspx.cs" Inherits="FujifilmUSA.Sync.Web.AddUpdateChangeLogManagement" %>
<%@ Register Src="~/UserControl/AddUpdateChangeLogManagement.ascx" TagName="AddUpdateChangeLogManagement" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:AddUpdateChangeLogManagement ID="ucAddUpdateChangeLogManagement"  runat="server"/>
</asp:Content>
