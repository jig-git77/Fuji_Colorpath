<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="AddLicenseManagement.ascx.cs"
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
        <h1><asp:Localize ID="locLicenseMgmt" runat="server" Text="License Management" 
                meta:resourcekey="locLicenseMgmtResource1"></asp:Localize></h1>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px;">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <center>
                    <fieldset style="width: 70%;">
                        <legend>
                            <asp:Label ID="lblHeader" runat="server" Text="Add License" 
                                meta:resourcekey="lblHeaderResource1"></asp:Label>
                        </legend>
                        <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" 
                            Visible="False" />
                        <table width="100%">
                            <tr>
                                <td align="right">
                                    <span style="font-weight: bold;"><asp:Localize Text="Company Name" 
                                        ID="locCompanyName" runat="server" meta:resourcekey="locCompanyNameResource1"></asp:Localize><span style="color: Red;">*</span>
                                    </span>
                                </td>
                                <td style="width:8px;">
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <asp:DropDownList ID="cmbCompany" runat="server" Width="370px" CssClass="select"
                                            AutoPostBack="True" 
                                            OnSelectedIndexChanged="cmbCompany_SelectedIndexChanged" 
                                            meta:resourcekey="cmbCompanyResource1">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvcompany" runat="server" Display="Dynamic" ControlToValidate="cmbCompany"
                                            ForeColor="Red" ValidationGroup="vgAddLicense"><ucValidationContainer:ValidationContainer ID="rfvCompanyContainer" runat="server"
                                                ValidationMsg="Please select company." Size="200" />
</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <span style="font-weight: bold;"><asp:Localize ID="locBranchName" 
                                        runat="server" Text="Branch Name" meta:resourcekey="locBranchNameResource1"></asp:Localize><span style="color: Red;">*</span> </span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td style="width: 370px">
                                    <div style="float: left;">
                                        <asp:DropDownList ID="cmbBranch" runat="server" Width="370px" CssClass="select" 
                                            meta:resourcekey="cmbBranchResource1">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" Display="Dynamic" ControlToValidate="cmbBranch"
                                            ForeColor="Red" ValidationGroup="vgAddLicense"><ucValidationContainer:ValidationContainer ID="rfvBranchContainer" runat="server"
                                                ValidationMsg="Please select branch." Size="200" />
</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px" align="right">
                                    <span style="font-weight: bold;"><asp:Localize runat="server" 
                                        Text="License Description" ID="locLicenseDesc" 
                                        meta:resourcekey="locLicenseDescResource1"></asp:Localize><span style="color: Red;">*</span>
                                    </span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td style="width: 370px">
                                    <div style="float: left;">
                                        <asp:TextBox ID="txtLicenseDescription" runat="server" MaxLength="50" Width="370px"
                                            Height="19px" meta:resourcekey="txtLicenseDescriptionResource1"></asp:TextBox>
                                    </div>
                                    <div style="float: left">
                                    <asp:RequiredFieldValidator ID="rfvLicenseDescription" runat="server" Display="Dynamic"
                                            ControlToValidate="txtLicenseDescription" ForeColor="Red" 
                                            ValidationGroup="vgAddLicense"><ucValidationContainer:ValidationContainer ID="rfvcLicenseDescriptionContainer" runat="server"
                                                ValidationMsg="Please provide license description." Size="200" />
</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px" align="right">
                                    <span style="font-weight: bold;"><asp:Localize Text="License Type" 
                                        runat="server" ID="locLicenseType" meta:resourcekey="locLicenseTypeResource1"></asp:Localize><span style="color: Red;">*</span>
                                    </span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td style="width: 370px">
                                    <div style="float: left;">
                                        <asp:CheckBox runat="server" ID="chkSync1D" CssClass="checkListBox" 
                                            meta:resourcekey="chkSync1DResource1"/>
                                        <asp:CheckBox runat="server" ID="chkDlp" CssClass="checkListBox" 
                                            meta:resourcekey="chkDlpResource1"/>
                                        <asp:CheckBox runat="server" ID="chkInk" CssClass="checkListBox" 
                                            meta:resourcekey="chkInkResource1"/>    
                                        <asp:CheckBox runat="server" ID="chkInk_Source" CssClass="checkListBox"/>
                                        <asp:CheckBox runat="server" ID="chkSync4D_Certification" CssClass="checkListBox"/>                               
                                    </div>
                                    <div style="float: left">
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px" align="right">
                                    <span style="font-weight: bold;"><asp:Localize Text="Activation Date" 
                                        ID="locActivationDate" runat="server" meta:resourcekey="locActivationDateResource1"></asp:Localize><span style="color: Red;">*</span>
                                    </span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td style="width: 370px">
                                    <div style="float: left;">
                                        <asp:TextBox ID="txtActivationDate" runat="server" OnKeyPress="javascript: return false;"
                                            MaxLength="50" Width="100px" Height="19px" 
                                            meta:resourcekey="txtActivationDateResource1"></asp:TextBox>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvActivationDate" runat="server" Display="Dynamic"
                                            ControlToValidate="txtActivationDate" ForeColor="Red" 
                                            ValidationGroup="vgAddLicense"><ucValidationContainer:ValidationContainer ID="rfvActivateContainer" runat="server"
                                                ValidationMsg="Please select activation date." Size="200" />
</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px" align="right">
                                    <span style="font-weight: bold;"><asp:Localize ID="locExpirationDate" 
                                        runat="server" Text="Expiration Date" 
                                        meta:resourcekey="locExpirationDateResource1"></asp:Localize><span style="color: Red;">*</span>
                                    </span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td style="width: 370px">
                                    <div style="float: left;">
                                        <asp:TextBox ID="txtExpirationDate" runat="server" OnKeyPress="javascript: return false;"
                                            MaxLength="50" Width="100px" Height="19px" 
                                            meta:resourcekey="txtExpirationDateResource1"></asp:TextBox>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvExpirationDate" runat="server" Display="Dynamic"
                                            ControlToValidate="txtExpirationDate" ForeColor="Red" 
                                            ValidationGroup="vgAddLicense"><ucValidationContainer:ValidationContainer ID="rfvExpiraDateContainer" runat="server"
                                                ValidationMsg="Please select expiration date." Size="200" />
</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px" align="right">
                                    <span style="font-weight: bold;"><asp:Localize Text="Is Active?" runat="server" 
                                        ID="locIsActive" meta:resourcekey="locIsActiveResource1"></asp:Localize></span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td style="width: 370px">
                                    <div style="float: left;">
                                        <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" 
                                            meta:resourcekey="chkIsActiveResource1" />
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
                                                    <span> <asp:Localize Text="Save" runat="server" ID="locSave" 
                                                meta:resourcekey="locSaveResource1"></asp:Localize>
</span>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="gray_bt" 
                                                PostBackUrl="~/LicenseManagement.aspx">
                                                    <span> <asp:Localize Text="Cancel" runat="server" ID="locCancel" 
                                                meta:resourcekey="locCancelResource1"></asp:Localize>
 </span>
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
