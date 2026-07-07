////*********** Declarations ***********////
var jsonTarget = [];
var jsonColors = [];
var jsonGray = [];
var jsonDeltaEPrimaryPatchList = [];
var jsonDeltaPaperLAB_1 = [];
var jsonDensityList = [];

//G7 result array
var jsonAnalyzeList = [];

//G7 result array
var jsonGrayFChartList = [];
var jsonBlackLChartList = [];
var jsonGrayLChartList = [];
var jsonBlackNPDChartList = [];
var jsonGrayNPDChartList = [];

//TVI result array
var jsonTVIG7ChartList = [];
var jsonDTISOTVIChartList = [];
var jsonDTSpreadChartData_CMYKList = [];
var jsonDTSpreadChartDataList = [];
var jsonDTSpreadSplineRangeChartDataList = [];

//DeltaSummaryDetails array
var jsonSummaryNameList = [];
var jsonSummaryValueList = [];
var jsonSummaryLimitList = [];
var measurementNamesList = [];
var _Resources = {};


var CURRENT_USER_ID
var SERVICE_BASE_URL = '';
//var PLUGIN_SERVICE_URL = '';
var $hdnBaseServiceUrl;
var $hdnCurrentUserID;
var $btnGeneratePDF;
var lnkDownloadReport;
var $lnkABComparisionLargeChart;
var $chartLoader;
var $hdMeasurement;
var $hdnProjectID;
var $hdnOptimizationRunID;
var $chart_TargetVersusActualAB;
var $chart_Gamut_ABComparison;
var $chart_L_Comaparision;
var $chart_L_BalanceChart;
var $chart_A_BalanceChart;
var $chart_B_BalanceChart;
var $chartTargetVersusActualABLarge;
var $chartTargetVersusActualABLargeArea;
var $kendoChartDummy;
var $hdnVerification;
var $hdnSync1DType;
var $hdnValidationType;
var $hdnCurrentDlpSubOperation;
var $isAverageOfMeasurements;
var $CurrentCulture;
var $rbtnListDeltaE;
var $divChartInkPaperABComparision;
var $selectedDeltaE;
var $divChartInkPaperPaperLab1;
var $divChartInkPaperPaperLab2;
var $divChartInkPaperDansityChart;
var $divChartG7GrayBalanceDeltaCh;
var $divChartG7BlackDeltaL;
var $divChartG7GrayDeltaL;
var $divChartG7BlackNPD;
var $divChartG7GrayNPD;
var $divChartTVISummaryDotGain;
var $divChartSummaryDeltaChart
var $divChartTVISpread;
var $divTVI_G7;
var $divTVI_ISO;
var $divMeasurementPatchDetails;
var $divAnalyzeChart;
var $divAnalyzeDestinationChart;
var $divChartTVIISO_CYAN;
var $divChartTVIISO_Magenta;
var $divChartTVIISO_Yellow;
var $divChartTVIISO_Black;
var $divChartTVIISO_CMYK;
var $tblDensityChart;
var $hdnReportDLPName;
var $hdnCurrentMeasurement;
var viewNumber = 0;
var pageNumber = 0;
var totalPages = 0;
var $btnPrevious;
var $ddlView;
var $ddlPaging;
var $btnNext;
var $btnLast;
var $btnFirst;
var $hdnCurrentProjectLicenseType;
var labelPrintXMLString = '';
var labelPrintStatusIconString = '';
var reportGradingPath = '';
var reportCMYKDensityPath = '';
var $lnkLComparisionLargeChart;
var $lnkAB_ComparisionLargeChart;
var $lnkPaperPaperLab1LargeChart;
var $lnkPaperPaperLab2LargeChart;
var $lnkGrayBalanceDeltaCHLargeChart;
var $lnkBlackDeltaLLargeChart;
var $lnkGrayDeltaLLargeChart;
var $lnk_A_GrayBalanceLargeChart;
var $lnk_B_GrayBalanceLargeChart;
var $lnk_L_GrayBalanceLargeChart;
var $hdnCurrentUserRole;
var AUTH_TOKEN = '';
var $hdnSyncDlpPlusType;
var $hdnIsDlpMultiColor;
var $hdnIsSync1DMultiColor;
var $hdnCurrentRunName;
var labelPrintHTMLString = '';
var $hdnCloudFlowCheck;

//********** kendo objects *********//
var drawing = kendo.drawing;
var geometry = kendo.geometry;

//********** Gradient kendo objects *********//

var gradientGray = new drawing.LinearGradient({
    start: [0, 0], // Bottom left
    end: [0, 1],   // Top left
    stops: [{
        offset: 0,
        color: "#000",
        opacity: 0.2
    }, {
        offset: 1,
        color: "#000",
        opacity: 0
    }]
});

var gradientRedGreen = new drawing.LinearGradient({
    start: [0, 1], // Bottom left
    end: [1, 1],   // Top left
    stops: [{
        offset: 0,
        color: "green",
        opacity: 0.4
    }, {
        offset: 1,
        color: "red",
        opacity: 0.2
    }]
});

var gradientPinkGreen = new drawing.LinearGradient({
    start: [0, 0], // Bottom left
    end: [0, 1],   // Top left
    stops: [{
        offset: 0,
        color: "#FF7FFF",
        //color: "pink",
        opacity: 0.5
    },
    {
        offset: 1,
        //color: "green",
        color: "#4C9371",
        opacity: 0.8
    }]
});

var gradientYellowBlue = new drawing.LinearGradient({
    start: [0, 0], // Bottom left
    end: [0, 1],   // Top left
    stops: [{
        offset: 0,
        color: "yellow",
        //color: "#F0EF9D",
        opacity: 0.5
    },
    {
        offset: 1,
        color: "blue",
        //color: "#4748C7",
        opacity: 0.8
    }]
});

var gradientGray2 = new drawing.LinearGradient({
    start: [1, 1], // Bottom left
    end: [1, 0],   // Top left
    stops: [{
        offset: 0,
        color: "#000",
        opacity: 0.3
    }, {
        offset: 1,
        color: "#000",
        opacity: 0
    }]
});

var gradientGray3 = new drawing.LinearGradient({
    start: [0, 0], // Bottom left
    end: [0, 1],   // Top left
    stops: [{
        offset: 0,
        color: "#000",
        opacity: 0
    }, {
        offset: 1,
        color: "#000",
        opacity: 0.4
    }]
});

$(document).ready(function () {
    ////*********** HTML Element Declarations ***********////
    $hdnBaseServiceUrl = $('#hdnBaseServiceUrl').val();
    $hdnCurrentUserID = $('#hdnCurrentUserID').val();
    $CurrentCulture = $("#hdfCurrentCulture").val();
    $hdnVerification = $('#hdnVerification').val();
    $hdnSync1DType = $('#hdnSync1DType').val();
    $hdnValidationType = isNaN(parseInt($('#hdnValidationType').val())) ? 0 : parseInt($('#hdnValidationType').val());
    $hdnCurrentDlpSubOperation = parseInt($('#hdnCurrentDlpSubOperation').val());
    $hdnReportDLPName = $('#hdnReportDLPName').val();
    $isAverageOfMeasurements = $('#chkAnalyzeAverageOfMeasurements').is(':checked');
    $hdMeasurement = $('#ContentPlaceHolder1_ucAnalyze_hdMeasurement');
    $hdnProjectID = $('#hdnProjectID').val();
    $hdnOptimizationRunID = $('#hdnOptimizationRunID').val();
    $chartLoader = $(".chart-loading");
    $lnkABComparisionLargeChart = $('#lnkABComparisionLargeChart');
    $btnGeneratePDF = $('#btnGeneratePDF');
    $lnkDownloadReport = $('#lnkDownloadReport');
    $rbtnListDeltaE = $('#rbtnListDeltaE');
    $chart_TargetVersusActualAB = $("#kendoChartTargetVersusActualAB");
    $chart_Gamut_ABComparison = $("#kendoChartGamut_ABComparison");
    $chart_L_Comaparision = $("#kendoChartLComaparision");
    $chart_L_BalanceChart = $("#kendoChartLBalanceChart");
    $chart_A_BalanceChart = $("#kendoChartABalanceChart");
    $chart_B_BalanceChart = $("#kendoChartBBalanceChart");
    $chartTargetVersusActualABLarge = $("#kendoChartTargetVersusActualABLarge");
    $chartTargetVersusActualABLargeArea = $("#kendoChartTargetVersusActualABLargeArea");
    $kendoChartDummy = $("#kendoChartDummy");
    $divChartInkPaperABComparision = $("#divChartInkPaperABComparision");
    $divChartInkPaperPaperLab1 = $("#divChartInkPaperPaperLab1");
    $divChartInkPaperPaperLab2 = $("#divChartInkPaperPaperLab2");
    $divChartInkPaperDansityChart = $("#divChartInkPaperDansityChart");
    $divChartG7GrayBalanceDeltaCh = $("#divChartG7GrayBalanceDeltaCh");
    $divChartG7BlackDeltaL = $("#divChartG7BlackDeltaL");
    $divChartG7GrayDeltaL = $("#divChartG7GrayDeltaL");
    $divChartG7BlackNPD = $("#divChartG7BlackNPD");
    $divChartG7GrayNPD = $("#divChartG7GrayNPD");
    $divChartTVISummaryDotGain = $("#divChartTVISummaryDotGain");
    $divChartTVISpread = $("#divChartTVISpread");
    $divTVI_G7 = $("#divTVI_G7");
    $divTVI_ISO = $("#divTVI_ISO");
    $divMeasurementPatchDetails = $("#divMeasurementPatchDetails");
    $divChartTVIISO_CYAN = $("#divChartTVIISO_CYAN");
    $divChartTVIISO_Magenta = $("#divChartTVIISO_Magenta");
    $divChartTVIISO_Yellow = $("#divChartTVIISO_Yellow");
    $divChartTVIISO_Black = $("#divChartTVIISO_Black");
    $divChartTVIISO_CMYK = $("#divChartTVIISO_CMYK");
    $divChartSummaryDeltaChart = $("#divChartSummaryDeltaChart");
    $divAnalyzeChart = $("#divAnalyzeChart");
    $divAnalyzeDestinationChart = $("#divAnalyzeDestinationChart");
    $tblDensityChart = $("#tblDensityChart");
    $selectedDeltaE = $rbtnListDeltaE.find('input:checked').val();
    $hdnCurrentMeasurement = $("#hdnCurrentMeasurement");
    $btnPrevious = $('#btnPrevious');
    $ddlView = $('#ddlView');
    $ddlPaging = $('#ddlPaging');
    $btnNext = $('#btnNext');
    $btnLast = $('#btnLast');
    $btnFirst = $('#btnFirst');
    $hdnCurrentProjectLicenseType = $('#hdnCurrentProjectLicenseType').val();
    $lnkLComparisionLargeChart = $('#lnkLComparisionLargeChart');
    $lnkAB_ComparisionLargeChart = $('#lnkAB_ComparisionLargeChart');
    $lnkPaperPaperLab1LargeChart = $('#lnkPaperPaperLab1LargeChart');
    $lnkPaperPaperLab2LargeChart = $('#lnkPaperPaperLab2LargeChart');
    $lnkGrayBalanceDeltaCHLargeChart = $('#lnkGrayBalanceDeltaCHLargeChart');
    $lnkBlackDeltaLLargeChart = $('#lnkBlackDeltaLLargeChart');
    $lnkGrayDeltaLLargeChart = $('#lnkGrayDeltaLLargeChart');
    $lnk_A_GrayBalanceLargeChart = $('#lnk_A_GrayBalanceLargeChart');
    $lnk_B_GrayBalanceLargeChart = $('#lnk_B_GrayBalanceLargeChart');
    $lnk_L_GrayBalanceLargeChart = $('#lnk_L_GrayBalanceLargeChart');
    //PLUGIN_SERVICE_URL = $('#hdnBasePluginServiceUrl').val();
    $hdnCurrentUserRole = $('#hdnCurrentUserRole');
    CURRENT_USER_ID = parseInt($hdnCurrentUserID);
    SERVICE_BASE_URL = $hdnBaseServiceUrl;
    AUTH_TOKEN = $("#hdfAuthToken").val();
    $hdnSyncDlpPlusType = $('#hdnSyncDlpPlusType').val();
    $hdnIsDlpMultiColor = $('#hdnIsDlpMultiColor').val();
    $hdnIsSync1DMultiColor = $('#hdnIsSync1DMultiColor').val();
    $hdnCurrentRunName = $('#hdnCurrentRunName').val();
    $hdnCloudFlowCheck = $('#hdnCloudFlowCheck').val();

    //check deltaE is underfined in validation project
    if (!$selectedDeltaE) {
        if ($hdnCurrentDlpSubOperation == ELicenseType.Sync1D || $hdnCurrentDlpSubOperation == ELicenseType.Sync1DMultiColor || $hdnCurrentDlpSubOperation == ELicenseType.Validation) {
            $selectedDeltaE = $('#hdnDefaultSelectedDelta').val();
        }
        else {
            $selectedDeltaE = 'E2000';  //set default value for sync1D projects
        }
    }
    //Bind analyze main chart
    BindAnalyzeChart();

    //Bind all chart tabs
    //BindAllTabs()


    $("#tabstrip").kendoTabStrip({
        animation: {
            open: {
                effects: "fadeIn"
            }
        }
    });

    //Bind click event for report generation
    $lnkDownloadReport.on('click', function (e) {
        e.preventDefault();
        DownloadReportFile();
    });

    //Bind click event for report generation
    $btnGeneratePDF.on('click', function (e) {
        e.preventDefault();
        DownloadReportFile();
    });

    function DownloadReportFile() {
        kendo.ui.progress($('#divReport'), true);
        var measurementID = parseInt($hdnCurrentMeasurement.val());
        var apiParameter = {
            //chart: chartContent,
            MeasurementId: measurementID,
            IsAverageOfMeasurements: $isAverageOfMeasurements,
            IsVerificationProject: $hdnVerification,
            IsSync1DTypeProject: $hdnSync1DType,
            fromDeltaE: $selectedDeltaE,
            validationType: $hdnValidationType,
            CurrentUserId: CURRENT_USER_ID,
            ReportDLPName: $hdnReportDLPName,
            ABComparisionSpiderChart: '',
            InkPaperPatchChart: '',
            ABActualTargetChart: '',
            LComparisionChart: '',
            LGrayBalanceChart: '',
            AGrayBalanceChart: '',
            BGrayBalanceChart: '',
            DeltaSummaryChart: '',
            InkPaperLAB1Chart: '',
            InkPaperLAB2Chart: '',
            InkPaperDansityChart: '',
            TVI_ISO_CMYK_Chart: '',
            TVI_ISO_Cyan_Chart: '',
            TVI_ISO_Magenta_Chart: '',
            TVI_ISO_Yellow_Chart: '',
            TVI_ISO_Black_Chart: '',

            TVI_Summary_Chart: '',
            GrayBalanceDeltaCH_Chart: '',
            GrayBalanceDeltaL_Chart: '',
            BlackBalanceDeltaL_Chart: '',
            BlackNPDChart: '',
            GrayNPDChart: '',

        };

        apiParameter.ABComparisionSpiderChart = GetABComparisionSpiderChartImage();
        apiParameter.InkPaperPatchChart = GetInkPaperPatchChartImage();
        if ($hdnValidationType == 1) {

            apiParameter.LComparisionChart = GetLComparisionChartImage();
            apiParameter.LGrayBalanceChart = Get_L_BalanceChartImage();
            apiParameter.AGrayBalanceChart = Get_A_BalanceChartImage();
            apiParameter.BGrayBalanceChart = Get_B_BalanceChartImage();
            apiParameter.InkPaperDansityChart = Get_InkPaperChartDansityChartImage();
            if ($hdnCurrentDlpSubOperation != ELicenseType.DLPPlus) {
                apiParameter.TVI_Summary_Chart = Get_TVISummaryDotGainChartImage();
            }
            else {
                apiParameter.TVI_Summary_Chart = "";
            }
            apiParameter.GrayBalanceDeltaCH_Chart = Get_GrayBalanceDelta_CH_ChartImage();
            apiParameter.GrayBalanceDeltaL_Chart = Get_GrayDelta_L_ChartImage();
            apiParameter.BlackBalanceDeltaL_Chart = Get_BlackDelta_L_ChartImage();
            apiParameter.BlackNPDChart = Get_Black_NPD_ChartImage();
            apiParameter.GrayNPDChart = Get_Gray_NPD_ChartImage();
            apiParameter.DeltaSummaryChart = Get_DeltaSummaryChartImage();
            apiParameter.ABActualTargetChart = GetActualVsTargetChartImage();
        }
        else if ($hdnValidationType == 2) {
            apiParameter.LComparisionChart = GetLComparisionChartImage();
            apiParameter.LGrayBalanceChart = Get_L_BalanceChartImage();
            apiParameter.AGrayBalanceChart = Get_A_BalanceChartImage();
            apiParameter.BGrayBalanceChart = Get_B_BalanceChartImage();

            apiParameter.InkPaperLAB1Chart = Get_InkPaperChartLAB_OneChartImage();
            apiParameter.InkPaperLAB2Chart = Get_InkPaperChartLAB_TwoChartImage();
            apiParameter.InkPaperDansityChart = Get_InkPaperChartDansityChartImage();

            apiParameter.TVI_ISO_CMYK_Chart = Get_TVI_ISO_CMYK_ChartImage();
            apiParameter.TVI_ISO_Cyan_Chart = Get_TVI_ISO_CYAN_ChartImage();
            apiParameter.TVI_ISO_Magenta_Chart = Get_TVI_ISO_MAGENTA_ChartImage();
            apiParameter.TVI_ISO_Yellow_Chart = Get_TVI_ISO_YELLOW_ChartImage();
            apiParameter.TVI_ISO_Black_Chart = Get_TVI_ISO_BLACK_ChartImage();
            apiParameter.ABActualTargetChart = GetActualVsTargetChartImage();
        }
        else if ($hdnValidationType == 0) {
            apiParameter.LGrayBalanceChart = Get_L_BalanceChartImage();
            apiParameter.AGrayBalanceChart = Get_A_BalanceChartImage();
            apiParameter.BGrayBalanceChart = Get_B_BalanceChartImage();
            apiParameter.InkPaperLAB1Chart = Get_InkPaperChartLAB_OneChartImage();
            apiParameter.InkPaperLAB2Chart = Get_InkPaperChartLAB_TwoChartImage();
            apiParameter.DeltaSummaryChart = Get_DeltaSummaryChartImage();
        }
        else {
            apiParameter.DeltaSummaryChart = Get_DeltaSummaryChartImage();
            //Start: - ------Extra charts added to support new version of 4D single page report-----------	
            apiParameter.ABActualTargetChart = GetActualVsTargetChartImage();
            apiParameter.LComparisionChart = GetLComparisionChartImage();
            apiParameter.LGrayBalanceChart = Get_L_BalanceChartImage();
            apiParameter.AGrayBalanceChart = Get_A_BalanceChartImage();
            apiParameter.BGrayBalanceChart = Get_B_BalanceChartImage();
            //End:
        }
        apiParameter.ProjectLicenseType = $hdnCurrentProjectLicenseType;
        apiParameter.LicenceType = $hdnCurrentDlpSubOperation;

        var ajaxParameter = {
            type: "POST",
            url: SERVICE_BASE_URL + 'api/Report/ExportAnalyzeChartReport',
            data: apiParameter,
            dataType: "json",
            headers: {
                'Authorization': 'Basic ' + AUTH_TOKEN
            }
        };

        commonAjaxCall(ajaxParameter, function (response) {
            kendo.ui.progress($('#divReport'), false);
            if (response.data != null) {
                var filename = '~' + response.data;
                window.location.href = SERVICE_BASE_URL + '/api/analyze/downloadfile?filepath=' + filename;
            }
        }, function (XMLHttpRequest, textStatus, errorThrown) {
            kendo.ui.progress($('#divReport'), false);
            if (XMLHttpRequest.status == 401) {
                ShowDialog("You are not authorized user. Please login again.", "Message");
            } else {
                alert(errorThrown);
            }
        });

    }


    $lnkABComparisionLargeChart.on('click', function (e) {
        e.preventDefault();

        var kendoOptions = $chart_TargetVersusActualAB.data("kendoChart").options;
        $chartTargetVersusActualABLarge.kendoChart(kendoOptions);
        $chartTargetVersusActualABLarge.data("kendoChart").options.chartArea.width = 580;
        $chartTargetVersusActualABLarge.data("kendoChart").options.chartArea.height = 480;


        $chartTargetVersusActualABLargeArea.kendoWindow({
            width: "600px",
            modal: true,
            height: '500px',
            resizable: false,
            title: $('#ContentPlaceHolder1_ucAnalyze_Td6').text().trim(),
            //content: url,
            visible: false
        });

        var popup = $chartTargetVersusActualABLargeArea.data('kendoWindow');
        popup.open();
        popup.center();
    });

    $lnkLComparisionLargeChart.on('click', function (e) {
        e.preventDefault();

        var kendoOptions = $chart_L_Comaparision.data("kendoChart").options;
        //kendoOptions.chartArea.width = 580;
        //kendoOptions.chartArea.height = 180;

        magnifyLpaperCharts(kendoOptions, 180, 580, '200px', '600px', $('#ContentPlaceHolder1_ucAnalyze_Td7').text().trim());

    });

    $lnkAB_ComparisionLargeChart.on('click', function (e) {
        e.preventDefault();
        var kendoOptions = $chart_Gamut_ABComparison.data("kendoChart").options;
        $('#kendoChart_AB_ComparisionLarge').kendoChart(kendoOptions);
        $('#kendoChart_AB_ComparisionLarge').data("kendoChart").options.chartArea.width = 580;
        $('#kendoChart_AB_ComparisionLarge').data("kendoChart").options.chartArea.height = 480;

        $('#kendoChart_AB_ComparisionLargeArea').kendoWindow({
            width: "600px",
            modal: true,
            height: '500px',
            resizable: false,
            title: $('#divABGamutComparision').text().trim(),
            visible: false
        });

        var popup = $('#kendoChart_AB_ComparisionLargeArea').data('kendoWindow');
        popup.open();
        popup.center();
    });

    $lnkPaperPaperLab1LargeChart.on('click', function (e) {
        e.preventDefault();
        var kendoOptions = $divChartInkPaperPaperLab1.data("kendoChart").options;
        $('#kendoChart_AB_ComparisionLarge').kendoChart(kendoOptions);
        $('#kendoChart_AB_ComparisionLarge').data("kendoChart").options.chartArea.width = 580;
        $('#kendoChart_AB_ComparisionLarge').data("kendoChart").options.chartArea.height = 480;

        $('#kendoChart_AB_ComparisionLargeArea').kendoWindow({
            width: "600px",
            modal: true,
            height: '500px',
            resizable: false,
            title: $('#spanlocPaperAB').text().trim(),
            visible: false
        });

        var popup = $('#kendoChart_AB_ComparisionLargeArea').data('kendoWindow');
        popup.open();
        popup.center();
    });

    $lnkPaperPaperLab2LargeChart.on('click', function (e) {
        e.preventDefault();

        var kendoOptions = $divChartInkPaperPaperLab2.data("kendoChart").options;
        //kendoOptions.chartArea.width = 180;
        //kendoOptions.chartArea.height = 375;

        magnifyLpaperCharts(kendoOptions, 375, 180, '400px', '200px', $('#spanlocPaperL').text().trim());
    });

    $lnkGrayBalanceDeltaCHLargeChart.on('click', function (e) {
        e.preventDefault();

        var kendoOptions = $divChartG7GrayBalanceDeltaCh.data("kendoChart").options;
        kendoOptions.chartArea.width = 580;
        kendoOptions.chartArea.height = 180;

        magnifyG7Chart(kendoOptions, '200px', '600px', $('#spanGrayBalanceDeltaCH').text().trim());
    });

    $lnkBlackDeltaLLargeChart.on('click', function (e) {
        e.preventDefault();

        var kendoOptions = $divChartG7BlackDeltaL.data("kendoChart").options;
        kendoOptions.chartArea.width = 580;
        kendoOptions.chartArea.height = 180;

        magnifyG7Chart(kendoOptions, '200px', '600px', $('#spanBlackDeltaL').text().trim());
    });

    $lnkGrayDeltaLLargeChart.on('click', function (e) {
        e.preventDefault();
        var kendoOptions = $divChartG7GrayDeltaL.data("kendoChart").options;
        //kendoOptions.chartArea.width = 580;
        //kendoOptions.chartArea.height = 180;

        magnifyG7Chart(kendoOptions, '200px', '600px', $('#spanGrayDeltaL').text().trim());
    });

    $lnk_A_GrayBalanceLargeChart.on('click', function (e) {
        e.preventDefault();

        var kendoOptions = $chart_A_BalanceChart.data("kendoChart").options;
        $('#kendoChart_A_Balance_Large').kendoChart(kendoOptions);
        $('#kendoChart_A_Balance_Large').data("kendoChart").options.chartArea.width = 580;
        $('#kendoChart_A_Balance_Large').data("kendoChart").options.chartArea.height = 200;



        $('#kendoChart_A_Balance_LargeArea').kendoWindow({
            width: "600px",
            modal: true,
            height: '225px',
            resizable: false,
            title: $('#spanGrayBalanceA').text().trim(),
            visible: false
        });

        var popup = $('#kendoChart_A_Balance_LargeArea').data('kendoWindow');
        popup.open();
        popup.center();
    });

    $lnk_B_GrayBalanceLargeChart.on('click', function (e) {
        e.preventDefault();

        var kendoOptions = $chart_B_BalanceChart.data("kendoChart").options;
        $('#kendoChart_B_Balance_Large').kendoChart(kendoOptions);
        $('#kendoChart_B_Balance_Large').data("kendoChart").options.chartArea.width = 580;
        $('#kendoChart_B_Balance_Large').data("kendoChart").options.chartArea.height = 200;

        $('#kendoChart_B_Balance_LargeArea').kendoWindow({
            width: "600px",
            modal: true,
            height: '225px',
            resizable: false,
            title: $('#spanGrayBalanceB').text().trim(),
            visible: false
        });

        var popup = $('#kendoChart_B_Balance_LargeArea').data('kendoWindow');
        popup.open();
        popup.center();
    });

    $lnk_L_GrayBalanceLargeChart.on('click', function (e) {
        e.preventDefault();

        var kendoOptions = $chart_L_BalanceChart.data("kendoChart").options;
        kendoOptions.chartArea.width = 580;
        kendoOptions.chartArea.height = 200;

        magnifyLpaperCharts(kendoOptions, 200, 580, '225px', '600px', $('#spanGrayBalanceL').text().trim());


    });

    $('[id^=rbtnListDeltaE_]').on('click', function (e) {

        $selectedDeltaE = $(this).val();
        BindAnalyzeChart();
    });

    //------------ Analyze paging feture events -------------//
    $ddlView.on('change', function (e) {
        e.preventDefault();
        BindPageNumber();
        PopulateAnalyzeChart();
    });

    $ddlPaging.on('change', function (e) {
        e.preventDefault();
        PopulateAnalyzeChart();
    });

    $btnPrevious.on('click', function (e) {
        e.preventDefault();

        var prevPageNumber = pageNumber - 1;
        $ddlPaging.val(prevPageNumber.toString());
        if (prevPageNumber == 1) {
            SetPrevButtonDisabled(true);
        }
        SetNextButtonDisabled(false);
        PopulateAnalyzeChart();
    });

    $btnNext.on('click', function (e) {
        e.preventDefault();

        var nextPageNumber = pageNumber + 1;
        $ddlPaging.val(nextPageNumber.toString());
        if (nextPageNumber == totalPages) {
            SetNextButtonDisabled(true);
        }
        SetPrevButtonDisabled(false);
        PopulateAnalyzeChart();
    });

    $btnFirst.on('click', function (e) {
        e.preventDefault();
        $ddlPaging.val('1');

        SetPrevButtonDisabled(true);
        SetNextButtonDisabled(false);
        PopulateAnalyzeChart();
    });

    $btnLast.on('click', function (e) {
        e.preventDefault();
        $ddlPaging.val(totalPages);

        SetPrevButtonDisabled(false);
        SetNextButtonDisabled(true);
        PopulateAnalyzeChart();
    });
});

function magnifyG7Chart(chartOptions, popupHeight, popupWidth, titleText) {

    $('#kendoChart_G7_Large').kendoChart(chartOptions);
    $('#kendoChart_G7_Large').data('kendoChart').options.legend.visible = false;
    $('#kendoChart_G7_Large').data('kendoChart').options.chartArea.width = 580;
    $('#kendoChart_G7_Large').data('kendoChart').options.chartArea.height = 180;

    if ($('#kendoChart_G7_LargeArea').data('kendoWindow')) {
        $('#kendoChart_G7_LargeArea').data('kendoWindow').title(titleText);
    } else {
        $('#kendoChart_G7_LargeArea').kendoWindow({
            title: titleText,
            width: popupWidth,
            height: popupHeight,
            modal: true,
            resizable: false,
            visible: false
        });
    }

    var popup = $('#kendoChart_G7_LargeArea').data('kendoWindow');
    popup.open();
    popup.center();
}

function magnifyLpaperCharts(chartOptions, chartHeight, chartWidth, heightSize, widthSize, titleText) {
    $('#kendoChart_L_ComparisionLarge').kendoChart(chartOptions);
    $('#kendoChart_L_ComparisionLarge').data('kendoChart').options.chartArea.width = chartWidth;
    $('#kendoChart_L_ComparisionLarge').data('kendoChart').options.chartArea.height = chartHeight;


    if ($('#kendoChart_L_ComparisionLargeArea').data('kendoWindow')) {
        $('#kendoChart_L_ComparisionLargeArea').data('kendoWindow').title(titleText);
        $('#kendoChart_L_ComparisionLargeArea').data('kendoWindow').setOptions({
            width: widthSize,
            height: heightSize
        });
    } else {
        $('#kendoChart_L_ComparisionLargeArea').kendoWindow({
            title: titleText,
            width: widthSize,
            height: heightSize,
            modal: true,
            resizable: false,
            visible: false
        });
    }

    var popup = $('#kendoChart_L_ComparisionLargeArea').data('kendoWindow');
    popup.open();
    popup.center();
}



////**************** START BIND THE ALL TABS  ****************////
function BindAllTabs() {

    var tabStrip = $("#tabstrip").kendoTabStrip().data("kendoTabStrip");
    $isAverageOfMeasurements = $('#chkAnalyzeAverageOfMeasurements').is(':checked');

    $(tabStrip.items()[0]).css('display', 'inline-block');
    $(tabStrip.items()[1]).css('display', 'inline-block');
    $(tabStrip.items()[3]).css('display', 'inline-block');
    $(tabStrip.items()[5]).css('display', 'inline-block');
    $(tabStrip.items()[6]).css('display', 'inline-block');
    $(tabStrip.items()[7]).css('display', 'inline-block');

    //Hide default tabs G7
    //$(tabStrip.items()[2]).hide();

    //Hide default tabs TVI
    //$(tabStrip.items()[4]).hide();

    //Bind Summary tab
    BindMeasurementSummary()

    //Ink paper tab charts
    BindInkPaperChartABComparision();

    //Bind gray(Gamut) charts
    BindGrayCharts();

    //Bind current measurement patch details

    if (($hdnIsDlpMultiColor == "True" || $hdnIsSync1DMultiColor == "True") && ($hdnCurrentRunName == "Run-0")) {
        BindMeasurementPatchDetailsForMultiColor(); //It called DLPMultiColor and Sync1DMultiColor
    }
    else {
        BindMeasurementPatchDetails();
    }

    //Bind report image
    BindReportImage();

    //Bind delta summary chart to display in report
    BindDeltaSummaryStatesChart();

    $('#divToRender').css('display', 'block');
    $('#divTabContainerLoading').css('display', 'none');
    UpdateAvgMeasurementSummary();
}

