<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OpenUpdatePopup.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.PopupControl.OpenUpdatePopup" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<script language="javascript" type="text/javascript">
    function CloseOpenUpdatePopupWindow() {
        var oWindow = GetOpenUpdatePopupRadWindow();
        GetOpenUpdatePopupRadWindow().close();
        return false;
    }

    function GetOpenUpdatePopupRadWindow() {
        var oWindow = null;
        if (window.radWindow) oWindow = window.radWindow;
        else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
        return oWindow;
    } 

</script>
<div style="display: block; margin: 5px;" class="contantbox tabcontant">
    <div class="section_top">
        &nbsp;</div>
    <div class="section_middle">
        <div class="combopart">
            <div style="padding: 5px;" id="cotainer">
                <%--<div>
                    <ucStatusbar:Statusbar ID="fujiStatusbar" runat="server" Visible="False" />
                </div>--%>
                <div class="OpenPrintCondition">
                    <table width="650px">
                        <tr>
                            <td>
                                <div>
                                    <table width="100%" style="height:98px !important;">
                                        <tr>
                                            <td class="Content" style="line-height: 24px; width:100%;">
                                                <asp:Label ID="lblOpenPrintConditionMessage" runat="server" Text="The ColorPath team has been hard at work to provide you with the best experience possible."></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="clear">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <div class="buttonright" style="padding-left: 3px">
                                    <asp:LinkButton ID="lbtnClose" runat="server" OnClientClick="javascript: CloseOpenUpdatePopupWindow();" OnClick="lbtnClose_Click"
                                        CssClass="gray_bt">
                                        <span> <asp:Localize runat="server" Text="Cancel" ID="locCancel"></asp:Localize></span>
                                    </asp:LinkButton>
                                </div>
                                <div class="buttonright">
                                    <asp:LinkButton ID="lbtnContinue" runat="server" CssClass="gray_bt" OnClick="lbtnContinue_Click">
                                         <span> View Changes </span>
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
