<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ValidationSetup.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.AddProject.ValidationSetup" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<%@ Register Src="~/UserControl/ValidationContainer.ascx" TagName="ValidationContainer"
    TagPrefix="ucValidationContainer" %>
<div class="head">
    <h1 id="hTitle" runat="server"><asp:Localize runat="server" 
            Text="New Print Condition - Validation Setup" ID="locNewPrintConditionValidationSetup"></asp:Localize>
    </h1>
    <asp:LinkButton ID="lnkbtnValidationSetup" runat="server" ValidationGroup="vgValidationSetup"
        CssClass="button" OnClick="lnkbtnValidationSetup_Click">
        <span>
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Search.png" 
        meta:resourcekey="Image1Resource1" />
            
<asp:Localize runat="server" Text="Review Selections" ID="locReviewSelection" 
        meta:resourcekey="locReviewSelectionResource1"></asp:Localize>
</span></asp:LinkButton>    
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
                                            ID="locValidationOptions"></asp:Localize>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="panelValidationOptions" Style="padding: 5px" runat="server"
                                BorderStyle="None">
                                <table>
                                    <tr>
                                        <td style="width: 160px; padding-left:5px;">
                                            <span><asp:Localize Text="Enable Validation:" runat="server" 
                                                ID="locEnableValidation"></asp:Localize><span style="color: Red;">&nbsp*</span> </span>
                                        </td>
                                        <td>
                                            <div style="float: left;">  
                                              <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td style="width:100px;">
                                                        <asp:RadioButtonList ID="rbtnEnableValidation" runat="server" 
                                                            AutoPostBack="true" onselectedindexchanged="rbtnEnableValidation_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                            <asp:ListItem Selected="True" Text="Yes" Value="Yes"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td>
                                                         <asp:Label ID="lbl_Valimsg" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                              </table> 
                                            </div>                                            
                                            <div class="clear">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                             <asp:Panel ID="panel_Metrictarget"  runat="server" BorderStyle="None">
                                                <table cellpadding="0" cellspacing="0" width="100%" style="padding: 10px 5px 10px 5px;">
                                                    <tr style="height: 44px;">
                                                        <td valign="top" style="width: 160px;">
                                                            <span><asp:Localize Text="Validation Metric:" runat="server" 
                                                                ID="locValidationMetric"></asp:Localize><span style="color: Red;">&nbsp*</span> </span>
                                                        </td>
                                                        <td valign="top">
                                                            <div style="float: left;">
                                                            <asp:DropDownList ID="cmbMetricTargetPreset" runat="server" Width="200px" 
                                                                    CssClass="select" AutoPostBack="True" 
                                                                    onselectedindexchanged="cmbMetricTargetPreset_SelectedIndexChanged"></asp:DropDownList>
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
                                            <label><asp:Localize runat="server" Text="Validation Chart" 
                                            ID="locValidationChart"></asp:Localize></label>
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
                                            <span><asp:Localize Text="Measurement Device:" runat="server" 
                                                ID="locMeasurementDevice"></asp:Localize><span style="color: Red;">&nbsp*</span> </span>
                                        </td>
                                        <td valign="top">
                                            <div style="float: left;">
                                            <asp:DropDownList ID="cmbMeasurementDevice" runat="server" Width="200px" CssClass="select" AutoPostBack="true"
                                                    OnSelectedIndexChanged="cmbMeasurementDevice_SelectedIndexChanged"></asp:DropDownList>
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
                                            <asp:Image ID="imgValidationChart" runat="server" Width="150px" />
                                       </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" style="width: 160px;">
                                            <span><asp:Localize Text="Validation Chart:" runat="server" 
                                                ID="locValidationChart2"></asp:Localize><span style="color: Red;">&nbsp*</span> </span>
                                        </td>
                                        <td valign="top">
                                            <div style="float: left;">
                                            <asp:DropDownList ID="cmbValidationChart" runat="server" Width="200px" CssClass="select" AutoPostBack="true"
                                                    OnSelectedIndexChanged="cmbValidationChart_SelectedIndexChanged"></asp:DropDownList>
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
    </table>
</div>