<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MeasurementCommon.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.MeasurementCommon" %>
<script src="JS/wz_jsgraphics.js?version=4" type="text/javascript"></script>
<script src="jQuery/json2.js?version=4" type="text/javascript"></script>
<script src="jQuery/jquery-blockUI.js?version=4" type="text/javascript"></script>
<script src="jQuery/wtooltip.min.js?version=4" type="text/javascript"></script>
<script src="JS/excanvas.js?version=4" type="text/javascript"></script>
<script src="JS/chartread.js?version=4" type="text/javascript"></script>
<script src="JS/i1read.js?version=4" type="text/javascript"></script>
<script src="JS/isisread.js?version=4" type="text/javascript"></script>
<script src="JS/WebComm.js?version=4" type="text/javascript"></script>
<script src="jQuery/jquery-1.7.1.min.js?version=4" type="text/javascript"></script>
<script src="jQuery/jquery.tmpl.js?version=4" type="text/javascript"></script>
<script src="JQuery/jquery.simplemodal.js?version=4" type="text/javascript"></script>
<style type="text/css">
    .my_rectangle
    {
        width: 25px;
        height: 25px;
        background: pink;
    }
    .tooltipclass
    {
        background: #fbfacd;
        border-width: 1px;
        border-style: solid;
        border-color: #000;
    }
    
    .pluginImageTooltipClass
    {
        background: #fbfacd;
        border-width: 1px;
        border-style: solid;
        border-color: #000;
        font-family: Tahoma;
        font-size: 11px;
    }
    #modalBackground
    {
        position: fixed;
        left: 0;
        top: 0;
        width: 100%;
        height: 100%;
        z-index: 9;
        background-color: #333333;
        display: none;
        opacity: 0.40;
        filter: alpha(opacity=40);
    }
    #modalWindow
    {
        position: fixed;
        left: 0;
        top: 0;
        z-index: 10;
        background-color: white;
        display: none;
        width: 40em;
        height: 10em;
    }
</style>
<div>
    <div class="info_form">
        <div class="float_l InformationtxtDivWidth">
            <asp:Label ID="lblMeasurementName" runat="server" Text="Measurement Name"></asp:Label>
            &nbsp; : &nbsp;&nbsp;
            <asp:TextBox ID="txtMeasurementName" runat="server" Width="178px" Height="19px">
            </asp:TextBox>
        </div>
        <div class="float_r">
            <span>
                <input type="button" id="btnCalibrate" value="Calibrate" /></span>
        </div>
        <div class="clear">
        </div>
        <div class="float_l">
            <input type="button" id="btnMeasureAndUploadChart" value="Measure / Upload" />
        </div>
        <div class="float_r">
            <input type="button" id="btnCancelMeasurement" value="Cancel Measurement" />
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="color_pallate">
        <div class="color_pallate_head">
            Colorbar</div>
        <div class="color_pallate_middle">
            <div class="color_bar">
                <canvas id="Canvas" height="350" width="630"></canvas>
                <%--<canvas id="Canvas" style="position: relative; height: 350px; width: 630px; overflow: visible;">
                </canvas>--%>
            </div>
        </div>
        <div class="color_pallate_bottom">
            &nbsp;</div>
    </div>
    <div class="color_pallate">
        <div class="color_pallate_head">
            Recent Upload</div>
        <div class="color_pallate_middle">
            <div id="measurementContainer">
            </div>
        </div>
        <div class="color_pallate_bottom">
            &nbsp;</div>
    </div>
</div>
<!-- modal content -->
<div id='confirm'>
    <div class='header'>
        <span></span>
    </div>
    <div class='message'>
    </div>
    <div class='buttons'>
        <div class='no simplemodal-close'>
            <input type="button" value="OK" id="btnCloseModelPopup" style="border: none; background-color: transparent;
                padding-top: 4px" /></div>
    </div>
