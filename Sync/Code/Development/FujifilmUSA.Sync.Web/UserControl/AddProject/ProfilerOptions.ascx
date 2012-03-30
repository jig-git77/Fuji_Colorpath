<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProfilerOptions.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.ProfilerOptions" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<div class="head">
    <h1 id="hTitle" runat="server">
        New Print Condition - Profiler Options
    </h1>
    <asp:LinkButton ID="lnkbtnProfilerOptions" runat="server" CssClass="button" OnClick="lnkbtnProfilerOptions_Click">
        <span>
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/new_project_icon.png" />
            Measurement Setup</span></asp:LinkButton>
</div>
<div class="formDeviceNoLine" style="display: none;">
    <div class="clear">
    </div>
</div>
<div class="listDevice" style="margin-left: 10px;">
    <ucStatusbar:Statusbar ID="fujiStatusbar" runat="server" Visible="false" />
    <table cellpadding="0" cellspacing="10" border="0" width="97%">
        <tr id="trListHeader" runat="server">
            <td>
                <div style="display: block;" class="contantbox tabcontant">
                    <div class="section_top">
                        &nbsp;</div>
                    <div class="section_middle">
                        <div class="combopart">
                            <div class="header">
                                <div class="content">
                                    <div class="text" style="float: left;">
                                        <span>Options Presets</span>
                                    </div>
                                    <div style="float: right">
                                        <%--  <asp:LinkButton ID="lbtnMeasurementSetup" runat="server" CssClass="gray_bt" OnClick="lbtnMeasurementSetup_Click">
                <span>Measurement Setup</span>
                                    </asp:LinkButton>--%>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="panelAddProjectDescription" Style="padding: 5px" runat="server" BorderStyle="None">
                                <table>
                                    <tr>
                                        <td style="width: 100px">
                                            <span>Selected Presets:</span>
                                        </td>
                                        <td style="width: 450px">
                                            <asp:DropDownList ID="cmbSelectedPresets" runat="server" Width="200px" CssClass="select"
                                                DataValueField="Key" DataTextField="Value" OnSelectedIndexChanged="cmbSelectedPresets_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" colspan="2">
                                            <span>Preset Description:</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:TextBox ID="txtProfileDescription" runat="server" Width="120%" Height="200px"
                                                TextMode="MultiLine" Enabled="false" ReadOnly="true" Text="This preset is best used for analog devices ……">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <div class="clear">
                            </div>
                        </div>
                    </div>
                    <div class="section_bottom">
                        &nbsp;</div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div style="display: block;" class="contantbox tabcontant">
                    <div class="section_top">
                        &nbsp;</div>
                    <div class="section_middle">
                        <div class="combopart">
                            <div class="header">
                                <div class="content">
                                    <div class="text">
                                        <div style="float: left">
                                        </div>
                                        <div style="float: left;">
                                            <label>
                                                Advanced Options</label>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="panelAdvancedOptions" Style="padding: 5px" runat="server" BorderStyle="None">
                                Advanced Options to be defined in next version....
                            </asp:Panel>
                        </div>
                    </div>
                    <div class="tab_bottom">
                        &nbsp;</div>
                </div>
            </td>
        </tr>
    </table>
</div>
