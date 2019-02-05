<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  <script src="http://labelwriter.com/software/dls/sdk/js/DYMO.Label.Framework.latest.js" type="text/javascript" charset="UTF-8"> </script>
  <script src="Scripts/myJSPrintLabel.js" type="text/javascript"></script>
  <script type="text/javascript">
    function getSelectedPrinter() {
      return '<%=this.selectedPrinter%>';
    }
    function getddlPrinters() {
      var form = document.getElementById('form1');
      return form.MainBody_ddlPrinters;
    }
  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="Server">
    <input type="hidden" id="hdSelectedPrinter" name="SelectedPrinter" value='<%=this.selectedPrinter %>' />
  <div>
    <h1>Print A Label</h1>
  </div>  
  <div>
      <asp:Label ID="lblPrinter" runat="server" Text="Printer:"></asp:Label>
      <br />
      <asp:DropDownList ID="ddlPrinters" runat="server" Width="428px" AutoPostBack="true" />
    </div>
    <br />
    <div>
      <asp:Label ID="lblLabel" runat="server" Text="Label:"></asp:Label>
      <br />
      <asp:DropDownList ID="ddlLabels" runat="server" Width="281px" AutoPostBack="True" />
    </div>
    <br />
    <div>
      <asp:Label ID="lblTextForLabel" runat="server" Text="Text for Label"></asp:Label>
      <br />
      <asp:TextBox ID="txtTextForLabel" runat="server" TextMode="MultiLine" Width="484px" Font-Size="XX-Large" Height="141px" onfocus="this.select();"></asp:TextBox>
    </div>
    <br />
    <br />
    <div>
      <asp:Button ID="btnPrintSubmit" runat="server" Text="Print" OnClick="btnPrintSubmit_Click" OnClientClick="UpdateSelectedPrinter()" />
    </div>
</asp:Content>
