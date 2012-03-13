<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Analyze.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.Analyze" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="uc" %>
<%@ Register Src="~/UserControl/PopupControl/CreateDLP.ascx" TagName="CreateDLP" TagPrefix="uc" %>
<%@ Register Src="~/UserControl/PopupControl/ConfirmSave.ascx" TagName="ConfirmSave" TagPrefix="uc" %>
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

    var secs;
    var timerID = null;
    var timerRunning = false;
    var delay = 1000;

    function ShowCreateDLP() {
        ChooseRadWindow();
    }

    //According to new or old Project open rad window
    function ChooseRadWindow() {
        if ("<%= IsNewDlp %>" == "True") {
            var oWnd = $find("<%= CreateDLPWindow.ClientID %>");
            InitializeTimer();
            oWnd.show();
        }
        else {
            var oWnd = $find("<%= ConfirmSaveWindow.ClientID %>");
            oWnd.show();
        }
    }


    function CloseCreateDLP() {
        var oWnd = $find("<%= CreateDLPWindow.ClientID %>");
        oWnd.close();
    }
    function CloseConfirmSaveWindow() {
        var oWnd = $find("<%= ConfirmSaveWindow.ClientID %>");
        oWnd.close();
    }


    function InitializeTimer() {
        // Set the length of the timer, in seconds
        secs = 10
        StopTheClock()
        StartTheTimer()
    }

    function StopTheClock() {
        if (timerRunning)
            clearTimeout(timerID)
        timerRunning = false
    }

    function StartTheTimer() {
        if (secs == 0) {
            StopTheClock();
            CloseCreateDLP();
            debugger;
            window.location = 'Export.aspx';
        }
        else {
            self.status = secs
            secs = secs - 1
            timerRunning = true
            timerID = self.setTimeout("StartTheTimer()", delay)
        }
    }

</script>
<div class="contant_top">
    &nbsp;</div>
<div class="contant_middle">
    <div class="head">
        <h1 id="header" runat="server">
            Analyze</h1>
        <div class="Information_divAnalyze">
            <asp:LinkButton ID="lbtnDownloadAndSaveDLP" runat="server" CssClass="button" OnClientClick="javascript:ShowCreateDLP(); return false;">
                <span>
                    <asp:Image ID="Image2" runat="server" ImageUrl="../Images/export.png" />
                    Download DLP</span> &nbsp;
            </asp:LinkButton>
        </div>
    </div>
    <div class="listDevice" style="margin-left: 10px;">
        <uc:Statusbar ID="ucStatusbar" runat="server" Visible="false" />
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
                                    <asp:Image ID="Image1" runat="server" ImageUrl="../Images/analyze_chart.png" />
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
</div>
<div class="clear">
</div>
<telerik:RadWindowManager ID="RadWindowManager" runat="server" Modal="True">
    <Windows>
        <telerik:RadWindow ID="CreateDLPWindow" runat="server" ShowContentDuringLoad="false"
            Title="Create DLP" Width="716" Height="250" Behavior="Move">
            <ContentTemplate>
                <uc:CreateDLP ID="ucCreateDLP" runat="server" />
            </ContentTemplate>
        </telerik:RadWindow>
        <telerik:RadWindow ID="ConfirmSaveWindow" runat="server" ShowContentDuringLoad="false"
            Title="Confirm Save" Width="718" Height="275"  Behavior="Close, Move">
            <ContentTemplate>
                <uc:ConfirmSave ID="ConfirmSave" runat="server" />
            </ContentTemplate>
        </telerik:RadWindow>
    </Windows>
</telerik:RadWindowManager>
<div class="contant_bottom">
    &nbsp;</div>
<asp:HiddenField ID="hdnSelectedTabIndex" runat="server" Value="0" />
