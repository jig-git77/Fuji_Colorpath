<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfirmSave.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.ConfirmSave" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
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
</script>
<div style="display: block; margin: 10px;" class="contantbox tabcontant">
    <div class="section_top">
        &nbsp;</div>
    <div class="section_middle">
        <div class="combopart">
            <div class="header">
                <div class="content">
                    <div>
                        <ucStatusbar:Statusbar ID="fujiStatusbar" runat="server" Visible="false" />
                    </div>
                    <div class="text" style="float: left">
                        Are you satisfied with the results?
                    </div>
                    <div class="text" style="float: right">
                        <div class="buttonright" style="padding-left: 3px">
                            <asp:LinkButton ID="lbtnOptimize" runat="server" CssClass="gray_bt" OnClick="lbtnOptimize_Click">
                        <span> No, Optimize </span>
                            </asp:LinkButton>
                        </div>
                        <div class="buttonright">
                            <asp:LinkButton ID="lbtnYes" runat="server" CssClass="gray_bt" OnClick="lbtnYes_Click">
                <span> Yes, Close </span>
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
                                Close:&nbsp;
                            </td>
                            <td>
                                <p style="padding-top: 7px;">
                                    Close sets the state of this DLP to Closed and is considered a confirmation that
                                    your are satisfied with the colormatch.
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
