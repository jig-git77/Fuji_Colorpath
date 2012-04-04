<%@ Page Language="C#" MasterPageFile="~/PopUpMaster.Master" 
AutoEventWireup="true" 
CodeBehind="PopupOpenPrintCondition.aspx.cs" 
Inherits="FujifilmUSA.Sync.Web.PopupOpenPrintCondition" Title="Open Print Condition" %>
<%@ Register Src="~/UserControl/PopupControl/OpenPrintCondition.ascx" TagName="OpenPrintCondition" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:OpenPrintCondition ID="ucOpenPrintCondition" runat="server" />
</asp:Content>
