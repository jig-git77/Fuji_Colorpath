<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ApplicationConfiguration.aspx.cs" Inherits="FujifilmUSA.Sync.Web.ApplicationConfiguration" %>
<%@ Register Src="~/UserControl/ApplicationConfiguration.ascx" TagName="ApplicationConfiguration" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <uc:ApplicationConfiguration ID="ucApplicationConfiguration"  runat="server"/>
</asp:Content>
