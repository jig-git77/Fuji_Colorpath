// -*- mode: javascript; indent-tabs-mode: nil; c-basic-offset: 4 -*-
// 
// This is a simplified example for just calibration and reading an X/Y chart.
// using the Techkon SpecroJet scanner.  The sample chart is the same one used
// for the iSis since this scanner does not use contrast separators like the i1
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
console.log("Loading sjread.js");

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
    var newHTML = '<B>Apago Color Manager Plugin Version ' + plugin().plugin_version + '</B>';            
    document.getElementById('ApagoCMVersion').innerHTML=newHTML;
    SDKVerHTML =plugin().sdk_version;
    document.getElementById('SDKVersion').innerHTML = SDKVerHTML;
}
function pluginLoaded() {
    //alert('Plugin Loaded');
    var newHTML = '<B>Apago Color Manager Plugin Version ' + plugin().plugin_version + '</B>';            
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

// Calibrate the device in absolute mode.  We pass in the scanner to calibrate,
//  a callback to present instructions to the user, and a callback
// to notify the user that the calibration is done.
function calibrateDeviceAbs(scanner)
{
    var myQuery = new Object();
    myQuery["CALIBRATEINSTRUCTIONS"] = "<B><CENTER>Please place SpectroJet in the calibration cradle<BR>and click anywhere in this window</CENTER></B>";
    myQuery["CALIBRATEACKNOWLEDGED"] = "<B><CENTER>Calibration In Progress</CENTER></B>";
    scanner["SampleSource"] = "Absolute";
    var result = plugin().calibrateDevice(scanner, myQuery, presentCalibrateDialog, presentCalibrateDone); 
    if (result != "OK") {
      alert("Scanning Device has been unplugged");
    }
}

// Calibrate the device in paper mode  We pass in the scanner to calibrate,
//  a callback to present instructions to the user, and a callback
// to notify the user that the calibration is done.
function calibrateDevicePaper(scanner)
{
    var myQuery = new Object();
    myQuery["CALIBRATEINSTRUCTIONS"] = "<B><CENTER>Please place SpectroJet on the paper sample<BR>and press press the trigger and hold till it beeps<BR>or click anywhere in this window</CENTER></B>";
    myQuery["CALIBRATEACKNOWLEDGED"] = "<B><CENTER>Calibration In Progress</CENTER></B>";
    scanner["SampleSource"] = "Paper";
    var result = plugin().calibrateDevice(scanner, myQuery, presentCalibrateDialog, presentCalibrateDone); 
    if (result != "OK") {
      alert("Scanning Device has been unplugged");
    }
}

// This function presents the EyeOne calibrate query.  We do this
// in javascript so the web app can change everything including
// the language of the query.
function presentSpotDialog(scanner, chart, query) 
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
      query["Prompt"] = "<B><CENTER>Spot read in progress</CENTER></B>";
    } else {
      query["Prompt"] = "<B><CENTER>Please read place the scanner over the spot" + chart["CurrentStrip"] + ".<BR>press the trigger.</CENTER></B>";
    }
    presentQuery(query);
}

// This function presents the EyeOne calibrate query.  We do this
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
      query["Prompt"] = "<B><CENTER>Please read place the scanner over the strip " + chart["CurrentStrip"] + ".<BR>press and hold the trigger while sliding the scanner across the strip.</CENTER></B>";
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

function presentSpotDone(scanner, mychart, final)
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
    alert("Spotread complete"); 
}

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
	  
                scanner["DeviceSettings"] = new Object();
                scanner["DeviceSettings"]["ColorimetricIlluminant"] = "D50";        
                scanner["DeviceSettings"]["RemoveScanBoundaries"] = "False";        

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
	    mychart["Result"] = null;
	    
            scanner["DeviceSettings"] = new Object();
            scanner["DeviceSettings"]["ColorimetricIlluminant"] = "D50";        
            scanner["DeviceSettings"]["RemoveScanBoundaries"] = "False";        

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
    if (typeof sync150["Result"] == "undefined") {
        // The final result saving place does not exist.  We create it
        // with all colorspaces requested right now so we don't have to
        // worry about it later.
        sync150["Result"] = new Array();
    }
    sync150["Result"] = sync150["Result"].concat(results);
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
	jg.fillRect(x*20, (y*20)+10, 20, 10);
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
                        
        if (typeof mychart["RefBase"] != "undefined") {
	    mychart["ReferencePatch"] = mychart["RefBase"].slice(next*mychart["NumberOfColumns"], (next + 1)*mychart["NumberOfColumns"]);
	}
        scanner["DeviceSettings"] = new Object();
        scanner["DeviceSettings"]["ColorimetricIlluminant"] = "D50";        
        scanner["DeviceSettings"]["ColorimetricWhiteReference"] = "ABSOLUTE";
        scanner["DeviceSettings"]["RemoveScanBoundaries"] = "True";        
	
        //alert(JSON.stringify(mychart));
        var result = plugin().read(scanner, mychart, query,
				   presentStripDialog, presentProgress, presentScanDone); 
	
	if (result != "OK") {
	  alert("Scanning Device has been unplugged");
	}
    }
}

