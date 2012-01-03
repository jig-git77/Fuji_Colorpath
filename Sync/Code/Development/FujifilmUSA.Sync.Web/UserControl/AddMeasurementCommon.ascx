<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddMeasurementCommon.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.AddMeasurementCommon" %>
<%@ Import Namespace="FujifilmUSA.Sync.Common" %>
<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js" type="text/javascript"></script>
<script type="text/javascript">
    if (typeof jQuery == 'undefined') {
        document.write(unescape("%3Cscript src='JQuery/jquery-1.6.2.min.js' type='text/javascript'%3E%3C/script%3E"));
    }

    
</script>
<script src="JS/wz_jsgraphics.js" type="text/javascript"></script>
<script src="jQuery/json2.js" type="text/javascript"></script>
<script src="jQuery/jquery-blockUI.js" type="text/javascript"></script>
<script src="jQuery/wtooltip.min.js" type="text/javascript"></script>
<script src="JS/excanvas.js" type="text/javascript"></script>
<script src="JS/chartread.js" type="text/javascript"></script>
<script src="JS/i1read.js" type="text/javascript"></script>
<script src="JS/isisread.js" type="text/javascript"></script>
<script src="JS/WebComm.js" type="text/javascript"></script>
<script src="jQuery/jquery-1.6.2.min.js" type="text/javascript"></script>
<script src="jQuery/jquery.tmpl.js" type="text/javascript"></script>
<script src="JQuery/jquery.simplemodal.js" type="text/javascript"></script>
<style>
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
            <asp:TextBox ID="txtMeasurementName" runat="server" width="178px" Height="19px">
            </asp:TextBox>
            <%--<telerik:RadTextBox ID="txtMeasurementName" runat="server" SkinID="width250px">
            </telerik:RadTextBox>--%>
        </div>
        <div class="float_r">
            <span>
                <input type="button" id="btnCalibrate" value="Calibrate" /></span>
            <%--<telerik:RadButton ID="btnCalibrate" runat="server" Text="Calibrate">
            </telerik:RadButton>--%>
        </div>
        <div class="clear">
        </div>
        <div class="float_l">
            <input type="button" id="btnMeasureAndUploadChart" value="Measure / Upload" />
            <%--<telerik:RadButton ID="btnMeasureAndUploadChart" runat="server" Text="Measure / Upload">
            </telerik:RadButton>--%>
        </div>
        <div class="float_r">
            <input type="button" id="btnCancelMeasurement" value="Cancel Measurement" />
            <%--<telerik:RadButton ID="btnCancelMeasurement" runat="server" Text="Cancel Measurement">
            </telerik:RadButton>--%>
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="color_pallate">
        <div class="color_pallate_head">
            Colorbar</div>
        <div class="color_pallate_middle">
            <div class="color_bar">
                <div id="Canvas" style="position: relative; height: 350px; width: 630px; overflow: visible;">
                </div>
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


