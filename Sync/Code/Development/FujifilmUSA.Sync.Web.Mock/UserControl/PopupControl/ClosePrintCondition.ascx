<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClosePrintCondition.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.Mock.UserControl.ClosePrintCondition" %>
<div>
    <div class="top">
        &nbsp;</div>
    <div class="OpenPrintCondition" style="margin-left:23px;">
        <div style="float:left;">
            <div class="Content">
                <asp:Label ID="lblClosePrintConditionMessage" runat="server"></asp:Label>
            </div>
        </div>
        <div style="float:left;">
            <div class="buttonleft">
                <asp:LinkButton ID="lbtnReOptimize" runat="server" CssClass="gray_bt">
                <span>Re Optimize</span>
                </asp:LinkButton>
            </div>
            <div class="buttonleft">
                <asp:LinkButton ID="lbtnDownLoadCurrentDLP" runat="server" CssClass="gray_bt">
                    <span>DownLoad Current DLP</span>
                </asp:LinkButton>
            </div>
            <div class="buttonleft">
                <asp:LinkButton ID="lbtnViewLastAnalysis" runat="server" CssClass="gray_bt">
                <span>View Last Analysis</span>
                </asp:LinkButton>
            </div>
            <div class="clear">
            </div>
            <div>
                </br>
                <div class="buttonright">
                    <asp:LinkButton ID="lbtnClose" runat="server" OnClientClick="javascript: CloseClosePrintConditionWindow();" CssClass="gray_bt">
                        <span>Close</span>
                    </asp:LinkButton>
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
        
    </div>
    <div class="bottom">
        &nbsp;</div>
</div>
