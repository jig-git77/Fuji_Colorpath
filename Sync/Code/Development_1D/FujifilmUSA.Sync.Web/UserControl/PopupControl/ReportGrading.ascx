<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportGrading.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.PopupControl.ReportGrading" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<script language="javascript" type="text/javascript">
    function Close() {
        var oWindow = GetMeasurementRadWindow();
        GetMeasurementRadWindow().close();
        return false;
    }

    function GetMeasurementRadWindow() {
        var oWindow = null;
        if (window.radWindow) oWindow = window.radWindow;
        else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
        return oWindow;
    }

    function CloseImportWindow() {
        var oWindow = GetMeasurementRadWindow();
        GetMeasurementRadWindow().close();
        top.location.href = top.location.href.replace("#", "");
    }

    function validateNumbers(evt) {

        if (evt.charCode > 31 && evt.charCode != 46 && (evt.charCode < 48 || evt.charCode > 57)) {
            alert('Allow only numbers');
            return false;
        }
    }

    function validateTolRange(txtValue, origin, position) {

        var txtTol = document.getElementById(txtValue.id);
        var txtTolvalue = document.getElementById(txtValue.id).value;
        var Tol1, Tol2, Tol3 = 0;
        var errormsg = "";
        if (origin == 'CMYK') {
            Tol1 = document.getElementById('<%= txtCMYKTol1.ClientID %>').value;
            Tol2 = document.getElementById('<%= txtCMYKTol2.ClientID %>').value;
            Tol3 = document.getElementById('<%= txtCMYKTol3.ClientID %>').value;
        }
        else if (origin == 'RGB') {
            Tol1 = document.getElementById('<%= txtRGBTol1.ClientID %>').value;
            Tol2 = document.getElementById('<%= txtRGBTol2.ClientID %>').value;
            Tol3 = document.getElementById('<%= txtRGBTol3.ClientID %>').value;
        }
        else if (origin == 'CMY') {
            Tol1 = document.getElementById('<%= txtCMYTol1.ClientID %>').value;
            Tol2 = document.getElementById('<%= txtCMYTol2.ClientID %>').value;
            Tol3 = document.getElementById('<%= txtCMYTol3.ClientID %>').value;
        }
        else if (origin == 'Paper') {
            Tol1 = document.getElementById('<%= txtPaperTol1.ClientID %>').value;
            Tol2 = document.getElementById('<%= txtPaperTol2.ClientID %>').value;
            Tol3 = document.getElementById('<%= txtPaperTol3.ClientID %>').value;
        }
        if (position == 1) {
            if (txtTolvalue < Tol2 || txtTolvalue < Tol3) {
                errormsg = "Tolerance value should be higher than Above Tolerance1 and Above Tolerance2 values";
            }
        }
        else if (position == 2) {
            if (txtTolvalue > Tol1 || txtTolvalue > Tol3) {
                errormsg = "Above Tolerance1 value should be less than Tolerance value and Above Tolerance1 value";
            }
        }
        else if (position == 3) {
            if (txtTolvalue > Tol1 || txtTolvalue < Tol2) {
                errormsg = "Above Tolerance2 value should be greater than Above Tolerance1 and less than Tolerance value";
            }
        }
        if (errormsg != "") {
            alert(errormsg);
            txtTol.value = "";
            txtTol.SetFocus();
            return false;
        }

        else
            return true;

    }

    function validateScoRange(txtValue, origin, position) {

        var txtSco = document.getElementById(txtValue.id);
        var txtScovalue = document.getElementById(txtValue.id).value;
        var Sco1, Sco2, Sco3 = 0;
         var errormsg = "";
         if (origin == 'CMYK') {
             Sco1 = document.getElementById('<%= txtCMYKSco1.ClientID %>').value;
             Sco2 = document.getElementById('<%= txtCMYKSco2.ClientID %>').value;
             Sco3 = document.getElementById('<%= txtCMYKSco3.ClientID %>').value;
         }
         else if (origin == 'RGB') {
             Sco1 = document.getElementById('<%= txtRGBSco1.ClientID %>').value;
             Sco2 = document.getElementById('<%= txtRGBSco2.ClientID %>').value;
             Sco3 = document.getElementById('<%= txtRGBSco3.ClientID %>').value;
         }
         else if (origin == 'CMY') {
             Sco1 = document.getElementById('<%= txtCMYSco1.ClientID %>').value;
             Sco2 = document.getElementById('<%= txtCMYSco2.ClientID %>').value;
             Sco3 = document.getElementById('<%= txtCMYSco3.ClientID %>').value;
         }
         else if (origin == 'Paper') {
             Sco1 = document.getElementById('<%= txtPaperSco1.ClientID %>').value;
             Sco2 = document.getElementById('<%= txtPaperSco2.ClientID %>').value;
             Sco3 = document.getElementById('<%= txtPaperSco3.ClientID %>').value;
         }
         if (position == 1) {
             if (txtScovalue < Sco2 || txtScovalue < Sco3) {
                 errormsg = "Score value should be higher than Above Tolerance Score1 and Above Tolerance Score2 values";
             }
         }
         else if (position == 2) {
             if (txtScovalue > Sco1 || txtScovalue < Sco3) {
                 errormsg = "Above Tolerance Score1 should be less than Score value and more than ABove Tolerance2 Score value";
             }
         }
         else if (position == 3) {
             if (txtScovalue > Sco1 || txtScovalue > Sco2) {
                 errormsg = "Above Tolerance Score2 value should be less than Above Tolerance Score1 and Score value";
             }
         }
         if (errormsg != "") {
             alert(errormsg);
             txtSco.value = "";
             txtSco.SetFocus();
             return false;
         }

        else
            return true;
    }

    function CloseGradingWindow() {
        var oWindow = GetMeasurementRadWindow();
        GetMeasurementRadWindow().close();
        top.location.href = top.location.href.replace("#", "");
    }