</div>
<asp:HiddenField ID="hdOptimizeRunId" runat="server" />
<asp:HiddenField ID="hdProjectId" runat="server" />
<asp:HiddenField ID="hdReferenceChartTypeId" runat="server" />
<asp:HiddenField ID="hdSerialNumber" runat="server" Value="0" />
<object id="plugin0" type="application/x-fujicmplugin" width="0" height="0">
</object>
<script id="MeasurementHeaderTemplate" type="text/x-jQuery-tmpl">
          <tr>
                <th scope="col">
                    Measurement Name
                </th>
                <th scope="col">
                    Upload Date Time
                </th>
                <th scope="col">
                    Delete
                </th>
          </tr>
</script>
<script id="MeasurementDetailTemplate" type="text/x-jQuery-tmpl">
         <tr id = "${MeasurementId}">
                <td align="center">
                    <span id="lblMeasurementName">
                        ${MeasurementName}</span>
                </td>
                <td align="center">
                    <span id="lblMeasurementDate">
                       ${MeasurementDate}</span>
                </td>
                <td align="center">
                    <input 
                           type="image" 
                            onclick="javascript:return ConfirmDeleteMeasurement(this);" 
                            alt="Delete"
                            src="Images/delete_icon.png" title="Delete Measurement" 
                            id="imgbtnDelete"
                            name=" ${MeasurementId}"
                            >
                </td>
         </tr>
