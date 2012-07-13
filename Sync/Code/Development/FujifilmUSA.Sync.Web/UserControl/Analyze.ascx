<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Analyze.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.Analyze" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="uc" %>
<%@ Register Src="~/UserControl/PopupControl/CreateDLP.ascx" TagName="CreateDLP"
    TagPrefix="uc" %>
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
 <script src="../JS/ChartTooltip.js" type="text/javascript"></script>

<div class="contant_top">
    &nbsp;</div>
<div class="contant_middle">
    <asp:UpdatePanel ID="upCreateDLP" runat="server">
        <ContentTemplate>
            <div class="head">
                <h1 id="header" runat="server">
                    Analyze</h1>
                <div class="Information_divAnalyze">
                    <asp:LinkButton ID="lbtnDownloadAndSaveDLP" runat="server" CssClass="button" OnClick="lbtnDownloadAndSaveDLP_Click"
                        OnClientClick="return ChooseRadWindow();">
                        <span>
                            <asp:Image ID="Image2" runat="server" ImageUrl="../Images/export.png" />
                            Create DLP</span> &nbsp;
                    </asp:LinkButton>
                </div>
            </div>
            <div class="listDevice" style="margin-left: 10px;">
                <uc:Statusbar ID="fujiStatusbar" runat="server" Visible="false" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="10" border="0" width="97%" style="margin-left: 10px;">
                <tr id="trListHeader" runat="server">
                    <td>
                        <div style="display: block;" class="contantbox tabcontant">
                            <div class="Analyze_head">
                                <div class="text" style="font-weight: bold; font-size: 14px; float: left">
                                    Analyze Measurement :
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
                                                alert("Atleast one measurement should be checked");
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
                                    <asp:Chart ID="chartAnalyze" runat="server" Width="680px" Height="160px" BackColor="#F0F0F0"
                                        BackGradientStyle="HorizontalCenter" BackSecondaryColor="Silver" Palette="SemiTransparent"
                                        AntiAliasing="All" BorderlineColor="#B0B0B0" BorderlineWidth="1" BorderlineDashStyle="Solid">                                         
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
                                                <Position Height="99" Width="99" X="0" Y="0" />
                                                <AxisY Title="&Delta; E2000" TitleFont="Verdana, 8pt" Interval="Auto" IsLabelAutoFit="false">
                                                    <MajorGrid LineColor="#D8D8D8 " />
                                                </AxisY>
                                                <AxisX Interval="1" TitleFont="Verdana, 5pt" IsLabelAutoFit="false">
                                                    <MajorGrid LineColor="#D8D8D8 " />
                                                    <LabelStyle Angle="0" />
                                                </AxisX>
                                            </asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>
                                    <asp:HiddenField ID="hdMeasurement" runat="server" Value="0" />
                                    <asp:Button ID="hdChartButton" runat="server" Text="Button" OnClick="hdChartButton_Click"
                                        Style="display: none;" />
                                    <asp:CheckBox ID="hdChartCheckBox" runat="server" Checked="false" Style="display: none;" />
                                    <asp:HiddenField ID="hdCheckBoxChange" runat="server" Value="0" />
                                </center>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true" AssociatedUpdatePanelID="upCreateDLP"
        DisplayAfter="50">
        <ProgressTemplate>
            <AjaxToolkit:AlwaysVisibleControlExtender ID="AlwaysVisibleControl" HorizontalSide="Center"
                VerticalSide="Middle" TargetControlID="updateProgress" HorizontalOffset="325"
                VerticalOffset="210" runat="server">
            </AjaxToolkit:AlwaysVisibleControlExtender>
            <div style="background: #CCC !important;">
                <uc:CreateDLP runat="server" ID="ucCreateDLP" DLPMessage='<%# DLPMessage %>' />
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
                <li><span><a href="#tab1">Summary</a></span></li>
                <li><span><a href="#tab2">Report</a></span></li>                                
                <li><span><a href="#tab3">Ink Paper</a></span></li>                                
                <li><span><a href="#tab4">Gray</a></span></li>                                
                <li><span><a href="#tab5">Patch Details</a></span></li>                                
            </ul>
            <div class="clear"></div>
        </section>


  <section id="tab1" class="contantbox tabcontant hideclass" >
        <div class="tab_top">&nbsp;</div>
        <div class="tab_middle">
           <div class="combopart">
        <div class="float_l" style="padding-left: 10px;" > 
          <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr id="tr1" runat="server" visible="false">
                                        <td style="width:100px;">
                                            <span>Selected Metric: &nbsp </span>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbSelectedMetric" runat="server" Width="50%" CssClass="select">
                                                <asp:ListItem Text="Idealliance Digital Chart" Value="Idealliance Digital Chart"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                    <td colspan="2">
                                         <asp:ListView ID="grdMeasurement" runat="server" ItemPlaceholderID="phItemPlaceholder">
                                            <LayoutTemplate>
                                                <table cellspacing="0" cellpadding="0" border="0" class="product_list_table" style="margin:0" >
                                                    <thead>
                                                      <tr>                                                         
                                                            <th style="font-size:13px;" >
                                                                Measurement Name
                                                            </th>
                                                            <th style="font-size:13px;">
                                                                Upload Date
                                                            </th>
                                                            <th style="font-size:13px;">
                                                                Avg ΔE
                                                            </th>
                                                            <th style="font-size:13px;">
                                                                Max ΔE
                                                            </th>
                                                            <th style="font-size:13px;">
                                                                Avg ΔE94
                                                            </th>
                                                            <th style="font-size:13px;">
                                                                Max ΔE94
                                                            </th>
                                                            <th style="font-size:13px;">
                                                                Avg ΔE2000
                                                            </th>
                                                            <th style="font-size:13px;">
                                                                Max ΔE2000
                                                            </th>
                                                      </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:PlaceHolder ID="phItemPlaceholder" runat="server"></asp:PlaceHolder>
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>
                                            <EmptyDataTemplate>
                                                <div class="no_data_found">No records available</div>
                                            </EmptyDataTemplate>
                                            <ItemTemplate>
                                                <tr class="Gray">                                                  
                                                     <td align="center">
                                                        <asp:Label ID="lblMeasurementName" runat="server" Text='<%#Eval("MeasurementName") %>'></asp:Label>
                                                    </td>
                                                    <td align="center" >
                                                        <asp:Label ID="lblMeasurementDate" runat="server" Text='<%# string.Format("{0:d}",Eval("MeasurementDate")) %>'></asp:Label>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="lblAvg_ΔE" runat="server" Text='<%#Eval("Avg_ΔE") %>'></asp:Label>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="lblMax_ΔE" runat="server" Text='<%#Eval("Max_ΔE") %>'></asp:Label>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="lblAvg_ΔE94" runat="server" Text='<%#Eval("Avg_ΔE94") %>'></asp:Label>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="lblMax_ΔE94" runat="server" Text='<%#Eval("Max_ΔE94") %>'></asp:Label>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="lblAvg_ΔE2000" runat="server" Text='<%#Eval("Avg_ΔE2000") %>'></asp:Label>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="lblMax_ΔE2000" runat="server" Text='<%#Eval("Max_ΔE2000") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </td>
                                    </tr>
                                </table>
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
            <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr runat="server">                                       
                                        <td>
                                        
                                          <asp:ImageButton ID="ImgBtn_Report" runat="server" 
                                                ImageUrl="../Images/Sync DLP Report_preview.jpg" onclick="ImgBtn_Report_Click" ToolTip="Download PDF Report"></asp:ImageButton>
                                        </td>
                                    </tr>                                   
                                </table>
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
       
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
              <ContentTemplate>    
                  <table border="1" cellpadding="0" cellspacing="0" width="100%" style="background:  url(images/ChartBg.jpg) no-repeat center;">
                    <tr>
                        <td style="width: 50%;" valign="top" class="CHARTTABLE">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%" >
                                <tr>
                                    <td colspan="2" class="green-title">
                                        &nbsp; &Delta;E Primary Patches
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 40%;" valign="top">
                                        <table cellpadding="0" cellspacing="0" width="100%"  >
                          
                                    <tr>
                                        <td colspan="2" style="height: 40px;" class="SyncBottomBorder">
                                        </td>
                                    </tr>
                                    <asp:Literal ID="LitDeltaEPatches" runat="server"></asp:Literal>
                                    </table>
                                    </td>
                                    <td style="width: 60%; " id="TDEPrimary" runat="server">
                                        <asp:Chart ID="ChartSummary_DeltaE" runat="server" Width="210px" Height="362px" EnableViewState="false" BackColor="Transparent">   
                                                                                                                              
                                                            <Series>
                                                                <asp:Series Name="Series0"  Color="Transparent">                                              
                                                                </asp:Series>
                                                                <asp:Series Name="Series1" BorderWidth="0" BorderColor="Gray" Color="Gray" 
                                                                                            IsVisibleInLegend="false">
                                                                </asp:Series>    
                                                            </Series>
                                                            <ChartAreas>
                                                                <asp:ChartArea Name="ChartArea1" BackColor="Transparent">
                                                                     <Area3DStyle  Enable3D="false" IsRightAngleAxes="False" WallWidth="0"
                                                                                              IsClustered="True"></Area3DStyle>
                                          
                                                                    <AxisX  Enabled="true"  IsReversed="true" IsLabelAutoFit="false" Interval="Auto" >
                                                                        <MajorGrid Enabled="false" LineColor="Beige" />
                                                                        <MajorTickMark Enabled="false" />
                                                                        <LabelStyle Font="Verdana, 7pt" />
                                                                    </AxisX>
                                                                     <AxisY Enabled="True" IsLabelAutoFit="False">
                                                                        <MajorGrid LineColor="Beige"></MajorGrid>
                                                                        <LabelStyle Format="###" Font="Verdana, 7pt" ForeColor="Black" />
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
                                        &nbsp; &#916; Paper L* a* b*
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
                                                                        EnableViewState="false">
                                                                        <Legends>
                                                                            <asp:Legend Name="Default" Enabled="False">
                                                                            </asp:Legend>
                                                                        </Legends>
                                                                        <Series>
                                                                            <asp:Series Name="Green" ChartType="Polar" 
                                                                                MarkerSize="10" MarkerBorderWidth="2" MarkerBorderColor="Green" MarkerStyle="Circle"
                                                                                MarkerColor="Green" ShadowOffset="1">                                                                                           
                                                                            </asp:Series>
                                                                            <asp:Series Name="Paper" ChartType="Polar" MarkerBorderColor="Black" MarkerColor="White"
                                                                                    MarkerSize="10" MarkerBorderWidth="2"
                                                                                MarkerStyle="Circle" ShadowOffset="1">                                                                                             
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
                                                                                    <MajorTickMark Enabled="false" />
                                                                                    <LabelStyle Enabled="False" />
                                                                                </AxisY>
                                                                                <AxisX IsMarginVisible="False" TitleAlignment="Near" Maximum="20" Minimum="-20" LineDashStyle="NotSet">
                                                                                    <MajorGrid Enabled="False" />
                                                                                    <MajorTickMark Enabled="false" />
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
                                                                        EnableViewState="false">
                                                                        <Legends>
                                                                            <asp:Legend Name="Default" Enabled="False">
                                                                            </asp:Legend>
                                                                        </Legends>
                                                                        <Series>
                                                                            <asp:Series Name="Series1" BorderColor="100, 0 , 0 , 0" ChartArea="Default" BorderWidth="1"
                                                                                IsVisibleInLegend="false">                                                                                               
                                                                            </asp:Series>
                                                                            <asp:Series Name="Series2" Color="Green" ChartArea="Default" BorderWidth="2" IsVisibleInLegend="false">
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
                                                                                    <MajorTickMark Enabled="false" />
                                                                                    <LabelStyle Enabled="False" />
                                                                                </AxisY>
                                                                                <AxisX IsMarginVisible="true" TitleAlignment="Near" LineDashStyle="NotSet">
                                                                                    <MajorGrid Enabled="False" />
                                                                                    <MajorTickMark Enabled="false" />
                                                                                    <LabelStyle Enabled="False" />
                                                                                </AxisX>
                                                                                <AxisX2 Interval="Auto" Minimum="0" Maximum="1" LineDashStyle="NotSet">
                                                                                    <MajorGrid Enabled="False" />
                                                                                    <MajorTickMark Enabled="false" />
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
                    </Triggers>  
            </asp:UpdatePanel>
        </div>
        <div class="clear"></div>
        </div>                            	
        </div>
        <div class="tab_bottom">&nbsp;</div>
  </section>
    
   <section id="tab4" class="contantbox tabcontant hideclass">
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
                                                    &nbsp; Gamut Plot
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table id="Table1" runat="server" cellpadding="0" cellspacing="0" border="0" width="100%" class="SYNCTABLE">                                                     
                                                        <tr>
                                                            <td class="green-title">
                                                                a* - b* Comparison
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" valign="top">
                                                           
                                                                <asp:Chart ID="CHART_ABComparison" runat="server" BackImageWrapMode="Scaled" BackImage="~/Images/CPSyncABComparison.jpg"
                                                                    ImageType="Jpeg" Width="280px" Height="250px" EnableViewState="false">
                                                                    <Legends>
                                                                        <asp:Legend Name="Default" Enabled="False">
                                                                        </asp:Legend>
                                                                    </Legends>
                                                                    <Series>
                                                                         <asp:Series Name="Targets" ChartType="Polar"
                                                                            MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1">
                                                                        </asp:Series>
                                                                        <asp:Series Name="Cyan" ChartType="Polar" 
                                                                            MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1">
                                                                        </asp:Series>
                                                                        <asp:Series Name="Magenta" ChartType="Polar" 
                                                                            MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1">
                                                                        </asp:Series>
                                                                        <asp:Series Name="Yellow" ChartType="Polar" 
                                                                            MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1">
                                                                        </asp:Series>
                                                                        <asp:Series Name="Red" ChartType="Polar" 
                                                                            MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1">
                                                                        </asp:Series>
                                                                        <asp:Series Name="Green" ChartType="Polar" 
                                                                            MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1">
                                                                        </asp:Series>
                                                                        <asp:Series Name="Blue" ChartType="Polar" 
                                                                            MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1">
                                                                        </asp:Series>
                                                                        <asp:Series Name="Paper" ChartType="Polar" 
                                                                            MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1">
                                                                        </asp:Series>
                                                                    </Series>
                                                                    <BorderSkin PageColor="AliceBlue" />
                                                                    <Titles>
                                                                        <asp:Title Name="Title1">
                                                                        </asp:Title>
                                                                    </Titles>
                                                                    <ChartAreas>
                                                                        <asp:ChartArea BackColor="Transparent" BorderColor="" Name="Default">
                                                                            <AxisY LineColor="120, 0, 0, 0" TitleAlignment="Near" Maximum="100" Minimum="-100"
                                                                                LineDashStyle="NotSet">
                                                                                <MajorGrid Enabled="False" />
                                                                                <MajorTickMark Enabled="false" />
                                                                                <LabelStyle Enabled="False" />
                                                                            </AxisY>
                                                                            <AxisX IsMarginVisible="False" TitleAlignment="Near" Maximum="100" Minimum="-100" LineDashStyle="NotSet">
                                                                                <MajorGrid Enabled="False" />
                                                                                <MajorTickMark Enabled="false" />
                                                                                <LabelStyle Enabled="False" />
                                                                            </AxisX>
                                                                        </asp:ChartArea>
                                                                    </ChartAreas>
                                                                </asp:Chart>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="green-title">
                                                                L* Comparison
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" valign="top">                                                            
                                                                <asp:Chart ID="CHART_LComparison" runat="server" BackGradientType="Center"
                                                                                        BackGradientEndColor="White" BorderLineColor="#808080" Width="280px" Height="75px" EnableViewState="false">
                                                                    <Legends>
                                                                        <asp:Legend Name="Default" Enabled="False">
                                                                        </asp:Legend>
                                                                    </Legends>
                                                                    <Series>
                                                                        <asp:Series Name="Series0" ChartType="Polar"
                                                                            MarkerSize="12" MarkerStyle="Circle" MarkerColor="Green" Color="Green" ShadowOffset="1">
                                                                        </asp:Series>
                                                                        <asp:Series Name="Series1" ChartType="Polar" 
                                                                            MarkerSize="12" MarkerStyle="Circle" MarkerColor="Green" MarkerBorderWidth="1"
                                                                            Color="Green" ShadowOffset="1">
                                                                        </asp:Series>
                                                                        <asp:Series Name="Series2" ChartType="FastPoint"
                                                                            MarkerSize="12" MarkerStyle="Circle" MarkerColor="Green" Color="Red" ShadowOffset="1">
                                                                        </asp:Series>
                                                                        <asp:Series Name="Series3" ChartType="FastPoint" 
                                                                            MarkerSize="12" MarkerStyle="Circle" ShadowOffset="1">
                                                                        </asp:Series>
                                                                        <asp:Series Name="Series4" ChartType="FastPoint" 
                                                                            MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1">
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
                                                                              <Position Height="99" Width="100" X="0" Y="0" />
                                                                         
                                                                              <AxisY  IsMarginVisible="False" TitleAlignment="Near" IsLabelAutoFit="False" Title="" Interval="15"
                                                                                    Minimum="-15" Maximum="15">
                                                                                    <MajorGrid LineColor="Gray"></MajorGrid>
                                                                                    <LabelStyle Angle="0" Font="Verdana, 6pt" />
                                                                                </AxisY>
                                                                                <AxisX IsLabelAutoFit="False" IsMarginVisible="False" Interval="20" Title="" Minimum="-100" Maximum="100">
                                                                                    <MajorGrid LineColor="Gray"></MajorGrid>
                                                                                    <LabelStyle Angle="0" Font="Verdana, 6pt" />
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
                                                                &nbsp; Gray Balance L* a* b*
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                
                                                                                <asp:Chart ID="Chart_L" runat="server" Width="365px" Height="125px" 
                                                                                        BackGradientType="Center"
                                                                                        BackGradientEndColor="White" BorderLineColor="#808080" EnableViewState="false">
                                                                                        <Titles>
                                                                                            <asp:Title ForeColor="Black" Text="L*">
                                                                                                <Position X="94" Y="10" />
                                                                                            </asp:Title>
                                                                                        </Titles>
                                                                                        <Legends>
                                                                                            <asp:Legend Name="Default" Enabled="false">
                                                                                            </asp:Legend>
                                                                                        </Legends>
                                                                                        <BorderSkin SkinStyle="None"></BorderSkin>
                                                                               
                                                                                        <Series>
                                                                                            <asp:Series Name="Series1" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                                                                                Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                                                                MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                                                                            </asp:Series>
                                                                                            <asp:Series MarkerSize="12" BackSecondaryColor="162, 164, 255" IsVisibleInLegend="false"
                                                                                                Color="Green" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                                                                                BorderWidth="2" Name="Series2">
                                                                                            </asp:Series>
                                                                                        </Series>
                                                                                        <ChartAreas>
                                                                                            <asp:ChartArea Name="Default" BorderColor="26, 59, 105" BackSecondaryColor="Gray"
                                                                                                BackColor="White" BackGradientStyle="TopBottom" BorderDashStyle="Solid" ShadowOffset="1">
                                                                                                <Position Height="99" Width="100" X="0" Y="0" />
                                                                                                <AxisY IsLabelAutoFit="False" Title="" TitleFont="Verdana, 8pt, style=Bold" Interval="25"
                                                                                                    Minimum="0" Maximum="100">
                                                                                                    <MajorGrid LineColor="Gray"></MajorGrid>
                                                                                                    <LabelStyle Angle="0" Font="Verdana, 6pt" />
                                                                                                </AxisY>
                                                                                                <AxisX IsLabelAutoFit="False" IsMarginVisible="False" Interval="25" Title="" Minimum="0" Maximum="100">
                                                                                                    <MajorGrid LineColor="Gray"></MajorGrid>
                                                                                                    <LabelStyle Angle="0" Font="Verdana, 6pt" ForeColor="Transparent" />
                                                                                                </AxisX>
                                                                                            </asp:ChartArea>
                                                                                        </ChartAreas>
                                                                                    </asp:Chart>

                                                                         </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>                                                                        

                                                                                 <asp:Chart ID="Chart_a" runat="server" Width="365px" Height="125px" BackGradientType="Center" 
                                                                                    BackGradientEndColor="White" BorderLineColor="#808080" EnableViewState="false">
                                                                                    <Legends>
                                                                                        <asp:Legend Name="Default" Enabled="false">
                                                                                        </asp:Legend>
                                                                                    </Legends>
                                                                                    <Titles>
                                                                                        <asp:Title ForeColor="Black" Text="a*">
                                                                                            <Position X="94" Y="10" />
                                                                                        </asp:Title>
                                                                                    </Titles>
                                                                                    <BorderSkin SkinStyle="None"></BorderSkin>                                                                                   
                                                                                    <Series>
                                                                                        <asp:Series Name="Series1" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                                                                            Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                                                            MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                                                                        </asp:Series>
                                                                                        <asp:Series MarkerSize="12" BackSecondaryColor="162, 164, 255" IsVisibleInLegend="false"
                                                                                            Color="Green" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                                                                            BorderWidth="2" Name="Series2">
                                                                                        </asp:Series>
                                                                                    </Series>
                                                                                    <ChartAreas>
                                                                                        <asp:ChartArea Name="Default" BorderColor="26, 59, 105" BackSecondaryColor="#339966"
                                                                                            BackColor="#ff66cc" BackGradientStyle="TopBottom" BorderDashStyle="Solid" ShadowOffset="1">
                                                                                            <Position Height="99" Width="100" X="0" Y="0" />
                                                                                            <AxisY IsLabelAutoFit="False" Title="" TitleFont="Verdana, 8pt, style=Bold" Interval="2"
                                                                                                Minimum="-6" Maximum="6">
                                                                                                <MajorGrid LineColor="Gray"></MajorGrid>
                                                                                                <LabelStyle Angle="0" Font="Verdana, 6pt" />
                                                                                            </AxisY>
                                                                                            <AxisX IsLabelAutoFit="False" IsMarginVisible="False" Title="" Interval="25" Minimum="0" Maximum="100">
                                                                                                <MajorGrid LineColor="Gray"></MajorGrid>
                                                                                                <LabelStyle Angle="0" Font="Verdana, 6pt" ForeColor="Transparent" />
                                                                                            </AxisX>
                                                                                        </asp:ChartArea>
                                                                                    </ChartAreas>
                                                                                </asp:Chart>
                                                                                
                                                                         </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                        
                                                                                 <asp:Chart ID="Chart_b" runat="server" Width="365px" Height="125px" BackGradientType="Center" 
                                                                                    BackGradientEndColor="White" BorderLineColor="#808080" EnableViewState="false">
                                                                                    <Legends>
                                                                                        <asp:Legend Name="Default" Enabled="false">
                                                                                        </asp:Legend>
                                                                                    </Legends>
                                                                                    <Titles>
                                                                                        <asp:Title ForeColor="Black" Text="b*">
                                                                                            <Position X="94" Y="10" />
                                                                                        </asp:Title>
                                                                                    </Titles>
                                                                                    <BorderSkin SkinStyle="None"></BorderSkin>
                                                                                   
                                                                                    <Series>
                                                                                        <asp:Series Name="Series1" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                                                                            Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                                                            MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                                                                        </asp:Series>
                                                                                        <asp:Series MarkerSize="12" BackSecondaryColor="162, 164, 255" IsVisibleInLegend="false"
                                                                                            Color="Green" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                                                                            BorderWidth="2" Name="Series2">
                                                                                        </asp:Series>
                                                                                    </Series>
                                                                                    <ChartAreas>
                                                                                        <asp:ChartArea Name="Default" BorderColor="26, 59, 105" BackSecondaryColor="#3333cc"
                                                                                            BackColor="#ffff99" BackGradientStyle="TopBottom" BorderDashStyle="Solid" ShadowOffset="1">
                                                                                            <Position Height="99" Width="100" X="0" Y="0" />
                                                                                            <AxisY IsLabelAutoFit="False" Title="" TitleFont="Verdana, 8pt, style=Bold" Interval="2"
                                                                                                Minimum="-6" Maximum="6">
                                                                                                <MajorGrid LineColor="Gray"></MajorGrid>
                                                                                                <LabelStyle Angle="0" Font="Verdana, 6pt" />
                                                                                            </AxisY>
                                                                                            <AxisX IsLabelAutoFit="False" IsMarginVisible="False" Interval="25" Minimum="0" Maximum="100"
                                                                                                Title="">
                                                                                                <MajorGrid LineColor="Gray"></MajorGrid>
                                                                                                <LabelStyle Angle="0" Font="Verdana, 6pt" />
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
                    </Triggers>  
           </asp:UpdatePanel>

        </div>
        <div class="clear"></div>
        </div>                            	
        </div>
        <div class="tab_bottom">&nbsp;</div>
  </section>

  <section id="tab5" class="contantbox tabcontant hideclass" >
        <div class="tab_top">&nbsp;</div>
        <div class="tab_middle">
           <div class="combopart">
        <div class="float_l" style="padding-left: 10px;" > 
           
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
              <ContentTemplate>         
           
            <table cellpadding="0" cellspacing="0" width="100%"> 
               <tr>
                <td align="right">
                    <asp:Button ID="btn_Excel" CssClass="button" Width="150px" Text="Export to Excel" runat="server" onclick="btn_Excel_Click" Visible="false"></asp:Button>                   
                    <asp:Button ID="btn_CSV" CssClass="button" Width="150px" Text="Export to CSV" runat="server" onclick="btn_CSV_Click"></asp:Button>                   
                </td>
               </tr>                            
               <tr>
                <td>   
                    <telerik:RadGrid ID="GridPatchDetails" ShowHeader="true"  AllowSorting="true" Width="650px" 
                    AllowPaging="True"  runat="server" AutoGenerateColumns="false" EnableLinqExpressions="false"  AllowFilteringByColumn="true" 
                    OnNeedDataSource="GridPatchDetails_NeedDataSource" OnItemDataBound="GridPatchDetails_ItemDataBound" HeaderStyle-Font-Bold="true" OnItemCommand="GridPatchDetails_ItemCommand"
                    OnSortCommand="GridPatchDetails_SortCommand">
                    <GroupingSettings CaseSensitive="false" />
                     <ClientSettings>
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True" FrozenColumnsCount="6">
                        </Scrolling>
                     </ClientSettings>

                    <PagerStyle Position="Top" AlwaysVisible="true" Font-Size="Small" />
                    <MasterTableView DataKeyNames="HtmlColor">                      
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="HtmlColor" HeaderText="Patch"
                                DataField="HtmlColor" AllowFiltering="true">
                                
                                 <FilterTemplate>
                                    <center>
                                    <asp:LinkButton ID="lbtnClearFillter" runat="server" ToolTip="Clear Fillter"  CommandName="Fillter">
                                                    <span>
                                                        <asp:Image ID="imgClearFillter" runat="server" AlternateText="Clear Fillter" ImageUrl="../Images/clear-fillter.png" />
                                                    </span>
                                    </asp:LinkButton>
                                    </center>
                                </FilterTemplate>
                                <ItemTemplate>         
                                    <asp:Label ID="lbl_HTML" runat="server" Text="" Height="14px" Width="30px"></asp:Label>                                   
                                </ItemTemplate>
                                
                            </telerik:GridTemplateColumn>                         

                            <telerik:GridBoundColumn DataField="SampleId" HeaderText="ID" SortExpression="SampleId" AllowFiltering="false" UniqueName="SampleId">
                            </telerik:GridBoundColumn>                       
                            <telerik:GridBoundColumn DataField="CmykC" HeaderText="C" SortExpression="CmykC" AllowFiltering="true" FilterControlWidth="30" CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="true" UniqueName="CmykC" DataType="System.Int32">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CmykM" HeaderText="M" SortExpression="CmykM" AllowFiltering="true" FilterControlWidth="30" CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="true" UniqueName="CmykM" DataType="System.Int32">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CmykY" HeaderText="Y" SortExpression="CmykY" AllowFiltering="true" FilterControlWidth="30" CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="true" UniqueName="CmykY" DataType="System.Int32">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CmykK" HeaderText="K" SortExpression="CmykK" AllowFiltering="true" FilterControlWidth="30" CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="true" UniqueName="CmykK" DataType="System.Int32">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ΔE" HeaderText="ΔE" SortExpression="ΔE" AllowFiltering="true" FilterControlWidth="30" CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="true" UniqueName="ΔE">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ΔE94" HeaderText="ΔE94" SortExpression="ΔE94" AllowFiltering="true" FilterControlWidth="30" CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ΔE2000" HeaderText="ΔE2000" SortExpression="ΔE2000" AllowFiltering="true" FilterControlWidth="30" CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="true">
                            </telerik:GridBoundColumn> 
                            <telerik:GridBoundColumn DataField="TargetL" HeaderText="Target L*" SortExpression="TargetL" AllowFiltering="true" FilterControlWidth="30" CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="true">
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="CielabL" HeaderText="Actual L*" SortExpression="CielabL" AllowFiltering="true" FilterControlWidth="30" CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Targeta" HeaderText="Target a*" SortExpression="Targeta" AllowFiltering="true" FilterControlWidth="30" CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="true">
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="CielabA" HeaderText="Actual a*" SortExpression="CielabA" AllowFiltering="true" FilterControlWidth="30" CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Targetb" HeaderText="Target b*" SortExpression="Targetb" AllowFiltering="true" FilterControlWidth="30" CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CielabB" HeaderText="Actual b*" SortExpression="CielabB" AllowFiltering="true" FilterControlWidth="30" CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="true">
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
<asp:Label ID="InjectScript" runat="server" Width="410px"></asp:Label>
<telerik:RadWindowManager ID="RadWindowManager" runat="server" Modal="True">
    <Windows>
        <telerik:RadWindow ID="ConfirmSaveWindow" runat="server" ShowContentDuringLoad="false"
            VisibleStatusbar="false" Title="Choose Next Action" Width="702" Height="250"
            Behaviors="Move,Close" NavigateUrl="~/PopupConfirmSave.aspx" >
        </telerik:RadWindow>
    </Windows>
</telerik:RadWindowManager>
 <div class="FadingTooltip" id="FADINGTOOLTIP" style="z-index: 999; visibility: hidden;
        position: absolute">
</div>