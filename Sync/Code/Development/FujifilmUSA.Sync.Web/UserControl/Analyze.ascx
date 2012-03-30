<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Analyze.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.Analyze" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="uc" %>
<%@ Register Src="~/UserControl/PopupControl/CreateDLP.ascx" TagName="CreateDLP"
    TagPrefix="uc" %>
<%@ Register Src="~/UserControl/PopupControl/ConfirmSave.ascx" TagName="ConfirmSave"
    TagPrefix="uc" %>
<script type="text/javascript">
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_initializeRequest(InitializeRequest);

    function InitializeRequest(sender, args) {
        var updateProgress = $get('<%= UpdateProgressClientID %>');
        var postBackElement = args.get_postBackElement();
        if (postBackElement.id == '<%= lbtnDownloadAndSaveDLP.ClientID %>') {
            updateProgress.control._associatedUpdatePanelId = 'dummyId';
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
    function GetRadWindow() {
        var oWindow = null;
        if (window.radWindow) oWindow = window.radWindow;
        else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
        return oWindow;
    }
    //According to new or old Project open rad window
    function ChooseRadWindow() {
        if ("<%= IsNewDlp %>" == "True") {
            return true;
        }
        else {
            return true;
        }
    }
</script>
<style type="text/css">
    .Alpha
    {
        filter: progid:DXImageTransform.Microsoft.Alpha(style=0,opacity=100);
        background-color: #4AB097 !important;
        opacity: 100;
        mozopacity: 100;
    }
</style>
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
                            <asp:Image ID="Image2" runat="server" ImageUrl="../Images/export.png" />Download
                            DLP</span> &nbsp;
                    </asp:LinkButton>
                </div>
            </div>
            <div class="listDevice" style="margin-left: 10px;">
                <uc:Statusbar ID="fujiStatusbar" runat="server" Visible="false" />
                <table cellpadding="0" cellspacing="10" border="0" width="97%">
                    <tr id="trListHeader" runat="server">
                        <td>
                            <div style="display: block;" class="contantbox tabcontant">
                                <div class="section_top">
                                    &nbsp;</div>
                                <div class="section_middle">
                                    <div class="combopart">
                                        <div class="header">
                                            <div class="content">
                                                <div class="text" style="float: left">
                                                    Analyze Measurement : Sheet_01
                                                </div>
                                                <div class="text" style="float: right">
                                                    Analyze Average Of Measurements
                                                </div>
                                                <div class="clear">
                                                </div>
                                            </div>
                                        </div>
                                        <div style="border-style: None; padding: 5px">
                                            <center>
                                                <script type="text/javascript" language="javascript">
                                                    function ClickOnPoint(MeasurementID) {
                                                        var hdCheckBoxChange = document.getElementById("<%= hdCheckBoxChange.ClientID  %>");
                                                        hdCheckBoxChange.value = "0";
                                                        var obj1 = document.getElementById("<%= hdMeasurement.ClientID %>");
                                                        obj1.value = MeasurementID;
                                                        document.getElementById("<%= hdChartButton.ClientID %>").click();
                                                    }
                                                    function ClickOnCheckBox(MeasurementID, checkedValue) {
                                                        var hdCheckBoxChange = document.getElementById("<%= hdCheckBoxChange.ClientID  %>");
                                                        hdCheckBoxChange.value = "1";
                                                        var obj1 = document.getElementById("<%= hdMeasurement.ClientID  %>");
                                                        obj1.value = MeasurementID;
                                                        var chekcBox = document.getElementById("<%= hdChartCheckBox.ClientID %>");
                                                        chekcBox.checked = checkedValue == "True" ? true : false;
                                                        document.getElementById("<%= hdChartButton.ClientID %>").click();
                                                    } 
                                                </script>
                                                <asp:Chart ID="chartAnalyze" runat="server" Width="650px" Height="250px" BackGradientType="Center"
                                                    BackGradientEndColor="#DFDFDF" BorderlineColor="#808080" BorderlineWidth="1"
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
                                                        <asp:ChartArea Name="ChartArea1">
                                                            <Position Height="99" Width="99" X="0" Y="0" />
                                                            <AxisY Enabled="True" Title="&Delta; E" TitleFont="Verdana, 8pt" Interval="Auto">
                                                                <MajorGrid LineColor="Beige" />
                                                            </AxisY>
                                                            <AxisX Interval="1" TitleFont="Verdana, 5pt" Title="M">
                                                                <MajorGrid LineColor="Beige" />
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
                                        <div class="clear">
                                        </div>
                                    </div>
                                </div>
                                <div class="section_bottom">
                                    &nbsp;</div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true" AssociatedUpdatePanelID="upCreateDLP"
        DisplayAfter="50">
        <ProgressTemplate>
            <AjaxToolkit:AlwaysVisibleControlExtender ID="AlwaysVisibleControl" HorizontalSide="Left"
                VerticalSide="Top" TargetControlID="updateProgress" HorizontalOffset="325" VerticalOffset="210"
                runat="server">
            </AjaxToolkit:AlwaysVisibleControlExtender>
            <div id="divUpdateCreateDLP" class="Alpha" runat="server">
                <uc:CreateDLP runat="server" ID="ucCreateDLP" />
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
                <li><span><a href="#tab2">Chart</a></span></li>                                
                <li><span><a href="#tab2">Ink Paper</a></span></li>                                
                <li><span><a href="#tab2">Gray</a></span></li>                                
                <li><span><a href="#tab2">Patch Deatils</a></span></li>                                
            </ul>
            <div class="clear"></div>
        </section>


        <section id="tab1" class="contantbox tabcontant hideclass" >
        <div class="tab_top">&nbsp;</div>
        <div class="tab_middle">
           <div class="combopart">
        <div class="float_l" style="padding-left: 10px;" > 
          <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <span>Selected Metric: &nbsp </span>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbSelectedMetric" runat="server" Width="200px" CssClass="select">
                                                <asp:ListItem Text="Idealliance Digital Chart" Value="Idealliance Digital Chart"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table></div>
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
     
        </div>
        <div class="clear"></div>
        </div>                            	
        </div>
        <div class="tab_bottom">&nbsp;</div>
        </section>
    <section id="tab3" class="contantbox tabcontant hideclass">
        <div class="tab_top">&nbsp;</div>
        <div class="tab_middle">
        <div class="combopart"><asp:HiddenField ID="HiddenField1" runat="server" Value="0"/>  
        <div class="float_l" style="padding-left: 10px;" >Ink Paper</div>
        <div class="clear"></div>
        </div>                            	
        </div>
        <div class="tab_bottom">&nbsp;</div>
        </section>
    <section id="tab4" class="contantbox tabcontant hideclass">
        <div class="tab_top">&nbsp;</div>
        <div class="tab_middle">
        <div class="combopart"><asp:HiddenField ID="HiddenField2" runat="server" Value="0"/>  
        <div class="float_l" style="padding-left: 10px;" >Gray</div>
        <div class="clear"></div>
        </div>                            	
        </div>
        <div class="tab_bottom">&nbsp;</div>
        </section>
    <section id="tab5" class="contantbox tabcontant hideclass">
        <div class="tab_top">&nbsp;</div>
        <div class="tab_middle">
        <div class="combopart"><asp:HiddenField ID="HiddenField3" runat="server" Value="0"/>  
        <div class="float_l" style="padding-left: 10px;" >Patch Deatils</div>
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
<telerik:RadWindowManager ID="RadWindowManager" runat="server" Modal="True">
    <Windows>
        <telerik:RadWindow ID="ConfirmSaveWindow" runat="server" ShowContentDuringLoad="false"
            VisibleStatusbar="false" Title="Confirm Save" Width="718" Height="275" Behavior="Close, Move"
            NavigateUrl="~/PopupConfirmSave.aspx">
        </telerik:RadWindow>
        <%--<telerik:RadWindow ID="CreateDLP" runat="server" ShowContentDuringLoad="false"
            VisibleStatusbar="false" Title="Create DLP" Width="718" Height="275" Behavior="Move">
            <ContentTemplate>
                 <uc:CreateDLP runat="server" ID="ucCreateDLP" />
            </ContentTemplate> 
        </telerik:RadWindow>--%>
    </Windows>
</telerik:RadWindowManager>
<div class="contant_bottom">
    &nbsp;</div>
<asp:HiddenField ID="hdnSelectedTabIndex" runat="server" Value="0" />
<asp:Label ID="InjectScript" runat="server" Width="410px"></asp:Label>