<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SavePrintCondition.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.Mock.UserControl.SavePrintCondition" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<%@ Register Src="~/UserControl/AddProject/SaveWindow.ascx" TagName="SaveWindow" TagPrefix="uc" %>

<div class="formDeviceNoLine" style="display:none;">
  <%--  <div style="float: left">
        <label>
        </label>
    </div>
    <div style="float: right">
        <asp:LinkButton ID="lbtnSave" runat="server" CssClass="gray_bt" 
            onclick="lbtnSave_Click">
                <span>Save</span>
        </asp:LinkButton>
    </div>--%>
    <div class="clear">
    </div>
</div>
<div class="listDevice" style="margin-left:10px">
    <ucStatusbar:Statusbar ID="ucStatusbar1" runat="server" Visible="false" />
     <%--<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                        <AjaxSettings>
                            <telerik:AjaxSetting AjaxControlID="rgProjectList">
                                <UpdatedControls>
                                    <telerik:AjaxUpdatedControl ControlID="panelAddProjectDescription" LoadingPanelID="RadAjaxLoadingPanel1" />
                                </UpdatedControls>
                            </telerik:AjaxSetting>
                        </AjaxSettings>
                    </telerik:RadAjaxManager>
                    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Office2010Silver" />
                --%>
    <table cellpadding="0" cellspacing="10" border="0" >
        <tr>
            <td valign="top" style="width:320px">
                <div style="display: block;" >
                    <div class="section_small_top">
                        &nbsp;</div>
                    <div class="section_small_middle">
                        <div class="combopart">
                            <div class="header" >
                                <div class="content">
                                <div class="text" style="float: left">
                                    <label>Description</label>
                                </div>
                                <div style="float: right">
                                </div>
                                <div class="clear">
                                </div>
                                </div>
                            </div>
                            <div ID="panelDescription" style="padding-left:10px">
                                <table>
                                    <tr>
                                        <td>
                                            <span>Media:
                                            </span>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMedia" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                      <tr>
                                        <td>
                                            <span >Colorant:
                                            </span>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblColorant" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                      <tr>
                                        <td>
                                            <span >Rip Settings:
                                            </span>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblRipSettings" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                      <tr>
                                        <td>
                                            <span >Notes:
                                            </span>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblNotes" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                      <tr>
                                        <td>
                                            <span >Technician Name:
                                            </span>
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
            <td valign="top" style="width:320px">
                 <div style="display: block;">
                  <div class="section_small_top">
                        &nbsp;</div>
                    <div class="section_small_middle">
                        <div class="combopart">
                            <div class="header" >
                               <div class="content">
                                <div class="text" style="float: left">
                                    <label>Spectrophotometer Options</label>
                                </div>
                                <div style="float: right">
                                </div>
                                <div class="clear">
                                </div>
                                </div>
                            </div>
                            <div ID="Div1" style="padding-left:10px">
                                <table>
                                    <tr>
                                        <td>
                                            <span >Measurement Device:
                                            </span>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMeasurementDevice" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                      <tr>
                                        <td>
                                            <span >Measurement Settings:
                                            </span>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMeasurementSettings" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                      <tr>
                                        <td>
                                            <span >Illuminant:
                                            </span>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblIlluminant" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                      <tr>
                                        <td>
                                            <span >Observer:
                                            </span>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblObserver" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                      <tr>
                                        <td>
                                            <span >UV Settings:
                                            </span>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblUVSettings" runat="server" Text=""></asp:Label>
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
        <tr>
            <td valign="top">
            <div style="display: block;" >
                    <div class="section_small_top">
                        &nbsp;</div>
                      <div class="section_small_middle">
                        <div class="combopart">
                              <div class="header" >
                               <div class="content">
                                <div class="text" style="float: left">
                                    <label>Option Presets</label>
                                </div>
                                <div style="float: right">
                                </div>
                                <div class="clear">
                                </div>
                                </div>
                            </div>
                            <div ID="Div2" style="padding-left:10px">
                                <table>
                                    <tr>
                                        <td>
                                            <span >Selected Presets:
                                            </span>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSelectedPresets" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                      <tr>
                                        <td>
                                            <span >Preset Description:
                                            </span>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPresetDescription" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                      <tr>
                                        <td>
                                            <span >Advanced Options:
                                            </span>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblAdvancedOptions" runat="server" Text=""></asp:Label>
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
            <td valign="top">
                <div style="display: block;">
                     <div class="section_small_top">
                        &nbsp;</div>
                      <div class="section_small_middle">
                        <div class="combopart">
                            <div class="header" >
                               <div class="content">
                                <div class="text" style="float: left">
                                        <label>
                                            Alignment Chart</label>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="panelAlignmentChart" runat="server" style="padding-left:5px" BorderStyle="None">
                                <table>
                                    <tr>
                                        <td>
                                            <span >Alignment Chart:</span> &nbsp
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbAlignmentChart" runat="server" Width="200px" CssClass="select">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                             <asp:TextBox ID="txtChartPriview" runat="server"  Width="96%" Height="200px"
                                                Style="margin-left: 10px;" TextMode="MultiLine" Enabled="false" ReadOnly="true"
                                                Text="chart priview">
                                            </asp:TextBox>
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
<div class="clear"></div>
<uc:SaveWindow ID="ucSaveWindow" runat="server" />
<div class="clear"></div>