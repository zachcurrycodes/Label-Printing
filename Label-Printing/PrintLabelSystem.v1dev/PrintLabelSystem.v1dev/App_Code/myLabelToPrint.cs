using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

public class myLabelToPrint {
    public myLabelToPrint(myLabel myLabel, tbl_LabelToPrint toPrint) {
        createLabelToPrint(myLabel, toPrint);
    }

    private string LabelText { get; set; }
    private string LabelType { get; set; }
    private string PrinterName { get; set; }
    private string CompletedLabelXML { get; set; }
    private int LabelPrinted { get; set; }
    private DateTime? DateTimeEntered { get; set; }
    private DateTime? DateTimePrinted { get; set; }
    private string ErrorInformation { get; set; }
    private myLabel myLabel { get; set; }
    private tbl_LabelToPrint tbl_LabelToPrint { get; set; }

    public void createLabelToPrint(myLabel myLabel, tbl_LabelToPrint tbl_LabelToPrint) {
        this.LabelText = "";
        this.LabelType = "";
        this.PrinterName = "";
        this.CompletedLabelXML = "";
        this.LabelPrinted = 0;
        this.DateTimeEntered = null;
        this.DateTimePrinted = null;
        this.ErrorInformation = "";
        this.myLabel = myLabel;
        this.tbl_LabelToPrint = tbl_LabelToPrint;
    }

    public void setLabelText(string labelText) {
        this.LabelText = labelText;
        setCompletedXML();
    }
    public void setLabelType(string labelType) {
        this.LabelType = labelType;
    }
    public void setPrinterName(string printerName) {
        this.PrinterName = printerName;
    }
    private void setCompletedXML() {
        XmlDocument xmlDoc = myLabel.getXMLDoc();
        XmlNode rootXML = xmlDoc.SelectSingleNode("/DieCutLabel/ObjectInfo");
        foreach (XmlElement obj in rootXML.ChildNodes) {
            if (obj.Name.ToUpper() == "BARCODEOBJECT") {
                XmlNode text = rootXML.SelectSingleNode("./BarcodeObject/Text");
                text.InnerText = this.LabelText;
                this.CompletedLabelXML = xmlDoc.InnerXml;
                break;
            } else if (obj.Name.ToUpper() == "ADDRESSOBJECT") { //TODO add for as many label types we encounter      
                                                                // 

                XmlNode text = rootXML.SelectSingleNode("./AddressObject/StyledText/Element/String");
                text.InnerText = this.LabelText;
                this.CompletedLabelXML = xmlDoc.InnerXml;
                break;

                //<xs:element name="ShapeObject" type="ShapeObject"/>
                //<xs:element name="BarcodeObject" type="BarcodeObject"/>
                //<xs:element name="DateTimeObject" type="DateTimeObject"/>
                //<xs:element name="CounterObject" type="CounterObject"/>
                //<xs:element name="TextObject" type="TextObject"/>
                //<xs:element name="AddressObject" type="AddressObject"/>
                //<xs:element name="ImageObject" type="ImageObject"/>
                //<xs:element name="CircularTextObject" type="CircularTextObject"/>
            }
        }
    }

    public tbl_LabelToPrint convertClassToDataModel() {
        tbl_LabelToPrint = new tbl_LabelToPrint();
        tbl_LabelToPrint.LabelText = this.LabelText;
        tbl_LabelToPrint.LabelType = this.LabelType;
        tbl_LabelToPrint.PrinterName = this.PrinterName;
        tbl_LabelToPrint.CompletedLabelXML = this.CompletedLabelXML;
        tbl_LabelToPrint.LabelPrinted = this.LabelPrinted;
        tbl_LabelToPrint.DateTimeEntered = DateTime.Now;
        tbl_LabelToPrint.DateTimePrinted = this.DateTimePrinted;
        tbl_LabelToPrint.ErrorInformation = this.ErrorInformation;
        return tbl_LabelToPrint;
    }
}