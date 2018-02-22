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

});


//Bind the measurement summary details
function BindMeasurementSummary() {
    var measurementID = parseInt($hdMeasurement.val());
    var apiParam = {};

    //$("#divMeasurementSummary").css('display', 'none');
    //$("#divValidationSummary").css('display', 'none');

    if ($hdnCurrentDlpSubOperation == ELicenseType.Sync1D || $hdnCurrentDlpSubOperation == ELicenseType.Validation) {

        //$("#divValidationSummary").css('display', 'block');
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

                        //$('#lblMeasurementNameNew').html(measurements[0].MeasurementName);
                        //$('#lblMeasurementID').html(measurements[0].MeasurementId);
                        //$('#lblMeasurementDate').html(formatMeasurementDateWithTime(measurements[0].MeasurementDate));
                        //$('#lblRAvg_ΔE').html(measurements[0].Avg_ΔE);
                        //$('#lblRMax_ΔE').html(measurements[0].Max_ΔE);
                        //$('#lblPri_ΔE').html(measurements[0].Pri_ΔE);
                        //$('#lblPri_ΔH').html(measurements[0].Pri_ΔH);
                        //$('#lblGray_ΔE').html(measurements[0].Gray_ΔE);
                        //$('#lblPercΔE_10').html(measurements[0].PercΔE_10);
                        //$('#lblPercΔE_90').html(measurements[0].PercΔE_90);
                        //$('#lblPercΔE_95').html(measurements[0].PercΔE_95);
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


////************************ CHART POPULATIONS  ***********************////

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

    $hdMeasurement.val($hdnCurrentMeasurement.val());
    //BindAllTabs();
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