<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminMenuBar.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.AdminMenuBar" %>
    
<link href="CSS/MenuBar.css" rel="stylesheet" type="text/css" />
<script src="JS/MenuBar.js" type="text/javascript"></script>
<script src="JS/jquery.js" type="text/javascript"></script>

  
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
          CssClass="dock-item overlapobject" PostBackUrl="~/CompanyManagement.aspx" >
            <img src="Images/small-company-manage-icon-s.gif" style="position:relative;z-index:1; " alt="Company" />
            <span> Company </span>
      </asp:LinkButton>
      <asp:LinkButton ID="lbtnBranch" runat="server" CssClass="dock-item overlapobject" PostBackUrl="~/BranchManagement.aspx">
            <img src="Images/small-branch-manage-icon-s.gif" style="position:relative;z-index:1; " alt="Branch" />
            <span> Branch </span>
      </asp:LinkButton>
      <asp:LinkButton ID="lbtnUser" runat="server" CssClass="dock-item overlapobject" PostBackUrl="~/UserManagement.aspx">
            <img src="Images/small-user-manage-icon-s.gif" style="position:relative;z-index:1; " alt="User" />
            <span> User </span>
      </asp:LinkButton>
      <asp:LinkButton ID="lbtnSourceProfile" runat="server" CssClass="dock-item overlapobject" >
            <img src="Images/small-sourceprofile-manage-icon.gif" style="position:relative;z-index:1; " alt="Source Profile" />
            <span> Source Profile </span>
      </asp:LinkButton>
      <asp:LinkButton ID="lbtnDevice" runat="server" CssClass="dock-item overlapobject" PostBackUrl="~/DeviceManagement.aspx">
            <img src="Images/small-device-manage-icon-s.gif" style="position:relative;z-index:1; " alt="Device" />
            <span> Device </span>
      </asp:LinkButton>
      <asp:LinkButton ID="ltnLicense" runat="server" CssClass="dock-item overlapobject">
            <img src="Images/small-license-manage-icon.gif" style="position:relative;z-index:1; " alt="License" />
            <span> License </span>
      </asp:LinkButton>
      <asp:LinkButton ID="lbtnMeasurementDevice" runat="server" CssClass="dock-item overlapobject">
            <img src="Images/small-syncmeasurementdevice-icon.png" style="position:relative;z-index:1; " alt="Measurement Device" />
            <span> Measurement Device </span>
      </asp:LinkButton>
  </div> 
</div>
</div>

