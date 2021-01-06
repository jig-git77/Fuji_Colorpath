//var EOptimizeRunStatus = {
//    Open: 1,
//    Closed: 2,
//    Certify: 3
//}

//var ELicenseType = {
//    None: 0,
//    Sync1D: 1,
//    DLP: 2,
//    InkOptimization: 3,
//    InkOptimizationSource: 4,
//    Sync4DCertification: 5,
//    CustomSourceProfile: 6,
//    ReProfileCustomSource: 7,
//    FullGamutCustomSource: 8,
//    MultiColorCustomSource: 9,
//    SpotLibrary: 10,
//    Validation: 11,
//    IccProfiling: 12
//}

//var EValidationStatus = {
//    None: 0,
//    NotCertified: 1,
//    Disabled: 2,
//    NoMeasurement: 3,
//    LastOK: 4,
//    LastFailed: 5
//}

var SERVICE_BASE_URL = '';
var $kendoProjectList;
var $hdfHasDeletePermission;
var CurrentDeviceID;
var CurrentUserID;
var Resources = {};
var CurrentCulture;
var $hdfToken;

$(document).ready(function () {

    SERVICE_BASE_URL = $('#hdnBaseServiceUrl').val();
    $kendoProjectList = $("#kendoProjectList");
    $hdfHasDeletePermission = $("#hdfHasDeletePermission");
    CurrentDeviceID = $("#hdfCurrentDeviceID").val();
    CurrentUserID = $("#hdfCurrentUser").val();
    CurrentCulture = $("#hdfCurrentCulture").val();
    hdfSpotProjectAllowed = $("#hdfSpotProjectAllowed");
    authToken = $("#hdfAuthToken").val();
    //var dID = $hdfCurrentDeviceID.val();
    $kendoProjectList.kendoGrid({
        dataSource: {
            transport: {
                read: function (options) {
                    //kendo.ui.progress($kendoProjectList, true);
                    kendo.ui.progress($('#kendoProjectList .k-grid-content'), true);

                    var ajaxParameter = {
                        type: "GET",
                        url: SERVICE_BASE_URL + "api/Project/GetProjectDetailsByDeviceID/",
                        data: { deviceID: CurrentDeviceID },
                        dataType: "json"
                    };

                    commonAjaxCall(ajaxParameter, function (response) {
                        var jsonProjectList = JSON.parse(response.data);
                        Resources = jsonProjectList.GlobalResources;
                        options.success(jsonProjectList.Projects);
                        kendo.ui.progress($('#kendoProjectList .k-grid-content'), false);

                        $kendoProjectList.find("th[data-field=ProjectName] b").html(Resources.GridTemplateColumnResource3);
                        $kendoProjectList.find("th[data-field=ProjectName] b").attr("title", Resources.GridTemplateColumnResource3);
                        $kendoProjectList.find("th[data-field=SourceProfileName] b").html(Resources.GridTemplateColumnResource4);
                        $kendoProjectList.find("th[data-field=SourceProfileName] b").attr("title", Resources.GridTemplateColumnResource4);
                        $kendoProjectList.find("th[data-field=PrintConditionMedia] b").html(Resources.GridTemplateColumnResource5);
                        $kendoProjectList.find("th[data-field=PrintConditionMedia] b").attr("title", Resources.GridTemplateColumnResource5);
                        $kendoProjectList.find("th[data-field=CreateDate] b").html(Resources.GridDateTimeColumnResource1);
                        $kendoProjectList.find("th[data-field=CreateDate] b").attr("title", Resources.GridDateTimeColumnResource1);


                    }, function (XMLHttpRequest, textStatus, errorThrown) {
                        if (XMLHttpRequest.status == 401) {
                            ShowDialog("You are not authorized user. Please login again.", "Message");
                        } else {
                            alert(errorThrown);
                        }
                        kendo.ui.progress($('#kendoProjectList .k-grid-content'), false);
                    });
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
                        CreateDate: {
                            from: "CreateDate",
                            type: "date"
                        }
                    }
                }
            },
            pageSize: 10
        },
        //scrollable: false,
        resizable: false,
        sortable: true,
        filterable: true,
        toolbar: ["search"],
        search: {
            fields: ["ProjectName", "SourceProfileName", "PrintConditionMedia", "CreateDate"]
        },
        pageable: {
            refresh: true,
            pageSizes: true,
            alwaysVisible: false,
        },
        noRecords: {
            template: ''
        },
        dataBound: function (e) {

            var projectTypeImagePath = '';
            var projectValidationImagePath = '';
            var projectValidationTooltip = '';
            var projectTypeTooltip = '';
            var dataSource = this.dataSource;
            var showSpotProject = false;
            var showSpotButtonEnableProject = false;
            var projectTypeImageTitle = '';
            this.element.find('tr[data-uid]').each(function () {
                var row = $(this);
                var data = dataSource.getByUid(row.data('uid'));
                //if (!(data.Status === EOptimizeRunStatus.Certify && data.LicenseType === ELicenseType.DLP && data.HasSpotProject === true)) {
                //    showSpotProject = false;

                //}
                //else { showSpotProject = true; }

                if (data.LicenseType == ELicenseType.DLP && data.Status == EOptimizeRunStatus.Certify && data.HasSpotProject == true) {
                    showSpotProject = true;//BUTON VISIBLE
                    showSpotButtonEnableProject = true; //BUTTON ENABLE 
                }
                else if (data.LicenseType == ELicenseType.DLP && data.Status != EOptimizeRunStatus.Certify && data.HasSpotProject == true) {
                    showSpotProject = true;//BUTON VISIBLE
                    showSpotButtonEnableProject = false; //BUTTON DISABLE
                }
                else if (data.LicenseType == ELicenseType.DLP && data.Status == EOptimizeRunStatus.Certify && data.HasSpotProject == false) {
                    showSpotProject = false;//BUTON NOT VISIBLE
                    showSpotButtonEnableProject = false; //BUTTON DISABLE
                }
                else if (data.LicenseType == ELicenseType.DLP && data.Status != EOptimizeRunStatus.Certify && data.HasSpotProject == false) {
                    showSpotProject = false;//BUTON NOT VISIBLE
                    showSpotButtonEnableProject = false; //BUTTON DISABLE
                }
                else if (data.LicenseType != ELicenseType.DLP) {
                    showSpotProject = false;//BUTON NOT VISIBLE
                    showSpotButtonEnableProject = false; //BUTTON DISABLE
                }
                else {
                    showSpotProject = false;
                    showSpotButtonEnableProject = false; //BUTTON DISABLE
                }

                //set project type image
                switch (data.Status) {
                    case EOptimizeRunStatus.Open:
                    case EOptimizeRunStatus.Closed:
                        switch (data.LicenseType) {
                            case ELicenseType.None: break;
                            case ELicenseType.Sync1D:
                                projectTypeImagePath = 'Images/AlignCurve.png';
                                projectTypeImageTitle = 'Align';
                                break;
                            case ELicenseType.DLP:
                                projectTypeImagePath = 'Images/DLP.png';
                                projectTypeImageTitle = 'DLP';
                                break;
                            case ELicenseType.InkOptimization:
                                projectTypeImagePath = 'Images/InkOptimize.png';
                                projectTypeImageTitle = 'Ink Optimize';
                                break;
                            case ELicenseType.InkOptimizationSource:
                                projectTypeImagePath = 'Images/InkOptimizeSource.png';
                                projectTypeImageTitle = 'Ink Optimize Source';
                                break;
                            case ELicenseType.Sync4DCertification:
                                projectTypeImagePath = 'Images/4DCertification.png';
                                projectTypeImageTitle = 'Certification';
                                break;
                            case ELicenseType.CustomSourceProfile:
                                projectTypeImagePath = 'Images/DLPSource.png';
                                projectTypeImageTitle = 'Source Profile';
                                break;
                            case ELicenseType.ReProfileCustomSource:
                                projectTypeImagePath = 'Images/DLPReProfileSource.png';
                                projectTypeImageTitle = 'DLP ReProfile Source';
                                break;
                            case ELicenseType.FullGamutCustomSource:
                                projectTypeImagePath = 'Images/DLPFullGamutSource.png';
                                projectTypeImageTitle = 'DLP Full Gamut Source';
                                break;
                            case ELicenseType.Validation:
                                break;
                            case ELicenseType.IccProfiling: break;
                            case ELicenseType.DLPPlus:
                                projectTypeImagePath = 'Images/DLP.png';
                                projectTypeImageTitle = 'DLPPlus';
                                break;
                        }
                        break;
                    case EOptimizeRunStatus.Certify:
                        switch (data.LicenseType) {
                            case ELicenseType.None: break;
                            case ELicenseType.Sync1D:
                                projectTypeImagePath = 'Images/AlignCurve_Certified.png';
                                projectTypeImageTitle = 'Align';
                                break;
                            case ELicenseType.DLP:
                                projectTypeImagePath = 'Images/DLP_Certified.png';
                                projectTypeImageTitle = 'DLP';
                                break;
                            case ELicenseType.InkOptimization:
                                projectTypeImagePath = 'Images/InkOptimize_Certified.png';
                                projectTypeImageTitle = 'Ink Optimize';
                                break;
                            case ELicenseType.Sync4DCertification:
                                projectTypeImagePath = 'Images/4DCertification_Certified.png';
                                projectTypeImageTitle = 'Certification';
                                break;
                            case ELicenseType.Validation:
                                break;
                            case ELicenseType.IccProfiling: break;
                            case ELicenseType.DLPPlus:
                                projectTypeImagePath = 'Images/DLP_Certified.png';
                                projectTypeImageTitle = 'DLPPlus';
                                break;
                        }
                        break;
                }

                //set project type image tooltip
                switch (data.LicenseType) {
                    case ELicenseType.None:
                        projectTypeTooltip = Resources.ELicenseType_None;
                        break;
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
                    case ELicenseType.CustomSourceProfile:
                        projectTypeTooltip = Resources.ELicenseType_CustomSourceProfile;
                        break;
                    case ELicenseType.ReProfileCustomSource:
                        projectTypeTooltip = Resources.ELicenseType_ReProfileCustomSourceProfile;
                        break;
                    case ELicenseType.FullGamutCustomSource:
                        projectTypeTooltip = Resources.ELicenseType_FullGamutCustomSourceProfile;
                        break;
                    case ELicenseType.MultiColorCustomSource:
                        projectTypeTooltip = Resources.ELicenseType_MultiColorCustomSourceProfile;
                        break;
                    case ELicenseType.Validation:
                        projectTypeTooltip = Resources.ELicenseType_Validation;
                        break;
                    case ELicenseType.IccProfiling: break;
                    case ELicenseType.DLPPlus:
                        projectTypeTooltip = "DLPPlus";
                        break;
                }

                //set project validation image
                switch (data.ValidationStatus) {
                    case EValidationStatus.NotCertified:
                        projectValidationImagePath = 'Images/Validation_Not_Certified.png';
                        row.find("td #lnkValidationIcon").attr('onclick', '');
                        row.find("td #lnkValidationIcon").removeClass("linktbutton");  //set the project validation button disabled/enable class
                        row.find("td #lnkValidationIcon").addClass("linktbuttonDisabled");  //set the project validation button disabled/enable class
                        projectValidationTooltip = Resources.ValidationStatus_NotCertified;
                        break;
                    case EValidationStatus.Disabled:
                        //projectValidationImagePath = 'Images/Validation_Not_Certified.png';
                        row.find("td #imgprojectValidation").css('display', 'none');
                        row.find("td #lnkValidationIcon").hide();  //set the project validation visibility
                        break;
                    case EValidationStatus.NoMeasurement: ;
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

                if (data.ValidationStatus == 3 || data.ValidationStatus == 4 || data.ValidationStatus == 5) {
                    row.find("td #lnkValidationIcon").attr("onclick", "javascript: return Redirect('" + data.ProjectId + "','ProjectValidation')");
                    row.find("td #lnkValidationIcon").addClass("linktbutton");  //set the project validation button disabled/enable class
                    row.find("td #lnkValidationIcon").removeClass("linktbuttonDisabled");  //set the project validation button disabled/enable class
                }


                if (projectTypeImagePath != '') {
                    row.find("td #imgprojectType").attr('src', projectTypeImagePath);   //set the project type image path
                    row.find("td #imgprojectType").attr('title', projectTypeTooltip);   //set the project type image path
                    row.find("td #imgprojectTypeIcon").attr('src', projectTypeImagePath);   //set the project type image path
                    row.find("td #imgprojectTypeIcon").attr('title', projectTypeTooltip);   //set the project type image path
                    row.find("td #spanImagePrjTypeTitle").text(projectTypeImageTitle);   //set the project type Title name for icon image
                    row.find("td #lnkProjectType").attr('title', projectTypeTooltip);   //set the project type image path
                    row.find("td #lnkProjectTypeIcon").attr('title', projectTypeTooltip);   //set the project type image path
                }
                if (projectValidationImagePath != '') {

                    row.find("td #imgprojectValidation").attr('src', projectValidationImagePath);   //set the project validation image path
                    row.find("td #imgprojectValidation").attr('title', projectValidationTooltip);   //set the project validation tooltip
                    row.find("td #lnkValidationIcon").attr('title', projectValidationTooltip);   //set the project validation tooltip
                    row.find("td #lnkValidationIcon").show();  //set the project validation visibility
                }
                if (showSpotProject == true) {
                    row.find("td #lnkSpotProjectIcon").show();
                    row.find("td #lnkSpotProjectIcon").attr('title', Resources.ProjectList_BcoButtonToolTip); //set the spotproject tooltip
                    row.find("td #imgprojectBCO").attr('title', Resources.ProjectList_BcoButtonToolTip);   //set the spotproject tooltip
                    if (showSpotButtonEnableProject == true) {                        
                        row.find("td #lnkSpotProjectIcon").attr("onclick", "javascript: return OpenSpotProjectListWindow('" + data.ProjectId + "','" + data.ProjectName + "')");
                        row.find("td #lnkSpotProjectIcon").addClass("linktbutton");  //set the Spotproject button disabled/enable class
                        row.find("td #lnkSpotProjectIcon").removeClass("linktbuttonDisabled");  //set the Spotproject button disabled/enable class
                    }
                    if (showSpotButtonEnableProject == false) {
                        row.find("td #lnkSpotProjectIcon").attr('onclick', '');
                        row.find("td #lnkSpotProjectIcon").addClass("linktbuttonDisabled");  //set the Spotproject button disabled/enable class
                        row.find("td #lnkSpotProjectIcon").removeClass("linktbutton");  //set the Spotproject button disabled/enable class
                    }
                }
                else {
                    row.find("td #lnkSpotProjectIcon").hide();  //set Spot Project button hide if DLP is not project
                }
                //if (showSpotProject == true) {
                //    //row.find("td #lnkSpotProjectIcon").show();  //set the project validation visibility
                //    row.find("td #lnkSpotProjectIcon").attr("onclick", "javascript: return OpenSpotProjectListWindow('" + data.ProjectId + "','" + data.ProjectName + "')");
                //    row.find("td #lnkSpotProjectIcon").addClass("linktbutton");  //set the Spotproject button disabled/enable class
                //    row.find("td #lnkSpotProjectIcon").removeClass("linktbuttonDisabled");  //set the Spotproject button disabled/enable class
                //    row.find("td #lnkSpotProjectIcon").attr('title', Resources.ProjectList_BcoEnableButtonToolTip); //set the spotproject tooltip
                //    row.find("td #imgprojectBCO").attr('title', Resources.ProjectList_BcoEnableButtonToolTip);   //set the spotproject tooltip
                //}
                //else {
                //    //row.find("td #lnkSpotProjectIcon").hide();  //set the project validation visibility
                //    row.find("td #lnkSpotProjectIcon").attr('onclick', '');
                //    row.find("td #lnkSpotProjectIcon").addClass("linktbuttonDisabled");  //set the Spotproject button disabled/enable class
                //    row.find("td #lnkSpotProjectIcon").removeClass("linktbutton");  //set the Spotproject button disabled/enable class
                //    row.find("td #lnkSpotProjectIcon").attr('title', Resources.ProjectList_BcoButtonToolTip); //set the spotproject tooltip
                //    row.find("td #imgprojectBCO").attr('title', Resources.ProjectList_BcoButtonToolTip);   //set the spotproject tooltip
                //    if (data.LicenseType != ELicenseType.DLP) {
                //        row.find("td #lnkSpotProjectIcon").hide();  //set Spot Project button hide if DLP is not project
                //    }
                //}

                switch (data.Status) {
                    case EOptimizeRunStatus.Open:
                        var dateCreate = new Date(data.CreateDate);
                        var dateCurrent = new Date();
                        var timeDiff = Math.abs(dateCurrent.getTime() - dateCreate.getTime());
                        var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));

                        row.find("td #lnkProjectType").attr("onclick", "javascript: return ShowOpenPrintConditionWindow('" + data.ProjectId + "','" + data.ProjectName + "','" + diffDays + "','" + data.CurrentDlpName + "','" + data.TotalRuns + "')");
                        row.find("td #lnkProjectTypeIcon").attr("onclick", "javascript: return ShowOpenPrintConditionWindow('" + data.ProjectId + "','" + data.ProjectName + "','" + diffDays + "','" + data.CurrentDlpName + "','" + data.TotalRuns + "')");
                        break;
                    case EOptimizeRunStatus.Closed:
                        row.find("td #lnkProjectType").attr("onclick", "javascript: return ShowClosePrintConditionWindow('" + data.ProjectId + "','" + data.ProjectName + "','" + data.LicenseType + "','" + data.CurrentDlpName + "')");
                        row.find("td #lnkProjectTypeIcon").attr("onclick", "javascript: return ShowClosePrintConditionWindow('" + data.ProjectId + "','" + data.ProjectName + "','" + data.LicenseType + "','" + data.CurrentDlpName + "')");
                        break;
                    case EOptimizeRunStatus.Certify:
                        row.find("td #lnkProjectType").attr("onclick", "javascript: return ShowClosePrintConditionWindow('" + data.ProjectId + "','" + data.ProjectName + "','" + data.LicenseType + "','" + data.CurrentDlpName + "')");
                        row.find("td #lnkProjectTypeIcon").attr("onclick", "javascript: return ShowClosePrintConditionWindow('" + data.ProjectId + "','" + data.ProjectName + "','" + data.LicenseType + "','" + data.CurrentDlpName + "')");
                        break;
                }

                //set resource message
                row.find('#lnkDeleteProject').attr('title', Resources.MsgDeleteProject);
                projectTypeImagePath = '';
                projectValidationImagePath = '';
                projectValidationTooltip = '';
                projectTypeTooltip = '';
                projectTypeImageTitle = '';
                showSpotProject = false;
                showSpotButtonEnableProject = false;

            });

        },

        columns: [
            {
                template: kendo.template($("#licenseType-printName-template").html()),
                field: "ProjectName",
                title: "<b title='Click here to sort'></b>",
                width: 70,
                filterable: false,
                attributes: {
                    style: "word-break: break-all;font-weight: bold;"
                }
            },
            {
                field: "SourceProfileName",
                title: "<b title='Click here to sort'></b>",
                width: 30,
                filterable: false,
                attributes: {
                    //style: "word-break: break-all;"
                }
            },
            {
                field: "PrintConditionMedia",
                title: "<b title='Click here to sort'></b>",
                width: 25,
                filterable: false,
                attributes: {
                    //style: "word-break: break-all;"
                }
            },
            {
                field: "CreateDate",
                title: "<b title='Click here to sort'></b>",
                template: '#=kendo.toString(new Date(CreateDate), "MM/dd/yyyy hh:mm tt") #',
                width: 25,
                filterable: false, attributes: {
                    //style: "word-break: break-all;"
                }
            },
            {
                filterable: false,
                title: "",
                template: "<center><a id='lnkDeleteProject' title='Delete Project' onclick='return deleteProject(#=id#);'><img id='imgspotProject' src='Images/delete_icon.png' style='width: 15px;'></img></a></center>",
                width: 10
            }
        ]
    });


    var grid = $kendoProjectList.data("kendoGrid");

    if ($hdfHasDeletePermission.val() !== 'True') {
        grid.hideColumn(4);
    }

    $kendoProjectList.on('click', '#lnkDeleteSpotProject', function (e) {
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

    $("#windowAlert").kendoWindow({
        width: "300px",
        height: "100px",
        modal: true,
        title: "Alert",
        visible: false,
        position: {
            top: 150,
            left: 500
        },
    });


    $('#btnAlertPopup').on('click', function (e) {
        var locationpath = "";
        if (window.location.hostname == "localhost") {
            locationpath = window.location.origin + '/FujifilmUSA.Sync.Web/Login.aspx'
        } else {
            locationpath = window.location.origin + '/Login.aspx'
        }
        window.location.href = locationpath;
    });

    $('.k-textbox input').on("keydown", function (e) {

        if (e.keyCode == 13) {
            e.preventDefault();
            return false;
        }
    });

});



