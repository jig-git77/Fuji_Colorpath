<%@ Page Title="" Language="C#" MasterPageFile="~/PopUpMaster.Master" AutoEventWireup="true" CodeBehind="PopupSpotProjectOpenPrintCondition.aspx.cs" Inherits="FujifilmUSA.Sync.Web.PopupSpotProjectOpenPrintCondition" %>
<%@ Register Src="~/UserControl/PopupControl/SpotProjectOpenPrintCondition.ascx" TagName="OpenPrintCondition" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:OpenPrintCondition ID="ucOpenPrintCondition" runat="server" />
</asp:Content>
