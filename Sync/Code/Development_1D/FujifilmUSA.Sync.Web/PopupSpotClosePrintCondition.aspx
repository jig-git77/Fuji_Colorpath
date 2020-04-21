<%@ Page Language="C#" MasterPageFile="~/PopUpMaster.Master" AutoEventWireup="true" 
    CodeBehind="PopupSpotClosePrintCondition.aspx.cs" Inherits="FujifilmUSA.Sync.Web.PopupSpotClosePrintCondition" 
    Title="Select Next Action" culture="auto" meta:resourcekey="PageResource1" uiculture="auto"%>
<%@ Register Src="~/UserControl/PopupControl/SpotClosePrintCondition.ascx" 
TagName="SpotClosePrintCondition" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:SpotClosePrintCondition ID="ucClosePrintCondition" runat="server" />
</asp:Content>
