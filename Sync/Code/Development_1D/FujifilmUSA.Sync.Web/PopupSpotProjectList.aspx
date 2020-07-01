<%@ Page Title="Brand Color Optimization" Language="C#" MasterPageFile="~/PopUpMaster.Master" AutoEventWireup="true" CodeBehind="PopupSpotProjectList.aspx.cs" 
    Inherits="FujifilmUSA.Sync.Web.PopupSpotProjectList" %>
<%@ Register Src="~/UserControl/PopupControl/SpotProjectList.ascx" 
TagName="SpotProjectList" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <uc:SpotProjectList ID="ucSpotProjectList" runat="server" />
</asp:Content>
