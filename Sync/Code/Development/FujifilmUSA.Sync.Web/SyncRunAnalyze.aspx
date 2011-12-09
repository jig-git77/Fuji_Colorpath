<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true"
    CodeBehind="SyncRunAnalyze.aspx.cs" Inherits="FujifilmUSA.Sync.Web.SyncRunAnalyze" %>

<%@ Register Src="~/UserControl/SyncRunAnalyze.ascx" TagName="SyncRunAnalyze" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:SyncRunAnalyze ID="ucSyncRunAnalyze" runat="server" />
</asp:Content>
