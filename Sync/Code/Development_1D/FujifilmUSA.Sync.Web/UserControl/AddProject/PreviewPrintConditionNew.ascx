<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PreviewPrintConditionNew.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.AddProject.PreviewPrintConditionNew" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<script type="text/javascript" language="javascript">
    function ShowConfirmSaveWindow() {
        if ('<%= IsXmfUser %>'.toLowerCase() == 'true') {
            return true;
        }
        else {
            var oWnd = $find("<%= ConfirmSaveWindow.ClientID %>");
            oWnd.show();
            return false;
        }
    }
</script>
<script language="javascript" type="text/javascript">
    function Close() {
        var oWnd = $find("<%= ConfirmSaveWindow.ClientID %>");
        oWnd.close();
        return false;
    }
</script>
<style type="text/css">
    .breakWord100 {
        width: 20% !important;
        /*max-width: 100px !important;*/
        word-break: break-all !important;
        word-wrap: break-word !important;
        line-height: 15px;
    }

    .breakWord90 {
        width: 20% !important;
        /*max-width: 20% !important;*/
        word-break: break-all !important;
        word-wrap: break-word !important;
        line-height: 15px;
    }
</style>
<div class="head">
    <h1 id="hTitle" runat="server">
        <asp:Localize runat="server"
            Text="New Print Condition - Review Selections" ID="locNewPrintCondition"
            meta:resourcekey="locNewPrintConditionResource1"></asp:Localize></h1>
    <asp:LinkButton ID="lnkbtnSavePrintCondition" runat="server" CssClass="button" OnClick="lnkbtnSavePrintCondition_Click"
        OnClientClick="javascript:return ShowConfirmSaveWindow();">
        <span>
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/save_icon.png"
                meta:resourcekey="Image1Resource1" />
            <asp:Localize runat="server" Text="Save" ID="locSave"
                meta:resourcekey="locSaveResource1"></asp:Localize>
        </span>
    </asp:LinkButton>

    <asp:LinkButton ID="btnValidationSetup" runat="server" CssClass="buttonLeft" OnClick="btnValidationSetup_Click" Visible="false">
        <span>
            <asp:Image ID="Image3" runat="server"
                ImageUrl="~/Images/new_project_icon.png"
                meta:resourcekey="Image2Resource1" />
            <asp:Localize runat="server" Text="Validation Setup" ID="locValidationSetup" meta:resourcekey="locValidationSetupResource1"></asp:Localize>
        </span>
    </asp:LinkButton>
</div>
<div class="formDeviceNoLine" style="display: none;">
    <div class="clear">
    </div>
