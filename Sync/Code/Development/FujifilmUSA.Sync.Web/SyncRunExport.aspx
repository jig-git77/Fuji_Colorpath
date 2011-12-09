<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true"
    CodeBehind="SyncRunExport.aspx.cs" Inherits="FujifilmUSA.Sync.Web.SyncRunExport" %>

<%@ Register Src="~/UserControl/SyncRunExport.ascx" TagName="SyncRunExport" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:SyncRunExport ID="ucSyncRunExport" runat="server" />
</asp:Content>
