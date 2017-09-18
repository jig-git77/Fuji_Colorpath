var EOptimizeRunStatus = {
    Open: 1,
    Closed: 2,
    Certify: 3
}

var ELicenseType = {
    None: 0,
    Sync1D: 1,
    DLP: 2,
    InkOptimization: 3,
    InkOptimizationSource: 4,
    Sync4DCertification: 5,
    CustomSourceProfile: 6,
    ReProfileCustomSource: 7,
    FullGamutCustomSource: 8,
    MultiColorCustomSource: 9,
    SpotLibrary: 10,
    Validation: 11,
    IccProfiling: 12
}

var EValidationStatus = {
    None: 0,
    NotCertified: 1,
    Disabled: 2,
    NoMeasurement: 3,
    LastOK: 4,
    LastFailed: 5
}

var SERVICE_BASE_URL = '';
var $kendoProjectList;
var $hdfHasDeletePermission;
var CurrentDeviceID;
var CurrentUserID;

$(document).ready(function () {
    SERVICE_BASE_URL = $('#hdnBaseServiceUrl').val();
    $kendoProjectList = $("#kendoProjectList");
    $hdfHasDeletePermission = $("#hdfHasDeletePermission");
    CurrentDeviceID = $("#hdfCurrentDeviceID").val();
    CurrentUserID = $("#hdfCurrentUser").val();
    //var dID = $hdfCurrentDeviceID.val();
    $kendoProjectList.kendoGrid({
        dataSource: {
            transport: {
                read: function (options) {
                    //kendo.ui.progress($kendoProjectList, true);
                    kendo.ui.progress($('#kendoProjectList .k-grid-content'), true);
                    $.ajax({
                        type: "GET",
                        url: SERVICE_BASE_URL + "api/Project/GetProjectDetailsByDeviceID/",
                        data: { deviceID: CurrentDeviceID },
                        dataType: "json",
                        success: function (response) {                            
                            var jsonProjectList = JSON.parse(response.data);
                            options.success(jsonProjectList);
                            kendo.ui.progress($('#kendoProjectList .k-grid-content'), false);
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                            kendo.ui.progress($('#kendoProjectList .k-grid-content'), false);
                        }
                    });
                    //kendo.ui.progress($('#kendoProjectList .k-grid-content'), false);
                    //kendo.ui.progress($kendoProjectList, false);
                },
            },
            schema: {
                model: {
                    id: "ProjectId",
                    fields: {
                        ProjectName: {
                            from: "ProjectName"
                        },
                        SourceProfileName: {
                            from: "SourceProfile.ProfileName"
                        },
                        PrintConditionMedia: {
                            from: "PrintCondition.Media"
                        },
                        PrintConditionColorant: {
                            from: "PrintCondition.Colorant"
                        },
                        CreateDate: {
                            from: "CreateDate"
                        }
                    }
                }
            },
            pageSize: 10
        },
        sortable: true,
        filterable: true,        
        pageable: {
            refresh: true,
            pageSizes: true
        },
        noRecords: {
            template: "No records to display."
        },
        detailInit: detailInit,
        dataBound: function (e) {
            var projectTypeImagePath = '';
            var projectValidationImagePath = '';
            var projectValidationTooltip = '';
            var dataSource = this.dataSource;
            this.element.find('tr.k-master-row').each(function () {
                var row = $(this);
                var data = dataSource.getByUid(row.data('uid'));
                if (!(data.Status === EOptimizeRunStatus.Certify && data.LicenseType === ELicenseType.DLP)) {
                    row.find("td.k-hierarchy-cell .k-icon").removeClass();
                }


                //set project type image
                switch (data.Status) {
                    case EOptimizeRunStatus.Open:
                    case EOptimizeRunStatus.Closed:
                        switch (data.LicenseType) {
                            case ELicenseType.None: break;
                            case ELicenseType.Sync1D:
                                projectTypeImagePath = 'Images/AlignCurve.png';
                                break;
                            case ELicenseType.DLP:
                                projectTypeImagePath = 'Images/DLP.png';
                                break;
                            case ELicenseType.InkOptimization:
                                projectTypeImagePath = 'Images/InkOptimize.png';
                                break;
                            case ELicenseType.InkOptimizationSource:
                                projectTypeImagePath = 'Images/InkOptimizeSource.png';
                                break;
                            case ELicenseType.Sync4DCertification:
                                projectTypeImagePath = 'Images/4DCertification.png';
                                break;
                            case ELicenseType.CustomSourceProfile:
                                projectTypeImagePath = 'Images/DLPSource.png';
                                break;
                            case ELicenseType.ReProfileCustomSource:
                                projectTypeImagePath = 'Images/DLPReProfileSource.png';
                                break;
                            case ELicenseType.FullGamutCustomSource:
                                projectTypeImagePath = 'Images/DLPFullGamutSource.png';
                                break;
                            case ELicenseType.Validation:
                                break;
                            case ELicenseType.IccProfiling: break;
                        }
                        break;
                    case EOptimizeRunStatus.Certify:
                        switch (data.LicenseType) {
                            case ELicenseType.None: break;
                            case ELicenseType.Sync1D:
                                projectTypeImagePath = 'Images/AlignCurve_Certified.png';
                                break;
                            case ELicenseType.DLP:
                                projectTypeImagePath = 'Images/DLP_Certified.png';
                                break;
                            case ELicenseType.InkOptimization:
                                projectTypeImagePath = 'Images/InkOptimize_Certified.png';
                                break;
                            case ELicenseType.Sync4DCertification:
                                projectTypeImagePath = 'Images/4DCertification_Certified.png';
                                break;
                            case ELicenseType.Validation:
                                break;
                            case ELicenseType.IccProfiling: break;
                        }
                        break;
                }

                //set project validation image
                switch (data.ValidationStatus) {
                    case EValidationStatus.NotCertified:
                        projectValidationImagePath = 'Images/Validation_Not_Certified.png';
                        projectValidationTooltip = 'Validation can not be opened as the project is not in a Certified state.';
                        break;
                    case EValidationStatus.Disabled:
                        //projectValidationImagePath = 'Images/Validation_Not_Certified.png';
                        row.find("td #imgprojectValidation").css('display', 'none');
                        break;
                    case EValidationStatus.NoMeasurement:;
                        projectValidationImagePath = 'Images/Validation_Not_Active.png';
                        break;
                    case EValidationStatus.LastOK:
                        projectValidationImagePath = 'Images/Validation_Last_OK.png';
                        projectValidationTooltip = 'Last validation is OK';
                        break;
                    case EValidationStatus.LastFailed:
                        projectValidationImagePath = 'Images/Validation_Last_Failed.png';
                        projectValidationTooltip = 'Last validation failed';
                        break;
                }


                if (projectTypeImagePath != '') {
                    row.find("td #imgprojectType").attr('src', projectTypeImagePath);   //set the project type image path
                }
                if (projectValidationImagePath != '') {
                    row.find("td #imgprojectValidation").attr('src', projectValidationImagePath);   //set the project validation image path
                    row.find("td #imgprojectValidation").attr('title', projectValidationTooltip);   //set the project validation tooltip
                }

                switch (data.Status) {
                    case EOptimizeRunStatus.Open:
                        row.find("td #lnkProjectType").attr("onclick", "javascript: return ShowOpenPrintConditionWindow('" + data.ProjectId + "','" + data.ProjectName + "','10','" + data.CurrentDlpName + "','" + data.TotalRuns + "')");
                        break;
                    case EOptimizeRunStatus.Closed:
                        row.find("td #lnkProjectType").attr("onclick", "javascript: return ShowClosePrintConditionWindow('" + data.ProjectId + "','" + data.ProjectName + "','" + data.LicenseType + "','" + data.CurrentDlpName + "')");
                        break;
                    case EOptimizeRunStatus.Certify:
                        row.find("td #lnkProjectType").attr("onclick", "javascript: return ShowClosePrintConditionWindow('" + data.ProjectId + "','" + data.ProjectName + "','" + data.LicenseType + "','" + data.CurrentDlpName + "')");
                        break;
                }


            });

        },
        columns: [
            {
                //filterable: false,
                title: "<b>Type</b>",
                field: "LicenseType",
                template: "<center><a id='lnkProjectType'><img id='imgprojectType' src=''></img></a></center>",
                width: 30,
                filterable: {
                    extra: false,
                    operators: {
                        string: {
                            eq: "Is equal to"
                        }
                    },
                    ui: function (element) {

                        $.ajax({
                            type: "GET",
                            url: SERVICE_BASE_URL + "api/Device/GetProjectLicenseType/",
                            data: { UserID: CurrentUserID, DeviceID: CurrentDeviceID },
                            dataType: "json",
                            success: function (response) {
                                if (response.data.length > 0) {
                                    element.kendoDropDownList({
                                        dataSource: response.data,
                                        dataTextField: "Type",
                                        dataValueField: "LicenseTypeId",
                                    });
                                }
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                console.log(errorThrown);
                            }
                        });
                    }
                }
            },
            {
                //filterable: true,
                field: "ValidationStatus",
                title: "<b>Validation</b>",
                template: "<center><a onclick='return Redirect(#=id#,\"ProjectValidation\");'><img id='imgprojectValidation'  title='this will be displayed as a tooltip' src=''></img></a></center>",
                width: 30,
                filterable: {
                    extra: false,
                    operators: {
                        string: {
                            eq: "Is equal to"
                        }
                    },
                    ui: function (element) {
                        var validationStatus = [{
                            text: "NotCertified", value: 1
                        }, {
                            text: "Disabled", value: 2
                        }, {
                            text: "LastOK", value: 4
                        }, {
                            text: "LastFailed", value: 5
                        }];

                        element.kendoDropDownList({
                            dataSource: validationStatus,
                            dataTextField: "text",
                            dataValueField: "value",
                        });
                    }
                }
            },
           {
               field: "ProjectName",
               title: "<b>Project Name</b>",
               width: 50,
           },
           {
               field: "SourceProfileName",
               title: "<b>Standard</b>",
               width: 50
           },
           {
               field: "PrintConditionMedia",
               title: "<b>Media</b>",
               width: 50,
           },
           {
               field: "PrintConditionColorant",
               title: "<b>Colorant</b>",
               width: 50,
           },
            {
                field: "CreateDate",
                title: "<b>Last Active</b>",
                template: '#=kendo.toString(new Date(CreateDate), "MM/dd/yyyy hh:mm") #',
                width: 50,
                filterable: {
                    extra: false,
                    operators: {
                        date: {
                            eq: "Equal to",
                            neq: "Not equal to",
                            gte: "Is after or equal to",
                            gt: "Is after",
                            lte: "Is before or equal to",
                            lt: "Is before"
                        }
                    },
                    ui: "datetimepicker" // use Kendo UI DateTimePicker
                }
            },
            {
                filterable: false,
                title: "<b></b>",
                template: "<center><a id='lnkDeleteProject' onclick='return deleteProject(#=id#);'><img id='imgspotProject' src='Images/delete_icon.png' style='width: 15px;'></img></a></center>",
                width: 20
            }
        ]
    });


    var grid = $kendoProjectList.data("kendoGrid");

    if ($hdfHasDeletePermission.val() !== 'True') {
        grid.hideColumn(7);
    }

    $kendoProjectList.on('click', '#lnkDeleteSpotProject', function (e) {
        var spotProjectID = $(this).attr('data-spot');
        var detailGrid = $(this).closest('.k-grid').data("kendoGrid");

        //$(this).closest('.k-grid').data('kendoGrid').dataSource.read();
        //$(this).closest('.k-grid').data('kendoGrid').refresh();

        if (confirm('Are you sure you want to delete all information of selected Spot Target Project?')) {
            kendo.ui.progress($('#kendoProjectList .k-grid-content'), true);
            //kendo.ui.progress($kendoProjectList, true);
            $.ajax({
                type: "POST",
                url: SERVICE_BASE_URL + "api/Project/DeleteSpotProject?spotProjectID=" + spotProjectID,
                success: function (response) {
                    console.log(JSON.stringify(response));
                    detailGrid.dataSource.read();
                    detailGrid.refresh();
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    console.log(errorThrown);
                }
            });
            kendo.ui.progress($kendoProjectList, false);
        }

    })
});

