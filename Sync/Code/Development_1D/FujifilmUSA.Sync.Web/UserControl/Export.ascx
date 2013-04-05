<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="Export.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.Export" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<script language="javascript" type="text/javascript">
    jQuery(document).ready(function () {
        $("div[id*='divAdvancedDownloadsHeader']").click
        (
                function () {
                    var $panel = $("div[id*='divAdvancedDownloadsContian']");
                    var $img = $("div[id*='imgHeader']");
                    $panel.slideToggle(500);
                    if ($img.hasClass('expanded')) {
                        $img.addClass('collapsed').removeClass('expanded');
                    }
                    else {
                        $img.addClass('expanded').removeClass('collapsed');
                    }
                    return false;
                }
        );
        
        var $panel = $("div[id*='divAdvancedDownloadsContian']");
        $panel.slideToggle(0);


        //Default Action
        $(".hideclass").hide(); //Hide all content
        $("ul.tabs li:eq(" + $('#<%= hdnExportCurveSelectedTabIndex.ClientID %>').val() + ")").addClass("active").show(); //Activate first tab
        $(".hideclass:eq(" + $('#<%= hdnExportCurveSelectedTabIndex.ClientID %>').val() + ")").show(); //Show first tab content
        //On Click Event
        $("ul.tabs li").click(function () {
            $("ul.tabs li").removeClass("active"); //Remove any "active" class
            $('#<%= hdnExportCurveSelectedTabIndex.ClientID %>').val($(this).index());
            $(this).addClass("active"); //Add "active" class to selected tab
            $(".hideclass").hide(); //Hide all tab content
            var activeTab = $(this).find("a").attr("href"); //Find the rel attribute value to identify the active tab + content
            $(activeTab).fadeIn(); //Fade in the active content

            return false;
        });
    });
</script>

<div class="contant_top">
</div>
<div class="contant_middle">
    <div class="head">
        <h1 id="header" runat="server"><asp:Localize ID="locDownload" runat="server" 
                Text="Download" meta:resourcekey="locDownloadResource1"></asp:Localize></h1>
        <div class="Information_divAnalyze">
            <asp:LinkButton ID="lbtnMeasure" runat="server" CssClass="button" 
                OnClick="lbtnMeasure_Click">
                <span>
                    <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/analyze.png" 
                meta:resourcekey="imgAddResource1" />
<asp:Localize runat="server" Text="Measure" ID="locMeasure" 
                meta:resourcekey="locMeasureResource1"></asp:Localize>
</span>
            </asp:LinkButton>
        </div>
    </div>
    <div class="listDevice" style="margin-left: 15px;">
        <table cellpadding="0" cellspacing="5" border="0">
            <tr id="tr0">
                <td>
                    <ucStatusbar:Statusbar ID="fujiStatusbar" runat="server" Visible="False" />
                </td>
            </tr>
            <tr id="tr1">
                <td>
                    <div style="display: block;" class="contantbox tabcontant">
                        <div class="section_top">
                            &nbsp;</div>
                        <div class="section_middle">
                            <div class="combopart">
                                <div class="header">
                                    <div class="content">
                                        <div class="text" style="float: left">
                                            <asp:Localize runat="server" Text="Download DLP and Chart" 
                                                ID="locDownloadDLPAndChart" meta:resourcekey="locDownloadDLPAndChartResource1"></asp:Localize>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                </div>
                                <div style="border-style: None; padding: 5px">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <span><asp:Localize runat="server" 
                                                        Text="Download DLP and Implement in Workflow :" 
                                                        ID="locDownloadDLPAndImplementWorkflow" 
                                                        meta:resourcekey="locDownloadDLPAndImplementWorkflowResource1"></asp:Localize></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID="lbtnDownloadDLP" runat="server" CssClass="gray_bt" 
                                                        OnClick="lbtnDownloadDLP_Click">
                                                    <span><asp:Localize runat="server" Text="Download DLP" ID="locDownloadDLP" 
                                                        meta:resourcekey="locDownloadDLPResource1"></asp:Localize>
