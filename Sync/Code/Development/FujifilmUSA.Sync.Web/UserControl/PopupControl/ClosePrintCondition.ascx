<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClosePrintCondition.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.ClosePrintCondition" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<%@ Register Src="~/UserControl/PopupControl/CreateDLP.ascx" TagName="CreateDLP" TagPrefix="uc" %>
<script language="javascript" type="text/javascript">
    function CloseReOptimize() {
        var oWindow = GetProjectInformationRadWindow();
        GetProjectInformationRadWindow().close();
        return false;
    }

    function CloseClosePrintConditionWindow() {
        var oWindow = GetProjectInformationRadWindow();
        GetProjectInformationRadWindow().close();
        return false;
    }

    function GetProjectInformationRadWindow() {
        var oWindow = null;
        if (window.radWindow) oWindow = window.radWindow;
        else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
        return oWindow;
    }

</script>
<div style="display: block; margin: 10px;" class="contantbox tabcontant">
    <div class="section_top">
        &nbsp;</div>
    <div class="section_middle">
        <div class="combopart">
            <div style="padding: 5px;" id="cotainer">
                <div class="OpenPrintCondition">
                    <table>
                        <tr>
                            <td>
                                <div>
                                    <table>
                                        <tr>
                                            <td>
                                                <ucStatusbar:Statusbar ID="fujiStatusbar" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Content" style="line-height: 24px;">
                                                <asp:Label ID="lblClosePrintConditionMessage" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="clear">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <div class="buttonleft" style="padding-left: 3px">
                                    <asp:UpdatePanel ID="upReOptimize" runat="server">
                                        <ContentTemplate>
                                    <asp:LinkButton ID="lbtnReOptimize" runat="server" CssClass="gray_bt" OnClick="lbtnReOptimize_Click">
                <span>Re Optimize</span>
                                    </asp:LinkButton>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="buttonleft">
                                    <asp:LinkButton ID="lbtnDownloadCurrentDLP" runat="server" CssClass="gray_bt" OnClick="lbtnDownloadCurrentDLP_Click">
                    <span>Download Current DLP</span>
                                    </asp:LinkButton>
                                </div>
                                <div class="buttonleft">
                                    <asp:LinkButton ID="lbtnViewLastAnalysis" runat="server" CssClass="gray_bt" OnClick="lbtnViewLastAnalysis_Click">
                <span>View Last Analysis</span>
                                    </asp:LinkButton>
                                </div>
                                <div class="buttonleft">
                                    <asp:LinkButton ID="lbtnClose" runat="server" OnClientClick="javascript: CloseClosePrintConditionWindow(); return false;"
                                        CssClass="gray_bt">
                        <span>Cancel</span>
                                    </asp:LinkButton>
                                </div>
                                <div class="clear">
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
    <div class="section_bottom">
        &nbsp;</div>
</div>
<asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true" AssociatedUpdatePanelID="upReOptimize"
    DisplayAfter="50" >
    <ProgressTemplate>
        <AjaxToolkit:AlwaysVisibleControlExtender ID="AlwaysVisibleControl" HorizontalSide="Left"
            VerticalSide="Top" TargetControlID="updateProgress" 
            runat="server">
        </AjaxToolkit:AlwaysVisibleControlExtender>
        <div class="UpdateProgressBackgroundWithNoOpacity" style="position: fixed; left: 0px; top: 0px;
            z-index: 1000; width: 100%; height: 100%;" unselectable="on">
            <div style="margin: 10px 0px 10px 3px;">
                <center>
                    <uc:CreateDLP runat="server" ID="ucCreateDLP" />
                </center>
            </div>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
