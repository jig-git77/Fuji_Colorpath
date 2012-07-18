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
        <h1><asp:Localize ID="locDeviceManagement" runat="server" Text="Device Management" 
                meta:resourcekey="locDeviceManagementResource1"></asp:Localize></h1>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px;">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <center>
                    <fieldset style="width: 60%;">
                        <legend>
                            <asp:Label ID="lblHeader" runat="server" Text="Add Device" 
                                meta:resourcekey="lblHeaderResource1"></asp:Label>
                        </legend>
                        <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" 
                            Visible="False" />
                        <table>
                            <tr>
                                <td align="right">
                                    <span style="font-weight: bold;"><asp:Localize Text="Company Name" 
                                        runat="server" ID="locCompanyName" meta:resourcekey="locCompanyNameResource1"></asp:Localize><span style="color: Red;">*</span>
                                    </span>
                                </td>
                                <td>
                                    &nbsp;:&nbsp;
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
                                            ForeColor="Red" ValidationGroup="vgAddDevice"><ucValidationContainer:ValidationContainer ID="rfvCompanycontainer" runat="server"
                                                ValidationMsg="Please select company." Size="200" />
</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <span style="font-weight: bold;"><asp:Localize Text="Branch Name" 
                                        runat="server" ID="locBranchName" meta:resourcekey="locBranchNameResource1"></asp:Localize><span style="color: Red;">*</span> </span>
                                </td>
                                <td>
                                    &nbsp;:&nbsp;
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <asp:DropDownList ID="cmbBranch" runat="server" Width="370px" CssClass="select" AutoPostBack="True"
                                            OnSelectedIndexChanged="cmbBranch_SelectedIndexChanged" 
                                            meta:resourcekey="cmbBranchResource1">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" Display="Dynamic" ControlToValidate="cmbBranch"
                                            ForeColor="Red" ValidationGroup="vgAddDevice"><ucValidationContainer:ValidationContainer ID="rfvBranchContainer" runat="server"
                                                ValidationMsg="Please select branch." Size="200" />
</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr style="display: none;">
                                <td align="right">
                                    <span style="font-weight: bold;"><asp:Localize Text="License Name" 
                                        runat="server" ID="locLicenseName" meta:resourcekey="locLicenseNameResource1"></asp:Localize><span style="color: Red;">*</span>
                                    </span>
                                </td>
                                <td>
                                    &nbsp;:&nbsp;
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <asp:DropDownList ID="cmbLicense" runat="server" Width="370px" 
                                            CssClass="select" meta:resourcekey="cmbLicenseResource1">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvLicense" runat="server" Display="Dynamic" ControlToValidate="cmbLicense"
                                            ForeColor="Red" ValidationGroup="vgAddDevice"><ucValidationContainer:ValidationContainer ID="rfvLicenseContainer" runat="server"
                                                ValidationMsg="Please select license." Size="200" />
</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <span style="font-weight: bold;"><asp:Localize Text="Device Name" 
                                        ID="locDeviceName" runat="server" meta:resourcekey="locDeviceNameResource1"></asp:Localize><span style="color: Red;">*</span> </span>
                                </td>
                                <td>
                                    &nbsp;:&nbsp;
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <asp:TextBox ID="txtDeviceName" runat="server" MaxLength="50" Width="370px" 
                                            Height="19px" meta:resourcekey="txtDeviceNameResource1"></asp:TextBox>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvDeviceName" runat="server" Display="Dynamic" ControlToValidate="txtDeviceName"
                                            ForeColor="Red" ValidationGroup="vgAddDevice"><ucValidationContainer:ValidationContainer ID="rfvDeviceContainer" runat="server"
                                                ValidationMsg="Please provide device name." Size="200" />
</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <span style="font-weight: bold;"><asp:Localize Text="Device Family" 
                                        runat="server" ID="locDeviceFamily" meta:resourcekey="locDeviceFamilyResource1"></asp:Localize><span style="color: Red;">*</span>
                                    </span>
                                </td>
                                <td>
                                    &nbsp;:&nbsp;
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <asp:DropDownList ID="cmbDeviceFamily" runat="server" Width="370px" 
                                            CssClass="select" meta:resourcekey="cmbDeviceFamilyResource1">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvDeviceFamily" runat="server" Display="Dynamic"
                                            ControlToValidate="cmbDeviceFamily" ForeColor="Red" ValidationGroup="vgAddDevice"><ucValidationContainer:ValidationContainer ID="rfvDeviceFamilyContainer" runat="server"
                                                ValidationMsg="Please select device family." Size="200" />