<asp:HiddenField ID="hdSyncRunID" runat="server" />
<asp:HiddenField ID="hdProjectID" runat="server" />
<asp:HiddenField ID="hdMeasurementTypeID" runat="server" />
<asp:HiddenField ID="hdProfileID" runat="server" Value="0" />

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
                            src="Images/delete_icon.png" title="Delete Project" 
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
        if ('<%=Type %>' == "Optimization") {
            GetRefChartData();
        }
    }
    function GetRefChartData() {
        
        jQuery("[id*='btnMeasureAndUploadChart']").val('Measure Chart');
        var Type = '<%=Type %>';
        var ReferenceChartTypeId = '';
        var SerialNo;
        if (Type == "DestinationProfile") {
            ReferenceChartTypeId = jQuery("[id*='cmbAlignmentChart']").val();
            SerialNo = jQuery("[id*='cmbDeviceType'] :selected").text(); //jQuery("[id*='cmbDeviceType']").text();
            SerialNo = SerialNo.substring(SerialNo.lastIndexOf('-') + 1);
            SerialNo = SerialNo.replace(" ", "")
        }

        GetReferenceChart(Type, (jQuery("[id*='hdProjectID']").val()), ReferenceChartTypeId, SerialNo);
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

        //Data binding of Uploaded Measurement
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
        jQuery("[id*='btnCloseModelPopup']").focus().click(function () {

        });
    }
    function saveMeasurement() {
        /****passing date and time from JavaScript to WCF serialization issue  http://stackoverflow.com/questions/4474352/send-jquery-json-to-wcf-rest-using-date ***/
        var Type = '<%=Type %>';
        var serviceLink;
        var dt = new Date();
        var dt1 = new Date(Date.UTC(dt.getFullYear(), dt.getMonth(), dt.getDate(), dt.getHours(), dt.getMinutes(), dt.getSeconds(), dt.getMilliseconds()));
        var MeasurementDateTime = dt1.toMSJSON();
        var SyncRunID = jQuery("input[id$=hdSyncRunID]").val();
        var MeasurementTypeID = jQuery("input[id$=hdMeasurementTypeID]").val();
        var MsmtName = jQuery("input[id*='txtMeasurementName']").val();
        var obj;
        var objSourceProfile;
        var objDestinationProfile;
        var objMeasurementLink;
        var objMeasurement;
        if (Type == "DestinationProfile") {
            SyncRunID = "0";
            MeasurementTypeID = jQuery("[id*='cmbAlignmentChart'] :selected").val();
            var DestinationProfilename = jQuery("[id$=txtDestinationProfile]").val();
            var ProfilerOptionID = jQuery("[id*='cmbProfilerOption'] :selected").val();
            var SyncDeviceID = '<%=SessionManager.Instance.CurrentSyncDevice.SyncDeviceId%>';
            var DestinationProfileID = jQuery("input[id$=hdProfileID]").val();
            objMeasurement = { MeasurementName: MsmtName, MeasurementDate: MeasurementDateTime, SyncRunId: 0, MeasurementTypeId: MeasurementTypeID, Patches: MeasuredPatches };
            objMeasurementLink = { Measurement: objMeasurement };
            objDestinationProfile = { ProfileId: DestinationProfileID, ProfileName: DestinationProfilename, SyncDeviceId: SyncDeviceID, IsUse: true, ProfileOptionId: ProfilerOptionID };
            obj = { DestinationProfile: objDestinationProfile, MeasurementLink: objMeasurementLink };
            serviceLink = "Services/PluginDataService.svc/SaveDestinationMeasurement";
        }
        else if (Type == "Optimization") {
            obj = { MeasurementName: MsmtName, MeasurementDate: MeasurementDateTime, SyncRunId: SyncRunID, MeasurementTypeId: MeasurementTypeID, Patches: MeasuredPatches };
            serviceLink = "Services/PluginDataService.svc/SaveMeasurement";
        }

        else if (Type == "SourceProfile") {
            SyncRunID = "0";
            MeasurementTypeID = jQuery("[id*='cmbAlignmentChart'] :selected").val();
            var SourceProfilename = jQuery("[id$=txtSourceProfile]").val();
            var ProfilerOptionID = jQuery("[id*='cmbProfilerOption'] :selected").val();
            var SyncDeviceID = 6;
            var SourceProfileID = jQuery("input[id$=hdProfileID]").val();
            objMeasurement = { MeasurementName: MsmtName, MeasurementDate: MeasurementDateTime, SyncRunId: 0, MeasurementTypeId: MeasurementTypeID, Patches: MeasuredPatches };
            objMeasurementLink = { Measurement: objMeasurement };
            objSourceProfile = { ProfileId: SourceProfileID, ProfileName: SourceProfilename, IsUse: true};
            obj = { SourceProfile: objSourceProfile, MeasurementLink: objMeasurementLink };
            serviceLink = "Services/PluginDataService.svc/SaveSourceMeasurement";
        }
        
        //Make ajax request...
        jQuery.ajax({
            type: "POST", //for complex object POST is preferred
            contentType: "application/json; charset=utf-8",   //this is very important application/json; charset=utf-8";  else it will fail
            url: serviceLink,  //"Services/PluginDataService.svc/SaveMeasurement",
            processData: false, //     I found that if I use processData=true, then the resulting string sent to the service is in querystring format, not in JSON. Not what I want for passing complex objects. So I set it to false. Using true would be fine for simpler non-JSON requests where you're doing WebGet, and all the params are in the query string
            dataType: "json",  ////this only applies to response from the server and (not request send to server)
            data: JSON.stringify(obj),
            dataFilter: function (data, type) {
                return data.replace(/"\\\/(Date\([0-9-]+\))\\\/"/gi, 'new $1');
            },
            //If the call succeeds
            success:
        function (response) {   //the response param passed to the success callback contains the returned json
            jQuery("input[id$=hdProfileID]").val(response);
            bindMeasurementData();
            //MeasurementDialog("Response From Server', 'measurement added successfully", "Successfully saved");
            //jQuery.growlUI('Response From Server', 'measurement added successfully', 1000);

        },
            //If the call fails
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Error");

                var jsonFault = JSON.parse(XMLHttpRequest.responseText);
               // MeasurementDialog("Error Cause:" + jsonFault.Reason + " Error Detail:" + jsonFault.DetailedInformation, "Error");
            }
        });

        /**clear the array **/
        
        OnCancel();
        refreshCanvas();
        jQuery("[id*='btnMeasureAndUploadChart']").val('Measure Chart');
       
    }




