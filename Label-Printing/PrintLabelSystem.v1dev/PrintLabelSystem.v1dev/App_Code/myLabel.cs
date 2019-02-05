using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;


public class myLabel {
    public myLabel(tbl_Label dmLabel) {
        createMyLabel(dmLabel);
    }

    private string Name { get; set; }
    private string Type { get; set; }
    private string XML { get; set; }
    private string DynamicXMLField { get; set; }
    private string StoredProcedure { get; set; }
    private string ValidPrinters { get; set; }
    private int ActiveInactive { get; set; }
    private XmlDocument XMLDoc { get; set; }
    private tbl_Label tbl_Label { get; set; }

    public void createMyLabel(tbl_Label dmLabel) {
        this.Name = "";
        this.Type = "";
        this.XML = "";
        this.DynamicXMLField = "";
        this.StoredProcedure = "";
        this.ValidPrinters = null;
        this.ActiveInactive = 0;
        this.XMLDoc = null;
        this.tbl_Label = dmLabel;
    }

    public void setMyLabel(tbl_Label rowFromDB) {
        this.Name = rowFromDB.Name;
        this.Type = rowFromDB.Type;
        this.XML = rowFromDB.XML;
        this.XMLDoc = setXMLDoc();
        this.DynamicXMLField = rowFromDB.DynamicXMLField;
        this.StoredProcedure = rowFromDB.StoredProcedure;
        this.ValidPrinters = rowFromDB.ValidPrinters;
        this.ActiveInactive = rowFromDB.ActiveInactive;
    }

    public XmlDocument getXMLDoc() {
        return this.XMLDoc;
    }

    private XmlDocument setXMLDoc() {
        if (this.XML != null) {
            this.XMLDoc = new XmlDocument();
            try {
                this.XMLDoc.LoadXml(this.XML);
            } catch (Exception e) {
            }
        }
        return this.XMLDoc;
    }

    // NOT CURRENTLY USED - Used to insert a label into the database
    private tbl_Label convertNewLabel() {
        tbl_Label = new tbl_Label();
        tbl_Label.Name = this.Name;
        tbl_Label.Type = this.Type;
        tbl_Label.XML = this.XML;
        tbl_Label.DynamicXMLField = this.DynamicXMLField;
        tbl_Label.StoredProcedure = this.StoredProcedure;
        tbl_Label.ValidPrinters = this.ValidPrinters;
        tbl_Label.ActiveInactive = this.ActiveInactive;
        return tbl_Label;
    }
}