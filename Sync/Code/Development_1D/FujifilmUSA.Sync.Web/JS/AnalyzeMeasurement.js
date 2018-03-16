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
    $isAverageOfMeasurements = $('#ContentPlaceHolder1_ucAnalyze_chkAnalyzeAverageOfMeasurements').is(':checked');
    $hdMeasurement = $('#ContentPlaceHolder1_ucAnalyze_hdMeasurement');
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
    $hdnCurrentMeasurement = $("#hdnCurrentMeasurement");
    $btnPrevious = $('#btnPrevious');
    $ddlView = $('#ddlView');
    $ddlPaging = $('#ddlPaging');
    $btnNext = $('#btnNext');
    $btnLast = $('#btnLast');
    $btnFirst = $('#btnFirst');
    $hdnCurrentProjectLicenseType = $('#hdnCurrentProjectLicenseType').val();
    CURRENT_USER_ID = parseInt($hdnCurrentUserID);
    SERVICE_BASE_URL = $hdnBaseServiceUrl;

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



////**************** START BIND THE ALL TABS  ****************////
function BindAllTabs() {

    //Bind Summary tab
    BindMeasurementSummary()

    //Ink paper tab charts
    BindInkPaperChartABComparision();
    
}

//Bind the measurement summary details
function BindMeasurementSummary() {
    var measurementID = parseInt($hdnCurrentMeasurement.val());
    var apiParam = {};

    $("#divMeasurementSummary").css('display', 'none');
    $("#divValidationSummary").css('display', 'none');

    if ($hdnCurrentDlpSubOperation == ELicenseType.Sync1D || $hdnCurrentDlpSubOperation == ELicenseType.Validation) {

        $("#divValidationSummary").css('display', 'block');
        apiParameter = {
            MeasurementId: measurementID,
            IsAverageOfMeasurements: $isAverageOfMeasurements,
            IsVerificationProject: $hdnVerification,
            IsSync1DTypeProject: $hdnSync1DType,
            fromDeltaE: $selectedDeltaE,
        }

        $.ajax({
            url: SERVICE_BASE_URL + 'api/Analyze/GetValidationMeasurementSummaryDetails',
            data: apiParameter,
            type: 'GET',
            headers: {
                "content-type": "application/json",
            },
            dataType: 'json',
            headers: {
                'Accept-Language': $CurrentCulture
            },
            success: function (response) {
                if (response.data != null) {
                    if (response.data.length > 0) {
                        var measurements = response.data;
                        console.log(measurements);
                        var measurement = measurements[0];
                        measurement.MeasurementDateFormatted = new Date(measurement.MeasurementDate);
                        var viewModel = kendo.observable(measurement);
                        if (measurements[0].PatchTolerances.length == 0) {

                            $('#tblPatchTolerance').css('display', 'none');
                        }
                        kendo.bind($("#divValidationSummary"), viewModel);

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

        $.ajax({
            url: SERVICE_BASE_URL + 'api/Analyze/GetMeasurementSummaryDetails',
            data: apiParameter,
            type: 'GET',
            headers: {
                "content-type": "application/json",
            },
            dataType: 'json',
            headers: {
                'Accept-Language': $CurrentCulture
            },
            success: function (response) {
                if (response.data != null) {
                    if (response.data.length > 0) {
                        var measurements = response.data;
                        var viewModel = kendo.observable(measurements[0]);
                        kendo.bind($("#divMeasurementSummary"), viewModel);                       
                    }
                }
            },
            error: function (xhr, errorType, exception) {
                alert(exception);
            }
        });
    }
}


////************************ CHART BINDINGS  ***********************////

////**************** START BIND THE ANALYZE CHART  ****************////
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
        success: function (response) {
            if (response.data != null) {
                jsonAnalyzeList = response.data;
                if ($hdnCurrentDlpSubOperation != ELicenseType.Validation) {
                    $('#divAnalyzeHeader').css('display', 'block');
                    $('#lblNewAnalyzeLabel').html(jsonAnalyzeList.Measurements[0].MeasurementName);
                } else {
                    $('#divAnalyzePagingSection').css('display', 'block');

                }
                //PopulateAnalyzeChart();
                //PopulateAnalyzeChart1();
                BindPageNumber();
                //SetInitialAnalyzeBinding();
            }
        },
        error: function (xhr, errorType, exception) {
            kendo.ui.progress($chartLoader, false);
        }
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
        error: function (xhr, errorType, exception) {
            //alert(exception);
            kendo.ui.progress($chartLoader, false);
        }
    });
}



////********************************************** CHART POPULATIONS  **********************************************////

////**************** POPULATE THE ANALYZE CHART  ****************////
function PopulateAnalyzeChart() {

    var measurementID = 0;
    var indexStart = 0;
    var indexEnd = 0;
    viewNumber = parseInt($ddlView.val());
    pageNumber = parseInt($ddlPaging.val());
    if (viewNumber != -1) {
        //indexEnd = viewNumber * pageNumber;
        //indexStart = indexEnd - viewNumber;
        //indexEnd = indexEnd > jsonAnalyzeList.Measurements.length ? jsonAnalyzeList.Measurements.length : indexEnd;

        indexEnd = jsonAnalyzeList.Measurements.length - ((totalPages - pageNumber) * viewNumber);
        indexStart = (indexEnd - viewNumber) < 0 ? 0 : (indexEnd - viewNumber);

    } else {
        indexEnd = jsonAnalyzeList.Measurements.length - 1;
        indexStart = 0;
    }

    $('#lblPagingMsg').html('Record(s) ' + (indexStart + 1) + ' to ' + (indexEnd) + ' of ' + jsonAnalyzeList.Measurements.length + '');


    var analyzeSeriesResult = $.grep(jsonAnalyzeList.Measurements, function (item, index) {
        return index > indexStart - 1 && index < indexEnd;
    });


    var patchArray = [];
    if (analyzeSeriesResult.length > 0) {

        //check last measurement
        //if ($hdnCurrentMeasurement.val() == '') {
        $hdnCurrentMeasurement.val(analyzeSeriesResult[analyzeSeriesResult.length - 1].MeasurementId);
        //}

        //get the name of the measurement name list
        //measurementNamesList = $.map(jsonAnalyzeList.Measurements, function (e) {
        //    return e.MeasurementName;
        //});

        measurementNamesList = $.map(analyzeSeriesResult, function (e) {
            var curObj = {
                MeasurementId: e.MeasurementId,
                MeasurementName: e.MeasurementName,
                IsUsed: e.IsUsed,
                MeasurementStatus: e.MeasurementStatus,
                MeasurementDate: e.MeasurementDate,
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
                            //markers: {
                            //    background: arr1Obj1[0].HtmlColor
                            //}
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
            // name: "valueAxis",
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
                        //

                        //set checked or not
                        var catText = '<div style="font-size:12px;cursor: pointer;text-align:center;">' + formatMeasurementDateWithTime(catValue.MeasurementDate) + '</div>';
                        //if (catValue.IsUsed) {
                        //    catText = '<div style="font-size:18px;cursor: pointer;">☑</div>'
                        //}
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
                    } else {

                        //set checked or not
                        var catText = '<div style="font-size:18px;cursor: pointer;">' + (catValue.IsUsed ? '☑' : '☐') + '</div>'
                        //if (catValue.IsUsed) {
                        //    catText = '<div style="font-size:18px;cursor: pointer;">☑</div>'
                        //}
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
            background: "gray",
            opacity: 0.3
        },

        axisLabelClick: function (e) {

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

            $.ajax({
                url: SERVICE_BASE_URL + 'api/Chart/UpdateMeasurementFromAnalyzeChart',
                data: apiParameters,
                type: 'GET',
                dataType: 'json',
                success: function (response) {
                    $hdnCurrentMeasurement.val(currentObj.MeasurementId);
                    BindAnalyzeChart();
                },
                error: function (xhr, errorType, exception) {
                    //alert(exception);                    
                }
            });

        },
        seriesClick: function (e) {
            //alert(1);
            $hdnCurrentMeasurement.val(e.category.MeasurementId);
            $hdMeasurement.val($hdnCurrentMeasurement.val());
            BindAllTabs();

            $divAnalyzeChart.data('kendoChart').refresh();
        }
    });

    //$hdMeasurement.val($hdnCurrentMeasurement.val());
    BindAllTabs();
}

////**************** POPULATE THE INK PAPER AB COMPARISION CHART  ****************////
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


    if ($hdnCurrentDlpSubOperation == ELicenseType.Sync1D || $hdnCurrentDlpSubOperation == ELicenseType.Validation) {
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


    var maxValue = 0;
    var marginValue = 1;
    switch (true) {
        case (maxDeltaLimit < 5): maxValue = 5; marginValue = 1; break;
        case (maxDeltaLimit == 5): maxValue = 6; marginValue = 1; break;
        case (maxDeltaLimit >= 5 && maxDeltaLimit < 8): maxValue = 8; marginValue = 2; break;
        case (maxDeltaLimit >= 8 && maxDeltaLimit < 10): maxValue = 10; marginValue = 2; break;
        case (maxDeltaLimit >= 10 && maxDeltaLimit < 15): maxValue = 15; marginValue = 3; break;
        case (maxDeltaLimit >= 15 && maxDeltaLimit < 20): maxValue = 20; marginValue = 5; break;
        case (maxDeltaLimit >= 20 && maxDeltaLimit < 30): maxValue = 30; marginValue = 5; break;
        case (maxDeltaLimit >= 30 && maxDeltaLimit < 40): maxValue = 40; marginValue = 5; break;
        case (maxDeltaLimit >= 40 && maxDeltaLimit < 50): maxValue = 50; marginValue = 5; break;
        case (maxDeltaLimit >= 50): maxValue = 50; marginValue = 5; break;
    }

    //generate the second stacked chart by max value
    var stackedChart = [];
    $.each(axis, function (index, num) {
        var stack = {
            value: maxDeltaLimit - num
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
            template: function (dataItem) {
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
        render: function (e) {

            //-----For Sync1D projects or validation projects
            if ($hdnCurrentDlpSubOperation == ELicenseType.Sync1D || $hdnCurrentDlpSubOperation == ELicenseType.Validation) {
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

                if ($hdnCurrentDlpSubOperation == ELicenseType.Sync1D && e.dataItem.preAvgDensity > 0) {
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
            //template: "#= dataItem.title # <br/> Actual L: *#= dataItem.CielabL # a: *#= dataItem.CielabA # b: *#= dataItem.CielabB # <br/>Target L: *#= dataItem.TargetL # a: *#= dataItem.Targeta # b: *#= dataItem.Targetb # <br/>E:   #= value # (Thr.0.00)"
            //template: "<b>#= dataItem.title #</b> <br/> " + _Resources._recActual + " :    #= dataItem.density # <br/>" + _Resources._recTarget + " :    #= dataItem.densityLimit # <br/>" + _Resources._recPrevious + " :    #= dataItem.preAvgDensity #"
            template: function (e) {
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
        PopulateAnalyzeChart();
    }
}

function SetPrevButtonDisabled(enabled) {
    $btnPrevious.prop('disabled', enabled);
    $btnFirst.prop('disabled', enabled);
}

function SetNextButtonDisabled(enabled) {
    $btnNext.prop('disabled', enabled);
    $btnLast.prop('disabled', enabled);
}

function SetInitialAnalyzeBinding() {
    SetNextButtonDisabled(true);
    $ddlPaging.find('option').last().attr('selected', true);
    PopulateAnalyzeChart();
}


/////////******************************** METHODS FOR BIND CHART BACKGROUNDS ********************************////////////

function renderChartWithBackgroundByAxis(e) {
    var xSlot = {};
    var ySlot = {};
    var path = {};
    var chart = e.sender;
    var xAxis = chart.getAxis("xAxis");
    var yAxis = chart.getAxis("yAxis");
    var xAxisArray = xAxis._axis.children.map(r => parseFloat(r.content));
    var yAxisArray = yAxis._axis.children.map(r => parseFloat(r.content));


    if (chart.element[0].id == "kendoChartTargetVersusActualAB" || chart.element[0].id == "kendoChartTargetVersusActualABLarge"
        || chart.element[0].id == "kendoChartGamut_ABComparison" || chart.element[0].id == "kendoChartABalanceChart"
        || chart.element[0].id == "kendoChartBBalanceChart" || chart.element[0].id == "kendoChartLComaparision"
        || chart.element[0].id == "divChartInkPaperPaperLab1" || chart.element[0].id == "kendoChartDummyBalanceL"
        || chart.element[0].id == "kendoChartDummyBalanceA" || chart.element[0].id == "kendoChartDummyBalanceB") {
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
    else if (chart.element[0].id == "kendoChartABalanceChart" || chart.element[0].id == "kendoChartDummyBalanceA") {
        path = drawing.Path.fromRect(rect, {
            stroke: null,
            fill: gradientPinkGreen
        });
    }
    else if (chart.element[0].id == "kendoChartLBalanceChart" || chart.element[0].id == "kendoChartDummyBalanceL") {
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
    else if (chart.element[0].id == "kendoChartBBalanceChart" || chart.element[0].id == "kendoChartDummyBalanceB") {
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
    var maxValue = Math.abs(Math.max.apply(Math, valueAxis._axis.children.map(r => parseFloat(r.content))))
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