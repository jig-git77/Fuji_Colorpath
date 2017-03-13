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
//This function is used to display preview as well as print label
//Argument indicates whether to preview/print and is of type bool
PrintLabel = function (previewLabel) {
    //First check whether printer is installed or not
    if (IsPrinterInstalled()) {

        //GetLabelXml() is registered from C# code in javascript in analyze.aspx page. 
        //It contains the label template in XML with the required values replaced with placeholders
        var label = dymo.label.framework.openLabelXml(GetLabelXml());
        // var label = dymo.label.framework.openLabelXml(getAddressLabelXml());
     
        //GetStatusIconImageString() is registered from C# code in javascript in analyze.aspx page.
        //It contains the image in base64 encoded string format which will be replaced with placeholders in template using below line of code
        label.setObjectText('imgMeasurementStatusIcon', GetStatusIconImageString());
        if (previewLabel) {
            //Code to view preview
            previewImageCodedString = label.render();
        }
        else {
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
                    alert(locNoPrintInstalled);
            }
            catch (e) {
                alert(locErrorWhilePrintingLabel);
            }
        }
    }
    return false;
};
var printPreviewWindow = null;
ShowLabelPrintPreview = function () {
    PrintLabel(true);
    if (printPreviewWindow != null) {
        printPreviewWindow.close()
        printPreviewWindow = null;
    }
    printPreviewWindow = window.open("PrintPreview.aspx", "PrintPreviewWindow", "width=410,height=275,resizable=no,scrollbars=no");
    printPreviewWindow.focus();
    return false;
};

function getAddressLabelXml() {

    var labelXml = '<?xml version="1.0" encoding="utf-8"?>\
                            <DieCutLabel Version="8.0" Units="twips">\
	                            <PaperOrientation>Landscape</PaperOrientation>\
	                            <Id>Address</Id>\
	                            <PaperName>30252 Address</PaperName>\
	                            <DrawCommands>\
		                            <RoundRectangle X="0" Y="0" Width="1581" Height="5040" Rx="270" Ry="270" />\
	                            </DrawCommands>\
	                            <ObjectInfo>\
		                            <AddressObject>\
			                            <Name>Address</Name>\
			                            <ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			                            <BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			                            <LinkedObjectName></LinkedObjectName>\
			                            <Rotation>Rotation0</Rotation>\
			                            <IsMirrored>False</IsMirrored>\
			                            <IsVariable>True</IsVariable>\
			                            <HorizontalAlignment>Left</HorizontalAlignment>\
			                            <VerticalAlignment>Middle</VerticalAlignment>\
			                            <TextFitMode>ShrinkToFit</TextFitMode>\
			                            <UseFullFontHeight>True</UseFullFontHeight>\
			                            <Verticalized>False</Verticalized>\
			                            <StyledText>\
				                            <Element>\
					                            <String>DYMO\
                                        828 San Pablo Ave Ste 101\
                                        Albany, CA 94706-1678</String>\
                                            <Attributes>\
                                                <Font Family="Arial" Size="12" Bold="False" Italic="False" Underline="False" Strikeout="False" />\
                                                <ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
                                            </Attributes>\
                                        </Element>\
                                    </StyledText>\
                                    <ShowBarcodeFor9DigitZipOnly>False</ShowBarcodeFor9DigitZipOnly>\
                                    <BarcodePosition>AboveAddress</BarcodePosition>\
                                    <LineFonts>\
                                        <Font Family="Arial" Size="12" Bold="False" Italic="False" Underline="False" Strikeout="False" />\
                                        <Font Family="Arial" Size="12" Bold="False" Italic="False" Underline="False" Strikeout="False" />\
                                        <Font Family="Arial" Size="12" Bold="False" Italic="False" Underline="False" Strikeout="False" />\
                                    </LineFonts>\
                                </AddressObject>\
                                <Bounds X="332" Y="150" Width="4455" Height="1260" />\
                            </ObjectInfo>\
                        </DieCutLabel>';
    return labelXml;
}

//function initTests() {
//    if (dymo.label.framework.init) {
//        //dymo.label.framework.trace = true;
//        dymo.label.framework.init(loadstart);
//    } else {
//        loadstart();
//    }
//}

//// register onload event
//if (window.addEventListener)
//    window.addEventListener("load", initTests, false);
//else if (window.attachEvent)
//    window.attachEvent("onload", initTests);
//else
//    window.onload = initTests;

//function loadstart() {
//    return true;
//}