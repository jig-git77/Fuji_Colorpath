var SERVICE_BASE_URL = '';
var $kendoSpotProjectList;
var $hdfHasDeletePermission;
var CurrentUserID;
var Resources = {};
var CurrentCulture;
var $hdfProjectID;

$(document).ready(function () {
    $kendoSpotProjectList = $("#kendoSpotProjectList");
    SERVICE_BASE_URL = $('#hdnBaseServiceUrl').val();
    $hdfHasDeletePermission = $("#hdfHasDeletePermission");
    CurrentUserID = $("#hdfCurrentUser").val();
    CurrentCulture = $("#hdfCurrentCulture").val();
    hdfSpotProjectAllowed = $("#hdfSpotProjectAllowed");
    hdfProjectID = $("#hdfProjectID");
    authToken = $("#hdfAuthToken").val();
    const params = new URLSearchParams(window.location.search)
    hdfProjectID.val(params.get('projectId'));

    $kendoSpotProjectList.kendoGrid({
        dataSource: {
            transport: {
                read: function (options) {

                    var ajaxParameter = {
                        type: "GET",
                        url: SERVICE_BASE_URL + "api/Project/GetSpotProjectDetailsByProjectID/",
                        data: { projectID: parseInt(hdfProjectID.val()) },
                        dataType: "json"
                    };

                    commonAjaxCall(ajaxParameter, function (response) {
                        var result = JSON.parse(response.data);
                        hdfSpotProjectAllowed.val(result.isAllowSpotProject);
                        options.success(result.SpotProjectLibraries);
                    }, function (XMLHttpRequest, textStatus, errorThrown) {
                        if (XMLHttpRequest.status == 401) {
                            ShowDialog("You are not authorized user. Please login again.", "Message");
                        } else {
                            alert(errorThrown);
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
                        },
                        IsSelected: {
                            type: "boolean"
                        }
                    }
                }
            },
            pageSize: 10
        },
        sortable: true,
        filterable: false,
        pageable: {
            refresh: true,
            alwaysVisible: false,
            pageSizes: true,
        },
        noRecords: {
            template: ''
        },
        dataBound: function (e) {
            var dataSource = this.dataSource;
            this.element.find('tr').each(function () {
                var row = $(this);
                if (dataSource.getByUid(row.data('uid')) != undefined) {

                    var data = dataSource.getByUid(row.data('uid'));
                    var SpotprojectTypeImagePath = '';
                    var SpotprojectTypeTooltip = '';

                    switch (data.Status) {
                        case EOptimizeRunStatus.Open:
                            var dateCreate = new Date(data.CreateDate);
                            var dateCurrent = new Date();
                            var timeDiff = Math.abs(dateCurrent.getTime() - dateCreate.getTime());
                            var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));

                            row.find("td #lnkSpotProject").attr("onclick", "javascript: return ShowOpenPrintConditionWindow('" + data.SpotProjectId + "','" + data.ProjectName + "','" + diffDays + "','','" + data.TotalRuns + "',true)");
                            SpotprojectTypeImagePath = 'Images/SpotProject.png';
                            SpotprojectTypeTooltip = Resources.SpotProject_Open;
                            break;
                        case EOptimizeRunStatus.Closed:
                            row.find("td #lnkSpotProject").attr("onclick", "javascript: return ShowSpotClosePrintConditionWindow('" + data.SpotProjectId + "','" + data.ProjectName + "','" + ELicenseType.SpotLibrary + "','')");
                            SpotprojectTypeImagePath = 'Images/SpotProject.png';
                            SpotprojectTypeTooltip = Resources.SpotProject_Open;
                            break;
                        case EOptimizeRunStatus.Certify:
                            row.find("td #lnkSpotProject").attr("onclick", "javascript: return ShowSpotClosePrintConditionWindow('" + data.SpotProjectId + "','" + data.ProjectName + "','" + ELicenseType.SpotLibrary + "','')");
                            SpotprojectTypeImagePath = 'Images/SpotProject_Certified.png';
                            SpotprojectTypeTooltip = Resources.SpotProject_Certified;
                            row.find("td #chkCombinedLibrary").prop('disabled', false);
                            break;
                    }
                    if (SpotprojectTypeImagePath != '') {
                        row.find("td #imgSpotProject").attr('src', SpotprojectTypeImagePath);   //set the Spot project type image path
                        row.find("td #imgSpotProject").attr('title', SpotprojectTypeTooltip);   //set the Spot project type tooltip
                    }


                }

            });

            //  enable/disabled button as per spot project limit            
            var spotProjectAllowed = hdfSpotProjectAllowed.val() == 'true';
            if (!spotProjectAllowed) {
                $("<%= btnCreateSpotProject.ClientID %>").hide();
                if (dataSource._data.length == 0) {
                    $("<%= btnExportSpotProject.ClientID %>").hide();
                }
            } else {
                $("<%= btnCreateSpotProject.ClientID %>").hide();
                if (dataSource._data.length == 0) {
                    $("<%= btnExportSpotProject.ClientID %>").hide();
                }
            }

        },
        columns: [
            {
                filterable: false,
                title: "<b></b>",
                template: "<center><a id='lnkSpotProject'><img id='imgSpotProject' title=''  src='' style='width: 20px;'></img></a></center>",
                width: 40
            },
            {
                filterable: false,
                title: "<b></b>",
                //template: "<center><input type='checkbox' id='chkCombinedLibrary' data-target='#=SpotProjectId#' disabled='disabled'/></center>",
                template: "<center><input type='checkbox' id='chkCombinedLibrary' onclick='spotProjectSelected(this)' #=IsSelected?'checked':''# disabled='disabled'/></center>",
                width: 40
            },
            {
                field: "ProjectName",
                title: "<b> Brand Color Library</b>",
            },
            {
                field: "LibraryName",
                title: "<b>Target Library</b>",
            },
            {
                field: "CreateDate",
                title: "<b>Last Activity</b>",
                template: '#=kendo.toString(new Date(CreateDate), "MM/dd/yyyy hh:mm tt") #',
                ui: "datetimepicker" // use Kendo UI DateTimePicker
            },
            {
                filterable: false,
                //title: "<center><a id='lnkInnerGridFilter' title='Clear Filter' data-spot='#=SpotProjectId#'><img id='imgClearFilter' src='Images/clear-fillter.png' style='width: 15px;'></img></a></center>",
                headerTemplate: "<center><a id='lnkInnerGridFilter' title='" + Resources.ClearFillterResource + "' onclick='clearNestGridFilters()'><img id='imgClearFilter' src='Images/clear-fillter.png' style='width: 15px;'></img></a></center>",
                template: "<center><a id='lnkDeleteSpotProject' title='" + Resources.MsgDeleteProject + "' data-spot='#=SpotProjectId#'><img id='imgspotProjectDelete' src='Images/delete_icon.png' style='width: 15px;'></img></a></center>",
                width: 40
            }
        ]
    });

    $kendoSpotProjectList.on('click', '#lnkDeleteSpotProject', function (e) {
        var spotProjectID = $(this).attr('data-spot');
        var detailGrid = $(this).closest('.k-grid').data("kendoGrid");

        //$(this).closest('.k-grid').data('kendoGrid').dataSource.read();
        //$(this).closest('.k-grid').data('kendoGrid').refresh();

        if (confirm('Are you sure you want to delete all information of selected Spot Target Project?')) {
            kendo.ui.progress($('#kendoProjectList .k-grid-content'), true);
            //kendo.ui.progress($kendoProjectList, true);

            var ajaxParameter = {
                type: "POST",
                url: SERVICE_BASE_URL + "api/Project/DeleteSpotProject?spotProjectID=" + spotProjectID,
                data: {},
                dataType: "json"
            };

            commonAjaxCall(ajaxParameter, function (response) {
                detailGrid.dataSource.read();
                detailGrid.refresh();
                kendo.ui.progress($kendoProjectList, false);
            }, function (XMLHttpRequest, textStatus, errorThrown) {
                if (XMLHttpRequest.status == 401) {
                    ShowDialog("You are not authorized user. Please login again.", "Message");
                } else {
                    alert(errorThrown);
                }
                kendo.ui.progress($kendoProjectList, false);
            });
        }

    })
});


