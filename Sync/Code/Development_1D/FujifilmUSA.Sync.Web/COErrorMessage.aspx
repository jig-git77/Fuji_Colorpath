<%@ Page Title="" Language="C#" MasterPageFile="~/COMaster.Master" AutoEventWireup="true" CodeBehind="COErrorMessage.aspx.cs" Inherits="FujifilmUSA.Sync.Web.COErrorMessage" %>
<%@ Register Src="~/UserControl/COErrorMessage.ascx" TagName="COErrorMessage"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:COErrorMessage ID="ucCOErrorMessage" runat="server" />
</asp:Content>
