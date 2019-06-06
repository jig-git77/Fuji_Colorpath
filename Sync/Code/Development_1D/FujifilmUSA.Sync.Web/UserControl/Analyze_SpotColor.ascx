<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Analyze_SpotColor.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.Analyze_SpotColor" %>

<link href="Content/kendo/2018.1.221/kendo.common.min.css" rel="stylesheet" />
<link href="Content/kendo/2018.1.221/kendo.default.min.css" rel="stylesheet" />
<script src="Scripts/kendo/2018.1.221/kendo.all.min.js"></script>
<style>
    .PatchDetailsSummaryTable {
        line-height: 35px;
        width: 55%;
    }

        .PatchDetailsSummaryTable th {
            color: black;
            text-align: center;
            font-weight: bold;
            background-color: darkgray;
        }

        .PatchDetailsSummaryTable td {
            text-align: left;
        }

    .spinner {
        opacity: 0;
        display: none;
        width: 20px;
        height: 10px;
        margin-bottom: 5px;
        -webkit-transition: opacity 0.25s, max-width 0.45s;
        -moz-transition: opacity 0.25s, max-width 0.45s;
        -o-transition: opacity 0.25s, max-width 0.45s;
        transition: opacity 0.25s, max-width 0.45s;
        /* Duration fixed since we animate additional hidden width */
    }

    .has-spinner.active {
        cursor: progress;
    }

        .has-spinner.active .spinner {
            display: inline-block;
            opacity: 1;
            max-width: 50px;
            /* More than it will ever come, notice that this affects on animation duration */
        }
    /*#tblSecondRunDeltas td{
        min-width:60px;
    }*/

    .k-i-sort-asc-sm, .k-i-sort-desc-sm {
        margin-left: 0px !important;
    }

    .chartBackground {
        background: url('Images/LabBackdrop.jpg');
        background-repeat: no-repeat;
        background-size: cover;
        /*height: 190px;*/
        /*width: 278px;*/
    }
</style>

