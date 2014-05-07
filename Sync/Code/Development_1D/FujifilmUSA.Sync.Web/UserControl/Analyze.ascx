<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="Analyze.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.Analyze" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="uc" %>
<%@ Register Src="~/UserControl/PopupControl/CreateDLP.ascx" TagName="CreateDLP"
    TagPrefix="uc" %>
<script src="JS/DYMO.Label.Framework.latest.js?version=<%=JsRandomNumber %>" type="text/javascript"></script>
<script src="JS/Printing.js?version=<%=JsRandomNumber %>" type="text/javascript"></script>



 <script type="text/javascript">

     function CheckAvgCompability() {
       
         var answer = document.getElementById('ContentPlaceHolder1_ucAnalyze_hd_CompError').value;
        
         if (answer == "0") { 
             setTimeout('__doPostBack(\'ctl00$ContentPlaceHolder1$ucAnalyze$chkAnalyzeAverageOfMeasurements\',\'\')', 0);
             return true;
         }
         else if (answer == "1") {
             jQuery("[id*='lblCompError']").html("Error in different ReferenceCharts, Please select only measurements which used same reference charts.");
             return false;
         }
         else if (answer == "2") {
             jQuery("[id*='lblCompError']").html("Error in different MetricTargetPresets, Please select only measurements which used same metric target presets.");
             return false;
         }
         else {
             return false;
         }
     }
   
    </script>

