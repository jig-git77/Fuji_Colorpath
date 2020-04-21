<%@ Page Language="C#" MasterPageFile="~/PopUpMaster.Master" AutoEventWireup="true" CodeBehind="PopupSpotColorPatchDetails.aspx.cs"
    Inherits="FujifilmUSA.Sync.Web.PopupSpotColorPatchDetails"
    Title="Patch Details" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register Src="~/UserControl/PopupControl/SpotColorPatchDetails.ascx" TagName="SpotColorPatchDetails" TagPrefix="pd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <pd:SpotColorPatchDetails runat="server" ID="ucSpotColorPathDetails" />
</asp:Content>
