<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="AddMeasurementDevice.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.AddMeasurementDevice" %>
<%@ Register Src="~/UserControl/AdminStatusbar.ascx" TagName="AdminStatusbar" TagPrefix="ucAdminStatusbar" %>
<%@ Register Src="~/UserControl/ValidationContainer.ascx" TagName="ValidationContainer"
    TagPrefix="ucValidationContainer" %>
<script src="JS/CommonFunctions.js?version=4" type="text/javascript"></script>
<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle" style="line-height: 34px;">
    <div class="head">
        <h1><asp:Localize Text="Measurement Device Management" runat="server" ID="locMDM" 
                meta:resourcekey="locMDMResource1"></asp:Localize></h1>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <center>
                    <fieldset style="width: 60%;">
                        <legend style="margin-left: 40% !important;">
                            <asp:Label ID="lblHeader" runat="server" Text="Add Measurement Device" 
                                meta:resourcekey="lblHeaderResource1"></asp:Label>
                        </legend>
                        <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" 
                            Visible="False" />
                        <table>
                            <tr>
                                <td align="right">
                                    <span style="font-weight: bold;"><asp:Localize Text="Company Name" 
                                        ID="locCompanyName" runat="server" meta:resourcekey="locCompanyNameResource1"></asp:Localize><span style="color: Red;">*</span>
                                    </span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <asp:DropDownList ID="cmbCompanyName" runat="server" DataValueField="Key" DataTextField="Value"
                                            OnSelectedIndexChanged="cmbCompanyName_SelectedIndexChanged" AutoPostBack="True"
                                            CssClass="select" Width="200px" meta:resourcekey="cmbCompanyNameResource1">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hdnMeasurementDeviceId" runat="server" Value="0" />
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvcompanyName" runat="server" Display="Dynamic"
                                            ControlToValidate="cmbCompanyName" ForeColor="Red" 
                                            ValidationGroup="vgAddMeasurementDevice"><ucValidationContainer:ValidationContainer ID="rfvCompnayContainer" runat="server"
                                                ValidationMsg="Please select company." Size="200" />
</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px" align="right">
                                    <span style="font-weight: bold;"><asp:Localize Text="Branch Name" 
                                        runat="server" ID="locBranchName" meta:resourcekey="locBranchNameResource1"></asp:Localize><span style="color: Red;">*</span> </span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td style="width: 400px">
                                    <div style="float: left;">
                                        <asp:DropDownList ID="cmbBranch" runat="server" DataValueField="Key" DataTextField="Value"
                                            CssClass="select" Width="200px" OnSelectedIndexChanged="cmbBranch_SelectedIndexChanged"
                                            AutoPostBack="True" meta:resourcekey="cmbBranchResource1">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" Display="Dynamic" ControlToValidate="cmbBranch"
                                            ForeColor="Red" ValidationGroup="vgAddMeasurementDevice"><ucValidationContainer:ValidationContainer ID="rfvBranchContainer" runat="server"
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
                                        Text="License Name" ID="locLicenseName" 
                                        meta:resourcekey="locLicenseNameResource1"></asp:Localize><span style="color: Red;">*</span>
                                    </span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td style="width: 400px">
                                    <div style="float: left;">
                                        <asp:DropDownList ID="cmbLicenseType" runat="server" DataValueField="Key" DataTextField="Value"
                                            CssClass="select" Width="200px" meta:resourcekey="cmbLicenseTypeResource1">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvLicenseType" runat="server" Display="Dynamic"
                                            ControlToValidate="cmbLicenseType" ForeColor="Red" 
                                            ValidationGroup="vgAddMeasurementDevice"><ucValidationContainer:ValidationContainer ID="rfvLicenseContainer" runat="server"
                                                ValidationMsg="Please select license name." Size="200" />
</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px" align="right">
                                    <span style="font-weight: bold;"><asp:Localize Text="Serial Number" 
                                        runat="server" ID="locSerialNumber" meta:resourcekey="locSerialNumberResource1"></asp:Localize><span style="color: Red;">*</span>
                                    </span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td style="width: 400px">
                                    <div style="float: left;">
                                        <asp:TextBox ID="txtSerialNumber" runat="server" Width="200px" Height="19px" MaxLength="20"
                                            onkeypress="return CheckNumericAlpha(event)" 
                                            meta:resourcekey="txtSerialNumberResource1"></asp:TextBox>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvSerialNumber" runat="server" Display="Dynamic"
                                            ControlToValidate="txtSerialNumber" ForeColor="Red" 
                                            ValidationGroup="vgAddMeasurementDevice"><ucValidationContainer:ValidationContainer ID="rfvSerialNumberContainer" runat="server"
                                                ValidationMsg="Please provide serial number." Size="200" />
</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px" align="right">
                                    <span style="font-weight: bold;"><asp:Localize runat="server" 
                                        Text="Instrument Type" ID="locInstrumentType" 
                                        meta:resourcekey="locInstrumentTypeResource1"></asp:Localize><span style="color: Red;">*</span>
                                    </span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td style="width: 400px">
                                    <div style="float: left">
                                        <asp:DropDownList ID="cmbInstrumentType" runat="server" DataValueField="Key" DataTextField="Value"
                                            CssClass="select" Width="200px" 
                                            meta:resourcekey="cmbInstrumentTypeResource1">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvInstrumentType" runat="server" Display="Dynamic"
                                            ControlToValidate="cmbInstrumentType" ForeColor="Red" 
                                            ValidationGroup="vgAddMeasurementDevice"><ucValidationContainer:ValidationContainer ID="rfvInstrumentContainer" runat="server"
                                                ValidationMsg="Please select instrument type." Size="200" />
</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                             <tr>
                                <td style="width: 130px" align="right">
                                    <span style="font-weight: bold;"><asp:Localize Text="Description" 
                                        runat="server" ID="locDescription" meta:resourcekey="locDescriptionResource1"></asp:Localize>
                                    </span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td style="width: 400px">
                                    <div style="float: left;">
                                        <asp:TextBox ID="txtDescription" runat="server" Width="200px" Height="19px" 
                                            MaxLength="100" meta:resourcekey="txtDescriptionResource1"></asp:TextBox>
                                    </div>
                                    <div style="float: left">
                                      
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px" align="right">
                                    <span style="font-weight: bold;"><asp:Localize runat="server" Text="Is Active?" 
                                        ID="locIsActive" meta:resourcekey="locIsActiveResource1"></asp:Localize></span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td style="width: 400px">
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
                                            <asp:LinkButton ID="lbtnSave" runat="server" CssClass="gray_bt" ValidationGroup="vgAddMeasurementDevice"
                                                OnClick="lbtnSave_Click">
                                                    <span> <asp:Localize Text="Save" runat="server" ID="locSave" 
                                                meta:resourcekey="locSaveResource1"></asp:Localize>
 </span>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="gray_bt" 
                                                PostBackUrl="~/MeasurementDeviceList.aspx">
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
