<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="AddReferenceChart.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.AddReferenceChart" %>
<%@ Register Src="~/UserControl/AdminStatusbar.ascx" TagName="AdminStatusbar" TagPrefix="ucAdminStatusbar" %>
<%@ Register Src="~/UserControl/ValidationContainer.ascx" TagName="ValidationContainer"
    TagPrefix="ucValidationContainer" %>

<style type="text/css">    
     .breakWord50
     {
            max-width: 50px !important;
            word-break: break-all !important;
            word-wrap: break-word !important;
            vertical-align: top;
            line-height: 15px;
     }  
</style>

<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle" style="line-height: 34px;">
    <div class="head">
        <h1><asp:Localize runat="server" Text="Reference Chart Management" 
                ID="locReferenceChartMgmt" meta:resourcekey="locReferenceChartMgmtResource1"></asp:Localize></h1>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 0px;">
        <center>
            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
            <center>
                <fieldset style="width: 92%;">
                    <legend>
                        <asp:Label ID="lblHeader" runat="server" Text="Add Reference Chart" 
                            meta:resourcekey="lblHeaderResource1"></asp:Label>
                    </legend>
                    <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" 
                        Visible="False" />
                    <table width="98%">                       
                        <tr>
                            <td align="right">
                                <span style="font-weight: bold;"><asp:Localize Text="Chart Name" runat="server" 
                                    ID="locChartName" meta:resourcekey="locChartNameResource1"></asp:Localize><span style="color: Red;">*</span> </span>
                            </td>
                            <td>
                                <b>&nbsp;:&nbsp;</b>
                            </td>
                            <td align="left">
                                <div style="float: left;"> 
                                                               
                                    <asp:TextBox ID="txtChartName" runat="server" MaxLength="25" Width="400px" 
                                        Height="19px" meta:resourcekey="txtChartNameResource1"></asp:TextBox>
                                      <asp:HiddenField ID="hdnRefChartID" runat="server" Value="0" />
                                </div>
                                <div style="float: left">
                                    <asp:RequiredFieldValidator ID="rfvChartName" runat="server" Display="Dynamic" ControlToValidate="txtChartName"
                                        ForeColor="Red" ValidationGroup="vgAddRefChart">
                                        <ucValidationContainer:ValidationContainer ID="rfvChartNameContainer" runat="server"
                                            ValidationMsg="Please provide chart name." Size="200" />
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
                                        ForeColor="Red" ValidationGroup="vgAddRefChart">
                                        <ucValidationContainer:ValidationContainer ID="rfvcNumOfRowsContainer" runat="server"
                                            ValidationMsg="Please provide total rows." Size="200" />
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
                                        MaxValue="500" MinValue="1" NumberFormat-DecimalDigits="0" Culture="en-US" 
                                        LabelWidth="" meta:resourcekey="txtNumOfColsResource1">
                                    </telerik:RadNumericTextBox>
                                </div>
                                <div style="float: left">
                                    <asp:RequiredFieldValidator ID="rfvNumOfCols" runat="server" Display="Dynamic" ControlToValidate="txtNumOfCols"
                                        ForeColor="Red" ValidationGroup="vgAddRefChart">
                                        <ucValidationContainer:ValidationContainer ID="rfvcNumOfColsContainer" runat="server"
                                            ValidationMsg="Please provide total columns." Size="200" />
                                    </asp:RequiredFieldValidator>
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
                                </span>
                            </td>
                            <td>
                                <b>&nbsp;: &nbsp;</b>
                            </td>
                            <td align="left">
                                <div style="float: left;">
                                    <asp:DropDownList ID="cmbInstrumentType" runat="server" Width="400px" CssClass="select"
                                        DataValueField="Key" DataTextField="Value" 
                                        meta:resourcekey="cmbInstrumentTypeResource1">
                                    </asp:DropDownList>
                                </div>
                                <div style="float: left">
                                    <asp:RequiredFieldValidator ID="rfvInstrumentType" runat="server" Display="Dynamic"
                                        ControlToValidate="cmbInstrumentType" ForeColor="Red" 
                                        ValidationGroup="vgAddRefChart">
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
                                <span style="font-weight: bold;"><asp:Localize Text="Project Type" 
                                    runat="server" ID="Localize3" 
                                    meta:resourcekey="locProjectResource1"></asp:Localize>
                                </span>
                            </td>
                            <td>
                                <b>&nbsp;: &nbsp;</b>
                            </td>
                            <td align="left">
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                             <div style="float: left;">                                   
                                                <asp:CheckBoxList ID="chklist_ProjectType" runat="server" 
                                                     CssClass="checkListBox">
                                                    <asp:ListItem Value="0">Sync1D_ISO: </asp:ListItem>
                                                    <asp:ListItem Value="1">Sync1D_G7: </asp:ListItem>
                                                    <asp:ListItem Value="2">Sync4D: </asp:ListItem>                                       
                                                </asp:CheckBoxList>                                   
                                            </div>
                                            <div style="float: left;">  
                                                <asp:DropDownList ID="cmbSync1D_ISO" runat="server">
                                                    <asp:ListItem Selected="True" Value="1">Profile Chart</asp:ListItem>
                                                    <asp:ListItem Value="2">Optimization Chart</asp:ListItem>
                                                </asp:DropDownList>
                                                <br />
                                                <asp:DropDownList ID="cmbSync1D_G7" runat="server">
                                                    <asp:ListItem Selected="True" Value="1">Profile Chart</asp:ListItem>
                                                    <asp:ListItem Value="2">Optimization Chart</asp:ListItem>
                                                </asp:DropDownList>
                                                <br />
                                                <asp:DropDownList ID="cmbSync4D" runat="server">
                                                    <asp:ListItem Selected="True" Value="1">Profile Chart</asp:ListItem>
                                                    <asp:ListItem Value="2">Optimization Chart</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>                              
                                           
                                        </td>
                                        <td valign="top">
                                            <div style="float: left;">  
                                                <asp:CheckBoxList ID="chklist_Validation" runat="server" 
                                                     CssClass="checkListBox">
                                                    <asp:ListItem Value="0">Validation 1D: </asp:ListItem>
                                                    <asp:ListItem Value="1">Validation 4D: </asp:ListItem>                                                                                    
                                                </asp:CheckBoxList>   
                                            </div>
                                            <div style="float: left;">  
                                                  <asp:DropDownList ID="cmbValidation_1D" runat="server">
                                                    <asp:ListItem Selected="True" Value="1">G7</asp:ListItem>
                                                    <asp:ListItem Value="2">ISO</asp:ListItem>
                                                </asp:DropDownList>
                                                <br />
                                                <asp:DropDownList ID="cmbValidation_4D" runat="server">
                                                    <asp:ListItem Selected="True" Value="1">G7</asp:ListItem>
                                                    <asp:ListItem Value="2">ISO</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </td>
                                       
                                    </tr>
                                    
                                </table>
                               
                            </td>
                        </tr>       
                           
                       
                        <tr>
                            <td align="right">
                                <span style="font-weight: bold;"><asp:Localize runat="server" 
                                    Text="Upload Chart Preview (only image)" ID="locUploadChartPreview" 
                                    meta:resourcekey="locUploadChartPreviewResource1"></asp:Localize><span style="color: Red;">*</span>
                                </span>
                            </td>
                            <td>
                                <b>&nbsp;: &nbsp;</b>
                            </td>
                            <td align="left">
                                <div style="float: left;">
                                    <telerik:RadAsyncUpload runat="server" ID="fucUploadChartPreview"
                                        MaxFileInputsCount="1" PostbackTriggers="lbtnSave" 
                                        meta:resourcekey="fucUploadChartPreviewResource1"  >
                                    </telerik:RadAsyncUpload>                                 
                                </div>                           
                                <div class="clear">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span style="font-weight: bold;">
                                <asp:Localize Text="Upload Original Chart (only pdf)" runat="server" 
                                    ID="locUploadOriginalChart" meta:resourcekey="locUploadOriginalChartResource1"></asp:Localize><span style="color: Red;">*</span>
                                </span>
                            </td>
                            <td>
                                <b>&nbsp;: &nbsp;</b>
                            </td>
                            <td align="left">
                                <div style="float: left;">
                                    <telerik:RadAsyncUpload runat="server" ID="fucUploadOriginalChart"
                                        MaxFileInputsCount="1" PostbackTriggers="lbtnSave" 
                                        meta:resourcekey="fucUploadOriginalChartResource1" >
                                    </telerik:RadAsyncUpload>                                 
                                </div>                           
                                <div class="clear">
                                </div>
                            </td>
                        </tr>
                        <tr id="Tr_UploadData" runat="server">
                            <td align="right" style="width:350px;">
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
                                                 OnClick="lbtnUploadChartData_Click" Width="170px" 
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
                                            <telerik:GridTemplateColumn DataField="SampleID" AllowFiltering="true" HeaderText="SampleID"
                                                UniqueName="SampleID" SortExpression="SampleID" ItemStyle-CssClass="breakWord50"
                                                ItemStyle-Wrap="true" ShowFilterIcon="true"  ItemStyle-Width="10%"
                                                meta:resourcekey="GridTemplateColumnResource1">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSampleID" runat="server" Text='<%# Eval("SampleID") %>' 
                                                        meta:resourcekey="lblSampleIDResource1" />
                                                </ItemTemplate>                                             
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="SampleName" AllowFiltering="true" HeaderText="Sample Name"
                                                UniqueName="SampleName" SortExpression="SampleName" ItemStyle-CssClass="breakWord50"
                                                ItemStyle-Wrap="true" ShowFilterIcon="true" ItemStyle-Width="15%"
                                                meta:resourcekey="GridTemplateColumnResource2">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSampleName" runat="server" Text='<%# Eval("SampleName") %>' 
                                                        meta:resourcekey="lblSampleNameResource1" />
                                                </ItemTemplate>                                             
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="CMYKC" AllowFiltering="true" HeaderText="CMYK_C"
                                                UniqueName="CMYKC" SortExpression="CMYKC" 
                                                ItemStyle-CssClass="breakWord50" ItemStyle-Wrap="true" ItemStyle-Width="15%"
                                                ShowFilterIcon="true" meta:resourcekey="GridTemplateColumnResource3">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCMYKC" runat="server" Text='<%# Eval("CMYKC") %>' 
                                                        meta:resourcekey="lblCMYKCResource1" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="CMYKM" AllowFiltering="true" HeaderText="CMYK_M"
                                                UniqueName="CMYKM" SortExpression="CMYKM" 
                                                ItemStyle-CssClass="breakWord50" ItemStyle-Wrap="true"  ItemStyle-Width="15%"
                                                ShowFilterIcon="true" meta:resourcekey="GridTemplateColumnResource4">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCMYKM" runat="server" Text='<%# Eval("CMYKM") %>' 
                                                        meta:resourcekey="lblCMYKMResource1" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="CMYKY" AllowFiltering="true" HeaderText="CMYK_Y"
                                                UniqueName="CMYKY" SortExpression="CMYKY" 
                                                ItemStyle-CssClass="breakWord50" ItemStyle-Wrap="true" ItemStyle-Width="15%"
                                                ShowFilterIcon="true" meta:resourcekey="GridTemplateColumnResource5">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCMYKY" runat="server" Text='<%# Eval("CMYKY") %>' 
                                                        meta:resourcekey="lblCMYKYResource1" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="CMYKK" AllowFiltering="true" HeaderText="CMYK_K"
                                                UniqueName="CMYKK" SortExpression="CMYKK" 
                                                ItemStyle-CssClass="breakWord50" ItemStyle-Wrap="true" ItemStyle-Width="15%"
                                                ShowFilterIcon="true" meta:resourcekey="GridTemplateColumnResource6">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCMYKK" runat="server" Text='<%# Eval("CMYKK") %>' 
                                                        meta:resourcekey="lblCMYKKResource1" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="HTMLColor" AllowFiltering="true" HeaderText="HTML RGB"
                                                UniqueName="HTMLColor" SortExpression="HTMLColor" ItemStyle-CssClass="breakWord50"
                                                ItemStyle-Wrap="true" ShowFilterIcon="true"  ItemStyle-Width="15%"
                                                meta:resourcekey="GridTemplateColumnResource7">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHTMLColor" runat="server" Text='<%# Eval("HTMLColor") %>' 
                                                        meta:resourcekey="lblHTMLColorResource1" />
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
                                        <asp:LinkButton ID="lbtnSave" runat="server" CssClass="gray_bt" ValidationGroup="vgAddRefChart"
                                            OnClick="lbtnSave_Click">
                                                    <span> <asp:Localize runat="server" Text="Save" ID="locSave" 
                                            meta:resourcekey="locSaveResource1" ></asp:Localize> 
                                             </span>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="gray_bt" 
                                            PostBackUrl="~/ReferenceChartManagement.aspx">
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
            </center>
            <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
        </center>
    </div>
</div>
<div class="admin_bottom">
    &nbsp;</div>
<div class="clear">
</div>
