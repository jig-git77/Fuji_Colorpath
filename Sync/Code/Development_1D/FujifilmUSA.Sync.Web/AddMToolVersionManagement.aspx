<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddMToolVersionManagement.aspx.cs" Inherits="FujifilmUSA.Sync.Web.AddMToolVersionManagement" %>
<%@ Register Src="~/UserControl/AddMToolVersionManagement.ascx" TagName="AddMToolVersionManagement" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <uc:AddMToolVersionManagement ID="ucAddMToolVersionManagement"  runat="server"/>
</asp:Content>
