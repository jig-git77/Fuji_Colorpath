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


// Define the Samples object for a 21x21 read
// I don't have expected values just yet.

console.log("Loading xychartread.js");

/** Chirag Create a Gloabal RefChart Object **/
var g_ScannerList = new Object();
var g_Chart = new Object();
g_Chart["Result"] = new Array();

//added by chirag declared a global array for measured patches
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
function plugin0() {
    return document.getElementById('plugin0');
}
plugin = plugin0;
function addEvent(obj, name, func) {
    if (window.addEventListener) {
        obj.addEventListener(name, func, false);
    } else {
        obj.attachEvent("on" + name, func);
    }
}

function load() {
    jQuery("#DivIngLogo").wTooltip({ content: '&nbsp; Error in plugin. &nbsp;', className: "pluginImageTooltipClass", style: false });    
    addEvent(plugin(), 'fired', function (x) {
        alert('onfired() from load(): ' + x)
    });
    // The following line turns on some debugging output in the plugin
    console.log("Loading plugin version " + plugin().plugin_version);
    console.log("GUI Version 1.7");
    if (OSName == "Windows") {
        plugin().setDebug("C:/temp/FujiCM.log");
    } else {
        plugin().setDebug("/tmp/isis.log");
    }
    var newHTML = '<B>Fuji Color Manager Plugin Version ' + plugin().plugin_version + '</B>';
    document.getElementById('ImgLoad').src = "Images/Green-Lamp.gif";
    //document.getElementById('ApagoCMVersion').innerHTML = newHTML;
    SDKVerHTML = plugin().sdk_version;
    jQuery("#DivIngLogo").wTooltip({ content: '&nbsp; ' + newHTML + '<br>&nbsp;SDK:' + SDKVerHTML + ' &nbsp;', className: "pluginImageTooltipClass", style: false });
    //document.getElementById('SDKVersion').innerHTML = SDKVerHTML;
}
function pluginLoaded() {
    //alert('Plugin Loaded');
    var newHTML = '<B>Fuji Color Manager Plugin Version ' + plugin().plugin_version + '</B>';    
    document.getElementById('ApagoCMVersion').innerHTML = newHTML;
    SDKVerHTML = plugin().sdk_version;
    document.getElementById('SDKVersion').innerHTML = SDKVerHTML;
}

function addTestEvent() {
    addEvent(plugin(), 'fired', function (x) {
        alert('onfired(): ' + x)
    });
}

function testEvent() {
    plugin().testEvent('test stuff');
}

//
// A Query Callback has to be completely thread independant.  What this
// means is the plugin will call this function Async from the lower thread,
// That thread will then lock on a semaphore waiting for a reply.  When
// The user query is done, They will put the result of the query in the
// question["Result"] field, and call queryDone with the done object.  This
// will set the threads results values and unlock the thread.
// The expected values in the question object are:
// "Scanner" - the Scanner object 
// "Action"    - "Ask", Ask a question -  
//                     "Inform", Inform the user of an action (generally timed popup)
//                     "Remove", Remove a querry dialog (for example if the scanner
//                                       button was pressed instead of a mouse click
// "Request" - A request object that is passed through the device interface.
// "Prompt"  - The string prompt to ask
// "Answers" - An string indicating  possible result answers types.
//                     Most will be either "OK" for just go on, and 
//                     "YesNo" for boolean
//                     "String",
//                     "Number"
//                     Result may always be "CANCEL" to tell the driver to just
//                     give up.
// Other properties of the question should be passed through for driver
// use.
function presentQuery(question) {
    //alert("About to call OnQuery with " + question['Prompt']);
    OnQuery(question);
}

