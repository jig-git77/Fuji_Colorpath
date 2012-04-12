<%@ Page Title="Save Print Condition And Download Chart" Language="C#" MasterPageFile="~/PopUpMaster.Master" 
AutoEventWireup="true" 
CodeBehind="PopupSaveProject.aspx.cs" 
Inherits="FujifilmUSA.Sync.Web.PopupSaveProject" %>
<%@ Register Src="~/UserControl/PopupControl/SaveWindow.ascx" 
    TagName="SaveWindow" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:SaveWindow ID="ucSaveWindow" runat="server" />
</asp:Content>
