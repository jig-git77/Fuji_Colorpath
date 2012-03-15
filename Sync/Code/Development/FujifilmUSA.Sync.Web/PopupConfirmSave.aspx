<%@ Page Title="" Language="C#" MasterPageFile="~/PopUpMaster.Master" 
AutoEventWireup="true" 
CodeBehind="PopupConfirmSave.aspx.cs" 
Inherits="FujifilmUSA.Sync.Web.PopupConfirmSave" %>
<%@ Register Src="~/UserControl/PopupControl/ConfirmSave.ascx" 
    TagName="ConfirmSave" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:ConfirmSave ID="ucConfirmSave" runat="server" />
</asp:Content>
