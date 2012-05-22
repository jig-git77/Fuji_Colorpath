<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddDeviceManagement.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.AddDeviceManagement" %>
<%@ Register Src="~/UserControl/AdminStatusbar.ascx" TagName="AdminStatusbar" TagPrefix="ucAdminStatusbar" %>
<%@ Register Src="~/UserControl/ValidationContainer.ascx" TagName="ValidationContainer"
    TagPrefix="ucValidationContainer" %>
<script src="JS/CommonFunctions.js?version=4" type="text/javascript"></script>
<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle" style="line-height: 34px;">
    <div class="head">
        <h1>
            Device Management</h1>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px;">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <center>
                    <fieldset style="width: 60%;">
                        <legend>
                            <asp:Label ID="lblHeader" runat="server" Text="Add Device"></asp:Label>
                        </legend>
                        <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" Visible="false" />
                        <table>
                            <tr>
                                <td align="right">
                                    <span style="font-weight: bold;">Company Name<span style="color: Red;">*</span>
                                    </span>
                                </td>
                                <td>
                                    &nbsp;:&nbsp;
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <asp:DropDownList ID="cmbCompany" runat="server" Width="400px" CssClass="select"
                                            AutoPostBack="true" OnSelectedIndexChanged="cmbCompany_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvcompany" runat="server" Display="Dynamic" ControlToValidate="cmbCompany"
                                            ForeColor="Red" ValidationGroup="vgAddDevice">
                                            <ucValidationContainer:ValidationContainer ID="rfvCompanycontainer" runat="server"
                                                ValidationMsg="Please select company." Size="200" />
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <span style="font-weight: bold;">Branch Name<span style="color: Red;">*</span> </span>
                                </td>
                                <td>
                                    &nbsp;:&nbsp;
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <asp:DropDownList ID="cmbBranch" runat="server" Width="400px" CssClass="select" AutoPostBack="true"
                                            OnSelectedIndexChanged="cmbBranch_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" Display="Dynamic" ControlToValidate="cmbBranch"
                                            ForeColor="Red" ValidationGroup="vgAddDevice">
                                            <ucValidationContainer:ValidationContainer ID="rfvBranchContainer" runat="server"
                                                ValidationMsg="Please select branch." Size="200" />
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr style="display: none;">
                                <td align="right">
                                    <span style="font-weight: bold;">License Name<span style="color: Red;">*</span>
                                    </span>
                                </td>
                                <td>
                                    &nbsp;:&nbsp;
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <asp:DropDownList ID="cmbLicense" runat="server" Width="400px" CssClass="select">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvLicense" runat="server" Display="Dynamic" ControlToValidate="cmbLicense"
                                            ForeColor="Red" ValidationGroup="vgAddDevice">
                                            <ucValidationContainer:ValidationContainer ID="rfvLicenseContainer" runat="server"
                                                ValidationMsg="Please select license." Size="200" />
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <span style="font-weight: bold;">Device Name<span style="color: Red;">*</span> </span>
                                </td>
                                <td>
                                    &nbsp;:&nbsp;
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <asp:TextBox ID="txtDeviceName" runat="server" MaxLength="50" Width="400px" Height="19px"></asp:TextBox>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvDeviceName" runat="server" Display="Dynamic" ControlToValidate="txtDeviceName"
                                            ForeColor="Red" ValidationGroup="vgAddDevice">
                                            <ucValidationContainer:ValidationContainer ID="rfvDeviceContainer" runat="server"
                                                ValidationMsg="Please provide device name." Size="200" />
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <span style="font-weight: bold;">Device Family<span style="color: Red;">*</span>
                                    </span>
                                </td>
                                <td>
                                    &nbsp;:&nbsp;
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <asp:DropDownList ID="cmbDeviceFamily" runat="server" Width="400px" CssClass="select">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvDeviceFamily" runat="server" Display="Dynamic"
                                            ControlToValidate="cmbDeviceFamily" ForeColor="Red" ValidationGroup="vgAddDevice">
                                            <ucValidationContainer:ValidationContainer ID="rfvDeviceFamilyContainer" runat="server"
                                                ValidationMsg="Please select device family." Size="200" />
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <span style="font-weight: bold;">No. of Projects<span style="color: Red;">*</span>
                                    </span>
                                </td>
                                <td>
                                    &nbsp;:&nbsp;
                                </td>
                                <td>
                                    <div style="line-height: 30px; overflow-y: scroll; height: 130px; border-width: 1px;
                                                                    border-style: solid; width: 220px">
                                    <table width="100%">
                                        <tr>
                                            <td style="width:130px;">
                                                <span>
                                                    <%=ELicenseSync1DText%> : </span>
                                            </td>
                                            <td>
                                                <div style="float: left;">
                                                    <asp:TextBox ID="txtSync1D" runat="server" Height="19px" Width="50px" MaxLength="4"
                                                        onkeypress="return CheckNumeric(event)"></asp:TextBox>
                                                </div>
                                                <div style="float: left">
                                                    <asp:RequiredFieldValidator ID="rfvSync1D" runat="server" Display="Dynamic" ControlToValidate="txtSync1D"
                                                        ForeColor="Red" ValidationGroup="vgAddDevice">
                                                        <ucValidationContainer:ValidationContainer ID="rfvNoOfProjectsContainer" runat="server"
                                                            ValidationMsg="Please provide no.of projects." />
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span>
                                                    <%=ELicenseDlpText%> : </span>
                                            </td>
                                            <td>
                                                <div style="float: left;">
                                                    <asp:TextBox ID="txtDlp" runat="server" Height="19px" Width="50px" MaxLength="4"
                                                        onkeypress="return CheckNumeric(event)"></asp:TextBox>
                                                </div>
                                                <div style="float: left">
                                                    <asp:RequiredFieldValidator ID="rfvDlp" runat="server" Display="Dynamic" ControlToValidate="txtDlp"
                                                        ForeColor="Red" ValidationGroup="vgAddDevice">
                                                        <ucValidationContainer:ValidationContainer ID="ValidationContainer1" runat="server"
                                                            ValidationMsg="Please provide no.of projects." />
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                 <span>
                                                    <%=ELicenseInkOptimizationText%> : </span>
                                            </td>
                                            <td>
                                                <div style="float: left;">
                                                    <asp:TextBox ID="txtInkOpt" runat="server" Height="19px" Width="50px" MaxLength="4"
                                                        onkeypress="return CheckNumeric(event)"></asp:TextBox>
                                                </div>
                                                <div style="float: left">
                                                    <asp:RequiredFieldValidator ID="rfvInkOpt" runat="server" Display="Dynamic" ControlToValidate="txtInkOpt"
                                                        ForeColor="Red" ValidationGroup="vgAddDevice">
                                                        <ucValidationContainer:ValidationContainer ID="ValidationContainer3" runat="server"
                                                            ValidationMsg="Please provide no.of projects." />
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span>
                                                    <%=ELicenseVarificationText%> : </span>
                                            </td>
                                            <td>
                                                <div style="float: left;">
                                                    <asp:TextBox ID="txtVarification" runat="server" Height="19px" Width="50px" MaxLength="4"
                                                        onkeypress="return CheckNumeric(event)"></asp:TextBox>
                                                </div>
                                                <div style="float: left">
                                                    <asp:RequiredFieldValidator ID="rfvVarification" runat="server" Display="Dynamic"
                                                        ControlToValidate="txtVarification" ForeColor="Red" ValidationGroup="vgAddDevice">
                                                        <ucValidationContainer:ValidationContainer ID="ValidationContainer2" runat="server"
                                                            ValidationMsg="Please provide no.of projects." />
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </td>
                                        </tr> 
                                    </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <span style="font-weight: bold;">Is Active? </span>
                                </td>
                                <td>
                                    &nbsp;:&nbsp;
                                </td>
                                <td>
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
                                            <asp:LinkButton ID="lbtnSave" runat="server" CssClass="gray_bt" ValidationGroup="vgAddDevice"
                                                OnClick="lbtnSave_Click">
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
