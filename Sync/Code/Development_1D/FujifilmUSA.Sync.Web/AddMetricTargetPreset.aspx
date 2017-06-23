<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddMetricTargetPreset.aspx.cs" 
Inherits="FujifilmUSA.Sync.Web.AddMetricTargetPreset" %>
<%@ Register Src="~/UserControl/AddMetricTargetPreset.ascx" TagName="AddMetricTargetPreset" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:AddMetricTargetPreset ID="ucAddMetricTargetPreset" runat="server"/>
</asp:Content>