</script>

<style type="text/css">
   
    .textwidth40
    {
        width: 40px;
    }
    .textwidth30
    {
        width: 30px;
    }
</style>

<div style="padding-left:5px;">


     <table cellpadding="0" cellspacing="10" width="100%">
        <tr>
            <td>
                <ucStatusbar:Statusbar ID="fujiStatusbar" runat="server" Visible="False" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:CheckBox ID="chkAverageOfMeasurements" runat="server"  Text="Average Of Measurements" />
            </td>
        </tr>
        <tr>
            <td>
                <div style="display: block;" class="contantbox tabcontant">
                    <div class="section_top">
                        &nbsp;</div>
                    <div class="section_middle">
                        <div class="combopart">
                            <div>
                            </div>
                            <div class="header">
                                <div class="content">
                                    <div style="float: left;" class="text"><asp:Localize ID="locTolerances" 
                                            runat="server" Text="Tolerances / Scoring(Lab)"></asp:Localize>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                            <div style="padding: 5px;" >
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <table style="width: 100%;">
                                                        <tr>
                                                            <td style="width:120px; font-weight:bold;">
                                                               
                                                            </td>
                                                            <td style="width:100px; font-weight:bold;">
                                                                Tolerance
                                                            </td>
                                                            <td style="width:100px; font-weight:bold;">
                                                                Score Within
                                                            </td>
                                                             <td style="width:100px; font-weight:bold;">
                                                                Above Tolerance
                                                            </td>
                                                            <td style="width:100px; font-weight:bold;">
                                                                Score
                                                            </td>
                                                           <td style="width:100px; font-weight:bold;">
                                                               Above Tolerance
                                                            </td>
                                                            <td style="width:100px; font-weight:bold;">
                                                                Score
                                                            </td>
                                                            <td style="width:100px; font-weight:bold;">
                                                               
                                                            </td>
                                                        </tr>
                                                       <tr>
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" Text="CMYK Primaries"></asp:Label>
                                                            </td>
                                                            <td>
                                                               <asp:TextBox ID="txtCMYKTol1" runat="server" CssClass="textwidth40" onkeypress="return validateNumbers(event)" onchange="return validateTolRange(this,'CMYK',1)">5</asp:TextBox>
                                                            </td>
                                                            <td>
                                                               <asp:TextBox ID="txtCMYKSco1" runat="server" CssClass="textwidth40" onkeypress="return validateNumbers(event)"  onchange="return validateScoRange(this,'CMYK',1)">5</asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCMYKTol2" runat="server" CssClass="textwidth40" onkeypress="return validateNumbers(event)"  onchange="return validateTolRange(this,'CMYK',2)">0.5</asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCMYKSco2" runat="server" CssClass="textwidth40" onkeypress="return validateNumbers(event)"  onchange="return validateScoRange(this,'CMYK',2)">2</asp:TextBox>
                                                            </td>
                                                            <td>
                                                               <asp:TextBox ID="txtCMYKTol3" runat="server" CssClass="textwidth40" onkeypress="return validateNumbers(event)"  onchange="return validateTolRange(this,'CMYK',3)">0.9</asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCMYKSco3" runat="server" CssClass="textwidth40" onkeypress="return validateNumbers(event)"  onchange="return validateScoRange(this,'CMYK',3)">1</asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblCMYKMaxSoc" runat="server" Font-Bold="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                      
                                                       <tr>
                                                            <td>
                                                                <asp:Label ID="Label3" runat="server" Text="RGB Secondaries"></asp:Label>
                                                            </td>
                                                            <td>
                                                               <asp:TextBox ID="txtRGBTol1" runat="server" CssClass="textwidth40" 
                                                                    onkeypress="return validateNumbers(event)" onchange="return validateTolRange(this,'RGB',1)">4</asp:TextBox>
                                                            </td>
                                                            <td>
                                                               <asp:TextBox ID="txtRGBSco1" runat="server" CssClass="textwidth40" 
                                                                    onkeypress="return validateNumbers(event)" onchange="return validateScoRange(this,'RGB',1)">3</asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtRGBTol2" runat="server" CssClass="textwidth40" 
                                                                    onkeypress="return validateNumbers(event)" onchange="return validateTolRange(this,'RGB',2)">1</asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtRGBSco2" runat="server" CssClass="textwidth40" 
                                                                    onkeypress="return validateNumbers(event)" onchange="return validateScoRange(this,'RGB',2)">2</asp:TextBox>
                                                            </td>
                                                            <td>
                                                               <asp:TextBox ID="txtRGBTol3" runat="server" CssClass="textwidth40" 
                                                                    onkeypress="return validateNumbers(event)" onchange="return validateTolRange(this,'RGB',3)">1.5</asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtRGBSco3" runat="server" CssClass="textwidth40" 
                                                                    onkeypress="return validateNumbers(event)" onchange="return validateScoRange(this,'RGB',3)">1</asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblRGBMaxSoc" runat="server" Font-Bold="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label4" runat="server" Text="ΔH Gray"></asp:Label>
                                                            </td>
                                                            <td>
                                                               <asp:TextBox ID="txtCMYTol1" runat="server" CssClass="textwidth40" 
                                                                    onkeypress="return validateNumbers(event)" onchange="return validateTolRange(this,'CMY',1)">4</asp:TextBox>
                                                            </td>
                                                            <td>
                                                               <asp:TextBox ID="txtCMYSco1" runat="server" CssClass="textwidth40" 
                                                                    onkeypress="return validateNumbers(event)" onchange="return validateScoRange(this,'CMY',1)">6</asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCMYTol2" runat="server" CssClass="textwidth40" 
                                                                    onkeypress="return validateNumbers(event)" onchange="return validateTolRange(this,'CMY',2)">2</asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCMYSco2" runat="server" CssClass="textwidth40" 
                                                                    onkeypress="return validateNumbers(event)" onchange="return validateScoRange(this,'CMY',2)">4</asp:TextBox>
                                                            </td>
                                                            <td>
                                                               <asp:TextBox ID="txtCMYTol3" runat="server" CssClass="textwidth40" 
                                                                    onkeypress="return validateNumbers(event)" onchange="return validateTolRange(this,'CMY',3)">2.5</asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCMYSco3" runat="server" CssClass="textwidth40" 
                                                                    onkeypress="return validateNumbers(event)" onchange="return validateScoRange(this,'CMY',3)">2</asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblCMYMaxSoc" runat="server" Font-Bold="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                            <td>
                                                                <asp:Label ID="Label2" runat="server" Text="Paper ΔE"></asp:Label>
                                                            </td>
                                                            <td>
                                                               <asp:TextBox ID="txtPaperTol1" runat="server" CssClass="textwidth40" 
                                                                    onkeypress="return validateNumbers(event)" onchange="return validateTolRange(this,'Paper',1)">6</asp:TextBox>
                                                            </td>
                                                            <td>
                                                               <asp:TextBox ID="txtPaperSco1" runat="server" CssClass="textwidth40" 
                                                                    onkeypress="return validateNumbers(event)" onchange="return validateScoRange(this,'Paper',1)">5</asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtPaperTol2" runat="server" CssClass="textwidth40" 
                                                                    onkeypress="return validateNumbers(event)" onchange="return validateTolRange(this,'Paper',2)">1</asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtPaperSco2" runat="server" CssClass="textwidth40" 
                                                                    onkeypress="return validateNumbers(event)" onchange="return validateScoRange(this,'Paper',2)">2</asp:TextBox>
                                                            </td>
                                                            <td>
                                                               <asp:TextBox ID="txtPaperTol3" runat="server" CssClass="textwidth40" 
                                                                    onkeypress="return validateNumbers(event)" onchange="return validateTolRange(this,'Paper',3)">1.8</asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtPaperSco3" runat="server" CssClass="textwidth40" 
                                                                    onkeypress="return validateNumbers(event)" onchange="return validateScoRange(this,'Paper',3)">1</asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblPaperMaxSoc" runat="server" Font-Bold="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <div class="clear">
                            </div>
                        </div>
                    </div>
                    <div class="section_bottom">
                        &nbsp;</div>
                </div>
            </td>
        </tr>
    
        <tr>
            <td>
                <div class="buttonright" style="padding-left: 3px">
                    <asp:LinkButton ID="lbtnClose" runat="server" OnClientClick="javascript: Close();"
                        CssClass="gray_bt">
                                        <span> <asp:Localize runat="server" ID="locCancel" 
                        Text="Cancel" meta:resourcekey="locCancelResource1" ></asp:Localize> 
                                
 
                                </span>
                    </asp:LinkButton>
                </div>
                <div class="buttonright">
                    <asp:LinkButton ID="lbtnGenerateReport" runat="server" ValidationGroup="vgSaveWindows" 
                        CssClass="gray_bt"
                        OnClientClick="if (!OnClientClick()) {return false;}" 
                        onclick="lbtnGenerateReport_Click" >
                       
                    <span> <asp:Localize ID="locGenReport" runat="server" Text="Generate Report" meta:resourcekey="locGenetareReportResource1" ></asp:Localize> 
                        
 
                        </span>
                    </asp:LinkButton>
                </div>
                <div class="clear">
                </div>
            </td>
        </tr>
    </table>

</div>

<div style="visibility:hidden;">
    <asp:TextBox ID="txt_MeasurementID" runat="server"></asp:TextBox>
    <asp:TextBox ID="txt_dTE" runat="server"></asp:TextBox>
    <asp:TextBox ID="txt_DLPName" runat="server"></asp:TextBox>
    <asp:TextBox ID="txt_Vali_Type" runat="server"></asp:TextBox>
    <asp:TextBox ID="txt_IsAvgMeasurement" runat="server" Text="False"></asp:TextBox>
</div>