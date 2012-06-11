// -*- mode: javascript; indent-tabs-mode: nil; c-basic-offset: 4 -*-
// 
// This is a simplified example for just calibration and reading an X/Y chart.
// It has all the other features removed for clarity.  Since the point read
// has been removed, reading the Density colorspace is not allowed.
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
if (!Array.prototype.indexOf) {
    Array.prototype.indexOf = function (obj, start) {
        for (var i = (start || 0), j = this.length; i < j; i++) {
            if (this[i] === obj) { return i; }
        }
        return -1;
    }
}

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

console.log("Loading xychartread.js");
var actKey = '';
var g_ScannerList = new Object();
var g_Chart = new Object();
g_Chart["Result"] = new Array();

MeasuredPatches = [];

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

//constructor for Density object
function Density(Red, Green, Blue, Visual) {
    this.Red = Red;
    this.Green = Green;
    this.Blue = Blue;
    this.Visual = Visual;
}

//Patch Object
function Patch(LAB, XYZ, Density, sName) {

    this.LAB = LAB;
    this.XYZ = XYZ;
    this.Density = Density;
    this.SampleName = sName;
}

// NOTE:  Don't overwrite the elements of this chart in the subsequent code, even
// if you have copied it to a temporary to pass down to the plugin.  Javascript does
// something very strange and will end up clobbering the original elements during
// the callback, even if the original was only copied.  We can always copy elements
// to new entries (i.e. copy mychart["ReferenceColors"] = random331samples["RefBase"][2])
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

function load() {    
    jQuery("[id*='lblPluginMessage']").html("Error in plugin.");
    addEvent(plugin(), 'fire', function (x) {
        alert('onfired() from load(): ' + x)
    });
    console.log("Loading plugin version " + plugin().plugin_version);
    console.log("GUI Version 1.7");
    if (OSName == "Windows") {
        plugin().setDebug("C:/temp/FujiCM.log");
    } else {
        plugin().setDebug("/tmp/isis.log");
    }
    var newHTML = 'Fuji Color Manager Plugin Version ' + plugin().plugin_version;
    jQuery("[id*='lblPluginMessage']").html(newHTML);
    //document.getElementById('ImgLoad').src = "Images/Green-Lamp.gif";
    SDKVerHTML = plugin().sdk_version;
    //jQuery("#DivIngLogo").wTooltip({ content: '&nbsp; ' + newHTML + '<br>&nbsp;SDK:' + SDKVerHTML + ' &nbsp;', className: "pluginImageTooltipClass", style: false });
}

function DisplayLog() {
    console.log("In DisplayLog");
    var log = plugin().getLog();
    var newHTML = 'Log:(' + log.length + ')<BR>';
    newHTML += '<PRE>\n';
    newHTML += log;
    newHTML += '</PRE><BR>';
    document.getElementById("Log").innerHTML = newHTML;
}
function presentScannerList(ScannerList) {    
    //ScannerList is an array of scanners with properties.    
    jQuery("[id*='lblPluginMessage']").html("");
    if (ScannerList.length == 0) return;
    if (ScannerList[0]["State"] == "Deleted") {

        jQuery("[id*='btnCalibrate']").css('display', 'none');
        jQuery("[id*='lblMessage']").css('color', 'Red');
        jQuery("[id*='lblMessage']").html("Status : Scanning Device has been unplugged.");
        jQuery("[id*='btnCalibrate']").css('display', 'none');
        jQuery("[id*='btnMeasureAndUploadChart']").css('display', 'none');
        jQuery("[id*='btnCancelMeasurement']").css('display', 'none');
    }
    else {
        g_ScannerList = ScannerList;
        if (g_ScannerList[0]["Requirements"].indexOf("RequiresCalibration") >= 0) {
            jQuery("[id*='lblMessage']").css('color', 'Black');
            jQuery("[id*='lblMessage']").html("Status : Please calibrate.");
            jQuery("[id*='btnCalibrate']").css('display', '');
        }
        else {
            jQuery("[id*='lblMessage']").css('color', 'Black');
            jQuery("[id*='lblMessage']").html("Status : Click on 'Measure Chart' button.");
            jQuery("[id*='btnMeasureAndUploadChart']").css('display', '');
            jQuery("[id*='btnCancelMeasurement']").css('display', '');
        }
    }
}