function commonAjaxCall(options, success_call, error_call) {
    var ajaxType = options.type;
    var ajaxUrl = options.url;
    var ajaxData = options.data;
    var ajaxDataType = options.dataType;

    $.ajax({
        type: ajaxType,
        url: ajaxUrl,
        data: ajaxData,
        dataType: ajaxDataType,
        headers: {
            'Accept-Language': CurrentCulture,
            'Authorization': 'Basic ' + authToken
        },
        success: function (response) {
            success_call(response);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            error_call(XMLHttpRequest, textStatus, errorThrown);
        }
    });
}
function Redirect(projectID, type) {
    document.location.href = 'RedirectFromProjectList.aspx?RedirectType=' + type + '&ProjectID=' + projectID;
    return false;
}
function ShowSpotLibraryExportWindow(btn) {
    var spotIDs = '';
    var isSpotProjectSelected = false;
    var dsSpots = $kendoSpotProjectList.data("kendoGrid").dataSource.data();
    for (var index = 0; index < dsSpots.length; index++) {
        if (dsSpots[index]['IsSelected']) {
            spotIDs = spotIDs + (dsSpots[index]['SpotProjectId'] + ',');
            isSpotProjectSelected = true;
        }
    };
    if (isSpotProjectSelected) {
        var oWindow;
        //oWindow = window.radopen("PopupSpotLibraryExport.aspx?OptimizationRunId=101&SpotProjectIds=" + spotIDs, "ReportSpotLibraryExportWindow");
        oWindow = GetRadWindowManager().open("PopupSpotLibraryExport.aspx?OptimizationRunId=101&SpotProjectIds=" + spotIDs, "ReportSpotLibraryExportWindow");
        oWindow.show();
    } else {
        alert('Please select atleast one certified project.');
    }

    return false;
}
function spotProjectSelected(chk) {
    var uid = $(chk).closest("[data-uid]").data("uid")
    var dataSource = $(chk).closest('.k-grid').data("kendoGrid").dataSource;
    //var dataSource = $('#divPathDetails').data("kendoGrid").dataSource;
    var item = dataSource.getByUid(uid);
    item.dirty = true;
    item.IsSelected = $(chk).prop('checked');
}
function ShowOpenPrintConditionWindow(projectId, projectName, days, DlpName, TotalRuns, isSpotProject) {
    // Need a random item in the querystring so the RadWindow does not pull from cache.
    var WinId = "Win" + Math.random().toString();

    if (isSpotProject === undefined) {
        isSpotProject = false;
    }

    var oWindow;
    //oWindow = window.radopen("PopupOpenPrintCondition.aspx?projectId=" + projectId + "&projectName=" + projectName + "&days=" + days + "&DlpName=" + DlpName + "&TotalRuns=" + TotalRuns + "&isSpotProject=" + isSpotProject, "OpenPrintConditionWindow");
    oWindow = GetRadWindowManager().open("PopupSpotProjectOpenPrintCondition.aspx?projectId=" + projectId + "&projectName=" + projectName + "&days=" + days + "&DlpName=" + DlpName + "&TotalRuns=" + TotalRuns + "&isSpotProject=" + isSpotProject + "&wid=" + WinId, "OpenPrintConditionWindow");
    oWindow.show();
    return false;
}
function ShowSpotClosePrintConditionWindow(projectId, projectName, licenseTypeId, DlpName) {
    // Need a random item in the querystring so the RadWindow does not pull from cache.
    var WinId = "Win" + Math.random().toString();
    var oWindow;
    //oWindow = window.radopen("PopupSpotClosePrintCondition.aspx?projectId=" + projectId + "&projectName=" + projectName + "&licenseTypeId=" + licenseTypeId + "&DlpName=" + DlpName, "ClosePrintConditionWindow");
    oWindow = GetRadWindowManager().open("PopupSpotClosePrintCondition.aspx?projectId=" + projectId + "&projectName=" + projectName + "&licenseTypeId=" + licenseTypeId + "&DlpName=" + DlpName + "&wid=" + WinId, "ClosePrintConditionWindow");
    oWindow.show();
    return false;
}

function ShowDialog(message, title) {
    $("#dialog").kendoDialog({
        width: "400px",
        title: title,
        closable: false,
        modal: true,
        content: message,
        open: function () {
            $($('.k-dialog-buttongroup button')[1]).kendoTooltip({
                content: "Help Details will come Later!",
                position: "bottom"
            });
        },
        actions: [
            {
                text: 'OK', primary: true, action: function (e) {
                    window.location.href = 'login.aspx';
                }
            },
            {
                text: 'Help', primary: true, action: function (e) {
                    //alert("Help will come later.");
                    window.location.href = 'login.aspx';
                }
            }
        ]
    });
}