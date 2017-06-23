<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddLookupTVILABDensity.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.AddLookupTVILABDensity" %>
<%@ Register Src="~/UserControl/AdminStatusbar.ascx" TagName="AdminStatusbar" TagPrefix="ucAdminStatusbar" %>
<%@ Register Src="~/UserControl/ValidationContainer.ascx" TagName="ValidationContainer" TagPrefix="ucValidationContainer" %>
<script src="JS/CommonFunctions.js?version=4" type="text/javascript"></script>
<script type = "text/javascript">
    function AllowOnlyNumber(evt) {
        if (evt.charCode > 31 && (evt.charCode < 48 || evt.charCode > 57)) {
            alert("Allow Only Numbers");
            return false;
        }
    }

    function validateTVI(evt) {

        if (evt.charCode > 31 && evt.charCode != 46 && (evt.charCode < 48 || evt.charCode > 57)) {
            alert("Allow Only Numbers");
            return false;
        }
    }

    function validateTVIRange(txtValue) {

        var txtTVI = document.getElementById(txtValue.id);
        var txtTVIvalue = document.getElementById(txtValue.id).value;
        if (txtTVIvalue > 50) {
            alert('TVI value should be between 0 to 50');
            txtTVI.value = "";
            txtTVI.SetFocus();
            return false;
        }

        else
            return true;

    }

    function validateDensityRange(txtValue) {

        var txtDensity = document.getElementById(txtValue.id);
        var txtDensityvalue = document.getElementById(txtValue.id).value;
        if (txtDensityvalue > 2) {
            alert('Density value should be between 0 to 2');
            txtDensity.value = "";
            txtDensity.SetFocus();
            return false;
        }

        else
            return true;

    }

    function AllowOnlyNumberLAB(evt) {

        if (evt.charCode > 31 && evt.charCode != 45 && evt.charCode != 46 && (evt.charCode < 48 || evt.charCode > 57)) {
            alert("Allow Only Numbers");
            return false;
        }
    }

    function validateLABRange(txtValue) {

        var txtLAB = document.getElementById(txtValue.id);
        var txtLABvalue = document.getElementById(txtValue.id).value;
        if (txtLABvalue < -101 || txtLABvalue > 101) {
            alert('LAB value should be between -100 to 100');
            txtLAB.value = "";
            txtLAB.SetFocus();
            return false;
        }

        else
            return true;

    }

     </script>
<style type="text/css">
   .textwidth35
    {
        width: 35px;
    }
    .textwidth40
    {
        width: 40px;
    }
