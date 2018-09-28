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
console.log = console.log || function(){};
console.warn = console.warn || function(){};
console.error = console.error || function(){};
console.info = console.info || function(){};

OSName="Unknown OS";
if (navigator.appVersion.indexOf("Win")!=-1) OSName="Windows";
if (navigator.appVersion.indexOf("Mac")!=-1) OSName="MacOS";
if (navigator.appVersion.indexOf("X11")!=-1) OSName="UNIX";
if (navigator.appVersion.indexOf("Linux")!=-1) OSName="Linux";

if (!Array.prototype.indexOf) {
    Array.prototype.indexOf = function(obj, start) {
        for (var i = (start || 0), j = this.length; i < j; i++) {
            if (this[i] === obj) { return i; }
        }
        return -1;
    }
}
// 
// Define the Samples object for a 21x21 read
// I don't have expected values just yet.
//
console.log("Loading xychartread.js");

// NOTE:  Don't overwrite the elements of this chart in the subsequent code, even
// if you have copied it to a temporary to pass down to the plugin.  Javascript does
// something very strange and will end up clobbering the original elements during
// the callback, even if the original was only copied.  We can always copy elements
// to new entries (i.e. copy mychart["ReferenceColors"] = random331samples["RefBase"][2])
// This is the spectral values of the paper used for density
// calculations.  Saved in the spot read function
paper = new Array();
function dec2hex(i)
{
    var result = "00";
    if      (i >= 0    && i <= 15)    { result = "0" + i.toString(16); }
    else if (i >= 16   && i <= 255)   { result = i.toString(16); }
    return result;
}
function plugin0()
{
    return document.getElementById('plugin0');
}
plugin = plugin0;
function addEvent(obj, name, func)
{
    if (window.addEventListener) {
        obj.addEventListener(name, func, false); 
    } else {
        obj.attachEvent("on"+name, func);
    }
}

function load()
{
    addEvent(plugin(), 'fired', function(x){
        alert('onfired() from load(): '+x)
    });
    // The following line turns on some debugging output in the plugin
    console.log("Loading plugin version " + plugin().plugin_version);
    console.log("GUI Version 1.7");
    plugin().setDebug(2024);
    var newHTML = '<B>Fuji Color Manager Plugin ' + plugin().plugin_version + '</B>';            
    document.getElementById('ApagoCMVersion').innerHTML=newHTML;
    SDKVerHTML =plugin().sdk_version;
    document.getElementById('SDKVersion').innerHTML = SDKVerHTML;
}
function pluginLoaded() {
    alert('Plugin Loaded');
    var newHTML = '<B>Fuji Color Manager Plugin Version ' + plugin().plugin_version + '</B>';            
    document.getElementById('ApagoCMVersion').innerHTML=newHTML;
    SDKVerHTML = plugin().sdk_version;
    document.getElementById('SDKVersion').innerHTML = SDKVerHTML;
}

function addTestEvent()
{
    addEvent(plugin(), 'fired', function(x){
        alert('onfired(): '+x)
    });
}

function testEvent()
{
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
function presentQuery(question)
{
    //alert("About to call OnQuery with " + question['Prompt']);
    OnQuery(question);  
}

// This function presents the EyeOne calibrate query.  We do this
// in javascript so the web app can change everything including
// the language of the query.
function presentCalibrateDialog(scanner, myQuery) 
{  
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
function presentCalibrateDone(scanner, msg, final)
{
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
            alert(msg);
        }
    } else {
        alert("Calibration Successful");
    }
}

