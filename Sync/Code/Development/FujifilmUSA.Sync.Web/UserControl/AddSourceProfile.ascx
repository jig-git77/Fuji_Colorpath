<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddSourceProfile.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.AddSourceProfile" %>
<%@ Register Src="~/UserControl/MeasurementStatusbar.ascx" TagName="MeasurementStatusbar" TagPrefix="uc" %>
<%@ Register Src="~/UserControl/AddMeasurementCommon.ascx" TagName="AddMeasurementCommon" TagPrefix="uc" %>
<%@ Register Src="~/UserControl/DeviceTypeSelectionCommon.ascx" TagName="DeviceTypeSelectionCommon" TagPrefix="uc" %>

<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle">
    <div class="head">
        <h1>
            Add Source Profile</h1>
        <asp:LinkButton ID="lbtnSourceProfileSave" runat="server" CssClass="button" 
            onclick="lbtnSourceProfileSave_Click">           
            <span>
                <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/save_icon.png" />
                Save</span></asp:LinkButton></div>
    <div class="clear">
    </div>
    <div>
    <uc:MeasurementStatusbar ID="ucMeasurementStatusbar" runat="server" />
    <div class="add_form">
        <table border="0" cellspacing="5" cellpadding="0" class="Width100percent">
            <tr>
                <td class="ProjectListtd1">
                    Name &nbsp; :
                </td>
                <td>
                    <asp:TextBox ID="txtSourceProfile" runat="server" Width="178px" Height="19px"></asp:TextBox>
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
            <uc:DeviceTypeSelectionCommon ID="ucDeviceTypeSelectionCommon" runat="server" Type="SourceProfile" />
        </table>
    </div>
    <uc:AddMeasurementCommon ID="ucAddMeasurementCommon" runat="server" Type="SourceProfile" />
    </div>
</div>
<div class="admin_bottom">
    &nbsp;</div>
<div class="clear">
</div>


