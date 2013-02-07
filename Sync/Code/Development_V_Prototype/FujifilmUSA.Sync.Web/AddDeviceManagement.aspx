<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" 
CodeBehind="AddDeviceManagement.aspx.cs" 
Inherits="FujifilmUSA.Sync.Web.AddDeviceManagement" %>
<%@ Register Src="~/UserControl/AddDeviceManagement.ascx" TagName="AddDeviceManagement" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<uc:AddDeviceManagement ID="ucAddDeviceManagement"  runat="server"/>
</asp:Content>
