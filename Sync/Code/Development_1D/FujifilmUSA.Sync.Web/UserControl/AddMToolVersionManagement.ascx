<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddMToolVersionManagement.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.AddMToolVersionManagement" %>
<%@ Register Src="~/UserControl/AdminStatusbar.ascx" TagName="AdminStatusbar" TagPrefix="ucAdminStatusbar" %>
<%@ Register Src="~/UserControl/ValidationContainer.ascx" TagName="ValidationContainer" TagPrefix="ucValidationContainer" %>
<script src="JS/CommonFunctions.js?version=4" type="text/javascript"></script>
<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle" style="line-height: 34px;">
    <div class="head">
        <h1><asp:Localize ID="locBranchManagement" runat="server" Text="MTool Version Management" meta:resourcekey="locMtoolManagementResource1"></asp:Localize></h1>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px;">
        <center>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <center>
                        <fieldset style="width: 60%;">
                            <legend>
                                <asp:Label ID="lblHeader" runat="server" Text="Add MTool Version" meta:resourcekey="locAddHeaderMtoolVersionResource1"></asp:Label>
                            </legend>
                            <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" 
                                Visible="False" />
                            <table>
                                 <tr>
                                <td align="right">
                                     <span style="font-weight: bold;"><asp:Localize ID="locOsPlatForm" runat="server" Text="OS PlatForm" meta:resourcekey="locOsPlatFormResource1" ></asp:Localize>
                                         <span style="color: Red;">*</span>
                                      </span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <asp:DropDownList ID="cmbOSPlatForm" runat="server" DataValueField="Key" DataTextField="Value"
                                            CssClass="select" Width="400px" AutoPostBack="false">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvOSPlatForm" runat="server" Display="Dynamic" ControlToValidate="cmbOSPlatForm"
                                            ForeColor="Red" ValidationGroup="vgAddMToolVersion" InitialValue="0"><ucValidationContainer:ValidationContainer ID="rfvOSPlatFormContainer" runat="server"
                                                ValidationMsg="Please select OS PlatForm." Size="200"/>
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize Text="Latest Version" 
                                            ID="locLatestVersion" runat="server" meta:resourcekey="locLatestVersionResource1"></asp:Localize><span style="color: Red;">*</span>
                                        </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;: &nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtLatestVersion" runat="server" MaxLength="50" Width="400px" 
                                                Height="19px"></asp:TextBox>
                                        </div>
                                        <div style="float: left">
                                           <asp:RequiredFieldValidator ID="rfvLatestVersion" runat="server" Display="Dynamic" ControlToValidate="txtLatestVersion"
                                                ForeColor="Red" ValidationGroup="vgAddMToolVersion"><ucValidationContainer:ValidationContainer ID="ValidationContainer2" runat="server"
                                                    ValidationMsg="Please provide latest version." Size="200"/>
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;">
                                            <asp:Localize Text="Min Req. Version" 
                                            Id="locMinReqVersion" runat="server" meta:resourcekey="locMinReqVersionResource1"></asp:Localize><span style="color: Red;">*</span> </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtMinReqVersion" runat="server" MaxLength="50" Width="400px" 
                                                Height="19px"></asp:TextBox>
                                        </div>
                                        <div style="float: left">
                                            <asp:RequiredFieldValidator ID="rfvMinReqVersion" runat="server" Display="Dynamic" ControlToValidate="txtMinReqVersion"
                                                ForeColor="Red" ValidationGroup="vgAddMToolVersion"><ucValidationContainer:ValidationContainer ID="rfvMinReqVersionContainer" runat="server"
                                                    ValidationMsg="Please provide minimum required version." Size="200"/>
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize Text="URL location" ID="locURLlocation" 
                                            runat="server" meta:resourcekey="locURLlocationResource1"></asp:Localize><span style="color: Red;">*</span> </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtURLlocation" runat="server" MaxLength="50" Width="400px" 
                                                Height="19px"></asp:TextBox>
                                        </div>
                                        <div style="float: left">
                                            <asp:RequiredFieldValidator ID="rfvURLlocation" runat="server" Display="Dynamic" ControlToValidate="txtURLlocation"
                                                ForeColor="Red" ValidationGroup="vgAddMToolVersion"><ucValidationContainer:ValidationContainer ID="ValidationContainer1" runat="server"
                                                    ValidationMsg="Please provide url location." Size="200" />
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                  <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize Text="Update Information" runat="server" 
                                            ID="locUpdateInfo" meta:resourcekey="locUpdateInfoResource1"></asp:Localize></span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtUpdateInfo" runat="server" Width="400px" 
                                                Height="19px"></asp:TextBox>
                                        </div>
                                        <div style="float: left">
                                           
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize ID="locIsUsed" runat="server" 
                                            Text="Is Used?" meta:resourcekey="locIsUsedResource1"></asp:Localize></span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:CheckBox ID="chkIsUsed" runat="server" Checked="True"/>
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
                                    <td align="left">
                                        <div class="text">
                                            <div style="padding-left: 3px;">
                                                <asp:LinkButton ID="lbtnSave" runat="server" CssClass="gray_bt" ValidationGroup="vgAddMToolVersion"
                                                    OnClick="lbtnSave_Click">
                                                    <span> <asp:Localize Text="Save" ID="locSave" runat="server" 
                                                    meta:resourcekey="locSaveResource1"></asp:Localize> </span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="gray_bt" 
                                                    PostBackUrl="~/MToolVersionManagement.aspx">
                                                    <span> <asp:Localize Text="Cancel" ID="locCancel" runat="server" 
                                                    meta:resourcekey="locCancelResource1"></asp:Localize> </span>
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
        </center>
    </div>
</div>
<div class="admin_bottom">
    &nbsp;</div>
<div class="clear">
</div>