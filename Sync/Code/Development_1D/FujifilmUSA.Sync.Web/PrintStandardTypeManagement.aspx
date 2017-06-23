<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="PrintStandardTypeManagement.aspx.cs" Inherits="FujifilmUSA.Sync.Web.PrintStandardTypeManagement" %>

<%@ Register Src="~/UserControl/PrintStandardTypeManagement.ascx" TagName="PrintStandardTypeManagement" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:PrintStandardTypeManagement ID="ucPrintStandardTypeManagement"  runat="server"/>
</asp:Content>