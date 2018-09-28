<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DatePicker.ascx.cs" Inherits="DatePicker" %>
<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="rad" %>
<asp:Panel ID="PNL_DATE_PICKER" runat="server">
    <rad:RadDatePicker ID="RadDatePicker1" runat="server" Width="75px" Style="vertical-align: top"></rad:RadDatePicker>&nbsp;
    <asp:Button ID="Button1" CssClass="BUTTON" runat="server" Text="GO" Width="25px" CommandName="SelectDate" ToolTip="Click to search" />
</asp:Panel>