////**************** Bind the measurement summary details ****************////
function BindMeasurementSummary() {
    var measurementID = parseInt($hdnCurrentMeasurement.val());
    var apiParam = {};

    $("#divMeasurementSummary").css('display', 'none');
    $("#divValidationSummary").css('display', 'none');

    if ($hdnCurrentDlpSubOperation == ELicenseType.Sync1D || $hdnCurrentDlpSubOperation == ELicenseType.Validation || $hdnCurrentDlpSubOperation == ELicenseType.DLPPlus || $hdnCurrentDlpSubOperation == ELicenseType.Sync1DMultiColor) {

        $("#divValidationSummary").css('display', 'block');
        apiParameter = {
            MeasurementId: measurementID,
            IsAverageOfMeasurements: $isAverageOfMeasurements,
            IsVerificationProject: $hdnVerification,
            IsSync1DTypeProject: $hdnSync1DType,
            fromDeltaE: $selectedDeltaE,
            IsSyncDlpPlusTypeProject: $hdnSyncDlpPlusType
        }
        
        $.ajax({
            url: SERVICE_BASE_URL + 'api/Analyze/GetValidationMeasurementSummaryDetails',
            data: apiParameter,
            type: 'GET',
            async: false,
            dataType: 'json',
            headers: {
                'Accept-Language': $CurrentCulture,
                "content-type": "application/json",
                'Authorization': 'Basic ' + AUTH_TOKEN
            },
            success: function (response) {
                if (response.data != null) {
                    if (response.data.length > 0) {

                        var measurements = response.data;
                        var measurement = measurements[0];

                        //generate dymo printing string to print the dymo label only for validation
                        if ($hdnCurrentDlpSubOperation == ELicenseType.Validation) {
                            PopulateDymoPrinter(measurement);
                            PopulateDymoPrinterNew(measurement);
                        }
                        measurement.MeasurementDateFormatted = new Date(measurement.MeasurementDate);
                        $hdnValidationType = measurement.ValidationType;

                        var viewModel = kendo.observable(measurement);
                        viewModel.isSync1DLicenseType = !(($hdnCurrentDlpSubOperation == ELicenseType.Sync1D || $hdnCurrentDlpSubOperation == ELicenseType.Sync1DMultiColor));
                        viewModel.isCurrentDlpSubOperation = ($hdnCurrentDlpSubOperation == ELicenseType.Sync1D || $hdnCurrentDlpSubOperation == ELicenseType.Sync1DMultiColor) ? ($hdnCurrentDlpSubOperation == ELicenseType.Validation) ? true : false : true;


                        if (measurements[0].PatchTolerances == null || measurements[0].PatchTolerances.length == 0) {

                            $('#tblPatchTolerance').css('display', 'none');
                        }
                        kendo.bind($("#divValidationSummary"), viewModel);
                        $('#tblAverageMeasurementSummary').css('display', 'none');

                        if ($hdnCurrentProjectLicenseType == ELicenseType.Sync1D) {

                            $('#rowCustomer').hide();
                            $('#rowPageSurface').hide();
                            $('#rowJobID').hide();
                            $('#rowNotes').hide();
                            if ($hdnCurrentDlpSubOperation == ELicenseType.Validation) {
                                $('#rowCustomer').show();
                                $('#rowNotes').show();
                            }
                        }
                        if ($hdnCurrentProjectLicenseType == ELicenseType.Sync1DMultiColor) {

                            $('#rowCustomer').hide();
                            $('#rowPageSurface').hide();
                            $('#rowJobID').hide();
                            $('#rowNotes').hide();
                            if ($hdnCurrentDlpSubOperation == ELicenseType.Validation) {
                                $('#rowCustomer').show();
                                $('#rowNotes').show();
                            }
                        }
                        if ($hdnCurrentDlpSubOperation == ELicenseType.DLPPlus) {

                            $('#rowCustomer').hide();
                            $('#rowPageSurface').hide();
                            $('#rowJobID').hide();
                            $('#rowNotes').hide();
                            if ($hdnCurrentDlpSubOperation == ELicenseType.Validation) {
                                $('#rowCustomer').show();
                                $('#rowNotes').show();
                            }
                            var tabStrip = $("#tabstrip").kendoTabStrip().data("kendoTabStrip");
                            $(tabStrip.items()[2]).css('display', 'inline-block');
                            //Bind G7 charts
                            //BindG7Charts(); 10/10/2022 Slowness issue that's why we comment, this calling 2 times .
                            //show average measurement checkbox
                            $('#ContentPlaceHolder1_ucAnalyze_tdAverageOfMeasurements').css('display', 'block');

                        }

                        //check validation type
                        if ($hdnValidationType == 1 || $hdnValidationType == 2) {

                            var tabStrip = $("#tabstrip").kendoTabStrip().data("kendoTabStrip");
                            if ($hdnValidationType == 1) {

                                //Show tabs G7
                                //$(tabStrip.items()[2]).show();
                                $(tabStrip.items()[2]).css('display', 'inline-block');


                                //Bind G7 charts
                                BindG7Charts();
                            }

                            //check the project type Sync1D or Validation
                            if ($hdnCurrentDlpSubOperation == ELicenseType.Sync1D || $hdnCurrentDlpSubOperation == ELicenseType.Sync1DMultiColor || $hdnCurrentDlpSubOperation == ELicenseType.Validation) {

                                //Hide default tabs TVI
                                //$(tabStrip.items()[4]).show();
                                $(tabStrip.items()[4]).css('display', 'inline-block');

                                //Bind TVI charts
                                BindTVICharts();

                                //show average measurement checkbox
                                if ($hdnCurrentDlpSubOperation == ELicenseType.Sync1D || $hdnCurrentDlpSubOperation == ELicenseType.Sync1DMultiColor) {
                                    $('#ContentPlaceHolder1_ucAnalyze_tdAverageOfMeasurements').css('display', 'block');
                                }
                            }
                        }

                        //set validation measurement status value for XMF user 
                        $('#hdnValidationMeasurementStatus').val(measurement.MeasurementStatus);

                        //create url to open report grading url for scoring report
                        reportGradingPath = 'PopupReportGrading.aspx?MeasurementID=' + measurement.MeasurementId + '&From_ΔE=' + $selectedDeltaE + '&DLPName=' + $hdnReportDLPName + '&Vali_Type=' + $hdnValidationType + '&IsAvgMeas=' + $isAverageOfMeasurements;

                        //create url to open report grading url for CMYK density report
                        reportCMYKDensityPath = 'PopupReportCMYKDensity.aspx?ProjectID=' + $hdnProjectID + '&OptimizeRunID=' + $hdnOptimizationRunID + '&MeasurementID=' + measurement.MeasurementId;
                    }
                }
            },
            error: function (xhr, errorType, exception) {
                alert(exception);
            }
        });


    } else {

        $("#divMeasurementSummary").css('display', 'block');

        apiParameter = {
            MeasurementId: measurementID,
            fromDeltaE: $selectedDeltaE,
            IsVerificationProject: $hdnVerification
        }

        var ajaxParameter = {
            type: "GET",
            url: SERVICE_BASE_URL + 'api/Analyze/GetMeasurementSummaryDetails',
            data: apiParameter,
            dataType: "json",
            headers: {
                "content-type": "application/json",
                'Accept-Language': $CurrentCulture,
                'Authorization': 'Basic ' + AUTH_TOKEN
            }
        };

        commonAjaxCall(ajaxParameter, function (response) {
            if (response.data != null) {
                if (response.data.length > 0) {
                    var measurements = response.data;
                    var measurement = measurements[0];
                    measurement.MeasurementDateFormatted = new Date(measurement.MeasurementDate);
                    var viewModel = kendo.observable(measurement);
                    kendo.bind($("#divMeasurementSummary"), viewModel);
                }
            }
        }, function (XMLHttpRequest, textStatus, errorThrown) {
            if (XMLHttpRequest.status == 401) {
                ShowDialog("You are not authorized user. Please login again.", "Message");
            } else {
                alert(errorThrown + ' ' + XMLHttpRequest.responseText);
            }
        });

    }


}

////**************** SET IMAGE ON LINK TO GENERATE REPORT  ****************////
function BindReportImage() {

    var imagePath = '';
    if ($hdnValidationType == 1) {  //G7 type
        imagePath = 'Images/Validation Report_G7_preview.jpg';  //For Sync1D and Sync4D Validation G7 Projects

        if ($hdnSync1DType == 'True') {
            imagePath = 'Images/Sync1D Report_preview_G7.jpg';   //For Sync1D G7 Projects
        }
        if ($hdnCurrentDlpSubOperation == ELicenseType.DLPPlus) {
            imagePath = 'Images/G7DLPPlus Report_preview.jpg';   //For DLPPlus G7 Projects
        }
    }
    else {          //ISO type OR ValidationType = 0 for Default View type

        imagePath = 'Images/Sync DLP Report_preview.jpg';       //For Sync 4D Projects    

        if ($hdnVerification == 'True') {
            imagePath = 'Images/Sync1D Report_preview_Vali.jpg';   //For Sync1D and Sync4D Validation Projects
        }
        if ($hdnSync1DType == 'True') {
            if ($hdnValidationType == 2) { //for sync 1D ISO projects
                imagePath = 'Images/Sync1D Report_preview_ISO.jpg';   //For Sync1D Projects
            }
            else {
                imagePath = 'Images/Sync1D Report_preview.jpg';   //For Sync1D Projects
            }

        }
    }
    $btnGeneratePDF.find('img').attr('src', imagePath);
}

////**************** START BIND THE CURRENT MEASUREMENT PATCH KENDO GRID  ****************////
function BindMeasurementPatchDetails() {

    var measurementID = parseInt($hdnCurrentMeasurement.val());

    $('#hdPatchMatricSum').val(''); //set the metric sum --All default value

    var patchGrid = $divMeasurementPatchDetails.data("kendoGrid");
    if (patchGrid) {
        $divMeasurementPatchDetails.data('kendoGrid').dataSource.read();
        $divMeasurementPatchDetails.data('kendoGrid').refresh();
    } else {
        $divMeasurementPatchDetails.kendoGrid({
            dataSource: {
                transport: {
                    read: function (options) {
                        kendo.ui.progress($('#divMeasurementPatchDetails .k-grid-content'), true);

                        var ajaxParameter = {
                            type: "GET",
                            url: SERVICE_BASE_URL + 'api/Chart/GetMeasurementPatchDetails',
                            data: {
                                MeasurementId: parseInt($hdnCurrentMeasurement.val()),
                                IsVerificationProject: $hdnVerification,
                                IsSync1DTypeProject: $hdnSync1DType,
                                metricSumID: $('#hdPatchMatricSum').val(),
                                IsDLPMultiColor: $hdnIsDlpMultiColor,
                                Is1DMultiColor: $hdnIsSync1DMultiColor,
                                CurrentRunName: $hdnCurrentRunName,
                                LicenseType: $hdnCurrentDlpSubOperation,
                                IsDestProfile: $hdnCurrentProjectLicenseType == 'DestinationProfile' ? 'True' : 'False'
                            },
                            dataType: "json",
                            headers: {
                                'Accept-Language': $CurrentCulture,
                                'Authorization': 'Basic ' + AUTH_TOKEN
                            }
                        };

                        commonAjaxCall(ajaxParameter, function (response) {
                            kendo.ui.progress($('#divMeasurementPatchDetails .k-grid-content'), false);
                            if (response.data != null) {
                                Resources = response.data.GlobalResources;
                                var patches = response.data.measurementPatchDetails;

                                options.success(patches);

                                //set the column header
                                $divMeasurementPatchDetails.find("th[data-field=HTMLColor] b").html(Resources.gridAnalyzeHeaderPatchResource);
                                $divMeasurementPatchDetails.find("th[data-field=SampleID] b").html(Resources.gridAnalyzeHeaderIDResource);
                                $divMeasurementPatchDetails.find("th[data-field=SampleName] b").html(Resources.GridTemplateColumnResource3);
                                $divMeasurementPatchDetails.find("th[data-field=CMYK_C] b").html(Resources.gridAnalyzeHeader_C_Resource);
                                $divMeasurementPatchDetails.find("th[data-field=CMYK_M] b").html(Resources.gridAnalyzeHeader_M_Resource);
                                $divMeasurementPatchDetails.find("th[data-field=CMYK_Y] b").html(Resources.gridAnalyzeHeader_Y_Resource);
                                $divMeasurementPatchDetails.find("th[data-field=CMYK_K] b").html(Resources.gridAnalyzeHeader_K_Resource);
                                $divMeasurementPatchDetails.find("th[data-field=ΔE] b").html(Resources.gridAnalyzeHeader_E_Resource);
                                $divMeasurementPatchDetails.find("th[data-field=ΔE94] b").html(Resources.gridAnalyzeHeaderE94Resource);
                                $divMeasurementPatchDetails.find("th[data-field=ΔE2000] b").html(Resources.gridAnalyzeHeaderE2000Resource);
                                $divMeasurementPatchDetails.find("th[data-field=TargetL] b").html(Resources.gridAnalyzeHeaderTargetLResource);
                                $divMeasurementPatchDetails.find("th[data-field=CIELAB_L] b").html(Resources.gridAnalyzeHeaderCIELAB_LResource);
                                $divMeasurementPatchDetails.find("th[data-field=Targeta] b").html(Resources.gridAnalyzeHeaderTargetAResource);
                                $divMeasurementPatchDetails.find("th[data-field=CIELAB_a] b").html(Resources.gridAnalyzeHeaderCIELAB_AResource);
                                $divMeasurementPatchDetails.find("th[data-field=Targetb] b").html(Resources.gridAnalyzeHeaderTargetBResource);
                                $divMeasurementPatchDetails.find("th[data-field=CIELAB_b] b").html(Resources.gridAnalyzeHeaderCIELAB_BResource);
                                $divMeasurementPatchDetails.find("th[data-field=Density_V] b").html(Resources.gridAnalyzeHeaderDensity_VResource);
                                $divMeasurementPatchDetails.find("th[data-field=Density_C] b").html(Resources.gridAnalyzeHeaderDensity_CResource);
                                $divMeasurementPatchDetails.find("th[data-field=Density_M] b").html(Resources.gridAnalyzeHeaderDensity_MResource);
                                $divMeasurementPatchDetails.find("th[data-field=Density_Y] b").html(Resources.gridAnalyzeHeaderDensity_YResource);
                                if (response.data.measurementPatchDetails[0].IsWeighted) {
                                    $divMeasurementPatchDetails.find("th[data-field=ΔL] b").html('wΔL');
                                    $divMeasurementPatchDetails.find("th[data-field=ΔCh] b").html('wΔCh');
                                }
                            }
                        }, function (XMLHttpRequest, textStatus, errorThrown) {
                            if (XMLHttpRequest.status == 401) {
                                ShowDialog("You are not authorized user. Please login again.", "Message");
                            } else {
                                //alert(errorThrown);
                                console.log(errorThrown);
                            }
                            kendo.ui.progress($('#divMeasurementPatchDetails .k-grid-content'), false);
                        });

                    },

                },
                schema: {
                    model: {
                        fields: {
                            SampleID: {
                                from: "SampleID",
                                type: "number"
                            },
                            SampleName: {
                                from: "SampleName"
                            },
                            CMYK_C: {
                                from: "CMYK_C",
                                type: "number"
                            },
                            CMYK_M: {
                                from: "CMYK_M",
                                type: "number"
                            },
                            CMYK_Y: {
                                from: "CMYK_Y",
                                type: "number"
                            },
                            CMYK_K: {
                                from: "CMYK_K",
                                type: "number"
                            },
                            HTMLColor: {
                                from: "HTMLColor"
                            },
                            TargetL: {
                                from: "TargetL",
                                type: "number"
                            },
                            Targeta: {
                                from: "Targeta",
                                type: "number"
                            },
                            Targetb: {
                                from: "Targetb",
                                type: "number"
                            },
                            CIELAB_L: {
                                from: "CIELAB_L",
                                type: "number"
                            },
                            CIELAB_a: {
                                from: "CIELAB_a",
                                type: "number"
                            },
                            CIELAB_b: {
                                from: "CIELAB_b",
                                type: "number"
                            },
                            ΔE: {
                                from: "ΔE",
                                type: "number"
                            },
                            ΔE94: {
                                from: "ΔE94",
                                type: "number"
                            },
                            ΔE2000: {
                                from: "ΔE2000",
                                type: "number"
                            },
                            Density_V: {
                                from: "Density_V",
                                type: "number"
                            },
                            Density_C: {
                                from: "Density_C",
                                type: "number"
                            },
                            Density_M: {
                                from: "Density_M",
                                type: "number"
                            },
                            Density_Y: {
                                from: "Density_Y",
                                type: "number"
                            },
                            PatchTypeID: {
                                from: "PatchTypeID",
                                type: "number"
                            },
                            ΔH: {
                                from: "ΔH",
                                type: "number"
                            },
                            ΔCh: {
                                from: "ΔCh",
                                type: "number"
                            },
                            ΔL: {
                                from: "ΔL",
                                type: "number"
                            }
                        }
                    }
                },
                pageSize: 10
            },
            sortable: true,
            scrollable: true,
            resizable: true,
            //height: 405,
            //filterable: true,
            filterable: {
                extra: false,
                //operators: {
                //    string: {
                //        eq: "Is equal to"
                //    }
                //}
            },
            pageable: {
                refresh: false,
                pageSizes: true
            },
            noRecords: {
                template: ''
            },
            toolbar: kendo.template($("#templatePatchDetails").html()),
            //toolbar: [{ 'name': 'exportCGATS', template: '<div style="float:left;"><button id="btnExportCGATS" class=\'k-button\'>Export CGATS</button></div>' }],
            columns: [
                {
                    field: "HTMLColor",
                    filterable: false,
                    title: "<b title='Patch'>Patch</b>",
                    //template: "<span id='sp' style='display:inline-block;background-color:#FFB700;height:14px;width:30px;'></span>",
                    template: "<span id='sp' style='display:inline-block;background-color:#=HTMLColor#;height:14px;width:30px;'></span>",
                    width: "50px",
                    locked: true,
                    sortable: false
                    //lockable: false,
                },
                {
                    field: "SampleID",
                    title: "<b title='ID'>ID</b>",
                    width: "50px",
                    filterable: false,
                    locked: true,
                },
                {
                    field: "SampleName",
                    title: "<b title='Name'>Name</b>",
                    filterable: false,
                    width: "75px",
                    locked: true,
                },
                {
                    field: "CMYK_C",
                    title: "<b title='C'>C</b>",
                    format: "{0:n2}",
                    width: "55px",
                    //locked: true,
                },
                {
                    field: "CMYK_M",
                    title: "<b title='M'>M</b>",
                    format: "{0:n2}",
                    width: "58px",
                    //locked: true,
                },
                {
                    field: "CMYK_Y",
                    title: "<b title='Y'>Y</b>",
                    format: "{0:n2}",
                    width: "55px",
                    //locked: true,
                },
                {
                    field: "CMYK_K",
                    title: "<b title='K'>K</b>",
                    format: "{0:n2}",
                    width: "55px",
                    // locked: true,
                },
                {
                    field: "ΔE",
                    title: "<b title='ΔE'>ΔE</b>",
                    width: "65px",
                    format: "{0:n2}",
                },
                {
                    field: "ΔE94",
                    title: "<b title='ΔE94'>ΔE94</b>",
                    width: "80px",
                    format: "{0:n2}",
                },
                {
                    field: "ΔE2000",
                    title: "<b title='ΔE2000'>ΔE2000</b>",
                    width: "90px",
                    format: "{0:n2}",
                },
                {
                    field: "TargetL",
                    title: "<b title='Target L*'>Target L*</b>",
                    width: "100px",
                    format: "{0:n2}",
                },
                {
                    field: "CIELAB_L",
                    title: "<b title='Actual L*'>Actual L*</b>",
                    width: "100px",
                    format: "{0:n2}",
                },
                {
                    field: "Targeta",
                    title: "<b title='Target a*'>Target a*</b>",
                    width: '100px',
                    format: "{0:n2}",
                },
                {
                    field: "CIELAB_a",
                    title: "<b title='Actual a*'>Actual a*</b>",
                    width: "100px",
                    format: "{0:n2}",
                },
                {
                    field: "Targetb",
                    title: "<b title='Target b*'>Target b*</b>",
                    width: '100px',
                    format: "{0:n2}",
                },
                {
                    field: "CIELAB_b",
                    title: "<b title='Actual b*'>Actual b*</b>",
                    width: "100px",
                    format: "{0:n2}",
                },
                {
                    field: "Density_V",
                    title: "<b title='Density_V'>Density_V</b>",
                    width: "110px",
                    format: "{0:n2}",
                },
                {
                    field: "Density_C",
                    title: "<b title='Density_C'>Density_C</b>",
                    width: '110px',
                    format: "{0:n2}",
                },
                {
                    field: "Density_M",
                    title: "<b title='Density_M'>Density_M</b>",
                    width: "110px",
                    format: "{0:n2}",
                },
                {
                    field: "Density_Y",
                    title: "<b title='Density_Y'>Density_Y</b>",
                    width: '110px',
                    format: "{0:n2}",
                },
                {
                    field: "ΔH",
                    title: "<b title='ΔH'>ΔH</b>",
                    width: '75px',
                    format: "{0:n2}",
                    hidden: true,
                },
                {
                    field: "ΔCh",
                    title: "<b title='ΔCh'>ΔCh</b>",
                    width: '75px',
                    format: "{0:n2}",
                    hidden: true,
                },
                {
                    field: "ΔL",
                    title: "<b title='ΔL'>ΔL</b>",
                    width: '75px',
                    format: "{0:n2}",
                    hidden: true,
                },
            ],
            dataBound: function (e) {
                var selectedMatricSum = parseInt($divMeasurementPatchDetails.find("#ddlMatricSum").val())
                var grid = $divMeasurementPatchDetails.data("kendoGrid");

                if ($hdnCurrentDlpSubOperation == ELicenseType.DestinationProfile) {
                    grid.hideColumn(7);
                    grid.hideColumn(8);
                    grid.hideColumn(9);
                    grid.hideColumn(10);
                    grid.hideColumn(12);
                    grid.hideColumn(14);
                }

                if (!isNaN(selectedMatricSum)) {
                    switch (selectedMatricSum) {
                        case 5:
                        case 6:
                        case 7:
                        case 25:
                        case 26:
                            grid.showColumn(20);
                            grid.hideColumn(21);
                            grid.hideColumn(22);
                            break;
                        case 8:
                        case 9:
                        case 36:
                        case 37:
                        case 48:
                        case 56:
                            grid.hideColumn(20);
                            grid.showColumn(21);
                            grid.hideColumn(22);
                            break;
                        case 10:
                        case 11:
                        case 12:
                        case 13:
                        case 38:
                        case 39:
                        case 40:
                        case 41:
                            grid.hideColumn(20);
                            grid.hideColumn(21);
                            grid.showColumn(22);
                            break;
                        default:
                            grid.hideColumn(20);
                            grid.hideColumn(21);
                            grid.hideColumn(22);
                    }
                } else {
                    grid.hideColumn(20);
                    grid.hideColumn(21);
                    grid.hideColumn(22);
                }
                //show measurement export button visible only for sys admin user
                if (($hdnCurrentDlpSubOperation == ELicenseType.Sync1D || $hdnCurrentDlpSubOperation == ELicenseType.Sync1DMultiColor || $hdnCurrentDlpSubOperation == ELicenseType.DLP || $hdnCurrentDlpSubOperation == ELicenseType.Sync4DCertification || $hdnCurrentDlpSubOperation == ELicenseType.Validation || $hdnCurrentDlpSubOperation == ELicenseType.DLPMultiColor || $hdnCurrentDlpSubOperation == ELicenseType.DLPPlus || $hdnCurrentDlpSubOperation == ELicenseType.DestinationProfile) && $hdnCurrentUserRole.val() == ERole.SystemAdmin) {
                    $divMeasurementPatchDetails.find('.k-grid-toolbar #btnExportMeasurement').show();
                }
                //Show Dot Gain report export button visible only if it is Sync1D LicenseType and CloudFlowcheck return 1 , make hide "JSON" export button
                if ($hdnCurrentDlpSubOperation == ELicenseType.Sync1D && $hdnCloudFlowCheck == "1") {
                    $divMeasurementPatchDetails.find('.k-grid-toolbar #btnExportDotGain').show();
                    $divMeasurementPatchDetails.find('.k-grid-toolbar #btnExportMeasurement').hide();
                }
            }


        });
    }




    if ($hdnCurrentDlpSubOperation == ELicenseType.Sync1D || $hdnCurrentDlpSubOperation == ELicenseType.Sync1DMultiColor || $hdnCurrentDlpSubOperation == ELicenseType.Validation || $hdnCurrentDlpSubOperation == ELicenseType.DLPPlus) {

        var measurementID = parseInt($hdnCurrentMeasurement.val());
        var params = {
            MeasurementId: measurementID,
            IsVerificationProject: $hdnVerification,
            IsSync1DTypeProject: $hdnSync1DType,
            fromDeltaE: $selectedDeltaE,
            IsAverageOfMeasurements: $isAverageOfMeasurements,
            IsSyncDlpPlusTypeProject: $hdnSyncDlpPlusType

        };

        $divMeasurementPatchDetails.find("#ddlMatricSum").kendoDropDownList({
            dataTextField: "Metric",
            dataValueField: "MetricSumID",
            autoBind: true,
            keepOpen: false,
            optionLabel: "All",
            dataSource: {
                transport: {
                    read: {
                        type: "GET",
                        url: SERVICE_BASE_URL + 'api/Chart/GetValidationMetrics',
                        dataType: "json",
                        data: params,
                    }
                }
            },
            dataBound: function (e) {
                e.sender.select(0); //set default value for matric sum dropdown on measurement change
            },
            change: function (e) {
                var matrixSum = '-1';
                if (this.value() != '')
                    matrixSum = this.value();

                $('#hdPatchMatricSum').val(this.value());
                $divMeasurementPatchDetails.data('kendoGrid').dataSource.read();
                $divMeasurementPatchDetails.data('kendoGrid').refresh();
            }
        });

    } else {
        $divMeasurementPatchDetails.find("#divMetricSum").css('display', 'none');
    }

    $divMeasurementPatchDetails.find('#btnExportMeasurement').unbind("click").on('click', function (e) {
        e.preventDefault();

        var measurementID = parseInt($hdnCurrentMeasurement.val());

        var ajaxParameter = {
            type: "POST",
            url: SERVICE_BASE_URL + 'api/Analyze/GetMeasurementFileExport?MeasurementId=' + measurementID + '&LicenseType=' + parseInt($hdnCurrentDlpSubOperation),
            data: {},
            dataType: "json",
            async: false,
            headers: {
                'Authorization': 'Basic ' + AUTH_TOKEN
            }
        };

        commonAjaxCall(ajaxParameter, function (response) {
            if (response.data != null) {
                var fileName = '~' + response.data;
                window.location.href = SERVICE_BASE_URL + '/api/Analyze/DownloadFile?filePath=' + fileName;
            }
        }, function (XMLHttpRequest, textStatus, errorThrown) {
            if (XMLHttpRequest.status == 401) {
                ShowDialog("You are not authorized user. Please login again.", "Message");
            } else {
                alert(errorThrown);
            }
        });

    });


    $divMeasurementPatchDetails.find('#btnExportCGATS').unbind("click").on('click', function (e) {
        e.preventDefault();

        var selectedMatricSum = '';
        if ($divMeasurementPatchDetails.find("#ddlMatricSum").data("kendoDropDownList")) {
            selectedMatricSum = $divMeasurementPatchDetails.find("#ddlMatricSum").data("kendoDropDownList").value();
        }
        var measurementID = parseInt($hdnCurrentMeasurement.val());

        var grid = $('#divMeasurementPatchDetails').data('kendoGrid')
        var optimizationRun = $(this).attr('data-id');
        var sortObj = null;
        var filterObj = null;
        //set the sorting parameters
        if (grid.dataSource._sort) {
            sortObj = grid.dataSource._sort[0];
        }
        //set the filtering parameters
        if (grid.dataSource._filter) {
            filterObj = {
                filters: grid.dataSource._filter.filters,
                logic: 'AND'
            };
            //set the type of filter fields
            $.each(filterObj.filters, function (index, obj) {
                obj.dtype = grid.dataSource.options.schema.model.fields[obj.field].type;
            });
        }

        //create object
        var filterObj = {
            MeasurementId: measurementID,
            IsVerificationProject: $hdnVerification,
            IsSync1DTypeProject: $hdnSync1DType,
            metricSumID: selectedMatricSum,
            sort: sortObj,
            filter: filterObj
        };

        var ajaxParameter = {
            type: "POST",
            url: SERVICE_BASE_URL + 'api/Analyze/GetCGATSPatchReport',
            data: filterObj,
            dataType: "json",
            async: false,
            headers: {
                'Authorization': 'Basic ' + AUTH_TOKEN
            }
        };

        commonAjaxCall(ajaxParameter, function (response) {
            if (response.data != null) {
                var fileName = '~' + response.data;
                window.location.href = SERVICE_BASE_URL + '/api/Analyze/DownloadFile?filePath=' + fileName;
            }
        }, function (XMLHttpRequest, textStatus, errorThrown) {
            if (XMLHttpRequest.status == 401) {
                ShowDialog("You are not authorized user. Please login again.", "Message");
            } else {
                alert(errorThrown);
            }
        });

    });

    $divMeasurementPatchDetails.find('#btnExportCxF').unbind("click").on('click', function (e) {
        e.preventDefault();
        var measurementID = parseInt($hdnCurrentMeasurement.val());
        var selectedMatricSum = '';
        if ($divMeasurementPatchDetails.find("#ddlMatricSum").data("kendoDropDownList")) {
            selectedMatricSum = $divMeasurementPatchDetails.find("#ddlMatricSum").data("kendoDropDownList").value();
        }

        var grid = $('#divMeasurementPatchDetails').data('kendoGrid')
        var optimizationRun = $(this).attr('data-id');
        var sortObj = null;
        var filterObj = null;
        //set the sorting parameters
        if (grid.dataSource._sort) {
            sortObj = grid.dataSource._sort[0];
        }
        //set the filtering parameters
        if (grid.dataSource._filter) {
            filterObj = {
                filters: grid.dataSource._filter.filters,
                logic: 'AND'
            };
            //set the type of filter fields
            $.each(filterObj.filters, function (index, obj) {
                obj.dtype = grid.dataSource.options.schema.model.fields[obj.field].type;
            });
        }

        //create object
        var filterObj = {
            MeasurementId: measurementID,
            IsVerificationProject: $hdnVerification,
            IsSync1DTypeProject: $hdnSync1DType,
            metricSumID: selectedMatricSum,
            sort: sortObj,
            filter: filterObj
        };

        var ajaxParameter = {
            type: "POST",
            url: SERVICE_BASE_URL + 'api/Analyze/GetCFXPatchReport',
            data: filterObj,
            dataType: "json",
            async: false,
            headers: {
                'Authorization': 'Basic ' + AUTH_TOKEN
            }
        };

        commonAjaxCall(ajaxParameter, function (response) {
            if (response.data != null) {
                var fileName = '~' + response.data;
                window.location.href = SERVICE_BASE_URL + '/api/Analyze/DownloadFile?filePath=' + fileName;
            }
        }, function (XMLHttpRequest, textStatus, errorThrown) {
            if (XMLHttpRequest.status == 401) {
                ShowDialog("You are not authorized user. Please login again.", "Message");
            } else {
                alert(errorThrown);
            }
        });

    });

    $divMeasurementPatchDetails.find('#btnExportDotGain').unbind("click").on('click', function (e) {
        e.preventDefault();

        var measurementID = parseInt($hdnCurrentMeasurement.val());

        var ajaxParameter = {
            type: "POST",
            url: SERVICE_BASE_URL + 'api/Analyze/GetDotGainPatchReport?MeasurementId=' + measurementID,
            data: {},
            dataType: "json",
            async: false,
            headers: {
                'Authorization': 'Basic ' + AUTH_TOKEN
            }
        };

        commonAjaxCall(ajaxParameter, function (response) {
            if (response.data != null) {
                var fileName = '~' + response.data;
                window.location.href = SERVICE_BASE_URL + '/api/Analyze/DownloadFile?filePath=' + fileName;
            }
        }, function (XMLHttpRequest, textStatus, errorThrown) {
            if (XMLHttpRequest.status == 401) {
                ShowDialog("You are not authorized user. Please login again.", "Message");
            } else {
                alert(errorThrown);
            }
        });

    });

    $divMeasurementPatchDetails.find('#lnkLinkFilter').on('click', function (e) {
        $("form.k-filter-menu button[type='reset']").trigger("click");
    });

    $('#divMeasurementPatchDetails .k-grid-header th.k-header').mouseenter(function () {
        $(this).find('.k-grid-filter').show();
    }).mouseleave(function () {
        $(this).find('.k-grid-filter').hide();
    });
}

