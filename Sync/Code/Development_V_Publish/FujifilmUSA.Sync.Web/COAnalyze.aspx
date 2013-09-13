<%@ Page Title="" Language="C#" MasterPageFile="~/COMaster.Master" AutoEventWireup="true"
    CodeBehind="COAnalyze.aspx.cs" Inherits="FujifilmUSA.Sync.Web.COAnalyze" EnableEventValidation="false" %>

<%@ Register Src="~/UserControl/Analyze.ascx" TagName="Analyze" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:Analyze ID="ucAnalyze" runat="server" />
</asp:Content>
