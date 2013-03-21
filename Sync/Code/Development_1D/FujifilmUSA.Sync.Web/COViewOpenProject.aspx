<%@ Page Title="" Language="C#" MasterPageFile="~/COMaster.Master" AutoEventWireup="true" CodeBehind="COViewOpenProject.aspx.cs" Inherits="FujifilmUSA.Sync.Web.COViewOpenProject" %>

<%@ Register Src="~/UserControl/COViewOpenProject.ascx" TagName="COViewOpenProject"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:COViewOpenProject ID="ucCOViewOpenProject" runat="server" />
</asp:Content>
