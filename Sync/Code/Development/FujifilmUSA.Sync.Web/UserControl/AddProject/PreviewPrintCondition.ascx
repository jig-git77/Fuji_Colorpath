<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PreviewPrintCondition.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.PreviewPrintCondition" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<script type="text/javascript" language="javascript">
    function ShowConfirmSaveWindow() {
        var oWnd = $find("<%= ConfirmSaveWindow.ClientID %>");
        oWnd.show();
    }
</script>
<script language="javascript" type="text/javascript">
    function Close() {
        var oWnd = $find("<%= ConfirmSaveWindow.ClientID %>");
        oWnd.close();
        return false;
    }
</script>
<div class="head">
    <h1 id="hTitle" runat="server">
        New Print Condition - Save Print Condition
    </h1>
    <asp:LinkButton ID="lnkbtnSavePrintCondition" runat="server" CssClass="button" OnClientClick=" javascript:ShowConfirmSaveWindow();return false;">
        <span>
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/save_icon.png" />
            Comfirm Save</span></asp:LinkButton>
</div>
<div class="formDeviceNoLine" style="display: none;">
    <div class="clear">
    </div>
</div>
<div class="listDevice" style="margin-left: 10px">
    <ucStatusbar:Statusbar ID="fujiStatusbar" runat="server" Visible="false" />
    <table cellpadding="0" cellspacing="10" border="0">
        <tr>
            <td valign="top" style="width: 320px">
                <div style="display: block;">
                    <div class="section_small_top">
                        &nbsp;</div>
                    <div class="section_small_middle">
                        <div class="combopart">
                            <div class="header">
                                <div class="content">
                                    <div class="text" style="float: left">
                                        <label>
                                            Description</label>
                                    </div>
                                    <div style="float: right">
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                            <div id="panelDescription" style="padding-left: 10px">
                                <table>
                                    <tr>
                                        <td>
                                            <span style="font-weight: bold;">Media </span>
                                        </td>
                                        <td style="width: 10px;">
                                            <span style="font-weight: bold;">:</span>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMedia" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="font-weight: bold;">Colorant</span>
                                        </td>
                                        <td style="width: 10px;">
                                            <span style="font-weight: bold;">:</span>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblColorant" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="font-weight: bold;">Rip Settings</span>
                                        </td>
                                        <td style="width: 10px;">
                                            <span style="font-weight: bold;">:</span>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblRipSettings" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="font-weight: bold;">Notes</span>
                                        </td>
                                        <td style="width: 10px;">
                                            <span style="font-weight: bold;">:</span>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblNotes" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="font-weight: bold;">Technician Name</span>
                                        </td>
                                        <td style="width: 10px;">
                                            <span style="font-weight: bold;">:</span>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTechnicianName" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="clear">
                            </div>
                        </div>
                    </div>
                    <div class="section_small_bottom">
                        &nbsp;</div>
                </div>
            </td>
            <td style="width: 10px">
            </td>
            <td valign="top" style="width: 320px">
                <div style="display: block;">
                    <div class="section_small_top">
                        &nbsp;</div>
                    <div class="section_small_middle">
                        <div class="combopart">
                            <div class="header">
                                <div class="content">
                                    <div class="text" style="float: left">
                                        <label>
                                            Spectrophotometer Options</label>
                                    </div>
                                    <div style="float: right">
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                            <div id="Div1" style="padding-left: 10px">
                                <table>
                                    <tr>
                                        <td>
                                            <span style="font-weight: bold;">Measurement Device</span>
                                        </td>
                                        <td style="width: 10px;">
                                            <span style="font-weight: bold;">:</span>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMeasurementDevice" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="font-weight: bold;">Illuminant</span>
                                        </td>
                                        <td style="width: 10px;">
                                            <span style="font-weight: bold;">:</span>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblIlluminant" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="font-weight: bold;">Observer</span>
                                        </td>
                                        <td style="width: 10px;">
                                            <span style="font-weight: bold;">:</span>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblObserver" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="clear">
                            </div>
                        </div>
                    </div>
                    <div class="section_small_bottom">
                        &nbsp;</div>
                </div>
            </td>
        </tr>
        <tr style="height: 10px">
        </tr>
        <tr>
            <td valign="top">
                <div style="display: block;">
                    <div class="section_small_top">
                        &nbsp;</div>
                    <div class="section_small_middle">
                        <div class="combopart">
                            <div class="header">
                                <div class="content">
                                    <div class="text" style="float: left">
                                        <label>
                                            Option Presets</label>
                                    </div>
                                    <div style="float: right">
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                            <div id="Div2" style="padding-left: 10px">
                                <table>
                                    <tr style="line-height: 16px;">
                                        <td>
                                            <span style="font-weight: bold;">Selected Presets: </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 20px;">
                                            <asp:Label ID="lblSelectedPresets" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="line-height: 16px;">
                                            <span style="font-weight: bold;">Preset Description: </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 20px;">
                                            <asp:Label ID="lblPresetDescription" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="clear">
                            </div>
                        </div>
                    </div>
                    <div class="section_small_bottom">
                        &nbsp;</div>
                </div>
            </td>
            <td style="width: 10px">
            </td>
            <td valign="top">
                <div style="display: block;">
                    <div class="section_small_top">
                        &nbsp;</div>
                    <div class="section_small_middle">
                        <div class="combopart">
                            <div class="header">
                                <div class="content">
                                    <div class="text" style="float: left">
                                        <label>
                                            Alignment Chart</label>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="panelAlignmentChart" runat="server" Style="padding-left: 5px" BorderStyle="None">
                                <table>
                                    <tr>
                                        <td>
                                            <span style="font-weight: bold;">Alignment Chart</span> &nbsp
                                        </td>
                                        <td style="width: 10px;">
                                            <span style="font-weight: bold;">:</span>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblAlignmentChart" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <%-- <asp:TextBox ID="txtChartPriview" runat="server" Width="96%" Height="200px" Style="margin-left: 10px;"
                                                TextMode="MultiLine" Enabled="false" ReadOnly="true" Text="chart priview">
                                            </asp:TextBox>--%>
                                            <asp:Image ID="imgChart" runat="server" Width="96%" Height="200px" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>
                    </div>
                    <div class="section_small_bottom">
                        &nbsp;</div>
                </div>
            </td>
        </tr>
    </table>
</div>
<div class="clear">
</div>
<telerik:RadWindowManager ID="RadWindowManager" runat="server" Modal="True">
    <Windows>
        <telerik:RadWindow ID="ConfirmSaveWindow" runat="server" VisibleStatusbar="false"
            ShowContentDuringLoad="false" Title="Download Chart" Width="730" Height="400"
            Behaviors="Close, Move" Behavior="Close, Move" NavigateUrl="~/PopupSaveProject.aspx">
        </telerik:RadWindow>
    </Windows>
</telerik:RadWindowManager>
<div class="clear">
</div>
