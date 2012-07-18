<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectDescription.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.ProjectDescription" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<%@ Register Src="~/UserControl/ValidationContainer.ascx" TagName="ValidationContainer"
    TagPrefix="ucValidationContainer" %>
<div class="head">
    <h1 id="hTitle" runat="server"><asp:Localize runat="server" 
            Text="New Print Condition - Description" ID="locNewPrintCondition" 
            meta:resourcekey="locNewPrintConditionResource1"></asp:Localize></h1>
    <asp:LinkButton ID="lnkbtnPrintDescription" runat="server" CssClass="button" ValidationGroup="vgDescription"
        OnClick="lnkbtnPrintDescription_Click">
        <span>
            <asp:Image ID="imgAdd" runat="server" 
        ImageUrl="~/Images/new_project_icon.png" meta:resourcekey="imgAddResource1" />
<asp:Localize runat="server" ID="locTargetColorSpace" Text="Target ColorSpace" 
        meta:resourcekey="locTargetColorSpaceResource1"></asp:Localize>

</span></asp:LinkButton>
</div>
<div class="formDeviceNoLine" style="display: none;">
    <ucStatusbar:Statusbar ID="fujiStatusbar" runat="server" Visible="False" />
    <div class="clear">
    </div>
</div>
<div class="listDevice">
<asp:UpdatePanel runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" border="0" width="97%">
                <tr id="trListHeader" runat="server">
                    <td runat="server">
                        <asp:Panel ID="panelAddProjectDescription" runat="server" BorderStyle="None" 
                            meta:resourcekey="panelAddProjectDescriptionResource1">
                            <table>
                                <tr>
                                    <td style="width: 130px">
                                        <span><asp:Localize runat="server" Text="Press:" ID="locPress" 
                                            meta:resourcekey="locPressResource1"></asp:Localize></span>
                                    </td>
                                    <td style="width: 400px">
                                        <asp:TextBox ID="txtPress" runat="server" Width="200px" Height="19px" 
                                            Enabled="False" meta:resourcekey="txtPressResource1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span><asp:Localize runat="server" Text="Device Family:" ID="locDeviceFamily" 
                                            meta:resourcekey="locDeviceFamilyResource1"></asp:Localize></span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDeviceFamily" runat="server" Width="200px" Height="19px" 
                                            Enabled="False" meta:resourcekey="txtDeviceFamilyResource1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span><asp:Localize runat="server" Text="Media:" ID="locMedia" 
                                            meta:resourcekey="locMediaResource1"></asp:Localize><span style="color: Red;">*</span></span>
                                    </td>
                                    <td>
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtMedia" runat="server" Width="200px" Height="19px" 
                                                MaxLength="50" meta:resourcekey="txtMediaResource1"></asp:TextBox>
                                        </div>
                                        <div style="float: left">
                                            <asp:RequiredFieldValidator ID="rfvMedia" runat="server" Display="Dynamic" ControlToValidate="txtMedia"
                                                ForeColor="Red" ValidationGroup="vgDescription"><ucValidationContainer:ValidationContainer ID="rfvcMediaContainer" runat="server"
                                                    ValidationMsg="Please provide media name." Size="200" />
</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span><asp:Localize runat="server" Text="Colorant:" ID="locColorant" 
                                            meta:resourcekey="locColorantResource1"></asp:Localize><span style="color: Red;">*</span></span>
                                    </td>
                                    <td>
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtColorant" runat="server" Width="200px" Height="19px" 
                                                MaxLength="50" meta:resourcekey="txtColorantResource1"></asp:TextBox>
                                        </div>
                                        <div style="float: left">
                                            <asp:RequiredFieldValidator ID="rfvColorant" runat="server" Display="Dynamic" ControlToValidate="txtColorant"
                                                ForeColor="Red" ValidationGroup="vgDescription"><ucValidationContainer:ValidationContainer ID="rfvColorantContainer" runat="server"
                                                    ValidationMsg="Please provide colorant name." Size="200" />
</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span><asp:Localize runat="server" Text="Rip Settings:" ID="locRipSettings" 
                                            meta:resourcekey="locRipSettingsResource1"></asp:Localize><span style="color: Red;">*</span></span>
                                    </td>
                                    <td>
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtRipSetting" runat="server" Width="200px" Height="19px" 
                                                MaxLength="50" meta:resourcekey="txtRipSettingResource1"></asp:TextBox>
                                        </div>
                                        <div style="float: left">
                                            <asp:RequiredFieldValidator ID="rfvRipSetting" runat="server" Display="Dynamic" ControlToValidate="txtRipSetting"
                                                ForeColor="Red" ValidationGroup="vgDescription"><ucValidationContainer:ValidationContainer ID="rfvRipSettingsContainer" runat="server"
                                                    ValidationMsg="Please provide rip settings." Size="200" />
</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <span><asp:Localize Text="Notes:" runat="server" ID="locNotes" 
                                            meta:resourcekey="locNotesResource1"></asp:Localize></span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNotes" runat="server" Width="300px" Height="100px" TextMode="MultiLine"
                                            MaxLength="500" meta:resourcekey="txtNotesResource1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span><asp:Localize runat="server" Text="Technician Name:" 
                                            ID="locTechnicianName" meta:resourcekey="locTechnicianNameResource1"></asp:Localize></span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTechnicianName" runat="server" Width="200px" Height="19px" 
                                            MaxLength="30" meta:resourcekey="txtTechnicianNameResource1"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
