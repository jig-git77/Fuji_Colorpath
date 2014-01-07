<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ValidationSetup.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.AddProject.ValidationSetup" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<%@ Register Src="~/UserControl/ValidationContainer.ascx" TagName="ValidationContainer"
    TagPrefix="ucValidationContainer" %>

<style type="text/css">
textarea {
    resize: none;
}
</style>

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

     <asp:LinkButton ID="lnkbtnMeasurementSetup" runat="server" 
        CssClass="buttonLeft" onclick="lnkbtnMeasurementSetup_Click">       
        <span>
            <asp:Image ID="Image2" runat="server" 
                ImageUrl="~/Images/new_project_icon.png" />
            
                <asp:Localize runat="server" Text="Measurement Setup" ID="locMeasurementSetup" 
                        meta:resourcekey="locMeasurementSetupResource1"></asp:Localize>
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
        <tr>
            <td>
                <asp:Label ID="lbl_Valimsg" runat="server" Text=""  ForeColor="Red"></asp:Label>
            </td>
        </tr>
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
                                    <tr id="trEnableValidationOption" runat="server">
                                        <td style="width: 160px; padding-left: 5px;">
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
                                                        <td valign="top" >
                                                             &nbsp;&nbsp;&nbsp;&nbsp; Description: 
                                                        </td>  
                                                        <td valign="top" align="right" style="width:200px;">                                                           
                                                            <asp:TextBox ID="txtMTPDesc" runat="server" Text="" TextMode="MultiLine" Enabled="false"  Width="190px" ></asp:TextBox>
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
                                    </tr>
                                   
                                     <tr id="trUVSettings" runat="server">
                                        <td >
                                            <span><asp:Localize runat="server" Text="UV Settings:" ID="locUVSettings" meta:resourcekey="locUVSettingsResource1"></asp:Localize></span>
                                        </td>
                                        <td >
                                            <asp:DropDownList ID="cmbUVSettings" runat="server" Width="200px" 
                                                CssClass="select" AutoPostBack="true" 
                                                onselectedindexchanged="cmbUVSettings_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                     <tr id="trReadMode" runat="server">
                                        <td >
                                            <span><asp:Localize runat="server" Text="Read Mode:" ID="Localize2"></asp:Localize></span>
                                        </td>
                                        <td >
                                            <asp:DropDownList ID="cmbReadMode" runat="server" Width="200px" 
                                                CssClass="select" AutoPostBack="true" 
                                                onselectedindexchanged="cmbReadMode_SelectedIndexChanged">
                                                <asp:ListItem Text="Strip" Value="Strip" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Point" Value="Point"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                     <tr id="trMeasCount" runat="server">
                                        <td id="Td1" runat="server">
                                            <span><asp:Localize runat="server" Text="Measurement Count:" ID="Localize1"></asp:Localize></span>
                                        </td>
                                        <td id="Td2" runat="server">
                                            <asp:DropDownList ID="cmbMeasurementCount" runat="server" Width="200px" CssClass="select">                                           
                                             <asp:ListItem Text="1" Value="1" Selected="True"></asp:ListItem>
                                              <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                               <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                 <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                            </asp:DropDownList>
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
                                    <tr>
                                        <td>
                                             <asp:Image ID="imgValidationChart" runat="server" Width="150px" 
                                                meta:resourcekey="imgValidationChartResource1" />
                                        </td>
                                        <td></td>
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
