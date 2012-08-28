<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MeasurementCommon.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.MeasurementCommon" %>
<%@ Import Namespace="FujifilmUSA.Sync.Business" %>
<script src="JS/wz_jsgraphics.js?version=<%=JsRandomNumber %>" type="text/javascript"></script>
<script src="jQuery/json2.js?version=<%=JsRandomNumber %>" type="text/javascript"></script>
<script src="jQuery/jquery-blockUI.js?version=<%=JsRandomNumber %>" type="text/javascript"></script>
<script src="jQuery/wtooltip.min.js?version=<%=JsRandomNumber %>" type="text/javascript"></script>
<script src="JS/excanvas.js?version=<%=JsRandomNumber %>" type="text/javascript"></script>
<script src="JS/chartread.js?version=<%=JsRandomNumber %>" type="text/javascript"></script>
<script src="JS/i1read.js?version=<%=JsRandomNumber %>" type="text/javascript"></script>
<script src="JS/isisread.js?version=<%=JsRandomNumber %>" type="text/javascript"></script>
<script src="JS/WebComm.js?version=<%=JsRandomNumber %>" type="text/javascript"></script>
<script src="jQuery/jquery-1.7.1.min.js?version=<%=JsRandomNumber %>" type="text/javascript"></script>
<script src="jQuery/jquery.tmpl.js?version=<%=JsRandomNumber %>" type="text/javascript"></script>
<script src="JQuery/jquery.simplemodal.js?version=<%=JsRandomNumber %>" type="text/javascript"></script>

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
    
    /************ Progress bar ***************/
    .UpdateProgressBackground
    {
        z-index: 3003;
        position: fixed !important;
        filter: progid:DXImageTransform.Microsoft.Alpha(style=0,opacity=10);
        background-color: #dddddd !important;
        width: 100% !important;
        height: 100% !important;
        top: 0px;
        left: 0px;
        opacity: .1;
        mozopacity: .1;
    }
    /***************************/
</style>
<script language="javascript" type="text/javascript">
    function playSound(soundfile) {
        document.getElementById("dummy").innerHTML =
 "<embed src=\"" + soundfile + "\" hidden=\"true\" autostart=\"true\" loop=\"false\" />";
    }
</script>
<div>
    <div class="info_form">
        <div class="float_l InformationtxtDivWidth">
            <asp:Label ID="lblMeasurementName" runat="server" Text="Measurement Name" 
                meta:resourcekey="lblMeasurementNameResource1"></asp:Label>
            &nbsp; : &nbsp;&nbsp;
            <asp:TextBox ID="txtMeasurementName" runat="server" Width="178px" Height="19px" 
                meta:resourcekey="txtMeasurementNameResource1"></asp:TextBox>
        </div>
        <div class="float_r">
            <span>
                <input type="button" id="btnCalibrate" value='<%= GetResourceString("calibrateButton") %>' /></span>
        </div>
        <div class="clear">
        </div>
        <div class="float_l">
            <input type="button" id="btnMeasureAndUploadChart" value='<%= GetResourceString("measureUpload") %>' />
        </div>
        <div class="float_r">
            <input type="button" id="btnCancelMeasurement" value='<%= GetResourceString("cancelMeasurement") %>' />
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="color_pallate">
        <div class="color_pallate_head">
            <asp:Localize runat="server" Text="Colorbar" ID="locColorBar" 
                meta:resourcekey="locColorBarResource1"></asp:Localize></div>
        <div class="color_pallate_middle">
            <div id='divProgressImageMaster' style="display: none;">
                <div style="width: 200px; text-align: center; margin-left: 150px;">
                    <img src="Images/processbar.gif" alt="Loading" style="vertical-align: middle; margin: 2px;" /><br />
                    <br />
                    <asp:Localize runat="server" Text="Uploading Measurement..." 
                        ID="locUploadingMeasurement" 
                        meta:resourcekey="locUploadingMeasurementResource1"></asp:Localize><div class="clear">
                    </div>
                </div>
            </div>
            <div class="color_bar">
                <canvas id="Canvas" height="350" width="630"></canvas>
            </div>
        </div>
        <div class="color_pallate_bottom">
            &nbsp;</div>
    </div>
    <div class="color_pallate">
        <div class="color_pallate_head">
            <asp:Localize runat="server" Text="Recent Upload" ID="locRecentUpload" 
                meta:resourcekey="locRecentUploadResource1"></asp:Localize></div>
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
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" Modal="True">
    <Windows>
        <telerik:RadWindow ID="DownloadSoftwareWindow" runat="server" IconUrl="" VisibleStatusbar="false"
            ShowContentDuringLoad="false" Title="Download Software" Width="730" Height="270"
            Behaviors="Close">
        </telerik:RadWindow>
    </Windows>
