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
        function(response) {
            //alert(
                 
                 jQuery.growlUI('Success', 'Web service ping was successful. The service status is: ' + response.GetStatusResult, 10000);
                },
        //If the call fails
        error: function(XMLHttpRequest, textStatus, errorThrown) {
        alert(XMLHttpRequest.responseText);
        jQuery.growlUI('Error', XMLHttpRequest.responseText,10000);
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

//Reference :   //http://stackoverflow.com/questions/2062053/passing-complex-objects-into-a-wcf-rest-service
//Json Validator: http://jsonlint.com/
function saveMeasurement() {



    /****passing date and time from JavaScript to WCF serialization issue  http://stackoverflow.com/questions/4474352/send-jquery-json-to-wcf-rest-using-date ***/
    var dt = new Date();
    var dt1 = new Date(Date.UTC(dt.getFullYear(), dt.getMonth(), dt.getDate(), dt.getHours(), dt.getMinutes(), dt.getSeconds(), dt.getMilliseconds()));   
    var MeasurementDateTime= dt1.toMSJSON();

    var obj = { objMeasurement: { MeasurementName: "Joel Test2", MeasurementDate: MeasurementDateTime, MeasuredPatches: MeasuredPatches} };

    //Make ajax request...
    jQuery.ajax({
        type: "POST", //for complex object POST is preferred
        contentType: "application/json; charset=utf-8",   //this is very important application/json; charset=utf-8";  else it will fail
        url: "WebServices/PluginService.svc/SaveMeasurement",
        processData: false, //     I found that if I use processData=true, then the resulting string sent to the service is in querystring format, not in JSON. Not what I want for passing complex objects. So I set it to false. Using true would be fine for simpler non-JSON requests where you're doing WebGet, and all the params are in the query string
        dataType: "json",  ////this only applies to response from the server and (not request send to server)
        data:  JSON.stringify(obj),
        dataFilter  : function (data, type) {
        // convert from "\/Date(nnnn)\/" to "new Date(nnnn)"
        return data.replace(/"\\\/(Date\([0-9-]+\))\\\/"/gi, 'new $1');
        }, 
        //If the call succeeds
        success:
        function(response) {   //the response param passed to the success callback contains the returned json
        //alert('measurement added successfully');
        jQuery.growlUI('Response From Server', 'measurement added successfully', 15000);
        },
        //If the call fails
        error: function (XMLHttpRequest, textStatus, errorThrown) {
        //alert(XMLHttpRequest.responseText);
        var jsonFault = JSON.parse(XMLHttpRequest.responseText);
        jQuery.growlUI('Error', "Error Cause:" + jsonFault.Reason + " Error Detail:" + jsonFault.DetailedInformation, 20000);
    }
    });
}



function GetReferenceChart(ChartTypeID) {
    //Make ajax request...
    jQuery.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PluginService.svc/GetReferenceChart",
        data: JSON.stringify({ ChartTypeID: ChartTypeID }),
        processData: false,
        dataType: "json", //this only applies to response from the server and (not request send to server)
        //If the call succeeds
        success:
        function (response) {
         //ReferenceChart object returned as JSON from WCF
         var objChart = response.GetReferenceChartResult;
         SetChartResult(objChart);   
        },
        //If the call fails
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            //alert(XMLHttpRequest.responseText);
            var jsonFault = JSON.parse(XMLHttpRequest.responseText);
            jQuery.growlUI('Error', "Error Cause:" + jsonFault.Reason + "</ br> Error Detail:" + jsonFault.DetailedInformation, 8000);
        }
    });
}

//this method initialize global chart object called g_chart which in turns being pass to the plug-in
function SetChartResult(objChart) {
    var g_paper = new Array(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

    g_chart["Size"] = objChart.TotalPatch;
    g_chart["RowNames"] = new Array();
    g_chart["Name"] = objChart.ChartName;
    g_chart["Height"] = objChart.PatchRows;
    g_chart["Width"] = objChart.NumOfColumns;
    g_chart["Substrate"] = g_paper;
    g_chart["ReadCS"] = "Density,CIELab,CIEXYZ,sRGB,Spectral";
    g_chart["ColNames"] = ["A"];
    g_chart["NumberOfRows"] = objChart.PatchRows;
    g_chart["NumberOfColumns"] = objChart.NumOfColumns;
//    if (objChart.ChartTypeID == 1) {
//        g_chart["ColNames"] = ["A"];
//    }
//    else if (objChart.ChartTypeID == 2) {
//        g_chart["ColNames"] = ["A", "B"];
//    }
//    else if (objChart.ChartTypeID == 5) {
//        g_chart["ColNames"] = ["A", "B"];
    //    }

    for (m = 1; m <= g_chart["NumberOfRows"]; m++) {

        g_chart["RowNames"].push("row " + m);
    }


    g_chart["CurrentStrip"] = "A";
//    g_chart["LAB"] = new Array();
//    for (i=0; i < g_chart["Size"]; i++)
//    {
//        g_chart["LAB"][i] = new LAB(objChart.ReferencePatch[i].TargetLAB.L, objChart.ReferencePatch[i].TargetLAB.A, objChart.ReferencePatch[i].TargetLAB.B);


//    }


    ListDevices();
    jQuery('#ListDevices').css('visibility', 'visible');

}     