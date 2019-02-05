using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Xml;
using System.Linq;
using System.Web.UI;

public partial class _Default : System.Web.UI.Page {
    #region Properties
    public string selectedPrinter { // needed for javascript AND for insert statement
        get { return Session["selectedPrinter"] as string; }
        set { Session["selectedPrinter"] = value; }
    }
    private myLabel myLabel {
        get { return Session["myLabel"] as myLabel; }
        set { Session["myLabel"] = value; }
    }
    private tbl_Label selectedLabel {
        get { return Session["selectedLabel"] as tbl_Label; }
        set { Session["selectedLabel"] = value; }
    }
    private BOXX_V2Entities dbConnection {
        get { return Session["dbConnection"] as BOXX_V2Entities; }
        set { Session["dbConnection"] = value; }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e) {
        SetUpPage();
    }

    private void SetUpPage() {
        // Initial Load, populate Label DropDown
        if (!IsPostBack) {
            SetupLabelDropDown();
        } else {
            // Set myLabel and selectedPrinter Session variables
            tbl_Label selectedLabel = dbConnection.tbl_Label.Where(x => x.ID.ToString() == ddlLabels.SelectedValue).First();
            myLabel.setMyLabel(selectedLabel);
            this.selectedPrinter = Request.Form["SelectedPrinter"];
        }

        // Lastly, focus on Textbox 
        txtTextForLabel.Focus();
    }

    private void SetupLabelDropDown() {
        // Create a connection to Tables in database
        dbConnection = new BOXX_V2Entities();

        // Get all labels listed as active in the label table and set selectedLabel to myLabel Session variable
        try {
            ddlLabels.Items.AddRange(dbConnection.tbl_Label.Where(x => x.ActiveInactive == 1).ToList().Select(y => new ListItem(y.Name, y.ID.ToString())).ToArray());
            setLabelSelectionAccordingToFavorite();
            selectedLabel = dbConnection.tbl_Label.Where(x => x.ID.ToString() == ddlLabels.SelectedValue).First();
            myLabel = new myLabel(selectedLabel);
        } catch (Exception e) {
            Response.Write("<script>alert('Unable to load Labels or No Active Labels in Label table');</script>");
        }
    }

    private void setLabelSelectionAccordingToFavorite() {
        //TODO make default selectedIndex according to saved favorite, if no saved favorite, default to 1st
        try {
            //cbxLabels.SelectedIndex = cbxLabels.Items.IndexOf(cbxLabels.Items.FindByText("SERIAL_NUMBER"));
            ddlLabels.SelectedIndex = 0; //ONLY FOR TESTING PURPOSES
        } catch (Exception e) {
            Response.Write("Error selecting a default label: " + e);
            ddlLabels.SelectedIndex = 0; //TODO or is it -1
        }
    }

    // Clicked Print - Adds to LabelToPrintDatabase
    protected void btnPrintSubmit_Click(object sender, EventArgs e) {
        dbConnection.tbl_LabelToPrint.Add(createNewLabelToPrint());
        try {
            dbConnection.SaveChanges();

        } catch (Exception ex) {

        }
    }

    // Create record to add to LabelToPrint Table
    private tbl_LabelToPrint createNewLabelToPrint() {
        myLabelToPrint myLabelToPrint = new myLabelToPrint(myLabel, new tbl_LabelToPrint());
        myLabelToPrint.setLabelText(txtTextForLabel.Text);
        myLabelToPrint.setLabelType(selectedLabel.Type);
        myLabelToPrint.setPrinterName(selectedPrinter);
        return myLabelToPrint.convertClassToDataModel();
    }
}