// This function presents the EyeOne calibrate query.  We do this
// in javascript so the web app can change everything including
// the language of the query.
function presentCalibrateDialog(scanner, myQuery) {
    // Everything needed for the generic presentQuery is already 
    // filled in, however you can use this to change the actual
    // text for example to do multi-language.
    //
    // Calibrate sets the Request to standard strings regardless of language and you can
    // index pre-supplied strings (CALIBRATEINSTRUCTIONS, CALIBRATEACKNOWLEDGED, CANCELLED) like this:
    if (myQuery["Request"] == "CALIBRATEACKNOWLEDGED") {
        RemoveModalWindow();
    }
    myQuery["Prompt"] = myQuery[myQuery["Request"]];
    presentQuery(myQuery);
}
//
// The "Done" callback functions take three args.  The scanner, the message
// (which may be replaced based on language requirements and an optional string.
// The string represents final cleanup and is usally a call back to the
// C++ side so we can close down any remaining threads and such.  If it
// is defined, all you have to do is eval it.
//
function presentCalibrateDone(scanner, msg, final) {
    // make sure the popup window is gone
    RemoveModalWindow();

    // We have to make one more call to the library.  The 
    // calibrate itself ran in a non-browser thread.  We need
    // the browser thread to join with the calibrate thread to
    // keep the process state tidy.
    // the library that the poll thread can be 

    //plugin().calibrateComplete(scanner);
    if (typeof final != "undefined") {
        //alert(final);
        eval(final);
    }
    //
    // Now let the user know how things ended up.
    if (typeof msg == "string") {
        // This would be either "CANCELLED" or "ERROR: ..."
        // for CANCELLED, you might just want to do nothing.
        if (!(msg == "OK" || msg == "CANCELLED")) {
            // alert(msg);
            confirm(msg, "Error");  
           
        }
        else {
            //alert("Calibration Successful");
            confirm("Calibration Successful", "Calibration");
            jQuery('#lblMessage').html("Calibration Successful");
            jQuery('#btnMeasureChart').css('display', '');
        }
    } 
}

// Calibrate the device.  We pass in the scanner to calibrate,
//  a callback to present instructions to the user, and a callback
// to notify the user that the calibration is done.
function calibrateDevice(scanner) {
    var myQuery = new Object();
    myQuery["CALIBRATEINSTRUCTIONS"] = "<B><CENTER>Please place i1Pro in the calibration cradle<BR>and press press the i1 trigger<BR>or click anywhere in this window</CENTER></B>";
    myQuery["CALIBRATEACKNOWLEDGED"] = "<B><CENTER>Calibration In Progress</CENTER></B>";

    // scanner calibration for i1 supports "Mode", and "SampleSource".  Mode allows you to calibrate for
    // "Point","Strip" or "Both".  
    // SampleSource sets either "Paper for reflectance,"LCD" or "CRT" for monitor 
    scanner["Mode"] = "Both"
    scanner["SampleSource"] = "Paper";  // Choices are Paper, LCD or CRT and for the latter the chart must have 1 sample (spot)
    var result = plugin().calibrateDevice(scanner, myQuery, presentCalibrateDialog, presentCalibrateDone);
    if (result != "OK") {
        alert("Scanning Device has been unplugged");
    }
}


// This function presents the iSis read query.  We do this
// in javascript so the web app can change everything including
// the language of the query.
function presentChartDialog(scanner, chart, query) {

    if (query["Request"] == "READACKNOWLEDGED") {
        query["Prompt"] = "<B><CENTER>Chart read in progress</CENTER></B>";
    } else {
        query["Prompt"] = "<B><CENTER>Please place the chart in the scanner<BR>and click in this window.</CENTER></B>";
    }
    presentQuery(query);
}


// This function presents the EyeOne calibrate query.  We do this
// in javascript so the web app can change everything including
// the language of the query.
function presentStripDialog(scanner, chart, query) {
    // 
    // For this example we passed in the "CurrentStrip" value in the query.
    // Using this we build the appropriate prompt for the use.  A nice GUI
    // would create a pop-up window with a small version of the cart and
    // everything but the row we were interested in grayed out.  This prompt
    // is for the actual strip read.  Not for a strip done.  The strip done
    // will present the options of which strip to read next (defaulting to the
    // next strip in the sequence).
    //
    if (query["Request"] == "READACKNOWLEDGED") {
        query["Prompt"] = "<B><CENTER>Strip read in progress</CENTER></B>";
    } else {
        if (scanner["Type"] == "iO") {
            query["Prompt"] = "<B><CENTER>Please place the chart on the platten and click to continue.</CENTER></B>";
        } else {
            query["Prompt"] = "<B><CENTER>Please place the scanner over the strip " + chart["CurrentStrip"] + ".<BR>press and hold the trigger while sliding the scanner across the strip.</CENTER></B>";
        }
    }
    presentQuery(query);
}