// This function requests the device list, but the result is 
// only a success or fail.  The listed callback function is
// called with the entire list once it is gathered.  
function ListDevices() {
    result = plugin().requestScannerList(presentScannerList);
    if (result != 'OK') {
        MeasurementDialog("Failed to request scanner list: " + result, "Error");
    }
}

// chart is an object with the following information
// chart["Name"] = string name of strip or chart
// chart["Description"] = description of strip or chart
// chart["Size"] = integer number of samples
// chart["NumberOfColumns"] = integer width
// chart["NumberOfRows"] = integer height
// chart["ReferencePatch"] = array of expected values
// Query is an arbitrary object that is passed through so the prompters and done functions know what was expected.
// other values may be included and are passed through to the callback functions
function scan(scanner, colorspace, chart) {
    
    if (MeasuredPatches.length > 0) MeasuredPatches.length = 0;
    if (g_Chart["Result"] > 0) g_Chart["Result"].length = 0;
    var localchart = chart;
    localchart["ReadCS"] = colorspace;
    localchart["Substrate"] = paper;
    if (scanner["Type"] == "iO" ||
        scanner["Type"] == "iSis") {
        // This device will read the entire chart in a single go. So we copy entire RefBase (how chart is laid out) to the ReferencePatch
        if (typeof chart["RefBase"] != "undefined") {
            localchart["ReferencePatch"] = chart["RefBase"];
            scanner["Mode"] = chart["Mode"];
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
    query["Success"] = "Samples Complete";
    query["Fail"] = "Failed to read samples";

    // Turn on automatic direction handling.  Options are tag[:value]
    // separated by semicolons, just like a CSS
    // I think you will want this on all strip scanners.  It is very reliable and really speeds things up for the user.			     
    scanner["Options"] = "AutoDirection";
    scanner["ActivationKey"] = actKey;
    scanner["SampleSource"] = "Paper";  // Choices are Paper, LCD or CRT and for the latter the chart must have 1 sample (spot)

    if (typeof chart["DeviceSettings"] == "object") {
        scanner["DeviceSettings"] = chart["DeviceSettings"]; // Move device settings to the scanner
    }

    if (scanner["Type"] == "iO" || scanner["Type"] == "iSis") {

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

        //----------------Scan code start----//     
        var result = plugin().read(scanner, localchart, query, presentChartDialogiSis, presentProgressiSis, presentScanDoneiSis);
        if (result == "ERROR: Activation Error") {
            jQuery("[id*='lblMessage']").css('color', 'Red');
            jQuery("[id*='lblMessage']").html("Status : This is not valid license device.");
        }
        else if (result != "OK") {
            jQuery("[id*='lblMessage']").css('color', 'Red');
            jQuery("[id*='lblMessage']").html("Status : Scanning Device has been unplugged.");
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
            jQuery("[id*='lblMessage']").html("Status : This is not valid license device.");
            return;
        }
        else if (result != "OK") {
            jQuery("[id*='lblMessage']").css('color', 'Red');
            jQuery("[id*='lblMessage']").html("Status : Scanning Device has been unplugged.");
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
        if (row >= 20 && row <= 40) {
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
    // jQuery("[id*='Canvas']").empty();

    var canvas = document.getElementById("Canvas");
    var context = canvas.getContext("2d");
    context.clearRect(0, 0, canvas.width, canvas.height);
}

// This function takes the current saved chart data and uploads it to
// the server as a json string, then pops up the result.
function upload() {
    saveMeasurement();
}

// From here on down is the modal window code.
var _rulesAdded = false;
function OnCancel() {
    jQuery("[id*='lblMessage']").css('color', 'Black');
    jQuery("[id*='lblMessage']").html("Status : Click on 'Measure Chart' button.");

    if (MeasuredPatches.length > 0) MeasuredPatches.length = 0;
    //g_Question["Result"] = 'CANCEL';
    //var cancelstat = plugin().queryDone(g_Question);
}


/* These functions deal with IE's retardedness in not allowing divs to 
* cover select elements by replacing the select elements with spans. */
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

/* utiltiy functions */
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
console.log("xychartread.js complete");
