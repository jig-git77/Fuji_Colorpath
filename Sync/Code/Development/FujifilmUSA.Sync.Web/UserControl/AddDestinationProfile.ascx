<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddDestinationProfile.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.AddDestinationProfile" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="uc" %>
<%@ Register Src="~/UserControl/MeasurementStatusbar.ascx" TagName="MeasurementStatusbar" TagPrefix="uc" %>
<%@ Register Src="~/UserControl/AddMeasurementCommon.ascx" TagName="AddMeasurementCommon" TagPrefix="uc" %>
<%@ Register Src="~/UserControl/DeviceTypeSelectionCommon.ascx" TagName="DeviceTypeSelectionCommon" TagPrefix="uc" %>

<script type="text/javascript" language="javascript">
    $(document).ready(function () {
        jQuery("select[id$=cmbDeviceType]").css("width", "180px");
        
        //Controlling Analyze button - If HasMeasurement = true do postback, else no postback
        jQuery('#<%= lbtnDestinationProfileSave.ClientID %>').click(
            function () {
                if (jQuery("input[id*='hdHasMeasurement']").val() > 0)
                    return true;
                else
                    return false;
            });
    });
        
</script>
<div class="contant_top">
    &nbsp;</div>
<div class="contant_middle">
    <div class="head">
        <h1>
            Add Destination Profile</h1>
        <asp:LinkButton ID="lbtnDestinationProfileSave" runat="server" CssClass="button"
            OnClick="lbtnDestinationProfileSave_Click">
            <span>
                <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/save_icon.png" />
                Save</span></asp:LinkButton></div>
    <div class="clear">
    </div>
    <uc:MeasurementStatusbar ID="ucMeasurementStatusbar" runat="server" />
    <div class="info_form">
        <uc:Statusbar ID="ucStatusbar1" runat="server" Visible="false" />
    </div>
    <div class="add_form">
        <table border="0" cellspacing="5" cellpadding="0" class="Width100percent">
            <tr>
                <td class="ProjectListtd1">
                    Name &nbsp; :
                </td>
                <td>
                    <asp:TextBox ID="txtDestinationProfile" runat="server" Width="178px" Height="19px"></asp:TextBox>
                    <%--<asp:RequiredFieldValidator ID="rfvProjectName" runat="server" ControlToValidate="txtProjectName" ErrorMessage="*" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="SaveProject"></asp:RequiredFieldValidator>--%>
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
    <uc:AddMeasurementCommon ID="ucAddMeasurementCommon" runat="server" Type="DestinationProfile" />
</div>
<div class="contant_bottom">
    &nbsp;</div>
<div class="clear">
</div>
