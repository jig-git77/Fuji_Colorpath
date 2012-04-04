<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminMenuBar.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.AdminMenuBar" %>
    
<link href="CSS/MenuBar.css?version=4" rel="stylesheet" type="text/css" />
<script src="JS/MenuBar.js?version=4" type="text/javascript"></script>
<script src="JS/jquery.js?version=4" type="text/javascript"></script>

  
<script type="text/javascript">
    $(document).ready(
        function () {
            $('#dock').Fisheye(
				{
				    maxWidth: 30,
				    items: 'a',
				    itemsText: 'span',
				    container: '.dock-container',
				    itemWidth: 24,
				    proximity: 100,
				    halign: 'center',

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
          onclick="lbtnCompany_Click" >
            <img src="Images/company-manage-icon1.png" style="position:relative;z-index:1; " alt="Company"></img>
            <span> Company </span>
      </asp:LinkButton>
      <asp:LinkButton ID="lbtnBranch" runat="server" 
          CssClass="dock-item overlapobject" PostBackUrl="~/BranchManagement.aspx" 
          onclick="lbtnBranch_Click">
            <img src="Images/branch-management.png" style="position:relative;z-index:1; " alt="Branch" />
            <span> Branch </span>
      </asp:LinkButton>
      <asp:LinkButton ID="lbtnUser" runat="server" CssClass="dock-item overlapobject" 
          PostBackUrl="~/UserManagement.aspx" onclick="lbtnUser_Click">
            <img src="Images/user-management.png" style="position:relative;z-index:1; " alt="User" />
            <span> User </span>
      </asp:LinkButton>
      <asp:LinkButton ID="lbtnSourceProfile" runat="server" 
          CssClass="dock-item overlapobject" onclick="lbtnSourceProfile_Click" >
            <img src="Images/sourceprofile-management.png" style="position:relative;z-index:1; " alt="Source Profile" />
            <span> Source Profile </span>
      </asp:LinkButton>
      <asp:LinkButton ID="lbtnDevice" runat="server" 
          CssClass="dock-item overlapobject" PostBackUrl="~/DeviceManagement.aspx" 
          onclick="lbtnDevice_Click">
            <img src="Images/device-management.png" style="position:relative;z-index:1; " alt="Device" />
            <span> Device </span>
      </asp:LinkButton>
      <asp:LinkButton ID="ltnLicense" runat="server" 
          CssClass="dock-item overlapobject" onclick="ltnLicense_Click">
            <img src="Images/license-management.png" style="position:relative;z-index:1; " alt="License" />
            <span> License </span>
      </asp:LinkButton>
      <asp:LinkButton ID="lbtnMeasurementDevice" runat="server" 
          CssClass="dock-item overlapobject" onclick="lbtnMeasurementDevice_Click">
            <img src="Images/syncmeasurementdevice-management.png" style="position:relative;z-index:1; " alt="Measurement Device" />
            <span> Measurement Device </span>
      </asp:LinkButton>
  </div> 
</div>
</div>

