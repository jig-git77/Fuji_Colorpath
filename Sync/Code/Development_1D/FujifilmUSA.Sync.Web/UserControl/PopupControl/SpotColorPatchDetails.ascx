<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SpotColorPatchDetails.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.PopupControl.SpotColorPatchDetails" %>
<link href="Content/kendo/2018.1.221/kendo.common.min.css" rel="stylesheet" />
<link href="Content/kendo/2018.1.221/kendo.default.min.css" rel="stylesheet" />
<script src="Scripts/kendo/2018.1.221/kendo.all.min.js"></script>
<style>
    /*body,html{
        background:'' !important;
    }*/

    .spotTable th{
        font-weight:bold;
        color:white;
    }

    .tblStates th{
        font-weight:bold;
        text-align:left;
    }
    .tblStates td{
        text-align:right;
    }

    .PatchDetailsTable {
        line-height: 20px;
        width: 100%;
    }

        .PatchDetailsTable table {
            width: 100%;
        }

        .PatchDetailsTable th {
            background-color: darkgrey;
            color: white;
        }

        .PatchDetailsTable td {
            text-align: center;
        }

    .chartBackground {
        background: url('Images/LabBackdrop.jpg');
        background-repeat: no-repeat;
        background-size: cover;
        /*height: 190px;*/
        /*width: 278px;*/
    }

    .chartPaperLBackground {
        background: url('Images/SpotL_Bg.jpg');
        background-repeat: no-repeat;
        background-size: cover;
        /*height: 180px;*/
        width: 46px;
    }
