var ddlPrinters;

(function () {
    // register onload event
    if (window.addEventListener)
        window.addEventListener("load", initTests, false);
    else if (window.attachEvent)
        window.attachEvent("onload", initTests);
    else
        window.onload = initTests;
}());

function initTests() {
    if (dymo.label.framework.init) {
        //dymo.label.framework.trace = true;
        dymo.label.framework.init(onload);
    } else {
        onload();
    }
}

// This runs after form has completely loaded
function onload() {
    ddlPrinters = getddlPrinters();

    // Find all DYMO Printers
    var printers = dymo.label.framework.getPrinters();
    if (printers.length === 0) {
        alert("No DYMO printers are installed. Install DYMO printers.");
        return;
    }

    // For each DYMO printer, add as selection to the ddlPrinters dropdown
    for (var i = 0; i < printers.length; i++) {
        var printer = printers[i];
        if (printer.printerType === "LabelWriterPrinter") {
            var printerName = printer.name;

            var option = document.createElement('option');
            option.value = printerName;
            option.appendChild(document.createTextNode(printerName));
            ddlPrinters.appendChild(option);
        }
    }
}

function UpdateSelectedPrinter() {
    document.getElementById("hdSelectedPrinter").value = ddlPrinters.value;
}