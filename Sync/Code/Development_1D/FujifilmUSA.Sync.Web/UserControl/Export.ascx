<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="Export.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.Export" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>

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
                                            <div id="Div_Chart" runat="server">
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
                                            </div>
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
            <tr id="trAdvancedDownload" runat="server">
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
                 <%-- <asp:UpdatePanel ID="UpdPanExportView" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>--%>
                    <div>
                        <table cellpadding="0" cellspacing="0" border="0" width="97%">
                        <tr>
                            <td>
                               
                                <section id="tabpart" style="margin-left: 0;">
                                    <section class="inner_tab">
                                        <ul class="tabs">
                                            <li><span><a href="#tab1"><asp:Localize Text="Export View" ID="locExportView" 
                                                    runat="server" meta:resourcekey="locExportViewResource1"></asp:Localize></a></span></li>                                                
                                            <li><span><a href="#tab2"><asp:Localize Text="ΔView" ID="locDeltaView" 
                                                    runat="server" meta:resourcekey="locDeltaViewResource1"></asp:Localize></a></span></li>                                             
                                        </ul>                                                       
                                        <div class="clear"></div>
                                    </section>
                                    <section id="tab1" class="contantbox tabcontant hideclass" >
                                        <div class="tab_top">&nbsp;</div>
                                         <div class="tab_middle">
                                            <div class="combopart"> 
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td colspan="2" style="padding: 5px">
                                                            <asp:Localize ID="locCurveType" runat="server"  Text="Curve Type :" meta:resourcekey="locCurveTypeResource1"></asp:Localize>
                                                            <asp:DropDownList ID="DdlCurveType" runat="server" Width="120px" CssClass="DROPDOWNLIST"
                                                                AutoPostBack="True" OnSelectedIndexChanged="DdlCurveType_SelectedIndexChanged">
                                                                <asp:ListItem Text="1%" meta:resourcekey="onepersResource1" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="5%" meta:resourcekey="fivepersResource1" Value="5"></asp:ListItem>
                                                                <asp:ListItem Text="10%" meta:resourcekey="tenpersResource1" Value="10"></asp:ListItem>
                                                                <asp:ListItem Text="Rampage" meta:resourcekey="RampageResource1" Value="15"></asp:ListItem>
                                                                <asp:ListItem Text="XMF" meta:resourcekey="XMFResource1" Value="25"></asp:ListItem>
                                                                <asp:ListItem Text="Harmony" meta:resourcekey="HarmonyResource1" Value="35"></asp:ListItem>
                                                                <asp:ListItem Text="Nexus" Value="45" meta:resourcekey="NexusResource1"></asp:ListItem>                                                                
                                                                <asp:ListItem Text="Caldera" meta:resourcekey="CalderaResource1" Value="55"></asp:ListItem>
                                                                <asp:ListItem Text="ColorGate-TVI" meta:resourcekey="ColTVIResource1" Value="100"></asp:ListItem>
                                                                <asp:ListItem Text="ColorGate-CGCX" meta:resourcekey="ColCGCXResource1" Value="101"></asp:ListItem>   
                                                                <asp:ListItem Text="Onyx G7 Import" meta:resourcekey="OnyxG7ImportResource1" Value="102"></asp:ListItem>                                                           
                                                            </asp:DropDownList>
                                                            <asp:HiddenField ID="HD_CurveType" runat="server" Value="1"></asp:HiddenField>
                                                            &nbsp;&nbsp;
                                                            <asp:CheckBox ID="Chk_C" runat="server" Text="Cyan" Checked="true" AutoPostBack="True"
                                                                OnCheckedChanged="Chk_CheckedChanged" meta:resourcekey="Chk_CResource1" />&nbsp;
                                                            <asp:CheckBox ID="Chk_M" runat="server" Text="Magenta" Checked="true" AutoPostBack="True"
                                                                OnCheckedChanged="Chk_CheckedChanged" meta:resourcekey="Chk_MResource1" />&nbsp;
                                                            <asp:CheckBox ID="Chk_Y" runat="server" Text="Yellow" Checked="true" AutoPostBack="True"
                                                                OnCheckedChanged="Chk_CheckedChanged" meta:resourcekey="Chk_YResource1" />&nbsp;
                                                            <asp:CheckBox ID="Chk_K" runat="server" Text="Black" Checked="true" AutoPostBack="True"
                                                                OnCheckedChanged="Chk_CheckedChanged" meta:resourcekey="Chk_KResource1" />
                                                        </td>
                                                    </tr>
                                                    <tr style="border: solid 1px;">
                                                        <td style="width: 60%">
                                                            <asp:Chart ID="CHRT_ExportCurve" runat="server" Width="410px" Height="350px" ImageType="Jpeg"
                                                                ImageUrl="~/UploadFiles/temp/CHRT_ExportCurve" BackGradientType="Center" Palette="SemiTransparent"
                                                                BackGradientEndColor="White" BorderLineColor="#808080" BackColor="#EDEDED">
                                                                <Series>
                                                                    <asp:Series Name="Series1" IsVisibleInLegend="false" Color="Cyan" BorderWidth="2" ChartType="Spline"
                                                                        MarkerBorderColor="64, 64, 64" MarkerStyle="Circle" MarkerSize="2">
                                                                    </asp:Series>
                                                                    <asp:Series Name="Series2" IsVisibleInLegend="false" Color="Magenta" BorderWidth="2"
                                                                        ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle" MarkerSize="2">
                                                                    </asp:Series>
                                                                    <asp:Series Name="Series3" IsVisibleInLegend="false" Color="Yellow" BorderWidth="2" ChartType="Spline"
                                                                        MarkerBorderColor="64, 64, 64" MarkerStyle="Circle" MarkerSize="2">
                                                                    </asp:Series>
                                                                    <asp:Series Name="Series4" IsVisibleInLegend="false" Color="Black" BorderWidth="2" ChartType="Spline"
                                                                        MarkerBorderColor="64, 64, 64" MarkerStyle="Circle" MarkerSize="2">
                                                                    </asp:Series>
                                                                </Series>
                                                                <BorderSkin SkinStyle="None"></BorderSkin>
                                                               
                                                                <ChartAreas>
                                                                    <asp:ChartArea Name="Default" BorderColor="26, 59, 105" BackSecondaryColor="White"
                                                                        BackColor="Gray" BackGradientStyle="TopBottom" BorderDashStyle="Solid" ShadowOffset="1">
                                                                        <Position Height="99" Width="94" X="0" Y="2" />
                                                                        <AxisY IsLabelAutoFit="False" Title="" Interval="10" Minimum="0" Maximum="100">
                                                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                                                        </AxisY>
                                                                        <AxisX IsLabelAutoFit="False" IsMarginVisible="False" Interval="25" Minimum="0" Maximum="100"
                                                                            Title="">
                                                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                                                            <LabelStyle Angle="0" />
                                                                        </AxisX>
                                                                    </asp:ChartArea>
                                                                </ChartAreas>
                                                            </asp:Chart>                                                           
                                                        
                                                         </td>

                                                         <td style="width: 40%">                                                               
                                                            <telerik:RadGrid ID="rgCurveExportView" AllowFilteringByColumn="False" AllowPaging="False"
                                                                    runat="server" AutoGenerateColumns="False" EnableLinqExpressions="False"
                                                                    OnNeedDataSource="rgCurveExportView_NeedDataSource" Width="100%" CellSpacing="0" GridLines="None"
                                                                    AllowSorting="False">
                                                                    <GroupingSettings CaseSensitive="False" />
                                                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                                                    <PagerStyle Position="Top" AlwaysVisible="True" Font-Size="Small" />
                                                                    <ClientSettings>
                                                                        <Scrolling AllowScroll="true" UseStaticHeaders="true" ScrollHeight="330" />
                                                                    </ClientSettings>
                                                                    <MasterTableView>                                                                    
                                                                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                                                                        </RowIndicatorColumn>
                                                                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                                                                        </ExpandCollapseColumn>
                                                                        <Columns>                                                                                                              
                                                                            <telerik:GridBoundColumn DataField="ReferenceNo" UniqueName="ReferenceNo" AllowFiltering="false"
                                                                                Visible="true" HeaderText="Reference%" ItemStyle-Width="35%" HeaderStyle-Width="35%"
                                                                                ItemStyle-HorizontalAlign="Center" meta:resourcekey="GridBoundColumnResource1">                                                                                     
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="CMYK_C" UniqueName="CMYK_C" AllowFiltering="false"
                                                                                ItemStyle-BackColor="#E0F3FF" Visible="true" HeaderText="C" ItemStyle-Width="16%"
                                                                                HeaderStyle-Width="16%" ItemStyle-HorizontalAlign="Center">                                                                                   
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="CMYK_M" UniqueName="CMYK_M" AllowFiltering="false"
                                                                                ItemStyle-BackColor="#FFEFF9" Visible="true" HeaderText="M" ItemStyle-Width="16%"
                                                                                HeaderStyle-Width="16%" ItemStyle-HorizontalAlign="Center">                                                                                     
                                                                            </telerik:GridBoundColumn>
										                                    <telerik:GridBoundColumn DataField="CMYK_Y" UniqueName="CMYK_Y" AllowFiltering="false"
                                                                                ItemStyle-BackColor="#FFFDEE" Visible="true" HeaderText="Y" ItemStyle-Width="16%"
                                                                                HeaderStyle-Width="16%" ItemStyle-HorizontalAlign="Center">                                                                                      
                                                                            </telerik:GridBoundColumn>
										                                    <telerik:GridBoundColumn DataField="CMYK_K" UniqueName="CMYK_K" AllowFiltering="false"
                                                                                ItemStyle-BackColor="#E7EBEE" Visible="true" HeaderText="K" ItemStyle-Width="17%"
                                                                                HeaderStyle-Width="17%" ItemStyle-HorizontalAlign="Center">                                                                                
                                                                            </telerik:GridBoundColumn>
                                                                        </Columns>                                                                   
                                                                    </MasterTableView>                                
                                                                </telerik:RadGrid>                                                                     
                                                         </td>
                                                     </tr>
                                                    <tr>
                                                        <td colspan="2" align="right" style="padding-right: 15px; padding-top: 10px;">
                                                           &nbsp;<asp:Button ID="BtnExportCurvesExportView" runat="server" CssClass="BUTTON" Text="Export Curves"
                                                                                                                OnClick="BtnExportCurvesExportView_Click" meta:resourcekey="BtnExportCurvesExportViewResource1" />
                                                         </td>
                                                    </tr>
                                                </table>                                                  
                                                                                 
                                            </div>
                                            <div class="clear"></div>                                                  
                                            </div>                                           
                                         <div class="tab_bottom">&nbsp;</div>
                                    </section>   
                                    <section id="tab2" class="contantbox tabcontant hideclass" >
                                        <div class="tab_top">&nbsp;</div>
                                         <div class="tab_middle">
                                            <div class="combopart">
                                               <%--  <asp:UpdatePanel ID="UpdPanDeltaView" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>--%>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%"> 
                                                     <tr>
                                                        <td colspan="2">
                                                          <asp:RadioButtonList ID="rbtnDetlaReflist" runat="server" AutoPostBack="True" 
                                                                onselectedindexchanged="rbtnDetlaReflist_SelectedIndexChanged" 
                                                                RepeatDirection="Horizontal">
                                                            <asp:ListItem Text="&#916;Ref" Value="0" Selected="True" meta:resourcekey="locRefResource1"></asp:ListItem>
                                                            <asp:ListItem Text="&#916;Previous Run" Value="1" meta:resourcekey="locPreRunResource1"></asp:ListItem>
                                                          </asp:RadioButtonList>                                                           
                                                        </td>
                                                     </tr>
                                                     <tr style="border: solid 1px;">
                                                        <td style="width: 60%">

                                                            <asp:Chart ID="CHRT_ExportCurveDeltaRef" runat="server" Width="410px" Height="350px"
                                                                ImageType="Jpeg" ImageUrl="~/UploadFiles/temp/CHRT_ExportCurveDeltaRef" BackGradientType="Center"
                                                                Palette="SemiTransparent" BackGradientEndColor="White" BorderLineColor="#808080" BackColor="#EDEDED">
                                                                <Series>
                                                                    <asp:Series Name="Series1" IsVisibleInLegend="false" Color="Cyan" BorderWidth="2" ChartType="Spline"
                                                                        MarkerBorderColor="64, 64, 64" MarkerStyle="Circle" MarkerSize="2">
                                                                    </asp:Series>
                                                                    <asp:Series Name="Series2" IsVisibleInLegend="false" Color="Magenta" BorderWidth="2"
                                                                        ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle" MarkerSize="2">
                                                                    </asp:Series>
                                                                    <asp:Series Name="Series3" IsVisibleInLegend="false" Color="Yellow" BorderWidth="2" ChartType="Spline"
                                                                        MarkerBorderColor="64, 64, 64" MarkerStyle="Circle" MarkerSize="2">
                                                                    </asp:Series>
                                                                    <asp:Series Name="Series4" IsVisibleInLegend="false" Color="Black" BorderWidth="2" ChartType="Spline"
                                                                        MarkerBorderColor="64, 64, 64" MarkerStyle="Circle" MarkerSize="2">
                                                                    </asp:Series>
                                                                </Series>
                                                              
                                                                <ChartAreas>
                                                                    <asp:ChartArea Name="Default" BorderColor="26, 59, 105" BackSecondaryColor="White"
                                                                        BackColor="Gray" BackGradientStyle="TopBottom" BorderDashStyle="Solid" ShadowOffset="1">
                                                                        <Position Height="99" Width="94" X="0" Y="2" />
                                                                        <AxisY IsLabelAutoFit="False" Title="" Interval="Auto" Minimum="-2" Maximum="2">
                                                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                                                        </AxisY>
                                                                        <AxisX IsLabelAutoFit="False" IsMarginVisible="False" Interval="25" Minimum="0" Maximum="100"
                                                                            Title="">
                                                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                                                            <LabelStyle Angle="0" />
                                                                        </AxisX>
                                                                    </asp:ChartArea>
                                                                </ChartAreas>
                                                            </asp:Chart>
                                                      
                                                         </td>
                                                         <td style="width: 40%">                                            
                                                            <telerik:RadGrid ID="rgCurveDeltaView" AllowFilteringByColumn="False" AllowPaging="False"
                                                        runat="server" AutoGenerateColumns="False" EnableLinqExpressions="False"
                                                        OnNeedDataSource="rgCurveDeltaView_NeedDataSource" Width="100%" CellSpacing="0" GridLines="None"
                                                        AllowSorting="False">
                                                        <GroupingSettings CaseSensitive="False" />
                                                        <HeaderStyle Font-Bold="True" />
                                                        <PagerStyle Position="Top" AlwaysVisible="True" Font-Size="Small" />
                                                        <ClientSettings>
                                                            <Scrolling AllowScroll="true" UseStaticHeaders="true" ScrollHeight="330" />
                                                        </ClientSettings>
                                                        <MasterTableView>                                                                    
                                                            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                                                            </RowIndicatorColumn>
                                                            <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                                                            </ExpandCollapseColumn>
                                                            <Columns>                                                                                                              
                                                                <telerik:GridBoundColumn DataField="ReferenceNo" UniqueName="ReferenceNo" AllowFiltering="false"
                                                                    Visible="true" HeaderText="Reference%" ItemStyle-Width="35%" HeaderStyle-Width="35%"
                                                                    ItemStyle-HorizontalAlign="Center">                                                                                     
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="CMYK_C" UniqueName="CMYK_C" AllowFiltering="false"
                                                                    ItemStyle-BackColor="#E0F3FF" Visible="true" HeaderText="C" ItemStyle-Width="16%"
                                                                    HeaderStyle-Width="16%" ItemStyle-HorizontalAlign="Center">                                                                                   
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="CMYK_M" UniqueName="CMYK_M" AllowFiltering="false"
                                                                    ItemStyle-BackColor="#FFEFF9" Visible="true" HeaderText="M" ItemStyle-Width="16%"
                                                                    HeaderStyle-Width="16%" ItemStyle-HorizontalAlign="Center">                                                                                     
                                                                </telerik:GridBoundColumn>
										                        <telerik:GridBoundColumn DataField="CMYK_Y" UniqueName="CMYK_Y" AllowFiltering="false"
                                                                    ItemStyle-BackColor="#FFFDEE" Visible="true" HeaderText="Y" ItemStyle-Width="16%"
                                                                    HeaderStyle-Width="16%" ItemStyle-HorizontalAlign="Center">                                                                                      
                                                                </telerik:GridBoundColumn>
										                        <telerik:GridBoundColumn DataField="CMYK_K" UniqueName="CMYK_K" AllowFiltering="false"
                                                                    ItemStyle-BackColor="#E7EBEE" Visible="true" HeaderText="K" ItemStyle-Width="17%"
                                                                    HeaderStyle-Width="17%" ItemStyle-HorizontalAlign="Center">                                                                                
                                                                </telerik:GridBoundColumn>
                                                            </Columns>                                                 
                                                        </MasterTableView>                               
                                                    </telerik:RadGrid>
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                        <td colspan="2" align="right" style="padding-right: 15px; padding-top: 10px;">
                                                           
                                                            <asp:Button ID="BtnExportCurvesDeltaView" runat="server" CssClass="BUTTON" Text="Export Delta"
                                                                OnClick="BtnExportCurvesDeltaView_Click" meta:resourcekey="BtnExportCurvesDeltaViewResource1" />
                                                        </td>
                                                    </tr>
                                                  </table>
                                                <%--   </ContentTemplate>
                                                    <Triggers>                                                                                              
                                                        <asp:PostBackTrigger ControlID="BtnExportCurvesDeltaView" />
                                                    </Triggers>
                                                </asp:UpdatePanel>--%>
                                            </div>
                                            <div class="clear"></div>                                                  
                                            </div>                                          
                                            <div class="tab_bottom">&nbsp;</div>
                                    </section>                                                                         
                                </section>
                                 
                            </td>
                        </tr>
                        </table>
                    </div>  
                    <%-- </ContentTemplate>
                        <Triggers>                                                                                              
                            <asp:PostBackTrigger ControlID="BtnExportCurvesExportView" />
                            <asp:PostBackTrigger ControlID="BtnExportCurvesDeltaView" /> 
                    </Triggers>
                </asp:UpdatePanel>--%>
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