</telerik:RadWindowManager>
<asp:HiddenField ID="hdOptimizeRunId" runat="server" />
<asp:HiddenField ID="hdProjectId" runat="server" />
<asp:HiddenField ID="hdReferenceChartTypeId" runat="server" />
<asp:HiddenField ID="hdSerialNumber" runat="server" Value="0" />
<asp:HiddenField ID="hdHasMeasurement" runat="server" Value="0" />
<asp:HiddenField ID="hdMeasurementLimit" runat="server" Value="0" />
<object id="plugin0" type="application/x-fujicmplugin" width="0" height="0">
</object>
<span id="dummy"></span>
<script id="MeasurementHeaderTemplate" type="text/x-jQuery-tmpl">
          <tr>
                <th scope="col">                
                    <%= GetResourceString("measurementName") %>
                </th>
                <th scope="col">
                    <%= GetResourceString("uploadDateTime") %>
                </th>
                <th scope="col">
                    <%= GetResourceString("deleteButton") %>
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
                       ${UploadDate}</span>
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
        jQuery("[id*='btnMeasureAndUploadChart']").val('<%= GetResourceString("measureChart") %>');
        jQuery("[id*='btnMeasureAndUploadChart']").css('display', 'none');
        jQuery("[id*='btnCancelMeasurement']").css('display', 'none');
        if(load('<%= ApplicationConfiguration.Instance.FujiFilmPluginVersion %>'))   //first you have to load the plugin
        {
            //Proceed further
            ListDevices();
            GetRefChartData();      
        }
        else
        {
            var oWindow;
            oWindow = window.radopen("PopupDownloadSoftware.aspx", "DownloadSoftwareWindow");
            oWindow.show();
        }        
    }
    
    function GetRefChartData() {
        jQuery("[id*='btnMeasureAndUploadChart']").val('<%= GetResourceString("measureChart") %>');
        var ReferenceChartTypeId = '';
        var SerialNo;
        GetReferenceChart(jQuery("[id*='hdProjectId']").val(), ReferenceChartTypeId, SerialNo);
    }
    jQuery(document).ready(function () {
   
        jQuery("[id*='btnCalibrate']").removeAttr('disabled'); //---Make Calibrate button Enabled
  
        jQuery("[id*='btnCalibrate']").click(function () {
            calibrateDevice(g_ScannerList);
            return false;
        });
        jQuery("[id*='btnMeasureAndUploadChart']").click(function () {
            if (jQuery("[id*='btnMeasureAndUploadChart']").val() == '<%= GetResourceString("measureChart") %>') {

                jQuery("[id*='btnCalibrate']").attr('disabled', ''); //------Make Calibrate button Disable after hitting Measure Chart button

                var MeasurementName = jQuery("[id*='txtMeasurementName']").val();
                if (jQuery.trim(MeasurementName).length < 4) {
                    MeasurementDialog('<%= GetResourceString("measurementNameLengthMsg") %>', '<%= GetResourceString("errorText") %>');
                    return false;
                }
                if(jQuery("input[id*='hdMeasurementLimit']").val() >= <%= ApplicationConfiguration.Instance.MeasurementsLimit %>)
                {
                    MeasurementDialog('<%= GetResourceString("runLimitReached") %>', '<%= GetResourceString("limitReachText") %>');
                    return false;
                }
                scan(g_ScannerList, 'Density,CIELab,CIEXYZ,sRGB', g_Chart);
            }
            else if (jQuery("[id*='btnMeasureAndUploadChart']").val() == 'Upload Measurement') {
                upload();
            }
        });
        jQuery("[id*='btnCancelMeasurement']").click(function () {
            refreshCanvas();
            jQuery("[id*='btnMeasureAndUploadChart']").val('<%= GetResourceString("measureChart") %>');
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

         //Load progress bar
        LoadProgressBrowserWindow();

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
                    StopProgressBrowserWindow();
                    jQuery("[id*='lblMessage']").html('<%= GetResourceString("statusText") %>' + " " + '<%= GetResourceString("measurementUploadedSuccess") %>');
                    jQuery("[id*='btnCalibrate']").removeAttr('disabled'); //---Make Calibrate button Enabled, after successfully uploading measurement in DB
                },
            error:
                function (XMLHttpRequest, textStatus, errorThrown) {
                    var jsonFault = JSON.parse(XMLHttpRequest.responseText);
                    StopProgressBrowserWindow();
                    MeasurementDialog('<%= GetResourceString("errorCause") %>'+ jsonFault.Reason +  " " + '<%= GetResourceString("errorDetail") %>' + jsonFault.DetailedInformation, "Error");
                }
        });
        OnCancel();
        refreshCanvas();
        jQuery("[id*='btnMeasureAndUploadChart']").val('<%= GetResourceString("measureChart") %>');
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
                   // for (i = 0; i < json.length; i++) {  //Removed these lines because now we are passing MeasurementDate as a string instead of DateTime as "UploadDate"
                       // var date = GetDate(json[i].MeasurementDate);
                       // json[i].MeasurementDate = date;                    
                    //}
                    if (json.length > 0) {
                        jQuery("input[id*='hdHasMeasurement']").val('true');
                        $("#measurementContainer").html("");
                        $("#measurementContainer").append("<table id ='lvMeasurement1'  cellspacing='0' cellpadding='0' border='0' class='recent_upload'><tbody></tbody></table>");
                        $("#MeasurementHeaderTemplate").tmpl().appendTo("#lvMeasurement1");
                        $("#MeasurementDetailTemplate").tmpl(json).appendTo("#lvMeasurement1");
                        jQuery("input[id*='hdMeasurementLimit']").val(json.length);  //----------Set MeasurementLimit in hiddeninput-------------//                       
                    }
                    else {
                        jQuery("input[id*='hdHasMeasurement']").val('false');
                        $("#measurementContainer").html("");
                        $("#measurementContainer").append("<div class='no_data_found'>" + '<%= GetResourceString("noRecordsAvailable") %>' + "</div>");
                    }
                },
            success:
                function (response) { },
            error:
                function (XMLHttpRequest, textStatus, errorThrown) {
                    var jsonFault = JSON.parse(XMLHttpRequest.responseText);
                    MeasurementDialog('<%= GetResourceString("errorCause") %>'+ jsonFault.Reason + " " + '<%= GetResourceString("errorDetail") %>' + jsonFault.DetailedInformation, "Error");
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
                MeasurementDialog('<%= GetResourceString("errorCause") %>' + jsonFault.Reason + " " + '<%= GetResourceString("errorDetail") %>' + jsonFault.DetailedInformation, "Error");
            }
        });
    }
    function GetDate(jsonDate) {
        var value = new Date(parseInt(jsonDate.substr(6)));
        return value.getMonth() + 1 + "/" + value.getDate() + "/" + value.getFullYear() + " " + value.toLocaleTimeString();
    }


    /************ Progress bar ***************/
    function LoadProgressBrowserWindow() {
        var divBackgroundProgress = document.getElementById('divBackgroundProgressMaster');
        var divProgressImage = document.getElementById('divProgressImageMaster');

        divBackgroundProgress.style.display = 'block';
        divProgressImage.style.display = 'block';

        var winWidth;
        var winHeight;
        winWidth = (window.screen.width / 2);
        winHeight = (window.screen.height / 2);

        divProgressImage.style.left = winWidth + 'px';
        divProgressImage.style.top = winHeight + 'px';

    }
    function StopProgressBrowserWindow() {
        jQuery("[id*='divBackgroundProgressMaster']").hide();
        jQuery("[id*='divProgressImageMaster']").hide();
    }


</script>
<div id="divBackgroundProgressMaster" class="UpdateProgressBackground" style="display: none;
    position: fixed; top: 0px; left: 0px; height: 100%; width: 100%;">
</div>
