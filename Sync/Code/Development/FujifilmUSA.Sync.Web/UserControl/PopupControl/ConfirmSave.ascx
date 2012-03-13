<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfirmSave.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.ConfirmSave" %>
<%@ Register Src="~/UserControl/PopupControl/CreateDLP.ascx" TagName="CreateDLP" TagPrefix="uc" %>

<script type="text/javascript" language="javascript">

        var secs;
        var timerID = null;
        var timerRunning = false;
        var delay = 1000;


        function ChooseOptimize() {
            CloseConfirmSaveWindow();
            var oWnd = $find("<%= CreateDLPWindow.ClientID %>");
            InitializeTimer();
            oWnd.show();
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
<div style="display: block; margin: 10px;" class="contantbox tabcontant">
    <div class="section_top">
        &nbsp;</div>
    <div class="section_middle">
        <div class="combopart">
            <div class="header">
                <div class="content">
                    <div class="text" style="float: left">
                        Are you satisfied with the results?
                    </div>
                    <div class="text" style="float:right">
                    <div class="buttonright" style="padding-left: 3px">
                    <asp:LinkButton ID="lbtnOptimize" runat="server" CssClass="gray_bt" OnClientClick="javascript:ChooseOptimize(); return false;">
                <span> No, Optimize </span>
                    </asp:LinkButton>
                </div>
                <div class="buttonright">
                    <asp:LinkButton ID="lbtnYes" runat="server" CssClass="gray_bt" 
                        onclick="lbtnYes_Click">
                <span> Yes, Close </span>
                    </asp:LinkButton>
                </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
            <div style="border-style: None; padding: 5px">
                <div style="float:left; line-height: 24px";>
                   <table  cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="font-weight: bold;">Optimize : &nbsp
                        </td>
                        <td>        
                            <p style="padding-top:7px;">              
                            Close sets the state of this DLP to Closed and is considered a confirmation that your are satisfied with the colormatch.
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold;">Close : &nbsp
                        </td>
                        <td>
                                             <p style="padding-top:7px;">              
                     Optimize creates new optimized DLP which should be implemented in your workflow.
                     </p>
                        </td>
                    </tr>
                   </table> 
                </div>
                <div class="buttonright" style="padding-left: 3px">
                    <asp:LinkButton ID="lbtnClose" runat="server" OnClientClick="javascript: CloseConfirmSaveWindow(); return false;"
                        CssClass="gray_bt">
                                        <span>
                                            Close </span>
                    </asp:LinkButton>
                </div>
                

                <div class="clear">
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
    <div class="section_bottom">
        &nbsp;</div>
</div>
<telerik:RadWindowManager ID="RadWindowManager" runat="server" Modal="True">
    <Windows>
        <telerik:RadWindow ID="CreateDLPWindow" runat="server" ShowContentDuringLoad="false"
            Title="Create DLP" Width="716" Height="250" Behavior="Move">
            <ContentTemplate>
                <uc:CreateDLP ID="ucCreateDLP" runat="server" />
            </ContentTemplate>
        </telerik:RadWindow>
    </Windows>
</telerik:RadWindowManager>