</style>
<script type="application/javascript">
    var hasMeasurement = true;
    var targetA = 0, targetB = 0, actualA = 0, actualB = 0, measuredA = 0, measuredB = 0, targetL = 0, actualL = 0, measuredL = 0;
    var drawing = kendo.drawing;
    var geometry = kendo.geometry;
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
        if (parseInt($('#hdnMeasurementID').val()) <= 0) {
            hasMeasurement = false;
        }
        PopulatePaperOneChart();
        PopulatePaperSecondChart();
    });

    function PopulatePaperOneChart() {
        if (hasMeasurement) {

            targetA = parseFloat($('#lblTargetA').html());
            targetB = parseFloat($('#lblTargetB').html());

            actualA = parseFloat($('#lblActualA').html());
            actualB = parseFloat($('#lblActualB').html());

            measuredA = parseFloat($('#lblMeasuredA').html());
            measuredB = parseFloat($('#lblMeasuredB').html());

        } else {

            targetA = parseFloat($('#lblTargetARun0').html());
            targetB = parseFloat($('#lblTargetBRun0').html());

            actualA = parseFloat($('#lblActualARun0').html());
            actualB = parseFloat($('#lblActualBRun0').html());

        }

        var targetPaperGreenObj = [actualA, actualB];
        var greenSeriesArray = [];
        greenSeriesArray.push(targetPaperGreenObj);
        var kendoGreenSeriseObj = {};
        kendoGreenSeriseObj.data = greenSeriesArray;
        kendoGreenSeriseObj.color = 'green';
        kendoGreenSeriseObj.markers = {
            background: 'green',
            size: 16,
            visual: function (e) {
                var src = kendo.format("Images/greendot.png");
                var image = new kendo.drawing.Image(src, e.rect);
                return image;
            }
        };
        kendoGreenSeriseObj.highlight = {
            visible: false
        }

        var targetPAPERRedObj = [targetA, targetB];
        var redSeriesArray = [];
        redSeriesArray.push(targetPAPERRedObj);
        var kendoRedSeriseObj = {};
        kendoRedSeriseObj.data = redSeriesArray;
        kendoRedSeriseObj.color = 'red';
        kendoRedSeriseObj.markers = {
            background: 'red',
            size: 12,
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

        $("#divChartABComparision").kendoChart({
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
                min: -128,
                max: 128,
                majorUnit: 128,
                axisCrossingValues: [-128, 128],
                //labels: { visible: false },
                title: { text: "" },
                //line: { visible: false },
                //majorGridLines: { visible: false },
                //majorTicks: { visible: false }
            },
            yAxis: {
                name: "yAxis",
                min: -128,
                max: 128,
                step: 128,
                majorUnit: 128,
                axisCrossingValues: [-128, 128],
                //labels: { visible: false },
                title: { text: "" },
                //line: { visible: false },
                //majorGridLines: { visible: false },
                //majorTicks: { visible: false }
            },
            chartArea: {
                width: 300,
                height: 250,
                background: ''
            },
        });
    }

    function PopulatePaperSecondChart() {        
        targetL = 0, actualL = 0, measuredL = 0;
        if (hasMeasurement) {

            targetL = parseFloat($('#lblTargetL').html());
            actualL = parseFloat($('#lblActualL').html());
            measuredL = parseFloat($('#lblMeasuredL').html());

        } else {
            targetL = parseFloat($('#lblTargetLRun0').html());
            actualL = parseFloat($('#lblActualLRun0').html());
        }

        var targetPaperGreenObj = [actualL];
        var greenSeriesArray = [];
        greenSeriesArray.push(targetPaperGreenObj);
        var kendoGreenSeriseObj = {};
        kendoGreenSeriseObj.data = greenSeriesArray;
        kendoGreenSeriseObj.color = 'green';
        kendoGreenSeriseObj.markers = {
            background: 'green',
            size: 16,
            visual: function (e) {
                var src = kendo.format("Images/greendot.png");
                var image = new kendo.drawing.Image(src, e.rect);
                return image;
            }
        };
        kendoGreenSeriseObj.highlight = {
            visible: false
        }

        var targetPAPERRedObj = [targetL];
        var redSeriesArray = [];
        redSeriesArray.push(targetPAPERRedObj);
        var kendoRedSeriseObj = {};
        kendoRedSeriseObj.data = redSeriesArray;
        kendoRedSeriseObj.color = 'red';
        kendoRedSeriseObj.markers = {
            background: 'red',
            size: 12,
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

        $("#divChartLComparision").kendoChart({
            seriesDefaults: {
                type: 'line',
            },
            series: [kendoGreenSeriseObj, kendoRedSeriseObj,kendoBlueSeriseObj],
            categoryAxis: {
                name: "categoryAxis",
                categories: [],                
            },
            valueAxis: {
                name: "valueAxis",                
                axisCrossingValues: [0, 100],
                min: 0,
                max: 100,
                step: 25,
                majorUnit: 25,               
            },
            chartArea: {
                width: 120,
                height: 250,                
            },
            render: function (e) {
                var chart = e.sender;
                //get value axis object
                var valueAxis = chart.getAxis("valueAxis");
                //get maximum value from the value axis
                //var maxValue = Math.abs(Math.max.apply(Math, valueAxis._axis.children.map(r => parseFloat(r.content))))
                var valueArray = $.map(valueAxis._axis.children, function (elem) {
                    return parseInt(elem.content);
                });
                var maxValue = Math.abs(Math.max.apply(Math, valueArray));
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

                var line = new kendo.drawing.Path({
                    stroke: {
                        color: "green",
                        width: 1
                    }
                });
                var valueSlotLine = valueAxis.slot(targetL);
                line.moveTo(valueSlotLine.origin).lineTo([maxCategorySlot.origin.x, valueSlotLine.origin.y]);
                var group = new kendo.drawing.Group();
                group.append(path,line);

                //set the path
                chart.surface.draw(group);
            }
        });
    }

    
</script>
<div style="display: block;" class="contantbox">
    <asp:HiddenField runat="server" ID="hdnMeasurementID" ClientIDMode="Static" />
    <div>
        <table width="100%">
            <tr>
                <td style="width: 25%;">
                    <table id="tblColorFirstRun" style="width: 100%;" runat="server" visible="false" class="spotTable">
                        <tr style="height:20px;background-color: darkgrey;">
                            <th>
                                <asp:Localize runat="server" Text="Target" ID="locTargetHead" meta:resourcekey="locTargetResource"></asp:Localize>
                                <img src="Images/greendot.png" style="width: 15px;"/>
                            </th>
                        </tr>
                        <tr style="height:115px;">
                            <td>
                                <div runat="server" id="divTargetRun0" style="height: 115px;background-color: #FFB700;"></div>
                            </td>
                        </tr>
                        <tr style="height:115px;">
                            <td>
                                <div runat="server" id="divActualRun0" style="height: 115px;background-color: #FFB700;"></div>
                            </td>
                        </tr>
                        <tr style="height:20px;background-color: darkgrey;">
                            <th>
                                <asp:Localize runat="server" Text="Predicted" ID="locPredictedHead" meta:resourcekey="locPredictedResource"></asp:Localize>
                                <img src="Images/purpledot.png" style="width: 15px;"/>
                            </th>
                        </tr>
                    </table>
                    <table id="tblColorMoreRun" style="width: 100%;" runat="server" visible="false" class="spotTable">
                        <tr style="height:20px;background-color: darkgrey;">
                            <th colspan="2">
                                <asp:Localize runat="server" Text="Target" ID="locTargetHead2" meta:resourcekey="locTargetResource"></asp:Localize>
                                <img src="Images/greendot.png" style="width: 15px;"/>
                            </th>
                        </tr>
                        <tr style="height:115px;">
                            <td colspan="2">
                                <div runat="server" id="divTarget" style="height: 115px;background-color: #FFB700;"></div>
                            </td>
                        </tr>
                        <tr style="height:115px;">
                            <td>
                                <div runat="server" id="divActual" style="height: 115px;background-color: #FFB700;"></div>
                            </td>
                            <td>
                                <div runat="server" id="divMeasured" style="height: 115px;background-color: #FFB700;"></div>
                            </td>
                        </tr>
                        <tr style="height:20px;background-color: darkgrey;">
                            <th>
                                <asp:Localize runat="server" Text="Predicted" ID="locPredictedHead2" meta:resourcekey="locPredictedResource"></asp:Localize>
                                <img src="Images/purpledot.png" style="width: 15px;"/>
                            </th>
                            <th>
                                <asp:Localize runat="server" Text="Measured" ID="locMeasuredHead2" meta:resourcekey="locMeasuredResource"></asp:Localize>
                                <img src="Images/guntarget.png" style="width: 15px;"/>
                            </th>
                        </tr>
                    </table>
                </td>
                <td style="width: 25%;">
                    <table style="width: 100%;" class="spotTable">
                        <tr style="height:20px;background-color: darkgrey;">
                            <th>a* b* Comparision</th>
                        </tr>
                        <tr style="height:250px;">
                            <td>
                                <div runat="server" id="div3" style="height: 250px;" class="chartBackground">
                                    <div id="divChartABComparision"></div>
                                </div>
                            </td>
                        </tr>                        
                    </table>
                </td>
                <td  style="width: 10%;">
                    <table style="width: 100%;"  class="spotTable">
                        <tr style="height:20px;background-color: darkgrey;">
                            <th>L* Comparision</th>
                        </tr>
                        <tr style="height:250px;">
                            <td>
                                <div style="height: 250px;">
                                    <div id="divChartLComparision"></div>
                                </div>
                            </td>
                        </tr>                        
                    </table>
                </td>
                <td style="width: 25%;">
                    <table id="tblDetailsFirstRun" runat="server" style="width: 100%;" visible="false" class="tblStates">
                        <tr style="height:20px;background-color: darkgrey;">
                            <td>
                                <table class="spotTable">
                                    <tr>
                                        <th style="width:60%"></th>
                                        <th style="width:20%"><asp:Localize runat="server" Text="Target" ID="locTargetTbl1" meta:resourcekey="locTargetResource"></asp:Localize></th>
                                        <th style="width:20%"><asp:Localize runat="server" Text="Predicted" ID="locPredictedTbl1" meta:resourcekey="locPredictedResource"></asp:Localize></th>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr style="height:70px;">
                            <td>
                                <table style="width:100%">
                                    <tr>
                                        <th style="width:30%">L*</th>
                                        <td style="width:20%"><asp:Label runat="server" ID="lblTargetLRun0"  ClientIDMode="Static"></asp:Label></td>
                                        <td style="width:20%"><asp:Label runat="server" ID="lblActualLRun0" ClientIDMode="Static"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <th>a*</th>
                                        <td><asp:Label runat="server" ID="lblTargetARun0" ClientIDMode="Static"></asp:Label></td>
                                        <td><asp:Label runat="server" ID="lblActualARun0" ClientIDMode="Static"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <th>b*</th>
                                        <td><asp:Label runat="server" ID="lblTargetBRun0" ClientIDMode="Static"></asp:Label></td>
                                        <td><asp:Label runat="server" ID="lblActualBRun0" ClientIDMode="Static"></asp:Label></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr style="height:2px;background-color: darkgrey;">
                            <td></td>
                        </tr>
                        <tr style="height:65px;">
                            <td>                                
                                <table style="width:100%">
                                    <tr>
                                        <th><asp:Localize runat="server" Text="ΔE Target-Prediction" ID="locDeltaTPRun0" meta:resourcekey="locDeltaTarget_Prediction"></asp:Localize></th>
                                        <td><asp:Label runat="server" ID="lblDeltaTargetPerdictionRun0" ></asp:Label></td>
                                    </tr>                                   
                                </table>
                            </td>
                        </tr>
                        <tr style="height:2px;background-color: darkgrey;">
                            <td></td>
                        </tr>
                        <tr style="height:30px;background-color: darkgrey;">
                            <td>
                                <table style="width:100%" class="spotTable">
                                    <tr>
                                        <th style="width:80%"></th>
                                        <th style="width:20%;text-align: right;">                                            
                                            <asp:Localize runat="server" Text="Delta_T_P" ID="locTPresource" meta:resourcekey="locT_PResource"></asp:Localize>
                                        </th>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr style="height:70px;">
                            <td>
                                <%--<div runat="server" id="div6" style="height: 100px;background-color: #FFB700;"></div>--%>
                                <table style="width:100%">
                                    <tr>
                                        <th style="width:80%">L*</th>                                        
                                        <td style="width:20%"><asp:Label runat="server" ID="lblDeltaL" ClientIDMode="Static"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <th>a*</th>
                                        <td><asp:Label runat="server" ID="lblDeltaA" ClientIDMode="Static"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <th>b*</th>
                                        <td><asp:Label runat="server" ID="lblDeltaB" ClientIDMode="Static"></asp:Label></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table id="tblDetailsMoreRun" runat="server" style="width: 100%;" visible="false" class="tblStates">
                        <tr style="height:20px;background-color: darkgrey;">
                            <td>
                                <table class="spotTable" style="width:100%">
                                    <tr>
                                        <th style="width:30%;"></th>
                                        <th style="width:20%;text-align:center;">T</th>
                                        <th style="width:20%;text-align:center;">P</th>
                                        <th style="width:20%;text-align:center;">M</th>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr style="height:70px;">
                            <td>
                                <table style="width:100%;height:70px" >
                                    <tr>
                                        <th style="width:25%">L*</th>
                                        <td style="width:20%"><asp:Label runat="server" ID="lblTargetL" ClientIDMode="Static"></asp:Label></td>
                                        <td style="width:20%"><asp:Label runat="server" ID="lblActualL" ClientIDMode="Static"></asp:Label></td>
                                        <td style="width:20%"><asp:Label runat="server" ID="lblMeasuredL" ClientIDMode="Static"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <th>a*</th>
                                        <td><asp:Label runat="server" ID="lblTargetA" ClientIDMode="Static"></asp:Label></td>
                                        <td><asp:Label runat="server" ID="lblActualA" ClientIDMode="Static"></asp:Label></td>
                                        <td><asp:Label runat="server" ID="lblMeasuredA" ClientIDMode="Static"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <th>b*</th>
                                        <td><asp:Label runat="server" ID="lblTargetB" ClientIDMode="Static"></asp:Label></td>
                                        <td><asp:Label runat="server" ID="lblActualB" ClientIDMode="Static"></asp:Label></td>
                                        <td><asp:Label runat="server" ID="lblMeasuredB" ClientIDMode="Static"></asp:Label></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr style="height:2px;background-color: darkgrey;">
                            <td></td>
                        </tr>
                        <tr style="height:75px;">
                            <td>                                
                                <table style="width:100%;height:75px;" >
                                    <tr>
                                        <th><asp:Localize runat="server" Text="ΔE Target-Prediction" ID="locDeltaTP" meta:resourcekey="locDeltaTarget_Prediction"></asp:Localize></th>
                                        <td><asp:Label runat="server" ID="lblDeltaTargetPrediction" ClientIDMode="Static"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <th><asp:Localize runat="server" Text="ΔE Target-Measured" ID="locDeltaTM" meta:resourcekey="locDeltaTarget_Measured"></asp:Localize></th>
                                        <td><asp:Label runat="server" ID="lblDeltaTargetMeasured" ClientIDMode="Static"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <th><asp:Localize runat="server" Text="ΔE Prediction-Measured" ID="locDeltaPM" meta:resourcekey="locDeltaMeasured_Prediction"></asp:Localize></th>
                                        <td><asp:Label runat="server" ID="lblDeltaPredictionMeasured" ClientIDMode="Static"></asp:Label></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr style="height:2px;background-color: darkgrey;">
                            <td></td>
                        </tr>
                        <tr style="height:20px;background-color: darkgrey;">
                            <td>
                                <table style="width:100%" class="spotTable">
                                    <tr>
                                        <th style="width:42%"></th>
                                        <th style="width:20%;text-align: center;">                                            
                                            <asp:Localize runat="server" Text="Delta_T_P" ID="locTP" meta:resourcekey="locT_PResource"></asp:Localize>
                                        </th>
                                        <th style="width:20%;text-align: center;">                                            
                                            <asp:Localize runat="server" Text="Delta_M_T" ID="locMT" meta:resourcekey="locM_TResource"></asp:Localize>
                                        </th>
                                        <th style="width:20%;text-align: center;">                                            
                                            <asp:Localize runat="server" Text="Delta_M_P" ID="locMP" meta:resourcekey="locM_PResource"></asp:Localize>
                                        </th>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr style="height:70px;">
                            <td>
                                <%--<div runat="server" id="div6" style="height: 100px;background-color: #FFB700;"></div>--%>
                                <table style="width:100%" class="tblStates">
                                    <tr>
                                        <th style="width:38%">L*</th>                                        
                                        <td style="width:20%"><asp:Label runat="server" ID="lblTarget_PredictedL" ClientIDMode="Static"></asp:Label></td>
                                        <td style="width:20%"><asp:Label runat="server" ID="lblMeasured_TargetL" ClientIDMode="Static"></asp:Label></td>
                                        <td style="width:20%"><asp:Label runat="server" ID="lblMeasured_PredictedL" ClientIDMode="Static"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <th>a*</th>
                                        <td><asp:Label runat="server" ID="lblTarget_PredictedA" ClientIDMode="Static"></asp:Label></td>
                                        <td><asp:Label runat="server" ID="lblMeasured_TargetA" ClientIDMode="Static"></asp:Label></td>
                                        <td><asp:Label runat="server" ID="lblMeasured_PredictedA" ClientIDMode="Static"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <th>b*</th>
                                        <td><asp:Label runat="server" ID="lblTarget_PredictedB" ClientIDMode="Static"></asp:Label></td>
                                        <td><asp:Label runat="server" ID="lblMeasured_TargetB" ClientIDMode="Static"></asp:Label></td>
                                        <td><asp:Label runat="server" ID="lblMeasured_PredictedB" ClientIDMode="Static"></asp:Label></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</div>