////**************** START BIND THE CURRENT MEASUREMENT PATCH KENDO GRID For MULTICOLOR  ****************////
function BindMeasurementPatchDetailsForMultiColor() {
    var measurementID = parseInt($hdnCurrentMeasurement.val());
    var TotalNoColor = 0;
    var IsSupportMulticolor = false;
    $('#hdPatchMatricSum').val(''); //set the metric sum --All default value

    var patchGrid = $divMeasurementPatchDetails.data("kendoGrid");
    if (patchGrid) {
        $divMeasurementPatchDetails.data('kendoGrid').dataSource.read();
        $divMeasurementPatchDetails.data('kendoGrid').refresh();
    } else {
        $divMeasurementPatchDetails.kendoGrid({
            dataSource: {
                transport: {
                    read: function (options) {
                        kendo.ui.progress($('#divMeasurementPatchDetails .k-grid-content'), true);


                        var ajaxParameter = {
                            type: "GET",
                            url: SERVICE_BASE_URL + 'api/Chart/GetMeasurementPatchDetails',
                            data: {
                                MeasurementId: parseInt($hdnCurrentMeasurement.val()),
                                IsVerificationProject: $hdnVerification,
                                IsSync1DTypeProject: $hdnSync1DType,
                                metricSumID: $('#hdPatchMatricSum').val(),
                                IsDLPMultiColor: $hdnIsDlpMultiColor,
                                Is1DMultiColor: $hdnIsSync1DMultiColor,
                                CurrentRunName: $hdnCurrentRunName,
                                LicenseType: $hdnCurrentDlpSubOperation,
                                IsDestProfile: $hdnCurrentProjectLicenseType == 'DestinationProfile' ? 'True' : 'False'
                            },
                            dataType: "json",
                            headers: {
                                'Accept-Language': $CurrentCulture,
                                'Authorization': 'Basic ' + AUTH_TOKEN
                            }
                        };

                        commonAjaxCall(ajaxParameter, function (response) {

                            kendo.ui.progress($('#divMeasurementPatchDetails .k-grid-content'), false);
                            if (response.data != null) {
                                Resources = response.data.GlobalResources;
                                var patches = response.data.measurementPatchDetails;
                                TotalNoColor = response.data.TotalNoColors;
                                IsSupportMulticolor = response.data.IsSupportMulticolor;
                                options.success(patches);

                                //set the column header
                                $divMeasurementPatchDetails.find("th[data-field=HTMLColor] b").html(Resources.gridAnalyzeHeaderPatchResource);
                                $divMeasurementPatchDetails.find("th[data-field=SampleID] b").html(Resources.gridAnalyzeHeaderIDResource);
                                $divMeasurementPatchDetails.find("th[data-field=SampleName] b").html(Resources.GridTemplateColumnResource3);
                                $divMeasurementPatchDetails.find("th[data-field=CMYK_C] b").html(Resources.gridAnalyzeHeader_C_Resource);
                                $divMeasurementPatchDetails.find("th[data-field=CMYK_M] b").html(Resources.gridAnalyzeHeader_M_Resource);
                                $divMeasurementPatchDetails.find("th[data-field=CMYK_Y] b").html(Resources.gridAnalyzeHeader_Y_Resource);
                                $divMeasurementPatchDetails.find("th[data-field=CMYK_K] b").html(Resources.gridAnalyzeHeader_K_Resource);
                                $divMeasurementPatchDetails.find("th[data-field=ΔE] b").html(Resources.gridAnalyzeHeader_E_Resource);
                                $divMeasurementPatchDetails.find("th[data-field=ΔE94] b").html(Resources.gridAnalyzeHeaderE94Resource);
                                $divMeasurementPatchDetails.find("th[data-field=ΔE2000] b").html(Resources.gridAnalyzeHeaderE2000Resource);
                                $divMeasurementPatchDetails.find("th[data-field=TargetL] b").html(Resources.gridAnalyzeHeaderTargetLResource);
                                $divMeasurementPatchDetails.find("th[data-field=CIELAB_L] b").html(Resources.gridAnalyzeHeaderCIELAB_LResource);
                                $divMeasurementPatchDetails.find("th[data-field=Targeta] b").html(Resources.gridAnalyzeHeaderTargetAResource);
                                $divMeasurementPatchDetails.find("th[data-field=CIELAB_a] b").html(Resources.gridAnalyzeHeaderCIELAB_AResource);
                                $divMeasurementPatchDetails.find("th[data-field=Targetb] b").html(Resources.gridAnalyzeHeaderTargetBResource);
                                $divMeasurementPatchDetails.find("th[data-field=CIELAB_b] b").html(Resources.gridAnalyzeHeaderCIELAB_BResource);
                                $divMeasurementPatchDetails.find("th[data-field=Density_V] b").html(Resources.gridAnalyzeHeaderDensity_VResource);
                                $divMeasurementPatchDetails.find("th[data-field=Density_C] b").html(Resources.gridAnalyzeHeaderDensity_CResource);
                                $divMeasurementPatchDetails.find("th[data-field=Density_M] b").html(Resources.gridAnalyzeHeaderDensity_MResource);
                                $divMeasurementPatchDetails.find("th[data-field=Density_Y] b").html(Resources.gridAnalyzeHeaderDensity_YResource);
                                if (response.data.measurementPatchDetails[0].IsWeighted) {
                                    $divMeasurementPatchDetails.find("th[data-field=ΔL] b").html('wΔL');
                                    $divMeasurementPatchDetails.find("th[data-field=ΔCh] b").html('wΔCh');
                                }

                                if (TotalNoColor == 5) {
                                    $divMeasurementPatchDetails.find("th[data-field=Channel5] b").html(Resources.Channel_5);
                                    $($divMeasurementPatchDetails.find("th[data-field=Channel5] b")[0]).prop("title", Resources.Channel_5); //Header ToolTip Channel5
                                }
                                else if (TotalNoColor == 6) {
                                    $divMeasurementPatchDetails.find("th[data-field=Channel5] b").html(Resources.Channel_5);
                                    $($divMeasurementPatchDetails.find("th[data-field=Channel5] b")[0]).prop("title", Resources.Channel_5); //Header ToolTip Channel5
                                    $divMeasurementPatchDetails.find("th[data-field=Channel6] b").html(Resources.Channel_6);
                                    $($divMeasurementPatchDetails.find("th[data-field=Channel6] b")[0]).prop("title", Resources.Channel_6); //Header ToolTip Channel6
                                }
                                else if (TotalNoColor == 7) {
                                    $divmeasurementpatchdetails.find("th[data-field=channel5] b").html(resources.Channel_5);
                                    $divmeasurementpatchdetails.find("th[data-field=channel6] b").html(resources.Channel_6);
                                    $($divMeasurementPatchDetails.find("th[data-field=Channel5] b")[0]).prop("title", Resources.Channel_5); //Header ToolTip Channel5
                                    $($divMeasurementPatchDetails.find("th[data-field=Channel6] b")[0]).prop("title", Resources.Channel_6); //Header ToolTip Channel6
                                    $divmeasurementpatchdetails.find("th[data-field=channel7] b").html(resources.Channel_7);
                                    $($divMeasurementPatchDetails.find("th[data-field=channel7] b")[0]).prop("title", Resources.Channel_7); //Header ToolTip Channel7
                                }
                                else if (TotalNoColor == 8) {
                                    $divmeasurementpatchdetails.find("th[data-field=channel5] b").html(resources.Channel_5);
                                    $divmeasurementpatchdetails.find("th[data-field=channel6] b").html(resources.Channel_6);
                                    $divmeasurementpatchdetails.find("th[data-field=channel7] b").html(resources.Channel_7);
                                    $($divMeasurementPatchDetails.find("th[data-field=Channel5] b")[0]).prop("title", Resources.Channel_5);  //Header ToolTip Channel5
                                    $($divMeasurementPatchDetails.find("th[data-field=Channel6] b")[0]).prop("title", Resources.Channel_6);  //Header ToolTip Channel6
                                    $($divMeasurementPatchDetails.find("th[data-field=channel7] b")[0]).prop("title", Resources.Channel_7);  //Header ToolTip Channel7
                                    $divmeasurementpatchdetails.find("th[data-field=channel8] b").html(resources.Channel_8);
                                    $($divMeasurementPatchDetails.find("th[data-field=channel8] b")[0]).prop("title", Resources.Channel_8); //Header ToolTip Channel8
                                }
                                else if (TotalNoColor == 9) {
                                    $divmeasurementpatchdetails.find("th[data-field=channel5] b").html(resources.Channel_5);
                                    $divmeasurementpatchdetails.find("th[data-field=channel6] b").html(resources.Channel_6);
                                    $divmeasurementpatchdetails.find("th[data-field=channel7] b").html(resources.Channel_7);
                                    $divmeasurementpatchdetails.find("th[data-field=channel8] b").html(resources.Channel_8);
                                    $($divMeasurementPatchDetails.find("th[data-field=Channel5] b")[0]).prop("title", Resources.Channel_5); //Header ToolTip Channel5
                                    $($divMeasurementPatchDetails.find("th[data-field=Channel6] b")[0]).prop("title", Resources.Channel_6); //Header ToolTip Channel6
                                    $($divMeasurementPatchDetails.find("th[data-field=channel7] b")[0]).prop("title", Resources.Channel_7); //Header ToolTip Channel7
                                    $($divMeasurementPatchDetails.find("th[data-field=channel8] b")[0]).prop("title", Resources.Channel_8); //Header ToolTip Channel8
                                    $divmeasurementpatchdetails.find("th[data-field=channel9] b").html(resources.Channel_9);
                                    $($divMeasurementPatchDetails.find("th[data-field=channel9] b")[0]).prop("title", Resources.Channel_9); //Header ToolTip Channel9
                                }





                            }
                        }, function (XMLHttpRequest, textStatus, errorThrown) {
                            if (XMLHttpRequest.status == 401) {
                                ShowDialog("You are not authorized user. Please login again.", "Message");
                            } else {
                                //alert(errorThrown);
                                console.log(errorThrown);
                            }
                            kendo.ui.progress($('#divMeasurementPatchDetails .k-grid-content'), false);
                        });
                    },

                },
                schema: {
                    model: {
                        fields: {
                            SampleID: {
                                from: "SampleID",
                                type: "number"
                            },
                            SampleName: {
                                from: "SampleName"
                            },
                            CMYK_C: {
                                from: "CMYK_C",
                                type: "number"
                            },
                            CMYK_M: {
                                from: "CMYK_M",
                                type: "number"
                            },
                            CMYK_Y: {
                                from: "CMYK_Y",
                                type: "number"
                            },
                            CMYK_K: {
                                from: "CMYK_K",
                                type: "number"
                            },
                            Channel5: {
                                from: "Channel5",
                                type: "number"
                            },
                            Channel6: {
                                from: "Channel6",
                                type: "number"
                            },
                            Channel7: {
                                from: "Channel7",
                                type: "number"
                            },
                            Channel8: {
                                from: "Channel8",
                                type: "number"
                            },
                            Channel9: {
                                from: "Channel9",
                                type: "number"
                            },
                            HTMLColor: {
                                from: "HTMLColor"
                            },
                            TargetL: {
                                from: "TargetL",
                                type: "number"
                            },
                            Targeta: {
                                from: "Targeta",
                                type: "number"
                            },
                            Targetb: {
                                from: "Targetb",
                                type: "number"
                            },
                            CIELAB_L: {
                                from: "CIELAB_L",
                                type: "number"
                            },
                            CIELAB_a: {
                                from: "CIELAB_a",
                                type: "number"
                            },
                            CIELAB_b: {
                                from: "CIELAB_b",
                                type: "number"
                            },
                            ΔE: {
                                from: "ΔE",
                                type: "number"
                            },
                            ΔE94: {
                                from: "ΔE94",
                                type: "number"
                            },
                            ΔE2000: {
                                from: "ΔE2000",
                                type: "number"
                            },
                            Density_V: {
                                from: "Density_V",
                                type: "number"
                            },
                            Density_C: {
                                from: "Density_C",
                                type: "number"
                            },
                            Density_M: {
                                from: "Density_M",
                                type: "number"
                            },
                            Density_Y: {
                                from: "Density_Y",
                                type: "number"
                            },
                            PatchTypeID: {
                                from: "PatchTypeID",
                                type: "number"
                            },
                            ΔH: {
                                from: "ΔH",
                                type: "number"
                            },
                            ΔCh: {
                                from: "ΔCh",
                                type: "number"
                            },
                            ΔL: {
                                from: "ΔL",
                                type: "number"
                            }

                        }
                    }
                },
                pageSize: 10
            },
            sortable: true,
            scrollable: true,
            resizable: true,
            //height: 405,
            //filterable: true,
            filterable: {
                extra: false,
                //operators: {
                //    string: {
                //        eq: "Is equal to"
                //    }
                //}
            },
            pageable: {
                refresh: false,
                pageSizes: true
            },
            noRecords: {
                template: ''
            },
            toolbar: kendo.template($("#templatePatchDetails").html()),
            //toolbar: [{ 'name': 'exportCGATS', template: '<div style="float:left;"><button id="btnExportCGATS" class=\'k-button\'>Export CGATS</button></div>' }],
            columns: [
                {
                    field: "HTMLColor",
                    filterable: false,
                    title: "<b title='Patch'>Patch</b>",
                    //template: "<span id='sp' style='display:inline-block;background-color:#FFB700;height:14px;width:30px;'></span>",
                    template: "<span id='sp' style='display:inline-block;background-color:#=HTMLColor#;height:14px;width:30px;'></span>",
                    width: "50px",
                    locked: true,
                    sortable: false
                    //lockable: false,
                },
                {
                    field: "SampleID",
                    title: "<b title='ID'>ID</b>",
                    width: "50px",
                    filterable: false,
                    locked: true,
                },
                {
                    field: "SampleName",
                    title: "<b title='Name'>Name</b>",
                    filterable: false,
                    width: "75px",
                    locked: true,
                },
                {
                    field: "CMYK_C",
                    title: "<b title='C'>C</b>",
                    format: "{0:n2}",
                    width: "55px",
                    //locked: true,
                },
                {
                    field: "CMYK_M",
                    title: "<b title='M'>M</b>",
                    format: "{0:n2}",
                    width: "58px",
                    // locked: true,
                },
                {
                    field: "CMYK_Y",
                    title: "<b title='Y'>Y</b>",
                    format: "{0:n2}",
                    width: "55px",
                    // locked: true,
                },
                {
                    field: "CMYK_K",
                    title: "<b title='K'>K</b>",
                    format: "{0:n2}",
                    width: "55px",
                    //locked: true,
                },
                {
                    field: "Channel5",
                    title: "<b title='Channel5'>Channel5</b>",
                    width: '75px',
                    format: "{0:n2}",

                },
                {
                    field: "Channel6",
                    title: "<b title='Channel6'>Channel6</b>",
                    width: '75px',
                    format: "{0:n2}",

                },

                {
                    field: "Channel7",
                    title: "<b title='Channel7'>Channel7</b>",
                    width: '75px',
                    format: "{0:n2}",

                },
                {
                    field: "Channel8",
                    title: "<b title='Channel8'>Channel8</b>",
                    width: '75px',
                    format: "{0:n2}",

                },
                {
                    field: "Channel9",
                    title: "<b title='Channel9'>Channel9</b>",
                    width: '75px',
                    format: "{0:n2}",

                },
                {
                    field: "ΔE",
                    title: "<b title='ΔE'>ΔE</b>",
                    width: "65px",
                    format: "{0:n2}",
                },
                {
                    field: "ΔE94",
                    title: "<b title='ΔE94'>ΔE94</b>",
                    width: "80px",
                    format: "{0:n2}",
                },
                {
                    field: "ΔE2000",
                    title: "<b title='ΔE2000'>ΔE2000</b>",
                    width: "90px",
                    format: "{0:n2}",
                },
                {
                    field: "TargetL",
                    title: "<b title='Target L*'>Target L*</b>",
                    width: "100px",
                    format: "{0:n2}",
                },
                {
                    field: "CIELAB_L",
                    title: "<b title='Actual L*'>Actual L*</b>",
                    width: "100px",
                    format: "{0:n2}",
                },
                {
                    field: "Targeta",
                    title: "<b title='Target a*'>Target a*</b>",
                    width: '100px',
                    format: "{0:n2}",
                },
                {
                    field: "CIELAB_a",
                    title: "<b title='Actual a*'>Actual a*</b>",
                    width: "100px",
                    format: "{0:n2}",
                },
                {
                    field: "Targetb",
                    title: "<b title='Target b*'>Target b*</b>",
                    width: '100px',
                    format: "{0:n2}",
                },
                {
                    field: "CIELAB_b",
                    title: "<b title='Actual b*'>Actual b*</b>",
                    width: "100px",
                    format: "{0:n2}",
                },
                {
                    field: "Density_V",
                    title: "<b title='Density_V'>Density_V</b>",
                    width: "110px",
                    format: "{0:n2}",
                },
                {
                    field: "Density_C",
                    title: "<b title='Density_C'>Density_C</b>",
                    width: '110px',
                    format: "{0:n2}",
                },
                {
                    field: "Density_M",
                    title: "<b title='Density_M'>Density_M</b>",
                    width: "110px",
                    format: "{0:n2}",
                },
                {
                    field: "Density_Y",
                    title: "<b title='Density_Y'>Density_Y</b>",
                    width: '110px',
                    format: "{0:n2}",
                },
                {
                    field: "ΔH",
                    title: "<b title='ΔH'>ΔH</b>",
                    width: '75px',
                    format: "{0:n2}",
                    hidden: true,
                },
                {
                    field: "ΔCh",
                    title: "<b title='ΔCh'>ΔCh</b>",
                    width: '75px',
                    format: "{0:n2}",
                    hidden: true,
                },
                {
                    field: "ΔL",
                    title: "<b title='ΔL'>ΔL</b>",
                    width: '75px',
                    format: "{0:n2}",
                    hidden: true,
                }
            ],
            dataBound: function (e) {
                var selectedMatricSum = parseInt($divMeasurementPatchDetails.find("#ddlMatricSum").val())
                var grid = $divMeasurementPatchDetails.data("kendoGrid");


                if (!isNaN(selectedMatricSum)) {
                    switch (selectedMatricSum) {
                        case 5:
                        case 6:
                        case 7:
                        case 25:
                        case 26:
                            grid.showColumn(25);//grid.showColumn(20);
                            grid.hideColumn(26);//grid.hideColumn(21);
                            grid.hideColumn(27);//grid.hideColumn(22);
                            break;
                        case 8:
                        case 9:
                        case 36:
                        case 37:
                        case 48:
                        case 56:
                            grid.hideColumn(25);//grid.hideColumn(20);
                            grid.showColumn(26);//grid.showColumn(21);
                            grid.hideColumn(27);//grid.hideColumn(22);
                            break;
                        case 10:
                        case 11:
                        case 12:
                        case 13:
                        case 38:
                        case 39:
                        case 40:
                        case 41:
                            grid.hideColumn(25);//grid.hideColumn(20);
                            grid.hideColumn(26);//grid.hideColumn(21);
                            grid.showColumn(27);//grid.showColumn(22);
                            break;
                        default:
                            grid.hideColumn(25);//grid.hideColumn(20);
                            grid.hideColumn(26);//grid.hideColumn(21);
                            grid.hideColumn(27);//grid.hideColumn(22);
                    }
                } else {
                    grid.hideColumn(25);//grid.hideColumn(20);
                    grid.hideColumn(26);//grid.hideColumn(21);
                    grid.hideColumn(27);//grid.hideColumn(22);
                }
                //show measurement export button visible only for sys admin user
                if (($hdnCurrentDlpSubOperation == ELicenseType.Sync1D || $hdnCurrentDlpSubOperation == ELicenseType.Sync1DMultiColor || $hdnCurrentDlpSubOperation == ELicenseType.DLP || $hdnCurrentDlpSubOperation == ELicenseType.Sync4DCertification || $hdnCurrentDlpSubOperation == ELicenseType.Validation || $hdnCurrentDlpSubOperation == ELicenseType.DLPMultiColor) && $hdnCurrentUserRole.val() == ERole.SystemAdmin) {
                    $divMeasurementPatchDetails.find('.k-grid-toolbar #btnExportMeasurement').show();
                }

                if (TotalNoColor == 5) {
                    grid.hideColumn(8);//grid.hideColumn(24);// Channel_6
                    grid.hideColumn(9);//grid.hideColumn(25);// Channel_7
                    grid.hideColumn(10);//grid.hideColumn(26);// Channel_8
                    grid.hideColumn(11);//grid.hideColumn(27);// Channel_9
                }
                else if (TotalNoColor == 6) {
                    grid.hideColumn(9);//grid.hideColumn(25); ///Channel_7
                    grid.hideColumn(10);//grid.hideColumn(26);///Channel_8
                    grid.hideColumn(11);//grid.hideColumn(27);///Channel_9

                }
                else if (TotalNoColor == 7) {
                    grid.hideColumn(10);//grid.hideColumn(26);// Channel_8
                    grid.hideColumn(11);//grid.hideColumn(27);// Channel_9
                }
                else if (TotalNoColor == 8) {
                    grid.hideColumn(11);//grid.hideColumn(27);// Channel_9
                }

            }

        });
    }




    if ($hdnCurrentDlpSubOperation == ELicenseType.Sync1D || $hdnCurrentDlpSubOperation == ELicenseType.Sync1DMultiColor || $hdnCurrentDlpSubOperation == ELicenseType.Validation || $hdnCurrentDlpSubOperation == ELicenseType.DLPPlus) {

        var measurementID = parseInt($hdnCurrentMeasurement.val());
        var params = {
            MeasurementId: measurementID,
            IsVerificationProject: $hdnVerification,
            IsSync1DTypeProject: $hdnSync1DType,
            fromDeltaE: $selectedDeltaE,
            IsAverageOfMeasurements: $isAverageOfMeasurements,
            IsSyncDlpPlusTypeProject: $hdnSyncDlpPlusType
        };

        $divMeasurementPatchDetails.find("#ddlMatricSum").kendoDropDownList({
            dataTextField: "Metric",
            dataValueField: "MetricSumID",
            autoBind: true,
            keepOpen: false,
            optionLabel: "All",
            dataSource: {
                transport: {
                    read: {
                        type: "GET",
                        url: SERVICE_BASE_URL + 'api/Chart/GetValidationMetrics',
                        dataType: "json",
                        data: params,
                    }
                }
            },
            dataBound: function (e) {
                e.sender.select(0); //set default value for matric sum dropdown on measurement change
            },
            change: function (e) {
                var matrixSum = '-1';
                if (this.value() != '')
                    matrixSum = this.value();

                $('#hdPatchMatricSum').val(this.value());
                $divMeasurementPatchDetails.data('kendoGrid').dataSource.read();
                $divMeasurementPatchDetails.data('kendoGrid').refresh();
            }
        });

    } else {
        $divMeasurementPatchDetails.find("#divMetricSum").css('display', 'none');
    }

    $divMeasurementPatchDetails.find('#btnExportMeasurement').unbind("click").on('click', function (e) {
        e.preventDefault();

        var measurementID = parseInt($hdnCurrentMeasurement.val());

        var ajaxParameter = {
            type: "POST",
            url: SERVICE_BASE_URL + 'api/Analyze/GetMeasurementFileExport?MeasurementId=' + measurementID + '&LicenseType=' + parseInt($hdnCurrentDlpSubOperation),
            data: {},
            dataType: "json",
            async: false,
            headers: {
                'Authorization': 'Basic ' + AUTH_TOKEN
            }
        };

        commonAjaxCall(ajaxParameter, function (response) {
            if (response.data != null) {
                var fileName = '~' + response.data;
                window.location.href = SERVICE_BASE_URL + '/api/Analyze/DownloadFile?filePath=' + fileName;
            }
        }, function (XMLHttpRequest, textStatus, errorThrown) {
            if (XMLHttpRequest.status == 401) {
                ShowDialog("You are not authorized user. Please login again.", "Message");
            } else {
                alert(errorThrown);
            }
        });

    });


    $divMeasurementPatchDetails.find('#btnExportCGATS').unbind("click").on('click', function (e) {
        e.preventDefault();

        var selectedMatricSum = '';
        if ($divMeasurementPatchDetails.find("#ddlMatricSum").data("kendoDropDownList")) {
            selectedMatricSum = $divMeasurementPatchDetails.find("#ddlMatricSum").data("kendoDropDownList").value();
        }
        var measurementID = parseInt($hdnCurrentMeasurement.val());
        var grid = $('#divMeasurementPatchDetails').data('kendoGrid')
        var optimizationRun = $(this).attr('data-id');
        var sortObj = null;
        var filterObj = null;
        //set the sorting parameters
        if (grid.dataSource._sort) {
            sortObj = grid.dataSource._sort[0];
        }
        //set the filtering parameters
        if (grid.dataSource._filter) {
            filterObj = {
                filters: grid.dataSource._filter.filters,
                logic: 'AND'
            };
            //set the type of filter fields
            $.each(filterObj.filters, function (index, obj) {
                obj.dtype = grid.dataSource.options.schema.model.fields[obj.field].type;
            });
        }

        //create object
        var filterObj = {
            MeasurementId: measurementID,
            IsVerificationProject: $hdnVerification,
            IsSync1DTypeProject: $hdnSync1DType,
            metricSumID: selectedMatricSum,
            sort: sortObj,
            filter: filterObj
        };

        var ajaxParameter = {
            type: "POST",
            url: SERVICE_BASE_URL + 'api/Analyze/GetCGATSPatchReport',
            data: filterObj,
            dataType: "json",
            async: false,
            headers: {
                'Authorization': 'Basic ' + AUTH_TOKEN
            }
        };

        commonAjaxCall(ajaxParameter, function (response) {
            if (response.data != null) {
                var fileName = '~' + response.data;
                window.location.href = SERVICE_BASE_URL + '/api/Analyze/DownloadFile?filePath=' + fileName;
            }
        }, function (XMLHttpRequest, textStatus, errorThrown) {
            if (XMLHttpRequest.status == 401) {
                ShowDialog("You are not authorized user. Please login again.", "Message");
            } else {
                alert(errorThrown);
            }
        });

    });

    $divMeasurementPatchDetails.find('#btnExportCxF').unbind("click").on('click', function (e) {
        e.preventDefault();
        var measurementID = parseInt($hdnCurrentMeasurement.val());
        var selectedMatricSum = '';
        if ($divMeasurementPatchDetails.find("#ddlMatricSum").data("kendoDropDownList")) {
            selectedMatricSum = $divMeasurementPatchDetails.find("#ddlMatricSum").data("kendoDropDownList").value();
        }

        var grid = $('#divMeasurementPatchDetails').data('kendoGrid')
        var optimizationRun = $(this).attr('data-id');
        var sortObj = null;
        var filterObj = null;
        //set the sorting parameters
        if (grid.dataSource._sort) {
            sortObj = grid.dataSource._sort[0];
        }
        //set the filtering parameters
        if (grid.dataSource._filter) {
            filterObj = {
                filters: grid.dataSource._filter.filters,
                logic: 'AND'
            };
            //set the type of filter fields
            $.each(filterObj.filters, function (index, obj) {
                obj.dtype = grid.dataSource.options.schema.model.fields[obj.field].type;
            });
        }

        //create object
        var filterObj = {
            MeasurementId: measurementID,
            IsVerificationProject: $hdnVerification,
            IsSync1DTypeProject: $hdnSync1DType,
            metricSumID: selectedMatricSum,
            sort: sortObj,
            filter: filterObj
        };

        var ajaxParameter = {
            type: "POST",
            url: SERVICE_BASE_URL + 'api/Analyze/GetCFXPatchReport',
            data: filterObj,
            dataType: "json",
            async: false,
            headers: {
                'Authorization': 'Basic ' + AUTH_TOKEN
            }
        };

        commonAjaxCall(ajaxParameter, function (response) {
            if (response.data != null) {
                var fileName = '~' + response.data;
                window.location.href = SERVICE_BASE_URL + '/api/Analyze/DownloadFile?filePath=' + fileName;
            }
        }, function (XMLHttpRequest, textStatus, errorThrown) {
            if (XMLHttpRequest.status == 401) {
                ShowDialog("You are not authorized user. Please login again.", "Message");
            } else {
                alert(errorThrown);
            }
        });

    });

    $divMeasurementPatchDetails.find('#btnExportDotGain').unbind("click").on('click', function (e) {
        e.preventDefault();

        var selectedMatricSum = '';
        if ($divMeasurementPatchDetails.find("#ddlMatricSum").data("kendoDropDownList")) {
            selectedMatricSum = $divMeasurementPatchDetails.find("#ddlMatricSum").data("kendoDropDownList").value();
        }
        var measurementID = parseInt($hdnCurrentMeasurement.val());
        var grid = $('#divMeasurementPatchDetails').data('kendoGrid')
        var optimizationRun = $(this).attr('data-id');
        var sortObj = null;
        var filterObj = null;
        //set the sorting parameters
        if (grid.dataSource._sort) {
            sortObj = grid.dataSource._sort[0];
        }
        //set the filtering parameters
        if (grid.dataSource._filter) {
            filterObj = {
                filters: grid.dataSource._filter.filters,
                logic: 'AND'
            };
            //set the type of filter fields
            $.each(filterObj.filters, function (index, obj) {
                obj.dtype = grid.dataSource.options.schema.model.fields[obj.field].type;
            });
        }

        //create object
        var filterObj = {
            MeasurementId: measurementID,
            IsVerificationProject: $hdnVerification,
            IsSync1DTypeProject: $hdnSync1DType,
            metricSumID: selectedMatricSum,
            sort: sortObj,
            filter: filterObj
        };

        var ajaxParameter = {
            type: "POST",
            url: SERVICE_BASE_URL + 'api/Analyze/GetDotGainPatchReport',
            data: filterObj,
            dataType: "json",
            async: false,
            headers: {
                'Authorization': 'Basic ' + AUTH_TOKEN
            }
        };

        commonAjaxCall(ajaxParameter, function (response) {
            if (response.data != null) {
                var fileName = '~' + response.data;
                window.location.href = SERVICE_BASE_URL + '/api/Analyze/DownloadFile?filePath=' + fileName;
            }
        }, function (XMLHttpRequest, textStatus, errorThrown) {
            if (XMLHttpRequest.status == 401) {
                ShowDialog("You are not authorized user. Please login again.", "Message");
            } else {
                alert(errorThrown);
            }
        });

    });

    $divMeasurementPatchDetails.find('#lnkLinkFilter').on('click', function (e) {
        $("form.k-filter-menu button[type='reset']").trigger("click");
    });

    $('#divMeasurementPatchDetails .k-grid-header th.k-header').mouseenter(function () {
        $(this).find('.k-grid-filter').show();
    }).mouseleave(function () {
        $(this).find('.k-grid-filter').hide();
    });
}

////************************ CHART BINDINGS  ***********************////

////**************** START BIND THE ANALYZE CHART  ****************////
function BindAnalyzeChart() {
    kendo.ui.progress($chartLoader, true);
    var apiParameters = {
        ProjectId: $hdnProjectID,
        OptimizeRunId: $hdnOptimizationRunID,
        IsSync1DTypeProject: $hdnSync1DType,
        fromDeltaE: $selectedDeltaE,
        ProjectType: $hdnCurrentDlpSubOperation
    };

    var ajaxParameter = {
        type: "GET",
        url: SERVICE_BASE_URL + 'api/Chart/GenerateAnalyzeChart',
        data: apiParameters,
        dataType: "json",
        headers: {
            'Accept-Language': $CurrentCulture,
            'Authorization': 'Basic ' + AUTH_TOKEN
        },
    };

    commonAjaxCall(ajaxParameter, function (response) {
        if (response.data != null) {
            jsonAnalyzeList = response.data;
            _Resources = response.data.GlobalResources;
            if ($hdnCurrentDlpSubOperation != ELicenseType.Validation) {
                $('#divAnalyzeHeader').css('display', 'block');
            } else {
                $('#divAnalyzePagingSection').css('display', 'block');

            }
            BindPageNumber();
        }
    }, function (XMLHttpRequest, textStatus, errorThrown) {
        if (XMLHttpRequest.status == 401) {
            ShowDialog("You are not authorized user. Please login again.", "Message");
        } else {
            alert(errorThrown);
        }
        kendo.ui.progress($chartLoader, false);
    });

}


////**************** START BIND THE AB COMPARISION INK PAPER CHART  ****************////
function BindInkPaperChartABComparision() {
    var measurementID = parseInt($hdnCurrentMeasurement.val());
    var apiParameters = {
        MeasurementId: measurementID,
        IsAverageOfMeasurements: $isAverageOfMeasurements,
        IsVerificationProject: $hdnVerification,
        IsSync1DTypeProject: $hdnSync1DType,
        licenseTypeId: $hdnCurrentDlpSubOperation,
        fromDeltaE: $selectedDeltaE,
    };

    var ajaxParameter = {
        type: "GET",
        url: SERVICE_BASE_URL + 'api/Chart/GenerateInkPaperChart',
        data: apiParameters,
        dataType: "json",
        headers: {
            'Accept-Language': $CurrentCulture,
            'Authorization': 'Basic ' + AUTH_TOKEN
        }
    };

    kendo.ui.progress($chartLoader, true);
    commonAjaxCall(ajaxParameter, function (response) {
        if (response.data != null) {
            jsonDeltaEPrimaryPatchList = response.data.DeltaEPrimaryPatchList;
            if ($hdnCurrentDlpSubOperation == ELicenseType.DLP || $hdnValidationType == 0) {
                $tblDensityChart.css('display', 'none');
            }
            jsonDensityList = response.data.DensityList;
            jsonDeltaPaperLAB_1 = response.data.PaperPatchList;
            _Resources = response.data.GlobalResources;
        }

        if (jsonDeltaEPrimaryPatchList && jsonDeltaEPrimaryPatchList.length > 0) {

            var patchType8 = jsonDeltaEPrimaryPatchList.find(function (item) {
                return item.PatchTypeId === 8;
            });
            if (patchType8) {
                // Set values to hidden fields
                $('#hdnCielabL').val(patchType8.CielabL);
                $('#hdnCielabA').val(patchType8.CielabA);
                $('#hdnCielabB').val(patchType8.CielabB);
            }
        }

        PopulateInkPaperChartABComparision();
        kendo.ui.progress($chartLoader, false);
    }, function (XMLHttpRequest, textStatus, errorThrown) {
        if (XMLHttpRequest.status == 401) {
            ShowDialog("You are not authorized user. Please login again.", "Message");
        } else {
            alert(errorThrown);
        }
        kendo.ui.progress($chartLoader, false);
    });

}

////**************** START BIND THE INK PAPER (GAMUT) CHARTS  ****************////
function BindGrayCharts() {
    var measurementID = parseInt($hdnCurrentMeasurement.val());

    var apiParameters = {
        MeasurementId: measurementID,
        IsAverageOfMeasurements: $isAverageOfMeasurements,
        IsVerificationProject: $hdnVerification,
        IsSync1DTypeProject: $hdnSync1DType,
        IsDlpMultiColor: $hdnIsDlpMultiColor,
        isDestProfile: $hdnCurrentProjectLicenseType == 'DestinationProfile' ? 'True' : 'False'
    };

    var ajaxParameter = {
        type: "GET",
        url: SERVICE_BASE_URL + 'api/Chart/GenerateGrayChart',
        data: apiParameters,
        dataType: "json",
        headers: {
            'Accept-Language': $CurrentCulture,
            'Authorization': 'Basic ' + AUTH_TOKEN
        }
    };


    kendo.ui.progress($chartLoader, true);
    commonAjaxCall(ajaxParameter, function (response) {
        jsonTarget = response.data.GamutChartActual;
        jsonColors = response.data.GamutChart;
        jsonGray = response.data.LABChart;
        _Resources = response.data.GlobalResources;
        if ($hdnIsDlpMultiColor == "True" && $hdnCurrentRunName == "Run-0") {
            PopulateChartTargetVersusActualABMultiColor();
        } else {
            PopulateChartTargetVersusActualAB();
        }
        PopulateChartGamutABComparision();
        PopulateChartLComparision();
        PopulateChartLGrayBalance();
        kendo.ui.progress($chartLoader, false);
    }, function (XMLHttpRequest, textStatus, errorThrown) {
        if (XMLHttpRequest.status == 401) {
            ShowDialog("You are not authorized user. Please login again.", "Message");
        } else {
            //alert(errorThrown);
            console.log(errorThrown);
        }
        kendo.ui.progress($chartLoader, false);
    });

}


////**************** START BIND THE DELTA SUMMARY STATASTICS CHART FOR REPORT  ****************////
function BindDeltaSummaryStatesChart() {
    var measurementID = parseInt($hdnCurrentMeasurement.val());
    var apiParameters = {
        MeasurementId: measurementID,
        IsAverageOfMeasurements: $isAverageOfMeasurements,
        IsVerificationProject: $hdnVerification,
        IsSync1DTypeProject: $hdnSync1DType,
        fromDeltaE: $selectedDeltaE,
        validationType: $hdnValidationType,
        licenseTypeId: $hdnCurrentDlpSubOperation
    };

    var ajaxParameter = {
        type: "GET",
        url: SERVICE_BASE_URL + 'api/Report/GenerateSummaryDeltaCharts',
        data: apiParameters,
        dataType: "json",
        headers: {
            'Authorization': 'Basic ' + AUTH_TOKEN
        }
    };


    commonAjaxCall(ajaxParameter, function (response) {
        if (response != null) {
            jsonSummaryNameList = response.data.DTDeltaSummaryLables;
            jsonSummaryValueList = response.data.DTDeltaSummaryValues;
            jsonSummaryLimitList = response.data.DTDeltaSummaryLimits;
        }
        PopulateSummaryDeltaChart();
    }, function (XMLHttpRequest, textStatus, errorThrown) {
        if (XMLHttpRequest.status == 401) {
            ShowDialog("You are not authorized user. Please login again.", "Message");
        } else {
            alert(errorThrown);
        }
    });

}

