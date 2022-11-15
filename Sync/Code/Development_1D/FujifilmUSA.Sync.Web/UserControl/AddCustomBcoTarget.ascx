<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddCustomBcoTarget.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.AddCustomBcoTarget" %>
<%@ Register Src="~/UserControl/AdminStatusbar.ascx" TagName="AdminStatusbar" TagPrefix="ucAdminStatusbar" %>
<%@ Register Src="~/UserControl/ValidationContainer.ascx" TagName="ValidationContainer"
    TagPrefix="ucValidationContainer" %>
<script src="JS/CommonFunctions.js?version=4" type="text/javascript"></script>

<div class="admin_top">
    &nbsp;
</div>
<div class="admin_middle" style="line-height: 34px;">
    <div class="head">
        <h1>
            <asp:Localize runat="server" Text="New Custom BCO Library"
                ID="locNewCustomrBCOLibrary" meta:resourcekey="locNewCustomrBCOLibraryResource1"></asp:Localize></h1>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 0px;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <center>
                    <center>
                        <fieldset style="width: 92%;">
                            <legend>
                                <asp:Label ID="lblHeader" runat="server" Text="Add Custom BCO Library"
                                    ></asp:Label>
                            </legend>
                            <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server"
                                Visible="False" />
                            <table width="98%">
                                <tr>
                                    <td align="right" style="width: 350px;">
                                        <span style="font-weight: bold;">
                                            <asp:Localize Text="Spot Target Name" runat="server"
                                                ID="locSpotTargetName" meta:resourcekey="locSpotTargetResource1"></asp:Localize><span style="color: Red;">*</span> </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">

                                            <asp:TextBox ID="txtSpotTargetName" runat="server" MaxLength="27" Width="400px" Height="19px" onkeypress="return validateText(event)"></asp:TextBox>
                                            <asp:HiddenField ID="hdnSpotTargetID" runat="server" Value="0" />
                                        </div>
                                        <div style="float: left">
                                            <asp:RequiredFieldValidator ID="rfvSpotTarget" runat="server" Display="Dynamic" ControlToValidate="txtSpotTargetName"
                                                ForeColor="Red" ValidationGroup="vgAddSpotTarget">
                                                <ucValidationContainer:ValidationContainer ID="rfvSpotTargetNameContainer" runat="server"
                                                    ValidationMsg="Please provide Target Library Name." Size="200" />
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                <tr runat="server" id="trCompanyName">
                                    <td align="right">
                                        <span style="font-weight: bold;">
                                            <asp:Localize Text="Company Name"
                                                ID="locCompanyName" runat="server" meta:resourcekey="locCompanyNameResource1"></asp:Localize><span style="color: Red;">*</span>
                                        </span>
                                    </td>
                                    <td style="width: 8px;">
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
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="vertical-align: top;">
                                        <span style="font-weight: bold;">
                                            <asp:Localize Text="Description" runat="server"
                                                ID="loc" meta:resourcekey="locDescriptionResource1"></asp:Localize></span>
                                    </td>
                                    <td style="vertical-align: top;">
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">

                                            <asp:TextBox ID="txtDescription" runat="server" MaxLength="27" Width="400px" Columns="20" Rows="3" TextMode="MultiLine"
                                                Height="50px"></asp:TextBox>
                                        </div>
                                        <div style="float: left">
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;">
                                            <asp:Localize Text="Total Rows" runat="server"
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
                                                    ValidationMsg="Please provide total rows." Size="200" ResourceValidationMsg="NumOfRowsRequiredResource" />
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;">
                                            <asp:Localize runat="server"
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
                                        <span style="font-weight: bold;">
                                            <asp:Localize runat="server"
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
                                                    ValidationMsg="Please provide number of pages." Size="200" ResourceValidationMsg="ChartPagesRequiredResource" />
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;">
                                            <asp:Localize runat="server"
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
                                                MaxValue="500" MinValue="1" NumberFormat-DecimalDigits="0" Culture="en-US" LabelWidth="">
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

                                <tr id="TR_Density" runat="server">
                                    <td align="left" colspan="3">
                                        <table width="100%" cellpadding="0" cellspacing="0" style="border: 1px Solid Gray;">
                                            <tr>
                                                <td valign="top" style="width: 100%;">
                                                    <div id="Div1" style="padding-left: 5px;">
                                                        <label>
                                                            <b>
                                                                <asp:Localize runat="server" Text="Option 1: Upload a set of colors"
                                                                    ID="Localize1"></asp:Localize></b></label>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" style="width: 100%; border-top: 1px Solid Gray; margin: 0;">
                                                    <table cellspacing="0" cellpadding="0" border="0" width="100%" style="line-height: 23px; margin-top:5px;">
                                                        <tr id="Tr_UploadData" runat="server" >
                                                            <td align="left" style="vertical-align: top; padding-left: 5px;">
                                                                <span style="font-weight: bold;">
                                                                    <asp:Localize runat="server"
                                                                        Text="Color Library CSV" ID="locUploadChartData"
                                                                        meta:resourcekey="locUploadChartDataResource1"></asp:Localize><span style="color: Red;">*</span>
                                                                </span>
                                                            </td>
                                                            <td>
                                                                <b>&nbsp;: &nbsp;</b>
                                                            </td>
                                                            <td style="vertical-align: top;">
                                                                <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                                                    <tr>
                                                                        <td>
                                                                            <telerik:RadAsyncUpload runat="server" ID="fucUploadChartData"
                                                                                MaxFileInputsCount="1" PostbackTriggers="lbtnSave,lbtnUploadChartData"
                                                                                Width="200px" meta:resourcekey="fucUploadChartDataResource1">
                                                                            </telerik:RadAsyncUpload>
                                                                        </td>
                                                                        <td></td>
                                                                        <td align="right" style="width: 200px;">
                                                                            <asp:LinkButton ID="lbtnUploadChartData" runat="server" CssClass="gray_bt"
                                                                                OnClick="lbtnUploadChartData_Click"
                                                                                meta:resourcekey="lbtnUploadChartDataResource1">
                                                                                <span>
                                                                                    <asp:Localize runat="server" Text="Submit"
                                                                                        ID="locUploadChartDataButton"
                                                                                        meta:resourcekey="locUploadChartDataButtonResource1"></asp:Localize>
                                                                                </span>
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>

                                                            </td>
                                                        </tr>

                                                    </table>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 5px;">
                                                    <i>*The CSV should be formatted with four columns: [Color_Name,Target_L*,Target_a*,Target_b*].
                                                        <br />
                                                        *The Color_Name should not include spaces (use _Underscores).
                                                        <br />
                                                        <a href="Download/SampleCustomSpotTarget.csv">
                                                            <input type='button' value='Download Sample CSV'></a>
                                                        </a>
                                                    </i>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;">
                                            <asp:Localize Text="Is Used?" runat="server"
                                                ID="locIsUsed" meta:resourcekey="locIsUsedResource1"></asp:Localize></span>
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
                                            <MasterTableView CommandItemDisplay="Top" >
                                                <CommandItemSettings
                                                    ShowExportToCsvButton="true"
                                                    ShowRefreshButton="false"
                                                    ShowAddNewRecordButton="false" />
                                                <Columns>
                                                    <telerik:GridTemplateColumn DataField="ColorID" AllowFiltering="true" HeaderText="ColorID"
                                                        UniqueName="ColorID" SortExpression="ColorID" ItemStyle-CssClass="breakWord50"
                                                        ItemStyle-Wrap="true" ShowFilterIcon="true" ItemStyle-Width="10%"
                                                        meta:resourcekey="GridTemplateColumnResource1">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblColorID" runat="server" Text='<%# Eval("SpotID") %>'
                                                                meta:resourcekey="lblColorID" />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn DataField="ColorName" AllowFiltering="true" HeaderText="Spot Name"
                                                        UniqueName="ColorName" SortExpression="ColorName" ItemStyle-CssClass="breakWord50"
                                                        ItemStyle-Wrap="true" ShowFilterIcon="true" ItemStyle-Width="15%"
                                                        meta:resourcekey="GridTemplateColumnResource2">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblColorName" runat="server" Text='<%# Eval("SpotName") %>'
                                                                meta:resourcekey="lblColorNameResource1" />
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
                                    <td></td>
                                    <td></td>
                                    <td align="left">
                                        <div class="text">
                                            <div style="padding-left: 3px;">
                                                <asp:LinkButton ID="lbtnSave" runat="server" CssClass="gray_bt" ValidationGroup="vgAddSpotTarget" OnClick="lbtnSave_Click">
                                                    <span>
                                                        <asp:Localize runat="server" Text="Save" ID="locSave"
                                                            meta:resourcekey="locSaveResource1"></asp:Localize>
                                                    </span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="gray_bt"
                                                    PostBackUrl="~/CustomBcoManagement.aspx">
                                                    <span>
                                                        <asp:Localize runat="server" Text="Cancel" ID="locCancel"
                                                            meta:resourcekey="locCancelResource1"></asp:Localize>
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
                </center>
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
