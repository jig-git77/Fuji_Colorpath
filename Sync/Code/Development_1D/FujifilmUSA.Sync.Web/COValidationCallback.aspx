<%@ Page Title="" Language="C#" MasterPageFile="~/COMaster.Master" AutoEventWireup="true" CodeBehind="COValidationCallback.aspx.cs" Inherits="FujifilmUSA.Sync.Web.COValidationCallback" %>
<%@ Register Src="~/UserControl/COValidationCallback.ascx" TagName="COValidationCallback"
    TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:COValidationCallback ID="ucCOValidationCallback" runat="server" />
</asp:Content>