////**************** START BIND THE G7 CHART  ****************////
function BindG7Charts() {
    var measurementID = parseInt($hdnCurrentMeasurement.val());
    var apiParameters = {
        MeasurementId: measurementID,
        IsAverageOfMeasurements: $isAverageOfMeasurements,
        IsVerificationProject: $hdnVerification,
        IsSyncDlpPlusTypeProject: $hdnSyncDlpPlusType
    };

    var ajaxParameter = {
        type: "GET",
        url: SERVICE_BASE_URL + 'api/Chart/GenerateG7Chart',
        data: apiParameters,
        dataType: "json",
        headers: {
            'Accept-Language': $CurrentCulture,
            'Authorization': 'Basic ' + AUTH_TOKEN
        }
    };

    commonAjaxCall(ajaxParameter, function (response) {
        if (response.data != null) {
            jsonGrayFChartList = response.data.GrayFChartList;
            jsonBlackLChartList = response.data.BlackLChartList;
            jsonGrayLChartList = response.data.GrayLChartList;
            jsonBlackNPDChartList = response.data.BlackNPDChartList;
            jsonGrayNPDChartList = response.data.GrayNPDChartList;

            kendo.bind($("#divGraySummaryList"), response.data);
            kendo.bind($("#divBlackSummaryList"), response.data);
            kendo.bind($("#divGrayLDeltaSummaryList"), response.data);

        }
        PopulateG7Charts();
    }, function (XMLHttpRequest, textStatus, errorThrown) {
        if (XMLHttpRequest.status == 401) {
            ShowDialog("You are not authorized user. Please login again.", "Message");
        } else {
            alert(errorThrown);
        }
        kendo.ui.progress($chartLoader, false);
    });


}

////**************** START BIND THE TVI CHARTS  ****************////
function BindTVICharts() {

    $("#tabstripTVI").kendoTabStrip({
        animation: {
            open: {
                effects: "fadeIn"
            }
        }
    });

    var measurementID = parseInt($hdnCurrentMeasurement.val());
    var apiParameters = {
        MeasurementId: measurementID,
        IsAverageOfMeasurements: $isAverageOfMeasurements,
        IsVerificationProject: $hdnVerification,
        IsSync1DTypeProject: $hdnSync1DType
    };

    if ($hdnValidationType == 2) {
        $divTVI_G7.css('display', 'none');
        $divTVI_ISO.css('display', 'block');

        var ajaxParameter = {
            type: "GET",
            url: SERVICE_BASE_URL + 'api/Chart/GenerateISOTVIChart',
            data: apiParameters,
            dataType: "json",
            headers: {
                'Accept-Language': $CurrentCulture,
                'Authorization': 'Basic ' + AUTH_TOKEN
            }
        };

        commonAjaxCall(ajaxParameter, function (response) {
            if (response.data != null) {
                jsonDTISOTVIChartList = response.data.DTISOTVIChartList;
                jsonDTSpreadChartData_CMYKList = response.data.DTSpreadChartData_CMYKList;
                jsonDTSpreadChartDataList = response.data.DTSpreadChartDataList;
                jsonDTSpreadSplineRangeChartDataList = response.data.DTSpreadSplineRangeChartDataList;
                PopulateTVIISOChart();
                PopulateTVIG7CMYKChart();
                PopulateTVISpreadChart();

                var spreadStatesViewModel = response.data.DTMaxSpreadChartDataList[0];
                kendo.bind($('#chromVariationTable'), spreadStatesViewModel);
            }
        }, function (XMLHttpRequest, textStatus, errorThrown) {
            if (XMLHttpRequest.status == 401) {
                ShowDialog("You are not authorized user. Please login again.", "Message");
            } else {
                alert(errorThrown);
            }
            kendo.ui.progress($chartLoader, false);
        });

    }


    if ($hdnValidationType == 1) {
        $divTVI_G7.css('display', 'block');
        $divTVI_ISO.css('display', 'none');

        var ajaxParameter = {
            type: "GET",
            url: SERVICE_BASE_URL + 'api/Chart/GenerateTVIChart',
            data: apiParameters,
            dataType: "json",
            headers: {
                'Accept-Language': $CurrentCulture,
                'Authorization': 'Basic ' + AUTH_TOKEN
            }
        };

        commonAjaxCall(ajaxParameter, function (response) {
            if (response.data != null) {
                jsonTVIG7ChartList = response.data.DTTVIChartList;
                PopulateTVIG7Chart();
            }
        }, function (XMLHttpRequest, textStatus, errorThrown) {
            if (XMLHttpRequest.status == 401) {
                ShowDialog("You are not authorized user. Please login again.", "Message");
            } else {
                alert(errorThrown);
            }
            kendo.ui.progress($chartLoader, false);
        });

    }

}


////********************************************** CHART POPULATIONS  **********************************************////

////**************** POPULATE THE ANALYZE CHART  ****************////
function PopulateAnalyzeChart() {

    var measurementID = 0;
    var indexStart = 0;
    var indexEnd = 0;
    viewNumber = parseInt($ddlView.val());
    pageNumber = parseInt($ddlPaging.val());

    if ($hdnCurrentDlpSubOperation != ELicenseType.Validation) {
        indexEnd = jsonAnalyzeList.Measurements.length;
        indexStart = 0;
    } else {
        if (viewNumber != -1) {
            indexEnd = jsonAnalyzeList.Measurements.length - ((totalPages - pageNumber) * viewNumber);
            indexStart = (indexEnd - viewNumber) < 0 ? 0 : (indexEnd - viewNumber);
        } else {
            indexEnd = jsonAnalyzeList.Measurements.length - 1;
            indexStart = 0;
        }
    }


    $('#lblPagingMsg').html('Record(s) ' + (indexStart + 1) + ' to ' + (indexEnd) + ' of ' + jsonAnalyzeList.Measurements.length + '');


    var analyzeSeriesResult = $.grep(jsonAnalyzeList.Measurements, function (item, index) {
        return index > indexStart - 1 && index < indexEnd;
    });


    var patchArray = [];
    if (analyzeSeriesResult.length > 0) {

        //check last measurement and make the last measurement as current in list
        if ($hdnCurrentMeasurement.val() == '') {
            $hdnCurrentMeasurement.val(analyzeSeriesResult[analyzeSeriesResult.length - 1].MeasurementId);
        } else {

            //check the current measurement is in current paging list if not then make the last measurement as current in list
            if ($.inArray(parseInt($hdnCurrentMeasurement.val()), $.map(analyzeSeriesResult, function (e) {
                return e.MeasurementId
            })) < 0) {
                $hdnCurrentMeasurement.val(analyzeSeriesResult[analyzeSeriesResult.length - 1].MeasurementId);
            }
        }

        //set name of the selected measurement in header lable
        if ($hdnCurrentDlpSubOperation != ELicenseType.Validation) {
            $('#lblNewAnalyzeLabel').html($.grep(analyzeSeriesResult, function (e) { return e.MeasurementId == parseInt($hdnCurrentMeasurement.val()); })[0].MeasurementName);
        }

        //create the list of measurement in chart
        measurementNamesList = $.map(analyzeSeriesResult, function (e) {
            var curObj = {
                MeasurementId: e.MeasurementId,
                MeasurementName: e.MeasurementName,
                IsUsed: e.IsUsed,
                MeasurementStatus: e.MeasurementStatus,
                MeasurementDate: e.MeasurementDate,
                dEMethod: e.dEMethod,
                MeasurementDateString: e.MeasurementDateString
            };
            return curObj;
        });

        //get the available list of patch id
        var measurementPatchList = $.map(analyzeSeriesResult[0].MeasurementPatches, function (e) {
            return e.MeasurementPatchId;
        });

        //generate new array to display the patches
        $.each(measurementPatchList, function (index, patchID) {

            //check one by one all html code
            $.each(analyzeSeriesResult, function (index, measureObj) {

                //check the current patch in the main list
                var arr1Obj1 = $.grep(measureObj.MeasurementPatches, function (e) {
                    return e.MeasurementPatchId == patchID;
                });

                //check the patch is already available in the resulted array
                var availablePatch = $.grep(patchArray, function (e) {
                    return e.PatchID == patchID;
                });
                //if exists then
                if (availablePatch.length > 0 && arr1Obj1.length > 0) {
                    availablePatch[0].data.push(arr1Obj1[0].Δe2000); //add into existing E2000 in data array
                }
                else {//add new object in resulted array

                    if (arr1Obj1.length > 0) {
                        var patchObj = {
                            data: [arr1Obj1[0].Δe2000],
                            color: arr1Obj1[0].HtmlColor,
                            PatchID: patchID
                        }

                        patchArray.push(patchObj);
                    }
                }
            });
        });
    }

    $divAnalyzeChart.kendoChart({
        seriesDefaults: {
            type: "line",
            style: "smooth",
            markers: {
                background: function (e) {
                    if ($hdnCurrentDlpSubOperation == ELicenseType.Validation && e.category.MeasurementId != parseInt($hdnCurrentMeasurement.val())) {
                        if (e.category.MeasurementStatus != 'OK') {
                            return "red";
                        } else {
                            return "gray";
                        }
                    }
                    else {
                        if (e.category.MeasurementId == parseInt($hdnCurrentMeasurement.val())) {
                            return e.series.color;
                        }
                        else {
                            return "gray";
                        }
                    }
                },
                border: {
                    width: 1,
                    color: function (e) {
                        if ($hdnCurrentDlpSubOperation == ELicenseType.Validation && e.category.MeasurementId != parseInt($hdnCurrentMeasurement.val())) {
                            if (e.category.MeasurementStatus != 'OK') {
                                return "red";
                            } else {
                                return "gray";
                            }
                        }
                        else {
                            if (e.category.MeasurementId == parseInt($hdnCurrentMeasurement.val())) {
                                return e.series.color;
                            }
                            else {
                                return "gray";
                            }
                        }
                    }
                },
                type: function (e) {
                    if ($hdnCurrentDlpSubOperation == ELicenseType.Validation) {
                        if (e.category.MeasurementId == parseInt($hdnCurrentMeasurement.val())) {
                            return "circle";
                        } else {
                            if (e.category.MeasurementStatus != 'OK') {
                                return "triangle";
                            } else {
                                return "circle";
                            }
                        }
                    }
                    else {
                        return "circle";
                    }
                },
            }
        },
        series: patchArray,
        valueAxis: {

            title: {
                text: 'Δ' + $selectedDeltaE,
                font: "12px Arial,Helvetica,sans-serif",
            },
            labels: {
                format: "{0}"
            },
            line: {
                visible: false
            },
            axisCrossingValue: -10,
            //majorUnit: 2
        },
        categoryAxis: {
            name: "categoryAxis",
            categories: measurementNamesList,
            majorGridLines: {
                visible: false
            },
            labels: {
                rotation: "auto",
                //cursor: "pointer",                
                visual: function (e) {

                    // Create an empty group that will hold the rendered label
                    var visual = new kendo.drawing.Group();

                    // Build an HTML fragment and append it to the body                    
                    //get the category values
                    var catValue = e.value;

                    if ($hdnCurrentDlpSubOperation == ELicenseType.Validation) {

                        var arr = catValue.MeasurementDateString.split('-');
                        var catText = "";
                        if (viewNumber > 10) {
                            //catText = '<div style="transform: rotate(-90deg);font-size:10px"><div>' + arr[0] + '</div><div>' + arr[1] + '</div></div>';
                            catText = '<div style="transform: rotate(-90deg);font-size:9px"><div>' + arr[0] + '</div></div>';
                        }
                        else {
                            catText = '<div style="text-align:center;"><div>' + arr[0] + '</div><div>' + arr[1] + '</div></div>';
                        }
                        //set checked or not                        
                        // var catText = '<div style="text-align:center;"><div>' + arr[0] + '</div><div>' + arr[1] + '</div></div>';                       

                        var html = $(catText).appendTo(document.body);

                        // Store a reference to the target rectangle, see below
                        var rect = e.rect;
                        //create kendo DOM
                        kendo.drawing.drawDOM(html)
                            .done(function (group) {
                                // Clean-up HTML fragment
                                html.remove();

                                // Center the label using Layout
                                var layout = new kendo.drawing.Layout(rect, {
                                    justifyContent: "center"
                                });
                                layout.append(group);
                                layout.reflow();
                                // Render the content
                                visual.append(layout);
                                if (viewNumber > 10) {
                                    visual.options.tooltip = {
                                        content: '<div style="text-align:center;"><div>' + arr[0] + '</div><div>' + arr[1] + '</div></div>'
                                    };
                                }
                            });
                    } else {

                        //set checked or not
                        var catText = '<div style="font-size:18px;cursor: pointer;">' + (catValue.IsUsed ? '☑' : '☐') + '</div>'

                        var html = $(catText).appendTo(document.body);
                        // Store a reference to the target rectangle, see below
                        var rect = e.rect;

                        //create kendo DOM
                        kendo.drawing.drawDOM(html)
                            .done(function (group) {
                                // Clean-up HTML fragment
                                html.remove();
                                // Center the label using Layout
                                var layout = new kendo.drawing.Layout(rect, {
                                    justifyContent: "center"
                                });
                                layout.append(group);
                                layout.reflow();
                                // Render the content
                                visual.append(layout);
                            });

                    }
                    return visual;
                }
            },
        },
        tooltip: {
            visible: true,
            template: "${category.MeasurementName}",
        },
        chartArea: {
            width: 690,
            height: 200,
            background: "#CECECE",
            //opacity: 0.3
        },

        axisLabelClick: function (e) {
            if (!$('#chkAnalyzeAverageOfMeasurements').is(':checked')) {
                var isChecked = $(e.element).text() != '☑' ? true : false;
                //get the current clicked category data
                var currentObj = e.value;

                var selectedMeasurement = currentObj;

                if (isChecked) {
                    selectedMeasurement = currentObj;
                }
                else {
                    var otherMeasurements = $.grep(measurementNamesList, function (m) {
                        return m.MeasurementId != currentObj.MeasurementId && m.IsUsed;
                    });

                    if (otherMeasurements.length > 0) {

                        selectedMeasurement = otherMeasurements.reduce(function (max, item) {
                            return item.MeasurementId > max.MeasurementId ? item : max;
                        });
                    }
                }

                //check only one category available or not
                if (measurementNamesList.length <= 1 && currentObj.IsUsed) {
                    alert(_Resources.selectMeasurementMsg);
                    return false;
                }

                //check in multiple category
                var otherMeasurements = $.grep(measurementNamesList, function (elem) {
                    return elem.MeasurementId != currentObj.MeasurementId && (elem.IsUsed || !currentObj.IsUsed)
                });

                //check atleast one category should checked
                if (otherMeasurements.length == 0) {
                    alert(_Resources.selectMeasurementMsg);
                    return false;
                }
                //api call to update the measurement
                var apiParameters = {
                    MeasurementId: currentObj.MeasurementId,
                    isMeasurementUsed: !currentObj.IsUsed,
                    IsVerificationProject: $hdnVerification,
                    IsSync1DTypeProject: $hdnSync1DType,
                };

                var ajaxParameter = {
                    type: "GET",
                    url: SERVICE_BASE_URL + 'api/Chart/UpdateMeasurementFromAnalyzeChart',
                    data: apiParameters,
                    dataType: "json",
                    headers: {
                        'Authorization': 'Basic ' + AUTH_TOKEN
                    }
                };

                commonAjaxCall(ajaxParameter, function (response) {
                    $hdnCurrentMeasurement.val(selectedMeasurement.MeasurementId);
                    $hdMeasurement.val(selectedMeasurement.MeasurementId);


                    //update the average measurement value 
                    $.ajax({
                        url: SERVICE_BASE_URL + 'api/Analyze/GetAverageMeasurementCompatibility',
                        data: { OptimizationRunID: parseInt($hdnOptimizationRunID) },
                        type: 'GET',
                        dataType: 'json',
                        success: function (response) {
                            $('#ContentPlaceHolder1_ucAnalyze_hd_CompError').val(response.data)
                            $("[id*='lblCompError']").html('');

                        },
                        error: function (xhr, errorType, exception) {
                        }
                    });

                    BindAnalyzeChart();
                }, function (XMLHttpRequest, textStatus, errorThrown) {
                    if (XMLHttpRequest.status == 401) {
                        ShowDialog("You are not authorized user. Please login again.", "Message");
                    } else {
                        alert(errorThrown);
                    }
                });
                if (isChecked) {
                    UpdateNewPaperValues1D(currentObj);
                } else {
                    UpdateNewPaperValues1D(selectedMeasurement);
                }
            }
        },
        seriesClick: function (e) {
            if (!$('#chkAnalyzeAverageOfMeasurements').is(':checked')) {
                if ($hdnCurrentDlpSubOperation == ELicenseType.Validation) {
                    var dMethod = e.category.dEMethod;
                    dMethod = dMethod.substring(1, dMethod.length);
                    $('#hdnDefaultSelectedDelta').val(dMethod);
                    $selectedDeltaE = dMethod;

                    $hdnCurrentMeasurement.val(e.category.MeasurementId);
                    $hdMeasurement.val($hdnCurrentMeasurement.val());
                    BindAllTabs();

                    var analyzeChart = $divAnalyzeChart.data('kendoChart');
                    analyzeChart.options.valueAxis.title.text = e.category.dEMethod;
                    analyzeChart.refresh();
                }
                else {
                    $hdnCurrentMeasurement.val(e.category.MeasurementId);
                    $hdMeasurement.val($hdnCurrentMeasurement.val());
                    BindAllTabs();

                    $divAnalyzeChart.data('kendoChart').refresh();
                    $('#lblNewAnalyzeLabel').html(e.category.MeasurementName);
                }
            }
            UpdateNewPaperValues1D(e.category);
        }
    });
    BindAllTabs();
}

function UpdateNewPaperValues1D(currentObj) {
    // 1. if RPS is on then get latest cielab data and set to hidden.
    if ($("#chk_IsPaperScaling1D").is(':checked') || $("[id*='chk_IsPaperScaling4D']").is(':checked')) {
        var measurementID = currentObj.MeasurementId;
        var apiParameters = {
            MeasurementId: measurementID,
            IsAverageOfMeasurements: $isAverageOfMeasurements,
            IsVerificationProject: $hdnVerification,
            IsSync1DTypeProject: $hdnSync1DType,
            licenseTypeId: $hdnCurrentDlpSubOperation,
            fromDeltaE: $selectedDeltaE,
        };

        var ajaxParameter = {
            type: "GET",
            url: SERVICE_BASE_URL + 'api/Chart/GenerateInkPaperChart',
            data: apiParameters,
            dataType: "json",
            headers: {
                'Accept-Language': $CurrentCulture,
                'Authorization': 'Basic ' + AUTH_TOKEN
            }
        };

        kendo.ui.progress($chartLoader, true);
        commonAjaxCall(ajaxParameter, function (response) {
            if (response.data != null) {
                jsonDeltaEPrimaryPatchList = response.data.DeltaEPrimaryPatchList;
                if ($hdnCurrentDlpSubOperation == ELicenseType.DLP || $hdnValidationType == 0) {
                    $tblDensityChart.css('display', 'none');
                }
                jsonDensityList = response.data.DensityList;
                jsonDeltaPaperLAB_1 = response.data.PaperPatchList;
                _Resources = response.data.GlobalResources;
            }

            if (jsonDeltaEPrimaryPatchList && jsonDeltaEPrimaryPatchList.length > 0) {
                var patchType8 = jsonDeltaEPrimaryPatchList.find(function (item) {
                    return item.PatchTypeId === 8;
                });
                if (patchType8) {
                    // Set values to hidden fields
                    $('#hdnCielabL').val(patchType8.CielabL);
                    $('#hdnCielabA').val(patchType8.CielabA);
                    $('#hdnCielabB').val(patchType8.CielabB);
                }
            }
            // 2. trigger 1D chk checkbox and update in db.
            if ($("#chk_IsPaperScaling1D").is(':checked')) {
                $("#btnRPSPostBack1D").click();
            } else if ($("[id*='chk_IsPaperScaling4D']").is(':checked')) {
                $("#btnRPSPostBack4D").click();
            }
        })
    }
}


function PopulateAnalyzeDestinationChart() {

    var measurementID = 0;
    var indexStart = 0;
    var indexEnd = 0;
    viewNumber = parseInt($ddlView.val());
    pageNumber = parseInt($ddlPaging.val());

    indexEnd = jsonAnalyzeList.Measurements.length;
    indexStart = 0;


    $('#lblPagingMsg').html('Record(s) ' + (indexStart + 1) + ' to ' + (indexEnd) + ' of ' + jsonAnalyzeList.Measurements.length + '');


    var analyzeSeriesResult = $.grep(jsonAnalyzeList.Measurements, function (item, index) {
        return index > indexStart - 1 && index < indexEnd;
    });


    var patchArray = [];
    if (analyzeSeriesResult.length > 0) {

        //check last measurement and make the last measurement as current in list
        $hdnCurrentMeasurement.val(analyzeSeriesResult[analyzeSeriesResult.length - 1].MeasurementId);

        //set name of the selected measurement in header lable
        $('#lblNewAnalyzeLabel').html($.grep(analyzeSeriesResult, function (e) { return e.MeasurementId == parseInt($hdnCurrentMeasurement.val()); })[0].MeasurementName);

        //create the list of measurement in chart
        measurementNamesList = $.map(analyzeSeriesResult, function (e) {
            var curObj = {
                MeasurementId: e.MeasurementId,
                MeasurementName: e.MeasurementName,
                IsUsed: e.IsUsed,
                MeasurementStatus: e.MeasurementStatus,
                MeasurementDate: e.MeasurementDate,
                dEMethod: e.dEMethod,
                MeasurementDateString: e.MeasurementDateString
            };
            return curObj;
        });

        //get the available list of patch id
        var measurementPatchList = $.map(analyzeSeriesResult[0].MeasurementPatches, function (e) {
            return e.MeasurementPatchId;
        });

        //generate new array to display the patches
        $.each(measurementPatchList, function (index, patchID) {

            //check one by one all html code
            $.each(analyzeSeriesResult, function (index, measureObj) {

                //check the current patch in the main list
                var arr1Obj1 = $.grep(measureObj.MeasurementPatches, function (e) {
                    return e.MeasurementPatchId == patchID;
                });

                //check the patch is already available in the resulted array
                var availablePatch = $.grep(patchArray, function (e) {
                    return e.PatchID == patchID;
                });
                //if exists then
                if (availablePatch.length > 0 && arr1Obj1.length > 0) {
                    availablePatch[0].data.push(arr1Obj1[0].Δe2000); //add into existing E2000 in data array
                }
                else {//add new object in resulted array

                    if (arr1Obj1.length > 0) {
                        var patchObj = {
                            data: [arr1Obj1[0].Δe2000],
                            color: arr1Obj1[0].HtmlColor,
                            PatchID: patchID
                        }

                        patchArray.push(patchObj);
                    }
                }
            });
        });
    }


    $divAnalyzeDestinationChart.kendoChart({
        seriesDefaults: {
            type: "line",
            style: "smooth",
            markers: {
                background: function (e) {
                    if (e.category.MeasurementId == parseInt($hdnCurrentMeasurement.val())) {
                        return "Green";
                    }
                    else {
                        return "Black";
                    }
                },
                border: {
                    width: 2,
                    color: function (e) {
                        if (e.category.MeasurementId == parseInt($hdnCurrentMeasurement.val())) {
                            return "Green";
                        }
                        else {
                            return "Black";
                        }
                    }
                },

            }
        },
        series: patchArray,
        valueAxis: {
            labels: {
                format: "{0}"
            },
            line: {
                visible: false
            },
            axisCrossingValue: 0,
            majorUnit: -1,

        },
        categoryAxis: {
            name: "categoryAxis",
            categories: measurementNamesList,
            majorGridLines: {
                visible: false
            },
            labels: {
                rotation: "auto",
                //cursor: "pointer",                
                visual: function (e) {

                    // Create an empty group that will hold the rendered label
                    var visual = new kendo.drawing.Group();

                    // Build an HTML fragment and append it to the body                    
                    //get the category values
                    var catValue = e.value;

                    var arr = catValue.MeasurementName.toString();

                    //set checked or not
                    var catText = '<div style="text-align:center;"><div>' + arr + '<div style="font-size:18px;cursor: pointer;">' + (catValue.IsUsed ? '☑' : '☐') + '</div>'

                    var html = $(catText).appendTo(document.body);
                    // Store a reference to the target rectangle, see below
                    var rect = e.rect;

                    //create kendo DOM
                    kendo.drawing.drawDOM(html)
                        .done(function (group) {
                            // Clean-up HTML fragment
                            html.remove();
                            // Center the label using Layout
                            var layout = new kendo.drawing.Layout(rect, {
                                justifyContent: "center"
                            });
                            layout.append(group);
                            layout.reflow();
                            // Render the content
                            visual.append(layout);
                        });


                    return visual;
                }
            },
        },
        tooltip: {
            visible: true,
            template: "${category.MeasurementName}",
        },
        chartArea: {
            width: 690,
            height: 160,
            background: "#CECECE",
        },

        axisLabelClick: function (e) {
            if (!$('#chkAnalyzeAverageOfMeasurements').is(':checked')) {
                var isChecked = $(e.element).text() != '☑' ? true : false;
                //get the current clicked category data
                var currentObj = e.value;
                //check only one category available or not
                if (measurementNamesList.length <= 1 && currentObj.IsUsed) {
                    alert(_Resources.selectMeasurementMsg);
                    return false;
                }

                //check in multiple category
                var otherMeasurements = $.grep(measurementNamesList, function (elem) {
                    return elem.MeasurementId != currentObj.MeasurementId && (elem.IsUsed || !currentObj.IsUsed)
                });

                //check atleast one category should checked
                if (otherMeasurements.length == 0) {
                    alert(_Resources.selectMeasurementMsg);
                    return false;
                }
                //api call to update the measurement
                var apiParameters = {
                    MeasurementId: currentObj.MeasurementId,
                    isMeasurementUsed: !currentObj.IsUsed,
                    IsVerificationProject: $hdnVerification,
                    IsSync1DTypeProject: $hdnSync1DType,
                };

                var ajaxParameter = {
                    type: "GET",
                    url: SERVICE_BASE_URL + 'api/Chart/UpdateMeasurementFromAnalyzeChart',
                    data: apiParameters,
                    dataType: "json",
                    headers: {
                        'Authorization': 'Basic ' + AUTH_TOKEN
                    }
                };

                commonAjaxCall(ajaxParameter, function (response) {
                    $hdnCurrentMeasurement.val(currentObj.MeasurementId);
                    $hdMeasurement.val(currentObj.MeasurementId);


                    //update the average measurement value 
                    $.ajax({
                        url: SERVICE_BASE_URL + 'api/Analyze/GetAverageMeasurementCompatibility',
                        data: { OptimizationRunID: parseInt($hdnOptimizationRunID) },
                        type: 'GET',
                        dataType: 'json',
                        success: function (response) {
                            $('#ContentPlaceHolder1_ucAnalyze_hd_CompError').val(response.data)
                            $("[id*='lblCompError']").html('');

                        },
                        error: function (xhr, errorType, exception) {
                        }
                    });

                    BindAnalyzeChart();
                }, function (XMLHttpRequest, textStatus, errorThrown) {
                    if (XMLHttpRequest.status == 401) {
                        ShowDialog("You are not authorized user. Please login again.", "Message");
                    } else {
                        alert(errorThrown);
                    }
                });

            }
        },
        seriesClick: function (e) {
            if (!$('#chkAnalyzeAverageOfMeasurements').is(':checked')) {

                $hdnCurrentMeasurement.val(e.category.MeasurementId);
                $hdMeasurement.val($hdnCurrentMeasurement.val());
                BindAllTabs();

                $divAnalyzeDestinationChart.data('kendoChart').refresh();
                $('#lblNewAnalyzeLabel').html(e.category.MeasurementName);

            }
        }
    });

    BindAllTabs();
}
////**************** POPULATE THE INK PAPER AB COMPARISION (GMAUT) CHART  ****************////

function BuildPatchTooltip(item) {

    var toolTipString = "";
    var textColor = "#FFFFFF";

    if (item.color) {

        var color = item.color.toUpperCase();

        // Light colors need dark text
        if (color == "#FFEC00" || color == "#FFF000" ||     // Yellow
            color == "#D4CBC5") {      // Paper
            textColor = "#000000";
        }
    }
    toolTipString += "<div style='";
    toolTipString += "background-color:" + item.color + ";";
    toolTipString += "color:" + textColor + ";";
    toolTipString += "margin:-8px;";
    toolTipString += "padding:8px;";
    toolTipString += "border:none;";
    toolTipString += "border-radius:5px;";
    toolTipString += "'>";

    toolTipString += "<table>";
    toolTipString += "<tr><td><b>" + item.title + "</b></td></tr>";

    toolTipString += "<tr><td><table>";

    toolTipString += "<tr><td><b>" + _Resources._recActual + "</b></td><td>";
    toolTipString += _Resources._recL + ": " + item.CielabL.toFixed(2);
    toolTipString += " " + _Resources._recA + ": " + item.CielabA.toFixed(2);
    toolTipString += " " + _Resources._recB + ": " + item.CielabB.toFixed(2);
    toolTipString += "</td></tr>";

    toolTipString += "<tr><td><b>" + _Resources._recTarget + "</b></td><td>";
    toolTipString += _Resources._recL + ": " + item.TargetL.toFixed(2);
    toolTipString += " " + _Resources._recA + ": " + item.Targeta.toFixed(2);
    toolTipString += " " + _Resources._recB + ": " + item.Targetb.toFixed(2);
    toolTipString += "</td></tr>";

    toolTipString += "<tr><td><b>" + _Resources._recDeltaE + "</b></td><td>";
    toolTipString += item.value.toFixed(2);

    //-----For Sync1D projects or validation projects
    if ($hdnCurrentDlpSubOperation == ELicenseType.Sync1D || $hdnCurrentDlpSubOperation == ELicenseType.Sync1DMultiColor || $hdnCurrentDlpSubOperation == ELicenseType.Validation || $hdnCurrentDlpSubOperation == ELicenseType.DLPPlus) {
        toolTipString += " (Thr." + item.ΔELimit.toFixed(2) + ")";
    }
    toolTipString += "</td></tr>";

    //-----For Sync1D projects, display Previous ΔE in tooltip also
    if (($hdnCurrentDlpSubOperation == ELicenseType.Sync1D || $hdnCurrentDlpSubOperation == ELicenseType.Sync1DMultiColor || $hdnCurrentDlpSubOperation == ELicenseType.DLPPlus) && item.PreAvgΔE > 0) {
        toolTipString += "<tr><td><b>" + _Resources._recPreDeltaE + "</b></td><td>";
        toolTipString += item.PreAvgΔE.toFixed(2);
        toolTipString += "</td></tr>";
    }

    toolTipString += "</table></td></tr>";
    toolTipString += "</table>";

    toolTipString += "</div>";

    return toolTipString;
}