</span>
                                                    </asp:LinkButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span><asp:Localize Text="Download Chart and Print through DLP Workflow :" 
                                                        runat="server" ID="locDownloadChartAndPrintThroughWorkflow" 
                                                        meta:resourcekey="locDownloadChartAndPrintThroughWorkflowResource1"></asp:Localize></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID="lbtnDownloadChart" runat="server" CssClass="gray_bt" 
                                                        OnClick="lbtnDownloadChart_Click">
                                                    <span><asp:Localize runat="server" Text="Download Chart" ID="locDownloadChart" 
                                                        meta:resourcekey="locDownloadChartResource1"></asp:Localize>
</span>
                                                    </asp:LinkButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                        </div>
                        <div class="section_bottom">
                            &nbsp;</div>
                    </div>
                </td>
            </tr>
            <tr id="tr2">
                <td>
                    <div style="display: block;" class="contantbox tabcontant">
                        <div class="section_top">
                            &nbsp;</div>
                        <div class="section_middle">
                            <div class="combopart">
                                <div id="divAdvancedDownloadsHeader" class="header collapsibleContainerTitle">
                                    <div class="content">
                                        <div class="text collapsibleContainerTitle" style="float: left">
                                            <div id="imgHeader" class="expanded">&nbsp;</div>
                                            <asp:Localize Text="Advanced Downloads" runat="server" 
                                                id="locAdvancedDownloads" meta:resourcekey="locAdvancedDownloadsResource1"></asp:Localize>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                </div>
                                <div id="divAdvancedDownloadsContian" style="border-style: None; padding: 5px">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID="lbtnDownloadDestinationICC" runat="server" CssClass="gray_bt"
                                                        OnClick="lbtnDownloadDestinationICC_Click">
                                                    <span><asp:Localize runat="server" Text="Download Destination ICC" 
                                                        ID="locDownloadDestinationICC" 
                                                        meta:resourcekey="locDownloadDestinationICCResource1"></asp:Localize>
