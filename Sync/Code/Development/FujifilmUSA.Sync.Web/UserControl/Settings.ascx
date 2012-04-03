<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.Settings" %>


<script type="text/javascript">
      jQuery(document).ready(function () {
          $('#dock').hide();
    });   
</script>

<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle">
    <div class="head">
        <h1>
            Admin</h1>
    </div>
    <div class="clear">
    </div>
    <center>
        <table cellspacing="0" border="0">
            <tr>
                <td style="height:50px;">
                </td>
            </tr>
            <tr>
                <td style=" float:left;" >
                    <div id="tdcompany" runat ="server"  align="center" style="width:150px;float:left;">
                        <asp:LinkButton ID="lbtnCompanyManagement" runat="server" ToolTip="Company Management"
                            PostBackUrl="~/CompanyManagement.aspx" 
                            onclick="lbtnCompanyManagement_Click">
                            <div>
                                <asp:Image ID="Image1" runat="server" AlternateText="Company Management" ImageUrl="~/Images/company-manage-icon.png"
                                    ToolTip="Company Management" /></div>
                            Company Management</asp:LinkButton>
                </div>
                <div id="divcompanySeprator" runat ="server" align="center" valign="bottom" style="float:left;">
                        <img alt="" src="Images/admin-icon-divider.png">
                </div>
                    <div id="tdBranch" runat ="server" align="center" valign="bottom" style="width:150px;float:left;">
                        <asp:LinkButton ID="lbtnBranchManagement" runat="server" ToolTip="Branch Management"
                            PostBackUrl="~/BranchManagement.aspx" onclick="lbtnBranchManagement_Click">
                            <div>
                                <asp:Image ID="Image2" runat="server" AlternateText="Branch Management" ImageUrl="~/Images/branch-manage-icon.png"
                                    ToolTip="Branch Management" /></div>
                            Branch Management</asp:LinkButton>
                    
                </div>
                    <div id="divBranchSeprator" runat ="server" align="center" valign="bottom" style="float:left;">
                        <img alt="" src="Images/admin-icon-divider.png">
                </div>
                    <div id="tdUser" runat ="server" align="center" valign="bottom" style="width:150px;float:left;">
                        <asp:LinkButton ID="lbtnUserManagement" runat="server" 
                            ToolTip="User Management" PostBackUrl="~/UserManagement.aspx" 
                            onclick="lbtnUserManagement_Click" >
                            <div>
                                <asp:Image ID="Image3" runat="server" AlternateText="User Management" ImageUrl="~/Images/user-manage-icon.png"
                                    ToolTip="User Management" /></div>
                            User Management</asp:LinkButton>
                </div>
                    <div id="divUserSeprator" runat ="server" align="center" valign="bottom" style="float:left;">
                        <img alt="" src="Images/admin-icon-divider.png">
                </div>
                    <div id="tdSourceProfile" runat ="server" align="center" valign="bottom" style="width:150px;float:left;">
                        <asp:LinkButton ID="lbtnSourceProfile" runat="server" ToolTip="Soruce Profile" 
                            PostBackUrl="~/SourceProfileManagement.aspx" onclick="lbtnSourceProfile_Click">
                            <div>
                                <asp:Image ID="imgSoruceProfile" runat="server" AlternateText="Soruce Profile" ImageUrl="~/Images/sourceprofile.png"
                                    ToolTip="Soruce Profile" /></div>
                            Source Profile</asp:LinkButton>
                </div>
                </td>
            </tr>
            <tr>
                <td style="height:50px;">
                </td>
            </tr>
            <tr>
                <td style=" float:left;" >
                <div id="tdDevice" runat ="server" align="center" valign="bottom" style="width:150px;float:left;">
                    <asp:LinkButton ID="lbtnDeviceManagement" runat="server" ToolTip="Device Management"
                        PostBackUrl="~/DeviceManagement.aspx" onclick="lbtnDeviceManagement_Click">
                        <div>
                            <asp:Image ID="imgDeviceManagement" runat="server" AlternateText="Device Management"
                                ImageUrl="~/Images/device-manage-icon.png" ToolTip="Device Management" /></div>
                        Device Management</asp:LinkButton>
                </div>
                <div id="divDeviceSeprator" runat ="server" align="center" valign="bottom" style="float:left;">
                    <img alt="" src="Images/admin-icon-divider.png">
                </div>
                <div id="tdLicense" runat ="server" align="center" valign="bottom" style="width:150px;float:left;" >
                    <asp:LinkButton ID="lbtnLicenseManagement" runat="server" 
                        ToolTip="License Management" onclick="lbtnLicenseManagement_Click">
                        <div>
                            <asp:Image ID="imgLicenseManagement" runat="server" AlternateText="License Management"
                                ImageUrl="~/Images/dongle-manage.png" ToolTip="License Management" /></div>
                        License Management</asp:LinkButton>
                </div>
                <div id="divLicenseSeprator" runat ="server" align="center" valign="bottom" style="float:left;">
                    <img alt="" src="Images/admin-icon-divider.png">
                </div>
                <div id="tdMeasurementDevice" runat ="server" align="center" valign="bottom" style="width:150px;float:left;" >
                    <asp:LinkButton ID="lnkMeasurementDevice" runat="server" ToolTip="Measurement Device"
                        PostBackUrl="~/MeasurementDeviceList.aspx" 
                        onclick="lnkMeasurementDevice_Click">
                        <div>
                            <asp:Image ID="imgMeasurementDevice" runat="server" AlternateText="Measurement Device"
                                ImageUrl="~/Images/syncmeasurementdevice.png" ToolTip="Measurement Device" /></div>
                        Measurement Device
                    </asp:LinkButton>
                </div>
                <div id="divMeasurementSeprator" runat ="server" align="center" valign="bottom" style="float:left;">
                    <img alt="" src="Images/admin-icon-divider.png">
                </div>
                </td>
            </tr>
        </table>


    </center>
</div>
<div class="admin_bottom">
    &nbsp;</div>
