<%@ Page Language="C#" MasterPageFile="~/PopUpMaster.Master" 
AutoEventWireup="true" 
CodeBehind="PopupClosePrintCondition.aspx.cs" 
Inherits="FujifilmUSA.Sync.Web.PopupClosePrintCondition" 
Title="Select Next Action" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register Src="~/UserControl/PopupControl/ClosePrintCondition.ascx" 
TagName="ClosePrintCondition" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:ClosePrintCondition ID="ucClosePrintCondition" runat="server" />
</asp:Content>
