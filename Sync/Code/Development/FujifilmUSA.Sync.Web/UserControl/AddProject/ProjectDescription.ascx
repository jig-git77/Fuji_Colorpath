<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectDescription.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.ProjectDescription" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<%@ Register Src="~/UserControl/ValidationContainer.ascx" TagName="ValidationContainer"
    TagPrefix="ucValidationContainer" %>
<div class="head">
    <h1 id="hTitle" runat="server">
        New Print Condition - Description
    </h1>
    <asp:LinkButton ID="lnkbtnPrintDescription" runat="server" CssClass="button" ValidationGroup="vgDescription"
        OnClick="lnkbtnPrintDescription_Click">
        <span>
            <asp:Image ID="imgAdd" runat="server" ImageUrl="~/Images/new_project_icon.png" />
            Target ColorSpace</span></asp:LinkButton>
</div>
<div class="formDeviceNoLine" style="display: none;">
    <ucStatusbar:Statusbar ID="fujiStatusbar" runat="server" Visible="false" />
    <div class="clear">
    </div>
</div>
<div class="listDevice">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" border="0" width="97%">
                <tr id="trListHeader" runat="server">
                    <td>
                        <asp:Panel ID="panelAddProjectDescription" runat="server" BorderStyle="None">
                            <table>
                                <tr>
                                    <td style="width: 130px">
                                        <span>Press:</span>
                                    </td>
                                    <td style="width: 400px">
                                        <asp:TextBox ID="txtPress" runat="server" Width="200px" Height="19px" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span>Device Family:</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDeviceFamily" runat="server" Width="200px" Height="19px" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span>Media:<span style="color: Red;">*</span></span>
                                    </td>
                                    <td>
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtMedia" runat="server" Width="200px" Height="19px" MaxLength="50"></asp:TextBox>
                                        </div>
                                        <div style="float: left">
                                            <asp:RequiredFieldValidator ID="rfvMedia" runat="server" Display="Dynamic" ControlToValidate="txtMedia"
                                                ForeColor="Red" ValidationGroup="vgDescription">
                                                <ucValidationContainer:ValidationContainer ID="rfvcMediaContainer" runat="server"
                                                    ValidationMsg="Please provide media name." Size="200" />
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span>Colorant:<span style="color: Red;">*</span></span>
                                    </td>
                                    <td>
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtColorant" runat="server" Width="200px" Height="19px" MaxLength="50  "></asp:TextBox>
                                        </div>
                                        <div style="float: left">
                                            <asp:RequiredFieldValidator ID="rfvColorant" runat="server" Display="Dynamic" ControlToValidate="txtColorant"
                                                ForeColor="Red" ValidationGroup="vgDescription">
                                                <ucValidationContainer:ValidationContainer ID="rfvColorantContainer" runat="server"
                                                    ValidationMsg="Please provide colorant name." Size="200" />
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span>Rip Settings:<span style="color: Red;">*</span></span>
                                    </td>
                                    <td>
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtRipSetting" runat="server" Width="200px" Height="19px" MaxLength="50"></asp:TextBox>
                                        </div>
                                        <div style="float: left">
                                            <asp:RequiredFieldValidator ID="rfvRipSetting" runat="server" Display="Dynamic" ControlToValidate="txtRipSetting"
                                                ForeColor="Red" ValidationGroup="vgDescription">
                                                <ucValidationContainer:ValidationContainer ID="rfvRipSettingsContainer" runat="server"
                                                    ValidationMsg="Please provide rip settings." Size="200" />
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <span>Notes:</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNotes" runat="server" Width="300px" Height="100px" TextMode="MultiLine"
                                            MaxLength="500"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span>Technician Name:</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTechnicianName" runat="server" Width="200px" Height="19px" MaxLength="30"></asp:TextBox>
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
