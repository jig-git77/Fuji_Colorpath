function getStatus() {

    //Make ajax request...
    jQuery.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PluginService.svc/GetStatus",
        processData: false,
        dataType: "json",
        //If the call succeeds
        success:
        function (response) {
            //alert(

            jQuery.growlUI('Success', 'Web service ping was successful. The service status is: ' + response.GetStatusResult, 3000);
        },
        //If the call fails
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(XMLHttpRequest.responseText);
            jQuery.growlUI('Error', XMLHttpRequest.responseText, 20000);
        }
    });
}




//Date Serialize Issue: http://stackoverflow.com/questions/206384/how-to-format-json-date
Date.prototype.toMSJSON = function () {

    var date = '/Date(' + this.getTime() + ')/'; //the number of milliseconds since midnight of January 1, 1970 and the specified date.
    return date;
};



/*
NOTE: ****The webservice should be in the same domain as where the javascript file came from***
The browser does not allow XMLHTTPRequest (AJAX) calls across domains in its default configuration.

Client side: 
You have to serialize your client javascript object into JSON, 
I personally use stringify() method of the JSON2 library: http://www.json.org/js.html written by Douglas Crockford
Firefox 3.5 or later and IE > 8 have native implementation of stringify()  and json2.js checks weather native implementation is avaialble
if avaiable use that to stringify()

You have to translate the serialized object into something "eatable" by your c# code.
Here you can use deserialize() method of Microsoft's JavaScriptSerializer class (but it might have some issues in .net 3.5 if you don't have SP installed), 
or otherwise JSON.net library http://james.newtonking.com/pages/json-net.aspx


server-side method signature should be: 

fSaveToDB(Object myObj) where "myObj" is the name of your client-side object container: which should be like {myObj: your object...} 



*/

/*
//Reference :   //http://stackoverflow.com/questions/2062053/passing-complex-objects-into-a-wcf-rest-service
//Json Validator: http://jsonlint.com/
function saveMeasurement() {



    /****passing date and time from JavaScript to WCF serialization issue  http://stackoverflow.com/questions/4474352/send-jquery-json-to-wcf-rest-using-date ***/
/*    var dt = new Date();
    var dt1 = new Date(Date.UTC(dt.getFullYear(), dt.getMonth(), dt.getDate(), dt.getHours(), dt.getMinutes(), dt.getSeconds(), dt.getMilliseconds()));
    var MeasurementDateTime = dt1.toMSJSON();

    var PressRunID = jQuery("input[id$=hiddenPressRunID]").val();  //document.getElementById('hiddenPressRunID').value;
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
            // convert from "\/Date(nnnn)\/" to "new Date(nnnn)"
            return data.replace(/"\\\/(Date\([0-9-]+\))\\\/"/gi, 'new $1');
        },
        //If the call succeeds
        success:
        function (response) {   //the response param passed to the success callback contains the returned json
            //alert('measurement added successfully');
            jQuery.growlUI('Response From Server', 'measurement added successfully', 1000);
        },
        //If the call fails
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            //alert(XMLHttpRequest.responseText);
            var jsonFault = JSON.parse(XMLHttpRequest.responseText);
            jQuery.growlUI('Error', "Error Cause:" + jsonFault.Reason + " Error Detail:" + jsonFault.DetailedInformation, 20000);
        }
    });

    /**clear the array **/
   /* if (MeasuredPatches.length > 0) MeasuredPatches.length = 0;
    ListDevices();

    jQuery('#btnUploadData').css('display', 'none');

}

*/

function GetReferenceChart(ProjectID) {    
    //Make ajax request...
    jQuery.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PluginService.svc/GetReferenceChart",
        data: JSON.stringify({ ProjectID: ProjectID }),
        processData: false,
        dataType: "json", //this only applies to response from the server and (not request send to server)
        //If the call succeeds
        success:
        function (response) {
            //ReferenceChart object returned as JSON from WCF
            var objChart = response.GetReferenceChartResult;
            SetChartResult(objChart);

            // Added By Softweb //                        
           /* var RowCount = 0;
            jQuery(g_Chart["RefBase"]).each(function (index, value) {

                var rgbx = value["HTMLColor"];
                var tooltip = value["SampleName"];
                divmain = jQuery("#DivMain")

                copy = jQuery("#original_rectangle").clone();
                copy.removeAttr("style");

                copy.css('background-color', rgbx);
                copy.css('float', 'left');
                if (RowCount == g_Chart["NumberOfColumns"]) {
                    RowCount = 1;
                    copy.css('clear', 'both');
                }
                else {
                    RowCount++;                    
                }
                copy.removeAttr("id").html("&nbsp;").appendTo(divmain)
                copy.wTooltip({ content: '&nbsp;' + tooltip + '&nbsp;', className: "tooltipclass", style: false });
            });  */
            //-----------------------//
        },
        //If the call fails
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            //alert(XMLHttpRequest.responseText);
            var jsonFault = JSON.parse(XMLHttpRequest.responseText);
            jQuery.growlUI('Error', "Error Cause:" + jsonFault.Reason + "</ br> Error Detail:" + jsonFault.DetailedInformation, 20000);
        }
    });
}

