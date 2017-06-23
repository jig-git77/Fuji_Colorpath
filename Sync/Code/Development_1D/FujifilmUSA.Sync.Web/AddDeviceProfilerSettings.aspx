<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddDeviceProfilerSettings.aspx.cs" Inherits="FujifilmUSA.Sync.Web.AddDeviceProfilerSettings" %>
<%@ Register Src="~/UserControl/AddDeviceProfilerSettings.ascx" TagName="AddProfilerSettings" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:AddProfilerSettings ID="ucAddProfilerSettings"  runat="server"/>
</asp:Content>