function PopulateInkPaperChartABComparision() {

    var maxNum = 0;
    var maxDeltaLimit = 0;
    var formattedDeltaEPrimaryPatchList = [];

    //set the color name and its tooltip title with resources
    var patchColor = [
        { colorName: _Resources._recCyan, colorTitle: _Resources._recCyanPathMetrics }, { colorName: _Resources._recMagenta, colorTitle: _Resources._recMagentaPathMetrics },
        { colorName: _Resources._recYellow, colorTitle: _Resources._recYellowPathMetrics }, { colorName: _Resources._recBlack, colorTitle: _Resources._recBlackPathMetrics },
        { colorName: _Resources._recRed, colorTitle: _Resources._recRedPathMetrics }, { colorName: _Resources._recGreen, colorTitle: _Resources._recGreenPathMetrics },
        { colorName: _Resources._recBlue, colorTitle: _Resources._recBluePathMetrics }, { colorName: _Resources._recPaper, colorTitle: _Resources._recPaperPathMetrics },
        { colorName: _Resources._recTriplet, colorTitle: _Resources._recTripletPathMetrics }];


    $.each(jsonDeltaEPrimaryPatchList, function (index, obj) {

        var item = {
            color: obj.HtmlColor,
            name: patchColor[index].colorName,
            title: patchColor[index].colorTitle,
            value: obj.ΔE,
            CielabL: obj.CielabL,
            CielabA: obj.CielabA,
            CielabB: obj.CielabB,
            TargetL: obj.TargetL,
            Targeta: obj.Targeta,
            Targetb: obj.Targetb,
            ΔELimit: obj.ΔELimit,
            PreAvgΔE: obj.PreAvgΔE
        };
        formattedDeltaEPrimaryPatchList.push(item);
    });

    //var axis = json.map(function (a) { return a["?E"]; });
    var axis = jsonDeltaEPrimaryPatchList.map(function (a) { return a.ΔE; });
    var patchColorList = formattedDeltaEPrimaryPatchList.map(function (a) { return a.name; });


    if ($hdnCurrentDlpSubOperation == ELicenseType.Sync1D || $hdnCurrentDlpSubOperation == ELicenseType.Sync1DMultiColor || $hdnCurrentDlpSubOperation == ELicenseType.Validation || $hdnCurrentDlpSubOperation == ELicenseType.DLPPlus) {
        maxNum = Math.max.apply(Math, axis) + 1;    //get the max ΔE with +1
        var DeltaLimitArray = jsonDeltaEPrimaryPatchList.map(function (a) { return a.ΔELimit; });
        maxDeltaLimit = Math.max.apply(Math, DeltaLimitArray) + 1
        if (maxNum > maxDeltaLimit) {
            maxDeltaLimit = maxNum;
        }
        maxDeltaLimit = (maxDeltaLimit > 4 ? maxDeltaLimit : maxDeltaLimit + 1)
    } else {
        maxDeltaLimit = Math.max.apply(Math, axis) + 2;    //get the max ΔE with +1
        maxDeltaLimit = (maxDeltaLimit > 4 ? maxDeltaLimit : maxDeltaLimit + 1)
    }

    maxDeltaLimit = Math.trunc(maxDeltaLimit) + 1;
    //generate the second stacked chart by max value
    var stackedChart = [];
    $.each(axis, function (index, num) {
        var stack = {
            value: maxDeltaLimit - num,
            patchData: formattedDeltaEPrimaryPatchList[index]
        };
        stackedChart.push(stack);
    });

    var thrashHold = { paths: [] };

    formattedDeltaEPrimaryPatchList = formattedDeltaEPrimaryPatchList.reverse();
    stackedChart = stackedChart.reverse();
    patchColorList = patchColorList.reverse();
    $divChartInkPaperABComparision.kendoChart({
        renderAs: "canvas",
        //title: {
        //    text: "ΔE2000 Primary Patches"
        //},
        seriesDefaults: {
            type: "bar",
            stack: true,
            width: 10,
            gap: 0.2,
            highlight: {
                toggle: function (e) {
                    // Don't create a highlight overlay,
                    // we'll modify the existing visual instead
                    e.preventDefault();

                    var visual = e.visual;
                    var opacity = e.show ? 0.8 : 1;

                    visual.opacity(opacity);
                }
            },
            visual: function (e) {
                var visual = e.createVisual();
                //-----For Sync1D projects or validation projects
                if ($hdnCurrentDlpSubOperation == ELicenseType.Sync1D || $hdnCurrentDlpSubOperation == ELicenseType.Sync1DMultiColor || $hdnCurrentDlpSubOperation == ELicenseType.Validation || $hdnCurrentDlpSubOperation == ELicenseType.DLPPlus) {
                    if (e.series.index == 0) {
                        var axis = e.sender.getAxis("valueAxis");

                        //------Green marker image plot for Previous Run's Avg ΔE
                        if (e.dataItem.PreAvgΔE > 0) {
                            var greenObj = {};
                            greenObj.x = axis.slot(e.dataItem.PreAvgΔE).origin.x;
                            greenObj.y1 = e.rect.topRight().y
                            greenObj.y2 = (e.rect.topRight().y + e.rect.size.height);
                            greenObj.y3 = (e.rect.topRight().y + (e.rect.size.height / 2));//---green dot mid of the line
                            greenObj.color = "#34E61E";
                            thrashHold.paths.push(greenObj);
                        }

                        //------Red marker image plot for Tolerance
                        if (e.dataItem.ΔELimit > 0) {
                            var redObj = {};
                            redObj.x = axis.slot(e.dataItem.ΔELimit).origin.x;
                            redObj.y1 = e.rect.topRight().y
                            redObj.y2 = (e.rect.topRight().y + e.rect.size.height);
                            redObj.y3 = (e.rect.topRight().y + (e.rect.size.height / 2)); //---red dot mid of the line
                            redObj.color = "red";
                            thrashHold.paths.push(redObj);
                        }
                        //thrashHold.paths = [];
                    }
                }
                return visual;
            }
        },
        series: [{
            data: formattedDeltaEPrimaryPatchList,
            overlay: {
                gradient: "glass",
            }
        },
        {
            data: stackedChart,
            overlay: {
                gradient: "glass",
            },
            //tooltip: {
            //    visible: false
            //},
            tooltip: {
                visible: true,
                template: function (e) {

                    var item = e.dataItem.patchData;
                    if (!item)
                        return "";
                    return BuildPatchTooltip(item);
                }
            },
            color: "#C8D6E3"
            //color: function (point) {
            //    return point.dataItem.patchData.color;
            //},
        }],
        panes: [{
            clip: false
        }],
        categoryAxis: {
            name: "categoryAxis",
            categories: patchColorList,
            majorGridLines: {
                visible: false
            },
            reverse: true
        },
        valueAxis: {
            name: "valueAxis",
            //max: 20,
            line: {
                visible: false
            },
            minorGridLines: {
                visible: false
            },
            labels: {
                rotation: "auto"
            },
            max: maxDeltaLimit,
            //margin: marginValue
        },
        tooltip: {
            visible: true,
            template: function (dataItem) {
                return BuildPatchTooltip(dataItem.dataItem);
            }
        },
        chartArea: {
            width: 310,
            height: 325
        },
        render: function (e) {
            //-----For Sync1D projects or validation projects
            if ($hdnCurrentDlpSubOperation == ELicenseType.Sync1D || $hdnCurrentDlpSubOperation == ELicenseType.Sync1DMultiColor || $hdnCurrentDlpSubOperation == ELicenseType.Validation || $hdnCurrentDlpSubOperation == ELicenseType.DLPPlus) {
                var chart = e.sender;
                $.each(thrashHold.paths, function (index, obj) {
                    //set the line 
                    var pathGreen = new kendo.drawing.Path({ stroke: { width: 0.6, color: obj.color } }).moveTo(obj.x, obj.y1).lineTo(obj.x, obj.y2);
                    chart.surface.draw(pathGreen);

                    //set the dot mid of the line
                    var circleGeoPath = new kendo.geometry.Circle([obj.x, obj.y3], 2);  //
                    var circlePath = new kendo.drawing.Circle(circleGeoPath).fill(obj.color, 1);
                    circlePath.options.stroke.color = obj.color;
                    chart.surface.draw(circlePath);
                });
                thrashHold.paths = [];
            }
        }
    });

    var formattedDensityList = [];
    $.each(jsonDensityList, function (index, obj) {
        var patchTitle = '';
        switch (obj.PatchTypeId) {
            case 1: patchTitle = _Resources._recCyanPathMetrics; break;
            case 2: patchTitle = _Resources._recMagentaPathMetrics; break;
            case 3: patchTitle = _Resources._recYellowPathMetrics; break;
            case 4: patchTitle = _Resources._recBlackPathMetrics; break;
        }

        var item = {
            color: obj.HtmlColor,
            title: patchTitle,
            value: obj.Density,
            density: obj.Density,
            densityLimit: obj.DensityLimit,
            preAvgDensity: obj.PreAvgDensity
        };
        formattedDensityList.push(item);
    });


    var densityPatchColorList = ['C', 'M', 'Y', 'K'];

    if ($hdnCurrentDlpSubOperation == ELicenseType.DLPPlus) {
        $("#tblDensityChart").hide();
    }
    $divChartInkPaperDansityChart.kendoChart({
        renderAs: "canvas",
        //title: {
        //    text: "ΔE2000 Primary Patches"
        //},
        seriesDefaults: {
            type: "column",
            stack: true,
            width: 10,
            gap: 0.5,
            highlight: {
                toggle: function (e) {
                    // Don't create a highlight overlay,
                    // we'll modify the existing visual instead
                    e.preventDefault();

                    var visual = e.visual;
                    var opacity = e.show ? 0.8 : 1;

                    visual.opacity(opacity);
                }
            },
            visual: function (e) {
                var visual = e.createVisual();
                var axis = e.sender.getAxis("valueAxis");

                var target = axis.slot(e.dataItem.densityLimit).origin.y;
                var path = new kendo.drawing.Path({ stroke: { width: 1, color: "red" } }).moveTo(e.rect.origin.x, target).lineTo(e.rect.topRight().x, target);
                visual.append(path);

                var circleGeometry = new kendo.geometry.Circle([(e.rect.origin.x + (e.rect.size.width / 2)), target], 3);
                var circleDensity = new kendo.drawing.Circle(circleGeometry).fill("red", 1);
                circleDensity.options.stroke.color = "red";
                visual.append(circleDensity);

                if (($hdnCurrentDlpSubOperation == ELicenseType.Sync1D || $hdnCurrentDlpSubOperation == ELicenseType.Sync1DMultiColor || $hdnCurrentDlpSubOperation == ELicenseType.DLPPlus) && e.dataItem.preAvgDensity > 0) {
                    var targetAvg = axis.slot(e.dataItem.preAvgDensity).origin.y;
                    var pathAvg = new kendo.drawing.Path({ stroke: { width: 1, color: "#34E61E" } }).moveTo(e.rect.origin.x, targetAvg).lineTo(e.rect.topRight().x, targetAvg);
                    visual.append(pathAvg);

                    var circleAvg = new kendo.geometry.Circle([(e.rect.origin.x + (e.rect.size.width / 2)), targetAvg], 3);
                    var circleDensityAvg = new kendo.drawing.Circle(circleAvg).fill("#34E61E", 1);
                    circleDensityAvg.options.stroke.color = "#34E61E";
                    visual.append(circleDensityAvg);
                }
                return visual;
            }
        },
        series: [{
            data: formattedDensityList,
            overlay: {
                gradient: "glass",
            }
        }],
        panes: [{
            clip: false
        }],
        categoryAxis: {
            name: "categoryAxis",
            categories: densityPatchColorList
        },
        valueAxis: {
            name: "valueAxis",
            //max: 20,
            line: {
                visible: false
            },
            minorGridLines: {
                visible: false
            },
            labels: {
                rotation: "auto"
            },
            majorUnit: 0.5,
        },
        tooltip: {
            visible: true,
            template: function (e) {
                var item = e.dataItem;

                tooltipString = '<table><tr><td><center><b>' + item.title + '</b></center><td></tr>';
                tooltipString += '<tr><td>' + _Resources._recActual + ":</b> " + item.density.toFixed(2) + '</td></tr>';
                tooltipString += '<tr><td>' + _Resources._recTarget + ":</b> " + item.densityLimit.toFixed(2) + '</td></tr>';
                if (($hdnCurrentDlpSubOperation == ELicenseType.Sync1D || $hdnCurrentDlpSubOperation == ELicenseType.Sync1DMultiColor || $hdnCurrentDlpSubOperation == ELicenseType.DLPPlus) && item.preAvgDensity > 0) {
                    tooltipString += '<tr><td>' + _Resources._recPrevious + ":</b> " + item.preAvgDensity.toFixed(2) + '</td></tr>';
                }
                tooltipString += '</table>';

                return tooltipString;
            }
        },
        chartArea: {
            width: 325,
            height: 150
        },
        render: renderChartWithBackgroundByCategory
    });



    var paperABGreen = {};
    paperABGreen.data = [[0, 0]];
    paperABGreen.color = "Green";
    paperABGreen.markers = { background: "Green" };
    paperABGreen.highlight = {
        visible: false
    }

    var tooltipString = "";
    var paperAB = {};
    var paperLArray = [];
    if (jsonDeltaPaperLAB_1.length > 0) {
        var paperObj = jsonDeltaPaperLAB_1[0];
        var deltaA = paperObj.Δa == 0 ? 0.01 : paperObj.Δa;
        paperAB.data = [[deltaA, paperObj.Δb]];

        tooltipString = '<table><tr><td><center><b>' + _Resources._recPaperPathMetrics + '</b></center><td></tr>';
        tooltipString += '<tr><td>' + _Resources._recActual + ' ' + _Resources._recL + ': ' + paperObj.CielabL.toFixed(2) + ' ' + _Resources._recA + ': ' + paperObj.CielabA.toFixed(2) + ' ' + _Resources._recB + ': ' + paperObj.CielabB.toFixed(2) + '</td></tr>';
        tooltipString += '<tr><td>' + _Resources._recTarget + ' ' + _Resources._recL + ': ' + paperObj.TargetL.toFixed(2) + ' ' + _Resources._recA + ': ' + paperObj.Targeta.toFixed(2) + ' ' + _Resources._recB + ': ' + paperObj.Targetb.toFixed(2) + '</td></tr>';
        tooltipString += '<tr><td>' + _Resources._recDeltaL + ':   ' + paperObj["ΔL"].toFixed(2) + '</td></tr>';
        tooltipString += '<tr><td>' + _Resources._recDeltaA + ':   ' + paperObj["Δa"].toFixed(2) + '</td></tr>';
        tooltipString += '<tr><td>' + _Resources._recDeltaB + ':   ' + paperObj["Δb"].toFixed(2) + '</td></tr>';
        tooltipString += '</table>';


        //bind json data for L chart
        var paperLObject = {
            //color: '#D4CBC5',
            color: '#FFF',
            //name: patchColor[index].colorName,
            value: paperObj["ΔL"],
        };
        paperLArray.push(paperLObject);
    }

    paperAB.color = "White";
    paperAB.markers = {
        background: "White",
        border: {
            width: 1,
            color: function (e) {
                return 'black';
            }
        }
    };
    paperAB.width = 2;
    paperAB.highlight = {
        visible: false
    }
    paperAB.tooltip = { visible: true, template: tooltipString, shared: true };


    $divChartInkPaperPaperLab1.kendoChart({
        renderAs: "canvas",
        seriesDefaults: {
            type: "scatterLine",
            style: "smooth"
        },
        series: [
            paperABGreen,
            paperAB
        ],
        xAxis: {
            name: "xAxis",
            min: -10,
            max: 10,
            majorUnit: 5,
            axisCrossingValues: [-10, 10],
            //labels: {
            //    format: "{0}"
            //},
            labels: {
                //step: 2,
                template: function (e) {
                    if (e.value == 0)
                        return 'Target';
                    return e.value;
                }
            },
            title: {
                //text: _Resources._recDeltaA
            },
            majorGridLines: {
                visible: true,
                step: -10
            }
        },
        yAxis: {
            name: "yAxis",
            min: -10,
            max: 10,
            step: 5,
            majorUnit: 5,
            axisCrossingValues: [-10, 10],
            //labels: {
            //    format: "{0}"
            //},
            labels: {
                //step: 2,
                template: function (e) {
                    if (e.value == 0)
                        return 'Target';
                    return e.value;
                },
                rotation: 270
            },
            title: {
                //text: _Resources._recDeltaB
            }, majorGridLines: {
                visible: true,
                step: -10
            }
        },
        chartArea: {
            width: 250,
            height: 180,
            background: ''
        },
        plotArea: {
            background: "white",
            margin: {
                top: 12,
                right: 12
            }
        },
        render: renderChartWithBackgroundByAxis
        //plotArea: {
        //    background: "lightgray"
        //}
    });


    $divChartInkPaperPaperLab2.kendoChart({
        renderAs: "canvas",
        seriesDefaults: {
            type: "column",
            width: 25,
            //gap: 2,
        },
        series: [{
            data: paperLArray,
        }],
        categoryAxis: {
            name: "categoryAxis",
            categories: [],
            line: {
                visible: true,
                width: 2,
            },
        },
        valueAxis: {
            name: "valueAxis",
            labels: {
                format: "{0}"
            },
            title: {
                //text: _Resources._recDeltaL
            },
            line: {
                visible: false,
            },
            axisCrossingValue: 0,
            min: -10,
            max: 10,
            majorUnit: 5,
        },
        tooltip: {
            visible: true,
            template: tooltipString
            //template: "#= dataItem.title # <br/> " + _Resources._recActual + " " + _Resources._recL + ": #= dataItem.CielabL # " + _Resources._recA + ": #= dataItem.CielabA # " + _Resources._recB + ": #= dataItem.CielabB # <br/>" + _Resources._recTarget + " " + _Resources._recL + ": #= dataItem.TargetL # " + _Resources._recA + ": #= dataItem.Targeta # " + _Resources._recB + ": #= dataItem.Targetb # <br/>" + _Resources._recDeltaE + ":  #= value #"
        },
        chartArea: {
            width: 100,
            height: 180
        },
        render: renderChartWithBackgroundByCategoryForPaperL
    });

}

////**************** START BIND THE AB COMPARISION (GMAUT) CHART  ****************////
function PopulateChartTargetVersusActualAB() {


    //************* Get object from array ***************//
    var paperObj = $.grep(jsonTarget, function (element) { return element.PatchType == "Paper"; })[0];

    //************* Target Objects ***************//
    var targetArray = {};
    var targetCYAN = {};
    var targetMAGENTA = {};
    var targetYELLOW = {};
    var targetRED = {};
    var targetGREEN = {};
    var targetBLUE = {};
    var targetPAPERObj = [paperObj.Targeta, paperObj.Targetb];
    var targetPAPER = [targetPAPERObj];

    //************* SERIES ARRAY ***************//

    var targetCYANSeriseObj = {};
    var targetMAGENTASeriseObj = {};
    var targetYELLOWSeriseObj = {};
    var targetREDSeriseObj = {};
    var targetGREENSeriseObj = {};
    var targetBLUESeriseObj = {};
    var targetPAPERSeriseObj = {};

    $.each(jsonTarget, function (index, element) {
        var seriesArray = [];

        //Get colors from the colors array ()
        var colorsArray = $.grep(jsonColors, function (colorObj) { return colorObj.PatchType == element.PatchType });
        if (colorsArray.length > 0) {
            seriesArray.push(targetPAPERObj);
            $.each(colorsArray, function (i, colorObj) {
                var dots = [colorObj.CielabA, colorObj.CielabB];
                seriesArray.push(dots);
            });
        }

        //add actuall color object
        var actualObject = [element.CielabA, element.CielabB];
        seriesArray.push(actualObject);

        //create the series of the array for line
        var kendoSeriseObj = {};
        kendoSeriseObj.data = seriesArray;
        kendoSeriseObj.color = element.PatchType;
        kendoSeriseObj.markers = {
            background: element.PatchType,
            //size: 6,
            border: {
                width: 1,
                color: function (e) {
                    return 'black';
                }
            }
        };
        kendoSeriseObj.width = 2;
        kendoSeriseObj.highlight = {
            visible: false
        }

        //Check patch type for tooltip caption
        var tooltipCaption = '';
        switch (element.PatchType) {
            case "Cyan":
                tooltipCaption = _Resources._recCyanPathMetrics;
                break;
            case "Magenta":
                tooltipCaption = _Resources._recMagentaPathMetrics;
                break;
            case "Yellow":
                tooltipCaption = _Resources._recYellowPathMetrics;
                break;
            case "Red":
                tooltipCaption = _Resources._recRedPathMetrics;
                break;
            case "Green":
                tooltipCaption = _Resources._recGreenPathMetrics;
                break;
            case "Blue":
                tooltipCaption = _Resources._recBluePathMetrics;
                break;
            case "Paper":
                tooltipCaption = _Resources._recPaperPathMetrics;
                break;
        }


        //create tool tip

        var bgColor = element.PatchType == "Paper" ? "White" : element.PatchType;
        var textColor = "white";
        if (element.PatchType == "Paper" || element.PatchType == "Yellow" || element.PatchType == "Cyan") {
            textColor = "black";
        }
        var tooltipString = '<div style="color: ' + textColor + '; padding: 2px; background-color: ' + bgColor + ';border-radius: 4px;padding: 5px 5px 5px 6px;background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABQAAAAWCAYAAADAQbwGAAAAGXRFW…8beQb+HTWQYgP/DHoD/466cAR4edRAyg38P6hLbIAAAwCnWhhVsxvdCAAAAABJRU5ErkJggg==);"><table><tr><td><center><b>' + tooltipCaption + '</b></center><td></tr>';
        tooltipString += '<tr><td>' + _Resources._recActual + ' ' + _Resources._recL + ': ' + element.CielabL.toFixed(2) + ' ' + _Resources._recA + ': ' + element.CielabA.toFixed(2) + ' ' + _Resources._recB + ': ' + element.CielabB.toFixed(2) + '</td></tr>';
        if ($hdnCurrentDlpSubOperation != ELicenseType.DestinationProfile) {
            tooltipString += '<tr><td>' + _Resources._recTarget + ' ' + _Resources._recL + ': ' + element.TargetL.toFixed(2) + ' ' + _Resources._recA + ': ' + element.Targeta.toFixed(2) + ' ' + _Resources._recB + ': ' + element.Targetb.toFixed(2) + '</td></tr>';
            tooltipString += '<tr><td>' + _Resources._recDeltaL + '   ' + element["ΔL"].toFixed(2) + '</td></tr>';
            tooltipString += '<tr><td>' + _Resources._recDeltaA + '   ' + element["Δa"].toFixed(2) + '</td></tr>';
            tooltipString += '<tr><td>' + _Resources._recDeltaB + '   ' + element["Δb"].toFixed(2) + '</td></tr>';
        }
        tooltipString += '</table></div>';

        kendoSeriseObj.tooltip = {
            visible: true, template: function (d) {

                var seriesData = d.series.data;
                var lastItem = seriesData[seriesData.length - 1];

                if (lastItem != d.dataItem) {
                    setTimeout(function () {
                        $(".k-chart-tooltip").addClass('tempTooltip');
                    }, 150);
                    return "";
                } else {
                    setTimeout(function () {
                        $(".k-chart-tooltip").removeData('tempTooltip');
                    }, 150);
                }
                return tooltipString;
            }, shared: false
        };


        //Calcaulte Targets
        var kendoTargetObj = {};
        kendoTargetObj.data = [[element.Targeta, element.Targetb]];
        kendoTargetObj.color = "Green";
        kendoTargetObj.markers = { background: "Green" };
        kendoTargetObj.highlight = {
            visible: false
        }

        $('#tootipDemo').html(tooltipString);
        if (element.PatchType == "Cyan") {
            targetCYANSeriseObj = kendoSeriseObj;
            targetCYAN = kendoTargetObj;
        }
        if (element.PatchType == "Magenta") {
            targetMAGENTASeriseObj = kendoSeriseObj;
            targetMAGENTA = kendoTargetObj;

        }
        if (element.PatchType == "Yellow") {
            targetYELLOWSeriseObj = kendoSeriseObj;
            targetYELLOW = kendoTargetObj;
        }
        if (element.PatchType == "Red") {
            targetREDSeriseObj = kendoSeriseObj;
            targetRED = kendoTargetObj;
        }
        if (element.PatchType == "Green") {
            targetGREENSeriseObj = kendoSeriseObj;
            targetGREEN = kendoTargetObj;
        }
        if (element.PatchType == "Blue") {
            targetBLUESeriseObj = kendoSeriseObj;
            targetBLUE = kendoTargetObj;
        }
        if (element.PatchType == "Paper") {
            kendoSeriseObj.data = [targetPAPERObj];
            kendoSeriseObj.color = "White";
            kendoSeriseObj.markers = { background: "White", size: 6 };
            targetPAPERSeriseObj = kendoSeriseObj;
        }
    });


    $chart_TargetVersusActualAB.kendoChart({
        renderAs: "canvas",
        seriesDefaults: {
            type: "scatterLine",
            style: "smooth"
        },
        //title: {
        //    text: "a* - b* Comparison"
        //},
        series: [
            targetCYAN,
            targetMAGENTA,
            targetYELLOW,
            targetRED,
            targetGREEN,
            targetBLUE,
            targetCYANSeriseObj,
            targetMAGENTASeriseObj,
            targetYELLOWSeriseObj,
            targetREDSeriseObj,
            targetGREENSeriseObj,
            targetBLUESeriseObj,
            targetPAPERSeriseObj
        ],
        xAxis: {
            name: "xAxis",
            min: -100,
            max: 100,
            majorUnit: 20,
            axisCrossingValues: [-100, 100],
            labels: {
                format: "{0}"
            },
            title: {
                text: ""
            }
        },
        yAxis: {
            name: "yAxis",
            min: -100,
            max: 100,
            step: 20,
            majorUnit: 20,
            axisCrossingValues: [-100, 100],
            labels: {
                format: "{0}"
            },
            title: {
                text: ""
            }
        },
        chartArea: {
            width: 300,
            height: 250,
            background: ''
        },
    });



}


////**************** START BIND THE AB COMPARISION (GMAUT) CHART MULTICOLOR  ****************////
function PopulateChartTargetVersusActualABMultiColor() {


    //************* Get object from array ***************//
    var paperObj = $.grep(jsonTarget, function (element) { return element.PatchType == "Paper"; })[0];

    //************* Target Objects ***************//
    var targetArray = {};
    var targetCYAN = {};
    var targetMAGENTA = {};
    var targetYELLOW = {};
    var targetRED = {};
    var targetGREEN = {};
    var targetBLUE = {};
    var targetPAPERObj = [paperObj.Targeta, paperObj.Targetb];
    var targetPAPER = [targetPAPERObj];

    //************* SERIES ARRAY ***************//

    var targetCYANSeriseObj = {};
    var targetMAGENTASeriseObj = {};
    var targetYELLOWSeriseObj = {};
    var targetREDSeriseObj = {};
    var targetGREENSeriseObj = {};
    var targetBLUESeriseObj = {};
    var targetPAPERSeriseObj = {};

    $.each(jsonTarget, function (index, element) {
        var seriesArray = [];

        //Get colors from the colors array ()
        var colorsArray = $.grep(jsonColors, function (colorObj) { return colorObj.PatchType == element.PatchType });
        if (colorsArray.length > 0) {
            seriesArray.push(targetPAPERObj);
            $.each(colorsArray, function (i, colorObj) {
                var dots = [colorObj.CielabA, colorObj.CielabB];
                seriesArray.push(dots);
            });
        }

        //add actuall color object
        var actualObject = [element.CielabA, element.CielabB];
        seriesArray.push(actualObject);

        //create the series of the array for line

        var kendoSeriseObj = {};
        kendoSeriseObj.data = seriesArray;
        kendoSeriseObj.color = element.PatchType;
        kendoSeriseObj.markers = {
            background: element.PatchType,
            //size: 6,
            border: {
                width: 1,
                color: function (e) {
                    return 'black';
                }
            }
        };
        kendoSeriseObj.width = 2;
        kendoSeriseObj.highlight = {
            visible: false
        }

        //Check patch type for tooltip caption
        var tooltipCaption = '';
        switch (element.PatchType) {
            case "Cyan":
                tooltipCaption = _Resources._recCyanPathMetrics;
                break;
            case "Magenta":
                tooltipCaption = _Resources._recMagentaPathMetrics;
                break;
            case "Yellow":
                tooltipCaption = _Resources._recYellowPathMetrics;
                break;
            case "Red":
                tooltipCaption = _Resources._recRedPathMetrics;
                break;
            case "Green":
                tooltipCaption = _Resources._recGreenPathMetrics;
                break;
            case "Blue":
                tooltipCaption = _Resources._recBluePathMetrics;
                break;
            case "Paper":
                tooltipCaption = _Resources._recPaperPathMetrics;
                break;
        }


        //create tool tip

        var bgColor = element.PatchType == "Paper" ? "White" : element.PatchType;
        var textColor = "white";
        if (element.PatchType == "Paper" || element.PatchType == "Yellow" || element.PatchType == "Cyan") {
            textColor = "black";
        }
        var tooltipString = '<div style="color: ' + textColor + '; padding: 2px; background-color: ' + bgColor + ';border-radius: 4px;padding: 5px 5px 5px 6px;background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABQAAAAWCAYAAADAQbwGAAAAGXRFW…8beQb+HTWQYgP/DHoD/466cAR4edRAyg38P6hLbIAAAwCnWhhVsxvdCAAAAABJRU5ErkJggg==);"><table><tr><td><center><b>' + tooltipCaption + '</b></center><td></tr>';
        tooltipString += '<tr><td>' + _Resources._recActual + ' ' + _Resources._recL + ': ' + element.CielabL.toFixed(2) + ' ' + _Resources._recA + ': ' + element.CielabA.toFixed(2) + ' ' + _Resources._recB + ': ' + element.CielabB.toFixed(2) + '</td></tr>';
        if ($hdnCurrentDlpSubOperation != ELicenseType.DestinationProfile) {
            tooltipString += '<tr><td>' + _Resources._recTarget + ' ' + _Resources._recL + ': ' + element.TargetL.toFixed(2) + ' ' + _Resources._recA + ': ' + element.Targeta.toFixed(2) + ' ' + _Resources._recB + ': ' + element.Targetb.toFixed(2) + '</td></tr>';
            tooltipString += '<tr><td>' + _Resources._recDeltaL + '   ' + element["ΔL"].toFixed(2) + '</td></tr>';
            tooltipString += '<tr><td>' + _Resources._recDeltaA + '   ' + element["Δa"].toFixed(2) + '</td></tr>';
            tooltipString += '<tr><td>' + _Resources._recDeltaB + '   ' + element["Δb"].toFixed(2) + '</td></tr>';
        }
        tooltipString += '</table></div>';

        kendoSeriseObj.tooltip = {
            visible: true, template: function (d) {
                var seriesData = d.series.data;
                var lastItem = seriesData[seriesData.length - 1];

                if (lastItem != d.dataItem) {
                    setTimeout(function () {
                        $(".k-chart-tooltip").addClass('tempTooltip');
                    }, 150);
                    return "";
                } else {
                    setTimeout(function () {
                        $(".k-chart-tooltip").removeData('tempTooltip');
                    }, 150);
                }
                return tooltipString;
            }, shared: false
        };


        //Calcaulte Targets
        var kendoTargetObj = {};
        kendoTargetObj.data = [[element.Targeta, element.Targetb]];
        kendoTargetObj.color = "Green";
        kendoTargetObj.markers = { background: "Green" };
        kendoTargetObj.highlight = {
            visible: false
        }


        $('#tootipDemo').html(tooltipString);
        if (element.PatchType == "Cyan") {
            targetCYANSeriseObj = kendoSeriseObj;
            targetCYAN = kendoTargetObj;
        }
        if (element.PatchType == "Magenta") {
            targetMAGENTASeriseObj = kendoSeriseObj;
            targetMAGENTA = kendoTargetObj;

        }
        if (element.PatchType == "Yellow") {
            targetYELLOWSeriseObj = kendoSeriseObj;
            targetYELLOW = kendoTargetObj;
        }
        if (element.PatchType == "Red") {
            targetREDSeriseObj = kendoSeriseObj;
            targetRED = kendoTargetObj;
        }
        if (element.PatchType == "Green") {
            targetGREENSeriseObj = kendoSeriseObj;
            targetGREEN = kendoTargetObj;
        }
        if (element.PatchType == "Blue") {
            targetBLUESeriseObj = kendoSeriseObj;
            targetBLUE = kendoTargetObj;
        }
        if (element.PatchType == "Paper") {
            kendoSeriseObj.data = [targetPAPERObj];
            kendoSeriseObj.color = "White";
            kendoSeriseObj.markers = { background: "White", size: 6 };
            targetPAPERSeriseObj = kendoSeriseObj;
        }
    });


    $chart_TargetVersusActualAB.kendoChart({
        renderAs: "canvas",
        seriesDefaults: {
            type: "scatterLine",
            style: "smooth"
        },
        //title: {
        //    text: "a* - b* Comparison"
        //},
        series: [
            targetCYAN,
            targetMAGENTA,
            targetYELLOW,
            targetRED,
            targetGREEN,
            targetBLUE,
            targetCYANSeriseObj,
            targetMAGENTASeriseObj,
            targetYELLOWSeriseObj,
            targetREDSeriseObj,
            targetGREENSeriseObj,
            targetBLUESeriseObj,
            targetPAPERSeriseObj
        ],
        xAxis: {
            name: "xAxis",
            min: -100,
            max: 100,
            majorUnit: 20,
            axisCrossingValues: [-100, 100],
            labels: {
                format: "{0}"
            },
            title: {
                text: ""
            }
        },
        yAxis: {
            name: "yAxis",
            min: -100,
            max: 100,
            step: 20,
            majorUnit: 20,
            axisCrossingValues: [-100, 100],
            labels: {
                format: "{0}"
            },
            title: {
                text: ""
            }
        },
        chartArea: {
            width: 300,
            height: 250,
            background: ''
        },
    });



}

////**************** START BIND THE AB COMPARISION GAMUT CHART  ****************////
function PopulateChartGamutABComparision() {

    var targetCYANPoint = {};
    var targetMAGENTAPoint = {};
    var targetYELLOWPoint = {};
    var targetREDPoint = {};
    var targetGREENPoint = {};
    var targetBLUEPoint = {};

    var arrTargetsABComparision = [];
    var arrActualABComparision = [];
    var colorOrderArray = ["Cyan", "Green", "Yellow", "Red", "Magenta", "Blue", "Cyan"]

    $.each(colorOrderArray, function (index, colorObj) {

        //set target points
        var jsonTargetColor = $.grep(jsonTarget, function (targetObj) { return targetObj.PatchType == colorObj })[0];
        var objTarget = [jsonTargetColor.Targeta, jsonTargetColor.Targetb];
        arrTargetsABComparision.push(objTarget);

        //set actual point black
        var objActual = [jsonTargetColor.CielabA, jsonTargetColor.CielabB];
        arrActualABComparision.push(objActual);

        //set actual color points
        var actualColorPointObj = {};
        actualColorPointObj.data = [objActual];
        actualColorPointObj.color = jsonTargetColor.PatchType;
        actualColorPointObj.markers = {
            background: jsonTargetColor.PatchType, size: 10,
            border: {
                width: 1,
                color: function (e) {
                    return 'black';
                }
            }
        };

        //Check patch type for tooltip caption
        var tooltipCaption = '';
        switch (jsonTargetColor.PatchType) {
            case "Cyan":
                tooltipCaption = _Resources._recCyanPathMetrics;
                break;
            case "Magenta":
                tooltipCaption = _Resources._recMagentaPathMetrics;
                break;
            case "Yellow":
                tooltipCaption = _Resources._recYellowPathMetrics;
                break;
            case "Red":
                tooltipCaption = _Resources._recRedPathMetrics;
                break;
            case "Green":
                tooltipCaption = _Resources._recGreenPathMetrics;
                break;
            case "Blue":
                tooltipCaption = _Resources._recBluePathMetrics;
                break;
            case "Paper":
                tooltipCaption = _Resources._recPaperPathMetrics;
                break;
        }

        //create tool tip
        var tooltipString = '<table><tr><td><center><b>' + tooltipCaption + '</b></center><td></tr>';
        tooltipString += '<tr><td>' + _Resources._recActual + ' ' + _Resources._recL + ': ' + jsonTargetColor.CielabL.toFixed(2) + ' ' + _Resources._recA + ': ' + jsonTargetColor.CielabA.toFixed(2) + ' ' + _Resources._recB + ': ' + jsonTargetColor.CielabB.toFixed(2) + '</td></tr>';

        if ($hdnCurrentDlpSubOperation != ELicenseType.DestinationProfile) {
            tooltipString += '<tr><td>' + _Resources._recTarget + ' ' + _Resources._recL + ': ' + jsonTargetColor.TargetL.toFixed(2) + ' ' + _Resources._recA + ': ' + jsonTargetColor.Targeta.toFixed(2) + ' ' + _Resources._recB + ': ' + jsonTargetColor.Targetb.toFixed(2) + '</td></tr>';
            tooltipString += '<tr><td>' + _Resources._recDeltaL + '   ' + jsonTargetColor["ΔL"].toFixed(2) + '</td></tr>';
            tooltipString += '<tr><td>' + _Resources._recDeltaA + '   ' + jsonTargetColor["Δa"].toFixed(2) + '</td></tr>';
            tooltipString += '<tr><td>' + _Resources._recDeltaB + '   ' + jsonTargetColor["Δb"].toFixed(2) + '</td></tr>';
        }
        tooltipString += '</table>';

        actualColorPointObj.tooltip = {
            visible: true,
            template: tooltipString,
            shared: true
        };

        actualColorPointObj.highlight = {
            visible: false
        }
        actualColorPointObj.width = 2;

        if (colorObj == "Cyan") {
            targetCYANPoint = actualColorPointObj;
        }
        if (colorObj == "Green") {
            targetGREENPoint = actualColorPointObj;
        }
        if (colorObj == "Yellow") {
            targetYELLOWPoint = actualColorPointObj;
        }
        if (colorObj == "Red") {
            targetREDPoint = actualColorPointObj;
        }
        if (colorObj == "Magenta") {
            targetMAGENTAPoint = actualColorPointObj;
        }
        if (colorObj == "Blue") {
            targetBLUEPoint = actualColorPointObj;
        }
    });
    //set the paper objects
    var paperObj = $.grep(jsonTarget, function (element) { return element.PatchType == "Paper"; })[0];
    var targetPAPERObject = [[paperObj.Targeta, paperObj.Targetb]];
    var colorPAPERObject = [[paperObj.CielabA, paperObj.CielabB]];
    //set actual color points
    var paperColorPointObj = {};
    paperColorPointObj.data = colorPAPERObject;
    paperColorPointObj.color = "White";
    paperColorPointObj.markers = {
        background: "White", size: 10,
        border: {
            width: 1,
            color: function (e) {
                return 'black';
            }
        }
    };


    //create tool tip
    var tooltipString = '<table><tr><td><center><b>' + _Resources._recPaperPathMetrics + '</b></center><td></tr>';
    tooltipString += '<tr><td>' + _Resources._recActual + ' ' + _Resources._recL + ': ' + paperObj.CielabL.toFixed(2) + ' ' + _Resources._recA + ': ' + paperObj.CielabA.toFixed(2) + ' ' + _Resources._recB + ': ' + paperObj.CielabB.toFixed(2) + '</td></tr>';
    if ($hdnCurrentDlpSubOperation != ELicenseType.DestinationProfile) {
        tooltipString += '<tr><td>' + _Resources._recTarget + ' ' + _Resources._recL + ': ' + paperObj.TargetL.toFixed(2) + ' ' + _Resources._recA + ': ' + paperObj.Targeta.toFixed(2) + ' ' + _Resources._recB + ': ' + paperObj.Targetb.toFixed(2) + '</td></tr>';
        tooltipString += '<tr><td>' + _Resources._recDeltaL + '   ' + paperObj["ΔL"].toFixed(2) + '</td></tr>';
        tooltipString += '<tr><td>' + _Resources._recDeltaA + '   ' + paperObj["Δa"].toFixed(2) + '</td></tr>';
        tooltipString += '<tr><td>' + _Resources._recDeltaB + '   ' + paperObj["Δb"].toFixed(2) + '</td></tr>';
    }
    tooltipString += '</table>';

    paperColorPointObj.tooltip = {
        visible: true,
        template: tooltipString,
        shared: true
    };

    paperColorPointObj.highlight = {
        visible: false
    }
    paperColorPointObj.width = 2;


    $chart_Gamut_ABComparison.kendoChart({
        renderAs: "canvas",
        seriesDefaults: {
            type: "scatterLine"
        },
        //title: {
        //    text: "a* - b* Gamut Comparison"
        //},
        series: [{
            data: arrTargetsABComparision,
            color: "Green",
            markers: { background: "Green" },
            highlight: {
                visible: false
            }
        }, {
            data: arrActualABComparision,
            color: "Black",
            markers: { background: "Black", visible: false },
            highlight: {
                visible: false
            }
        }, {
            data: targetPAPERObject,
            color: "Green",
            markers: { background: "Green", size: 10 },
            highlight: {
                visible: false
            }
        },
            paperColorPointObj,
            targetCYANPoint,
            targetMAGENTAPoint,
            targetYELLOWPoint,
            targetREDPoint,
            targetGREENPoint,
            targetBLUEPoint
        ],
        xAxis: {
            name: "xAxis",
            min: -100,
            max: 100,
            majorUnit: 20,
            axisCrossingValues: [-100, 100],
            labels: {
                format: "{0}"
            },
            title: {
                text: ""
            }
        },
        yAxis: {
            name: "yAxis",
            min: -100,
            max: 100,
            step: 20,
            majorUnit: 20,
            axisCrossingValues: [-100, 100],
            labels: {
                format: "{0}"
            },
        },
        chartArea: {
            width: 300,
            height: 250,
            background: ''
        },
    });
}

