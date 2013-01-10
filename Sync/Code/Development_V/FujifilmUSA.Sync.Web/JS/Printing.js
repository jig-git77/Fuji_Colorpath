/*
This file contains the functions and code required to print label on Dymo Label Writer Printer
*/

PrintLabel = function () {

    var label = dymo.label.framework.openLabelXml(GetLabelXml());
    label.setObjectText('imgMeasurementStatusIcon', GetStatusIconImageString());
    //Code to print
    var printers = dymo.label.framework.getPrinters();
    if (printers.length == 0)
        alert("No DYMO printers are installed. Please install it.");

    var printerName = "";
    for (var i = 0; i < printers.length; ++i) {
        var printer = printers[i];
        if (printer.printerType == "LabelWriterPrinter") {
            printerName = printer.name;
            break;
        }
    }
    try {
        label.print(printerName);
    }
    catch (e) {
        alert("Error while printing label. Please try again.");
    }

    return false;
};