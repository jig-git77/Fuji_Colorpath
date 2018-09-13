<%@ Page Title="" Language="C#" MasterPageFile="~/ReportMaster.Master" AutoEventWireup="true" CodeBehind="GenerateCustomReports.aspx.cs" Inherits="FujifilmUSA.Sync.Web.GenerateCustomReports" %>
<%@ Register Src="~/UserControl/GenerateCustomReports.ascx" TagName="GenerateCustomReports" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:GenerateCustomReports ID="ucGenerateCustomReports" runat="server" />
</asp:Content>