////**************** START BIND THE L COMPARISION  (GMAUT) CHART  ****************////
function PopulateChartLComparision() {

    var cyanLComparisionPoint = {};
    var yellowLComparisionPoint = {};
    var blueLComparisionPoint = {};
    var redLComparisionPoint = {};
    var magentaLComparisionPoint = {};
    var greenLComparisionPoint = {};
    var paperLComparisionPoint = {};

    var cyanLComparisionPointSmall = {};
    var yellowLComparisionPointSmall = {};
    var blueLComparisionPointSmall = {};
    var redLComparisionPointSmall = {};
    var magentaLComparisionPointSmall = {};
    var greenLComparisionPointSmall = {};
    var paperLComparisionPointSmall = {};

    $.each(jsonTarget, function (index, element) {

        var pointsObj = {};
        pointsObj.data = [[element.CielabA, 0]];
        pointsObj.color = element.PatchType;
        pointsObj.markers = { background: element.PatchType, size: 10 };
        pointsObj.highlight = {
            visible: false
        }


        var pointsObjSmall = {};
        pointsObjSmall.data = [[element.CielabA, element["ΔL"]]];
        pointsObjSmall.color = element.PatchType;
        pointsObjSmall.markers = {
            background: element.PatchType, size: 6,
            border: {
                width: 0.25,
                color: function (e) {
                    return 'black';
                }
            }
        };
        pointsObjSmall.highlight = {
            visible: false
        }

        //Check patch type for tooltip caption
        var tooltipCaption = '';
        switch (element.PatchType) {
            case "Cyan":
                tooltipCaption = _Resources._recCyanPathMetrics;
                break;
            case "Magenta":
                tooltipCaption = _Resources._recMagentaPathMetrics;
                break;
            case "Yellow":
                tooltipCaption = _Resources._recYellowPathMetrics;
                break;
            case "Red":
                tooltipCaption = _Resources._recRedPathMetrics;
                break;
            case "Green":
                tooltipCaption = _Resources._recGreenPathMetrics;
                break;
            case "Blue":
                tooltipCaption = _Resources._recBluePathMetrics;
                break;
            case "Paper":
                tooltipCaption = _Resources._recPaperPathMetrics;
                break;
        }

        var tooltipString = '<table><tr><td><center><b>' + tooltipCaption + '</b></center><td></tr>';
        tooltipString += '<tr><td>' + _Resources._recActual + ' ' + _Resources._recL + ': ' + element.CielabL + ' ' + _Resources._recA + ': ' + element.CielabA + ' ' + _Resources._recB + ': ' + element.CielabB + '</td></tr>';

        if ($hdnCurrentDlpSubOperation != ELicenseType.DestinationProfile) {
            tooltipString += '<tr><td>' + _Resources._recTarget + ' ' + _Resources._recL + ': ' + element.TargetL + ' ' + _Resources._recA + ': ' + element.Targeta + ' ' + _Resources._recB + ': ' + element.Targetb + '</td></tr>';
            tooltipString += '<tr><td>' + _Resources._recDeltaL + '   ' + element["ΔL"] + '</td></tr>';
            tooltipString += '<tr><td>' + _Resources._recDeltaA + '   ' + element["Δa"] + '</td></tr>';
            tooltipString += '<tr><td>' + _Resources._recDeltaB + '   ' + element["Δb"] + '</td></tr>';
        }
        tooltipString += '</table>';

        pointsObj.tooltip = { visible: true, template: tooltipString };
        //pointsObjSmall.tooltip = { visible: true, template: tooltipString };

        if (element.PatchType == "Cyan") {
            cyanLComparisionPoint = pointsObj;
            cyanLComparisionPointSmall = pointsObjSmall;
        }
        if (element.PatchType == "Magenta") {
            magentaLComparisionPoint = pointsObj;
            magentaLComparisionPointSmall = pointsObjSmall;
        }
        if (element.PatchType == "Yellow") {
            yellowLComparisionPoint = pointsObj;
            yellowLComparisionPointSmall = pointsObjSmall;
        }
        if (element.PatchType == "Red") {
            redLComparisionPoint = pointsObj;
            redLComparisionPointSmall = pointsObjSmall;
        }
        if (element.PatchType == "Green") {
            greenLComparisionPoint = pointsObj;
            greenLComparisionPointSmall = pointsObjSmall;
        }
        if (element.PatchType == "Blue") {
            blueLComparisionPoint = pointsObj;
            blueLComparisionPointSmall = pointsObjSmall;
        }
        if (element.PatchType == "Paper") {
            paperLComparisionPoint = pointsObj;
            paperLComparisionPoint.color = "White";

            paperLComparisionPointSmall = pointsObjSmall;
            paperLComparisionPointSmall.color = "White";
            paperLComparisionPointSmall.markers.background = "White";
        }

    });

    $chart_L_Comaparision.kendoChart({
        renderAs: "canvas",
        seriesDefaults: {
            type: "scatter",
        },
        //title: {
        //    text: "L* Comparison"
        //},
        series: [
            cyanLComparisionPoint
            , magentaLComparisionPoint
            , yellowLComparisionPoint
            , redLComparisionPoint
            , greenLComparisionPoint
            , blueLComparisionPoint
            , paperLComparisionPoint
            , cyanLComparisionPointSmall
            , magentaLComparisionPointSmall
            , yellowLComparisionPointSmall
            , redLComparisionPointSmall
            , greenLComparisionPointSmall
            , blueLComparisionPointSmall
            , paperLComparisionPointSmall
        ],
        xAxis: {
            name: "xAxis",
            min: -100,
            max: 100,
            majorUnit: 20,
            axisCrossingValues: [-100, 100],
            labels: {
                format: "{0}"
            },
            title: {
                text: ""
            }
        },
        yAxis: {
            name: "yAxis",
            min: -15,
            max: 15,
            majorUnit: 15,
            axisCrossingValues: [-15, 15],
            labels: {
                format: "{0}"
            },
            title: {
                text: ""
            }
        },
        chartArea: {
            width: 300,
            height: 80
        },
        plotArea: {
            border: {
                width: 2,
                color: "black"
            }
        },
        render: renderChartWithBackgroundByAxis
    });
}

////**************** START BIND THE GRAY BALANCE L*  (GMAUT) CHART  ****************////
function PopulateChartLGrayBalance() {

    // if project is destination profile then display actual lab value in tooltip .
    var tooltipString = '<table><tr><td><center><b>' + '#= kendo.toString(dataItem.CmykC,"n0") #' + _Resources._recgbPatchMetrics + '</b></center><td></tr>';
    tooltipString += '<tr><td>' + _Resources._recActual + ' ' + _Resources._recL + ' : #= kendo.toString(dataItem.CielabL,"n") # ' + _Resources._recA + ': #= kendo.toString(dataItem.CielabA,"n")# ' + _Resources._recB + ' #= kendo.toString(dataItem.CielabB,"n")#</td></tr>';

    if ($hdnCurrentDlpSubOperation != ELicenseType.DestinationProfile) {
        tooltipString += '<tr><td>' + _Resources._recActual + ' ' + _Resources._recL + ' : #= kendo.toString(dataItem.CielabL,"n") # ' + _Resources._recA + ': #= kendo.toString(dataItem.CielabA,"n")# ' + _Resources._recB + ' #= kendo.toString(dataItem.CielabB,"n")#</td></tr>';
        tooltipString += '<tr><td>' + _Resources._recDeltaL + '   #=kendo.toString(dataItem.ΔL,"n")#</td></tr>';
        tooltipString += '<tr><td>' + _Resources._recDeltaA + '   #=kendo.toString(dataItem.Δa,"n")#</td></tr>';
        tooltipString += '<tr><td>' + _Resources._recDeltaB + '   #=kendo.toString(dataItem.Δb,"n")#</td></tr>';
    }
    tooltipString += '</table>';

    $chart_L_BalanceChart.kendoChart({
        renderAs: "canvas",
        seriesDefaults: {
            type: "scatterLine",
            style: "smooth"
        },
        dataSource: jsonGray,
        //title: {
        //    text: _Resources._recLBalanceChartTitle,
        //    font: "13px Tahoma"
        //},
        series: [{
            xField: "CmykC",
            yField: "TargetL",
            color: "Green",
            markers: { background: "Green", size: 4 },
            highlight: {
                visible: false
            }
        }, {
            xField: "CmykC",
            yField: "CielabL",
            color: "Black",
            markers: { background: "Black", size: 4 },
            tooltip: {
                visible: true,
                template: tooltipString
            }
        }],
        xAxis: {
            name: "xAxis",
            min: 0,
            max: 100,
            majorUnit: 25,
            axisCrossingValues: [0, 100],
            labels: {
                format: "{0}"
            },
            title: {
                text: ""
            }
        },
        yAxis: {
            name: "yAxis",
            min: 0,
            max: 100,
            step: 25,
            majorUnit: 25,
            axisCrossingValues: [0, 100],
            labels: {
                format: "{0}"
            },
        },
        chartArea: {
            width: 350,
            height: 150//180
        },
        plotArea: {
            border: {
                width: 2,
                color: "black"
            }
        },
        render: renderChartWithBackgroundByAxis
    });

    $chart_A_BalanceChart.kendoChart({
        renderAs: "canvas",
        seriesDefaults: {
            type: "scatterLine",
            style: "smooth"
        },
        dataSource: jsonGray,
        //title: {
        //    text: _Resources._recABalanceChartTitle,
        //    font: "13px Tahoma"
        //},
        series: [{
            xField: "CmykC",
            yField: "Targeta",
            color: "Green",
            markers: { background: "Green", size: 4 },
            highlight: {
                visible: false
            }
        }, {
            xField: "CmykC",
            yField: "CielabA",
            color: "Black",
            markers: { background: "Black", size: 4 },
            tooltip: {
                visible: true,
                template: tooltipString
            }
        }],
        xAxis: {
            name: "xAxis",
            min: 0,
            max: 100,
            majorUnit: 25,
            axisCrossingValues: [0, 25],
            labels: {
                format: "{0}"
            },
        },
        yAxis: {
            name: "yAxis",
            min: -12,
            max: 12,
            majorUnit: 4,
            axisCrossingValues: [-12, 12],
            labels: {
                format: "{0}"
            },
        },
        chartArea: {
            width: 350,
            height: 150//180
        },
        plotArea: {
            border: {
                width: 2,
                color: "black"
            }
        },
        render: renderChartWithBackgroundByAxis
    });

    $chart_B_BalanceChart.kendoChart({
        renderAs: "canvas",
        seriesDefaults: {
            type: "scatterLine",
            style: "smooth"
        },
        dataSource: jsonGray,
        series: [{
            //data: grayBlackL,            
            xField: "CmykC",
            yField: "Targetb",
            color: "Green",
            markers: { background: "Green", size: 4 },
            highlight: {
                visible: false
            }
        }, {
            //data: grayBlackL,
            xField: "CmykC",
            yField: "CielabB",
            color: "Black",
            markers: { background: "Black", size: 4 },
            tooltip: {
                visible: true,
                template: tooltipString
            }
        }],
        xAxis: {
            name: "xAxis",
            min: 0,
            max: 100,
            majorUnit: 25,
            axisCrossingValues: [0, 25],
            labels: {
                format: "{0}"
            },
            title: {
                text: ""
            }
        },
        yAxis: {
            name: "yAxis",
            min: -12,
            max: 12,
            majorUnit: 4,
            axisCrossingValues: [-12, 12],
            labels: {
                format: "{0}"
            },
            title: {
                text: ""
            }
        },
        chartArea: {
            width: 350,
            height: 150//180
        },
        plotArea: {
            border: {
                width: 2,
                color: "black"
            }
        },
        render: renderChartWithBackgroundByAxis
    });

}

////**************** POPULATE THE DELTA SUMMARY REPORT CHART  ****************////
function PopulateSummaryDeltaChart() {
    var maxValue = Math.max.apply(Math, jsonSummaryValueList);
    var maxLimit = Math.max.apply(Math, jsonSummaryLimitList);
    var maxNum = 0;
    maxNum = maxValue > maxLimit ? maxValue : maxLimit;
    maxNum = Math.trunc(maxNum) + 1;


    //generate the second stacked chart by max value
    var summarySeries = [];
    var stackedChart = [];
    var categoryList = [];

    for (var i = 0; i < jsonSummaryValueList.length; i++) {

        //create the series chart
        var obj = {
            value: jsonSummaryValueList[i],
            limit: jsonSummaryLimitList.length > 0 ? jsonSummaryLimitList[i] : 0
        }
        summarySeries.push(obj);


        //set the stack chart(upper chart)
        var stack = {
            value: maxNum - jsonSummaryValueList[i]
        };
        stackedChart.push(stack);
    }

    var selectedDeltaE = $selectedDeltaE;
    if (selectedDeltaE == "E") {
        selectedDeltaE = "ΔE";
    }

    $.each(jsonSummaryNameList, function (index, text) {
        switch (text) {
            case "Avg_ΔE": categoryList.push("Average " + selectedDeltaE); break;
            case "Max_ΔE": categoryList.push("Maximum " + selectedDeltaE); break;
            case "PriΔE": categoryList.push("Max Primary " + selectedDeltaE); break;
            case "PriΔH": categoryList.push("Max Primary ΔH"); break;
            case "GrayΔE": categoryList.push("Gray Average " + selectedDeltaE); break;
            case "NinetyFiveΔE": categoryList.push("95th Percentile " + selectedDeltaE); break;
            //default: categoryList.push(selectedDeltaE); break;
        }
    });
    //reverse the chart bars to show valuesaxis labels to top
    summarySeries = summarySeries.reverse();
    stackedChart = stackedChart.reverse();
    categoryList = categoryList.reverse();


    var chartHeight = 150;
    if (summarySeries.length == 3) {
        chartHeight = 130;
    }
    else if (summarySeries.length == 4) {
        chartHeight = 170;
    }
    else if (summarySeries.length == 5) {
        chartHeight = 205;
    }
    else if (summarySeries.length == 6) {
        chartHeight = 240;
    }
    else if (summarySeries.length == 7) {
        chartHeight = 268;
    }
    else if (summarySeries.length == 8) {
        chartHeight = 300;
    }
    else if (summarySeries.length == 9) {
        chartHeight = 335;
    }
    else if (summarySeries.length == 10) {
        chartHeight = 375;
    }
    else if (summarySeries.length == 11) {
        chartHeight = 410;
    }
    else if (summarySeries.length == 12) {
        chartHeight = 440;
    }

    var thrashHold = { paths: [] };
    $divChartSummaryDeltaChart.kendoChart({
        renderAs: "canvas",
        //title: {
        //    text: "ΔE2000 Primary Patches"
        //},
        seriesDefaults: {
            type: "bar",
            stack: true,
            width: 10,
            gap: 0.2,
            color: "#A1A1A1",
            visual: function (e) {
                var visual = e.createVisual();
                //-----For Sync1D projects or validation projects
                if ($hdnCurrentDlpSubOperation == ELicenseType.Sync1D || $hdnCurrentDlpSubOperation == ELicenseType.Sync1DMultiColor || $hdnCurrentDlpSubOperation == ELicenseType.Validation || $hdnCurrentDlpSubOperation == ELicenseType.DLPPlus) {
                    if (e.series.index == 0) {
                        var axis = e.sender.getAxis("valueAxis");


                        //------Red marker image plot for Tolerance
                        if (e.dataItem.limit > 0) {
                            var redObj = {};
                            redObj.x = axis.slot(e.dataItem.limit).origin.x;
                            redObj.y1 = e.rect.topRight().y
                            redObj.y2 = (e.rect.topRight().y + e.rect.size.height);
                            redObj.y3 = (e.rect.topRight().y + (e.rect.size.height / 2)); //---red dot mid of the line
                            redObj.color = "red";
                            thrashHold.paths.push(redObj);
                        }
                    }
                }
                return visual;
            }
        },
        series: [{
            //data: jsonSummaryValueList,summarySeries
            data: summarySeries,
            overlay: {
                gradient: "glass",
            }
        }, {
            data: stackedChart,
            overlay: {
                gradient: "glass",
            },
            color: "#C8D6E3"
        }],
        panes: [{
            clip: false
        }],
        categoryAxis: {
            name: "categoryAxis",
            //categories: categoryList,
            categories: [],
            majorGridLines: {
                visible: false
            },
            labels: {
                font: "12px Arial,Helvetica,sans-serif"
            },
            reverse: true
        },
        valueAxis: {
            name: "valueAxis",
            //max: 20,
            line: {
                visible: false
            },
            minorGridLines: {
                visible: false
            },
            labels: {
                rotation: "auto",
                font: "12px Arial,Helvetica,sans-serif"
            },
            max: maxNum,
            //margin: marginValue
        },
        chartArea: {
            width: 400,
            height: chartHeight
        },
        render: function (e) {

            //-----For Sync1D projects or validation projects
            if ($hdnCurrentDlpSubOperation == ELicenseType.Sync1D || $hdnCurrentDlpSubOperation == ELicenseType.Sync1DMultiColor || $hdnCurrentDlpSubOperation == ELicenseType.Validation || $hdnCurrentDlpSubOperation == ELicenseType.DLPPlus) {
                var chart = e.sender;
                $.each(thrashHold.paths, function (index, obj) {
                    //set the line 
                    var pathRed = new kendo.drawing.Path({ stroke: { width: 2, color: obj.color } }).moveTo(obj.x, obj.y1).lineTo(obj.x, obj.y2);
                    chart.surface.draw(pathRed);

                    //set the dot mid of the line
                    var circleGeoPath = new kendo.geometry.Circle([obj.x, obj.y3], 2);  //
                    var circlePath = new kendo.drawing.Circle(circleGeoPath).fill(obj.color, 1);
                    circlePath.options.stroke.color = obj.color;
                    chart.surface.draw(circlePath);
                });
            }
        }
    });
}

////**************** START BIND THE G7 CHART  ****************////
function PopulateG7Charts() {

    $divChartG7GrayBalanceDeltaCh.kendoChart({
        legend: {
            visible: false
        },
        renderAs: "canvas",
        seriesDefaults: {
            type: "scatterLine",
            style: "smooth"
        },
        dataSource: jsonGrayFChartList,
        series: [{
            name: "Weighted",
            xField: "Cmyk_Value",
            yField: "wΔValue",
            color: "Blue",
            markers: { background: "Blue", size: 4 },
            tooltip: {
                visible: true,
                template: '<table><tr><td><center><b>#= dataItem.Cmyk_Value.toFixed() #' + _Resources._recpercentPatchMetrics + '</b></center><td></tr><tr><td> wΔCh*:  #= dataItem.wΔValue #</td></tr></table>'
            }
        },
        {
            xField: "Cmyk_Value",
            yField: "ΔValue",
            color: "Black",
            markers: { background: "Black", size: 4 },
            //highlight: {
            //    visible: false
            //},
            tooltip: {
                visible: true,
                template: '<table><tr><td><center><b>#= dataItem.Cmyk_Value.toFixed() #' + _Resources._recpercentPatchMetrics + '</b></center><td></tr><tr><td> ΔCh*:  #= dataItem.ΔValue #</td></tr></table>'
            }
        }, {
            name: "Non-Weighted",
            xField: "Cmyk_Value",
            yField: "ΔLimit",
            color: "Red",
            markers: { background: "Red", size: 0 },
            highlight: {
                visible: false
            }
        }
        ],
        xAxis: {
            name: "xAxis",
            min: 0,
            max: 100,
            majorUnit: 25,
            axisCrossingValues: [0, 100],
            labels: {
                format: "{0}"
            },
            title: {
                text: ""
            }
        },
        yAxis: {
            name: "yAxis",
            min: 0,
            max: 6,
            majorUnit: 2,
            axisCrossingValues: [0, 100],
            labels: {
                format: "{0}"
            },
        },
        chartArea: {
            width: 400,
            height: 120
        },
        plotArea: {
            border: {
                width: 2,
                color: "black"
            }
        },
        render: renderChartWithBackgroundByAxis
    });

    $divChartG7BlackDeltaL.kendoChart({
        legend: {
            visible: false
        },
        renderAs: "canvas",
        seriesDefaults: {
            type: "scatterLine",
            style: "smooth"
        },
        dataSource: jsonBlackLChartList,
        series: [
            {
                name: "Weighted",
                xField: "Cmyk_Value",
                yField: "wΔValue",
                color: "Blue",
                markers: { background: "Blue", size: 4 },
                tooltip: {
                    visible: true,
                    template: '<table><tr><td><center><b>#= dataItem.Cmyk_Value.toFixed() #' + _Resources._recpercentPatchMetrics + '</b></center><td></tr><tr><td> wΔL*:  #= dataItem.wΔValue #</td></tr></table>'
                }
            },
            {
                name: "Non-Weighted",
                xField: "Cmyk_Value",
                yField: "ΔValue",
                color: "Black",
                markers: { background: "Black", size: 4 },
                //highlight: {
                //    visible: false
                //},
                tooltip: {
                    visible: true,
                    template: '<table><tr><td><center><b>#= dataItem.Cmyk_Value.toFixed() #' + _Resources._recpercentPatchMetrics + '</b></center><td></tr><tr><td> ΔL*:  #= dataItem.ΔValue #</td></tr></table>'
                }
            },
            {
                xField: "Cmyk_Value",
                yField: "ΔLimit",
                color: "Red",
                markers: { background: "Red", size: 0 },
                highlight: {
                    visible: false
                }
            }
        ],
        xAxis: {
            name: "xAxis",
            min: 0,
            max: 100,
            majorUnit: 25,
            axisCrossingValues: [0, 100],
            labels: {
                format: "{0}"
            },
            title: {
                text: ""
            }
        },
        yAxis: {
            name: "yAxis",
            min: 0,
            max: 6,
            majorUnit: 2,
            axisCrossingValues: [0, 100],
            labels: {
                format: "{0}"
            },
        },
        chartArea: {
            width: 400,
            height: 120
        },
        plotArea: {
            border: {
                width: 2,
                color: "black"
            }
        },
        render: renderChartWithBackgroundByAxis
    });

    $divChartG7GrayDeltaL.kendoChart({
        legend: {
            position: "top"
        },
        legendItemClick: function (e) {
            var seriesDeltaCh = $divChartG7GrayBalanceDeltaCh.data("kendoChart").findSeriesByIndex(e.seriesIndex);
            var seriesDeltaL = $divChartG7BlackDeltaL.data("kendoChart").findSeriesByIndex(e.seriesIndex);

            seriesDeltaCh.toggleVisibility(!seriesDeltaCh._options.visible);
            seriesDeltaL.toggleVisibility(!seriesDeltaL._options.visible);
        },
        renderAs: "canvas",
        seriesDefaults: {
            type: "scatterLine",
            style: "smooth"
        },
        legend: {
            position: "bottom"
        },
        dataSource: jsonGrayLChartList,
        series: [{
            name: "Weighted",
            xField: "Cmyk_Value",
            yField: "wΔValue",
            color: "Blue",
            markers: { background: "Blue", size: 4 },
            tooltip: {
                visible: true,
                template: '<table><tr><td><center><b>#= dataItem.Cmyk_Value.toFixed() #' + _Resources._recpercentPatchMetrics + '</b></center><td></tr><tr><td> wΔL*:  #= dataItem.wΔValue #</td></tr></table>'
            }
        },
        {
            name: "Non-Weighted",
            xField: "Cmyk_Value",
            yField: "ΔValue",
            color: "Black",
            markers: { background: "Black", size: 4 },
            //highlight: {
            //    visible: false
            //},
            tooltip: {
                visible: true,
                template: '<table><tr><td><center><b>#= dataItem.Cmyk_Value.toFixed() #' + _Resources._recpercentPatchMetrics + '</b></center><td></tr><tr><td> ΔL*:  #= dataItem.ΔValue #</td></tr></table>'
            }
        },
        {
            xField: "Cmyk_Value",
            yField: "ΔLimit",
            color: "Red",
            markers: { background: "Red", size: 0 },
            highlight: {
                visible: false
            }
        }
        ],
        xAxis: {
            name: "xAxis",
            min: 0,
            max: 100,
            majorUnit: 25,
            axisCrossingValues: [0, 100],
            labels: {
                format: "{0}"
            },
            title: {
                text: ""
            }
        },
        yAxis: {
            name: "yAxis",
            min: 0,
            max: 6,
            majorUnit: 2,
            axisCrossingValues: [0, 100],
            labels: {
                format: "{0}"
            },
        },
        chartArea: {
            width: 400,
            height: 150
        },
        plotArea: {
            border: {
                width: 2,
                color: "black"
            }
        },
        render: renderChartWithBackgroundByAxis
    });

    $divChartG7BlackNPD.kendoChart({
        //title: {
        //    text: "Black NPD"
        //},
        renderAs: "canvas",
        seriesDefaults: {
            type: "scatterLine",
            style: "smooth"
        },
        legend: {
            position: "bottom"
        },
        dataSource: jsonBlackNPDChartList,
        series: [
            {
                xField: "Cmyk_Value",
                yField: "ΔLimit",
                color: "Green",
                markers: { background: "Green", size: 0 },
                highlight: {
                    visible: false
                },
                tooltip: {
                    visible: true,
                    template: '<table><tr><td><center><b>Black Target NPD  #= dataItem.Cmyk_Value.toFixed() #% ' + _Resources._recPatchMetrics + '</b></center><td></tr><tr><td> G7NPDC:  #= dataItem.ΔLimit #</td></tr></table>'
                }
            },
            {
                xField: "Cmyk_Value",
                yField: "ΔValue",
                color: "Black",
                markers: { background: "Black", size: 0 },
                highlight: {
                    visible: false
                },
                tooltip: {
                    visible: true,
                    template: '<table><tr><td><center><b>Black Actual NPD  #= dataItem.Cmyk_Value.toFixed() #% ' + _Resources._recPatchMetrics + '</b></center><td></tr><tr><td> G7NPDC Actual:  #= dataItem.ΔValue #</td></tr></table>'
                }
            }
        ],
        xAxis: {
            name: "xAxis",
            min: 0,
            max: 100,
            majorUnit: 25,
            axisCrossingValues: [0, 100],
            labels: {
                format: "{0}"
            },
            title: {
                text: ""
            }
        },
        yAxis: {
            name: "yAxis",
            min: 0,
            max: 2,
            majorUnit: 0.5,
            axisCrossingValues: [0, 100],
            labels: {
                format: "{0}"
            },
        },
        chartArea: {
            width: 320,
            height: 250
        },
        plotArea: {
            border: {
                width: 2,
                color: "black"
            }
        },
        render: renderChartWithBackgroundByAxis
    });

    $divChartG7GrayNPD.kendoChart({
        //title: {
        //    text: "Gray NPD"
        //},
        renderAs: "canvas",
        seriesDefaults: {
            type: "scatterLine",
            style: "smooth"
        },
        legend: {
            position: "bottom"
        },
        dataSource: jsonGrayNPDChartList,
        series: [
            {
                xField: "Cmyk_Value",
                yField: "ΔLimit",
                color: "Green",
                markers: { background: "Green", size: 0 },
                highlight: {
                    visible: false
                },
                tooltip: {
                    visible: true,
                    template: '<table><tr><td><center><b>Gray Target NPD  #= dataItem.Cmyk_Value.toFixed() #% ' + _Resources._recPatchMetrics + '</b></center><td></tr><tr><td> G7NPDC:  #= dataItem.ΔLimit #</td></tr></table>'
                }
            },
            {
                xField: "Cmyk_Value",
                yField: "ΔValue",
                color: "Black",
                markers: { background: "Black", size: 0 },
                highlight: {
                    visible: false
                },
                tooltip: {
                    visible: true,
                    template: '<table><tr><td><center><b>Gray Actual NPD  #= dataItem.Cmyk_Value.toFixed() #% ' + _Resources._recPatchMetrics + '</b></center><td></tr><tr><td> G7NPDC Actual:  #= dataItem.ΔValue #</td></tr></table>'
                }
            }
        ],
        xAxis: {
            name: "xAxis",
            min: 0,
            max: 100,
            majorUnit: 25,
            axisCrossingValues: [0, 100],
            labels: {
                format: "{0}"
            },
            title: {
                text: ""
            }
        },
        yAxis: {
            name: "yAxis",
            min: 0,
            max: 2,
            majorUnit: 0.5,
            axisCrossingValues: [0, 100],
            labels: {
                format: "{0}"
            },
        },
        chartArea: {
            width: 320,
            height: 250
        },
        plotArea: {
            border: {
                width: 2,
                color: "black"
            }
        },
        render: renderChartWithBackgroundByAxis
    });
}

