////*********** Declarations ***********////
var CURRENT_USER_ID
var SERVICE_BASE_URL = '';
var $hdnBaseServiceUrl;
var $CurrentCulture;
var $hdnOptimizationRunID;
var $drpCurveType;
var $rbtnDetlaReflistClient;
var objectResult = {};
var _Resources = {};
var isCurrentDeltaSelected = true;


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


$(document).ready(function () {

    $hdnBaseServiceUrl = $('#hdnBaseServiceUrl').val();
    $CurrentCulture = $("#hdfCurrentCulture").val();
    $drpCurveType = $("#drpCurveType");
    SERVICE_BASE_URL = $hdnBaseServiceUrl;
    $hdnOptimizationRunID = $('#hdnOptimizationRunID').val();
    $hdnExportFileName = $('#hdnCurveFileName').val();
    $rbtnDetlaReflistClient = $('#rbtnDetlaReflistClient');

    $drpCurveType.kendoDropDownList({
        height: 1000
    });

    BindChart();
    $drpCurveType.on('change', function (e) {
        e.preventDefault();
        $('#HD_CurveType').val($(this).val());
        BindChart();
    });
    $("#tabstrip").kendoTabStrip({
        animation: {
            open: {
                effects: "fadeIn"
            }
        }
    });


    $('#BtnExportCurvesExportView').on('click', function (e) {
        e.preventDefault();
        var curveTypeID = parseInt($drpCurveType.val());

        var apiParam = {
            OptimizationRunId: parseInt($hdnOptimizationRunID),
            CurveType: curveTypeID,
            ExportFileName: $hdnExportFileName
        };

        $.ajax({
            type: "GET",
            url: SERVICE_BASE_URL + 'api/Report/ExportGenerateCurveChart',
            data: apiParam,
            dataType: "json",
            success: function (response) {
                if (response.data != null) {
                    console.log(response.data);
                    var fileName = '~' + response.data;
                    window.location.href = SERVICE_BASE_URL + '/api/Analyze/DownloadFile?filePath=' + fileName;
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                console.log(XMLHttpRequest.responseText);
            }
        });

    });

    $('#BtnExportCurvesDeltaView').on('click', function (e) {
        e.preventDefault();
        var curveTypeID = parseInt($drpCurveType.val());

        var apiParam = {
            OptimizationRunId: parseInt($hdnOptimizationRunID),
            CurveType: curveTypeID,
            ExportFileName: $hdnExportFileName,
            isCurrentRun: isCurrentDeltaSelected
        };

        $.ajax({
            type: "GET",
            url: SERVICE_BASE_URL + 'api/Report/ExportDeltaCurveChart',
            data: apiParam,
            dataType: "json",
            success: function (response) {
                if (response.data != null) {
                    console.log(response.data);
                    var fileName = '~' + response.data;
                    window.location.href = SERVICE_BASE_URL + '/api/Analyze/DownloadFile?filePath=' + fileName;
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                console.log(XMLHttpRequest.responseText);
            }
        });

    });

    $('[id^=rbtnDetlaReflistClient_]').on('change', function (e) {

        var radBtnValue = $(this).val();
        isCurrentDeltaSelected = (radBtnValue == "0") ? true : false;
        BindDeltaView(objectResult);
    });
});

function BindChart() {

    var curveTypeID = parseInt($drpCurveType.val());
    //---Set same value for "ColorGate-TVI" and "ColorGate-CGCX" like "5%" for export curve data
    if (curveTypeID == 100 || curveTypeID == 101) {
        curveTypeID = 5;
    }

    //---Set same value for "Onyx G7 Import" like "Nexus" for export curve data
    if (curveTypeID == 102) {
        curveTypeID = 45;
    }

    var apiParameters = {
        OptimizationRunId: $hdnOptimizationRunID,
        CurveType: curveTypeID,
    };

    $.ajax({
        url: SERVICE_BASE_URL + 'api/Chart/GenerateCurveChart',
        data: apiParameters,
        type: 'GET',
        dataType: 'json',
        headers: {
            'Accept-Language': $CurrentCulture
        },
        success: function (response) {
            if (response.data != null) {
                _Resources = response.data.GlobalResources;
                objectResult = response.data;
                BindExportView(objectResult);
                BindDeltaView(objectResult);
            }
        },
        error: function (xhr, errorType, exception) {
            kendo.ui.progress($chartLoader, false);
        }
    });
}

function BindExportView(result) {

    var kendoGrid = $("#tblExport").data("kendoGrid");

    $("#tblExport").kendoGrid({
        dataSource: {
            data: result.CurveExportView
        },
        schema: {
            model: {
                fields: {
                    ReferenceNo: {
                        from: "ReferenceNo",
                        type: "number",
                    },
                    CMYK_C: {
                        from: "CMYK_C",
                        type: "number",
                        format: "{n4}"
                    },
                    CMYK_M: {
                        from: "CMYK_M",
                        type: "number",
                        format: "{n4}"
                    },
                    CMYK_Y: {
                        from: "CMYK_Y",
                        type: "number",
                        format: "{n4}"
                    },
                    CMYK_K: {
                        from: "CMYK_K",
                        type: "number",
                        format: "{n4}"
                    },
                }
            }
        },
        pageable: false,
        scrollable: true,
        height: 400,
        columns: [
            {
                field: "ReferenceNo",
                filterable: false,
                title: "<center><b title='Reference%'>Reference%</b></center>",
                width: '80px',
            },
            {
                field: "CMYK_C",
                filterable: false,
                title: "<center><b title='C'>C</b></center>",
                width: '41px'
            },
            {
                field: "CMYK_M",
                filterable: false,
                title: "<center><b title='M'>M</b></center>",
                width: '41px'
            },
            {
                field: "CMYK_Y",
                filterable: false,
                title: "<center><b title='Y'>Y</b></center>",
                width: '41px'
            },
            {
                field: "CMYK_K",
                filterable: false,
                title: "<center><b title='K'>K</b></center>",
                width: '41px'
            }
        ],
        dataBound: function (e) {
            $(e.sender.tbody).find("tr td:nth-child(2)").css('background-color', '#E0F3FF');
            $(e.sender.tbody).find("tr td:nth-child(3)").css('background-color', '#FFEFF9');
            $(e.sender.tbody).find("tr td:nth-child(4)").css('background-color', '#FFFDEE');
            $(e.sender.tbody).find("tr td:nth-child(5)").css('background-color', '#E7EBEE');
        }
    });



    var listC = {
        name: "Cyan",
        color: "cyan",
        markers: { background: "black", border: 0.1, size: 3 },
        data: []
    };
    var listY = {
        name: "Yellow",
        color: "yellow",
        markers: { background: "black", border: 0.1, size: 3 },
        data: []
    };
    var listM = {
        name: "Magenta",
        color: "magenta",
        markers: { background: "black", border: 0.1, size: 3 },
        data: []
    };
    var listB = {
        name: "Black",
        color: "black",
        markers: { background: "black", border: 0.1, size: 3 },
        data: []
    };

    $.each(result.CurveExportView, function (index, obj) {
        listC.data.push([obj.ReferenceNo, obj.CMYK_C.toFixed(0)]);
        listM.data.push([obj.ReferenceNo, obj.CMYK_M.toFixed(0)]);
        listY.data.push([obj.ReferenceNo, obj.CMYK_Y.toFixed(0)]);
        listB.data.push([obj.ReferenceNo, obj.CMYK_K.toFixed(0)]);
    });




    $("#Chart_Curve").kendoChart({
        renderAs: "canvas",
        //title: {
        //    text: "Export View"
        //},
        legend: {
            visible: true,
            position: 'top'
        },
        seriesDefaults: {
            type: "scatterLine"
        },
        series: [listC, listY, listM, listB],
        xAxis: {
            name: "xAxis",
            min: 0,
            max: 100,
            majorUnit: 25,
            labels: {
                format: "{0}"
            }
        },
        yAxis: {
            name: "yAxis",
            min: 0,
            max: 100,
            majorUnit: 10,
            labels: {
                format: "{0}"
            },

        },
        //tooltip: {
        //    visible: true,
        //    format: "{1}% in {0} minutes"
        //},
        //chartArea: {
        //    background: "#EFEFEF"
        //    //width: 500,
        //    //height: 400
        //},
        render: renderChartWithBackgroundByAxis
    });
}

function BindDeltaView(result) {

    var deltaRefView = [];
    if (isCurrentDeltaSelected) {
        deltaRefView = result.CurveDeltaRefView;
    } else {
        deltaRefView = result.CurveDeltaPrevRunView;
    }

    $("#tblDeltaView").kendoGrid({
        dataSource: {
            data: deltaRefView
        },
        schema: {
            model: {
                fields: {
                    ReferenceNo: {
                        from: "ReferenceNo",
                        type: "number",
                    },
                    CMYK_C: {
                        from: "CMYK_C",
                        type: "number",
                        format: "{n4}"
                    },
                    CMYK_M: {
                        from: "CMYK_M",
                        type: "number",
                        format: "{n4}"
                    },
                    CMYK_Y: {
                        from: "CMYK_Y",
                        type: "number",
                        format: "{n4}"
                    },
                    CMYK_K: {
                        from: "CMYK_K",
                        type: "number",
                        format: "{n4}"
                    },
                }
            }
        },
        pageable: false,
        scrollable: true,
        height: 400,
        columns: [
            {
                field: "ReferenceNo",
                filterable: false,
                title: "<center><b title='Reference%'>Reference%</b></center>",
                width: '80px',
            },
            {
                field: "CMYK_C",
                filterable: false,
                title: "<center><b title='C'>C</b></center>",
                width: '41px'
            },
            {
                field: "CMYK_M",
                filterable: false,
                title: "<center><b title='M'>M</b></center>",
                width: '41px'
            },
            {
                field: "CMYK_Y",
                filterable: false,
                title: "<center><b title='Y'>Y</b></center>",
                width: '41px'
            },
            {
                field: "CMYK_K",
                filterable: false,
                title: "<center><b title='K'>K</b></center>",
                width: '41px'
            }
        ],
        dataBound: function (e) {
            $(e.sender.tbody).find("tr td:nth-child(2)").css('background-color', '#E0F3FF');
            $(e.sender.tbody).find("tr td:nth-child(3)").css('background-color', '#FFEFF9');
            $(e.sender.tbody).find("tr td:nth-child(4)").css('background-color', '#FFFDEE');
            $(e.sender.tbody).find("tr td:nth-child(5)").css('background-color', '#E7EBEE');
        }
    });

    var listC = {
        name: "Cyan",
        color: "cyan",
        markers: { background: "black", border: 0.1, size: 3, visible: false },
        data: []
    };
    var listY = {
        name: "Yellow",
        color: "yellow",
        markers: { background: "black", border: 0.1, size: 3, visible: false },
        data: []
    };
    var listM = {
        name: "Magenta",
        color: "magenta",
        markers: { background: "black", border: 0.1, size: 3, visible: false },
        data: []
    };
    var listB = {
        name: "Black",
        color: "black",
        markers: { background: "black", border: 0.1, size: 3, visible: false },
        data: []
    };

    $.each(deltaRefView, function (index, obj) {
        listC.data.push([obj.ReferenceNo, obj.CMYK_C.toFixed(0)]);
        listM.data.push([obj.ReferenceNo, obj.CMYK_M.toFixed(0)]);
        listY.data.push([obj.ReferenceNo, obj.CMYK_Y.toFixed(0)]);
        listB.data.push([obj.ReferenceNo, obj.CMYK_K.toFixed(0)]);
    });

    var minimumX = Math.min.apply(null, $.map(deltaRefView, function (e) {
        return Math.min(e.CMYK_C, e.CMYK_M, e.CMYK_Y, e.CMYK_K);
    }));

    var maximumX = Math.max.apply(null, $.map(deltaRefView, function (e) {
        return Math.max(e.CMYK_C, e.CMYK_M, e.CMYK_Y, e.CMYK_K);
    }));

    minimumX = parseInt(minimumX.toFixed(0)) - 1;
    maximumX = parseInt(maximumX.toFixed(0)) + 1;
    var division = (parseInt(minimumX.toFixed(0)) + parseInt(maximumX.toFixed(0))) / 5;

    $("#Chart_Delta").kendoChart({
        renderAs: "canvas",
        legend: {
            visible: true,
            position: 'top'
        },
        seriesDefaults: {
            type: "scatterLine",
            width: 2,
        },
        series: [listC, listY, listM, listB],
        xAxis: {
            name: "xAxis",
            min: 0,
            max: 100,
            majorUnit: 25,
            labels: {
                format: ""
            },
            line: {
                visible: false
            },
            axisCrossingValues: [-20, 100]
        },
        yAxis: {
            name: "yAxis",
            min: minimumX,
            max: maximumX,
            axisCrossingValues: [minimumX, 100]
        },
        render: renderChartWithBackgroundByAxis
    });
}

function renderChartWithBackgroundByAxis(e) {
    var xSlot = {};
    var ySlot = {};
    var path = {};
    var chart = e.sender;
    var xAxis = chart.getAxis("xAxis");
    var yAxis = chart.getAxis("yAxis");
    var xAxisArray = $.map(xAxis._axis.children, function (elem) {
        return parseFloat(elem.content);
    });
    var yAxisArray = $.map(yAxis._axis.children, function (elem) {
        return parseFloat(elem.content);
    });


    xSlot = xAxis.slot(Math.abs(Math.min.apply(Math, xAxisArray)), Math.abs(Math.max.apply(Math, xAxisArray)));
    ySlot = yAxis.slot(Math.abs(Math.min.apply(Math, yAxisArray)), Math.abs(Math.max.apply(Math, yAxisArray)));

    var yAxisHeight = ySlot.height();
    if (yAxisHeight == 0) {
        yAxisHeight = xSlot.origin.y - ySlot.origin.y
    }

    //set the rectangle area on chart
    var rect = new geometry.Rect([
        xSlot.origin.x, ySlot.origin.y
    ], [
            xSlot.width(), yAxisHeight
        ]);

    path = drawing.Path.fromRect(rect, {
        stroke: null,
        fill: gradientGray
    });

    chart.surface.draw(path);
}
