<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="DestinationProfileList.aspx.cs" Inherits="FujifilmUSA.Sync.Web.DestinationProfileList" %>
<%@ Register Src="~/UserControl/DestinationProfileList.ascx" TagName="DestinationProfileList" TagPrefix="uc"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<uc:DestinationProfileList ID="ucDestinationProfile" runat="server" />
</asp:Content>
 