// Calibrate the device.  We pass in the scanner to calibrate,
//  a callback to present instructions to the user, and a callback
// to notify the user that the calibration is done.
function calibrateDevice(scanner)
{
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

// This function presents the GetPosition query.  We do this
// in javascript so the web app can change everything including
// the language of the query.
function presentGetPosDialog(scanner, myQuery) 
{  
    // Everything needed for the generic presentQuery is already 
    // filled in, however you can use this to change the actual
    // text for example to do multi-language.
    //
    // Calibrate sets the Request to standard strings regardless of language and you can
    // index pre-supplied strings (GETPOSINSTRUCTIONS, GETPOINTINSTRUCTIONS,
    // GETPOSACKNOWLEDGED, CANCELLED) like this:
    if (myQuery["Request"] == "CANCELLED") {
        RemoveModalWindow();
	return;
    }
    if (myQuery["Request"] == "GETPOSACKNOWLEDGED") {
        RemoveModalWindow();
    }
    myQuery["Prompt"] = myQuery[myQuery["Request"]];
    presentQuery(myQuery);
}
//
// The "Done" callback functions take three args.  The scanner, the original
// query structure with embedded "Result" element containing the answer,
// in this case "X" and "Y" as floats, and the string final cleanup function
// and is usally a call back to the C++ side so we can close down any remaining
// threads and such.  If it is defined, all you have to do is eval it.
//
function presentGetPosDone(scanner, query, final)
{
    // make sure the popup window is gone
    RemoveModalWindow();

    // We have to make one more call to the library.  The 
    // async request itself ran in a non-browser thread.  We need
    // the browser thread to join with the thread to keep the process state tidy.
    if (typeof final != "undefined") {
        eval(final);
    }
    //
    // Now let the user know how things ended up.
    if (typeof query == "string") {
        // This would be either "CANCELLED" or "ERROR: ..."
        // for CANCELLED, you might just want to do nothing.
        if (!(msg == "OK" || msg == "CANCELLED")) {
            alert(msg);
        }
    }
    // Save this position and move to the next corner
    if (typeof g_TheChart["UpperLeft"] == "undefined") {
	g_TheChart["UpperLeft"] = query["Result"];
	var myQuery = new Object();
	myQuery["GETPOINTINSTRUCTIONS"] = "<B><CENTER>Please move the i1 to the upper right corner of the chart<BR> and press press the i1 trigger<BR>or click anywhere in this window</CENTER></B>";
	var result = plugin().getPosition(scanner, myQuery, presentGetPosDialog, presentGetPosDone); 
	if (result != "OK") {
	    alert("Scanning Device has been unplugged");
	}
    } else if (typeof g_TheChart["UpperRight"] == "undefined") {
	g_TheChart["UpperRight"] = query["Result"];
	var myQuery = new Object();
	myQuery["GETPOINTINSTRUCTIONS"] = "<B><CENTER>Please move the i1 to the lower right corner of the chart<BR> and press press the i1 trigger<BR>or click anywhere in this window</CENTER></B>";
	myQuery["First"] = "false"; // This should be true, but for testing this turns off the POSINSTRUCTIONS query

	var result = plugin().getPosition(scanner, myQuery, presentGetPosDialog, presentGetPosDone); 
	if (result != "OK") {
	    alert("Scanning Device has been unplugged");
	}
    } else {
	g_TheChart["LowerRight"] = query["Result"];
	// We have three corners.  Figure the 4th and recaluclate the sample size
	var llx = g_TheChart["UpperLeft"]["X"] - (g_TheChart["UpperRight"]["X"] - g_TheChart["LowerRight"]["X"]);
	var lly = g_TheChart["UpperLeft"]["Y"] - (g_TheChart["UpperRight"]["Y"] - g_TheChart["LowerRight"]["Y"]);
	g_TheChart["LowerLeft"]["X"] = llx;
	g_TheChart["LowerLeft"]["Y"] = lly;

        // We can now redo the sample size ,though it should not have changed for pre-defined charts
	// unless the printed it at the wrong res.
	var cwidth = Math.sqrt(pow(g_TheChart["UpperRight"]["X"] - g_TheChart["UpperLeft"]["X"], 2.0) + 
                               pow(g_TheChart["UpperRight"]["Y"] - g_TheChart["UpperLeft"]["Y"], 2.0));
		g_TheChart["PatchSizeX"] = cwidth / g_TheChart["NumberOfColumns"];
	var cheight = Math.sqrt(pow(g_TheChart["LowerRight"]["X"] - g_TheChart["UpperRight"]["X"], 2.0) + 
                               pow(g_TheChart["LowerRight"]["Y"] - g_TheChart["UpperRight"]["Y"], 2.0));
	g_TheChart["PatchSizeY"] = cheight / g_TheChart["NumberOfRows"];
    }
}

// This function provides an interface to fetch three corners of a chart from an iO device
// and update the chart definition so alignment of the chart isn't important.
// This function just starts up the fetching of the corners.  The presentGetPosDone will
// look at the results and decide if it needs to do additional corners
function getChartLocation(scanner)
{
    var myQuery = new Object();
    delete(g_TheChart["UpperLeft"]);
    delete(g_TheChart["UpperRight"]);
    delete(g_TheChart["LowerLeft"]);
    delete(g_TheChart["LowerRight"]);
    myQuery["GETPOSINSTRUCTIONS"] = "<B><CENTER>Please place your chart on the iO table and click in this window to continue</CENTER></B>";
    myQuery["GETPOINTINSTRUCTIONS"] = "<B><CENTER>Please move the i1 to the upper left corner of the chart<BR> and press press the i1 trigger<BR>or click anywhere in this window</CENTER></B>";
    myQuery["First"] = "true";

    var result = plugin().getPosition(scanner, myQuery, presentGetPosDialog, presentGetPosDone); 
    if (result != "OK") {
      alert("Scanning Device has been unplugged");
    }
}

// This function presents the EyeOne strip query.  We do this
// in javascript so the web app can change everything including
// the language of the query.
function presentStripDialog(scanner, chart, query) 
{  
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
//
// The "Done" callback functions take three args.  The scanner, 
// the prompt object and the final function.  If the final function
// is a function object it is called, if it is a string then
// it is evaluated.  This generally will be a call back to the
// C++ side to do final thread and memory cleanup. Sample values
// must be retriveved before the final call.
//

function presentScanDone(scanner, mychart, final)
{
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
    var newHTML = "";
    newHTML += "<UL>";  
    
    var results = mychart["Result"];
    var idx = mychart["NumberOfRows"];
    
    if (!(scanner["Type"] == "iO" || scanner["Type"] == "iSis")) {
	if (results.length != mychart["NumberOfColumns"]) {
	    // Prompt the user to try again
	    alert("Failed to read expected number of samples" + results.length + " vs " + mychart["NumberOfColumns"]); 
	    if (typeof final == "string") {
		eval(final);
	    } else if (typeof final == "object") {
		final();
	    }
	    
	    var query = new Object();
	    query["Success"] = "Samples Complete";
	    query["Fail"] = "Failed to read samples";
	    delete mychart["Result"];
	    
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
    if (typeof g_TheChart["Result"] == "undefined") {
        // The final result saving place does not exist.  We create it
        // with all colorspaces requested right now so we don't have to
        // worry about it later.
        g_TheChart["Result"] = new Array();
    }
    g_TheChart["Result"] = g_TheChart["Result"].concat(results);
    // Check to see if this was the last strip to be read.
    var next = -1;
    if (idx < (mychart["NumberOfRows"] - 1)) {
	next = idx + 1;
    }
    // If next == 1, then we add the canvas, otherwise we add the uplaod (

    var jg = new jsGraphics("Canvas");    // Use the "Canvas" div for drawing
    
    // repaint the entire thing, for simplicity
    var x = 0;
    var y = 0;
    results = g_TheChart["Result"]; 

    if (scanner["Name"] == "iO") {
	// If the number of results is expected, something failed
	if (results.length != g_TheChart["Size"]) {
	    alert("Failed to read expected(" + g_TheChart["Size"] + " samples, got " + results.length);
	}
	var xinc = 1;
	var yinc = 1;
	var limit = g_TheChart["NumberOfColumns"];
	if (g_TheChart["ReadDirection"] == "LeftToRight" ||
	    g_TheChart["ReadDirection"] == "RightToLeft") {
	    if (g_TheChart["FirstSample"] == "TopRight") {
		x = limit - 1;
		limit = -1;
		xinc = -1;
	    }
	}
	var startx = x;
	for (var cursample = 0; cursample < results.length; cursample++) {
	    var srgb = results[cursample]["sRGB"];
	    var rgbx = "#" + dec2hex(srgb["R"]) + dec2hex(srgb["G"]) + dec2hex(srgb["B"]);
	    jg.setColor(rgbx);
	    jg.fillRect(x*20, (y*20)+10, 20, 10);
	    x += xinc;
	    if (x == limit) {
		x = startx;
		y += yinc;
		//alert("y incremented to " + y);
	    }
	}
    } else {
	for (var cursample = 0; cursample < results.length; cursample++) {
	    var srgb = results[cursample]["sRGB"];
	    var rgbx = "#" + dec2hex(srgb["R"]) + dec2hex(srgb["G"]) + dec2hex(srgb["B"]);
	    jg.setColor(rgbx);
	    jg.fillRect(x*20, (y*20)+10, 20, 10);
	    y++;
	    if (y >= mychart["NumberOfColumns"]) {
		y = 0;
		x++;
	    }
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
	delete mychart["Result"];
                        
        if (typeof mychart["RefBase"] != "undefined") {
	    mychart["ReferencePatch"] = mychart["RefBase"].slice(next*mychart["NumberOfColumns"], (next + 1)*mychart["NumberOfColumns"]);
	}
	
        //alert(JSON.stringify(mychart));
        var result = plugin().read(scanner, mychart, query,
				   presentStripDialog, presentProgress, presentScanDone); 
	
	if (result != "OK") {
	  alert("Scanning Device has been unplugged");
	}
    }
}

function presentProgress(scanner, mychart) 
{
    // This is a simple version that rebuild the entire graph every time.
    // It might be more efficient to only repaint the new data.
    console.log("In presentProgress");

    var result = mychart["Result"];
    var samples = result.length;    
    var prog = new Object();
    var width = mychart["NumberOfColumns"];
    var height = mychart["NumberOfRows"];       
    var lines = samples / width;
    prog["Prompt"] = "<CENTER>Reading progress " + samples + " samples</CENTER>";
    prog["Answers"] = "NONE";
    prog["Class"] = "Progress";
    prog["Scanner"] = scanner;
    presentQuery(prog);

    var jg = new jsGraphics("Canvas");    // Use the "Canvas" div for drawing
    var cursample = 0;
    // We don't get progress callbacks for an i1, only the iO
    var x = 0;
    var y = 0;
    var xinc = 1;
    var yinc = 1;
    var limit = width;
    if (mychart["ReadDirection"] == "LeftToRight" ||  
	mychart["ReadDirection"] == "RightToLeft") {
	if (mychart["FirstSample"] == "TopRight") {
	    x = width - 1;
	    if (mychart["ReadDirection"] == "RightToLeft") {
		limit = -1;
		xinc = -1;
	    } 
	} 
	var startx = x;
	for (cursample = 0; cursample < samples; cursample++) {
	    var srgb = result[cursample]["sRGB"];
	    var rgbx = "#" + dec2hex(srgb["R"]) + dec2hex(srgb["G"]) + dec2hex(srgb["B"]);
	    jg.setColor(rgbx);
	    jg.fillRect(x*20, (y*20)+10, 20, 10);
	    x += xinc;
	    if (x == limit) {
		x = startx;
		y += yinc;
	    }
	}
    } else {
	// This would be top to bottom/bottom to top reading
    }
    jg.paint();

    return "OK";			
}

function DisplayLog()
{
    console.log("In DisplayLog");
    // Display the saved log messages 
    var log = plugin().getLog();
    var newHTML = 'Log:(' + log.length + ')<BR>';
    newHTML += '<PRE>\n';
    newHTML += log;
    newHTML += '</PRE><BR>';
    document.getElementById("Log").innerHTML=newHTML;
}

// Read a chart 
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
function scan(scanner, colorspace, chart)
{
    if (colorspace.indexOf("Density") != -1) {
        alert("You must first spot read the substrate before attempting a Density read");
        return;
    }
    var localchart = chart;
    localchart["ReadCS"] = colorspace;
    if (scanner["Type"] == "iO" || 
        scanner["Type"] == "iSis") {
        // This device will read the entire chart in a single go.
	if (typeof chart["RefBase"] != "undefined") {
	    localchart["ReferencePatch"] = chart["RefBase"];
	}
    } else {
        localchart["CurrentStrip"] = chart["RowNames"][0];
	if (typeof chart["RefBase"] != "undefined") {
	    localchart["ReferencePatch"] = chart["RefBase"].slice(0, chart["NumberOfColumns"]);
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

    if (typeof chart["DeviceSettings"] != "undefined") {
	scanner["DeviceSettings"] = chart["DeviceSettings"];
    } else {
	scanner["DeviceSettings"] = new Object();
    }
    // This is to work around an I1 SDK bug where they delete the first sample
    scanner["DeviceSettings"]["RemoveScanBoundaries"] = "False";
    // Set the scan speed based on sample size.  Larger patches can be scanned faster
    // This is a relatively simple linear approach based on the width of a patch.
    // You could get as complex as you like.
    if (scanner["Type"] == "iO") {     
        scanner["DeviceSettings"]["I1TScanSpeed"] = Math.max(600, chart["PatchSizeX"] * 30);
    } 
    // Set the activation key. This changes so this fixed string won't work.  Use your own code here
    scanner["ActivationKey"] = "Your Activation String";

    var newHTML = '';
    newHTML += '<DIV ID="Canvas" style="position:relative;height:618px;width:440px;">';
    newHTML += '</DIV>';
    var span = "UploadData"+scanner["Name"];
    document.getElementById(span).innerHTML=newHTML;
    // Clear the final charts results so we don't just keep adding and adding
    g_TheChart["Result"] = new Array();

    var result = plugin().read(scanner, localchart, query,
                  presentStripDialog, presentProgress, presentScanDone); 
    if (result != "OK") {
      alert("Scanning Device has been unplugged");
    }
    // Display the original chart from the reference values.
    // We draw the chart like we would scan it.  This will be different for an
    // I1 vs the iO table.
    //
    var jg = new jsGraphics("Canvas");    // Use the "Canvas" div for drawing
    if (scanner["Name"] == "iO") {
	var x = 0;
	var y = 0;
	var xinc = 1;
	var yinc = 1;
	var limit = chart["NumberOfColumns"];
	if (chart["ReadDirection"] == "LeftToRight" ||  
	    chart["ReadDirection"] == "RightToLeft") {
	    if (chart["FirstSample"] == "TopRight") {
		// This has to be TopRight going RightToLeft 
		x = limit - 1;
		limit = -1;
		xinc = -1;
	    } 
	    var startx = x;
	    for (var cursample = 0; cursample < chart["RefBase"].length; cursample++) {
		var rgbx = chart["RefBase"][cursample]["HTMLColor"];
		jg.setColor(rgbx);
		jg.fillRect(x*20, (y*20), 20, 10);
		x += xinc;
		if (x == limit) {
		    x = startx;
		    y += yinc;
		}
	    }
	}
    } else {
	// Assume an i1 where the chart is sideways
	var x = 0;
	var y = 0;
	for (var cursample = 0; cursample < chart["RefBase"].length; cursample++) {
	    var rgbx = chart["RefBase"][cursample]["HTMLColor"];
	    jg.setColor(rgbx);
	    jg.fillRect(x*20, y*20, 20, 10);
	    y++;
	    if (y >= chart["NumberOfColumns"]) {
		y = 0;
		x++;
	    }
	}
    }
    jg.paint();
}

// This function presents the EyeOne calibrate query.  We do this
// in javascript so the web app can change everything including
// the language of the query.
function presentPointDialog(scanner, chart, query) 
{  
    // 
    // This should just present the place sample dialog, click okay then
    // move the arm to the spot and click
    //
    if (scanner["Type"] == "iO") {
	if (query["Request"] == "READINSTRUCTIONS") {
	    query["Prompt"] = "<B><CENTER>Please place the chart on the platten and click to continue</CENTER></B>";
	} else {
	    query["Prompt"] = "<B><CENTER>Please place the EyeOne over the sample and press the trigger or click in this window.</CENTER></B>";
	}
    }
    presentQuery(query);
}

//
// The "Done" callback functions take three args.  The scanner, 
// the prompt object and the final function.  If the final function
// is a function object it is called, if it is a string then
// it is evaluated.  This generally will be a call back to the
// C++ side to do final thread and memory cleanup. Sample values
// must be retriveved before the final call.
//

function presentPointDone(scanner, mychart, final)
{
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
    var results = mychart["Result"];
    var idx = mychart["NumberOfRows"];
    
    var srgb = results[0]["sRGB"];
    var rgbx = "#" + dec2hex(srgb["R"]) + dec2hex(srgb["G"]) + dec2hex(srgb["B"]);
    alert("Read spot with RGB result = " + rgbx);
    //
    // Proceed to thread clean up.  For spots, do this after any user interaction
    // because it will return the arm to the home position.  This gives them
    // time to move
    //
    if (typeof final == "string") {
	eval(final);
    } else if (typeof final == "object") {
	final();
    } 

}

// Read a point
// chart is a synthetic chart we define on the fly with:
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
function pointscan(scanner, colorspace)
{
    if (colorspace.indexOf("Density") != -1) {
        alert("You must first spot read the substrate before attempting a Density read");
        return;
    }
    var localchart = new Object();
    localchart["Name"] = "Spot Read";
    localchart["Size"] = 1;
    localchart["NumberOfColumns"] = 1;
    localchart["NumberOfRows"] = 1;
    localchart["PatchWidth"] = 1;
    localchart["PatchHeight"] = 1;
    localchart["ReadCS"] = colorspace;
    localchart["Mode"] = "Spot";

    var query = new Object();
    query["Success"] = "Sample Complete";
    query["Fail"] = "Failed to read sample";

    scanner["SampleSource"] = "Paper";  // Choices are Paper, LCD or CRT and for the latter the chart must have 1 sample (spot)

    if (typeof localchart["DeviceSettings"] != "undefined") {
	scanner["DeviceSettings"] = chart["DeviceSettings"];
    } else {
	scanner["DeviceSettings"] = new Object();
    }

    var newHTML = '';

    // Clear the final charts results so we don't just keep adding and adding
    localchart["Result"] = new Array();

    var result = plugin().read(scanner, localchart, query,
                  presentPointDialog, presentProgress, presentPointDone); 

    if (result != "OK") {
      alert("Scanning Device has been unplugged");
    }
}

// This acts very much like a combined getChartLocation+scan.  The idea here is to
// query two points, create a chart definition around these two points and call
// scan so the single strip between those two points can be read.
var g_stripleft;
var g_stripright;
function presentGetStripPosDialog(scanner, myQuery) 
{  
    // Everything needed for the generic presentQuery is already 
    // filled in, however you can use this to change the actual
    // text for example to do multi-language.
    //
    // Calibrate sets the Request to standard strings regardless of language and you can
    // index pre-supplied strings (GETPOSINSTRUCTIONS, GETPOINTINSTRUCTIONS,
    // GETPOSACKNOWLEDGED, CANCELLED) like this:
    if (myQuery["Request"] == "CANCELLED") {
        RemoveModalWindow();
	return;
    }
    if (myQuery["Request"] == "GETPOSACKNOWLEDGED") {
        RemoveModalWindow();
    }
    myQuery["Prompt"] = myQuery[myQuery["Request"]];
    presentQuery(myQuery);
}

// Ask for the second point if we don't have it already.
// If we do,  then call scan.
function presentGetStripPosDone(scanner, query, final) 
{
    // make sure the popup window is gone
    RemoveModalWindow();

    // We have to make one more call to the library.  The 
    // calibrate itself ran in a non-browser thread.  We need
    // the browser thread to join with the calibrate thread to
    // keep the process state tidy.
    // the library that the poll thread can be 

    if (typeof final != "undefined") {
        eval(final);
    }
    //
    // Now let the user know how things ended up.
    if (typeof query == "string") {
        // This would be either "CANCELLED" or "ERROR: ..."
        // for CANCELLED, you might just want to do nothing.
        if (!(msg == "OK" || msg == "CANCELLED")) {
            alert(msg);
        }
    }
    // Save this position and move to the next corner
    //alert("In PosDone type of g_stripleft = " + typeof g_stripleft);
    if (typeof g_stripleft != "object") {
	g_stripleft = query["Result"];
	var myQuery = new Object();
	myQuery["GETPOINTINSTRUCTIONS"] = "<B><CENTER>Please move the i1 to the right of the strip<BR>and press press the i1 trigger<BR>or click anywhere in this window</CENTER></B>";
	var result = plugin().getPosition(scanner, myQuery, presentGetStripPosDialog, presentGetStripPosDone); 
	if (result != "OK") {
	    alert("Scanning Device has been unplugged");
	}
    } else {
        var newHTML = '';
	newHTML += '<DIV ID="Canvas" style="position:relative;height:618px;width:440px;">';
	newHTML += '</DIV>';
	var span = "UploadData"+scanner["Name"];
	document.getElementById(span).innerHTML=newHTML;

	g_stripright = query["Result"];

	// We have two points.  Create a simple chart that represents this strip
	var ulx = g_stripleft["X"];
	var uly = g_stripleft["Y"];
	var urx = g_stripright["X"];
	var ury = g_stripright["Y"];
	var llx = g_stripleft["X"] + .1;
	var lly = g_stripleft["Y"] + .1;
	var lrx = g_stripright["X"] - .1;
	var lry = g_stripright["Y"] + .1;
        confirm("Strip location from (" + ulx + "," + uly + ") to (" + urx + "," + ury + ")");

	var localchart = new Object();
	localchart["Name"] = "Strip Read";
	localchart["Size"] = 25;
	// This does need to be known, without it how would we know if we got the right number
	// If you need to, ask the user
	localchart["NumberOfColumns"] = 25; 
	localchart["NumberOfRows"] = 1;
	localchart["PatchSizeX"] = 1;  // made up, won't matter
	localchart["PatchSizeY"] = 1;
	localchart["ReadCS"] = "CIELab,sRGB";
	localchart["UpperLeft"] = new Object();
	localchart["UpperLeft"]["X"] = ulx;
	localchart["UpperLeft"]["Y"] = uly;
	localchart["UpperRight"] = new Object();
	localchart["UpperRight"]["X"] = urx;
	localchart["UpperRight"]["Y"] = ury;
	localchart["LowerLeft"] = new Object();
	localchart["LowerLeft"]["X"] = llx;
	localchart["LowerLeft"]["Y"] = lly;
	localchart["LowerRight"] = new Object();
	localchart["LowerRight"]["X"] = lrx;
	localchart["LowerRight"]["Y"] = lry;

	var query = new Object();
	query["Success"] = "Strip Complete";
	query["Fail"] = "Failed to read strip";

	scanner["SampleSource"] = "Paper";  // Choices are Paper, LCD or CRT and for the latter the chart must have 1 sample (spot)

	if (typeof scanner["DeviceSettings"] == "undefined") {
	    scanner["DeviceSettings"] = new Object();
	}
	scanner["DeviceSettings"]["RemoveScanBoundaries"] = "False";
	scanner["DeviceSettings"]["Colorimetric.Illumination"] = "D50";        
	scanner["DeviceSettings"]["Colorimetric.Observer"] = "TwoDegree";        
	scanner["DeviceSettings"]["Colorimetric.WhiteBase"] = "Absolute";        
	scanner["DeviceSettings"]["Colorimetric.DensityStandard"] = "ANSIA";        
	scanner["DeviceSettings"]["Colorimetric.DensityFilterMode"] = "Auto";        

	// Clear the final charts results so we don't just keep adding and adding
	localchart["Result"] = new Array();
	
	var result = plugin().read(scanner, localchart, query,
				   presentStripDialog, presentProgress, presentStripDone); 
	
	if (result != "OK") {
	    alert("Scanning Device has been unplugged");
	}
    }
}

// This is a stripped down version of the prior function.  The coordinates are hard
// coded to see if the get position is causing the oddities I'm seeing
function fixedGetStrip(scanner)
{

    // We have to make one more call to the library.  The 
    // calibrate itself ran in a non-browser thread.  We need
    // the browser thread to join with the calibrate thread to
    // keep the process state tidy.
    // the library that the poll thread can be 

    var newHTML = '';
    newHTML += '<DIV ID="Canvas" style="position:relative;height:618px;width:440px;">';
    newHTML += '</DIV>';
    var span = "UploadData"+scanner["Name"];
    document.getElementById(span).innerHTML=newHTML;


	// We have two points.  Create a simple chart that represents this strip
	var ulx = 121.9;
	var uly = 217.3;
	var urx = -120.1;
	var ury = 205.9;
	var llx = ulx + .1;
	var lly = uly + .1;
	var lrx = urx - .1;
	var lry = ury + .1;
        //alert("Strip location from (" + ulx + "," + uly + ") to (" + urx + "," + ury + ")");

	var localchart = new Object();
	localchart["Name"] = "Strip Read";
	localchart["Size"] = 27;
	localchart["NumberOfColumns"] = 27; // This does need to be known.  
	localchart["NumberOfRows"] = 1;
	localchart["PatchSizeX"] = 1;  // made up, won't matter
	localchart["PatchSizeY"] = 1;
	localchart["ReadCS"] = "CIELab,sRGB";
	localchart["UpperLeft"] = new Object();
	localchart["UpperLeft"]["X"] = ulx;
	localchart["UpperLeft"]["Y"] = uly;
	localchart["UpperRight"] = new Object();
	localchart["UpperRight"]["X"] = urx;
	localchart["UpperRight"]["Y"] = ury;
	localchart["LowerLeft"] = new Object();
	localchart["LowerLeft"]["X"] = llx;
	localchart["LowerLeft"]["Y"] = lly;
	localchart["LowerRight"] = new Object();
	localchart["LowerRight"]["X"] = llx;
	localchart["LowerRight"]["Y"] = lly;

	var query = new Object();
	query["Success"] = "Strip Complete";
	query["Fail"] = "Failed to read strip";

	scanner["SampleSource"] = "Paper";  // Choices are Paper, LCD or CRT and for the latter the chart must have 1 sample (spot)

	if (typeof scanner["DeviceSettings"] == "undefined") {
	    scanner["DeviceSettings"] = new Object();
	}
	scanner["DeviceSettings"]["RemoveScanBoundaries"] = "False";
	scanner["DeviceSettings"]["Colorimetric.Illumination"] = "D50";        
	scanner["DeviceSettings"]["Colorimetric.Observer"] = "TwoDegree";        
	scanner["DeviceSettings"]["Colorimetric.WhiteBase"] = "Absolute";        
	scanner["DeviceSettings"]["Colorimetric.DensityStandard"] = "ANSIA";        
	scanner["DeviceSettings"]["Colorimetric.DensityFilterMode"] = "Auto";        

	// Clear the final charts results so we don't just keep adding and adding
	localchart["Result"] = new Array();
	
	var result = plugin().read(scanner, localchart, query,
				   presentStripDialog, presentProgress, presentStripDone); 
	
	if (result != "OK") {
	    alert("Scanning Device has been unplugged");
	}
}

function presentStripDone(scanner, mychart, final)
{
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
    if (typeof final == "string") {
	eval(final);
    } else if (typeof final == "object") {
	final();
    }

    var newHTML = "";
    newHTML += "<UL>";  
    
    var results = mychart["Result"];
    var jg = new jsGraphics("Canvas");    // Use the "Canvas" div for drawing
    if (scanner["Name"] == "iO") {
	var x = 0;
	var y = 0;
	var xinc = 1;
	var yinc = 1;
	var limit = mychart["NumberOfColumns"];

	var startx = x;
	for (var cursample = 0; cursample < results.length; cursample++) {
	    var srgb = results[cursample]["sRGB"];
	    var rgbx = "#" + dec2hex(srgb["R"]) + dec2hex(srgb["G"]) + dec2hex(srgb["B"]);
	    jg.setColor(rgbx);
	    jg.fillRect(x*20, (y*20), 20, 20);
	    x += xinc;
	    if (x == limit) {
		x = startx;
		y += yinc;
	    }
	}
    }
    jg.paint(); 

}

function getStrip(scanner)
{
    // I have tried delete here. It does not work.  The object below in the alert will still be an object
    g_stripleft = "";
    g_stripright = "";

    var myQuery = new Object();
    myQuery["GETPOSINSTRUCTIONS"] = "<B><CENTER>Please place your chart on the iO table and click in this window to continue</CENTER></B>";
    myQuery["GETPOINTINSTRUCTIONS"] = "<B><CENTER>Please move the i1 to the left of the desired strip<BR>and press press the i1 trigger<BR>or click anywhere in this window</CENTER></B>";
    myQuery["First"] = "true"; // This should be true, but for testing this turns off the POSINSTRUCTIONS query

    var result = plugin().getPosition(scanner, myQuery, presentGetStripPosDialog, presentGetStripPosDone); 
    if (result != "OK") {
      alert("Scanning Device has been unplugged");
    }
}

function presentScannerList(ScannerList) {
    // Present the listed scanners in ScannerList to the user by updating
    // the DOM tree.  ScannerList is an array of scanners with properties.

    g_ScannerList = ScannerList;
    var newHTML = "";

    for (var i = 0; i < ScannerList.length; i++) {
        var scanner = g_ScannerList[i];     
	if (scanner["State"] != "Deleted") {
	    var scantag = "g_ScannerList["+i+"]";    
	    var features = scanner["Requirements"];
	    newHTML += '<DT>'+scanner['Name']+':'+scanner["Type"]+':'+scanner['Serial']+'</DT>';
	    if (features.indexOf("RequiresCalibration") >= 0) {
		newHTML += '<DD>';
		newHTML += '<A HREF="#" onclick="javascript:calibrateDevice('+scantag+')">Calibrate</A><BR>';
		newHTML += '</DD>';
	    }
	    features = scanner["Features"];
	    if (features.indexOf("GetPosition") >= 0) {
		
		newHTML += '<DD>';
		newHTML += '<A HREF="#" onclick="javascript:getStrip('+scantag+')">Single Strip Read</A><BR>';
		newHTML += '</DD>';
		newHTML += '<DD>';
		newHTML += '<A HREF="#" onclick="javascript:getChartLocation('+scantag+')">Get Chart Position</A><BR>';
		newHTML += '</DD>';
	    }
	    newHTML += '<DD>';
	    newHTML += '<A HREF="#" onclick="javascript:pointscan('+scantag+', \'CIELab,sRGB\')">Single Spot Read</A><BR>';
	    newHTML += '</DD>';
	    // Offer to print the chart, then the read.
	    newHTML += '<DD>';
	    newHTML += '<DIV STYLE="margin-left:60px">';
	    if (typeof g_TheChart["PrintName"] != "undefined") {
		newHTML += '<A HREF="' + g_TheChart["PrintLink"] + '">' + g_TheChart["PrintName"] + '</A> &mdash; ';
	    }
	    
	    newHTML += '<A HREF="#" onclick="javascript:scan('+scantag+',\'CIELab,sRGB,DE94\', g_TheChart)"> Read In ' + g_TheChart["ShortName"] + ' Chart</A> ';
	    var span = "UploadData"+scanner["Name"];
	    newHTML += '<SPAN ID="' + span + '"></SPAN> ';
	    newHTML += '</DIV>';
	    newHTML += '</DD>';
	}
    }
    document.getElementById('ScannerList').innerHTML=newHTML;

} 

// This function requests the device list, but the result is 
// only a success or fail.  The listed callback function is
// called with the entire list once it is gathered.  
function ListDevices()
{
    result = plugin().requestScannerList(presentScannerList);
    if (result != 'OK') {
        alert("Failed to request scanner list: " + result);
    }
}

//
// This function takes the current saved chart data and uploads it to
// the server as a json string, then pops up the result.
//
function uploadCB(msg, xml) 
{
    alert(msg);
    RemoveModalWindow();
}
function upload()
{
    var uploadmsg = new Object();
    uploadmsg["Prompt"] = "Upload of data in progress";
    uploadmsg["Answers"] = "NONE";
    OnQuery(uploadmsg);

    var parameters = new SOAPClientParameters();
    // For debugging
    g_senddata = JSON.stringify(g_TheChart["Result"]);
    parameters.add("chart", "Random331");
    parameters.add("results", g_senddata);
    var result = SOAPClient.invoke("http://www.apago.com/services/ApagoCMSAPI.cgi",
				   'submit', parameters, true, uploadCB);
    RemoveModalWindow();
}

//
// From here on down is the modal window code.
//
var _rulesAdded = false;

function OnCancel()
{
    g_Question["Result"] = 'CANCEL';
    var cancelstat = plugin().queryDone(g_Question);
    if (cancelstat == "Unhandled") {
        // Remove the modal window ourselves
        RemoveModalWindow();
    }
}

function OnQuery(question)
{
    // Setup the clock to close the window with the appropriate question
    $('modalWindow').onclick = function() { OnModalWindowClick(question) };
    // Change the text in the window then display it
    
    var newHTML = question['Prompt'];
    g_Question = question;
    newHTML += '<span style ="position:absolute;right:0;bottom:0">';
    newHTML += '<A HREF="#" onclick="javascript:OnCancel()">';
    newHTML += 'CANCEL';
    newHTML += '</A>';
    newHTML += '</span>';
    
    document.getElementById('modalWindow').innerHTML=newHTML;
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

function OnWindowResize()
{
    // we only need to move the dialog based on scroll position if
    //   we're using a browser that doesn't support position: fixed, like < IE 7
    var left = window.XMLHttpRequest == null ? document.documentElement.scrollLeft : 0;
    var top = window.XMLHttpRequest == null ? document.documentElement.scrollTop : 0;
    var div = $('modalWindow');
    
    div.style.left = Math.max((left + (GetWindowWidth() - div.offsetWidth) / 2), 0) + 'px';
    div.style.top = '200px'; // Math.max((top + (GetWindowHeight() - div.offsetHeight) / 2), 0) + 'px';
}

function OnModalWindowClick(question)
{
    if (question["Answers"] == "NONE") {
        // If you can't answer it here, then just return
	return;
    }
    RemoveModalWindow()
    // Notify the user that we are done
    question["Result"] = "OK";
    plugin().queryDone(question);
}

function RemoveModalWindow()
{
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

function RemoveSelectSpans()
{
    var selects = document.getElementsByTagName('select');
    
    for (var i = 0; i < selects.length; i++)
    {
	var select = selects[i];
	
	if (select.clientWidth == 0 || select.clientHeight == 0 || 
	    select.nextSibling == null || select.nextSibling.className != 'selectReplacement')
	{
	    continue;
	}
	
	select.parentNode.removeChild(select.nextSibling);
	select.style.display = select.cachedDisplay;
    }
}

function ReplaceSelectsWithSpans()
{
    var selects = document.getElementsByTagName('select');
    
    for (var i = 0; i < selects.length; i++)
    {
	var select = selects[i];
	
	if (select.clientWidth == 0 || select.clientHeight == 0 || 
	    select.nextSibling == null || select.nextSibling.className == 'selectReplacement')
	{
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

function GetWindowWidth()
{
    var width =
	document.documentElement && document.documentElement.clientWidth ||
	document.body && document.body.clientWidth ||
	document.body && document.body.parentNode && document.body.parentNode.clientWidth ||
	0;
    
    return width;
}

function GetWindowHeight()
{
    var height =
	document.documentElement && document.documentElement.clientHeight ||
	document.body && document.body.clientHeight ||
  	document.body && document.body.parentNode && document.body.parentNode.clientHeight ||
  	0;
    
    return height;
}

function $(id)
{
    return document.getElementById(id);
}
console.log("xychartread.js complete");
