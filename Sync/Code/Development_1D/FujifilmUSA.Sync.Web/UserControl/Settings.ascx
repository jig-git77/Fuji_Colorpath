<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="Settings.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.Settings" %>


<script type="text/javascript">
      jQuery(document).ready(function () {
          $('#dock').hide();
    });   
</script>

<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle">
    <div class="head">
        <h1><asp:Localize runat="server" Text="Admin" ID="locAdmin" 
                meta:resourcekey="locAdminResource1"></asp:Localize></h1>
    </div>
    <div class="clear">
    </div>
    <center>
        <table cellspacing="0" border="0">
            <tr>
                <td style="height:50px;">
                </td>
            </tr>
            <tr align="center">
                <td  align="center" >
                    <div id="tdcompany" runat ="server"  align="center" style="width:150px;float:left;">
                        <asp:LinkButton ID="lbtnCompanyManagement" runat="server" ToolTip="Company Management"
                            PostBackUrl="~/CompanyManagement.aspx" 
                            onclick="lbtnCompanyManagement_Click">
                            <div>
                                <asp:Image ID="Image1" runat="server" AlternateText="Company Management" ImageUrl="~/Images/company-manage-icon.png"
                                    ToolTip="Company Management" meta:resourcekey="Image1Resource1" />
</div>
                            <asp:Localize Text="Company Management" ID="locCompanyManagement" 
                            runat="server" meta:resourcekey="locCompanyManagementResource1"></asp:Localize>
</asp:LinkButton>
                </div>
                <div id="divBranchSeprator" runat ="server" align="center" valign="bottom" style="float:left;">
                        <img alt="" src="Images/admin-icon-divider.png" />
                </div>
                    <div id="tdBranch" runat ="server" align="center" valign="bottom" style="width:150px;float:left;">
                        <asp:LinkButton ID="lbtnBranchManagement" runat="server" ToolTip="Branch Management"
                            PostBackUrl="~/BranchManagement.aspx" onclick="lbtnBranchManagement_Click">
                            <div>
                                <asp:Image ID="Image2" runat="server" AlternateText="Branch Management" ImageUrl="~/Images/branch-manage-icon.png"
                                    ToolTip="Branch Management" meta:resourcekey="Image2Resource1" />
</div>
                            <asp:Localize Text="Branch Management" runat="server" 
                            ID="locBranchManagement" meta:resourcekey="locBranchManagementResource1"></asp:Localize>
</asp:LinkButton>
                    
                </div>
                    <div id="divUserSeprator" runat ="server" align="center" valign="bottom" style="float:left;">
                        <img alt="" src="Images/admin-icon-divider.png" />
                </div>
                    <div id="tdUser" runat ="server" align="center" valign="bottom" style="width:150px;float:left;">
                        <asp:LinkButton ID="lbtnUserManagement" runat="server" 
                            ToolTip="User Management" PostBackUrl="~/UserManagement.aspx" 
                            onclick="lbtnUserManagement_Click">
                            <div>
                                <asp:Image ID="Image3" runat="server" AlternateText="User Management" ImageUrl="~/Images/user-manage-icon.png"
                                    ToolTip="User Management" meta:resourcekey="Image3Resource1" />
</div>
                            <asp:Localize Text="User Management" runat="server" ID="locUserMgmt" 
                            meta:resourcekey="locUserMgmtResource1"></asp:Localize>
</asp:LinkButton>
                </div>
                    <div id="divProfileManagementSeprator" runat ="server" align="center" valign="bottom" style="float:left;">
                        <img alt="" src="Images/admin-icon-divider.png" />
                </div>
                 <div id="tdProfileManagement" runat ="server" align="center" valign="bottom" style="width:150px;float:left;" >
                       <asp:LinkButton ID="lnkProfileMgmt" runat="server" ToolTip="My Settings"
                        PostBackUrl="~/ProfileManagement.aspx" onclick="lnkProfileMgmt_Click">                      
                        <div>
                            <asp:Image ID="Image4" runat="server" AlternateText="My Settings"
                                ImageUrl="~/Images/Profile-Management.png" ToolTip="My Settings" 
                                meta:resourcekey="Image4Resource1" />
</div>
                       <asp:Localize Text="My Settings" runat="server" ID="locMySettings" 
                           meta:resourcekey="locMySettingsResource1"></asp:Localize>
                    
</asp:LinkButton>
                </div>
                <div id="divLicenseSeprator" runat ="server" align="center" valign="bottom" style="float:left;">
                    <img alt="" src="Images/admin-icon-divider.png" />
                </div>
                <div id="tdLicense" runat ="server" align="center" valign="bottom" style="width:150px;float:left;" >
                    <asp:LinkButton ID="lbtnLicenseManagement" runat="server" 
                        ToolTip="License Management" onclick="lbtnLicenseManagement_Click">
                        <div>
                            <asp:Image ID="imgLicenseManagement" runat="server" AlternateText="License Management"
                                ImageUrl="~/Images/dongle-manage.png" ToolTip="License Management" 
                                meta:resourcekey="imgLicenseManagementResource1" />