//    if (window.addEventListener) // W3C standard
//    {
//        window.addEventListener('load', ControlVisibility(), false); // NB **not** 'onload'
//    }
//    else if (window.attachEvent) // Microsoft
//    {
//        window.attachEvent('onload', ControlVisibility());
//    }




    function bindMeasurementData() {        
        var Type = '<%=Type %>';
        var serviceUrl;
        if (Type == "DestinationProfile") {
            var ProfileId = jQuery("input[id$=hdProfileID]").val();
            serviceUrl = "Services/PluginDataService.svc/GetMeasurementsByProfileType/" + ProfileId + "/" + Type;
        }
        if (Type == "Optimization") {
            var SyncRunId = jQuery("input[id$=hdSyncRunID]").val();
            serviceUrl = "Services/PluginDataService.svc/GetMeasurementsBySyncId/" + SyncRunId;
        }

        jQuery.ajax({
            type: "GET", //for complex object POST is preferred
            contentType: "application/json; charset=utf-8",   //this is very important application/json; charset=utf-8";  else it will fail
            url: serviceUrl, // "Services/PluginDataService.svc/GetMeasurementsByProfileType/" + ProfileId +"/"+Type,
            processData: false, //     I found that if I use processData=true, then the resulting string sent to the service is in querystring format, not in JSON. Not what I want for passing complex objects. So I set it to false. Using true would be fine for simpler non-JSON requests where you're doing WebGet, and all the params are in the query string
            dataType: "json",  ////this only applies to response from the server and (not request send to server)
            cache: false,
            dataFilter: function (data, type) {

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
            //If the call succeeds
            success:
                            function (response) {
                            },
            //If the call fails
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                var jsonFault = JSON.parse(XMLHttpRequest.responseText);
                MeasurementDialog("Error Cause:" + jsonFault.Reason + " Error Detail:" + jsonFault.DetailedInformation, "Error");
            }
        });
    }

    var isDeleted = false;
    function ConfirmDeleteMeasurement(measurement) {
        if (confirm('<%= AddMeasurement_DeleteMeasurement_Confirmation %>')) {

            deletemeasurement(measurement.name);
            bindMeasurementData();
        }
        return false;
    }

    function deletemeasurement(smeasurementId) {
        //measurement.name
        var isdeleted = false;
        var measurementId = $.trim(smeasurementId);
        var serviceLink = "Services/PluginDataService.svc/DeleteMeasurement/" + measurementId;
        //Make ajax request...
        jQuery.ajax({
            type: "POST", //for complex object POST is preferred
            contentType: "application/json; charset=utf-8",   //this is very important application/json; charset=utf-8";  else it will fail
            url: serviceLink,  //"Services/PluginDataService.svc/SaveMeasurement",
            processData: false, //     I found that if I use processData=true, then the resulting string sent to the service is in querystring format, not in JSON. Not what I want for passing complex objects. So I set it to false. Using true would be fine for simpler non-JSON requests where you're doing WebGet, and all the params are in the query string
            dataType: "json",  ////this only applies to response from the server and (not request send to server)
            dataFilter: function (data, type) {
                return false;
            },

            //If the call succeeds
            success:
                    function (response) {   //the response param passed to the success callback contains the returned json
                    },

            //If the call fails
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Error");

                var jsonFault = JSON.parse(XMLHttpRequest.responseText);
                MeasurementDialog("Error Cause:" + jsonFault.Reason + " Error Detail:" + jsonFault.DetailedInformation, "Error");
            }
        });
    }

    function RemoveTr(isdeleted, measurementId) {

        if (isdeleted == true) {
            $('#' + measurementId).remove();
        }
        var rowCount = $('#lvMeasurement1 tr').length;
        if (rowCount <= 0) {
            $("#measurementContainer").html("");
            $("#measurementContainer").append("<div class='no_data_found'>No records available</div>");
        }
    }

    /*--------------- Function to convert json date string to javascript date ----------------------*/
    function GetDate(jsonDate) {
        var value = new Date(parseInt(jsonDate.substr(6)));
        return value.getMonth() + 1 + "/" + value.getDate() + "/" + value.getFullYear() + " " + value.toLocaleTimeString();
    }



</script>
