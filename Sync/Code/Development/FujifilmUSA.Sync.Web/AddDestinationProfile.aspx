<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="AddDestinationProfile.aspx.cs" Inherits="FujifilmUSA.Sync.Web.AddDestinationProfile" %>
<%@ Register Src="~/UserControl/AddDestinationProfile.ascx" TagName="AddDestinationProfile" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<uc:AddDestinationProfile ID="ucAddDestinationProfile" runat="server" />
</asp:Content>
