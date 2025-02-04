<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddDeviceProfilerSettings.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.AddDeviceProfilerSettings" %>
<%@ Register Src="~/UserControl/AdminStatusbar.ascx" TagName="AdminStatusbar" TagPrefix="ucAdminStatusbar" %>
<%@ Register Src="~/UserControl/ValidationContainer.ascx" TagName="ValidationContainer"
    TagPrefix="ucValidationContainer" %>
<script src="JS/CommonFunctions.js?version=4" type="text/javascript"></script>
<style type="text/css">    
     .breakWord50
     {
            max-width: 50px !important;
            word-break: break-all !important;
            word-wrap: break-word !important;
            vertical-align: top;
            line-height: 15px;
     }  
</style>

<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle" style="line-height: 34px;">
    <div class="head">
        <h1><asp:Localize runat="server" Text="Profiler Option Presets Management" 
                ID="locProfilerPresetsMgmt" meta:resourcekey="locReferenceChartMgmtResource1"></asp:Localize></h1>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 0px;">
        <center>
            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
            <center>
                <fieldset style="width: 92%;">
                    <legend>
                        <asp:Label ID="lblHeader" runat="server" Text="Add Device Family Profiler Presets" 
                            meta:resourcekey="lblHeaderResource1"></asp:Label>
                    </legend>
                    <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" 
                        Visible="False" />
                    <table width="98%">
                        <tr>
                            <td align="right" style="width:350px;">
                                <span style="font-weight: bold;"><asp:Localize Text="Project Type" 
                                    runat="server" ID="locProjectType" meta:resourcekey="locProjectTypeResource1"></asp:Localize><span style="color: Red;">*</span>
                                </span>
                            </td>
                            <td style="width:15px;">
                                <b>&nbsp;: &nbsp;</b>
                            </td>
                            <td style="width:100px;">
                                <div style="float: left;">
                                    <asp:DropDownList ID="ddlProjectType" runat="server" Width="400px" CssClass="select"
                                        DataValueField="Key" DataTextField="Value" 
                                        meta:resourcekey="ddlProjectTypeResource1">
                                    </asp:DropDownList>
                                </div>
                                <div style="float: left">
                                    <asp:RequiredFieldValidator ID="rfvProjectType" runat="server" Display="Dynamic"
                                        ControlToValidate="ddlProjectType" ForeColor="Red" 
                                        ValidationGroup="vgAddProfiles">
                                        <ucValidationContainer:ValidationContainer ID="rfvcProjectTypeContainer" runat="server"
                                            ValidationMsg="Please select project type." Size="200" ResourceValidationMsg="ProjectTypeRequiredResource"/>
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="clear">
                                </div>
                            </td>
                        </tr>
                    
                        
                           <tr>
                            <td align="right" style="width:350px;">
                                <span style="font-weight: bold;"><asp:Localize Text="Device Family" 
                                    runat="server" ID="locDeviceFamily" meta:resourcekey="locDeviceFamilyResource1"></asp:Localize><span style="color: Red;">*</span>
                                </span>
                            </td>
                            <td style="width:15px;">
                                <b>&nbsp;: &nbsp;</b>
                            </td>
                            <td style="width:100px;">
                                <div style="float: left;">
                                    <asp:DropDownList ID="ddlDeviceFamily" runat="server" Width="400px" CssClass="select"
                                        DataValueField="Key" DataTextField="Value" 
                                        meta:resourcekey="ddlDeviceFamily">
                                    </asp:DropDownList>
                                </div>
                                <div style="float: left">
                                    <asp:RequiredFieldValidator ID="rfvDeviceFamily" runat="server" Display="Dynamic"
                                        ControlToValidate="ddlDeviceFamily" ForeColor="Red" 
                                        ValidationGroup="vgAddProfiles">
                                        <ucValidationContainer:ValidationContainer ID="rfvcDeviceFamilyContainer" runat="server"
                                            ValidationMsg="Please select Device Family." Size="200" ResourceValidationMsg="DeviceFamilyRequiredResource" />
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="clear">
                                </div>
                            </td>
                        </tr>

                        <tr>
                            <td align="right">
                                <span style="font-weight: bold;"><asp:Localize Text="Preset Name" runat="server" 
                                    ID="locPresetName" meta:resourcekey="locPresetNameResource1"></asp:Localize><span style="color: Red;">*</span> </span>
                            </td>
                            <td>
                                <b>&nbsp;:&nbsp;</b>
                            </td>
                            <td align="left">
                                <div style="float: left;"> 
                                                               
                                    <asp:TextBox ID="txtPresetName" runat="server" MaxLength="50" Width="400px" 
                                        Height="19px" meta:resourcekey="txtPresetNameResource1" onkeypress="return validateText(event)"></asp:TextBox>
                                      <asp:HiddenField ID="hdnRefChartID" runat="server" Value="0" />
                                </div>
                                <div style="float: left">
                                    <asp:RequiredFieldValidator ID="rfvPresetName" runat="server" Display="Dynamic" ControlToValidate="txtPresetName"
                                        ForeColor="Red" ValidationGroup="vgAddProfiles">
                                        <ucValidationContainer:ValidationContainer ID="rfvPresetNameContainer" runat="server"
                                            ValidationMsg="Please provide Preset name." Size="200" ResourceValidationMsg="PresetNameRequiredResource" />
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="clear">
                                </div>
                            </td>
                        </tr>

                        
                        <tr>
                            <td align="right">
                                <span style="font-weight: bold;"><asp:Localize Text="Preset Description" runat="server" 
                                    ID="Localize1" meta:resourcekey="locPresetDescriptionResource1"></asp:Localize><span style="color: Red;">*</span> </span>
                            </td>
                            <td>
                                <b>&nbsp;:&nbsp;</b>
                            </td>
                            <td align="left">
                                <div style="float: left;"> 
                                                               
                                    <asp:TextBox ID="txtPresetDesc" runat="server" MaxLength="290" Width="400px" 
                                        Height="50px" TextMode="MultiLine"  meta:resourcekey="txtPresetDescResource1"></asp:TextBox>
                                      <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                                </div>
                                <div style="float: left">
                                    <asp:RequiredFieldValidator ID="rfvPresetDesc" runat="server" Display="Dynamic" ControlToValidate="txtPresetDesc"
                                        ForeColor="Red" ValidationGroup="vgAddProfiles">
                                        <ucValidationContainer:ValidationContainer ID="rfvPresetDescContainer" runat="server"
                                            ValidationMsg="Please provide Preset Description." Size="200" ResourceValidationMsg="PresetDescriptionRequiredResource" />
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="clear">
                                </div>
                            </td>
                        </tr>
                         <tr>
                            <td align="right">
                                <span style="font-weight: bold;"><asp:Localize Text="Is Active" runat="server" 
                                    ID="Localize2" meta:resourcekey="locIsActivePresetResource1"></asp:Localize></span>
                            </td>
                            <td>
                                <b>&nbsp;:&nbsp;</b>
                            </td>
                            <td align="left">
                                <div style="float: left;"> 
                                                               
                                    <asp:CheckBox ID="chkIsActive" runat="server" MaxLength="290" Width="400px" style ="margin-top:24px;"
                                        Height="50px" TextMode="MultiLine"  meta:resourcekey="chkIsActivePresetResource1"></asp:CheckBox>
                                      <asp:HiddenField ID="HiddenField2" runat="server" Value="0" />
                                     <asp:HiddenField ID="hdnDeviceFamilyProfilerOptionID" runat="server" Value="0" />
                                </div>
                                <div class="clear">
                                </div>
                            </td>
                        </tr>
                            <tr>
                            <td align="right">
                                <span style="font-weight: bold;">
                                <asp:Localize Text="Upload Profiler Settings (only csv)" runat="server" 
                                    ID="locUploadPofileFile" meta:resourcekey="locUploadProfilerResource1"></asp:Localize><span style="color: Red;">*</span>
                                </span>
                            </td>
                            <td>
                                <b>&nbsp;: &nbsp;</b>
                            </td>
                            <td align="left">
                                <div style="float: left;">
                                    <telerik:RadAsyncUpload runat="server" ID="fnUploadProfileSettings"
                                        MaxFileInputsCount="1" PostbackTriggers="lbtnSave" 
                                        meta:resourcekey="fncUploadProfileSettingsResource1" >
                                    </telerik:RadAsyncUpload>                                 
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
                                        <asp:LinkButton ID="lbtnSave" runat="server" CssClass="gray_bt" ValidationGroup="vgAddProfiles"
                                            OnClick="lbtnSave_Click">
                                                    <span> <asp:Localize runat="server" Text="Save" ID="locSave" 
                                            meta:resourcekey="locSaveResource1" ></asp:Localize> 
                                             </span>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="gray_bt" 
                                            PostBackUrl="~/ProfilerOptionPresetsManagement.aspx">
                                                    <span> <asp:Localize runat="server" Text="Cancel" ID="locCancel" 
                                            meta:resourcekey="locCancelResource1" ></asp:Localize> 
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
            <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
        </center>
    </div>
</div>
<div class="admin_bottom">
    &nbsp;</div>
<div class="clear">
</div>