function detailInit(e) {
    var pID = e.data.id;
    $("<div/>").appendTo(e.detailCell).kendoGrid({
        dataSource: {
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "GET",
                        url: SERVICE_BASE_URL + "api/Project/GetSpotProjectDetailsByProjectID/",
                        data: { projectID: pID },
                        dataType: "json",
                        success: function (response) {
                            var jsonSpotProjectList = JSON.parse(response.data);
                            console.log(response.data);
                            console.log(jsonSpotProjectList);
                            options.success(jsonSpotProjectList);
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                        }
                    });
                },
            },
            schema: {
                model: {
                    fields: {
                        ProjectId: {
                            from: "ProjectId"
                        },
                        SpotProjectId: {
                            from: "SpotProjectId"
                        },
                        ProjectName: {
                            from: "ProjectName"
                        },
                        LibraryName: {
                            from: "SpotTarget.SpotTargetName"
                        },
                        CreateDate: {
                            from: "CreateDate"
                        }
                    }
                }
            },
            pageSize: 5
        },
        sortable: true,
        filterable: true,
        pageable: {
            refresh: true,
            pageSizes: true
        },
        noRecords: {
            template: "No records to display."
        },
        //toolbar: [{ 'name': 'createSpot', template: '<button onclick="location.href = \'http://localhost/FujifilmUSA.Sync.Web/RedirectFromProjectList.aspx?ProjectID=' + pID + ';\'" class=\'k-button\'>Create New Library</button>' }],
        toolbar: [{ 'name': 'createSpot', template: '<button onclick="return Redirect(' + pID + ',\'AddSpotProject\');" class=\'k-button\'>Create New Library</button>' }],
        columns: [
             //{
             //    filterable: false,
             //    title: "<b></b>",
             //    template: "<center><img id='imgspotProject' src='Images/open_folder_yellow.png' style='width: 20px;'></img></center>",
             //    width: 40
             //},
           {
               field: "ProjectName",
               title: "<b>Name</b>",
           },
           {
               field: "LibraryName",
               title: "<b>Library</b>",
           },
            {
                field: "CreateDate",
                title: "<b>Last Active</b>",
                template: '#=kendo.toString(new Date(CreateDate), "MM/dd/yyyy hh:mm") #',
            },
            {
                filterable: false,
                title: "<b></b>",
                template: "<center><a id='lnkDeleteSpotProject' data-spot='#=SpotProjectId#'><img id='imgspotProject' src='Images/delete_icon.png' style='width: 15px;'></img></a></center>",
                width: 40
            }
        ]
    });



}