////**************** POPULATE THE TVI ISO CHART  ****************////
function PopulateTVIISOChart() {
    var isoCyanList = [];
    var isoMagentaList = [];
    var isoYellowList = [];
    var isoBlackList = [];

    if (jsonDTISOTVIChartList.length > 0) {
        isoCyanList = $.grep(jsonDTISOTVIChartList, function (element) {
            var matched = false;
            switch (element.PatchTypeId) {
                case CPPatchType.Cyan:
                case CPPatchType.Paper:
                case CPPatchType.Cyan_Quarter_Tone:
                case CPPatchType.Cyan_Half_Tone:
                case CPPatchType.Cyan_Three_Quarter_Tone:
                case CPPatchType.Cyan_10:
                case CPPatchType.Cyan_20:
                case CPPatchType.Cyan_25:
                case CPPatchType.Cyan_30:
                case CPPatchType.Cyan_40:
                case CPPatchType.Cyan_50:
                case CPPatchType.Cyan_60:
                case CPPatchType.Cyan_70:
                case CPPatchType.Cyan_75:
                case CPPatchType.Cyan_80:
                case CPPatchType.Cyan_90:
                    matched = true;
                    break;
                default: break;
            }
            return matched;
        });
        isoCyanList.sort(function (a, b) { return a.CmykC - b.CmykC; });

        $.each(isoCyanList, function (index, obj) {
            var thrashHoldObject = [];
            var ThresholdTop = 0;
            var ThresholdBottom = 0;
            var thresHoldValue = obj.Threshold;
            if (thresHoldValue == 0 && index != 0 && index != isoCyanList.length - 1) {
                thresHoldValue = (isoCyanList[index - 1].Threshold + isoCyanList[index + 1].Threshold) / 2;
            }
            ThresholdTop = obj.TargetDotGain + thresHoldValue;
            ThresholdBottom = obj.TargetDotGain - thresHoldValue;
            obj.ThresholdTop = ThresholdTop;
            obj.ThresholdBottom = ThresholdBottom;
        });

    }

    if (jsonDTISOTVIChartList.length > 0) {
        isoMagentaList = $.grep(jsonDTISOTVIChartList, function (element) {
            var matched = false;
            switch (element.PatchTypeId) {
                case CPPatchType.Magenta:
                case CPPatchType.Paper:
                case CPPatchType.Magenta_Quarter_Tone:
                case CPPatchType.Magenta_Half_Tone:
                case CPPatchType.Magenta_Three_Quarter_Tone:
                case CPPatchType.Magenta_10:
                case CPPatchType.Magenta_20:
                case CPPatchType.Magenta_25:
                case CPPatchType.Magenta_30:
                case CPPatchType.Magenta_40:
                case CPPatchType.Magenta_50:
                case CPPatchType.Magenta_60:
                case CPPatchType.Magenta_70:
                case CPPatchType.Magenta_75:
                case CPPatchType.Magenta_80:
                case CPPatchType.Magenta_90:
                    matched = true;
                    break;
                default: break;
            }
            return matched;
        });
        isoMagentaList.sort(function (a, b) { return a.CmykM - b.CmykM; });

        $.each(isoMagentaList, function (index, obj) {
            var thrashHoldObject = [];
            var ThresholdTop = 0;
            var ThresholdBottom = 0;
            var thresHoldValue = obj.Threshold;
            if (thresHoldValue == 0 && index != 0 && index != isoMagentaList.length - 1) {
                thresHoldValue = (isoMagentaList[index - 1].Threshold + isoMagentaList[index + 1].Threshold) / 2;
            }
            ThresholdTop = obj.TargetDotGain + thresHoldValue;
            ThresholdBottom = obj.TargetDotGain - thresHoldValue;
            obj.ThresholdTop = ThresholdTop;
            obj.ThresholdBottom = ThresholdBottom;
        });
        //console.log(isoMagentaList);
    }

    if (jsonDTISOTVIChartList.length > 0) {
        isoYellowList = $.grep(jsonDTISOTVIChartList, function (element) {
            var matched = false;
            switch (element.PatchTypeId) {
                case CPPatchType.Yellow:
                case CPPatchType.Paper:
                case CPPatchType.Yellow_Quarter_Tone:
                case CPPatchType.Yellow_Half_Tone:
                case CPPatchType.Yellow_Three_Quarter_Tone:
                case CPPatchType.Yellow_10:
                case CPPatchType.Yellow_20:
                case CPPatchType.Yellow_25:
                case CPPatchType.Yellow_30:
                case CPPatchType.Yellow_40:
                case CPPatchType.Yellow_50:
                case CPPatchType.Yellow_60:
                case CPPatchType.Yellow_70:
                case CPPatchType.Yellow_75:
                case CPPatchType.Yellow_80:
                case CPPatchType.Yellow_90:
                    matched = true;
                    break;
                default: break;
            }
            return matched;
        });
        isoYellowList.sort(function (a, b) { return a.CmykY - b.CmykY; });

        $.each(isoYellowList, function (index, obj) {
            var thrashHoldObject = [];
            var ThresholdTop = 0;
            var ThresholdBottom = 0;
            var thresHoldValue = obj.Threshold;
            if (thresHoldValue == 0 && index != 0 && index != isoYellowList.length - 1) {
                thresHoldValue = (isoYellowList[index - 1].Threshold + isoYellowList[index + 1].Threshold) / 2;
            }
            ThresholdTop = obj.TargetDotGain + thresHoldValue;
            ThresholdBottom = obj.TargetDotGain - thresHoldValue;
            obj.ThresholdTop = ThresholdTop;
            obj.ThresholdBottom = ThresholdBottom;
        });
        //console.log(isoYellowList);
    }

    if (jsonDTISOTVIChartList.length > 0) {
        isoBlackList = $.grep(jsonDTISOTVIChartList, function (element) {
            var matched = false;
            switch (element.PatchTypeId) {
                case CPPatchType.Black:
                case CPPatchType.Paper:
                case CPPatchType.Black_Quarter_Tone:
                case CPPatchType.Black_Half_Tone:
                case CPPatchType.Black_Three_Quarter_Tone:
                case CPPatchType.Black_10:
                case CPPatchType.Black_20:
                case CPPatchType.Black_25:
                case CPPatchType.Black_30:
                case CPPatchType.Black_40:
                case CPPatchType.Black_50:
                case CPPatchType.Black_60:
                case CPPatchType.Black_70:
                case CPPatchType.Black_75:
                case CPPatchType.Black_80:
                case CPPatchType.Black_90:
                    matched = true;
                    break;
                default: break;
            }
            return matched;
        });
        isoBlackList.sort(function (a, b) { return a.CmykK - b.CmykK; });

        $.each(isoBlackList, function (index, obj) {
            var thrashHoldObject = [];
            var ThresholdTop = 0;
            var ThresholdBottom = 0;
            var thresHoldValue = obj.Threshold;
            if (thresHoldValue == 0 && index != 0 && index != isoBlackList.length - 1) {
                thresHoldValue = (isoBlackList[index - 1].Threshold + isoBlackList[index + 1].Threshold) / 2;
            }
            ThresholdTop = obj.TargetDotGain + thresHoldValue;
            ThresholdBottom = obj.TargetDotGain - thresHoldValue;
            obj.ThresholdTop = ThresholdTop;
            obj.ThresholdBottom = ThresholdBottom;
        });
        //console.log(isoBlackList);
    }

    $divChartTVIISO_CYAN.kendoChart({
        renderAs: "canvas",
        //title: {
        //    text: "Cyan"
        //},
        dataSource: {
            data: isoCyanList
        },
        series: [
            {
                type: "rangeArea",
                fromField: "ThresholdBottom",
                toField: "ThresholdTop",
                color: "#148787",
                line: {
                    style: "smooth",
                    color: "#058e8e",
                    width: 1
                },
                highlight: {
                    visible: false,
                }
            },
            {
                type: "line",
                field: "TargetDotGain",
                color: "#3eb257",
                markers: { background: "#3eb257" },
                highlight: {
                    visible: false
                }
            },
            {
                type: "line",
                field: "DotGain",
                color: "Cyan",
                markers: { background: "Cyan" },
                tooltip: {
                    visible: true,
                    template: '<table><tr><td><center><b>' + _Resources._recCyan + ' #= dataItem.CmykC.toFixed() #% ' + _Resources._recPatchMetrics + '</b></center><td></tr><tr><td> ' + _Resources._recactualTVI + '  #= dataItem.DotGain #%</td></tr><tr><td> ' + _Resources._rectargetTVI + '  #= kendo.toString(dataItem.TargetDotGain, "n") #%</td></tr><tr><td> ' + _Resources._recTVIThr + '  #= kendo.toString(dataItem.Threshold, "n") #%</td></tr></table>'
                }
            }
        ],
        valueAxis: {
            min: 0,
            name: "valueAxis",
            majorUnit: 5
        },
        seriesDefaults: {
            line: {
                style: "smooth"
            }
        },
        categoryAxis: {
            name: "categoryAxis",
            field: "CmykC",
            justified: true
        },
        chartArea: {
            width: 325,
            height: 200
        },
        render: renderChartWithBackgroundByCategory
    });


    $divChartTVIISO_Magenta.kendoChart({
        renderAs: "canvas",
        //title: {
        //    text: "Magenta"
        //},
        dataSource: {
            data: isoMagentaList
        },
        series: [
            {
                type: "rangeArea",
                fromField: "ThresholdBottom",
                toField: "ThresholdTop",
                color: "#148787",
                line: {
                    style: "smooth",
                    color: "#058e8e",
                    width: 1
                },
                highlight: {
                    visible: false
                }
            },
            {
                type: "line",
                field: "TargetDotGain",
                color: "#3eb257",
                markers: { background: "#3eb257" },
                highlight: {
                    visible: false
                }
            },
            {
                type: "line",
                field: "DotGain",
                color: "Magenta",
                markers: { background: "Magenta" },
                tooltip: {
                    visible: true,
                    //template: '<table><tr><td><center><b>Cyan #= dataItem.CmykM.toFixed() #% Patch Matrics</b></center><td></tr><tr><td> Actual TVI:  #= dataItem.DotGain #%</td></tr><tr><td> Target TVI:  #= kendo.toString(dataItem.TargetDotGain, "n") #%</td></tr><tr><td> TVI Threashhold:  #= kendo.toString(dataItem.Threshold, "n") #%</td></tr></table>'
                    template: '<table><tr><td><center><b>' + _Resources._recMagenta + ' #= dataItem.CmykM.toFixed() #% ' + _Resources._recPatchMetrics + '</b></center><td></tr><tr><td> ' + _Resources._recactualTVI + '  #= dataItem.DotGain #%</td></tr><tr><td> ' + _Resources._rectargetTVI + '  #= kendo.toString(dataItem.TargetDotGain, "n") #%</td></tr><tr><td> ' + _Resources._recTVIThr + '  #= kendo.toString(dataItem.Threshold, "n") #%</td></tr></table>'
                }
            }
        ],
        valueAxis: {
            min: 0,
            name: "valueAxis",
            majorUnit: 5,
            //axisCrossingValue: -10
        },
        seriesDefaults: {
            line: {
                style: "smooth"
            }
        },
        categoryAxis: {
            name: "categoryAxis",
            field: "CmykM",
            justified: true
        },
        chartArea: {
            width: 325,
            height: 200
        },
        render: renderChartWithBackgroundByCategory
    });


    $divChartTVIISO_Yellow.kendoChart({
        renderAs: "canvas",
        //title: {
        //    text: "Yellow"
        //},
        dataSource: {
            data: isoYellowList
        },
        series: [
            {
                type: "rangeArea",
                fromField: "ThresholdBottom",
                toField: "ThresholdTop",
                color: "#148787",
                line: {
                    style: "smooth",
                    color: "#058e8e",
                    width: 1
                },
                highlight: {
                    visible: false
                }
            },
            {
                type: "line",
                field: "TargetDotGain",
                color: "#3eb257",
                markers: { background: "#3eb257" },
                highlight: {
                    visible: false
                }
            },
            {
                type: "line",
                field: "DotGain",
                color: "Yellow",
                markers: { background: "Yellow" },
                tooltip: {
                    visible: true,
                    //template: '<table><tr><td><center><b>Cyan #= dataItem.CmykY.toFixed() #% Patch Matrics</b></center><td></tr><tr><td> Actual TVI:  #= dataItem.DotGain #%</td></tr><tr><td> Target TVI:  #= kendo.toString(dataItem.TargetDotGain, "n") #%</td></tr><tr><td> TVI Threashhold:  #= kendo.toString(dataItem.Threshold, "n") #%</td></tr></table>'
                    template: '<table><tr><td><center><b>' + _Resources._recYellow + ' #= dataItem.CmykY.toFixed() #% ' + _Resources._recPatchMetrics + '</b></center><td></tr><tr><td> ' + _Resources._recactualTVI + '  #= dataItem.DotGain #%</td></tr><tr><td> ' + _Resources._rectargetTVI + '  #= kendo.toString(dataItem.TargetDotGain, "n") #%</td></tr><tr><td> ' + _Resources._recTVIThr + '  #= kendo.toString(dataItem.Threshold, "n") #%</td></tr></table>'
                }
            }
        ],
        valueAxis: {
            min: 0,
            name: "valueAxis",
            majorUnit: 5
        },
        seriesDefaults: {
            line: {
                style: "smooth"
            }
        },
        categoryAxis: {
            name: "categoryAxis",
            field: "CmykY",
            justified: true
        },
        chartArea: {
            width: 325,
            height: 200
        },
        render: renderChartWithBackgroundByCategory
    });

    $divChartTVIISO_Black.kendoChart({
        renderAs: "canvas",
        //title: {
        //    text: "Black"
        //},
        dataSource: {
            data: isoBlackList
        },
        series: [
            {
                type: "rangeArea",
                fromField: "ThresholdBottom",
                toField: "ThresholdTop",
                color: "#148787",
                line: {
                    style: "smooth",
                    color: "#058e8e",
                    width: 1
                },
                highlight: {
                    visible: false
                }
            },
            {
                type: "line",
                field: "TargetDotGain",
                color: "#3eb257",
                markers: { background: "#3eb257" },
                highlight: {
                    visible: false
                }
            },
            {
                type: "line",
                field: "DotGain",
                color: "Black",
                markers: { background: "Black" },
                tooltip: {
                    visible: true,
                    //template: '<table><tr><td><center><b>Cyan #= dataItem.CmykK.toFixed() #% Patch Matrics</b></center><td></tr><tr><td> Actual TVI:  #= dataItem.DotGain #%</td></tr><tr><td> Target TVI:  #= kendo.toString(dataItem.TargetDotGain, "n") #%</td></tr><tr><td> TVI Threashhold:  #= kendo.toString(dataItem.Threshold, "n") #%</td></tr></table>'
                    template: '<table><tr><td><center><b>' + _Resources._recBlack + ' #= dataItem.CmykK.toFixed() #% ' + _Resources._recPatchMetrics + '</b></center><td></tr><tr><td> ' + _Resources._recactualTVI + '  #= dataItem.DotGain #%</td></tr><tr><td> ' + _Resources._rectargetTVI + '  #= kendo.toString(dataItem.TargetDotGain, "n") #%</td></tr><tr><td> ' + _Resources._recTVIThr + '  #= kendo.toString(dataItem.Threshold, "n") #%</td></tr></table>'
                }
            }
        ],
        valueAxis: {
            min: 0,
            name: "valueAxis",
            majorUnit: 5
        },
        seriesDefaults: {
            line: {
                style: "smooth"
            }
        },
        categoryAxis: {
            name: "categoryAxis",
            field: "CmykK",
            justified: true
        },
        chartArea: {
            width: 325,
            height: 200
        },
        render: renderChartWithBackgroundByCategory
    });
}

////**************** POPULATE THE TVI G7 CHART  ****************////
function PopulateTVIG7CMYKChart() {

    var jsonCMYK_C = $.grep(jsonDTSpreadChartData_CMYKList, function (element) { return (element.CmykC != 0 || element.DotGain == 0) });
    var jsonCMYK_M = $.grep(jsonDTSpreadChartData_CMYKList, function (element) { return (element.CmykM != 0 || element.DotGain == 0) });
    var jsonCMYK_Y = $.grep(jsonDTSpreadChartData_CMYKList, function (element) { return (element.CmykY != 0 || element.DotGain == 0) });
    var jsonCMYK_K = $.grep(jsonDTSpreadChartData_CMYKList, function (element) { return (element.CmykK != 0 || element.DotGain == 0) });


    var arrValues = $.map(jsonDTSpreadChartData_CMYKList, function (elem) { return elem.DotGain; });
    var maxElement = Math.abs(Math.max.apply(Math, arrValues));
    var maxValues = 0;


    if (maxElement >= 0 && maxElement < 10) {
        maxValues = 10
    } else if (maxElement >= 10 && maxElement < 20) {
        maxValues = 20;
    } else if (maxElement >= 20 && maxElement < 30) {
        maxValues = 30;
    } else if (maxElement >= 30 && maxElement < 40) {
        maxValues = 40;
    } else if (maxElement >= 40 && maxElement < 50) {
        maxValues = 50;
    } else if (maxElement >= 50 && maxElement < 60) {
        maxValues = 60;
    } else if (maxElement >= 60 && maxElement < 70) {
        maxValues = 70;
    } else if (maxElement >= 70 && maxElement < 80) {
        maxValues = 80;
    } else if (maxElement >= 80 && maxElement < 90) {
        maxValues = 90;
    } else {
        maxValues = 100;
    }

    $divChartTVIISO_CMYK.kendoChart({
        renderAs: "canvas",
        //title: {
        //    text: "TVI CMYK Chart"
        //},
        seriesDefaults: {
            type: "scatterLine",
            style: "smooth",
            width: 2,
        },
        series: [
            {
                data: jsonCMYK_C,
                xField: "CmykC",
                yField: "DotGain",
                name: "C",
                color: "Cyan",
                markers: {
                    background: "Cyan",
                    size: 7,
                    border: {
                        width: 1,
                        color: function (e) {
                            return 'black';
                        }
                    }
                },
                highlight: {
                    visible: false
                },
                tooltip: {
                    visible: true,
                    //template: '<table><tr><td><center><b>Cyan #= dataItem.CmykC.toFixed() #% Patch Matrics</b></center><td></tr><tr><td> Actual TVI:  #= dataItem.DotGain #</td></tr></table>'
                    template: '<table><tr><td><center><b>' + _Resources._recCyan + ' #= dataItem.CmykC.toFixed() #% ' + _Resources._recPatchMetrics + '</b></center><td></tr><tr><td> ' + _Resources._recactualTVI + '  #= dataItem.DotGain #</td></tr></table>'
                },
                //line: {
                //    width: 5
                //},
            },
            {
                data: jsonCMYK_M,
                xField: "CmykM",
                yField: "DotGain",
                color: "Magenta",
                name: "M",
                markers: {
                    background: "Magenta", size: 7, border: {
                        width: 1,
                        color: function (e) {
                            return 'black';
                        }
                    }
                },
                tooltip: {
                    visible: true,
                    //template: '<table><tr><td><center><b>Cyan #= dataItem.CmykM.toFixed() #% Patch Matrics</b></center><td></tr><tr><td> Actual TVI:  #= dataItem.DotGain #</td></tr></table>'
                    template: '<table><tr><td><center><b>' + _Resources._recMagenta + ' #= dataItem.CmykM.toFixed() #% ' + _Resources._recPatchMetrics + '</b></center><td></tr><tr><td> ' + _Resources._recactualTVI + '  #= dataItem.DotGain #</td></tr></table>'
                },
                highlight: {
                    visible: false
                },
            },
            {
                data: jsonCMYK_Y,
                xField: "CmykY",
                yField: "DotGain",
                color: "Yellow",
                name: "Y",
                markers: {
                    background: "Yellow", size: 7, border: {
                        width: 1,
                        color: function (e) {
                            return 'black';
                        }
                    }
                },
                tooltip: {
                    visible: true,
                    //template: '<table><tr><td><center><b>Cyan #= dataItem.CmykY.toFixed() #% Patch Matrics</b></center><td></tr><tr><td> Actual TVI:  #= dataItem.DotGain #</td></tr></table>'
                    template: '<table><tr><td><center><b>' + _Resources._recYellow + ' #= dataItem.CmykY.toFixed() #% ' + _Resources._recPatchMetrics + '</b></center><td></tr><tr><td> ' + _Resources._recactualTVI + '  #= dataItem.DotGain #</td></tr></table>'
                },
                highlight: {
                    visible: false
                },
            }, {
                data: jsonCMYK_K,
                xField: "CmykK",
                yField: "DotGain",
                color: "Black",
                name: "K",
                markers: { background: "Black", size: 7 },
                tooltip: {
                    visible: true,
                    //template: '<table><tr><td><center><b>Cyan #= dataItem.CmykK.toFixed() #% Patch Matrics</b></center><td></tr><tr><td> Actual TVI:  #= dataItem.DotGain #</td></tr></table>'
                    template: '<table><tr><td><center><b>' + _Resources._recBlack + ' #= dataItem.CmykK.toFixed() #% ' + _Resources._recPatchMetrics + '</b></center><td></tr><tr><td> ' + _Resources._recactualTVI + '  #= dataItem.DotGain #</td></tr></table>'
                },
                highlight: {
                    visible: false
                },
            },
        ],
        xAxis: {
            name: "xAxis",
            //min: 0,
            max: 100,
            majorUnit: 25,
            //axisCrossingValues: [0, 100],
            labels: {
                format: "{0}"
            },
            title: {
                text: ""
            }
        },
        yAxis: {
            name: "yAxis",
            min: 0,
            max: maxValues,
            majorUnit: 5,
            axisCrossingValues: -100,
            labels: {
                format: "{0}"
            },
        },
        chartArea: {
            width: 650,
            height: 400
        },
        plotArea: {
            border: {
                width: 1,
                color: "black"
            }
        },
        render: renderChartWithBackgroundByAxis,
        legend: {
            position: "top",
            item: {
                visual: function (e) {
                    var color = e.options.markers.background;
                    var labelColor = e.options.labels.color;

                    // Define the target dimensions for the legend item
                    var rect = new kendo.geometry.Rect([0, 0], [100, 50]);

                    // A layout will hold the checkbox and the default visual                    
                    var layout = new kendo.drawing.Layout(rect, {
                        spacing: 5,
                        alignItems: "center"
                    });
                    // Cheat a bit by rendering the checkbox using the Unicode ballot symbol
                    var cbSymbol = e.active ? "☑" : "☐";
                    var cb = new kendo.drawing.Text(cbSymbol, [0, 0], {
                        font: "16px sans-serif"
                    });

                    // This will give us the default box + text
                    var defaultVisual = e.createVisual();

                    // Reflow them together
                    layout.append(cb, defaultVisual);
                    layout.reflow()

                    return layout;
                }
            }
        },
    });
}

////**************** POPULATE THE TVI SPREAD CHART  ****************////
function PopulateTVISpreadChart() {

    var jsonSpread_C = $.grep(jsonDTSpreadChartDataList, function (element) { return (element.CmykC == 0 && element.CmykM == 0 && element.CmykY == 0 && element.DotGain == 0) || element.CmykC != 0 });
    var jsonSpread_M = $.grep(jsonDTSpreadChartDataList, function (element) { return (element.CmykM == 0 && element.CmykC == 0 && element.CmykY == 0 && element.DotGain == 0) || element.CmykM != 0 });
    var jsonSpread_Y = $.grep(jsonDTSpreadChartDataList, function (element) { return (element.CmykY == 0 && element.CmykC == 0 && element.CmykM == 0 && element.DotGain == 0) || element.CmykY != 0 });

    var splineRangeResultList = $.grep(jsonDTSpreadSplineRangeChartDataList, function (element) { return element.UpperLimit != 0 && element.LowerLimit != 0 });

    //var obj = Object.assign({}, splineRangeResultList[0]);
    var obj = {};
    CopyObject(splineRangeResultList[0], obj)
    obj.UpperLimit = null;
    obj.LowerLimit = null;
    obj.TVI = 0;

    splineRangeResultList.push(obj);

    obj = {};
    //obj = Object.assign({}, splineRangeResultList[0])
    CopyObject(splineRangeResultList[0], obj)
    obj.UpperLimit = null;
    obj.LowerLimit = null;
    obj.TVI = 100;
    splineRangeResultList.push(obj);


    splineRangeResultList = splineRangeResultList.sort(function (e, e1) { return e.TVI - e1.TVI });

    $.each(jsonSpread_C, function (index, obj) {
        var magValue = 0;
        var yelValue = 0;
        if (obj.CmykC != 0) {

            var Mres = $.grep(jsonSpread_M, function (e) { return e.CmykM == obj.CmykC; });
            if (Mres.length > 0) {
                magValue = Mres[0].DotGain;
            }

            var Yres = $.grep(jsonSpread_Y, function (e) { return e.CmykY == obj.CmykC; });
            if (Yres.length > 0) {
                yelValue = Yres[0].DotGain;
            }

            var DiffrentOne = obj.DotGain - magValue;
            var DiffrentTwo = obj.DotGain - yelValue;
            var DifferentTitleOne = "C - M";
            var DifferentTitleTwo = "C - Y";
            obj.DiffrentOne = Math.abs(DiffrentOne);
            obj.DiffrentTwo = Math.abs(DiffrentTwo);
            obj.DifferentTitleOne = DifferentTitleOne;
            obj.DifferentTitleTwo = DifferentTitleTwo;
        }
    });


    $.each(jsonSpread_M, function (index, obj) {
        var cynValue = 0;
        var yelValue = 0;
        if (obj.CmykM != 0) {

            var Cres = $.grep(jsonSpread_C, function (e) { return e.CmykC == obj.CmykM; });
            if (Cres.length > 0) {
                cynValue = Cres[0].DotGain;
            }


            var Yres = $.grep(jsonSpread_Y, function (e) { return e.CmykY == obj.CmykM; });
            if (Yres.length > 0) {
                yelValue = Yres[0].DotGain;
            }

            var DiffrentOne = obj.DotGain - cynValue;
            var DiffrentTwo = obj.DotGain - yelValue;
            var DifferentTitleOne = "M - C";
            var DifferentTitleTwo = "M - Y";
            obj.DiffrentOne = Math.abs(DiffrentOne);
            obj.DiffrentTwo = Math.abs(DiffrentTwo);
            obj.DifferentTitleOne = DifferentTitleOne;
            obj.DifferentTitleTwo = DifferentTitleTwo;
        }
    });

    $.each(jsonSpread_Y, function (index, obj) {
        var cynValue = 0;
        var magValue = 0;
        if (obj.CmykY != 0) {

            var Cres = $.grep(jsonSpread_C, function (e) { return e.CmykC == obj.CmykY; });
            if (Cres.length > 0) {
                cynValue = Cres[0].DotGain;
            }

            var Mres = $.grep(jsonSpread_M, function (e) { return e.CmykM == obj.CmykY; });
            if (Mres.length > 0) {
                magValue = Mres[0].DotGain;
            }

            var DiffrentOne = obj.DotGain - cynValue;
            var DiffrentTwo = obj.DotGain - magValue;
            var DifferentTitleOne = "Y - C";
            var DifferentTitleTwo = "Y - M";
            obj.DiffrentOne = Math.abs(DiffrentOne);
            obj.DiffrentTwo = Math.abs(DiffrentTwo);
            obj.DifferentTitleOne = DifferentTitleOne;
            obj.DifferentTitleTwo = DifferentTitleTwo;
        }
    });

    var arrC = $.map(jsonSpread_C, function (e) { return e.CmykC; });
    var arrM = $.map(jsonSpread_M, function (e) { return e.CmykM; });
    var arrY = $.map(jsonSpread_Y, function (e) { return e.CmykY; });
    var unionArr = arrayUnique(arrC.concat(arrM));
    unionArr = arrayUnique(unionArr.concat(arrY));



    //Calculation for Yellow
    var arrNewY = [];

    $.each(unionArr, function (index, obj) {
        if ($.inArray(obj, arrY) == -1) {
            //var newObj = Object.assign({}, jsonSpread_Y[0]);
            var newObj = {};
            CopyObject(jsonSpread_Y[0], newObj)
            newObj.CmykY = obj;
            newObj.markerDisplay = false;
            arrNewY.push(newObj);
        }
    });

    jsonSpread_Y = jsonSpread_Y.concat(arrNewY);
    jsonSpread_Y = jsonSpread_Y.sort(function (a, b) {
        return a.CmykY - b.CmykY;
    });

    //
    $.each(jsonSpread_Y, function (index, yObj) {
        if (yObj.markerDisplay == undefined) {
            yObj.markerDisplay = true;
        }
    });


    //calculation for Magenta
    var arrNewM = [];

    $.each(unionArr, function (index, obj) {
        if ($.inArray(obj, arrM) == -1) {
            //var newObj = Object.assign({}, jsonSpread_M[0]);
            var newObj = {};
            CopyObject(jsonSpread_M[0], newObj)
            newObj.CmykM = obj;
            newObj.markerDisplay = false;
            arrNewM.push(newObj);
        }
    });

    jsonSpread_M = jsonSpread_M.concat(arrNewM);
    jsonSpread_M = jsonSpread_M.sort(function (a, b) {
        return a.CmykM - b.CmykM;
    });


    $.each(jsonSpread_M, function (index, mObj) {
        if (mObj.markerDisplay == undefined) {
            mObj.markerDisplay = true;
        }
    });


    //Calculation for Cyan
    var arrNewC = [];

    $.each(unionArr, function (index, obj) {
        if ($.inArray(obj, arrC) == -1) {
            //var newObj = Object.assign({}, jsonSpread_C[0]);
            var newObj = {};
            CopyObject(jsonSpread_C[0], newObj)
            newObj.CmykC = obj;
            newObj.markerDisplay = false;
            arrNewC.push(newObj);
        }
    });

    jsonSpread_C = jsonSpread_C.concat(arrNewC);
    jsonSpread_C = jsonSpread_C.sort(function (a, b) {
        return a.CmykC - b.CmykC;
    });

    //
    $.each(jsonSpread_C, function (index, cObj) {
        if (cObj.markerDisplay == undefined) {
            cObj.markerDisplay = true;
        }
    });


    //calculate spline result
    var splineArray = [];
    $.each(jsonSpread_C, function (index, elem) {

        var foundTVI = $.grep(splineRangeResultList, function (e) {
            return e.TVI == elem.CmykC;
        });

        if (foundTVI.length > 0) {
            splineArray.push(foundTVI[0]);
        } else {
            //var newObj = Object.assign({}, splineRangeResultList[0]);
            var newObj = {};
            CopyObject(splineRangeResultList[0], newObj)
            newObj.TVI = elem.CmykC;
            splineArray.push(newObj);
        }
    });


    $.each(jsonSpread_Y, function (index, obj) {
        if (obj.DotGain == 0 && index != 0 && index != jsonSpread_Y.length - 1) {
            obj.DotGain = (jsonSpread_Y[index - 1].DotGain + jsonSpread_Y[index + 1].DotGain) / 2;
        }
    });

    $.each(jsonSpread_M, function (index, obj) {
        if (obj.DotGain == 0 && index != 0 && index != jsonSpread_M.length - 1) {
            obj.DotGain = (jsonSpread_M[index - 1].DotGain + jsonSpread_M[index + 1].DotGain) / 2;
        }
    });

    $.each(jsonSpread_C, function (index, obj) {
        if (obj.DotGain == 0 && index != 0 && index != jsonSpread_C.length - 1) {
            obj.DotGain = (jsonSpread_C[index - 1].DotGain + jsonSpread_C[index + 1].DotGain) / 2;
        }
    });

    $divChartTVISpread.kendoChart({
        renderAs: "canvas",
        series: [
            {
                data: jsonSpread_C,
                type: "line",
                field: "DotGain",
                color: "Cyan",
                name: "C",
                markers: {
                    visible: function (e) {
                        return e.dataItem.markerDisplay;
                    },
                    background: "Cyan"
                },
                highlight: {
                    visible: false
                },
                tooltip: {
                    visible: true,
                    template: '<table><tr><td><center><b>' + _Resources._recCyan + ' #= dataItem.CmykC.toFixed() #% Patch Matrics</b></center><td></tr><tr><td> Actual TVI:  #= dataItem.DotGain #%</td></tr><tr><td> Spread Threshold:  #= kendo.toString(dataItem.DotGainSpreadTh, "n") #</td></tr><tr><td> #= dataItem.DifferentTitleOne#: #= dataItem.DiffrentOne.toFixed() #</td></tr><tr><td> #= dataItem.DifferentTitleTwo#: #= dataItem.DiffrentTwo.toFixed() #</td></tr></table>'
                }
            },
            {
                data: jsonSpread_M,
                type: "line",
                field: "DotGain",
                color: "Magenta",
                name: "M",
                markers: {
                    visible: function (e) {
                        return e.dataItem.markerDisplay;
                    },
                    background: "Magenta"
                },
                highlight: {
                    visible: false
                },
                tooltip: {
                    visible: true,
                    template: '<table><tr><td><center><b>' + _Resources._recMagenta + ' #= dataItem.CmykM.toFixed() #% Patch Matrics</b></center><td></tr><tr><td> Actual TVI:  #= dataItem.DotGain #%</td></tr><tr><td> Spread Threshold:  #= kendo.toString(dataItem.DotGainSpreadTh, "n") #</td></tr><tr><td> #= dataItem.DifferentTitleOne#: #= dataItem.DiffrentOne.toFixed() #</td></tr><tr><td> #= dataItem.DifferentTitleTwo#: #= dataItem.DiffrentTwo.toFixed() #</td></tr></table>'
                }
            },
            {
                data: jsonSpread_Y,
                type: "line",
                field: "DotGain",
                color: "Yellow",
                name: "Y",
                markers: {
                    visible: function (e) {
                        return e.dataItem.markerDisplay;
                    },
                    background: "Yellow"
                },
                highlight: {
                    visible: false
                },
                tooltip: {
                    visible: true,
                    template: '<table><tr><td><center><b>' + _Resources._recYellow + ' #= dataItem.CmykY.toFixed() #% Patch Matrics</b></center><td></tr><tr><td> Actual TVI:  #= dataItem.DotGain #%</td></tr><tr><td> Spread Threshold:  #= kendo.toString(dataItem.DotGainSpreadTh, "n") #</td></tr><tr><td> #= dataItem.DifferentTitleOne#: #= dataItem.DiffrentOne.toFixed() #</td></tr><tr><td> #= dataItem.DifferentTitleTwo#: #= dataItem.DiffrentTwo.toFixed() #</td></tr></table>'
                }
            },
            {
                data: splineArray,
                type: "rangeArea",
                fromField: "LowerLimit",
                toField: "UpperLimit",
                categoryField: "TVI",
                color: "#148787",
                line: {
                    style: "smooth",
                    color: "green",
                    width: 1,
                },
                highlight: {
                    visible: false,
                }
            },
        ],
        valueAxis: {
            name: "valueAxis",
            //majorUnit: 5
            axisCrossingValues: -100,
        },
        seriesDefaults: {
            line: {
                style: "smooth"
            }
        },
        categoryAxis: {
            name: "categoryAxis",
            justified: true,
            //categories: [0, 25, 50, 75, 100],
            field: "CmykC",
            //min: 0,
            //max: 100,
        },
        chartArea: {
            width: 650,
            height: 350
        },
        render: renderChartWithBackgroundByCategory,
        legend: {
            position: "top",
            item: {
                visual: function (e) {
                    var color = e.options.markers.background;
                    var labelColor = e.options.labels.color;

                    // Define the target dimensions for the legend item
                    var rect = new kendo.geometry.Rect([0, 0], [100, 50]);

                    // A layout will hold the checkbox and the default visual                    
                    var layout = new kendo.drawing.Layout(rect, {
                        spacing: 5,
                        alignItems: "center"
                    });
                    // Cheat a bit by rendering the checkbox using the Unicode ballot symbol
                    var cbSymbol = e.active ? "☑" : "☐";
                    var cb = new kendo.drawing.Text(cbSymbol, [0, 0], {
                        font: "16px sans-serif"
                    });

                    // This will give us the default box + text
                    var defaultVisual = e.createVisual();

                    // Reflow them together
                    layout.append(cb, defaultVisual);
                    layout.reflow()

                    return layout;
                }
            }
        },
    });
}

