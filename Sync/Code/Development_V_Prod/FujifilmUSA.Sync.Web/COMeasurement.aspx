<%@ Page Title="" Language="C#" MasterPageFile="~/COMaster.Master" AutoEventWireup="true"
    CodeBehind="COMeasurement.aspx.cs" Inherits="FujifilmUSA.Sync.Web.COMeasurement"
    EnableEventValidation="false" %>

<%@ Register Src="~/UserControl/Measurement.ascx" TagName="Measurement" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:Measurement ID="ucMeasurement" runat="server" />
</asp:Content>
