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
    myQuery["Prompt"] = myQuery[myQuery["Request"]];
    jQuery("[id*='lblMessage']").css('color', 'Black');
    jQuery("[id*='lblMessage']").html("Status : " + myQuery["Prompt"]);
}
//
// The "Done" callback functions take three args.  The scanner, the message
// (which may be replaced based on language requirements and an optional string.
// The string represents final cleanup and is usally a call back to the
// C++ side so we can close down any remaining threads and such.  If it
// is defined, all you have to do is eval it.
//
function presentCalibrateDone(scanner, msg, final) {
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
    if (typeof msg == "string") {
        // This would be either "CANCELLED" or "ERROR: ..."
        // for CANCELLED, you might just want to do nothing.
        if (!(msg == "OK" || msg == "CANCELLED")) {
            jQuery("[id*='lblMessage']").css('color', 'Black');
            jQuery("[id*='lblMessage']").html("Status : " + msg);
        }
        else {
            jQuery("[id*='lblMessage']").css('color', 'Black');
            jQuery("[id*='lblMessage']").html("Status : Calibration Successful, Click on 'Measure Chart' button.");
            jQuery("[id*='btnMeasureAndUploadChart']").css('display', '');
            jQuery("[id*='btnCancelMeasurement']").css('display', '');
        }
    }
}
// Calibrate the device.  We pass in the scanner to calibrate,
//  a callback to present instructions to the user, and a callback
// to notify the user that the calibration is done.
function calibrateDevice(scanner) {
    var myQuery = new Object();
    myQuery["CALIBRATEINSTRUCTIONS"] = "Please place i1Pro in the calibration cradle and press the i1 trigger.";
    myQuery["CALIBRATEACKNOWLEDGED"] = "Calibration In Progress.";

    // scanner calibration for i1 supports "Mode", and "SampleSource".  Mode allows you to calibrate for
    // "Point","Strip" or "Both".  
    // SampleSource sets either "Paper for reflectance,"LCD" or "CRT" for monitor 
    scanner["Mode"] = "Both"
    scanner["SampleSource"] = "Paper";  // Choices are Paper, LCD or CRT and for the latter the chart must have 1 sample (spot)
    var result = plugin().calibrateDevice(scanner, myQuery, presentCalibrateDialog, presentCalibrateDone);
    if (result != "OK") {
        jQuery("[id*='lblMessage']").css('color', 'Red');
        jQuery("[id*='lblMessage']").html("Status : Scanning Device has been unplugged.");
    }
}
// This function presents the EyeOne calibrate query.  We do this
// in javascript so the web app can change everything including
// the language of the query.
function presentStripDialogi1(scanner, chart, query) {
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
        query["Prompt"] = "Strip read in progress.";
    } else if (query["Request"] == "GETCHARTLOCATION") {
        // reget the chart position and we'll see if it changes.
        g_SavedQuery = query;
        getChartLocation(scanner);
    } else {
        query["Prompt"] = "Please place the scanner over the strip " + chart["CurrentStrip"] + ".press and hold the trigger while sliding the scanner across the strip.";
    }

    jQuery("[id*='lblMessage']").css('color', 'Black');
    jQuery("[id*='lblMessage']").html("Status : " + query["Prompt"]);


}

