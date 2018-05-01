<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="Export.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.Export" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>

<link href="Content/kendo/2018.1.221/kendo.common.min.css" rel="stylesheet" />
<link href="Content/kendo/2018.1.221/kendo.default.min.css" rel="stylesheet" />

<script src="Scripts/kendo/2018.1.221/kendo.all.min.js"></script>
<script src="JS/Export.js" type="text/javascript"></script>
<style>
    .k-grid td {
        font-size:12px;
        padding: 0.2em .4em !important;
        text-align: center;
    }    
    
</style>
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
        <h1 id="header" runat="server">
            <asp:Localize ID="locDownload" runat="server"
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
            <tr id="trDLPDownload" runat="server">
                <td>
                    <div style="display: block;" class="contantbox tabcontant">
                        <div class="section_top">
                            &nbsp;
                        </div>
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
                                            <div id="Div_DLP" runat="server">
                                                <tr>
                                                    <td>
                                                        <span>
                                                            <asp:Localize runat="server"
                                                                Text="Download DLP and Implement in Workflow :"
                                                                ID="locDownloadDLPAndImplementWorkflow"
                                                                meta:resourcekey="locDownloadDLPAndImplementWorkflowResource1"></asp:Localize></span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="lbtnDownloadDLP" runat="server" CssClass="gray_bt"
                                                            OnClick="lbtnDownloadDLP_Click">
                                                            <span>
                                                                <asp:Localize runat="server" Text="Download DLP" ID="locDownloadDLP"
                                                                    meta:resourcekey="locDownloadDLPResource1"></asp:Localize>
                                                            </span>
                                                        </asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </div>
                                            <div id="Div_Chart" runat="server">
                                                <tr>
                                                    <td>
                                                        <span>
                                                            <asp:Localize Text="Download Chart and Print through DLP Workflow :"
                                                                runat="server" ID="locDownloadChartAndPrintThroughWorkflow"
                                                                meta:resourcekey="locDownloadChartAndPrintThroughWorkflowResource1"></asp:Localize></span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="lbtnDownloadChart" runat="server" CssClass="gray_bt"
                                                            OnClick="lbtnDownloadChart_Click">
                                                            <span>
                                                                <asp:Localize runat="server" Text="Download Chart" ID="locDownloadChart"
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
                            &nbsp;
                        </div>
                    </div>
                </td>
            </tr>
            <tr id="trExportCMYKLibrary" runat="server" visible="false">
                <td>
                    <div style="display: block;" class="contantbox tabcontant">
                        <div class="section_top">
                            &nbsp;
                        </div>
                        <div class="section_middle">
                            <div class="combopart">
                                <div class="header">
                                    <div class="content">
                                        <div class="text" style="float: left">
                                            <asp:Localize runat="server" Text="Download CMYK Library"
                                                ID="locDownloadCMYKLibraryResource" meta:resourcekey="locDownloadCMYKLibraryResource"></asp:Localize>
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
                                                    <asp:LinkButton ID="lbtnExportCMYK" runat="server" CssClass="gray_bt" OnClick="lbtnExportCMYK_Click">
                                                        <span>
                                                            <asp:Localize runat="server" Text="Export CMYK"
                                                                ID="locExportCMYK"
                                                                meta:resourcekey="locExportCMYKResource"></asp:Localize>
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
                            &nbsp;
                        </div>
                    </div>
                </td>
            </tr>
            <tr id="trSourceProfDownload" runat="server" visible="false">
                <td>
                    <div style="display: block;" class="contantbox tabcontant">
                        <div class="section_top">
                            &nbsp;
                        </div>
                        <div class="section_middle">
                            <div class="combopart">
                                <div id="div1" class="header collapsibleContainerTitle">
                                    <div class="content">
                                        <div class="text" style="float: left">
                                            <asp:Localize Text="Download Source Profile" runat="server"
                                                ID="Localize1" meta:resourcekey="locDownloadSourceProfResource1"></asp:Localize>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                </div>
                                <div id="div3" style="border-style: None; padding: 5px">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID="lbtnDownloadSourceProf" runat="server" CssClass="gray_bt" OnClick="lbtnDownloadSourceProf_Click">
                                                        <span>
                                                            <asp:Localize runat="server" Text="Download Source Profile"
                                                                ID="Localize2"
                                                                meta:resourcekey="locDownloadSourceProfICCResource1"></asp:Localize>
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
                            &nbsp;
                        </div>
                    </div>
                </td>
            </tr>
            <tr id="trAdvancedDownload" runat="server">
                <td>
                    <div style="display: block;" class="contantbox tabcontant">
                        <div class="section_top">
                            &nbsp;
                        </div>
                        <div class="section_middle">
                            <div class="combopart">
                                <div id="divAdvancedDownloadsHeader" class="header collapsibleContainerTitle">
                                    <div class="content">
                                        <div class="text collapsibleContainerTitle" style="float: left">
                                            <div id="imgHeader" class="expanded">&nbsp;</div>
                                            <asp:Localize Text="Advanced Downloads" runat="server"
                                                ID="locAdvancedDownloads" meta:resourcekey="locAdvancedDownloadsResource1"></asp:Localize>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                </div>
                                <div id="divAdvancedDownloadsContian" style="border-style: None; padding: 5px">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID="lbtnDownloadDestinationICC" runat="server" CssClass="gray_bt"
                                                        OnClick="lbtnDownloadDestinationICC_Click">
                                                        <span>
                                                            <asp:Localize runat="server" Text="Download Destination ICC"
                                                                ID="locDownloadDestinationICC"
                                                                meta:resourcekey="locDownloadDestinationICCResource1"></asp:Localize>
                                                        </span>
                                                    </asp:LinkButton>
                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                     <asp:LinkButton ID="lbtnNamedColorICC" runat="server" CssClass="gray_bt" Visible="false" OnClick="lbtnNamedColorICC_Click">
                                                         <span>
                                                             <asp:Localize runat="server" Text="Download Named Color Profile ICC"
                                                                 ID="Localize4"
                                                                 meta:resourcekey="locNamedColorICCResource1"></asp:Localize>
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
                            &nbsp;
                        </div>
                    </div>

                </td>
            </tr>
            <tr id="trExportViewForSync1D" runat="server" visible="false">
                <td>
                    <asp:HiddenField ID="HD_CurveType" runat="server" Value="1" ClientIDMode="Static"></asp:HiddenField>
                    <div class="demo-section k-content">
                        <div id="tabstrip">
                            <ul>
                                <li class="k-state-active">
                                    <asp:Localize Text="Export View" ID="Localize5"
                                        runat="server" meta:resourcekey="locExportViewResource1"></asp:Localize>
                                </li>
                                <li>
                                    <asp:Localize Text="ΔView" ID="Localize6"
                                        runat="server" meta:resourcekey="locDeltaViewResource1"></asp:Localize>
                                </li>
                            </ul>
                            <div>
                                <span>&nbsp;</span>
                                <div>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td colspan="2" style="padding: 5px">
                                                <asp:Localize ID="Localize3" runat="server" Text="Curve Type :" meta:resourcekey="locCurveTypeResource1"></asp:Localize>
                                                <asp:DropDownList ID="drpCurveType" ClientIDMode="Static" runat="server" Width="150px">
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
                                            </td>
                                        </tr>
                                        <tr style="border: solid 1px; vertical-align: top;">
                                            <td style="width: 60%">

                                                <div id="Chart_Curve"></div>
                                            </td>

                                            <td style="width: 40%">
                                                <div class="scrollableTable" id="tblExport" style="width: 100%"></div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="right" style="padding-right: 15px; padding-top: 10px;">&nbsp;
                                                <asp:Button ID="BtnExportCurvesExportView" runat="server" CssClass="BUTTON" Text="Export Curves" ClientIDMode="Static"
                                                    meta:resourcekey="BtnExportCurvesExportViewResource1" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div>
                                <span>&nbsp;</span>
                                <div>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td colspan="2">
                                                <asp:RadioButtonList ID="rbtnDetlaReflistClient" runat="server" AutoPostBack="True"
                                                    OnSelectedIndexChanged="rbtnDetlaReflistClient_SelectedIndexChanged"
                                                    RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="&#916;Ref" Value="0" Selected="True" meta:resourcekey="locRefResource1"></asp:ListItem>
                                                    <asp:ListItem Text="&#916;Previous Run" Value="1" meta:resourcekey="locPreRunResource1"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr style="border: solid 1px; vertical-align: top;">
                                            <td style="width: 60%">
                                                <div id="Chart_Delta"></div>
                                            </td>
                                            <td style="width: 40%">
                                                <%--<table class="scrollableTable" id="tblDeltaView" style="width: 100%">
                                                    <thead>
                                                        <tr>
                                                            <th>Reference%</th>
                                                            <th>C</th>
                                                            <th>M</th>
                                                            <th>Y</th>
                                                            <th>K</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody data-template="export-summary" data-bind="source: CurveDeltaRefView"></tbody>
                                                    
                                                </table>--%>
                                    <div class="scrollableTable" id="tblDeltaView" style="width: 100%"></div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="right" style="padding-right: 15px; padding-top: 10px;">

                                                <asp:Button ID="BtnExportCurvesDeltaView" runat="server" CssClass="BUTTON" Text="Export Delta"
                                                    OnClick="BtnExportCurvesDeltaView_Click" meta:resourcekey="BtnExportCurvesDeltaViewResource1" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>

                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</div>
<div class="contant_bottom">
    &nbsp;
</div>
<div class="clear">
</div>
<asp:HiddenField ID="hdnExportCurveSelectedTabIndex" runat="server" Value="0" />
<asp:HiddenField runat="server" ID="hdnBaseServiceUrl" ClientIDMode="Static" />
<asp:HiddenField runat="server" ID="hdfCurrentCulture" ClientIDMode="Static" />
<asp:HiddenField runat="server" ID="hdnOptimizationRunID" ClientIDMode="Static" />
