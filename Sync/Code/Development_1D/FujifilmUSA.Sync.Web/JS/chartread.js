// -*- mode: javascript; indent-tabs-mode: nil; c-basic-offset: 4 -*-
// 
// Make sure the console functions exist.  If the in chrome then
// they already exist.  For firefox you need the firebug plugin
// If they do not exist, then they are dummied up and the script will
// still run.
if (!window.console) console = {};
console.log = console.log || function () { };
console.warn = console.warn || function () { };
console.error = console.error || function () { };
console.info = console.info || function () { };

OSName = "Unknown OS";
if (navigator.appVersion.indexOf("Win") != -1) OSName = "Windows";
if (navigator.appVersion.indexOf("Mac") != -1) OSName = "MacOS";
if (navigator.appVersion.indexOf("X11") != -1) OSName = "UNIX";
if (navigator.appVersion.indexOf("Linux") != -1) OSName = "Linux";

/* for Internet Exploere no Array.IndexOf Method */
/*
if (!Array.prototype.indexOf) {
    Array.prototype.indexOf = function (obj, start) {
        for (var i = (start || 0), j = this.length; i < j; i++) {
            if (this[i] === obj) { return i; }
        }
        return -1;
    }
}
*/
/*
if (/msie/i.test(navigator.userAgent)) //only override IE
{
    document.nativeGetElementById = document.getElementById;
    document.getElementById = function (id) {
        var elem = document.nativeGetElementById(id);
        if (elem) {
            //make sure that it is a valid match on id
            if (elem.id == id) {
                return elem;
            }
            else {
                //otherwise find the correct element
                for (var i = 1; i < document.all[id].length; i++) {
                    if (document.all[id][i].id == id) {
                        return document.all[id][i];
                    }
                }
            }
        }
        return null;
    };
}
*/
console.log("Loading chartread.js");
var actKey = '';
var g_ScannerList = new Object();
var g_Chart = new Object();
g_Chart["Result"] = new Array();


var next = 0;
var backup1 = 0;

MeasuredPatches = [];
/*
//constructor for CIELAB object
function LAB(L, A, B) {
    this.L = L;
    this.A = A;
    this.B = B;
}


//constructor for CIEXYZ object
function XYZ(X, Y, Z) {
    this.X = X;
    this.Y = Y;
    this.Z = Z;
}
*/
//constructor for Density object
function Density(Red, Green, Blue, Visual) {
    this.Red = Red;
    this.Green = Green;
    this.Blue = Blue;
    this.Visual = Visual;
}

//constructor for Spectral object
function Spectral(nm380, nm390, nm400, nm410, nm420, nm430, nm440, nm450, nm460, nm470, nm480, nm490, nm500, nm510, nm520, nm530, nm540, nm550, nm560, nm570, nm580, nm590, nm600, nm610, nm620, nm630, nm640, nm650, nm660, nm670, nm680, nm690, nm700, nm710, nm720, nm730) {
    this.nm380 = nm380;
    this.nm390 = nm390;
    this.nm400 = nm400;
    this.nm410 = nm410;
    this.nm420 = nm420;
    this.nm430 = nm430;
    this.nm440 = nm440;
    this.nm450 = nm450;
    this.nm460 = nm460;
    this.nm470 = nm470;
    this.nm480 = nm480;
    this.nm490 = nm490;
    this.nm500 = nm500;
    this.nm510 = nm510;
    this.nm520 = nm520;
    this.nm530 = nm530;
    this.nm540 = nm540;
    this.nm550 = nm550;
    this.nm560 = nm560;
    this.nm570 = nm570;
    this.nm580 = nm580;
    this.nm590 = nm590;
    this.nm600 = nm600;
    this.nm610 = nm610;
    this.nm620 = nm620;
    this.nm630 = nm630;
    this.nm640 = nm640;
    this.nm650 = nm650;
    this.nm660 = nm660;
    this.nm670 = nm670;
    this.nm680 = nm680;
    this.nm690 = nm690;
    this.nm700 = nm700;
    this.nm710 = nm710;
    this.nm720 = nm720;
    this.nm730 = nm730;
}

function find_angle(ul, lr, c) {
    var p0c = Math.sqrt(Math.pow(c.x - ul.x, 2) +
                        Math.pow(c.y - ul.y, 2));
    var p1c = Math.sqrt(Math.pow(c.x - lr.x, 2) +
                        Math.pow(c.y - lr.y, 2));
    var p0p1 = Math.sqrt(Math.pow(lr.x - ul.x, 2) +
                         Math.pow(lr.y - ul.y, 2));
    return Math.acos((p1c * p1c + p0c * p0c - p0p1 * p0p1) / (2 * p1c * p0c)) * 57.29578;
}