// This function provides an interface to fetch three corners of a chart from an iO device
// and update the chart definition so alignment of the chart isn't important.
// This function just starts up the fetching of the corners.  The presentGetPosDone will
// look at the results and decide if it needs to do additional corners
function getChartLocation(scanner) {

    var myQuery = new Object();
    delete (g_Chart["UpperLeft"]);
    delete (g_Chart["UpperRight"]);
    delete (g_Chart["LowerLeft"]);
    delete (g_Chart["LowerRight"]);
    myQuery["GETPOSINSTRUCTIONS"] = "Please place your chart on the iO table and click in this window to continue";
    myQuery["GETPOINTINSTRUCTIONS"] = "Please move the i1 to the upper left corner of the chart and press press the i1 trigger";
    myQuery["First"] = "true";
    playSound('Download/Done.wav')
    var result = plugin().getPosition(scanner, myQuery, presentGetPosDialog, presentGetPosDone);
    if (result != "OK") {
        alert("Scanning Device has been unplugged");
    }
}
function presentGetPosDialog(scanner, myQuery) {
    // alert("#6 About to call presentGetPosDialog");
    // Everything needed for the generic presentQuery is already 
    // filled in, however you can use this to change the actual
    // text for example to do multi-language.
    //
    // Calibrate sets the Request to standard strings regardless of language and you can
    // index pre-supplied strings (GETPOSINSTRUCTIONS, GETPOINTINSTRUCTIONS,
    // GETPOSACKNOWLEDGED, CANCELLED) like this:
    //alert('In presentGetPosDialog with request ' + myQuery["Request"]);
    // if (myQuery["Request"] == "CANCELLED") {
    //  RemoveModalWindow();
    //  return;
    // }
    // if (myQuery["Request"] == "GETPOSACKNOWLEDGED") {
    // RemoveModalWindow();
    // }
    myQuery["Prompt"] = myQuery[myQuery["Request"]];
    // presentQuery(myQuery);
    // myQuery["Prompt"] = myQuery[myQuery["Request"]];
    //  jQuery("[id*='lblMessage']").css('color', 'Black');
    //jQuery("[id*='lblMessage']").html("Status : " + myQuery["Prompt"]);

    OnQuery(myQuery);
}
function OnQuery(question) {
    // Setup the clock to close the window with the appropriate question
    //$('modalWindow').onclick = function() { OnModalWindowClick(question) };
    // Change the text in the window then display it
    var newHTML = question['Prompt'];
    if (question["Request"] == "GETPOSINSTRUCTIONS") {



        //g_Question = question;
        alert(newHTML)
        if (question["Answers"] == "NONE") {
            // If you can't answer it here, then just return
            return;
        }

        // Notify the user that we are done
        question["Result"] = "OK";
        plugin().queryDone(question);
    }
    else {
        // myQuery["Prompt"] = myQuery[myQuery["Request"]];
        jQuery("[id*='lblMessage']").css('color', 'Black');
        jQuery("[id*='lblMessage']").html("Status : " + newHTML);
    }

}
function presentGetPosDone(scanner, query, final) {
    //alert("#7 About to call presentGetPosDone");

    // We have to make one more call to the library.  The 
    // async request itself ran in a non-browser thread.  We need
    // the browser thread to join with the thread to keep the process state tidy.
    if (typeof final != "undefined") {
        eval(final);
    }
    //
    // Now let the user know how things ended up.
    //alert('In presentGetPosDone');
    if (typeof query == "string") {
        // This would be either "CANCELLED" or "ERROR: ..."
        // for CANCELLED, you might just want to do nothing.
        if (!(msg == "OK" || msg == "CANCELLED")) {
            // They hit cancel so clear the primary thread too.
            if (g_SavedQuery != null) {
                g_SavedQuery["Result"] = "CANCEL";
                plugin().queryDone(g_SavedQuery);
            }

            //alert(msg);
        }
    }
    // Save this position and move to the next corner
    if (typeof g_Chart["UpperLeft"] == "undefined") {
        g_Chart["UpperLeft"] = query["Result"];
        //alert(g_Chart["UpperLeft"]["X"]);
        var myQuery = new Object();
        myQuery["GETPOINTINSTRUCTIONS"] = "Please move the i1 to the upper right corner of the chart and press press the i1 trigger.";
        var result = plugin().getPosition(scanner, myQuery, presentGetPosDialog, presentGetPosDone);
        if (result != "OK") {
            alert("Scanning Device has been unplugged");
        }
    } else if (typeof g_Chart["UpperRight"] == "undefined") {
        g_Chart["UpperRight"] = query["Result"];
        var myQuery = new Object();
        myQuery["GETPOINTINSTRUCTIONS"] = "Please move the i1 to the lower right corner of the chart and press press the i1 trigger.";
        myQuery["First"] = "false"; // This should be true, but for testing this turns off the POSINSTRUCTIONS query

        var result = plugin().getPosition(scanner, myQuery, presentGetPosDialog, presentGetPosDone);
        if (result != "OK") {
            alert("Scanning Device has been unplugged");
        }
    } else {
        g_Chart["LowerRight"] = query["Result"];
        // We have three corners.  Figure the 4th and recaluclate the sample size
        var llx = g_Chart["UpperLeft"]["X"] - (g_Chart["UpperRight"]["X"] - g_Chart["LowerRight"]["X"]);
        var lly = g_Chart["UpperLeft"]["Y"] - (g_Chart["UpperRight"]["Y"] - g_Chart["LowerRight"]["Y"]);
        g_Chart["LowerLeft"] = new Object();
        g_Chart["LowerLeft"]["X"] = llx;
        g_Chart["LowerLeft"]["Y"] = lly;

        if (g_SavedQuery != null) {
            alert('Click to start scan');
            g_SavedQuery["Result"] = "OK";
            g_SavedQuery["Chart"] = g_Chart;
            //alert('Waking up nested query');
            plugin().queryDone(g_SavedQuery);
        }
    }
}

