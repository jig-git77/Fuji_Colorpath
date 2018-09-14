<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="MetricTargetManagement.aspx.cs" Inherits="FujifilmUSA.Sync.Web.MetricTargetManagement" %>
<%@ Register Src="~/UserControl/MetricTargetManagement.ascx" TagName="MetricTargetManagement" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:MetricTargetManagement ID="ucMetricTargetManagement"  runat="server"/>
</asp:Content>
