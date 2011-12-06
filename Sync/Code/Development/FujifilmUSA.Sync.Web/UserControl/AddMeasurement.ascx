<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddMeasurement.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.AddMeasurement" %>
<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js" type="text/javascript"></script>
<script type="text/javascript">
    if (typeof jQuery == 'undefined') {
        document.write(unescape("%3Cscript src='/JQuery/jquery-1.6.2.min.js' type='text/javascript'%3E%3C/script%3E"));
    }
</script>
<script src="../JS/wz_jsgraphics.js" type="text/javascript"></script>
<script src="../jQuery/json2.js" type="text/javascript"></script>
<script src="../JQuery/jquery.simplemodal.js" type="text/javascript"></script>
<script src="../jQuery/jquery-blockUI.js" type="text/javascript"></script>
<script src="../jQuery/wtooltip.min.js" type="text/javascript"></script>
<script src="../JS/excanvas.js" type="text/javascript"></script>
<script src="../JS/chartread.js" type="text/javascript"></script>
<script src="../JS/i1read.js" type="text/javascript"></script>
<script src="../JS/isisread.js" type="text/javascript"></script>
<script src="../JS/WebComm.js" type="text/javascript"></script>
<div class="contant_top">
    &nbsp;</div>
<div class="contant_middle">
    <div class="head">
        <h1>
            Add Measurement</h1>
        <div class="Information_divAnalyze">
            <telerik:RadButton ID="btnAnalyze" Text="Analyze" runat="server">
            </telerik:RadButton>
        </div>
    </div>
    <div class="clear">
    </div>
    <div class="info_form">
        <div class="float_l InformationtxtDivWidth">
            <asp:Label ID="lblMeasurementName" runat="server" Text="Measurement Name" CssClass="LabelTitle"></asp:Label>
            &nbsp;
            <telerik:RadTextBox ID="txtMeasurementName" runat="server" SkinID="width250px">
            </telerik:RadTextBox>
        </div>
        <div class="float_r">
            <telerik:RadButton ID="btnCalibrate" runat="server" Text="Calibrate">
            </telerik:RadButton>
        </div>
        <div class="clear">
        </div>
        <div class="float_l">
            <telerik:RadButton ID="btnMeasureAndUploadChart" runat="server" Text="Measure / Upload">
            </telerik:RadButton>
        </div>
        <div class="float_r">
            <telerik:RadButton ID="btnCancelMeasurement" runat="server" Text="Cancel Measurement">
            </telerik:RadButton>
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="color_pallate">
        <div class="color_pallate_head">
            Colorbar</div>
        <div class="color_pallate_middle">
            <div class="color_bar">
                Color bar comes here
            </div>
        </div>
        <div class="color_pallate_bottom">
            &nbsp;</div>
    </div>
    <div class="color_pallate">
        <div class="color_pallate_head">
            Recent Upload</div>
        <div class="color_pallate_middle">
            <telerik:RadGrid ID="grdRecentUploadList" runat="server" AutoGenerateColumns="false"
                OnNeedDataSource="grdRecentUploadList_NeedDataSource" BorderStyle="None" Width="99.9%">
                <MasterTableView>
                    <Columns>
                        <telerik:GridBoundColumn DataField="MeasurementName" UniqueName="MeasurementName"
                            HeaderText="Measurement Name">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="UploadDateTime" UniqueName="UploadDateTime" HeaderText="Upload Date Time">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Delete">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" ImageUrl="~/Images/delete_icon.png" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </div>
        <div class="color_pallate_bottom">
            &nbsp;</div>
    </div>
    <div class="clear">
    </div>
</div>
<div class="contant_bottom">
    &nbsp;</div>
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
<p>
    <asp:HiddenField ID="hiddenPressRunID" runat="server" />
    <asp:HiddenField ID="hiddenProjectTypeID" runat="server" />
    <asp:HiddenField ID="hdProjectID" runat="server" />
    <asp:HiddenField ID="hdChartTypeID" runat="server" />