//Patch Object
function Patch(LAB, XYZ, Density, sName) {

    this.LAB = LAB;
    this.XYZ = XYZ;
    this.Density = Density;
    this.SampleName = sName;
}

function ClipDecimal(number) {
    var num;
    if (number > 0) {
        num = Math.floor(number * 100000) / 100000;
    }
    else {
        num = 0;
    }
    return num;
}


// This is the spectral values of the paper used for density
// calculations.  Saved in the spot read function
//paper = new Array();

paper = new Array(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

function dec2hex(i) {
    var result = "00";
    if (i >= 0 && i <= 15) { result = "0" + i.toString(16); }
    else if (i >= 16 && i <= 255) { result = i.toString(16); }
    return result;
}
function plugin() {
    return document.getElementById('plugin0');
}

function addEvent(obj, name, func) {
    if (window.addEventListener) {
        obj.addEventListener(name, func, false);
    } else {
        obj.attachEvent("on" + name, func);
    }
}


function load(pluginVersion) {
    jQuery("[id*='lblPluginMessage']").html(locErrorPlugin);
    addEvent(plugin(), 'fire', function (x) {
        alert(locAlertOnFiredLoad + " " + x);
    });
    console.log("Loading plugin version " + plugin().plugin_version);

    if (plugin().plugin_version == undefined) { //---If plugin has some issues or not been installed yet, let give option to install them first
        jQuery("[id*='lblMessage']").css('color', 'Black');
        jQuery("[id*='lblMessage']").html(locFujiCMPluginNotInstalled);
        return false;
    }   
   

    if (OSName == "Windows") {
        plugin().setDebug("C:/temp/FujiCM.log");
    } else {
        plugin().setDebug("/tmp/FujiCM.log");
    }
    var newHTML = locFujiCMPluginVersion + ' ' + plugin().plugin_version;
    jQuery("[id*='lblPluginMessage']").html(newHTML);
    //document.getElementById('ImgLoad').src = "Images/Green-Lamp.gif";
    SDKVerHTML = plugin().sdk_version;
    if (plugin().plugin_version < pluginVersion) {
        if (plugin().plugin_version == undefined) {
            jQuery("[id*='lblMessage']").css('color', 'Black');
            jQuery("[id*='lblMessage']").html(locFujiCMPluginNotInstalled);
        }
        else {
            jQuery("[id*='lblMessage']").css('color', 'Red');
            jQuery("[id*='lblMessage']").html(locFujiCMPluginIsOutdated);
        }
        return false;
    }
    return true;
    //jQuery("#DivIngLogo").wTooltip({ content: '&nbsp; ' + newHTML + '<br>&nbsp;SDK:' + SDKVerHTML + ' &nbsp;', className: "pluginImageTooltipClass", style: false });
}


/*
function DisplayLog() {
    console.log("In DisplayLog");
    var log = plugin().getLog();
    var newHTML = 'Log:(' + log.length + ')<BR>';
    newHTML += '<PRE>\n';
    newHTML += log;
    newHTML += '</PRE><BR>';
    document.getElementById("Log").innerHTML = newHTML;
}
*/
function presentScannerList(ScannerList) {
    //ScannerList is an array of scanners with properties.    
    jQuery("[id*='lblPluginMessage']").html("");
    g_ScannerList = {};


    for (var i = 0; i < ScannerList.length; i++) {
        if (ScannerList[i]["State"] == "Deleted" || ScannerList[i]["Serial"] != g_Chart["MeasurementDeviceSerial"]) {

            jQuery("[id*='btnCalibrate']").css('display', 'none');
            jQuery("[id*='lblMessage']").css('color', 'Red');
            if (ScannerList[i]["State"] == "Deleted") {
                jQuery("[id*='lblMessage']").html(locScanningDeviceUnplugged);
            }
            else {
                jQuery("[id*='lblMessage']").html(locDeviceSerialNotFound1 + " " + g_Chart["MeasurementDeviceSerial"] + " " + locDeviceSerialNotFound2);
            }
            jQuery("[id*='btnCalibrate']").css('display', 'none');
            jQuery("[id*='btnMeasureAndUploadChart']").css('display', 'none');
            jQuery("[id*='btnCancelMeasurement']").css('display', 'none');
            jQuery("[id*='btnRewindMeasurement']").css('display', 'none');

        }
        else {
            g_ScannerList = ScannerList[i];
            if (g_ScannerList["Requirements"].indexOf("RequiresCalibration") >= 0) {
                jQuery("[id*='lblMessage']").css('color', 'Black');
                jQuery("[id*='lblMessage']").html(locPleaseCalibrate);
                jQuery("[id*='btnCalibrate']").css('display', '');
                return;
            }
            else {
                jQuery("[id*='lblMessage']").css('color', 'Black');
                jQuery("[id*='lblMessage']").html(locClickMeasureButton);
                jQuery("[id*='btnMeasureAndUploadChart']").css('display', '');
                jQuery("[id*='btnCancelMeasurement']").css('display', '');
                if (!(scanner["Type"] == "iO" || scanner["Type"] == "iSis")) { //--------------Only display "Rewind" button in "i1-Pro" mode-----------//
                    jQuery("[id*='btnRewindMeasurement']").css('display', '');
                }              
                ControlDisability(0, false, 1, false, 0, false, 0, false); //------Make Measure button enabled.
                return;
            }
        }
    }
}

// This function requests the device list, but the result is 
// only a success or fail.  The listed callback function is
// called with the entire list once it is gathered. 

function ListDevices() {
    setTimeout("ListDevicesNow()", 3000);
}

function ListDevicesNow() {
    result = plugin().requestScannerList(presentScannerList);
    if (result != 'OK') {
        MeasurementDialog(locNoScannerList + " " + result, locErrorText);
    }
}

function scan(scanner, colorspace, chart) {

    if (MeasuredPatches.length > 0) MeasuredPatches.length = 0;
    if (g_Chart["Result"] > 0) g_Chart["Result"].length = 0;
    var localchart = chart;
    localchart["ReadCS"] = colorspace;
    localchart["Substrate"] = paper;
    if (scanner["Type"] == "iO" ||
        scanner["Type"] == "iSis" || scanner["Type"] == "SpectroLFP") {
        // This device will read the entire chart in a single go. So we copy entire RefBase (how chart is laid out) to the ReferencePatch
        if (typeof chart["RefBase"] != "undefined") {
            localchart["ReferencePatch"] = chart["RefBase"];

            //temp patch to work around the mode setting being different for io and ISIS
            if (scanner["Type"] == "iO") {
                scanner["Mode"] = chart["ReadMode"];
            }
            else {
                //this setting still needs to copy the chart["Mode"] setting to the scanner
                scanner["Mode"] = chart["Mode"];
            }

        }
    }
    else {
        localchart["CurrentStrip"] = chart["RowNames"][0];
        if (typeof chart["RefBase"] != "undefined") {
            localchart["ReferencePatch"] = chart["RefBase"].slice(0, chart["NumberOfColumns"]);  /* copy only one row at a time */
        }
    }

    // Create a global variable for the result multi-strip chart
    // Each row we get, we place in the gChart Results element.
    var query = new Object();
    query["Success"] = locSamplesCompleteText;
    query["Fail"] = locReadSampleFailedText;

    // Turn on automatic direction handling.  Options are tag[:value]
    // separated by semicolons, just like a CSS
    // I think you will want this on all strip scanners.  It is very reliable and really speeds things up for the user.			     
    scanner["Options"] = "AutoDirection";
    scanner["ActivationKey"] = actKey;
    scanner["SampleSource"] = "Paper";  // Choices are Paper, LCD or CRT and for the latter the chart must have 1 sample (spot)



    if (typeof chart["DeviceSettings"] == "object") {
        scanner["DeviceSettings"] = chart["DeviceSettings"]; // Move device settings to the scanner
    }





    if (scanner["Type"] == "iSis" || scanner["Type"] == "iO" || scanner["Type"] == "SpectroLFP") {

        //------------------Preview chart---------------//                             

        var canvas = document.getElementById("Canvas");
        var context = canvas.getContext("2d");
        context.clearRect(0, 0, canvas.width, canvas.height);

        var chartWidth = parseInt(jQuery("[id*='Canvas']").width());
        var chartHeight = parseInt(jQuery("[id*='Canvas']").height());

        var columnCount = chart["NumberOfColumns"];
        var rowCount = chart["NumberOfRows"];
        var cellSize = Math.min((chartWidth / columnCount), (chartHeight / rowCount));
        cellSize = Math.round(cellSize - 0.5);

        var result = chart["RefBase"];
        var samples = result.length;

        context.globalAlpha = 0.2; //--------------Make transparant background----------//

        DrawChart(result, cellSize, columnCount, 0, samples, samples, "Preview");       //---Call DrawChart() function to draw chart for preview on canvas-----//    

        context.globalAlpha = 1.0; //--------------Make regular background----------//

        if (scanner["Type"] == "iO" || scanner["Type"] == "SpectroLFP") {

            // var canvas = document.getElementById("Canvas");
            //var context = canvas.getContext("2d");
            // context.clearRect(0, 0, canvas.width, canvas.height);

            // Clear the chart location so the caller asks for the IO coordinate location
            //
            delete (localchart["UpperLeft"]);
            delete (localchart["UpperRight"]);
            delete (localchart["LowerLeft"]);
            delete (localchart["LowerRight"]);

            scanner["DeltaLLimit"] = "100.0";        // Any line delta E over this causes a retry

            var result = plugin().read(scanner, localchart, query,
                 presentStripDialogi1, presentProgressiSis, presentScanDoneiSis);

            //if (result == "ERROR: Activation Error") {
            //   jQuery("[id*='lblMessage']").css('color', 'Red');
            //  jQuery("[id*='lblMessage']").html("Status : This is not valid licensed measurement device.");
            // }
            //  else if (result != "OK") {
            //      jQuery("[id*='lblMessage']").css('color', 'Red');
            //      jQuery("[id*='lblMessage']").html("Status : Scanning Device has been unplugged.");
            // }
        }
        else if (scanner["Type"] == "iSis") {

            var result = plugin().read(scanner, localchart, query, presentChartDialogiSis, presentProgressiSis, presentScanDoneiSis);
        }
        if (result == "ERROR: Activation Error") {
            jQuery("[id*='lblMessage']").css('color', 'Red');
            jQuery("[id*='lblMessage']").html(locInvalidMeasurementDevice);
        }
        else if (result != "OK") {
            jQuery("[id*='lblMessage']").css('color', 'Red');
            jQuery("[id*='lblMessage']").html(locScanningDeviceUnplugged);
        }
    }
    else {

        //-----------------Preview chart for i1-Pro----------------//
        var canvas = document.getElementById("Canvas");
        var context = canvas.getContext("2d");
        context.clearRect(0, 0, canvas.width, canvas.height);

        var result = plugin().read(scanner, localchart, query, presentStripDialogi1, presentProgressi1, presentScanDonei1);
        if (result == "ERROR: Activation Error") {
            jQuery("[id*='lblMessage']").css('color', 'Red');
            jQuery("[id*='lblMessage']").html(locInvalidMeasurementDevice);
            return;
        }
        else if (result != "OK") {
            jQuery("[id*='lblMessage']").css('color', 'Red');
            jQuery("[id*='lblMessage']").html(locScanningDeviceUnplugged);
            return;
        }
        // Display the original chart from the reference values.

        var area = new Object();
        area["W"] = 0;
        area["H"] = 0;

        var distance = new Object();
        distance["R"] = 0;
        distance["C"] = 0;

        var canvas = document.getElementsByTagName('Canvas')[0];
        canvas.height = 350;        //---Assign original height back

        var w = parseInt(jQuery("[id*='Canvas']").width());
        var h = parseInt(jQuery("[id*='Canvas']").height());
        var startPosition = 0;
        col = chart["NumberOfColumns"];
        row = chart["NumberOfRows"];

        if (row < 20) {
            h = h - 50;
        }
        else if (row >= 20 && row <= 40) {
            h = h * 2;
        }
        else if (row > 40 && row <= 50) {
            h = h * 5;
        }
        else {
            h = h * 6;
        }

        canvas.width = w;       //---Assign new height & width
        canvas.height = h;

        if (row != 1) {

            //--------Fuji 150 or more big chart----------//

            area["H"] = ((h / row) / 2) - 1;
            distance["R"] = ((h / row) / 2) + 1;

            area["W"] = ((w / col) / 2) + '';
            distance["C"] = ((w / col) / 2) + '';
        }
        else {

            //---------ISO 24 Patch chart----------//
            area["H"] = 20;
            distance["R"] = 1;

            area["W"] = ((w / col) / 2) - 1 + '';       // Have to do 1 minus because it doesn't fit in canvas width for last cell
            distance["C"] = ((w / col) / 2) - 1 + '';   // Have to do 1 minus because it doesn't fit in canvas width for last cell
        }


        var noOfCells = chart["Size"];
        var drawCells = 0;
        var cId = 0;
        var x1 = 0;
        var y1 = 0;

        for (i = 0; i < row; i++) {

            x1 = 0;

            for (j = 0; j < col; j++) {

                if (noOfCells > drawCells) {

                    var xx1 = startPosition + x1;
                    var yy1 = startPosition + y1;
                    x1 = x1 + parseInt(area["W"]) + parseInt(distance["C"]);

                    var rgbx = chart["RefBase"][cId]["HTMLColor"];

                    context.fillStyle = rgbx;
                    context.fillRect(xx1, yy1, parseInt(area["W"]), parseInt(area["H"]));

                    drawCells++;
                    cId++;
                }
            }
            y1 = y1 + (parseInt(area["H"])) + parseInt(distance["R"]);
        }
    }
}
function refreshCanvas() {
   
    var canvas = document.getElementById("Canvas");
    var context = canvas.getContext("2d");
    context.clearRect(0, 0, canvas.width, canvas.height);
    y1 = 0;
    drawCells = 0;
}


// This function takes the current saved chart data and uploads it to
// the server as a json string, then pops up the result.
function upload() {
    saveMeasurement();
}

function OnCancel() {
    g_SavedQuery["Result"] = 'CANCEL'; 
    var cancelstat = plugin().queryDone(g_SavedQuery);

    //added these lines for clean up of cancel???
    jQuery("[id*='lblMessage']").css('color', 'Black');
    jQuery("[id*='lblMessage']").html(locClickMeasureButton);
    
    if (cancelstat == "Unhandled") {
        // If unhandled clean up this stuff here...
        jQuery("[id*='lblMessage']").css('color', 'Black');
        jQuery("[id*='lblMessage']").html(locClickMeasureButton);
       // delete mychart["Result"];
    }
}

function OnRewind() {

    if (next > 0) { //---------Call "Rewind" functionality if we are in a middle of the measurement and want to do rewind-------------//
        g_SavedQuery["Result"] = 'REWIND';
        backup1 = 1;
      
        var cancelstat = plugin().queryDone(g_SavedQuery);

        var query = new Object();
        query["Success"] = "Samples Complete";
        query["Fail"] = "Failed to read samples";
        query["Backup"] = "Backup";      
    }
    else { //----------If we are in rewinding mode at row 1 and someone click "Rewind" button again and again, then stick with the row 1 re-measure mode, because row 1 is the last row to measure------//
        jQuery("[id*='lblMessage']").css('color', 'Black');
        jQuery("[id*='lblMessage']").html("Status : Please place the scanner over the strip row 1 press and hold the button while sliding the scanner across the strip.");
    }
}

// From here on down is the modal window code.
//var _rulesAdded = false;


/* These functions deal with IE's retardedness in not allowing divs to 
* cover select elements by replacing the select elements with spans. */
/*
function RemoveSelectSpans() {
var selects = document.getElementsByTagName('select');
for (var i = 0; i < selects.length; i++) {
var select = selects[i];
if (select.clientWidth == 0 || select.clientHeight == 0 ||
select.nextSibling == null || select.nextSibling.className != 'selectReplacement') {
continue;
}
select.parentNode.removeChild(select.nextSibling);
select.style.display = select.cachedDisplay;
}
}

function ReplaceSelectsWithSpans() {
var selects = document.getElementsByTagName('select');
for (var i = 0; i < selects.length; i++) {
var select = selects[i];
if (select.clientWidth == 0 || select.clientHeight == 0 ||
select.nextSibling == null || select.nextSibling.className == 'selectReplacement') {
continue;
}
var span = document.createElement('span');
// this would be "- 3", but for that appears to shift the block that contains the span 
//   one pixel down; instead we tolerate the span being 1px shorter than the select
span.style.height = (select.clientHeight - 4) + 'px';
span.style.width = (select.clientWidth - 6) + 'px';
span.style.display = 'inline-block';
span.style.border = '1px solid rgb(200, 210, 230)';
span.style.padding = '1px 0 0 4px';
span.style.fontFamily = 'Arial';
span.style.fontSize = 'smaller';
span.style.position = 'relative';
span.style.top = '1px';
span.className = 'selectReplacement';
span.innerHTML = select.options[select.selectedIndex].innerHTML +
'<img src="custom_drop.gif" alt="drop down" style="position: absolute; right: 1px; top: 1px;" />';
select.cachedDisplay = select.style.display;
select.style.display = 'none';
select.parentNode.insertBefore(span, select.nextSibling);
}
}

*/
/* utiltiy functions 
function GetWindowWidth() {
    return width =
	document.documentElement && document.documentElement.clientWidth ||
	document.body && document.body.clientWidth ||
	document.body && document.body.parentNode && document.body.parentNode.clientWidth ||
	0;
}

function GetWindowHeight() {
    return height =
	document.documentElement && document.documentElement.clientHeight ||
	document.body && document.body.clientHeight ||
  	document.body && document.body.parentNode && document.body.parentNode.clientHeight ||
  	0;
}

function $(id) {
    return document.getElementById(id);
}
*/
console.log("chartread.js complete");
