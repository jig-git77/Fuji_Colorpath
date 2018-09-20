<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddReferenceChartPreset.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.AddReferenceChartPreset" %>
<script type="text/javascript">
    function OnSelectedIndexChange(index, sender) {
        //debugger
        //alert(1);
        var grid = $find("<%=rgMetricTargetPreset.ClientID %>");
        var MasterTable = grid.get_masterTableView();
        var row = MasterTable.get_dataItems()[index];
        var RefID = $(row._element).find('#hdfReferenceChartTypeID').val();
        //var RefID = row.findElement("hdfReferenceChartTypeID").value
        var chk = document.getElementById(sender);
        
        var selectedReferences = $('#hdfSelectedReference').val();        
        if (chk.checked) {
            selectedReferences += (RefID + ',');
        } else {
            var arrRefs = selectedReferences.split(',');
            if (arrRefs.length > 0) {
                selectedReferences = '';
                $.each(arrRefs, function (index, item) {
                    
                    if (item != RefID && item != '') {
                        selectedReferences += (item + ',');
                    }
                });
            }            
        }
        $('#hdfSelectedReference').val(selectedReferences);
           <%--  var textBox;
            if (chk.checked)
            {
                row.findElement("TextBoxCantidadEquipo").disabled = false;
                return;
            }
            row.findElement("TextBoxCantidadEquipo").disabled = true;     --%>
    }
</script>
<div class="admin_top">
    &nbsp;