<script type="application/javascript">
    var record = 0;
    var SERVICE_BASE_URL = '';
    var Resources = {};
    var currentSpotName = '';
    var $hdnCurrentUserRole;
    var $hdnCurrentProjectLicenseType;
    //variables for chart 
    var drawing = kendo.drawing;
    var geometry = kendo.geometry;
    var hasMeasurement = false;
    var gradientGray = new drawing.LinearGradient({
        start: [1, 1], // Bottom left
        end: [1, 0],   // Top left
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
        //Default Action
        $(".hideclass").hide(); //Hide all content
        $("ul.tabs li:eq(" + $('#<%= hdnSelectedTabIndex.ClientID %>').val() + ")").addClass("active").show(); //Activate first tab
        $(".hideclass:eq(" + $('#<%= hdnSelectedTabIndex.ClientID %>').val() + ")").show(); //Show first tab content
        //On Click Event
        $("ul.tabs li").click(function () {
            $("ul.tabs li").removeClass("active"); //Remove any "active" class
            $('#<%= hdnSelectedTabIndex.ClientID %>').val($(this).index());
            $(this).addClass("active"); //Add "active" class to selected tab
            $(".hideclass").hide(); //Hide all tab content
            var activeTab = $(this).find("a").attr("href"); //Find the rel attribute value to identify the active tab + content
            $(activeTab).fadeIn(); //Fade in the active content

            return false;
        });
        var CurrentCulture = $("#hdfCurrentCulture").val();
        SERVICE_BASE_URL = $('#hdnBaseServiceUrl').val();
        var optimizationId = $('#hdfOptimizationRunID').val();
        $hdnCurrentProjectLicenseType = $('#hdnCurrentProjectLicenseType').val();
        $hdnCurrentUserRole = parseInt($('#hdnCurrentUserRole').val());
        var $divPathDetails = $("#divPathDetails");

        $divPathDetails.kendoGrid({
            dataSource: {
                //transport: {
                //    //read: "/FujifilmUSA.Sync.Web/Analyze_SpotColor.aspx/GetPatchDetails"
                //    read: "http://localhost/FujiFilmUSA.Sync.Service/api/Chart/GetPatchDetails/" + oId
                //},
                transport: {
                    read: function (options) {
                        kendo.ui.progress($('#divPathDetails .k-grid-content'), true);
                        $.ajax({
                            type: "GET",
                            url: SERVICE_BASE_URL + "api/Chart/GetSpotPatchDetails/",
                            data: { id: optimizationId },
                            dataType: "json",
                            headers: {
                                'Accept-Language': CurrentCulture
                            },
                            success: function (response) {
                                kendo.ui.progress($('#divPathDetails .k-grid-content'), false);
                                if (response.data != null) {
                                    Resources = response.data.GlobalResources;
                                    var spotStates = response.data.spotAnalyzeDetails;
                                    $('#hdfMeasurementID').val(spotStates.measurementID);

                                    //check base run or not
                                    hasMeasurement = parseInt($('#hdfMeasurementID').val()) > 0;

                                    $divPathDetails.find("th[data-field=SpotID] b").html(Resources.gridAnalyzeHeaderIDResource);
                                    $divPathDetails.find("th[data-field=SpotPatch] b").html(Resources.gridAnalyzeHeaderPatchResource);
                                    $divPathDetails.find("th[data-field=SpotView] b").html(Resources.gridAnalyzeHeaderViewResource);
                                    $divPathDetails.find("th[data-field=SpotName] b").html(Resources.gridAnalyzeHeaderSpotNameResource);
                                    $divPathDetails.find("th[data-field=ΔL] b").html(Resources.gridAnalyzeHeaderDeltaLResource);
                                    $divPathDetails.find("th[data-field=Δa] b").html(Resources.gridAnalyzeHeaderDeltaAResource);
                                    $divPathDetails.find("th[data-field=Δb] b").html(Resources.gridAnalyzeHeaderDeltaBesource);
                                    $divPathDetails.find("th[data-field=ΔE] b").html(Resources.gridAnalyzeHeaderDeltaEResource);
                                    $divPathDetails.find("th[data-field=ΔE_T_P] b").html(Resources.gridAnalyzeHeaderETPResource);
                                    $divPathDetails.find("th[data-field=ΔE_M_T] b").html(Resources.gridAnalyzeHeaderEMTResource);
                                    $divPathDetails.find("th[data-field=ΔE_M_P] b").html(Resources.gridAnalyzeHeaderEMPResource);

                                    generateABTargetChart(0, 0, 0, 0, hasMeasurement);
                                    generateLTargetChart(0, 0, hasMeasurement);
                                }
                                options.success(spotStates.patchDetails);
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                kendo.ui.progress($('#divPathDetails .k-grid-content'), false);
                                console.log(XMLHttpRequest);
                                //alert(XMLHttpRequest);
                            }
                        });
                    },

                },
                schema: {
                    model: {
                        fields: {
                            SpotID: {
                                from: "SpotID",
                                type: "number"
                            },
                            SpotName: {
                                from: "SpotName",
                                type: "string"
                            },
                            CMYK_C: {
                                from: "CMYK_C",
                                type: "number",
                                //format: "{n4}"
                            },
                            CMYK_M: {
                                from: "CMYK_M",
                                type: "number",
                                //format: "{n4}"
                            },
                            CMYK_Y: {
                                from: "CMYK_Y",
                                type: "number",
                                //format: "{n4}"
                            },
                            CMYK_K: {
                                from: "CMYK_K",
                                type: "number",
                                //format: "{n4}"
                            },
                            HTMLColor: {
                                from: "HTMLColor",
                                type: "string"
                            },
                            TargetL: {
                                from: "TargetL",
                                type: "number",
                                //format: "{n4}"
                            },
                            Targeta: {
                                from: "Targeta",
                                type: "number",
                                //format: "{n4}"
                            },
                            Targetb: {
                                from: "Targetb",
                                type: "number",
                                //format: "{n4}"
                            },
                            CIELAB_L: {
                                from: "CIELAB_L",
                                type: "number",
                                //format: "{n4}"
                            },
                            CIELAB_a: {
                                from: "CIELAB_a",
                                type: "number",
                                //format: "{n4}"
                            },
                            ΔL: {
                                from: "ΔL",
                                type: "number",
                                //format: "{n4}"
                            },
                            Δa: {
                                from: "Δa",
                                type: "number",
                                //format: "{n4}"
                            },
                            Δb: {
                                from: "Δb",
                                type: "number",
                                //format: "{n4}"
                            },
                            ΔE: {
                                from: "ΔE",
                                type: "number",
                                //format: "{n4}"
                            },

                            Measured_L: {
                                from: "Measured_L",
                                type: "number",
                                //format: "{n4}"
                            },
                            Measured_a: {
                                from: "Measured_a",
                                type: "number",
                                //format: "{n4}"
                            },
                            Measured_b: {
                                from: "Measured_b",
                                type: "number",
                                //format: "{n4}"
                            },
                            MeasurementID: {
                                from: "MeasurementID",
                                type: "number"
                            },
                            Predicted_HTMLColor: {
                                from: "Predicted_HTMLColor",
                                type: "string"
                            },
                            Measured_HTMLColor: {
                                from: "Measured_HTMLColor",
                                type: "string"
                            },
                            ΔE_T_P: {
                                from: "ΔE_T_P",
                                type: "number",
                                format: "{n4}"
                            },
                            ΔE_M_T: {
                                from: "ΔE_M_T",
                                type: "number",
                                format: "{n4}"
                            },
                            ΔE_M_P: {
                                from: "ΔE_M_P",
                                type: "number",
                                format: "{n4}"
                            },
                            SpotProjectLABID: {
                                from: "SpotProjectLABID",
                                type: "number",
                            },
                            IsSelected: {
                                from: "IsSelected",
                                type: "boolean",
                            },
                        }
                    }
                },
                pageSize: 20
            },
            resizable: true,
            sortable: true,
            filterable: {
                extra: false,
                operators: {
                    number: {
                        lte: "Is less than or equal to",
                        eq: "Is equal to",
                        neq: "Is not equal to",
                        gte: "Is greater than or equal to",
                        gt: "Is greater than",
                        lt: "Is less than"
                    },
                    string: {
                        contains: "Contains",
                        eq: "Is equal to",
                        neq: "Is not equal to",
                        startswith: "Starts with",
                        doesnotcontain: "Does not contain",
                        endswith: "Ends with"
                    }
                }
            },
            sortable: true,
            selectable: "true",
            //filterable: true,
            pageable: {
                refresh: true,
                pageSizes: true
            },
            noRecords: {
                template: ''
            },
            //toolbar: [{ 'name': 'downloadPatch', template: '<div style="float:right;"><button id="btnDownloadSpotPatch" onclick="generateReport(' + optimizationId + ')" class=\'k-button\'>Export to PDF</button></div>' }],
            toolbar: [{
                'name': 'downloadPatch', template: '<div><button id="btnSaveSpotPatch" data-id="' + optimizationId + '" class=\'k-button has-spinner\'><span class="spinner"><img src="Images/buttonloader.gif" /></span>Save</button> <div style="float:right;"><a id="lnkLinkFilter" title="Clear filter"><img id="imgClearFilter" src="Images/clear-fillter.png" style="width: 15px;margin-top: 5px;margin-right: 5px;"></img></a><button id="btnExportJsonMeasurement" style="display:none;" data-id="' + optimizationId + '" class=\'k-button has-spinner\' style="margin-right: 5px;"><span class="spinner"><img src="Images/buttonloader.gif" /></span>Export Measurement File</button><button id="btnDownloadSpotPatchExcel" data-id="' + optimizationId + '" class=\'k-button has-spinner\' style="margin-right: 5px;"><span class="spinner"><img src="Images/buttonloader.gif" /></span>Export to Excel</button><button id="btnDownloadSpotPatch" data-id="' + optimizationId + '" class=\'k-button has-spinner\'><span class="spinner"><img src="Images/buttonloader.gif" /></span>Export to PDF</button></div></div>'
            }],
            change: function (e) {
                var item = e.sender.dataItem(e.sender.select());
                ShowSpotColorPatchDetailsWindow(item.HTMLColor.substring(1), item.CIELAB_L, item.CIELAB_a, item.CIELAB_b, item.TargetL, item.Targeta, item.Targetb, item.Measured_L, item.Measured_a, item.Measured_b, item.MeasurementID, item.Predicted_HTMLColor.substring(1), item.Measured_HTMLColor.substring(1), item.ΔE, item.ΔE_T_P, item.ΔE_M_T, item.ΔE_M_P, item.SpotName);
            },
            columns: [
                {
                    field: "SpotID",
                    title: "<b>ID</b>",
                    width: 50,
                    filterable: false,
                },
                {
                    field: "SpotPatch",
                    filterable: false,
                    title: "<b>Patch</b>",
                    //template: "<span id='sp' style='display:inline-block;background-color:#FFB700;height:14px;width:30px;'></span>",
                    template: "<span id='sp' style='display:inline-block;background-color:#=HTMLColor#;height:14px;width:30px;'></span>",
                    width: 55
                },
                //{
                //    field: "SpotView",
                //    filterable: false,
                //    title: "<b>View</b>",
                //    template: "<center><a id='lnkView' title='View Patch Details' onclick='return ShowSpotColorPatchDetailsWindow(\"#=HTMLColor.substring(1)#\",#=CIELAB_L#,#=CIELAB_a#,#=CIELAB_b#,#=TargetL#,#=Targeta#,#=Targetb#,#=Measured_L#,#=Measured_a#,#=Measured_b#,#=MeasurementID#,\"#=Predicted_HTMLColor.substring(1)#\",\"#=Measured_HTMLColor.substring(1)#\",#=ΔE#,#=ΔE_T_P#,#=ΔE_M_T#,#=ΔE_M_P#,\"#=SpotName#\");'><img src='Images/detail1.png'></img></a></center>",
                //    width: 55,                    
                //},
                {
                    field: "IsSelected",
                    filterable: false,
                    //title: "<b>View</b>",
                    title: "<img src='Images/tick-mark.png' style='width: 13px;height: 13px;margin-left: 10px;vertical-align: middle;' />",
                    template: "<center><input type='checkbox' id='chkIsSelected' onclick='spotPatchSelected(this)' #=IsSelected?'checked':''#/></center>",
                    width: 55,
                    sortable: {
                        initialDirection: "desc"
                    }
                },
                {
                    field: "SpotName",
                    title: "<b>Spot Name</b>",
                    width: 175
                },
                {
                    field: "ΔL",
                    title: "<b title='Predicted ∆L*'>pΔL</b>",
                    width: 75
                },
                {
                    field: "Δa",
                    title: "<b title='Predicted ∆a*'>pΔa</b>",
                    width: 75
                },
                {
                    field: "Δb",
                    title: "<b title='Predicted ∆b*'>pΔb</b>",
                    width: 75
                },
                {
                    field: "ΔE",
                    title: "<b title='Predicted ∆E'>pΔE</b>",
                    width: 80
                },
                {
                    field: "ΔE_T_P",
                    title: "<b title='∆E Target-Prediction'>ΔE_T_P</b>",
                    width: 100
                },
                {
                    field: "ΔE_M_T",
                    title: "<b title='∆E Measured-Target'>ΔE_M_T</b>",
                    width: 100
                },
                {
                    field: "ΔE_M_P",
                    title: "<b title='∆E Measured-Prediction'>ΔE_M_P</b>",
                    width: 100
                }
            ],
            dataBound: function (e) {
                var measurementId = parseInt($('#hdfMeasurementID').val());

                if (measurementId > 0) {
                    this.hideColumn(4);
                    this.hideColumn(5);
                    this.hideColumn(6);
                    this.hideColumn(7);
                    $("#lnkPredicationExplainerRun0").css('display', 'none');
                }
                else {
                    this.hideColumn(8);
                    this.hideColumn(9);
                    this.hideColumn(10);
                    $("#lnkPredicationExplainer").css('display', 'none');
                }


                var dataSource = this.dataSource;
                this.element.find('tr').each(function () {
                    var row = $(this);
                    //set resource message
                    row.find('#lnkView').attr('title', Resources.gridAnalyzeToolTipViewResource);
                });

                //show measurement export button visible only for sys admin user
                if (measurementId > 0 && $hdnCurrentProjectLicenseType == ELicenseType.SpotLibrary && $hdnCurrentUserRole == ERole.SystemAdmin) {
                    $divPathDetails.find('.k-grid-toolbar #btnExportJsonMeasurement').show();
                }
            },

        });

        $("#lnkPredicationExplainerRun0").kendoTooltip({
            position: "right",
            content: '<table style="text-align: left;"><tr><td><b>Target vs. Predicted</b></td><td> Compares the defined target Lab values to the <br/><u>optimal</u> device performance for a given color.</td></tr></table>'
        }).data("kendoTooltip");


        $("#lnkPredicationExplainer").kendoTooltip({
            position: "right",
            content: '<table style="text-align: left;"><tr><td><b>Target vs. Predicted</b></td><td> Compares the defined target Lab values to the <br/><u>optimal</u> device performance for a given color.</td></tr>' +
                '<tr><td><b>Measured vs. Target</b></td><td> Compares the defined target Lab values to the <br/><u>actual</u> device performance for a given color.</td></tr>' +
                '<tr><td><b>Measured vs. Predicted</b></td><td> Compares the optimal device performance to the <br/>actual device performance for a given color.</td></tr></table>'
        }).data("kendoTooltip");


        $("#divPathDetails").kendoTooltip({
            filter: "tbody tr", //this filter selects the first column cells
            position: "right",
            content: 'Click a row for more detail'
        }).data("kendoTooltip");


        $('#lnkLinkFilter').on('click', function (e) {
            $("form.k-filter-menu button[type='reset']").trigger("click");
        });

        $('#btnDownloadSpotPatch').on('click', function (e) {
            e.preventDefault();
            var grid = $('#divPathDetails').data('kendoGrid')
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
                OptimizationRunId: parseInt(optimizationRun),
                sort: sortObj,
                filter: filterObj
            };

            //console.log(filterObj);
            $('#btnDownloadSpotPatch').addClass('active');
            $.ajax({
                type: "POST",
                url: SERVICE_BASE_URL + 'api/Analyze/GetSpotPatchDetailsReport',
                data: filterObj,
                dataType: "json",
                success: function (response) {
                    $('#btnDownloadSpotPatch').removeClass('active');
                    if (response.data != null) {
                        //console.log(response.data);
                        var fileName = '~' + response.data;
                        window.location.href = SERVICE_BASE_URL + '/api/Analyze/DownloadFile?filePath=' + fileName;
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $('#btnDownloadSpotPatchExcel').removeClass('active');
                    console.log(XMLHttpRequest.responseText);
                }
            });


        })

        $('#btnDownloadSpotPatchExcel').on('click', function (e) {
            e.preventDefault();
            var grid = $('#divPathDetails').data('kendoGrid')
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
                OptimizationRunId: optimizationRun,
                sort: sortObj,
                filter: filterObj
            };

            //console.log(filterObj);

            $('#btnDownloadSpotPatchExcel').addClass('active');
            $.ajax({
                type: "POST",
                url: SERVICE_BASE_URL + 'api/Analyze/GetSpotPatchDetailsExcelReport',
                data: filterObj,
                dataType: "json",
                success: function (response) {
                    $('#btnDownloadSpotPatchExcel').removeClass('active');
                    if (response.data != null) {
                        console.log(response.data);
                        var fileName = '~' + response.data;
                        window.location.href = SERVICE_BASE_URL + '/api/Analyze/DownloadFile?filePath=' + fileName;
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $('#btnDownloadSpotPatchExcel').removeClass('active');
                    console.log(XMLHttpRequest.responseText);
                }
            });
        });

        $('#btnSaveSpotPatch').on('click', function (e) {
            e.preventDefault();
            var dataSource = $('#divPathDetails').data("kendoGrid").dataSource;
            var changedSpots = $.grep(dataSource.data(), function (item) {
                return item.dirty == true;
            });

            var spotpatchlab = [];
            $.each(changedSpots, function (index, item) {
                var spotlab = {
                    SpotProjectLABID: item.SpotProjectLABID,
                    //SpotProjectID: 0,
                    //SpotID: 0,
                    //SpotName: '',
                    //Target_L: 0,
                    //Target_a: 0,
                    //Target_b: 0,
                    //Predicted_L: 0,
                    //Predicted_a: 0,
                    //Predicted_b: 0,
                    //deltaE: 0,
                    //Predicted_HTMLColor: '',
                    IsSelected: item.IsSelected,
                };
                spotpatchlab.push(spotlab);
            });

            var obj = {
                spotProjectLabs: spotpatchlab
            }

            //console.log(filterObj);
            $('#btnSaveSpotPatch').addClass('active');
            $.ajax({
                type: "POST",
                url: SERVICE_BASE_URL + 'api/Analyze/SaveSpotProjectLABPatches',
                data: obj,
                dataType: "json",
                success: function (response) {
                    $('#btnSaveSpotPatch').removeClass('active');
                    //if (response.data != null) {
                    //    //console.log(response.data);
                    //    var fileName = '~' + response.data;
                    //    window.location.href = SERVICE_BASE_URL + '/api/Analyze/DownloadFile?filePath=' + fileName;
                    //}
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $('#btnSaveSpotPatch').removeClass('active');
                    console.log(XMLHttpRequest.responseText);
                }
            });


        });

        $('#lnkGenerateSpotReport').on('click', function (e) {
            e.preventDefault();
            debugger;
            kendo.ui.progress($('#divReport'), true);

            var chartList = [];
            var grid = $("#divPathDetails").data('kendoGrid')
            var selectedPatch = $.grep(grid.dataSource.data(), function (elem) {
                return elem.IsSelected == true;
            });

            $.each(selectedPatch, function (index, item) {
                var detail = {
                    spotName: item.SpotName,
                    TargetL: item.TargetL,
                    Targeta: item.Targeta,
                    Targetb: item.Targetb,

                    CIELAB_L: item.CIELAB_L,
                    CIELAB_a: item.CIELAB_a,
                    CIELAB_b: item.CIELAB_b,

                    Measured_L: item.Measured_L,
                    Measured_a: item.Measured_a,
                    Measured_b: item.Measured_b,

                    deltaE: item.ΔE,
                    deltaE_M_P: item.ΔE_M_P,
                    deltaE_M_T: item.ΔE_M_T,
                    deltaE_T_P: item.ΔE_T_P,

                    HTMLColor: item.HTMLColor,
                };

                var P_T_L = item.TargetL - item.CIELAB_L;
                var M_T_L = item.TargetL - item.Measured_L;

                var P_T_A = item.Targeta - item.CIELAB_a;
                var M_T_A = item.Targeta - item.Measured_a;

                var P_T_B = item.Targetb - item.CIELAB_b;
                var M_T_B = item.Targetb - item.Measured_b;

                detail.patchChart_AB = generateABTargetChart(P_T_A, M_T_A, P_T_B, M_T_B, hasMeasurement);
                detail.patchChart_L = generateLTargetChart(P_T_L, M_T_L, hasMeasurement);

                chartList.push(detail);
            });

            var optimizeRunId = parseInt($('#hdfOptimizationRunID').val());
            var apiParameter = {
                OptimizeRunId: optimizeRunId,
                CurrentUserId: parseInt($('#hdnCurrentUserID').val()),
                hasMeasurement: hasMeasurement,
                chartDetailList: chartList
            };

            $.ajax({
                type: "POST",
                url: SERVICE_BASE_URL + 'api/Report/ExportSpotAnalyzeChartReport',
                data: apiParameter,
                dataType: "json",
                success: function (response) {
                    kendo.ui.progress($('#divReport'), false);
                    if (response.data != null) {
                        console.log(response.data);
                        var fileName = '~' + response.data;
                        window.location.href = SERVICE_BASE_URL + '/api/Analyze/DownloadFile?filePath=' + fileName;
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    kendo.ui.progress($('#divReport'), false);
                    console.log(XMLHttpRequest.responseText);
                }
            });
        });

        $('#btnExportJsonMeasurement').on('click', function (e) {
            e.preventDefault();

            var optimizationRun = $(this).attr('data-id');
            $('#btnExportJsonMeasurement').addClass('active');
            $.ajax({
                type: "POST",
                url: SERVICE_BASE_URL + 'api/Analyze/GetSpotMeasurementFileExport?OptimizeRunId=' + optimizationRun,
                //data: {
                //    MeasurementId: measurementID,
                //    IsSpotProject: true
                //},
                dataType: "json",
                success: function (response) {
                    $('#btnExportJsonMeasurement').removeClass('active');
                    if (response.data != null) {
                        var fileName = '~' + response.data;
                        window.location.href = SERVICE_BASE_URL + '/api/Analyze/DownloadFile?filePath=' + fileName;
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $('#btnExportJsonMeasurement').removeClass('active');
                    console.log(XMLHttpRequest.responseText);
                }
            });
        });
    });

    function generateABTargetChart(predicredA, measuredA, predicredB, measuredB, hasMeasurement) {


        var targetPaperGreenObj = [0, 0];
        var greenSeriesArray = [];
        greenSeriesArray.push(targetPaperGreenObj);
        var kendoGreenSeriseObj = {};
        kendoGreenSeriseObj.data = greenSeriesArray;
        kendoGreenSeriseObj.color = 'green';
        kendoGreenSeriseObj.markers = {
            background: 'green',
            size: 10,
            visual: function (e) {
                var src = kendo.format("Images/greendot.png");
                var image = new kendo.drawing.Image(src, e.rect);
                return image;
            }
        };
        kendoGreenSeriseObj.highlight = {
            visible: false
        }

        var targetPAPERRedObj = [predicredA, predicredB];
        var redSeriesArray = [];
        redSeriesArray.push(targetPAPERRedObj);
        var kendoRedSeriseObj = {};
        kendoRedSeriseObj.data = redSeriesArray;
        kendoRedSeriseObj.color = 'red';
        kendoRedSeriseObj.markers = {
            background: 'red',
            size: 7,
            visual: function (e) {
                var src = kendo.format("Images/purpledot.png");
                var image = new kendo.drawing.Image(src, e.rect);
                return image;
            }
        };
        kendoRedSeriseObj.highlight = {
            visible: false
        };

        var measuredPAPERBlueObj = [measuredA, measuredB];
        var blueSeriesArray = [];
        blueSeriesArray.push(measuredPAPERBlueObj);
        var kendoBlueSeriseObj = {};
        kendoBlueSeriseObj.data = blueSeriesArray;
        kendoBlueSeriseObj.color = 'blue';
        kendoBlueSeriseObj.markers = {
            background: 'blue',
            size: 15,
            visible: hasMeasurement,
            visual: function (e) {
                var src = kendo.format("Images/rotateguntarget.png");
                var image = new kendo.drawing.Image(src, e.rect);
                return image;
            }
        };
        kendoBlueSeriseObj.highlight = {
            visible: false
        }

        $("#divABCharts").kendoChart({
            seriesDefaults: {
                type: "scatter",
            },
            series: [
                kendoGreenSeriseObj,
                kendoRedSeriseObj,
                kendoBlueSeriseObj
            ],
            xAxis: {
                name: "xAxis",
                min: -10,
                max: 10,
                majorUnit: 2.5,
                axisCrossingValues: [-10, 10],
                labels: {
                    step: 2,
                    template: function (e) {
                        if (e.value == 0)
                            return 'Target';
                        return e.value;
                    },
                    font: "10px Arial,Helvetica,sans-serif"
                },
            },
            yAxis: {
                name: "yAxis",
                min: -10,
                max: 10,
                majorUnit: 2.5,
                axisCrossingValues: [-10, 10],
                labels: {
                    step: 2,
                    template: function (e) {
                        if (e.value == 0)
                            return 'Target';
                        return e.value;
                    },
                    rotation: 270,
                    font: "10px Arial,Helvetica,sans-serif"
                },
            },
            chartArea: {
                width: 175,
                height: 140,
                background: ''
            },
            plotArea: {
                background: "white",
                margin: {
                    top: 8,
                    right: 8,
                }
            },
        });

        var dummyChart = $('#divABCharts').data('kendoChart');
        dummyChart.options.transitions = false;
        dummyChart.refresh();
        return dummyChart.imageDataURL();
    }

    function generateLTargetChart(predicredL, measuredL, hasMeasurement) {

        var targetPAPERRedObj = [predicredL];
        var redSeriesArray = [];
        redSeriesArray.push(targetPAPERRedObj);
        var kendoRedSeriseObj = {};
        kendoRedSeriseObj.data = redSeriesArray;
        kendoRedSeriseObj.color = 'red';
        kendoRedSeriseObj.markers = {
            background: 'red',
            size: 7,
            visual: function (e) {
                var src = kendo.format("Images/purpledot.png");
                var image = new kendo.drawing.Image(src, e.rect);
                return image;
            }
        };
        kendoRedSeriseObj.highlight = {
            visible: false
        }

        var measuredPAPERBlueObj = [measuredL];
        var blueSeriesArray = [];
        blueSeriesArray.push(measuredPAPERBlueObj);
        var kendoBlueSeriseObj = {};
        kendoBlueSeriseObj.data = blueSeriesArray;
        kendoBlueSeriseObj.color = 'blue';
        kendoBlueSeriseObj.markers = {
            background: 'blue',
            size: 12,
            visible: hasMeasurement,
            visual: function (e) {
                var src = kendo.format("Images/guntarget.png");
                var image = new kendo.drawing.Image(src, e.rect);
                return image;
            }
        };
        kendoBlueSeriseObj.highlight = {
            visible: false
        }

        $("#divCharts").kendoChart({
            seriesDefaults: {
                type: 'line',
            },
            //series: [kendoGreenSeriseObj, kendoRedSeriseObj,kendoBlueSeriseObj],
            series: [kendoRedSeriseObj, kendoBlueSeriseObj],
            categoryAxis: {
                name: "categoryAxis",
                categories: [],
            },
            valueAxis: {
                name: "valueAxis",
                axisCrossingValues: [0, 10],
                min: -10,
                max: 10,
                majorUnit: 5,
                labels: {
                    template: function (e) {

                        if (e.value == 0)
                            return 'Target';
                        return e.value;
                    },
                    rotation: 270,
                    font: "10px Arial,Helvetica,sans-serif"
                },
            },
            chartArea: {
                width: 70,
                height: 140,
            },
            render: function (e) {
                var chart = e.sender;
                //get value axis object
                var valueAxis = chart.getAxis("valueAxis");
                //get maximum value from the value axis
                var valueChartArray = $.map(valueAxis._axis.children, function (elem) {
                    return parseInt(elem.content);
                });
                //assign all value number with number
                var valueArray = [];
                $.each(valueChartArray, function (index, item) {
                    valueArray.push(isNaN(item) ? 0 : item);
                });

                //get top slot
                var maxValue = Math.abs(Math.max.apply(Math, valueArray));
                var maxValueSlot = valueAxis.slot(maxValue);

                var minValue = Math.min.apply(Math, valueArray);
                var minValueSlot = valueAxis.slot(minValue);

                // Locate right-most category slot                
                var categoryAxis = chart.getAxis("categoryAxis");
                var lastCategoryIndex = Math.max(1, categoryAxis.range().max);
                var minCategorySlot = categoryAxis.slot(0);
                var maxCategorySlot = categoryAxis.slot(lastCategoryIndex);

                //calculate height and width
                var plotWidth = maxCategorySlot.origin.x - maxValueSlot.origin.x;
                var plotHeight = minValueSlot.origin.y - maxValueSlot.origin.y;

                //create ractangle
                var rect = new geometry.Rect([
                    // Origin X, Y
                    maxValueSlot.origin.x, maxValueSlot.origin.y
                ], [plotWidth, plotHeight]);

                //create path using gradient
                var path = drawing.Path.fromRect(rect, {
                    stroke: null,
                    fill: gradientGray
                });

                var line = new kendo.drawing.Path({
                    stroke: {
                        color: "green",
                        width: 1
                    }
                });
                var valueSlotLine = valueAxis.slot(0);
                line.moveTo(valueSlotLine.origin).lineTo([maxCategorySlot.origin.x, valueSlotLine.origin.y]);
                var group = new kendo.drawing.Group();
                group.append(path, line);

                //set the path
                chart.surface.draw(group);
            }
        });

        //return base64 string of chart
        var dummyChart = $('#divCharts').data('kendoChart');
        dummyChart.options.transitions = false;
        dummyChart.refresh();
        return dummyChart.imageDataURL();
    }

    function ShowSpotColorPatchDetailsWindow(htmlColor, actualL, actualA, actualB, targetL, targetA, targetB, measuredL, measuredA, measuredB, measurementID, P_htmlColor, M_htmlColor, deltaE, deltaTP, deltaMT, deltaMP, SpotName) {
        measuredL = (isNaN(measuredL) || measuredL == null) ? 0 : measuredL;
        measuredA = (isNaN(measuredA) || measuredA == null) ? 0 : measuredA;
        measuredB = (isNaN(measuredB) || measuredB == null) ? 0 : measuredB;
        deltaE = (isNaN(deltaE) || deltaE == null) ? 0 : deltaE;
        deltaTP = (isNaN(deltaTP) || deltaTP == null) ? 0 : deltaTP;
        deltaMT = (isNaN(deltaMT) || deltaMT == null) ? 0 : deltaMT;
        deltaMP = (isNaN(deltaMP) || deltaMP == null) ? 0 : deltaMP;
        currentSpotName = SpotName;
        //console.log("PopupSpotColorPatchDetails.aspx?htmlColor=" + htmlColor + "&actualL=" + actualL + "&actualA=" + actualA + "&actualB=" + actualB + "&targetL=" + targetL + "&targetA=" + targetA + "&targetB=" + targetB + "&measuredL=" + measuredL + "&measuredA=" + measuredA + "&measuredB=" + measuredB + "&measurementID=" + measurementID + "&P_htmlColor=" + P_htmlColor + "&M_htmlColor=" + M_htmlColor);
        var oWindow;
        var passingURL = "PopupSpotColorPatchDetails.aspx?htmlColor=" + htmlColor + "&actualL=" + actualL + "&actualA=" + actualA + "&actualB=" + actualB + "&targetL=" + targetL + "&targetA=" + targetA + "&targetB=" + targetB + "&measuredL=" + measuredL + "&measuredA=" + measuredA + "&measuredB=" + measuredB + "&measurementID=" + measurementID + "&P_htmlColor=" + P_htmlColor + "&M_htmlColor=" + M_htmlColor + "&deltaE=" + deltaE + "&deltaTP=" + deltaTP + "&deltaMT=" + deltaMT + "&deltaMP=" + deltaMP;
        oWindow = window.radopen(passingURL, "windowPatchDetails");
        oWindow.show();

        return false;
    }

    function generateReport(optimizationRun) {
        $.ajax({
            url: SERVICE_BASE_URL + 'api/Chart/GetSpotPatchDetailsReport',
            data: { OptimizationRunID: optimizationRun },
            type: 'GET',
            //headers: {
            //    "content-type": "application/json",
            //},
            success: function (response) {
                //console.log(response);
                window.open(SERVICE_BASE_URL + response.data, "_blank");
            },
            error: function (xhr, errorType, exception) {
                alert(exception);
            }
        });
    }

    function OnSpotDetailsLoad(sender, eventArgs) {
        sender.set_title('<table style="width:100%;margin-top: -4px;"><tr><td style="width:45%">Patch Details</td><td style="width:55%;">' + currentSpotName + '</td></tr></table>')
    }

    function OnSpotDetailsClose(sender, eventArgs) {
        sender.set_title('<table style="width:100%;margin-top: -4px;"><tr><td style="width:45%">Patch Details</td><td style="width:55%;"></td></tr></table>')
    }

    function spotPatchSelected(chk) {
        var uid = $(chk).closest("[data-uid]").data("uid")
        var dataSource = $('#divPathDetails').data("kendoGrid").dataSource;
        var item = dataSource.getByUid(uid);
        item.dirty = true;
        item.IsSelected = $(chk).prop('checked');
    }

</script>

<script type="text/javascript" language="javascript">

    //According to new or old Project open rad window
    function ChooseRadWindow() {
        if (jQuery("input[id$=hdHasMeasurement]").length > 0) {

            if (jQuery("input[id$=hdHasMeasurement]").val() == 'True' || jQuery("input[id$=hdHasMeasurement]").val() == 'true') {  //-------------If there are measurements under this opt. run, then only able to create DLP

                if (jQuery("input[id$=hdnIsNewNCP]").val() == "True") {
                    return true;
                }
                else {
                    if (jQuery("input[id$=hdnIsNcpCreated]").val() == "True") {
                        return true;
                    }
                    else {
                        var oWnd = $find("<%= ConfirmSaveWindow.ClientID %>");
                        oWnd.show();
                        return false;
                    }
                }
            }
            else
                return false;
        }
    }
</script>

<div class="contant_top">
    &nbsp;
</div>
<div class="contant_middle">
    <div class="head">
        <asp:HiddenField runat="server" ID="hdnBaseServiceUrl" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="hdnCurrentUserID" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="hdnIsNewNCP" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="hdnIsNcpCreated" ClientIDMode="Static" />
        <asp:HiddenField ID="hdfOptimizationRunID" runat="server" Value="0" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnSelectedTabIndex" runat="server" Value="0" />
        <asp:HiddenField ID="hdMeasurement" runat="server" Value="0" />
        <asp:HiddenField runat="server" ID="hdfCurrentCulture" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="hdnCurrentProjectLicenseType" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="hdnCurrentUserRole" ClientIDMode="Static" />
        <h1 id="header" runat="server">
            <asp:Localize runat="server" Text="Analyze" ID="btnAnalyze" meta:resourcekey="btnAnalyzeResource1"></asp:Localize></h1>
        <div class="Information_divAnalyze">
            <asp:LinkButton ID="lbtnDownloadAndSaveNCP" runat="server" CssClass="button" OnClick="lbtnDownloadAndSaveNCP_Click" OnClientClick="return ChooseRadWindow();">
                <span>
                    <asp:Image ID="Image2" runat="server" ImageUrl="../Images/export.png" meta:resourcekey="Image2Resource1" />
                    <asp:Localize runat="server" Text="Create NCP" ID="locCreateNCP" meta:resourcekey="locCreateNCPResource1"></asp:Localize>
                </span>
            </asp:LinkButton>
        </div>
    </div>
    <%--<div class="listDevice" style="margin-left: 10px;">
        <uc:Statusbar ID="fujiStatusbar" runat="server" Visible="False" />
    </div>--%>
    <div class="listDevice" style="margin-left: 10px;">
        <div id="divToRender" runat="server">
            <%--<div style="display: block; width: 100%">--%>
            <div id="divCharts" style="display: none; width: 100%"></div>
            <div style="display: none; width: 100%" class="chartBackground">
                <div id="divABCharts"></div>
            </div>

            <%--</div>--%>
            <table cellpadding="0" cellspacing="10" border="0" width="97%">
                <tr>
                    <td>
                        <section id="tabpart" style="margin-left: 0;">
                            <section class="inner_tab">
                                <ul class="tabs">
                                    <li><span><a href="#tab1">
                                        <asp:Localize Text="Summary" ID="locSummary"
                                            runat="server" meta:resourcekey="locSummaryResource1"></asp:Localize></a></span></li>
                                    <li><span><a href="#tab2">
                                        <asp:Localize Text="Patch Details" ID="locPatchDetails"
                                            runat="server" meta:resourcekey="locPatchDetailsResource1"></asp:Localize></a></span></li>
                                    <li><span><a href="#tab3">
                                        <asp:Localize Text="Report" ID="locReport"
                                            runat="server" meta:resourcekey="locReportResource1"></asp:Localize></a></span></li>
                                </ul>

                                <div class="clear"></div>
                            </section>

                            <div>
                                <section id="tab1" class="contantbox tabcontant hideclass">
                                    <div class="tab_top">&nbsp;</div>
                                    <div class="tab_middle">
                                        <div class="combopart">
                                            <div style="padding-left: 10px;">

                                                <table class="PatchDetailsSummaryTable">
                                                    <tr>
                                                        <td style="width: 42%;"></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <th colspan="2"><span>
                                                            <asp:Localize runat="server" ID="locSummary_Header"></asp:Localize>
                                                        </span></th>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <table id="tblFirstRunDeltas" runat="server" visible="false" style="width: 100%;">
                                                                <tr>
                                                                    <td style="width: 42%;"><span style="font-weight: bold;">
                                                                        <asp:Localize Text="Min ΔE" runat="server"
                                                                            ID="locMinDeltaE" meta:resourcekey="locMinDeltaEResource1"></asp:Localize>
                                                                    </span></td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="lblMinDeltaE" ClientIDMode="Static" Text="Loading..."></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><span style="font-weight: bold;">
                                                                        <asp:Localize Text="Average ΔE" runat="server"
                                                                            ID="locAvgDeltaE" meta:resourcekey="locAvgDeltaEResource1"></asp:Localize>
                                                                    </span></td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="lblAvgDeltaE" ClientIDMode="Static" Text="Loading..."></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><span style="font-weight: bold;">
                                                                        <asp:Localize Text="Max ΔE" runat="server"
                                                                            ID="loclocMaxDeltaE" meta:resourcekey="locMaxDeltaEResource1"></asp:Localize>
                                                                    </span></td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="lblMaxDeltaE" ClientIDMode="Static" Text="Loading..."></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" style="width: 100%;">
                                                                        <hr />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <%--<table id="tblSecondRunDeltas" runat="server" visible="false" style="width: 100%;">
                                                                <tr>
                                                                    <td style="width: 42%;"></td>
                                                                    <td><span style="font-weight: bold;">∆E Target-Prediction</span></td>
                                                                    <td><span style="font-weight: bold;">∆E Measured-Target</span></td>
                                                                    <td><span style="font-weight: bold;">∆E Measured-Prediction</span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><span style="font-weight: bold;">
                                                                        <asp:Localize Text="Average ΔE" runat="server"
                                                                            ID="Localize2" meta:resourcekey="locMinimumDeltaResource"></asp:Localize>
                                                                    </span></td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="lblMinETP" ClientIDMode="Static" Text="Loading..."></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="lblMinEMT" ClientIDMode="Static" Text="Loading..."></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="lblMinEMP" ClientIDMode="Static" Text="Loading..."></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><span style="font-weight: bold;">
                                                                        <asp:Localize Text="Average ΔE" runat="server"
                                                                            ID="locAverageDelta" meta:resourcekey="locAverageDeltaResource1"></asp:Localize>
                                                                    </span></td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="lblAvgETP" ClientIDMode="Static" Text="Loading..."></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="lblAvgEMT" ClientIDMode="Static" Text="Loading..."></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="lblAvgEMP" ClientIDMode="Static" Text="Loading..."></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><span style="font-weight: bold;">
                                                                        <asp:Localize Text="Average ΔE" runat="server"
                                                                            ID="Localize1" meta:resourcekey="locMaximumDeltaResource1"></asp:Localize>
                                                                    </span></td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="lblMaxETP" ClientIDMode="Static" Text="Loading..."></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="lblMaxEMT" ClientIDMode="Static" Text="Loading..."></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="lblMaxEMP" ClientIDMode="Static" Text="Loading..."></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4">
                                                                        <hr />
                                                                    </td>
                                                                </tr>
                                                            </table>--%>
                                                            <table id="tblSecondRunDeltas" runat="server" visible="false" style="width: 100%;">
                                                                <tr>
                                                                    <td style="width: 42%;"></td>
                                                                    <td><span style="font-weight: bold;">
                                                                        <asp:Localize Text="Average ΔE" runat="server"
                                                                            ID="Localize2" meta:resourcekey="locMinimumDeltaResource"></asp:Localize></span>
                                                                    </td>
                                                                    <td><span style="font-weight: bold;">
                                                                        <asp:Localize Text="Average ΔE" runat="server"
                                                                            ID="locAverageDelta" meta:resourcekey="locAverageDeltaResource1"></asp:Localize></span>
                                                                    </td>
                                                                    <td><span style="font-weight: bold;">
                                                                        <asp:Localize Text="Average ΔE" runat="server"
                                                                            ID="Localize1" meta:resourcekey="locMaximumDeltaResource1"></asp:Localize>
                                                                    </span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><span style="font-weight: bold;">∆E Target-Prediction</span></td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="lblMinETP" ClientIDMode="Static" Text="Loading..."></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="lblAvgETP" ClientIDMode="Static" Text="Loading..."></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="lblMaxETP" ClientIDMode="Static" Text="Loading..."></asp:Label></td>
                                                                        
                                                                </tr>
                                                                <tr>
                                                                    <td><span style="font-weight: bold;">∆E Measured-Target</span></td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="lblMinEMT" ClientIDMode="Static" Text="Loading..."></asp:Label></td>                                                                        
                                                                    <td>
                                                                        <asp:Label runat="server" ID="lblAvgEMT" ClientIDMode="Static" Text="Loading..."></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="lblMaxEMT" ClientIDMode="Static" Text="Loading..."></asp:Label></td>
                                                                        
                                                                </tr>
                                                                <tr>
                                                                    <td><span style="font-weight: bold;">∆E Measured-Prediction</span></td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="lblMinEMP" ClientIDMode="Static" Text="Loading..."></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="lblAvgEMP" ClientIDMode="Static" Text="Loading..."></asp:Label></td>                                                                        
                                                                    <td>
                                                                        <asp:Label runat="server" ID="lblMaxEMP" ClientIDMode="Static" Text="Loading..."></asp:Label></td>
                                                                        
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4">
                                                                        <hr />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>

                                                    <tr id="trMeasurementID" runat="server" visible="false">
                                                        <td>
                                                            <span style="font-weight: bold;">
                                                                <asp:Localize Text="MeasurementID" ID="Localize24" runat="server" meta:resourcekey="locMeasurementIDResource1"></asp:Localize>
                                                            </span>
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblMeasurementID" ClientIDMode="Static" Text="Loading..."></asp:Label></td>
                                                    </tr>
                                                    <tr id="trMeasurementName" runat="server" visible="false">
                                                        <td>
                                                            <span style="font-weight: bold;">
                                                                <asp:Localize Text="Measurement Name" ID="Localize3" runat="server" meta:resourcekey="locMeasurementNameResource1"></asp:Localize>
                                                            </span>
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblMeasurementName" ClientIDMode="Static" Text="Loading..."></asp:Label></td>
                                                    </tr>
                                                    <tr id="trMeasurementDate" runat="server" visible="false">
                                                        <td>
                                                            <span style="font-weight: bold;">
                                                                <asp:Localize Text="Measurement Date" ID="Localize4" runat="server" meta:resourcekey="locMeasurementDateResource1"></asp:Localize>
                                                            </span>
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblMeasurementDate" ClientIDMode="Static" Text="Loading..."></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><span style="font-weight: bold;">
                                                            <asp:Localize Text="Media" runat="server"
                                                                ID="locMedia" meta:resourcekey="locMediaResource1"></asp:Localize>
                                                        </span></td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblMedia"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><span style="font-weight: bold;">
                                                            <asp:Localize Text="Colorant" runat="server"
                                                                ID="locColorant" meta:resourcekey="locColorantResource1"></asp:Localize></span></td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblColorant"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><span style="font-weight: bold;">
                                                            <asp:Localize runat="server"
                                                                Text="Rip Settings" ID="locRipSettings"
                                                                meta:resourcekey="locRipSettingsResource1"></asp:Localize></span></td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblRipSettings"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><span style="font-weight: bold;">
                                                            <asp:Localize runat="server"
                                                                Text="Spot Target" ID="locSpotTarget"
                                                                meta:resourcekey="locSpotTargetResource1"></asp:Localize></span>
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblSpotTarget"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><span style="font-weight: bold;">
                                                            <asp:Localize runat="server"
                                                                Text="ΔE Type" ID="locDeltaType"
                                                                meta:resourcekey="locDeltaTypeResource"></asp:Localize></span>
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblDeltaType" Text="ΔE2000"></asp:Label></td>
                                                    </tr>
                                                </table>

                                            </div>
                                            <div class="clear"></div>
                                        </div>
                                    </div>
                                    <div class="tab_bottom">
                                        &nbsp;
                                    </div>
                                </section>
                            </div>


                            <div>
                                <section id="tab2" class="contantbox tabcontant hideclass">
                                    <div class="tab_top">&nbsp;</div>
                                    <div class="tab_middle">
                                        <div class="combopart">
                                            <a id="lnkPredicationExplainerRun0" style="position: absolute; margin-top: -43px; right: 0; margin-right: 50px; color: black; font-size: larger;"><u>What am I looking at?</u></a>
                                            <a id="lnkPredicationExplainer" style="position: absolute; margin-top: -43px; right: 0; margin-right: 50px; color: black; font-size: larger;"><u>What am I looking at?</u></a>
                                            <div style="padding-left: 10px;">
                                                <div id="divPathDetails"></div>
                                            </div>
                                            <div class="clear"></div>
                                        </div>
                                    </div>
                                    <div class="tab_bottom">
                                        &nbsp;
                                    </div>
                                </section>
                            </div>

                            <div>
                                <section id="tab3" class="contantbox tabcontant hideclass">
                                    <div class="tab_top">&nbsp;</div>
                                    <div class="tab_middle">
                                        <div class="combopart">
                                            <div id="divReport" style="padding-left: 10px; height: 450px;">
                                                <div>
                                                    <a id="lnkGenerateSpotReport">
                                                        <img id="imgSpotReport" src="Images/SpotDetailsReport.png" alt="Spot Details Report" />
                                                    </a>
                                                </div>
                                            </div>
                                            <div>
                                                <asp:Localize runat="server"
                                                    Text="*Select colors in the Patch Details tab and click 'save' to add colors to the Saved Color Report." ID="locSpotReportMessage"
                                                    meta:resourcekey="locSpotReportMessageResource1"></asp:Localize>
                                            </div>
                                            <div class="clear"></div>
                                        </div>
                                    </div>
                                    <div class="tab_bottom">
                                        &nbsp;
                                    </div>
                                </section>
                            </div>
                        </section>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>
<div class="contant_bottom">
    &nbsp;
</div>
<asp:HiddenField ID="hdHasMeasurement" runat="server" Value="0" />

<asp:HiddenField ID="hdfMeasurementID" runat="server" Value="0" ClientIDMode="Static" />
<telerik:RadWindowManager ID="RadWindowManager" runat="server" Modal="True"
    Height="240" Width="900" VisibleStatusbar="false"
    ShowContentDuringLoad="false" Behaviors="Move,Close">
    <Windows>
        <telerik:RadWindow ID="windowPatchDetails" runat="server" ShowContentDuringLoad="false"
            VisibleStatusbar="false" Title="Patch Details" Width="900" Height="330"
            Behaviors="Move,Close" OnClientClose="OnSpotDetailsClose" OnClientPageLoad="OnSpotDetailsLoad">
        </telerik:RadWindow>
        <telerik:RadWindow ID="ConfirmSaveWindow" runat="server" ShowContentDuringLoad="false"
            VisibleStatusbar="false" Title="Choose Next Action" Width="702" Height="250"
            Behaviors="Move,Close" NavigateUrl="~/PopupConfirmSave.aspx" meta:resourcekey="ConfirmSaveWindowResource1">
        </telerik:RadWindow>
    </Windows>
</telerik:RadWindowManager>
