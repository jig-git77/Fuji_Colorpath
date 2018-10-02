<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SyncMeasurement.aspx.cs"
    Inherits="SyncMeasurement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="rad" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS/Default.css" rel="stylesheet" type="text/css" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        if (typeof jQuery == 'undefined') {
            document.write(unescape("%3Cscript src='/JQuery/jquery-1.6.2.min.js' type='text/javascript'%3E%3C/script%3E"));
        }
    </script>
    <script src="JS/wz_jsgraphics.js" type="text/javascript"></script>
    <script src="jQuery/json2.js" type="text/javascript"></script>
    <script src="JQuery/jquery.simplemodal.js" type="text/javascript"></script>
    <script src="jQuery/jquery-blockUI.js" type="text/javascript"></script>
    <script src="jQuery/wtooltip.min.js" type="text/javascript"></script>
    <script src="JS/excanvas.js" type="text/javascript"></script>
    <script src="JS/chartread.js" type="text/javascript"></script>
    <script src="JS/i1read.js" type="text/javascript"></script>
    <script src="JS/isisread.js" type="text/javascript"></script>
    <script src="JS/WebComm.js" type="text/javascript"></script>
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

    <script language="javascript" type="text/javascript">
        function playSound(soundfile) {
            document.getElementById("dummy").innerHTML =
            "<embed src=\"" + soundfile + "\" hidden=\"true\" autostart=\"true\" loop=\"false\" />";
        }
 </script>

