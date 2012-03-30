<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddLicenseType.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.AddLicenseType" %>
<%@ Register Src="~/UserControl/AdminStatusbar.ascx" TagName="AdminStatusbar" TagPrefix="ucAdminStatusbar" %>
<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle" style="line-height: 34px;">
    <div class="head">
        <h1>
            Add License Type</h1>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px;">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" Visible="false" />
                <table cellpadding="0" cellspacing="0" border="0" width="97%">
                    <tr id="trListHeader" runat="server">
                        <td>
                            <asp:Panel ID="panelAddProjectDescription" runat="server" BorderStyle="None">
                                <table>
                                    <tr>
                                        <td>
                                            <span style="font-weight: bold;">License Type: &nbsp<span style="color: Red;">*</span>
                                            </span>
                                        </td>
                                        <td>
                                            <div style="float: left;">
                                                <asp:TextBox ID="txtLicenseType" runat="server" MaxLength="30" Width="400px" Height="19px"></asp:TextBox>
                                            </div>
                                            <div style="float: left">
                                                <asp:RequiredFieldValidator ID="rfvLicenseType" runat="server" Display="Dynamic"
                                                    ControlToValidate="txtLicenseType" ForeColor="Red" ValidationGroup="vgAddDeviceFamily">
                                            <div class="tooltip_round_box" style="width: 225px;">
                                            <div class="tooltip_arrow" style="margin-top: -3px;">
                                                &nbsp;</div>
                                            <div class="tooltip_top_left">
                                            </div>
                                            <div class="tooltip_top_right">
                                            </div>
                                            <div class="tooltip_inside">
                                                Please insert license type.
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
                                        <td>
                                        </td>
                                        <td>
                                            <div class="text">
                                                <div style="padding-left: 3px;">
                                                    <asp:LinkButton ID="lbtnSave" runat="server" CssClass="gray_bt" ValidationGroup="vgAddDeviceFamily"
                                                        OnClick="lbtnSave_Click">
                                                    <span> Save </span>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="gray_bt" PostBackUrl="~/LicenseManagement.aspx">
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
