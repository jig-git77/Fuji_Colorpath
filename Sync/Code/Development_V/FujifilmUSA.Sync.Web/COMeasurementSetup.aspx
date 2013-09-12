<%@ Page Title="" Language="C#" MasterPageFile="~/COMaster.Master" AutoEventWireup="true" CodeBehind="COMeasurementSetup.aspx.cs" Inherits="FujifilmUSA.Sync.Web.COMeasurementSetup" %>
<%@ Register Src="~/UserControl/AddProject/MeasurementSetup.ascx" TagName="MeasurementSetup" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="contant_top">&nbsp;</div>
        <div class="contant_middle"><uc:MeasurementSetup ID="ucMeasurementSetup" runat="server" /></div>    
    <div class="contant_bottom">&nbsp;</div>
</asp:Content>
