<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ProfilerOptions.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.ProfilerOptions" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<style type="text/css">
    .style1
    {
        width: 150px;
        text-align: left;
    }
    
    .style4
    {
        width: 80px;
    }
    
    .checkboxFont
    {
    }
    
    .checkboxFont Label
    {
        font-size: 12px;
    }
    
    textarea {
        resize: none;
    }
    
    .textwidth50
    {
        width: 50px;
    }
</style>
<script language="javascript" type="text/javascript">
    jQuery(document).ready(function () {
        $("div[id*='accordion']").click
        (
                function () {
                    var $panel = $("div[id*='panelAdvancedOptions']");
                    var $img = $("div[id*='imgHeader']");
                    $panel.slideToggle(500);
                    if ($img.hasClass('expanded')) {
                        $img.addClass('collapsed').removeClass('expanded');
                    }
                    else {
                        $img.addClass('expanded').removeClass('collapsed');
                    }
                    return false;
                }
        );
        //        var $panel = $("div[id*='panelAdvancedOptions']");
        //        $panel.slideToggle(0);
        $("div[id*='accordion']").click();

        $('input[id*="txtTVI"]').each(function () {
            $(this).numeric({ allow: '.' });
        });
    });
</script>
<div class="head">
    <h1 id="hTitle" runat="server"><asp:Localize runat="server" 
            Text="New Print Condition - Profiler Options" Id="locNewPrintCondition" 
            meta:resourcekey="locNewPrintConditionResource1"></asp:Localize>
    </h1>
    <asp:LinkButton ID="lnkbtnProfilerOptions" runat="server" CssClass="button" 
        OnClick="lnkbtnProfilerOptions_Click">
        <span>
            <asp:Image ID="Image1" runat="server" 
        ImageUrl="~/Images/new_project_icon.png" meta:resourcekey="Image1Resource1" />
            
<asp:Localize runat="server" Text="Measurement Setup" ID="locMeasurementSetup" 
        meta:resourcekey="locMeasurementSetupResource1"></asp:Localize>
</span></asp:LinkButton>
    <asp:LinkButton ID="lnkbtnTargetColorSpace" runat="server" 
        CssClass="buttonLeft" OnClick="lnkbtnTargetColorSpace_Click">
        <span>
            <asp:Image ID="imgAdd" runat="server" 
        ImageUrl="~/Images/new_project_icon.png" meta:resourcekey="imgAddResource1" />
            
<asp:Localize runat="server" Text="Target ColorSpace" ID="locTargetColorSpace" 
        meta:resourcekey="locTargetColorSpaceResource1"></asp:Localize>
</span></asp:LinkButton>
</div>
<div class="formDeviceNoLine" style="display: none;">
    <div class="clear">
    </div>
