<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ValidationSetup.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.AddProject.ValidationSetup" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<%@ Register Src="~/UserControl/ValidationContainer.ascx" TagName="ValidationContainer"
    TagPrefix="ucValidationContainer" %>
<div class="head">
    <h1 id="hTitle" runat="server"><asp:Localize runat="server" 
            Text="New Print Condition - Validation Setup" 
            ID="locNewPrintConditionValidationSetup" 
            meta:resourcekey="locNewPrintConditionValidationSetupResource1"></asp:Localize>
    </h1>
    <asp:LinkButton ID="lnkbtnValidationSetup" runat="server" ValidationGroup="vgValidationSetup"
        CssClass="button" OnClick="lnkbtnValidationSetup_Click">
        <span>
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Search.png" />
            
<asp:Localize runat="server" Text="Review Selections" ID="locReviewSelection" 
        meta:resourcekey="locReviewSelectionResource1"></asp:Localize>
</span></asp:LinkButton>    
    <asp:LinkButton ID="btnValidationMeasure" runat="server" 
        ValidationGroup="vgValidationSetup" CssClass="button" 
        OnClick="lnkbtnMeasure_Click">
        <span>
        <asp:Image ID="imgAdd" runat="server" ImageUrl="~/Images/analyze.png" />
            
<asp:Localize runat="server" ID="locMeasure" Text="Measure" 
        meta:resourcekey="locMeasureResource1"></asp:Localize>
        

        </span>
    </asp:LinkButton>
</div>
<div class="formDeviceNoLine" style="display: none;">
    <div class="clear">
    </div>
