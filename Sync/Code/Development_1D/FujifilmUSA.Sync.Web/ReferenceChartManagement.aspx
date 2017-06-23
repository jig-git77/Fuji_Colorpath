<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ReferenceChartManagement.aspx.cs" Inherits="FujifilmUSA.Sync.Web.ReferenceChartManagement" %>
<%@ Register Src="~/UserControl/ReferenceChartManagement.ascx" TagName="ReferenceChart" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:ReferenceChart ID="ucReferenceChart"  runat="server"/>    
</asp:Content>