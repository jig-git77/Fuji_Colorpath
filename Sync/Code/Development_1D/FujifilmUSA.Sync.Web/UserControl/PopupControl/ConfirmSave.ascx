<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ConfirmSave.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.ConfirmSave" %>
<%@ Import Namespace="FujifilmUSA.Sync.Business" %>
<%@ Import Namespace="FujifilmUSA.Sync.Common" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>
<%@ Register Src="~/UserControl/PopupControl/CreateDLP.ascx" TagName="CreateDLP"
    TagPrefix="uc" %>
<%@ Register TagPrefix="uc" TagName="Statusbar" Src="~/UserControl/Statusbar.ascx" %>



<script type="text/javascript" language="javascript">

    //Click on Close Button
    function CloseConfirmSaveWindow() {
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

    function CheckOptRunLimit() {
   
          if(jQuery("input[id*='hdOptRunLimit']").val() >= <%= ApplicationConfiguration.Instance.OptimizeRunLimit  %>)
                {
                    alert("You reach your limit of adding Optimization Runs, Now you have to certify this run.");
                    return false;
                }
                else
                    return true;
   }

</script>
<div id="divConfirmSave" style="display: block; margin: 3px;" class="contantbox tabcontant">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

        <!---Code for stopping cache to store in browser for this page in order to limit same error pops up everytime from cache--->
        <meta http-equiv="cache-control" content="max-age=0" />
        <meta http-equiv="cache-control" content="no-cache" />
        <meta http-equiv="expires" content="0" />
        <meta http-equiv="expires" content="Tue, 01 Jan 1980 1:00:00 GMT" />
        <meta http-equiv="pragma" content="no-cache" />
        <!---End Code--->

            <div style="padding-bottom: 3px;">
                <uc:Statusbar ID="fujiStatusbar" runat="server" Visible="False" />
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="section_top">
        &nbsp;</div>
    <div class="section_middle">
        <div class="combopart">
            <div class="header">
                <div class="content">
                    <div class="text" style="float: left">
                        <asp:Localize runat="server" Text="Are you satisfied with the results?" 
                            ID="locSatisfiedMsg" meta:resourcekey="locSatisfiedMsgResource1"></asp:Localize>
                    </div>
                    <div class="text" style="float: right">
                        <div class="buttonright" style="padding-left: 3px">
                            <asp:UpdatePanel ID="upOptimize" runat="server">
                                <ContentTemplate>
                                    <asp:LinkButton ID="lbtnOptimize" runat="server" CssClass="gray_bt" OnClick="lbtnOptimize_Click"
                                        OnClientClick="javascript: return CheckOptRunLimit();">
                                                <span> <asp:Localize runat="server" Text="No, Optimize" 
                                        ID="locNoOptimize" meta:resourcekey="locNoOptimizeResource1"></asp:Localize>
 </span>
                                    </asp:LinkButton>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="buttonright">
                            <asp:LinkButton ID="lbtnYes" runat="server" CssClass="gray_bt" 
                                OnClick="lbtnYes_Click">
                <span> <asp:Localize runat="server" Text="Yes, Certify" ID="locYesCertify" 
                                meta:resourcekey="locYesCertifyResource1"></asp:Localize> 
 </span>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
            <div style="border-style: None; padding: 5px">
                <div style="float: left; line-height: 24px">
                    <table cellpadding="0" cellspacing="0">
                        <tr id="Tr_Certify" runat="server">
                            <td style="font-weight: bold;">
                                <asp:Localize runat="server" Text="Certify:" ID="locCertify" 
                                    meta:resourcekey="locCertifyResource1"></asp:Localize>&nbsp;
                            </td>
                            <td>
                                <p style="padding-top: 7px;">
                                    <asp:Localize runat="server" 
                                        Text="Certify sets the state of this DLP to Certified and is considered a confirmation that your are satisfied with the colormatch." 
                                        ID="locSatisfiedColorMatchMsg" 
                                        meta:resourcekey="locSatisfiedColorMatchMsgResource1"  Visible="false"></asp:Localize>
                                    
                                    <asp:Localize runat="server" 
                                        Text="Certify sets the state of this NCP to Certified and is considered a confirmation that your are satisfied with the colormatch." 
                                        ID="locSatisfiedColorMatchMsgNCP" 
                                        meta:resourcekey="locSatisfiedColorMatchMsgResource1NCP" Visible="false"></asp:Localize>
                                </p>
                            </td>
                        </tr>
                        <tr id="Tr_Optimize" runat="server">
                            <td style="font-weight: bold;">
                                <asp:Localize runat="server" Text="Optimize:" ID="locOptimize" 
                                    meta:resourcekey="locOptimizeResource1"></asp:Localize>&nbsp;
                            </td>
                            <td>
                                <p style="padding-top: 7px;">
                                    <asp:Localize runat="server" 
                                        Text="Optimize creates new optimized DLP which should be implemented in your workflow." 
                                        ID="locOptimizeDLP" meta:resourcekey="locOptimizeDLPResource1"  Visible="false"></asp:Localize>

                                    <asp:Localize runat="server" 
                                        Text="Optimize creates new optimized NCP which should be implemented in your workflow." 
                                        ID="locOptimizeNCP" meta:resourcekey="locOptimizeDLPResource1NCP"  Visible="false"></asp:Localize>
                                </p>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="buttonright" style="padding-left: 3px">
                    <asp:LinkButton ID="lbtnClose" runat="server" OnClientClick="javascript: CloseConfirmSaveWindow(); return true;"
                        CssClass="gray_bt">
                                        <span> <asp:Localize runat="server" Text="Cancel" 
                        ID="locCancel" meta:resourcekey="locCancelResource1"></asp:Localize> 
 </span>
                    </asp:LinkButton>
                </div>
                <div class="clear">
                    <input id="Hidden_ProjectName" type="hidden" runat="server" value="" />
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
    <div class="section_bottom">
        &nbsp;</div>
</div>
<asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="upOptimize"
    DisplayAfter="50">
    <ProgressTemplate>
        <AjaxToolkit:AlwaysVisibleControlExtender ID="AlwaysVisibleControl" 
            TargetControlID="updateProgress" runat="server" Enabled="True">
        </AjaxToolkit:AlwaysVisibleControlExtender>
        <div class="UpdateProgressBackgroundWithNoOpacity" style="position: fixed; left: 0px;
            top: 0px; z-index: 1000; width: 100%; height: 100%;" unselectable="on">
            <div style="margin: 10px 0px 10px 3px;">
                <uc:CreateDLP runat="server" ID="ucCreateDLP" />
            </div>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
<asp:HiddenField ID="hdOptRunLimit" runat="server" Value="0" />