</div>
<div class="listDevice" style="margin-left: 10px;">
    <ucStatusbar:Statusbar ID="fujiStatusbar" runat="server" Visible="False" />
    <table cellpadding="0" cellspacing="10" border="0" width="97%" id="tbOptionPreset" runat="server">
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
                                        <span><asp:Localize runat="server" Text="Options Presets" 
                                            ID="locOptionsPresets" meta:resourcekey="locOptionsPresetsResource1"></asp:Localize></span>
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
                            <asp:Panel ID="panelAddProjectDescription" Style="padding: 5px" runat="server" 
                                BorderStyle="None" meta:resourcekey="panelAddProjectDescriptionResource1">
                                <table>
                                    <tr>
                                        <td style="width: 100px">
                                            <span><asp:Localize runat="server" Text="Selected Presets:" 
                                                ID="locSelectedPresets" meta:resourcekey="locSelectedPresetsResource1"></asp:Localize></span>
                                        </td>
                                        <td style="width: 450px">
                                            <asp:DropDownList ID="cmbSelectedPresets" runat="server" CssClass="select" DataValueField="Key"
                                                DataTextField="Value" AutoPostBack="True" 
                                                OnSelectedIndexChanged="cmbSelectedPresets_SelectedIndexChanged" 
                                                meta:resourcekey="cmbSelectedPresetsResource1">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" colspan="2">
                                            <span><asp:Localize runat="server" Text="Preset Description:" 
                                                ID="locPresetDescription" meta:resourcekey="locPresetDescriptionResource1"></asp:Localize></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:TextBox ID="txtProfileDescription" runat="server" Width="120%" Height="100px"
                                                TextMode="MultiLine" Enabled="False" ReadOnly="True" 
                                                Text="This preset is best used for analog devices ……" 
                                                meta:resourcekey="txtProfileDescriptionResource1"></asp:TextBox>
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
                            <div id="accordion" class="header collapsibleContainerTitle">
                                <div class="content">
                                    <div class="text">
                                        <div style="float: left">
                                        </div>
                                        <div style="float: left;">
                                            <div id="imgHeader" class="expanded">
                                                &nbsp;</div>
                                            <label class="collapsibleContainerTitle">
                                                <asp:Localize runat="server" Text="Advanced Options" 
                                                ID="locAdvancedOptions" meta:resourcekey="locAdvancedOptionsResource1"></asp:Localize></label>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="panelAdvancedOptions" Style="padding: 1px" runat="server" BorderStyle="None"
                                Width="100%" Enabled="False" 
                                meta:resourcekey="panelAdvancedOptionsResource1">
                                <table cellpadding="0" cellspacing="10" border="0">
                                    <tr>
                                        <td valign="top" style="width: 320px; padding-right: 0px;">
                                            <div style="padding-bottom: 10px">
                                                <div class="section_small_top">
                                                    &nbsp;</div>
                                                <div class="section_small_middle" style="height: 70px">
                                                    <div class="combopart">
                                                        <div class="header">
                                                            <div class="content">
                                                                <div class="text" style="float: left">
                                                                    <label><asp:Localize runat="server" Text="Rendering" ID="locRendering1" 
                                                                        meta:resourcekey="locRendering1Resource1"></asp:Localize></label>
                                                                </div>
                                                                <div style="float: right">
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div id="panelRendering" style="padding-left: 10px">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <span><asp:Localize runat="server" ID="locRendering2" Text="Rendering:" 
                                                                            meta:resourcekey="locRendering2Resource1"></asp:Localize>&nbsp; </span>&nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList runat="server" ID="cmbRendering" 
                                                                            meta:resourcekey="cmbRenderingResource1">
                                                                        </asp:DropDownList>
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
                                            <div style="display: block;">
                                                <div class="section_small_top">
                                                    &nbsp;</div>
                                                <div class="section_small_middle" style="height: 306px">
                                                    <div class="combopart">
                                                        <div class="header">
                                                            <div class="content">
                                                                <div class="text" style="float: left">
                                                                    <label><asp:Localize runat="server" Text="Black - Generation" 
                                                                        ID="locBlackGeneration" meta:resourcekey="locBlackGenerationResource1"></asp:Localize></label>
                                                                </div>
                                                                <div style="float: right">
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div id="panelBlackGeneration" style="padding-left: 10px">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td class="style4">
                                                                        <span><asp:Localize runat="server" Text="GCR:" ID="locGCR" 
                                                                            meta:resourcekey="locGCRResource1"></asp:Localize></span>
                                                                    </td>
                                                                    <td style="width: 160px;">
                                                                        <telerik:RadSlider runat="server" ID="rdsMode" MinimumValue="-100" MaximumValue="10"
                                                                            Width="160px" DbValue="-100" Height="22px" Length="160" 
                                                                            meta:resourcekey="rdsModeResource1" SelectionEnd="-100" SelectionStart="-100" 
                                                                            Value="-100" Enabled="false">
                                                                        </telerik:RadSlider>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="txtMode" Width="30px"
                                                                            meta:resourcekey="txtModeResource1"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style4">
                                                                        <span><asp:Localize runat="server" Text="Black-Start:" ID="locBlackStart" 
                                                                            meta:resourcekey="locBlackStartResource1"></asp:Localize></span>
                                                                    </td>
                                                                    <td style="width: 160px;">
                                                                        <telerik:RadSlider runat="server" ID="rdsBlackStart"
                                                                            Width="160px" DbValue="0" Height="22px" Length="160" 
                                                                            meta:resourcekey="rdsBlackStartResource1" Enabled="false">
                                                                        </telerik:RadSlider>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="txtBlackStart" Width="30px" 
                                                                            meta:resourcekey="txtBlackStartResource1"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style4">
                                                                        <span><asp:Localize runat="server" Text="Black-Width:" ID="locBlackWidth" 
                                                                            meta:resourcekey="locBlackWidthResource1"></asp:Localize></span>
                                                                    </td>
                                                                    <td style="width: 160px;">
                                                                        <telerik:RadSlider runat="server" ID="rdsBlackWidth"
                                                                            Width="160px" DbValue="0" Height="22px" Length="160" 
                                                                            meta:resourcekey="rdsBlackWidthResource1" Enabled="false">
                                                                        </telerik:RadSlider>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="txtBlackWidth" Width="30px" 
                                                                            meta:resourcekey="txtBlackWidthResource1"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style4">
                                                                        <span><asp:Localize runat="server" Text="Max-Ink:" ID="locMaxInk" 
                                                                            meta:resourcekey="locMaxInkResource1"></asp:Localize></span>
                                                                    </td>
                                                                    <td style="width: 160px;">
                                                                        <asp:TextBox runat="server" ID="txtMaxInk" Width="70px" 
                                                                            meta:resourcekey="txtMaxInkResource1"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style4">
                                                                        <span><asp:Localize runat="server" Text="Custom:" ID="locCustom" 
                                                                            meta:resourcekey="locCustomResource1"></asp:Localize></span>
                                                                    </td>
                                                                    <td style="width: 160px;">
                                                                        <asp:TextBox runat="server" ID="txtBlackPointCustom" Width="150px" 
                                                                            meta:resourcekey="txtBlackPointCustomResource1"></asp:TextBox>
                                                                    </td>
                                                                    <td>
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
                                        <td valign="top" style="width: 320px;">
                                            <div style="display: block;">
                                                <div class="section_small_top">
                                                    &nbsp;</div>
                                                <div class="section_small_middle" style="height: 400px;">
                                                    <div class="combopart">
                                                        <div class="header">
                                                            <div class="content">
                                                                <div class="text" style="float: left">
                                                                    <label><asp:Localize runat="server" Text="Exceptions" ID="locExceptions" 
                                                                        meta:resourcekey="locExceptionsResource1"></asp:Localize>
                                                                        </label>
                                                                </div>
                                                                <div style="float: right">
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div id="panelExceptions" style="padding-left: 10px">
                                                            <table width="100%" class="checkboxFont" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td class="style1">
                                                                        <asp:CheckBox ID="chkTriplex" runat="server" Text="  Triplex" 
                                                                            meta:resourcekey="chkTriplexResource1" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox runat="server" ID="chkDuplex" Text="  Duplex" 
                                                                            meta:resourcekey="chkDuplexResource1" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style1">
                                                                        <asp:CheckBox runat="server" ID="chkGray" Text="  Gray" 
                                                                            meta:resourcekey="chkGrayResource1" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox runat="server" ID="chk100Black" Text="  100% Black" 
                                                                            meta:resourcekey="chk100BlackResource1" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style1">
                                                                        <asp:CheckBox runat="server" ID="chk100CMY" Text="  100% C,M,Y" 
                                                                            meta:resourcekey="chk100CMYResource1" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox runat="server" ID="chk100RGB" Text="  100% R,G,B" 
                                                                            meta:resourcekey="chk100RGBResource1" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style1">
                                                                        <asp:CheckBox runat="server" ID="chkMaxCMY" Text="  Max. C,M,Y" 
                                                                            meta:resourcekey="chkMaxCMYResource1" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox runat="server" ID="chkMaxRGB" Text="  Max. R,G,B" 
                                                                            meta:resourcekey="chkMaxRGBResource1" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style1" colspan="2">
                                                                        <asp:CheckBox ID="chkPrimaries" runat="server" Text="  Primaries" 
                                                                            meta:resourcekey="chkPrimariesResource1" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style1" colspan="2">
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:CheckBox ID="chkPC" runat="server" Text="  C" 
                                                                            meta:resourcekey="chkPCResource1" />
                                                                        <asp:CheckBox ID="chkPM" runat="server" Text="  M" 
                                                                            meta:resourcekey="chkPMResource1" />
                                                                        <asp:CheckBox ID="chkPY" runat="server" Text="  Y" 
                                                                            meta:resourcekey="chkPYResource1" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style1" colspan="2">
                                                                        <asp:CheckBox ID="chkSecondaries" runat="server" Text="  Secondaries" 
                                                                            meta:resourcekey="chkSecondariesResource1" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style1" colspan="2">
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:CheckBox ID="chkSMY" runat="server" Text="  MY" 
                                                                            meta:resourcekey="chkSMYResource1" />
                                                                        <asp:CheckBox ID="chkSCY" runat="server" Text="  CY" 
                                                                            meta:resourcekey="chkSCYResource1" />
                                                                        <asp:CheckBox ID="chkSCM" runat="server" Text="  CM" 
                                                                            meta:resourcekey="chkSCMResource1" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style1">
                                                                        <asp:CheckBox runat="server" ID="chkBlackOverPrint" Text="  Black Overprint" 
                                                                            meta:resourcekey="chkBlackOverPrintResource1" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox runat="server" ID="chkBorderClipping" Text="  Border Clipping" 
                                                                            meta:resourcekey="chkBorderClippingResource1" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style1">
                                                                        <asp:CheckBox runat="server" ID="chkWhite" Text="  White" 
                                                                            meta:resourcekey="chkWhiteResource1" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox runat="server" ID="chk400Black" Text="  400% Black" 
                                                                            meta:resourcekey="chk400BlackResource1" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" valign="top">
                                                                        <table cellpadding="0" cellspacing="0" width="98%">
                                                                            <tr>
                                                                                <td colspan="3" valign="top">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td><asp:Localize runat="server" Text="Range:" ID="locRange" 
                                                                                        meta:resourcekey="locRangeResource1"></asp:Localize>&nbsp;
                                                                                </td>
                                                                                <td>
                                                                                    <telerik:RadSlider runat="server" ID="rdsRange" DbValue="0" Height="22px" 
                                                                                        Length="200" meta:resourcekey="rdsRangeResource1" Width="200px" Enabled="false">
                                                                                    </telerik:RadSlider>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox runat="server" ID="txtRange" Width="30px" 
                                                                                        meta:resourcekey="txtRangeResource1"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
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
                                </table>
                            </asp:Panel>
                        </div>
                    </div>
                    <div class="tab_bottom">
                        &nbsp;</div>
                </div>
            </td>
        </tr>
    </table>

    <table cellpadding="0" cellspacing="10" border="0" width="97%" id="tbMetricPreset" runat="server">
        <tr id="tr1" runat="server">
            <td>
                <div style="display: block;" class="contantbox tabcontant">
                    <div class="section_top">
                        &nbsp;</div>
                    <div class="section_middle">
                        <div class="combopart">
                            <div class="header">
                                <div class="content">
                                    <div class="text" style="float: left;">
                                        <span><asp:Localize runat="server" Text="Metric Presets" 
                                            ID="locMetricPresets"></asp:Localize></span>
                                    </div>
                                    <div style="float: right">                
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="pnlAddProjectDescriptionMetricPreset" Style="padding: 5px" runat="server" 
                                BorderStyle="None">
                                <table>
                                    <tr>
                                        <td style="width: 100px">
                                            <span><asp:Localize runat="server" Text="Selected Presets:" 
                                                ID="locMSelectedPresets"></asp:Localize></span>
                                        </td>
                                        <td style="width: 450px">
                                            <asp:DropDownList ID="ddlMetricPresets" runat="server" CssClass="select" AutoPostBack="True" 
                                                OnSelectedIndexChanged="ddlMetricPresets_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" colspan="2">
                                            <span><asp:Localize runat="server" Text="Preset Description:" 
                                                ID="locMPresetDescription"></asp:Localize></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:TextBox ID="txtMPresetDescription" runat="server" Width="120%" Height="100px"
                                                TextMode="MultiLine" Enabled="False" ReadOnly="True" 
                                                Text="This preset is best used for analog devices ……" ></asp:TextBox>
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
                            <div id="dvaccordionMetric" class="header collapsibleContainerTitle">
                                <div class="content">
                                    <div class="text">
                                        <div style="float: left">
                                        </div>
                                        <div style="float: left;">
                                            <div id="dvimgHeader" class="expanded">
                                                &nbsp;</div>
                                            <label class="collapsibleContainerTitle">
                                                <asp:Localize runat="server" Text="Advanced Options" 
                                                ID="locAdvancedOptionsMetric"></asp:Localize></label>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="panelAdvancedOptionsMetricPresets" Style="padding: 1px" runat="server" BorderStyle="None"
                                Width="100%" Enabled="false">
                                <table cellpadding="0" cellspacing="10" border="0">
                                    <tr>
                                        <td valign="top" style="width: 320px; padding-right: 0px;">                                            
                                            <div style="display: block;">
                                                <div class="section_small_top">
                                                    &nbsp;</div>
                                                <div class="section_small_middle" style="height: 306px">
                                                    <div class="combopart">
                                                        <div class="header">
                                                            <div class="content">
                                                                <div class="text" style="float: left">
                                                                    <label><asp:Localize runat="server" Text="Tolerances" 
                                                                        ID="locTolerances"></asp:Localize></label>
                                                                </div>
                                                                <div style="float: right">
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div id="dvTolerances" style="padding-left: 10px">
                                                            <asp:ListView ID="lvTolerances" runat="server" ItemPlaceholderID="phItemTolerances">
                                                                <LayoutTemplate>
                                                                    <table border="0" cellpadding="0" cellspacing="0">
	                                                                    <tr>
                                                                            <td></td>
                                                                            <td></td>                                                                            
                                                                        </tr>
                                                                        <asp:PlaceHolder ID="phItemTolerances" runat="server"></asp:PlaceHolder>
                                                                    </table>                                                                    
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                        <tr>
		                                                                    <td style="width: 160px;">
			                                                                    <asp:Label runat="server" Text='<%# Eval("MetricSumName") %>' ID="lblToleranceName"></asp:Label>
		                                                                    </td>
		                                                                    <td class="style4">
			                                                                    <asp:Label runat="server" Text='<%# Eval("TargetValue") %>' ID="lblToleranceValue"></asp:Label>
		                                                                    </td>		                                                                    
	                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                            <br />
                                                            <asp:CheckBox ID="chkWeightedTolerances" runat="server" />&nbsp;<asp:Label ID="lblWeightedTolerances" runat="server" Text="Use Weighted Tolerances"></asp:Label>
                                                        </div>
                                                        <div class="clear">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="section_small_bottom">
                                                    &nbsp;</div>
                                            </div>
                                        </td>
                                        <td valign="top" style="width: 320px;">
                                            <div style="display: block;">
                                                <div class="section_small_top">
                                                    &nbsp;</div>
                                                <div class="section_small_middle" style="height: 400px;">
                                                    <div class="combopart">
                                                        <div class="header">
                                                            <div class="content">
                                                                <div class="text" style="float: left">
                                                                    <label><asp:Localize runat="server" Text="TVI Targets" ID="locTVITargets"></asp:Localize>
                                                                        </label>
                                                                </div>
                                                                <div style="float: right">
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div id="dvTVITargets" style="padding-left: 10px">
                                                           <table cellspacing="0" cellpadding="0" border="0" width="90%">
                                                                <tr>
                                                                    <td>
                                                                        <span><b><asp:Localize ID="locTVIPatch" runat="server" Text="Patch"></asp:Localize></b></span>
                                                                    </td>
                                                                    <td>
                                                                        <span><b><asp:Localize ID="locTVICyan" runat="server" Text="Cyan"></asp:Localize></b></span>
                                                                    </td>
                                                                    <td>
                                                                        <span><b><asp:Localize ID="locTVIMagenta" runat="server" Text="Magenta"></asp:Localize></b></span>
                                                                    </td>
                                                                    <td>
                                                                        <span><b><asp:Localize ID="locTVIYellow" runat="server" Text="Yellow"></asp:Localize></b></span>
                                                                    </td>
                                                                    <td>
                                                                        <span><b><asp:Localize ID="locTVIBlack" runat="server" Text="Black"></asp:Localize></b></span>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td><asp:Label ID="lblTVI25" runat="server" Text="1/4 Tone"></asp:Label></td>
                                                                    <td><asp:TextBox ID="txtTVI25Cyan" runat="server" CssClass="textwidth50" MaxLength="5"></asp:TextBox></td>
                                                                    <td><asp:TextBox ID="txtTVI25Magenta" runat="server" CssClass="textwidth50" MaxLength="5"></asp:TextBox></td>
                                                                    <td><asp:TextBox ID="txtTVI25Yellow" runat="server" CssClass="textwidth50" MaxLength="5"></asp:TextBox></td>
                                                                    <td><asp:TextBox ID="txtTVI25Black" runat="server" CssClass="textwidth50" MaxLength="5"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><asp:Label ID="lblTVI50" runat="server" Text="Mid Tone"></asp:Label></td>
                                                                    <td><asp:TextBox ID="txtTVI50Cyan" runat="server" CssClass="textwidth50" MaxLength="5"></asp:TextBox></td>
                                                                    <td><asp:TextBox ID="txtTVI50Magenta" runat="server" CssClass="textwidth50" MaxLength="5"></asp:TextBox></td>
                                                                    <td><asp:TextBox ID="txtTVI50Yellow" runat="server" CssClass="textwidth50" MaxLength="5"></asp:TextBox></td>
                                                                    <td><asp:TextBox ID="txtTVI50Black" runat="server" CssClass="textwidth50" MaxLength="5"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><asp:Label ID="lblTVI75" runat="server" Text="3/4 Tone"></asp:Label></td>
                                                                    <td><asp:TextBox ID="txtTVI75Cyan" runat="server" CssClass="textwidth50" MaxLength="5"></asp:TextBox></td>
                                                                    <td><asp:TextBox ID="txtTVI75Magenta" runat="server" CssClass="textwidth50" MaxLength="5"></asp:TextBox></td>
                                                                    <td><asp:TextBox ID="txtTVI75Yellow" runat="server" CssClass="textwidth50" MaxLength="5"></asp:TextBox></td>
                                                                    <td><asp:TextBox ID="txtTVI75Black" runat="server" CssClass="textwidth50" MaxLength="5"></asp:TextBox></td>
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
                                </table>
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
