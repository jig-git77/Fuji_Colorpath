<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MeasurementStatusbar.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.MeasurementStatusbar" %>

<div class="statusbar info_form">
    <div>
        <asp:Label ID="lblMessage" runat="server" Text="<b>Status</b> : Scanning device is not loaded." style="float:left;" ></asp:Label>
        <asp:Label ID="lblPluginMessage" runat="server" Text="loading..." style="float:right;"></asp:Label>
    </div>
</div>