function deleteProject(id) {
    if (confirm('Are you sure you want to delete all information of selected Project?')) {
        kendo.ui.progress($kendoProjectList, true);
        $.ajax({
            type: "POST",
            url: SERVICE_BASE_URL + "api/Project/DeleteProject?projectID=" + id,
            success: function (response) {
                console.log(JSON.stringify(response));
                $kendoProjectList.data('kendoGrid').dataSource.read();
                $kendoProjectList.data('kendoGrid').refresh();
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                console.log(errorThrown);

            }
        });
        kendo.ui.progress($kendoProjectList, false);
    }
}

function deleteSpotProject(id) {
    if (confirm('Are you sure you want to delete all information of selected Spot Target Project?')) {
        //kendo.ui.progress($kendoProjectList, true);
        //$.ajax({
        //    type: "POST",
        //    url: SERVICE_BASE_URL + "api/Project/DeleteSpotProject?spotProjectID=" + id,
        //    success: function (response) {
        //        console.log(JSON.stringify(response));
        //        //$kendoProjectList.data('kendoGrid').dataSource.read();
        //        //$kendoProjectList.data('kendoGrid').refresh();
        //    },
        //    error: function (XMLHttpRequest, textStatus, errorThrown) {
        //        console.log(errorThrown);

        //    }
        //});
        //kendo.ui.progress($kendoProjectList, false);
    }
}

function Redirect(projectID, type) {
    document.location.href = 'RedirectFromProjectList.aspx?RedirectType=' + type + '&ProjectID=' + projectID;
    return false;
}