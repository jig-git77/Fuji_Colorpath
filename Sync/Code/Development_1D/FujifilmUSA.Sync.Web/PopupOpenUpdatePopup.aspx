<%@ Page Language="C#" MasterPageFile="~/PopUpMaster.Master" 
AutoEventWireup="true" 
CodeBehind="PopupOpenUpdatePopup.aspx.cs" 
Inherits="FujifilmUSA.Sync.Web.PopupOpenUpdatePopup" Title="We've got some updates for you!" culture="auto" uiculture="auto" %>

<%@ Register Src="~/UserControl/PopupControl/OpenUpdatePopup.ascx" TagName="OpenUpdatePopup" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:OpenUpdatePopup ID="ucOpenUpdatePopup" runat="server" />
</asp:Content>
