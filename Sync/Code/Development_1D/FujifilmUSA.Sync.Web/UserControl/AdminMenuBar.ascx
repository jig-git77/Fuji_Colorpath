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
			) }
	);
</script>

<div style="margin:0px 25px 0px 25px">
<!--top dock -->
<div class="dock" id="dock" >
  <div class="dock-container" style="width: 300px">
      <asp:LinkButton ID="lbtnCompany" runat="server" 
          CssClass="dock-item overlapobject" PostBackUrl="~/CompanyManagement.aspx" 
          onclick="lbtnCompany_Click">
            <img src="Images/company-management.png" style="position:relative;z-index:1; " alt="Company"></img>
            <span > <asp:Localize Text="Company" ID="locCompany" runat="server" 
          meta:resourcekey="locCompanyResource1"></asp:Localize> 
 </span>
      </asp:LinkButton>
      <asp:LinkButton ID="lbtnBranch" runat="server" 
          CssClass="dock-item overlapobject" PostBackUrl="~/BranchManagement.aspx" 
          onclick="lbtnBranch_Click">
            <img src="Images/branch-management.png" style="position:relative;z-index:1; " alt="Branch" />
            <span> <asp:Localize Text="Branch" runat="server" ID="locBranch" 
          meta:resourcekey="locBranchResource1"></asp:Localize> 
 </span>
      </asp:LinkButton>
      <asp:LinkButton ID="lbtnUser" runat="server" CssClass="dock-item overlapobject" 
          PostBackUrl="~/UserManagement.aspx" onclick="lbtnUser_Click">
            <img src="Images/user-management.png" style="position:relative;z-index:1; " alt="User" />
            <span> <asp:localize runat="server" Text="User" ID="locUser" 
          meta:resourcekey="locUserResource1"></asp:localize> 
 </span>
      </asp:LinkButton>
      <asp:LinkButton ID="lbtProfileMgmt" runat="server" 
           CssClass="dock-item overlapobject" onclick="lbtProfileMgmt_Click">
            <img src="Images/Profile-Management-icon.png" style="position:relative;z-index:1; " alt="My Settings" />
            <span> <asp:Localize Text="My Settings" runat="server" ID="locMySettings" 
          meta:resourcekey="locMySettingsResource1"></asp:Localize> 
 </span>
      </asp:LinkButton>
      <asp:LinkButton ID="ltnLicense" runat="server" 
          CssClass="dock-item overlapobject" onclick="ltnLicense_Click">
            <img src="Images/license-management.png" style="position:relative;z-index:1; " alt="License" />
            <span> <asp:Localize Text="License" runat="server" ID="locLicense" 
          meta:resourcekey="locLicenseResource1"></asp:Localize> 
 </span>
      </asp:LinkButton>      
      <asp:LinkButton ID="lbtnDevice" runat="server" 
          CssClass="dock-item overlapobject" PostBackUrl="~/DeviceManagement.aspx" 
          onclick="lbtnDevice_Click">
            <img src="Images/device-management.png" style="position:relative;z-index:1; " alt="Device" />
            <span> <asp:Localize Text="Device" runat="server" ID="locDevice" 
          meta:resourcekey="locDeviceResource1"></asp:Localize> 
 </span>
      </asp:LinkButton>      
      <asp:LinkButton ID="lbtnMeasurementDevice" runat="server" 
          CssClass="dock-item overlapobject" onclick="lbtnMeasurementDevice_Click">
            <img src="Images/syncmeasurementdevice-management.png" style="position:relative;z-index:1; " alt="Measurement Device" />
            <span> <asp:Localize Text="Measurement Device" runat="server" 
          ID="locMeasurementDevice" meta:resourcekey="locMeasurementDeviceResource1"></asp:Localize> 
 </span>
      </asp:LinkButton>
      <asp:LinkButton ID="lbtDownload" runat="server" 
           CssClass="dock-item overlapobject" onclick="lbtDownload_Click">
            <img src="Images/download-icon-small.png" style="position:relative;z-index:1; " alt="Download" />
            <span> <asp:Localize Text="Download" runat="server" ID="locDownload" 
          meta:resourcekey="locDownloadResource1"></asp:Localize> 
 </span>
      </asp:LinkButton>
      <asp:LinkButton ID="lbtnRefChart" runat="server" 
           CssClass="dock-item overlapobject" onclick="lbtnRefChart_Click">
            <img src="Images/Reference-Chart-management.jpg" style="position:relative;z-index:1; " alt="Reference Chart" />
            <span> <asp:Localize runat="server" ID="locReferenceChart" 
          Text="Reference Chart" meta:resourcekey="locReferenceChartResource1"></asp:Localize> 
        </span>
      </asp:LinkButton>
      <asp:LinkButton ID="lbtnSourceProfile" runat="server" 
          CssClass="dock-item overlapobject" onclick="lbtnSourceProfile_Click">
            <img src="Images/sourceprofile-management.png" style="position:relative;z-index:1; " alt="Source Profile" />
            <span> <asp:Localize Text="Source Profile" runat="server" 
          ID="locSourceProfile" meta:resourcekey="locSourceProfileResource1"></asp:Localize> 
        </span>
      </asp:LinkButton>   
      <asp:LinkButton ID="lbtnActivityValidationRules" runat="server" 
          CssClass="dock-item overlapobject" onclick="lbtnActivityValidationRules_Click">
            <img src="Images/activity-validation-rules.png" style="position:relative;z-index:1; " alt="Activity Validation Rules" />
            <span> <asp:Localize Text="Activity Validation Rules" runat="server" 
          ID="locActivityValidationRules" meta:resourcekey="locActivityValidationRulesResource1"></asp:Localize> 
        </span>
      </asp:LinkButton>      
       <asp:LinkButton ID="lbtnProfilerSettings" runat="server" 
          CssClass="dock-item overlapobject" onclick="lbtnProfilerSettings_Click">
            <img src="Images/Profiler-Settings-management.jpg" style="position:relative;z-index:1; " alt="Device Family Profiler Settings" />
            <span> <asp:Localize Text="Device Family Profiler Settings" runat="server" 
          ID="locProfilerSettings" meta:resourcekey="locProfilerSettingsResource1"></asp:Localize> 
            </span>
      </asp:LinkButton>   
       <asp:LinkButton ID="lbtnApplicationConfiguration" runat="server" 
          CssClass="dock-item overlapobject" onclick="lbtnApplicationConfiguration_Click">
            <img src="Images/application-configuration.png" style="position:relative;z-index:1; " alt="Application Configuration" />
            <span> <asp:Localize Text="Application Configuration" runat="server" 
          ID="locApplicationConfiguration" meta:resourcekey="locApplicationConfigurationResource1"></asp:Localize> 
            </span>
      </asp:LinkButton>  
       <asp:LinkButton ID="lbtnMetricTargetPreset" runat="server" 
          CssClass="dock-item overlapobject" onclick="lbtnMetricTargetPreset_Click" >
            <img src="Images/application-configuration.png" style="position:relative;z-index:1; " alt="Metric Target Preset" />
            <span> <asp:Localize Text="Metric Target Preset" runat="server" 
          ID="Localize1"></asp:Localize> 
            </span>
      </asp:LinkButton> 
      <asp:LinkButton ID="lbtnPrintStandardType" runat="server" 
          CssClass="dock-item overlapobject" onclick="lbtnPrintStandardType_Click" >
            <img src="Images/application-configuration.png" style="position:relative;z-index:1; " alt="Print Standard Type" />
            <span> <asp:Localize Text="Print Standard Type" runat="server" 
          ID="Localize2"></asp:Localize> 
            </span>
      </asp:LinkButton>
      <asp:LinkButton ID="lnknPatchTolerance" runat="server" 
          CssClass="dock-item overlapobject" onclick="lnkbtnPatchTolerance_Click">
            <img src="Images/application-configuration.png" style="position:relative;z-index:1; " alt="Patch Tolerance" />
            <span> <asp:Localize Text="Patch Tolerance" runat="server" 
          ID="Localize3"></asp:Localize> 
            </span>
      </asp:LinkButton>
  </div> 
</div>
</div>

