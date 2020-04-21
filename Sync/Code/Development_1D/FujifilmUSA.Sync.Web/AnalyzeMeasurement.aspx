<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="AnalyzeMeasurement.aspx.cs" Inherits="FujifilmUSA.Sync.Web.AnalyzeMeasurement" EnableEventValidation="false" %>
<%@ Register Src="~/UserControl/Analyze.ascx" TagName="Analyze" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:Analyze ID="ucAnalyze" runat="server" />
</asp:Content>