function presentScanDoneiSis(scanner, mychart, final) {
    // make sure the popup window is gone
    RemoveModalWindow();

    // Before we finalize the request, get all the values from
    // the strip read and present them to the user.
    // This is a syncronous operation since the data is 
    // stored and ready to go.
    // The samples object is the same object that describes the
    // chart and expected results, but with the values we actually
    // read added.
    if (typeof mychart["Result"] == "string") {
        // This type of query means we finish up with an error, so just present the
        // error
        if (!(mychart["Result"] == "OK" || mychart["Result"] == "CANCELLED")) {
            alert(mychart["Result"]);
        }
        if (typeof final == "string") {
            eval(final);
        } else if (typeof final == "object") {
            final();
        }
        return;
    }
    //
    // Now let the user know how things ended up.
    //
    var newHTML = "";
    newHTML += "<UL>";

    var results = mychart["Result"];
    var samples = results.length;
    var width = mychart["NumberOfColumns"];
    var expected = mychart["NumberOfRows"] * width;
    if (results.length != expected) {
        // Prompt the user to try again
        alert("Failed to read expected number of samples(" + expected + " != " + results.length + ")");
        if (typeof final == "string") {
            eval(final);
        } else if (typeof final == "object") {
            final();
        }
        return;
    }

    // This sort of chart always reads all samples if it were successful
    // The result will be an array of colorspaces, where each colorspace is a two
    // dimensional array of "Rows" then "Columns" then samples as a vector.
    //
    //iSisEx["Result"] = results;


    /*** Chirag add data to Patch Object ***/
    for (ind = 0; ind < samples; ind++) {

        var LABResults = results[ind]["CIELab"];
        var XYZResults = results[ind]["CIEXYZ"];
        var DensityResults = results[ind]["Density"];
        var sName = mychart["ReferencePatch"][ind]["SampleName"];
        var PatchTypeID = mychart["ReferencePatch"][ind]["PatchTypeID"];
        //Density Filters: Red for Cyan, Green For Mag and Blue for Yellow
        //mpatch = { LAB_L : LABResults[0].toFixed(2), LAB_a: LABResults[1].toFixed(2), LAB_b: LABResults[2].toFixed(2),XYZ_X : XYZResults[0].toFixed(2), XYZ_Y: XYZResults[1].toFixed(2), XYZ_Z: XYZResults[2].toFixed(2), Density_V: DensityResults[3].toFixed(2),Density_R: DensityResults[0].toFixed(2), Density_G: DensityResults[1].toFixed(2),Density_B: DensityResults[2].toFixed(2) };


        var CIELab = new LAB(LABResults["L"].toFixed(2), LABResults["A"].toFixed(2), LABResults["B"].toFixed(2));
        var CIEXYZ = new XYZ(XYZResults["X"].toFixed(2), XYZResults["Y"].toFixed(2), XYZResults["Z"].toFixed(2));
        var _Density = new Density(DensityResults[0].toFixed(5), DensityResults[1].toFixed(5), DensityResults[2].toFixed(5), DensityResults[3].toFixed(5));

        mpatch = new Patch();
        mpatch.LAB = LABResults;
        mpatch.XYZ = XYZResults;
        mpatch.Density = _Density;
        mpatch.SampleName = sName;
        mpatch.CMYK_C = mychart["ReferencePatch"][ind]["CMYK_C"];
        mpatch.CMYK_M = mychart["ReferencePatch"][ind]["CMYK_M"];
        mpatch.CMYK_Y = mychart["ReferencePatch"][ind]["CMYK_Y"];
        mpatch.CMYK_K = mychart["ReferencePatch"][ind]["CMYK_K"];
        mpatch.PatchTypeID = mychart["ReferencePatch"][ind]["PatchTypeID"];
        MeasuredPatches.push(mpatch);


    }



    // From here on down, we are done with the reading of the strips.
    // We can now present the final results to the user.
    // Lets draw our sampled colors over the top of the displayed
    // image.
    //
    // Add an upload button and canvas
    //
    var newHTML = '<A HREF="#" onclick="javascript:upload()">Upload Data</A><BR>';
    newHTML += '<DIV ID="Canvas" style="position:relative;height:450px;width:440px;">';
    newHTML += '</DIV>';
    var span = "UploadData" + scanner["Name"];
    document.getElementById(span).innerHTML = newHTML;

    var jg = new jsGraphics("Canvas");    // Use the "Canvas" div for drawing
    var cursample = 0;
    var x = 0;
    var y = 0;
    for (cursample = 0; cursample < samples; cursample++) {
        var srgb = results[cursample]["sRGB"];
        var rgbx = "#" + dec2hex(srgb["R"]) + dec2hex(srgb["G"]) + dec2hex(srgb["B"]);
        jg.setColor(rgbx);
        jg.fillRect(x * 20, y * 20, 20, 20);
        x++;
        if (x >= width) {
            x = 0;
            y++;
        }
    }
    jg.paint();

    // We have to make one more call to the library.  The 
    // calibrate itself ran in a non-browser thread.  We need
    // the browser thread to join with the calibrate thread to
    // keep the process state tidy.
    if (typeof final == "string") {
        eval(final);
    } else if (typeof final == "object") {
        final();
    }
}



