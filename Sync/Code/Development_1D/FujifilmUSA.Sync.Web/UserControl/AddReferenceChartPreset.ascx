<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddReferenceChartPreset.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.AddReferenceChartPreset" %>
<div class="admin_top">
    &nbsp;
</div>
<div class="admin_middle" style="line-height: 34px;">
    <div class="head">
        <h1>
            <asp:Localize ID="locMetricTargetPreset" runat="server" Text="Metric Target Management"
                meta:resourcekey="locMetricTargetPresetResource1"></asp:Localize></h1>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px;">
        <center>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>                   
                    <center>
                    <fieldset  style="width: 60%;">
                        <legend>
                                <asp:Label ID="Label1" runat="server" Text="Add Metric Sum"  meta:resourcekey="lblHeaderResource1">
                                  </asp:Label>
                            </legend>                      
                            <table>
                                                                 
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize Text="Target Metric Preset" 
                                            Id="Localize1" runat="server" 
                                            meta:resourcekey="locPresetNameResource1"></asp:Localize><span style="color: Red;">*</span> </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                             <asp:DropDownList ID="cmbTargetPresets" runat="server" CssClass="select" DataValueField="Key"
                                                DataTextField="Value" AutoPostBack="True" 
                                                OnSelectedIndexChanged="cmbTargetPresets_SelectedIndexChanged" 
                                                meta:resourcekey="cmbSelectedPresetsResource1">
                                            </asp:DropDownList>
                                        </div>                                        
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="3">
                                        <asp:GridView runat="server" ID="grdPresetRefChart"></asp:GridView>
                                        <%--<asp:GridView runat="server" ID="grdPresetRefChart" AllowPaging="true"
                                            AutoGenerateColumns = "false" Font-Names = "Arial"
                                                Font-Size = "11pt" AlternatingRowStyle-BackColor = "#C2D69B" 
                                                HeaderStyle-BackColor = "green" 
                                                OnPageIndexChanging = "OnPaging" OnRowDataBound = "RowDataBound"  PageSize="10">
                                                <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkAll" runat="server" 
                                                            onclick = "checkAll(this);" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBox1" runat="server"
                                                            onclick = "Check_Click(this)"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField ItemStyle-Width = "150px"
                                                    DataField = "CustomerID" HeaderText = "CustomerID" />
                                                <asp:BoundField ItemStyle-Width = "150px"
                                                    DataField = "City" HeaderText = "City"/>
                                                <asp:BoundField ItemStyle-Width = "150px"
                                                    DataField = "Country" HeaderText = "Country"/>
                                                <asp:BoundField ItemStyle-Width = "150px"
                                                    DataField = "PostalCode" HeaderText = "PostalCode"/>
                                                </Columns>
                                                </asp:GridView>     --%>                                   

<%--                                        <telerik:RadGrid ID="rgMetricTargetPreset" AllowFilteringByColumn="True" AllowSorting="True"
                    AllowPaging="True" PageSize="5" runat="server" AutoGenerateColumns="False" EnableLinqExpressions="False"
                    OnNeedDataSource="rgMetricTargetPreset_NeedDataSource" 
                    Width="98%" CellSpacing="0" GridLines="None"
                    meta:resourcekey="rgMetricTargetPresetResource1">
                    <GroupingSettings CaseSensitive="False" />
                    <HeaderStyle Font-Bold="True" />
                    <PagerStyle Position="Top" AlwaysVisible="True" Font-Size="Small" />
                    <MasterTableView DataKeyNames="ReferenceChartTypeID">                                                
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="Validation_4D" AllowFiltering="False" 
                                HeaderText="V_4D?" FilterControlWidth="50px" DataField="Validation_4D" 
                                FilterControlAltText="Filter Validation_4D column" 
                                meta:resourcekey="GridTemplateColumnResource9">
                                <ItemTemplate>
                                    <asp:Image ID="imgValidation_4D" runat="server" 
                                        ImageUrl='<%# Convert.ToBoolean(Eval("Validation_4D")) ? "~/Images/checkbox_checked.png" : "~/Images/checkbox_unchecked.png" %>' 
                                        meta:resourcekey="imgValidation_4DResource1" />
                                </ItemTemplate>
                                <HeaderStyle Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="MetricTargetPresetName" HeaderText="Preset Name"
                                UniqueName="MetricTargetPresetName" SortExpression="MetricTargetPresetName"
                                FilterControlWidth="90px" AutoPostBackOnFilter="True"
                                CurrentFilterFunction="Contains"
                                FilterControlAltText="Filter MetricTargetPresetName column"
                                meta:resourcekey="GridTemplateColumnResource1">
                                <ItemTemplate>
                                    <asp:Label ID="lblMetricTargetPresetName" runat="server" Text='<%# Eval("MetricTargetPresetName") %>'
                                        meta:resourcekey="lblMetricTargetPresetNameResource1" />
                                </ItemTemplate>
                                <HeaderStyle Width="90px" />
                                <ItemStyle CssClass="breakWord90" Width="90px" Wrap="True" />
                            </telerik:GridTemplateColumn>                                       
                        </Columns>
                    </MasterTableView>                    
                </telerik:RadGrid>--%>
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
                                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="gray_bt" OnClick="lbtnSave_Click">
                                                    <span> <asp:Localize Text="Save" ID="Localize2" runat="server" 
                                                    meta:resourcekey="locSaveResource1"></asp:Localize> </span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton2" runat="server" CssClass="gray_bt"  PostBackUrl="~/MetricTargetManagement.aspx">
                                                    <span> <asp:Localize Text="Cancel" ID="Localize3" runat="server" 
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
    &nbsp;
</div>
<div class="clear">
</div>
