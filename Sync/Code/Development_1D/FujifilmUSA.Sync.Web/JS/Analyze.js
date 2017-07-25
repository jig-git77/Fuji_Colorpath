var jsonTarget = [];
var jsonColors = [];
var jsonGray = [];
var _Resources = {};


$(document).ready(function () {
    var SERVICE_BASE_URL = $('#hdnBaseServiceUrl').val();

    var measurementID = parseInt($('#ContentPlaceHolder1_ucAnalyze_hdMeasurement').val());
    var isAverageOfMeasurements = true;
    kendo.ui.progress($(".chart-loading"), true);
    $.ajax({
        url: SERVICE_BASE_URL + 'api/Chart/GenerateGrayChart',
        data: { MeasurementId: measurementID, IsAverageOfMeasurements: isAverageOfMeasurements },
        type: 'GET',
        dataType: 'json',
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
            kendo.ui.progress($(".chart-loading"), false);
        },
        error: function (xhr, errorType, exception) {
            //alert(exception);
            kendo.ui.progress($(".chart-loading"), false);
        }
    });

    $('#imgEyeABCompLarge').on('click', function (e) {
        e.preventDefault();

        var kendoOptions = $("#kendoChartTargetVersusActualAB").data("kendoChart").options;
        kendoOptions.chartArea.width = 580;
        kendoOptions.chartArea.height = 480;

        $("#kendoChartTargetVersusActualABLarge").kendoChart(kendoOptions);

        $("#kendoChartTargetVersusActualABLargeArea").kendoWindow({
            width: "600px",
            modal: true,
            height: '500px',
            resizable: false,
            title: "a* - b* Comparison Chart",
            //content: url,
            visible: false
        });

        var popup = $("#kendoChartTargetVersusActualABLargeArea").data('kendoWindow');
        popup.open();
        popup.center();
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
        seriesArray.push(targetPAPERObj);


        //Get colors from the colors array
        var colorsArray = $.grep(jsonColors, function (colorObj) { return colorObj.PatchType == element.PatchType });
        $.each(colorsArray, function (i, colorObj) {
            var dots = [colorObj.CielabA, colorObj.CielabB];
            seriesArray.push(dots);
        });

        //add actuall color object
        var actualObject = [element.CielabA, element.CielabB];
        seriesArray.push(actualObject);

        //create the series of the array for line
        var kendoSeriseObj = {};
        kendoSeriseObj.data = seriesArray;
        kendoSeriseObj.color = element.PatchType;
        kendoSeriseObj.markers = { background: element.PatchType, size: 6 };
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
        //var tooltipString = '<table><tr><td><center><b>' + element.PatchType + ' Patch Matrics</b></center><td></tr>';
        //tooltipString += '<tr><td>Actual L: *' + element.CielabL + ' a:*: ' + element.CielabA + ' b:*' + element.CielabB + '</td></tr>';
        //tooltipString += '<tr><td>Target L: *' + element.TargetL + ' a:*: ' + element.Targeta + ' b:*' + element.Targetb + '</td></tr>';
        //tooltipString += '<tr><td>ΔL*   ' + element["ΔL"] + '</td></tr>';
        //tooltipString += '<tr><td>Δa*   ' + element["Δa"] + '</td></tr>';
        //tooltipString += '<tr><td>Δb*   ' + element["Δb"] + '</td></tr>';
        //tooltipString += '</table>';

        var tooltipString = '<table><tr><td><center><b>' + tooltipCaption + '</b></center><td></tr>';
        tooltipString += '<tr><td>' + _Resources._recActual + ' ' + _Resources._recL + ': ' + element.CielabL + ' ' + _Resources._recA + ': ' + element.CielabA + ' ' + _Resources._recB + ': ' + element.CielabB + '</td></tr>';
        tooltipString += '<tr><td>' + _Resources._recTarget + ' L: *' + element.TargetL + ' a*: ' + element.Targeta + ' b*: ' + element.Targetb + '</td></tr>';
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


    $("#kendoChartTargetVersusActualAB").kendoChart({
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
            background: "lightgray"
        }
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
        actualColorPointObj.markers = { background: jsonTargetColor.PatchType, size: 10 };
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


    $("#kendoChartGamut_ABComparison").kendoChart({
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
            background: "lightgray"
        }
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


        var pointsObjSmall = {};
        pointsObjSmall.data = [[element.CielabA, element["ΔL"]]];
        pointsObjSmall.color = element.PatchType;
        pointsObjSmall.markers = { background: element.PatchType, size: 6 };


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
        tooltipString += '<tr><td>' + _Resources._recTarget + ' L: *' + element.TargetL + ' a*: ' + element.Targeta + ' b*: ' + element.Targetb + '</td></tr>';
        tooltipString += '<tr><td>' + _Resources._recDeltaL + '   ' + element["ΔL"] + '</td></tr>';
        tooltipString += '<tr><td>' + _Resources._recDeltaA + '   ' + element["Δa"] + '</td></tr>';
        tooltipString += '<tr><td>' + _Resources._recDeltaB + '   ' + element["Δb"] + '</td></tr>';
        tooltipString += '</table>';

        ////create tool tip
        //var tooltipString = '<table><tr><td><center><b>' + element.PatchType + ' Patch Matrics</b></center><td></tr>';
        //tooltipString += '<tr><td>Actual L: *' + element.CielabL + ' a:*: ' + element.CielabA + ' b:*' + element.CielabB + '</td></tr>';
        //tooltipString += '<tr><td>Target L: *' + element.TargetL + ' a:*: ' + element.Targeta + ' b:*' + element.Targetb + '</td></tr>';
        //tooltipString += '<tr><td>ΔL*   ' + element["ΔL"] + '</td></tr>';
        //tooltipString += '<tr><td>Δa*   ' + element["Δa"] + '</td></tr>';
        //tooltipString += '<tr><td>Δb*   ' + element["Δb"] + '</td></tr>';
        //tooltipString += '</table>';

        pointsObj.tooltip = { visible: true, template: tooltipString };
        pointsObjSmall.tooltip = { visible: true, template: tooltipString };

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
            paperLComparisionPoint.color = "Black";
            paperLComparisionPoint.markers = { background: "White", size: 10 };

            paperLComparisionPointSmall = pointsObjSmall;
            paperLComparisionPointSmall.color = "Black";
            paperLComparisionPointSmall.markers = { background: "White", size: 10 };
        }

    });

    $("#kendoChartLComaparision").kendoChart({
        seriesDefaults: {
            type: "scatter",
        },
        //title: {
        //    text: "L* Comparison"
        //},
        series: [cyanLComparisionPoint
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
        ],
        xAxis: {
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
            background: "lightgray"
        }
    });
}

////**************** START BIND THE GRAY BALANCE L* CHART  ****************////
function BindChartLGrayBalance() {


    $("#kendoChartLBalanceChart").kendoChart({
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
            markers: { background: "Green" },
            highlight: {
                visible: false
            }
        }, {
            xField: "CmykC",
            yField: "CielabL",
            color: "Black",
            markers: { background: "Black" },
            tooltip: {
                visible: true,
                template: '<table><tr><td><center><b>100% ' + _Resources._recgbPatchMetrics + '</b></center><td></tr><tr><td>' + _Resources._recActual + ' ' + _Resources._recL + ' : #= dataItem.CielabL # ' + _Resources._recA + ': #= dataItem.CielabA# ' + _Resources._recB + ' #= dataItem.CielabB#</td></tr><tr><td>' + _Resources._recTarget + ' L*: #=dataItem.TargetL# a*: #=dataItem.Targeta# b*: #=dataItem.Targetb#</td></tr><tr><td>' + _Resources._recDeltaL + '   #=dataItem.ΔL#</td></tr><tr><td>' + _Resources._recDeltaA + '   #=dataItem.Δa#</td></tr><tr><td>' + _Resources._recDeltaB + '   #=dataItem.Δb#</td></tr></table>'
            }
        }],
        xAxis: {
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
            background: "lightgray"
        }
    });

    $("#kendoChartABalanceChart").kendoChart({
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
            markers: { background: "Green" },
            highlight: {
                visible: false
            }
        }, {
            xField: "CmykC",
            yField: "CielabA",
            color: "Black",
            markers: { background: "Black" },
            tooltip: {
                visible: true,
                template: '<table><tr><td><center><b>100% ' + _Resources._recgbPatchMetrics + '</b></center><td></tr><tr><td>' + _Resources._recActual + ' ' + _Resources._recL + ' : #= dataItem.CielabL # ' + _Resources._recA + ': #= dataItem.CielabA# ' + _Resources._recB + ' #= dataItem.CielabB#</td></tr><tr><td>' + _Resources._recTarget + ' L*: #=dataItem.TargetL# a*: #=dataItem.Targeta# b*: #=dataItem.Targetb#</td></tr><tr><td>' + _Resources._recDeltaL + '   #=dataItem.ΔL#</td></tr><tr><td>' + _Resources._recDeltaA + '   #=dataItem.Δa#</td></tr><tr><td>' + _Resources._recDeltaB + '   #=dataItem.Δb#</td></tr></table>'
            }
        }],
        xAxis: {
            min: 0,
            max: 100,
            majorUnit: 25,
            axisCrossingValues: [0, 25],
            labels: {
                format: "{0}"
            },
        },
        yAxis: {
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
            background: "lightgray"
        }
    });

    $("#kendoChartBBalanceChart").kendoChart({
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
            markers: { background: "Green" },
            highlight: {
                visible: false
            }
        }, {
            //data: grayBlackL,
            xField: "CmykC",
            yField: "CielabB",
            color: "Black",
            markers: { background: "Black" },
            tooltip: {
                visible: true,
                template: '<table><tr><td><center><b>100% ' + _Resources._recgbPatchMetrics + '</b></center><td></tr><tr><td>' + _Resources._recActual + ' ' + _Resources._recL + ' : #= dataItem.CielabL # ' + _Resources._recA + ': #= dataItem.CielabA# ' + _Resources._recB + ' #= dataItem.CielabB#</td></tr><tr><td>' + _Resources._recTarget + ' L*: #=dataItem.TargetL# a*: #=dataItem.Targeta# b*: #=dataItem.Targetb#</td></tr><tr><td>' + _Resources._recDeltaL + '   #=dataItem.ΔL#</td></tr><tr><td>' + _Resources._recDeltaA + '   #=dataItem.Δa#</td></tr><tr><td>' + _Resources._recDeltaB + '   #=dataItem.Δb#</td></tr></table>'
            }
        }],
        xAxis: {
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
            background: "lightgray"
        }
    });
}
