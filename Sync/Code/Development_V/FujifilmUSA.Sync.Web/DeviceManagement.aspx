<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" 
CodeBehind="DeviceManagement.aspx.cs" 
Inherits="FujifilmUSA.Sync.Web.DeviceManagement" %>
<%@ Register Src="~/UserControl/DeviceManagement.ascx" TagName="DeviceManagement" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<uc:DeviceManagement ID="ucDeviceManagement"  runat="server"/>
</asp:Content>