//this method initialize global chart object called g_chart which in turns being pass to the plug-in
function SetChartResult(objChart) {
    
    //var g_paper = new Array(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
    g_Chart["DeviceSettings"] = new Object();
    g_Chart["RowNames"] = new Array();
    g_Chart["Size"] = objChart.TotalPatch;
    g_Chart["Name"] = objChart.ChartName;
    g_Chart["NumberOfRows"] = objChart.PatchRows;
    g_Chart["NumberOfColumns"] = objChart.NumOfColumns;
    g_Chart["ColorBase"] = objChart.MeasureDeviceSrNo;
    g_Chart["ReadCS"] = "Density,CIELab,CIEXYZ,sRGB,Spectral";
    
    //g_Chart["RowNames"] = ["A", "B", "C", "D", "E", "F"];      // objChart.RowNames;
    for (m = 1; m <= g_Chart["NumberOfRows"]; m++) {

        g_Chart["RowNames"].push("row " + m);
    }

    

    if (objChart.DeviceSetting != null && objChart.DeviceSetting != "undefined") {
        g_Chart["DeviceSettings"]["Colorimetric.Illumination"] = objChart.DeviceSetting.Illuminant;
        g_Chart["DeviceSettings"]["Colorimetric.DensityStandard"] = objChart.DeviceSetting.DensityStandard;
        g_Chart["DeviceSettings"]["Colorimetric.Observer"] = objChart.DeviceSetting.Observer;
        g_Chart["Mode"] = objChart.DeviceSetting.Mode;
    }
    
    
    

    //g_Chart["CurrentStrip"] =   g_Chart["RowNames"][0];
    //g_chart["Substrate"] = g_paper;
    if (objChart.InstrumentTypeID > 2 && objChart.ChartSettings != null) {
        g_Chart["ChartType"] = "Native";
        g_Chart["ChartSizeX"] = objChart.ChartSettings["ChartSizeX"];
        g_Chart["ChartSizeY"] = objChart.ChartSettings["ChartSizeY"];
        g_Chart["BarSizeX"] = objChart.ChartSettings["BarSizeX"];
        g_Chart["BarSizeY"] = objChart.ChartSettings["BarSizeY"];
        g_Chart["DistToBar"] = objChart.ChartSettings["DistToBar"];
        g_Chart["DistToFirstLineY"] = objChart.ChartSettings["DistToFirstLineY"];
        g_Chart["MarkerDistance"] = objChart.ChartSettings["MarkerDistance"];
        g_Chart["DistToFirstField"] = objChart.ChartSettings["DistToFirstField"];
        g_Chart["PatchSizeX"] = objChart.ChartSettings["PatchSizeX"];
        g_Chart["PatchSizeY"] = objChart.ChartSettings["PatchSizeY"];


    }

    g_Chart["RefBase"] = new Array();

    
    for (i = 0; i < g_Chart["Size"]; i++) {
        //g_chart["LAB"][i] = new LAB(objChart.ReferencePatch[i].TargetLAB.L, objChart.ReferencePatch[i].TargetLAB.A, objChart.ReferencePatch[i].TargetLAB.B);
        var patch = new Object();
        patch["SampleName"] = objChart.ReferencePatch[i].SampleName;
        patch["HTMLColor"] = objChart.ReferencePatch[i].HTMLColor;
        patch["CMYK_C"] = objChart.ReferencePatch[i].CMYK_C;
        patch["CMYK_M"] = objChart.ReferencePatch[i].CMYK_M;
        patch["CMYK_Y"] = objChart.ReferencePatch[i].CMYK_Y;
        patch["CMYK_K"] = objChart.ReferencePatch[i].CMYK_K;
        patch["PatchTypeID"] = objChart.ReferencePatch[i].PatchTypeID;
        g_Chart["RefBase"].push(patch);
    }    

    //clear the Measured patch array
    if (MeasuredPatches.length > 0) MeasuredPatches.length = 0;

    alert(g_Chart["ColorBase"]);

    
   //ListDevices();
    
    //jQuery('#ListDevices').css('visibility', 'visible');
}     