</div>
                        <asp:Localize Text="License Management" runat="server" ID="locLicenseMgmt" 
                        meta:resourcekey="locLicenseMgmtResource1"></asp:Localize>
</asp:LinkButton>
                </div>   
                </td>
            </tr>
            <tr>
                <td style="height:50px;">
                </td>
            </tr>
            <tr align="center">
                <td align="center" >
                <div id="tdDevice" runat ="server" align="center" valign="bottom" style="width:150px;float:left;">
                    <asp:LinkButton ID="lbtnDeviceManagement" runat="server" ToolTip="Device Management"
                        PostBackUrl="~/DeviceManagement.aspx" onclick="lbtnDeviceManagement_Click">
                        <div>
                            <asp:Image ID="imgDeviceManagement" runat="server" AlternateText="Device Management"
                                ImageUrl="~/Images/device-manage-icon.png" ToolTip="Device Management" 
                                meta:resourcekey="imgDeviceManagementResource1" />
</div>
                        <asp:Localize Text="Device Management" runat="server" ID="locDeviceMgmt" 
                        meta:resourcekey="locDeviceMgmtResource1"></asp:Localize>
</asp:LinkButton>
                </div>
                <div id="divMeasurementSeprator" runat ="server" align="center" valign="bottom" style="float:left;">
                    <img alt="" src="Images/admin-icon-divider.png" />
                </div>
               
               
                <div id="tdMeasurementDevice" runat ="server" align="center" valign="bottom" style="width:150px;float:left;" >
                    <asp:LinkButton ID="lnkMeasurementDevice" runat="server" ToolTip="Measurement Device"
                        PostBackUrl="~/MeasurementDeviceList.aspx" 
                        onclick="lnkMeasurementDevice_Click">
                        <div>
                            <asp:Image ID="imgMeasurementDevice" runat="server" AlternateText="Measurement Device"
                                ImageUrl="~/Images/syncmeasurementdevice.png" ToolTip="Measurement Device" 
                                meta:resourcekey="imgMeasurementDeviceResource1" />
</div>
                        <asp:Localize Text="Measurement Device" runat="server" 
                        ID="locMeasurementDevice" meta:resourcekey="locMeasurementDeviceResource1"></asp:Localize>
                    
</asp:LinkButton>
                </div>
                <div id="divDownloadSeprator" runat ="server" align="center" valign="bottom" style="float:left;">
                    <img alt="" src="Images/admin-icon-divider.png" />
                </div>
                 <div id="tdDownload" runat ="server" align="center" valign="bottom" style="width:150px;float:left;" >
                       <asp:LinkButton ID="lnkDownload" runat="server" ToolTip="Download"
                        PostBackUrl="~/DownloadSoftwares.aspx" onclick="lnkDownload_Click">                      
                        <div>
                            <asp:Image ID="Image5" runat="server" AlternateText="Download"
                                ImageUrl="~/Images/download-icon.png" ToolTip="Download" 
                                meta:resourcekey="Image5Resource1" />
</div>
                        <asp:Localize Text="Download" runat="server" ID="locDownload" 
                           meta:resourcekey="locDownloadResource1"></asp:Localize>
                    
</asp:LinkButton>
                </div>
                <div id="divRefChartSeparator" runat ="server" align="center" valign="bottom" style="float:left;">
                    <img alt="" src="Images/admin-icon-divider.png" />
                </div>
                  <div id="tdReferenceChart" runat ="server" align="center" valign="bottom" style="width:150px;float:left;" >
                    <asp:LinkButton ID="lnkRefChart" runat="server" ToolTip="Reference Chart"
                        PostBackUrl="~/ReferenceChartManagement.aspx" onclick="lnkRefChart_Click">
                       
                        <div>
                            <asp:Image ID="imgRefChart" runat="server" AlternateText="Reference Chart"
                                ImageUrl="~/Images/Reference-Chart.jpg" ToolTip="Reference Chart" 
                                meta:resourcekey="imgRefChartResource1" />
                        </div>
                        <asp:Localize runat="server" Text="Reference Chart" ID="locReferenceChart" 
                          meta:resourcekey="locReferenceChartResource1"></asp:Localize>
                    
                    </asp:LinkButton>
                </div>
                 <div id="divSourceProfileSeprator" runat ="server" align="center" valign="bottom" style="float:left;">
                    <img alt="" src="Images/admin-icon-divider.png" />
                </div>
               <div id="tdSourceProfile" runat ="server" align="center" valign="bottom" style="width:150px;float:left;">
                        <asp:LinkButton ID="lbtnSourceProfile" runat="server" ToolTip="Soruce Profile" 
                            PostBackUrl="~/SourceProfileManagement.aspx" 
                            onclick="lbtnSourceProfile_Click">
                            <div>
                                <asp:Image ID="imgSoruceProfile" runat="server" AlternateText="Soruce Profile" ImageUrl="~/Images/sourceprofile.png"
                                    ToolTip="Soruce Profile" meta:resourcekey="imgSoruceProfileResource1" />
