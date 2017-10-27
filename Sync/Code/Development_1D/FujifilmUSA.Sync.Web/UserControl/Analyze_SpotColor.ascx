<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Analyze_SpotColor.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.Analyze_SpotColor" %>
<%@ Register Src="~/UserControl/PopupControl/SpotColorPatchDetails.ascx" TagName="SpotColorPatchDetails" TagPrefix="pd" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="uc" %>
<link href="Content/kendo/2017.2.621/kendo.common.min.css" rel="stylesheet" />
<link href="Content/kendo/2017.2.621/kendo.default.min.css" rel="stylesheet" />
<script src="Scripts/kendo/2017.2.621/kendo.all.min.js"></script>
<style>
    .PatchDetailsSummaryTable {
        line-height: 35px;
        width: 50%;
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
</style>
<script src="JS/ChartTooltip.js" type="text/javascript"></script>

<script type="text/javascript">
    var record = 0;
    var SERVICE_BASE_URL = '';
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
        SERVICE_BASE_URL = $('#hdnBaseServiceUrl').val();
        var optimizationId = $('#hdfOptimizationRunID').val();
        $("#divPathDetails").kendoGrid({
            dataSource: {
                //transport: {
                //    //read: "/FujifilmUSA.Sync.Web/Analyze_SpotColor.aspx/GetPatchDetails"
                //    read: "http://localhost/FujiFilmUSA.Sync.Service/api/Chart/GetPatchDetails/" + oId
                //},
                transport: {
                    read: function (options) {
                        $.ajax({
                            type: "GET",
                            url: SERVICE_BASE_URL + "api/Chart/GetPatchDetails/",
                            data: { id: optimizationId },
                            dataType: "json",
                            success: function (response) {
                                console.log(response.data);
                                if (response.data != null) {
                                    $('#lblMinDeltaE').html(response.data.minDeltaE);
                                    $('#lblMaxDeltaE').html(response.data.maxDeltaE);
                                    $('#lblAvgDeltaE').html(response.data.avgDeltaE);
                                }
                                options.success(response.data.patchDetails);
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
            columns: [
                {
                    field: "SpotID",
                    title: "<b>ID</b>",
                    width: 50,
                    filterable: false,
                },
                {
                    filterable: false,
                    title: "<b>Patch</b>",
                    //template: "<span id='sp' style='display:inline-block;background-color:#FFB700;height:14px;width:30px;'></span>",
                    template: "<span id='sp' style='display:inline-block;background-color:#=HTMLColor#;height:14px;width:30px;'></span>",
                    width: 50
                },
                {
                    filterable: false,
                    title: "<b>View</b>",
                    template: "<center><a id='lnkView' onclick='return ShowSpotColorPatchDetailsWindow(\"#=HTMLColor.substring(1)#\",#=CIELAB_L#,#=CIELAB_a#,#=CIELAB_b#,#=TargetL#,#=Targeta#,#=Targetb#,#=Measured_L#,#=Measured_a#,#=Measured_b#,#=MeasurementID#);'><img src='Images/detail1.png'></img></a></center>",
                    //template: "<center><a id='lnkView' onclick='return ShowSpotColorPatchDetailsWindow(\"#=HTMLColor.substring(1)#\",#=CIELAB_L#,#=CIELAB_a#,#=CIELAB_b#,#=TargetL#,#=Targeta#,#=Targetb#);'><img src='Images/detail1.png'></img></a></center>",
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
                var measurementId = 0;
                if (e.sender.dataSource._view.length > 0) {
                    measurementId = e.sender.dataSource._view[0].MeasurementID;
                }

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
            }
        });
    });

    function ShowSpotColorPatchDetailsWindow(htmlColor, actualL, actualA, actualB, targetL, targetA, targetB, measuredL, measuredA, measuredB, measurementID) {
        measuredL = isNaN(measuredL) ? 0 : measuredL;
        measuredA = isNaN(measuredA) ? 0 : measuredA;
        measuredB = isNaN(measuredB) ? 0 : measuredB;

        var oWindow;
        oWindow = window.radopen("PopupSpotColorPatchDetails.aspx?htmlColor=" + htmlColor + "&actualL=" + actualL + "&actualA=" + actualA + "&actualB=" + actualB + "&targetL=" + targetL + "&targetA=" + targetA + "&targetB=" + targetB + "&measuredL=" + measuredL + "&measuredA=" + measuredA + "&measuredB=" + measuredB + "&measurementID=" + measurementID, "ClosePrintConditionWindow");
        oWindow.show();
        return false;
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
    <div class="listDevice" style="margin-left: 10px;">
        <uc:Statusbar ID="fujiStatusbar" runat="server" Visible="False" />
    </div>
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
                                                <%--<pd:SpotColorPatchDetails runat="server" ID="ucSpotColorPathDetails" />--%>

                                                <table class="PatchDetailsSummaryTable">
                                                    <tr>
                                                        <th colspan="2">Optimal Device Prediction</th>
                                                    </tr>
                                                    <tr>
                                                        <td><b>Min ΔE</b></td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblMinDeltaE" ClientIDMode="Static" Text="Loading..."></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><b>Average ΔE</b></td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblAvgDeltaE" ClientIDMode="Static" Text="Loading..."></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><b>Max ΔE</b></td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblMaxDeltaE" ClientIDMode="Static" Text="Loading..."></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><b>Media</b></td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblMedia"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><b>Colorant</b></td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblColorant"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><b>Rip Settings</b></td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblRipSettings"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><b>Spot Target</b></td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblSpotTarget"></asp:Label></td>
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
            </table>
        </div>
    </div>
</div>
<div class="contant_bottom">
    &nbsp;
</div>
<asp:HiddenField ID="hdHasMeasurement" runat="server" Value="0" />
<telerik:RadWindowManager ID="RadWindowManager" runat="server" Modal="True"
    Height="240" Width="900" VisibleStatusbar="false" Title="Patch Details"
    ShowContentDuringLoad="false" Behaviors="Move,Close">
    <Windows>
        <telerik:RadWindow ID="windowPatchDetails" runat="server">
        </telerik:RadWindow>
        <telerik:RadWindow ID="ConfirmSaveWindow" runat="server" ShowContentDuringLoad="false"
            VisibleStatusbar="false" Title="Choose Next Action" Width="702" Height="250"
            Behaviors="Move,Close" NavigateUrl="~/PopupConfirmSave.aspx" meta:resourcekey="ConfirmSaveWindowResource1">
        </telerik:RadWindow>
    </Windows>
</telerik:RadWindowManager>