<script type="text/javascript">
    var previewImageCodedString = '';
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_initializeRequest(InitializeRequest);

    function InitializeRequest(sender, args) {
        //  alert(jQuery("input[id$=hdHasMeasurement]").val());
        if (jQuery("input[id$=hdHasMeasurement]").length > 0) {
            if (jQuery("input[id$=hdHasMeasurement]").val() == 'True' || jQuery("input[id$=hdHasMeasurement]").val() == 'true') {  //-------------If there are measurements under this opt. run, then only able to create DLP
                console.log(jQuery("input[id$=hdHasMeasurement]").val());
                var updateProgress = $get('<%= UpdateProgressClientID %>');
                var postBackElement = args.get_postBackElement();
                if (postBackElement.id == '<%= lbtnDownloadAndSaveDLP.ClientID %>' || postBackElement.id == '<%= lbtnMeasurement.ClientID %>') {
                    if (($('#<%= lbtnDownloadAndSaveDLP.ClientID %>').length > 0 && $get('<%= lbtnDownloadAndSaveDLP.ClientID %>').text == "Next") || postBackElement.id == '<%= lbtnMeasurement.ClientID %>') {
                        updateProgress.control._associatedUpdatePanelId = null;
                        $get('<%= CreateDLPUpdateProgressClientID %>').control._associatedUpdatePanelId = 'dummyId';
                    }

                    else {
                        updateProgress.control._associatedUpdatePanelId = 'dummyId';
                    }
                }
                else if (postBackElement.id == '<%= lbtnSubmit.ClientID %>') {
                    updateProgress.control._associatedUpdatePanelId = null;
                    $get('<%= CreateDLPUpdateProgressClientID %>').control._associatedUpdatePanelId = 'dummyId';
                }
                else {
                    if (postBackElement.id == '<%= lbtnMeasurementVerification.ClientID %>') {
                        $get('<%= CreateDLPUpdateProgressClientID %>').control._associatedUpdatePanelId = 'dummyId';
                    }
                    else {
                        updateProgress.control._associatedUpdatePanelId = null;
                    }
                }
            }
            else
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

    function ShowClosePrintConditionWindow(projectId, projectName, licenseTypeId, DlpName) {
        var oWindow;
        oWindow = window.radopen("PopupClosePrintCondition.aspx?projectId=" + projectId + "&projectName=" + projectName + "&licenseTypeId=" + licenseTypeId + "&DlpName=" + DlpName + "&ToSubOperation=0", "ClosePrintConditionWindow");
        oWindow.show();
        return false;
    }

    function CloseClosePrintConditionWindow() {
        var oWnd = $find("<%= ClosePrintConditionWindow.ClientID %>");
        oWnd.close();
    }       
</script>
<script type="text/javascript" language="javascript">
    $(document).ready(function () {
        if (!IsPrinterInstalled()) {
            $('input[type="image"][id*="ibtnPrintPreview"]').hide();
            $('input[type="image"][id*="ibtnPrintLabel"]').hide();
        }
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

        if (jQuery("input[id$=hdHasMeasurement]").length > 0) {

            if (jQuery("input[id$=hdHasMeasurement]").val() == 'True' || jQuery("input[id$=hdHasMeasurement]").val() == 'true') {  //-------------If there are measurements under this opt. run, then only able to create DLP

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
            else
                return false;
        }
    }
</script>
<script src="JS/ChartTooltip.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
    function funTVIVisiblity() {
        document.getElementById("trTVI").style.display = "";
        document.getElementById("trSpread").style.display = "none";

    }
    function funSpreadVisiblity() {
        document.getElementById("trTVI").style.display = "none";
        document.getElementById("trSpread").style.display = "";

    }
</script>
<script language="javascript" type="text/javascript">
    jQuery(document).ready(function () {
        $("div[id*='accordion']").click
        (
                function () {
                    var $panel = $("div[id*='panelabGamutComparison']");
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

        $("div[id*='accordion']").click();        
    });
    
</script>
<div class="contant_top">
    &nbsp;</div>
<div class="contant_middle">
    <asp:UpdatePanel ID="upCreateDLP" runat="server">
        <ContentTemplate>
            <div class="head">
                <h1 id="header" runat="server">
                    <asp:Localize runat="server" Text="Analyze" ID="btnAnalyze" meta:resourcekey="btnAnalyzeResource1"></asp:Localize></h1>
                <div class="Information_divAnalyze">
                    <asp:LinkButton ID="lbtnDownloadAndSaveDLP" runat="server" CssClass="button" OnClick="lbtnDownloadAndSaveDLP_Click"
                        OnClientClick="return ChooseRadWindow();">
                        <span>
                            <asp:Image ID="Image2" runat="server" ImageUrl="../Images/export.png" meta:resourcekey="Image2Resource1" />
                            <asp:Localize runat="server" Text="Create DLP" ID="locCreateDLP" meta:resourcekey="locCreateDLPResource1"></asp:Localize>
                        </span>
                    </asp:LinkButton>
                    <asp:LinkButton ID="lbtnSubmit" CssClass="buttonNormal" runat="server" Visible="false"
                        OnClick="lbtnSubmit_Click">
                        <span>
                            <asp:Localize runat="server" Text="Submit" ID="locSubmit" meta:resourcekey="locSubmitResource1"></asp:Localize>
                        </span>
                    </asp:LinkButton>
                    &nbsp;
                    <asp:LinkButton ID="lbtnMeasurement" runat="server" CssClass="buttonLeft" OnClick="lbtnMeasurement_Click">
                        <span>
                            <asp:Localize runat="server" Text="Measurement" ID="locMeasurement" meta:resourcekey="locMeasurementResource1"></asp:Localize>
                        </span>
                    </asp:LinkButton>
                    <asp:LinkButton ID="lbtnMeasurementVerification" runat="server" CssClass="button"
                        OnClick="lbtnMeasurementVerification_Click">
                        <span>
                            <asp:Image ID="imgAnalyze" runat="server" ImageUrl="../Images/analyze.png" />
                            <asp:Localize runat="server" Text="Measure" ID="locMeasurementVerification" meta:resourcekey="locMeasurementVerificationResource1"></asp:Localize>
                        </span>
                    </asp:LinkButton>
                    <asp:LinkButton ID="lbtnOptimize" CssClass="buttonNormal" runat="server" Visible="false">
                        <span>
                            <asp:Localize runat="server" Text="Optimize" ID="locOptimize" meta:resourcekey="locOptimizeResource1"></asp:Localize>
                        </span>
                    </asp:LinkButton>
                </div>
            </div>
            <div class="listDevice" style="margin-left: 10px;">
                <uc:Statusbar ID="fujiStatusbar" runat="server" Visible="False" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>--%>
    <table cellpadding="0" cellspacing="10" border="0" width="97%" style="margin-left: 10px;">
        <tr id="trRBtnListDeltaE" runat="server">
            <td align="center">
                <asp:RadioButtonList ID="rbtnListDeltaE" runat="server" RepeatDirection="Horizontal"
                    AutoPostBack="true" OnSelectedIndexChanged="rbtnListDeltaE_SelectedIndexChanged"
                    CssClass="RadioList">
                    <asp:ListItem Text="ΔE2000" Value="E2000" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="ΔE" Value="E"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td id="tdAverageOfMeasurements" runat="server" visible="false">                
                <asp:CheckBox ID="chkAnalyzeAverageOfMeasurements" runat="server" Checked="false" EnableViewState="true"
                    Font-Bold="true" AutoPostBack="true" OnCheckedChanged="chkAnalyzeAverageOfMeasurements_Click" >
                </asp:CheckBox>
                <span style="font-weight: bold; vertical-align: text-top;">Analyze Average Of Measurements</span> 
                <asp:Label ID="lblCompError" runat="server" Text="" ForeColor="Red"></asp:Label> 
                <input id="hd_CompError" type="hidden" runat="server"  value="5"/>
            </td>
        </tr>
        <tr id="trListHeader" runat="server">
            <td runat="server">
                <div style="display: block;" class="contantbox tabcontant1">
                    <div class="Analyze_head">
                        <div id="Div_Opt_Header" runat="server" visible="false" class="Analyze_head_text"
                            style="font-weight: bold; font-size: 14px; float: left;">
                            <asp:Localize runat="server" Text="Analyze Measurement :" ID="locAnaluzeMeasurement"
                                meta:resourcekey="locAnaluzeMeasurementResource1"></asp:Localize>
                            <asp:Label ID="lblMeasurementName" Style="font-size: 12px !important;" runat="server"></asp:Label>
                        </div>                      
                        <div id="Div_Validation_Header" runat="server" visible="false">
                            <asp:HiddenField ID="hdnTotalRecord" runat="server" Value="0" />
                            <asp:Literal ID="ltrlRecordsSummary" runat="server" Text="Record(s) {start} to {end} of {total}"></asp:Literal>&nbsp;|&nbsp;<asp:ImageButton
                                ID="IBTN_FirstPage" runat="server" ImageUrl="~/Images/Nav_FirstPage_off.gif"
                                OnClick="IBTN_FirstPage_Click" />&nbsp;<asp:ImageButton ID="IBTN_PreviousPage" runat="server"
                                    ImageUrl="~/Images/Nav_PreviousPage_off.gif" OnClick="IBTN_PreviousPage_Click"
                                    Style="height: 13px" />&nbsp;<asp:ImageButton ID="IBTN_NextPage" runat="server" ImageUrl="~/Images/Nav_NextPage_off.gif"
                                        OnClick="IBTN_NextPage_Click" />&nbsp;<asp:ImageButton ID="IBTN_LastPage" runat="server"
                                            ImageUrl="~/Images/Nav_LastPage_off.gif" OnClick="IBTN_LastPage_Click" />&nbsp;|&nbsp;<asp:Localize
                                                runat="server" ID="locRecords" Text="Records" meta:resourcekey="locRecordsResource1"></asp:Localize>&nbsp;<asp:DropDownList
                                                    ID="DDL_PageSize" runat="server" CssClass="DROPDOWNLIST2" AutoPostBack="true"
                                                    OnSelectedIndexChanged="DDL_PageSize_SelectedIndexChanged">
                                                    <asp:ListItem Value="5" Selected="True">5</asp:ListItem>
                                                    <asp:ListItem Value="10">10</asp:ListItem>
                                                    <asp:ListItem Value="25">25</asp:ListItem>
                                                    <asp:ListItem Value="50">50</asp:ListItem>
                                                    <asp:ListItem Value="100">100</asp:ListItem>
                                                </asp:DropDownList>
                            &nbsp;|&nbsp;<asp:Localize Text="Go to page" runat="server" ID="locGoToPage" meta:resourcekey="locGoToPageResource1"></asp:Localize>&nbsp;<asp:DropDownList
                                ID="DDL_GoToPage" runat="server" CssClass="DROPDOWNLIST2" Width="45px" AutoPostBack="true"
                                OnSelectedIndexChanged="DDL_GoToPage_SelectedIndexChanged">
                            </asp:DropDownList>
                            &nbsp;
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
                                BackGradientStyle="HorizontalCenter" BackSecondaryColor="Silver" Palette="SemiTransparent"
                                BorderlineColor="176, 176, 176" BorderlineDashStyle="Solid">
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
                                        <AxisY TitleFont="Verdana, 8pt" IsLabelAutoFit="False">
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
    <%-- </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="rbtnListDeltaE" />
        </Triggers>
    </asp:UpdatePanel>--%>
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
        <div id="divToRender" runat="server">
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
                <li id="Tab_G7" runat="server"><span><a href="#tab6"><asp:Localize Text="G7" ID="locG7" runat="server" 
                        meta:resourcekey="locG7Resource1"></asp:Localize></a></span></li>                                    
                <li><span><a href="#tab3"><asp:Localize Text="Gamut" ID="locGray" runat="server" 
                        meta:resourcekey="locGrayResource1"></asp:Localize></a></span></li>  
                <li id="Tab_TVI" runat="server"><span><a href="#tab7"><asp:Localize Text="Report" ID="locTVI" runat="server" 
                        meta:resourcekey="locTVIResource1"></asp:Localize></a></span></li>                                
                <li id="Tab_PatchDetails" runat="server"><span><a href="#tab4"><asp:Localize Text="Patch Details" ID="locPatchDetails" 
                        runat="server" meta:resourcekey="locPatchDetailsResource1"></asp:Localize></a></span></li>  
                <li><span><a href="#tab5"><asp:Localize Text="Report" ID="locReport" runat="server" 
                        meta:resourcekey="locReportResource1"></asp:Localize></a></span></li>  
                
            </ul>
            <div style="float: right; padding-right: 3px; height: 26px; padding-top: 2px;">
                <asp:ImageButton ID="ibtnPrintPreview" runat="server" 
                    ImageUrl="../Images/DymoPrintPreviewIcon.png" ToolTip="Preview label" 
                    OnClientClick="ShowLabelPrintPreview(); return false;" 
                    meta:resourcekey="ibtnPrintPreviewResource1"  />
                <asp:ImageButton ID="ibtnPrintLabel" runat="server" 
                    ImageUrl="../Images/DymoPrintIcon.png" ToolTip="Print label" 
                    OnClientClick="PrintLabel(false); return false;" 
                    meta:resourcekey="ibtnPrintLabelResource1"  />
            </div>                
            <div class="clear"></div>
        </section>

    <div>
  <section id="tab1" class="contantbox tabcontant hideclass" >
        <div class="tab_top">&nbsp;</div>
        <div class="tab_middle">
           <div class="combopart">
        <div class="float_l" style="padding-left: 10px;" > 
           <asp:UpdatePanel ID="UpdatePanel5" runat="server">
              <ContentTemplate>                   
                  <table id="tbOptSummary" runat="server" cellpadding="0" cellspacing="0" width="100%">                                   
                                    <tr>
                                    <td>
                                         <asp:ListView ID="grdMeasurement" runat="server" ItemPlaceholderID="phItemPlaceholder" OnItemDataBound="grdMeasurement_ItemDataBound" >
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
                                                    <td style="font-size:13px; width:200px;"><asp:Localize ID="locMeasurementName" runat="server" meta:resourcekey="locMeasurementNameResource1" Text="Measurement Name:"></asp:Localize> </td>                                               
                                                    <td>
                                                        <asp:Label ID="lblMeasurementName" runat="server" 
                                                            Text='<%# Eval("MeasurementName") %>' 
                                                            ></asp:Label>
                                                    </td>
                                                </tr>
                                                 <tr class="White">   
                                                    <td style="font-size:13px;"><asp:Localize Text="MeasurementID:" ID="Localize1" runat="server" meta:resourcekey="locMeasurementIDResource1"></asp:Localize> </td>                                  
                                                    <td>
                                                         <asp:Label ID="lblMeasurementID" runat="server" 
                                                            Text='<%# Eval("MeasurementID") %>' 
                                                            ></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="Gray">   
                                                    <td style="font-size:13px;"><asp:Localize Text="Upload Date:" ID="locUploadDate" runat="server" meta:resourcekey="locUploadDateResource1"></asp:Localize> </td>                                  
                                                    <td>
                                                        <asp:Label ID="lblMeasurementDate" runat="server" 
                                                            Text='<%# string.Format("{0:d}",Eval("MeasurementDate")) %>' 
                                                           ></asp:Label>
                                                    </td>
                                                </tr>                                               
                                                <tr class="White">   
                                                    <td style="font-size:13px;"><asp:Localize Text="Average Δ" ID="locAverageDelta" runat="server" meta:resourcekey="locAverageDeltaResource1"></asp:Localize> 
                                                        <asp:Label ID="lblAvgΔE" runat="server"></asp:Label></td>                                  
                                                    <td>
                                                        <asp:Label ID="lblRAvg_ΔE" runat="server" Text='<%# Eval("Avg_ΔE") %>' 
                                                           ></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="Gray">   
                                                    <td style="font-size:13px;"><asp:Localize Text="Maximum Δ" ID="locMaximumDelta" 
                                                            runat="server" meta:resourcekey="locMaximumDeltaResource1"></asp:Localize>
                                                        <asp:Label ID="lblMaxΔE" runat="server"></asp:Label></td>                                  
                                                    <td>
                                                        <asp:Label ID="lblRMax_ΔE" runat="server" Text='<%# Eval("Max_ΔE") %>' 
                                                           ></asp:Label>
                                                    </td>
                                                </tr>    
                                                
                                                  <tr class="White">   
                                                    <td style="font-size:13px;"><asp:Localize Text="Maximum Primary Δ" runat="server" 
                                                            ID="locMaxPrimaryDelta" meta:resourcekey="locMaxPrimaryDeltaResource1"></asp:Localize>
                                                        <asp:Label ID="lblPriΔE" runat="server"></asp:Label></td>                                  
                                                    <td>
                                                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("Pri_ΔE") %>' 
                                                           ></asp:Label>
                                                    </td>
                                                </tr>    
                                                  <tr class="Gray">   
                                                    <td style="font-size:13px;"><asp:Localize Text="Maximum Primary ΔH:" 
                                                            ID="locMaxPrimaryDeltaH" runat="server" meta:resourcekey="locMaxPrimaryDeltaHResource1"
                                                            ></asp:Localize></td>                                  
                                                    <td>
                                                        <asp:Label ID="Label7" runat="server" Text='<%# Eval("Pri_ΔH") %>' 
                                                           ></asp:Label>
                                                    </td>
                                                </tr>    
                                                  <tr class="White">   
                                                    <td style="font-size:13px;"><asp:Localize Text="Gray Average Δ" 
                                                            ID="locGrayAvgDelta" runat="server" meta:resourcekey="locGrayAvgDeltaResource1"></asp:Localize>
                                                        <asp:Label ID="lblGrayΔE" runat="server" ></asp:Label></td>                                  
                                                    <td>
                                                        <asp:Label ID="Label9" runat="server" Text='<%# Eval("Gray_ΔE") %>' 
                                                            ></asp:Label>
                                                    </td>
                                                </tr>    
                                                                                        
                                                <tr class="Gray">   
                                                    <td style="font-size:13px;"><asp:Localize Text="10th Percentile Δ" 
                                                            ID="loc10PercentileDelta" runat="server" 
                                                            meta:resourcekey="loc10PercentileDeltaResource1"></asp:Localize>
                                                        <asp:Label ID="lblPercΔE10" runat="server" 
                                                           ></asp:Label> </td>                                                                                     
                                                    <td>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("PercΔE_10") %>' 
                                                            ></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="White">   
                                                    <td style="font-size:13px;"><asp:Localize Text="90th Percentile Δ" 
                                                            ID="loc90PercentileDelta" runat="server" 
                                                            meta:resourcekey="loc90PercentileDeltaResource1"></asp:Localize>
                                                        <asp:Label ID="lblPercΔE90" runat="server" 
                                                            ></asp:Label> </td>                                                                                    
                                                    <td>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("PercΔE_90") %>' 
                                                            ></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="Gray">   
                                                    <td style="font-size:13px;"><asp:Localize Text="95th Percentile Δ" 
                                                            ID="loc95PercentileDelta" runat="server" 
                                                            meta:resourcekey="loc95PercentileDeltaResource1"></asp:Localize>
                                                        <asp:Label ID="lblPercΔE95" runat="server" 
                                                            ></asp:Label> </td>                                                                                    
                                                    <td>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("PercΔE_95") %>' 
                                                            ></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </td>
                                    </tr>
                                </table>
                             
                  <table id="tbValidationSummary" runat="server" cellpadding="0" cellspacing="0" width="650px">
                        <tr>
                            <td valign="top" style="width:63%; border:1px Solid Gray;">
                                <asp:DetailsView ID="dvValidationSummary" runat="server" Width="100%" 
                                    AutoGenerateRows="False" GridLines="None" CssClass="Validation_Summary"
                                    meta:resourcekey="dvValidationSummaryResource1">
                                     <EmptyDataTemplate>
                THIS IS EMPTY
            </EmptyDataTemplate>
                                    <Fields>
                                        <asp:BoundField DataField="MeasurementName" HeaderText="Measurement Name:"
                                            HeaderStyle-Width="150px" HeaderStyle-CssClass="Summary_Left" 
                                            ItemStyle-CssClass="Summary_Right">                                            
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Status:" HeaderStyle-CssClass="Summary_Left_Alt" 
                                            ItemStyle-CssClass="Summary_Right_Alt" 
                                            meta:resourcekey="TemplateFieldResource1">
                                            <ItemTemplate>
                                                <div class='<%# ((string)Eval("MeasurementStatus") == "OK") ? "Validation_Status_Green" : "Validation_Status_Red" %>'>
                                                    <asp:Label runat="server" ID="lblStatus" 
                                                        Text='<%# Eval("MeasurementStatus") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField> 
                                          
                                        <asp:BoundField DataField="MetricTargetPresetName" HeaderText="Metric Preset:" 
                                            HeaderStyle-CssClass="Summary_Left" ItemStyle-CssClass="Summary_Right" 
                                            meta:resourcekey="BoundFieldResource2"> 
                                        </asp:BoundField>

                                        <asp:BoundField DataField="PrintStandardType" HeaderText="Print Standard:" 
                                            HeaderStyle-CssClass="Summary_Left_Alt" ItemStyle-CssClass="Summary_Right_Alt" 
                                            meta:resourcekey="BoundFieldResource3">   
                                        </asp:BoundField>

                                        <asp:BoundField DataField="MeasurementID" HeaderText="Measurement ID:"  
                                            HeaderStyle-CssClass="Summary_Left" ItemStyle-CssClass="Summary_Right" 
                                            meta:resourcekey="BoundFieldResource4">   
                                        </asp:BoundField>

                                        <asp:BoundField DataField="MeasurementDate" HeaderText="Timestamp:"  
                                            HeaderStyle-CssClass="Summary_Left_Alt" ItemStyle-CssClass="Summary_Right_Alt" 
                                            meta:resourcekey="BoundFieldResource5">
                                        </asp:BoundField>

                                        <asp:BoundField DataField="Customer" HeaderText="Customer:" 
                                            HeaderStyle-CssClass="Summary_Left" ItemStyle-CssClass="Summary_Right" 
                                            meta:resourcekey="BoundFieldResource6">   
                                        </asp:BoundField>

                                        <asp:BoundField DataField="dEMethod" HeaderText="ΔE Type:" 
                                            HeaderStyle-CssClass="Summary_Left_Alt" ItemStyle-CssClass="Summary_Right_Alt" 
                                            meta:resourcekey="BoundFieldResource7">
                                        </asp:BoundField>

                                        <asp:BoundField DataField="PageOrSurface" HeaderText="Page/Surface:" 
                                            HeaderStyle-CssClass="Summary_Left" ItemStyle-CssClass="Summary_Right" 
                                            meta:resourcekey="BoundFieldResource8">
                                        </asp:BoundField>

                                        <asp:BoundField DataField="JDFJobId" HeaderText="JDF Job ID:" 
                                            HeaderStyle-CssClass="Summary_Left_Alt" ItemStyle-CssClass="Summary_Right_Alt" 
                                            meta:resourcekey="BoundFieldResource9">
                                        </asp:BoundField>

                                        <asp:BoundField DataField="Notes" HeaderText="Notes:"  
                                            HeaderStyle-CssClass="Summary_Left" ItemStyle-CssClass="Summary_Right" 
                                            meta:resourcekey="BoundFieldResource10">  
                                        </asp:BoundField>
                                    </Fields>
                                </asp:DetailsView>
                            </td>
                            <td style="width:2%;"></td>
                            <td valign="top" style="width:35%; ">                              
                                <telerik:RadGrid ID="rgValidationSummaryMetrics" runat="server" 
                                    AutoGenerateColumns="False" EnableLinqExpressions="False"                     
                                    OnItemDataBound="rgValidationSummaryMetrics_ItemDataBound" CellSpacing="0" 
                                    GridLines="None" meta:resourcekey="rgValidationSummaryMetricsResource1">   
                                        <HeaderStyle Font-Bold="True" />  
                                        <ItemStyle BackColor="#F2F2F2" />
                                        <AlternatingItemStyle BackColor="#FFFFFF" />                    
                                            <MasterTableView DataKeyNames="MetricMeasuredID">  

                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="Metric" HeaderText="Metric" 
                                                        UniqueName="MetricSumName" meta:resourcekey="GridBoundColumnResource18">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Tolerance" HeaderText="Tol" 
                                                        UniqueName="TargetValue" meta:resourcekey="GridBoundColumnResource19">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Actual" HeaderText="Actual" 
                                                        UniqueName="MeasuredValue" meta:resourcekey="GridBoundColumnResource20">
                                                    </telerik:GridBoundColumn>     
                                                    <telerik:GridBoundColumn DataField="IsNotOK" HeaderText="Is Not OK" 
                                                        UniqueName="IsNotOK" Display="false"
                                                        meta:resourcekey="GridBoundColumnResource21">
                                                    </telerik:GridBoundColumn>                                               
                                                </Columns>                        


                                            </MasterTableView>                        

                                </telerik:RadGrid>                                
                            </td>
                        </tr>
                    </table>               
              </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="chartAnalyze" />  
                        <asp:PostBackTrigger ControlID="rbtnListDeltaE" />  
                        <asp:PostBackTrigger ControlID="chkAnalyzeAverageOfMeasurements" />  
                    </Triggers>  
           </asp:UpdatePanel>
        </div>
        <div class="clear"></div>
        </div>  
    </div>    
    <div class="tab_bottom">
        &nbsp;</div>
    </section>  
</div>

    <div>
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
                                        &nbsp; &Delta;<asp:Label ID="lblEPryPatches" runat="server" 
                                            ></asp:Label>
                                        <asp:Localize Text=" Primary Patches" runat="server"
                                            meta:resourcekey="LocalizeResource1" ></asp:Localize>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 40%;" valign="top">
                                        <table cellpadding="0" cellspacing="0" width="100%">                          
                                            <tr>
                                                <td colspan="2" style="height: 40px;" class="SyncBottomBorder">
                                                </td>
                                            </tr>
                                            <asp:Literal ID="LitDeltaEPatches" runat="server" 
                                                ></asp:Literal>
                                        </table>
                                    </td>
                                    <td style="width: 60%; " id="TDEPrimary" runat="server">
                                        <asp:Chart ID="chartDeltaE" runat="server" Width="210px" Height="362px" 
                                            BackColor="Transparent" meta:resourcekey="chartDeltaEResource1">                  
                                                            <Series>
                                                                <asp:Series Name="Series0"  Color="212, 232, 250" ChartArea="ChartArea1" 
                                                                    ChartType="Bar">                                              
                                                                </asp:Series>
                                                                <asp:Series Name="Series1" BorderWidth="0" BorderColor="Gray" Color="Gray" 
                                                                                            IsVisibleInLegend="False" ChartArea="ChartArea1" ChartType="Bar">
                                                                </asp:Series>   
                                                                 <asp:Series Name="Series2" BorderWidth="0"  Color="Transparent" 
                                                                        IsVisibleInLegend="False" ChartArea="ChartArea1" ChartType="Bar" MarkerImage="~/Images/Chart-Red-Image-Vertical.png">
                                                                </asp:Series> 
                                                            </Series>
                                                            <ChartAreas>
                                                                <asp:ChartArea Name="ChartArea1" BackColor="Transparent">
                                                                      <Area3DStyle  Enable3D="True" Inclination="0" Rotation="0" >
                                                                      </Area3DStyle>
                                          
                                                                    <AxisX  Enabled="True"  IsReversed="True" >
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
                         <%-- <div id="Div_Density" runat="server" visible="false">--%>
                            <table id="Table_Density" runat="server" visible="false" border="0" cellpadding="0" cellspacing="0" width="100%" >                                                
                                 <tr>
                                    <td class="green-title">
                                        &nbsp; <asp:Localize Text="Density" runat="server" ID="locDensity" 
                                            meta:resourcekey="locDensityResource1"></asp:Localize>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                                          
                                            <tr>
                                                <td>
                                                    <asp:Chart ID="CHRT_SummaryDensity" ImageType="Jpeg" runat="server" Width="380px"
                                                        Height="175px" ImageUrl="~/UploadFiles/temp/CHRT_DensityDeviation" BackGradientType="Center"
                                                        BackGradientEndColor="#FFFFFF" BorderLineColor="#808080"
                                                        EnableViewState="false">
                                                        <Series>
                                                            <asp:Series Name="Series0" BackGradientStyle="DiagonalRight" BorderColor="26, 59, 105"
                                                                BorderWidth="0" Color="Transparent" ShadowOffset="70" Font="Microsoft Sans Serif, 7pt, style=Underline"
                                                                Label="" IsVisibleInLegend="true" ChartType="Column">
                                                            </asp:Series>
                                                            <asp:Series Name="Series1" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="1"
                                                                IsVisibleInLegend="false" ChartType="Column">
                                                            </asp:Series>
                                                            <asp:Series Name="Series2" BorderWidth="0" Color="Transparent"
                                                                IsVisibleInLegend="false" MarkerImage="~/Images/green.png">
                                                            </asp:Series>                                                             
                                                        </Series>
                                                        <BorderSkin SkinStyle="None"></BorderSkin>                                                       
                                                        <ChartAreas>
                                                            <asp:ChartArea Name="ChartArea1" BorderColor="26, 59, 105" BackSecondaryColor="White"
                                                                BackColor="Gray" BackGradientStyle="TopBottom" BorderDashStyle="Solid" ShadowOffset="1">
                                                                <Area3DStyle  Enable3D="True" Inclination="0" Rotation="0"  IsRightAngleAxes="true" IsClustered="false" WallWidth="0"  LightStyle="Realistic">
                                                                </Area3DStyle>                                                           
                                                                <AxisY Enabled="True">
                                                                    <MajorGrid LineColor="Beige" />                                                                   
                                                                </AxisY>
                                                                <AxisX Enabled="True" Interval="Auto">
                                                                    <MajorGrid LineColor="Beige" />                                                                   
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
                         <%-- </div>--%>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%" >    

                                <tr>
                                    <td class="green-title">
                                        &nbsp; &#916; <asp:Localize Text="Paper L* a* b*" runat="server" ID="locPaper" 
                                            meta:resourcekey="locPaperResource1"></asp:Localize>
                                    </td>
                                </tr>
                                <tr>                                  
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr><td></td></tr>
                                                    <tr>
                                                        <td align="center" style="background:  url(images/BGISOPaperGraph1_Sync4D.jpg) no-repeat center; width: 276px;
                                                                                height: 180px; padding-left: 20px; padding-bottom: 14px;">
                                                                                
                                                            <asp:Chart ID="chartPaper1" runat="server" BackImageMode="Scaled" BackImage="~/Images/ISOPaperGraph1_Sync4D.jpg"
                                                                ImageType="Jpeg"  Width="240px" Height="130px" 
                                                                meta:resourcekey="chartPaper1Resource1">
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
                                                   
                                                        <td align="center" style="background: url(images/BGISOPaperGraph2_Sync4D.jpg) no-repeat center; width: 104px;
                                                                                height: 169px; padding-left: 22px; padding-bottom: 12px;" >


                                                                <asp:Chart ID="chartPaper2" runat="server" BackImageMode="Scaled" BackImage="~/Images/ISOPaperGraph2_Sync4D.jpg"
                                                                ImageType="Jpeg" Width="69px" Height="130px" 
                                                                    meta:resourcekey="chartPaper2Resource1">
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
                        <asp:PostBackTrigger ControlID="rbtnListDeltaE" />  
                        <asp:PostBackTrigger ControlID="chkAnalyzeAverageOfMeasurements" />  
                        
                    </Triggers>  
            </asp:UpdatePanel>
        </div>
        <div class="clear"></div>
        </div>                            	
        </div>
        <div class="tab_bottom">&nbsp;</div>
  </section>
  </div>
    
    <div id="dvTab_G7" runat="server">
   <section id="tab6" class="contantbox tabcontant hideclass">
      <div class="tab_top">&nbsp;</div>
        <div class="tab_middle">
           <div class="combopart">
        <div class="float_l" style="padding-left: 10px;" > 
          <asp:UpdatePanel ID="UpdatePanel7" runat="server">
              <ContentTemplate>         
           
                    <table cellpadding="0" cellspacing="0" width="650px">
                         <tr>
                            <td valign="top" style="width: 59%">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                                    <tr>
                                        <td class="green-title">
                                            &nbsp; <asp:Localize Text="Gray Balance ΔCh" runat="server" 
                                                ID="locGrayBalanceDeltaCH" meta:resourcekey="locGrayBalanceDeltaCHResource1"></asp:Localize>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                                <asp:Chart ID="Chart_GrayF" runat="server" Width="400px"  Height="105px" BackGradientType="Center" 
                                                BackGradientEndColor="White" BorderLineColor="#808080" EnableViewState="false">
                                                <Legends>
                                                    <asp:Legend Name="Default" Enabled="False">
                                                    </asp:Legend>
                                                </Legends>
                                                <BorderSkin SkinStyle="None"></BorderSkin>                                               
                                                <Series>
                                                    <asp:Series Name="Series1" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                                        Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                        MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                                    </asp:Series>                                                         
                                                    <asp:Series Name="Series1Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                                        Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                        MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                                    </asp:Series>  
                                                    <asp:Series  Name="Series2" MarkerSize="12" BackSecondaryColor="162, 164, 255" IsVisibleInLegend="false" Color="Red" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                                                    BorderWidth="2">
                                                    </asp:Series>  
                                                    <asp:Series Name="Series2Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                                            Color="Red" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                                BackSecondaryColor="162, 164, 255">
                                                    </asp:Series>                                                       
                                                </Series>
                                                <ChartAreas>
                                                    <asp:ChartArea Name="Default" BorderColor="26, 59, 105" BackSecondaryColor="White"
                                                        BackColor="Gray" BackGradientStyle="TopBottom" BorderDashStyle="Solid" ShadowOffset="1">
                                                        <Position Height="99" Width="94" X="0" Y="2" />
                                                        <AxisY IsLabelAutoFit="False" Title="" TitleFont="Verdana, 8pt, style=Bold" Interval="2"
                                                            Minimum="0" Maximum="6">
                                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                                            <LabelStyle Angle="0" Font="Verdana, 6pt" />
                                                        </AxisY>
                                                        <AxisX IsLabelAutoFit="False" IsMarginVisible="False" Interval="25" Title="" Minimum="0" Maximum="100">
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
                            <td style="width: 2%;">
                            </td>
                            <td style="width: 39%" valign="top">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                                    <tr>
                                        <td class="green-title">
                                            &nbsp; <asp:Localize Text="Gray  ΔCh Summary" runat="server" 
                                                ID="locGrayDeltaCHSummary" meta:resourcekey="locGrayDeltaCHSummaryResource1"></asp:Localize>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" style="padding: 2px">
                                            <telerik:RadGrid ID="RG_ΔChSummary" runat="server" AutoGenerateColumns="False" EnableLinqExpressions="False"                     
                                                OnItemDataBound="RG_ΔChSummary_ItemDataBound" CellSpacing="0" 
                                                GridLines="None" meta:resourcekey="RG_ΔChSummaryResource1">   
                                                    <HeaderStyle Font-Bold="True" />  
                                                    <ItemStyle BackColor="#F2F2F2" />
                                                    <AlternatingItemStyle BackColor="#FFFFFF" />                    
                                                        <MasterTableView DataKeyNames="MetricMeasuredID">  

                                                            <Columns>
                                                                <telerik:GridBoundColumn DataField="Metric" HeaderText="Metric" 
                                                                    UniqueName="MetricSumName" meta:resourcekey="GridBoundColumnResource22">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="Tolerance" HeaderText="Tolerance" 
                                                                    UniqueName="TargetValue" meta:resourcekey="GridBoundColumnResource23">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="Actual" HeaderText="Actual" 
                                                                    UniqueName="MeasuredValue" meta:resourcekey="GridBoundColumnResource24">
                                                                </telerik:GridBoundColumn>     
                                                                <telerik:GridBoundColumn DataField="IsNotOK" HeaderText="Is Not OK" 
                                                                    UniqueName="IsNotOK" Display="false"
                                                                    meta:resourcekey="GridBoundColumnResource25">
                                                                </telerik:GridBoundColumn>                                               
                                                            </Columns>                        

                                                        </MasterTableView>                        


                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </table>
                            </td>                                
                        </tr>
                    </table>
                    
                    <table cellpadding="0" cellspacing="0" width="650px">
                        <tr>
                        <td valign="top" style="width: 59%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                                <tr>
                                    <td class="green-title">
                                        &nbsp; <asp:Localize Text="Black ΔL" runat="server" ID="locBlackDeltaL" 
                                            meta:resourcekey="locBlackDeltaLResource1"></asp:Localize>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                            <asp:Chart ID="Chart_GrayL" runat="server" Width="400px"  Height="105px" BackGradientType="Center" 
                                            BackGradientEndColor="White" BorderLineColor="#808080" EnableViewState="false">
                                            <Legends>
                                                <asp:Legend Name="Default" Enabled="False">
                                                </asp:Legend>
                                            </Legends>
                                            <BorderSkin SkinStyle="None"></BorderSkin>                                               
                                            <Series>
                                                <asp:Series Name="Series1" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                                </asp:Series>                                                         
                                                <asp:Series Name="Series1Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                                </asp:Series>  
                                                <asp:Series  Name="Series2" MarkerSize="12" BackSecondaryColor="162, 164, 255" IsVisibleInLegend="false" Color="Red" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                                                BorderWidth="2">
                                                </asp:Series>  
                                                <asp:Series Name="Series2Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                                        Color="Red" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                            BackSecondaryColor="162, 164, 255">
                                                </asp:Series>                                                       
                                            </Series>
                                            <ChartAreas>
                                                <asp:ChartArea Name="Default" BorderColor="26, 59, 105" BackSecondaryColor="White"
                                                    BackColor="Gray" BackGradientStyle="TopBottom" BorderDashStyle="Solid" ShadowOffset="1">
                                                    <Position Height="99" Width="94" X="0" Y="2" />
                                                    <AxisY IsLabelAutoFit="False" Title="" TitleFont="Verdana, 8pt, style=Bold" Interval="2"
                                                        Minimum="0" Maximum="6">
                                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                                        <LabelStyle Angle="0" Font="Verdana, 6pt" />
                                                    </AxisY>
                                                    <AxisX IsLabelAutoFit="False" IsMarginVisible="False" Interval="25" Title="" Minimum="0" Maximum="100">
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
                        <td style="width: 2%;">
                        </td>
                        <td style="width: 39%" valign="top">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                                    <tr>
                                        <td class="green-title">
                                            &nbsp; <asp:Localize Text="Black  ΔL Summary" runat="server" 
                                                ID="locBlackDeltaLSummary" meta:resourcekey="locBlackDeltaLSummaryResource1"></asp:Localize>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" style="padding: 2px">
                                            <telerik:RadGrid ID="RG_ΔLBlackSummary" runat="server" 
                                                AutoGenerateColumns="False" EnableLinqExpressions="False"                     
                                                OnItemDataBound="RG_ΔLBlackSummary_ItemDataBound" CellSpacing="0" 
                                                GridLines="None" meta:resourcekey="RG_ΔLBlackSummaryResource1">   
                                                    <HeaderStyle Font-Bold="True" />  
                                                    <ItemStyle BackColor="#F2F2F2" />
                                                    <AlternatingItemStyle BackColor="#FFFFFF" />                    
                                                        <MasterTableView DataKeyNames="MetricMeasuredID">  

                                                            <Columns>
                                                                <telerik:GridBoundColumn DataField="Metric" HeaderText="Metric" 
                                                                    UniqueName="MetricSumName" meta:resourcekey="GridBoundColumnResource26">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="Tolerance" HeaderText="Tolerance" 
                                                                    UniqueName="TargetValue" meta:resourcekey="GridBoundColumnResource27">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="Actual" HeaderText="Actual" 
                                                                    UniqueName="MeasuredValue" meta:resourcekey="GridBoundColumnResource28">
                                                                </telerik:GridBoundColumn>     
                                                                <telerik:GridBoundColumn DataField="IsNotOK" HeaderText="Is Not OK" 
                                                                    UniqueName="IsNotOK" Display="false"
                                                                    meta:resourcekey="GridBoundColumnResource29">
                                                                </telerik:GridBoundColumn>                                               
                                                            </Columns>                        


                                                        </MasterTableView>                        

<FilterMenu EnableImageSprites="False"></FilterMenu>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </table>
                        </td>                                
                    </tr>
                </table>

                    <table cellpadding="0" cellspacing="0" width="650px">
                        <tr>
                        <td valign="top" style="width: 59%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                                <tr>
                                    <td class="green-title">
                                        &nbsp; <asp:Localize runat="server" Text="Gray ΔL" ID="locGrayDeltaL" 
                                            meta:resourcekey="locGrayDeltaLResource1"></asp:Localize>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                            <asp:Chart ID="Chart_BlackL" runat="server" Width="400px"  Height="105px" BackGradientType="Center" 
                                            BackGradientEndColor="White" BorderLineColor="#808080" EnableViewState="false">
                                            <Legends>
                                                <asp:Legend Name="Default" Enabled="False">
                                                </asp:Legend>
                                            </Legends>
                                            <BorderSkin SkinStyle="None"></BorderSkin>                                               
                                            <Series>
                                                <asp:Series Name="Series1" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                                </asp:Series>                                                         
                                                <asp:Series Name="Series1Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                                </asp:Series>  
                                                <asp:Series  Name="Series2" MarkerSize="12" BackSecondaryColor="162, 164, 255" IsVisibleInLegend="false" Color="Red" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                                                BorderWidth="2">
                                                </asp:Series>  
                                                <asp:Series Name="Series2Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                                        Color="Red" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                            BackSecondaryColor="162, 164, 255">
                                                </asp:Series>                                                       
                                            </Series>
                                            <ChartAreas>
                                                <asp:ChartArea Name="Default" BorderColor="26, 59, 105" BackSecondaryColor="White"
                                                    BackColor="Gray" BackGradientStyle="TopBottom" BorderDashStyle="Solid" ShadowOffset="1">
                                                    <Position Height="99" Width="94" X="0" Y="2" />
                                                    <AxisY IsLabelAutoFit="False" Title="" TitleFont="Verdana, 8pt, style=Bold" Interval="2"
                                                        Minimum="0" Maximum="6">
                                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                                        <LabelStyle Angle="0" Font="Verdana, 6pt" />
                                                    </AxisY>
                                                    <AxisX IsLabelAutoFit="False" IsMarginVisible="False" Interval="25" Title="" Minimum="0" Maximum="100">
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
                        <td style="width: 2%;">
                        </td>
                        <td style="width: 39%" valign="top">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                                    <tr>
                                        <td class="green-title">
                                            &nbsp; <asp:Localize runat="server" Text="Gray  ΔL Summary" 
                                                ID="locGrayDeltaLSummary" meta:resourcekey="locGrayDeltaLSummaryResource1"></asp:Localize>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" style="padding: 2px">
                                            <telerik:RadGrid ID="RG_ΔLGraySummary" runat="server" 
                                                AutoGenerateColumns="False" EnableLinqExpressions="False"                     
                                                OnItemDataBound="RG_ΔLGraySummary_ItemDataBound" CellSpacing="0" 
                                                GridLines="None" meta:resourcekey="RG_ΔLGraySummaryResource1">   
                                                    <HeaderStyle Font-Bold="True" />  
                                                    <ItemStyle BackColor="#F2F2F2" />
                                                    <AlternatingItemStyle BackColor="#FFFFFF" />                    
                                                        <MasterTableView DataKeyNames="MetricMeasuredID">  

                                                            <Columns>
                                                                <telerik:GridBoundColumn DataField="Metric" HeaderText="Metric" 
                                                                    UniqueName="MetricSumName" meta:resourcekey="GridBoundColumnResource30">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="Tolerance" HeaderText="Tolerance" 
                                                                    UniqueName="TargetValue" meta:resourcekey="GridBoundColumnResource31">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="Actual" HeaderText="Actual" 
                                                                    UniqueName="MeasuredValue" meta:resourcekey="GridBoundColumnResource32">
                                                                </telerik:GridBoundColumn>     
                                                                <telerik:GridBoundColumn DataField="IsNotOK" HeaderText="Is Not OK" 
                                                                    UniqueName="IsNotOK" Display="false"
                                                                    meta:resourcekey="GridBoundColumnResource33">
                                                                </telerik:GridBoundColumn>                                               
                                                            </Columns>                        


                                                        </MasterTableView>                        


                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </table>
                        </td>                                
                    </tr>
                </table>                   

                    <%--<table cellpadding="0" cellspacing="0" width="650px">
                        <tr>
                            <td style="width: 49%">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                                <tr>
                                    <td class="green-title">
                                        &nbsp; Black NPD
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <asp:Chart ID="CHRT_BlackNPD" runat="server" Width="320px" Height="250px" ImageType="Jpeg"
                                            ImageUrl="~/UploadFiles/temp/GrayNPD" BackGradientType="Center" 
                                            BackGradientEndColor="White" BorderLineColor="#808080" EnableViewState="false">
                                            <Legends>
                                                <asp:Legend Name="Default" Enabled="false">
                                                </asp:Legend>
                                            </Legends>
                                            <BorderSkin SkinStyle="None"></BorderSkin>
                                           
                                            <Series>
                                                <asp:Series Name="Series0" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                                    Color="Black" BorderWidth="3" ChartType="Spline" MarkerBorderColor="Black" BackSecondaryColor="162, 164, 255">
                                                </asp:Series>
                                                <asp:Series Name="Series0Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                                    Color="Black" BorderWidth="3" ChartType="Spline" MarkerBorderColor="Black" BackSecondaryColor="162, 164, 255">
                                                </asp:Series>                                               
                                            </Series>
                                            <ChartAreas>
                                                <asp:ChartArea Name="Default" BorderColor="26, 59, 105" BackSecondaryColor="White"
                                                    BackColor="Gray" BackGradientStyle="TopBottom" BorderDashStyle="Solid" ShadowOffset="1">
                                                    <Position Height="99" Width="94" X="0" Y="2" />
                                                    <AxisY IsLabelAutoFit="False" Title="" TitleFont="Verdana, 8pt, style=Bold" Interval="Auto">
                                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                                        <LabelStyle Angle="0" Font="Verdana, 6pt" />
                                                    </AxisY>
                                                    <AxisX IsLabelAutoFit="False" IsMarginVisible="False" Interval="25" Title="" Minimum="0" Maximum="100">
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
                            <td style="width: 2%;">
                            </td>
                            <td style="width: 49%;">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                                    <tr>
                                        <td class="green-title">
                                            &nbsp; Gray NPD
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Chart ID="CHRT_GrayNPD" runat="server" Width="320px" Height="250px" ImageType="Jpeg"
                                                ImageUrl="~/UploadFiles/temp/GrayNPD" BackGradientType="Center" 
                                                BackGradientEndColor="White" BorderLineColor="#808080" EnableViewState="false">
                                                <Legends>
                                                    <asp:Legend Name="Default" Enabled="false">
                                                    </asp:Legend>
                                                </Legends>
                                                <BorderSkin SkinStyle="None"></BorderSkin>
                                              
                                                <Series>
                                                    <asp:Series Name="Series0" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                                        Color="Black" BorderWidth="3" ChartType="Spline" MarkerBorderColor="Black" MarkerStyle="Circle"
                                                        MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                                    </asp:Series>                                            
                                                </Series>
                                                <ChartAreas>
                                                    <asp:ChartArea Name="Default" BorderColor="26, 59, 105" BackSecondaryColor="White"
                                                        BackColor="Gray" BackGradientStyle="TopBottom" BorderDashStyle="Solid" ShadowOffset="1">
                                                        <Position Height="99" Width="94" X="0" Y="2" />
                                                        <AxisY IsLabelAutoFit="False" Title="" TitleFont="Verdana, 8pt, style=Bold" Interval="Auto">
                                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                                            <LabelStyle Angle="0" Font="Verdana, 6pt" />
                                                        </AxisY>
                                                        <AxisX IsLabelAutoFit="False" IsMarginVisible="False" Interval="25" Title="" Minimum="0" Maximum="100">
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
                    </table>--%>
               
               </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="chartAnalyze" />   
                        <asp:PostBackTrigger ControlID="rbtnListDeltaE" />            
                        <asp:PostBackTrigger ControlID="chkAnalyzeAverageOfMeasurements" />            
                        
                    </Triggers>  
           </asp:UpdatePanel>       
        </div>
        <div class="clear"></div>
        </div>                            	
        </div>
        <div class="tab_bottom">&nbsp;</div>
   </section>
   </div>

    <div>
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
                                                                <asp:Localize ID="locComparison" runat="server" Text="a* - b* Comparison" 
                                                                    meta:resourcekey="locComparisonResource1"></asp:Localize>
                                                            </td>
                                                        </tr>
                                                      
                                                        <tr>
                                                            <td align="center" valign="top">                                                      
                                                                <asp:Chart ID="chartABComparison" runat="server" 
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
                                                                                 <MajorGrid LineColor="Gray"></MajorGrid>
                                                                                 <LabelStyle Font="Verdana, 6pt" />
                                                                            </AxisY>
                                                                            <AxisX LineColor="120, 0, 0, 0" Maximum="100" Minimum="-100" Interval="20" LineDashStyle="NotSet"  IsMarginVisible="False" TitleAlignment="Near" IsLabelAutoFit="False">                                                                             
                                                                                 <MajorGrid LineColor="Gray"></MajorGrid>
                                                                                 <LabelStyle Font="Verdana, 6pt" />
                                                                            </AxisX>
                                                                        </asp:ChartArea>
                                                                    </ChartAreas>
                                                                </asp:Chart>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                                <div style=" padding-left:-5px;" >
                                                                    <div id="accordion" class="header collapsibleContainerTitle">
                                                                        <div>
                                                                            <div class="green-title">
                                                                                <div style="float: left">
                                                                                </div>
                                                                                <div style="float: left;" >
                                                                                    <div id="imgHeader" class="expanded">
                                                                                        &nbsp;</div>
                                                                                    <asp:Localize ID="Localize2" runat="server" Text="a* - b* Gamut Comparison"></asp:Localize>
                                                                                </div>
                                                                                <div class="clear">
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                     <asp:Panel ID="panelabGamutComparison" runat="server" BorderStyle="None"
                                                                        Width="100%" meta:resourcekey="panelabGamutComparisonResource1">                                                                        
                                                                            <asp:chart ID="chartABComparison_1" runat="server"  Width="280px" Height="250px" BackGradientType="Center" 
                                                                                    BackGradientEndColor="White" BorderLineColor="#808080"  EnableViewState="false">
                                                                                        <Legends>
                                                                                                <asp:Legend Name="Default" Enabled="False">
                                                                                            </asp:Legend>
                                                                                        </Legends>
                                                                                        <Series>
                                                                                            <asp:Series Name="Series0" ChartType="Polar" 
                                                                                                MarkerSize="10" MarkerStyle="Circle" MarkerColor="Green" Color="Green" ShadowOffset="1">
                                                                                            </asp:Series>
                                                                                            <asp:Series Name="Series1" ChartType="Polar" 
                                                                                                MarkerSize="10" MarkerStyle="Circle" MarkerColor="Green" MarkerBorderColor="Black"
                                                                                                MarkerBorderWidth="1" Color="Green" ShadowOffset="1">
                                                                                            </asp:Series>
                                                                                            <asp:Series Name="Series2" ChartType="FastPoint" 
                                                                                                MarkerSize="10" MarkerStyle="Circle" MarkerColor="Green" Color="Red" ShadowOffset="1">
                                                                                            </asp:Series>
                                                                                            <asp:Series Name="Series3" ChartType="FastPoint" 
                                                                                                MarkerSize="10" MarkerStyle="Circle" MarkerBorderColor="Black" ShadowOffset="1">
                                                                                            </asp:Series>
                                                                                            <asp:Series Name="Series4" ChartType="FastPoint" 
                                                                                                MarkerSize="4" MarkerStyle="Circle" MarkerBorderColor="Black" ShadowOffset="1">
                                                                                            </asp:Series>
                                                                                        </Series>
                                                                                        <BorderSkin PageColor="AliceBlue" />
                                                                                            <Titles>
                                                                                            <asp:Title ForeColor="Black" Text="a*">
                                                                                                <Position X="15" Y="42" />
                                                                                            </asp:Title>
                                                                                            <asp:Title ForeColor="Black" Text="b*">
                                                                                                <Position X="57" Y="87" />
                                                                                            </asp:Title>
                                                                                        </Titles>
                                                                                            <ChartAreas>
                                                                                            <asp:ChartArea  BackSecondaryColor="#FACECF" BackColor="#B3DEC3" BackGradientStyle="LeftRight" BorderColor="26, 59, 105" BorderDashStyle="Solid" Name="Default"  ShadowOffset="1">
                                                                                                <Position Height="99" Width="100" X="0" Y="0" />
                                                                                                <AxisY LineColor="120, 0, 0, 0"  Maximum="100" Minimum="-100" Interval="20"  IsMarginVisible="False" TitleAlignment="Near" IsLabelAutoFit="False"
                                                                                                    LineDashStyle="NotSet">                                                                           
                                                                                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                                                                                        <LabelStyle Font="Verdana, 6pt" />
                                                                                                </AxisY>
                                                                                                <AxisX LineColor="120, 0, 0, 0" Maximum="100" Minimum="-100" Interval="20" LineDashStyle="NotSet"  IsMarginVisible="False" TitleAlignment="Near" IsLabelAutoFit="False">                                                                             
                                                                                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                                                                                        <LabelStyle Font="Verdana, 6pt" />
                                                                                                </AxisX>
                                                                                            </asp:ChartArea>
                                                                                        </ChartAreas>
                                                                                    </asp:chart>                                                                              
                                                                    </asp:Panel>
                                                                    </div>
                                                            </td>
                                                        </tr>                                                      
                                                        <tr runat="server">
                                                            <td class="green-title" runat="server">
                                                                <asp:Localize runat="server" Text="L* Comparison" ID="locLComparison" 
                                                                    meta:resourcekey="locLComparisonResource1"></asp:Localize>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server">
                                                            <td align="center" valign="top" runat="server">                                                            
                                                                <asp:Chart ID="chartLComparison" runat="server" BackGradientType="Center"
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
                                                                
                                                                                <asp:Chart ID="chartL" runat="server" Width="365px" Height="125px" 
                                                                                        BackGradientType="Center"
                                                                                        BackGradientEndColor="White" BorderLineColor="Gray" 
                                                                                    meta:resourcekey="chartLResource1">
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

                                                                                 <asp:Chart ID="chartA" runat="server" Width="365px" Height="125px" BackGradientType="Center" 
                                                                                    BackGradientEndColor="White" BorderLineColor="Gray" 
                                                                                     meta:resourcekey="chartAResource1">
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
                                                                        
                                                                                 <asp:Chart ID="chartB" runat="server" Width="365px" Height="125px" BackGradientType="Center" 
                                                                                    BackGradientEndColor="White" BorderLineColor="Gray" 
                                                                                     meta:resourcekey="chartBResource1">
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
                    </Triggers>  
           </asp:UpdatePanel>

        </div>
        <div class="clear"></div>
        </div>                            	
        </div>
        <div class="tab_bottom">&nbsp;</div>
  </section>
  </div>

    <div id="dvTab_TVI" runat="server">
   <section id="tab7" class="contantbox tabcontant hideclass">
      <div class="tab_top">&nbsp;</div>
        <div class="tab_middle">
           <div class="combopart">
        <div class="float_l" style="padding-left: 10px;" > 
          <asp:UpdatePanel ID="UpdatePanel_TVI_G7" runat="server">
              <ContentTemplate>         
           
                    <table cellpadding="0" cellspacing="0" width="650px" >
                         <tr>
                            <td valign="top" style="width: 59%">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                                   
                                    <tr>
                                        <td valign="top">

                                            <asp:Chart ID="CHRT_SummaryDotgain" runat="server" Width="650px" Height="360px"
                                            ImageType="Jpeg" ImageUrl="~/UploadFiles/temp/CHRT_SummaryTVI" BackGradientType="Center"
                                            BackGradientEndColor="White" BorderLineColor="#808080"
                                            EnableViewState="false">
                                            <Series>
                                                <asp:Series Name="Series1" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                                    Color="Cyan" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                                </asp:Series>
                                                <asp:Series Name="Series1Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                                    Color="Cyan" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                                </asp:Series>
                                                <asp:Series Name="Series2" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                                    Color="Magenta" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                                </asp:Series>
                                                 <asp:Series Name="Series2Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                                    Color="Magenta" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                                </asp:Series>
                                                <asp:Series Name="Series3" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                                    Color="Yellow" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                                </asp:Series>
                                                <asp:Series Name="Series3Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                                    Color="Yellow" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                                </asp:Series>
                                                <asp:Series Name="Series4" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                                </asp:Series>
                                                <asp:Series Name="Series4Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                                </asp:Series>
                                            </Series>
                                            <BorderSkin SkinStyle="None"></BorderSkin>                                            
                                            <ChartAreas>
                                                <asp:ChartArea Name="Default" BorderColor="26, 59, 105" BackSecondaryColor="White"
                                                    BackColor="Gray" BackGradientStyle="TopBottom" BorderDashStyle="Solid" ShadowOffset="1">
                                                    <Position Height="99" Width="94" X="0" Y="2" />
                                                    <AxisY IsLabelAutoFit="False" Title="" Interval="Auto">
                                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                                    </AxisY>
                                                    <AxisX IsLabelAutoFit="False" IsMarginVisible="False" Interval="25" Title="">
                                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                                        <LabelStyle Angle="0" />
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
                 
               </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="chartAnalyze" />   
                        <asp:PostBackTrigger ControlID="rbtnListDeltaE" />
                        <asp:PostBackTrigger ControlID="chkAnalyzeAverageOfMeasurements" />
                                    
                    </Triggers>  
           </asp:UpdatePanel>    
          
          <asp:UpdatePanel ID="UpdatePanel_TVI_ISO" runat="server">
              <ContentTemplate>    
              
                <table border="0" cellpadding="0" cellspacing="0" width="650px">
                    <tr>
                        <td>
                            <asp:Button ID="btnTVI" runat="server" Text="TVI" OnClientClick="funTVIVisiblity(); return false;" />
                            &nbsp;<asp:Button ID="btnSpread" OnClientClick="funSpreadVisiblity(); return false;"
                                runat="server" Text="Spread" />
                        </td>
                    </tr>
                    <tr id="trTVI">
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%" class="SYNCTABLE">
                                            <tr>
                                                <td class="green-title" align="center">
                                                    Cyan
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="center">
                                                    <asp:Chart ID="CHRT_DotgainISOCyan" runat="server" Width="325px" Height="175px"
                                                        ImageType="Jpeg" ImageUrl="~/UploadFiles/temp/DotgainISOCyan" BackGradientStyle="Center"
                                                        BackSecondaryColor="White" BorderLineColor="#808080">
                                                        <Legends>
                                                            <asp:Legend Name="Default" Enabled="false">
                                                            </asp:Legend>
                                                        </Legends>
                                                        <BorderSkin SkinStyle="None"></BorderSkin>
                                                                                   
                                                        <Series>
                                                           <asp:Series Name="Series0" MarkerSize="12" BackSecondaryColor="162, 164, 255" IsVisibleInLegend="false"
                                                                Color="Green" ChartType="Spline" MarkerBorderColor="Green" MarkerStyle="Circle"
                                                                BorderWidth="2">
                                                            </asp:Series>
                                                            <asp:Series YValuesPerPoint="2" XValueType="Double" Name="SeriesTop" ChartType="SplineRange"
                                                                BorderColor="220, 65, 140, 140" Color="80, 65, 140, 140" YValueType="Double">
                                                            </asp:Series>                                                            
                                                             <asp:Series Name="Series1" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                                                Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="Black" MarkerStyle="Circle"
                                                                MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                                            </asp:Series>                                                         
                                                        </Series>
                                                        <ChartAreas>
                                                            <asp:ChartArea Name="Default" BorderColor="26, 59, 105" BackSecondaryColor="White"
                                                                BackColor="Gray" BackGradientStyle="TopBottom" BorderDashStyle="Solid" ShadowOffset="1">
                                                                <Position Height="99" Width="94" X="0" Y="2" />
                                                                <AxisY IsLabelAutoFit="False" Title="" TitleFont="Verdana, 8pt, style=Bold" Interval="Auto">
                                                                    <MajorGrid LineColor="Gray"></MajorGrid>
                                                                </AxisY>
                                                                <AxisX IsLabelAutoFit="False" IsMarginVisible="False" Interval="25" Title="">
                                                                    <MajorGrid LineColor="Gray"></MajorGrid>
                                                                    <LabelStyle Angle="0" />
                                                                </AxisX>
                                                            </asp:ChartArea>
                                                        </ChartAreas>
                                                    </asp:Chart>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%" class="SYNCTABLE">
                                            <tr>
                                                <td class="green-title" align="center">
                                                    Magenta
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Chart ID="CHRT_DotgainISOMeganta" runat="server" Width="325px" Height="175px"
                                                        ImageType="Jpeg" ImageUrl="~/UploadFiles/temp/DotgainISOMagenta" BackGradientStyle="Center"
                                                        BackSecondaryColor="White" BorderLineColor="#808080">
                                                        <Legends>
                                                            <asp:Legend Name="Default" Enabled="false">
                                                            </asp:Legend>
                                                        </Legends>
                                                        <BorderSkin SkinStyle="None"></BorderSkin>
                                                                                   
                                                        <Series>
                                                            <asp:Series Name="Series0" MarkerSize="12" BackSecondaryColor="162, 164, 255" IsVisibleInLegend="false"
                                                                Color="Green" ChartType="Spline" MarkerBorderColor="Green" MarkerStyle="Circle"
                                                                BorderWidth="2">
                                                            </asp:Series>                                                           
                                                            <asp:Series YValuesPerPoint="2" XValueType="Double" Name="SeriesTop" ChartType="SplineRange"
                                                                BorderColor="220, 65, 140, 140" Color="80, 65, 140, 140" YValueType="Double">
                                                            </asp:Series>
                                                            <asp:Series Name="Series1" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                                                Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="Black" MarkerStyle="Circle"
                                                                MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                                            </asp:Series>                                                         
                                                        </Series>
                                                        <ChartAreas>
                                                            <asp:ChartArea Name="Default" BorderColor="26, 59, 105" BackSecondaryColor="White"
                                                                BackColor="Gray" BackGradientStyle="TopBottom" BorderDashStyle="Solid" ShadowOffset="1">
                                                                <Position Height="99" Width="94" X="0" Y="2" />
                                                                <AxisY IsLabelAutoFit="False" Title="" TitleFont="Verdana, 8pt, style=Bold" Interval="Auto">
                                                                    <MajorGrid LineColor="Gray"></MajorGrid>
                                                                </AxisY>
                                                                <AxisX IsLabelAutoFit="False" IsMarginVisible="False" Interval="25" Title="">
                                                                    <MajorGrid LineColor="Gray"></MajorGrid>
                                                                    <LabelStyle Angle="0" />
                                                                </AxisX>
                                                            </asp:ChartArea>
                                                        </ChartAreas>
                                                    </asp:Chart>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%" class="SYNCTABLE">
                                            <tr>
                                                <td class="green-title" align="center">
                                                    Yellow
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Chart ID="CHRT_DotgainISOYellow" runat="server" Width="325px" Height="175px"
                                                        ImageType="Jpeg" ImageUrl="~/UploadFiles/temp/DotgainISOYellow" BackGradientStyle="Center"
                                                        BackSecondaryColor="White" BorderLineColor="#808080">
                                                        <Legends>
                                                            <asp:Legend Name="Default" Enabled="false">
                                                            </asp:Legend>
                                                        </Legends>
                                                        <BorderSkin SkinStyle="None"></BorderSkin>
                                                                                    
                                                        <Series>
                                                            <asp:Series Name="Series0" MarkerSize="12" BackSecondaryColor="162, 164, 255" IsVisibleInLegend="false"
                                                                Color="Green" ChartType="Spline" MarkerBorderColor="Green" MarkerStyle="Circle"
                                                                BorderWidth="2">
                                                            </asp:Series>                                                           
                                                            <asp:Series YValuesPerPoint="2" XValueType="Double" Name="SeriesTop" ChartType="SplineRange"
                                                                BorderColor="220, 65, 140, 140" Color="80, 65, 140, 140" YValueType="Double">
                                                            </asp:Series>
                                                            <asp:Series Name="Series1" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                                                Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="Black" MarkerStyle="Circle"
                                                                MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                                            </asp:Series>                                                          
                                                        </Series>
                                                        <ChartAreas>
                                                            <asp:ChartArea Name="Default" BorderColor="26, 59, 105" BackSecondaryColor="White"
                                                                BackColor="Gray" BackGradientStyle="TopBottom" BorderDashStyle="Solid" ShadowOffset="1">
                                                                <Position Height="99" Width="94" X="0" Y="2" />
                                                                <AxisY IsLabelAutoFit="False" Title="" TitleFont="Verdana, 8pt, style=Bold" Interval="Auto">
                                                                    <MajorGrid LineColor="Gray"></MajorGrid>
                                                                </AxisY>
                                                                <AxisX IsLabelAutoFit="False" IsMarginVisible="False" Interval="25" Title="">
                                                                    <MajorGrid LineColor="Gray"></MajorGrid>
                                                                    <LabelStyle Angle="0" />
                                                                </AxisX>
                                                            </asp:ChartArea>
                                                        </ChartAreas>
                                                    </asp:Chart>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%" class="SYNCTABLE">
                                            <tr>
                                                <td class="green-title" align="center">
                                                    Black
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Chart ID="CHRT_DotgainISOBlack" runat="server" Width="325px" Height="175px"
                                                        ImageType="Jpeg" ImageUrl="~/UploadFiles/temp/DotgainISOBlack" BackGradientStyle="Center"
                                                        BackSecondaryColor="White" BorderLineColor="#808080">
                                                        <Legends>
                                                            <asp:Legend Name="Default" Enabled="false">
                                                            </asp:Legend>
                                                        </Legends>
                                                        <BorderSkin SkinStyle="None"></BorderSkin>
                                                                                   
                                                         <Series>
                                                            <asp:Series Name="Series0" MarkerSize="12" BackSecondaryColor="162, 164, 255" IsVisibleInLegend="false"
                                                                Color="Green" ChartType="Spline" MarkerBorderColor="Green" MarkerStyle="Circle"
                                                                BorderWidth="2">
                                                            </asp:Series>                                                           
                                                            <asp:Series YValuesPerPoint="2" XValueType="Double" Name="SeriesTop" ChartType="SplineRange"
                                                                BorderColor="220, 65, 140, 140" Color="80, 65, 140, 140" YValueType="Double">
                                                            </asp:Series>
                                                            <asp:Series Name="Series1" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                                                Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="Black" MarkerStyle="Circle"
                                                                MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                                            </asp:Series>                                                           
                                                        </Series>
                                                        <ChartAreas>
                                                            <asp:ChartArea Name="Default" BorderColor="26, 59, 105" BackSecondaryColor="White"
                                                                BackColor="Gray" BackGradientStyle="TopBottom" BorderDashStyle="Solid" ShadowOffset="1">
                                                                <Position Height="99" Width="94" X="0" Y="2" />
                                                                <AxisY IsLabelAutoFit="False" Title="" TitleFont="Verdana, 8pt, style=Bold" Interval="Auto">
                                                                    <MajorGrid LineColor="Gray"></MajorGrid>
                                                                </AxisY>
                                                                <AxisX IsLabelAutoFit="False" IsMarginVisible="False" Interval="25" Title="">
                                                                    <MajorGrid LineColor="Gray"></MajorGrid>
                                                                    <LabelStyle Angle="0" />
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
                    </tr>
                    <tr id="trSpread" style="display: none">
                        <td>
                             <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" width="650px">
                                        <tr>
                                            <td align="right" style="padding-right: 30px">
                                                <asp:CheckBox ID="ChkC" runat="server" Text="C" Checked="true" AutoPostBack="true"
                                                    OnCheckedChanged="Chk_CheckedChanged" />
                                                &nbsp;&nbsp;<asp:CheckBox ID="ChkM" runat="server" Text="M" Checked="true" AutoPostBack="true"
                                                    OnCheckedChanged="Chk_CheckedChanged" />&nbsp;<asp:CheckBox ID="ChkY" runat="server"
                                                        Text="Y" Checked="true" AutoPostBack="true" OnCheckedChanged="Chk_CheckedChanged" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Chart ID="CHRT_SummaryISODotgain" runat="server" Width="650px" Height="310px"
                                                    ImageType="Jpeg" ImageUrl="~/UploadFiles/temp/CHRT_SummaryISODotgain" BackGradientStyle="Center"
                                                    BackSecondaryColor="#EDEDED" BorderLineColor="#808080"
                                                    EnableViewState="false">
                                                    <Series>
                                                        <asp:Series Name="Series1" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                                            Color="Cyan" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                            MarkerStyle="Circle" MarkerSize="15"  BackSecondaryColor="162, 164, 255">
                                                        </asp:Series>
                                                        <asp:Series Name="Series1Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                                            Color="Cyan" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                            MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                                        </asp:Series>
                                                        <asp:Series Name="Series2" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                                            Color="Magenta" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                            MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                                        </asp:Series>
                                                        <asp:Series Name="Series2Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                                            Color="Magenta" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                            MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                                        </asp:Series>
                                                        <asp:Series Name="Series3" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                                            Color="Yellow" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                            MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                                        </asp:Series>
                                                        <asp:Series Name="Series3Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                                            Color="Yellow" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                            MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                                        </asp:Series>
                                                        <asp:Series YValuesPerPoint="2" IsVisibleInLegend="false" XValueType="String" Name="SeriesTop"
                                                            ChartType="SplineRange" BorderColor="220, 65, 140, 140" Color="80, 65, 140, 140"
                                                            YValueType="String">
                                                        </asp:Series>
                                                    </Series>
                                                    <BorderSkin SkinStyle="None"></BorderSkin>
                                                                               
                                                    <ChartAreas>
                                                        <asp:ChartArea Name="Default" BorderColor="26, 59, 105" BackSecondaryColor="White"
                                                            BackColor="Gray" BackGradientStyle="TopBottom" BorderDashStyle="Solid" ShadowOffset="1">
                                                            <Position Height="99" Width="94" X="0" Y="2" />
                                                            <AxisY IsLabelAutoFit="False" Title="" Interval="Auto">
                                                                <MajorGrid LineColor="Gray"></MajorGrid>
                                                            </AxisY>
                                                            <AxisX IsLabelAutoFit="False" IsMarginVisible="False" Interval="25" Title="">
                                                                <MajorGrid LineColor="Gray"></MajorGrid>
                                                            </AxisX>
                                                        </asp:ChartArea>
                                                    </ChartAreas>
                                                </asp:Chart>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 40px">
                                                <table class="TDBOX" border="0" cellpadding="5" cellspacing="0" width="90%">
                                                 
                                                    <tr class="TRTABLEBOX">
                                                        <td class="TDTABLEBOXLEFT">
                                                            <b>Chromatic Variation</b>
                                                        </td>                                                       
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                             <telerik:RadGrid ID="rgChromVariation" runat="server" 
                                                                AutoGenerateColumns="False" EnableLinqExpressions="False"                     
                                                                CellSpacing="0" GridLines="None" OnItemDataBound="rgChromVariation_ItemDataBound">   
                                                                    <HeaderStyle Font-Bold="True" />  
                                                                    <ItemStyle BackColor="#F2F2F2" />
                                                                    <AlternatingItemStyle BackColor="#FFFFFF" />                    
                                                                        <MasterTableView> 
                                                                            <Columns>
                                                                                <telerik:GridBoundColumn DataField="Tone20" HeaderText="20" 
                                                                                    UniqueName="Tone20">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="Tone25" HeaderText="25" 
                                                                                    UniqueName="Tone25">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="Tone40" HeaderText="40" 
                                                                                    UniqueName="Tone40">
                                                                                </telerik:GridBoundColumn>     
                                                                                <telerik:GridBoundColumn DataField="Tone50" HeaderText="50" 
                                                                                    UniqueName="Tone50">
                                                                                </telerik:GridBoundColumn>    
                                                                                <telerik:GridBoundColumn DataField="Tone60" HeaderText="60" 
                                                                                    UniqueName="Tone60">
                                                                                </telerik:GridBoundColumn>     
                                                                                <telerik:GridBoundColumn DataField="Tone75" HeaderText="75" 
                                                                                    UniqueName="Tone75">
                                                                                </telerik:GridBoundColumn>     
                                                                                <telerik:GridBoundColumn DataField="Tone80" HeaderText="80" 
                                                                                    UniqueName="Tone80">
                                                                                </telerik:GridBoundColumn>                                                
                                                                            </Columns>  
                                                                        </MasterTableView> 
                                                            </telerik:RadGrid>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
              
              </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="chartAnalyze" />   
                        <asp:PostBackTrigger ControlID="rbtnListDeltaE" />            
                        <asp:PostBackTrigger ControlID="chkAnalyzeAverageOfMeasurements" />            
                        
                    </Triggers>  
           </asp:UpdatePanel>  
   
        </div>
        <div class="clear"></div>
        </div>                            	
        </div>
        <div class="tab_bottom">&nbsp;</div>
   </section>
   </div>

    <div id="dvTab_PatchDetails" runat="server">
  <section id="tab4" class="contantbox tabcontant hideclass" >
        <div class="tab_top">&nbsp;</div>
        <div class="tab_middle">
           <div class="combopart">           
        <div class="float_l" style="padding-left: 10px;" > 
           
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
              <ContentTemplate>         
           
            <table cellpadding="0" cellspacing="0" width="100%"> 
               <tr>
                <td>
                    <div id="Div_MetricSumFilter" runat="server">
                        <asp:Localize Text="Filter by Metric Sum:" runat="server" 
                            ID="locFilterMetricSum" meta:resourcekey="locFilterMetricSumResource1"></asp:Localize> &nbsp;
                         <telerik:RadComboBox ID="rcmbMetricSumlist" AppendDataBoundItems="True" runat="server"                                               
                                      Width="150px" DropDownWidth="150px" AutoPostBack="true" OnSelectedIndexChanged ="rcmbMetricSumlist_SelectedIndexChanged">
                         </telerik:RadComboBox> 
                    </div>
                </td>
                <td align="right">
                     <asp:Button ID="btnCGATSExport" CssClass="button" Width="150px" Text="Export to CGATS" 
                        runat="server" onclick="btnCGATSExport_Click" meta:resourcekey="btnCGATSExportResource1"></asp:Button> 
                    <asp:Button ID="btnExcelExport" CssClass="button" Width="150px" 
                        Text="Export to Excel" runat="server" onclick="btnExcelExport_Click" Visible="False" 
                        meta:resourcekey="btnExcelExportResource1"></asp:Button>                   
                    <asp:Button ID="btnCSVExport" CssClass="button" Width="150px" Text="Export to CSV" 
                        runat="server" onclick="btnCSVExport_Click" meta:resourcekey="btnCSVExportResource1"></asp:Button>      
                                    
                </td>
               </tr>                            
               <tr>
                <td colspan="2">                
                    <telerik:RadGrid ID="rgPatchDetails"  AllowSorting="True" Width="650px" 
                    AllowPaging="True" runat="server" AutoGenerateColumns="False" EnableLinqExpressions="False"
                       AllowFilteringByColumn="True" 
                    OnNeedDataSource="rgPatchDetails_NeedDataSource" 
                        OnItemDataBound="rgPatchDetails_ItemDataBound" OnItemCommand="rgPatchDetails_ItemCommand"
                    OnSortCommand="rgPatchDetails_SortCommand" CellSpacing="0" GridLines="None" 
                        meta:resourcekey="rgPatchDetailsResource1" >
                    <GroupingSettings CaseSensitive="False" />
                         <ClientSettings>
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" FrozenColumnsCount="6">
                            </Scrolling>
                         </ClientSettings>
                        <HeaderStyle Font-Bold="True"/>
                            <PagerStyle Position="Top" AlwaysVisible="True" Font-Size="Small" />
                            <MasterTableView DataKeyNames="HTMLColor" >                      
                                <CommandItemSettings ExportToPdfText="Export to PDF" />                             
                                <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" 
                                    Visible="True">
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
                                    Visible="True">
                                </ExpandCollapseColumn>
                                <Columns>
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

                                    <telerik:GridBoundColumn DataField="SampleID" HeaderText="ID" 
                                        SortExpression="SampleID" AllowFiltering="False" UniqueName="SampleId" 
                                        FilterControlAltText="Filter SampleId column" 
                                        meta:resourcekey="GridBoundColumnResource1">
                                    </telerik:GridBoundColumn> 
                                    <telerik:GridBoundColumn DataField="CMYK_C" HeaderText="C" 
                                        SortExpression="CMYK_C" FilterControlWidth="30px" 
                                        CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True" 
                                        UniqueName="CmykC" DataType="System.Int32" 
                                        FilterControlAltText="Filter CmykC column" 
                                        meta:resourcekey="GridBoundColumnResource2">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CMYK_M" HeaderText="M" 
                                        SortExpression="CMYK_M" FilterControlWidth="30px" 
                                        CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True" 
                                        UniqueName="CmykM" DataType="System.Int32" 
                                        FilterControlAltText="Filter CmykM column" 
                                        meta:resourcekey="GridBoundColumnResource3">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CMYK_Y" HeaderText="Y" 
                                        SortExpression="CMYK_Y" FilterControlWidth="30px" 
                                        CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True" 
                                        UniqueName="CmykY" DataType="System.Int32" 
                                        FilterControlAltText="Filter CmykY column" 
                                        meta:resourcekey="GridBoundColumnResource4">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CMYK_K" HeaderText="K" 
                                        SortExpression="CMYK_K" FilterControlWidth="30px" 
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
                                     <telerik:GridBoundColumn DataField="CIELAB_L" HeaderText="Actual L*" 
                                        SortExpression="CIELAB_L" FilterControlWidth="30px" 
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
                                     <telerik:GridBoundColumn DataField="CIELAB_a" HeaderText="Actual a*" 
                                        SortExpression="CIELAB_a" FilterControlWidth="30px" 
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
                                    <telerik:GridBoundColumn DataField="CIELAB_b" HeaderText="Actual b*" 
                                        SortExpression="CIELAB_b" FilterControlWidth="30px" 
                                        CurrentFilterFunction="GreaterThanOrEqualTo" AutoPostBackOnFilter="True" 
                                        FilterControlAltText="Filter CielabB column" 
                                        meta:resourcekey="GridBoundColumnResource14" UniqueName="CielabB">
                                    </telerik:GridBoundColumn>    
                                     
                                     <telerik:GridBoundColumn DataField="Density_V" HeaderText="Density_V" 
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
                                    </telerik:GridBoundColumn>           
                                </Columns>                        



                            </MasterTableView>                        


                </telerik:RadGrid>                             
                 </td>
                </tr>
            </table>             
         </ContentTemplate> 
         <Triggers>
                <asp:PostBackTrigger ControlID="btnCGATSExport" />         
                <asp:PostBackTrigger ControlID="btnExcelExport" />               
                <asp:PostBackTrigger ControlID="btnCSVExport" />   
                <asp:PostBackTrigger ControlID="rcmbMetricSumlist" />                                
         </Triggers>        
       </asp:UpdatePanel>

       </div>
        <div class="clear"></div>
        </div>  
    </div>    
    <div class="tab_bottom">
        &nbsp;</div>
  </section>
  </div>

    <div>
  <section id="tab5" class="contantbox tabcontant hideclass">
      <div class="tab_top">&nbsp;</div>
        <div class="tab_middle">
           <div class="combopart">
        <div class="float_l" style="padding-left: 10px;" > 
          <asp:UpdatePanel ID="UpdatePanel6" runat="server">
              <ContentTemplate>         
           
                 <table cellpadding="0" cellspacing="0" width="650px">                                                                       
                        <tr >
                            <td>
                                <table id="TblGeneratePDFReport" runat="server" border="0" width="100%" style="height: 100%;"
                                        cellpadding="0" cellspacing="0" class="TABLE1">
                                    <tr runat="server">
                                        <td valign="top" runat="server">
                                         <iframe id="iframeGeneratePDFReport" runat="server" name="iframeGeneratePDFReport" style="height: 350px;
                                                width: 100%; display: none" frameborder="0" scrolling="no"></iframe>                                           
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
  </div>
   

  </section>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="clear">
    </div>
</div>
<div class="contant_bottom">
    &nbsp;</div>
<asp:HiddenField ID="hdnSelectedTabIndex" runat="server" Value="0" />
<asp:HiddenField ID="hdHasMeasurement" runat="server" Value="0" />
<asp:Label ID="InjectScript" runat="server" Width="410px" meta:resourcekey="InjectScriptResource1"></asp:Label>
<telerik:RadWindowManager ID="RadWindowManager" runat="server" Modal="True" meta:resourcekey="RadWindowManagerResource1">
    <Windows>
        <telerik:RadWindow ID="ConfirmSaveWindow" runat="server" ShowContentDuringLoad="false"
            VisibleStatusbar="false" Title="Choose Next Action" Width="702" Height="250"
            Behaviors="Move,Close" NavigateUrl="~/PopupConfirmSave.aspx" meta:resourcekey="ConfirmSaveWindowResource1">
        </telerik:RadWindow>
        <telerik:RadWindow ID="ClosePrintConditionWindow" runat="server" VisibleStatusbar="false"
            ShowContentDuringLoad="false" Title="Close Print Condition" Width="720" Height="200"
            Behaviors="Move,Close">
        </telerik:RadWindow>
    </Windows>
</telerik:RadWindowManager>
<div class="FadingTooltip" id="FADINGTOOLTIP" style="z-index: 999; visibility: hidden;
    position: absolute">
</div>
<asp:Literal ID="LTRL_PageSize" runat="server" Visible="False" meta:resourcekey="LTRL_PageSizeResource1"
    Text="5"></asp:Literal>
<asp:Literal ID="LTRL_CurrentPage" runat="server" Visible="False" meta:resourcekey="LTRL_CurrentPageResource1"
    Text="-1"></asp:Literal>
<asp:Literal ID="LTRL_StartIndex" runat="server" Visible="False" meta:resourcekey="LTRL_StartIndexResource1"
    Text="0"></asp:Literal>
