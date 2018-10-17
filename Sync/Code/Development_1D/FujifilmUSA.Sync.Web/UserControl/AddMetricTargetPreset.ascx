<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddMetricTargetPreset.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.AddMetricTargetPreset" %>
<%@ Register Src="~/UserControl/AdminStatusbar.ascx" TagName="AdminStatusbar" TagPrefix="ucAdminStatusbar" %>
<%@ Register Src="~/UserControl/ValidationContainer.ascx" TagName="ValidationContainer" TagPrefix="ucValidationContainer" %>
<script src="JS/CommonFunctions.js?version=4" type="text/javascript"></script>
<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle" style="line-height: 34px;">
    <div class="head">
        <h1><asp:Localize ID="locMetricTargetPreset" runat="server" Text="Metric Target Preset Management" 
                meta:resourcekey="locMetricTargetPresetResource1"></asp:Localize></h1>
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
                                <asp:Label ID="lblHeader" runat="server" Text="Add Metric Target Preset"  meta:resourcekey="lblHeaderResource1">
                                  </asp:Label>
                            </legend>
                            <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" 
                                Visible="False" />
                            <table>
                                 <tr>
                                <td align="right">
                                     <span style="font-weight: bold;"><asp:Localize ID="locRegion" runat="server" 
                                         Text="Region" meta:resourcekey="locRegionResource1"></asp:Localize>
                                        </span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <asp:DropDownList ID="cmbRegion" runat="server" DataValueField="Key" DataTextField="Value"
                                            CssClass="select" Width="400px" AutoPostBack="True" 
                                            OnSelectedIndexChanged="cmbRegion_SelectedIndexChanged" 
                                            meta:resourcekey="cmbRegionResource1">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left">
                                    
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize Text="Company Name" 
                                            ID="locCompanyName" runat="server" meta:resourcekey="locCompanyNameResource1"></asp:Localize>
                                        </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;: &nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:DropDownList ID="cmbCompanyName" runat="server" Width="400px" 
                                                CssClass="select" meta:resourcekey="cmbCompanyNameResource1" >                                              
                                            </asp:DropDownList>
                                        </div>
                                        <div style="float: left">
                                           
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize Text="Preset Name" 
                                            Id="locPresetName" runat="server" 
                                            meta:resourcekey="locPresetNameResource1"></asp:Localize><span style="color: Red;">*</span> </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtPresetName" runat="server" MaxLength="50" Width="400px" 
                                                Height="19px" meta:resourcekey="txtPresetNameResource1" onkeypress="return validateText(this)"></asp:TextBox>
                                        </div>
                                        <div style="float: left">
                                            <asp:RequiredFieldValidator ID="rfvPresetName" runat="server" Display="Dynamic" ControlToValidate="txtPresetName"
                                                ForeColor="Red" ValidationGroup="vgAddMetricTargetPreset"><ucValidationContainer:ValidationContainer ID="rfvPresetContainer" runat="server"
                                                    ValidationMsg="Please provide Preset name." Size="200" ResourceValidationMsg="PresetNameRequiredResource"/>
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize Text="Description" ID="locDescription" 
                                            runat="server" meta:resourcekey="locDescriptionResource1"></asp:Localize>
                                        </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtDescription" runat="server" MaxLength="50" Width="400px" 
                                                Height="52px" meta:resourcekey="txtDescriptionResource1" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                       
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                   <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize Text="ΔE Method" runat="server" 
                                            ID="locEType" meta:resourcekey="locETypeResource1"></asp:Localize>
                                            <span style="color: Red;">*</span>
                                       </span></td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:DropDownList ID="cmbEType" runat="server">
                                                <asp:ListItem Selected="True" Value="ΔE" Text="ΔE"></asp:ListItem>
                                                <asp:ListItem Value="ΔE2000" Text="ΔE2000"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div style="float: left">
                                           
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                  <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize Text="Validation Type" runat="server" 
                                            ID="locValidationType" meta:resourcekey="locValidationTypeResource1"></asp:Localize>
                                            <span style="color: Red;">*</span>
                                        </span></td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:DropDownList ID="cmbValidationType" runat="server">
                                                <asp:ListItem Selected="True" Value="0" Text="Default">Default</asp:ListItem>
                                                <asp:ListItem Value="1" Text="G7">G7</asp:ListItem>
                                                <asp:ListItem Value="2" Text="ISO">ISO</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div style="float: left">
                                           
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="right">
                                      
                                    </td>
                                    <td>
                                      
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:CheckBoxList ID="chklist_PresetType" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem>IsSync1D</asp:ListItem>
                                                <asp:ListItem>Validation_1D</asp:ListItem>
                                                <asp:ListItem>Validation_4D</asp:ListItem>
                                            </asp:CheckBoxList>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize ID="locIsActive" runat="server" 
                                            Text="Is Active?" meta:resourcekey="locIsActiveResource1"></asp:Localize></span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
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
                                    <td align="left">
                                        <div class="text">
                                            <div style="padding-left: 3px;">
                                                <asp:LinkButton ID="lbtnSave" runat="server" CssClass="gray_bt" ValidationGroup="vgAddMetricTargetPreset"
                                                    OnClick="lbtnSave_Click">
                                                    <span> <asp:Localize Text="Save" ID="locSave" runat="server" 
                                                    meta:resourcekey="locSaveResource1"></asp:Localize> </span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="gray_bt" 
                                                    onclick="lbtnCancel_Click">
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
