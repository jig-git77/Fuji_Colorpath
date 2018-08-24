<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="AddDeviceFamily.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.AddDeviceFamily" %>
<%@ Register Src="~/UserControl/AdminStatusbar.ascx" TagName="AdminStatusbar" TagPrefix="ucAdminStatusbar" %>
<%@ Register Src="~/UserControl/ValidationContainer.ascx" TagName="ValidationContainer"
    TagPrefix="ucValidationContainer" %>
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
                        <legend style="margin-left: 42% !important;">
                            <asp:Label ID="lblHeader" runat="server" Text="Add Device Family" 
                                meta:resourcekey="lblHeaderResource1"></asp:Label>
                        </legend>
                        <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" 
                            Visible="False" />
                        <table>
                            <tr>
                                <td align="right">
                                    <span style="font-weight: bold;"><asp:Localize Text="Device Family Name" 
                                        ID="locDeviceFamilyName" runat="server" 
                                        meta:resourcekey="locDeviceFamilyNameResource1"></asp:Localize><span style="color: Red;">*</span>
                                    </span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <asp:TextBox ID="txtDeviceFamilyName" runat="server" MaxLength="25" Width="370px"
                                            Height="19px" meta:resourcekey="txtDeviceFamilyNameResource1"></asp:TextBox>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvDeviceFamilyName" runat="server" Display="Dynamic"
                                            ControlToValidate="txtDeviceFamilyName" ForeColor="Red" 
                                            ValidationGroup="vgAddDeviceFamily">
                                            <ucValidationContainer:ValidationContainer ID="Validationcontainer1" runat="server"
                                                ValidationMsg="Please provide device family name." Size="225" ResourceValidationMsg="DeviceFamilyRequiredResource"/>
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <span style="font-weight: bold;"><asp:Localize Text="Is Active?" runat="server" 
                                        ID="locIsActive" meta:resourcekey="locIsActiveResource1"></asp:Localize></span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
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
                                            <asp:LinkButton ID="lbtnSave" runat="server" CssClass="gray_bt" ValidationGroup="vgAddDeviceFamily"
                                                OnClick="lbtnSave_Click">
                                                    <span> <asp:Localize Text="Save" runat="server" ID="locSave" 
                                                meta:resourcekey="locSaveResource1"></asp:Localize> 
 </span>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="gray_bt" 
                                                PostBackUrl="~/DeviceManagement.aspx">
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
