<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddPatchTolerance.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.AddPatchTolerance" %>
<%@ Register Src="~/UserControl/AdminStatusbar.ascx" TagName="AdminStatusbar" TagPrefix="ucAdminStatusbar" %>
<%@ Register Src="~/UserControl/ValidationContainer.ascx" TagName="ValidationContainer" TagPrefix="ucValidationContainer" %>
<script src="JS/CommonFunctions.js?version=4" type="text/javascript"></script>
<script type = "text/javascript">
    function AllowOnlyNumber(evt) {
        if (evt.charCode > 31 && evt.charCode != 46 && (evt.charCode < 48 || evt.charCode > 57)) {
            alert("Allow Only Numbers");
            return false;
        }
    }

    function validateTolRange(txtValue) {

        var txtTol = document.getElementById(txtValue.id);
        var txtTolvalue = document.getElementById(txtValue.id).value;
        if (txtTolvalue > 10) {
            alert('Tolerance value should be between 0 to 10');
            txtTol.value = "";
            txtTol.SetFocus();
            return false;
        }

        else
            return true;

    }

     </script>

<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle" style="line-height: 34px;">
    <div class="head">
        <h1><asp:Localize ID="locMetricTargetPreset" runat="server" Text="Metric Target Patch Level Preset Management" 
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
                                <asp:Label ID="lblHeader" runat="server" Text="Add Metric Target Patch Level Preset"  meta:resourcekey="lblHeaderResource1">
                                  </asp:Label>
                            </legend>
                            <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" 
                                Visible="False" />
                            <table>
                                 <tr>
                                <td align="right">
                                     <span style="font-weight: bold;"><asp:Localize ID="locRegion" runat="server" 
                                         Text="Region" meta:resourcekey="locRegionResource1"></asp:Localize>
                                        </span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <asp:DropDownList ID="cmbRegion" runat="server" DataValueField="Key" DataTextField="Value"
                                            CssClass="select" Width="400px" AutoPostBack="True" 
                                            OnSelectedIndexChanged="cmbRegion_SelectedIndexChanged" 
                                            meta:resourcekey="cmbRegionResource1">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left">
                                    
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize Text="Company Name" 
                                            ID="locCompanyName" runat="server" meta:resourcekey="locCompanyNameResource1"></asp:Localize>
                                        </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;: &nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:DropDownList ID="cmbCompanyName" runat="server" Width="400px" 
                                                CssClass="select" meta:resourcekey="cmbCompanyNameResource1" >                                              
                                            </asp:DropDownList>
                                        </div>
                                        <div style="float: left">
                                           
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize Text="Preset Name" 
                                            Id="locPresetName" runat="server" 
                                            meta:resourcekey="locPresetNameResource1"></asp:Localize><span style="color: Red;">*</span> </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtPresetName" runat="server" MaxLength="50" Width="400px" 
                                                Height="19px" meta:resourcekey="txtPresetNameResource1"></asp:TextBox>
                                        </div>
                                        <div style="float: left">
                                            <asp:RequiredFieldValidator ID="rfvPresetName" runat="server" Display="Dynamic" ControlToValidate="txtPresetName"
                                                ForeColor="Red" ValidationGroup="vgAddMetricTargetPreset"><ucValidationContainer:ValidationContainer ID="rfvPresetContainer" runat="server"
                                                    ValidationMsg="Please provide Preset name." Size="200" />
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize Text="Description" ID="locDescription" 
                                            runat="server" meta:resourcekey="locDescriptionResource1"></asp:Localize>
                                        </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtDescription" runat="server" MaxLength="50" Width="400px" 
                                                Height="52px" meta:resourcekey="txtDescriptionResource1" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                       
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                 
                                 <tr>
                                    <td align="right">
                                          <span style="font-weight: bold;"><asp:Localize Text="Instrument Type" 
                                            runat="server" ID="locInstrumentType" 
                                            meta:resourcekey="locInstrumentTypeResource1"></asp:Localize><span style="color: Red;">*</span>
                                       </span></td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                         <div style="float: left;">
                                            <asp:DropDownList ID="cmbInstrumentType" runat="server" Width="400px" CssClass="select" OnSelectedIndexChanged="cmbInstrumentType_SelectedIndexChanged" AutoPostBack="true"
                                                DataValueField="Key" DataTextField="Value" 
                                                meta:resourcekey="cmbInstrumentTypeResource1">
                                            </asp:DropDownList>
                                        </div>
                                        <div style="float: left">
                                            <asp:RequiredFieldValidator ID="rfvInstrumentType" runat="server" Display="Dynamic"
                                                ControlToValidate="cmbInstrumentType" ForeColor="Red" 
                                                ValidationGroup="vgAddMetricTargetPreset">
                                                <ucValidationContainer:ValidationContainer ID="rfvcInstrumentTypeContainer" runat="server"
                                                    ValidationMsg="Please select instrument type." Size="200" />
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="right">
                                          <span style="font-weight: bold;"><asp:Localize Text="Validation Chart" 
                                            runat="server" ID="locValidationChart" 
                                            meta:resourcekey="locValidationChartResource1"></asp:Localize><span style="color: Red;">*</span>
                                       </span></td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:DropDownList ID="cmbAlignmentChart" runat="server" Width="200px" CssClass="select"
                                              meta:resourcekey="cmbAlignmentChartResource1" AutoPostBack="True" 
                                                onselectedindexchanged="cmbAlignmentChart_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div style="float: left">
                                            <asp:RequiredFieldValidator ID="rfvAlignmentChart" runat="server" Display="Dynamic"
                                                ControlToValidate="cmbAlignmentChart" ForeColor="Red" InitialValue="0" 
                                                ValidationGroup="vgAddMetricTargetPreset"><ucValidationContainer:ValidationContainer ID="rfvcAlignmentChartContainer" runat="server"
                                                    ValidationMsg="Please select Validation chart." Size="200" />
                                            </asp:RequiredFieldValidator>
                                        </div>                                            
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize Text="ΔE Methord" runat="server" 
                                            ID="locEType" meta:resourcekey="locETypeResource1"></asp:Localize>
                                            <span style="color: Red;">*</span>
                                       </span></td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:DropDownList ID="cmbEType" runat="server">
                                                <asp:ListItem Selected="True" Value="ΔE" Text="ΔE"></asp:ListItem>
                                                <asp:ListItem Value="ΔE2000" Text="ΔE2000"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div style="float: left">
                                           
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize ID="locIsActive" runat="server" 
                                            Text="Is Active?" meta:resourcekey="locIsActiveResource1"></asp:Localize></span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" 
                                                meta:resourcekey="chkIsActiveResource1" />
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>



                                 <tr id="TR_MetricTolerance" runat="server" visible="false">
                                  
                                    <td align="left" colspan="3">
                                        <table cellpadding="0" cellspacing="10" border="0">
                                    <tr>
                                        <td valign="top" style="width: 250px; padding-right: 0px; border:1px Solid Gray;">  
                                                        <div id="dvTolerances" style="padding-left:5px;">
                                                             <label><b><asp:Localize runat="server" Text="Metric Target Preset Tolerances" 
                                                                        ID="locTolerances" meta:resourcekey="locTolerancesResource1" ></asp:Localize></b></label>
                                                            <br />
                                                            <asp:ListView ID="lvMTPTolerances" runat="server" 
                                                                 ItemPlaceholderID="phMTPItemTolerances" 
                                                                 onitemdatabound="lvMTPTolerances_ItemDataBound">
                                                                <LayoutTemplate>
                                                                    <table border="0" cellpadding="0" cellspacing="0">
	                                                                    <tr>
                                                                            <td></td>
                                                                            <td></td>                                                                            
                                                                        </tr>
                                                                        <asp:PlaceHolder ID="phMTPItemTolerances" runat="server"></asp:PlaceHolder>
                                                                    </table>                                                                    
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                        <tr>
		                                                                    <td style="width: 160px;">
                                                                              <asp:Label runat="server" Text='<%# Eval("MetricSumID") %>' ID="lblMetricSumID" Visible="false"></asp:Label>
			                                                                  <asp:Label runat="server" Text='<%# Eval("MetricSumName") %>' ID="lblToleranceName"></asp:Label>
		                                                                    </td>
		                                                                    <td>
			                                                                  <asp:TextBox runat="server" Text='<%# Eval("TargetValue") %>' ID="txtTolerance" onkeypress="return AllowOnlyNumber(event)" onchange="return validateTolRange(this)" Width="30px" MaxLength="4"></asp:TextBox>  
		                                                                    </td>		                                                                    
	                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                          </div>
                                        </td>
                                        <td valign="top" style="width: 250px; border:1px Solid Gray;">
                                          
                                                               
                                                        <div id="dvPatchTargets" style="padding-left: 10px">
                                                            <label><b><asp:Localize runat="server" Text="Patch Tolerances" ID="locPatchTargets" meta:resourcekey="locPatchTargetsResource1"></asp:Localize></b>
                                                            </label>
                                                            <br />
                                                           <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <asp:DropDownList ID="cmbPatchSampleList1" runat="server" DataValueField="Key" DataTextField="Value" >
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="txtPatchTolerance1" onkeypress="return AllowOnlyNumber(event)" onchange="return validateTolRange(this)" Width="30px" MaxLength="4"></asp:TextBox>  
                                                                    </td>
                                                                </tr>
                                                                <tr >
                                                                    <td>
                                                                        <asp:DropDownList ID="cmbPatchSampleList2" runat="server" DataValueField="Key" DataTextField="Value" >
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="txtPatchTolerance2" onkeypress="return AllowOnlyNumber(event)" onchange="return validateTolRange(this)" Width="30px" MaxLength="4"></asp:TextBox>  
                                                                    </td>
                                                                </tr>
                                                                <tr >
                                                                    <td>
                                                                        <asp:DropDownList ID="cmbPatchSampleList3" runat="server" DataValueField="Key" DataTextField="Value" >
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="txtPatchTolerance3" onkeypress="return AllowOnlyNumber(event)" onchange="return validateTolRange(this)" Width="30px" MaxLength="4" ></asp:TextBox>  
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:DropDownList ID="cmbPatchSampleList4" runat="server" DataValueField="Key" DataTextField="Value" >
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="txtPatchTolerance4" onkeypress="return AllowOnlyNumber(event)" onchange="return validateTolRange(this)" Width="30px" MaxLength="4" ></asp:TextBox>  
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:DropDownList ID="cmbPatchSampleList5" runat="server" DataValueField="Key" DataTextField="Value" >
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="txtPatchTolerance5" onkeypress="return AllowOnlyNumber(event)" Width="30px" onchange="return validateTolRange(this)" MaxLength="4"></asp:TextBox>  
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
                                                <asp:LinkButton ID="lbtnSave" runat="server" CssClass="gray_bt" ValidationGroup="vgAddMetricTargetPreset"
                                                    OnClick="lbtnSave_Click">
                                                    <span> <asp:Localize Text="Save" ID="locSave" runat="server" 
                                                    meta:resourcekey="locSaveResource1"></asp:Localize> </span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="gray_bt" 
                                                    PostBackUrl="~/PatchPresetManagement.aspx" 
                                                    onclick="lbtnCancel_Click">
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