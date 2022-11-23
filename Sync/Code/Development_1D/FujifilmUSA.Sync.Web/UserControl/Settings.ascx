<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="Settings.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.Settings" %>


<script type="text/javascript">
    jQuery(document).ready(function () {
        $('#dock').hide();
    });
</script>
<style>
    .grid-container {
        display: flex;
        align-items: center;
        flex-wrap: wrap;
    }

    .grid-item {
        display: inherit;
        /*margin: 10px 0;*/
        margin: 10px 8px;
        flex-basis: 18%;
        text-align: center;
    }
</style>
<div class="admin_top">
    &nbsp;
</div>
<div class="admin_middle">
    <div class="head">
        <h1>
            <asp:Localize runat="server" Text="Admin" ID="locAdmin"
                meta:resourcekey="locAdminResource1"></asp:Localize></h1>
    </div>
    <div class="clear">
    </div>
    <%-- <center>--%>
    <div class="grid-container">
        <div class="grid-item" runat="server" id="divGridItemCompany">
            <div id="tdcompany" runat="server" align="center" style="width: 100%;">
                <asp:LinkButton ID="lbtnCompanyManagement" runat="server"
                    PostBackUrl="~/CompanyManagement.aspx"
                    OnClick="lbtnCompanyManagement_Click">
                    <div>
                        <asp:Image ID="Image1" runat="server" AlternateText="Company Management" ImageUrl="~/Images/company-manage-icon.png"
                            ToolTip="Company Management" meta:resourcekey="Image1Resource1" />
                    </div>
                    <asp:Localize Text="Company Management" ID="locCompanyManagement"
                        runat="server" meta:resourcekey="locCompanyManagementResource1"></asp:Localize>
                </asp:LinkButton>
            </div>
        </div>
        <div class="grid-item" runat="server" id="divGridItemBranch">
            <%--<div id="divBranchSeprator" runat="server" align="center" valign="bottom" style="float: left;">
                <img alt="" src="Images/admin-icon-divider.png" />
            </div>--%>
            <div id="tdBranch" runat="server" align="center" valign="bottom" style="width: 100%;">
                <asp:LinkButton ID="lbtnBranchManagement" runat="server"
                    PostBackUrl="~/BranchManagement.aspx" OnClick="lbtnBranchManagement_Click">
                    <div>
                        <asp:Image ID="Image2" runat="server" AlternateText="Branch Management" ImageUrl="~/Images/branch-manage-icon.png"
                            ToolTip="Branch Management" meta:resourcekey="Image2Resource1" />
                    </div>
                    <asp:Localize Text="Branch Management" runat="server"
                        ID="locBranchManagement" meta:resourcekey="locBranchManagementResource1"></asp:Localize>
                </asp:LinkButton>

            </div>
        </div>
        <div class="grid-item" runat="server" id="divGridItemUserManagement">
         <%--   <div id="divUserSeprator" runat="server" align="center" valign="bottom" style="float: left;">
                <img alt="" src="Images/admin-icon-divider.png" />
            </div>--%>
            <div id="tdUser" runat="server" align="center" valign="bottom" style="width: 100%;">
                <asp:LinkButton ID="lbtnUserManagement" runat="server"
                    PostBackUrl="~/UserManagement.aspx"
                    OnClick="lbtnUserManagement_Click">
                    <div>
                        <asp:Image ID="Image6" runat="server" AlternateText="User Management" ImageUrl="~/Images/user-manage-icon.png"
                            ToolTip="User Management" meta:resourcekey="Image3Resource1" />
                    </div>
                    <asp:Localize Text="User Management" runat="server" ID="locUserMgmt"
                        meta:resourcekey="locUserMgmtResource1"></asp:Localize>
                </asp:LinkButton>
            </div>
        </div>
        <div class="grid-item" runat="server" id="divGridItemProfileManagement">
           <%-- <div id="divProfileManagementSeprator" runat="server" align="center" valign="bottom" style="float: left;">
                <img alt="" src="Images/admin-icon-divider.png" />
            </div>--%>
            <div id="tdProfileManagement" runat="server" align="center" valign="bottom" style="width: 100%;">
                <asp:LinkButton ID="lnkProfileMgmt" runat="server"
                    PostBackUrl="~/ProfileManagement.aspx" OnClick="lnkProfileMgmt_Click">
                    <div>
                        <asp:Image ID="Image7" runat="server" AlternateText="My Settings"
                            ImageUrl="~/Images/Profile-Management.png" ToolTip="My Settings"
                            meta:resourcekey="Image4Resource1" />
                    </div>
                    <asp:Localize Text="My Settings" runat="server" ID="locMySettings"
                        meta:resourcekey="locMySettingsResource1"></asp:Localize>

                </asp:LinkButton>
            </div>
        </div>
        <div class="grid-item" runat="server" id="divGridItemLicenseManagement">
           <%-- <div id="divLicenseSeprator" runat="server" align="center" valign="bottom" style="float: left;">
                <img alt="" src="Images/admin-icon-divider.png" />
            </div>--%>
            <div id="tdLicense" runat="server" align="center" valign="bottom" style="width: 100%;">
                <asp:LinkButton ID="lbtnLicenseManagement" runat="server"
                    OnClick="lbtnLicenseManagement_Click">
                    <div>
                        <asp:Image ID="imgLicenseManagement" runat="server" AlternateText="License Management"
                            ImageUrl="~/Images/dongle-manage.png" ToolTip="License Management"
                            meta:resourcekey="imgLicenseManagementResource1" />
                    </div>
                    <asp:Localize Text="License Management" runat="server" ID="locLicenseMgmt"
                        meta:resourcekey="locLicenseMgmtResource1"></asp:Localize>
                </asp:LinkButton>
            </div>
        </div>
        <div class="grid-item" runat="server" id="divGridItemDeviceManagement">
            <div id="tdDevice" runat="server" align="center" valign="bottom" style="width: 100%;">
                <asp:LinkButton ID="lbtnDeviceManagement" runat="server"
                    PostBackUrl="~/DeviceManagement.aspx" OnClick="lbtnDeviceManagement_Click">
                    <div>
                        <asp:Image ID="imgDeviceManagement" runat="server" AlternateText="Device Management"
                            ImageUrl="~/Images/device-manage-icon.png" ToolTip="Device Management"
                            meta:resourcekey="imgDeviceManagementResource1" />
                    </div>
                    <asp:Localize Text="Device Management" runat="server" ID="locDeviceMgmt"
                        meta:resourcekey="locDeviceMgmtResource1"></asp:Localize>
                </asp:LinkButton>
            </div>
        </div>
        <div class="grid-item" runat="server" id="divGridItemMeasurementDevice">
         <%--   <div id="divMeasurementSeprator" runat="server" align="center" valign="bottom" style="float: left;">
                <img alt="" src="Images/admin-icon-divider.png" />
            </div>--%>


            <div id="tdMeasurementDevice" runat="server" align="center" valign="bottom" style="width: 100%;">
                <asp:LinkButton ID="lnkMeasurementDevice" runat="server"
                    PostBackUrl="~/MeasurementDeviceList.aspx"
                    OnClick="lnkMeasurementDevice_Click">
                    <div>
                        <asp:Image ID="imgMeasurementDevice" runat="server" AlternateText="Measurement Device"
                            ImageUrl="~/Images/syncmeasurementdevice.png" ToolTip="Measurement Device"
                            meta:resourcekey="imgMeasurementDeviceResource1" />
                    </div>
                    <asp:Localize Text="Measurement Device" runat="server"
                        ID="locMeasurementDevice" meta:resourcekey="locMeasurementDeviceResource1"></asp:Localize>

                </asp:LinkButton>
            </div>
        </div>
        <div class="grid-item" runat="server" id="divGridItemDownload">
         <%--   <div id="divDownloadSeprator" runat="server" align="center" valign="bottom" style="float: left;">
                <img alt="" src="Images/admin-icon-divider.png" />
            </div>--%>
            <div id="tdDownload" runat="server" align="center" valign="bottom" style="width: 100%;">
                <asp:LinkButton ID="lnkDownload" runat="server"
                    PostBackUrl="~/DownloadSoftwares.aspx" OnClick="lnkDownload_Click">
                    <div>
                        <asp:Image ID="Image5" runat="server" AlternateText="Download"
                            ImageUrl="~/Images/download-icon.png" ToolTip="Download"
                            meta:resourcekey="Image5Resource1" />
                    </div>
                    <asp:Localize Text="Download" runat="server" ID="locDownload"
                        meta:resourcekey="locDownloadResource1"></asp:Localize>

                </asp:LinkButton>
            </div>
        </div>
        <div class="grid-item" runat="server" id="divGridItemReferenceChart">
           <%-- <div id="divRefChartSeparator" runat="server" align="center" valign="bottom" style="float: left;">
                <img alt="" src="Images/admin-icon-divider.png" />
            </div>--%>
            <div id="tdReferenceChart" runat="server" align="center" valign="bottom" style="width: 100%;">
                <asp:LinkButton ID="lnkRefChart" runat="server"
                    PostBackUrl="~/ReferenceChartManagement.aspx" OnClick="lnkRefChart_Click">

                    <div>
                        <asp:Image ID="imgRefChart" runat="server" AlternateText="Reference Chart"
                            ImageUrl="~/Images/Reference-Chart.jpg" ToolTip="Reference Chart"
                            meta:resourcekey="imgRefChartResource1" />
                    </div>
                    <asp:Localize runat="server" Text="Reference Chart" ID="locReferenceChart"
                        meta:resourcekey="locReferenceChartResource1"></asp:Localize>

                </asp:LinkButton>
            </div>
        </div>
        <div class="grid-item" runat="server" id="divGridItemSourceProfile">
          <%--  <div id="divSourceProfileSeprator" runat="server" align="center" valign="bottom" style="float: left;">
                <img alt="" src="Images/admin-icon-divider.png" />
            </div>--%>
            <div id="tdSourceProfile" runat="server" align="center" valign="bottom" style="width: 100%;">
                <asp:LinkButton ID="lbtnSourceProfile" runat="server"
                    PostBackUrl="~/SourceProfileManagement.aspx"
                    OnClick="lbtnSourceProfile_Click">
                    <div>
                        <asp:Image ID="imgSoruceProfile" runat="server" AlternateText="Soruce Profile" ImageUrl="~/Images/sourceprofile.png"
                            ToolTip="Soruce Profile" meta:resourcekey="imgSoruceProfileResource1" />
                    </div>
                    <asp:Localize Text="Source Profile" runat="server" ID="locSourceProfile"
                        meta:resourcekey="locSourceProfileResource1"></asp:Localize>
                </asp:LinkButton>
            </div>
        </div>
        <div class="grid-item" runat="server" id="divGridItemActivityValidationRules">
            <div id="dvActivityValidationRules" runat="server" align="center" valign="bottom" style="width: 100%;">
                <asp:LinkButton ID="lnkBtnActivityValidationRules" runat="server"
                    PostBackUrl="~/ActivityValidationRules.aspx">
                    <div>
                        <asp:Image ID="imgActivityValidationRules" runat="server" AlternateText="Activity Validation Rules"
                            ImageUrl="~/Images/activity-validation-rules-icon.png" ToolTip="Activity Validation Rules" meta:resourcekey="imgActivityValidationRulesResource1" />
                    </div>
                    <asp:Localize Text="Activity Validation Rules" runat="server" ID="locActivityValidationRules" meta:resourcekey="locActivityValidationRulesResource1"></asp:Localize>
                </asp:LinkButton>
            </div>
        </div>
        <div class="grid-item" runat="server" id="divGridItemProfilerSettings">
            <%--<div id="divProfilerSettingSeparator" runat="server" align="center" valign="bottom" style="float: left;">
                <img alt="" src="Images/admin-icon-divider.png" />
            </div>--%>
            <div id="dvProfilerSettings" runat="server" align="center" valign="bottom" style="width: 100%;">
                <asp:LinkButton ID="lnkProfilerSettings" runat="server"
                    PostBackUrl="~/AddDeviceProfilerSettings.aspx" Text="Device Family Profiler Settings">
                    <div>
                        <asp:Image ID="Image_ProfilerSettings" runat="server" AlternateText="Device Family Profiler Settings"
                            ImageUrl="~/Images/Profiler-Settings.jpg" ToolTip="Device Family Profiler Settings"
                            meta:resourcekey="imgProfilerSettingsResource1" />
                    </div>
                    <asp:Localize runat="server" Text="Device Family Profiler Settings" ID="locProfilerSettings"
                        meta:resourcekey="locProfilerSettingsResource1"></asp:Localize>
                </asp:LinkButton>
            </div>
        </div>
        <div class="grid-item" runat="server" id="divGridItemApplicationConfiguration">
           <%-- <div id="divApplicationConfigurationSeparator" runat="server" align="center" valign="bottom" style="float: left;">
                <img alt="" src="Images/admin-icon-divider.png" />
            </div>--%>
            <div id="dvApplicationConfiguration" runat="server" align="center" valign="bottom" style="width: 100%;">
                <asp:LinkButton ID="lnkbtnApplicationConfiguration" runat="server" PostBackUrl="~/ApplicationConfiguration.aspx">
                    <div>
                        <asp:Image ID="imgApplicationConfig" runat="server" AlternateText="Application Configuration" ImageUrl="~/Images/application-configuration-icon.png" ToolTip="Application Configuration" meta:resourcekey="imgApplicationConfigResource1" />
                    </div>
                    <asp:Localize Text="Application Configuration" runat="server" ID="locApplicationConfiguration" meta:resourcekey="locApplicationConfigurationResource1"></asp:Localize>
                </asp:LinkButton>
            </div>
        </div>
        <div class="grid-item" runat="server" id="divGridItemMetricTargetPreset">
            <%--<div id="divMetricTargetPresetSeparator" runat="server" align="center" valign="bottom" style="float: left;">
                <img alt="" src="Images/admin-icon-divider.png" />
            </div>--%>
            <div id="divMetricTargetPreset" runat="server" align="center" valign="bottom" style="width: 100%;">
                <asp:LinkButton ID="lnkbtnMetricTargetPreset" runat="server"
                    PostBackUrl="~/MetricTargetPresetManagement.aspx" OnClick="lnkbtnMetricTargetPreset_Click">
                    <div>
                        <asp:Image ID="Image3" runat="server" AlternateText="Metric Target Preset" ImageUrl="~/Images/application-configuration-icon.png" ToolTip="Metric Target Preset" meta:resourcekey="imgMetricTargetPresetResource1" />
                    </div>
                    <asp:Localize Text="Metric Target Preset" runat="server" ID="locMetricTargetPreset" meta:resourcekey="locMetricTargetPresetResource1"></asp:Localize>
                </asp:LinkButton>
            </div>
        </div>
        <div class="grid-item" runat="server" id="divGridItemPatchPreset">
           <%-- <div id="divPatchPresetSeparator" runat="server" align="center" valign="bottom" style="float: left;">
                <img alt="" src="Images/admin-icon-divider.png" />
            </div>--%>
            <div id="divPatchPreset" runat="server" align="center" valign="bottom" style="width: 100%;">
                <asp:LinkButton ID="lnkbtnPatchPreset" runat="server"
                    PostBackUrl="~/PatchPresetManagement.aspx" OnClick="lnkbtnPatchPreset_Click">
                    <div>
                        <asp:Image ID="Image9" runat="server" AlternateText="Patch Preset" ImageUrl="~/Images/application-configuration-icon.png" ToolTip="Patch Preset" meta:resourcekey="imgPatchPresetResource1" />
                    </div>
                    <asp:Localize Text="Patch Preset" runat="server" ID="Localize3" meta:resourcekey="locPatchPresetResource1"></asp:Localize>
                </asp:LinkButton>
            </div>
        </div>

        <div class="grid-item" runat="server" id="divGridItemPrintStandardType">
            <div id="divPrintStandardType" runat="server" align="center" valign="bottom" style="width: 100%;">
                <asp:LinkButton ID="lnkbtnPrintStandardType" runat="server"
                    ToolTip="Print Standard Type"
                    PostBackUrl="~/PrintStandardTypeManagement.aspx"
                    OnClick="lnkbtnPrintStandardType_Click">
                    <div>
                        <asp:Image ID="Image8" runat="server" AlternateText="Print Standard Type" ImageUrl="~/Images/application-configuration-icon.png" ToolTip="Print Standard Type" />
                    </div>
                    <asp:Localize Text="Print Standard Type" runat="server" ID="Localize4"></asp:Localize>
                </asp:LinkButton>
            </div>
        </div>

        <div class="grid-item" runat="server" id="divGridItemLookupTVILABDensity">
           <%-- <div id="divLookupTVILABDensitySeparator" runat="server" align="center" valign="bottom" style="float: left;">
                <img alt="" src="Images/admin-icon-divider.png" />
            </div>--%>
            <div id="divLookupTVILABDensity" runat="server" align="center" valign="bottom" style="width: 100%;">
                <asp:LinkButton ID="LinkButton3" runat="server"
                    ToolTip="Add Lookup TVILABDensity"
                    PostBackUrl="~/AddLookupTVILABDensity.aspx">
                    <div>
                        <asp:Image ID="Image4" runat="server" AlternateText="AddLookupTVILABDensity" ImageUrl="~/Images/application-configuration-icon.png" ToolTip="Add Lookup TVILABDensity" />
                    </div>
                    <asp:Localize Text="Sync1D LAB TVI Density Values" runat="server" ID="Localize5"></asp:Localize>
                </asp:LinkButton>
            </div>
        </div>

        <div class="grid-item" runat="server" id="divGridItemSpotTarget">
           <%-- <div id="divSpotTargetSeparator" runat="server" align="center" valign="bottom" style="float: left;">
                <img alt="" src="Images/admin-icon-divider.png" />
            </div>--%>
            <div id="divSpotTarget" runat="server" align="center" valign="bottom" style="width: 100%;">
                <asp:LinkButton ID="lnkSpotTarget" runat="server"
                    PostBackUrl="~/SpotTargetManagement.aspx" OnClick="lnkSpotTarget_Click">
                    <div>
                        <asp:Image ID="Image10" runat="server" AlternateText="Spot Target"
                            ImageUrl="~/Images/Reference-Chart.jpg" ToolTip="Spot Target"
                            meta:resourcekey="imgSpotTargetResource1" />
                    </div>
                    <asp:Localize runat="server" Text="Spot Target" ID="Localize6"
                        meta:resourcekey="locSpotTargetResource1"></asp:Localize>
                </asp:LinkButton>
            </div>
        </div>

        <div class="grid-item" runat="server" id="divGridItemMetricTarget">
           <%-- <div id="divMetricTargetSeperator" runat="server" align="center" valign="bottom" style="float: left;">
                <img alt="" src="Images/admin-icon-divider.png" />
            </div>--%>
            <div id="divMetricTarget" runat="server" align="center" valign="bottom" style="width: 100%;">
                <asp:LinkButton ID="lnkMetricTarget" runat="server"
                    PostBackUrl="~/MetricTargetManagement.aspx">
                    <div>
                        <asp:Image ID="Image11" runat="server" AlternateText="Spot Target"
                            ImageUrl="~/Images/application-configuration-icon.png" ToolTip="Metric Target"
                            meta:resourcekey="imgMetricTargetResource1" />
                    </div>
                    <asp:Localize runat="server" Text="Metric Target" ID="Localize7"
                        meta:resourcekey="locMetricTargetResource1"></asp:Localize>
                </asp:LinkButton>
            </div>
        </div>

        <div class="grid-item" runat="server" id="divGridItemMeasurementLog">
            <%--<div id="divMeasurementLogSeperator" runat="server" align="center" valign="bottom" style="float: left;">
                <img alt="" src="Images/admin-icon-divider.png" />
            </div>--%>
            <div id="divMeasurementLog" runat="server" align="center" valign="bottom" style="width: 100%;">
                <asp:LinkButton ID="lnkMeasurementLog" runat="server"
                    PostBackUrl="~/MeasurementLog.aspx">
                    <div>
                        <asp:Image ID="ImgMeasurementLog" runat="server" AlternateText="Measurement Log"
                            ImageUrl="~/Images/application-configuration-icon.png" ToolTip="Measurement Log"
                            meta:resourcekey="imgMeasurementLogResource1" />
                    </div>
                    <asp:Localize runat="server" Text="Measurement Log" ID="Localize8"
                        meta:resourcekey="locMeasurementLogResource1"></asp:Localize>
                </asp:LinkButton>
            </div>

        </div>

        <div class="grid-item" runat="server" id="divGridItemMToolVersion">
            <div id="divMToolVersion" runat="server" align="center" valign="bottom" style="width: 100%;">
                <asp:LinkButton ID="lnkMToolVersion" runat="server"
                    PostBackUrl="~/MToolVersionManagement.aspx">
                    <div>
                        <asp:Image ID="ImgMToolVersion" runat="server" AlternateText="MTool Version"
                            ImageUrl="~/Images/application-configuration-icon.png" ToolTip="MTool Version" />
                    </div>
                    <asp:Localize runat="server" Text="MTool Version" ID="Localize9"></asp:Localize>
                </asp:LinkButton>
            </div>
        </div>

        <div class="grid-item" runat="server" id="divGridItemUpdateChangeLog">
           <%-- <div id="divUpdateChangeLogSeperator" runat="server" align="center" valign="bottom" style="float: left;">
                <img alt="" src="Images/admin-icon-divider.png" />
            </div>--%>
            <div id="divUpdateChangeLog" runat="server" align="center" valign="bottom" style="width: 100%;">
                <asp:LinkButton ID="lnkUpdateChangeLog" runat="server"
                    PostBackUrl="~/UpdateChangeLogManagement.aspx">
                    <div>
                        <asp:Image ID="ImgUpdateChangeLog" runat="server" AlternateText="Update Change Log" ImageUrl="~/Images/application-configuration-icon.png" ToolTip="Update Change Log" />
                    </div>
                    <asp:Localize runat="server" Text="Update Change Log" ID="Localize10"></asp:Localize>
                </asp:LinkButton>
            </div>
        </div>

        <div class="grid-item" runat="server" id="divGridItemAssignChartWithMTP">
           <%-- <div id="divAssignChartWithMTPSeperator" runat="server" align="center" valign="bottom" style="float: left;">
                <img alt="" src="Images/admin-icon-divider.png" />
            </div>--%>
            <div id="divAssignChartWithMTP" runat="server" align="center" valign="bottom" style="width: 100%;">
                <asp:LinkButton ID="lnkAssignChartWithMTP" runat="server"
                    PostBackUrl="~/AssignRefChartWMetricTargetPreset.aspx">
                    <div>
                        <asp:Image ID="ImgAssignChartWithMTP" runat="server" AlternateText="Assign Chart with MTP" ImageUrl="~/Images/application-configuration-icon.png" ToolTip="Assign Chart with MTP" />
                    </div>
                    <asp:Localize runat="server" Text="Assign Chart with MTP" ID="Localize11"></asp:Localize>
                </asp:LinkButton>
            </div>
        </div>

        <div class="grid-item" runat="server" id="divGridItemCustomBcoTarget">
           <%-- <div id="divAddCustomBcoTargetSeperator" runat="server" align="center" valign="bottom" style="float: left;">
                <img alt="" src="Images/admin-icon-divider.png" />
            </div>--%>
            <div id="divAddCustomBcoTarget" runat="server" align="center" valign="bottom" style="width: 100%;">
                <asp:LinkButton ID="lnkAddCustomBcoTarget" runat="server"
                    PostBackUrl="~/CustomBcoManagement.aspx">
                    <div>
                        <asp:Image ID="ImgAddCustomBcoTarget" runat="server" AlternateText="Custom BCO Target Library" ImageUrl="~/Images/custom_bco_icon.png" ToolTip="Custom BCO Target Library" />
                    </div>
                    <asp:Localize runat="server" Text="Custom BCO Target Library" ID="Localize12"></asp:Localize>
                </asp:LinkButton>
            </div>
        </div>
    </div>
    <%--  <table cellspacing="0" border="0">
            <tr>
                <td style="height: 50px;"></td>
            </tr>
            <tr align="center">
                <td align="center">
                    <div id="tdcompany" runat="server" align="center" style="width: 150px; float: left;">
                        <asp:LinkButton ID="lbtnCompanyManagement" runat="server"
                            PostBackUrl="~/CompanyManagement.aspx"
                            OnClick="lbtnCompanyManagement_Click">
                            <div>
                                <asp:Image ID="Image1" runat="server" AlternateText="Company Management" ImageUrl="~/Images/company-manage-icon.png"
                                    ToolTip="Company Management" meta:resourcekey="Image1Resource1" />
                            </div>
                            <asp:Localize Text="Company Management" ID="locCompanyManagement"
                                runat="server" meta:resourcekey="locCompanyManagementResource1"></asp:Localize>
                        </asp:LinkButton>
                    </div>
                    <div id="divBranchSeprator" runat="server" align="center" valign="bottom" style="float: left;">
                        <img alt="" src="Images/admin-icon-divider.png" />
                    </div>
                    <div id="tdBranch" runat="server" align="center" valign="bottom" style="width: 150px; float: left;">
                        <asp:LinkButton ID="lbtnBranchManagement" runat="server"
                            PostBackUrl="~/BranchManagement.aspx" OnClick="lbtnBranchManagement_Click">
                            <div>
                                <asp:Image ID="Image2" runat="server" AlternateText="Branch Management" ImageUrl="~/Images/branch-manage-icon.png"
                                    ToolTip="Branch Management" meta:resourcekey="Image2Resource1" />
                            </div>
                            <asp:Localize Text="Branch Management" runat="server"
                                ID="locBranchManagement" meta:resourcekey="locBranchManagementResource1"></asp:Localize>
                        </asp:LinkButton>

                    </div>
                    <div id="divUserSeprator" runat="server" align="center" valign="bottom" style="float: left;">
                        <img alt="" src="Images/admin-icon-divider.png" />
                    </div>
                    <div id="tdUser" runat="server" align="center" valign="bottom" style="width: 150px; float: left;">
                        <asp:LinkButton ID="lbtnUserManagement" runat="server"
                            PostBackUrl="~/UserManagement.aspx"
                            OnClick="lbtnUserManagement_Click">
                            <div>
                                <asp:Image ID="Image3" runat="server" AlternateText="User Management" ImageUrl="~/Images/user-manage-icon.png"
                                    ToolTip="User Management" meta:resourcekey="Image3Resource1" />
                            </div>
                            <asp:Localize Text="User Management" runat="server" ID="locUserMgmt"
                                meta:resourcekey="locUserMgmtResource1"></asp:Localize>
                        </asp:LinkButton>
                    </div>
                    <div id="divProfileManagementSeprator" runat="server" align="center" valign="bottom" style="float: left;">
                        <img alt="" src="Images/admin-icon-divider.png" />
                    </div>
                    <div id="tdProfileManagement" runat="server" align="center" valign="bottom" style="width: 150px; float: left;">
                        <asp:LinkButton ID="lnkProfileMgmt" runat="server"
                            PostBackUrl="~/ProfileManagement.aspx" OnClick="lnkProfileMgmt_Click">
                            <div>
                                <asp:Image ID="Image4" runat="server" AlternateText="My Settings"
                                    ImageUrl="~/Images/Profile-Management.png" ToolTip="My Settings"
                                    meta:resourcekey="Image4Resource1" />
                            </div>
                            <asp:Localize Text="My Settings" runat="server" ID="locMySettings"
                                meta:resourcekey="locMySettingsResource1"></asp:Localize>

                        </asp:LinkButton>
                    </div>
                    <div id="divLicenseSeprator" runat="server" align="center" valign="bottom" style="float: left;">
                        <img alt="" src="Images/admin-icon-divider.png" />
                    </div>
                    <div id="tdLicense" runat="server" align="center" valign="bottom" style="width: 150px; float: left;">
                        <asp:LinkButton ID="lbtnLicenseManagement" runat="server"
                            OnClick="lbtnLicenseManagement_Click">
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
                <td style="height: 50px;"></td>
            </tr>
            <tr align="center">
                <td align="center">
                    <div id="tdDevice" runat="server" align="center" valign="bottom" style="width: 150px; float: left;">
                        <asp:LinkButton ID="lbtnDeviceManagement" runat="server"
                            PostBackUrl="~/DeviceManagement.aspx" OnClick="lbtnDeviceManagement_Click">
                            <div>
                                <asp:Image ID="imgDeviceManagement" runat="server" AlternateText="Device Management"
                                    ImageUrl="~/Images/device-manage-icon.png" ToolTip="Device Management"
                                    meta:resourcekey="imgDeviceManagementResource1" />
                            </div>
                            <asp:Localize Text="Device Management" runat="server" ID="locDeviceMgmt"
                                meta:resourcekey="locDeviceMgmtResource1"></asp:Localize>
                        </asp:LinkButton>
                    </div>
                    <div id="divMeasurementSeprator" runat="server" align="center" valign="bottom" style="float: left;">
                        <img alt="" src="Images/admin-icon-divider.png" />
                    </div>


                    <div id="tdMeasurementDevice" runat="server" align="center" valign="bottom" style="width: 150px; float: left;">
                        <asp:LinkButton ID="lnkMeasurementDevice" runat="server"
                            PostBackUrl="~/MeasurementDeviceList.aspx"
                            OnClick="lnkMeasurementDevice_Click">
                            <div>
                                <asp:Image ID="imgMeasurementDevice" runat="server" AlternateText="Measurement Device"
                                    ImageUrl="~/Images/syncmeasurementdevice.png" ToolTip="Measurement Device"
                                    meta:resourcekey="imgMeasurementDeviceResource1" />
                            </div>
                            <asp:Localize Text="Measurement Device" runat="server"
                                ID="locMeasurementDevice" meta:resourcekey="locMeasurementDeviceResource1"></asp:Localize>

                        </asp:LinkButton>
                    </div>
                    <div id="divDownloadSeprator" runat="server" align="center" valign="bottom" style="float: left;">
                        <img alt="" src="Images/admin-icon-divider.png" />
                    </div>
                    <div id="tdDownload" runat="server" align="center" valign="bottom" style="width: 150px; float: left;">
                        <asp:LinkButton ID="lnkDownload" runat="server"
                            PostBackUrl="~/DownloadSoftwares.aspx" OnClick="lnkDownload_Click">
                            <div>
                                <asp:Image ID="Image5" runat="server" AlternateText="Download"
                                    ImageUrl="~/Images/download-icon.png" ToolTip="Download"
                                    meta:resourcekey="Image5Resource1" />
                            </div>
                            <asp:Localize Text="Download" runat="server" ID="locDownload"
                                meta:resourcekey="locDownloadResource1"></asp:Localize>

                        </asp:LinkButton>
                    </div>
                    <div id="divRefChartSeparator" runat="server" align="center" valign="bottom" style="float: left;">
                        <img alt="" src="Images/admin-icon-divider.png" />
                    </div>
                    <div id="tdReferenceChart" runat="server" align="center" valign="bottom" style="width: 150px; float: left;">
                        <asp:LinkButton ID="lnkRefChart" runat="server"
                            PostBackUrl="~/ReferenceChartManagement.aspx" OnClick="lnkRefChart_Click">

                            <div>
                                <asp:Image ID="imgRefChart" runat="server" AlternateText="Reference Chart"
                                    ImageUrl="~/Images/Reference-Chart.jpg" ToolTip="Reference Chart"
                                    meta:resourcekey="imgRefChartResource1" />
                            </div>
                            <asp:Localize runat="server" Text="Reference Chart" ID="locReferenceChart"
                                meta:resourcekey="locReferenceChartResource1"></asp:Localize>

                        </asp:LinkButton>
                    </div>
                    <div id="divSourceProfileSeprator" runat="server" align="center" valign="bottom" style="float: left;">
                        <img alt="" src="Images/admin-icon-divider.png" />
                    </div>
                    <div id="tdSourceProfile" runat="server" align="center" valign="bottom" style="width: 150px; float: left;">
                        <asp:LinkButton ID="lbtnSourceProfile" runat="server"
                            PostBackUrl="~/SourceProfileManagement.aspx"
                            OnClick="lbtnSourceProfile_Click">
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
                <td style="height: 50px;"></td>
            </tr>
            <tr align="center">
                <td align="center">
                    <div id="dvActivityValidationRules" runat="server" align="center" valign="bottom" style="width: 150px; float: left;">
                        <asp:LinkButton ID="lnkBtnActivityValidationRules" runat="server"
                            PostBackUrl="~/ActivityValidationRules.aspx">
                            <div>
                                <asp:Image ID="imgActivityValidationRules" runat="server" AlternateText="Activity Validation Rules"
                                    ImageUrl="~/Images/activity-validation-rules-icon.png" ToolTip="Activity Validation Rules" meta:resourcekey="imgActivityValidationRulesResource1" />
                            </div>
                            <asp:Localize Text="Activity Validation Rules" runat="server" ID="locActivityValidationRules" meta:resourcekey="locActivityValidationRulesResource1"></asp:Localize>
                        </asp:LinkButton>
                    </div>
                    <div id="divProfilerSettingSeparator" runat="server" align="center" valign="bottom" style="float: left;">
                        <img alt="" src="Images/admin-icon-divider.png" />
                    </div>
                    <div id="dvProfilerSettings" runat="server" align="center" valign="bottom" style="width: 150px; float: left;">
                        <asp:LinkButton ID="lnkProfilerSettings" runat="server"
                            PostBackUrl="~/AddDeviceProfilerSettings.aspx" Text="Device Family Profiler Settings">
                            <div>
                                <asp:Image ID="Image_ProfilerSettings" runat="server" AlternateText="Device Family Profiler Settings"
                                    ImageUrl="~/Images/Profiler-Settings.jpg" ToolTip="Device Family Profiler Settings"
                                    meta:resourcekey="imgProfilerSettingsResource1" />
                            </div>
                            <asp:Localize runat="server" Text="Device Family Profiler Settings" ID="locProfilerSettings"
                                meta:resourcekey="locProfilerSettingsResource1"></asp:Localize>
                        </asp:LinkButton>
                    </div>
                    <div id="divApplicationConfigurationSeparator" runat="server" align="center" valign="bottom" style="float: left;">
                        <img alt="" src="Images/admin-icon-divider.png" />
                    </div>
                    <div id="dvApplicationConfiguration" runat="server" align="center" valign="bottom" style="width: 150px; float: left">
                        <asp:LinkButton ID="lnkbtnApplicationConfiguration" runat="server" PostBackUrl="~/ApplicationConfiguration.aspx">
                            <div>
                                <asp:Image ID="imgApplicationConfig" runat="server" AlternateText="Application Configuration" ImageUrl="~/Images/application-configuration-icon.png" ToolTip="Application Configuration" meta:resourcekey="imgApplicationConfigResource1" />
                            </div>
                            <asp:Localize Text="Application Configuration" runat="server" ID="locApplicationConfiguration" meta:resourcekey="locApplicationConfigurationResource1"></asp:Localize>
                        </asp:LinkButton>
                    </div>
                    <div id="divMetricTargetPresetSeparator" runat="server" align="center" valign="bottom" style="float: left;">
                        <img alt="" src="Images/admin-icon-divider.png" />
                    </div>
                    <div id="divMetricTargetPreset" runat="server" align="center" valign="bottom" style="width: 150px; float: left">
                        <asp:LinkButton ID="lnkbtnMetricTargetPreset" runat="server"
                            PostBackUrl="~/MetricTargetPresetManagement.aspx" OnClick="lnkbtnMetricTargetPreset_Click">
                            <div>
                                <asp:Image ID="Image7" runat="server" AlternateText="Metric Target Preset" ImageUrl="~/Images/application-configuration-icon.png" ToolTip="Metric Target Preset" meta:resourcekey="imgMetricTargetPresetResource1" />
                            </div>
                            <asp:Localize Text="Metric Target Preset" runat="server" ID="locMetricTargetPreset" meta:resourcekey="locMetricTargetPresetResource1"></asp:Localize>
                        </asp:LinkButton>
                    </div>
                    <div id="divPatchPresetSeparator" runat="server" align="center" valign="bottom" style="float: left;">
                        <img alt="" src="Images/admin-icon-divider.png" />
                    </div>
                    <div id="divPatchPreset" runat="server" align="center" valign="bottom" style="width: 150px; float: left">
                        <asp:LinkButton ID="lnkbtnPatchPreset" runat="server"
                            PostBackUrl="~/PatchPresetManagement.aspx" OnClick="lnkbtnPatchPreset_Click">
                            <div>
                                <asp:Image ID="Image9" runat="server" AlternateText="Patch Preset" ImageUrl="~/Images/application-configuration-icon.png" ToolTip="Patch Preset" meta:resourcekey="imgPatchPresetResource1" />
                            </div>
                            <asp:Localize Text="Patch Preset" runat="server" ID="Localize1" meta:resourcekey="locPatchPresetResource1"></asp:Localize>
                        </asp:LinkButton>
                    </div>


                </td>
            </tr>
            <tr>
                <td style="height: 50px;"></td>
            </tr>
            <tr align="center">
                <td align="center">
                    <div id="divPrintStandardType" runat="server" align="center" valign="bottom" style="width: 150px; float: left">
                        <asp:LinkButton ID="lnkbtnPrintStandardType" runat="server"
                            ToolTip="Print Standard Type"
                            PostBackUrl="~/PrintStandardTypeManagement.aspx"
                            OnClick="lnkbtnPrintStandardType_Click">
                            <div>
                                <asp:Image ID="Image8" runat="server" AlternateText="Print Standard Type" ImageUrl="~/Images/application-configuration-icon.png" ToolTip="Print Standard Type" />
                            </div>
                            <asp:Localize Text="Print Standard Type" runat="server" ID="Localize2"></asp:Localize>
                        </asp:LinkButton>
                    </div>
                    <div id="divLookupTVILABDensitySeparator" runat="server" align="center" valign="bottom" style="float: left;">
                        <img alt="" src="Images/admin-icon-divider.png" />
                    </div>
                    <div id="divLookupTVILABDensity" runat="server" align="center" valign="bottom" style="width: 150px; float: left">
                        <asp:LinkButton ID="LinkButton1" runat="server"
                            ToolTip="Add Lookup TVILABDensity"
                            PostBackUrl="~/AddLookupTVILABDensity.aspx">
                            <div>
                                <asp:Image ID="Image6" runat="server" AlternateText="AddLookupTVILABDensity" ImageUrl="~/Images/application-configuration-icon.png" ToolTip="Add Lookup TVILABDensity" />
                            </div>
                            <asp:Localize Text="Sync1D LAB TVI Density Values" runat="server" ID="Localize3"></asp:Localize>
                        </asp:LinkButton>
                    </div>
                    <div id="divSpotTargetSeparator" runat="server" align="center" valign="bottom" style="float: left;">
                        <img alt="" src="Images/admin-icon-divider.png" />
                    </div>
                    <div id="divSpotTarget" runat="server" align="center" valign="bottom" style="width: 150px; float: left;">
                        <asp:LinkButton ID="lnkSpotTarget" runat="server"
                            PostBackUrl="~/SpotTargetManagement.aspx" OnClick="lnkSpotTarget_Click">
                            <div>
                                <asp:Image ID="Image10" runat="server" AlternateText="Spot Target"
                                    ImageUrl="~/Images/Reference-Chart.jpg" ToolTip="Spot Target"
                                    meta:resourcekey="imgSpotTargetResource1" />
                            </div>
                            <asp:Localize runat="server" Text="Spot Target" ID="Localize4"
                                meta:resourcekey="locSpotTargetResource1"></asp:Localize>
                        </asp:LinkButton>
                    </div>
                    <div id="divMetricTargetSeperator" runat="server" align="center" valign="bottom" style="float: left;">
                        <img alt="" src="Images/admin-icon-divider.png" />
                    </div>
                    <div id="divMetricTarget" runat="server" align="center" valign="bottom" style="width: 150px; float: left;">
                        <asp:LinkButton ID="lnkMetricTarget" runat="server"
                            PostBackUrl="~/MetricTargetManagement.aspx">
                            <div>
                                <asp:Image ID="Image11" runat="server" AlternateText="Spot Target"
                                    ImageUrl="~/Images/application-configuration-icon.png" ToolTip="Metric Target"
                                    meta:resourcekey="imgMetricTargetResource1" />
                            </div>
                            <asp:Localize runat="server" Text="Metric Target" ID="Localize5"
                                meta:resourcekey="locMetricTargetResource1"></asp:Localize>
                        </asp:LinkButton>
                    </div>
                    <div id="divMeasurementLogSeperator" runat="server" align="center" valign="bottom" style="float: left;">
                        <img alt="" src="Images/admin-icon-divider.png" />
                    </div>
                    <div id="divMeasurementLog" runat="server" align="center" valign="bottom" style="width: 150px; float: left;">
                        <asp:LinkButton ID="lnkMeasurementLog" runat="server"
                            PostBackUrl="~/MeasurementLog.aspx">
                            <div>
                                <asp:Image ID="ImgMeasurementLog" runat="server" AlternateText="Measurement Log"
                                    ImageUrl="~/Images/application-configuration-icon.png" ToolTip="Measurement Log"
                                    meta:resourcekey="imgMeasurementLogResource1" />
                            </div>
                            <asp:Localize runat="server" Text="Measurement Log" ID="Localize6"
                                meta:resourcekey="locMeasurementLogResource1"></asp:Localize>
                        </asp:LinkButton>
                    </div>


                </td>
            </tr>

            <tr>
                <td style="height: 50px;"></td>
            </tr>
            <tr style="vertical-align: central;">
                <td style="vertical-align: central;">
                    <div id="divMToolVersion" runat="server" align="center" valign="bottom" style="width: 150px; float: left;">
                        <asp:LinkButton ID="lnkMToolVersion" runat="server"
                            PostBackUrl="~/MToolVersionManagement.aspx">
                            <div>
                                <asp:Image ID="ImgMToolVersion" runat="server" AlternateText="MTool Version"
                                    ImageUrl="~/Images/application-configuration-icon.png" ToolTip="MTool Version" />
                            </div>
                            <asp:Localize runat="server" Text="MTool Version" ID="Localize7"></asp:Localize>
                        </asp:LinkButton>
                    </div>
                    <div id="divUpdateChangeLogSeperator" runat="server" align="center" valign="bottom" style="float: left;">
                        <img alt="" src="Images/admin-icon-divider.png" />
                    </div>
                    <div id="divUpdateChangeLog" runat="server" align="center" valign="bottom" style="width: 150px; float: left;">
                        <asp:LinkButton ID="lnkUpdateChangeLog" runat="server"
                            PostBackUrl="~/UpdateChangeLogManagement.aspx">
                            <div>
                                <asp:Image ID="ImgUpdateChangeLog" runat="server" AlternateText="Update Change Log" ImageUrl="~/Images/application-configuration-icon.png" ToolTip="Update Change Log" />
                            </div>
                            <asp:Localize runat="server" Text="Update Change Log" ID="Localize8"></asp:Localize>
                        </asp:LinkButton>
                    </div>

                    <div id="divAssignChartWithMTPSeperator" runat="server" align="center" valign="bottom" style="float: left;">
                        <img alt="" src="Images/admin-icon-divider.png" />
                    </div>
                    <div id="divAssignChartWithMTP" runat="server" align="center" valign="bottom" style="width: 150px; float: left;">
                        <asp:LinkButton ID="lnkAssignChartWithMTP" runat="server"
                            PostBackUrl="~/AssignRefChartWMetricTargetPreset.aspx">
                            <div>
                                <asp:Image ID="ImgAssignChartWithMTP" runat="server" AlternateText="Assign Chart with MTP" ImageUrl="~/Images/application-configuration-icon.png" ToolTip="Assign Chart with MTP" />
                            </div>
                            <asp:Localize runat="server" Text="Assign Chart with MTP" ID="Localize9"></asp:Localize>
                        </asp:LinkButton>
                    </div>

                    <div id="divAddCustomBcoTargetSeperator" runat="server" align="center" valign="bottom" style="float: left;">
                        <img alt="" src="Images/admin-icon-divider.png" />
                    </div>
                    <div id="divAddCustomBcoTarget" runat="server" align="center" valign="bottom" style="width: 150px; float: left;">
                        <asp:LinkButton ID="lnkAddCustomBcoTarget" runat="server"
                            PostBackUrl="~/CustomBcoManagement.aspx">
                            <div>
                                <asp:Image ID="ImgAddCustomBcoTarget" runat="server" AlternateText="Custom BCO Target Library" ImageUrl="~/Images/custom_bco_icon.png" ToolTip="Custom BCO Target Library" />
                            </div>
                            <asp:Localize runat="server" Text="Custom BCO Target Library" ID="Localize10"></asp:Localize>
                        </asp:LinkButton>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="height: 50px;"></td>
            </tr>

        </table>--%>
    <%--</center>--%>
</div>
<div class="admin_bottom">
    &nbsp;
</div>
