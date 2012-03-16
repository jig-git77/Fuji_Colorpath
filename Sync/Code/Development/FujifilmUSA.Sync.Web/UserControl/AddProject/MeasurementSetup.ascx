<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MeasurementSetup.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.MeasurementSetup" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<div class="head">
    <h1 id="hTitle" runat="server">
        New Print Condition - Measurement Setup
    </h1>
    <asp:LinkButton ID="lnkbtnMeasurementSetup" runat="server" ValidationGroup="vgMeasurementSetup"
        CssClass="button" OnClick="lnkbtnMeasurementSetup_Click">
        <span>
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Search.png" />
            Preview</span></asp:LinkButton>
</div>
<div class="formDeviceNoLine" style="display: none;">
    <div class="clear">
    </div>
</div>
<div class="listDevice" style="margin-left: 10px;">
    <ucStatusbar:Statusbar ID="ucStatusbar1" runat="server" Visible="false" />
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
                                    <div class="text" style="float: left">
                                        Spectrophotometer Options
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="panelSpectrophotometerOptions" Style="padding: 5px" runat="server"
                                BorderStyle="None">
                                <table>
                                    <tr>
                                        <td style="width: 140px">
                                            <span>Measurement Device:<span style="color: Red;">&nbsp*</span> </span>
                                        </td>
                                        <td>
                                            <div style="float: left;">
                                                <asp:DropDownList ID="cmbMeasurementDevice" AutoPostBack="true" runat="server" Width="200px" OnSelectedIndexChanged="cmbMeasurementDevice_SelectedIndexChanged" CssClass="select">
                                                </asp:DropDownList>
                                            </div>
                                            <div style="float: left">
                                                <asp:RequiredFieldValidator ID="rfvMeasurementDevice" runat="server" Display="Dynamic"
                                                    ControlToValidate="cmbMeasurementDevice" ForeColor="Red" ValidationGroup="vgMeasurementSetup">
                                              <div class="tooltip_round_box" style="width: 200px;">
                                            <div class="tooltip_arrow" style="margin-top: -3px;">
                                                &nbsp;</div>
                                            <div class="tooltip_top_left">
                                            </div>
                                            <div class="tooltip_top_right">
                                            </div>
                                            <div class="tooltip_inside">
                                                Please select measurement device.
                                            </div>
                                            <div class="tooltip_bottom_left">
                                            </div>
                                            <div class="tooltip_bottom_right">
                                            </div>
                                        </div>
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="clear">
                                            </div>
                                        </td>
                                    </tr>
                                    <%-- <tr>
                                        <td colspan="2">
                                            <label>
                                                Measurement Settings</label>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td>
                                            <span>Illuminant:</span>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbIlluminant" runat="server" Width="200px" CssClass="select">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span>Observer:</span>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbObserver" runat="server" Width="200px" CssClass="select">
                                            </asp:DropDownList>
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
                                    <div class="text" style="float: left;">
                                        <label>
                                            Alignment Chart</label>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="panelAlignmentChart" Style="margin-left: 5px; margin-right: 5px;"
                                runat="server" BorderStyle="None">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <span>Alignment Chart:</span>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbAlignmentChart" runat="server" Width="200px" CssClass="select">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:TextBox ID="txtChartPriview" runat="server" Width="99%" Height="200px" TextMode="MultiLine"
                                                Enabled="false" ReadOnly="true" Text="chart priview">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>
                    </div>
                    <div class="section_bottom">
                        &nbsp;</div>
                </div>
            </td>
        </tr>
    </table>
</div>