function presentProgress(scanner, percent) 
{
    return "OK";			
}
// Read the sync150 chart
//  
// chart is an object with the following information
// chart["Name"] = string name of strip or chart
// chart["Description"] = description of strip or chart
// chart["Size"] = integer number of samples
// chart["Width"] = integer width
// chart["Height"] = integer height
// chart["Values"] = array of expected Lab values
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
    localchart["CurrentStrip"] = sync150["RowNames"][0];
    localchart["ReferenceColors"] = sync150["RefBase"][0];
    // Create a global variable for the result multi-strip chart
    // Each row we get, we place in the gChart Results element.

    // Display the expected values
    var jg = new jsGraphics("Canvas");    // Use the "Canvas" div for drawing
    var cursample = 0;
    var x = 0;
    var y = 0;
    for (cursample = 0; cursample < chart["RefBase"].length; cursample++) {
	var rgbx = chart["RefBase"][cursample]["HTMLColor"];
	jg.setColor(rgbx);
	jg.fillRect(x*20, y*20, 20, 10);
	x++;
	if (x >= chart["NumberOfColumns"]) {
	    x = 0;
            y++;
	}
    }
    jg.paint();       

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
    scanner["DeviceSettings"] = new Object();
    scanner["DeviceSettings"]["ColorimetricIlluminant"] = "D50";        
    scanner["DeviceSettings"]["ColorimetricWhiteReference"] = "ABSOLUTE";
    scanner["DeviceSettings"]["RemoveScanBoundaries"] = "True";        
    scanner["ActivationKey"] = "Your Activation Key Here";

    var result = plugin().read(scanner, localchart, query,
                  presentStripDialog, presentProgress, presentScanDone); 
    if (result != "OK") {
      alert("Scanning Device has been unplugged");
    }
}

function spot(scanner, colorspace, chart)
{
    if (colorspace.indexOf("Density") != -1) {
        alert("You must first spot read the substrate before attempting a Density read");
        return;
    }
    var localchart = chart;
    localchart["ReadCS"] = colorspace;
    localchart["CurrentStrip"] = sync150["RowNames"][0];
    localchart["ReferenceColors"] = sync150["RefBase"][0];
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
    var result = plugin().read(scanner, localchart, query,
                  presentSpotDialog, presentProgress, presentSpotDone); 
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
        var scantag = "g_ScannerList["+i+"]";    
        var features = scanner["Requirements"];
        newHTML += '<DT>'+scanner['Name']+':'+scanner["Type"]+':'+scanner['Serial']+'</DT>';
        newHTML += '<DD>';
        if (features.indexOf("RequiresCalibration") >= 0) {
            newHTML += '<A HREF="#" onclick="javascript:calibrateDevicePaper('+scantag+')">Calibrate Paper</A><BR>';
            newHTML += '<DD>';      
            newHTML += '<A HREF="#" onclick="javascript:calibrateDeviceAbs('+scantag+')">Calibrate Absolute</A><BR>';
        }
	// Offer to print the chart, then the read.
        newHTML += '<DIV STYLE="margin-left:60px">';
	newHTML += '<A HREF="sync150.tif">sync150 Chart</A> &mdash; ';

        newHTML += '<A HREF="#" onclick="javascript:scan('+scantag+',\'CIELab,sRGB\', sync150)">Read sync150 Strip Chart</A><BR> ';
        newHTML += '<A HREF="#" onclick="javascript:spot('+scantag+',\'CIELab,sRGB\', spotchart)">Read spot value</A> ';
	var span = "UploadData"+scanner["Name"];
        newHTML += '<SPAN ID="' + span + '"></SPAN> ';
        newHTML += '</DIV>';
        newHTML += '</DD>';
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
//
// This function takes the current saved chart data and uploads it to
// the server as a json string, then pops up the result.
//
function uploadCB(msg, xml) 
{
    alert(msg);
    RemoveModalWindow();
}

// This function does not work in internet explorer.  The JSON.stringify appears missing
function upload()
{
    var uploadmsg = new Object();
    uploadmsg["Prompt"] = "Upload of data in progress";
    uploadmsg["Answers"] = "NONE";
    OnQuery(uploadmsg);

    var parameters = new SOAPClientParameters();
    // For debugging
    g_senddata = JSON.stringify(sync150["Result"]);
    parameters.add("chart", "SpectroJet");
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
    div.style.top = '200px'; //Math.max((top + (GetWindowHeight() - div.offsetHeight) / 2), 0) + 'px';
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