</style>
<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle" style="line-height: 34px;">
    <div class="head">
        <h1><asp:Localize ID="locMetricTargetPreset" runat="server" Text="Add Sync1D Lookup Values Management" 
                meta:resourcekey="locMetricTargetPresetResource1"></asp:Localize></h1>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px;">
        <center>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <center>
                        <fieldset style="width: 60%;">
                            <legend>
                                <asp:Label ID="lblHeader" runat="server" Text="Add Sync 1D Lookup Values"  meta:resourcekey="lblHeaderResource1">
                                  </asp:Label>
                            </legend>
                            <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" 
                                Visible="False" />
                            <table width="90%">
                                
                                 <tr>
                                    <td align="right">
                                          <span style="font-weight: bold;"><asp:Localize Text="Print Standard Type" 
                                            runat="server" ID="locPrintStandardType" 
                                            meta:resourcekey="locPrintStandardTypeResource1"></asp:Localize><span style="color: Red;">*</span>
                                       </span></td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:DropDownList ID="cmbPrintStandardType" runat="server" Width="200px" CssClass="select"
                                              meta:resourcekey="cmbPrintStandardTypeResource1" AutoPostBack="True" 
                                                onselectedindexchanged="cmbPrintStandardType_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div style="float: left">
                                            <asp:RequiredFieldValidator ID="rfvPrintStandardType" runat="server" Display="Dynamic"
                                                ControlToValidate="cmbPrintStandardType" ForeColor="Red" InitialValue="0" 
                                                ValidationGroup="vgAddLookupTVILABDensity"><ucValidationContainer:ValidationContainer ID="rfvcPrintStandardTypeContainer" runat="server"
                                                    ValidationMsg="Please select PrintStandardType." Size="200" />
                                            </asp:RequiredFieldValidator>
                                        </div>                                            
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                  <tr id="TR_FirstTime" visible="false" runat="server" >
                                    <td align="left" style="padding-left:10px;" colspan="3">
                                        <asp:Label ID="lblNewEntryMsg" runat="server" Text="Please choose Project Type because this is first time entries for this Print Standard Type:"></asp:Label>
                                        <asp:RadioButtonList ID="rbt_ProjectType" runat="server" AutoPostBack="True" 
                                            onselectedindexchanged="rbt_ProjectType_SelectedIndexChanged" 
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1">G7</asp:ListItem>
                                            <asp:ListItem Value="2">ISO</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                 <tr id="TR_LABTVI" runat="server">
                                    <td align="left" colspan="3">
                                        <table width="600px" cellpadding="0" cellspacing="10" border="0">
                                            <tr>
                                                <td valign="top" style="width: 60%; padding-right: 0px; border:1px Solid Gray;">  
                                                <div id="dvLAB" style="padding-left:5px;">
                                                                    <label><b><asp:Localize runat="server" Text="LAB Values" 
                                                                            ID="locLAB" meta:resourcekey="locLABResource1" ></asp:Localize></b></label>
                                                                <br />
                                                                <asp:ListView ID="lvLAB" runat="server" 
                                                                        ItemPlaceholderID="phLABItem" 
                                                                        onitemdatabound="lvLAB_ItemDataBound">
                                                                    <LayoutTemplate>
                                                                        <table border="0" cellpadding="0" cellspacing="0">
	                                                                        <tr>
                                                                                <td align="center"><b>Patch</b></td>
                                                                                <td align="center"><b>Target L</b></td>  
                                                                                <td align="center"><b>Target a</b></td>
                                                                                <td align="center"><b>Target b</b></td>                                                                          
                                                                            </tr>
                                                                            <asp:PlaceHolder ID="phLABItem" runat="server"></asp:PlaceHolder>
                                                                        </table>                                                                    
                                                                    </LayoutTemplate>
                                                                    <ItemTemplate>
                                                                            <tr>
		                                                                        <td style="width: 50%">
                                                                                    <asp:Label runat="server" Text='<%# Eval("PatchType.PatchTypeId") %>' ID="lblPatchTypeID" Visible="false"></asp:Label>
			                                                                        <asp:Label runat="server" Text='<%# Eval("PatchType.Type") %>' ID="lblPatchType"></asp:Label>
		                                                                        </td>
		                                                                        <td style="width: 16%">
			                                                                        <asp:TextBox runat="server" Text='<%# Eval("TargetL") %>' ID="txtTargetL" onkeypress="return AllowOnlyNumberLAB(event)"  onchange="return validateLABRange(this)" CssClass="textwidth40" MaxLength="6"></asp:TextBox>  
		                                                                        </td>
                                                                                <td style="width: 16%">
                                                                                    <asp:TextBox runat="server" Text='<%# Eval("Targeta") %>' ID="txtTargeta" onkeypress="return AllowOnlyNumberLAB(event)"  onchange="return validateLABRange(this)" CssClass="textwidth40" MaxLength="6"></asp:TextBox>  
                                                                                </td>
                                                                                <td style="width: 16%">
                                                                                    <asp:TextBox runat="server" Text='<%# Eval("Targetb") %>' ID="txtTargetb" onkeypress="return AllowOnlyNumberLAB(event)"  onchange="return validateLABRange(this)" CssClass="textwidth40" MaxLength="6"></asp:TextBox>  
                                                                                </td>		                                                                    
	                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                                </div>
                                            </td>
                                                <td valign="top" style="width: 40%; border:1px Solid Gray;">
                                                <div id="dvTVI" style="padding-left:5px;">
                                                    <label><b><asp:Localize runat="server" Text="TVI Values" 
                                                            ID="locTVI" meta:resourcekey="locTVIResource1" ></asp:Localize></b></label>
                                                    <br />
                                                
                                                    <table cellspacing="0" cellpadding="0" border="0" width="100%" style="line-height:30px;">
                                                                <tr style="line-height:15px;">
                                                                    <td></td>
                                                                    <td colspan="2" align="center"><b>Targets</b></td>
                                                                </tr>
                                                                <tr style="line-height:15px;">
                                                                    <td align="left">
                                                                        <span><b><asp:Localize ID="locTVIPatch" runat="server" Text="Patch %" meta:resourcekey="locTVIPatchPerResource1"></asp:Localize></b></span>
                                                                    </td>
                                                                    <td align="center">
                                                                        <span><b><asp:Localize ID="locTVICyan" runat="server" Text="CMY" meta:resourcekey="locTVICMYResource1"></asp:Localize></b></span>
                                                                    </td>                                                             
                                                                    <td align="center">
                                                                        <span><b><asp:Localize ID="locTVIBlack" runat="server" Text="K" meta:resourcekey="locTVIBlackResource1"></asp:Localize></b></span>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td><asp:Label ID="lblTVI10" runat="server" Text="10"></asp:Label></td>
                                                                    <td><asp:TextBox ID="txtTVI10CMY" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)"  onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                                    <td><asp:TextBox ID="txtTVI10K" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)"  onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><asp:Label ID="lblTVI20" runat="server" Text="20"></asp:Label></td>
                                                                    <td><asp:TextBox ID="txtTVI20CMY" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)"  onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                                    <td><asp:TextBox ID="txtTVI20K" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)"  onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><asp:Label ID="lblTVI25" runat="server" Text="25"></asp:Label></td>
                                                                    <td><asp:TextBox ID="txtTVI25CMY" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)"  onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                                    <td><asp:TextBox ID="txtTVI25K" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)"  onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                                </tr>     
                                                                <tr>
                                                                    <td><asp:Label ID="lblTVI30" runat="server" Text="30"></asp:Label></td>
                                                                    <td><asp:TextBox ID="txtTVI30CMY" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)"  onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                                    <td><asp:TextBox ID="txtTVI30K" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)"  onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                                </tr> 
                                                                <tr>
                                                                    <td><asp:Label ID="lblTVI40" runat="server" Text="40"></asp:Label></td>
                                                                    <td><asp:TextBox ID="txtTVI40CMY" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)"  onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                                    <td><asp:TextBox ID="txtTVI40K" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)"  onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                                </tr> 
                                                                <tr>
                                                                    <td><asp:Label ID="lblTVI50" runat="server" Text="50"></asp:Label></td>
                                                                    <td><asp:TextBox ID="txtTVI50CMY" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)"  onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                                    <td><asp:TextBox ID="txtTVI50K" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)"  onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                                </tr> 
                                                                <tr>
                                                                    <td><asp:Label ID="lblTVI60" runat="server" Text="60"></asp:Label></td>
                                                                    <td><asp:TextBox ID="txtTVI60CMY" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)"  onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                                    <td><asp:TextBox ID="txtTVI60K" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)"  onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                                </tr> 
                                                                <tr>
                                                                    <td><asp:Label ID="lblTVI70" runat="server" Text="70"></asp:Label></td>
                                                                    <td><asp:TextBox ID="txtTVI70CMY" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)"  onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                                    <td><asp:TextBox ID="txtTVI70K" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)"  onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                                </tr> 
                                                                <tr>
                                                                    <td><asp:Label ID="lblTVI75" runat="server" Text="75"></asp:Label></td>
                                                                    <td><asp:TextBox ID="txtTVI75CMY" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)"  onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                                    <td><asp:TextBox ID="txtTVI75K" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)"  onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                                </tr> 
                                                                <tr>
                                                                    <td><asp:Label ID="lblTVI80" runat="server" Text="80"></asp:Label></td>
                                                                    <td><asp:TextBox ID="txtTVI80CMY" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)"  onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                                    <td><asp:TextBox ID="txtTVI80K" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)"  onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                                </tr>   
                                                                <tr>
                                                                    <td><asp:Label ID="lblTVI90" runat="server" Text="90"></asp:Label></td>
                                                                    <td><asp:TextBox ID="txtTVI90CMY" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)"  onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                                    <td><asp:TextBox ID="txtTVI90K" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)"  onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                                </tr>                                                         
                                                           </table>
                                                </div>    
                                            </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                                 <tr id="TR_Density" runat="server">
                                    <td align="left" colspan="3">
                                        <table width="100%" cellpadding="0" cellspacing="10" border="0">
                                            <tr>
                                                <td valign="top" style="width: 100%; border:1px Solid Gray;">
                                                    <div id="Div1" style="padding-left:5px;">
                                                    <label><b><asp:Localize runat="server" Text="Density Values" 
                                                            ID="Localize1" meta:resourcekey="locTVIResource1" ></asp:Localize></b></label>
                                                    <br />
                                                    <table cellspacing="0" cellpadding="0" border="0" width="50%" style="line-height:23px;">
                                                        <tr>
                                                            <td colspan="4" align="center"><b>Targets</b></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <span><b><asp:Localize ID="Localize3" runat="server" Text="C" meta:resourcekey="locDensityCResource1"></asp:Localize></b></span>
                                                            </td>
                                                            <td align="center">
                                                                <span><b><asp:Localize ID="Localize4" runat="server" Text="M" meta:resourcekey="locDensityMResource1"></asp:Localize></b></span>
                                                            </td>                                                             
                                                            <td align="center">
                                                                <span><b><asp:Localize ID="Localize5" runat="server" Text="Y" meta:resourcekey="locDensityYResource1"></asp:Localize></b></span>
                                                            </td>
                                                            <td align="center">
                                                                <span><b><asp:Localize ID="Localize6" runat="server" Text="K" meta:resourcekey="locDensityKResource1"></asp:Localize></b></span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <asp:TextBox ID="txtDensityC" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)"  onchange="return validateDensityRange(this)"></asp:TextBox>
                                                            </td>
                                                            <td align="center">
                                                                <asp:TextBox ID="txtDensityM" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)"  onchange="return validateDensityRange(this)"></asp:TextBox>
                                                            </td>
                                                            <td align="center">
                                                                <asp:TextBox ID="txtDensityY" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)"  onchange="return validateDensityRange(this)"></asp:TextBox>
                                                            </td>
                                                            <td align="center">
                                                                <asp:TextBox ID="txtDensityK" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)"  onchange="return validateDensityRange(this)"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>    
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td align="left">
                                        <div class="text">
                                            <div style="padding-left: 3px;">
                                                <asp:LinkButton ID="lbtnSave" runat="server" CssClass="gray_bt" ValidationGroup="vgAddLookupTVILABDensity"
                                                    OnClick="lbtnSave_Click">
                                                    <span> <asp:Localize Text="Save" ID="locSave" runat="server" 
                                                    meta:resourcekey="locSaveResource1"></asp:Localize> </span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="gray_bt" 
                                                    PostBackUrl="~/Settings.aspx">
                                                    <span> <asp:Localize Text="Cancel" ID="locCancel" runat="server" 
                                                    meta:resourcekey="locCancelResource1"></asp:Localize> </span>
                                                </asp:LinkButton>
                                             </div>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </center>
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
</div>
<div class="admin_bottom">
    &nbsp;</div>
<div class="clear">
</div>