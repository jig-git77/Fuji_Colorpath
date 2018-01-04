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
var measurementNamesList = [];
var _Resources = {};

var CURRENT_USER_ID
var SERVICE_BASE_URL = '';
var $hdnBaseServiceUrl;
var $hdnCurrentUserID;
var $btnGeneratePDF;
var $imgEyeABCompLarge;
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
var $divChartTVIISO_CYAN;
var $divChartTVIISO_Magenta;
var $divChartTVIISO_Yellow;
var $divChartTVIISO_Black;
var $divChartTVIISO_CMYK;
var $tblDensityChart;
var $hdnReportDLPName;

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


$(document).ready(function() {

    ////*********** HTML Element Declarations ***********////
    $hdnBaseServiceUrl = $('#hdnBaseServiceUrl').val();
    $hdnCurrentUserID = $('#hdnCurrentUserID').val();
    $CurrentCulture = $("#hdfCurrentCulture").val();
    $hdnVerification = $('#hdnVerification').val();
    $hdnSync1DType = $('#hdnSync1DType').val();
    $hdnValidationType = isNaN(parseInt($('#hdnValidationType').val())) ? 0 : parseInt($('#hdnValidationType').val());
    $hdnCurrentDlpSubOperation = parseInt($('#hdnCurrentDlpSubOperation').val());
    $hdnReportDLPName = $('#hdnReportDLPName').val();
    $isAverageOfMeasurements = $('#ContentPlaceHolder1_ucAnalyze_chkAnalyzeAverageOfMeasurements').is(':checked');
    $hdMeasurement = $('#ContentPlaceHolder1_ucAnalyze_hdMeasurement').val();
    $hdnProjectID = $('#hdnProjectID').val();
    $hdnOptimizationRunID = $('#hdnOptimizationRunID').val();
    $chartLoader = $(".chart-loading");
    $imgEyeABCompLarge = $('#imgEyeABCompLarge');
    $btnGeneratePDF = $('#btnGeneratePDF');
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
    $tblDensityChart = $("#tblDensityChart");
    $selectedDeltaE = $rbtnListDeltaE.find('input:checked').val();
    CURRENT_USER_ID = parseInt($hdnCurrentUserID);
    SERVICE_BASE_URL = $hdnBaseServiceUrl;

    var measurementID = parseInt($hdMeasurement);

    if (!$selectedDeltaE) {//set default value for sync1D projects
        $selectedDeltaE = 'E2000';
    }

    BindAnalyzeChart(); //Bind analyze chart

    var apiParameters = {
        MeasurementId: measurementID,
        IsAverageOfMeasurements: $isAverageOfMeasurements,
        IsVerificationProject: $hdnVerification,
        IsSync1DTypeProject: $hdnSync1DType,
        //fromDeltaE: $selectedDeltaE,
        //validationType: $hdnValidationType
    };

    kendo.ui.progress($chartLoader, true);
    $.ajax({
        //url: SERVICE_BASE_URL + 'api/Chart/GenerateAnalyzeCharts',
        url: SERVICE_BASE_URL + 'api/Chart/GenerateGrayChart',
        //data: { MeasurementId: measurementID, IsAverageOfMeasurements: $isAverageOfMeasurements, IsVerificationProject: $hdnVerification, IsSync1DTypeProject: $hdnSync1DType },
        data: apiParameters,
        type: 'GET',
        dataType: 'json',
        headers: {
            'Accept-Language': $CurrentCulture
        },
        success: function(response) {
            //console.log(response);
            jsonTarget = response.data.GamutChartActual;
            jsonColors = response.data.GamutChart;
            jsonGray = response.data.LABChart;
            _Resources = response.data.GlobalResources;
            BindChartTargetVersusActualAB();
            BindChartGamutABComparision();
            BindChartLComparision();
            BindChartLGrayBalance();
            kendo.ui.progress($chartLoader, false);

        },
        error: function(xhr, errorType, exception) {
            //alert(exception);
            kendo.ui.progress($chartLoader, false);
        }
    });


    BindInkPaperChartABComparision();

    BindMeasurementPatchDetails();
    if ($hdnValidationType == 1) {
        BindG7Charts();
    }

    if ($hdnCurrentDlpSubOperation == ELicenseType.Sync1D || $hdnCurrentDlpSubOperation == ELicenseType.Validation) {

        BindTVICharts();
    }

    BindDeltaSummaryStatesChart();

    $imgEyeABCompLarge.on('click', function(e) {
        e.preventDefault();

        var kendoOptions = $chart_TargetVersusActualAB.data("kendoChart").options;
        kendoOptions.chartArea.width = 580;
        kendoOptions.chartArea.height = 480;

        $chartTargetVersusActualABLarge.kendoChart(kendoOptions);
        //$chartTargetVersusActualABLarge.render = renderChartWithBackgroundByAxis;

        $chartTargetVersusActualABLargeArea.kendoWindow({
            width: "600px",
            modal: true,
            height: '500px',
            resizable: false,
            title: "a* - b* Comparison Chart",
            //content: url,
            visible: false
        });

        var popup = $chartTargetVersusActualABLargeArea.data('kendoWindow');
        popup.open();
        popup.center();
    });

    $btnGeneratePDF.on('click', function(e) {
        e.preventDefault();

        var chartContent = {
            CurrentUserId: CURRENT_USER_ID,
            ABComparisionSpiderChart: '',
            ABActualTargetChart: '',
            LComparisionChart: '',
            LGrayBalanceChart: '',
            AGrayBalanceChart: '',
            BGrayBalanceChart: ''
        };

        //chartContent.ABComparisionSpiderChart = GetABComparisionSpiderChartImage();
        //chartContent.ABActualTargetChart = GetActualVsTargetChartImage();
        //chartContent.LComparisionChart = GetLComparisionChartImage();
        //chartContent.LGrayBalanceChart = Get_L_BalanceChartImage();
        //chartContent.AGrayBalanceChart = Get_A_BalanceChartImage();
        //chartContent.BGrayBalanceChart = Get_B_BalanceChartImage();

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
            DeltaSummaryChart: ''
        };

        apiParameter.ABComparisionSpiderChart = GetABComparisionSpiderChartImage();
        apiParameter.InkPaperPatchChart = GetInkPaperPatchChartImage();
        apiParameter.DeltaSummaryChart = Get_DeltaSummaryChartImage();
        //apiParameter.ABActualTargetChart = GetActualVsTargetChartImage();
        //apiParameter.LComparisionChart = GetLComparisionChartImage();
        //apiParameter.LGrayBalanceChart = Get_L_BalanceChartImage();
        //apiParameter.AGrayBalanceChart = Get_A_BalanceChartImage();
        //apiParameter.BGrayBalanceChart = Get_B_BalanceChartImage();

        $.ajax({
            url: SERVICE_BASE_URL + 'api/Report/ExportPrepressSingleMeasurementReportTest',
            //url: SERVICE_BASE_URL + 'api/Report/ExportChartReport',
            //data: {},
            //data: JSON.stringify(chartContent),
            data: JSON.stringify(apiParameter),
            type: 'POST',
            //dataType: 'json',
            headers: {
                "content-type": "application/json",
            },
            success: function(response) {
                //console.log(response);
                window.open(SERVICE_BASE_URL + response.data, "_blank");
            },
            error: function(xhr, errorType, exception) {
                alert(exception);

            }
        });
    });
});


////************************ CHART BINDINGS  ***********************////

function BindAnalyzeChart() {

    var apiParameters = {
        ProjectId: $hdnProjectID,
        OptimizeRunId: $hdnOptimizationRunID,
        IsSync1DTypeProject: $hdnSync1DType,
        fromDeltaE: $selectedDeltaE,
        ProjectType: $hdnCurrentDlpSubOperation
    };

    $.ajax({
        url: SERVICE_BASE_URL + 'api/Chart/GenerateAnalyzeChart',
        data: apiParameters,
        type: 'GET',
        dataType: 'json',
        headers: {
            'Accept-Language': $CurrentCulture
        },
        success: function(response) {
            if (response.data != null) {
                jsonAnalyzeList = response.data;
                PopulateAnalyzeChart();
            }
        },
        error: function(xhr, errorType, exception) {
            kendo.ui.progress($chartLoader, false);
        }
    });
}

