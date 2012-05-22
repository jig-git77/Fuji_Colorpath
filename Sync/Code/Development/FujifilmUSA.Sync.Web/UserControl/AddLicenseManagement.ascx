<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddLicenseManagement.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.AddLicenseManagement" %>
<%@ Register Src="~/UserControl/AdminStatusbar.ascx" TagName="AdminStatusbar" TagPrefix="ucAdminStatusbar" %>
<%@ Register Src="~/UserControl/ValidationContainer.ascx" TagName="ValidationContainer"
    TagPrefix="ucValidationContainer" %>
<link rel="stylesheet" href="JQuery/datepicker-themes/base/jquery.ui.all.css">
<script type="text/javascript" src="JQuery/jquery.ui.core.js"></script>
<script type="text/javascript" src="JQuery/jquery.ui.datepicker.js"></script>
<script type="text/javascript" language="javascript">
    function pageLoad() {
        jQuery("[id*='txtActivationDate']").datepicker();
        jQuery("[id*='txtExpirationDate']").datepicker();
    }

</script>
<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle" style="line-height: 34px;">
    <div class="head">
        <h1>
            License Management</h1>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px;">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <center>
                    <fieldset style="width: 70%;">
                        <legend>
                            <asp:Label ID="lblHeader" runat="server" Text="Add License"></asp:Label>
                        </legend>
                        <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" Visible="false" />
                        <table width="100%">
                            <tr>
                                <td align="right">
                                    <span style="font-weight: bold;">Company Name<span style="color: Red;">*</span>
                                    </span>
                                </td>
                                <td style="width:8px;">
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <asp:DropDownList ID="cmbCompany" runat="server" Width="400px" CssClass="select"
                                            AutoPostBack="true" OnSelectedIndexChanged="cmbCompany_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvcompany" runat="server" Display="Dynamic" ControlToValidate="cmbCompany"
                                            ForeColor="Red" ValidationGroup="vgAddLicense">
                                            <ucValidationContainer:ValidationContainer ID="rfvCompanyContainer" runat="server"
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
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td style="width: 400px">
                                    <div style="float: left;">
                                        <asp:DropDownList ID="cmbBranch" runat="server" Width="400px" CssClass="select">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" Display="Dynamic" ControlToValidate="cmbBranch"
                                            ForeColor="Red" ValidationGroup="vgAddLicense">
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
                                    <span style="font-weight: bold;">License Description<span style="color: Red;">*</span>
                                    </span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td style="width: 400px">
                                    <div style="float: left;">
                                        <asp:TextBox ID="txtLicenseDescription" runat="server" MaxLength="50" Width="400px"
                                            Height="19px"></asp:TextBox>
                                    </div>
                                    <div style="float: left">
                                    <asp:RequiredFieldValidator ID="rfvLicenseDescription" runat="server" Display="Dynamic"
                                            ControlToValidate="txtLicenseDescription" ForeColor="Red" ValidationGroup="vgAddLicense">
                                            <ucValidationContainer:ValidationContainer ID="rfvcLicenseDescriptionContainer" runat="server"
                                                ValidationMsg="Please provide license description." Size="200" />
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px" align="right">
                                    <span style="font-weight: bold;">License Type<span style="color: Red;">*</span>
                                    </span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td style="width: 400px">
                                    <div style="float: left;">
                                        <asp:CheckBox runat="server" ID="chkSync1D" CssClass="checkListBox"/>
                                        <asp:CheckBox runat="server" ID="chkDlp" CssClass="checkListBox"/>
                                        <asp:CheckBox runat="server" ID="chkInk" CssClass="checkListBox"/>
                                        <asp:CheckBox runat="server" ID="chkVarification" CssClass="checkListBox"/>
                                    </div>
                                    <div style="float: left">
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px" align="right">
                                    <span style="font-weight: bold;">Activation Date<span style="color: Red;">*</span>
                                    </span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td style="width: 400px">
                                    <div style="float: left;">
                                        <asp:TextBox ID="txtActivationDate" runat="server" OnKeyPress="javascript: return false;"
                                            MaxLength="50" Width="100px" Height="19px"></asp:TextBox>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvActivationDate" runat="server" Display="Dynamic"
                                            ControlToValidate="txtActivationDate" ForeColor="Red" ValidationGroup="vgAddLicense">
                                            <ucValidationContainer:ValidationContainer ID="rfvActivateContainer" runat="server"
                                                ValidationMsg="Please select activation date." Size="200" />
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px" align="right">
                                    <span style="font-weight: bold;">Expiration Date<span style="color: Red;">*</span>
                                    </span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td style="width: 400px">
                                    <div style="float: left;">
                                        <asp:TextBox ID="txtExpirationDate" runat="server" OnKeyPress="javascript: return false;"
                                            MaxLength="50" Width="100px" Height="19px"></asp:TextBox>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvExpirationDate" runat="server" Display="Dynamic"
                                            ControlToValidate="txtExpirationDate" ForeColor="Red" ValidationGroup="vgAddLicense">
                                            <ucValidationContainer:ValidationContainer ID="rfvExpiraDateContainer" runat="server"
                                                ValidationMsg="Please select expiration date." Size="200" />
                                        </asp:RequiredFieldValidator>
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
                                            <asp:LinkButton ID="lbtnSave" runat="server" CssClass="gray_bt" ValidationGroup="vgAddLicense"
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
