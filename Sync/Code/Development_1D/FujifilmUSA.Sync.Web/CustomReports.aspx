<%@ Page Title="" Language="C#" MasterPageFile="~/ReportMaster.Master" AutoEventWireup="true" CodeBehind="CustomReports.aspx.cs" Inherits="FujifilmUSA.Sync.Web.CustomReports" %>
<%@ Register Src="~/UserControl/CustomReports.ascx" TagName="CustomReports" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:CustomReports ID="ucCustomReports" runat="server" />
</asp:Content>