var area = new Object();
area["W"] = 0;
area["H"] = 0;

var distance = new Object();
distance["R"] = 0;
distance["C"] = 0;

var drawCells = 0;
var cId = 0;
var x1 = 0;
var y1 = 0;

//
// The "Done" callback functions take three args.  The scanner, 
// the prompt object and the final function.  If the final function
// is a function object it is called, if it is a string then
// it is evaluated.  This generally will be a call back to the
// C++ side to do final thread and memory cleanup. Sample values
// must be retriveved before the final call.
//
function presentScanDonei1(scanner, mychart, final) {
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
            //--------Play "Error - Not Matched" sound--------//
            playSound('Download/Error.wav');
            alert(mychart["Result"]);
            //MeasurementDialog(mychart["Result"], "Error");           
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

                var result = plugin().read(scanner, mychart, query, presentStripDialogi1, presentProgressi1, presentScanDonei1);
                if (result != "OK") {
                    jQuery("[id*='lblMessage']").css('color', 'Red');
                    jQuery("[id*='lblMessage']").html("Status : Scanning Device has been unplugged.");
                }
            }
        }
        return;
    }
    //
    // Now let the user know how things ended up.
    //
    var results = mychart["Result"];
    var idx = mychart["NumberOfRows"];

    if (!(scanner["Type"] == "iO" || scanner["Type"] == "iSis")) {
        if (results.length != mychart["NumberOfColumns"]) {
            //--------Play "Error - Not Matched" sound--------//
            playSound('Download/Error.wav');
            alert("Failed to read expected number of samples" + results.length + " vs " + mychart["NumberOfColumns"]);
            // MeasurementDialog("Failed to read expected number of samples" + results.length + " vs " + mychart["NumberOfColumns"], "Error");

            if (typeof final == "string") {
                eval(final);
            } else if (typeof final == "object") {
                final();
            }

            var query = new Object();
            query["Success"] = "Samples Complete";
            query["Fail"] = "Failed to read samples";
            mychart["Result"] = null;


            var result = plugin().read(scanner, mychart, query, presentStripDialogi1, presentProgressi1, presentScanDonei1);
            if (result != "OK") {
                jQuery("[id*='lblMessage']").css('color', 'Red');
                jQuery("[id*='lblMessage']").html("Status : Scanning Device has been unplugged.");
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
            //MeasurementDialog("ERROR: Cannot determine current read column", "Error");
            return;
        }
    }

    // Proceed to thread clean up.  This will happen quickly
    if (typeof final == "string") {
        eval(final);
    } else if (typeof final == "object") {
        final();
    }

    // All I should need to do is append each array element
    // to the end of my current results since it is now a flat array
    if (typeof g_Chart["Result"] == "undefined") {
        g_Chart["Result"] = new Array();
    }
    g_Chart["Result"] = g_Chart["Result"].concat(results);

    for (ind = 0; ind < g_Chart["NumberOfColumns"]; ind++) {
        var LABResults = results[ind]["CIELab"];
        var XYZResults = results[ind]["CIEXYZ"];
        mpatch = new Patch();
        mpatch.CiexyzX = XYZResults["X"].toFixed(2);
        mpatch.CiexyzY = XYZResults["Y"].toFixed(2);
        mpatch.CiexyzZ = XYZResults["Z"].toFixed(2);
        mpatch.CielabL = LABResults["L"].toFixed(2);
        mpatch.CielabA = LABResults["A"].toFixed(2);
        mpatch.CielabB = LABResults["B"].toFixed(2);
        mpatch.SampleName = mychart["ReferencePatch"][ind]["SampleName"];
        mpatch.PatchTypeID = mychart["ReferencePatch"][ind]["PatchTypeID"];
        MeasuredPatches.push(mpatch);
    }

    // Check to see if this was the last strip to be read.
    var next = -1;
    if (idx < (mychart["NumberOfRows"] - 1)) {
        next = idx + 1;
    }

    var w = parseInt(jQuery("[id*='Canvas']").width());
    var h = parseInt(jQuery("[id*='Canvas']").height());
    var startPosition = 0;
    col = mychart["NumberOfColumns"];
    row = mychart["NumberOfRows"];


    if (row != 1) {

        //--------Fuji 150 or larger chart----------//
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


    var noOfCells = mychart["Size"];
    if (y1 == 0) {
        y1 = parseInt(area["H"]);
    }


    x1 = 0;
    col = results.length;
    for (j = 0; j < col; j++) {
        if (noOfCells > drawCells) {


            var xx1 = startPosition + x1;
            var yy1 = startPosition + y1;

            x1 = x1 + parseInt(area["W"]) + parseInt(distance["C"]);

            var canvas = document.getElementById("Canvas");
            var context = canvas.getContext("2d");

            var srgb = results[j]["sRGB"];
            var rgbx = "#" + dec2hex(srgb["R"]) + dec2hex(srgb["G"]) + dec2hex(srgb["B"]);

            context.fillStyle = rgbx;
            context.fillRect(xx1, yy1, parseInt(area["W"]), parseInt(area["H"]));

            drawCells++;
        }
    }

    y1 = y1 + (parseInt(area["H"])) + parseInt(distance["R"]);

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

        var result = plugin().read(scanner, mychart, query, presentStripDialogi1, presentProgressi1, presentScanDonei1);
        if (result != "OK") {
            jQuery("[id*='lblMessage']").css('color', 'Red');
            jQuery("[id*='lblMessage']").html("Status : Scanning Device has been unplugged.");
        }
    }

    if (next == -1) {

        jQuery("[id*='lblMessage']").css('color', 'Black');
        jQuery("[id*='lblMessage']").html("Status : Measurement Complete.");
        jQuery("[id*='btnMeasureAndUploadChart']").val('Upload Measurement');
        drawCells = 0;
        area = new Object();
        area["W"] = 0;
        area["H"] = 0;

        distance = new Object();
        distance["R"] = 0;
        distance["C"] = 0;

        drawCells = 0;
        cId = 0;
        x1 = 0;
        y1 = 0;
    }

    //--------Play "OK - Passed" sound--------//
    playSound('Download/Done.wav');
}
function presentProgressi1(scanner, mychart) {
    console.log("In presentProgress");
    var result = mychart["Result"];
    var samples = result.length;
    var prog = new Object();
    var width = mychart["NumberOfColumns"];
    var height = mychart["NumberOfRows"];
    var lines = samples / width;
    prog["Prompt"] = "Reading progress " + samples + " samples";
    prog["Answers"] = "NONE";
    jQuery("[id*='lblMessage']").css('color', 'Black');
    jQuery("[id*='lblMessage']").html("Status : " + prog["Prompt"]);

    return "OK";
}