//
// The "Done" callback functions take three args.  The scanner, 
// the prompt object and the final function.  If the final function
// is a function object it is called, if it is a string then
// it is evaluated.  This generally will be a call back to the
// C++ side to do final thread and memory cleanup. Sample values
// must be retriveved before the final call.
//
function presentScanDone(scanner, mychart, final) {

    // make sure the popup window is gone
    RemoveModalWindow();

    /* chirag put logic to return if lengh of measurepatch = total patches so far*/
    //if (mychart["Size"] == MeasuredPatches.length) return;

    // Before we finalize the request, get all the values from
    // the strip read and present them to the user.
    // This is a syncronous operation since the data is 
    // stored and ready to go.
    // The samples object is the same object that describes the
    // chart and expected results, but with the values we actually
    // read added.
    if (typeof mychart["Result"] == "string") {
        // This type of query means we finish up with an error, so just present the
        // error
        if (!(mychart["Result"] == "OK" || mychart["Result"] == "CANCELLED")) {
            //alert(mychart["Result"]);
            confirm(mychart["Result"], "Error");  
        }
        if (typeof final == "string") {
            eval(final);
        } else if (typeof final == "object") {
            final();
        }
        if (mychart["Result"] != "CANCELLED") {
            // Retry minor errors, all others are stoppers
            if (typeof mychart["ErrorLevel"] == "string" &&
                mychart["ErrorLevel"] == "Minor") {
                var query = new Object();
                query["Success"] = "Samples Complete";
                query["Fail"] = "Failed to read samples";

                var result = plugin().read(scanner, mychart, query,
		   		           presentStripDialog, presentProgress, presentScanDone);
                if (result != "OK") {
                    alert("Scanning Device has been unplugged");
                }
            }
        }
        return;
    }
    //
    // Now let the user know how things ended up.
    //
    //var newHTML = "";
    //newHTML += "<UL>";

    var results = mychart["Result"];
    var idx = mychart["NumberOfRows"];

    if (!(scanner["Type"] == "iO" || scanner["Type"] == "iSis")) {
        if (results.length != mychart["NumberOfColumns"]) {
            // Prompt the user to try again
            alert("Failed to read expected number of samples" + results.length + " vs " + mychart["NumberOfColumns"]);
            //confirm("Failed to read expected number of samples" + results.length + " vs " + mychart["NumberOfColumns"], "Error"); 
            if (typeof final == "string") {
                eval(final);
            } else if (typeof final == "object") {
                final();
            }

            var query = new Object();
            query["Success"] = "Samples Complete";
            query["Fail"] = "Failed to read samples";
            mychart["Result"] = null;

            var result = plugin().read(scanner, mychart, query,
                                       presentStripDialog, presentProgress, presentScanDone);
            if (result != "OK") {
                alert("Scanning Device has been unplugged");
            }
            return;
        }

        // Save this result
        var idx;
        var curstr = mychart["CurrentStrip"];
        console.log("Looking for strip " + curstr);
        idx = mychart["RowNames"].indexOf(curstr);

        if (idx < 0 || idx > mychart["NumberOfRows"]) {
            alert("ERROR: Cannot determine current read column");
            return;
        }
    }

    //
    // Proceed to thread clean up.  This will happen
    // quickly
    //
    if (typeof final == "string") {
        eval(final);
    } else if (typeof final == "object") {
        final();
    }

    // All I should need to do is append each array element
    // to the end of my current results since it is now a flat 
    // array
    if (typeof g_Chart["Result"] == "undefined") {
        // The final result saving place does not exist.  We create it
        // with all colorspaces requested right now so we don't have to
        // worry about it later.
        g_Chart["Result"] = new Array();
    }
    g_Chart["Result"] = g_Chart["Result"].concat(results);


    /*** Chirag add data to Patch Object ***/
    for (ind = 0; ind < g_Chart["NumberOfColumns"]; ind++) {

        var LABResults = results[ind]["CIELab"];
        var XYZResults = results[ind]["CIEXYZ"];
        var DensityResults = results[ind]["Density"];
        var sName = mychart["ReferencePatch"][ind]["SampleName"];
        var PatchTypeID = mychart["ReferencePatch"][ind]["PatchTypeID"];
        //Density Filters: Red for Cyan, Green For Mag and Blue for Yellow
        //mpatch = { LAB_L : LABResults[0].toFixed(2), LAB_a: LABResults[1].toFixed(2), LAB_b: LABResults[2].toFixed(2),XYZ_X : XYZResults[0].toFixed(2), XYZ_Y: XYZResults[1].toFixed(2), XYZ_Z: XYZResults[2].toFixed(2), Density_V: DensityResults[3].toFixed(2),Density_R: DensityResults[0].toFixed(2), Density_G: DensityResults[1].toFixed(2),Density_B: DensityResults[2].toFixed(2) };


        var CIELab = new LAB(LABResults["L"].toFixed(2), LABResults["A"].toFixed(2), LABResults["B"].toFixed(2));
        var CIEXYZ = new XYZ(XYZResults["X"].toFixed(2), XYZResults["Y"].toFixed(2), XYZResults["Z"].toFixed(2));
        var _Density = new Density(DensityResults[0].toFixed(5), DensityResults[1].toFixed(5), DensityResults[2].toFixed(5), DensityResults[3].toFixed(5));

        mpatch = new Patch();
        mpatch.LAB = LABResults;
        mpatch.XYZ = XYZResults;
        mpatch.Density = _Density;
        mpatch.SampleName = sName;
        mpatch.CMYK_C = mychart["ReferencePatch"][ind]["CMYK_C"];
        mpatch.CMYK_M = mychart["ReferencePatch"][ind]["CMYK_M"];
        mpatch.CMYK_Y = mychart["ReferencePatch"][ind]["CMYK_Y"];
        mpatch.CMYK_K = mychart["ReferencePatch"][ind]["CMYK_K"];
        mpatch.PatchTypeID = mychart["ReferencePatch"][ind]["PatchTypeID"];
        MeasuredPatches.push(mpatch);


    }



    // Check to see if this was the last strip to be read.
    var next = -1;
    if (idx < (mychart["NumberOfRows"] - 1)) {
        next = idx + 1;
    }

    var jg = new jsGraphics("Canvas");    // Use the "Canvas" div for drawing
    var x = 0;
    var y = idx;

    for (var cursample = 0; cursample < results.length; cursample++) {
        var srgb = results[cursample]["sRGB"];
        var rgbx = "#" + dec2hex(srgb["R"]) + dec2hex(srgb["G"]) + dec2hex(srgb["B"]);
        jg.setColor(rgbx);
        jg.fillRect(x * 20, y * 20, 20, 20);
        x++;
        if (x >= mychart["NumberOfColumns"]) {
            x = 0;
            y++;
        }
    }
    jg.paint();

    //
    // Now ask for the next strip, if we were not at the end
    //
    if (next != -1) {

        
        mychart["CurrentStrip"] = mychart["RowNames"][next];
        var query = new Object();
        query["Success"] = "Samples Complete";
        query["Fail"] = "Failed to read samples";
        mychart["Result"] = null;

        // We have no reference patches for this chart yet
        if (typeof mychart["RefBase"] != "undefined") {
            mychart["ReferencePatch"] = mychart["RefBase"].slice(next * mychart["NumberOfColumns"], (next + 1) * mychart["NumberOfColumns"]);
        }

        //alert(JSON.stringify(mychart));
        var result = plugin().read(scanner, mychart, query,
				   presentStripDialog, presentProgress, presentScanDone);

        if (result != "OK") {
            alert("Scanning Device has been unplugged");
        }
    }

    if (next == -1) {
        //var newHTML = '<A HREF="#" onclick="javascript:upload()">Upload Data</A><BR>';
        //newHTML += '<DIV ID="Canvas" style="position:relative;height:918px;width:440px;">';
        //newHTML += '</DIV>';
        // repaint the entire array the last time round
        //var span = "UploadData" + scanner["Name"];
        //document.getElementById(span).innerHTML = newHTML;

        jQuery('#btnUploadData').css('display', '');


        var jg = new jsGraphics("Canvas");    // Use the "Canvas" div for drawing

        var x = 0;
        var y = 0;
        for (cursample = 0; cursample < mychart["RefBase"].length; cursample++) {
            var rgbx = mychart["RefBase"][cursample]["HTMLColor"];
            jg.setColor(rgbx);
            jg.fillRect(x * 20, y * 20, 20, 10);
            x++;
            if (x >= mychart["NumberOfColumns"]) {
                x = 0;
                y++;
            }
        }
        jg.paint();

        x = 0;
        y = 0;
        for (var cursample = 0; cursample < g_Chart["Result"].length; cursample++) {
            var srgb = g_Chart["Result"][cursample]["sRGB"];
            var rgbx = "#" + dec2hex(srgb["R"]) + dec2hex(srgb["G"]) + dec2hex(srgb["B"]);
            jg.setColor(rgbx);
            jg.fillRect(x * 20, (y * 20) + 10, 20, 10);
            x++;
            if (x >= mychart["NumberOfColumns"]) {
                x = 0;
                y++;
            }
        }
        jg.paint();
    }
}