function clearFilter() {
    $("form.k-filter-menu button[type='reset']").trigger("click");
}

function clearNestGridFilters(btn) {
    $("form.k-filter-menu button[type='reset']").trigger("click");
}

function deleteProject(id) {
    if (confirm('Are you sure you want to delete all information of selected Project?')) {
        kendo.ui.progress($kendoProjectList, true);

        var ajaxParameter = {
            type: "POST",
            url: SERVICE_BASE_URL + "api/Project/DeleteProject?projectID=" + id,
            data: {},
            dataType: ""
        };

        commonAjaxCall(ajaxParameter, function (response) {
            $kendoProjectList.data('kendoGrid').dataSource.read();
            $kendoProjectList.data('kendoGrid').refresh();
        }, function (XMLHttpRequest, textStatus, errorThrown) {
            if (XMLHttpRequest.status == 401) {
                ShowDialog("You are not authorized user. Please login again.", "Message");
            } else {
                alert(errorThrown);
            }
        });
        kendo.ui.progress($kendoProjectList, false);
    }
}

function spotProjectSelected(chk) {
    var uid = $(chk).closest("[data-uid]").data("uid")
    var dataSource = $(chk).closest('.k-grid').data("kendoGrid").dataSource;
    //var dataSource = $('#divPathDetails').data("kendoGrid").dataSource;
    var item = dataSource.getByUid(uid);
    item.dirty = true;
    item.IsSelected = $(chk).prop('checked');
}


