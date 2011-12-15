<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="DestinationProfile.aspx.cs" Inherits="FujifilmUSA.Sync.Web.DestinationProfile" %>
<%@ Register Src="~/UserControl/DestinationProfile.ascx" TagName="DestinationProfile" TagPrefix="uc"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<uc:DestinationProfile ID="ucDestinationProfile" runat="server" />
</asp:Content>