function presentProgress(scanner, mychart) {
    console.log("In presentProgress");

    var result = mychart["Result"];
    var samples = result.length;
    var prog = new Object();
    var width = mychart["NumberOfColumns"];
    var height = mychart["NumberOfRows"];
    var lines = samples / width;
    prog["Prompt"] = "<CENTER>Reading progress " + samples + " samples</CENTER>";
    prog["Answers"] = "NONE";
    presentQuery(prog);

    var jg = new jsGraphics("Canvas");    // Use the "Canvas" div for drawing
    var cursample = 0;
    var x = 0;
    var y = 0;
    for (cursample = 0; cursample < samples; cursample++) {
        var srgb = result[cursample]["sRGB"];
        var rgbx = "#" + dec2hex(srgb["R"]) + dec2hex(srgb["G"]) + dec2hex(srgb["B"]);
        jg.setColor(rgbx);
        jg.fillRect(x * 20, y * 20, 20, 20);
        x++;
        if (x >= width) {
            x = 0;
            y++;
        }
    }
    jg.paint();

    return "OK";
}


function presentProgressiSis(scanner, mychart) {
    console.log("In presentProgress");

    var result = mychart["Result"];
    var samples = result.length;
    var prog = new Object();
    var width = mychart["NumberOfColumns"];
    var height = mychart["NumberOfRows"];
    var lines = samples / width;
    prog["Prompt"] = "<CENTER>Reading progress " + samples + " samples</CENTER>";
    prog["Answers"] = "NONE";
    presentQuery(prog);

    var jg = new jsGraphics("Canvas");    // Use the "Canvas" div for drawing
    var cursample = 0;
    var x = 0;
    var y = 0;
    for (cursample = 0; cursample < samples; cursample++) {
        var srgb = result[cursample]["sRGB"];
        var rgbx = "#" + dec2hex(srgb["R"]) + dec2hex(srgb["G"]) + dec2hex(srgb["B"]);
        jg.setColor(rgbx);
        jg.fillRect(x * 20, y * 20, 20, 20);
        x++;
        if (x >= width) {
            x = 0;
            y++;
        }
    }
    jg.paint();

    return "OK";
}


