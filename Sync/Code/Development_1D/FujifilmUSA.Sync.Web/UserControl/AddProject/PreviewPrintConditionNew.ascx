<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PreviewPrintConditionNew.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.AddProject.PreviewPrintConditionNew" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<script type="text/javascript" lang="javascript">
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
<script lang="javascript" type="text/javascript">
    var SERVICE_BASE_URL = '';
    var AUTH_TOKEN = '';
    function Close() {
        var oWnd = $find("<%= ConfirmSaveWindow.ClientID %>");
        oWnd.close();
        return false;
    }
    $(document).ready(function () {
        SERVICE_BASE_URL = $('#hdnBaseServiceUrl').val();
        AUTH_TOKEN = $("#hdfAuthToken").val();
    });
    function DownloadPrintPreviewReport() {
        var Show_ValidationBox_hdn = false;
        var Show_OffSetPrintingBox_hdn = false;
        var Show_RetargetingBox_hdn = false;
        if ($("#<%= hdnShowValidationBox.ClientID %>").val() == "1") {
            Show_ValidationBox_hdn = true;
        }
        if ($("#<%= hdnShowOffSetPrinting.ClientID %>").val() == "1") {
            Show_OffSetPrintingBox_hdn = true;
        }
        if ($("#<%= hdnShowRetargeting.ClientID %>").val() == "1") {
            Show_RetargetingBox_hdn = true;
        }

        var apiParameter = {
            Description_Media: $("#<%= lblMedia.ClientID %>").text(),
            Description_Colorant: $("#<%= lblColorant.ClientID %>").text(),
            Description_RipSettings: $("#<%= lblRipSettings.ClientID %>").text(),
            Description_Notes: $("#<%= lblNotes.ClientID %>").text(),
            Description_TechincalName: $("#<%= lblTechnicianName.ClientID %>").text(),

            Metric_TargetPrintCond: $("#<%= lblTargetPrintCondition.ClientID %>").text(),
            Metric_SourceProfile: $("#<%= lblSourceProfile.ClientID %>").text(),
            Metric_RelativePaperScaling: $("#<%= lblMetric_RelationScaling.ClientID %>").text(),
            Metric_PaperLabValues: $("#<%= lblMetric_PaperLABValues.ClientID %>").text(),
            Metric_MinLPatchValues: $("#<%= lblMetric_MinPatchValue.ClientID %>").text(),
            Metric_SelectedPereset: $("#<%= lblSelectedPresets.ClientID %>").text(),
            Metric_PresetDescription: $("#<%= lblPresetDescription.ClientID %>").text(),

            Measurement_Device: $("#<%= lblMeasurementDevice.ClientID %>").text(),
            Measurement_Illuminat: $("#<%= lblIlluminant.ClientID %>").text(),
            Measurement_Observer: $("#<%= lblObserver.ClientID %>").text(),
            Measurement_DensityType: $("#<%= lblDesitytype.ClientID %>").text(),
            Measurement_UVMode: $("#<%= lblUVMode.ClientID %>").text(),
            Measurement_BaseChart: $("#<%= lblAlignmentChart.ClientID %>").text(),
            Measurement_OptimizationChart: $("#<%= lblOptimizationChart.ClientID %>").text(),

            Validation_Metric: $("#<%= lblVali_Metric.ClientID %>").text(),
            Validation_MetricDescription: $("#<%= lblVali_Metric_Description.ClientID %>").text(),
            Validation_MeasurementDevice: $("#<%= lblVali_MeaDevice.ClientID %>").text(),
            Validation_UVMode: $("#<%= lblVali_Metric_UVMode.ClientID %>").text(),
            Validation_ValidationChart: $("#<%= lblValiChart.ClientID %>").text(),

            OffSetPrinting_InkOrder: $("#<%= lblInkOderTxt.ClientID %>").text(),
            OffSetPrinting_FountainSolution: $("#<%= lblFountainSolution.ClientID %>").text(),
            OffSetPrinting_Blanket: $("#<%= lblBlanket.ClientID %>").text(),
            OffSetPrinting_BlanketAge: $("#<%= lblBlanketAge.ClientID %>").text(),
            OffSetPrinting_Screening: $("#<%= lblScreening.ClientID %>").text(),
            OffSetPrinting_Coating: $("#<%= lblCoating.ClientID %>").text(),
            OffSetPrinting_ProjectType: $("#<%= lblProjctType.ClientID %>").text(),

            Retargeting_Device: $("#<%= lblRetargetingDeviceName.ClientID %>").text(),
            Retargeting_PrintCondition: $("#<%= lblRetargetingPrintCondition.ClientID %>").text(),

            Show_ValidationBox:  Show_ValidationBox_hdn,
            Show_OffSetPrintingBox: Show_OffSetPrintingBox_hdn,
            Show_RetargetingBox: Show_RetargetingBox_hdn,
            BaseChartId: $("#<%= hdnBaseChartId.ClientID %>").val(),
            OptimizationChartId: $("#<%= hdnOptimizationChartId.ClientID %>").val(),
            ValidationChartId: $("#<%= hdnValidationChartId.ClientID %>").val(),
        }
        var ajaxParameter = {
            type: "POST",
            url: SERVICE_BASE_URL +  'api/Report/ExportPreviewPrintCondition',
            data: apiParameter,
            dataType: "json",
            headers: {
                'Authorization': 'Basic ' + AUTH_TOKEN
            }
        };

        commonAjaxCall(ajaxParameter, function (response) {            
            if (response.data != null) {
                var filename = '~' + response.data;
                window.location.href = SERVICE_BASE_URL + '/api/analyze/downloadfile?filepath=' + filename;
            }
        }, function (XMLHttpRequest, textStatus, errorThrown) {           
            if (XMLHttpRequest.status == 401) {
                ShowDialog("You are not authorized user. Please login again.", "Message");
            } else {
                alert(errorThrown);
            }
        });
        

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
    <asp:LinkButton ID="lnkbtnDownloadReport" runat="server" CssClass="button" OnClientClick="javascript:DownloadPrintPreviewReport();">
        <span>
            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/save_icon.png"
                meta:resourcekey="Image1Resource1" />
            <asp:Localize runat="server" Text="Download Report" ID="locDownalodReport"
                meta:resourcekey="locDownalodReport"></asp:Localize>
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
<div class="listDevice" style="margin-left: 18px; line-height: 25px;">
    <ucStatusbar:Statusbar ID="fujiStatusbar" runat="server" Visible="False" />
    <table cellpadding="0" cellspacing="10" border="0" style="width: 100%; vertical-align: top;">
        <tr>
            <td valign="top" style="width: 100%">
                <div style="display: block;">
                    <div class="section_top_large">
                        &nbsp;
                    </div>
                    <div class="section_large">
                        <div class="combopart">
                            <div class="header" style="width: 670px;">
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
                            <div id="panelDescription" style="padding-left: 10px; width: 670px;">
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
            <td valign="top" style="width: 100%">
                <div style="display: block;">
                    <div class="section_top_large">
                        &nbsp;
                    </div>
                    <div class="section_large">
                        <div class="combopart">
                            <div class="header" style="width: 670px;">
                                <div class="content">
                                    <div class="text" style="float: left">
                                        <label>
                                            <asp:Localize runat="server" Text="Metric Settings" ID="lblMetricSettings"
                                                meta:resourcekey="lblMetricSettings"></asp:Localize></label>
                                    </div>
                                    <div style="float: right">
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                            <div id="panelMetricSettings" style="padding-left: 10px; width: 670px;">
                                <table style="vertical-align: top; width: 100%;">
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

                                    <%-- <div id="Div_SpotTarget" runat="server" visible="false">
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

                                    </div>   --%>
                                    <asp:Panel ID="trRelativePaper" runat="server" Visible="false">
                                        <tr>
                                            <td class="breakWord90">
                                                <span style="font-weight: bold;">
                                                    <asp:Localize runat="server" Text="Relative Paper Scaling" ID="lblRelativePaperScaling"
                                                        meta:resourcekey="lblRelativePaperScaling"></asp:Localize>
                                                </span>
                                            </td>
                                            <td style="width: 10px;">
                                                <span style="font-weight: bold;">:</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMetric_RelationScaling" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="breakWord90">
                                                <span style="font-weight: bold;">
                                                    <asp:Localize runat="server" Text="PaperL *a *b Values" ID="lblPaperLabValues"
                                                        meta:resourcekey="locPaperLabValues"></asp:Localize>
                                                </span>
                                            </td>
                                            <td style="width: 10px;">
                                                <span style="font-weight: bold;">:</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMetric_PaperLABValues" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="breakWord90">
                                                <span style="font-weight: bold;">
                                                    <asp:Localize runat="server" Text="MinL* Patch Values" ID="lblMinLPatchValues"
                                                        meta:resourcekey="locMinLPatchValues"></asp:Localize>
                                                </span>
                                            </td>
                                            <td style="width: 10px;">
                                                <span style="font-weight: bold;">:</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMetric_MinPatchValue" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </asp:Panel>

                                    <tr style="line-height: 16px;">
                                        <td style="vertical-align: top;">
                                            <span style="font-weight: bold;">
                                                <asp:Localize runat="server"
                                                    Text="Selected Presets" ID="locSelectedPresets"
                                                    meta:resourcekey="locSelectedPresetsResource1"></asp:Localize>
                                            </span>
                                        </td>
                                        <td style="width: 10px; vertical-align: top;">
                                            <span style="font-weight: bold;">:</span>
                                        </td>
                                        <td style="vertical-align: top;">
                                            <asp:Label ID="lblSelectedPresets" runat="server"
                                                meta:resourcekey="lblSelectedPresetsResource1"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top;">
                                            <span style="font-weight: bold;">
                                                <asp:Localize runat="server"
                                                    Text="Preset Description" ID="locPresetDesc"
                                                    meta:resourcekey="locPresetDescResource1"></asp:Localize>
                                            </span>
                                        </td>
                                        <td style="width: 10px; vertical-align: top;">
                                            <span style="font-weight: bold;">:</span>
                                        </td>
                                        <td style="vertical-align: top;">
                                            <asp:Label ID="lblPresetDescription" runat="server"
                                                meta:resourcekey="lblPresetDescriptionResource1"></asp:Label>
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
            <td valign="top" style="width: 100%">
                <div style="display: block;">
                    <div class="section_top_large">
                        &nbsp;
                    </div>
                    <div class="section_large">
                        <div class="combopart">
                            <div class="header" style="width: 670px;">
                                <div class="content">
                                    <div class="text" style="float: left">
                                        <label>
                                            <asp:Localize runat="server" Text="Measurment Settings" ID="lblMeasurmentSettings"
                                                meta:resourcekey="lblMeasurmentSettings"></asp:Localize></label>
                                    </div>
                                    <div style="float: right">
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                            <div class="header" style="width: 670px;">
                                <div class="content">
                                    <div class="text" style="float: left">
                                        <table style="width: 100%">
                                            <tr>
                                                <td>
                                                    <span style="font-weight: bold; font-size: 12px;">
                                                        <asp:Localize runat="server"
                                                            Text="Measurement Device" ID="locMeasurementDevice"
                                                            meta:resourcekey="locMeasurementDeviceResource1"></asp:Localize>
                                                    </span>
                                                </td>
                                                <td style="width: 10px;">
                                                    <span style="font-weight: bold;">:</span>
                                                </td>
                                                <td style="font-weight: normal; font-size: 12px;">
                                                    <asp:Label ID="lblMeasurementDevice" runat="server"
                                                        meta:resourcekey="lblMeasurementDeviceResource1"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span style="font-weight: bold; font-size: 12px;">
                                                        <asp:Localize runat="server" Text="Illuminant"
                                                            ID="locIlluminant" meta:resourcekey="locIlluminantResource1"></asp:Localize>t
                                                    </span>
                                                </td>
                                                <td>
                                                    <span style="font-weight: bold;">:</span>
                                                </td>
                                                <td style="font-weight: normal; font-size: 12px;">
                                                    <asp:Label ID="lblIlluminant" runat="server"
                                                        meta:resourcekey="lblIlluminantResource1"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span style="font-weight: bold; font-size: 12px;">
                                                        <asp:Localize runat="server" Text="Observer"
                                                            ID="locObserver" meta:resourcekey="locObserverResource1"></asp:Localize>
                                                    </span>
                                                </td>
                                                <td style="width: 10px;">
                                                    <span style="font-weight: bold;">:</span>
                                                </td>
                                                <td style="font-weight: normal; font-size: 12px;">
                                                    <asp:Label ID="lblObserver" runat="server"
                                                        meta:resourcekey="lblObserverResource1"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span style="font-weight: bold; font-size: 12px;">
                                                        <asp:Localize runat="server" Text="Density Type" ID="lblDensityType"
                                                            meta:resourcekey="lblDensityType"></asp:Localize>
                                                    </span>
                                                </td>
                                                <td style="width: 10px;">
                                                    <span style="font-weight: bold;">:</span>
                                                </td>
                                                <td style="font-weight: normal; font-size: 12px;">
                                                    <asp:Label ID="lblDesitytype" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span style="font-weight: bold; font-size: 12px;">
                                                        <asp:Localize runat="server" Text="UV Mode" ID="locUVMode"
                                                            meta:resourcekey="lblUVMode"></asp:Localize>
                                                    </span>
                                                </td>
                                                <td style="width: 10px;">
                                                    <span style="font-weight: bold;">:</span>
                                                </td>
                                                <td style="font-weight: normal; font-size: 12px;">
                                                    <asp:Label ID="lblUVMode" runat="server"></asp:Label>
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
                            <div id="panelMeasurmentSettings" style="padding-left: 10px; width: 670px;">
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
            <td>
                <table style="width: 100%; vertical-align: top;">
                    <tr>
                        <td style="vertical-align: top; width: 320px;">
                            <asp:Panel ID="tdValidationSetting" runat="server" Visible="false">
                                <div class="section_small_top">
                                    &nbsp;
                                </div>
                                <div class="section_small_middle" style="height: auto; min-height: 320px !important;">
                                    <div class="combopart">
                                        <div class="header">
                                            <div class="content">
                                                <div class="text" style="float: left">
                                                    <label>
                                                        <asp:Localize runat="server" Text="Validation Settings" ID="lblValidationSettings"
                                                            meta:resourcekey="lblValidationSettings"></asp:Localize>
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
                                                        <span style="font-weight: bold;">
                                                            <asp:Localize runat="server" Text="Validation Metric" ID="lblValidationMetric"
                                                                meta:resourcekey="lblValidationMetric"></asp:Localize>
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
                                                        <span style="font-weight: bold;">
                                                            <asp:Localize runat="server" Text="Metric Description" ID="lblMetricDescription"
                                                                meta:resourcekey="lblMetricDescription"></asp:Localize>
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
                                                        <span style="font-weight: bold;">
                                                            <asp:Localize runat="server"
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
                                                        <span style="font-weight: bold;">
                                                            <asp:Localize runat="server" Text="UV Mode" ID="locVUVMode"
                                                                meta:resourcekey="lblUVMode"></asp:Localize>
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
                                                        <span style="font-weight: bold;">
                                                            <asp:Localize runat="server" Text="Validation Chart" ID="lblValidationChart"
                                                                meta:resourcekey="lblValidationChart"></asp:Localize>
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
                            </asp:Panel>
                        </td>
                        <td style="width: 0px"></td>
                        <td style="vertical-align: top; width: 320px;">
                            <asp:Panel ID="trOffSetPrintingInfo" runat="server" Visible="false">
                                <div class="section_small_top">
                                    &nbsp;
                                </div>
                                <div class="section_small_middle">
                                    <div class="combopart">
                                        <div class="header">
                                            <div class="content">
                                                <div class="text" style="float: left">
                                                    <label>
                                                        <asp:Localize runat="server" Text="Offset Printing Information" ID="lblOffsetPrintingInformation"
                                                            meta:resourcekey="lblOffsetPrintingInformation"></asp:Localize>
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
                                                        <span style="font-weight: bold;">
                                                            <asp:Localize runat="server" Text="Ink Order" ID="lblInkOrder"
                                                                meta:resourcekey="lblInkOrder"></asp:Localize>
                                                        </span>&nbsp;
                                                    </td>
                                                    <td style="width: 10px;">
                                                        <span style="font-weight: bold;">:</span>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblInkOderTxt" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 40%">
                                                        <span style="font-weight: bold;">
                                                            <asp:Localize runat="server" Text="Fountain Solution" ID="locFountainSolution"
                                                                meta:resourcekey="lblFountainSolution"></asp:Localize>
                                                        </span>&nbsp;
                                                    </td>
                                                    <td style="width: 10px;">
                                                        <span style="font-weight: bold;">:</span>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblFountainSolution" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 40%">
                                                        <span style="font-weight: bold;">
                                                            <asp:Localize runat="server" Text="Blanket" ID="locBlanket"
                                                                meta:resourcekey="lblBlanket"></asp:Localize>
                                                        </span>&nbsp;
                                                    </td>
                                                    <td style="width: 10px;">
                                                        <span style="font-weight: bold;">:</span>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblBlanket" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 40%">
                                                        <span style="font-weight: bold;">&nbsp;&nbsp;
                                                                <asp:Localize runat="server" Text="Blanket Age" ID="locBlanketAge"
                                                                    meta:resourcekey="lblBlanketAge"></asp:Localize>
                                                        </span>&nbsp;
                                                    </td>
                                                    <td style="width: 10px;">
                                                        <span style="font-weight: bold;">:</span>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblBlanketAge" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 40%">
                                                        <span style="font-weight: bold;">
                                                            <asp:Localize runat="server" Text="Screening" ID="locScreening"
                                                                meta:resourcekey="lblScreening"></asp:Localize>
                                                        </span>&nbsp;
                                                    </td>
                                                    <td style="width: 10px;">
                                                        <span style="font-weight: bold;">:</span>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblScreening" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 40%">
                                                        <span style="font-weight: bold;">
                                                            <asp:Localize runat="server" Text="Coating" ID="locCoating"
                                                                meta:resourcekey="lblCoating"></asp:Localize>
                                                        </span>&nbsp;
                                                    </td>
                                                    <td style="width: 10px;">
                                                        <span style="font-weight: bold;">:</span>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCoating" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 40%">
                                                        <span style="font-weight: bold;">
                                                            <asp:Localize runat="server" Text="Project Type" ID="locProjectType"
                                                                meta:resourcekey="lblProjectType"></asp:Localize>
                                                        </span>&nbsp;
                                                    </td>
                                                    <td style="width: 10px;">
                                                        <span style="font-weight: bold;">:</span>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblProjctType" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </div>
                                </div>
                                <div class="section_small_bottom">
                                    &nbsp;
                                </div>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trRetragetting" runat="server" visible="false">
            <td style="vertical-align: top; width: 320px;">
                <table style="width: 50%; vertical-align: top;">
                    <tr>
                        <td style="vertical-align: top;">
                            <div id="Div1" runat="server" style="display: block;">
                                <div class="section_small_top">
                                    &nbsp;
                                </div>
                                <div class="section_small_middle">
                                    <div class="combopart">
                                        <div class="header" style="width: 310px;">
                                            <div class="content">
                                                <div class="text" style="float: left">
                                                    <label>
                                                        <asp:Localize runat="server" Text="Retargeting" ID="lblRetargeting"
                                                            meta:resourcekey="lblRetargeting"></asp:Localize>
                                                    </label>
                                                </div>
                                                <div class="clear">
                                                </div>
                                            </div>
                                        </div>
                                        <table style="vertical-align: top;">
                                            <tr>
                                                <td style="vertical-align: top;">
                                                    <span style="font-weight: bold;">
                                                        <asp:Localize runat="server" Text="Device" ID="lblDevice"
                                                            meta:resourcekey="lblDevice"></asp:Localize>
                                                    </span>&nbsp;
                                                </td>
                                                <td style="vertical-align: top;">
                                                    <span style="font-weight: bold;">:</span>
                                                </td>
                                                <td style="vertical-align: top; width: 60%">
                                                    <asp:Label ID="lblRetargetingDeviceName" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top;">
                                                    <span style="font-weight: bold;">
                                                        <asp:Localize runat="server" Text="Print Condition" ID="lblPrintCondition"
                                                            meta:resourcekey="lblPrintCondition"></asp:Localize>
                                                    </span>&nbsp;
                                                </td>
                                                <td style="vertical-align: top;">
                                                    <span style="font-weight: bold;">:</span>
                                                </td>
                                                <td style="vertical-align: top; width: 65%;">
                                                    <asp:Label ID="lblRetargetingPrintCondition" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
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
<asp:HiddenField ID="hdnBaseChartId" runat="server" Value="0" />
<asp:HiddenField ID="hdnOptimizationChartId" runat="server" Value="0" />
<asp:HiddenField ID="hdnValidationChartId" runat="server" Value="0" />
<asp:HiddenField ID="hdnShowValidationBox" runat="server" Value="0" />
<asp:HiddenField ID="hdnShowOffSetPrinting" runat="server" Value="0" />
<asp:HiddenField ID="hdnShowRetargeting" runat="server" Value="0" />
<asp:HiddenField runat="server" ID="hdnBaseServiceUrl" ClientIDMode="Static" />
<asp:HiddenField runat="server" ID="hdfAuthToken" ClientIDMode="Static" />
