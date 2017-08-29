<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Analyze_SpotColor.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.Analyze_SpotColor" %>
<div class="contant_top">
    &nbsp;
</div>
<div class="contant_middle">
    <asp:HiddenField ID="hdMeasurement" runat="server" Value="0" />
    <div class="listDevice" style="margin-left: 10px;">
        <div id="divToRender" runat="server">
            <table cellpadding="0" cellspacing="10" border="0" width="97%">
                <tr>
                    <td>
                        <section id="tabpart" style="margin-left: 0;">
                            <section class="inner_tab">
                                <ul class="tabs">
                                    <li><span><a href="#tab1">
                                        <asp:Localize Text="Summary" ID="locSummary"
                                            runat="server" meta:resourcekey="locSummaryResource1"></asp:Localize></a></span></li>
                                    <li id="Tab_PatchDetails" runat="server"><span><a href="#tab2">
                                        <asp:Localize Text="Patch Details" ID="locPatchDetails"
                                            runat="server" meta:resourcekey="locPatchDetailsResource1"></asp:Localize></a></span></li>
                                </ul>
                                <div style="float: right; padding-right: 3px; height: 26px; padding-top: 2px;">
                                    <%--<asp:ImageButton ID="ibtnPrintPreview" runat="server"
                                        ImageUrl="../Images/DymoPrintPreviewIcon.png" ToolTip="Preview label"
                                        OnClientClick="ShowLabelPrintPreview(); return false;"
                                        meta:resourcekey="ibtnPrintPreviewResource1" />
                                    <asp:ImageButton ID="ibtnPrintLabel" runat="server"
                                        ImageUrl="../Images/DymoPrintIcon.png" ToolTip="Print label"
                                        OnClientClick="PrintLabel(false); return false;"
                                        meta:resourcekey="ibtnPrintLabelResource1" />--%>
                                </div>
                                <div class="clear"></div>
                            </section>

                            <div>
                                <section id="tab1" class="contantbox tabcontant hideclass">
                                    <div class="tab_top">&nbsp;</div>
                                    <div class="tab_middle">
                                        <div class="combopart">
                                            <div class="float_l" style="padding-left: 10px;">
                                            </div>
                                            <div class="clear"></div>
                                        </div>
                                    </div>
                                    <div class="tab_bottom">
                                        &nbsp;
                                    </div>
                                </section>
                            </div>

                            <div>
                                <section id="tab2" class="contantbox tabcontant hideclass">
                                    <div class="tab_top">&nbsp;</div>
                                    <div class="tab_middle">
                                        <div class="combopart">
                                            <div class="float_l" style="padding-left: 10px;">
                                                <telerik:RadGrid ID="rgPatchDetails" AllowSorting="True" Width="650px"
                                                    AllowPaging="True" runat="server" AutoGenerateColumns="False" EnableLinqExpressions="False"
                                                    AllowFilteringByColumn="True"
                                                    OnNeedDataSource="rgPatchDetails_NeedDataSource" OnItemDataBound="rgPatchDetails_ItemDataBound" 
                                                     OnSortCommand="rgPatchDetails_SortCommand"  OnItemCommand="rgPatchDetails_ItemCommand"
                                                    CellSpacing="0" GridLines="None"
                                                    meta:resourcekey="rgPatchDetailsResource1">
                                                    <GroupingSettings CaseSensitive="False" />
                                                    <ClientSettings>
                                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" FrozenColumnsCount="7"></Scrolling>
                                                    </ClientSettings>
                                                    <HeaderStyle Font-Bold="True" />
                                                    <PagerStyle Position="Top" AlwaysVisible="True" Font-Size="Small" />
                                                    <MasterTableView DataKeyNames="HTMLColor">
                                                        <CommandItemSettings ExportToPdfText="Export to PDF" />
                                                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column"
                                                            Visible="True">
                                                        </RowIndicatorColumn>
                                                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column"
                                                            Visible="True">
                                                        </ExpandCollapseColumn>
                                                        <Columns>
                                                             <telerik:GridBoundColumn DataField="SpotID" HeaderText="ID"
                                                                SortExpression="SpotID" AllowFiltering="False" UniqueName="SpotID"
                                                                FilterControlAltText="Filter SpotId column"
                                                                meta:resourcekey="GridBoundColumnResource1">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridTemplateColumn UniqueName="HtmlColor" HeaderText="Patch"
                                                                DataField="HTMLColor" FilterControlAltText="Filter HtmlColor column"
                                                                meta:resourcekey="GridTemplateColumnResource1">
                                                                <FilterTemplate>
                                                                    <center>
                                                                            <asp:LinkButton ID="lbtnClearFillter" runat="server" ToolTip="Clear Fillter"  
                                                                                    CommandName="Fillter" meta:resourcekey="lbtnClearFillterResource2">
                                                                                            <span>
                                                                                                <asp:Image ID="imgClearFillter" runat="server" 
                                                                                    AlternateText="Clear Fillter" ImageUrl="../Images/clear-fillter.png" 
                                                                                    meta:resourcekey="imgClearFillterResource1" />


                                                                                            </span>
                                                                            </asp:LinkButton>
                                                                            </center>
                                                                </FilterTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_HTML" runat="server" Height="14px" Width="30px"
                                                                        meta:resourcekey="lbl_HTMLResource1"></asp:Label>
                                                                </ItemTemplate>

                                                            </telerik:GridTemplateColumn>

                                                           
                                                            <telerik:GridBoundColumn DataField="SpotName" HeaderText="Name"
                                                                SortExpression="SpotName" AllowFiltering="False" UniqueName="SpotName"
                                                                FilterControlAltText="Filter SpotName column"
                                                                meta:resourcekey="GridBoundColumnResource38">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="CMYK_C" HeaderText="Cyan"
                                                                SortExpression="CMYK_C" FilterControlWidth="30px"
                                                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True"
                                                                UniqueName="CmykC" DataType="System.Int32"
                                                                FilterControlAltText="Filter CmykC column"
                                                                meta:resourcekey="GridBoundColumnResource2">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="CMYK_M" HeaderText="Magenta"
                                                                SortExpression="CMYK_M" FilterControlWidth="30px"
                                                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True"
                                                                UniqueName="CmykM" DataType="System.Int32"
                                                                FilterControlAltText="Filter CmykM column"
                                                                meta:resourcekey="GridBoundColumnResource3">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="CMYK_Y" HeaderText="Yellow"
                                                                SortExpression="CMYK_Y" FilterControlWidth="30px"
                                                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True"
                                                                UniqueName="CmykY" DataType="System.Int32"
                                                                FilterControlAltText="Filter CmykY column"
                                                                meta:resourcekey="GridBoundColumnResource4">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="CMYK_K" HeaderText="Black"
                                                                SortExpression="CMYK_K" FilterControlWidth="30px"
                                                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True"
                                                                UniqueName="CmykK" DataType="System.Int32"
                                                                FilterControlAltText="Filter CmykK column"
                                                                meta:resourcekey="GridBoundColumnResource5">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn DataField="TargetL" HeaderText="Target L*"
                                                                SortExpression="TargetL" FilterControlWidth="30px"
                                                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True"
                                                                FilterControlAltText="Filter TargetL column"
                                                                meta:resourcekey="GridBoundColumnResource9" UniqueName="TargetL">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Targeta" HeaderText="Target a*"
                                                                SortExpression="Targeta" FilterControlWidth="30px"
                                                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True"
                                                                FilterControlAltText="Filter Targeta column"
                                                                meta:resourcekey="GridBoundColumnResource11" UniqueName="Targeta">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Targetb" HeaderText="Target b*"
                                                                SortExpression="Targetb" FilterControlWidth="30px"
                                                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True"
                                                                FilterControlAltText="Filter Targetb column"
                                                                meta:resourcekey="GridBoundColumnResource13" UniqueName="Targetb">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="CIELAB_L" HeaderText="P_L*"
                                                                SortExpression="CIELAB_L" FilterControlWidth="30px"
                                                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True"
                                                                FilterControlAltText="Filter CielabL column"
                                                                meta:resourcekey="GridBoundColumnResource10" UniqueName="CielabL">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="CIELAB_a" HeaderText="P_a*"
                                                                SortExpression="CIELAB_a" FilterControlWidth="30px"
                                                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True"
                                                                FilterControlAltText="Filter CielabA column"
                                                                meta:resourcekey="GridBoundColumnResource12" UniqueName="CielabA">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="CIELAB_b" HeaderText="P_b*"
                                                                SortExpression="CIELAB_b" FilterControlWidth="30px"
                                                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True"
                                                                FilterControlAltText="Filter CielabB column"
                                                                meta:resourcekey="GridBoundColumnResource14" UniqueName="CielabB">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="ΔE" HeaderText="ΔE2" SortExpression="ΔE"
                                                                FilterControlWidth="30px" CurrentFilterFunction="GreaterThanOrEqualTo"
                                                                AutoPostBackOnFilter="True" UniqueName="ΔE"
                                                                FilterControlAltText="Filter ΔE column"
                                                                meta:resourcekey="GridBoundColumnResource6">
                                                            </telerik:GridBoundColumn>
                                                            <%--<telerik:GridBoundColumn DataField="ΔE94" HeaderText="ΔE94"
                                                                SortExpression="ΔE94" FilterControlWidth="30px"
                                                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True"
                                                                FilterControlAltText="Filter ΔE94 column"
                                                                meta:resourcekey="GridBoundColumnResource7" UniqueName="ΔE94">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="ΔE2000" HeaderText="ΔE2000"
                                                                SortExpression="ΔE2000" FilterControlWidth="30px"
                                                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True"
                                                                FilterControlAltText="Filter ΔE2000 column"
                                                                meta:resourcekey="GridBoundColumnResource8" UniqueName="ΔE2000">
                                                            </telerik:GridBoundColumn>--%>
                                                            <%--<telerik:GridBoundColumn DataField="Density_V" HeaderText="Density_V"
                                                                SortExpression="Density_V" FilterControlWidth="30px"
                                                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True"
                                                                FilterControlAltText="Filter Density_V column"
                                                                meta:resourcekey="GridBoundColumnResource34" UniqueName="Density_V">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Density_C" HeaderText="Density_C"
                                                                SortExpression="Density_C" FilterControlWidth="30px"
                                                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True"
                                                                FilterControlAltText="Filter Density_C column"
                                                                meta:resourcekey="GridBoundColumnResource35" UniqueName="Density_C">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Density_M" HeaderText="Density_M"
                                                                SortExpression="Density_M" FilterControlWidth="30px"
                                                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True"
                                                                FilterControlAltText="Filter Density_M column"
                                                                meta:resourcekey="GridBoundColumnResource36" UniqueName="Density_M">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Density_Y" HeaderText="Density_Y"
                                                                SortExpression="Density_Y" FilterControlWidth="30px"
                                                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True"
                                                                FilterControlAltText="Filter Density_Y column"
                                                                meta:resourcekey="GridBoundColumnResource37" UniqueName="Density_Y">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn DataField="ΔH" HeaderText="ΔH"
                                                                SortExpression="ΔH" FilterControlWidth="30px"
                                                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True"
                                                                FilterControlAltText="Filter ΔH column"
                                                                meta:resourcekey="GridBoundColumnResource15" UniqueName="ΔH" Visible="false">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="ΔCh" HeaderText="ΔCh"
                                                                SortExpression="ΔCh" FilterControlWidth="30px"
                                                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True"
                                                                FilterControlAltText="Filter ΔCh column"
                                                                meta:resourcekey="GridBoundColumnResource16" UniqueName="ΔCh" Visible="false">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="ΔL" HeaderText="ΔL"
                                                                SortExpression="ΔL" FilterControlWidth="30px"
                                                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True"
                                                                FilterControlAltText="Filter ΔL column"
                                                                meta:resourcekey="GridBoundColumnResource17" UniqueName="ΔL" Visible="false">
                                                            </telerik:GridBoundColumn>--%>
                                                        </Columns>



                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                            </div>
                                            <div class="clear"></div>
                                        </div>
                                    </div>
                                    <div class="tab_bottom">&nbsp;</div>
                                </section>
                            </div>
                        </section>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>
