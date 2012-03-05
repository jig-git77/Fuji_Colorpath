<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MeasurementSetup.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.Mock.UserControl.MeasurementSetup" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<div class="formDeviceNoLine" style="display:none;">
    <%--<div style="float: left">
        <label>
        </label>
    </div>
    <div style="float: right">
        <asp:LinkButton ID="lbtnComfirmSave" runat="server" CssClass="gray_bt" 
            onclick="lbtnComfirmSave_Click">
                <span>Comfirm Save</span>
        </asp:LinkButton>
    </div>--%>
    <div class="clear">
    </div>
</div>
<div class="listDevice" style="margin-left:10px;">
    <ucStatusbar:Statusbar ID="ucStatusbar1" runat="server" Visible="false" />
    <table cellpadding="0" cellspacing="10" border="0" width="97%">
        <tr id="trListHeader" runat="server">
            <td>
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
                            <asp:Panel ID="panelSpectrophotometerOptions" style="padding:5px" runat="server" BorderStyle="None">
                                <table>
                                    <tr>
                                        <td style="width: 140px">
                                            <span>Measurement Device:<span style="color: Red;">&nbsp*</span>
                                            </span>
                                        </td>
                                        <td >
                                            <asp:DropDownList ID="cmbMeasurementDevice" runat="server" Width="200px" CssClass="select">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator 
                                            ID="rfvMeasurementDevice" 
                                            runat="server" 
                                            Display="Dynamic"
                                            ControlToValidate="cmbMeasurementDevice" 
                                            ErrorMessage = "Please select measurement device." 
                                             ForeColor="Red"
                                            ValidationGroup="vgMeasurementSetup" ></asp:RequiredFieldValidator>
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
                            <asp:Panel ID="panelAlignmentChart" style="margin-left: 5px; margin-right:5px;" runat="server" BorderStyle="None">
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
                                             <asp:TextBox ID="txtChartPriview" runat="server" 
                                                Width="99%" Height="200px"
                                                TextMode="MultiLine" 
                                                Enabled="false" 
                                                ReadOnly="true"
                                                Text="chart priview">
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
