<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SpotClosePrintCondition.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.PopupControl.SpotClosePrintCondition" %>
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
    <div style="margin-bottom: 3px;">
        <ucStatusbar:Statusbar ID="fujiStatusbar" runat="server" Visible="False" />
    </div>
    <div class="section_top">
        &nbsp;
    </div>
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
                                            <td class="Content" style="line-height: 24px;">
                                                <asp:Label ID="lblClosePrintConditionMessage" runat="server"
                                                    meta:resourcekey="lblClosePrintConditionMessageResource1"></asp:Label>
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
                                            <asp:LinkButton ID="lbtnReOptimize" runat="server" CssClass="gray_bt"
                                                OnClick="lbtnReOptimize_Click">
                                                <span>
                                                    <asp:Localize runat="server" Text="Re Optimize" ID="locReOptimize"
                                                        meta:resourcekey="locReOptimizeResource1"></asp:Localize>
                                                </span>
                                            </asp:LinkButton>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="buttonleft">
                                    <asp:LinkButton ID="lbtnDownloadCertifiedDownloads" runat="server" CssClass="gray_bt"
                                        OnClick="lbtnDownloadCurrentDLP_Click">
                                        <span>
                                            <asp:Localize runat="server" Text="Download Current DLP"
                                                ID="locDownloadCurrentDLP" meta:resourcekey="locDownloadCertifiedNCPResource1"></asp:Localize>
                                        </span>
                                    </asp:LinkButton>
                                </div>
                                <%--<div class="buttonleft">
                                    <asp:LinkButton ID="lbtnDownloadSourceProf" runat="server" CssClass="gray_bt"
                                        Visible="false" OnClick="lbtnDownloadSourceProf_Click">
                                        <span>
                                            <asp:Localize runat="server" Text="Download Source Profile"
                                                ID="locDownloadSourceProf" meta:resourcekey="locDownloadSourceProfResource1"></asp:Localize>
                                        </span>
                                    </asp:LinkButton>
                                </div>--%>
                                <div class="buttonleft">
                                    <asp:LinkButton ID="lbtnViewLastAnalysis" runat="server" CssClass="gray_bt"
                                        OnClick="lbtnViewLastAnalysis_Click">
                                        <span>
                                            <asp:Localize runat="server" Text="View Last Analysis" ID="locViewLastAnalysis"
                                                meta:resourcekey="locViewLastAnalysisResource1"></asp:Localize>
                                        </span>
                                    </asp:LinkButton>                                    
                                </div>
                                <div class="buttonleft">
                                    <asp:LinkButton ID="lbtnClose" runat="server" OnClientClick="javascript: CloseClosePrintConditionWindow(); return false;"
                                        CssClass="gray_bt">
                                        <span>
                                            <asp:Localize runat="server" Text="Cancel" ID="locCancel"
                                                meta:resourcekey="locCancelResource1"></asp:Localize>
                                        </span>
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
        &nbsp;
    </div>
</div>
<asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="upReOptimize"
    DisplayAfter="50">
    <ProgressTemplate>
        <AjaxToolkit:AlwaysVisibleControlExtender ID="AlwaysVisibleControl" TargetControlID="updateProgress"
            runat="server" Enabled="True">
        </AjaxToolkit:AlwaysVisibleControlExtender>
        <div class="UpdateProgressBackgroundWithNoOpacity" style="position: fixed; left: 0px; top: 0px; z-index: 1000; width: 100%; height: 100%;"
            unselectable="on">
            <div style="margin: 10px 0px 10px 3px;">
                <center>
                    <uc:CreateDLP runat="server" ID="ucCreateDLP" />
                </center>
            </div>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