////**************** START BIND THE AB COMPARISION CHART  ****************////
function BindChartTargetVersusActualAB() {


    //************* Get object from array ***************//
    var paperObj = $.grep(jsonTarget, function(element) { return element.PatchType == "Paper"; })[0];

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

    $.each(jsonTarget, function(index, element) {

        var seriesArray = [];



        //Get colors from the colors array ()
        var colorsArray = $.grep(jsonColors, function(colorObj) { return colorObj.PatchType == element.PatchType });
        if (colorsArray.length > 0) {
            seriesArray.push(targetPAPERObj);
            $.each(colorsArray, function(i, colorObj) {
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
                color: function(e) {
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

        var tooltipString = '<table><tr><td><center><b>' + tooltipCaption + '</b></center><td></tr>';
        tooltipString += '<tr><td>' + _Resources._recActual + ' ' + _Resources._recL + ': ' + element.CielabL.toFixed(2) + ' ' + _Resources._recA + ': ' + element.CielabA.toFixed(2) + ' ' + _Resources._recB + ': ' + element.CielabB.toFixed(2) + '</td></tr>';
        tooltipString += '<tr><td>' + _Resources._recTarget + ' ' + _Resources._recL + ': ' + element.TargetL.toFixed(2) + ' ' + _Resources._recA + ': ' + element.Targeta.toFixed(2) + ' ' + _Resources._recB + ': ' + element.Targetb.toFixed(2) + '</td></tr>';
        tooltipString += '<tr><td>' + _Resources._recDeltaL + '   ' + element["ΔL"].toFixed(2) + '</td></tr>';
        tooltipString += '<tr><td>' + _Resources._recDeltaA + '   ' + element["Δa"].toFixed(2) + '</td></tr>';
        tooltipString += '<tr><td>' + _Resources._recDeltaB + '   ' + element["Δb"].toFixed(2) + '</td></tr>';
        tooltipString += '</table>';

        kendoSeriseObj.tooltip = { visible: true, template: tooltipString, shared: true };

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

////**************** START BIND THE AB COMPARISION GAMUT CHART  ****************////
function BindChartGamutABComparision() {

    var targetCYANPoint = {};
    var targetMAGENTAPoint = {};
    var targetYELLOWPoint = {};
    var targetREDPoint = {};
    var targetGREENPoint = {};
    var targetBLUEPoint = {};

    var arrTargetsABComparision = [];
    var arrActualABComparision = [];
    var colorOrderArray = ["Cyan", "Green", "Yellow", "Red", "Magenta", "Blue", "Cyan"]

    $.each(colorOrderArray, function(index, colorObj) {

        //set target points
        var jsonTargetColor = $.grep(jsonTarget, function(targetObj) { return targetObj.PatchType == colorObj })[0];
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
                color: function(e) {
                    return 'black';
                }
            }
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
    var paperObj = $.grep(jsonTarget, function(element) { return element.PatchType == "Paper"; })[0];
    var targetPAPERObject = [[paperObj.Targeta, paperObj.Targetb]];
    var colorPAPERObject = [[paperObj.CielabA, paperObj.CielabB]];


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
        }, {
            data: colorPAPERObject,
            color: "Black",
            markers: { background: "White", size: 10 },
            highlight: {
                visible: false
            }
        }, targetCYANPoint,
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

////**************** START BIND THE L COMPARISION CHART  ****************////
function BindChartLComparision() {

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

    $.each(jsonTarget, function(index, element) {

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
                color: function(e) {
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
        tooltipString += '<tr><td>' + _Resources._recTarget + ' ' + _Resources._recL + ': ' + element.TargetL + ' ' + _Resources._recA + ': ' + element.Targeta + ' ' + _Resources._recB + ': ' + element.Targetb + '</td></tr>';
        tooltipString += '<tr><td>' + _Resources._recDeltaL + '   ' + element["ΔL"] + '</td></tr>';
        tooltipString += '<tr><td>' + _Resources._recDeltaA + '   ' + element["Δa"] + '</td></tr>';
        tooltipString += '<tr><td>' + _Resources._recDeltaB + '   ' + element["Δb"] + '</td></tr>';
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
            //paperLComparisionPoint.markers = {
            //    background: "White", size: 10,
            //    //border: {
            //    //    width: 1,
            //    //    color: function (e) {
            //    //        return 'black';
            //    //    }
            //    //}
            //};

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

////**************** START BIND THE GRAY BALANCE L* CHART  ****************////
function BindChartLGrayBalance() {


    $chart_L_BalanceChart.kendoChart({
        renderAs: "canvas",
        seriesDefaults: {
            type: "scatterLine",
            style: "smooth"
        },
        dataSource: jsonGray,
        title: {
            text: _Resources._recLBalanceChartTitle,
            font: "13px Tahoma"
        },
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
                template: '<table><tr><td><center><b>100% ' + _Resources._recgbPatchMetrics + '</b></center><td></tr><tr><td>' + _Resources._recActual + ' ' + _Resources._recL + ' : #= kendo.toString(dataItem.CielabL,"n") # ' + _Resources._recA + ': #= kendo.toString(dataItem.CielabA,"n")# ' + _Resources._recB + ' #= kendo.toString(dataItem.CielabB,"n")#</td></tr><tr><td>' + _Resources._recTarget + ' ' + _Resources._recL + ': #=kendo.toString(dataItem.TargetL,"n")# ' + _Resources._recA + ': #=kendo.toString(dataItem.Targeta,"n")# ' + _Resources._recB + ': #=kendo.toString(dataItem.Targetb,"n")#</td></tr><tr><td>' + _Resources._recDeltaL + '   #=kendo.toString(dataItem.ΔL,"n")#</td></tr><tr><td>' + _Resources._recDeltaA + '   #=kendo.toString(dataItem.Δa,"n")#</td></tr><tr><td>' + _Resources._recDeltaB + '   #=kendo.toString(dataItem.Δb,"n")#</td></tr></table>'
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
            height: 180
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
        title: {
            text: _Resources._recABalanceChartTitle,
            font: "13px Tahoma"
        },
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
                template: '<table><tr><td><center><b>100% ' + _Resources._recgbPatchMetrics + '</b></center><td></tr><tr><td>' + _Resources._recActual + ' ' + _Resources._recL + ' : #= kendo.toString(dataItem.CielabL,"n") # ' + _Resources._recA + ': #= kendo.toString(dataItem.CielabA,"n")# ' + _Resources._recB + ' #= kendo.toString(dataItem.CielabB,"n")#</td></tr><tr><td>' + _Resources._recTarget + ' ' + _Resources._recL + ': #=kendo.toString(dataItem.TargetL,"n")# ' + _Resources._recA + ': #=kendo.toString(dataItem.Targeta,"n")# ' + _Resources._recB + ': #=kendo.toString(dataItem.Targetb,"n")#</td></tr><tr><td>' + _Resources._recDeltaL + '   #=kendo.toString(dataItem.ΔL,"n")#</td></tr><tr><td>' + _Resources._recDeltaA + '   #=kendo.toString(dataItem.Δa,"n")#</td></tr><tr><td>' + _Resources._recDeltaB + '   #=kendo.toString(dataItem.Δb,"n")#</td></tr></table>'
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
            min: -6,
            max: 6,
            step: 2,
            majorUnit: 2,
            axisCrossingValues: [-6, 6],
            labels: {
                format: "{0}"
            },
        },
        chartArea: {
            width: 350,
            height: 180
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
        title: {
            text: _Resources._recBBalanceChartTitle,
            font: "13px Tahoma"
        },
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
                template: '<table><tr><td><center><b>100% ' + _Resources._recgbPatchMetrics + '</b></center><td></tr><tr><td>' + _Resources._recActual + ' ' + _Resources._recL + ' : #= kendo.toString(dataItem.CielabL,"n") # ' + _Resources._recA + ': #= kendo.toString(dataItem.CielabA,"n")# ' + _Resources._recB + ' #= kendo.toString(dataItem.CielabB,"n")#</td></tr><tr><td>' + _Resources._recTarget + ' ' + _Resources._recL + ': #=kendo.toString(dataItem.TargetL,"n")# ' + _Resources._recA + ': #=kendo.toString(dataItem.Targeta,"n")# ' + _Resources._recB + ': #=kendo.toString(dataItem.Targetb,"n")#</td></tr><tr><td>' + _Resources._recDeltaL + '   #=kendo.toString(dataItem.ΔL,"n")#</td></tr><tr><td>' + _Resources._recDeltaA + '   #=kendo.toString(dataItem.Δa,"n")#</td></tr><tr><td>' + _Resources._recDeltaB + '   #=kendo.toString(dataItem.Δb,"n")#</td></tr></table>'
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
            min: -6,
            max: 6,
            step: 2,
            majorUnit: 2,
            axisCrossingValues: [-6, 6],
            labels: {
                format: "{0}"
            },
            title: {
                text: ""
            }
        },
        chartArea: {
            width: 350,
            height: 180
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


////**************** START BIND THE DELTA SUMMARY STATASTICS CHART FOR REPORT  ****************////
function BindDeltaSummaryStatesChart() {
    var measurementID = parseInt($hdMeasurement);
    var apiParameters = {
        MeasurementId: measurementID,
        IsAverageOfMeasurements: $isAverageOfMeasurements,
        IsVerificationProject: $hdnVerification,
        IsSync1DTypeProject: $hdnSync1DType,
        fromDeltaE: $selectedDeltaE,
        validationType: $hdnValidationType
    };

    $.ajax({
        url: SERVICE_BASE_URL + 'api/Report/GenerateSummaryDeltaCharts',
        data: apiParameters,
        type: 'GET',
        dataType: 'json',
        success: function(response) {
            if (response != null) {
                jsonSummaryNameList = response.data.DTDeltaSummaryLables;
                jsonSummaryValueList = response.data.DTDeltaSummaryValues;
            }
            PopulateSummaryDeltaChart();
        },
        error: function(xhr, errorType, exception) {
            //alert(exception);
            kendo.ui.progress($chartLoader, false);
        }
    });


}

////**************** START BIND THE AB COMPARISION INK PAPER CHART  ****************////
function BindInkPaperChartABComparision() {
    var measurementID = parseInt($hdMeasurement);
    var apiParameters = {
        MeasurementId: measurementID,
        IsAverageOfMeasurements: $isAverageOfMeasurements,
        IsVerificationProject: $hdnVerification,
        IsSync1DTypeProject: $hdnSync1DType,
        fromDeltaE: $selectedDeltaE,
    };

    kendo.ui.progress($chartLoader, true);
    $.ajax({
        url: SERVICE_BASE_URL + 'api/Chart/GenerateInkPaperChart',
        data: apiParameters,
        type: 'GET',
        dataType: 'json',
        headers: {
            'Accept-Language': $CurrentCulture
        },
        success: function(response) {
            //console.log('Ink paper chart');
            //console.log(response);
            if (response.data != null) {
                jsonDeltaEPrimaryPatchList = response.data.DeltaEPrimaryPatchList;
                if ($hdnCurrentDlpSubOperation == ELicenseType.DLP || $hdnValidationType == 0) {
                    $tblDensityChart.css('display', 'none');
                }
                jsonDensityList = response.data.DensityList;
                jsonDeltaPaperLAB_1 = response.data.PaperPatchList;
                _Resources = response.data.GlobalResources;
            }

            PopulateInkPaperChartABComparision();
            kendo.ui.progress($chartLoader, false);
        },
        error: function(xhr, errorType, exception) {
            //alert(exception);
            kendo.ui.progress($chartLoader, false);
        }
    });
}

////**************** START BIND THE G7 CHART  ****************////
function BindG7Charts() {
    var measurementID = parseInt($hdMeasurement);
    var apiParameters = {
        MeasurementId: measurementID,
        IsAverageOfMeasurements: $isAverageOfMeasurements,
        IsVerificationProject: $hdnVerification,
    };

    //kendo.ui.progress($chartLoader, true);
    $.ajax({
        url: SERVICE_BASE_URL + 'api/Chart/GenerateG7Chart',
        data: apiParameters,
        type: 'GET',
        dataType: 'json',
        headers: {
            'Accept-Language': $CurrentCulture
        },
        success: function(response) {
            //console.log('G7 Chart data');
            //console.log(response);
            //console.log(JSON.stringify(response));
            if (response.data != null) {
                jsonGrayFChartList = response.data.GrayFChartList;
                jsonBlackLChartList = response.data.BlackLChartList;
                jsonGrayLChartList = response.data.GrayLChartList;
                jsonBlackNPDChartList = response.data.BlackNPDChartList;
                jsonGrayNPDChartList = response.data.GrayNPDChartList;
            }
            GenerateG7Charts();

            //kendo.ui.progress($chartLoader, false);
        },
        error: function(xhr, errorType, exception) {
            //alert(exception);
            kendo.ui.progress($chartLoader, false);
        }
    });
}

////**************** START BIND THE TVI CHARTS  ****************////
function BindTVICharts() {

    var measurementID = parseInt($hdMeasurement);
    var apiParameters = {
        MeasurementId: measurementID,
        IsAverageOfMeasurements: $isAverageOfMeasurements,
        IsVerificationProject: $hdnVerification,
        IsSync1DTypeProject: $hdnSync1DType
    };


    if ($hdnValidationType == 2) {

        $divTVI_G7.css('display', 'none');
        $divTVI_ISO.css('display', 'block');


        //kendo.ui.progress($chartLoader, true);
        $.ajax({
            url: SERVICE_BASE_URL + 'api/Chart/GenerateISOTVIChart',
            data: apiParameters,
            type: 'GET',
            dataType: 'json',
            headers: {
                'Accept-Language': $CurrentCulture
            },
            success: function(response) {

                if (response.data != null) {
                    jsonDTISOTVIChartList = response.data.DTISOTVIChartList;
                    jsonDTSpreadChartData_CMYKList = response.data.DTSpreadChartData_CMYKList;
                    jsonDTSpreadChartDataList = response.data.DTSpreadChartDataList;
                    jsonDTSpreadSplineRangeChartDataList = response.data.DTSpreadSplineRangeChartDataList;
                    PopulateTVIISOChart();
                    PopulateTVIG7CMYKChart();
                    PopulateTVISpreadChart();
                }
                //kendo.ui.progress($chartLoader, false);
            },
            error: function(xhr, errorType, exception) {
                //alert(exception);
                kendo.ui.progress($chartLoader, false);
            }
        });
    }


    if ($hdnValidationType == 1) {
        $divTVI_G7.css('display', 'block');
        $divTVI_ISO.css('display', 'none');

        //kendo.ui.progress($chartLoader, true);
        $.ajax({
            url: SERVICE_BASE_URL + 'api/Chart/GenerateTVIChart',
            data: apiParameters,
            type: 'GET',
            dataType: 'json',
            headers: {
                'Accept-Language': $CurrentCulture
            },
            success: function(response) {

                if (response.data != null) {

                    jsonTVIG7ChartList = response.data.DTTVIChartList;
                    PopulateTVIG7Chart();
                }

                //kendo.ui.progress($chartLoader, false);
            },
            error: function(xhr, errorType, exception) {
                //alert(exception);
                kendo.ui.progress($chartLoader, false);
            }
        });

    }


}

////**************** START BIND THE G7 CHART  ****************////
function GenerateG7Charts() {

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
            xField: "Cmyk_Value",
            yField: "ΔValue",
            color: "Black",
            markers: { background: "Black", size: 4 },
            //highlight: {
            //    visible: false
            //},
            tooltip: {
                visible: true,
                template: '<table><tr><td><center><b>#= dataItem.Cmyk_Value.toFixed() #% Patch Matrics</b></center><td></tr><tr><td> ΔCh*:  #= dataItem.ΔValue #</td></tr></table>'
            }
        },
        {
            name: "Weighted",
            xField: "Cmyk_Value",
            yField: "wΔValue",
            color: "Blue",
            markers: { background: "Blue", size: 4 },
            tooltip: {
                visible: true,
                template: '<table><tr><td><center><b>#= dataItem.Cmyk_Value.toFixed() #% Patch Matrics</b></center><td></tr><tr><td> wΔCh*:  #= dataItem.ΔValue #</td></tr></table>'
            }
        },
        {
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
        series: [{
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
                template: '<table><tr><td><center><b>#= dataItem.Cmyk_Value.toFixed() #% Patch Matrics</b></center><td></tr><tr><td> ΔCh*:  #= dataItem.ΔValue #</td></tr></table>'
            }
        },
        {
            name: "Weighted",
            xField: "Cmyk_Value",
            yField: "wΔValue",
            color: "Blue",
            markers: { background: "Blue", size: 4 },
            tooltip: {
                visible: true,
                template: '<table><tr><td><center><b>#= dataItem.Cmyk_Value.toFixed() #% Patch Matrics</b></center><td></tr><tr><td> wΔCh*:  #= dataItem.wΔValue #</td></tr></table>'
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
        legendItemClick: function(e) {
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
                template: '<table><tr><td><center><b>#= dataItem.Cmyk_Value.toFixed() #% Patch Matrics</b></center><td></tr><tr><td> wΔCh*:  #= dataItem.wΔValue #</td></tr></table>'
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
                template: '<table><tr><td><center><b>#= dataItem.Cmyk_Value.toFixed() #% Patch Matrics</b></center><td></tr><tr><td> ΔCh*:  #= dataItem.ΔValue #</td></tr></table>'
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
                color: "Black",
                markers: { background: "Black", size: 0 },
                highlight: {
                    visible: false
                },
                tooltip: {
                    visible: true,
                    template: '<table><tr><td><center><b>Black Target NPD  #= dataItem.Cmyk_Value.toFixed() #% Patch Matrics</b></center><td></tr><tr><td> G7NPDC:  #= dataItem.ΔLimit #</td></tr></table>'
                }
            },
            {
                xField: "Cmyk_Value",
                yField: "ΔValue",
                color: "Green",
                markers: { background: "Green", size: 0 },
                highlight: {
                    visible: false
                },
                tooltip: {
                    visible: true,
                    template: '<table><tr><td><center><b>Black Actual NPD  #= dataItem.Cmyk_Value.toFixed() #% Patch Matrics</b></center><td></tr><tr><td> G7NPDC Actual:  #= dataItem.ΔValue #</td></tr></table>'
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
                color: "Black",
                markers: { background: "Black", size: 0 },
                highlight: {
                    visible: false
                },
                tooltip: {
                    visible: true,
                    template: '<table><tr><td><center><b>Gray Target NPD  #= dataItem.Cmyk_Value.toFixed() #% Patch Matrics</b></center><td></tr><tr><td> G7NPDC:  #= dataItem.ΔLimit #</td></tr></table>'
                }
            },
            {
                xField: "Cmyk_Value",
                yField: "ΔValue",
                color: "Green",
                markers: { background: "Green", size: 0 },
                highlight: {
                    visible: false
                },
                tooltip: {
                    visible: true,
                    template: '<table><tr><td><center><b>Gray Actual NPD  #= dataItem.Cmyk_Value.toFixed() #% Patch Matrics</b></center><td></tr><tr><td> G7NPDC Actual:  #= dataItem.ΔValue #</td></tr></table>'
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

function BindMeasurementPatchDetails() {

    var measurementID = parseInt($hdMeasurement);
    var apiParameters = {
        MeasurementId: measurementID,
        IsVerificationProject: $hdnVerification,
        IsSync1DTypeProject: $hdnSync1DType
    };


    $divMeasurementPatchDetails.kendoGrid({
        dataSource: {
            transport: {
                read: function(options) {
                    $.ajax({
                        type: "GET",
                        url: SERVICE_BASE_URL + 'api/Chart/GetMeasurementPatchDetails',
                        data: apiParameters,
                        dataType: "json",
                        headers: {
                            'Accept-Language': $CurrentCulture
                        },
                        success: function(response) {
                            debugger;
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
                            }

                        },
                        error: function(XMLHttpRequest, textStatus, errorThrown) {
                            console.log(XMLHttpRequest.responseText);
                        }
                    });
                },

            },
            schema: {
                model: {
                    fields: {
                        SampleID: {
                            from: "SampleID"
                        },
                        SampleName: {
                            from: "SampleName"
                        },
                        CMYK_C: {
                            from: "CMYK_C"
                        },
                        CMYK_M: {
                            from: "CMYK_M"
                        },
                        CMYK_Y: {
                            from: "CMYK_Y"
                        },
                        CMYK_K: {
                            from: "CMYK_K"
                        },
                        HTMLColor: {
                            from: "HTMLColor"
                        },
                        TargetL: {
                            from: "TargetL"
                        },
                        Targeta: {
                            from: "Targeta"
                        },
                        Targetb: {
                            from: "Targetb"
                        },
                        CIELAB_L: {
                            from: "CIELAB_L"
                        },
                        CIELAB_a: {
                            from: "CIELAB_a"
                        },
                        CIELAB_b: {
                            from: "CIELAB_b"
                        },
                        ΔE: {
                            from: "ΔE"
                        },
                        ΔE94: {
                            from: "ΔE94"
                        },
                        ΔE2000: {
                            from: "ΔE2000"
                        },
                        Density_V: {
                            from: "Density_V"
                        },
                        Density_C: {
                            from: "Density_C"
                        },
                        Density_M: {
                            from: "Density_M"
                        },
                        Density_Y: {
                            from: "Density_Y"
                        },
                        PatchTypeID: {
                            from: "PatchTypeID"
                        },
                        ΔH: {
                            from: "ΔH"
                        },
                        ΔCh: {
                            from: "ΔCh"
                        },
                        ΔL: {
                            from: "ΔL"
                        }
                    }
                }
            },
            pageSize: 10
        },
        sortable: true,
        scrollable: true,
        filterable: true,
        //filterable: {
        //    extra: false,
        //    operators: {
        //        string: {
        //            eq: "Is equal to"
        //        }
        //    }
        //},
        pageable: {
            refresh: true,
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
                title: "<b>Patch</b>",
                //template: "<span id='sp' style='display:inline-block;background-color:#FFB700;height:14px;width:30px;'></span>",
                template: "<span id='sp' style='display:inline-block;background-color:#=HTMLColor#;height:14px;width:30px;'></span>",
                width: "50px",
                //locked: true,
                //lockable: false,
            },
            {
                field: "SampleID",
                title: "<b>ID</b>",
                width: "50px",
                filterable: false,
                //locked: true,
            },
            {
                field: "SampleName",
                title: "<b>Name</b>",
                filterable: false,
                width: "50px",
                // locked: true,
            },
            {
                field: "CMYK_C",
                title: "<b>C</b>",
                format: "{0:n2}",
                width: "50px",
                //locked: true,
            },
            {
                field: "CMYK_M",
                title: "<b>M</b>",
                format: "{0:n2}",
                width: "50px",
                //locked: true,
            },
            {
                field: "CMYK_Y",
                title: "<b>Y</b>",
                format: "{0:n2}",
                width: "50px",
                //locked: true,
            },
            {
                field: "CMYK_K",
                title: "<b>K</b>",
                format: "{0:n2}",
                width: "50px",
                //locked: true,
            },
            {
                field: "ΔE",
                title: "<b>ΔE</b>",
                width: "50px",
                format: "{0:n2}",
            },
            {
                field: "ΔE94",
                title: "<b>ΔE94</b>",
                width: "50px",
                format: "{0:n2}",
            },
            {
                field: "ΔE2000",
                title: "<b>ΔE2000</b>",
                width: "80px",
                format: "{0:n2}",
            },
            {
                field: "TargetL",
                title: "<b>TargetL</b>",
                width: "80px",
                format: "{0:n2}",
            },
            {
                field: "CIELAB_L",
                title: "<b>CIELAB_L</b>",
                width: "80px",
                format: "{0:n2}",
            },
            {
                field: "Targeta",
                title: "<b>Targeta</b>",
                width: '80px',
                format: "{0:n2}",
            },
            {
                field: "CIELAB_a",
                title: "<b>CIELAB_a</b>",
                width: "80px",
                format: "{0:n2}",
            },
            {
                field: "Targetb",
                title: "<b>Targetb</b>",
                width: '80px',
                format: "{0:n2}",
            },
            {
                field: "CIELAB_b",
                title: "<b>CIELAB_b</b>",
                width: "80px",
                format: "{0:n2}",
            },
            {
                field: "Density_V",
                title: "<b>Density_V</b>",
                width: "80px",
                format: "{0:n2}",
            },
            {
                field: "Density_C",
                title: "<b>Density_C</b>",
                width: '80px',
                format: "{0:n2}",
            },
            {
                field: "Density_M",
                title: "<b>Density_M</b>",
                width: "80px",
                format: "{0:n2}",
            },
            {
                field: "Density_Y",
                title: "<b>Density_Y</b>",
                width: '80px',
                format: "{0:n2}",
            },
        ],
        //dataBound: function (e) {
        //    debugger;
        //    var measurementId = 0;
        //    if (e.sender.dataSource._view.length > 0) {
        //        measurementId = e.sender.dataSource._view[0].MeasurementID;
        //    }

        //    if (measurementId > 0) {
        //        this.hideColumn(4);
        //        this.hideColumn(5);
        //        this.hideColumn(6);
        //        this.hideColumn(7);
        //    }
        //    else {
        //        this.hideColumn(8);
        //        this.hideColumn(9);
        //        this.hideColumn(10);
        //    }


        //    var dataSource = this.dataSource;
        //    this.element.find('tr').each(function () {
        //        var row = $(this);
        //        //set resource message
        //        row.find('#lnkView').attr('title', Resources.gridAnalyzeToolTipViewResource);
        //    });
        //}
    });


    if ($hdnCurrentDlpSubOperation == ELicenseType.Sync1D || $hdnCurrentDlpSubOperation == ELicenseType.Validation) {

        var measurementID = parseInt($hdMeasurement);
        var params = {
            MeasurementId: measurementID,
            IsVerificationProject: $hdnVerification,
            IsSync1DTypeProject: $hdnSync1DType,
            fromDeltaE: $selectedDeltaE,
            IsAverageOfMeasurements: $isAverageOfMeasurements
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
            }
        });

    } else {
        $divMeasurementPatchDetails.find("#divMetricSum").css('display', 'none');
    }




    //$divMeasurementPatchDetails.data('kendoGrid').refresh();
    //$divMeasurementPatchDetails.find('.k-grid-content-locked').css('height', 'unset');

    $divMeasurementPatchDetails.find('#btnExportCGATS').on('click', function(e) {
        e.preventDefault();

        var selectedMatricSum = '';
        if ($divMeasurementPatchDetails.find("#ddlMatricSum").data("kendoDropDownList")) {
            selectedMatricSum = $divMeasurementPatchDetails.find("#ddlMatricSum").data("kendoDropDownList").value();
        }
        var measurementID = parseInt($hdMeasurement);
        var apiReportParameters = {
            MeasurementId: measurementID,
            IsVerificationProject: $hdnVerification,
            IsSync1DTypeProject: $hdnSync1DType,
            metricSumID: selectedMatricSum
        };

        $.ajax({
            type: "GET",
            url: SERVICE_BASE_URL + 'api/Chart/GetCGATSPatchReport',
            data: apiReportParameters,
            dataType: "json",
            success: function(response) {
                if (response.data != null) {
                    var fileName = '~' + response.data;
                    window.location.href = SERVICE_BASE_URL + '/api/Chart/DownloadFile?filePath=' + fileName;
                }
            },
            error: function(XMLHttpRequest, textStatus, errorThrown) {
                console.log(XMLHttpRequest.responseText);
            }
        });
    });

    $divMeasurementPatchDetails.find('#btnExportCFX').on('click', function(e) {
        e.preventDefault();
        var measurementID = parseInt($hdMeasurement);

        var selectedMatricSum = '';
        if ($divMeasurementPatchDetails.find("#ddlMatricSum").data("kendoDropDownList")) {
            selectedMatricSum = $divMeasurementPatchDetails.find("#ddlMatricSum").data("kendoDropDownList").value();
        }
        var apiReportParameters = {
            MeasurementId: measurementID,
            IsVerificationProject: $hdnVerification,
            IsSync1DTypeProject: $hdnSync1DType,
            metricSumID: selectedMatricSum
        };
        $.ajax({
            type: "GET",
            url: SERVICE_BASE_URL + 'api/Chart/GetCFXPatchReport',
            data: apiReportParameters,
            dataType: "json",
            success: function(response) {
                if (response.data != null) {
                    var fileName = '~' + response.data;
                    window.location.href = SERVICE_BASE_URL + '/api/Chart/DownloadFile?filePath=' + fileName;
                }
            },
            error: function(XMLHttpRequest, textStatus, errorThrown) {
                console.log(XMLHttpRequest.responseText);
            }
        });
    });
}

////************************ CHART POPULATIONS  ***********************////

////**************** POPULATE THE ANALYZE CHART  ****************////
function PopulateAnalyzeChart() {
    //console.log(jsonAnalyzeList);
    //var measurementNamesList = [];
    var patchArray = [];
    if (jsonAnalyzeList.Measurements.length > 0) {

        //get the name of the measurement name list
        //measurementNamesList = $.map(jsonAnalyzeList.Measurements, function (e) {
        //    return e.MeasurementName;
        //});

        measurementNamesList = $.map(jsonAnalyzeList.Measurements, function(e) {
            var curObj = {
                MeasurementId: e.MeasurementId,
                MeasurementName: e.MeasurementName,
                IsUsed: e.IsUsed
            };
            return curObj;
        });

        //get the available list of htmlcodes
        var htmlPatchCodeList = $.map(jsonAnalyzeList.Measurements[0].MeasurementPatches, function(e) {
            return e.HtmlColor;
        });

        //generate new array to display the patches
        $.each(htmlPatchCodeList, function(index, htmlCode) {

            //check one by one all html code
            $.each(jsonAnalyzeList.Measurements, function(index, measureObj) {

                //check the current patch in the main list
                var arr1Obj1 = $.grep(measureObj.MeasurementPatches, function(e) {
                    return e.HtmlColor == htmlCode;
                });

                //check the patch is already available in the resulted array
                var availablePatch = $.grep(patchArray, function(e) {
                    return e.color == htmlCode;
                });
                //if exists then
                if (availablePatch.length > 0) {
                    availablePatch[0].data.push(arr1Obj1[0].Δe2000); //add into existing E2000 in data array
                }
                else {//add new object in resulted array

                    if (arr1Obj1.length > 0) {
                        var patchObj = {
                            data: [arr1Obj1[0].Δe2000],
                            color: arr1Obj1[0].HtmlColor,
                            markers: {
                                background: arr1Obj1[0].HtmlColor
                            }
                        }
                        patchArray.push(patchObj);
                    }
                }
            });
        });
    }



    $divAnalyzeChart.kendoChart({
        //title: {
        //    text: "Analyze Chart"
        //},
        //legend: {
        //    position: "bottom"
        //},
        //chartArea: {
        //    background: ""
        //},
        seriesDefaults: {
            type: "line",
            style: "smooth"
        },
        series: patchArray,
        valueAxis: {
            title: {
                text: 'Δ'+$selectedDeltaE
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
            categories: measurementNamesList,
            majorGridLines: {
                visible: false
            },
            labels: {
                rotation: "auto",
                //cursor: "pointer",
                visual: function(e) {

                    //console.log(e);

                    // Build an HTML fragment and append it to the body                    
                    //get the category values
                    var catValue = e.value;

                    //set checked or not
                    var catText = '<div style="font-size:18px;cursor: pointer;">☐</div>'
                    if (catValue.IsUsed) {
                        catText = '<div style="font-size:18px;cursor: pointer;">☑</div>'
                    }
                    var html = $(catText).appendTo(document.body);

                    // Create an empty group that will hold the rendered label
                    var visual = new kendo.drawing.Group();

                    // Store a reference to the target rectangle, see below
                    var rect = e.rect;

                    kendo.drawing.drawDOM(html)
                        .done(function(group) {
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
        //tooltip: {
        //    visible: true,
        //    format: "{0}%",
        //    template: "#= series.name #: #= value #"
        //},
        chartArea: {
            width: 675,
            height: 200,
        },
        plotArea: {
            background: "lightgray"
        },
        axisLabelClick: function(e) {

            //get the current clicked category data
            var currentObj = e.value;
            //check only one category available or not
            if (measurementNamesList.length <= 1 && currentObj.IsUsed) {
                alert(_Resources.selectMeasurementMsg);
                return false;
            }

            //check in multiple category
            var otherMeasurements = $.grep(measurementNamesList, function(elem) {
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

            $.ajax({
                url: SERVICE_BASE_URL + 'api/Chart/UpdateMeasurementFromAnalyzeChart',
                data: apiParameters,
                type: 'GET',
                dataType: 'json',
                success: function(response) {
                    BindAnalyzeChart();
                },
                error: function(xhr, errorType, exception) {
                    //alert(exception);                    
                }
            });

        }
    });
}

////**************** POPULATE THE INK PAPER AB COMPARISION CHART  ****************////
function PopulateInkPaperChartABComparision() {

    //var json = jsonDeltaEPrimaryPatchList;

    var formattedDeltaEPrimaryPatchList = [];

    //set the color name and its tooltip title with resources
    var patchColor = [
        { colorName: _Resources._recCyan, colorTitle: _Resources._recCyanPathMetrics }, { colorName: _Resources._recMagenta, colorTitle: _Resources._recMagentaPathMetrics },
        { colorName: _Resources._recYellow, colorTitle: _Resources._recYellowPathMetrics }, { colorName: _Resources._recBlack, colorTitle: _Resources._recBlackPathMetrics },
        { colorName: _Resources._recRed, colorTitle: _Resources._recRedPathMetrics }, { colorName: _Resources._recGreen, colorTitle: _Resources._recGreenPathMetrics },
        { colorName: _Resources._recBlue, colorTitle: _Resources._recBluePathMetrics }, { colorName: _Resources._recPaper, colorTitle: _Resources._recPaperPathMetrics },
        { colorName: _Resources._recTriplet, colorTitle: _Resources._recTripletPathMetrics }];


    $.each(jsonDeltaEPrimaryPatchList, function(index, obj) {

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
    var axis = jsonDeltaEPrimaryPatchList.map(function(a) { return a.ΔE; });
    var patchColorList = formattedDeltaEPrimaryPatchList.map(function(a) { return a.name; });
    var maxNum = Math.max.apply(Math, axis);
    var maxValue = 0;
    var marginValue = 1;
    switch (true) {
        case (maxNum < 5): maxValue = 5; marginValue = 1; break;
        case (maxNum == 5): maxValue = 6; marginValue = 1; break;
        case (maxNum >= 5 && maxNum < 10): maxValue = 10; marginValue = 2; break;
        case (maxNum >= 10 && maxNum < 15): maxValue = 15; marginValue = 3; break;
        case (maxNum >= 15 && maxNum < 20): maxValue = 20; marginValue = 5; break;
        case (maxNum >= 20 && maxNum < 30): maxValue = 30; marginValue = 5; break;
        case (maxNum >= 30): maxValue = 50; marginValue = 5; break;
    }

    //generate the second stacked chart by max value
    var stackedChart = [];
    $.each(axis, function(index, num) {
        var stack = {
            value: maxValue
        };
        stackedChart.push(stack);
    });

    var thrashHold = { paths: [] };

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
                toggle: function(e) {
                    // Don't create a highlight overlay,
                    // we'll modify the existing visual instead
                    e.preventDefault();

                    var visual = e.visual;
                    var opacity = e.show ? 0.8 : 1;

                    visual.opacity(opacity);
                }
            },
            visual: function(e) {
                var visual = e.createVisual();
                //-----For Sync1D projects or validation projects
                if ($hdnCurrentDlpSubOperation == ELicenseType.Sync1D || $hdnCurrentDlpSubOperation == ELicenseType.Validation) {
                    if (e.series.index == 0) {
                        var axis = e.sender.getAxis("valueAxis");

                        ////set the path for the green target
                        // var targetGreen = axis.slot(e.dataItem.PreAvgΔE).origin.x;
                        // var pathGreen = new kendo.drawing.Path({ stroke: { width: 2, color: "#34E61E" } }).moveTo(targetGreen, e.rect.topRight().y).lineTo(targetGreen, (e.rect.topRight().y + e.rect.size.height));
                        // visual.append(pathGreen);
                        // //set the dot on the green target
                        // var circleGeoGreenPath = new kendo.geometry.Circle([axis.slot(e.dataItem.PreAvgΔE).origin.x, (e.rect.topRight().y + (e.rect.size.height / 2))], 2);
                        // var circleGreenPath = new kendo.drawing.Circle(circleGeoGreenPath).fill("#34E61E", 1);
                        // circleGreenPath.options.stroke.color = "#34E61E";
                        // visual.append(circleGreenPath);

                        // //set the path for the red target
                        // var targetRed = axis.slot(e.dataItem.ΔELimit).origin.x;
                        // var pathRed = new kendo.drawing.Path({ stroke: { width: 2, color: "red" } }).moveTo(targetRed, e.rect.topRight().y).lineTo(targetRed, (e.rect.topRight().y + e.rect.size.height));
                        // visual.append(pathRed);
                        // //set the dot on the red target
                        // var circleGeoRedPath = new kendo.geometry.Circle([axis.slot(e.dataItem.ΔELimit).origin.x, (e.rect.topRight().y + (e.rect.size.height / 2))], 2);
                        // var circleRedPath = new kendo.drawing.Circle(circleGeoRedPath).fill("red", 1);
                        // circleRedPath.options.stroke.color = "red";
                        // visual.append(circleRedPath);

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
            tooltip: {
                visible: false
            },
            color: "#C8D6E3"
        }],
        panes: [{
            clip: false
        }],
        categoryAxis: {
            name: "categoryAxis",
            categories: patchColorList,
            majorGridLines: {
                visible: false
            }
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
            max: maxValue,
            margin: marginValue
        },
        tooltip: {
            visible: true,
            //template: "#= dataItem.title # <br/> Actual L: *#= dataItem.CielabL # a: *#= dataItem.CielabA # b: *#= dataItem.CielabB # <br/>Target L: *#= dataItem.TargetL # a: *#= dataItem.Targeta # b: *#= dataItem.Targetb # <br/>E:   #= value # (Thr.0.00)"
            //template: "<b>#= dataItem.title #</b> <br/> " + _Resources._recActual + " " + _Resources._recL + ": #= dataItem.CielabL # " + _Resources._recA + ": #= dataItem.CielabA # " + _Resources._recB + ": #= dataItem.CielabB # <br/>" + _Resources._recTarget + " " + _Resources._recL + ": #= dataItem.TargetL # " + _Resources._recA + ": #= dataItem.Targeta # " + _Resources._recB + ": #= dataItem.Targetb # <br/>" + _Resources._recDeltaE + ":  #= value #"
            template: function(dataItem) {
                debugger;
                var item = dataItem.dataItem;
                var toolTipString = "<table>";
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
                if ($hdnCurrentDlpSubOperation == ELicenseType.Sync1D || $hdnCurrentDlpSubOperation == ELicenseType.Validation) {
                    toolTipString += " (Thr." + item.ΔELimit.toFixed(2) + ")";
                }

                //-----For Sync1D projects, display Previous ΔE in tooltip also
                if ($hdnCurrentDlpSubOperation == ELicenseType.Sync1D && item.PreAvgΔE > 0) {
                    toolTipString += "<tr><td><b>" + _Resources._recPreDeltaE + "</b></td><td>";
                    toolTipString += item.PreAvgΔE.toFixed(2);
                    toolTipString += "</td></tr>";
                }

                toolTipString += "</table></td></tr>";
                toolTipString += "</table>";
                //var toolTipString = "<b>#= item.title #</b> <br/> " + _Resources._recActual + " " + _Resources._recL + ": #= item.CielabL # " + _Resources._recA + ": #= item.CielabA # " + _Resources._recB + ": #= item.CielabB # <br/>" + _Resources._recTarget + " " + _Resources._recL + ": #= item.TargetL # " + _Resources._recA + ": #= item.Targeta # " + _Resources._recB + ": #= item.Targetb # <br/>" + _Resources._recDeltaE + ":  #= item.value #";
                return toolTipString;
            }
        },
        chartArea: {
            width: 310,
            height: 325
        },
        render: function(e) {

            //-----For Sync1D projects or validation projects
            if ($hdnCurrentDlpSubOperation == ELicenseType.Sync1D || $hdnCurrentDlpSubOperation == ELicenseType.Validation) {
                var chart = e.sender;
                $.each(thrashHold.paths, function(index, obj) {
                    //set the line 
                    var pathGreen = new kendo.drawing.Path({ stroke: { width: 2, color: obj.color } }).moveTo(obj.x, obj.y1).lineTo(obj.x, obj.y2);
                    chart.surface.draw(pathGreen);

                    //set the dot mid of the line
                    var circleGeoPath = new kendo.geometry.Circle([obj.x, obj.y3], 2);  //
                    var circlePath = new kendo.drawing.Circle(circleGeoPath).fill(obj.color, 1);
                    circlePath.options.stroke.color = obj.color;
                    chart.surface.draw(circlePath);
                });
            }
        }
    });

    //var densityPatchColor = [
    //    { colorName: _Resources._recCyan, colorTitle: _Resources._recCyanPathMetrics },
    //    { colorName: _Resources._recMagenta, colorTitle: _Resources._recMagentaPathMetrics },
    //    { colorName: _Resources._recYellow, colorTitle: _Resources._recYellowPathMetrics },
    //    { colorName: _Resources._recBlack, colorTitle: _Resources._recBlackPathMetrics }
    //];

    var formattedDensityList = [];
    $.each(jsonDensityList, function(index, obj) {
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
                toggle: function(e) {
                    // Don't create a highlight overlay,
                    // we'll modify the existing visual instead
                    e.preventDefault();

                    var visual = e.visual;
                    var opacity = e.show ? 0.8 : 1;

                    visual.opacity(opacity);
                }
            },
            visual: function(e) {
                var visual = e.createVisual();
                var axis = e.sender.getAxis("valueAxis");

                var target = axis.slot(e.dataItem.densityLimit).origin.y;
                var path = new kendo.drawing.Path({ stroke: { width: 3, color: "red" } }).moveTo(e.rect.origin.x, target).lineTo(e.rect.topRight().x, target);
                visual.append(path);

                var circleGeometry = new kendo.geometry.Circle([(e.rect.origin.x + (e.rect.size.width / 2)), target], 3);
                var circleDensity = new kendo.drawing.Circle(circleGeometry).fill("red", 1);
                circleDensity.options.stroke.color = "red";
                visual.append(circleDensity);

                if ($hdnCurrentDlpSubOperation == ELicenseType.Sync1D && e.dataItem.preAvgDensity > 0) {
                    var targetAvg = axis.slot(e.dataItem.preAvgDensity).origin.y;
                    var pathAvg = new kendo.drawing.Path({ stroke: { width: 3, color: "#34E61E" } }).moveTo(e.rect.origin.x, targetAvg).lineTo(e.rect.topRight().x, targetAvg);
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
            //template: "#= dataItem.title # <br/> Actual L: *#= dataItem.CielabL # a: *#= dataItem.CielabA # b: *#= dataItem.CielabB # <br/>Target L: *#= dataItem.TargetL # a: *#= dataItem.Targeta # b: *#= dataItem.Targetb # <br/>E:   #= value # (Thr.0.00)"
            //template: "<b>#= dataItem.title #</b> <br/> " + _Resources._recActual + " :    #= dataItem.density # <br/>" + _Resources._recTarget + " :    #= dataItem.densityLimit # <br/>" + _Resources._recPrevious + " :    #= dataItem.preAvgDensity #"
            template: function(e) {
                var item = e.dataItem;
                var tooltipString = "<b>" + item.title + "</b><br/>";
                tooltipString += "<b>" + _Resources._recActual + ":</b> " + item.density.toFixed(2) + "<br/>";
                tooltipString += "<b>" + _Resources._recTarget + ":</b> " + item.densityLimit.toFixed(2) + "<br/>";
                if ($hdnCurrentDlpSubOperation == ELicenseType.Sync1D && item.preAvgDensity > 0) {
                    tooltipString += "<b>" + _Resources._recPrevious + ":</b> " + item.preAvgDensity.toFixed(2);
                }
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
            color: '#D4CBC5',
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
            color: function(e) {
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
            labels: {
                format: "{0}"
            },
            title: {
                //text: _Resources._recDeltaA
            }, majorGridLines: {
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
            labels: {
                format: "{0}"
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
            height: 180
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
        render: renderChartWithBackgroundByCategory
    });





}

////**************** POPULATE THE TVI CHART  ****************////
function PopulateTVIG7Chart() {
    var jsonEmpty = $.grep(jsonTVIG7ChartList, function(element) { return element.CmykC == 0 && element.CmykM == 0 && element.CmykY == 0 && element.CmykK == 0; })

    var jsonC = $.grep(jsonTVIG7ChartList, function(element) { return element.CmykC != 0; });
    var jsonM = $.grep(jsonTVIG7ChartList, function(element) { return element.CmykM != 0; });
    var jsonY = $.grep(jsonTVIG7ChartList, function(element) { return element.CmykY != 0; });
    var jsonK = $.grep(jsonTVIG7ChartList, function(element) { return element.CmykK != 0; });

    var jsonCResult = jsonEmpty.concat(jsonC);
    var jsonMResult = jsonEmpty.concat(jsonM);
    var jsonYResult = jsonEmpty.concat(jsonY);
    var jsonKResult = jsonEmpty.concat(jsonK);

    $divChartTVISummaryDotGain.kendoChart({
        //title: {
        //    text: "TVI Chart"
        //},
        seriesDefaults: {
            type: "scatterLine",
            style: "smooth"
        },
        //dataSource: jsonKResult,
        series: [
            {
                data: jsonCResult,
                xField: "CmykC",
                yField: "DotGain",
                color: "Cyan",
                markers: {
                    background: "Cyan",
                    size: 6,
                    border: {
                        width: 1,
                        color: function(e) {
                            return 'black';
                        }
                    }
                },
                highlight: {
                    visible: false
                },
                tooltip: {
                    visible: true,
                    template: '<table><tr><td><center><b>Cyan #= dataItem.CmykC.toFixed() #% Patch Matrics</b></center><td></tr><tr><td> Dot Gain:  #= dataItem.DotGain #</td></tr></table>'
                }
            }, {
                data: jsonMResult,
                xField: "CmykM",
                yField: "DotGain",
                color: "Magenta",
                markers: {
                    background: "Magenta", size: 6, border: {
                        width: 1,
                        color: function(e) {
                            return 'black';
                        }
                    }
                },
                tooltip: {
                    visible: true,
                    template: '<table><tr><td><center><b>Cyan #= dataItem.CmykM.toFixed() #% Patch Matrics</b></center><td></tr><tr><td> Dot Gain:  #= dataItem.DotGain #</td></tr></table>'
                },
                highlight: {
                    visible: false
                },
            }, {
                data: jsonYResult,
                xField: "CmykY",
                yField: "DotGain",
                color: "Yellow",
                markers: {
                    background: "Yellow", size: 6, border: {
                        width: 1,
                        color: function(e) {
                            return 'black';
                        }
                    }
                },
                tooltip: {
                    visible: true,
                    template: '<table><tr><td><center><b>Cyan #= dataItem.CmykY.toFixed() #% Patch Matrics</b></center><td></tr><tr><td> Dot Gain:  #= dataItem.DotGain #</td></tr></table>'
                },
                highlight: {
                    visible: false
                },
            }, {
                data: jsonKResult,
                xField: "CmykK",
                yField: "DotGain",
                color: "Black",
                markers: { background: "Black", size: 4 },
                tooltip: {
                    visible: true,
                    template: '<table><tr><td><center><b>Cyan #= dataItem.CmykK.toFixed() #% Patch Matrics</b></center><td></tr><tr><td> Dot Gain:  #= dataItem.DotGain #</td></tr></table>'
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
            //title: {
            //    text: ""
            //}
        },
        yAxis: {
            name: "yAxis",
            // min: -5,
            // max: 25,
            //majorUnit: 5,
            // axisCrossingValues: [-5, 25],
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
        render: renderChartWithBackgroundByAxis
    });

    //load the chart again to adjust the xAxis lables according to negative values
    var chart = $divChartTVISummaryDotGain.data("kendoChart");
    var yArray = chart._plotArea.axisY.children.map(r => parseInt(r.content));
    var minValue = Math.abs(Math.min.apply(Math, yArray));
    if (minValue > 0) {
        var kendoOptions = chart.options;
        kendoOptions.xAxis.labels.padding.top = (minValue * 10);
        chart.options = kendoOptions.options;
        chart.redraw();
    }
}

////**************** POPULATE THE TVI ISO CHART  ****************////
function PopulateTVIISOChart() {
    var isoCyanList = [];
    var isoMagentaList = [];
    var isoYellowList = [];
    var isoBlackList = [];

    if (jsonDTISOTVIChartList.length > 0) {
        isoCyanList = $.grep(jsonDTISOTVIChartList, function(element) {
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
        isoCyanList.sort(function(a, b) { return a.CmykC - b.CmykC; });

        $.each(isoCyanList, function(index, obj) {
            var thrashHoldObject = [];
            var ThresholdTop = 0;
            var ThresholdBottom = 0;
            var thresHoldValue = obj.Threshold;
            ThresholdTop = obj.TargetDotGain + thresHoldValue;
            ThresholdBottom = obj.TargetDotGain - thresHoldValue;
            obj.ThresholdTop = ThresholdTop;
            obj.ThresholdBottom = ThresholdBottom;
        });
        //console.log(isoCyanList);
    }

    if (jsonDTISOTVIChartList.length > 0) {
        isoMagentaList = $.grep(jsonDTISOTVIChartList, function(element) {
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
        isoMagentaList.sort(function(a, b) { return a.CmykM - b.CmykM; });

        $.each(isoMagentaList, function(index, obj) {
            var thrashHoldObject = [];
            var ThresholdTop = 0;
            var ThresholdBottom = 0;
            var thresHoldValue = obj.Threshold;
            ThresholdTop = obj.TargetDotGain + thresHoldValue;
            ThresholdBottom = obj.TargetDotGain - thresHoldValue;
            obj.ThresholdTop = ThresholdTop;
            obj.ThresholdBottom = ThresholdBottom;
        });
        //console.log(isoMagentaList);
    }

    if (jsonDTISOTVIChartList.length > 0) {
        isoYellowList = $.grep(jsonDTISOTVIChartList, function(element) {
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
        isoYellowList.sort(function(a, b) { return a.CmykY - b.CmykY; });

        $.each(isoYellowList, function(index, obj) {
            var thrashHoldObject = [];
            var ThresholdTop = 0;
            var ThresholdBottom = 0;
            var thresHoldValue = obj.Threshold;
            ThresholdTop = obj.TargetDotGain + thresHoldValue;
            ThresholdBottom = obj.TargetDotGain - thresHoldValue;
            obj.ThresholdTop = ThresholdTop;
            obj.ThresholdBottom = ThresholdBottom;
        });
        //console.log(isoYellowList);
    }

    if (jsonDTISOTVIChartList.length > 0) {
        isoBlackList = $.grep(jsonDTISOTVIChartList, function(element) {
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
        isoBlackList.sort(function(a, b) { return a.CmykK - b.CmykK; });

        $.each(isoBlackList, function(index, obj) {
            var thrashHoldObject = [];
            var ThresholdTop = 0;
            var ThresholdBottom = 0;
            var thresHoldValue = obj.Threshold;
            ThresholdTop = obj.TargetDotGain + thresHoldValue;
            ThresholdBottom = obj.TargetDotGain - thresHoldValue;
            obj.ThresholdTop = ThresholdTop;
            obj.ThresholdBottom = ThresholdBottom;
        });
        //console.log(isoBlackList);
    }

    $divChartTVIISO_CYAN.kendoChart({
        renderAs: "canvas",
        title: {
            text: "Cyan"
        },
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
                    template: '<table><tr><td><center><b>Cyan #= dataItem.CmykC.toFixed() #% Patch Matrics</b></center><td></tr><tr><td> Actual TVI:  #= dataItem.DotGain #%</td></tr><tr><td> Target TVI:  #= kendo.toString(dataItem.TargetDotGain, "n") #%</td></tr><tr><td> TVI Threashhold:  #= kendo.toString(dataItem.Threshold, "n") #%</td></tr></table>'
                }
            }
        ],
        valueAxis: {
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
        title: {
            text: "Magenta"
        },
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
                    template: '<table><tr><td><center><b>Cyan #= dataItem.CmykM.toFixed() #% Patch Matrics</b></center><td></tr><tr><td> Actual TVI:  #= dataItem.DotGain #%</td></tr><tr><td> Target TVI:  #= kendo.toString(dataItem.TargetDotGain, "n") #%</td></tr><tr><td> TVI Threashhold:  #= kendo.toString(dataItem.Threshold, "n") #%</td></tr></table>'
                }
            }
        ],
        valueAxis: {
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
        title: {
            text: "Yellow"
        },
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
                    template: '<table><tr><td><center><b>Cyan #= dataItem.CmykY.toFixed() #% Patch Matrics</b></center><td></tr><tr><td> Actual TVI:  #= dataItem.DotGain #%</td></tr><tr><td> Target TVI:  #= kendo.toString(dataItem.TargetDotGain, "n") #%</td></tr><tr><td> TVI Threashhold:  #= kendo.toString(dataItem.Threshold, "n") #%</td></tr></table>'
                }
            }
        ],
        valueAxis: {
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
        title: {
            text: "Black"
        },
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
                    template: '<table><tr><td><center><b>Cyan #= dataItem.CmykK.toFixed() #% Patch Matrics</b></center><td></tr><tr><td> Actual TVI:  #= dataItem.DotGain #%</td></tr><tr><td> Target TVI:  #= kendo.toString(dataItem.TargetDotGain, "n") #%</td></tr><tr><td> TVI Threashhold:  #= kendo.toString(dataItem.Threshold, "n") #%</td></tr></table>'
                }
            }
        ],
        valueAxis: {
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

    var jsonCMYK_C = $.grep(jsonDTSpreadChartData_CMYKList, function(element) { return (element.CmykC != 0 || element.DotGain == 0) });
    var jsonCMYK_M = $.grep(jsonDTSpreadChartData_CMYKList, function(element) { return (element.CmykM != 0 || element.DotGain == 0) });
    var jsonCMYK_Y = $.grep(jsonDTSpreadChartData_CMYKList, function(element) { return (element.CmykY != 0 || element.DotGain == 0) });
    var jsonCMYK_K = $.grep(jsonDTSpreadChartData_CMYKList, function(element) { return (element.CmykK != 0 || element.DotGain == 0) });


    $divChartTVIISO_CMYK.kendoChart({
        renderAs: "canvas",
        //title: {
        //    text: "TVI CMYK Chart"
        //},
        seriesDefaults: {
            type: "scatterLine",
            style: "smooth",

        },
        legend: {
            position: "top",
            //labels: {
            //    template: "Name: #= text #"
            //}
        },
        //dataSource: jsonKResult,
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
                        color: function(e) {
                            return 'black';
                        }
                    }
                },
                highlight: {
                    visible: false
                },
                tooltip: {
                    visible: true,
                    template: '<table><tr><td><center><b>Cyan #= dataItem.CmykC.toFixed() #% Patch Matrics</b></center><td></tr><tr><td> Actual TVI:  #= dataItem.DotGain #</td></tr></table>'
                },
                line: {
                    width: 5
                },
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
                        color: function(e) {
                            return 'black';
                        }
                    }
                },
                tooltip: {
                    visible: true,
                    template: '<table><tr><td><center><b>Cyan #= dataItem.CmykM.toFixed() #% Patch Matrics</b></center><td></tr><tr><td> Actual TVI:  #= dataItem.DotGain #</td></tr></table>'
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
                        color: function(e) {
                            return 'black';
                        }
                    }
                },
                tooltip: {
                    visible: true,
                    template: '<table><tr><td><center><b>Cyan #= dataItem.CmykY.toFixed() #% Patch Matrics</b></center><td></tr><tr><td> Actual TVI:  #= dataItem.DotGain #</td></tr></table>'
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
                    template: '<table><tr><td><center><b>Cyan #= dataItem.CmykK.toFixed() #% Patch Matrics</b></center><td></tr><tr><td> Actual TVI:  #= dataItem.DotGain #</td></tr></table>'
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
            //min: 0,
            //max: 20,
            majorUnit: 5,
            //axisCrossingValues: [0, 20],
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
        render: renderChartWithBackgroundByAxis
        //render: function (e) {
        //    var chart = e.sender;
        //    var xAxis = chart.getAxis("xAxis");
        //    var yAxis = chart.getAxis("yAxis");
        //    var xAxisArray = xAxis._axis.children.map(r => parseInt(r.content));
        //    var yAxisArray = yAxis._axis.children.map(r => parseInt(r.content));
        //    var xSlot = xAxis.slot(Math.abs(Math.min.apply(Math, xAxisArray)), Math.abs(Math.max.apply(Math, xAxisArray)));
        //    var ySlot = yAxis.slot(Math.abs(Math.min.apply(Math, yAxisArray)), Math.abs(Math.max.apply(Math, yAxisArray)));

        //    var rect = new geometry.Rect([
        //        // Origin X, Y
        //        xSlot.origin.x, ySlot.origin.y
        //    ], [
        //            // Width, height
        //            xSlot.width(), ySlot.height()
        //        ]);

        //    var path = drawing.Path.fromRect(rect, {
        //        stroke: null,
        //        fill: gradientGray
        //    });

        //    chart.surface.draw(path);
        //}
    });
}

////**************** POPULATE THE TVI SPREAD CHART  ****************////
function PopulateTVISpreadChart() {

    var jsonSpread_C = $.grep(jsonDTSpreadChartDataList, function(element) { return (element.CmykC == 0 && element.CmykM == 0 && element.CmykY == 0 && element.DotGain == 0) || element.CmykC != 0 });
    var jsonSpread_M = $.grep(jsonDTSpreadChartDataList, function(element) { return (element.CmykM == 0 && element.CmykC == 0 && element.CmykY == 0 && element.DotGain == 0) || element.CmykM != 0 });
    var jsonSpread_Y = $.grep(jsonDTSpreadChartDataList, function(element) { return (element.CmykY == 0 && element.CmykC == 0 && element.CmykM == 0 && element.DotGain == 0) || element.CmykY != 0 });

    var splineRangeResultList = $.grep(jsonDTSpreadSplineRangeChartDataList, function(element) { return element.UpperLimit != 0 && element.LowerLimit != 0 });

    var obj = Object.assign({}, splineRangeResultList[0]);
    obj.UpperLimit = null;
    obj.LowerLimit = null;
    obj.TVI = 0;

    splineRangeResultList.push(obj);

    obj = Object.assign({}, splineRangeResultList[0])
    obj.UpperLimit = null;
    obj.LowerLimit = null;
    obj.TVI = 100;
    splineRangeResultList.push(obj);


    splineRangeResultList = splineRangeResultList.sort(function(e, e1) { return e.TVI - e1.TVI });

    $.each(jsonSpread_C, function(index, obj) {

        if (obj.CmykC != 0) {

            var Mres = $.grep(jsonSpread_M, function(e) { return e.CmykM == obj.CmykC; });
            var magValue = Mres[0].DotGain;

            var Yres = $.grep(jsonSpread_Y, function(e) {
                return e.CmykY == obj.CmykC;
            });

            var yelValue = Yres[0].DotGain;

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


    $.each(jsonSpread_M, function(index, obj) {

        if (obj.CmykM != 0) {

            var Cres = $.grep(jsonSpread_C, function(e) { return e.CmykC == obj.CmykM; });
            var cynValue = Cres[0].DotGain;

            var Yres = $.grep(jsonSpread_Y, function(e) { return e.CmykY == obj.CmykM; });
            var yelValue = Yres[0].DotGain;

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

    $.each(jsonSpread_Y, function(index, obj) {

        if (obj.CmykY != 0) {

            var Cres = $.grep(jsonSpread_C, function(e) { return e.CmykC == obj.CmykY; });
            var cynValue = Cres[0].DotGain;

            var Mres = $.grep(jsonSpread_M, function(e) { return e.CmykM == obj.CmykY; });
            var magValue = Mres[0].DotGain;

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


    $divChartTVISpread.kendoChart({
        //title: {
        //    text: "Cyan"
        //},
        renderAs: "canvas",
        legend: {
            position: "top",
            //labels: {
            //    template: "Name: #= text #"
            //}
        },
        //dataSource: {
        //    data: jsonSpread_C
        //},
        series: [

            {
                data: jsonSpread_C,
                type: "line",
                field: "DotGain",
                color: "Cyan",
                name: "C",
                markers: { background: "Cyan" },
                tooltip: {
                    visible: true,
                    template: '<table><tr><td><center><b>Cyan #= dataItem.CmykC.toFixed() #% Patch Matrics</b></center><td></tr><tr><td> Actual TVI:  #= dataItem.DotGain #%</td></tr><tr><td> Spread Threshold:  #= kendo.toString(dataItem.DotGainSpreadTh, "n") #</td></tr><tr><td> #= dataItem.DifferentTitleOne#: #= dataItem.DiffrentOne.toFixed() #</td></tr><tr><td> #= dataItem.DifferentTitleTwo#: #= dataItem.DiffrentTwo.toFixed() #</td></tr></table>'
                }
            },

            {
                data: jsonSpread_M,
                type: "line",
                field: "DotGain",
                color: "Magenta",
                name: "M",
                markers: { background: "Magenta" },
                tooltip: {
                    visible: true,
                    template: '<table><tr><td><center><b>Magenta #= dataItem.CmykM.toFixed() #% Patch Matrics</b></center><td></tr><tr><td> Actual TVI:  #= dataItem.DotGain #%</td></tr><tr><td> Spread Threshold:  #= kendo.toString(dataItem.DotGainSpreadTh, "n") #</td></tr><tr><td> #= dataItem.DifferentTitleOne#: #= dataItem.DiffrentOne.toFixed() #</td></tr><tr><td> #= dataItem.DifferentTitleTwo#: #= dataItem.DiffrentTwo.toFixed() #</td></tr></table>'
                }
            },

            {
                data: jsonSpread_Y,
                type: "line",
                field: "DotGain",
                color: "Yellow",
                name: "Y",
                markers: { background: "Yellow" },
                tooltip: {
                    visible: true,
                    template: '<table><tr><td><center><b>Yellow #= dataItem.CmykY.toFixed() #% Patch Matrics</b></center><td></tr><tr><td> Actual TVI:  #= dataItem.DotGain #%</td></tr><tr><td> Spread Threshold:  #= kendo.toString(dataItem.DotGainSpreadTh, "n") #</td></tr><tr><td> #= dataItem.DifferentTitleOne#: #= dataItem.DiffrentOne.toFixed() #</td></tr><tr><td> #= dataItem.DifferentTitleTwo#: #= dataItem.DiffrentTwo.toFixed() #</td></tr></table>'
                }
            },
            {
                data: splineRangeResultList,
                type: "rangeArea",
                fromField: "LowerLimit",
                toField: "UpperLimit",
                categoryField: "TVI",
                color: "#148787",
                line: {
                    style: "smooth",
                    color: "#058e8e",
                    width: 1,
                },
                highlight: {
                    visible: false,
                }
            },
        ],
        valueAxis: {
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
            justified: true,
            categories: [0, 25, 50, 75, 100],
            //min: 0,
            //max: 100,
        },
        chartArea: {
            width: 650,
            height: 350
        },
        render: renderChartWithBackgroundByCategory
    });
}

////**************** POPULATE THE DELTA SUMMARY REPORT CHART  ****************////
function PopulateSummaryDeltaChart() {

    //var axis = json.map(function (a) { return a["?E"]; });
    //var axis = jsonDeltaEPrimaryPatchList.map(function (a) { return a.ΔE; });
    //var patchColorList = formattedDeltaEPrimaryPatchList.map(function (a) { return a.name; });
    var maxNum = Math.max.apply(Math, jsonSummaryValueList);
    var maxValue = 0;
    var marginValue = 1;
    switch (true) {
        case (maxNum < 5): maxValue = 5; marginValue = 1; break;
        case (maxNum == 5): maxValue = 6; marginValue = 1; break;
        case (maxNum >= 5 && maxNum < 10): maxValue = 10; marginValue = 2; break;
        case (maxNum >= 10 && maxNum < 15): maxValue = 15; marginValue = 3; break;
        case (maxNum >= 15 && maxNum < 20): maxValue = 20; marginValue = 5; break;
        case (maxNum >= 20 && maxNum < 30): maxValue = 30; marginValue = 5; break;
        case (maxNum >= 30): maxValue = 50; marginValue = 5; break;
    }

    //generate the second stacked chart by max value
    var stackedChart = [];
    var categoryList = [];
    $.each(jsonSummaryValueList, function(index, num) {
        var stack = {
            value: maxValue
        };
        stackedChart.push(stack);
    });

    var selectedDeltaE = $selectedDeltaE;
    if (selectedDeltaE == "E") {
        selectedDeltaE = "ΔE";
    }

    $.each(jsonSummaryNameList, function(index, text) {
        switch (text) {
            case "Avg_ΔE": categoryList.push("Average " + selectedDeltaE); break;
            case "Max_ΔE": categoryList.push("Maximum " + selectedDeltaE); break;
            case "PriΔE": categoryList.push("Max Primary " + selectedDeltaE); break;
            case "PriΔH": categoryList.push("Max Primary ΔH"); break;
            case "GrayΔE": categoryList.push("Gray Average " + selectedDeltaE); break;
            case "NinetyFiveΔE": categoryList.push("95th Percentile " + selectedDeltaE); break;
        }
    });

    //console.log(jsonSummaryNameList);
    //console.log(categoryList);

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
            color: "#A1A1A1"
        },
        series: [{
            data: jsonSummaryValueList,
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
            categories: categoryList,
            //categories: [],
            majorGridLines: {
                visible: false
            }
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
            max: maxValue,
            margin: marginValue
        },
        //tooltip: {
        //    visible: true,
        //    //template: "#= dataItem.title # <br/> Actual L: *#= dataItem.CielabL # a: *#= dataItem.CielabA # b: *#= dataItem.CielabB # <br/>Target L: *#= dataItem.TargetL # a: *#= dataItem.Targeta # b: *#= dataItem.Targetb # <br/>E:   #= value # (Thr.0.00)"
        //    template: "<b>#= dataItem.title #</b> <br/> " + _Resources._recActual + " " + _Resources._recL + ": #= dataItem.CielabL # " + _Resources._recA + ": #= dataItem.CielabA # " + _Resources._recB + ": #= dataItem.CielabB # <br/>" + _Resources._recTarget + " " + _Resources._recL + ": #= dataItem.TargetL # " + _Resources._recA + ": #= dataItem.Targeta # " + _Resources._recB + ": #= dataItem.Targetb # <br/>" + _Resources._recDeltaE + ":  #= value #"
        //},
        chartArea: {
            width: 310,
            height: 260
        },
        //render: renderChartWithBackgroundByCategory
    });
}

////**************** GET IMAGE OF AB COMPARSION SPIDER CHART FOR REPORT  ****************////
function GetABComparisionSpiderChartImage() {
    var chart = $chart_TargetVersusActualAB.data("kendoChart");
    return chart.imageDataURL();
    //var kendoOptions = $chart_TargetVersusActualAB.data("kendoChart").options;
    ////kendoOptions.plotArea.background = 'gray';
    ////kendoOptions.chartArea.width = 700;
    ////kendoOptions.chartArea.height = 100;
    //$kendoChartDummy.kendoChart(kendoOptions);
    //var dummyChart = $kendoChartDummy.data('kendoChart');
    //dummyChart.options.plotArea.background = '#D0D0D0';
    //dummyChart.options.transitions = false
    //dummyChart.refresh();
    //return dummyChart.imageDataURL();
}

////**************** GET IMAGE OF INK PAPER CHART FOR REPORT  ****************////
function GetInkPaperPatchChartImage() {
    var chart = $divChartInkPaperABComparision.data("kendoChart");
    return chart.imageDataURL();
    //var kendoOptions = $chart_Gamut_ABComparison.data("kendoChart").options;
    //kendoOptions.plotArea.background = '#D0D0D0';
    ////kendoOptions.chartArea.width = 700;
    ////kendoOptions.chartArea.height = 100;
    //$kendoChartDummy.kendoChart(kendoOptions);
    //var dummyChart = $kendoChartDummy.data('kendoChart');
    //dummyChart.options.transitions = false
    //dummyChart.refresh();
    //return dummyChart.imageDataURL();
}

////**************** GET IMAGE OF ACTUAL VS TARGET CHART FOR REPORT  ****************////
function GetActualVsTargetChartImage() {
    var chart = $chart_Gamut_ABComparison.data("kendoChart");
    return chart.imageDataURL();
    //var kendoOptions = $chart_Gamut_ABComparison.data("kendoChart").options;
    //kendoOptions.plotArea.background = '#D0D0D0';
    ////kendoOptions.chartArea.width = 700;
    ////kendoOptions.chartArea.height = 100;
    //$kendoChartDummy.kendoChart(kendoOptions);
    //var dummyChart = $kendoChartDummy.data('kendoChart');
    //dummyChart.options.transitions = false
    //dummyChart.refresh();
    //return dummyChart.imageDataURL();
}

////**************** GET IMAGE OF L-COMPARSION CHART FOR REPORT  ****************////
function GetLComparisionChartImage() {

    var chart = $chart_L_Comaparision.data("kendoChart");
    return chart.imageDataURL();

    //var kendoOptions = $chart_L_Comaparision.data("kendoChart").options;
    //kendoOptions.plotArea.background = '#D0D0D0';
    //kendoOptions.chartArea.width = 650;
    //kendoOptions.chartArea.height = 150;
    //$kendoChartDummy.kendoChart(kendoOptions);
    //var dummyChart = $kendoChartDummy.data('kendoChart');
    //dummyChart.options.transitions = false
    //dummyChart.refresh();
    //return dummyChart.imageDataURL();
}

////**************** GET IMAGE OF L-BALANCE CHART FOR REPORT  ****************////
function Get_L_BalanceChartImage() {
    var chart = $chart_L_BalanceChart.data("kendoChart");
    return chart.imageDataURL();

    //var kendoOptions = $chart_L_BalanceChart.data("kendoChart").options;
    //var kendodataSource = $chart_L_BalanceChart.data("kendoChart").dataSource;

    //kendoOptions.plotArea.background = '#D0D0D0';
    ////kendoOptions.chartArea.width = 700;
    ////kendoOptions.chartArea.height = 100;
    //$kendoChartDummy.kendoChart(kendoOptions);
    //var dummyChart = $kendoChartDummy.data('kendoChart');
    //dummyChart.dataSource = kendodataSource;
    //dummyChart.options.transitions = false
    //dummyChart.refresh();
    //return dummyChart.imageDataURL();
}

////**************** GET IMAGE OF A-BALANCE CHART FOR REPORT  ****************////
function Get_A_BalanceChartImage() {

    var chart = $chart_A_BalanceChart.data("kendoChart");
    return chart.imageDataURL();

    //var kendoOptions = $chart_A_BalanceChart.data("kendoChart").options;
    //var kendodataSource = $chart_A_BalanceChart.data("kendoChart").dataSource;
    //kendoOptions.plotArea.background = '#D0D0D0';
    ////kendoOptions.chartArea.width = 700;
    ////kendoOptions.chartArea.height = 100;
    //$kendoChartDummy.kendoChart(kendoOptions);
    //var dummyChart = $kendoChartDummy.data('kendoChart');
    //dummyChart.dataSource = kendodataSource;
    //dummyChart.options.transitions = false
    //dummyChart.refresh();
    //return dummyChart.imageDataURL();
}

////**************** GET IMAGE OF B-BALANCE CHART FOR REPORT  ****************////
function Get_B_BalanceChartImage() {

    var chart = $chart_B_BalanceChart.data("kendoChart");
    return chart.imageDataURL();


    //var kendoOptions = $chart_B_BalanceChart.data("kendoChart").options;
    //var kendodataSource = $chart_B_BalanceChart.data("kendoChart").dataSource;
    //kendoOptions.plotArea.background = '#D0D0D0';
    ////kendoOptions.chartArea.width = 700;
    ////kendoOptions.chartArea.height = 100;
    //$kendoChartDummy.kendoChart(kendoOptions);
    //var dummyChart = $kendoChartDummy.data('kendoChart');
    //dummyChart.dataSource = kendodataSource;
    //dummyChart.options.transitions = false
    //dummyChart.refresh();
    //return dummyChart.imageDataURL();
}

////**************** GET IMAGE OF DELTA SUMMARY CHART FOR REPORT  ****************////
function Get_DeltaSummaryChartImage() {
    var chart = $divChartSummaryDeltaChart.data("kendoChart");
    return chart.imageDataURL();
}

function createColumn(e, rect, color) {
    var origin = rect.origin;
    var center = rect.center();
    var bottomRight = rect.bottomRight();
    var radiusX = rect.width() / 2;
    var radiusY = radiusX / 3;
    var gradient = new drawing.LinearGradient({
        stops: [{
            offset: 0,
            color: color
        }, {
            offset: 0.5,
            color: color,
            opacity: 1.9
        },
        {
            offset: 0.5,
            color: color,
            opacity: 0.9
        }, {
            offset: 1,
            color: color
        }]
    });

    var path = new drawing.Path({
        fill: gradient,
        stroke: {
            color: "#ebebeb"
        }
    })
        .moveTo(origin.x, origin.y)
        .lineTo(origin.x, bottomRight.y)
        .arc(180, 0, radiusX, radiusY, true)
        .lineTo(bottomRight.x, origin.y)
        .arc(0, 180, radiusX, radiusY);

    var topArcGeometry = new geometry.Arc([center.x, origin.y], {
        startAngle: 0,
        endAngle: 360,
        radiusX: radiusX,
        radiusY: radiusY
    });

    var topArc = new drawing.Arc(topArcGeometry, {
        fill: {
            color: color
        },
        stroke: {
            color: "#ebebeb"
        }
    });
    var group = new drawing.Group();
    group.append(path, topArc);
    return group;
}

function renderChartWithBackgroundByAxis(e) {
    var xSlot = {};
    var ySlot = {};
    var path = {};
    var chart = e.sender;
    var xAxis = chart.getAxis("xAxis");
    var yAxis = chart.getAxis("yAxis");
    var xAxisArray = xAxis._axis.children.map(r => parseInt(r.content));
    var yAxisArray = yAxis._axis.children.map(r => parseInt(r.content));


    if (chart.element[0].id == "kendoChartTargetVersusActualAB" || chart.element[0].id == "kendoChartTargetVersusActualABLarge" || chart.element[0].id == "kendoChartGamut_ABComparison" || chart.element[0].id == "kendoChartABalanceChart" || chart.element[0].id == "kendoChartBBalanceChart" || chart.element[0].id == "kendoChartLComaparision" || chart.element[0].id == "divChartInkPaperPaperLab1") {
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


    if (chart.element[0].id == "kendoChartTargetVersusActualAB" || chart.element[0].id == "kendoChartTargetVersusActualABLarge" || chart.element[0].id == "kendoChartGamut_ABComparison" || chart.element[0].id == "divChartInkPaperPaperLab1") {
        path = drawing.Path.fromRect(rect, {
            stroke: null,
            fill: gradientRedGreen
        });
    }
    else if (chart.element[0].id == "kendoChartABalanceChart") {
        path = drawing.Path.fromRect(rect, {
            stroke: null,
            fill: gradientPinkGreen
        });
    }
    else if (chart.element[0].id == "kendoChartLBalanceChart") {
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
    else if (chart.element[0].id == "kendoChartBBalanceChart") {
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

function renderChartWithBackgroundByCategory(e) {
    var chart = e.sender;

    //get value axis object
    var valueAxis = chart.getAxis("valueAxis");
    //get maximum value from the value axis
    var maxValue = Math.abs(Math.max.apply(Math, valueAxis._axis.children.map(r => parseInt(r.content))))
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

function getBlobFromURI(url, data, responseType, success, error, continueFunc) {
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.open('post', url, true);
    xmlHttp.setRequestHeader("Content-Type", "text/plain; charset=utf-8");
    if (responseType != null) {
        xmlHttp.setRequestHeader("Accept", responseType);
        xmlHttp.responseType = "blob";
    }
    xmlHttp.send(data);
    xmlHttp.onreadystatechange = function() {
        if (xmlHttp.readyState === 4) {
            try {
                if (xmlHttp.status === 200) {
                    if (xmlHttp.responseType === "blob") {
                        success.call(null, xmlHttp.response, continueFunc, xmlHttp.getResponseHeader('Extension'));
                    } else {
                        success.call(null, JSON.parse(xmlHttp.responseText), continueFunc);
                    }
                } else {
                    if (xmlHttp.responseType === "blob") {
                        error.call(null, xmlHttp.response, continueFunc);
                    } else {
                        error.call(null, xmlHttp.responseText, continueFunc);
                    }
                }
            } catch (e) {
                if (continueFunc) {
                    continueFunc.call(null, e);
                }
            }
        } else {
        }
    };
};




function downloadFileDirect(file) {
    getBlobFromURI(file.URL, null, file.ContentType, function(response, continueFunc) {
        var downloadUri = URL.createObjectURL(response);

        var downloadElem = $("<a href=\"" + downloadUri + "\" download=\"" + file.Name + "\" target=\"_blank\">Download</a>");
        downloadElem.get(0).click();
        setTimeout(function() { URL.revokeObjectURL(downloadUri) }, 1000);

    }, function(error, continueFunc) {
        alert('File downloaded not succesfully');
    });
}