</head>
<body onload="ControlVisiblity()">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                    <tr>
                        <td>
                            <iframe id="modalIframe" style="display: none;" src="javascript:false;" frameborder="0"
                                scrolling="no"></iframe>
                            <div id="modalWindow">
                                Eh?</div>
                            <div id="modalBackground">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="green-title" style="padding-left: 5px;">
                            Measurement Information &nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 5px">
                            <table border="0" width="100%" cellpadding="0" cellspacing="2">
                                <tr>
                                    <td class="FORM-TEXT-BLACK" style="width: 20%; padding-left: 0px;">
                                        Measurement Name :
                                    </td>
                                    <td style="width: 22%;" align="left">
                                        <asp:TextBox ID="TxtMeasurementName" runat="server" Width="150px" MaxLength="90"
                                            CssClass="TEXTBOX"></asp:TextBox>
                                    </td>
                                    <td style="width: 23%;" align="left">
                                        <input id="btnCalibrate" type="button" value="Calibrate" class="BUTTON" onclick="javascript:calibrateDevice(g_ScannerList[0])"
                                            style="display: none" />
                                    </td>                                     
                                    <td style="width: 16%;" align="right">
                                        <input id="btnMeasureAndUploadChart" type="button" value="Measure Chart" class="BUTTON"
                                             style="display: none" />
                                    </td>
                                    <td style="width: 8%;" align="right">
                                        <input id="btnUploadData" type="button" value="Upload Data" class="BUTTON" style="display: none" />
                                        <input id="btnCancelMeasurement" type="button" value=" Cancel " class="BUTTON"  style="display: none" />
                                    </td>
                                    <td style="width: 8%;" align="right">
                                        <input id="btnRefresh" type="button" value="Refresh" class="BUTTON" />
                                    </td>
                                   <%-- <td style="width: 4%; padding-right: 2px;">
                                        <div id="DivIngLogo" style="width: 20px">
                                            <img id="ImgLoad" src="Images/Red-Lamp.gif"></img>
                                        </div>
                                    </td>--%>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 2px;">
                            <table id="tblMessage" runat="server" width="100%" style="border: 1px solid #0C3D30;
                                background-color: #6BB19C; color: Black; height:45px;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="padding: 2px 2px 2px 5px; font-weight: bold;" valign="top">
                                        <asp:Label ID="lblMessage" runat="server" Text="Status : Scanning device is not loaded."></asp:Label>
                                    </td>
                                    <td style="padding: 2px 5px 2px 2px; font-weight: bold;" align="right" valign="top">
                                        <asp:Label ID="lblPluginMessage" runat="server" Text="loading..."></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 2px;">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="green-title" style="padding-left: 5px;">
                                        Colorbar &nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div id="Canvas" style="position: relative; height: 350px; width: 630px; overflow: visible;
                                            top: 50px; left: 100px;">
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnBindGrid" runat="server" Text="Bind" Style="display: none;" OnClick="btnBindGrid_Click" />
                                    <table style="width: 98%; border: 1PX SOLID #C1C1C1" cellspacing="0" cellpadding="0"
                                        border="0">
                                        <tr>
                                            <td style="width: 50%; border-right: 1PX SOLID #C1C1C1;" class="BGRIDTITLE" align="center">
                                                Measurement Name
                                            </td>
                                            <td style="width: 40%; border-right: 1PX SOLID #C1C1C1;" class="BGRIDTITLE" align="center">
                                                Upload Date Time
                                            </td>
                                            <td style="width: 10%; border-right: 1PX SOLID #C1C1C1;" class="BGRIDTITLE" align="center">
                                                Delete
                                            </td>
                                        </tr>
                                    </table>
                                    <rad:RadGrid ID="RG_SyncMeasurement" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                        AllowSorting="true" AllowFilteringByColumn="false" Width="100%" Skin="Green"
                                        OnItemCommand="RG_SyncMeasurement_ItemCommand">
                                        <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                        <ClientSettings>
                                            <Scrolling AllowScroll="true" UseStaticHeaders="true" ScrollHeight="80" />
                                            <Scrolling AllowScroll="true" UseStaticHeaders="true" ScrollHeight="80" />
                                            <Scrolling AllowScroll="true" UseStaticHeaders="true" ScrollHeight="80" />
                                            <Scrolling AllowScroll="true" UseStaticHeaders="true" ScrollHeight="80" />
                                            <Scrolling AllowScroll="true" UseStaticHeaders="true" ScrollHeight="80" />
                                            <Scrolling AllowScroll="true" UseStaticHeaders="true" ScrollHeight="80" />
                                            <Scrolling AllowScroll="true" UseStaticHeaders="true" ScrollHeight="80" />
                                            <Scrolling AllowScroll="true" UseStaticHeaders="true" ScrollHeight="80" />
                                        </ClientSettings>
                                        <PagerStyle Position="Top" />
                                        <MasterTableView>
                                            <Columns>
                                                <rad:GridBoundColumn DataField="MeasurementID" HeaderText="MeasurementID" Visible="false">
                                                </rad:GridBoundColumn>
                                                <rad:GridTemplateColumn DataField="MeasurementName" HeaderText="MeasurementName"
                                                    ItemStyle-Width="50%" HeaderStyle-Width="50%" AllowFiltering="false" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lbl_Measurement" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MeasurementName").ToString()%>'></asp:Label>
                                                    </ItemTemplate>
                                                </rad:GridTemplateColumn>
                                                <rad:GridTemplateColumn DataField="UploadDateTime" DataType="System.DateTime" UniqueName="UploadDateTime"
                                                    AllowFiltering="false" Visible="true" HeaderText="Upload Date Time" ItemStyle-Width="40%"
                                                    HeaderStyle-Width="40%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "UploadDateTime").ToString().Replace(":000", " ") %>
                                                    </ItemTemplate>
                                                </rad:GridTemplateColumn>
                                                <rad:GridTemplateColumn HeaderText="Delete" ItemStyle-Width="10%" HeaderStyle-Width="10%"
                                                    AllowFiltering="false" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="IMGBTN_Delete" runat="server" OnClientClick="return confirm('Are you sure want to delete ?');"
                                                            CommandName="Delete" ImageUrl="~/Images/Delete-Icon.gif" ToolTip="Delete" />
                                                    </ItemTemplate>
                                                </rad:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </rad:RadGrid>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdateProgress runat="server" DynamicLayout="true" ID="UP_Home1" AssociatedUpdatePanelID="UpdatePanel1"
                                DisplayAfter="0">
                                <ProgressTemplate>
                                    <div id="DIV_Progress1" runat="server" class="PROGRESS" align="center">
                                        <img src="Images/Loading2.gif" style="background: transparent;" alt="Loading" /><br />
                                        <b>Loading</b></div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <p>
        <asp:HiddenField ID="hiddenPressRunID" runat="server" />
        <asp:HiddenField ID="hiddenProjectTypeID" runat="server" />
        <asp:HiddenField ID="hdProjectID" runat="server" />
        <asp:HiddenField ID="hdChartTypeID" runat="server" />
    </p>
    <!-- modal content -->
    <div id='confirm'>
        <div class='header'>
            <span></span>
        </div>
        <div class='message'>
        </div>        
        <div class='buttons'>            
            <div class='no simplemodal-close'>                
                <input type="button" value="OK" id="btnCloseModelPopup" style="border:none; background-color:transparent; padding-top:4px" /></div>
        </div>
    </div>
    </form>
    <object id="plugin0" type="application/x-fujicmplugin" width="0" height="0">
    </object>

    <span id="dummy"></span>

</body>
<script language="javascript" type="text/javascript">
    var refreshTry = 0;
    function ControlVisiblity() {
        refreshCanvas();
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
              
                document.getElementById('<%= btnBindGrid.ClientID %>').click();

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
       // OnCancel();
        //if (MeasuredPatches.length > 0) MeasuredPatches.length = 0;
        // ListDevices();
       
        refreshCanvas();
        //jQuery('#btnUploadData').css('display', 'none');
        jQuery('#btnMeasureAndUploadChart').val('Measure Chart');
    }
</script>
</html>