</div>
<div class="listDevice" style="margin-left: 10px;">
    <ucStatusbar:Statusbar ID="fujiStatusbar" runat="server" Visible="False" />
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
                                        <asp:Localize runat="server" Text="Validation Options" 
                                            ID="locValidationOptions" meta:resourcekey="locValidationOptionsResource1"></asp:Localize>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="panelValidationOptions" Style="padding: 5px" runat="server"
                                BorderStyle="None">
                                <table>
                                    <tr>
                                        <td style="width: 160px">
                                            <span>
                                            <asp:Localize Text="Enable Validation:" runat="server" 
                                                ID="locEnableValidation" meta:resourcekey="locEnableValidationResource1"></asp:Localize><span style="color: Red;">&nbsp*</span> </span>
                                        </td>
                                        <td>
                                            <div style="float: left;">  
                                              <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td style="width:100px;">
                                                        <asp:RadioButtonList ID="rbtnEnableValidation" runat="server" 
                                                            AutoPostBack="True" 
                                                            onselectedindexchanged="rbtnEnableValidation_SelectedIndexChanged" 
                                                            RepeatDirection="Horizontal" meta:resourcekey="rbtnEnableValidationResource1">
                                                            <asp:ListItem Selected="True" Text="Yes" Value="Yes" 
                                                                meta:resourcekey="ListItemResource1"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="No" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td>
                                                         <asp:Label ID="lbl_Valimsg" runat="server" 
                                                             meta:resourcekey="lbl_ValimsgResource1"></asp:Label>
                                                    </td>
                                                </tr>
                                              </table> 
                                            </div>                                            
                                            <div class="clear">
                                            </div>
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
                                            <asp:Localize runat="server" Text="Validation Chart" 
                                            ID="locValidationChart" meta:resourcekey="locValidationChartResource1"></asp:Localize></label>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                </div>
                                <asp:Panel ID="panelValidationChart" Style="margin-left: 5px; margin-right: 5px;"
                                runat="server" BorderStyle="None">
                                <table cellpadding="0" cellspacing="0" width="100%" style="padding: 10px 5px 10px 5px;">
                                    <tr style="height: 44px;">
                                        <td valign="top" style="width: 160px;">
                                            <span>
                                            <asp:Localize Text="Measurement Device:" runat="server" 
                                                ID="locMeasurementDevice" meta:resourcekey="locMeasurementDeviceResource1"></asp:Localize><span style="color: Red;">&nbsp*</span> </span>
                                        </td>
                                        <td valign="top">
                                            <div style="float: left;">
                                            <asp:DropDownList ID="cmbMeasurementDevice" runat="server" Width="200px" 
                                                    CssClass="select" AutoPostBack="True"
                                                    OnSelectedIndexChanged="cmbMeasurementDevice_SelectedIndexChanged" 
                                                    meta:resourcekey="cmbMeasurementDeviceResource1"></asp:DropDownList>
                                            </div>
                                            <div style="float: left;">
                                                <asp:RequiredFieldValidator ID="rfvMeasurementDevice" runat="server" Display="Dynamic"
                                                    ControlToValidate="cmbMeasurementDevice" ForeColor="Red" InitialValue="0" 
                                                    ValidationGroup="vgValidationSetup"><ucValidationContainer:ValidationContainer ID="rfvcMeasurementDeviceContainer" runat="server"
                                                        ValidationMsg="Please select Measurement device." Size="200" />
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="clear">
                                            </div>
                                        </td>
                                        <td valign="middle" align="right" rowspan="2">
                                            <asp:Image ID="imgValidationChart" runat="server" Width="150px" 
                                                meta:resourcekey="imgValidationChartResource1" />
                                       </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" style="width: 160px;">
                                            <span>
                                            <asp:Localize Text="Validation Chart:" runat="server" 
                                                ID="locValidationChart2" meta:resourcekey="locValidationChart2Resource1"></asp:Localize><span style="color: Red;">&nbsp*</span> </span>
                                        </td>
                                        <td valign="top">
                                            <div style="float: left;">
                                            <asp:DropDownList ID="cmbValidationChart" runat="server" Width="200px" 
                                                    CssClass="select" AutoPostBack="True"
                                                    OnSelectedIndexChanged="cmbValidationChart_SelectedIndexChanged" 
                                                    meta:resourcekey="cmbValidationChartResource1"></asp:DropDownList>
                                            </div>
                                            <div style="float: left;">
                                                <asp:RequiredFieldValidator ID="rfvValidationChart" runat="server" Display="Dynamic"
                                                    ControlToValidate="cmbValidationChart" ForeColor="Red" InitialValue="0" 
                                                    ValidationGroup="vgValidationSetup"><ucValidationContainer:ValidationContainer ID="rfvcValidationChartContainer" runat="server"
                                                        ValidationMsg="Please select Validation chart." Size="200" />
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="clear">
                                            </div>
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
                                            <asp:Localize runat="server" Text="Validation Metrics" 
                                            ID="locValidationMetrics" meta:resourcekey="locValidationMetricsResource1"></asp:Localize></label>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                </div>
                                <asp:Panel ID="panel_Metrictarget" Style="margin-left: 5px; margin-right: 5px;"
                                runat="server" BorderStyle="None">
                                <table cellpadding="0" cellspacing="0" width="100%" style="padding: 10px 5px 10px 5px;">
                                    <tr style="height: 44px;">
                                        <td valign="top" style="width: 160px;">
                                            <span>
                                            <asp:Localize Text="Validation Metric:" runat="server" 
                                                ID="locValidationMetric" meta:resourcekey="locValidationMetricResource1"></asp:Localize><span style="color: Red;">&nbsp*</span> </span>
                                        </td>
                                        <td valign="top">
                                            <div style="float: left;">
                                            <asp:DropDownList ID="cmbMetricTargetPreset" runat="server" Width="200px" 
                                                    CssClass="select" meta:resourcekey="cmbMetricTargetPresetResource1"></asp:DropDownList>
                                            </div>
                                            <div style="float: left;">
                                                <asp:RequiredFieldValidator ID="rfvMetricTargetPreset" runat="server" Display="Dynamic"
                                                    ControlToValidate="cmbMetricTargetPreset" ForeColor="Red" InitialValue="0" 
                                                    ValidationGroup="vgValidationSetup"><ucValidationContainer:ValidationContainer ID="vccmbMetricTargetPreset" runat="server"
                                                        ValidationMsg="Please select Validation metric." Size="200" />
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="clear">
                                            </div>
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