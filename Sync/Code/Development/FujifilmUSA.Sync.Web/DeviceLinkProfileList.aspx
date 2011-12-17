<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="DeviceLinkProfileList.aspx.cs" Inherits="FujifilmUSA.Sync.Web.DeviceLinkProfileList" %>
<%@ Register Src="~/UserControl/DeviceLinkProfileList.ascx" TagName="DeviceLinkProfileList" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<uc:DeviceLinkProfileList ID="ucDeviceLinkProfileList" runat="server"/>
</asp:Content>