</div>
                            <asp:Localize Text="Source Profile" runat="server" ID="locSourceProfile" 
                            meta:resourcekey="locSourceProfileResource1"></asp:Localize>
</asp:LinkButton>
                </div>                
                </td>
            </tr>
            <tr>
                <td style="height:50px;">
                </td>
            </tr>
            <tr align="center">
                <td align="center" >
                <div id="dvActivityValidationRules" runat ="server" align="center" valign="bottom" style="width:150px;float:left;">
                    <asp:LinkButton ID="lnkBtnActivityValidationRules" runat="server" ToolTip="Activity Validation Rules"
                        PostBackUrl="~/ActivityValidationRules.aspx">
                        <div>
                            <asp:Image ID="Image6" runat="server" AlternateText="Activity Validation Rules"
                                ImageUrl="~/Images/activity-validation-rules-icon.png" ToolTip="Activity Validation Rules" />
                        </div>
                        <asp:Localize Text="Activity Validation Rules" runat="server" ID="locActivityValidationRules"></asp:Localize>
                    </asp:LinkButton>
                </div>                               
                <div id="divProfilerSettingSeparator" runat ="server" align="center" valign="bottom" style="float:left;">
                    <img alt="" src="Images/admin-icon-divider.png" />
                </div>                             
                  <div id="dvProfilerSettings" runat ="server" align="center" valign="bottom" style="width:150px;float:left;" >
                     <asp:LinkButton ID="lnkProfilerSettings" runat="server" ToolTip="Device Family Profiler Settings"
                        PostBackUrl="~/AddDeviceProfilerSettings.aspx" Text="Device Family Profiler Settings" >                                   
                         <div>
                            <asp:Image ID="Image_ProfilerSettings" runat="server" AlternateText="Device Family Profiler Settings"
                                ImageUrl="~/Images/Profiler-Settings.jpg" ToolTip="Device Family Profiler Settings" 
                                meta:resourcekey="imgProfilerSettingsResource1" />
                        </div>
                        <asp:Localize runat="server" Text="Device Family Profiler Settings" ID="locProfilerSettings" 
                          meta:resourcekey="locProfilerSettingsResource1"></asp:Localize>                    
                    </asp:LinkButton>
                </div>
                 <div id="divApplicationConfigurationSeparator" runat ="server" align="center" valign="bottom" style="float:left;">
                    <img alt="" src="Images/admin-icon-divider.png" />
                </div> 
                <div id="dvApplicationConfiguration" runat="server" align="center" valign="bottom" style="width: 150px; float: left">
                    <asp:LinkButton ID="lnkbtnApplicationConfiguration" runat="server" ToolTip="Application Configuration" PostBackUrl="~/ApplicationConfiguration.aspx">
                        <div>
                            <asp:Image ID="imgApplicationConfig" runat="server" AlternateText="Application Configuration" ImageUrl="~/Images/application-configuration-icon.png" ToolTip="Application Configuration" />
                        </div>
                        <asp:Localize Text="Application Configuration" runat="server" ID="locApplicationConfiguration"></asp:Localize>
                    </asp:LinkButton>                
                </div>     
                 <div id="div1" runat ="server" align="center" valign="bottom" style="float:left;">
                    <img alt="" src="Images/admin-icon-divider.png" />
                </div> 
                 <div id="divMetricTargetPreset" runat="server" align="center" valign="bottom" style="width: 150px; float: left">
                    <asp:LinkButton ID="lnkbtnMetricTargetPreset" runat="server" 
                         ToolTip="Metric Target Preset" 
                         PostBackUrl="~/MetricTargetPresetManagement.aspx" 
                         onclick="lnkbtnMetricTargetPreset_Click">
                        <div>
                            <asp:Image ID="Image7" runat="server" AlternateText="Metric Target Preset" ImageUrl="~/Images/application-configuration-icon.png" ToolTip="Metric Target Preset" />
                        </div>
                        <asp:Localize Text="Metric Target Preset" runat="server" ID="Localize1"></asp:Localize>
                    </asp:LinkButton>                
                </div>               
                
                </td>
                </tr>            
        </table>


    </center>
</div>
<div class="admin_bottom">
    &nbsp;</div>
