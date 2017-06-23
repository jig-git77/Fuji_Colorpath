<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="FujifilmUSA.Sync.Web.Settings" %>
<%@ Register Src="~/UserControl/Settings.ascx" TagName="Settings" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<uc:Settings ID="ucSettings" runat="server" />
</asp:Content>