</div>
<div class="admin_middle" style="line-height: 34px;">
    <div class="head">
        <h1>
            <asp:Localize ID="locMetricTargetPreset" runat="server" Text="Assign Reference Chart To Metric Preset"
                meta:resourcekey="locMetricTargetPresetResource1"></asp:Localize></h1>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px;">
        <center>
            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>                   
                    <asp:HiddenField runat="server" ID="hdfSelectedReference" ClientIDMode="Static" />
                    <center>
                    <fieldset  style="width: 60%;">
                        <legend>
                                <asp:Label ID="Label1" runat="server" Text="Assign Reference Chart To Metric Preset"  meta:resourcekey="lblHeaderResource1">
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
                                        
                                        
                                        <telerik:RadGrid ID="rgMetricTargetPreset" AllowFilteringByColumn="True" AllowSorting="True"
                                        AllowPaging="True" PageSize="10" runat="server" AutoGenerateColumns="False" EnableLinqExpressions="False"                    
                                        Width="98%" CellSpacing="0" GridLines="None" OnNeedDataSource="rgMetricTargetPreset_NeedDataSource" 
                                        OnItemDataBound="rgMetricTargetPreset_ItemDataBound" OnItemCreated="rgMetricTargetPreset_ItemCreated">
                                        <GroupingSettings CaseSensitive="False" />
                                        <HeaderStyle Font-Bold="True" />
                                        <PagerStyle Position="Top" AlwaysVisible="True" Font-Size="Small" />
                                        <MasterTableView DataKeyNames="ReferenceChartTypeID">                                                
                                            <Columns>                                                 
                                                <telerik:GridTemplateColumn UniqueName="IsSelected" AllowFiltering="False" 
                                                    HeaderText="IsSelected" FilterControlWidth="50px" DataField="Validation_4D" 
                                                    FilterControlAltText="Filter IsSelected column" 
                                                    meta:resourcekey="GridTemplateColumnResource9">
                                                    <ItemTemplate>
                                                        <asp:CheckBox runat="server" ID="chkIsSelected" Checked='<%# Convert.ToBoolean(Eval("IsSelected"))%>' />
                                                        <asp:HiddenField runat="server" ID="hdfReferenceChartTypeID" Value='<%# Convert.ToInt32(Eval("ReferenceChartTypeID"))%>' ClientIDMode="Static" />
                                                        <asp:HiddenField runat="server" ID="hdfReferenceChartPresetID" Value='<%# Convert.ToInt32(Eval("ReferenceChartPresetID"))%>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="50px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn 
                                                    UniqueName="ChartName" HeaderText="Chart Name" DataField="ChartName" 
                                                    SortExpression="ChartName" FilterControlWidth="130px" 
                                                    AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" 
                                                    FilterControlAltText="Filter ChartName column" 
                                                    meta:resourcekey="GridTemplateColumnResource1">                            
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblChartName" runat="server" Text='<%# Eval("ChartName") %>' 
                                                            meta:resourcekey="lblChartNameResource1"/>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="130px" />
                                                    <ItemStyle Width="130px" Wrap="True" />
                                                </telerik:GridTemplateColumn>
                                               <telerik:GridTemplateColumn 
                                                    UniqueName="NumOfRows" HeaderText="Num Of Rows" DataField="NumOfRows" 
                                                    SortExpression="NumOfRows" FilterControlWidth="70px" 
                                                    AutoPostBackOnFilter="True" CurrentFilterFunction="GreaterThanOrEqualTo" 
                                                    FilterControlAltText="Filter NumOfRows column" 
                                                    meta:resourcekey="GridTemplateColumnResource2">
                                                    <ItemTemplate>
                                                            <asp:Label ID="lblNumOfRows" runat="server" Text='<%# Eval("NumOfRows") %>' 
                                                                meta:resourcekey="lblNumOfRowsResource1" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="70px" />
                                                    <ItemStyle Width="70px" Wrap="True" />
                                                </telerik:GridTemplateColumn>                        
                                                 <telerik:GridTemplateColumn 
                                                    UniqueName="NumOfCols" HeaderText="Num Of Cols" DataField="NumOfCols" 
                                                    SortExpression="NumOfCols" FilterControlWidth="70px" 
                                                    AutoPostBackOnFilter="True" CurrentFilterFunction="GreaterThanOrEqualTo" 
                                                    FilterControlAltText="Filter NumOfCols column" 
                                                    meta:resourcekey="GridTemplateColumnResource3">
                                                    <ItemTemplate>
                                                            <asp:Label ID="lblNumOfCols" runat="server" Text='<%# Eval("NumOfCols") %>' 
                                                                meta:resourcekey="lblNumOfColsResource1" />
                                                    </ItemTemplate>
                                                     <HeaderStyle Width="70px" />
                                                     <ItemStyle Width="70px" Wrap="True" />
                                                </telerik:GridTemplateColumn>

                                                <telerik:GridTemplateColumn 
                                                    UniqueName="TotalPatches" HeaderText="Total Patches" 
                                                    DataField="TotalPatches" SortExpression="TotalPatches" 
                                                    FilterControlWidth="70px" AutoPostBackOnFilter="True" 
                                                    CurrentFilterFunction="GreaterThanOrEqualTo" 
                                                    FilterControlAltText="Filter TotalPatches column" 
                                                    meta:resourcekey="GridTemplateColumnResource4">
                                                    <ItemTemplate>
                                                            <asp:Label ID="lblTotalPatches" runat="server" 
                                                                Text='<%# Eval("TotalPatches") %>' 
                                                                meta:resourcekey="lblTotalPatchesResource1" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="70px" />
                                                    <ItemStyle Width="70px" Wrap="True" />
                                                </telerik:GridTemplateColumn>
                                                 <telerik:GridTemplateColumn 
                                                    UniqueName="InstrumentName" HeaderText="Instrument Name" 
                                                    DataField="InstrumentName" 
                                                    SortExpression="InstrumentName" FilterControlWidth="90px" 
                                                    AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" 
                                                    FilterControlAltText="Filter InstrumentName column" 
                                                    meta:resourcekey="GridTemplateColumnResource5">
                                                    <ItemTemplate>
                                                            <asp:Label ID="lblInstrumentName" runat="server" 
                                                                Text='<%# Eval("InstrumentName") %>' 
                                                                meta:resourcekey="lblInstrumentNameResource1" />
                                                    </ItemTemplate>
                                                     <HeaderStyle Width="90px" />
                                                     <ItemStyle Width="90px" Wrap="True" />
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