</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <span style="font-weight: bold;"><asp:Localize Text="No. of Projects" 
                                        runat="server" ID="locNoOfProjects" meta:resourcekey="locNoOfProjectsResource1"></asp:Localize><span style="color: Red;">*</span>
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
                                                    <%=ELicenseSync1DText%> &nbsp;</span></td>
                                            <td>
                                                <div style="float: left;">
                                                    <asp:TextBox ID="txtSync1D" runat="server" Height="19px" Width="50px" MaxLength="4"
                                                        onkeypress="return CheckNumeric(event)" 
                                                        meta:resourcekey="txtSync1DResource1"></asp:TextBox>
                                                </div>
                                                <div style="float: left">
                                                    <asp:RequiredFieldValidator ID="rfvSync1D" runat="server" Display="Dynamic" ControlToValidate="txtSync1D"
                                                        ForeColor="Red" ValidationGroup="vgAddDevice" 
                                                        meta:resourcekey="rfvSync1DResource1"><ucValidationContainer:ValidationContainer ID="rfvNoOfProjectsContainer" runat="server"
                                                            ValidationMsg="Please provide no.of projects." />
</asp:RequiredFieldValidator>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span>
                                                    <%=ELicenseDlpText%> &nbsp;</span></td>
                                            <td>
                                                <div style="float: left;">
                                                    <asp:TextBox ID="txtDlp" runat="server" Height="19px" Width="50px" MaxLength="4"
                                                        onkeypress="return CheckNumeric(event)" meta:resourcekey="txtDlpResource1"></asp:TextBox>
                                                </div>
                                                <div style="float: left">
                                                    <asp:RequiredFieldValidator ID="rfvDlp" runat="server" Display="Dynamic" ControlToValidate="txtDlp"
                                                        ForeColor="Red" ValidationGroup="vgAddDevice"><ucValidationContainer:ValidationContainer ID="ValidationContainer1" runat="server"
                                                            ValidationMsg="Please provide no.of projects." />
</asp:RequiredFieldValidator>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                 <span>
                                                    <%=ELicenseInkOptimizationText%> &nbsp;</span></td>
                                            <td>
                                                <div style="float: left;">
                                                    <asp:TextBox ID="txtInkOpt" runat="server" Height="19px" Width="50px" MaxLength="4"
                                                        onkeypress="return CheckNumeric(event)" 
                                                        meta:resourcekey="txtInkOptResource1"></asp:TextBox>
                                                </div>
                                                <div style="float: left">
                                                    <asp:RequiredFieldValidator ID="rfvInkOpt" runat="server" Display="Dynamic" ControlToValidate="txtInkOpt"
                                                        ForeColor="Red" ValidationGroup="vgAddDevice"><ucValidationContainer:ValidationContainer ID="ValidationContainer3" runat="server"
                                                            ValidationMsg="Please provide no.of projects." />
</asp:RequiredFieldValidator>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span>
                                                    <%=ELicenseVarificationText%> &nbsp;</span></td>
                                            <td>
                                                <div style="float: left;">
                                                    <asp:TextBox ID="txtVarification" runat="server" Height="19px" Width="50px" MaxLength="4"
                                                        onkeypress="return CheckNumeric(event)" 
                                                        meta:resourcekey="txtVarificationResource1"></asp:TextBox>
                                                </div>
                                                <div style="float: left">
                                                    <asp:RequiredFieldValidator ID="rfvVarification" runat="server" Display="Dynamic"
                                                        ControlToValidate="txtVarification" ForeColor="Red" 
                                                        ValidationGroup="vgAddDevice"><ucValidationContainer:ValidationContainer ID="ValidationContainer2" runat="server"
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
                                    <span style="font-weight: bold;"><asp:Localize Text="Is Active?" runat="server" 
                                        ID="locIsActive" meta:resourcekey="locIsActiveResource1"></asp:Localize> </span>
                                </td>
                                <td>
                                    &nbsp;:&nbsp;
                                </td>
                                <td>
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
                                            <asp:LinkButton ID="lbtnSave" runat="server" CssClass="gray_bt" ValidationGroup="vgAddDevice"
                                                OnClick="lbtnSave_Click">
                                                    <span> <asp:Localize runat="server" Text="Save" ID="locSave" 
                                                meta:resourcekey="locSaveResource1"></asp:Localize>
 </span>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="gray_bt" 
                                                PostBackUrl="~/DeviceManagement.aspx">
                                                    <span> <asp:Localize runat="server" Text="Cancel" ID="locCancel" 
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
