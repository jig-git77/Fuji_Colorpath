// This function presents the iSis read query.  We do this
// in javascript so the web app can change everything including
// the language of the query.
function presentChartDialogiSis(scanner, chart, query) {
    if (query["Request"] == "READACKNOWLEDGED") {
        query["Prompt"] = locChartReadProgress;
    } else {
        query["Prompt"] = locPlaceChartScannerClickWindow;
        g_SavedQuery = query;
    }
    jQuery("[id*='lblMessage']").css('color', 'Black');
    jQuery("[id*='lblMessage']").html(locStatusText + " " + query["Prompt"]);
}


function presentScanDoneiSis(scanner, mychart, final) {
    // Before we finalize the request, get all the values from
    // the strip read and present them to the user.
    // This is a syncronous operation since the data is 
    // stored and ready to go.
    // The samples object is the same object that describes the
    // chart and expected results, but with the values we actually
    // read added.
    /*
    if (typeof mychart["Result"] == "string" || typeof mychart == "string")  {
        // This type of query means we finish up with an error, so just present the
        // error
        if (mychart["Result"] == "CANCELLED" || mychart == "CANCELLED" ) {

            jQuery("[id*='lblMessage']").css('color', 'Black');
            jQuery("[id*='lblMessage']").html("Status : Click on 'Measure Chart' button.");

        }
        if (typeof final == "string") {
            eval(final);
        } else if (typeof final == "object") {
            final();
        }
        if (typeof mychart == "string") {
            alert(mychart);
        } else if (typeof mychart["Result"] == "string") {
            alert(mychart["Result"]);
        }
        
        //if (MeasuredPatches.length > 0) MeasuredPatches.length = 0;
        // delete (g_Chart["Result"]);
        delete mychart["Result"];
        return;
    }*/

    
    if (typeof mychart["Result"] == "string") {
        // This type of query means we finish up with an error, so just present the
        // error
        if (!(mychart["Result"] == "OK" || mychart["Result"] == "CANCELLED")) {
            MeasurementDialog(mychart["Result"], locErrorText);
        }
        if (typeof final == "string") {
            eval(final);
        } else if (typeof final == "object") {
            final();
        }
        return;
    }
    
 
 
    if (mychart == "CANCELLED") {
      
        refreshCanvas()
        if (typeof final == "string") {
            eval(final);
        } else if (typeof final == "object") {
            final();
        }
        refreshCanvas();
        jQuery("[id*='lblMessage']").css('color', 'Black');
        jQuery("[id*='lblMessage']").html(locClickMeasureButton);   
        ControlDisability(0, false, 1, false, 1, true, 1, true); //------Make Cancel button disabled & Measure button enabled.
        return;
    }

    if (typeof mychart["Result"] == "string") {
        // This type of query means we finish up with an error, so just present the error
        if (!(mychart["Result"] == "OK" || mychart["Result"] == "CANCELLED")) {          
            refreshCanvas();
            ControlDisability(0, false, 1, false, 1, true, 1, true); //------Make Cancel button disabled & Measure button enabled.
        }
        if (typeof final == "string") {
            eval(final);
        } else if (typeof final == "object") {
            final();
        }



        // You could reissue a read here, but for a full chart reader, this isn't
        // generally a simple error, so we don't reissue the read.
        //if (mychart["Result"] != "CANCELLED") {
        //    var query = new Object();
        //    query["Success"] = "Samples Complete";
        //    query["Fail"] = "Failed to read samples";
        //  
        //    var result = plugin().read(scanner, mychart, query,
        //				       presentChartDialog, presentProgress, presentScanDone); 
        //    if (result != "OK") {
        //       alert("Scanning Device has been unplugged");
        //    }
        //}
        
        jQuery("[id*='lblMessage']").css('color', 'Black');
        jQuery("[id*='lblMessage']").html(locClickMeasureButton);
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
        MeasurementDialog(locFailedReadExpectedSamplesError1 + expected + locFailedReadExpectedSamplesError2 + results.length + locFailedReadExpectedSamplesError3, locErrorText);
        if (typeof final == "string") {
            eval(final);
        } else if (typeof final == "object") {
            final();
        }
        return;
    }

    // From here on down, we are done with the reading of the strips.
    // We can now present the final results to the user.

    // Lets draw our sampled colors over the top of the displayed
    // image.

    prog["Prompt"] = locReadingProgress1 + " " + samples + " " + locReadingProgress2;
    jQuery("[id*='lblMessage']").html(locStatusText + " " + prog["Prompt"]);

    for (ind = 0; ind < samples; ind++) {
        //---------------Code to paint last row on canvas-------------------//
        if (ind == end) {

            if (cellSize == 0) {    //----For "Fuji 40 Chart" of iSis (has only 2 row, so directly goes to this function without assigning cellsize in presentProgressiSis())
                chartWidth = parseInt(jQuery("[id*='Canvas']").width());
                chartHeight = parseInt(jQuery("[id*='Canvas']").height());

                columnCount = mychart["NumberOfColumns"];
                rowCount = mychart["NumberOfRows"];
                cellSize = Math.min((chartWidth / columnCount), (chartHeight / rowCount));
                cellSize = Math.round(cellSize - 0.5);
            }
            //------Call just one time to print last row
            DrawChart(results, cellSize, columnCount, ind, samples, samples, "");
        }

        //-----------Code to add all measurement patches in DB-----------------------//
        var LABResults = results[ind]["CIELab"];
        var XYZResults = results[ind]["CIEXYZ"];
        var DensityResults = results[ind]["Density"];
        var SpectralResults = results[ind]["Spectral"];

        var _Density = new Density(DensityResults[0].toFixed(5), DensityResults[1].toFixed(5), DensityResults[2].toFixed(5), DensityResults[3].toFixed(5));
        var _Spectral = new Spectral(ClipDecimal(SpectralResults[0]), ClipDecimal(SpectralResults[1]), ClipDecimal(SpectralResults[2]), ClipDecimal(SpectralResults[3]), ClipDecimal(SpectralResults[4]), ClipDecimal(SpectralResults[5]), ClipDecimal(SpectralResults[6]), ClipDecimal(SpectralResults[7]), ClipDecimal(SpectralResults[8]), ClipDecimal(SpectralResults[9]), ClipDecimal(SpectralResults[10]), ClipDecimal(SpectralResults[11]), ClipDecimal(SpectralResults[12]), ClipDecimal(SpectralResults[13]), ClipDecimal(SpectralResults[14]), ClipDecimal(SpectralResults[15]), ClipDecimal(SpectralResults[16]), ClipDecimal(SpectralResults[17]), ClipDecimal(SpectralResults[18]), ClipDecimal(SpectralResults[19]), ClipDecimal(SpectralResults[20]), ClipDecimal(SpectralResults[21]), ClipDecimal(SpectralResults[22]), ClipDecimal(SpectralResults[23]), ClipDecimal(SpectralResults[24]), ClipDecimal(SpectralResults[25]), ClipDecimal(SpectralResults[26]), ClipDecimal(SpectralResults[27]), ClipDecimal(SpectralResults[28]), ClipDecimal(SpectralResults[29]), ClipDecimal(SpectralResults[30]), ClipDecimal(SpectralResults[31]), ClipDecimal(SpectralResults[32]), ClipDecimal(SpectralResults[33]), ClipDecimal(SpectralResults[34]), ClipDecimal(SpectralResults[35]));

        mpatch = new Patch();
        mpatch.SampleName = mychart["ReferencePatch"][ind]["SampleName"];
        mpatch.CiexyzX = XYZResults["X"].toFixed(2);
        mpatch.CiexyzY = XYZResults["Y"].toFixed(2);
        mpatch.CiexyzZ = XYZResults["Z"].toFixed(2);
        mpatch.CielabL = LABResults["L"].toFixed(2);
        mpatch.CielabA = LABResults["A"].toFixed(2);
        mpatch.CielabB = LABResults["B"].toFixed(2);
        mpatch.Density = _Density;
        mpatch.Spectral = _Spectral;       
        MeasuredPatches.push(mpatch);
    }

    x = 0;
    y = 0;

    //
    // Add an upload button and canvas
    //

    jQuery("[id*='lblMessage']").css('color', 'Black');
    jQuery("[id*='lblMessage']").html(locMeasurmentComplete);
    ControlDisability(0, false, 1, false, 1, true, 1, true); //------Make Cancel button disabled & Measure button enabled now.   
    jQuery("[id*='btnMeasureAndUploadChart']").val(locUploadMeasurementText);
    jQuery("[id*='hdMeasureAndUploadChart']").val('uploadmeasurement');   

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


//----------Global Variables Declaration for "presentProgressiSis" function-------------//
var start = 0;
var end = 0;
var x = 0;
var y = 0;
var prog = new Object();
var chartWidth, chartHeight, columnCount, rowCount, cellSize = 0;
var cursample = 0;
var countglobal = 0;
var srgb;
var rgbx;
var canvas;
var context;


function presentProgressiSis(scanner, mychart) {
    console.log("In presentProgress");

    var result = mychart["Result"];
    var samples = result.length;


    if (start == 0) {                                               //Start: ---by Dhruti: code to avoid loop starts from 0 everytime
        end = samples;

        chartWidth = parseInt(jQuery("[id*='Canvas']").width());
        chartHeight = parseInt(jQuery("[id*='Canvas']").height());

        columnCount = mychart["NumberOfColumns"];
        rowCount = mychart["NumberOfRows"];
        cellSize = Math.min((chartWidth / columnCount), (chartHeight / rowCount));
        cellSize = Math.round(cellSize - 0.5);

        prog["Answers"] = "NONE";
        jQuery("[id*='lblMessage']").css('color', 'Black');
    }

    else {
        start = end;
        end = samples;
    }                                                               //End

    //-----------Call DrawChart function to paint the chart on canvas-------------------//
    DrawChart(result, cellSize, columnCount, start, end, samples, "");
    start = 1;

    return "OK";
}

function DrawChart(result, cellSize, columnCount, startpoint, endpoint, samples, camefrom) {

    if (camefrom != "Preview") {
        prog["Prompt"] = locReadingProgress1 + " " + samples + " " + locReadingProgress2;
        jQuery("[id*='lblMessage']").html(locStatusText + " " + prog["Prompt"]);
    }

    //-------------Create Instance of Canvas---------------//
    canvas = document.getElementById("Canvas");
    context = canvas.getContext("2d");

    var avg = "";

    // alert("x is: " + x + " y is: " + y + " startpoint is: " + startpoint + " end point is: " + endpoint);

    for (cursample = startpoint; cursample < endpoint; cursample++) {

        if (camefrom == "Preview") {

            if (cursample == 0) {              //-----------Just for when something wrong happens and stop chart reading in middle, start from 1st row---------//
                x = 0;
                y = 0;
                start = 0;
            }

            rgbx = result[cursample]["HTMLColor"];

            context.fillStyle = rgbx;
            context.fillRect(x * cellSize, y * cellSize, cellSize, cellSize);
        }
        else {

            //alert("x is: " + x + " y is: " + y + " cellSize is: " + cellSize + " start pointX is: " + x * cellSize + " end pointY is: " + y * cellSize);

            srgb = result[cursample]["sRGB"];

            rgbx = "#" + dec2hex(srgb["R"]) + dec2hex(srgb["G"]) + dec2hex(srgb["B"]);

            context.fillStyle = rgbx;
            context.fillRect(x * cellSize, y * cellSize, cellSize, cellSize);
        }

        x++;

        if (x >= columnCount) {
            x = 0;
            y++;
        }
    }


    if (camefrom == "Preview") {
        x = 0;                      //----It will make chart redrawn from 1st point on "Canvas"-----//
        y = 0;
    }
}


