<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HomeMenu.ascx.cs" Inherits="OrisWeb.Common.HomeMenu" %>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td width="23" height="36" valign="top">
            <img src="images/menu-curve-left.png" alt="FujiFilm" border="0" />
        </td>
        <td valign="top" class="content-menu-top">
            <table border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td style="padding-right:10px;padding-left:10px;" id="TDJob" runat="server">
                        <a href="JobsReport.aspx" title="Jobs"><img src="images/jobs-icon.gif" alt="Jobs" border="0" />&nbsp; Jobs</a>
                    </td>
                    <td id="TDJobSpacer" runat="server" class="content-menu-top-divider" width="1">
                        <img src="images/spacer.gif" alt="FujiFilm" border="0" />
                    </td>                    
                    <td style="padding-right:10px;padding-left:10px;" id="TDDevice" runat="server" align="center">
                        <a href="Devices.aspx" title="Device">
                            <img src="images/device-icon.gif" alt="Device" border="0" />&nbsp; Device</a>
                    </td>
                    <td id="TDDeviceSpacer" runat="server" class="content-menu-top-divider" width="1">
                        <img src="images/spacer.gif" alt="FujiFilm" border="0" />
                    </td>                    
                    <td id="TdStatus" runat="server" style="padding-right:10px;padding-left:10px;" align="center">
                        <a href="ProofandPlates.aspx" title="Status">
                            <img src="images/status-icon.gif" alt="Status" border="0" />&nbsp; Status</a>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </td>
        <td width="23" valign="top">
            <img src="images/menu-curve-right.png" alt="FujiFilm" border="0" />
        </td>
    </tr>
</table>
