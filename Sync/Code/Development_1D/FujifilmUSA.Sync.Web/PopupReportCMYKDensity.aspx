<%@ Page Title="" Language="C#" MasterPageFile="~/PopUpMaster.Master" AutoEventWireup="true" CodeBehind="PopupReportCMYKDensity.aspx.cs" Inherits="FujifilmUSA.Sync.Web.PopupReportCMYKDensity" %>
<%@ Register Src="~/UserControl/PopupControl/ReportCMYKDensity.ascx" 
    TagName="ReportCMYKDensityWindow" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:ReportCMYKDensityWindow ID="ucReportCMYKDensityWindow" runat="server" />
</asp:Content>
