<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OpenPrintCondition.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.Mock.UserControl.OpenPrintCondition" %>
<div>
    <div class="top">
        &nbsp;</div>
    <div class="OpenPrintCondition">
        <table>
            <tr>
                <td>
                    <div style="float: left">
                        <div class="divImage">
                            <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/error.png" />
                        </div>
                        <div class="Content">
                            <asp:Label ID="lblOpenPrintConditionMessage" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="buttonright">
                        <asp:LinkButton ID="lbtnClose" runat="server" OnClientClick="javascript: CloseOpenPrintConditionWindow() return flase;"
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