</p>
<object id="plugin0" type="application/x-fujicmplugin" width="0" height="0">
</object>
<script language="javascript" type="text/javascript">
    var refreshTry = 0;
    function ControlVisiblity() {
        //refreshCanvas();
        jQuery('#btnCalibrate').css('display', 'none');
        jQuery('#btnMeasureAndUploadChart').val('Measure Chart');
        jQuery('#btnMeasureAndUploadChart').css('display', 'none');
        jQuery('#btnCancelMeasurement').css('display', 'none');
        load();   //first you have to load the plugin
        GetReferenceChart((jQuery('#hdProjectID').val()));
        ListDevices();
    }
    jQuery(document).ready(function () {
        jQuery('#btnMeasureAndUploadChart').click(function () {
            if (jQuery('#btnMeasureAndUploadChart').val() == 'Measure Chart') {
                var MeasurementName = jQuery("#TxtMeasurementName").val();
                if (jQuery.trim(MeasurementName).length < 4) {
                    confirm("Please enter measurement name that is at least three characters long.", "Error");
                    return false;
                }
                scan(g_ScannerList[0], 'Density,CIELab,CIEXYZ,sRGB', g_Chart);
            }
            else if (jQuery('#btnMeasureAndUploadChart').val() == 'Upload Measurement') {
                upload();
                //document.getElementById('<%= btnBindGrid.ClientID %>').click();
            }
        });

        jQuery('#btnCancelMeasurement').click(function () {
            refreshCanvas();
            jQuery('#btnMeasureAndUploadChart').val('Measure Chart');
            OnCancel();
            ListDevices();
        });
        jQuery('#btnRefresh').click(function () {
            refreshTry++;
            ControlVisiblity();
            if (refreshTry == 2) {
                jQuery('#lblMessage').html(jQuery('#lblMessage').text() + '<br>Hint: Having problem with plugins? Try to close and re-open your browser!</br>');
            }
        });
    });

    function confirm(message, type) {
        jQuery('#confirm').modal({
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
        jQuery('#btnCloseModelPopup').focus().click(function () {

        });
    }
    function saveMeasurement() {
        /****passing date and time from JavaScript to WCF serialization issue  http://stackoverflow.com/questions/4474352/send-jquery-json-to-wcf-rest-using-date ***/
        var dt = new Date();
        var dt1 = new Date(Date.UTC(dt.getFullYear(), dt.getMonth(), dt.getDate(), dt.getHours(), dt.getMinutes(), dt.getSeconds(), dt.getMilliseconds()));
        var MeasurementDateTime = dt1.toMSJSON();
        var PressRunID = jQuery("input[id$=hiddenPressRunID]").val();
        var ProjectTypeID = jQuery("input[id$=hiddenProjectTypeID]").val();
        var ChartTypeID = jQuery("input[id$=hdChartTypeID]").val();
        var MsmtName = jQuery("#TxtMeasurementName").val();
        var obj = { objMeasurement: { MeasurementName: MsmtName, MeasurementDate: MeasurementDateTime, PressRunID: PressRunID, ChartTypeID: ChartTypeID, ProjectTypeID: ProjectTypeID, MeasuredPatches: MeasuredPatches} };

        //Make ajax request...
        jQuery.ajax({
            type: "POST", //for complex object POST is preferred
            contentType: "application/json; charset=utf-8",   //this is very important application/json; charset=utf-8";  else it will fail
            url: "Services/PluginService.svc/SaveMeasurement",
            processData: false, //     I found that if I use processData=true, then the resulting string sent to the service is in querystring format, not in JSON. Not what I want for passing complex objects. So I set it to false. Using true would be fine for simpler non-JSON requests where you're doing WebGet, and all the params are in the query string
            dataType: "json",  ////this only applies to response from the server and (not request send to server)
            data: JSON.stringify(obj),
            dataFilter: function (data, type) {
                return data.replace(/"\\\/(Date\([0-9-]+\))\\\/"/gi, 'new $1');
            },
            //If the call succeeds
            success:
        function (response) {   //the response param passed to the success callback contains the returned json
            document.getElementById('<%= btnBindGrid.ClientID %>').click();
            jQuery.growlUI('Response From Server', 'measurement added successfully', 1000);

        },
            //If the call fails
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                //alert(XMLHttpRequest.responseText);
                var jsonFault = JSON.parse(XMLHttpRequest.responseText);
                confirm("Error Cause:" + jsonFault.Reason + " Error Detail:" + jsonFault.DetailedInformation, "Error");
                //jQuery.growlUI('Error', "Error Cause:" + jsonFault.Reason + " Error Detail:" + jsonFault.DetailedInformation, 20000);
            }
        });

        /**clear the array **/
        OnCancel();
        //if (MeasuredPatches.length > 0) MeasuredPatches.length = 0;
        //ListDevices();
        refreshCanvas();
        //jQuery('#btnUploadData').css('display', 'none');
        jQuery('#btnMeasureAndUploadChart').val('Measure Chart');
    }
</script>
