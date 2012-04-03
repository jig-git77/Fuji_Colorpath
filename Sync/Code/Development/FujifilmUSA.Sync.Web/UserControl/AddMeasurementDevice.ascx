<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddMeasurementDevice.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.AddMeasurementDevice" %>
<%@ Register Src="~/UserControl/AdminStatusbar.ascx" TagName="AdminStatusbar" TagPrefix="ucAdminStatusbar" %>
<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle" style="line-height: 34px;">
    <div class="head">
        <h1>
            Measurement Device Management</h1>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <center>
                    <fieldset style="width: 60%;">
                        <legend style=" margin-left:40% !important;">
                            <asp:Label ID="lblHeader" runat="server" Text="Add Measurement Device"></asp:Label>
                        </legend>
                        <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" Visible="false" />
                        <table>
                            <tr>
                                <td>
                                    <span>Company Name: &nbsp<span style="color: Red;">*</span> </span>
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <asp:DropDownList ID="cmbCompanyName" runat="server" DataValueField="Key" DataTextField="Value"
                                            OnSelectedIndexChanged="cmbCompanyName_SelectedIndexChanged" AutoPostBack="true"
                                            CssClass="select" Width="200px">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hdnMeasurementDeviceId" runat="server" Value="0" />
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvCompnayName" runat="server" Display="Dynamic"
                                            ControlToValidate="cmbCompanyName" ForeColor="Red" ValidationGroup="vgAddMeasurementDevice">
                                            <div class="tooltip_round_box" style="width: 200px;">
                                            <div class="tooltip_arrow" style="margin-top: -3px;">
                                                &nbsp;</div>
                                            <div class="tooltip_top_left">
                                            </div>
                                            <div class="tooltip_top_right">
                                            </div>
                                            <div class="tooltip_inside">
                                                Please select company.
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
                                    <span>Branch Name: &nbsp<span style="color: Red;">*</span> </span>
                                </td>
                                <td style="width: 400px">
                                    <div style="float: left;">
                                        <asp:DropDownList ID="cmbBranch" runat="server" DataValueField="Key" DataTextField="Value"
                                            CssClass="select" Width="200px" OnSelectedIndexChanged="cmbBranch_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left">
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px">
                                    <span>License Type: &nbsp<span style="color: Red;">*</span> </span>
                                </td>
                                <td style="width: 400px">
                                    <div style="float: left;">
                                        <asp:DropDownList ID="cmbLicenseType" runat="server" DataValueField="Key" DataTextField="Value"
                                            CssClass="select" Width="200px">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left">
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px">
                                    <span>Serial Number: &nbsp<span style="color: Red;">*</span> </span>
                                </td>
                                <td style="width: 400px">
                                    <div style="float: left;">
                                        <asp:TextBox ID="txtSerialNumber" runat="server" Width="200px" Height="19px"></asp:TextBox>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvSerialNumber" runat="server" Display="Dynamic"
                                            ControlToValidate="txtSerialNumber" ForeColor="Red" ValidationGroup="vgSerialNumber">
                                            <div class="tooltip_round_box" style="width: 200px;">
                                            <div class="tooltip_arrow" style="margin-top: -3px;">
                                                &nbsp;</div>
                                            <div class="tooltip_top_left">
                                            </div>
                                            <div class="tooltip_top_right">
                                            </div>
                                            <div class="tooltip_inside">
                                                Please Serial Number.
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
                                    <span>Instrument Type: &nbsp<span style="color: Red;">*</span> </span>
                                </td>
                                <td style="width: 400px">
                                    <div style="float: left;">
                                    </div>
                                    <div style="float: left">
                                        <asp:DropDownList ID="cmbInstrumentType" runat="server" DataValueField="Key" DataTextField="Value"
                                            CssClass="select" Width="200px">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px">
                                    <span>Is Active ?: &nbsp &nbsp</span>
                                </td>
                                <td style="width: 400px">
                                    <div style="float: left;">
                                        <asp:CheckBox ID="chkIsActive" runat="server" Checked="true" />
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
                                            <asp:LinkButton ID="lbtnSave" runat="server" CssClass="gray_bt" ValidationGroup="vgAddMeasurementDevice"
                                                OnClick="lbtnSave_Click">
                                                    <span> Save </span>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="gray_bt" PostBackUrl="~/MeasurementDeviceList.aspx">
                                                    <span> Cancel </span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </center>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
<div class="admin_bottom">
    &nbsp;</div>
<div class="clear">
</div>