</span>
                                                    </asp:LinkButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                        </div>
                        <div class="section_bottom">
                            &nbsp;</div>
                    </div>
                </td>
            </tr>
            <tr id="trExportViewForSync1D" runat="server" visible ="false">
                <td>
                    <div>
                        <table cellpadding="0" cellspacing="0" border="0" width="97%">
                        <tr>
                            <td>
                                <section id="tabpart" style="margin-left: 0;">
                                    <section class="inner_tab">
                                        <ul class="tabs">
                                            <li><span><a href="#tab1"><asp:Localize Text="Export View" ID="locExportView" 
                                                    runat="server"></asp:Localize></a></span></li>                                                
                                            <li><span><a href="#tab2"><asp:Localize Text="ΔView" ID="locDeltaView" 
                                                    runat="server"></asp:Localize></a></span></li>                                             
                                        </ul>                                                       
                                        <div class="clear"></div>
                                    </section>
                                    <section id="tab1" class="contantbox tabcontant hideclass" >
                                        <div class="tab_top">&nbsp;</div>
                                         <div class="tab_middle">
                                            <div class="combopart">
                                                <div class="float_l">                                                                                                                 
                                                   Export View Chart here                                                          
                                                </div>
                                                <div class="float_r">                                                   
                                                    <telerik:RadGrid ID="rgCurveExportView" AllowFilteringByColumn="False" AllowPaging="True"
                                                        PageSize="5" runat="server" AutoGenerateColumns="False" EnableLinqExpressions="False"
                                                        OnNeedDataSource="rgCurveExportView_NeedDataSource" Width="100%" CellSpacing="0" GridLines="None"
                                                        AllowSorting="False">
                                                        <GroupingSettings CaseSensitive="False" />
                                                        <HeaderStyle Font-Bold="True" />
                                                        <PagerStyle Position="Top" AlwaysVisible="True" Font-Size="Small" />
                                                        <MasterTableView>                                                                    
                                                            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                                                            </RowIndicatorColumn>
                                                            <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                                                            </ExpandCollapseColumn>
                                                            <Columns>                                                                                                              
                                                                <telerik:GridBoundColumn DataField="ReferenceNo" HeaderText="Reference %" UniqueName="ReferenceNo"
                                                                    SortExpression="ReferenceNo">                                                                                        
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="CMYK_C" HeaderText="C" UniqueName="CMYK_C"
                                                                    SortExpression="CMYK_C">                                                                                        
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="CMYK_M" HeaderText="M" UniqueName="CMYK_M"
                                                                    SortExpression="CMYK_M">                                                                                       
                                                                </telerik:GridBoundColumn>
										                        <telerik:GridBoundColumn DataField="CMYK_Y" HeaderText="Y" UniqueName="CMYK_Y"
                                                                    SortExpression="CMYK_Y">                                                                                       
                                                                </telerik:GridBoundColumn>
										                        <telerik:GridBoundColumn DataField="CMYK_K" HeaderText="K" UniqueName="CMYK_K"
                                                                    SortExpression="CMYK_K">                                                                                      
                                                                </telerik:GridBoundColumn>
                                                            </Columns>                                                                   
                                                        </MasterTableView>                                
                                                    </telerik:RadGrid>
                                                </div>
                                                <div class="clear"></div>                                                  
                                            </div>
                                            </div>
                                            <div class="tab_bottom">&nbsp;</div>
                                    </section>   
                                    <section id="tab2" class="contantbox tabcontant hideclass" >
                                        <div class="tab_top">&nbsp;</div>
                                         <div class="tab_middle">
                                            <div class="combopart">
                                                <div class="float_l">                                                                                                                 
                                                 Delta View  Chart here                                                          
                                                </div>
                                                <div class="float_r">                                                  
                                                    <telerik:RadGrid ID="rgCurveDeltaView" AllowFilteringByColumn="False" AllowPaging="True"
                                                        PageSize="5" runat="server" AutoGenerateColumns="False" EnableLinqExpressions="False"
                                                        OnNeedDataSource="rgCurveDeltaView_NeedDataSource" Width="100%" CellSpacing="0" GridLines="None"
                                                        AllowSorting="False">
                                                        <GroupingSettings CaseSensitive="False" />
                                                        <HeaderStyle Font-Bold="True" />
                                                        <PagerStyle Position="Top" AlwaysVisible="True" Font-Size="Small" />
                                                        <MasterTableView>                                                                    
                                                            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                                                            </RowIndicatorColumn>
                                                            <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                                                            </ExpandCollapseColumn>
                                                            <Columns>                                                                                                              
                                                                <telerik:GridBoundColumn DataField="ReferenceNo" HeaderText="Reference %" UniqueName="ReferenceNo"
                                                                    SortExpression="ReferenceNo">                                                                                        
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="CMYK_C" HeaderText="C" UniqueName="CMYK_C"
                                                                    SortExpression="CMYK_C">                                                                                        
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="CMYK_M" HeaderText="M" UniqueName="CMYK_M"
                                                                    SortExpression="CMYK_M">                                                                                       
                                                                </telerik:GridBoundColumn>
										                        <telerik:GridBoundColumn DataField="CMYK_Y" HeaderText="Y" UniqueName="CMYK_Y"
                                                                    SortExpression="CMYK_Y">                                                                                       
                                                                </telerik:GridBoundColumn>
										                        <telerik:GridBoundColumn DataField="CMYK_K" HeaderText="K" UniqueName="CMYK_K"
                                                                    SortExpression="CMYK_K">                                                                                      
                                                                </telerik:GridBoundColumn>
                                                            </Columns>                                                                   
                                                        </MasterTableView>                                
                                                    </telerik:RadGrid>
                                                </div>
                                                <div class="clear"></div>                                                  
                                            </div>
                                            </div>
                                            <div class="tab_bottom">&nbsp;</div>
                                    </section>                                                                         
                                </section>
                            </td>
                        </tr>
                        </table>
                    </div>  
                </td>
            </tr>
        </table>              
    </div>
</div>
<div class="contant_bottom">
    &nbsp;</div>
<div class="clear">
</div>
<asp:HiddenField ID="hdnExportCurveSelectedTabIndex" runat="server" Value="0" />