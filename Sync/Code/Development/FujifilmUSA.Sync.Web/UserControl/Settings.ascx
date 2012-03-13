<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="Settings.ascx.cs" 
    Inherits="FujifilmUSA.Sync.Web.UserControl.Settings" %>
<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle">
    <div class="head">
        <h1>
            Settings</h1>
    </div>
    <div class="clear">
    </div>
    <%--<table border="0" cellspacing="0" cellpadding="1" align="center">
    <tr>        
        <td align="center" valign="bottom" style="width: 130px" runat="server" id="TD_Role">
            <a href="MeasurementDeviceList.aspx" title="Measurement Device">
            <img src="../Images/syncmeasurementdevice.png" border="0" alt='MeasurementDeviceList' /><br />Measurement Device</a>
        </td>
        <td width="5" align="center" valign="top" id="TD_divider" runat="server" visible="false"><img src="images/admin-icon-divider.png" alt="divider" border="0" /></td>
    </tr>
</table>--%>
    <table border="0" cellpadding="1" cellspacing="0" style="margin-left: 10px"  >
        <tr>
            <td align="center" valign="bottom" style="width: 130px">
                <asp:LinkButton ID="lnkMeasurementDevice" runat="server" ToolTip="Measurement Device"
                    OnClick="lnkMeasurementDevice_Click">
                    <div>
                        <asp:Image ID="imgMeasurementDevice" runat="server" AlternateText="Measurement Device"
                            ImageUrl="../Images/syncmeasurementdevice.png" ToolTip="Measurement Device" /></div>
                    Measurement Device</asp:LinkButton>
            </td>
            <td align="center" valign="bottom" style="width: 130px">
                <asp:LinkButton ID="lbtnCompanyManagement" runat="server" ToolTip="Company Management" >
                    <div>
                        <asp:Image ID="Image1" runat="server" AlternateText="Company Management"
                            ImageUrl="../Images/company-manage-icon.png" ToolTip="Company Management" /></div>
                    Company</asp:LinkButton>
            </td>
            <td align="center" valign="bottom" style="width: 130px">
                <asp:LinkButton ID="lbtnBranchManagement" runat="server" ToolTip="Branch Management">
                    <div>
                        <asp:Image ID="Image2" runat="server" AlternateText="Branch Management"
                            ImageUrl="../Images/branch-manage-icon.png" ToolTip="Branch Management" /></div>
                    Branch</asp:LinkButton>
            </td>
            <td align="center" valign="bottom" style="width: 130px">
                <asp:LinkButton ID="lbtnUserManagement" runat="server" ToolTip="User Management">
                    <div>
                        <asp:Image ID="Image3" runat="server" AlternateText="User Management"
                            ImageUrl="../Images/user-manage-icon.png" ToolTip="User Management" /></div>
                    User</asp:LinkButton>
            </td>
            
        </tr>
    </table>
</div>
<div class="admin_bottom">
    &nbsp;</div>
