<%@ Page Title="" Language="C#" MasterPageFile="~/COMaster.Master" AutoEventWireup="true"
    CodeBehind="COTransferSiteControl.aspx.cs" Inherits="FujifilmUSA.Sync.Web.COTransferSiteControl" %>

<%@ Register Src="~/UserControl/COTransferSiteControl.ascx" TagName="COTransferSiteControl"
    TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:COTransferSiteControl ID="ucCOTransferSiteControl" runat="server" />
</asp:Content>
