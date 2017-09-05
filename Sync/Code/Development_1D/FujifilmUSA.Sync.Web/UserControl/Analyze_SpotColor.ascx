<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Analyze_SpotColor.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.Analyze_SpotColor" %>
<%@ Register Src="~/UserControl/PopupControl/SpotColorPatchDetails.ascx" TagName="SpotColorPatchDetails" TagPrefix="pd" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="uc" %>

<style>
    .PatchDetailsSummaryTable {
        line-height: 35px;
        width: 50%;
    }

        .PatchDetailsSummaryTable th {
            color: black;
            text-align: center;
            font-weight: bold;
            background-color: darkgray;
        }

        .PatchDetailsSummaryTable td {
            text-align: left;
        }
</style>
<script src="JS/ChartTooltip.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {
        //Default Action
        $(".hideclass").hide(); //Hide all content
        $("ul.tabs li:eq(" + $('#<%= hdnSelectedTabIndex.ClientID %>').val() + ")").addClass("active").show(); //Activate first tab
        $(".hideclass:eq(" + $('#<%= hdnSelectedTabIndex.ClientID %>').val() + ")").show(); //Show first tab content
        //On Click Event
        $("ul.tabs li").click(function () {
            $("ul.tabs li").removeClass("active"); //Remove any "active" class
            $('#<%= hdnSelectedTabIndex.ClientID %>').val($(this).index());
            $(this).addClass("active"); //Add "active" class to selected tab
            $(".hideclass").hide(); //Hide all tab content
            var activeTab = $(this).find("a").attr("href"); //Find the rel attribute value to identify the active tab + content
            $(activeTab).fadeIn(); //Fade in the active content

            return false;
        });
    });

    function ShowSpotColorPatchDetailsWindow(htmlColor, actualL, actualA, actualB, targetL, targetA, targetB) {
        var oWindow;
        oWindow = window.radopen("PopupSpotColorPatchDetails.aspx?htmlColor=" + htmlColor + "&actualL=" + actualL + "&actualA=" + actualA + "&actualB=" + actualB + "&targetL=" + targetL + "&targetA=" + targetA + "&targetB=" + targetB, "ClosePrintConditionWindow");
        oWindow.show();
        return false;
    }
</script>
<div class="contant_top">
    &nbsp;