function DisplayLog() {
    console.log("In DisplayLog");
    // Display the saved log messages 
    var log = plugin().getLog();
    var newHTML = 'Log:(' + log.length + ')<BR>';
    newHTML += '<PRE>\n';
    newHTML += log;
    newHTML += '</PRE><BR>';
    document.getElementById("Log").innerHTML = newHTML;
}

// Read the sync150 chart.

// chart is an object with the following information
// chart["Name"] = string name of strip or chart
// chart["Description"] = description of strip or chart
// chart["Size"] = integer number of samples
// chart["NumberOfColumns"] = integer width
// chart["NumberOfRows"] = integer height
// chart["ReferencePatch"] = array of expected values
// 
// Query is an arbitrary object that is passed through
// so the prompters and done functions know what was expected.
//
// other values may be included and are passed through to the
// callback functions
//
function scan(scanner, colorspace, chart) {

    if (MeasuredPatches.length > 0) MeasuredPatches.length = 0;
    if (g_Chart["Result"].length > 0) g_Chart["Result"].length = 0;

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
    } else {

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

    // 
    // Turn on automatic direction handling.  Options are tag[:value]
    // separated by semicolons, just like a CSS
    //
    // I think you will want this on all strip scanners.  It is very reliable
    // and really speeds things up for the user.			     
    //
    scanner["Options"] = "AutoDirection";
    scanner["SampleSource"] = "Paper";  // Choices are Paper, LCD or CRT and for the latter the chart must have 1 sample (spot)

    if (typeof chart["DeviceSettings"] == "object") {
        scanner["DeviceSettings"] = chart["DeviceSettings"]; // Move device settings to the scanner
    }

    /*var newHTML = '';
    newHTML += '<DIV ID="Canvas" style="position:relative;height:850px;width:850px;">';
    newHTML += '</DIV>';
    var span = "UploadData" + scanner["Name"];
    document.getElementById(span).innerHTML = newHTML;
    */


    if (scanner["Type"] == "iO" ||
        scanner["Type"] == "iSis") {

        var result = plugin().read(scanner, localchart, query,
                  presentChartDialog, presentProgressiSis, presentScanDoneiSis);

    } else {
        var result = plugin().read(scanner, localchart, query,
                  presentStripDialog, presentProgress, presentScanDone);

        if (result != "OK") {
            alert("Scanning Device has been unplugged");
        }

        // Display the original chart from the reference values.
        var jg = new jsGraphics("Canvas");    // Use the "Canvas" div for drawing
        var x = 0;
        var y = 0;
        for (var cursample = 0; cursample < chart["RefBase"].length; cursample++) {
            var rgbx = chart["RefBase"][cursample]["HTMLColor"];
            jg.setColor(rgbx);
            jg.fillRect(x * 20, y * 20, 20, 10);
            x++;
            if (x >= chart["NumberOfColumns"]) {
                x = 0;
                y++;
            }
        }
        jg.paint();

    }

}

