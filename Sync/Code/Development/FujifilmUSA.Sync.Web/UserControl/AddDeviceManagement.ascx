<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddDeviceManagement.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.AddDeviceManagement" %>
<%@ Register Src="~/UserControl/AdminStatusbar.ascx" TagName="AdminStatusbar" TagPrefix="ucAdminStatusbar" %>
<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle" style="line-height: 34px;">
    <div class="head">
        <h1>
            Add Device Management</h1>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px;">
       <asp:UpdatePanel runat="server">
       <ContentTemplate>
       <ucAdminStatusbar:AdminStatusbar ID="ucAdminStatusbar" runat="server" Visible="false" />
        <table cellpadding="0" cellspacing="0" border="0" width="97%">
            <tr id="trListHeader" runat="server">
                <td>
                    <asp:Panel ID="panelAddProjectDescription" runat="server" BorderStyle="None">
                        <table>
                            <tr>
                                <td>
                                    <span style="font-weight:bold;">Company Name: &nbsp<span style="color: Red;">*</span> </span>
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <asp:DropDownList ID="cmbCompany" runat="server"  Width="400px" 
                                            CssClass="select" AutoPostBack="true" onselectedindexchanged="cmbCompany_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvCompnay" runat="server" Display="Dynamic"
                                            ControlToValidate="cmbCompany" ForeColor="Red" ValidationGroup="vgAddDevice">
                                            <div class="tooltip_round_box" style="width: 200px;">
                                            <div class="tooltip_arrow" style="margin-top: -3px;">
                                                &nbsp;</div>
                                            <div class="tooltip_top_left">
                                            </div>
                                            <div class="tooltip_top_right">
                                            </div>
                                            <div class="tooltip_inside">
                                                Please select compnay.
                                            </div>
                                            <div class="tooltip_bottom_left">
                                            </div>
                                            <div class="tooltip_bottom_right">
                                            </div>
                                        </div>
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px">
                                    <span style="font-weight:bold;">Branch Name: &nbsp<span style="color: Red;">*</span> </span>
                                </td>
                                <td style="width: 400px">
                                    <div style="float: left;">
                                         <asp:DropDownList ID="cmbBranch" runat="server"  Width="400px" 
                                             CssClass="select" AutoPostBack="true" onselectedindexchanged="cmbBranch_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" Display="Dynamic"
                                            ControlToValidate="cmbBranch" ForeColor="Red" ValidationGroup="vgAddDevice">
                                            <div class="tooltip_round_box" style="width: 200px;">
                                            <div class="tooltip_arrow" style="margin-top: -3px;">
                                                &nbsp;</div>
                                            <div class="tooltip_top_left">
                                            </div>
                                            <div class="tooltip_top_right">
                                            </div>
                                            <div class="tooltip_inside">
                                                Please select branch.
                                            </div>
                                            <div class="tooltip_bottom_left">
                                            </div>
                                            <div class="tooltip_bottom_right">
                                            </div>
                                        </div>
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px">
                                    <span style="font-weight:bold;">License Name: &nbsp<span style="color: Red;">*</span> </span>
                                </td>
                                <td style="width: 400px">
                                    <div style="float: left;">
                                         <asp:DropDownList ID="cmbLicense" runat="server"  Width="400px" CssClass="select">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvLicense" runat="server" Display="Dynamic"
                                            ControlToValidate="cmbLicense" ForeColor="Red" ValidationGroup="vgAddDevice">
                                            <div class="tooltip_round_box" style="width: 200px;">
                                            <div class="tooltip_arrow" style="margin-top: -3px;">
                                                &nbsp;</div>
                                            <div class="tooltip_top_left">
                                            </div>
                                            <div class="tooltip_top_right">
                                            </div>
                                            <div class="tooltip_inside">
                                                Please select license.
                                            </div>
                                            <div class="tooltip_bottom_left">
                                            </div>
                                            <div class="tooltip_bottom_right">
                                            </div>
                                        </div>
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px">
                                    <span style="font-weight:bold;">Device Name: &nbsp<span style="color: Red;">*</span> </span>
                                </td>
                                <td style="width: 400px">
                                    <div style="float: left;">
                                        <asp:TextBox ID="txtDeviceName" runat="server" MaxLength="50" Width="400px" Height="19px" ></asp:TextBox>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvDeviceName" runat="server" Display="Dynamic"
                                            ControlToValidate="txtDeviceName" ForeColor="Red" ValidationGroup="vgAddDevice">
                                            <div class="tooltip_round_box" style="width: 200px;">
                                            <div class="tooltip_arrow" style="margin-top: -3px;">
                                                &nbsp;</div>
                                            <div class="tooltip_top_left">
                                            </div>
                                            <div class="tooltip_top_right">
                                            </div>
                                            <div class="tooltip_inside">
                                                Please provide device name.
                                            </div>
                                            <div class="tooltip_bottom_left">
                                            </div>
                                            <div class="tooltip_bottom_right">
                                            </div>
                                        </div>
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px">
                                    <span style="font-weight:bold;">Device Family: &nbsp<span style="color: Red;">*</span> </span>
                                </td>
                                <td style="width: 400px">
                                    <div style="float: left;">
                                         <asp:DropDownList ID="cmbDeviceFamily" runat="server"  Width="400px" CssClass="select">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvDeviceFamily" runat="server" Display="Dynamic"
                                            ControlToValidate="cmbDeviceFamily" ForeColor="Red" ValidationGroup="vgAddDevice">
                                            <div class="tooltip_round_box" style="width: 200px;">
                                            <div class="tooltip_arrow" style="margin-top: -3px;">
                                                &nbsp;</div>
                                            <div class="tooltip_top_left">
                                            </div>
                                            <div class="tooltip_top_right">
                                            </div>
                                            <div class="tooltip_inside">
                                                Please select device family.
                                            </div>
                                            <div class="tooltip_bottom_left">
                                            </div>
                                            <div class="tooltip_bottom_right">
                                            </div>
                                        </div>
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                              <tr>
                                <td style="width: 130px">
                                    <span style="font-weight:bold;">Is Active ?: &nbsp &nbsp</span>
                                </td>
                                <td style="width: 400px">
                                    <div style="float: left;">
                                        <asp:CheckBox ID="chkIsActive" runat ="server" />
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                        <div class="text" >
                                            <div style="padding-left: 3px;">
                                                <asp:LinkButton ID="lbtnSave" 
                                                    runat="server" 
                                                    CssClass="gray_bt"
                                                    ValidationGroup="vgAddDevice" onclick="lbtnSave_Click" 
                                                    >
                                                    <span> Save </span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="gray_bt" PostBackUrl="~/DeviceManagement.aspx">
                                                    <span> Cancel </span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="clear">
                                        </div>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
<div class="admin_bottom">
    &nbsp;</div>
<div class="clear">
</div>
