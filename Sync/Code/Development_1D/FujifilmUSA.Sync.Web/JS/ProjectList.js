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
var Resources = {};

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
                            console.log(jsonProjectList);
                            Resources = jsonProjectList.GlobalResources;
                            options.success(jsonProjectList.Projects);
                            kendo.ui.progress($('#kendoProjectList .k-grid-content'), false);

                            //var grid = $kendoProjectList.data("kendoGrid");
                            $kendoProjectList.find("th[data-field=LicenseType] b").html(Resources.GridTemplateColumnResource0);
                            $kendoProjectList.find("th[data-field=ValidationStatus] b").html(Resources.GridTemplateColumnResource1);
                            $kendoProjectList.find("th[data-field=ProjectName] b").html(Resources.GridTemplateColumnResource3);
                            $kendoProjectList.find("th[data-field=ProjectName] b").attr("title", Resources.ToolTipClickToShort);
                            $kendoProjectList.find("th[data-field=SourceProfileName] b").html(Resources.GridTemplateColumnResource4);
                            $kendoProjectList.find("th[data-field=SourceProfileName] b").attr("title", Resources.ToolTipClickToShort);
                            $kendoProjectList.find("th[data-field=PrintConditionMedia] b").html(Resources.GridTemplateColumnResource5);
                            $kendoProjectList.find("th[data-field=PrintConditionMedia] b").attr("title", Resources.ToolTipClickToShort);
                            $kendoProjectList.find("th[data-field=PrintConditionColorant] b").html(Resources.GridTemplateColumnResource6);
                            $kendoProjectList.find("th[data-field=PrintConditionColorant] b").attr("title", Resources.ToolTipClickToShort);
                            $kendoProjectList.find("th[data-field=CreateDate] b").html(Resources.GridDateTimeColumnResource1);
                            $kendoProjectList.find("th[data-field=CreateDate] b").attr("title", Resources.ToolTipClickToShort);

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
                //data: "",
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
                            from: "CreateDate",
                            type: "date"
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
            var projectTypeTooltip = '';
            var dataSource = this.dataSource;
            this.element.find('tr.k-master-row').each(function () {
                var row = $(this);
                var data = dataSource.getByUid(row.data('uid'));
                if (!(data.Status === EOptimizeRunStatus.Certify && data.LicenseType === ELicenseType.DLP && data.HasSpotProject === true)) {
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

                //set project type image tooltip
                switch (data.LicenseType) {
                    case ELicenseType.None: break;
                    case ELicenseType.Sync1D:
                        //projectTypeTooltip = 'Alignment Curves';
                        projectTypeTooltip = Resources.ELicenseType_Sync1D;
                        break;
                    case ELicenseType.DLP:
                        projectTypeTooltip = Resources.ELicenseType_DLP;
                        break;
                    case ELicenseType.InkOptimization:
                        projectTypeTooltip = Resources.ELicenseType_InkOptimization;
                        break;
                    case ELicenseType.InkOptimizationSource:
                        projectTypeTooltip = Resources.ELicenseType_InkOptimization_Source;
                        break;
                    case ELicenseType.Sync4DCertification:
                        projectTypeTooltip = Resources.ELicenseType_Sync4DCertification;
                        break;
                    case ELicenseType.Validation:
                        break;
                    case ELicenseType.IccProfiling: break;
                }

                //set project validation image
                switch (data.ValidationStatus) {
                    case EValidationStatus.NotCertified:
                        projectValidationImagePath = 'Images/Validation_Not_Certified.png';
                        row.find("td #lnkValidation").attr('onclick', '');

                        projectValidationTooltip = Resources.ValidationStatus_NotCertified;
                        break;
                    case EValidationStatus.Disabled:
                        //projectValidationImagePath = 'Images/Validation_Not_Certified.png';
                        row.find("td #imgprojectValidation").css('display', 'none');
                        break;
                    case EValidationStatus.NoMeasurement:;
                        projectValidationImagePath = 'Images/Validation_Not_Active.png';
                        projectValidationTooltip = Resources.ValidationStatus_NoMeasurement;
                        break;
                    case EValidationStatus.LastOK:
                        projectValidationImagePath = 'Images/Validation_Last_OK.png';
                        projectValidationTooltip = Resources.ValidationStatus_LastOK;
                        break;
                    case EValidationStatus.LastFailed:
                        projectValidationImagePath = 'Images/Validation_Last_Failed.png';
                        projectValidationTooltip = Resources.ValidationStatus_LastFailed;
                        break;
                }


                if (projectTypeImagePath != '') {
                    row.find("td #imgprojectType").attr('src', projectTypeImagePath);   //set the project type image path
                    row.find("td #imgprojectType").attr('title', projectTypeTooltip);   //set the project type image path
                }
                if (projectValidationImagePath != '') {

                    row.find("td #imgprojectValidation").attr('src', projectValidationImagePath);   //set the project validation image path
                    row.find("td #imgprojectValidation").attr('title', projectValidationTooltip);   //set the project validation tooltip
                }
                switch (data.Status) {
                    case EOptimizeRunStatus.Open:
                        var dateCreate = new Date(data.CreateDate);
                        var dateCurrent = new Date();
                        var timeDiff = Math.abs(dateCurrent.getTime() - dateCreate.getTime());
                        var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));

                        row.find("td #lnkProjectType").attr("onclick", "javascript: return ShowOpenPrintConditionWindow('" + data.ProjectId + "','" + data.ProjectName + "','" + diffDays + "','" + data.CurrentDlpName + "','" + data.TotalRuns + "')");
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
                title: "<b></b>",
                field: "LicenseType",
                template: "<center><a id='lnkProjectType'><img id='imgprojectType' src=''></img></a></center>",
                width: 30,
                sortable: false,
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
                title: "<b></b>",
                template: "<center><a id='lnkValidation' onclick='return Redirect(#=id#,\"ProjectValidation\");'><img id='imgprojectValidation'  title='this will be displayed as a tooltip' src=''></img></a></center>",
                width: 30,
                sortable: false,
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
               title: "<b title='Click here to sort'></b>",
               width: 50,
               filterable: {
                   operators: {
                       string: {
                           contains: "Contains",
                           eq: "Is equal to",
                           neq: "Not equal to",
                           startswith: "Starts with",
                           doesnotcontain: "Does not contain",
                           endswith: "Ends with",
                           isnull: "Is null",
                           isnotnull: "Is not null",
                           isempty: "Is empty",
                           isnotempty: "Is not empty",
                       }
                   },
               }
           },
           {
               field: "SourceProfileName",
               title: "<b title='Click here to sort'></b>",
               width: 50,
               filterable: {
                   operators: {
                       string: {
                           contains: "Contains",
                           eq: "Is equal to",
                           neq: "Not equal to",
                           startswith: "Starts with",
                           doesnotcontain: "Does not contain",
                           endswith: "Ends with",
                           isnull: "Is null",
                           isnotnull: "Is not null",
                           isempty: "Is empty",
                           isnotempty: "Is not empty",
                       }
                   },
               }
           },
           {
               field: "PrintConditionMedia",
               title: "<b title='Click here to sort'></b>",
               width: 50,
               filterable: {
                   operators: {
                       string: {
                           contains: "Contains",
                           eq: "Is equal to",
                           neq: "Not equal to",
                           startswith: "Starts with",
                           doesnotcontain: "Does not contain",
                           endswith: "Ends with",
                           isnull: "Is null",
                           isnotnull: "Is not null",
                           isempty: "Is empty",
                           isnotempty: "Is not empty",
                       }
                   },
               }
           },
           {
               field: "PrintConditionColorant",
               title: "<b title='Click here to sort'></b>",
               width: 50,
               filterable: {
                   operators: {
                       string: {
                           contains: "Contains",
                           eq: "Is equal to",
                           neq: "Not equal to",
                           startswith: "Starts with",
                           doesnotcontain: "Does not contain",
                           endswith: "Ends with",
                           isnull: "Is null",
                           isnotnull: "Is not null",
                           isempty: "Is empty",
                           isnotempty: "Is not empty",
                       }
                   },
               }
           },
            {
                field: "CreateDate",
                title: "<b title='Click here to sort'></b>",
                template: '#=kendo.toString(new Date(CreateDate), "MM/dd/yyyy hh:mm") #',
                width: 50,
                filterable: {
                    extra: false,
                    operators: {
                        date: {
                            gt: "Is after",
                            eq: "Equal to",
                            neq: "Not equal to",
                            gte: "Is after or equal to",
                            lte: "Is before or equal to",
                            lt: "Is before"
                        }
                    },
                    ui: "datetimepicker" // use Kendo UI DateTimePicker
                }
            },
            {
                filterable: false,
                title: "<center><a id='lnkLinkFilter' title='Clear Filter' onclick='clearFilter()'><img id='imgClearFilter' src='Images/clear-fillter.png' style='width: 15px;'></img></a></center>",
                template: "<center><a id='lnkDeleteProject' title='Delete Project' onclick='return deleteProject(#=id#);'><img id='imgspotProject' src='Images/delete_icon.png' style='width: 15px;'></img></a></center>",
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
        toolbar: [{ 'name': 'createSpot', template: '<button id="btnCreateSpotProject" onclick="return Redirect(' + pID + ',\'AddSpotProject\');" class=\'k-button\'>Create New Library</button>' }],
        dataBound: function (e) {
            console.log('chile bound event');

            var dataSource = this.dataSource;
            this.element.find('tr').each(function (){
                var row = $(this);
                if (dataSource.getByUid(row.data('uid')) != undefined) {

                    var data = dataSource.getByUid(row.data('uid'));
                    switch (data.Status) {
                        case EOptimizeRunStatus.Open:
                            var dateCreate = new Date(data.CreateDate);
                            var dateCurrent = new Date();
                            var timeDiff = Math.abs(dateCurrent.getTime() - dateCreate.getTime());
                            var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));

                            row.find("td #lnkSpotProject").attr("onclick", "javascript: return ShowOpenPrintConditionWindow('" + data.SpotProjectId + "','" + data.ProjectName + "','" + diffDays + "','','" + data.TotalRuns + "',true)");
                            break;
                        case EOptimizeRunStatus.Closed:
                            row.find("td #lnkSpotProject").attr("onclick", "javascript: return ShowClosePrintConditionWindow('" + data.SpotProjectId + "','" + data.ProjectName + "','" + ELicenseType.SpotLibrary + "','',true)");
                            break;
                        case EOptimizeRunStatus.Certify:
                            row.find("td #lnkSpotProject").attr("onclick", "javascript: return ShowClosePrintConditionWindow('" + data.SpotProjectId + "','" + data.ProjectName + "','" + ELicenseType.SpotLibrary + "','',true)");
                            break;
                    }

                    if (!data.isAllowSpotProject) {
                        var gridCurrent = row[0].closest('.k-grid');
                        $(gridCurrent).find('.k-grid-toolbar').hide();
                    }
                }
            });
        },
        columns: [
             {
                 filterable: false,
                 title: "<b></b>",
                 //template: "<center><a onclick='return Redirect(#=SpotProjectId#,\"SelectSpotTargetProject\");'><img title='Open' id='imgspotProject'  src='Images/open_folder_yellow.png' style='width: 20px;'></img></a></center>",
                 template: "<center><a id='lnkSpotProject'><img title='Open' id='imgSpotProject'  src='Images/open_folder_yellow.png' style='width: 20px;'></img></a></center>",
                 width: 40
             },
           {
               field: "ProjectName",
               title: "<b>" + Resources.GridTemplateColumnResource3 + "</b>",
               filterable: {
                   operators: {
                       string: {
                           contains: "Contains",
                           eq: "Is equal to",
                           neq: "Not equal to",
                           startswith: "Starts with",
                           doesnotcontain: "Does not contain",
                           endswith: "Ends with",
                           isnull: "Is null",
                           isnotnull: "Is not null",
                           isempty: "Is empty",
                           isnotempty: "Is not empty",
                       }
                   },
               }
           },
           {
               field: "LibraryName",               
               title: "<b>" + Resources.GridTemplateColumnResourceLibrary + "</b>",
               filterable: {
                   operators: {
                       string: {
                           contains: "Contains",
                           eq: "Is equal to",
                           neq: "Not equal to",
                           startswith: "Starts with",
                           doesnotcontain: "Does not contain",
                           endswith: "Ends with",
                           isnull: "Is null",
                           isnotnull: "Is not null",
                           isempty: "Is empty",
                           isnotempty: "Is not empty",
                       }
                   },
               }
           },
            {
                field: "CreateDate",
                title: "<b>" + Resources.GridDateTimeColumnResource1 + "</b>",
                template: '#=kendo.toString(new Date(CreateDate), "MM/dd/yyyy hh:mm") #',
                filterable: {
                    extra: false,
                    operators: {
                        date: {
                            lt: "Is before",
                            gt: "Is after",
                            eq: "Equal to",
                            neq: "Not equal to",
                            gte: "Is after or equal to",
                            lte: "Is before or equal to",

                        }
                    },
                    ui: "datetimepicker" // use Kendo UI DateTimePicker
                }
            },
            {
                filterable: false,
                //title: "<center><a id='lnkInnerGridFilter' title='Clear Filter' data-spot='#=SpotProjectId#'><img id='imgClearFilter' src='Images/clear-fillter.png' style='width: 15px;'></img></a></center>",
                headerTemplate: "<center><a id='lnkInnerGridFilter' title='Clear Filter' onclick='clearNestGridFilters()'><img id='imgClearFilter' src='Images/clear-fillter.png' style='width: 15px;'></img></a></center>",
                template: "<center><a id='lnkDeleteSpotProject' title='Delete Project' data-spot='#=SpotProjectId#'><img id='imgspotProjectDelete' src='Images/delete_icon.png' style='width: 15px;'></img></a></center>",
                width: 40
            }
        ]
    });

}


function clearFilter() {
    $("form.k-filter-menu button[type='reset']").trigger("click");
}

function clearNestGridFilters(btn) {
    $("form.k-filter-menu button[type='reset']").trigger("click");
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


function Redirect(projectID, type) {
    document.location.href = 'RedirectFromProjectList.aspx?RedirectType=' + type + '&ProjectID=' + projectID;
    return false;
}