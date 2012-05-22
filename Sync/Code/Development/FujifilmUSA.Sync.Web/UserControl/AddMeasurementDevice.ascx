<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddMeasurementDevice.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.AddMeasurementDevice" %>
<%@ Register Src="~/UserControl/AdminStatusbar.ascx" TagName="AdminStatusbar" TagPrefix="ucAdminStatusbar" %>
<%@ Register Src="~/UserControl/ValidationContainer.ascx" TagName="ValidationContainer"
    TagPrefix="ucValidationContainer" %>
<script src="JS/CommonFunctions.js?version=4" type="text/javascript"></script>
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
                        <legend style="margin-left: 40% !important;">
                            <asp:Label ID="lblHeader" runat="server" Text="Add Measurement Device"></asp:Label>
                        </legend>
                        <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" Visible="false" />
                        <table>
                            <tr>
                                <td align="right">
                                    <span style="font-weight: bold;">Company Name<span style="color: Red;">*</span>
                                    </span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
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
                                        <asp:RequiredFieldValidator ID="rfvcompanyName" runat="server" Display="Dynamic"
                                            ControlToValidate="cmbCompanyName" ForeColor="Red" ValidationGroup="vgAddMeasurementDevice">
                                            <ucValidationContainer:ValidationContainer ID="rfvCompnayContainer" runat="server"
                                                ValidationMsg="Please select company." Size="200" />
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px" align="right">
                                    <span style="font-weight: bold;">Branch Name<span style="color: Red;">*</span> </span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td style="width: 400px">
                                    <div style="float: left;">
                                        <asp:DropDownList ID="cmbBranch" runat="server" DataValueField="Key" DataTextField="Value"
                                            CssClass="select" Width="200px" OnSelectedIndexChanged="cmbBranch_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" Display="Dynamic" ControlToValidate="cmbBranch"
                                            ForeColor="Red" ValidationGroup="vgAddMeasurementDevice">
                                            <ucValidationContainer:ValidationContainer ID="rfvBranchContainer" runat="server"
                                                ValidationMsg="Please select branch." Size="200" />
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px" align="right">
                                    <span style="font-weight: bold;">License Name<span style="color: Red;">*</span>
                                    </span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td style="width: 400px">
                                    <div style="float: left;">
                                        <asp:DropDownList ID="cmbLicenseType" runat="server" DataValueField="Key" DataTextField="Value"
                                            CssClass="select" Width="200px">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvLicenseType" runat="server" Display="Dynamic"
                                            ControlToValidate="cmbLicenseType" ForeColor="Red" ValidationGroup="vgAddMeasurementDevice">
                                            <ucValidationContainer:ValidationContainer ID="rfvLicenseContainer" runat="server"
                                                ValidationMsg="Please select license name." Size="200" />
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px" align="right">
                                    <span style="font-weight: bold;">Serial Number<span style="color: Red;">*</span>
                                    </span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td style="width: 400px">
                                    <div style="float: left;">
                                        <asp:TextBox ID="txtSerialNumber" runat="server" Width="200px" Height="19px" MaxLength="20"
                                            onkeypress="return CheckNumeric(event)"></asp:TextBox>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvSerialNumber" runat="server" Display="Dynamic"
                                            ControlToValidate="txtSerialNumber" ForeColor="Red" ValidationGroup="vgAddMeasurementDevice">
                                            <ucValidationContainer:ValidationContainer ID="rfvSerialNumberContainer" runat="server"
                                                ValidationMsg="Please provide serial number." Size="200" />
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px" align="right">
                                    <span style="font-weight: bold;">Instrument Type<span style="color: Red;">*</span>
                                    </span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td style="width: 400px">
                                    <div style="float: left">
                                        <asp:DropDownList ID="cmbInstrumentType" runat="server" DataValueField="Key" DataTextField="Value"
                                            CssClass="select" Width="200px">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvInstrumentType" runat="server" Display="Dynamic"
                                            ControlToValidate="cmbInstrumentType" ForeColor="Red" ValidationGroup="vgAddMeasurementDevice">
                                            <ucValidationContainer:ValidationContainer ID="rfvInstrumentContainer" runat="server"
                                                ValidationMsg="Please select instrument type." Size="200" />
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                             <tr>
                                <td style="width: 130px" align="right">
                                    <span style="font-weight: bold;">Description
                                    </span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td style="width: 400px">
                                    <div style="float: left;">
                                        <asp:TextBox ID="txtDescription" runat="server" Width="200px" Height="19px" MaxLength="100">
                                           </asp:TextBox>
                                    </div>
                                    <div style="float: left">
                                      
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px" align="right">
                                    <span style="font-weight: bold;">Is Active?</span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
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
