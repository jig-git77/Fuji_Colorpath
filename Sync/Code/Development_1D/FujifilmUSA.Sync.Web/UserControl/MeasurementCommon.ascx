<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="MeasurementCommon.ascx.cs"
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
            <asp:HiddenField ID="hdnPCSType" runat="server" />
            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td style="width: 22%;" align="right">
                        <asp:Label ID="lblMeasurementName" runat="server" Text="Measurement Name" meta:resourcekey="lblMeasurementNameResource1"></asp:Label>:&nbsp;
                    </td>
                    <td style="height: 27px;">
                        <asp:TextBox ID="txtMeasurementName" runat="server" Width="178px" Height="19px" meta:resourcekey="txtMeasurementNameResource1"></asp:TextBox>        
                    </td>                    
                    <td rowspan="2" ID="tdNotesCaption" runat="server" visible="false" align="right">
                        <asp:Label ID="lblNotes" runat="server" Text="Notes" meta:resourcekey="lblNotesResource1"></asp:Label>:&nbsp;
                    </td>
                    <td rowspan="2" ID="tdNotes" runat="server" visible="false">
                        <asp:TextBox ID="txtNotes" runat="server" Width="178px" Height="50px" TextMode="MultiLine" style="resize: none;" MaxLength="1000"></asp:TextBox>
                    </td>
                </tr>
                <tr ID="trCustomer" runat="server" visible="false">
                    <td align="right">            
                        <asp:Label ID="lblCustomer" runat="server" Text="Customer" meta:resourcekey="lblCustomerResource1"></asp:Label>:&nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtCustomer" runat="server" Width="178px" Height="19px" MaxLength="50"></asp:TextBox>
                    </td>                   
                </tr>
                <tr id="trValidationDetails" runat="server" visible="false">
                    <td align="right">
                        <asp:Label ID="lblPageOrSurface" runat="server" Text="Page/Surface"  meta:resourcekey="lblPageOrSurfaceResource1"></asp:Label>:&nbsp;
                    </td>
                    <td style="height: 27px;">
                        <asp:TextBox ID="txtPageOrSurface" runat="server" Width="178px" Height="19px" MaxLength="50"></asp:TextBox>        
                    </td>
                    <td align="right">
                        <asp:Label ID="lblJDFJobId" runat="server" Text="JDF Job ID"  meta:resourcekey="lblJDFJobIdResource1"></asp:Label>:&nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtJDFJobId" runat="server" Width="178px" Height="19px" MaxLength="50"></asp:TextBox>                          
                    </td>
                </tr>
            </table>                               
        </div>
        <div class="float_r">
            <span>
                <input type="button" id="btnCalibrate" value='<%= GetResourceString("calibrateButton") %>' /></span>
        </div>
        <div class="clear">
        </div>
        <div class="float_l">
            <input type="button" id="btnMeasureAndUploadChart" value='<%= GetResourceString("measureUpload") %>' />
            <input type="hidden" id="hdMeasureAndUploadChart" value="measurechart" />
        </div>
        <div class="float_r">
            <input type="button" id="btnCancelMeasurement" value='<%= GetResourceString("cancelMeasurement") %>' />
        </div>
        <div class="float_r">
            <input type="button" id="btnRewindMeasurement" value="Rewind" />
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="color_pallate">
        <div class="color_pallate_head">
            <asp:Localize runat="server" Text="Colorbar" ID="locColorBar" meta:resourcekey="locColorBarResource1"></asp:Localize></div>
        <div class="color_pallate_middle">
            <div id='divProgressImageMaster' style="display: none;">
                <div style="width: 200px; text-align: center; margin-left: 150px;">
                    <img src="Images/processbar.gif" alt="Loading" style="vertical-align: middle; margin: 2px;" /><br />
                    <br />
                    <asp:Localize runat="server" Text="Uploading Measurement..." ID="locUploadingMeasurement"
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
            <asp:Localize runat="server" Text="Recent Upload" ID="locRecentUpload" meta:resourcekey="locRecentUploadResource1"></asp:Localize>
              <div id="Div_ImportMeasurements" style="float:right; padding-right:10px;">
                <input type="button" id="btnImportMeasurements"  value='<%= GetResourceString("btnImportMeasurements") %>' onclick="javascript:return ShowImportWindow();" />             
            </div>
        </div>
        <div class="color_pallate_middle">
            <div id="measurementContainer">
            </div>
        </div>
        <div class="color_pallate_bottom">
            &nbsp;</div>
    </div>
