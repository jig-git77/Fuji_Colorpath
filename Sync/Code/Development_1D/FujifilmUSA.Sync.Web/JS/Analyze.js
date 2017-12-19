////*********** Declarations ***********////
var jsonTarget = [];
var jsonColors = [];
var jsonGray = [];
var jsonDeltaEPrimaryPatchList = [];
var jsonDeltaPaperLAB_1 = [];
var jsonDensityList = [];

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

var _Resources = {};

var CURRENT_USER_ID
var SERVICE_BASE_URL = '';
var $hdnBaseServiceUrl;
var $hdnCurrentUserID;
var $btnGeneratePDF;
var $imgEyeABCompLarge;
var $chartLoader;
var $hdMeasurement;
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
var $divChartTVISpread;
var $divTVI_G7;
var $divTVI_ISO;

var $divChartTVIISO_CYAN;
var $divChartTVIISO_Magenta;
var $divChartTVIISO_Yellow;
var $divChartTVIISO_Black;
var $divChartTVIISO_CMYK;
var $tblDensityChart

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
        opacity: 0.4
    }, {
        offset: 1,
        color: "green",
        opacity: 0.5
    }]
});

var gradientYellowBlue = new drawing.LinearGradient({
    start: [0, 0], // Bottom left
    end: [0, 1],   // Top left
    stops: [{
        offset: 0,
        color: "yellow",
        opacity: 0.5
    }, {
        offset: 1,
        color: "blue",
        opacity: 0.5
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


$(document).ready(function () {

    ////*********** HTML Element Declarations ***********////
    $hdnBaseServiceUrl = $('#hdnBaseServiceUrl').val();
    $hdnCurrentUserID = $('#hdnCurrentUserID').val();
    $CurrentCulture = $("#hdfCurrentCulture").val();
    $hdnVerification = $('#hdnVerification').val();
    $hdnSync1DType = $('#hdnSync1DType').val();
    $hdnValidationType = isNaN(parseInt($('#hdnValidationType').val())) ? 0 : parseInt($('#hdnValidationType').val());
    $hdnCurrentDlpSubOperation = parseInt($('#hdnCurrentDlpSubOperation').val());
    $isAverageOfMeasurements = $('#ContentPlaceHolder1_ucAnalyze_chkAnalyzeAverageOfMeasurements').is(':checked');
    $hdMeasurement = $('#ContentPlaceHolder1_ucAnalyze_hdMeasurement').val();
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

    $divChartTVIISO_CYAN = $("#divChartTVIISO_CYAN");
    $divChartTVIISO_Magenta = $("#divChartTVIISO_Magenta");
    $divChartTVIISO_Yellow = $("#divChartTVIISO_Yellow");
    $divChartTVIISO_Black = $("#divChartTVIISO_Black");
    $divChartTVIISO_CMYK = $("#divChartTVIISO_CMYK");

    $tblDensityChart = $("#tblDensityChart");
    $selectedDeltaE = $rbtnListDeltaE.find('input:checked').val();
    CURRENT_USER_ID = parseInt($hdnCurrentUserID);
    SERVICE_BASE_URL = $hdnBaseServiceUrl;

    var measurementID = parseInt($hdMeasurement);

    if (!$selectedDeltaE) {//set default value for sync1D projects
        $selectedDeltaE = 'E2000';
    }

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
        success: function (response) {
            console.log(response);
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
        error: function (xhr, errorType, exception) {
            //alert(exception);
            kendo.ui.progress($chartLoader, false);
        }
    });


    BindInkPaperChartABComparision();

    if ($hdnValidationType == 1) {
        BindG7Charts();
    }

    if ($hdnCurrentDlpSubOperation == ELicenseType.Sync1D || $hdnCurrentDlpSubOperation == ELicenseType.Validation) {

        BindTVICharts();
    }

    $imgEyeABCompLarge.on('click', function (e) {
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

    $btnGeneratePDF.on('click', function (e) {
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
            ABComparisionSpiderChart: '',
            InkPaperPatchChart:'',
            ABActualTargetChart: '',
            LComparisionChart: '',
            LGrayBalanceChart: '',
            AGrayBalanceChart: '',
            BGrayBalanceChart: ''
        };

        apiParameter.ABComparisionSpiderChart = GetABComparisionSpiderChartImage();
        apiParameter.InkPaperPatchChart = GetInkPaperPatchChartImage();        
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
            success: function (response) {
                console.log(response);
                window.open(SERVICE_BASE_URL + response.data, "_blank");
            },
            error: function (xhr, errorType, exception) {
                alert(exception);

            }
        });
    });
});

////**************** START BIND THE AB COMPARISION CHART  ****************////
function BindChartTargetVersusActualAB() {


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

        var tooltipString = '<table><tr><td><center><b>' + tooltipCaption + '</b></center><td></tr>';
        tooltipString += '<tr><td>' + _Resources._recActual + ' ' + _Resources._recL + ': ' + element.CielabL + ' ' + _Resources._recA + ': ' + element.CielabA + ' ' + _Resources._recB + ': ' + element.CielabB + '</td></tr>';
        tooltipString += '<tr><td>' + _Resources._recTarget + ' ' + _Resources._recL + ': ' + element.TargetL + ' ' + _Resources._recA + ': ' + element.Targeta + ' ' + _Resources._recB + ': ' + element.Targetb + '</td></tr>';
        tooltipString += '<tr><td>' + _Resources._recDeltaL + '   ' + element["ΔL"] + '</td></tr>';
        tooltipString += '<tr><td>' + _Resources._recDeltaA + '   ' + element["Δa"] + '</td></tr>';
        tooltipString += '<tr><td>' + _Resources._recDeltaB + '   ' + element["Δb"] + '</td></tr>';
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
                template: '<table><tr><td><center><b>100% ' + _Resources._recgbPatchMetrics + '</b></center><td></tr><tr><td>' + _Resources._recActual + ' ' + _Resources._recL + ' : #= dataItem.CielabL # ' + _Resources._recA + ': #= dataItem.CielabA# ' + _Resources._recB + ' #= dataItem.CielabB#</td></tr><tr><td>' + _Resources._recTarget + ' L*: #=dataItem.TargetL# a*: #=dataItem.Targeta# b*: #=dataItem.Targetb#</td></tr><tr><td>' + _Resources._recDeltaL + '   #=dataItem.ΔL#</td></tr><tr><td>' + _Resources._recDeltaA + '   #=dataItem.Δa#</td></tr><tr><td>' + _Resources._recDeltaB + '   #=dataItem.Δb#</td></tr></table>'
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
                template: '<table><tr><td><center><b>100% ' + _Resources._recgbPatchMetrics + '</b></center><td></tr><tr><td>' + _Resources._recActual + ' ' + _Resources._recL + ' : #= dataItem.CielabL # ' + _Resources._recA + ': #= dataItem.CielabA# ' + _Resources._recB + ' #= dataItem.CielabB#</td></tr><tr><td>' + _Resources._recTarget + ' L*: #=dataItem.TargetL# a*: #=dataItem.Targeta# b*: #=dataItem.Targetb#</td></tr><tr><td>' + _Resources._recDeltaL + '   #=dataItem.ΔL#</td></tr><tr><td>' + _Resources._recDeltaA + '   #=dataItem.Δa#</td></tr><tr><td>' + _Resources._recDeltaB + '   #=dataItem.Δb#</td></tr></table>'
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
                template: '<table><tr><td><center><b>100% ' + _Resources._recgbPatchMetrics + '</b></center><td></tr><tr><td>' + _Resources._recActual + ' ' + _Resources._recL + ' : #= dataItem.CielabL # ' + _Resources._recA + ': #= dataItem.CielabA# ' + _Resources._recB + ' #= dataItem.CielabB#</td></tr><tr><td>' + _Resources._recTarget + ' L*: #=dataItem.TargetL# a*: #=dataItem.Targeta# b*: #=dataItem.Targetb#</td></tr><tr><td>' + _Resources._recDeltaL + '   #=dataItem.ΔL#</td></tr><tr><td>' + _Resources._recDeltaA + '   #=dataItem.Δa#</td></tr><tr><td>' + _Resources._recDeltaB + '   #=dataItem.Δb#</td></tr></table>'
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
        success: function (response) {
            console.log('Ink paper chart');
            console.log(response);
            debugger;
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
        error: function (xhr, errorType, exception) {
            //alert(exception);
            kendo.ui.progress($chartLoader, false);
        }
    });
}

function PopulateInkPaperChartABComparision() {


    debugger;

    //var json = jsonDeltaEPrimaryPatchList;

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

        };
        formattedDeltaEPrimaryPatchList.push(item);
    });

    //var axis = json.map(function (a) { return a["?E"]; });
    var axis = jsonDeltaEPrimaryPatchList.map(function (a) { return a.ΔE; });

    var patchColorList = formattedDeltaEPrimaryPatchList.map(function (a) { return a.name; });

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

            //visual: function (e) {
            //    return createColumn(e, e.rect, e.dataItem.color);
            //}
        },
        series: [{
            data: formattedDeltaEPrimaryPatchList,
            overlay: {
                gradient: "glass",
            }
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
            }
        },
        tooltip: {
            visible: true,
            //template: "#= dataItem.title # <br/> Actual L: *#= dataItem.CielabL # a: *#= dataItem.CielabA # b: *#= dataItem.CielabB # <br/>Target L: *#= dataItem.TargetL # a: *#= dataItem.Targeta # b: *#= dataItem.Targetb # <br/>E:   #= value # (Thr.0.00)"
            template: "<b>#= dataItem.title #</b> <br/> " + _Resources._recActual + " " + _Resources._recL + ": #= dataItem.CielabL # " + _Resources._recA + ": #= dataItem.CielabA # " + _Resources._recB + ": #= dataItem.CielabB # <br/>" + _Resources._recTarget + " " + _Resources._recL + ": #= dataItem.TargetL # " + _Resources._recA + ": #= dataItem.Targeta # " + _Resources._recB + ": #= dataItem.Targetb # <br/>" + _Resources._recDeltaE + ":  #= value #"
        },
        chartArea: {
            width: 310,
            height: 325
        },
        //render: renderChartWithBackgroundByCategory
    });

    //var densityPatchColor = [
    //    { colorName: _Resources._recCyan, colorTitle: _Resources._recCyanPathMetrics },
    //    { colorName: _Resources._recMagenta, colorTitle: _Resources._recMagentaPathMetrics },
    //    { colorName: _Resources._recYellow, colorTitle: _Resources._recYellowPathMetrics },
    //    { colorName: _Resources._recBlack, colorTitle: _Resources._recBlackPathMetrics }
    //];

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
            //visual: function (e) {
            //    return createColumn(e, e.rect, e.dataItem.color);
            //}
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
            template: "<b>#= dataItem.title #</b> <br/> " + _Resources._recActual + " :    #= dataItem.density # <br/>" + _Resources._recTarget + " :    #= dataItem.densityLimit # <br/>" + _Resources._recPrevious + " :    #= dataItem.preAvgDensity #"
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
        tooltipString += '<tr><td>' + _Resources._recActual + ' ' + _Resources._recL + ': ' + paperObj.CielabL + ' ' + _Resources._recA + ': ' + paperObj.CielabA + ' ' + _Resources._recB + ': ' + paperObj.CielabB + '</td></tr>';
        tooltipString += '<tr><td>' + _Resources._recTarget + ' ' + _Resources._recL + ': ' + paperObj.TargetL + ' ' + _Resources._recA + ': ' + paperObj.Targeta + ' ' + _Resources._recB + ': ' + paperObj.Targetb + '</td></tr>';
        tooltipString += '<tr><td>' + _Resources._recDeltaL + ':   ' + paperObj["ΔL"] + '</td></tr>';
        tooltipString += '<tr><td>' + _Resources._recDeltaA + ':   ' + paperObj["Δa"] + '</td></tr>';
        tooltipString += '<tr><td>' + _Resources._recDeltaB + ':   ' + paperObj["Δb"] + '</td></tr>';
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
        success: function (response) {
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
        error: function (xhr, errorType, exception) {
            //alert(exception);
            kendo.ui.progress($chartLoader, false);
        }
    });
}

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
            success: function (response) {

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
            error: function (xhr, errorType, exception) {
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
            success: function (response) {

                if (response.data != null) {

                    jsonTVIG7ChartList = response.data.DTTVIChartList;
                    PopulateTVIG7Chart();
                }

                //kendo.ui.progress($chartLoader, false);
            },
            error: function (xhr, errorType, exception) {
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
            name: "Non-Weighted",
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


////**************** START BIND THE TVI CHART  ****************////
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
                        color: function (e) {
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
                        color: function (e) {
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
            ThresholdTop = obj.TargetDotGain + thresHoldValue;
            ThresholdBottom = obj.TargetDotGain - thresHoldValue;
            obj.ThresholdTop = ThresholdTop;
            obj.ThresholdBottom = ThresholdBottom;
        });
        //console.log(isoCyanList);
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
                color: "Green",
                line: {
                    style: "smooth",
                    color: "green",
                    width: 1
                },
                highlight: {
                    visible: false,
                }
            },
            {
                type: "line",
                field: "TargetDotGain",
                color: "Green",
                markers: { background: "Green" },
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
                color: "Green",
                line: {
                    style: "smooth",
                    color: "green",
                    width: 1
                },
                highlight: {
                    visible: false
                }
            },
            {
                type: "line",
                field: "TargetDotGain",
                color: "Green",
                markers: { background: "Green" },
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
                color: "Green",
                line: {
                    style: "smooth",
                    color: "green",
                    width: 1
                },
                highlight: {
                    visible: false
                }
            },
            {
                type: "line",
                field: "TargetDotGain",
                color: "Green",
                markers: { background: "Green" },
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
                color: "Green",
                line: {
                    style: "smooth",
                    color: "green",
                    width: 1
                },
                highlight: {
                    visible: false
                }
            },
            {
                type: "line",
                field: "TargetDotGain",
                color: "Green",
                markers: { background: "Green" },
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

function PopulateTVIG7CMYKChart() {

    var jsonCMYK_C = $.grep(jsonDTSpreadChartData_CMYKList, function (element) { return (element.CmykC != 0 || element.DotGain == 0) });
    var jsonCMYK_M = $.grep(jsonDTSpreadChartData_CMYKList, function (element) { return (element.CmykM != 0 || element.DotGain == 0) });
    var jsonCMYK_Y = $.grep(jsonDTSpreadChartData_CMYKList, function (element) { return (element.CmykY != 0 || element.DotGain == 0) });
    var jsonCMYK_K = $.grep(jsonDTSpreadChartData_CMYKList, function (element) { return (element.CmykK != 0 || element.DotGain == 0) });


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
                        color: function (e) {
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
                        color: function (e) {
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
            min: 0,
            max: 20,
            majorUnit: 5,
            axisCrossingValues: [0, 20],
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

function PopulateTVISpreadChart() {

    var jsonSpread_C = $.grep(jsonDTSpreadChartDataList, function (element) { return (element.CmykC == 0 && element.CmykM == 0 && element.CmykY == 0 && element.DotGain == 0) || element.CmykC != 0 });
    var jsonSpread_M = $.grep(jsonDTSpreadChartDataList, function (element) { return (element.CmykM == 0 && element.CmykC == 0 && element.CmykY == 0 && element.DotGain == 0) || element.CmykM != 0 });
    var jsonSpread_Y = $.grep(jsonDTSpreadChartDataList, function (element) { return (element.CmykY == 0 && element.CmykC == 0 && element.CmykM == 0 && element.DotGain == 0) || element.CmykY != 0 });

    var splineRangeResultList = $.grep(jsonDTSpreadSplineRangeChartDataList, function (element) { return element.UpperLimit != 0 && element.LowerLimit != 0 });

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


    splineRangeResultList = splineRangeResultList.sort(function (e, e1) { return e.TVI - e1.TVI });

    $.each(jsonSpread_C, function (index, obj) {

        if (obj.CmykC != 0) {

            var Mres = $.grep(jsonSpread_M, function (e) { return e.CmykM == obj.CmykC; });
            var magValue = Mres[0].DotGain;

            var Yres = $.grep(jsonSpread_Y, function (e) {
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


    $.each(jsonSpread_M, function (index, obj) {

        if (obj.CmykM != 0) {

            var Cres = $.grep(jsonSpread_C, function (e) { return e.CmykC == obj.CmykM; });
            var cynValue = Cres[0].DotGain;

            var Yres = $.grep(jsonSpread_Y, function (e) { return e.CmykY == obj.CmykM; });
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

    $.each(jsonSpread_Y, function (index, obj) {

        if (obj.CmykY != 0) {

            var Cres = $.grep(jsonSpread_C, function (e) { return e.CmykC == obj.CmykY; });
            var cynValue = Cres[0].DotGain;

            var Mres = $.grep(jsonSpread_M, function (e) { return e.CmykM == obj.CmykY; });
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
                color: "Green",
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
    else if (chart.element[0].id == "kendoChartLComaparision" || chart.element[0].id == " kendoChartLBalanceChart") {
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

//function createColumn(rect, color) {
//    var origin = rect.origin;
//    var center = rect.center();
//    var bottomRight = rect.bottomRight();
//    var radiusX = rect.width() / 2;
//    var radiusY = radiusX / 3;
//    var gradient = new drawing.LinearGradient({
//        stops: [{
//            offset: 0,
//            color: color
//        }, {
//            offset: 0.5,
//            color: color,
//            opacity: 0.9
//        }, {
//            offset: 0.5,
//            color: color,
//            opacity: 0.9
//        }, {
//            offset: 1,
//            color: color
//        }]
//    });

//    var path = new drawing.Path({
//        fill: gradient,
//        stroke: {
//            color: "none"
//        }
//    })
//    //.moveTo(origin.x, origin.y)
//    //.lineTo(origin.x, bottomRight.y)
//    //.arc(180, 0, radiusX, radiusY, true)
//    //.lineTo(bottomRight.x, origin.y)
//    //.arc(0, 180, radiusX, radiusY);


//    //.moveTo(origin.x, origin.y)
//    //.lineTo(origin.x, bottomRight.y)
//    //.arc(180, 0, radiusX, radiusY, true)
//    //.lineTo(bottomRight.y, origin.x)
//    //.arc(0, 180, radiusY, radiusX);

//    var topArcGeometry = new geometry.Arc([center.x, origin.y], {
//        startAngle: 0,
//        endAngle: 360,
//        radiusX: radiusX,
//        radiusY: radiusY
//    });

//    var topArc = new drawing.Arc(topArcGeometry, {
//        fill: {
//            color: color
//        },
//        stroke: {
//            color: "#ebebeb"
//        }
//    });
//    var group = new drawing.Group();
//    group.append(path, topArc);
//    return group;
//}


