<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="MetricTargetPresetManagement.aspx.cs" Inherits="FujifilmUSA.Sync.Web.MetricTargetPresetManagement" %>
<%@ Register Src="~/UserControl/MetricTargetPresetManagement.ascx" TagName="MetricTargetPresetManagement" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<uc:MetricTargetPresetManagement ID="ucMetricTargetPresetManagement"  runat="server"/>
</asp:Content>
