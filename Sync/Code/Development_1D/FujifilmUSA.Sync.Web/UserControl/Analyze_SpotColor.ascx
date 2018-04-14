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

    /*#tblSecondRunDeltas td{
        min-width:60px;
    }*/
</style>

<script type="application/javascript">
    var record = 0;
    var SERVICE_BASE_URL = '';
    var Resources = {};
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
        var $divPathDetails = $("#divPathDetails");
        $divPathDetails.kendoGrid({
            dataSource: {
                //transport: {
                //    //read: "/FujifilmUSA.Sync.Web/Analyze_SpotColor.aspx/GetPatchDetails"
                //    read: "http://localhost/FujiFilmUSA.Sync.Service/api/Chart/GetPatchDetails/" + oId
                //},
                transport: {
                    read: function (options) {
                        $.ajax({
                            type: "GET",
                            url: SERVICE_BASE_URL + "api/Chart/GetSpotPatchDetails/",
                            data: { id: optimizationId },
                            dataType: "json",
                            headers: {
                                'Accept-Language': CurrentCulture
                            },
                            success: function (response) {
                                if (response.data != null) {
                                    Resources = response.data.GlobalResources;
                                    var spotStates = response.data.spotAnalyzeDetails;
                                    $('#hdfMeasurementID').val(spotStates.measurementID);
                               
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

                                }
                                options.success(spotStates.patchDetails);
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
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
                                from: "SpotID"
                            },
                            SpotName: {
                                from: "SpotName"
                            },
                            CMYK_C: {
                                from: "CMYK_C"
                            },
                            CMYK_M: {
                                from: "CMYK_M"
                            },
                            CMYK_Y: {
                                from: "CMYK_Y"
                            },
                            CMYK_K: {
                                from: "CMYK_K"
                            },
                            HTMLColor: {
                                from: "HTMLColor"
                            },
                            TargetL: {
                                from: "TargetL"
                            },
                            Targeta: {
                                from: "Targeta"
                            },
                            Targetb: {
                                from: "Targetb"
                            },
                            CIELAB_L: {
                                from: "CIELAB_L"
                            },
                            CIELAB_a: {
                                from: "CIELAB_a"
                            },
                            ΔL: {
                                from: "ΔL"
                            },
                            Δa: {
                                from: "Δa"
                            },
                            Δb: {
                                from: "Δb"
                            },
                            Measured_L: {
                                from: "Measured_L"
                            },
                            Measured_a: {
                                from: "Measured_a"
                            },
                            Measured_b: {
                                from: "Measured_b"
                            },
                            MeasurementID: {
                                from: "MeasurementID"
                            },
                            Predicted_HTMLColor: {
                                from: "Predicted_HTMLColor"
                            },
                            Measured_HTMLColor: {
                                from: "Measured_HTMLColor"
                            }
                        }
                    }
                },
                pageSize: 10
            },
            sortable: true,
            filterable: {
                extra: false,
                operators: {
                    string: {
                        eq: "Is equal to"
                    }
                }
            },
            sortable: true,
            filterable: true,
            pageable: {
                refresh: true,
                pageSizes: true
            },
            noRecords: {
                template: ''
            },
            //toolbar: [{ 'name': 'downloadPatch', template: '<div style="float:right;"><button id="btnDownloadSpotPatch" onclick="generateReport(' + optimizationId + ')" class=\'k-button\'>Export to PDF</button></div>' }],
            toolbar: [{ 'name': 'downloadPatch', template: '<div style="float:right;"><button id="btnDownloadSpotPatch" data-id="' + optimizationId + '" class=\'k-button\'>Export to PDF</button></div>' }],
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
                    width: 50
                },
                {
                    field: "SpotView",
                    filterable: false,
                    title: "<b>View</b>",
                    template: "<center><a id='lnkView' title='View Patch Details' onclick='return ShowSpotColorPatchDetailsWindow(\"#=HTMLColor.substring(1)#\",#=CIELAB_L#,#=CIELAB_a#,#=CIELAB_b#,#=TargetL#,#=Targeta#,#=Targetb#,#=Measured_L#,#=Measured_a#,#=Measured_b#,#=MeasurementID#,\"#=Predicted_HTMLColor.substring(1)#\",\"#=Measured_HTMLColor.substring(1)#\");'><img src='Images/detail1.png'></img></a></center>",
                    width: 55
                },
                {
                    field: "SpotName",
                    title: "<b>Spot Name</b>",
                    width: 150
                },
                {
                    field: "ΔL",
                    title: "<b>pΔL</b>",
                    width: 80
                },
                {
                    field: "Δa",
                    title: "<b>pΔa</b>",
                    width: 80
                },
                {
                    field: "Δb",
                    title: "<b>pΔb</b>",
                    width: 80
                },
                {
                    field: "ΔE",
                    title: "<b>pΔE</b>",
                    width: 80
                },
                {
                    field: "ΔE_T_P",
                    title: "<b>ΔE_T_P</b>",
                    width: 120
                },
                {
                    field: "ΔE_M_T",
                    title: "<b>ΔE_M_T</b>",
                    width: 120
                },
                {
                    field: "ΔE_M_P",
                    title: "<b>ΔE_M_P</b>",
                    width: 120
                }
            ],
            dataBound: function (e) {
                var measurementId = parseInt($('#hdfMeasurementID').val());
                //if (e.sender.dataSource._view.length > 0) {
                //    measurementId = e.sender.dataSource._view[0].MeasurementID;
                //}
               
                if (measurementId > 0) {
                    this.hideColumn(4);
                    this.hideColumn(5);
                    this.hideColumn(6);
                    this.hideColumn(7);
                }
                else {
                    this.hideColumn(8);
                    this.hideColumn(9);
                    this.hideColumn(10);
                }


                var dataSource = this.dataSource;
                this.element.find('tr').each(function () {
                    var row = $(this);
                    //set resource message
                    row.find('#lnkView').attr('title', Resources.gridAnalyzeToolTipViewResource);
                });
            }
        });

        $('#btnDownloadSpotPatch').on('click', function (e) {
            e.preventDefault();            
            var optimizationRun = $(this).attr('data-id');
            DownloadSpotPatchFile(optimizationRun)
            //$.ajax({
            //    url: SERVICE_BASE_URL + 'api/Chart/GetSpotPatchDetailsReport',
            //    data: { OptimizationRunID: optimizationRun },
            //    type: 'GET',
            //    success: function (response) {
            //        window.open(SERVICE_BASE_URL + response.data, "_blank");
            //    },
            //    error: function (xhr, errorType, exception) {
            //        alert(exception);
            //    }
            //});
        })
    });

    function ShowSpotColorPatchDetailsWindow(htmlColor, actualL, actualA, actualB, targetL, targetA, targetB, measuredL, measuredA, measuredB, measurementID, P_htmlColor, M_htmlColor) {
        measuredL = (isNaN(measuredL) || measuredL == null) ? 0 : measuredL;
        measuredA = (isNaN(measuredA) || measuredA == null) ? 0 : measuredA;
        measuredB = (isNaN(measuredB) || measuredB == null) ? 0 : measuredB;

        var oWindow;
        oWindow = window.radopen("PopupSpotColorPatchDetails.aspx?htmlColor=" + htmlColor + "&actualL=" + actualL + "&actualA=" + actualA + "&actualB=" + actualB + "&targetL=" + targetL + "&targetA=" + targetA + "&targetB=" + targetB + "&measuredL=" + measuredL + "&measuredA=" + measuredA + "&measuredB=" + measuredB + "&measurementID=" + measurementID + "&P_htmlColor=" + P_htmlColor + "&M_htmlColor=" + M_htmlColor, "ClosePrintConditionWindow");
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

    function DownloadSpotPatchFile(id) {
        //debugger;
        //var xhrReq =  $.ajax({
        //    url: SERVICE_BASE_URL + 'api/Chart/GetSpotPatchDetailsReportTest',
        //    data: { OptimizationRunID: id },
        //    dataType: null,            
        //    type:'GET',
        //    xhrFields: {
        //        responseType: 'application/octet-stream'
        //    },
        //    success: function (blob) {

        //        debugger;
        //        console.log(xhrReq);
        //        console.log(blob.size);
        //        var link = document.createElement('a');
        //        link.href = window.URL.createObjectURL(blob);
        //        link.download = "Dossier_" + new Date() + ".pdf";
        //        link.click();
        //    },
        //    error: function (xhr, errorType, exception) {
        //        debugger;
        //        alert(exception);
        //    }
        //});

        //var request = new XMLHttpRequest();
        //request.open("GET", SERVICE_BASE_URL + 'api/Chart/GetSpotPatchDetailsReportTest?OptimizationRunID=' + id, true);
        //request.responseType = "blob";
        //request.onload = function (e) {
        //    //debugger;
        //    if (this.status === 200) {
        //        // `blob` response
        //        console.log(this.response);
        //        // create `objectURL` of `this.response` : `.pdf` as `Blob`
        //        var file = window.URL.createObjectURL(this.response);
        //        var a = document.createElement("a");
        //        a.href = file;
        //        a.download = this.response.name || "SpotPatch.pdf";
        //        document.body.appendChild(a);
        //        a.click();
        //        // remove `a` following `Save As` dialog, 
        //        // `window` regains `focus`
        //        window.onfocus = function () {
        //            document.body.removeChild(a)
        //        }
        //    };
        //};
        //request.send();

        location.href = SERVICE_BASE_URL + 'api/Chart/GetSpotPatchDetailsReportTest?OptimizationRunID=' + id;
    }
</script>

<script type="text/javascript" language="javascript">

    //According to new or old Project open rad window
    function ChooseRadWindow() {
        debugger;
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
        <asp:HiddenField runat="server" ID="hdnIsNewNCP" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="hdnIsNcpCreated" ClientIDMode="Static" />
        <asp:HiddenField ID="hdfOptimizationRunID" runat="server" Value="0" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnSelectedTabIndex" runat="server" Value="0" />
        <asp:HiddenField ID="hdMeasurement" runat="server" Value="0" />
        <asp:HiddenField runat="server" ID="hdfCurrentCulture" ClientIDMode="Static" />
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
                                                        <th colspan="2"><span>
                                                            <asp:Localize runat="server" ID="locSummary_Header"></asp:Localize>
                                                        </span></th>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <table id="tblFirstRunDeltas" runat="server" visible="false" style="width: 100%;">
                                                                <tr>
                                                                    <td style="width: 31%;"><span style="font-weight: bold;">
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
                                                            </table>
                                                            <table id="tblSecondRunDeltas" runat="server" visible="false" style="width: 100%;">
                                                                <tr>
                                                                    <td style="width: 43%;"></td>
                                                                    <td><span style="font-weight: bold;">ΔE_T_P</span></td>
                                                                    <td><span style="font-weight: bold;">ΔE_M_T</span></td>
                                                                    <td><span style="font-weight: bold;">ΔE_M_P</span></td>
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
                        </section>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="BtnExportCMYK" runat="server" CssClass="BUTTON" Text="Export CMYK"
                            meta:resourcekey="BtnExportCMYKResource1" OnClick="BtnExportCMYK_Click" />
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
    Height="240" Width="900" VisibleStatusbar="false" Title="Patch Details"
    ShowContentDuringLoad="false" Behaviors="Move,Close">
    <Windows>
        <telerik:RadWindow ID="windowPatchDetails" runat="server" ShowContentDuringLoad="false" VisibleStatusbar="false" Title="Patch Detailserere">
        </telerik:RadWindow>
        <telerik:RadWindow ID="ConfirmSaveWindow" runat="server" ShowContentDuringLoad="false"
            VisibleStatusbar="false" Title="Choose Next Action" Width="702" Height="250"
            Behaviors="Move,Close" NavigateUrl="~/PopupConfirmSave.aspx" meta:resourcekey="ConfirmSaveWindowResource1">
        </telerik:RadWindow>
    </Windows>
</telerik:RadWindowManager>
