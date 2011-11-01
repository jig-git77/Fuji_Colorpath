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
            confirm(msg, "Error");
        }
        else {
            confirm("Calibration Successful", "Calibration");
            jQuery('#tblMessage').css('background-color', '#6BB19C');
            jQuery('#lblMessage').html("Status : Calibration Successful.");
            jQuery('#btnMeasureAndUploadChart').css('display', '');
            jQuery('#btnCancelMeasurement').css('display', '');
        }
    }
}
// Calibrate the device.  We pass in the scanner to calibrate,
//  a callback to present instructions to the user, and a callback
// to notify the user that the calibration is done.
function calibrateDevice(scanner) {
    var myQuery = new Object();
    myQuery["CALIBRATEINSTRUCTIONS"] = "<B><CENTER>Please place i1Pro in the calibration cradle <br> and press press the i1 trigger<BR>or click anywhere in this window</CENTER></B>";
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
        //query["Prompt"] = "<B><CENTER>Strip read in progress</CENTER></B>";
        query["Prompt"] = "<B>Strip read in progress.</B>";
    } else {
        if (scanner["Type"] == "iO") {
            //query["Prompt"] = "<B><CENTER>Please place the chart on the platten and click to continue.</CENTER></B>";
            query["Prompt"] = "<B>Please place the chart on the platten and click to continue.</B>";
        } else {
            //query["Prompt"] = "<B><CENTER>Please place the scanner over the strip " + chart["CurrentStrip"] + ".<BR>press and hold the trigger while sliding the scanner across the strip.</CENTER></B>";
            query["Prompt"] = "<B>Please place the scanner over the strip " + chart["CurrentStrip"] + ".<BR>press and hold the trigger while sliding the scanner across the strip.</B>";
        }
    }    
    jQuery('#tblMessage').css('background-color', '#6BB19C');
    jQuery('#lblMessage').html("Status : " + query["Prompt"]);
    //presentQuery(query);
}
//
// The "Done" callback functions take three args.  The scanner, 
// the prompt object and the final function.  If the final function
// is a function object it is called, if it is a string then
// it is evaluated.  This generally will be a call back to the
// C++ side to do final thread and memory cleanup. Sample values
// must be retriveved before the final call.
//
function presentScanDonei1(scanner, mychart, final) {

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

                var result = plugin().read(scanner, mychart, query, presentStripDialogi1, presentProgressi1, presentScanDonei1);
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
    var results = mychart["Result"];
    var idx = mychart["NumberOfRows"];

    if (!(scanner["Type"] == "iO" || scanner["Type"] == "iSis")) {
        if (results.length != mychart["NumberOfColumns"]) {
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

            var result = plugin().read(scanner, mychart, query, presentStripDialogi1, presentProgressi1, presentScanDonei1);
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
        var DensityResults = results[ind]["Density"];
        var sName = mychart["ReferencePatch"][ind]["SampleName"];
        var PatchTypeID = mychart["ReferencePatch"][ind]["PatchTypeID"];
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

        var result = plugin().read(scanner, mychart, query, presentStripDialogi1, presentProgressi1, presentScanDonei1);
        if (result != "OK") {
            alert("Scanning Device has been unplugged");
        }
    }

    if (next == -1) {
        //jQuery('#btnUploadData').css('display', '');
        jQuery('#btnMeasureAndUploadChart').val('Upload Measurement');

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
function presentProgressi1(scanner, mychart) {
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