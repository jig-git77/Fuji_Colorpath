<%@ Page Title="" Language="C#" MasterPageFile="~/PopUpMaster.Master" AutoEventWireup="true" CodeBehind="PopupSpotLibraryExport.aspx.cs" Inherits="FujifilmUSA.Sync.Web.PopupSpotLibraryExport" %>
<%@ Register Src="~/UserControl/PopupControl/SpotLibraryExport.ascx" TagName="SpotLibraryExport" TagPrefix="pd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <pd:SpotLibraryExport runat="server" ID="ucSpotLibraryExport" />
</asp:Content>