function Redirect(projectID, type) {
    document.location.href = 'RedirectFromProjectList.aspx?RedirectType=' + type + '&ProjectID=' + projectID;
    return false;
}
function ShowOpenPrintConditionWindow(projectId, projectName, days, DlpName, TotalRuns, isSpotProject) {
    // Need a random item in the querystring so the RadWindow does not pull from cache.
    var WinId = "Win" + Math.random().toString();

    if (isSpotProject === undefined) {
        isSpotProject = false;
    }

    var oWindow;
    //oWindow = window.radopen("PopupOpenPrintCondition.aspx?projectId=" + projectId + "&projectName=" + projectName + "&days=" + days + "&DlpName=" + DlpName + "&TotalRuns=" + TotalRuns + "&isSpotProject=" + isSpotProject, "OpenPrintConditionWindow");
    oWindow = GetRadWindowManager().open("PopupOpenPrintCondition.aspx?projectId=" + projectId + "&projectName=" + projectName + "&days=" + days + "&DlpName=" + DlpName + "&TotalRuns=" + TotalRuns + "&isSpotProject=" + isSpotProject + "&wid=" + WinId, "OpenPrintConditionWindow");
    oWindow.show();
    return false;
}
function ShowClosePrintConditionWindow(projectId, projectName, licenseTypeId, DlpName) {
    // Need a random item in the querystring so the RadWindow does not pull from cache.
    var WinId = "Win" + Math.random().toString();
    var oWindow;
    //oWindow = window.radopen("PopupClosePrintCondition.aspx?projectId=" + projectId + "&projectName=" + projectName + "&licenseTypeId=" + licenseTypeId + "&DlpName=" + DlpName, "ClosePrintConditionWindow");
    oWindow = GetRadWindowManager().open("PopupClosePrintCondition.aspx?projectId=" + projectId + "&projectName=" + projectName + "&licenseTypeId=" + licenseTypeId + "&DlpName=" + DlpName + "&wid=" + WinId, "ClosePrintConditionWindow");
    oWindow.show();
    return false;
}

function OpenSpotProjectListWindow(projectId, projectName) {
    // Need a random item in the querystring so the RadWindow does not pull from cache.
    var WinId = "Win" + Math.random().toString();
    var oWindow;
    oWindow = GetRadWindowManager().open("PopupSpotProjectList.aspx?projectId=" + projectId + "&projectName=" + projectName + "&wid=" + WinId, "ShowSpotProjectListWindow");
    oWindow.show();
    return false;

}

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

//k-textbox k-grid-search k-display-flex
//('.k-textbox k-grid-search k-display-flex input')

