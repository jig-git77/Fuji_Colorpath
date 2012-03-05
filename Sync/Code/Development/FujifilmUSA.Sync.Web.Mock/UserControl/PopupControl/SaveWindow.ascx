<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SaveWindow.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.Mock.UserControl.SaveWindow" %>
<div class="listDevice" style="margin-left:10px">
    <div class="OpenPrintCondition">
        <table cellpadding="0" class="product_list_table" cellspacing="0" width="100%">
            <tr>
                <td>
                    <table>
                        <tr>
                            <td colspan="2">
                                1. Name Print Condition
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Print Condition Name:
                            </td>
                            <td>
                                <asp:TextBox ID="txtProjectName" runat="server" Width="200px" Height="19px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td colspan="2">
                                2. Download Chart
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                Download chart and print chart through a workflow that has no colormanagement applied.
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:LinkButton ID="lbtnTargetColorSpace" runat="server" CssClass="gray_bt">
                                        <span>DawnLoad Chart</span>
                                </asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbtnFileName" runat="server">
                                        <span>Sync 500</span>
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="buttonright">
                        <asp:LinkButton ID="lbtnClose" runat="server" OnClientClick="javascript: Close();"
                            CssClass="gray_bt">
                                        <span>
                                            Close </span>
                        </asp:LinkButton>
                    </div>
                    <div class="buttonright">
                        <asp:LinkButton ID="lbtnContinue" runat="server" CssClass="gray_bt">
                <span> Continue </span>
                        </asp:LinkButton>
                    </div>
                    <div class="clear">
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div class="bottom">
        &nbsp;</div>
</div>
