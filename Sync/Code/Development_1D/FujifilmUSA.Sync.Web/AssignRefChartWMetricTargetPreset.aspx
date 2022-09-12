<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/AdminMaster.Master" CodeBehind="AssignRefChartWMetricTargetPreset.aspx.cs" Inherits="FujifilmUSA.Sync.Web.AssignRefChartWMetricTargetPreset"  EnableEventValidation="false" %>
<%@ Register Src="~/UserControl/AssignRefChartWMetricTargetPreset.ascx" TagName="AssignRefChartWMetricTargetPreset" TagPrefix="uc" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:AssignRefChartWMetricTargetPreset ID="ucAssignRefChartWMetricTargetPreset"  runat="server"/>
</asp:Content>