<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Analyze.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.Analyze" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="uc" %>
<%@ Register Src="~/UserControl/PopupControl/CreateDLP.ascx" TagName="CreateDLP" TagPrefix="uc" %>

<script type="text/javascript">
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_initializeRequest(InitializeRequest);

    function InitializeRequest(sender, args) {
        var updateProgress = $get('<%= UpdateProgressClientID %>');
        var postBackElement = args.get_postBackElement();
        if (postBackElement.id == '<%= lbtnDownloadAndSaveDLP.ClientID %>') {
            if ($get('<%= lbtnDownloadAndSaveDLP.ClientID %>').text == "Next") {
                updateProgress.control._associatedUpdatePanelId = null;
                $get('<%= updateProgress.ClientID %>').control._associatedUpdatePanelId = 'dummyId';
            }
            else {
                updateProgress.control._associatedUpdatePanelId = 'dummyId';
            }
        }
        else {
            updateProgress.control._associatedUpdatePanelId = null;
        }
    }

</script>
<script type="text/javascript" language="javascript">
    function ConfirmMeasurement() {
        if (confirm('<%= AddMeasurementDeleteMeasurementConfirmation %>'))
            return true;
        else
            return false;
    }
</script>
<script type="text/javascript" language="javascript">
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
</script>
<script type="text/javascript" language="javascript">

    //According to new or old Project open rad window
    function ChooseRadWindow() {
        if ("<%= IsNewDlp %>" == "True") {
            return true;
        }
        else {
            if ("<%= IsDlpCreated %>" == "True") {
                return true;
            }
            else {
                var oWnd = $find("<%= ConfirmSaveWindow.ClientID %>");
                oWnd.show();
                return false;
            }
        }
    }
</script>
 <script src="JS/ChartTooltip.js" type="text/javascript"></script>

<div class="contant_top">
    &nbsp;</div>
