<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="AddSpotTarget.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.AddSpotTarget" %>
<%@ Register Src="~/UserControl/AdminStatusbar.ascx" TagName="AdminStatusbar" TagPrefix="ucAdminStatusbar" %>
<%@ Register Src="~/UserControl/ValidationContainer.ascx" TagName="ValidationContainer"
    TagPrefix="ucValidationContainer" %>
<script src="JS/CommonFunctions.js?version=4" type="text/javascript"></script>
<script type="text/javascript">
    function submitFile(sender, e) {
        //alert(1);
        //$("#<%= lbtnUploadChartData.ClientID %>").click();
        //$("<%= lbtnUploadChartData.ClientID %>").click();
        //document.getElementById('<%# lbtnUploadChartData.ClientID %>').click();
    }
</script>
<div class="admin_top">
    &nbsp;
</div>
<div class="admin_middle" style="line-height: 34px;">
    <div class="head">
        <h1>
            <asp:Localize runat="server" Text="Spot Target Management"
                ID="locSpotTargetMgmt" meta:resourcekey="locSpotTargetMgmtResource1"></asp:Localize></h1>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 0px;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <center><center>
            <fieldset style="width: 92%;">
            <legend>
                        <asp:Label ID="lblHeader" runat="server" Text="Spot Target Chart" 
                            meta:resourcekey="lblHeaderResource1"></asp:Label>
            </legend>
            <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" 
                        Visible="False" />
            <table width="98%">   
                        <tr>
                            <td align="right" style="width: 350px;">
                                <span style="font-weight: bold;"><asp:Localize Text="Spot Target Name" runat="server" 
                                    ID="locSpotTargetName" meta:resourcekey="locSpotTargetResource1"></asp:Localize><span style="color: Red;">*</span> </span>
                            </td>
                            <td>
                                <b>&nbsp;:&nbsp;</b>
                            </td>
                            <td align="left">
                                <div style="float: left;"> 
                                                               
                                    <asp:TextBox ID="txtSpotTargetName" runat="server" MaxLength="27" Width="400px" Height="19px" onkeypress="return validateText(this)"></asp:TextBox>
                                      <asp:HiddenField ID="hdnSpotTargetID" runat="server" Value="0" />
                                </div>
                                <div style="float: left">
                                   <asp:RequiredFieldValidator ID="rfvSpotTarget" runat="server" Display="Dynamic" ControlToValidate="txtChartName"
                                        ForeColor="Red" ValidationGroup="vgAddSpotTarget">
                                        <ucValidationContainer:ValidationContainer ID="rfvSpotTargetNameContainer" runat="server"
                                            ValidationMsg="Please provide spot target name." ResourceValidationMsg="SpotTargetNameRequiredResource" Size="200" />
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="clear">
                                </div>
                            </td>
                        </tr>
                        <tr>
                                <td align="right">
                                    <span style="font-weight: bold;"><asp:Localize Text="Company Name" 
                                        ID="locCompanyName" runat="server" meta:resourcekey="locCompanyNameResource1"></asp:Localize><span style="color: Red;">*</span>
                                    </span>
                                </td>
                                <td style="width:8px;">
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <asp:DropDownList ID="cmbCompany" runat="server" Width="400px" CssClass="select"
                                            AutoPostBack="True"                                             
                                            meta:resourcekey="cmbCompanyResource1">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left">
                                        <%--<asp:RequiredFieldValidator ID="rfvcompany" runat="server" Display="Dynamic" ControlToValidate="cmbCompany"
                                            ForeColor="Red" ValidationGroup="vgAddLicense"><ucValidationContainer:ValidationContainer ID="rfvCompanyContainer" runat="server"
                                                ValidationMsg="Please select company." Size="200" />
</asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                        <tr>
                            <td align="right">
                                <span style="font-weight: bold;"><asp:Localize Text="Description" runat="server" 
                                    ID="loc" meta:resourcekey="locDescriptionResource1"></asp:Localize><span style="color: Red;">*</span> </span>
                            </td>
                            <td>
                                <b>&nbsp;:&nbsp;</b>
                            </td>
                            <td align="left">
                                <div style="float: left;"> 
                                                               
                                    <asp:TextBox ID="txtDescription" runat="server" MaxLength="27" Width="400px" Columns="20" Rows="3" TextMode="MultiLine"
                                        Height="50px"></asp:TextBox>
                                </div>
                                <div style="float: left">
                                   <%-- <asp:RequiredFieldValidator ID="rfvChartName" runat="server" Display="Dynamic" ControlToValidate="txtChartName"
                                        ForeColor="Red" ValidationGroup="vgAddRefChart">
                                        <ucValidationContainer:ValidationContainer ID="rfvChartNameContainer" runat="server"
                                            ValidationMsg="Please provide chart name." Size="200" />
                                    </asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="clear">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span style="font-weight: bold;"><asp:Localize Text="Chart Name" runat="server" 
                                    ID="locChartNameResource" meta:resourcekey="locChartNameResource1"></asp:Localize><span style="color: Red;">*</span> </span>
                            </td>
                            <td>
                                <b>&nbsp;:&nbsp;</b>
                            </td>
                            <td align="left">
                                <div style="float: left;">                                                                
                                    <asp:TextBox ID="txtChartName" runat="server" MaxLength="27" Width="400px" 
                                        Height="19px"></asp:TextBox>
                                </div>
                                <div style="float: left">
                                   <asp:RequiredFieldValidator ID="rfvChartName" runat="server" Display="Dynamic" ControlToValidate="txtChartName"
                                        ForeColor="Red" ValidationGroup="vgAddSpotTarget">
                                        <ucValidationContainer:ValidationContainer ID="rfvChartNameContainer" runat="server"
                                            ValidationMsg="Please provide chart name." ResourceValidationMsg="ChartNameRequiredResource" Size="200" />
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="clear">
                                </div>
                            </td>
                        </tr>        
                        <tr>
                            <td align="right">
                                <span style="font-weight: bold;"><asp:Localize Text="Total Rows" runat="server" 
                                    ID="locTotalRows" meta:resourcekey="locTotalRowsResource1"></asp:Localize><span style="color: Red;">*</span> </span>
                            </td>
                            <td>
                                <b>&nbsp;:&nbsp;</b>
                            </td>
                            <td align="left">
                                <div style="float: left;">
                                    <telerik:RadNumericTextBox ID="txtNumOfRows" runat="server" Width="100px" Height="16px"
                                        MaxValue="500" MinValue="1" NumberFormat-DecimalDigits="0" Culture="en-US" 
                                        LabelWidth="" meta:resourcekey="txtNumOfRowsResource1">
                                    </telerik:RadNumericTextBox>
                                </div>
                                <div style="float: left">
                                  <asp:RequiredFieldValidator ID="rfvNumOfRows" runat="server" Display="Dynamic" ControlToValidate="txtNumOfRows"
                                        ForeColor="Red" ValidationGroup="vgAddSpotTarget">
                                        <ucValidationContainer:ValidationContainer ID="rfvcNumOfRowsContainer" runat="server"
                                            ValidationMsg="Please provide total rows." Size="200" ResourceValidationMsg="NumOfRowsRequiredResource"  />
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="clear">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span style="font-weight: bold;"><asp:Localize runat="server" 
                                    Text="Total Columns" ID="locTotalColumns" 
                                    meta:resourcekey="locTotalColumnsResource1"></asp:Localize><span style="color: Red;">*</span>
                                </span>
                            </td>
                            <td>
                                <b>&nbsp;:&nbsp;</b>
                            </td>
                            <td align="left">
                                <div style="float: left;">
                                    <telerik:RadNumericTextBox ID="txtNumOfCols" runat="server" Width="100px" Height="16px"
                                        MaxValue="500" MinValue="1" NumberFormat-DecimalDigits="0" Culture="en-US" LabelWidth="">
                                    </telerik:RadNumericTextBox>
                                </div>
                                <div style="float: left">
                                    <asp:RequiredFieldValidator ID="rfvNumOfCols" runat="server" Display="Dynamic" ControlToValidate="txtNumOfCols"
                                        ForeColor="Red" ValidationGroup="vgAddSpotTarget">
                                        <ucValidationContainer:ValidationContainer ID="rfvcNumOfColsContainer" runat="server"
                                            ValidationMsg="Please provide total columns." Size="200" ResourceValidationMsg="NumOfColsRequiredResource" />
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="clear">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span style="font-weight: bold;"><asp:Localize runat="server" 
                                    Text="Chart Size X" ID="locChartSizeX" 
                                    meta:resourcekey="locChartSizeXResource1"></asp:Localize><span style="color: Red;">*</span>
                                </span>
                            </td>
                            <td>
                                <b>&nbsp;:&nbsp;</b>
                            </td>
                            <td align="left">
                                <div style="float: left;">
                                    <telerik:RadNumericTextBox ID="txtChartSizeX" runat="server" Width="100px" Height="16px"
                                        MaxValue="500" MinValue="1" NumberFormat-DecimalDigits="1" Culture="en-US" LabelWidth="">
                                    </telerik:RadNumericTextBox>
                                </div>
                                <div style="float: left">
                                    <asp:RequiredFieldValidator ID="rfvChartSizeX" runat="server" Display="Dynamic" ControlToValidate="txtChartSizeX"
                                        ForeColor="Red" ValidationGroup="vgAddSpotTarget">
                                        <ucValidationContainer:ValidationContainer ID="rfvcChartSizeXContainer" runat="server"
                                            ValidationMsg="Please provide chart size x." Size="200" ResourceValidationMsg="ChartSizeXRequiredResource"  />
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="clear">
                                </div>
                            </td>
                        </tr>
                        <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize runat="server" 
                                            Text="Chart Size Y" ID="locChartSizeY" 
                                            meta:resourcekey="locChartSizeYResource1"></asp:Localize><span style="color: Red;">*</span>
                                        </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <telerik:RadNumericTextBox ID="txtChartSizeY" runat="server" Width="100px" Height="16px"
                                                MaxValue="500" MinValue="1" NumberFormat-DecimalDigits="1" Culture="en-US" LabelWidth="">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <div style="float: left">
                                            <asp:RequiredFieldValidator ID="rfvChartSizeY" runat="server" Display="Dynamic" ControlToValidate="txtChartSizeY"
                                                ForeColor="Red" ValidationGroup="vgAddSpotTarget">
                                                <ucValidationContainer:ValidationContainer ID="rfvcChartSizeYContainer" runat="server"
                                                    ValidationMsg="Please provide chart size y." Size="200"  ResourceValidationMsg="ChartSizeYRequiredResource"/>
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                        <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize runat="server" 
                                            Text="Patch Size X" ID="locPatchSizeX" 
                                            meta:resourcekey="locPatchSizeXResource1"></asp:Localize><span style="color: Red;">*</span>
                                        </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <telerik:RadNumericTextBox ID="txtPatchSizeX" runat="server" Width="100px" Height="16px"
                                                MaxValue="500" MinValue="1" NumberFormat-DecimalDigits="1" Culture="en-US" LabelWidth="">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <div style="float: left">
                                            <asp:RequiredFieldValidator ID="rfvPatchSizeX" runat="server" Display="Dynamic" ControlToValidate="txtPatchSizeX"
                                                ForeColor="Red" ValidationGroup="vgAddSpotTarget">
                                                <ucValidationContainer:ValidationContainer ID="rfvcPatchSizeXContainer" runat="server"
                                                    ValidationMsg="Please provide patch size x." Size="200"  ResourceValidationMsg="PatchSizeXRequiredResource"/>
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                        <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize runat="server" 
                                            Text="Patch Size Y" ID="locPatchSizeY" 
                                            meta:resourcekey="locPatchSizeYResource1"></asp:Localize><span style="color: Red;">*</span>
                                        </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <telerik:RadNumericTextBox ID="txtPatchSizeY" runat="server" Width="100px" Height="16px"
                                                MaxValue="500" MinValue="1" NumberFormat-DecimalDigits="1" Culture="en-US" LabelWidth="">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <div style="float: left">
                                            <asp:RequiredFieldValidator ID="rfvPatchSizeY" runat="server" Display="Dynamic" ControlToValidate="txtPatchSizeY"
                                                ForeColor="Red" ValidationGroup="vgAddSpotTarget">
                                                <ucValidationContainer:ValidationContainer ID="rfvcPatchSizeYContainer" runat="server"
                                                    ValidationMsg="Please provide patch size y." Size="200" ResourceValidationMsg="PatchSizeYRequiredResource" />
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                        <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize runat="server" 
                                            Text="Pages" ID="locPagesResource" 
                                            meta:resourcekey="locPagesResource1"></asp:Localize><span style="color: Red;">*</span>
                                        </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <telerik:RadNumericTextBox ID="txtPages" runat="server" Width="100px" Height="16px"
                                                MaxValue="500" MinValue="1" NumberFormat-DecimalDigits="0" Culture="en-US" LabelWidth="">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <div style="float: left">
                                            <asp:RequiredFieldValidator ID="rfvPages" runat="server" Display="Dynamic" ControlToValidate="txtPages"
                                                ForeColor="Red" ValidationGroup="vgAddSpotTarget">
                                                <ucValidationContainer:ValidationContainer ID="rfvcPagesContainer" runat="server"
                                                    ValidationMsg="Please provide number of pages." Size="200" ResourceValidationMsg="ChartPagesRequiredResource"  />
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                        <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize runat="server" 
                                            Text="Rows Per Page" ID="locRowsPerPageResource" 
                                            meta:resourcekey="locRowsPerPageResource1"></asp:Localize><span style="color: Red;">*</span>
                                        </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <telerik:RadNumericTextBox ID="txtRowsPerPage" runat="server" Width="100px" Height="16px"
                                                MaxValue="500" MinValue="1" NumberFormat-DecimalDigits="0" Culture="en-US" LabelWidth="" >
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <div style="float: left">
                                            <asp:RequiredFieldValidator ID="rfvRowsPerPage" runat="server" Display="Dynamic" ControlToValidate="txtRowsPerPage"
                                                ForeColor="Red" ValidationGroup="vgAddSpotTarget">
                                                <ucValidationContainer:ValidationContainer ID="rfvcRowsPerPageContainer" runat="server"
                                                    ValidationMsg="Please provide rows per page." Size="200" ResourceValidationMsg="ChartRowsPerPageRequiredResource" />
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                        <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize runat="server" 
                                            Text="Chart Border" ID="locChartBorderResource" 
                                            meta:resourcekey="locChartBorderResource1"></asp:Localize><span style="color: Red;">*</span>
                                        </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <telerik:RadNumericTextBox ID="txtChartBorder" runat="server" Width="100px" Height="16px"
                                                MaxValue="500" MinValue="0" NumberFormat-DecimalDigits="0" Culture="en-US" LabelWidth="">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <div style="float: left">
                                            <asp:RequiredFieldValidator ID="rfvChartBorder" runat="server" Display="Dynamic" ControlToValidate="txtChartBorder"
                                                ForeColor="Red" ValidationGroup="vgAddRefChart">
                                                <ucValidationContainer:ValidationContainer ID="rfvcChartBorder" runat="server"
                                                    ValidationMsg="Please provide chart border size." Size="200" ResourceValidationMsg="ChartBorderSizeRequiredResource"/>
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                        <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize runat="server" 
                                            Text="Vgap" ID="locVgapResource" 
                                            meta:resourcekey="locVgapResource1"></asp:Localize><span style="color: Red;">*</span>
                                        </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <telerik:RadNumericTextBox ID="txtVgap" runat="server" Width="100px" Height="16px"
                                                MaxValue="500" MinValue="0" NumberFormat-DecimalDigits="0" Culture="en-US" LabelWidth="">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <div style="float: left">
                                            <asp:RequiredFieldValidator ID="rfvVgap" runat="server" Display="Dynamic" ControlToValidate="txtVgap"
                                                ForeColor="Red" ValidationGroup="vgAddSpotTarget">
                                                <ucValidationContainer:ValidationContainer ID="rfvcVgap" runat="server"
                                                    ValidationMsg="Please provide vertical gap." Size="200" ResourceValidationMsg="ChartVGapRequiredResource" />
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                        <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize runat="server" 
                                            Text="Hgap" ID="locHgapResource" 
                                            meta:resourcekey="locHgapResource1"></asp:Localize><span style="color: Red;">*</span>
                                        </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <telerik:RadNumericTextBox ID="txtHgap" runat="server" Width="100px" Height="16px"
                                                MaxValue="500" MinValue="0" NumberFormat-DecimalDigits="0" Culture="en-US" LabelWidth="">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <div style="float: left">
                                            <asp:RequiredFieldValidator ID="rfvHgap" runat="server" Display="Dynamic" ControlToValidate="txtHgap"
                                                ForeColor="Red" ValidationGroup="vgAddSpotTarget">
                                                <ucValidationContainer:ValidationContainer ID="rfvcHgap" runat="server"
                                                    ValidationMsg="Please provide horizontal gap." Size="200" ResourceValidationMsg="ChartHGapRequiredResource" />
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                        <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize runat="server" 
                                            Text="GapLevel" ID="locGapLevelResource" 
                                            meta:resourcekey="locGapLevelResource1"></asp:Localize><span style="color: Red;">*</span>
                                        </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <telerik:RadNumericTextBox ID="txtGapLevel" runat="server" Width="100px" Height="16px"
                                                MaxValue="500" MinValue="0" NumberFormat-DecimalDigits="0" Culture="en-US" LabelWidth="">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <div style="float: left">
                                            <asp:RequiredFieldValidator ID="rfvGapLevel" runat="server" Display="Dynamic" ControlToValidate="txtGapLevel"
                                                ForeColor="Red" ValidationGroup="vgAddSpotTarget">
                                                <ucValidationContainer:ValidationContainer ID="rfvcGapLevelContainer" runat="server"
                                                    ValidationMsg="Please provide total columns." Size="200" ResourceValidationMsg="ChartTotalColumnsRequiredResource" />
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                        <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize runat="server" 
                                            Text="PreviousL" ID="locPreviousLResource" 
                                            meta:resourcekey="locPreviousLResource1"></asp:Localize><span style="color: Red;">*</span>
                                        </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <telerik:RadNumericTextBox ID="txtPreviousL" runat="server" Width="100px" Height="16px"
                                                MaxValue="500" MinValue="0" NumberFormat-DecimalDigits="0" Culture="en-US" LabelWidth="">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <div style="float: left">
                                            <asp:RequiredFieldValidator ID="rfvPreviousL" runat="server" Display="Dynamic" ControlToValidate="txtPreviousL"
                                                ForeColor="Red" ValidationGroup="vgAddSpotTarget">
                                                <ucValidationContainer:ValidationContainer ID="rfvcPreviousLContainer" runat="server"
                                                    ValidationMsg="Please provide pervious L." Size="200" ResourceValidationMsg="ChartPrevLRequiredResource" />
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                        <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize runat="server" 
                                            Text="ChartRes" ID="locChartResResource" 
                                            meta:resourcekey="locChartResResource1"></asp:Localize><span style="color: Red;">*</span>
                                        </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <telerik:RadNumericTextBox ID="txtChartRes" runat="server" Width="100px" Height="16px"
                                                MaxValue="500" MinValue="0" NumberFormat-DecimalDigits="0" Culture="en-US" LabelWidth="">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <div style="float: left">
                                            <asp:RequiredFieldValidator ID="rfvChartRes" runat="server" Display="Dynamic" ControlToValidate="txtChartRes"
                                                ForeColor="Red" ValidationGroup="vgAddSpotTarget">
                                                <ucValidationContainer:ValidationContainer ID="rfvcChartResContainer" runat="server"
                                                    ValidationMsg="Please provide chart res." Size="200" ResourceValidationMsg="ChartResRequiredResource" />
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                        </tr>
                        <tr id="Tr_UploadData" runat="server">
                            <td align="right">
                                <span style="font-weight: bold;">
                                <asp:Localize runat="server" 
                                    Text="Upload Chart Data (SAMPLEID, SAMPLE_NAME, CMYK_C, CMYK_M, CMYK_Y, CMYK_K, HTML_RGB)" ID="locUploadChartData" 
                                    meta:resourcekey="locUploadChartDataResource1"></asp:Localize><span style="color: Red;">*</span>
                                </span>
                            </td>
                            <td>
                                <b>&nbsp;: &nbsp;</b>
                            </td>
                            <td>
                                <table cellpadding="0" cellspacing="0" style="width:100%;">
                                    <tr>
                                        <td>
                                              <telerik:RadAsyncUpload runat="server" ID="fucUploadChartData"
                                                 MaxFileInputsCount="1" PostbackTriggers="lbtnSave,lbtnUploadChartData" 
                                                  Width="200px" meta:resourcekey="fucUploadChartDataResource1">
                                             </telerik:RadAsyncUpload>
                                        </td>
                                        <td>
                                           
                                        </td>
                                        <td align="right" style="width:200px;">
                                             <asp:LinkButton ID="lbtnUploadChartData" runat="server" CssClass="gray_bt" 
                                                 Width="170px" OnClick="lbtnUploadChartData_Click" 
                                                 meta:resourcekey="lbtnUploadChartDataResource1">
                                                    <span> <asp:Localize runat="server" Text="Upload Chart Data" 
                                                 ID="locUploadChartDataButton" 
                                                 meta:resourcekey="locUploadChartDataButtonResource1"></asp:Localize> 
                                                </span>
                                             </asp:LinkButton>
                                        </td>
                                    </tr>                                   
                                </table>
                              
                            </td>
                        </tr>
                <tr>
                            <td align="right">
                                <span style="font-weight: bold;"><asp:Localize Text="Is Used?" runat="server" 
                                    id="locIsUsed" meta:resourcekey="locIsUsedResource1"></asp:Localize></span>
                            </td>
                            <td>
                                <b>&nbsp;:&nbsp;</b>
                            </td>
                            <td align="left">
                                <div style="float: left;">
                                    <asp:CheckBox ID="chkIsUsed" runat="server" Checked="True" 
                                        meta:resourcekey="chkIsUsedResource1" />
                                </div>
                                <div class="clear">
                                </div>
                            </td>
                        </tr>
                <tr>
                            <td colspan="3">
                                <telerik:RadGrid ID="rgChartData" AllowFilteringByColumn="True"
                                    AllowSorting="True" AllowPaging="True" runat="server" AutoGenerateColumns="False"
                                    EnableLinqExpressions="False" Width="99%" HeaderStyle-Font-Bold="true" 
                                    OnNeedDataSource="rgChartData_OnNeedDataSource" 
                                    GridLines="None">

                                    <HeaderStyle Font-Bold="True"></HeaderStyle>

                                    <PagerStyle Position="Top" AlwaysVisible="true" Font-Size="Small" />
                                    <MasterTableView>
                                    <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

                                        <Columns>
                                            <telerik:GridTemplateColumn DataField="SpotID" AllowFiltering="true" HeaderText="SpotID"
                                                UniqueName="SpotID" SortExpression="SpotID" ItemStyle-CssClass="breakWord50"
                                                ItemStyle-Wrap="true" ShowFilterIcon="true"  ItemStyle-Width="10%"
                                                meta:resourcekey="GridTemplateColumnResource1">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSpotID" runat="server" Text='<%# Eval("SpotID") %>' 
                                                        meta:resourcekey="lblSpotID" />
                                                </ItemTemplate>                                             
                                            </telerik:GridTemplateColumn>     
                                            <telerik:GridTemplateColumn DataField="SpotName" AllowFiltering="true" HeaderText="Spot Name"
                                                UniqueName="SpotName" SortExpression="SpotName" ItemStyle-CssClass="breakWord50"
                                                ItemStyle-Wrap="true" ShowFilterIcon="true" ItemStyle-Width="15%"
                                                meta:resourcekey="GridTemplateColumnResource2">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSpotName" runat="server" Text='<%# Eval("SpotName") %>' 
                                                        meta:resourcekey="lblSpotNameResource1" />
                                                </ItemTemplate>                                             
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="Target_L" AllowFiltering="true" HeaderText="Target L*"
                                                UniqueName="Target_L" SortExpression="Target_L" 
                                                ItemStyle-CssClass="breakWord50" ItemStyle-Wrap="true" ItemStyle-Width="15%"
                                                ShowFilterIcon="true" meta:resourcekey="GridTemplateColumnResource3">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTarget_L" runat="server" Text='<%# Eval("Target_L") %>' 
                                                        meta:resourcekey="lblTarget_LResource1" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="Target_a" AllowFiltering="true" HeaderText="Target a*"
                                                UniqueName="Target_a" SortExpression="Target_a" 
                                                ItemStyle-CssClass="breakWord50" ItemStyle-Wrap="true" ItemStyle-Width="15%"
                                                ShowFilterIcon="true" meta:resourcekey="GridTemplateColumnResource4">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTarget_a" runat="server" Text='<%# Eval("Target_a") %>' 
                                                        meta:resourcekey="lblTarget_aResource1" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="Target_b" AllowFiltering="true" HeaderText="Target b*"
                                                UniqueName="Target_b" SortExpression="Target_b" 
                                                ItemStyle-CssClass="breakWord50" ItemStyle-Wrap="true" ItemStyle-Width="15%"
                                                ShowFilterIcon="true" meta:resourcekey="GridTemplateColumnResource5">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTarget_b" runat="server" Text='<%# Eval("Target_b") %>' 
                                                        meta:resourcekey="lblTarget_bResource1" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>

                                    </MasterTableView>
                                </telerik:RadGrid>
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
                                        <asp:LinkButton ID="lbtnSave" runat="server" CssClass="gray_bt" ValidationGroup="vgAddSpotTarget" OnClick="lbtnSave_Click">
                                                    <span> <asp:Localize runat="server" Text="Save" ID="locSave" 
                                            meta:resourcekey="locSaveResource1" ></asp:Localize> 
                                             </span>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="gray_bt" 
                                            PostBackUrl="~/SpotTargetManagement.aspx">
                                                    <span> <asp:Localize runat="server" Text="Cancel" ID="locCancel" 
                                            meta:resourcekey="locCancelResource1" ></asp:Localize> 
                                            </span>
                                        </asp:LinkButton>
                                        <input id="Hidden_OriginalChart" type="hidden" value="0" runat="server" />
                                        <input id="Hidden_ChartPreview" type="hidden" value="0" runat="server" />
                                    </div>
                                </div>
                                <div class="clear">
                                </div>
                            </td>
                        </tr>
                </table>
                </fieldset>
        </center></center>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="lbtnSave" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</div>
<div class="admin_bottom">
    &nbsp;
</div>
<div class="clear">
</div>
