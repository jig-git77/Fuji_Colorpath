<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfirmSave.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.ConfirmSave" %>
<%@ Import Namespace="FujifilmUSA.Sync.Business" %>
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
         if(jQuery("input[id*='hdOptRunLimit']").val() >= <%= ApplicationConfiguration.Instance.OptimizeRunLimit %>)
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
            <div style="padding-bottom: 3px;">
                <uc:Statusbar ID="fujiStatusbar" runat="server" Visible="false" />
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
                        Are you satisfied with the results?
                    </div>
                    <div class="text" style="float: right">
                        <div class="buttonright" style="padding-left: 3px">
                            <asp:UpdatePanel ID="upOptimize" runat="server">
                                <ContentTemplate>
                                    <asp:LinkButton ID="lbtnOptimize" runat="server" CssClass="gray_bt" OnClick="lbtnOptimize_Click"
                                        OnClientClick="javascript: return CheckOptRunLimit();">
                                                <span> No, Optimize </span>
                                    </asp:LinkButton>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="buttonright">
                            <asp:LinkButton ID="lbtnYes" runat="server" CssClass="gray_bt" OnClick="lbtnYes_Click">
                <span> Yes, Certify </span>
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
                        <tr>
                            <td style="font-weight: bold;">
                                Certify:&nbsp;
                            </td>
                            <td>
                                <p style="padding-top: 7px;">
                                    Certify sets the state of this DLP to Certified and is considered a confirmation
                                    that your are satisfied with the colormatch.
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold;">
                                Optimize:&nbsp;
                            </td>
                            <td>
                                <p style="padding-top: 7px;">
                                    Optimize creates new optimized DLP which should be implemented in your workflow.
                                </p>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="buttonright" style="padding-left: 3px">
                    <asp:LinkButton ID="lbtnClose" runat="server" OnClientClick="javascript: CloseConfirmSaveWindow(); return true;"
                        CssClass="gray_bt">
                                        <span>
                                            Cancel </span>
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
<asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true" AssociatedUpdatePanelID="upOptimize"
    DisplayAfter="50">
    <ProgressTemplate>
        <AjaxToolkit:AlwaysVisibleControlExtender ID="AlwaysVisibleControl" HorizontalSide="Left"
            VerticalSide="Top" TargetControlID="updateProgress" runat="server">
        </AjaxToolkit:AlwaysVisibleControlExtender>
        <div class="UpdateProgressBackgroundWithNoOpacity" style="position: fixed; left: 0px;
            top: 0px; z-index: 1000; width: 100%; height: 100%;" unselectable="on">
            <div style="margin: 10px 0px 10px 3px;">
                <uc:CreateDLP runat="server" ID="ucCreateDLP" DLPMessage='<%# DLPMessage %>' />
            </div>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
<asp:HiddenField ID="hdOptRunLimit" runat="server" Value="0" />