<div class="contant_middle">
    <asp:UpdatePanel ID="upCreateDLP" runat="server">
        <ContentTemplate>
            <div class="head">
                <h1 id="header" runat="server"><asp:Localize runat="server" Text="Analyze" 
                        ID="btnAnalyze" meta:resourcekey="btnAnalyzeResource1"></asp:Localize></h1>
                <div class="Information_divAnalyze">
                    <asp:LinkButton ID="lbtnDownloadAndSaveDLP" runat="server" CssClass="button" OnClick="lbtnDownloadAndSaveDLP_Click"
                        OnClientClick="return ChooseRadWindow();" 
                        meta:resourcekey="lbtnDownloadAndSaveDLPResource1">
                        <span>
                            <asp:Image ID="Image2" runat="server" ImageUrl="../Images/export.png" 
                        meta:resourcekey="Image2Resource1" />
                        <asp:Localize runat="server" Text="Create DLP" ID="locCreateDLP" 
                        meta:resourcekey="locCreateDLPResource1"></asp:Localize>
                        </span> &nbsp;
                    </asp:LinkButton>
                </div>
            </div>
            <div class="listDevice" style="margin-left: 10px;">
                <uc:Statusbar ID="fujiStatusbar" runat="server" Visible="False" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
     <asp:UpdatePanel ID="UpdatePanel2" runat="server">
         <ContentTemplate>
            <table cellpadding="0" cellspacing="10" border="0" width="97%" style="margin-left: 10px;">
                <tr>
                    <td align="center">
                        <asp:RadioButtonList ID="Rbl_DT_E" runat="server" RepeatDirection="Horizontal" 
                            AutoPostBack="true" onselectedindexchanged="Rbl_DT_E_SelectedIndexChanged" CssClass="RadioList">
                            <asp:ListItem Text="ΔE2000" Value="E2000" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="ΔE" Value="E"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr id="trListHeader" runat="server">
                    <td runat="server">
                    
                            <div style="display: block;" class="contantbox tabcontant">
                            <div class="Analyze_head">
                                <div class="text" style="font-weight: bold; font-size: 14px; float: left">
                                    <asp:Localize runat="server" Text="Analyze Measurement :" ID="locAnaluzeMeasurement" meta:resourcekey="locAnaluzeMeasurementResource1"></asp:Localize>
                                    <asp:Label ID="lblMeasurementName" Style="font-size: 12px !important;" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="section_middle">
                                <center>
                                    <script type="text/javascript" language="javascript">
                                        function ClickOnPoint(MeasurementID) {
                                            var hdCheckBoxChange = document.getElementById("<%= hdCheckBoxChange.ClientID  %>");
                                            hdCheckBoxChange.value = "0";
                                            var obj1 = document.getElementById("<%= hdMeasurement.ClientID %>");
                                            obj1.value = MeasurementID;
                                            document.getElementById("<%= hdChartButton.ClientID %>").click();
                                        }
                                        function ClickOnCheckBox(MeasurementID, checkedValue, checkCount) {
                                            if (checkCount <= 1 && checkedValue == "False") {
                                                alert('<%= GetResourceString("selectMeasurementMsg") %>');
                                                return false;
                                            }
                                            else {
                                                var hdCheckBoxChange = document.getElementById("<%= hdCheckBoxChange.ClientID  %>");
                                                hdCheckBoxChange.value = "1";
                                                var obj1 = document.getElementById("<%= hdMeasurement.ClientID  %>");
                                                obj1.value = MeasurementID;
                                                var chekcBox = document.getElementById("<%= hdChartCheckBox.ClientID %>");
                                                chekcBox.checked = checkedValue == "True" ? true : false;
                                                document.getElementById("<%= hdChartButton.ClientID %>").click();
                                            }
                                        } 
                                    </script>
                                    <asp:Chart ID="chartAnalyze" runat="server" Width="680px" Height="160px" BackColor="240, 240, 240"
                                        BackGradientStyle="HorizontalCenter" BackSecondaryColor="Silver" 
                                        Palette="SemiTransparent" BorderlineColor="176, 176, 176" 
                                        BorderlineDashStyle="Solid">                                         
                                        <Series>
                                            <asp:Series Name="Series0" ChartType="Spline" Color="Transparent" ChartArea="ChartArea1"
                                                BorderWidth="2">
                                            </asp:Series>
                                            <asp:Series Name="Series1" ChartType="Spline" Color="Green" ChartArea="ChartArea1"
                                                BorderWidth="2">
                                            </asp:Series>
                                            <asp:Series Name="Series2" ChartType="Spline" Color="Red" ChartArea="ChartArea1"
                                                BorderWidth="2">
                                            </asp:Series>
                                            <asp:Series Name="Series3" ChartType="Spline" Color="Blue" ChartArea="ChartArea1"
                                                BorderWidth="2">
                                            </asp:Series>
                                            <asp:Series Name="Series4" ChartType="Spline" Color="Blue" ChartArea="ChartArea1"
                                                BorderWidth="2">
                                            </asp:Series>
                                            <asp:Series Name="Series5" ChartType="Spline" Color="Blue" ChartArea="ChartArea1"
                                                BorderWidth="2">
                                            </asp:Series>
                                            <asp:Series Name="Series6" ChartType="Spline" Color="Blue" ChartArea="ChartArea1"
                                                BorderWidth="2">
                                            </asp:Series>
                                            <asp:Series Name="Series7" ChartType="Spline" Color="Blue" ChartArea="ChartArea1"
                                                BorderWidth="2">
                                            </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea1" BackColor="Transparent" BackSecondaryColor="White"
                                                ShadowColor="Transparent">
                                                <Position Height="99" Width="99" Auto="False" />
                                                <AxisY  TitleFont="Verdana, 8pt" IsLabelAutoFit="False">
                                                    <MajorGrid LineColor="216, 216, 216" />
                                                </AxisY>
                                                <AxisX Interval="1" TitleFont="Verdana, 5pt" IsLabelAutoFit="False">
                                                    <MajorGrid LineColor="216, 216, 216" />
                                                </AxisX>
                                            </asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>
                                    <asp:HiddenField ID="hdMeasurement" runat="server" Value="0" />
                                    <asp:Button ID="hdChartButton" runat="server" Text="Button" OnClick="hdChartButton_Click"
                                        Style="display: none;" />
                                    <asp:CheckBox ID="hdChartCheckBox" runat="server" Style="display: none;" />
                                    <asp:HiddenField ID="hdCheckBoxChange" runat="server" Value="0" />
                                </center>
                            </div>
                        </div>
                        
                    </td>
                </tr>
            </table>
        </ContentTemplate>    
        <Triggers>                      
            <asp:PostBackTrigger ControlID="Rbl_DT_E" />  
        </Triggers>              
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="upCreateDLP"
        DisplayAfter="50">
        <ProgressTemplate>
            <AjaxToolkit:AlwaysVisibleControlExtender ID="AlwaysVisibleControl" HorizontalSide="Center"
                VerticalSide="Middle" TargetControlID="updateProgress" HorizontalOffset="325"
                VerticalOffset="210" runat="server" Enabled="True">
            </AjaxToolkit:AlwaysVisibleControlExtender>
            <div style="background: #CCC !important;">
                <uc:CreateDLP runat="server" ID="ucCreateDLP" />
            </div>
            <div class="UpdateProgressBackgroundLessOptimize" style="position: fixed; left: 0px;
                top: 0px; z-index: 1000; width: 100%; height: 100%;" unselectable="on">
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="listDevice" style="margin-left: 10px;">
        <table cellpadding="0" cellspacing="10" border="0" width="97%">
            <tr>
                <td>
                    <section id="tabpart" style="margin-left: 0;">
        <section class="inner_tab">
            <ul class="tabs">
                <li><span><a href="#tab1"><asp:Localize Text="Summary" ID="locSummary" 
                        runat="server" meta:resourcekey="locSummaryResource1"></asp:Localize></a></span></li>                                       
                <li><span><a href="#tab2"><asp:Localize Text="Ink Paper" ID="locInkPaper" 
                        runat="server" meta:resourcekey="locInkPaperResource1"></asp:Localize></a></span></li>                                
                <li><span><a href="#tab3"><asp:Localize Text="Gray" ID="locGray" runat="server" 
                        meta:resourcekey="locGrayResource1"></asp:Localize></a></span></li>                                
                <li><span><a href="#tab4"><asp:Localize Text="Patch Details" ID="locPatchDetails" 
                        runat="server" meta:resourcekey="locPatchDetailsResource1"></asp:Localize></a></span></li>  
                <li><span><a href="#tab5"><asp:Localize Text="Report" ID="locReport" runat="server" 
                        meta:resourcekey="locReportResource1"></asp:Localize></a></span></li>                                   
            </ul>
            <div class="clear"></div>
        </section>


  <section id="tab1" class="contantbox tabcontant hideclass" >
        <div class="tab_top">&nbsp;</div>
        <div class="tab_middle">
           <div class="combopart">
        <div class="float_l" style="padding-left: 10px;" > 
           <asp:UpdatePanel ID="UpdatePanel5" runat="server">
              <ContentTemplate>   
                  <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr id="tr1" runat="server" visible="false">
                                        <td style="width:100px;">
                                            <span> <asp:Localize runat="server" Text="Selected Metric:" 
                                                ID="locSelectedMetric" meta:resourcekey="locSelectedMetricResource1" ></asp:Localize> &nbsp; </span>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbSelectedMetric" runat="server" Width="50%" 
                                                CssClass="select" meta:resourcekey="cmbSelectedMetricResource1">
                                                <asp:ListItem Text="Idealliance Digital Chart" 
                                                    Value="Idealliance Digital Chart" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                    <td colspan="2">
                                         <asp:ListView ID="grdMeasurement" runat="server" ItemPlaceholderID="phItemPlaceholder" OnItemDataBound="grdMeasurement_ItemDataBound">
                                            <LayoutTemplate>
                                                <table cellspacing="0" cellpadding="0" border="0" class="product_list_table" style="margin:0" >                                  
                                                    <tbody>
                                                        <asp:PlaceHolder ID="phItemPlaceholder" runat="server"></asp:PlaceHolder>
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>
                                            <EmptyDataTemplate>
                                                <div class="no_data_found"><asp:Localize runat="server" Text="No records available" 
                                                        ID="locNoRecordsAvailable" meta:resourcekey="locNoRecordsAvailableResource1"></asp:Localize></div>
                                            </EmptyDataTemplate>
                                            <ItemTemplate>
                                                <tr class="Gray">   
                                                    <td style="font-size:13px; width:200px;">Measurement Name: </td>                                               
                                                    <td>
                                                        <asp:Label ID="lblMeasurementName" runat="server" Text='<%#Eval("MeasurementName") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="White">   
                                                    <td style="font-size:13px;">Upload Date: </td>                                  
                                                    <td>
                                                        <asp:Label ID="lblMeasurementDate" runat="server" Text='<%# string.Format("{0:d}",Eval("MeasurementDate")) %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="Gray">   
                                                    <td style="font-size:13px;">Average Δ<asp:Label ID="lblAvg_ΔE" runat="server" Text=""></asp:Label></td>                                  
                                                    <td>
                                                        <asp:Label ID="lblRAvg_ΔE" runat="server" Text='<%#Eval("Avg_ΔE") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="White">   
                                                    <td style="font-size:13px;">Maximum Δ<asp:Label ID="lblMax_ΔE" runat="server" Text=""></asp:Label></td>                                  
                                                    <td>
                                                        <asp:Label ID="lblRMax_ΔE" runat="server" Text='<%#Eval("Max_ΔE") %>'></asp:Label>
                                                    </td>
                                                </tr>                                            
                                                <tr class="Gray">   
                                                    <td style="font-size:13px;">10th Percentile Δ<asp:Label ID="lblPercΔE_10" runat="server" Text=""></asp:Label> </td>                                                                                     
                                                    <td>
                                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("PercΔE_10") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="White">   
                                                    <td style="font-size:13px;">90th Percentile Δ<asp:Label ID="lblPercΔE_90" runat="server" Text=""></asp:Label> </td>                                                                                    
                                                    <td>
                                                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("PercΔE_90") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="Gray">   
                                                    <td style="font-size:13px;">95th Percentile Δ<asp:Label ID="lblPercΔE_95" runat="server" Text=""></asp:Label> </td>                                                                                    
                                                    <td>
                                                        <asp:Label ID="Label3" runat="server" Text='<%#Eval("PercΔE_95") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </td>
                                    </tr>
                                </table>
              </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="chartAnalyze" />  
                        <asp:PostBackTrigger ControlID="Rbl_DT_E" />  
                    </Triggers>  
           </asp:UpdatePanel>
        </div>
        <div class="clear"></div>
        </div>  
    </div>    
    <div class="tab_bottom">
        &nbsp;</div>
    </section>
    
   <section id="tab2" class="contantbox tabcontant hideclass">
        <div class="tab_top">&nbsp;</div>
        <div class="tab_middle">
        <div class="combopart">
        <div class="float_l" style="padding-left: 10px;" >
       
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
              <ContentTemplate>    
                  <table border="1" cellpadding="0" cellspacing="0" width="100%" style="background-color:White;">
                    <tr>
                        <td style="width: 50%;" valign="top" class="CHARTTABLE">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%" >
                                <tr>
                                    <td colspan="2" class="green-title">
                                        &nbsp; &Delta;<asp:Label ID="lblE_PryPatches" runat="server" Text=""></asp:Label>
                                        <asp:Localize Text=" Primary Patches" runat="server"
                                            meta:resourcekey="LocalizeResource1" ></asp:Localize>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 40%;" valign="top">
                                        <table cellpadding="0" cellspacing="0" width="100%"  >
                          
                                    <tr>
                                        <td colspan="2" style="height: 40px;" class="SyncBottomBorder">
                                        </td>
                                    </tr>
                                    <asp:Literal ID="LitDeltaEPatches" runat="server" 
                                                meta:resourcekey="LitDeltaEPatchesResource1"></asp:Literal>
                                    </table>
                                    </td>
                                    <td style="width: 60%; " id="TDEPrimary" runat="server">
                                        <asp:Chart ID="ChartSummary_DeltaE" runat="server" Width="210px" Height="362px" 
                                            BackColor="Transparent" meta:resourcekey="ChartSummary_DeltaEResource1">   
                                                                                                                              
                                                            <Series>
                                                                <asp:Series Name="Series0"  Color="Transparent" ChartArea="ChartArea1">                                              
                                                                </asp:Series>
                                                                <asp:Series Name="Series1" BorderWidth="0" BorderColor="Gray" Color="Gray" 
                                                                                            IsVisibleInLegend="False" ChartArea="ChartArea1">
                                                                </asp:Series>    
                                                            </Series>
                                                            <ChartAreas>
                                                                <asp:ChartArea Name="ChartArea1" BackColor="Transparent">
                                                                     <Area3DStyle IsRightAngleAxes="False" WallWidth="0"
                                                                                              IsClustered="True"></Area3DStyle>
                                          
                                                                    <AxisX  Enabled="True"  IsReversed="True" IsLabelAutoFit="False" >
                                                                        <MajorGrid Enabled="False" LineColor="Beige" />
                                                                        <MajorTickMark Enabled="False" />
                                                                        <LabelStyle Font="Verdana, 7pt" />
                                                                    </AxisX>
                                                                     <AxisY Enabled="True" IsLabelAutoFit="False">
                                                                        <MajorGrid LineColor="Beige"></MajorGrid>
                                                                        <LabelStyle Format="###" Font="Verdana, 7pt" />
                                                                    </AxisY>
                                                                </asp:ChartArea>
                                                            </ChartAreas>
                                                        </asp:Chart>
                                    </td>
                                </tr>
                           </table>
                        </td>
                   
                        <td style="width: 50%;" valign="top" class="CHARTTABLE">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%" >                                                
                           
                                <tr>
                                    <td class="green-title">
                                        &nbsp; &#916; <asp:Localize Text="Paper L* a* b*" runat="server" ID="locPaper" 
                                            meta:resourcekey="locPaperResource1"></asp:Localize>
                                    </td>
                                </tr>
                                <tr>
                                  
                                          <td style="width:380px;">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr><td></td></tr>
                                                            <tr>
                                                                <td align="center" style="background:  url(images/BGISOPaperGraph1_Sync4D.jpg) no-repeat center; width: 276px;
                                                                                        height: 180px; padding-left: 34px; padding-bottom: 14px;">
                                                                                
                                                                    <asp:Chart ID="CHRT_Paper1" runat="server" BackImageMode="Scaled" BackImage="~/Images/ISOPaperGraph1_Sync4D.jpg"
                                                                        ImageType="Jpeg"  Width="240px" Height="130px" 
                                                                        meta:resourcekey="CHRT_Paper1Resource1">
                                                                        <Legends>
                                                                            <asp:Legend Name="Default" Enabled="False">
                                                                            </asp:Legend>
                                                                        </Legends>
                                                                        <Series>
                                                                            <asp:Series Name="Green" ChartType="Polar" 
                                                                                MarkerSize="10" MarkerBorderWidth="2" MarkerBorderColor="Green" MarkerStyle="Circle"
                                                                                MarkerColor="Green" ShadowOffset="1" ChartArea="Default" Legend="Default">                                                                                           
                                                                            </asp:Series>
                                                                            <asp:Series Name="Paper" ChartType="Polar" MarkerBorderColor="Black" MarkerColor="White"
                                                                                    MarkerSize="10" MarkerBorderWidth="2"
                                                                                MarkerStyle="Circle" ShadowOffset="1" ChartArea="Default" Legend="Default">                                                                                             
                                                                            </asp:Series>
                                                                        </Series>
                                                                        <BorderSkin PageColor="AliceBlue" />
                                                                        <Titles>
                                                                            <asp:Title Name="Title1">
                                                                            </asp:Title>
                                                                        </Titles>
                                                                        <ChartAreas>
                                                                            <asp:ChartArea BackColor="Transparent" BorderColor="" Name="Default">
                                                                                <AxisY LineColor="120, 0, 0, 0" TitleAlignment="Near" Maximum="10" Minimum="-10"
                                                                                    LineDashStyle="NotSet">
                                                                                    <MajorGrid Enabled="False" />
                                                                                    <MajorTickMark Enabled="False" />
                                                                                    <LabelStyle Enabled="False" />
                                                                                </AxisY>
                                                                                <AxisX IsMarginVisible="False" TitleAlignment="Near" Maximum="20" Minimum="-20" LineDashStyle="NotSet">
                                                                                    <MajorGrid Enabled="False" />
                                                                                    <MajorTickMark Enabled="False" />
                                                                                    <LabelStyle Enabled="False" />
                                                                                </AxisX>
                                                                            </asp:ChartArea>
                                                                        </ChartAreas>
                                                                    </asp:Chart>

                                                                </td>
                                                            </tr>
                                                        
                                                            <tr>
                                                                <td align="center" style="background: url(images/BGISOPaperGraph2_Sync4D.jpg) no-repeat center; width: 104px;
                                                                                        height: 169px; padding-left: 28px; padding-bottom: 12px;" >


                                                                        <asp:Chart ID="CHRT_Paper2" runat="server" BackImageMode="Scaled" BackImage="~/Images/ISOPaperGraph2_Sync4D.jpg"
                                                                        ImageType="Jpeg" Width="69px" Height="130px" 
                                                                            meta:resourcekey="CHRT_Paper2Resource1">
                                                                        <Legends>
                                                                            <asp:Legend Name="Default" Enabled="False">
                                                                            </asp:Legend>
                                                                        </Legends>
                                                                        <Series>
                                                                            <asp:Series Name="Series1" BorderColor="100, 0, 0, 0" ChartArea="Default"
                                                                                IsVisibleInLegend="False" Legend="Default">                                                                                               
                                                                            </asp:Series>
                                                                            <asp:Series Name="Series2" Color="Green" ChartArea="Default" BorderWidth="2" 
                                                                                IsVisibleInLegend="False" Legend="Default">
                                                                            </asp:Series>
                                                                        </Series>
                                                                        <BorderSkin PageColor="AliceBlue" />
                                                                        <Titles>
                                                                            <asp:Title Name="Title1">
                                                                            </asp:Title>
                                                                        </Titles>
                                                                        <ChartAreas>
                                                                            <asp:ChartArea BackColor="Transparent" BorderColor="" Name="Default">
                                                                                <AxisY LineColor="120, 0, 0, 0" TitleAlignment="Near" Maximum="10" Minimum="-10"
                                                                                    LineDashStyle="NotSet">
                                                                                    <MajorGrid Enabled="False" />
                                                                                    <MajorTickMark Enabled="False" />
                                                                                    <LabelStyle Enabled="False" />
                                                                                </AxisY>
                                                                                <AxisX TitleAlignment="Near" LineDashStyle="NotSet">
                                                                                    <MajorGrid Enabled="False" />
                                                                                    <MajorTickMark Enabled="False" />
                                                                                    <LabelStyle Enabled="False" />
                                                                                </AxisX>
                                                                                <AxisX2 Minimum="0" Maximum="1" LineDashStyle="NotSet">
                                                                                    <MajorGrid Enabled="False" />
                                                                                    <MajorTickMark Enabled="False" />
                                                                                    <LabelStyle Enabled="False" />
                                                                                </AxisX2>
                                                                            </asp:ChartArea>
                                                                        </ChartAreas>
                                                                    </asp:Chart>


                                                                    </td>
                                                            </tr>
                                                        </table>
                                                </td>
                                
                                </tr>
                                   
                            </table>
                        </td>
                    </tr>
                  </table>
       
              </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="chartAnalyze" />  
                        <asp:PostBackTrigger ControlID="Rbl_DT_E" />  
                    </Triggers>  
            </asp:UpdatePanel>
        </div>
        <div class="clear"></div>
        </div>                            	
        </div>
        <div class="tab_bottom">&nbsp;</div>
  </section>
    
   <section id="tab3" class="contantbox tabcontant hideclass">
        <div class="tab_top">&nbsp;</div>
        <div class="tab_middle">
        <div class="combopart">
        <div class="float_l" style="padding-left: 10px;" >

         <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate> 

              <table border="1" cellpadding="0" cellspacing="0" width="200px" style="background-color:White;">
                                <tr>
                                    <td style="width: 45%;" valign="top" class="CHARTTABLE">
                                         <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td class="green-title">
                                                    &nbsp; <asp:Localize runat="server" Text="Gamut Plot" ID="locGamutPlot" 
                                                        meta:resourcekey="locGamutPlotResource1"></asp:Localize>
                                                </td> 
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table id="Table1" runat="server" cellpadding="0" cellspacing="0" border="0" width="100%" class="SYNCTABLE">                                                     
                                                        <tr runat="server">
                                                            <td class="green-title" runat="server">
                                                                <asp:Localize ID="locComparison" runat="server" Text="a* - b* Comparison"></asp:Localize>
                                                            </td>
                                                        </tr>
                                                      
                                                        <tr>
                                                            <td align="center" valign="top">
                                                         <%--  BackImageWrapMode="Scaled" BackImage="~/Images/CPSyncABComparison.jpg"
                                                                    ImageType="Jpeg"  Width="280px" Height="250px"--%>
                                                                <asp:Chart ID="CHART_ABComparison" runat="server" 
                                                                     Width="280px" Height="250px" BackGradientType="Center" 
                                                                                    BackGradientEndColor="White" BorderLineColor="#808080"  EnableViewState="false">
                                                                    <Titles>
                                                                        <asp:Title ForeColor="Black" Text="a*">
                                                                            <Position X="15" Y="42" />
                                                                        </asp:Title>
                                                                        <asp:Title ForeColor="Black" Text="b*">
                                                                            <Position X="57" Y="87" />
                                                                        </asp:Title>
                                                                    </Titles>
                                                                    <Legends>
                                                                        <asp:Legend Name="Default" Enabled="False">
                                                                        </asp:Legend>
                                                                    </Legends>
                                                                    <Series>
                                                                         <asp:Series Name="Targets" ChartType="Polar"
                                                                            MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1" ChartArea="Default" 
                                                                             Legend="Default">
                                                                        </asp:Series>
                                                                        <asp:Series Name="Cyan" ChartType="Polar" 
                                                                            MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1" ChartArea="Default" 
                                                                             Legend="Default">
                                                                        </asp:Series>
                                                                        <asp:Series Name="Magenta" ChartType="Polar" 
                                                                            MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1" ChartArea="Default" 
                                                                             Legend="Default">
                                                                        </asp:Series>
                                                                        <asp:Series Name="Yellow" ChartType="Polar" 
                                                                            MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1" ChartArea="Default" 
                                                                             Legend="Default">
                                                                        </asp:Series>
                                                                        <asp:Series Name="Red" ChartType="Polar" 
                                                                            MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1" ChartArea="Default" 
                                                                             Legend="Default">
                                                                        </asp:Series>
                                                                        <asp:Series Name="Green" ChartType="Polar" 
                                                                            MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1" ChartArea="Default" 
                                                                             Legend="Default">
                                                                        </asp:Series>
                                                                        <asp:Series Name="Blue" ChartType="Polar" 
                                                                            MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1" ChartArea="Default" 
                                                                             Legend="Default">
                                                                        </asp:Series>
                                                                        <asp:Series Name="Paper" ChartType="Polar" 
                                                                            MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1" ChartArea="Default" 
                                                                             Legend="Default">
                                                                        </asp:Series>
                                                                    </Series>
                                                                    <BorderSkin PageColor="AliceBlue" />
                                                                   
                                                                    <ChartAreas>
                                                                        <asp:ChartArea  BackSecondaryColor="#FACECF" BackColor="#B3DEC3" BackGradientStyle="LeftRight" BorderColor="26, 59, 105" BorderDashStyle="Solid" Name="Default"  ShadowOffset="1">
                                                                         <Position Height="99" Width="100" X="0" Y="0" />
                                                                            <AxisY LineColor="120, 0, 0, 0"  Maximum="100" Minimum="-100" Interval="20"  IsMarginVisible="False" TitleAlignment="Near" IsLabelAutoFit="False"
                                                                                LineDashStyle="NotSet">
                                                                              <%--  <MajorGrid Enabled="False" />
                                                                                <MajorTickMark Enabled="false" />
                                                                                <LabelStyle Enabled="False" />--%>
                                                                                 <MajorGrid LineColor="Gray"></MajorGrid>
                                                                                    <LabelStyle Font="Verdana, 6pt" />
                                                                            </AxisY>
                                                                            <AxisX LineColor="120, 0, 0, 0" Maximum="100" Minimum="-100" Interval="20" LineDashStyle="NotSet"  IsMarginVisible="False" TitleAlignment="Near" IsLabelAutoFit="False">
                                                                                <%--<MajorGrid Enabled="False" />
                                                                                <MajorTickMark Enabled="false" />
                                                                                <LabelStyle Enabled="False" />--%>
                                                                                 <MajorGrid LineColor="Gray"></MajorGrid>
                                                                                    <LabelStyle Font="Verdana, 6pt" />
                                                                            </AxisX>
                                                                        </asp:ChartArea>
                                                                    </ChartAreas>
                                                                </asp:Chart>

                                                            </td>
                                                        </tr>
                                                        <tr runat="server">
                                                            <td class="green-title" runat="server">
                                                                <asp:Localize runat="server" Text="L* Comparison" ID="locLComparison"></asp:Localize>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server">
                                                            <td align="center" valign="top" runat="server">                                                            
                                                                <asp:Chart ID="CHART_LComparison" runat="server" BackGradientType="Center"
                                                                                        BackGradientEndColor="White" BorderLineColor="Gray" 
                                                                    Width="280px" Height="75px">
                                                                    <Legends>
                                                                        <asp:Legend Name="Default" Enabled="False">
                                                                        </asp:Legend>
                                                                    </Legends>
                                                                    <Series>
                                                                        <asp:Series Name="Series0" ChartType="Polar"
                                                                            MarkerSize="12" MarkerStyle="Circle" MarkerColor="Green" Color="Green" 
                                                                            ShadowOffset="1" ChartArea="Default" Legend="Default">
                                                                        </asp:Series>
                                                                        <asp:Series Name="Series1" ChartType="Polar" 
                                                                            MarkerSize="12" MarkerStyle="Circle" MarkerColor="Green"
                                                                            Color="Green" ShadowOffset="1" ChartArea="Default" Legend="Default">
                                                                        </asp:Series>
                                                                        <asp:Series Name="Series2" ChartType="FastPoint"
                                                                            MarkerSize="12" MarkerStyle="Circle" MarkerColor="Green" Color="Red" 
                                                                            ShadowOffset="1" ChartArea="Default" Legend="Default">
                                                                        </asp:Series>
                                                                        <asp:Series Name="Series3" ChartType="FastPoint" 
                                                                            MarkerSize="12" MarkerStyle="Circle" ShadowOffset="1" ChartArea="Default" 
                                                                            Legend="Default">
                                                                        </asp:Series>
                                                                        <asp:Series Name="Series4" ChartType="FastPoint" 
                                                                            MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1" ChartArea="Default" 
                                                                            Legend="Default">
                                                                        </asp:Series>
                                                                    </Series>
                                                                    <BorderSkin PageColor="AliceBlue" />
                                                                    <Titles>
                                                                        <asp:Title Name="Title1">
                                                                        </asp:Title>
                                                                    </Titles>
                                                                    <ChartAreas>
                                                                        <asp:ChartArea Name="Default" BorderColor="26, 59, 105" BackSecondaryColor="Gray"
                                                                                                BackColor="White" BackGradientStyle="TopBottom" BorderDashStyle="Solid" ShadowOffset="1">
                                                                              <Position Height="99" Width="100" Auto="False" />
                                                                         
                                                                              <AxisY  IsMarginVisible="False" TitleAlignment="Near" IsLabelAutoFit="False" Interval="15"
                                                                                    Minimum="-15" Maximum="15">
                                                                                    <MajorGrid LineColor="Gray"></MajorGrid>
                                                                                    <LabelStyle Font="Verdana, 6pt" />
                                                                                </AxisY>
                                                                                <AxisX IsLabelAutoFit="False" IsMarginVisible="False" Interval="20" 
                                                                                  Minimum="-100" Maximum="100">
                                                                                    <MajorGrid LineColor="Gray"></MajorGrid>
                                                                                    <LabelStyle Font="Verdana, 6pt" />
                                                                                </AxisX>
                                                                        </asp:ChartArea>
                                                                    </ChartAreas>
                                                                </asp:Chart>

                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                                                       
                                    <td style="width: 55%;" valign="top">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td>
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                                                        <tr>
                                                            <td class="green-title">
                                                                &nbsp; <asp:Localize runat="server" Text="Gray Balance L* a* b*" 
                                                                    ID="locGrayBalanceLAB" meta:resourcekey="locGrayBalanceLABResource1"></asp:Localize>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                
                                                                                <asp:Chart ID="Chart_L" runat="server" Width="365px" Height="125px" 
                                                                                        BackGradientType="Center"
                                                                                        BackGradientEndColor="White" BorderLineColor="Gray" 
                                                                                    meta:resourcekey="Chart_LResource1">
                                                                                        <Titles>
                                                                                            <asp:Title Text="L*" Name="Title1">
                                                                                                <Position X="94" Y="10" Auto="False" />
                                                                                            </asp:Title>
                                                                                        </Titles>
                                                                                        <Legends>
                                                                                            <asp:Legend Name="Default" Enabled="False">
                                                                                            </asp:Legend>
                                                                                        </Legends>
                                                                               
                                                                                        <Series>
                                                                                            <asp:Series Name="Series1" LabelFormat="0" IsVisibleInLegend="False"
                                                                                                Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                                                                MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255" 
                                                                                                ChartArea="Default" Legend="Default">
                                                                                            </asp:Series>
                                                                                            <asp:Series MarkerSize="12" BackSecondaryColor="162, 164, 255" IsVisibleInLegend="False"
                                                                                                Color="Green" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                                                                                BorderWidth="2" Name="Series2" ChartArea="Default" Legend="Default">
                                                                                            </asp:Series>
                                                                                        </Series>
                                                                                        <ChartAreas>
                                                                                            <asp:ChartArea Name="Default" BorderColor="26, 59, 105" BackSecondaryColor="Gray"
                                                                                                BackColor="White" BackGradientStyle="TopBottom" BorderDashStyle="Solid" ShadowOffset="1">
                                                                                                <Position Height="99" Width="100" Auto="False" />
                                                                                                <AxisY IsLabelAutoFit="False" TitleFont="Verdana, 8pt, style=Bold" Interval="25"
                                                                                                    Minimum="0" Maximum="100">
                                                                                                    <MajorGrid LineColor="Gray"></MajorGrid>
                                                                                                    <LabelStyle Font="Verdana, 6pt" />
                                                                                                </AxisY>
                                                                                                <AxisX IsLabelAutoFit="False" IsMarginVisible="False" Interval="25" Minimum="0" 
                                                                                                    Maximum="100">
                                                                                                    <MajorGrid LineColor="Gray"></MajorGrid>
                                                                                                    <LabelStyle Font="Verdana, 6pt" ForeColor="Transparent" />
                                                                                                </AxisX>
                                                                                            </asp:ChartArea>
                                                                                        </ChartAreas>
                                                                                    </asp:Chart>

                                                                         </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>                                                                        

                                                                                 <asp:Chart ID="Chart_a" runat="server" Width="365px" Height="125px" BackGradientType="Center" 
                                                                                    BackGradientEndColor="White" BorderLineColor="Gray" 
                                                                                     meta:resourcekey="Chart_aResource1">
                                                                                    <Legends>
                                                                                        <asp:Legend Name="Default" Enabled="False">
                                                                                        </asp:Legend>
                                                                                    </Legends>
                                                                                    <Titles>
                                                                                        <asp:Title Text="a*" Name="Title1">
                                                                                            <Position X="94" Y="10" Auto="False" />
                                                                                        </asp:Title>
                                                                                    </Titles>
                                                                                    <Series>
                                                                                        <asp:Series Name="Series1" LabelFormat="0" IsVisibleInLegend="False"
                                                                                            Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                                                            MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255" 
                                                                                            ChartArea="Default" Legend="Default">
                                                                                        </asp:Series>
                                                                                        <asp:Series MarkerSize="12" BackSecondaryColor="162, 164, 255" IsVisibleInLegend="False"
                                                                                            Color="Green" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                                                                            BorderWidth="2" Name="Series2" ChartArea="Default" Legend="Default">
                                                                                        </asp:Series>
                                                                                    </Series>
                                                                                    <ChartAreas>
                                                                                        <asp:ChartArea Name="Default" BorderColor="26, 59, 105" BackSecondaryColor="51, 153, 102"
                                                                                            BackColor="255, 102, 204" BackGradientStyle="TopBottom" 
                                                                                            BorderDashStyle="Solid" ShadowOffset="1">
                                                                                            <Position Height="99" Width="100" Auto="False" />
                                                                                            <AxisY IsLabelAutoFit="False" TitleFont="Verdana, 8pt, style=Bold" Interval="2"
                                                                                                Minimum="-6" Maximum="6">
                                                                                                <MajorGrid LineColor="Gray"></MajorGrid>
                                                                                                <LabelStyle Font="Verdana, 6pt" />
                                                                                            </AxisY>
                                                                                            <AxisX IsLabelAutoFit="False" IsMarginVisible="False" Interval="25" Minimum="0" 
                                                                                                Maximum="100">
                                                                                                <MajorGrid LineColor="Gray"></MajorGrid>
                                                                                                <LabelStyle Font="Verdana, 6pt" ForeColor="Transparent" />
                                                                                            </AxisX>
                                                                                        </asp:ChartArea>
                                                                                    </ChartAreas>
                                                                                </asp:Chart>
                                                                                
                                                                         </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                        
                                                                                 <asp:Chart ID="Chart_b" runat="server" Width="365px" Height="125px" BackGradientType="Center" 
                                                                                    BackGradientEndColor="White" BorderLineColor="Gray" 
                                                                                     meta:resourcekey="Chart_bResource1">
                                                                                    <Legends>
                                                                                        <asp:Legend Name="Default" Enabled="False">
                                                                                        </asp:Legend>
                                                                                    </Legends>
                                                                                    <Titles>
                                                                                        <asp:Title Text="b*" Name="Title1">
                                                                                            <Position X="94" Y="10" Auto="False" />
                                                                                        </asp:Title>
                                                                                    </Titles>
                                                                                   
                                                                                    <Series>
                                                                                        <asp:Series Name="Series1" LabelFormat="0" IsVisibleInLegend="False"
                                                                                            Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                                                            MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255" 
                                                                                            ChartArea="Default" Legend="Default">
                                                                                        </asp:Series>
                                                                                        <asp:Series MarkerSize="12" BackSecondaryColor="162, 164, 255" IsVisibleInLegend="False"
                                                                                            Color="Green" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                                                                            BorderWidth="2" Name="Series2" ChartArea="Default" Legend="Default">
                                                                                        </asp:Series>
                                                                                    </Series>
                                                                                    <ChartAreas>
                                                                                        <asp:ChartArea Name="Default" BorderColor="26, 59, 105" BackSecondaryColor="51, 51, 204"
                                                                                            BackColor="255, 255, 153" BackGradientStyle="TopBottom" 
                                                                                            BorderDashStyle="Solid" ShadowOffset="1">
                                                                                            <Position Height="99" Width="100" Auto="False" />
                                                                                            <AxisY IsLabelAutoFit="False" TitleFont="Verdana, 8pt, style=Bold" Interval="2"
                                                                                                Minimum="-6" Maximum="6">
                                                                                                <MajorGrid LineColor="Gray"></MajorGrid>
                                                                                                <LabelStyle Font="Verdana, 6pt" />
                                                                                            </AxisY>
                                                                                            <AxisX IsLabelAutoFit="False" IsMarginVisible="False" Interval="25" Minimum="0" 
                                                                                                Maximum="100">
                                                                                                <MajorGrid LineColor="Gray"></MajorGrid>
                                                                                                <LabelStyle Font="Verdana, 6pt" />
                                                                                            </AxisX>
                                                                                        </asp:ChartArea>
                                                                                    </ChartAreas>
                                                                                </asp:Chart>

                                                                          </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                        </td>

                                                                         </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                 </table>
               </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="chartAnalyze" /> 
                       <%-- <asp:PostBackTrigger ControlID="Rbl_DT_E" />  --%> 
                    </Triggers>  
           </asp:UpdatePanel>

        </div>
        <div class="clear"></div>
        </div>                            	
        </div>
        <div class="tab_bottom">&nbsp;</div>
  </section>

  <section id="tab4" class="contantbox tabcontant hideclass" >
        <div class="tab_top">&nbsp;</div>
        <div class="tab_middle">
           <div class="combopart">
        <div class="float_l" style="padding-left: 10px;" > 
           
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
              <ContentTemplate>         
           
            <table cellpadding="0" cellspacing="0" width="100%"> 
               <tr>
                <td align="right">
                    <asp:Button ID="btn_Excel" CssClass="button" Width="150px" 
                        Text="Export to Excel" runat="server" onclick="btn_Excel_Click" Visible="False" 
                        meta:resourcekey="btn_ExcelResource1"></asp:Button>                   
                    <asp:Button ID="btn_CSV" CssClass="button" Width="150px" Text="Export to CSV" 
                        runat="server" onclick="btn_CSV_Click" meta:resourcekey="btn_CSVResource1"></asp:Button>                   
                </td>
               </tr>                            
               <tr>
                <td>   
                    <telerik:RadGrid ID="GridPatchDetails"  AllowSorting="True" Width="650px" 
                    AllowPaging="True"  runat="server" AutoGenerateColumns="False" 
                        EnableLinqExpressions="False"  AllowFilteringByColumn="True" 
                    OnNeedDataSource="GridPatchDetails_NeedDataSource" 
                        OnItemDataBound="GridPatchDetails_ItemDataBound" OnItemCommand="GridPatchDetails_ItemCommand"
                    OnSortCommand="GridPatchDetails_SortCommand" CellSpacing="0" GridLines="None">
                    <GroupingSettings CaseSensitive="False" />
                     <ClientSettings>
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" FrozenColumnsCount="6">
                        </Scrolling>
                     </ClientSettings>

                        <HeaderStyle Font-Bold="True" />

                    <PagerStyle Position="Top" AlwaysVisible="True" Font-Size="Small" />
                    <MasterTableView DataKeyNames="HtmlColor">                      
                        <CommandItemSettings ExportToPdfText="Export to PDF" />
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" 
                            Visible="True">
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
                            Visible="True">
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="HtmlColor" HeaderText="Patch"
                                DataField="HtmlColor" FilterControlAltText="Filter HtmlColor column" 
                                meta:resourcekey="GridTemplateColumnResource1">
                                
                                 <FilterTemplate>
                                    <center>
                                    <asp:LinkButton ID="lbtnClearFillter" runat="server" ToolTip="Clear Fillter"  
                                            CommandName="Fillter">
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

                            <telerik:GridBoundColumn DataField="SampleId" HeaderText="ID" 
                                SortExpression="SampleId" AllowFiltering="False" UniqueName="SampleId" 
                                FilterControlAltText="Filter SampleId column" 
                                meta:resourcekey="GridBoundColumnResource1">
                            </telerik:GridBoundColumn>                       
                            <telerik:GridBoundColumn DataField="CmykC" HeaderText="C" 
                                SortExpression="CmykC" FilterControlWidth="30px" 
                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True" 
                                UniqueName="CmykC" DataType="System.Int32" 
                                FilterControlAltText="Filter CmykC column" 
                                meta:resourcekey="GridBoundColumnResource2">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CmykM" HeaderText="M" 
                                SortExpression="CmykM" FilterControlWidth="30px" 
                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True" 
                                UniqueName="CmykM" DataType="System.Int32" 
                                FilterControlAltText="Filter CmykM column" 
                                meta:resourcekey="GridBoundColumnResource3">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CmykY" HeaderText="Y" 
                                SortExpression="CmykY" FilterControlWidth="30px" 
                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True" 
                                UniqueName="CmykY" DataType="System.Int32" 
                                FilterControlAltText="Filter CmykY column" 
                                meta:resourcekey="GridBoundColumnResource4">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CmykK" HeaderText="K" 
                                SortExpression="CmykK" FilterControlWidth="30px" 
                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True" 
                                UniqueName="CmykK" DataType="System.Int32" 
                                FilterControlAltText="Filter CmykK column" 
                                meta:resourcekey="GridBoundColumnResource5">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ΔE" HeaderText="ΔE" SortExpression="ΔE" 
                                FilterControlWidth="30px" CurrentFilterFunction="GreaterThanOrEqualTo" 
                                AutoPostBackOnFilter="True" UniqueName="ΔE" 
                                FilterControlAltText="Filter ΔE column" 
                                meta:resourcekey="GridBoundColumnResource6">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ΔE94" HeaderText="ΔE94" 
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
                            </telerik:GridBoundColumn> 
                            <telerik:GridBoundColumn DataField="TargetL" HeaderText="Target L*" 
                                SortExpression="TargetL" FilterControlWidth="30px" 
                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True" 
                                FilterControlAltText="Filter TargetL column" 
                                meta:resourcekey="GridBoundColumnResource9" UniqueName="TargetL">
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="CielabL" HeaderText="Actual L*" 
                                SortExpression="CielabL" FilterControlWidth="30px" 
                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True" 
                                FilterControlAltText="Filter CielabL column" 
                                meta:resourcekey="GridBoundColumnResource10" UniqueName="CielabL">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Targeta" HeaderText="Target a*" 
                                SortExpression="Targeta" FilterControlWidth="30px" 
                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True" 
                                FilterControlAltText="Filter Targeta column" 
                                meta:resourcekey="GridBoundColumnResource11" UniqueName="Targeta">
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="CielabA" HeaderText="Actual a*" 
                                SortExpression="CielabA" FilterControlWidth="30px" 
                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True" 
                                FilterControlAltText="Filter CielabA column" 
                                meta:resourcekey="GridBoundColumnResource12" UniqueName="CielabA">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Targetb" HeaderText="Target b*" 
                                SortExpression="Targetb" FilterControlWidth="30px" 
                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True" 
                                FilterControlAltText="Filter Targetb column" 
                                meta:resourcekey="GridBoundColumnResource13" UniqueName="Targetb">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CielabB" HeaderText="Actual b*" 
                                SortExpression="CielabB" FilterControlWidth="30px" 
                                CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True" 
                                FilterControlAltText="Filter CielabB column" 
                                meta:resourcekey="GridBoundColumnResource14" UniqueName="CielabB">
                            </telerik:GridBoundColumn>     
                            
                                                
                        </Columns>                        
                    </MasterTableView>                        
                </telerik:RadGrid>

                 </td>
                </tr>
            </table>
            
         </ContentTemplate> 
         <Triggers>
                <asp:PostBackTrigger ControlID="btn_Excel" />               
                <asp:PostBackTrigger ControlID="btn_CSV" />                
         </Triggers>        
       </asp:UpdatePanel>

       </div>
        <div class="clear"></div>
        </div>  
    </div>    
    <div class="tab_bottom">
        &nbsp;</div>
  </section>

  <section id="tab5" class="contantbox tabcontant hideclass">
      <div class="tab_top">&nbsp;</div>
        <div class="tab_middle">
           <div class="combopart">
        <div class="float_l" style="padding-left: 10px;" > 
          <asp:UpdatePanel ID="UpdatePanel6" runat="server">
              <ContentTemplate>         
           
                 <table cellpadding="0" cellspacing="0" width="650px">
                        <tr id="Tr2" runat="server" visible="false">                                       
                            <td>                                        
                                <asp:ImageButton ID="ImgBtn_Report" runat="server" 
                                    ImageUrl="../Images/Sync DLP Report_preview.jpg" 
                                    onclick="ImgBtn_Report_Click" ToolTip="Download PDF Report" 
                                    meta:resourcekey="ImgBtn_ReportResource1"></asp:ImageButton>
                            </td>
                        </tr>                                                    
                        <tr >
                            <td>
                                <table id="TblGeneratePDFReport" runat="server" border="0" width="100%" style="height: 640px;"
                                                         cellpadding="0" cellspacing="0" class="TABLE1"  visible="true">
                                                        <tr>
                                                            <td valign="top">
                                                                <iframe id="FRMGeneratePDFReport" runat="server" name="FRMGeneratePDFReport" style="height: 1600px;
                                                                    width: 100%; display: none" frameborder="0" scrolling="yes"></iframe>
                                                            </td>
                                                        </tr>
                                                    </table>
                            </td>
                        </tr>
                 </table>  
               </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="chartAnalyze" /> 
                       <%-- <asp:PostBackTrigger ControlID="Rbl_DT_E" />  --%> 
                    </Triggers>  
           </asp:UpdatePanel>       
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
    <div class="clear">
    </div>
</div>
<div class="contant_bottom">
    &nbsp;</div>
<asp:HiddenField ID="hdnSelectedTabIndex" runat="server" Value="0" />
<asp:Label ID="InjectScript" runat="server" Width="410px" 
    meta:resourcekey="InjectScriptResource1"></asp:Label>
<telerik:RadWindowManager ID="RadWindowManager" runat="server" Modal="True" 
    meta:resourcekey="RadWindowManagerResource1">
    <Windows>
        <telerik:RadWindow ID="ConfirmSaveWindow" runat="server" ShowContentDuringLoad="false"
            VisibleStatusbar="false" Title="Choose Next Action" Width="702" Height="250"
            Behaviors="Move,Close" NavigateUrl="~/PopupConfirmSave.aspx" 
            meta:resourcekey="ConfirmSaveWindowResource1" >
        </telerik:RadWindow>
    </Windows>
</telerik:RadWindowManager>
 <div class="FadingTooltip" id="FADINGTOOLTIP" style="z-index: 999; visibility: hidden;
        position: absolute">
</div>
