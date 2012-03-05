<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddDestinationProfile.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.AddDestinationProfile" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="uc" %>
<%@ Register Src="~/UserControl/AddMeasurementCommon.ascx" TagName="AddMeasurementCommon" TagPrefix="uc" %>
<%@ Register Src="~/UserControl/DeviceTypeSelectionCommon.ascx" TagName="DeviceTypeSelectionCommon" TagPrefix="uc" %>
<%@ Import Namespace="FujifilmUSA.Sync.Common" %>
<div class="contant_top">
    &nbsp;</div>
<div class="contant_middle">
    <div class="head">
        <h1>
            Add Destination Profile</h1>
        <asp:LinkButton ID="lbtnDestinationProfileSave" 
            runat="server" 
            ValidationGroup="vgDestination" 
            CssClass="button"
            OnClick="lbtnDestinationProfileSave_Click">
            <span>
                <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/save_icon.png" />Add Measurement</span></asp:LinkButton></div>
    <div class="clear">
    </div>    
    <div class="info_form" style="margin : 0 20px 0" >
        <uc:Statusbar ID="ucStatusbar1" runat="server" Visible="false" />
    </div>
    <div id="validationSummary" class="add_form" >
        <asp:ValidationSummary 
                ID="vsSaveDestinationProfile" 
                runat="server" 
                ShowSummary="true" 
                ValidationGroup="vgDestination" 
                ForeColor="Red"
                >
         </asp:ValidationSummary>
    </div>
    <div class="add_form">
        <table border="0" cellspacing="5" cellpadding="0" class="Width100percent">
            <tr>
                <td class="ProjectListtd1">
                    Name &nbsp; :
                </td>
                <td>
                    <asp:TextBox ID="txtDestinationProfile" runat="server" Width="178px" Height="19px"></asp:TextBox>
                    <asp:RequiredFieldValidator 
                        ID="rfvDestinationProfileName" 
                        runat="server" 
                        Display="None"
                        ControlToValidate="txtDestinationProfile" 
                        ErrorMessage = "Please provide destination profile name." 
                        ValidationGroup="vgDestination" ></asp:RequiredFieldValidator>
                </td>
                <td class="ProjectListtd1">
                    Profiler Option &nbsp; :
                </td>
                <td>
                    <asp:DropDownList ID="cmbProfilerOption" runat="server" Width="180px" CssClass="select">
                    </asp:DropDownList>
               </td>
            </tr>
            <uc:DeviceTypeSelectionCommon ID="ucDeviceTypeSelectionCommon" runat="server" Type="DestinationProfile" />
        </table>
    </div>    
</div>
<div class="contant_bottom">
    &nbsp;</div>
<div class="clear">
</div>
