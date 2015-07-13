<%@ Page Title="" Language="C#" MasterPageFile="~/PopUpMaster.Master" AutoEventWireup="true" CodeBehind="PopupReportGrading.aspx.cs" Inherits="FujifilmUSA.Sync.Web.PopupReportGrading" %>
<%@ Register Src="~/UserControl/PopupControl/ReportGrading.ascx" 
    TagName="ReportGradingWindow" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <uc:ReportGradingWindow ID="ucReportGradingWindow" runat="server" />
</asp:Content>
