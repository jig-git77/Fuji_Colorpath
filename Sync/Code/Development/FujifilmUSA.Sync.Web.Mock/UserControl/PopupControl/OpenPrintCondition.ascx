<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OpenPrintCondition.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.OpenPrintCondition" %>
<div>
    <div class="top">
        &nbsp;</div>
    <div class="OpenPrintCondition">
        <table>
            <tr>
                <td>
                    <div>
                        <table>
                            <tr>
                                <td valign="top" style=" padding-top:10px; width: 50px">
                                    <%--<asp:Image ID="imgAdd" runat="server" ImageUrl="../../Images/error.png" />--%>
                                </td>
                                <td class="Content"><asp:Label ID="lblOpenPrintConditionMessage" runat="server"></asp:Label>
                                </td>    
                            </tr>
                        </table>
                    </div>
                    <div class="clear">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="buttonright">
                        <asp:LinkButton ID="lbtnClose" runat="server" 
                        OnClientClick="javascript: CloseOpenPrintConditionWindow(); return false;"
                            CssClass="gray_bt">
                                        <span>
                                            Close </span>
                        </asp:LinkButton>
                    </div>

                    <div class="buttonright">
                        <asp:LinkButton ID="lbtnContinue" runat="server" CssClass="gray_bt" 
                            OnClick="lbtnContinue_Click">
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
