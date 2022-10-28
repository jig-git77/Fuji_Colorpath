<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="AdminMenuBar.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.AdminMenuBar" %>

<script src="JS/jquery.js?version=4" type="text/javascript"></script>
<link href="CSS/MenuBar.css?version=4" rel="stylesheet" type="text/css" />
<script src="JS/MenuBar.js?version=4" type="text/javascript"></script>


<script type="text/javascript">
    $(document).ready(
        function () {
            $('#dock').Fisheye(
                {
                    maxWidth: 24,
                    items: 'a',
                    itemsText: 'span',
                    container: '.dock-container',
                    itemWidth: 20,
                    proximity: 100,
                    halign: 'center',
                    valign: 'center',
                }
            )
        }
    );
</script>

<div style="margin: 0px 25px 0px 25px">
    <!--top dock -->
    <div class="dock" id="dock">
        <div class="dock-container" style="width: 300px">
            <asp:LinkButton ID="lbtnCompany" runat="server"
                CssClass="dock-item overlapobject" PostBackUrl="~/CompanyManagement.aspx"
                OnClick="lbtnCompany_Click">
                <img src="Images/company-management.png" style="position: relative; z-index: 1;" alt="Company"></img>
                <span>
                    <asp:Localize Text="Company" ID="locCompany" runat="server"
                        meta:resourcekey="locCompanyResource1"></asp:Localize>
                </span>
            </asp:LinkButton>
            <asp:LinkButton ID="lbtnBranch" runat="server"
                CssClass="dock-item overlapobject" PostBackUrl="~/BranchManagement.aspx"
                OnClick="lbtnBranch_Click">
                <img src="Images/branch-management.png" style="position: relative; z-index: 1;" alt="Branch" />
                <span>
                    <asp:Localize Text="Branch" runat="server" ID="locBranch"
                        meta:resourcekey="locBranchResource1"></asp:Localize>
                </span>
            </asp:LinkButton>
            <asp:LinkButton ID="lbtnUser" runat="server" CssClass="dock-item overlapobject"
                PostBackUrl="~/UserManagement.aspx" OnClick="lbtnUser_Click">
                <img src="Images/user-management.png" style="position: relative; z-index: 1;" alt="User" />
                <span>
                    <asp:Localize runat="server" Text="User" ID="locUser"
                        meta:resourcekey="locUserResource1"></asp:Localize>
                </span>
            </asp:LinkButton>
            <asp:LinkButton ID="lbtProfileMgmt" runat="server"
                CssClass="dock-item overlapobject" OnClick="lbtProfileMgmt_Click">
                <img src="Images/Profile-Management-icon.png" style="position: relative; z-index: 1;" alt="My Settings" />
                <span>
                    <asp:Localize Text="My Settings" runat="server" ID="locMySettings"
                        meta:resourcekey="locMySettingsResource1"></asp:Localize>
                </span>
            </asp:LinkButton>
            <asp:LinkButton ID="ltnLicense" runat="server"
                CssClass="dock-item overlapobject" OnClick="ltnLicense_Click">
                <img src="Images/license-management.png" style="position: relative; z-index: 1;" alt="License" />
                <span>
                    <asp:Localize Text="License" runat="server" ID="locLicense"
                        meta:resourcekey="locLicenseResource1"></asp:Localize>
                </span>
            </asp:LinkButton>
            <asp:LinkButton ID="lbtnDevice" runat="server"
                CssClass="dock-item overlapobject" PostBackUrl="~/DeviceManagement.aspx"
                OnClick="lbtnDevice_Click">
                <img src="Images/device-management.png" style="position: relative; z-index: 1;" alt="Device" />
                <span>
                    <asp:Localize Text="Device" runat="server" ID="locDevice"
                        meta:resourcekey="locDeviceResource1"></asp:Localize>
                </span>
            </asp:LinkButton>
            <asp:LinkButton ID="lbtnMeasurementDevice" runat="server"
                CssClass="dock-item overlapobject" OnClick="lbtnMeasurementDevice_Click">
                <img src="Images/syncmeasurementdevice-management.png" style="position: relative; z-index: 1;" alt="Measurement Device" />
                <span>
                    <asp:Localize Text="Measurement Device" runat="server"
                        ID="locMeasurementDevice" meta:resourcekey="locMeasurementDeviceResource1"></asp:Localize>
                </span>
            </asp:LinkButton>
            <asp:LinkButton ID="lbtDownload" runat="server"
                CssClass="dock-item overlapobject" OnClick="lbtDownload_Click">
                <img src="Images/download-icon-small.png" style="position: relative; z-index: 1;" alt="Download" />
                <span>
                    <asp:Localize Text="Download" runat="server" ID="locDownload"
                        meta:resourcekey="locDownloadResource1"></asp:Localize>
                </span>
            </asp:LinkButton>
            <asp:LinkButton ID="lbtnRefChart" runat="server"
                CssClass="dock-item overlapobject" OnClick="lbtnRefChart_Click">
                <img src="Images/Reference-Chart-management.jpg" style="position: relative; z-index: 1;" alt="Reference Chart" />
                <span>
                    <asp:Localize runat="server" ID="locReferenceChart"
                        Text="Reference Chart" meta:resourcekey="locReferenceChartResource1"></asp:Localize>
                </span>
            </asp:LinkButton>
            <asp:LinkButton ID="lbtnSourceProfile" runat="server"
                CssClass="dock-item overlapobject" OnClick="lbtnSourceProfile_Click">
                <img src="Images/sourceprofile-management.png" style="position: relative; z-index: 1;" alt="Source Profile" />
                <span>
                    <asp:Localize Text="Source Profile" runat="server"
                        ID="locSourceProfile" meta:resourcekey="locSourceProfileResource1"></asp:Localize>
                </span>
            </asp:LinkButton>
            <asp:LinkButton ID="lbtnActivityValidationRules" runat="server"
                CssClass="dock-item overlapobject" OnClick="lbtnActivityValidationRules_Click">
                <img src="Images/activity-validation-rules.png" style="position: relative; z-index: 1;" alt="Activity Validation Rules" />
                <span>
                    <asp:Localize Text="Activity Validation Rules" runat="server"
                        ID="locActivityValidationRules" meta:resourcekey="locActivityValidationRulesResource1"></asp:Localize>
                </span>
            </asp:LinkButton>
            <asp:LinkButton ID="lbtnProfilerSettings" runat="server"
                CssClass="dock-item overlapobject" OnClick="lbtnProfilerSettings_Click">
                <img src="Images/Profiler-Settings-management.jpg" style="position: relative; z-index: 1;" alt="Device Family Profiler Settings" />
                <span>
                    <asp:Localize Text="Device Family Profiler Settings" runat="server"
                        ID="locProfilerSettings" meta:resourcekey="locProfilerSettingsResource1"></asp:Localize>
                </span>
            </asp:LinkButton>
            <asp:LinkButton ID="lbtnApplicationConfiguration" runat="server"
                CssClass="dock-item overlapobject" OnClick="lbtnApplicationConfiguration_Click">
                <img src="Images/application-configuration.png" style="position: relative; z-index: 1;" alt="Application Configuration" />
                <span>
                    <asp:Localize Text="Application Configuration" runat="server"
                        ID="locApplicationConfiguration" meta:resourcekey="locApplicationConfigurationResource1"></asp:Localize>
                </span>
            </asp:LinkButton>
            <asp:LinkButton ID="lbtnMetricTargetPreset" runat="server"
                CssClass="dock-item overlapobject" OnClick="lbtnMetricTargetPreset_Click">
                <img src="Images/application-configuration.png" style="position: relative; z-index: 1;" alt="Metric Target Preset" />
                <span>
                    <asp:Localize Text="Metric Target Preset" runat="server"
                        ID="locMetricTargetPreset" meta:resourcekey="locMetricTargetPresetResource1"></asp:Localize>
                </span>
            </asp:LinkButton>
            <asp:LinkButton ID="lbtnPatchPreset" runat="server"
                CssClass="dock-item overlapobject" OnClick="lbtnPatchPreset_Click">
                <img src="Images/application-configuration.png" style="position: relative; z-index: 1;" alt="Patch Preset" />
                <span>
                    <asp:Localize Text="Patch Preset" runat="server"
                        ID="locPatchPreset" meta:resourcekey="locPatchPresetResource1"></asp:Localize>
                </span>
            </asp:LinkButton>
            <asp:LinkButton ID="lbtnPrintStandardType" runat="server"
                CssClass="dock-item overlapobject" OnClick="lbtnPrintStandardType_Click">
                <img src="Images/application-configuration.png" style="position: relative; z-index: 1;" alt="Print Standard Type" />
                <span>
                    <asp:Localize Text="Print Standard Type" runat="server"
                        ID="locPrintStandardType" meta:resourcekey="locPrintStandardTypeResource1"></asp:Localize>
                </span>
            </asp:LinkButton>
            <asp:LinkButton ID="lbtnLookupTVILABDensity" runat="server"
                CssClass="dock-item overlapobject"
                OnClick="lbtnLookupTVILABDensity_Click">
                <img src="Images/application-configuration.png" style="position: relative; z-index: 1;" alt="Add Lookup TVILABDensity" />
                <span>
                    <asp:Localize Text="Sync1D LAB TVI Density Values" runat="server"
                        ID="locLookupTVILABDensity" meta:resourcekey="locLookupTVILABDensityResource1"></asp:Localize>
                </span>
            </asp:LinkButton>
            <asp:LinkButton ID="lbtnSpotTarget" runat="server"
                CssClass="dock-item overlapobject" OnClick="lbtnSpotTarget_Click">
                <img src="Images/Reference-Chart-management.jpg" style="position: relative; z-index: 1;" alt="Spot Target" />
                <span>
                    <asp:Localize runat="server" ID="locSpotTarget"
                        Text="Spot Target" meta:resourcekey="locSpotTargetResource1"></asp:Localize>
                </span>
            </asp:LinkButton>
            <asp:LinkButton ID="lbtnMetricTarget" runat="server"
                CssClass="dock-item overlapobject"
                OnClick="lbtnMetricTarget_Click">
                <img src="Images/application-configuration.png" style="position: relative; z-index: 1;" alt="Metric Target" />
                <span>
                    <asp:Localize Text="Metric Target" runat="server"
                        ID="Localize1" meta:resourcekey="locMetricTargetResource1"></asp:Localize>
                </span>
            </asp:LinkButton>
            <asp:LinkButton ID="lbtnMToolVersion" runat="server"
                CssClass="dock-item overlapobject"
                OnClick="lbtnMToolVersion_Click">
                <img src="Images/application-configuration.png" style="position: relative; z-index: 1;" alt="Metric Target" />
                <span>
                    <asp:Localize Text="MToolVersion" runat="server"
                        ID="Localize2"></asp:Localize>
                </span>
            </asp:LinkButton>
              
            <asp:LinkButton ID="lbtnUpdateChangeLog" runat="server"
                CssClass="dock-item overlapobject"
                OnClick="lbtnUpdateChangeLog_Click">
                <img src="Images/application-configuration.png" style="position: relative; z-index: 1;" alt="Add Update Change Log" />
                <span>
                    <asp:Localize Text="Update Change Log" runat="server"
                        ID="Localize3"></asp:Localize>
                </span>
            </asp:LinkButton>

             <asp:LinkButton ID="lbtAssignChartwithMTP" runat="server"
                CssClass="dock-item overlapobject"
                OnClick="lbtAssignChartwithMTP_Click">
                <img src="Images/application-configuration.png" style="position: relative; z-index: 1;" alt="Assign Chart with MTP" />
                <span>
                    <asp:Localize Text="Assign Chart with MTP" runat="server"
                        ID="Localize4"></asp:Localize>
                </span>
            </asp:LinkButton>

             <asp:LinkButton ID="lbtCustomBCOTargetLibrary" runat="server"
                CssClass="dock-item overlapobject"
                OnClick="lbtCustomBCOTargetLibrary_Click">
                <img src="Images/custom_bco_icon.png" style="position: relative; z-index: 1;" alt="Custom BCO Target Library" />
                <span>
                    <asp:Localize Text="Custom BCO Target Library" runat="server"
                        ID="Localize5"></asp:Localize>
                </span>
            </asp:LinkButton>

        </div>
    </div>
</div>

