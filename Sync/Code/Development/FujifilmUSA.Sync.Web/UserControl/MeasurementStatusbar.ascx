<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MeasurementStatusbar.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.MeasurementStatusbar" %>
<div class="mesurementStatusbar info_form">
    <div class="top">
        &nbsp;</div>
    <div class="middle" runat="server" id="tblMessage">
        <asp:Label ID="lblMessage" runat="server" Text="<b>Status</b> : Scanning device is not loaded."
            Style="float: left;"></asp:Label>
        <asp:Label ID="lblPluginMessage" runat="server" Text="loading..." Style="float: right;"></asp:Label>
        &nbsp;
        <br />
    </div>
    <div class="bottom">
        &nbsp;</div>
</div>