function presentScannerList(ScannerList) {
    // Present the listed scanners in ScannerList to the user by updating
    // the DOM tree.  ScannerList is an array of scanners with properties.

    g_ScannerList = ScannerList;
    jQuery('#lblMessage').html("Please calibrate");
    jQuery('#btnCalibrate').css('display', '');
    //var newHTML = "";
   // alert(JSON.stringify(ScannerList));

//    for (var i = 0; i < ScannerList.length; i++) {
//        var scanner = g_ScannerList[i];
//        var scantag = "g_ScannerList[" + i + "]";
//        var features = scanner["Requirements"];
//        newHTML += '<DT>' + scanner['Name'] + ':' + scanner["Type"] + ':' + scanner['Serial'] + '</DT>';
//        newHTML += '<DD>';
//        if (features.indexOf("RequiresCalibration") >= 0 ||
//            features.indexOf("RequiresCalibration") >= 0) {
//            newHTML += '<A HREF="#" onclick="javascript:calibrateDevice(' + scantag + ')">Calibrate</A><BR>';
//        }
//        // Offer to print the chart, then the read.
//        newHTML += '<DIV STYLE="margin-left:60px">';
//        newHTML += '<A HREF="#" onclick="javascript:scan(' + scantag + ',\'Density,CIELab,CIEXYZ,sRGB\', g_Chart)">Read Sync ' + g_Chart["Name"] + ' Chart</A> ';
//        var span = "UploadData" + scanner["Name"];
//        newHTML += '<SPAN ID="' + span + '"></SPAN> ';
//        newHTML += '</DIV>';
//        newHTML += '</DD>';
//    }
//    document.getElementById('ScannerList').innerHTML = newHTML;

}

// This function requests the device list, but the result is 
// only a success or fail.  The listed callback function is
// called with the entire list once it is gathered.  
function ListDevices() {
    result = plugin().requestScannerList(presentScannerList);
    if (result != 'OK') {
        //alert("Failed to request scanner list: " + result);
        confirm("Failed to request scanner list: " + result, "Error");
        //document.getElementById('lblMessage').innerHTML = "Failed to request scanner list: " + result;
    }
}

