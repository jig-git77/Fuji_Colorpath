<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Statusbar.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.Statusbar" %>

 <table id="tblMessage" runat="server" width="100%" style="border: 1px solid #0C3D30;
            background-color: #6BB19C; color: Black; height: 45px;" cellpadding="0" cellspacing="0">
            <tr>
                <td style="padding: 2px 2px 2px 5px; font-weight: bold;" valign="top">
                    <asp:Label ID="lblMessage" runat="server" Text="Status : Scanning device is not loaded."></asp:Label>
                </td>
                <td style="padding: 2px 5px 2px 2px; font-weight: bold;" align="right" valign="top">
                    <asp:Label ID="lblPluginMessage" runat="server" Text="loading..."></asp:Label>
                </td>
            </tr>
        </table>