</div>
<div class="listDevice" style="margin-left: 18px">
    <ucStatusbar:Statusbar ID="fujiStatusbar" runat="server" Visible="False" />
    <table cellpadding="0" cellspacing="10" border="0" style="width: 100%; vertical-align: top;">
        <tr>
            <td valign="top" style="width: 320px">
                <div style="display: block;">
                    <div class="section_top_large">
                        &nbsp;
                    </div>
                    <div class="section_large">
                        <div class="combopart">
                            <div class="header" style="width: 980px;">
                                <div class="content">
                                    <div class="text" style="float: left">
                                        <label>
                                            <asp:Localize runat="server" Text="Description" ID="Description"
                                                meta:resourcekey="DescriptionResource1"></asp:Localize></label>
                                    </div>
                                    <div style="float: right">
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                            <div id="panelDescription" style="padding-left: 10px; width: 980px;">
                                <table style="vertical-align: top; width: 100%;">
                                    <tr>
                                        <td style="vertical-align: top; width: 50%;">
                                            <table style="vertical-align: top; width: 100%;">
                                                <tr>
                                                    <td class="breakWord90">
                                                        <span style="font-weight: bold;">
                                                            <asp:Localize Text="Media" runat="server"
                                                                ID="locMedia" meta:resourcekey="locMediaResource1"></asp:Localize>
                                                        </span>
                                                    </td>
                                                    <td style="width: 2%">
                                                        <span style="font-weight: bold;">:</span>
                                                    </td>
                                                    <td class="breakWord100">
                                                        <asp:Label ID="lblMedia" runat="server" meta:resourcekey="lblMediaResource1"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="breakWord90">
                                                        <span style="font-weight: bold;">
                                                            <asp:Localize Text="Colorant" runat="server"
                                                                ID="locColorant" meta:resourcekey="locColorantResource1"></asp:Localize></span>
                                                    </td>
                                                    <td style="width: 2%">
                                                        <span style="font-weight: bold;">:</span>
                                                    </td>
                                                    <td class="breakWord100">
                                                        <asp:Label ID="lblColorant" runat="server"
                                                            meta:resourcekey="lblColorantResource1"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="breakWord90">
                                                        <span style="font-weight: bold;">
                                                            <asp:Localize runat="server"
                                                                Text="Rip Settings" ID="locRipSettings"
                                                                meta:resourcekey="locRipSettingsResource1"></asp:Localize></span>
                                                    </td>
                                                    <td style="width: 2%">
                                                        <span style="font-weight: bold;">:</span>
                                                    </td>
                                                    <td class="breakWord100">
                                                        <asp:Label ID="lblRipSettings" runat="server"
                                                            meta:resourcekey="lblRipSettingsResource1"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="vertical-align: top; width: 50%;">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td class="breakWord90">
                                                        <span style="font-weight: bold;">
                                                            <asp:Localize runat="server" Text="Notes"
                                                                ID="locNotes" meta:resourcekey="locNotesResource1"></asp:Localize></span>
                                                    </td>
                                                    <td style="width: 2%">
                                                        <span style="font-weight: bold;">:</span>
                                                    </td>
                                                    <td class="breakWord100">
                                                        <asp:Label ID="lblNotes" runat="server" meta:resourcekey="lblNotesResource1"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="breakWord90">
                                                        <span style="font-weight: bold;">
                                                            <asp:Localize runat="server"
                                                                Text="Technician Name" ID="locTechnicianName"
                                                                meta:resourcekey="locTechnicianNameResource1"></asp:Localize></span>
                                                    </td>
                                                    <td style="width: 2%">
                                                        <span style="font-weight: bold;">:</span>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblTechnicianName" runat="server" CssClass="breakWord100"
                                                            meta:resourcekey="lblTechnicianNameResource1"></asp:Label>
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
                    <div class="section_bottom_large">
                        &nbsp;
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td valign="top" style="width: 320px">
                <div style="display: block;">
                    <div class="section_top_large">
                        &nbsp;
                    </div>
                    <div class="section_large">
                        <div class="combopart">
                            <div class="header" style="width: 980px;">
                                <div class="content">
                                    <div class="text" style="float: left">
                                        <label>
                                            Metric Settings</label>
                                    </div>
                                    <div style="float: right">
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                            <div id="panelMetricSettings" style="padding-left: 10px; width: 980px;">
                                <table style="vertical-align: top; width: 100%;">
                                    <div id="Div_TargetColorSpace" runat="server">
                                        <tr>
                                            <td class="breakWord90">
                                                <span style="font-weight: bold;">
                                                    <asp:Localize runat="server"
                                                        Text="Target Print Condition" ID="locTargetPrintCondition" meta:resourcekey="locTargetPrintConditionResource1"></asp:Localize></span>
                                            </td>
                                            <td style="width: 10px;">
                                                <span style="font-weight: bold;">:</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTargetPrintCondition" runat="server" CssClass="breakWord100"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="breakWord90">
                                                <span style="font-weight: bold;">
                                                    <asp:Localize runat="server"
                                                        Text="Source Profile" ID="locSourceProfile"
                                                        meta:resourcekey="locSourceProfileResource1"></asp:Localize></span>
                                            </td>
                                            <td style="width: 10px;">
                                                <span style="font-weight: bold;">:</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSourceProfile" runat="server" CssClass="breakWord100"
                                                    meta:resourcekey="lblSourceProfileResource1"></asp:Label>
                                            </td>
                                        </tr>
                                    </div>
                                    <div id="Div_SpotTarget" runat="server" visible="false">
                                        <tr>
                                            <td class="breakWord90">
                                                <span style="font-weight: bold;">
                                                    <asp:Localize runat="server"
                                                        Text="Spot Target" ID="Localize3" meta:resourcekey="locSpotTargetResource1"></asp:Localize></span>
                                            </td>
                                            <td style="width: 10px;">
                                                <span style="font-weight: bold;">:</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSpotTarget" runat="server" CssClass="breakWord100"></asp:Label>
                                            </td>
                                        </tr>

                                    </div>
                                    <div id="Div_RelativePaper" runat="server" visible="false">
                                        <tr>
                                            <td class="breakWord90">
                                                <span style="font-weight: bold;">Relative paer Scaling
                                                </span>
                                            </td>
                                            <td style="width: 10px;">
                                                <span style="font-weight: bold;">:</span>
                                            </td>
                                            <td><asp:Label ID="lblMetric_RelationScaling" runat="server"
                                                    ></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="breakWord90">
                                                <span style="font-weight: bold;">Paper L*a*bValues
                                                </span>
                                            </td>
                                            <td style="width: 10px;">
                                                <span style="font-weight: bold;">:</span>
                                            </td>
                                            <td><asp:Label ID="lblMetric_PaperLABValues" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="breakWord90">
                                                <span style="font-weight: bold;">Min L* Patch Values
                                                </span>
                                            </td>
                                            <td style="width: 10px;">
                                                <span style="font-weight: bold;">:</span>
                                            </td>
                                            <td><asp:Label ID="lblMetric_MinPatchValue" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </div>
                                    <div id="Div2" style="padding-left: 10px">
                                        <tr style="line-height: 16px;">
                                            <td>
                                                <span style="font-weight: bold;">
                                                    <asp:Localize runat="server"
                                                        Text="Selected Presets:" ID="locSelectedPresets"
                                                        meta:resourcekey="locSelectedPresetsResource1"></asp:Localize>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 20px;">
                                                <asp:Label ID="lblSelectedPresets" runat="server"
                                                    meta:resourcekey="lblSelectedPresetsResource1"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="line-height: 16px;">
                                                <span style="font-weight: bold;">
                                                    <asp:Localize runat="server"
                                                        Text="Preset Description:" ID="locPresetDesc"
                                                        meta:resourcekey="locPresetDescResource1"></asp:Localize>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 20px;">
                                                <asp:Label ID="lblPresetDescription" runat="server"
                                                    meta:resourcekey="lblPresetDescriptionResource1"></asp:Label>
                                            </td>
                                        </tr>
                                    </div>
                                </table>
                            </div>
                            <div class="clear">
                            </div>
                        </div>
                    </div>
                    <div class="section_bottom_large">
                        &nbsp;
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td valign="top" style="width: 320px">
                <div style="display: block;">
                    <div class="section_top_large">
                        &nbsp;
                    </div>
                    <div class="section_large">
                        <div class="combopart">
                            <div class="header" style="width: 980px;">
                                <div class="content">
                                    <div class="text" style="float: left">
                                        <label>
                                            Measurment Settings</label>
                                    </div>
                                    <div style="float: right">
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                            <div class="header" style="width: 980px;">
                                <div class="content">
                                    <div class="text" style="float: left">
                                        <table style="width: 100%">
                                            <tr>
                                                <td>
                                                    <span style="font-weight: bold; font-size: 12px;"><asp:Localize runat="server" 
                                                Text="Measurement Device" ID="locMeasurementDevice" 
                                                meta:resourcekey="locMeasurementDeviceResource1"></asp:Localize>
                                                    </span>
                                                </td>
                                                <td style="width: 10px;">
                                                    <span style="font-weight: bold;">:</span>
                                                </td>
                                                <td style="font-weight: normal; font-size: 12px;"><asp:Label ID="lblMeasurementDevice" runat="server" 
                                                meta:resourcekey="lblMeasurementDeviceResource1"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span style="font-weight: bold; font-size: 12px;"><asp:Localize runat="server" Text="Illuminant" 
                                                ID="locIlluminant" meta:resourcekey="locIlluminantResource1"></asp:Localize>t
                                                    </span>
                                                </td>
                                                <td>
                                                    <span style="font-weight: bold;">:</span>
                                                </td>
                                                <td style="font-weight: normal; font-size: 12px;"><asp:Label ID="lblIlluminant" runat="server" 
                                                meta:resourcekey="lblIlluminantResource1"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span style="font-weight: bold; font-size: 12px;"><asp:Localize runat="server" Text="Observer" 
                                                ID="locObserver" meta:resourcekey="locObserverResource1"></asp:Localize>
                                                    </span>
                                                </td>
                                                <td style="width: 10px;">
                                                    <span style="font-weight: bold;">:</span>
                                                </td>
                                                <td style="font-weight: normal; font-size: 12px;"> <asp:Label ID="lblObserver" runat="server" 
                                                meta:resourcekey="lblObserverResource1"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span style="font-weight: bold; font-size: 12px;">Density Type
                                                    </span>
                                                </td>
                                                <td style="width: 10px;">
                                                    <span style="font-weight: bold;">:</span>
                                                </td>
                                                <td style="font-weight: normal; font-size: 12px;"><asp:Label ID="lblDesitytype" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span style="font-weight: bold; font-size: 12px;">UV Mode
                                                    </span>
                                                </td>
                                                <td style="width: 10px;">
                                                    <span style="font-weight: bold;">:</span>
                                                </td>
                                                <td style="font-weight: normal; font-size: 12px;"><asp:Label ID="lblUVMode" runat="server" ></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div style="float: right">
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                            <div id="panelMeasurmentSettings" style="padding-left: 10px; width: 980px;">
                                <table style="vertical-align: top; width: 100%;">
                                    <tr>
                                        <td style="width: 50%; vertical-align: top;">
                                            <table style="width: 100%; vertical-align: top">
                                                <tr>
                                                    <td style="width: 40%">
                                                        <span style="font-weight: bold;">
                                                            <asp:Localize runat="server"
                                                                Text="Base DLP Chart" ID="locAlignmentChart2"
                                                                meta:resourcekey="locAlignmentChart2Resource1"></asp:Localize></span> &nbsp;
                                                    </td>
                                                    <td style="width: 10px;">
                                                        <span style="font-weight: bold;">:</span>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblAlignmentChart" runat="server"
                                                            meta:resourcekey="lblAlignmentChartResource1"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">

                                                        <asp:Image ID="imgChart" runat="server"
                                                            meta:resourcekey="imgChartResource1" />

                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 50%; vertical-align: top;">
                                            <table style="width: 100%; vertical-align: top;">
                                                <tr>
                                                    <td style="width: 40%">
                                                        <span style="font-weight: bold;">
                                                            <asp:Localize runat="server"
                                                                Text="Optimization Chart" ID="Localize2"
                                                                meta:resourcekey="locOptimizationChart2Resource1"></asp:Localize></span> &nbsp;
                                                    </td>
                                                    <td style="width: 10px;">
                                                        <span style="font-weight: bold;">:</span>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblOptimizationChart" runat="server"
                                                            meta:resourcekey="lblOptimizationChartResource1"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">

                                                        <asp:Image ID="Image_OptimizationChart" runat="server"
                                                            meta:resourcekey="imgOptimizationChartResource1" />

                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%; vertical-align: top;">
                    <tr>
                        <td style="vertical-align: top;">
                            <div id="Div_OptChart"  runat="server" style="display: block;">
                                <div class="section_small_top">
                                    &nbsp;
                                </div>
                                <div class="section_small_middle">
                                    <div class="combopart">
                                        <div class="header">
                                            <div class="content">
                                                <div class="text" style="float: left">
                                                    <label>
                                                        Validation Settings
                                                    </label>
                                                </div>
                                                <div class="clear">
                                                </div>
                                            </div>
                                        </div>
                                        <asp:Panel ID="Panel_ValidationSeting" runat="server" Style="padding-left: 5px"
                                            BorderStyle="None" meta:resourcekey="panelAlignmentChartResource1">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 40%">
                                                        <span style="font-weight: bold;">Validation Metric
                                                        </span>&nbsp;
                                                    </td>
                                                    <td style="width: 10px;">
                                                        <span style="font-weight: bold;">:</span>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblVali_Metric" runat="server"
                                                            meta:resourcekey="lblVali_MetricResource1"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 40%">
                                                        <span style="font-weight: bold;">Metric Description
                                                        </span>&nbsp;
                                                    </td>
                                                    <td style="width: 10px;">
                                                        <span style="font-weight: bold;">:</span>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblVali_Metric_Description" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 40%">
                                                        <span style="font-weight: bold;"><asp:Localize runat="server" 
                                                            Text="Validation Device" ID="locVali_MeaDevice" 
                                                            meta:resourcekey="locVali_MeaDeviceResource1"></asp:Localize></span>
                                                    </td>
                                                    <td style="width: 10px;">
                                                        <span style="font-weight: bold;">:</span>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblVali_MeaDevice" runat="server" 
                                                            meta:resourcekey="lblVali_MeaDeviceResource1"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 40%">
                                                        <span style="font-weight: bold;">UV Mode
                                                        </span>&nbsp;
                                                    </td>
                                                    <td style="width: 10px;">
                                                        <span style="font-weight: bold;">:</span>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblVali_Metric_UVMode" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 40%">
                                                        <span style="font-weight: bold;">Validation Chart
                                                        </span>&nbsp;
                                                    </td>
                                                    <td style="width: 10px;">
                                                        <span style="font-weight: bold;">:</span>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblValiChart" runat="server"
                                                            meta:resourcekey="lblValiChartResource1"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">

                                                        <asp:Image ID="Image_ValiChart" runat="server"
                                                            meta:resourcekey="imgValiChartResource1" />

                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </div>
                                </div>
                                <div class="section_small_bottom">
                                    &nbsp;
                                </div>
                            </div>
                        </td>
                        <td style="width: 0px"></td>
                        <td style="vertical-align: top;">
                            <div id="Div_OffSetPrintingInformation" runat="server" style="display: block;">
                                <div class="section_small_top">
                                    &nbsp;
                                </div>
                                <div class="section_small_middle">
                                    <div class="combopart">
                                        <div class="header">
                                            <div class="content">
                                                <div class="text" style="float: left">
                                                    <label>
                                                        Offset Printing Information
                                                    </label>
                                                </div>
                                                <div class="clear">
                                                </div>
                                            </div>
                                        </div>
                                        <asp:Panel ID="panel_OffsetPrintingInformation" runat="server" Style="padding-left: 5px"
                                            BorderStyle="None" meta:resourcekey="panelAlignmentChartResource1">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="width: 40%">
                                                        <span style="font-weight: bold;">Ink Order</span> &nbsp;
                                                    </td>
                                                    <td style="width: 10px;">
                                                        <span style="font-weight: bold;">:</span>
                                                    </td>
                                                    <td> <asp:Label ID="lblInkOderTxt" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 40%">
                                                        <span style="font-weight: bold;">Fountain Solution</span> &nbsp;
                                                    </td>
                                                    <td style="width: 10px;">
                                                        <span style="font-weight: bold;">:</span>
                                                    </td>
                                                    <td><asp:Label ID="lblFountainSolution" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 40%">
                                                        <span style="font-weight: bold;">Blanket</span> &nbsp;
                                                    </td>
                                                    <td style="width: 10px;">
                                                        <span style="font-weight: bold;">:</span>
                                                    </td>
                                                    <td><asp:Label ID="lblBlanket" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 40%">
                                                        <span style="font-weight: bold;">&nbsp;&nbsp; Blanket Age</span> &nbsp;
                                                    </td>
                                                    <td style="width: 10px;">
                                                        <span style="font-weight: bold;">:</span>
                                                    </td>
                                                    <td><asp:Label ID="lblBlanketAge" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 40%">
                                                        <span style="font-weight: bold;">Screening</span> &nbsp;
                                                    </td>
                                                    <td style="width: 10px;">
                                                        <span style="font-weight: bold;">:</span>
                                                    </td>
                                                    <td><asp:Label ID="lblScreening" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 40%">
                                                        <span style="font-weight: bold;">Coating</span> &nbsp;
                                                    </td>
                                                    <td style="width: 10px;">
                                                        <span style="font-weight: bold;">:</span>
                                                    </td>
                                                    <td><asp:Label ID="lblCoating" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 40%">
                                                        <span style="font-weight: bold;">Project Type</span> &nbsp;
                                                    </td>
                                                    <td style="width: 10px;">
                                                        <span style="font-weight: bold;">:</span>
                                                    </td>
                                                    <td><asp:Label ID="lblProjctType" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </div>
                                </div>
                                <div class="section_small_bottom">
                                    &nbsp;
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trRetragetting" runat="server" visible="false">
            <td>
                <table style="width: 100%; vertical-align: top;">
                    <tr>
                        <td style="vertical-align: top;">
                            <div id="Div1" runat="server" style="display: block;">
                                <div class="section_small_top">
                                    &nbsp;
                                </div>
                                <div class="section_small_middle">
                                    <div class="combopart">
                                        <div class="header">
                                            <div class="content">
                                                <div class="text" style="float: left">
                                                    <label>
                                                        Retargeting
                                                    </label>
                                                </div>
                                                <div class="clear">
                                                </div>
                                            </div>
                                        </div>
                                        <table style="width: 100%; vertical-align: top;">
                                            <tr>
                                                <td style="width: 40%">
                                                    <span style="font-weight: bold;">Device
                                                    </span>&nbsp;
                                                </td>
                                                <td style="width: 10px;">
                                                    <span style="font-weight: bold;">:</span>
                                                </td>
                                                <td><asp:Label ID="lblRetargetingDeviceName" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 40%">
                                                    <span style="font-weight: bold;">Print Condition
                                                    </span>&nbsp;
                                                </td>
                                                <td style="width: 10px;">
                                                    <span style="font-weight: bold;">:</span>
                                                </td>
                                                <td><asp:Label ID="lblRetargetingPrintCondition" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>

                        </td>
                    </tr>
                </table>
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
