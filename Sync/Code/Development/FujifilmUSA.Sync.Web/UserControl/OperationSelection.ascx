<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OperationSelection.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.OperationSelection" %>
<div class="popup">
    <ul>
        <li class="first">
            <asp:LinkButton ID="lbtnAligmentCurves" runat="server" CssClass="disable_Popup_bt" OnClick="lbtn_Click">
                <span id="spanAligmentCurves" runat="server" style="padding-left: 12px;"></span>
            </asp:LinkButton>
        </li>
        <li>
            <asp:LinkButton ID="lbtnDLPOptimization" runat="server" CssClass="disable_Popup_bt" OnClick="lbtn_Click">
                <span id="spanDLPOptimization" runat="server" style="padding-left: 12px;"></span>
            </asp:LinkButton>
        </li>
        <li>
            <asp:LinkButton ID="lbtnInkOptimization" runat="server" CssClass="disable_Popup_bt" OnClick="lbtn_Click">
                <span id="spanInkOptimization" runat="server" style="padding-left: 12px;"></span>
            </asp:LinkButton>
        </li>
        <li>
            <asp:LinkButton ID="lbtnColorValidation" runat="server" CssClass="disable_Popup_bt" OnClick="lbtn_Click">
                <span id="spanColorValidation" runat="server" style="padding-left: 12px;"></span>
            </asp:LinkButton>
        </li>
        <li class="last">
            <asp:LinkButton ID="lbtnICCProfiling" runat="server" CssClass="disable_Popup_bt" OnClick="lbtn_Click">
                <span id="spanICCProfiling" runat="server" style="padding-left: 12px;"></span>
            </asp:LinkButton>
        </li>
    </ul>
</div>