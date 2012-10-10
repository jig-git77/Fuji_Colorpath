<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/PopUpMaster.Master"
    CodeBehind="PopupOpenForgotPassword.aspx.cs" Inherits="FujifilmUSA.Sync.Web.PopupOpenForgotPassword" %>

<%@ Register Src="~/UserControl/PopupControl/ForgotPassword.ascx" TagName="ForgotPassword"
    TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:ForgotPassword ID="ucForgotPassword" runat="server" />
</asp:Content>
