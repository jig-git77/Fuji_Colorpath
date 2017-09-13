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

$(document).ready(function () {
    var dID = 16;
    $("#kendoProjectList").kendoGrid({
        dataSource: {
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "GET",
                        url: "http://localhost/FujiFilmUSA.Sync.Service/api/Grid/GetProjectDetailsByDeviceID/",
                        data: { deviceID: dID },
                        dataType: "json",
                        success: function (response) {
                            options.success(response.data)
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                        }
                    });
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
        detailInit: detailInit,
        dataBound: function () {
            var projectTypeImagePath = '';
            var projectValidationImagePath = '';
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
                        break;
                    case EValidationStatus.LastFailed:
                        projectValidationImagePath = 'Images/Validation_Last_Failed.png';
                        break;
                }


                if (projectTypeImagePath != '') {
                    row.find("td #imgprojectType").attr('src', projectTypeImagePath);   //set the project type image path
                }
                if (projectValidationImagePath != '') {
                    row.find("td #imgprojectValidation").attr('src', projectValidationImagePath);   //set the project validation image path
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
                template: "<center><a id='lnkProjectType'><img id='imgprojectType' src=''></img></a></center>",
                width: 30,
                filterable: {
                    ui: function (element) {
                        element.kendoAutoComplete(); // initialize a Kendo UI AutoComplete
                    }
                }
            },
            {
                filterable: false,
                title: "<b>Validation</b>",
                template: "<center><img id='imgprojectValidation' src=''></img></center>",
                width: 30
            },
           {
               field: "ProjectName",
               title: "<b>Project Name</b>",
               width: 50,
               filterable: {
                   ui: function (element) {
                       element.kendoAutoComplete(); // initialize a Kendo UI AutoComplete
                   }
               }
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
                template: "<center><img id='imgspotProject' src='Images/delete_icon.png' style='width: 15px;'></img></center>",
                width: 20
            }
        ]
    });
});

function detailInit(e) {
    var pID = e.data.id;
    $("<div/>").appendTo(e.detailCell).kendoGrid({
        dataSource: {
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "GET",
                        url: "http://localhost/FujiFilmUSA.Sync.Service/api/Grid/GetSpotProjectDetailsByProjectID/",
                        data: { projectID: pID },
                        dataType: "json",
                        success: function (response) {
                            options.success(response.data)
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
        columns: [
             {
                 filterable: false,
                 title: "<b></b>",
                 template: "<center><img id='imgspotProject' src='Images/open_folder_yellow.png' style='width: 20px;'></img></center>",
                 width: 40
             },
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
            }
        ]
    });

}