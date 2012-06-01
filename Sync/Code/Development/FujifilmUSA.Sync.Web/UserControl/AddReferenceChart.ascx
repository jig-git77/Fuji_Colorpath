<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddReferenceChart.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.AddReferenceChart" %>
<%@ Register Src="~/UserControl/AdminStatusbar.ascx" TagName="AdminStatusbar" TagPrefix="ucAdminStatusbar" %>
<%@ Register Src="~/UserControl/ValidationContainer.ascx" TagName="ValidationContainer"
    TagPrefix="ucValidationContainer" %>
<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle" style="line-height: 34px;">
    <div class="head">
        <h1>
            Reference Chart Management</h1>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px;">
        <center>
            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
            <center>
                <fieldset style="width: 80%;">
                    <legend>
                        <asp:Label ID="lblHeader" runat="server" Text="Add Reference Chart"></asp:Label>
                    </legend>
                    <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" Visible="false" />
                    <table width="98%">
                        <tr>
                            <td align="right">
                                <span style="font-weight: bold;">Project Type<span style="color: Red;">*</span>
                                </span>
                            </td>
                            <td>
                                <b>&nbsp;: &nbsp;</b>
                            </td>
                            <td style="width:100px;">
                                <div style="float: left;">
                                    <asp:DropDownList ID="cmbProjectType" runat="server" Width="400px" CssClass="select"
                                        DataValueField="Key" DataTextField="Value">
                                    </asp:DropDownList>
                                </div>
                                <div style="float: left">
                                    <asp:RequiredFieldValidator ID="rfvProjectType" runat="server" Display="Dynamic"
                                        ControlToValidate="cmbProjectType" ForeColor="Red" ValidationGroup="vgAddRefChart">
                                        <ucValidationContainer:ValidationContainer ID="rfvcProjectTypeContainer" runat="server"
                                            ValidationMsg="Please select project type." Size="200" />
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="clear">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span style="font-weight: bold;">Chart Name<span style="color: Red;">*</span> </span>
                            </td>
                            <td>
                                <b>&nbsp;:&nbsp;</b>
                            </td>
                            <td align="left">
                                <div style="float: left;">
                                    <asp:TextBox ID="txtChartName" runat="server" MaxLength="25" Width="400px" Height="19px"></asp:TextBox>
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
                                <span style="font-weight: bold;">Total Rows<span style="color: Red;">*</span> </span>
                            </td>
                            <td>
                                <b>&nbsp;:&nbsp;</b>
                            </td>
                            <td align="left">
                                <div style="float: left;">
                                    <telerik:RadNumericTextBox ID="txtNumOfRows" runat="server" Width="100px" Height="16px"
                                        MaxValue="500" MinValue="1" NumberFormat-DecimalDigits="0" Type="Number">
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
                                <span style="font-weight: bold;">Total Columns<span style="color: Red;">*</span>
                                </span>
                            </td>
                            <td>
                                <b>&nbsp;:&nbsp;</b>
                            </td>
                            <td align="left">
                                <div style="float: left;">
                                    <telerik:RadNumericTextBox ID="txtNumOfCols" runat="server" Width="100px" Height="16px"
                                        MaxValue="500" MinValue="1" NumberFormat-DecimalDigits="0" Type="Number">
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
                                <span style="font-weight: bold;">Instrument Type<span style="color: Red;">*</span>
                                </span>
                            </td>
                            <td>
                                <b>&nbsp;: &nbsp;</b>
                            </td>
                            <td align="left">
                                <div style="float: left;">
                                    <asp:DropDownList ID="cmbInstrumentType" runat="server" Width="400px" CssClass="select"
                                        DataValueField="Key" DataTextField="Value">
                                    </asp:DropDownList>
                                </div>
                                <div style="float: left">
                                    <asp:RequiredFieldValidator ID="rfvInstrumentType" runat="server" Display="Dynamic"
                                        ControlToValidate="cmbInstrumentType" ForeColor="Red" ValidationGroup="vgAddRefChart">
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
                                <span style="font-weight: bold;">Upload Chart Preview (only image)<span style="color: Red;">*</span>
                                </span>
                            </td>
                            <td>
                                <b>&nbsp;: &nbsp;</b>
                            </td>
                            <td align="left">
                                <div style="float: left;">
                                    <telerik:RadAsyncUpload runat="server" ID="fucUploadChartPreview" MultipleFileSelection="Disabled"
                                        MaxFileInputsCount="1" PostbackTriggers="lbtnSave"  >
                                    </telerik:RadAsyncUpload>
                                    <%--<asp:FileUpload ID="fucUploadChartPreview" runat="server" />--%>
                                </div>
                                <%--<div style="float: left">
                                    <asp:RequiredFieldValidator ID="rfvUploadChartPreview" runat="server" Display="Dynamic"
                                        ControlToValidate="fucUploadChartPreview" ForeColor="Red" ValidationGroup="vgAddRefChart">
                                        <ucValidationContainer:ValidationContainer ID="rfvcUploadChartPreview" runat="server"
                                            ValidationMsg="Please upload chart preview." Size="200" />
                                    </asp:RequiredFieldValidator>
                                </div>--%>
                                <div class="clear">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span style="font-weight: bold;">Upload Original Chart (only pdf)<span style="color: Red;">*</span>
                                </span>
                            </td>
                            <td>
                                <b>&nbsp;: &nbsp;</b>
                            </td>
                            <td align="left">
                                <div style="float: left;">
                                    <telerik:RadAsyncUpload runat="server" ID="fucUploadOriginalChart" MultipleFileSelection="Disabled"
                                        MaxFileInputsCount="1" PostbackTriggers="lbtnSave" >
                                    </telerik:RadAsyncUpload>
                                    <%--<asp:FileUpload ID="fucUploadOriginalChart" runat="server" />--%>
                                </div>
                                <%--<div style="float: left">
                                    <asp:RequiredFieldValidator ID="rfvUploadOriginalChart" runat="server" Display="Dynamic"
                                        ControlToValidate="fucUploadOriginalChart" ForeColor="Red" ValidationGroup="vgAddRefChart">
                                        <ucValidationContainer:ValidationContainer ID="rfvcUploadOriginalChart" runat="server"
                                            ValidationMsg="Please upload original chart." Size="200" />
                                    </asp:RequiredFieldValidator>
                                </div>--%>
                                <div class="clear">
                                </div>
                            </td>
                        </tr>
                        <tr id="Tr_UploadData" runat="server">
                            <td align="right">
                                <span style="font-weight: bold;">Upload Chart Data (C,M,Y,K)<span style="color: Red;">*</span>
                                </span>
                            </td>
                            <td>
                                <b>&nbsp;: &nbsp;</b>
                            </td>
                            <td>
                                <table cellpadding="0" cellspacing="0" style="width:90%;">
                                    <tr>
                                        <td>
                                              <telerik:RadAsyncUpload runat="server" ID="fucUploadChartData" MultipleFileSelection="Disabled"
                                                 MaxFileInputsCount="1" PostbackTriggers="lbtnSave,lbtnUploadChartData" Width="200px">
                                             </telerik:RadAsyncUpload>
                                        </td>
                                        <td>
                                             <%--<div style="float: left">
                                                    <asp:RequiredFieldValidator ID="rfvUploadChartData" runat="server" Display="Dynamic"
                                                        ControlToValidate="fucUploadChartData" ForeColor="Red" ValidationGroup="vgAddRefChart">
                                                        <ucValidationContainer:ValidationContainer ID="rfvcUploadChartData" runat="server"
                                                            ValidationMsg="Please upload chart data." Size="200" />
                                                    </asp:RequiredFieldValidator>
                                                </div>--%>
                                        </td>
                                        <td align="right" style="width:200px;">
                                             <asp:LinkButton ID="lbtnUploadChartData" runat="server" CssClass="gray_bt" OnClick="lbtnUploadChartData_Click" Width="170px">
                                                    <span> Upload Chart Data </span>
                                             </asp:LinkButton>
                                        </td>
                                    </tr>                                   
                                </table>
                              
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span style="font-weight: bold;">Is Used?</span>
                            </td>
                            <td>
                                <b>&nbsp;:&nbsp;</b>
                            </td>
                            <td align="left">
                                <div style="float: left;">
                                    <asp:CheckBox ID="chkIsUsed" runat="server" Checked="true" />
                                </div>
                                <div class="clear">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <telerik:RadGrid ID="rgChartData" ShowHeader="true" AllowFilteringByColumn="true"
                                    AllowSorting="true" AllowPaging="True" PageSize="10" runat="server" AutoGenerateColumns="false"
                                    EnableLinqExpressions="false" Width="99%" HeaderStyle-Font-Bold="true" OnNeedDataSource="rgChartData_OnNeedDataSource">
                                    <PagerStyle Position="Top" AlwaysVisible="true" Font-Size="Small" />
                                    <MasterTableView>
                                        <Columns>
                                            <telerik:GridTemplateColumn DataField="SampleID" AllowFiltering="true" HeaderText="SampleID"
                                                UniqueName="SampleID" SortExpression="SampleID" ItemStyle-CssClass="breakWord200"
                                                ItemStyle-Wrap="true" ShowFilterIcon="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSampleID" runat="server" Text='<%# Eval("SampleID") %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="SampleName" AllowFiltering="true" HeaderText="Sample Name"
                                                UniqueName="SampleName" SortExpression="SampleName" ItemStyle-CssClass="breakWord200"
                                                ItemStyle-Wrap="true" ShowFilterIcon="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSampleName" runat="server" Text='<%# Eval("SampleName") %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="CMYKC" AllowFiltering="true" HeaderText="CMYK_C"
                                                UniqueName="CMYKC" SortExpression="CMYKC" ItemStyle-CssClass="breakWord200" ItemStyle-Wrap="true"
                                                ShowFilterIcon="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCMYKC" runat="server" Text='<%# Eval("CMYKC") %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="CMYKM" AllowFiltering="true" HeaderText="CMYK_M"
                                                UniqueName="CMYKM" SortExpression="CMYKM" ItemStyle-CssClass="breakWord200" ItemStyle-Wrap="true"
                                                ShowFilterIcon="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCMYKM" runat="server" Text='<%# Eval("CMYKM") %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="CMYKY" AllowFiltering="true" HeaderText="CMYK_Y"
                                                UniqueName="CMYKY" SortExpression="CMYKY" ItemStyle-CssClass="breakWord200" ItemStyle-Wrap="true"
                                                ShowFilterIcon="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCMYKY" runat="server" Text='<%# Eval("CMYKY") %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="CMYKK" AllowFiltering="true" HeaderText="CMYK_K"
                                                UniqueName="CMYKK" SortExpression="CMYKK" ItemStyle-CssClass="breakWord200" ItemStyle-Wrap="true"
                                                ShowFilterIcon="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCMYKK" runat="server" Text='<%# Eval("CMYKK") %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <%--<telerik:GridTemplateColumn DataField="PatchTypeId" AllowFiltering="true" HeaderText="Patch Type"
                                                UniqueName="PatchTypeId" SortExpression="PatchTypeId" ItemStyle-CssClass="breakWord200"
                                                ItemStyle-Wrap="true" ShowFilterIcon="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPatchTypeId" runat="server" Text='<%# Eval("PatchTypeId") %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>--%>
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
                                                    <span> Save </span>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="gray_bt" PostBackUrl="~/ReferenceChartManagement.aspx">
                                                    <span> Cancel </span>
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
