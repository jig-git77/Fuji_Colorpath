/*
This file contains the functions and code required to print label on Dymo Label Writer Printer
*/

//This function will check whether the Dymo printer is installed on client machine or not
IsPrinterInstalled = function () {
    var printers = dymo.label.framework.getPrinters();
    if (printers.length == 0)        
        return false;    
    return true;
};

//This function will print the label via dymo printer on client machine
PrintLabel = function () {
    //First check whether printer is installed or not
    if (IsPrinterInstalled()) {

        //GetLabelXml() is registered from C# code in javascript in analyze.aspx page. 
        //It contains the label template in XML with the required values replaced with placeholders
        var label = dymo.label.framework.openLabelXml(GetLabelXml());

        //GetStatusIconImageString() is registered from C# code in javascript in analyze.aspx page.
        //It contains the image in base64 encoded string format which will be replaced with placeholders in template using below line of code
        label.setObjectText('imgMeasurementStatusIcon', GetStatusIconImageString());

        //Code to print    
        var printerName = "";
        var printers = dymo.label.framework.getPrinters();
        if (printers.length == 0)
            return false;    
        for (var i = 0; i < printers.length; ++i) {
            var printer = printers[i];
            if (printer.printerType == "LabelWriterPrinter") {
                printerName = printer.name;
                break;
            }
        }
        try {
            if (printerName != "")
                label.print(printerName); //This will print the label
            else
                alert("No label writer printers available/installed.");
        }
        catch (e) {
            alert("Error while printing label. Please try again.");
        }
    }
    return false;
};