</div>
<div class="contant_middle">
    <div class="head">
    <asp:HiddenField ID="hdnSelectedTabIndex" runat="server" Value="0" />
    <asp:HiddenField ID="hdMeasurement" runat="server" Value="0" />
         <div class="Information_divAnalyze">
                    <asp:LinkButton ID="lbtnDownloadAndSaveNCP" runat="server" CssClass="button" OnClick="lbtnDownloadAndSaveNCP_Click">
                        <span>
                            <asp:Image ID="Image2" runat="server" ImageUrl="../Images/export.png" meta:resourcekey="Image2Resource1" />
                            <asp:Localize runat="server" Text="Create NCP" ID="locCreateNCP" meta:resourcekey="locCreateNCPResource1"></asp:Localize>
                        </span>
                    </asp:LinkButton>
                </div>
           </div>
            <div class="listDevice" style="margin-left: 10px;">
                <uc:Statusbar ID="fujiStatusbar" runat="server" Visible="False" />
            </div>
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
                                    <li><span><a href="#tab2">
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
                                            <div style="padding-left: 10px;">
                                                <%--<pd:SpotColorPatchDetails runat="server" ID="ucSpotColorPathDetails" />--%>

                                                <table class="PatchDetailsSummaryTable">
                                                    <tr>
                                                        <th colspan="2">Optimal Device Predication</th>
                                                    </tr>
                                                    <tr>
                                                        <td><b>Max ΔE</b></td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblMaxDeltaE"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><b>Min ΔE</b></td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblMinDeltaE"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><b>Average ΔE</b></td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblAvgDeltaE"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><b>Media</b></td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblMedia" Text="Epson 24in Proofer Media"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><b>Colorant</b></td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblColorant" Text="Epson UltraChrome HDR/PX-P"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><b>Rip Settings</b></td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblRipSettings" Text="720 dpi"></asp:Label></td>
                                                    </tr>
                                                </table>

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
                                                    OnSortCommand="rgPatchDetails_SortCommand" OnItemCommand="rgPatchDetails_ItemCommand"
                                                    CellSpacing="0" GridLines="None"
                                                    meta:resourcekey="rgPatchDetailsResource1">
                                                    <GroupingSettings CaseSensitive="False" />
                                                   <%-- <ClientSettings>
                                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" FrozenColumnsCount="4"></Scrolling>
                                                    </ClientSettings>--%>
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
                                                                            <asp:LinkButton ID="lbtnClearFillter" runat="server" ToolTip="Clear Filter"  
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
                                                            <telerik:GridTemplateColumn UniqueName="ViewDetail" HeaderText="View" AllowFiltering="False">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="lnkView" runat="server" ImageUrl="~/Images/detail1.png"></asp:ImageButton>
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
                                                                meta:resourcekey="GridBoundColumnResource2" Visible="false">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="CMYK_M" HeaderText="Magenta"
                                                                SortExpression="CMYK_M" FilterControlWidth="30px"
                                                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True"
                                                                UniqueName="CmykM" DataType="System.Int32"
                                                                FilterControlAltText="Filter CmykM column"
                                                                meta:resourcekey="GridBoundColumnResource3" Visible="false">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="CMYK_Y" HeaderText="Yellow"
                                                                SortExpression="CMYK_Y" FilterControlWidth="30px"
                                                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True"
                                                                UniqueName="CmykY" DataType="System.Int32"
                                                                FilterControlAltText="Filter CmykY column"
                                                                meta:resourcekey="GridBoundColumnResource4" Visible="false">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="CMYK_K" HeaderText="Black"
                                                                SortExpression="CMYK_K" FilterControlWidth="30px"
                                                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True"
                                                                UniqueName="CmykK" DataType="System.Int32"
                                                                FilterControlAltText="Filter CmykK column"
                                                                meta:resourcekey="GridBoundColumnResource5" Visible="false">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn DataField="TargetL" HeaderText="Target L*"
                                                                SortExpression="TargetL" FilterControlWidth="30px"
                                                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True"
                                                                FilterControlAltText="Filter TargetL column"
                                                                meta:resourcekey="GridBoundColumnResource9" UniqueName="TargetL" Visible="false">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Targeta" HeaderText="Target a*"
                                                                SortExpression="Targeta" FilterControlWidth="30px"
                                                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True"
                                                                FilterControlAltText="Filter Targeta column"
                                                                meta:resourcekey="GridBoundColumnResource11" UniqueName="Targeta" Visible="false">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Targetb" HeaderText="Target b*"
                                                                SortExpression="Targetb" FilterControlWidth="30px"
                                                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True"
                                                                FilterControlAltText="Filter Targetb column"
                                                                meta:resourcekey="GridBoundColumnResource13" UniqueName="Targetb" Visible="false">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="CIELAB_L" HeaderText="P_L*"
                                                                SortExpression="CIELAB_L" FilterControlWidth="30px"
                                                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True"
                                                                FilterControlAltText="Filter CielabL column"
                                                                meta:resourcekey="GridBoundColumnResource10" UniqueName="CielabL" Visible="false">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="CIELAB_a" HeaderText="P_a*"
                                                                SortExpression="CIELAB_a" FilterControlWidth="30px"
                                                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True"
                                                                FilterControlAltText="Filter CielabA column"
                                                                meta:resourcekey="GridBoundColumnResource12" UniqueName="CielabA" Visible="false">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="CIELAB_b" HeaderText="P_b*"
                                                                SortExpression="CIELAB_b" FilterControlWidth="30px"
                                                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True"
                                                                FilterControlAltText="Filter CielabB column"
                                                                meta:resourcekey="GridBoundColumnResource14" UniqueName="CielabB" Visible="false">
                                                            </telerik:GridBoundColumn>

                                                             <telerik:GridBoundColumn DataField="ΔL" HeaderText="ΔL"
                                                                SortExpression="ΔL" FilterControlWidth="30px"
                                                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True"
                                                                FilterControlAltText="Filter CielabL column"
                                                                meta:resourcekey="GridBoundColumnResource15" UniqueName="ΔL" >
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Δa" HeaderText="Δa"
                                                                SortExpression="Δa" FilterControlWidth="30px"
                                                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True"
                                                                FilterControlAltText="Filter CielabA column"
                                                                meta:resourcekey="GridBoundColumnResource16" UniqueName="Δa">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Δb" HeaderText="Δb"
                                                                SortExpression="Δb" FilterControlWidth="30px"
                                                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True"
                                                                FilterControlAltText="Filter CielabB column"
                                                                meta:resourcekey="GridBoundColumnResource17" UniqueName="Δb">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="ΔE" HeaderText="ΔE" SortExpression="ΔE"
                                                                FilterControlWidth="30px" CurrentFilterFunction="GreaterThanOrEqualTo"
                                                                AutoPostBackOnFilter="True" UniqueName="ΔE"
                                                                FilterControlAltText="Filter ΔE column"
                                                                meta:resourcekey="GridBoundColumnResource6">
                                                            </telerik:GridBoundColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                                <telerik:RadWindowManager ID="RadWindowManager" runat="server" Modal="True"
                                                    Height="240" Width="900" VisibleStatusbar="false" Title="Patch Details"
                                                    ShowContentDuringLoad="false" Behaviors="Move,Close">
                                                    <Windows>
                                                        <telerik:RadWindow ID="windowPatchDetails" runat="server">
                                                        </telerik:RadWindow>
                                                    </Windows>
                                                </telerik:RadWindowManager>
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
