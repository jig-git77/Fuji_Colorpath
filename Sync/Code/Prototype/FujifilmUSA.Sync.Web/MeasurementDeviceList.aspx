<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="MeasurementDeviceList.aspx.cs" Inherits="FujifilmUSA.Sync.Web.MeasurementDeviceList" %>
<%@ Register Src="~/UserControl/MeasurementDeviceList.ascx" TagName="MeasurementDevice" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<uc:MeasurementDevice ID="ucMeasurementDevice"  runat="server"/>
</asp:Content>