</script>
<script language="javascript" type="text/javascript">
    var refreshTry = 0;
    function ControlVisibility() {
        jQuery("[id*='btnCalibrate']").css('display', 'none');
        jQuery("[id*='btnMeasureAndUploadChart']").val('Measure Chart');
        jQuery("[id*='btnMeasureAndUploadChart']").css('display', 'none');
        jQuery("[id*='btnCancelMeasurement']").css('display', 'none');
        load();   //first you have to load the plugin
        ListDevices();
        GetRefChartData();
    }
    function GetRefChartData() {
        jQuery("[id*='btnMeasureAndUploadChart']").val('Measure Chart');
        var ReferenceChartTypeId = '';
        var SerialNo;
        GetReferenceChart(jQuery("[id*='hdProjectId']").val(), ReferenceChartTypeId, SerialNo);
    }
    jQuery(document).ready(function () {
        jQuery("[id*='btnCalibrate']").click(function () {
            calibrateDevice(g_ScannerList[0]);
            return false;
        });
        jQuery("[id*='btnMeasureAndUploadChart']").click(function () {
            if (jQuery("[id*='btnMeasureAndUploadChart']").val() == 'Measure Chart') {
                var MeasurementName = jQuery("[id*='txtMeasurementName']").val();
                if (jQuery.trim(MeasurementName).length < 4) {
                    MeasurementDialog("Please enter measurement name that is at least three characters long.", "Error");
                    return false;
                }
                scan(g_ScannerList[0], 'Density,CIELab,CIEXYZ,sRGB', g_Chart);
            }
            else if (jQuery("[id*='btnMeasureAndUploadChart']").val() == 'Upload Measurement') {
                upload();
            }
        });
        jQuery("[id*='btnCancelMeasurement']").click(function () {
            refreshCanvas();
            jQuery("[id*='btnMeasureAndUploadChart']").val('Measure Chart');
            OnCancel();
            ListDevices();
        });
        bindMeasurementData();
        ControlVisibility();
    });

    function MeasurementDialog(message, type) {
        jQuery("[id*='confirm']").modal({
            closeHTML: "<a href='#' title='Close' class='modal-close'>x</a>",
            position: ["20%", ],
            overlayId: 'confirm-overlay',
            containerId: 'confirm-container',
            onShow: function (dialog) {
                var modal = this;
                jQuery('.message', dialog.data[0]).append(message);
                jQuery('.header', dialog.data[0]).append(type);
            }
        });
    }
    function saveMeasurement() {
        var dt = new Date();
        var dt1 = new Date(Date.UTC(dt.getFullYear(), dt.getMonth(), dt.getDate(), dt.getHours(), dt.getMinutes(), dt.getSeconds(), dt.getMilliseconds()));
        var MeasurementDateTime = dt1.toMSJSON();
        var OptimizeRunId = jQuery("input[id$=hdOptimizeRunId]").val();
        var ReferenceChartTypeId = jQuery("input[id$=hdReferenceChartTypeId]").val();
        var MsmtName = jQuery("input[id*='txtMeasurementName']").val();
        var obj;
        obj = {
            MeasurementName: MsmtName,
            MeasurementDate: MeasurementDateTime,
            OptimizeRunId: OptimizeRunId,
            ReferenceChartTypeId: ReferenceChartTypeId,
            MeasurementPatches: MeasuredPatches
        };
        //Make ajax request...
        jQuery.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Services/PluginDataService.svc/SaveMeasurement",
            processData: false,
            dataType: "json",
            data: JSON.stringify(obj),
            dataFilter: function (data, type) {
                return data.replace(/"\\\/(Date\([0-9-]+\))\\\/"/gi, 'new $1');
            },
            success:
                function (response) {
                    bindMeasurementData();
                    //MeasurementDialog("Response From Server', 'measurement added successfully", "Successfully saved");
                    jQuery.growlUI('Response From Server', 'measurement added successfully', 1000);
                },
            error:
                function (XMLHttpRequest, textStatus, errorThrown) {
                    var jsonFault = JSON.parse(XMLHttpRequest.responseText);
                    MeasurementDialog("Error Cause:" + jsonFault.Reason + " Error Detail:" + jsonFault.DetailedInformation, "Error");
                }
        });
        OnCancel();
        refreshCanvas();
        jQuery("[id*='btnMeasureAndUploadChart']").val('Measure Chart');
    }

    function bindMeasurementData() {
        var OptimizeRunId = jQuery("input[id$=hdOptimizeRunId]").val();
        jQuery.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: "Services/PluginDataService.svc/GetMeasurementsByOptimizeId/" + OptimizeRunId,
            processData: false,
            dataType: "json",
            cache: false,
            dataFilter:
                function (data, type) {
                    var json = jQuery.parseJSON(data);
                    for (i = 0; i < json.length; i++) {
                        var date = GetDate(json[i].MeasurementDate);
                        json[i].MeasurementDate = date;
                    }
                    if (json.length > 0) {
                        jQuery("input[id*='hdHasMeasurement']").val(json.length);
                        $("#measurementContainer").html("");
                        $("#measurementContainer").append("<table id ='lvMeasurement1'  cellspacing='0' cellpadding='0' border='0' class='recent_upload'><tbody></tbody></table>");
                        $("#MeasurementHeaderTemplate").tmpl().appendTo("#lvMeasurement1");
                        $("#MeasurementDetailTemplate").tmpl(json).appendTo("#lvMeasurement1");
                    }
                    else {
                        jQuery("input[id*='hdHasMeasurement']").val('0');
                        $("#measurementContainer").html("");
                        $("#measurementContainer").append("<div class='no_data_found'>No records available</div>");
                    }
                },
            success:
                function (response) { },
            error:
                function (XMLHttpRequest, textStatus, errorThrown) {
                    var jsonFault = JSON.parse(XMLHttpRequest.responseText);
                    MeasurementDialog("Error Cause:" + jsonFault.Reason + " Error Detail:" + jsonFault.DetailedInformation, "Error");
                }
        });
    }
    function ConfirmDeleteMeasurement(measurement) {
        if (confirm('<%= AddMeasurement_DeleteMeasurement_Confirmation %>')) {
            deletemeasurement(measurement.name);
            bindMeasurementData();
        }
        return false;
    }
    function deletemeasurement(smeasurementId) {
        var measurementId = $.trim(smeasurementId);
        jQuery.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Services/PluginDataService.svc/DeleteMeasurement/" + measurementId,
            processData: false,
            dataType: "json",
            dataFilter: function (data, type) {
                return false;
            },
            success:
                    function (response) {
                    },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                var jsonFault = JSON.parse(XMLHttpRequest.responseText);
                MeasurementDialog("Error Cause:" + jsonFault.Reason + " Error Detail:" + jsonFault.DetailedInformation, "Error");
            }
        });
    }
    function GetDate(jsonDate) {
        var value = new Date(parseInt(jsonDate.substr(6)));
        return value.getMonth() + 1 + "/" + value.getDate() + "/" + value.getFullYear() + " " + value.toLocaleTimeString();
    }
</script>