//
// This function takes the current saved chart data and uploads it to
// the server as a json string, then pops up the result.
//
function uploadCB(msg, xml) {
    alert(msg);
    RemoveModalWindow();
}
function upload() {

    //upload and save measurement 
    saveMeasurement();
    



    //    var uploadmsg = new Object();
    //    uploadmsg["Prompt"] = "Upload of data in progress";
    //    uploadmsg["Answers"] = "NONE";
    //    OnQuery(uploadmsg);

    //    var parameters = new SOAPClientParameters();
    //    // For debugging
    //    g_senddata = JSON.stringify(g_Chart["Result"]);
    //    parameters.add("chart", "sync150");
    //    parameters.add("results", g_senddata);
    //    var result = SOAPClient.invoke("http://www.apago.com/services/ApagoCMSAPI.cgi",
    //				   'submit', parameters, true, uploadCB);
    RemoveModalWindow();
}

//
// From here on down is the modal window code.
//
var _rulesAdded = false;

function OnCancel() {

    if (MeasuredPatches.length > 0) MeasuredPatches.length = 0;
    g_Question["Result"] = 'CANCEL';
    var cancelstat = plugin().queryDone(g_Question);
    if (cancelstat == "Unhandled") {
        // Remove the modal window ourselves
        RemoveModalWindow();
    }
}

function OnQuery(question) {
    // Setup the clock to close the window with the appropriate question
    $('modalWindow').onclick = function () { OnModalWindowClick(question) };
    // Change the text in the window then display it

    var newHTML = question['Prompt'];
    g_Question = question;
    newHTML += '<span style ="position:absolute;right:0;bottom:0">';
    newHTML += '<A HREF="#" onclick="javascript:OnCancel()">';
    newHTML += 'CANCEL';
    newHTML += '</A>';
    newHTML += '</span>';

    document.getElementById('modalWindow').innerHTML = newHTML;
    $('modalWindow').style.display = $('modalBackground').style.display = 'block';

    // call once to center everything
    OnWindowResize();

    if (window.attachEvent)
        window.attachEvent('onresize', OnWindowResize);
    else if (window.addEventListener)
        window.addEventListener('resize', OnWindowResize, false);
    else
        window.onresize = OnWindowResize;

    // we won't bother with using javascript in CSS to take care
    //   keeping the window centered
    if (document.all)
        document.documentElement.onscroll = OnWindowResize;
}

function OnWindowResize() {
    // we only need to move the dialog based on scroll position if
    //   we're using a browser that doesn't support position: fixed, like < IE 7
    var left = window.XMLHttpRequest == null ? document.documentElement.scrollLeft : 0;
    var top = window.XMLHttpRequest == null ? document.documentElement.scrollTop : 0;
    var div = $('modalWindow');

    div.style.left = Math.max((left + (GetWindowWidth() - div.offsetWidth) / 2), 0) + 'px';
    div.style.top = '50px'; // Math.max((top + (GetWindowHeight() - div.offsetHeight) / 2), 0) + 'px'; before value 200 px
}

function OnModalWindowClick(question) {
    if (question["Answers"] == "NONE") {
        // If you can't answer it here, then just return
        return;
    }
    RemoveModalWindow()
    // Notify the user that we are done
    question["Result"] = "OK";
    plugin().queryDone(question);
}

function RemoveModalWindow() {
    $('modalWindow').style.display = $('modalBackground').style.display = 'none';

    if (window.detachEvent)
        window.detachEvent('onresize', OnWindowResize);
    else if (window.removeEventListener)
        window.removeEventListener('resize', OnWindowResize, false);
    else
        window.onresize = null;
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
    var width =
	document.documentElement && document.documentElement.clientWidth ||
	document.body && document.body.clientWidth ||
	document.body && document.body.parentNode && document.body.parentNode.clientWidth ||
	0;

    return width;
}

function GetWindowHeight() {
    var height =
	document.documentElement && document.documentElement.clientHeight ||
	document.body && document.body.clientHeight ||
  	document.body && document.body.parentNode && document.body.parentNode.clientHeight ||
  	0;

    return height;
}

function $(id) {
    return document.getElementById(id);
}
console.log("xychartread.js complete");
