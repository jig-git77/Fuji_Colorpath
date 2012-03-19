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
            jQuery("[id*='lblMessage']").html("Status : Calibration Successful.");
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
    } else {
        if (scanner["Type"] == "iO") {
            query["Prompt"] = "Please place the chart on the platten and click to continue.";
        } else {
            query["Prompt"] = "Please place the scanner over the strip " + chart["CurrentStrip"] + ".press and hold the trigger while sliding the scanner across the strip.";
        }
    }
    jQuery("[id*='lblMessage']").css('color', 'Black');
    jQuery("[id*='lblMessage']").html("Status : " + query["Prompt"]);
}
//
// The "Done" callback functions take three args.  The scanner, 
// the prompt object and the final function.  If the final function
// is a function object it is called, if it is a string then
// it is evaluated.  This generally will be a call back to the
// C++ side to do final thread and memory cleanup. Sample values
// must be retriveved before the final call.
//
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
            MeasurementDialog(mychart["Result"], "Error");
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
            MeasurementDialog("Failed to read expected number of samples" + results.length + " vs " + mychart["NumberOfColumns"], "Error");
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
            MeasurementDialog("ERROR: Cannot determine current read column", "Error");
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
    var startPosition = 1;
    col = mychart["NumberOfColumns"];
    row = mychart["NumberOfRows"];

    if (row != 1) {
        area["H"] = (((h / row) / 2) / 2) + '';
        distance["R"] = (((h / row) / 2) / 2) + '';
    }
    else {
        area["H"] = 20;
        distance["R"] = 1;
    }        
    
    area["W"] = ((w / col) / 2) + '';
    distance["C"] = ((w / col) / 2) + '';
    var noOfCells = mychart["Size"];
    if (y1 == 0) {
        y1 = parseInt(area["H"]);
    }
    y1 = y1 + (parseInt(area["H"])) + parseInt(distance["R"] * 2);

    x1 = 0;
    col = results.length;
    for (j = 0; j < col; j++) {        
        if (noOfCells > drawCells) {            
            x1 = x1 + parseInt(area["W"]) + parseInt(distance["C"]);

            var xx1 = startPosition + x1;
            var yy1 = startPosition + y1;

            //var jg2 = new jsGraphics("Canvas");
            var jg2 = new jsGraphics(document.getElementById("Canvas").id);
            var srgb = results[j]["sRGB"];            
            var rgbx = "#" + dec2hex(srgb["R"]) + dec2hex(srgb["G"]) + dec2hex(srgb["B"]);            
            jg2.setColor(rgbx);
            jg2.fillRect(xx1, yy1, parseInt(area["W"]), parseInt(area["H"]));
            jg2.paint();
            drawCells++;
        }
    }

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
        //jQuery('#btnUploadData').css('display', '');
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