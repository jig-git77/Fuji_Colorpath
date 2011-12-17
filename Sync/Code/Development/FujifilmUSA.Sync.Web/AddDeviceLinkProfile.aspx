<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddDeviceLinkProfile.aspx.cs" Inherits="FujifilmUSA.Sync.Web.AddDeviceLinkProfile" %>
<%@ Register Src="~/UserControl/AddDeviceLinkProfile.ascx" TagName="AddDeviceLinkProfile" TagPrefix="uc"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<uc:AddDeviceLinkProfile ID="ucAddDeviceLinkProfile" runat="server" />
</asp:Content>
