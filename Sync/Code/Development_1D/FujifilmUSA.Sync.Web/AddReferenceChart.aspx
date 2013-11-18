<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="AddReferenceChart.aspx.cs" Inherits="FujifilmUSA.Sync.Web.AddReferenceChart" ValidateRequest="false" %>

<%@ Register Src="~/UserControl/AddReferenceChart.ascx" TagName="AddReferenceChart"
    TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:AddReferenceChart ID="ucAddReferenceChart" runat="server" />
</asp:Content>
