<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="Analyze.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.Analyze" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="uc" %>
<%@ Register Src="~/UserControl/PopupControl/CreateDLP.ascx" TagName="CreateDLP" TagPrefix="uc" %>
 <script type="text/javascript">
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_initializeRequest(InitializeRequest);

    function InitializeRequest(sender, args) {
        //  alert(jQuery("input[id$=hdHasMeasurement]").val());
        if (jQuery("input[id$=hdHasMeasurement]").length > 0) {
            if (jQuery("input[id$=hdHasMeasurement]").val() == 'True' || jQuery("input[id$=hdHasMeasurement]").val() == 'true') {  //-------------If there are measurements under this opt. run, then only able to create DLP

                var updateProgress = $get('<%= UpdateProgressClientID %>');
                var postBackElement = args.get_postBackElement();
                if (postBackElement.id == '<%= lbtnDownloadAndSaveDLP.ClientID %>' || postBackElement.id == '<%= lbtnMeasurement.ClientID %>') {
                    if ($get('<%= lbtnDownloadAndSaveDLP.ClientID %>').text == "Next" || postBackElement.id == '<%= lbtnMeasurement.ClientID %>') {
                        updateProgress.control._associatedUpdatePanelId = null;
                        $get('<%= CreateDLPUpdateProgressClientID %>').control._associatedUpdatePanelId = 'dummyId';
                    }
                    else {
                        updateProgress.control._associatedUpdatePanelId = 'dummyId';
                    }
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

    function ShowClosePrintConditionWindow(projectId, projectName, DlpName, licenseType) {
        var oWindow;
        oWindow = window.radopen("PopupClosePrintCondition.aspx?projectId=" + projectId + "&projectName=" + projectName + "&DlpName=" + DlpName + "&ToSubOperation=0" + "&LicenseTypeId=" + licenseType, "ClosePrintConditionWindow");
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
        // alert(jQuery("input[id$=hdHasMeasurement]").val());
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
                    &nbsp;
                    <asp:LinkButton ID="lbtnMeasurement" runat="server" CssClass="buttonLeft" OnClick="lbtnMeasurement_Click">
                        <span>
                            <asp:Localize runat="server" Text="Measurement" ID="locMeasurement"></asp:Localize>
                        </span>
                    </asp:LinkButton>
                    <asp:LinkButton ID="lbtnMeasurementVerification" runat="server" CssClass="button"
                        OnClick="lbtnMeasurementVerification_Click">
                        <span>
                            <asp:Image ID="imgAnalyze" runat="server" ImageUrl="../Images/analyze.png" />
                            <asp:Localize runat="server" Text="Measure" ID="locMeasurementVerification"></asp:Localize>
                        </span>
                    </asp:LinkButton>
                    <asp:LinkButton ID="lbtnOptimize" CssClass="buttonNormal" runat="server" Visible="false">
                        <span>
                            <asp:Localize runat="server" Text="Optimize" ID="locOptimize"></asp:Localize>
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
                        <asp:RadioButtonList ID="rbtnListDeltaE" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                            OnSelectedIndexChanged="rbtnListDeltaE_SelectedIndexChanged" CssClass="RadioList">
                            <asp:ListItem Text="ΔE2000" Value="E2000" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="ΔE" Value="E"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr id="trListHeader" runat="server">
                    <td runat="server">
                        <div style="display: block;" class="contantbox tabcontant1">
                            <div class="Analyze_head">
                                <div id="Div_Opt_Header" runat="server" visible="false" class="Analyze_head_text" style="font-weight: bold; font-size: 14px; float: left; ">
                                    <asp:Localize runat="server" Text="Analyze Measurement :" ID="locAnaluzeMeasurement"
                                        meta:resourcekey="locAnaluzeMeasurementResource1"></asp:Localize>
                                    <asp:Label ID="lblMeasurementName" Style="font-size: 12px !important;" runat="server" ></asp:Label>
                                </div>
                                <div id="Div_Validation_Header" runat="server" visible="false">                                   
                                     Record(s)&nbsp;<asp:Literal ID="LTRL_StartRecord" runat="server">0</asp:Literal>&nbsp;to&nbsp;<asp:Literal ID="LTRL_EndRecord" runat="server">0</asp:Literal>&nbsp;of&nbsp;<asp:Literal ID="LTRL_TotalRec" runat="server">0</asp:Literal>&nbsp;|&nbsp;<asp:ImageButton ID="IBTN_FirstPage" runat="server" ImageUrl="~/Images/Nav_FirstPage_off.gif" OnClick="IBTN_FirstPage_Click" />&nbsp;<asp:ImageButton 
                                    ID="IBTN_PreviousPage" runat="server" 
                                    ImageUrl="~/Images/Nav_PreviousPage_off.gif" OnClick="IBTN_PreviousPage_Click" 
                                    style="height: 13px" />&nbsp;<asp:ImageButton ID="IBTN_NextPage" runat="server" ImageUrl="~/Images/Nav_NextPage_off.gif" OnClick="IBTN_NextPage_Click" />&nbsp;<asp:ImageButton ID="IBTN_LastPage" runat="server" ImageUrl="~/Images/Nav_LastPage_off.gif" OnClick="IBTN_LastPage_Click" />&nbsp;|&nbsp;Records&nbsp;<asp:DropDownList ID="DDL_PageSize" runat="server" CssClass="DROPDOWNLIST2" AutoPostBack="true" OnSelectedIndexChanged="DDL_PageSize_SelectedIndexChanged">
                                        <asp:ListItem Value="5" Selected="True">5</asp:ListItem>
                                        <asp:ListItem Value="10">10</asp:ListItem>
                                        <asp:ListItem Value="25">25</asp:ListItem>
                                        <asp:ListItem Value="50">50</asp:ListItem>
                                        <asp:ListItem Value="100">100</asp:ListItem>                                       
                                    </asp:DropDownList>&nbsp;|&nbsp;Go to page&nbsp;<asp:DropDownList ID="DDL_GoToPage" runat="server" CssClass="DROPDOWNLIST2" Width="45px" AutoPostBack="true" OnSelectedIndexChanged="DDL_GoToPage_SelectedIndexChanged"></asp:DropDownList>&nbsp;
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
                                                    <td style="font-size:13px;">Average Δ<asp:Label ID="lblAvgΔE" runat="server" Text=""></asp:Label></td>                                  
                                                    <td>
                                                        <asp:Label ID="lblRAvg_ΔE" runat="server" Text='<%#Eval("Avg_ΔE") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="White">   
                                                    <td style="font-size:13px;">Maximum Δ<asp:Label ID="lblMaxΔE" runat="server" Text=""></asp:Label></td>                                  
                                                    <td>
                                                        <asp:Label ID="lblRMax_ΔE" runat="server" Text='<%#Eval("Max_ΔE") %>'></asp:Label>
                                                    </td>
                                                </tr>    
                                                
                                                  <tr class="Gray">   
                                                    <td style="font-size:13px;">Maximum Primary Δ<asp:Label ID="lblPriΔE" runat="server" Text=""></asp:Label></td>                                  
                                                    <td>
                                                        <asp:Label ID="Label5" runat="server" Text='<%#Eval("Pri_ΔE") %>'></asp:Label>
                                                    </td>
                                                </tr>    
                                                  <tr class="White">   
                                                    <td style="font-size:13px;">Maximum Primary ΔH:</td>                                  
                                                    <td>
                                                        <asp:Label ID="Label7" runat="server" Text='<%#Eval("Pri_ΔH") %>'></asp:Label>
                                                    </td>
                                                </tr>    
                                                  <tr class="Gray">   
                                                    <td style="font-size:13px;">Gray Average Δ<asp:Label ID="lblGrayΔE" runat="server" Text=""></asp:Label></td>                                  
                                                    <td>
                                                        <asp:Label ID="Label9" runat="server" Text='<%#Eval("Gray_ΔE") %>'></asp:Label>
                                                    </td>
                                                </tr>    
                                                                                        
                                                <tr class="White">   
                                                    <td style="font-size:13px;">10th Quartile Δ<asp:Label ID="lblPercΔE10" runat="server" Text=""></asp:Label> </td>                                                                                     
                                                    <td>
                                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("PercΔE_10") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="Gray">   
                                                    <td style="font-size:13px;">90th Quartile Δ<asp:Label ID="lblPercΔE90" runat="server" Text=""></asp:Label> </td>                                                                                    
                                                    <td>
                                                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("PercΔE_90") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="White">   
                                                    <td style="font-size:13px;">95th Quartile Δ<asp:Label ID="lblPercΔE95" runat="server" Text=""></asp:Label> </td>                                                                                    
                                                    <td>
                                                        <asp:Label ID="Label3" runat="server" Text='<%#Eval("PercΔE_95") %>'></asp:Label>
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
                                <asp:DetailsView ID="dvValidationSummary" runat="server" Width="100%" AutoGenerateRows="false" GridLines="None" CssClass="Validation_Summary" >
                                    <Fields>
                                       <%--<asp:ImageField dataimageurlfield="MeasurementStatus" dataimageurlformatstring="~/Images/{0}.png" headertext="Measurement Status:" ItemStyle-BackColor="#F2F2F2" HeaderStyle-BackColor="#F2F2F2"></asp:ImageField>                                      --%>
                                        <asp:BoundField DataField="MeasurementName" HeaderText="Job:" HeaderStyle-Width="150px" HeaderStyle-CssClass="Summary_Left" ItemStyle-CssClass="Summary_Right"/>
                                        <asp:TemplateField HeaderText="Measurement Status:" HeaderStyle-CssClass="Summary_Left_Alt" ItemStyle-CssClass="Summary_Right_Alt">
                                            <ItemTemplate>
                                                <div class='<%# ((string)Eval("MeasurementStatus") == "OK") ? "Validation_Status_Green" : "Validation_Status_Red" %>'>
                                                    <asp:Label runat="server" ID="lblStatus" Text='<%# Eval("MeasurementStatus") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>   
                                        <asp:BoundField DataField="MetricTargetPresetName" HeaderText="Metric Preset:" HeaderStyle-CssClass="Summary_Left" ItemStyle-CssClass="Summary_Right"/>                                  
                                        <asp:BoundField DataField="MeasurementID" HeaderText="Measurement ID:"  HeaderStyle-CssClass="Summary_Left_Alt" ItemStyle-CssClass="Summary_Right_Alt"/>                                       
                                        <asp:BoundField DataField="MeasurementDate" HeaderText="Timestamp:"  HeaderStyle-CssClass="Summary_Left" ItemStyle-CssClass="Summary_Right"/>
                                        <asp:BoundField DataField="Customer" HeaderText="Customer:" HeaderStyle-CssClass="Summary_Left_Alt" ItemStyle-CssClass="Summary_Right_Alt"/>                                        
                                        <asp:BoundField DataField="dEMethod" HeaderText="ΔE Type:" HeaderStyle-CssClass="Summary_Left" ItemStyle-CssClass="Summary_Right"/>
                                        <asp:BoundField DataField="Notes" HeaderText="Notes:"  HeaderStyle-CssClass="Summary_Left_Alt" ItemStyle-CssClass="Summary_Right_Alt"/>                                       
                                    </Fields>
                                </asp:DetailsView>
                            </td>
                            <td style="width:2%;"></td>
                            <td valign="top" style="width:35%; ">
                                <telerik:RadGrid ID="rgValidationSummaryMetrics" runat="server" AutoGenerateColumns="false" EnableLinqExpressions="False"                     
                                    OnItemDataBound="rgValidationSummaryMetrics_ItemDataBound" CellSpacing="0" GridLines="None">   
                                        <HeaderStyle Font-Bold="True" />  
                                        <ItemStyle BackColor="#F2F2F2" />
                                        <AlternatingItemStyle BackColor="#FFFFFF" />                    
                                            <MasterTableView DataKeyNames="MetricMeasuredID">  
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="Metric" HeaderText="Metric" 
                                                        UniqueName="MetricSumName">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Tolerance" HeaderText="Tol" 
                                                        UniqueName="TargetValue">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Actual" HeaderText="Actual" 
                                                        UniqueName="MeasuredValue">
                                                    </telerik:GridBoundColumn>     
                                                    <telerik:GridBoundColumn DataField="IsNotOK" HeaderText="Is Not OK" 
                                                        UniqueName="IsNotOK" Visible="false">
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
                                        &nbsp; &Delta;<asp:Label ID="lblEPryPatches" runat="server" Text=""></asp:Label>
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
                                                meta:resourcekey="LitDeltaEPatchesResource1"></asp:Literal>
                                        </table>
                                    </td>
                                    <td style="width: 60%; " id="TDEPrimary" runat="server">
                                        <asp:Chart ID="chartDeltaE" runat="server" Width="210px" Height="362px" 
                                            BackColor="Transparent" meta:resourcekey="chartDeltaEResource1">                  
                                                            <Series>
                                                                <asp:Series Name="Series0"  Color="#D4E8FA" ChartArea="ChartArea1" ChartType="Bar">                                              
                                                                </asp:Series>
                                                                <asp:Series Name="Series1" BorderWidth="0" BorderColor="Gray" Color="Gray" 
                                                                                            IsVisibleInLegend="False" ChartArea="ChartArea1" ChartType="StackedBar">
                                                                </asp:Series>    
                                                            </Series>
                                                            <ChartAreas>
                                                                <asp:ChartArea Name="ChartArea1" BackColor="Transparent">
                                                                     <Area3DStyle IsRightAngleAxes="False" WallWidth="0"
                                                                                              IsClustered="True"></Area3DStyle>
                                          
                                                                    <AxisX  Enabled="True"  IsReversed="True" IsLabelAutoFit="True" >
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
                                                            </tr>
                                                        
                                                            <tr>
                                                                <td align="center" style="background: url(images/BGISOPaperGraph2_Sync4D.jpg) no-repeat center; width: 104px;
                                                                                        height: 169px; padding-left: 28px; padding-bottom: 12px;" >


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
                    </Triggers>  
            </asp:UpdatePanel>
        </div>
        <div class="clear"></div>
        </div>                            	
        </div>
        <div class="tab_bottom">&nbsp;</div>
  </section>
    
   <section id="tab6" class="contantbox tabcontant hideclass" runat="server">
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
                                            &nbsp; Gray Balance ΔCh
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
                                                            Minimum="0" Maximum="10">
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
                                            &nbsp; Gray  ΔCh Summary
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" style="padding: 2px">
                                            <telerik:RadGrid ID="RG_ΔChSummary" runat="server" AutoGenerateColumns="false" EnableLinqExpressions="False"                     
                                                OnItemDataBound="RG_ΔChSummary_ItemDataBound" CellSpacing="0" GridLines="None">   
                                                    <HeaderStyle Font-Bold="True" />  
                                                    <ItemStyle BackColor="#F2F2F2" />
                                                    <AlternatingItemStyle BackColor="#FFFFFF" />                    
                                                        <MasterTableView DataKeyNames="MetricMeasuredID">  
                                                            <Columns>
                                                                <telerik:GridBoundColumn DataField="Metric" HeaderText="Metric" 
                                                                    UniqueName="MetricSumName">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="Tolerance" HeaderText="Tolerance" 
                                                                    UniqueName="TargetValue">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="Actual" HeaderText="Actual" 
                                                                    UniqueName="MeasuredValue">
                                                                </telerik:GridBoundColumn>     
                                                                <telerik:GridBoundColumn DataField="IsNotOK" HeaderText="Is Not OK" 
                                                                    UniqueName="IsNotOK" Visible="false">
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
                                        &nbsp; Black ΔL
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
                                                        Minimum="0" Maximum="10">
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
                                            &nbsp; Black  ΔL Summary
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" style="padding: 2px">
                                            <telerik:RadGrid ID="RG_ΔLBlackSummary" runat="server" AutoGenerateColumns="false" EnableLinqExpressions="False"                     
                                                OnItemDataBound="RG_ΔLBlackSummary_ItemDataBound" CellSpacing="0" GridLines="None">   
                                                    <HeaderStyle Font-Bold="True" />  
                                                    <ItemStyle BackColor="#F2F2F2" />
                                                    <AlternatingItemStyle BackColor="#FFFFFF" />                    
                                                        <MasterTableView DataKeyNames="MetricMeasuredID">  
                                                            <Columns>
                                                                <telerik:GridBoundColumn DataField="Metric" HeaderText="Metric" 
                                                                    UniqueName="MetricSumName">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="Tolerance" HeaderText="Tolerance" 
                                                                    UniqueName="TargetValue">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="Actual" HeaderText="Actual" 
                                                                    UniqueName="MeasuredValue">
                                                                </telerik:GridBoundColumn>     
                                                                <telerik:GridBoundColumn DataField="IsNotOK" HeaderText="Is Not OK" 
                                                                    UniqueName="IsNotOK" Visible="false">
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
                                        &nbsp; Gray ΔL
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
                                                        Minimum="0" Maximum="10">
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
                                            &nbsp; Gray  ΔL Summary
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" style="padding: 2px">
                                            <telerik:RadGrid ID="RG_ΔLGraySummary" runat="server" AutoGenerateColumns="false" EnableLinqExpressions="False"                     
                                                OnItemDataBound="RG_ΔLGraySummary_ItemDataBound" CellSpacing="0" GridLines="None">   
                                                    <HeaderStyle Font-Bold="True" />  
                                                    <ItemStyle BackColor="#F2F2F2" />
                                                    <AlternatingItemStyle BackColor="#FFFFFF" />                    
                                                        <MasterTableView DataKeyNames="MetricMeasuredID">  
                                                            <Columns>
                                                                <telerik:GridBoundColumn DataField="Metric" HeaderText="Metric" 
                                                                    UniqueName="MetricSumName">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="Tolerance" HeaderText="Tolerance" 
                                                                    UniqueName="TargetValue">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="Actual" HeaderText="Actual" 
                                                                    UniqueName="MeasuredValue">
                                                                </telerik:GridBoundColumn>     
                                                                <telerik:GridBoundColumn DataField="IsNotOK" HeaderText="Is Not OK" 
                                                                    UniqueName="IsNotOK" Visible="false">
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
                                                        <tr runat="server">
                                                            <td class="green-title" runat="server">
                                                                <asp:Localize runat="server" Text="L* Comparison" ID="locLComparison"></asp:Localize>
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
                    <asp:Button ID="btnExcelExport" CssClass="button" Width="150px" 
                        Text="Export to Excel" runat="server" onclick="btnExcelExport_Click" Visible="False" 
                        meta:resourcekey="btnExcelExportResource1"></asp:Button>                   
                    <asp:Button ID="btnCSVExport" CssClass="button" Width="150px" Text="Export to CSV" 
                        runat="server" onclick="btnCSVExport_Click" meta:resourcekey="btnCSVExportResource1"></asp:Button>                   
                </td>
               </tr>                            
               <tr>
                <td>                
                    <telerik:RadGrid ID="rgPatchDetails"  AllowSorting="True" Width="650px" 
                    AllowPaging="True" PageSize="10" runat="server" AutoGenerateColumns="False" EnableLinqExpressions="False"
                       AllowFilteringByColumn="True" 
                    OnNeedDataSource="rgPatchDetails_NeedDataSource" 
                        OnItemDataBound="rgPatchDetails_ItemDataBound" OnItemCommand="rgPatchDetails_ItemCommand"
                    OnSortCommand="rgPatchDetails_SortCommand" CellSpacing="0" GridLines="None">
                    <GroupingSettings CaseSensitive="False" />
                         <ClientSettings>
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" FrozenColumnsCount="6">
                            </Scrolling>
                         </ClientSettings>
                        <HeaderStyle Font-Bold="True"/>
                            <PagerStyle Position="Top" AlwaysVisible="True" Font-Size="Small" />
                            <MasterTableView DataKeyNames="HTMLColor"  >                      
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
                            
                                                
                                </Columns>                        
                            </MasterTableView>                        
                </telerik:RadGrid>                             
                 </td>
                </tr>
            </table>             
         </ContentTemplate> 
         <Triggers>
                <asp:PostBackTrigger ControlID="btnExcelExport" />               
                <asp:PostBackTrigger ControlID="btnCSVExport" />                
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
                        <tr >
                            <td>
                                <table id="TblGeneratePDFReport" runat="server" border="0" width="100%" style="height: 100%;"
                                        cellpadding="0" cellspacing="0" class="TABLE1"  visible="true">
                                    <tr>
                                        <td valign="top">
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



<asp:Literal ID="LTRL_PageSize" runat="server" Visible="false">5</asp:Literal>
<asp:Literal ID="LTRL_CurrentPage" runat="server" Visible="false">-1</asp:Literal>
<asp:Literal ID="LTRL_StartIndex" runat="server" Visible="false">0</asp:Literal>