////**************** POPULATE THE TVI CHART  ****************////
function PopulateTVIG7Chart() {
    var jsonEmpty = $.grep(jsonTVIG7ChartList, function (element) { return element.CmykC == 0 && element.CmykM == 0 && element.CmykY == 0 && element.CmykK == 0; })

    var jsonC = $.grep(jsonTVIG7ChartList, function (element) { return element.CmykC != 0; });
    var jsonM = $.grep(jsonTVIG7ChartList, function (element) { return element.CmykM != 0; });
    var jsonY = $.grep(jsonTVIG7ChartList, function (element) { return element.CmykY != 0; });
    var jsonK = $.grep(jsonTVIG7ChartList, function (element) { return element.CmykK != 0; });

    var jsonCResult = jsonEmpty.concat(jsonC);
    var jsonMResult = jsonEmpty.concat(jsonM);
    var jsonYResult = jsonEmpty.concat(jsonY);
    var jsonKResult = jsonEmpty.concat(jsonK);

    var arrValues = $.map(jsonTVIG7ChartList, function (elem) { return elem.DotGain; });
    var minElement = Math.abs(Math.min.apply(Math, arrValues));
    var maxElement = Math.abs(Math.max.apply(Math, arrValues));
    var maxValues = 0;
    var minValues = 0;

    if (minElement > -10 && minElement < 0) {
        minValues = -10
    } else if (minElement >= -20 && minElement < -10) {
        minValues = -20;
    } else if (minElement >= -30 && minElement < -20) {
        minValues = -30;
    } else if (minElement >= -40 && minElement < -30) {
        minValues = -40;
    } else if (minElement >= -50 && minElement < -40) {
        minValues = -50;
    } else if (minElement >= -60 && minElement < -50) {
        minValues = -60;
    } else if (minElement >= -70 && minElement < -60) {
        minValues = -70;
    } else if (minElement >= -80 && minElement < -70) {
        minValues = -80;
    }

    if (maxElement >= 0 && maxElement < 10) {
        maxValues = 10
    } else if (maxElement >= 10 && maxElement < 20) {
        maxValues = 20;
    } else if (maxElement >= 20 && maxElement < 30) {
        maxValues = 30;
    } else if (maxElement >= 30 && maxElement < 40) {
        maxValues = 40;
    } else if (maxElement >= 40 && maxElement < 50) {
        maxValues = 50;
    } else if (maxElement >= 50 && maxElement < 60) {
        maxValues = 60;
    } else if (maxElement >= 60 && maxElement < 70) {
        maxValues = 70;
    } else if (maxElement >= 70 && maxElement < 80) {
        maxValues = 80;
    } else if (maxElement >= 80 && maxElement < 90) {
        maxValues = 90;
    } else {
        maxValues = 100;
    }

    $divChartTVISummaryDotGain.kendoChart({
        renderAs: "canvas",
        //legend: {
        //    position: "top",            
        //},
        seriesDefaults: {
            type: "scatterLine",
            style: "smooth",
            width: 2
        },
        //dataSource: jsonKResult,
        series: [
            {
                name: "C",
                data: jsonCResult,
                xField: "CmykC",
                yField: "DotGain",
                color: "Cyan",
                markers: {
                    background: "Cyan",
                    size: 6,
                    border: {
                        width: 1,
                        color: function (e) {
                            return 'black';
                        }
                    }
                },
                highlight: {
                    visible: false
                },
                tooltip: {
                    visible: true,
                    template: '<table><tr><td><center><b>' + _Resources._recCyan + ' #= dataItem.CmykC.toFixed() #% Patch Matrics</b></center><td></tr><tr><td> Dot Gain:  #= dataItem.DotGain #</td></tr></table>'
                }
            }, {
                name: "M",
                data: jsonMResult,
                xField: "CmykM",
                yField: "DotGain",
                color: "Magenta",
                markers: {
                    background: "Magenta", size: 6, border: {
                        width: 1,
                        color: function (e) {
                            return 'black';
                        }
                    }
                },
                tooltip: {
                    visible: true,
                    template: '<table><tr><td><center><b>' + _Resources._recMagenta + ' #= dataItem.CmykM.toFixed() #% Patch Matrics</b></center><td></tr><tr><td> Dot Gain:  #= dataItem.DotGain #</td></tr></table>'
                },
                highlight: {
                    visible: false
                },
            }, {
                name: "Y",
                data: jsonYResult,
                xField: "CmykY",
                yField: "DotGain",
                color: "Yellow",
                markers: {
                    background: "Yellow", size: 6, border: {
                        width: 1,
                        color: function (e) {
                            return 'black';
                        }
                    }
                },
                tooltip: {
                    visible: true,
                    template: '<table><tr><td><center><b>' + _Resources._recYellow + ' #= dataItem.CmykY.toFixed() #% Patch Matrics</b></center><td></tr><tr><td> Dot Gain:  #= dataItem.DotGain #</td></tr></table>'
                },
                highlight: {
                    visible: false
                },
            }, {
                name: "K",
                data: jsonKResult,
                xField: "CmykK",
                yField: "DotGain",
                color: "Black",
                markers: { background: "Black", size: 4 },
                tooltip: {
                    visible: true,
                    template: '<table><tr><td><center><b>' + _Resources._recBlack + ' #= dataItem.CmykK.toFixed() #% Patch Matrics</b></center><td></tr><tr><td> Dot Gain:  #= dataItem.DotGain #</td></tr></table>'
                },
                highlight: {
                    visible: false
                },
            },

        ],
        xAxis: {
            name: "xAxis",
            min: 0,
            max: 100,
            majorUnit: 25,
            axisCrossingValues: [0, 100],
            labels: {
                format: "{0}",
                padding: { top: 0 }
            },
        },
        yAxis: {
            name: "yAxis",
            min: minValues,
            max: maxValues,
            labels: {
                format: "{0}"
            },
        },
        chartArea: {
            width: 650,
            height: 400
        },
        plotArea: {
            border: {
                width: 2,
                color: "black"
            }
        },
        render: renderChartWithBackgroundByAxis,
        legend: {
            visible: true,
            position: "top",
            item: {
                visual: function (e) {

                    var color = e.options.markers.background;
                    var labelColor = e.options.labels.color;

                    // Define the target dimensions for the legend item
                    var rect = new kendo.geometry.Rect([0, 0], [100, 50]);

                    // A layout will hold the checkbox and the default visual                    
                    var layout = new kendo.drawing.Layout(rect, {
                        spacing: 5,
                        alignItems: "center"
                    });
                    // Cheat a bit by rendering the checkbox using the Unicode ballot symbol
                    var cbSymbol = e.active ? "☑" : "☐";
                    var cb = new kendo.drawing.Text(cbSymbol, [0, 0], {
                        font: "16px sans-serif"
                    });

                    // This will give us the default box + text
                    var defaultVisual = e.createVisual();

                    // Reflow them together
                    layout.append(cb, defaultVisual);
                    layout.reflow()

                    return layout;
                }
            }
        },
    });

    //load the chart again to adjust the xAxis lables according to negative values
    var chart = $divChartTVISummaryDotGain.data("kendoChart");
    //var yArray = chart._plotArea.axisY.children.map(r => parseInt(r.content));
    var yArray = $.map(chart._plotArea.axisY.children, function (elem) {
        return parseInt(elem.content);
    });
    var minValue = Math.abs(Math.min.apply(Math, yArray));
    if (minValue > 0) {
        var kendoOptions = chart.options;
        kendoOptions.xAxis.labels.padding.top = (minValue * 10);
        chart.options = kendoOptions.options;
        chart.redraw();
    }
}

////**************** GENERATE SCORE MEASUREMENT REPORT  ****************////
function GetGradingReport(reportDetail) {

    var measurementID = parseInt($hdnCurrentMeasurement.val());
    var apiParameter = {
        MeasurementId: measurementID,
        ProjectId: $hdnProjectID,
        IsAverageOfMeasurements: $isAverageOfMeasurements,
        IsVerificationProject: $hdnVerification,
        IsSync1DTypeProject: $hdnSync1DType,
        fromDeltaE: $selectedDeltaE,
        validationType: $hdnValidationType,
        CurrentUserId: CURRENT_USER_ID,
        ReportDLPName: $hdnReportDLPName,
        ABComparisionSpiderChart: '',
        InkPaperPatchChart: '',
        ABActualTargetChart: '',
        LComparisionChart: '',
        LGrayBalanceChart: '',
        AGrayBalanceChart: '',
        BGrayBalanceChart: '',
        DeltaSummaryChart: '',
        InkPaperLAB1Chart: '',
        InkPaperLAB2Chart: '',
        InkPaperDansityChart: '',
        TVI_ISO_CMYK_Chart: '',
        TVI_ISO_Cyan_Chart: '',
        TVI_ISO_Magenta_Chart: '',
        TVI_ISO_Yellow_Chart: '',
        TVI_ISO_Black_Chart: '',

        TVI_Summary_Chart: '',
        GrayBalanceDeltaCH_Chart: '',
        GrayBalanceDeltaL_Chart: '',
        BlackBalanceDeltaL_Chart: '',
        BlackNPDChart: '',
        GrayNPDChart: '',
        grading: reportDetail
    };

    apiParameter.ABComparisionSpiderChart = GetABComparisionSpiderChartImage();
    apiParameter.InkPaperPatchChart = GetInkPaperPatchChartImage();
    apiParameter.LGrayBalanceChart = Get_L_BalanceChartImage();
    apiParameter.AGrayBalanceChart = Get_A_BalanceChartImage();
    apiParameter.BGrayBalanceChart = Get_B_BalanceChartImage();
    apiParameter.InkPaperLAB1Chart = Get_InkPaperChartLAB_OneChartImage();
    apiParameter.InkPaperLAB2Chart = Get_InkPaperChartLAB_TwoChartImage();

    var ajaxParameter = {
        type: "POST",
        url: SERVICE_BASE_URL + 'api/Report/ExportReportGrading',
        data: apiParameter,
        dataType: "json",
        headers: {
            'Authorization': 'Basic ' + AUTH_TOKEN
        }
    };


    commonAjaxCall(ajaxParameter, function (response) {
        //kendo.ui.progress($('#divReport'), false);
        if (response.data != null) {
            //console.log(response.data);
            var fileName = '~' + response.data;
            window.location.href = SERVICE_BASE_URL + '/api/Analyze/DownloadFile?filePath=' + fileName;
        }
    }, function (XMLHttpRequest, textStatus, errorThrown) {
        if (XMLHttpRequest.status == 401) {
            ShowDialog("You are not authorized user. Please login again.", "Message");
        } else {
            alert(errorThrown);
        }
    });

}



////////////////// Analyze chart paging functions //////////////////

//Bind the page details of analyze chart paging
function BindPageNumber() {

    if (jsonAnalyzeList.Measurements.length > 0) {
        viewNumber = parseInt($ddlView.val());
        totalPages = jsonAnalyzeList.Measurements.length % viewNumber == 0 ? (jsonAnalyzeList.Measurements.length / viewNumber) : (jsonAnalyzeList.Measurements.length / viewNumber + 1);
        totalPages = totalPages <= 0 ? 1 : parseInt(totalPages);
        if (totalPages == 1) {
            SetPrevButtonDisabled(true);
            SetNextButtonDisabled(true);
        } else {
            pageNumber = totalPages;
            SetPrevButtonDisabled(false);
            SetNextButtonDisabled(true);
        }
        $ddlPaging.empty();
        for (var index = 1; index <= totalPages; index++) {
            $ddlPaging.append($('<option>', {
                value: index,
                text: index
            }));
        }
        $ddlPaging.find('option').last().attr('selected', true);
        if ($hdnCurrentDlpSubOperation != ELicenseType.DestinationProfile) {
            PopulateAnalyzeChart();
        }
        else {
            PopulateAnalyzeDestinationChart();
        }
    }
}

//Method to set enable/disable previous button in treand line chart
function SetPrevButtonDisabled(enabled) {
    $btnPrevious.prop('disabled', enabled);
    $btnFirst.prop('disabled', enabled);
}

//Method to set enable/disable next button in treand line chart
function SetNextButtonDisabled(enabled) {
    $btnNext.prop('disabled', enabled);
    $btnLast.prop('disabled', enabled);
}

/////////******************************** METHODS FOR BIND CHART BACKGROUNDS ********************************////////////

//Method to set the background in XY charts
function renderChartWithBackgroundByAxis(e) {

    var xSlot = {};
    var ySlot = {};
    var path = {};
    var chart = e.sender;

    if (chart.element[0].id != "kendoChartTargetVersusActualAB" && chart.element[0].id != "kendoChartGamut_ABComparison") {



        var xAxis = chart.getAxis("xAxis");
        var yAxis = chart.getAxis("yAxis");
        //var xAxisArray = xAxis._axis.children.map(r => parseFloat(r.content));
        //var yAxisArray = yAxis._axis.children.map(r => parseFloat(r.content));
        var xAxisArray = $.map(xAxis._axis.children, function (elem) {
            return parseFloat(elem.content);
        });
        var yAxisArray = $.map(yAxis._axis.children, function (elem) {
            return parseFloat(elem.content);
        });

        if (chart.element[0].id == "kendoChartTargetVersusActualABDummy"
            || chart.element[0].id == "kendoChartGamut_ABComparisonDummy" || chart.element[0].id == "kendoChartABalanceChart"
            || chart.element[0].id == "kendoChartBBalanceChart" || chart.element[0].id == "kendoChartLComaparision"
            || chart.element[0].id == "kendoChartInkPaperLabOneDummy" || chart.element[0].id == "kendoChartDummyBalanceL"
            || chart.element[0].id == "kendoChartDummyBalanceA" || chart.element[0].id == "kendoChartDummyBalanceB" || chart.element[0].id == "kendoChart_A_Balance_Large"
            || chart.element[0].id == "kendoChart_L_ComparisionLarge" || chart.element[0].id == "kendoChart_B_Balance_Large") {
            xSlot = xAxis.slot(Math.min.apply(Math, xAxisArray), Math.max.apply(Math, xAxisArray));
            ySlot = yAxis.slot(Math.min.apply(Math, yAxisArray), Math.max.apply(Math, yAxisArray));


        }
        else {
            xSlot = xAxis.slot(Math.abs(Math.min.apply(Math, xAxisArray)), Math.abs(Math.max.apply(Math, xAxisArray)));
            ySlot = yAxis.slot(Math.abs(Math.min.apply(Math, yAxisArray)), Math.abs(Math.max.apply(Math, yAxisArray)));
        }


        //set the rectangle area on chart
        var rect = new geometry.Rect([
            xSlot.origin.x, ySlot.origin.y
        ], [
            xSlot.width(), ySlot.height()
        ]);


        if (chart.element[0].id == "kendoChartTargetVersusActualABDummy" || chart.element[0].id == "kendoChartGamut_ABComparisonDummy" || chart.element[0].id == "kendoChartInkPaperLabOneDummy") {
            path = drawing.Path.fromRect(rect, {
                stroke: null,
                fill: gradientRedGreen
            });
        }
        else if (chart.element[0].id == "kendoChartABalanceChart" || chart.element[0].id == "kendoChartDummyBalanceA" || chart.element[0].id == "kendoChart_A_Balance_Large") {
            path = drawing.Path.fromRect(rect, {
                stroke: null,
                fill: gradientPinkGreen
            });
        }
        else if (chart.element[0].id == "kendoChartLBalanceChart" || chart.element[0].id == "kendoChartDummyBalanceL" || chart.element[0].id == "kendoChart_L_ComparisionLarge") {
            path = drawing.Path.fromRect(rect, {
                stroke: null,
                fill: gradientGray3
            });
        }
        else if (chart.element[0].id == "kendoChartLComaparision") {
            path = drawing.Path.fromRect(rect, {
                stroke: null,
                fill: gradientGray2
            });
        }
        else if (chart.element[0].id == "kendoChartBBalanceChart" || chart.element[0].id == "kendoChartDummyBalanceB" || chart.element[0].id == "kendoChart_B_Balance_Large") {
            path = drawing.Path.fromRect(rect, {
                stroke: null,
                fill: gradientYellowBlue
            });
        }
        else {
            path = drawing.Path.fromRect(rect, {
                stroke: null,
                fill: gradientGray
            });
        }

        chart.surface.draw(path);
    }
}

//Method to set the background for category chart
function renderChartWithBackgroundByCategory(e) {
    var chart = e.sender;

    //get value axis object
    var valueAxis = chart.getAxis("valueAxis");
    //get maximum value from the value axis
    //var maxValue = Math.abs(Math.max.apply(Math, valueAxis._axis.children.map(r => parseFloat(r.content))))
    var maxValueArray = $.map(valueAxis._axis.children, function (elem) {
        return elem.content;
    });
    var maxValue = Math.abs(Math.max.apply(Math, maxValueArray))
    //get top slot
    var valueSlot = valueAxis.slot(maxValue);

    // Locate right-most category slot                
    var categoryAxis = chart.getAxis("categoryAxis");
    var lastCategoryIndex = Math.max(1, categoryAxis.range().max);
    var minCategorySlot = categoryAxis.slot(0);
    var maxCategorySlot = categoryAxis.slot(lastCategoryIndex);

    //calculate height and width
    var plotWidth = maxCategorySlot.origin.x - valueSlot.origin.x;
    var plotHeight = maxCategorySlot.origin.y - valueSlot.origin.y;

    //create ractangle
    var rect = new geometry.Rect([
        // Origin X, Y
        valueSlot.origin.x, valueSlot.origin.y
    ], [plotWidth, plotHeight]);

    //create path using gradient
    var path = drawing.Path.fromRect(rect, {
        stroke: null,
        fill: gradientGray
    });

    //set the path
    chart.surface.draw(path);
}

//Method to set the background for category paper L chart
function renderChartWithBackgroundByCategoryForPaperL(e) {
    var chart = e.sender;
    var path = {};
    //get value axis object
    var valueAxis = chart.getAxis("valueAxis");
    //get maximum value from the value axis
    //var maxValue = Math.abs(Math.max.apply(Math, valueAxis._axis.children.map(r => parseFloat(r.content))))
    var maxValueArray = $.map(valueAxis._axis.children, function (elem) {
        return elem.content;
    });

    var minValue = Math.min.apply(Math, maxValueArray);
    var maxValue = Math.abs(Math.max.apply(Math, maxValueArray));
    //get top slot
    var valueMaxSlot = valueAxis.slot(maxValue);
    var valueMinSlot = valueAxis.slot(minValue);

    // Locate right-most category slot                
    var categoryAxis = chart.getAxis("categoryAxis");
    var lastCategoryIndex = Math.max(1, categoryAxis.range().max);
    var minCategorySlot = categoryAxis.slot(0);
    var maxCategorySlot = categoryAxis.slot(lastCategoryIndex);

    //calculate height and width
    var plotWidth = maxCategorySlot.origin.x - valueMaxSlot.origin.x;
    var plotHeight = valueMinSlot.origin.y - valueMaxSlot.origin.y;

    //create ractangle
    var rect = new geometry.Rect([
        // Origin X, Y
        valueMaxSlot.origin.x, valueMaxSlot.origin.y
    ], [plotWidth, plotHeight]);

    //create path using gradient
    path = drawing.Path.fromRect(rect, {
        stroke: null,
        fill: gradientGray2
    });

    //set the path
    chart.surface.draw(path);
}




/////////******************************** METHODS FOR BIND CHART IMAGES FOR REPORT GENERATIONS ********************************////////////

////**************** GET IMAGE OF AB COMPARSION SPIDER CHART FOR REPORT  ****************////
function GetABComparisionSpiderChartImage() {
    //var chart = $chart_TargetVersusActualAB.data("kendoChart");
    //return chart.imageDataURL();

    var chart = $chart_TargetVersusActualAB.data("kendoChart");
    var kendoOptions = chart.options;
    var kendodataSource = chart.dataSource;
    $('#kendoChartTargetVersusActualABDummy').kendoChart(kendoOptions)
    var chartDummy = $('#kendoChartTargetVersusActualABDummy').data('kendoChart')
    chartDummy.dataSource = kendodataSource;
    //delete chartDummy.options.chartArea.background;
    chartDummy.options.transitions = false;
    chartDummy.refresh()
    return chartDummy.imageDataURL();
}

////**************** GET IMAGE OF INK PAPER CHART FOR REPORT  ****************////

function GetInkPaperPatchChartImage() {
    var chart = $divChartInkPaperABComparision.data("kendoChart");
    var kendoOptions = $divChartInkPaperABComparision.data("kendoChart").options;
    var kendodataSource = $divChartInkPaperABComparision.data("kendoChart").dataSource;
    //kendoOptions.categoryAxis.labels.visible = false;    
    $('#divChartInkPaperDummy').kendoChart(kendoOptions);
    var dummyChart = $('#divChartInkPaperDummy').data('kendoChart');
    dummyChart.options.categoryAxis.labels.visible = false;
    dummyChart.dataSource = kendodataSource;
    dummyChart.options.transitions = false;
    dummyChart.refresh();
    return dummyChart.imageDataURL();

}

////**************** GET IMAGE OF ACTUAL VS TARGET CHART FOR REPORT  ****************////
function GetActualVsTargetChartImage() {
    //var chart = $chart_Gamut_ABComparison.data("kendoChart");
    //return chart.imageDataURL();

    var chart = $chart_Gamut_ABComparison.data("kendoChart");
    var kendoOptions = chart.options;
    var kendodataSource = chart.dataSource;
    $('#kendoChartGamut_ABComparisonDummy').kendoChart(kendoOptions)
    var chartDummy = $('#kendoChartGamut_ABComparisonDummy').data('kendoChart')
    chartDummy.dataSource = kendodataSource;
    //delete chartDummy.options.chartArea.background;
    chartDummy.options.transitions = false;
    chartDummy.refresh()
    return chartDummy.imageDataURL();
}

////**************** GET IMAGE OF L-COMPARSION CHART FOR REPORT  ****************////
function GetLComparisionChartImage() {
    var chart = $chart_L_Comaparision.data("kendoChart");
    return chart.imageDataURL();
}

////**************** GET IMAGE OF L-BALANCE CHART FOR REPORT  ****************////
function Get_L_BalanceChartImage() {
    if ($hdnValidationType == 2) {

        if ($hdnSync1DType == 'True') {

            var kendoOptions = $chart_L_BalanceChart.data("kendoChart").options;
            var kendodataSource = $chart_L_BalanceChart.data("kendoChart").dataSource;
            $('#kendoChartDummyBalanceL').kendoChart(kendoOptions);
            var dummyChart = $('#kendoChartDummyBalanceL').data('kendoChart');
            dummyChart.dataSource = kendodataSource;
            dummyChart.options.chartArea.width = 600;
            dummyChart.options.chartArea.height = 250;
            dummyChart.options.transitions = false;
            dummyChart.refresh();
            return dummyChart.imageDataURL();
        }
    }
    var chart = $chart_L_BalanceChart.data("kendoChart");
    return chart.imageDataURL();
}

////**************** GET IMAGE OF A-BALANCE CHART FOR REPORT  ****************////
function Get_A_BalanceChartImage() {

    if ($hdnValidationType == 2) {

        if ($hdnSync1DType == 'True') {

            var kendoOptions = $chart_A_BalanceChart.data("kendoChart").options;
            var kendodataSource = $chart_A_BalanceChart.data("kendoChart").dataSource;
            $('#kendoChartDummyBalanceA').kendoChart(kendoOptions);
            var dummyChart = $('#kendoChartDummyBalanceA').data('kendoChart');
            dummyChart.dataSource = kendodataSource;
            dummyChart.options.chartArea.width = 600;
            dummyChart.options.chartArea.height = 250;
            dummyChart.options.transitions = false;
            dummyChart.refresh();
            return dummyChart.imageDataURL();
        }
    }
    var chart = $chart_A_BalanceChart.data("kendoChart");
    return chart.imageDataURL();
}

////**************** GET IMAGE OF B-BALANCE CHART FOR REPORT  ****************////
function Get_B_BalanceChartImage() {

    if ($hdnValidationType == 2) {

        if ($hdnSync1DType == 'True') {

            var kendoOptions = $chart_B_BalanceChart.data("kendoChart").options;
            var kendodataSource = $chart_B_BalanceChart.data("kendoChart").dataSource;
            $('#kendoChartDummyBalanceB').kendoChart(kendoOptions);
            var dummyChart = $('#kendoChartDummyBalanceB').data('kendoChart');
            dummyChart.dataSource = kendodataSource;
            dummyChart.options.chartArea.width = 600;
            dummyChart.options.chartArea.height = 250;
            dummyChart.options.transitions = false;
            dummyChart.refresh();
            return dummyChart.imageDataURL();
        }
    }

    var chart = $chart_B_BalanceChart.data("kendoChart");
    return chart.imageDataURL();
}

////**************** GET IMAGE OF INK PAPER CHART LAB ONE CHART FOR REPORT  ****************////
function Get_InkPaperChartLAB_OneChartImage() {
    //var chart = $divChartInkPaperPaperLab1.data("kendoChart");
    //return chart.imageDataURL();

    var chart = $divChartInkPaperPaperLab1.data("kendoChart");
    var kendoOptions = chart.options;
    var kendodataSource = chart.dataSource;
    $('#kendoChartInkPaperLabOneDummy').kendoChart(kendoOptions)
    var chartDummy = $('#kendoChartInkPaperLabOneDummy').data('kendoChart')
    chartDummy.dataSource = kendodataSource;
    //delete chartDummy.options.chartArea.background;
    chartDummy.options.transitions = false;
    chartDummy.refresh()
    return chartDummy.imageDataURL();
}

////**************** GET IMAGE OF INK PAPER CHART LAB TWO CHART FOR REPORT  ****************////
function Get_InkPaperChartLAB_TwoChartImage() {
    var chart = $divChartInkPaperPaperLab2.data("kendoChart");
    return chart.imageDataURL();
}

////**************** GET IMAGE OF INK PAPER DANSITY CHART FOR REPORT  ****************////
function Get_InkPaperChartDansityChartImage() {
    var chart = $divChartInkPaperDansityChart.data("kendoChart");
    return chart.imageDataURL();
}


////**************** GET IMAGE OF TVI ISO CMYK CHART FOR REPORT  ****************////
function Get_TVI_ISO_CMYK_ChartImage() {
    var chart = $divChartTVIISO_CMYK.data("kendoChart");
    return chart.imageDataURL();
}


////**************** GET IMAGE OF TVI ISO CYAN CHART FOR REPORT  ****************////
function Get_TVI_ISO_CYAN_ChartImage() {
    var chart = $divChartTVIISO_CYAN.data("kendoChart");
    return chart.imageDataURL();
}

////**************** GET IMAGE OF TVI ISO MAGENTA CHART FOR REPORT  ****************////
function Get_TVI_ISO_MAGENTA_ChartImage() {
    var chart = $divChartTVIISO_Magenta.data("kendoChart");
    return chart.imageDataURL();
}

////**************** GET IMAGE OF TVI ISO YELLOW CHART FOR REPORT  ****************////
function Get_TVI_ISO_YELLOW_ChartImage() {
    var chart = $divChartTVIISO_Yellow.data("kendoChart");
    return chart.imageDataURL();
}

////**************** GET IMAGE OF TVI ISO BLACK CHART FOR REPORT  ****************////
function Get_TVI_ISO_BLACK_ChartImage() {
    var chart = $divChartTVIISO_Black.data("kendoChart");
    return chart.imageDataURL();
}


////**************** GET IMAGE OF TVI SUMMARY DOT GAIN CHART FOR REPORT  ****************////
function Get_TVISummaryDotGainChartImage() {
    var chart = $divChartTVISummaryDotGain.data("kendoChart");
    return chart.imageDataURL();
}

////**************** GET IMAGE OF GRAY BALANCE DELTA CH CHART FOR REPORT  ****************////
function Get_GrayBalanceDelta_CH_ChartImage() {
    var chart = $divChartG7GrayBalanceDeltaCh.data("kendoChart");
    return chart.imageDataURL();
}

////**************** GET IMAGE OF BLACK DELTA L CHART FOR REPORT  ****************////
function Get_BlackDelta_L_ChartImage() {
    var chart = $divChartG7BlackDeltaL.data("kendoChart");
    return chart.imageDataURL();
}

////**************** GET IMAGE OF GRAY DELTA L CHART FOR REPORT  ****************////
function Get_GrayDelta_L_ChartImage() {

    var kendoOptions = $divChartG7GrayDeltaL.data("kendoChart").options;
    var kendodataSource = $divChartG7GrayDeltaL.data("kendoChart").dataSource;
    kendoOptions.legend.visible = false;
    $kendoChartDummy.kendoChart(kendoOptions);
    var dummyChart = $kendoChartDummy.data('kendoChart');
    dummyChart.dataSource = kendodataSource;
    dummyChart.options.transitions = false;
    dummyChart.refresh();
    return dummyChart.imageDataURL();
}

////**************** GET IMAGE OF G7 BLACK NPD CHART FOR REPORT  ****************////
function Get_Black_NPD_ChartImage() {
    var chart = $divChartG7BlackNPD.data("kendoChart");
    return chart.imageDataURL();
}

////**************** GET IMAGE OF G7 GRAY NPD CHART FOR REPORT  ****************////
function Get_Gray_NPD_ChartImage() {
    var chart = $divChartG7GrayNPD.data("kendoChart");
    return chart.imageDataURL();
}

////**************** GET IMAGE OF DELTA SUMMARY CHART FOR REPORT  ****************////
function Get_DeltaSummaryChartImage() {

    var chart = $divChartSummaryDeltaChart.data("kendoChart");
    return chart.imageDataURL();
}

////**************** METHOD TO CHECK AVG COMPATIBILITY  ****************////

//check average compatibility for measurement
function CheckAvgCompability() {
    var answer = document.getElementById('ContentPlaceHolder1_ucAnalyze_hd_CompError').value;
    if (answer == "0") {
        //BindMeasurementSummary();
        BindAllTabs();
        UpdateAvgMeasurementSummary();
        return true;
    }
    else if (answer == "1") {
        jQuery("[id*='lblCompError']").html("Error in different ReferenceCharts, Please select only measurements which used same reference charts.");
    }
    else if (answer == "2") {
        jQuery("[id*='lblCompError']").html("Error in different MetricTargetPresets, Please select only measurements which used same metric target presets.");
    }
    return false;
}

function UpdateAvgMeasurementSummary() {
    var answer = document.getElementById('ContentPlaceHolder1_ucAnalyze_hd_CompError').value;
    if (answer == "0") {
        var tabStrip = $("#tabstrip").kendoTabStrip().data("kendoTabStrip");
        $isAverageOfMeasurements = $('#chkAnalyzeAverageOfMeasurements').is(':checked');
        if ($isAverageOfMeasurements) {
            if (tabStrip.select().index() == 5) {
                tabStrip.select(0);
            }
            //Hide default tabs patch details
            $(tabStrip.items()[5]).hide();

            //show average measurement summary tab
            $('#tblValidationSummary').css('display', 'none');
            $('#tblAverageMeasurementSummary').css('display', 'block');
        }
        else {
            //Show default tabs patch details
            //$(tabStrip.items()[5]).show();
            $(tabStrip.items()[5]).css('display', 'inline-block');
            $('#tblAverageMeasurementSummary').css('display', 'none');
            //$('#tblValidationSummary').css('display', 'block');
            $('#tblValidationSummary').show();
        }

        //freez the all markers of kendo chart
        var chart = $divAnalyzeChart.data('kendoChart');
        if (chart.options.series.length > 0) {
            $.each(chart.options.series, function (i, item) {
                item.tooltip.visible = !$isAverageOfMeasurements
                item.highlight.visible = !$isAverageOfMeasurements
            });
            chart.refresh();
        }
    }
}

//removes duplicate values from array
function arrayUnique(array) {
    var a = array.concat();
    for (var i = 0; i < a.length; ++i) {
        for (var j = i + 1; j < a.length; ++j) {
            if (a[i] == a[j])
                a.splice(j--, 1);
        }
    }

    return a;
}

//copy object into other
function CopyObject(sourceObj, destObj) {
    Object.keys(sourceObj).forEach(function (k) {
        destObj[k] = sourceObj[k];
    });
}

//set the measurement detail for dymo label printing
function PopulateDymoPrinter(obj) {

    var apiParameter = {
        LicenseType: $hdnCurrentProjectLicenseType,
        //LicenseType: 2,
        DeviceName: $('#hdnCurrentDeviceName').val(),
        MeasurementSummary: obj
    };

    var ajaxParameter = {
        type: "POST",
        url: SERVICE_BASE_URL + 'api/Analyze/GetDymoPrintData',
        data: JSON.stringify(apiParameter),
        dataType: "json",
        headers: {
            "content-type": "application/json",
            'Authorization': 'Basic ' + AUTH_TOKEN
        }
    };

    commonAjaxCall(ajaxParameter, function (response) {
        if (response.data != null) {
            labelPrintXMLString = response.data.labelPrintString;
            labelPrintStatusIconString = response.data.dymoStatusIcon;
        }
    }, function (XMLHttpRequest, textStatus, errorThrown) {
        if (XMLHttpRequest.status == 401) {
            ShowDialog("You are not authorized user. Please login again.", "Message");
        } else {
            alert(errorThrown);
        }
    });

}



//set the measurement detail for dymo label printing
function PopulateDymoPrinterNew(obj) {

    var apiParameter = {
        LicenseType: $hdnCurrentProjectLicenseType,
        //LicenseType: 2,
        DeviceName: $('#hdnCurrentDeviceName').val(),
        MeasurementSummary: obj
    };

    var ajaxParameter = {
        type: "POST",
        url: SERVICE_BASE_URL + 'api/Analyze/GetDymoPrintDataNew',
        data: JSON.stringify(apiParameter),
        dataType: "json",
        headers: {
            "content-type": "application/json",
            'Authorization': 'Basic ' + AUTH_TOKEN
        }
    };

    commonAjaxCall(ajaxParameter, function (response) {
        if (response.data != null) {
            labelPrintHTMLString = response.data.labelPrintHtmlStringNew;
        }
    }, function (XMLHttpRequest, textStatus, errorThrown) {
        if (XMLHttpRequest.status == 401) {
            ShowDialog("You are not authorized user. Please login again.", "Message");
        } else {
            alert(errorThrown);
        }
    });
}
//method to get label string to print in dymo label
function GetLabelXml() {
    return labelPrintXMLString;
}
//method to get label string to print in dymo label
function GetLabelHTML() {
    return labelPrintHTMLString;
}
var printPreviewWindow = null;
function ShowLabelPrintPreviewNew() {
    if (printPreviewWindow != null) {
        printPreviewWindow.close()
        printPreviewWindow = null;
    }
    printPreviewWindow = window.open("PrintPreviewNew.aspx", "PrintPreviewWindow", "width=768,height=650,resizable=no,scrollbars=no,menubar=yes");
    printPreviewWindow.focus();
}
//method to get icon image in label print dymo label
function GetStatusIconImageString() {
    return labelPrintStatusIconString;
}

function getPathForReportGrading() {
    return reportGradingPath;
}
function getPathForCMYKDensityReport() {
    return reportCMYKDensityPath;
}

Math.trunc = Math.trunc || function (x) {
    if (isNaN(x)) {
        return NaN;
    }
    if (x > 0) {
        return Math.floor(x);
    }
    return Math.ceil(x);
};