</div>
<!-- modal content -->
<div id='MeasurementConfirmDialog'>
    <div class='header'>
        <span></span>
    </div>
    <div class='message'>
    </div>
    <div class='buttons'>
        <div class='no simplemodal-close'>
            <input type="button" id="btnCloseModelPopup" value='<%= GetResourceString("btnCloseModelPopup") %>' style="border: none; background-color: transparent;
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
<asp:HiddenField ID="hdReferenceChartDisplayType" runat="server" />
<asp:HiddenField ID="hdIsVerification" runat="server" />
<asp:HiddenField ID="hdLicenseType" runat="server" />
<asp:HiddenField ID="hdOptimizeRunId" runat="server" />
<asp:HiddenField ID="hdProjectId" runat="server" />
<asp:HiddenField ID="hdReferenceChartTypeId" runat="server" />
<asp:HiddenField ID="hdSerialNumber" runat="server" Value="0" />
<asp:HiddenField ID="hdHasMeasurement" runat="server" Value="0" />
<asp:HiddenField ID="hdMeasurementLimit" runat="server" Value="0" />
<asp:HiddenField ID="hdDisplayImport" runat="server" Value="no" />
<asp:HiddenField ID="hdDeviceType" runat="server"/>
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
    $(document).ready(function(){
        var SessionTimeOut = <%= Session.Timeout %>;
        setInterval(function(){ 
            jQuery.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Services/PluginDataService.svc/KeepUserSessionAlive/",
                dataType: "JSON",             
                success: function (response) { },
                error: function (XMLHttpRequest, textStatus, errorThrown) { }
            });
         },(SessionTimeOut * 60 * 1000) - (1 * 60 * 1000));
    });
    function ControlVisibility() {
        jQuery("[id*='btnCalibrate']").css('display', 'none');
        jQuery("[id*='hdMeasureAndUploadChart']").val('measurechart');
        jQuery("[id*='btnMeasureAndUploadChart']").val('<%= GetResourceString("measureChart") %>');
        jQuery("[id*='btnMeasureAndUploadChart']").css('display', 'none');
        jQuery("[id*='btnCancelMeasurement']").css('display', 'none');
        jQuery("[id*='btnRewindMeasurement']").css('display', 'none');
      
        if(load('<%= ApplicationConfiguration.Instance.FujiFilmPluginVersion %>'))   //first you have to load the plugin
        {        
            //Proceed further
            ListDevices();
            GetRefChartData();    
            
            //---If DeviceType is i1-Pro2, i1-IO-Pro2, LFP, eXact, i1-Sis2 and plugin is older than 1.9.6 version--> force them to download newer plugin---//
            if(plugin().plugin_version != '1.9.6' &&  (jQuery("[id*='hdDeviceType']").val() == '4' || jQuery("[id*='hdDeviceType']").val() == '5' || jQuery("[id*='hdDeviceType']").val() == '6' || jQuery("[id*='hdDeviceType']").val() == '7' || jQuery("[id*='hdDeviceType']").val() == '8'))
            {
                var oWindow;
                oWindow = window.radopen("PopupDownloadSoftware.aspx", "DownloadSoftwareWindow");
                oWindow.show();
            } 
        }
        else
        {
            var oWindow;
            oWindow = window.radopen("PopupDownloadSoftware.aspx", "DownloadSoftwareWindow");
            oWindow.show();
        }        
    }
    
    function GetRefChartData() {
        jQuery("[id*='hdMeasureAndUploadChart']").val('measurechart');
        jQuery("[id*='btnMeasureAndUploadChart']").val('<%= GetResourceString("measureChart") %>');
        var ReferenceChartTypeId = '';
        var SerialNo;
        GetReferenceChart(jQuery("[id*='hdProjectId']").val(), jQuery("input[id$=hdReferenceChartDisplayType]").val());
    }
    jQuery(document).ready(function () {   
        
        ControlDisability(1,false,1,false,1,true,1,true);
   
        jQuery("[id*='btnCalibrate']").click(function () {    
       
            ControlDisability(1,true,0,false,0,false,0,false);              
            calibrateDevice(g_ScannerList);     
            return false;
        });
        jQuery("[id*='btnMeasureAndUploadChart']").click(function () {
            if (jQuery("[id*='hdMeasureAndUploadChart']").val() == 'measurechart') 
            {              
                ControlDisability(1,true,1,true,1,false,1,true);
                var MeasurementName = jQuery("[id*='txtMeasurementName']").val();
                if (jQuery.trim(MeasurementName).length < 4) 
                {
                    MeasurementDialog('<%= GetResourceString("measurementNameLengthMsg") %>', '<%= GetResourceString("errorText") %>');
                    ControlDisability(0,false,1,false,1,true,1,true);
                    return false;
                }
                if(('<%= (FujifilmUSA.Sync.Common.SessionManager.Instance.CurrentDlpSubOperation != FujifilmUSA.Sync.Entity.ELicenseType.Validation).ToString().ToLower() %>' == 'true') && (jQuery("input[id*='hdMeasurementLimit']").val() >= <%= (FujifilmUSA.Sync.Common.SessionManager.Instance.CurrentProject.LicenseType == FujifilmUSA.Sync.Entity.ELicenseType.Sync1D ? ApplicationConfiguration.Instance.MeasurementLimitSync1D : ApplicationConfiguration.Instance.MeasurementsLimit) %>))
                {
                    MeasurementDialog('<%= GetResourceString("runLimitReached") %>', '<%= GetResourceString("limitReachText") %>');
                    ControlDisability(1,false,1,false,1,true,1,true);
                    return false;
                }            
                scan(g_ScannerList, g_Chart["ReadCS"], g_Chart);
            }
            else if (jQuery("[id*='hdMeasureAndUploadChart']").val() == 'uploadmeasurement') {
                upload();
            }
        });
        jQuery("[id*='btnCancelMeasurement']").click(function () {  
                          
            jQuery("[id*='lblMessage']").html("Status : Cancelling...");
            OnCancel();       
            ControlDisability(1,false,1,false,1,true,1,true);
        });
        jQuery("[id*='btnRewindMeasurement']").click(function () {  
                          
            jQuery("[id*='lblMessage']").html("Status : Rewinding...");
            OnRewind();       
            ControlDisability(1,false,1,true,1,false,1,false);
        });
        bindMeasurementData();
        ControlVisibility();
    });
    
    function MeasurementDialog(message, type) {
        jQuery("[id*='MeasurementConfirmDialog']").modal({
            closeHTML: "<a href='#' title='Close' class='modal-close'>x</a>",
            position: ["20%", ],
            overlayId: 'MeasurementConfirmDialog-overlay',
            containerId: 'MeasurementConfirmDialog-container',
            onShow: function (dialog) {
                var modal = this;
                jQuery('.message', dialog.data[0]).append(message);
                jQuery('.header', dialog.data[0]).append(type);
            }
        });       
    }


    function saveMeasurement() {

        var MeasurementName = jQuery("[id*='txtMeasurementName']").val();
        if (jQuery.trim(MeasurementName).length < 4) {
            MeasurementDialog('<%= GetResourceString("measurementNameLengthMsg") %>', '<%= GetResourceString("errorText") %>');
            return false;
        }
        else
        {
             //Load progress bar
            LoadProgressBrowserWindow();

            var dt = new Date();
            var dt1 = new Date(Date.UTC(dt.getFullYear(), dt.getMonth(), dt.getDate(), dt.getHours(), dt.getMinutes(), dt.getSeconds(), dt.getMilliseconds()));
            var MeasurementDateTime = dt1.toMSJSON();     
            var OptimizeRunId = jQuery("input[id$=hdOptimizeRunId]").val();
            var ProjectId = jQuery("input[id$=hdProjectId]").val();
            var ReferenceChartTypeId = jQuery("input[id$=hdReferenceChartTypeId]").val();
            var MsmtName = jQuery("input[id*='txtMeasurementName']").val();                        
            var saveMeasurementUrl="";
            var PCSDataType = jQuery("input[id*='hdnPCSType']").val();
            var obj;
            if(jQuery("input[id$=hdIsVerification]").val() == "true")
            {
                saveMeasurementUrl="Services/PluginDataService.svc/SaveVMeasurement";
                obj = {
                    MeasurementName: MsmtName,
                    MeasurementDate: MeasurementDateTime,
                    ProjectId: ProjectId,
                    ReferenceChartTypeId: ReferenceChartTypeId,
                    MeasurementPatches: MeasuredPatches,
                    Notes: jQuery('textarea[id*="txtNotes"]').val(),
                    Customer: jQuery('input[id*="txtCustomer"]').val(),
                    PageOrSurface: jQuery('input[id*="txtPageOrSurface"]').val(),
                    JDFJobId: jQuery('input[id*="txtJDFJobId"]').val()
                };
            }
            else if(jQuery("input[id$=hdLicenseType]").val()  == '<%= (int)FujifilmUSA.Sync.Entity.ELicenseType.Sync1D %>')
            {
                saveMeasurementUrl="Services/PluginDataService.svc/Save1DMeasurement";
                obj = {
                    MeasurementName: MsmtName,
                    MeasurementDate: MeasurementDateTime,
                    OptimizeRunId: OptimizeRunId,
                    ReferenceChartTypeId: ReferenceChartTypeId,
                    MeasurementPatches: MeasuredPatches,
                    PCSDataType :PCSDataType
                };
            }
            else
            {
                saveMeasurementUrl="Services/PluginDataService.svc/SaveMeasurement";
                obj = {
                    MeasurementName: MsmtName,
                    MeasurementDate: MeasurementDateTime,
                    OptimizeRunId: OptimizeRunId,
                    ReferenceChartTypeId: ReferenceChartTypeId,
                    MeasurementPatches: MeasuredPatches,
                    PCSDataType :PCSDataType
                };
            }
            
            //Make ajax request...
            jQuery.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: saveMeasurementUrl,
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
                        ControlDisability(1,false,0,false,1,true,1,true);
                    },
                error:
                    function (XMLHttpRequest, textStatus, errorThrown) {
                        var jsonFault = JSON.parse(XMLHttpRequest.responseText);
                        StopProgressBrowserWindow();
                        MeasurementDialog('<%= GetResourceString("errorCause") %>'+ jsonFault.Reason +  " " + '<%= GetResourceString("errorDetail") %>' + jsonFault.DetailedInformation, "Error");
                    }
            });
     
            refreshCanvas();
            jQuery("[id*='btnMeasureAndUploadChart']").val('<%= GetResourceString("measureChart") %>');
            jQuery("[id*='hdMeasureAndUploadChart']").val('measurechart');
        }
    }

    function bindMeasurementData() {
        var OptimizeRunId = jQuery("input[id$=hdOptimizeRunId]").val();
        var ProjectId = jQuery("input[id$=hdProjectId]").val();
        var getMeasurementUrl = "";
        if(jQuery("input[id$=hdIsVerification]").val() == "true")
        {
            getMeasurementUrl = "Services/PluginDataService.svc/GetVMeasurementsByProjectId/" + ProjectId;
        }
        else if(jQuery("input[id$=hdLicenseType]").val()  == '<%= (int)FujifilmUSA.Sync.Entity.ELicenseType.Sync1D %>')
        {
            getMeasurementUrl = "Services/PluginDataService.svc/Get1DMeasurementsByOptimizeId/" + OptimizeRunId;
        }
        else
        {
            getMeasurementUrl = "Services/PluginDataService.svc/GetMeasurementsByOptimizeId/" + OptimizeRunId;
        }
        jQuery.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: getMeasurementUrl,
            processData: false,
            dataType: "json",
            cache: false,
            dataFilter:
                function (data, type) {               
                    var json = jQuery.parseJSON(data);
                
                    if (json.length > 0) {
                        jQuery("input[id*='hdHasMeasurement']").val('true');
                        $("#measurementContainer").html("");
                        $("#measurementContainer").append("<table id ='lvMeasurement1'  cellspacing='0' cellpadding='0' border='0' class='recent_upload'><tbody></tbody></table>");
                        $("#MeasurementHeaderTemplate").tmpl().appendTo("#lvMeasurement1");
                        $("#MeasurementDetailTemplate").tmpl(json).appendTo("#lvMeasurement1");
                        jQuery("input[id*='hdMeasurementLimit']").val(json.length);  //----------Set MeasurementLimit in hiddeninput-------------//      
                        //----------------Display Import Button--------------//  
                        if(jQuery("input[id*='hdDisplayImport']").val() == "yes")
                        {
                            if(json.length < <%= (FujifilmUSA.Sync.Common.SessionManager.Instance.CurrentProject.LicenseType == FujifilmUSA.Sync.Entity.ELicenseType.Sync1D ? ApplicationConfiguration.Instance.MeasurementLimitSync1D : ApplicationConfiguration.Instance.MeasurementsLimit)%>)
                            {                               
                                $("#Div_ImportMeasurements").css('visibility', 'visible');
                            }    
                            else
                            {                               
                                $("#Div_ImportMeasurements").css('visibility', 'hidden');
                            }
                        } 
                        else
                        {
                             $("#Div_ImportMeasurements").css('visibility', 'hidden');
                        }                  
                    }
                    else {
                        jQuery("input[id*='hdHasMeasurement']").val('false');
                        $("#measurementContainer").html("");
                        $("#measurementContainer").append("<div class='no_data_found'>" + '<%= GetResourceString("noRecordsAvailable") %>' + "</div>");
                        //----------------Display Import Button--------------//
                        if(jQuery("input[id*='hdDisplayImport']").val() == "yes")
                        {
                             $("#Div_ImportMeasurements").css('visibility', 'visible');
                        }
                        else
                        {
                             $("#Div_ImportMeasurements").css('visibility', 'hidden');
                        } 
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
        var deleteMeasurementUrl = "";
        if(jQuery("input[id$=hdIsVerification]").val() == "true")
        {
            deleteMeasurementUrl = "Services/PluginDataService.svc/DeleteVMeasurement/" + measurementId;
        }
        else if(jQuery("input[id$=hdLicenseType]").val()  == '<%= (int)FujifilmUSA.Sync.Entity.ELicenseType.Sync1D %>')
        {
            deleteMeasurementUrl = "Services/PluginDataService.svc/Delete1DMeasurement/" + measurementId;
        }
        else
        {
            deleteMeasurementUrl = "Services/PluginDataService.svc/DeleteMeasurement/" + measurementId;
        }
        jQuery.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: deleteMeasurementUrl,
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


    function ControlDisability(btnCalibrate, CalibrateFlag, btnMeasure, MeasureFlag, btnCancel, CancelFlag, btnRewind, RewindFlag) {
        if(btnCalibrate == 1)
        {
            if(CalibrateFlag == true)
            {
                jQuery("[id*='btnCalibrate']").attr('disabled', '');
            }
            else
            {
                jQuery("[id*='btnCalibrate']").removeAttr('disabled', '');
            }
        }
        if(btnMeasure == 1)
        {
            if(MeasureFlag == true)
            {
                jQuery("[id*='btnMeasureAndUploadChart']").attr('disabled', '');
            }
            else
            {
                jQuery("[id*='btnMeasureAndUploadChart']").removeAttr('disabled', '');
            }
        }
        if(btnCancel == 1)
        {
            if(CancelFlag == true)
            {
                jQuery("[id*='btnCancelMeasurement']").attr('disabled', '');
            }
            else
            {
                jQuery("[id*='btnCancelMeasurement']").removeAttr('disabled', '');
            }
        }
        if(btnRewind == 1)
        {
            if(RewindFlag == true)
            {
                jQuery("[id*='btnRewindMeasurement']").attr('disabled', '');                
            }
            else
            {
                jQuery("[id*='btnRewindMeasurement']").removeAttr('disabled', '');            
            }
        }
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

     function ShowImportWindow() {      
        var oWnd = $find("<%= ImportMeasurementWindow.ClientID %>");
        oWnd.show();
        return false;
    }

      function Close() {
        var oWnd = $find("<%= ImportMeasurementWindow.ClientID %>");
        oWnd.close();
        return false;
    }
</script>
<div id="divBackgroundProgressMaster" class="UpdateProgressBackground" style="display: none;
    position: fixed; top: 0px; left: 0px; height: 100%; width: 100%;">
</div>

<telerik:RadWindowManager ID="RadWindowManager" runat="server" Modal="True">
    <Windows>
        <telerik:RadWindow ID="ImportMeasurementWindow" runat="server" VisibleStatusbar="false"
            ShowContentDuringLoad="false" Title="Import Measurements" Width="730" Height="400"
            Behaviors="Close, Move" Behavior="Close, Move" NavigateUrl="~/PopupImportMeasurements.aspx">
        </telerik:RadWindow>
    </Windows>
</telerik:RadWindowManager>
