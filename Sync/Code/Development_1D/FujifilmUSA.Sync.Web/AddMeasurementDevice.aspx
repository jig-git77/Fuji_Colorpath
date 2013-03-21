<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" 
CodeBehind="AddMeasurementDevice.aspx.cs" Inherits="FujifilmUSA.Sync.Web.AddMeasurementDevice" %>
<%@ Register Src="~/UserControl/AddMeasurementDevice.ascx" TagName="AddMeasurementDevice" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:AddMeasurementDevice ID="ucAddMeasurementDevice"  runat="server"/>
</asp:Content>
