<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OperationSelection.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.OperationSelection" %>
<!--pop up start-->
<div class="popup">
    <ul>
        <li class="first">
            <asp:LinkButton ID="lbtAligmentCurves" runat="server">
                <span id="spanAligmentCurves" runat="server" style="padding-left: 12px;">Alignment Curves
                </span>
            </asp:LinkButton>
        </li>
        <li>
            <asp:LinkButton ID="lbtnDLPOptimization" runat="server" CssClass="disable_Popup_bt"
                OnClick="lbtnDLPOptimization_Click">
                <span id="spanDLPOptimization" runat="server" style="padding-left: 12px;">DLP Optimization
                </span>
            </asp:LinkButton>
        </li>
        <li>
            <asp:LinkButton ID="lbtInkOptimization" runat="server" CssClass="disable_Popup_bt"
                OnClick="lbtInkOptimization_Click">
                <span id="spanInkOptimization" runat="server" style="padding-left: 12px;">Ink Optimization
                </span>
            </asp:LinkButton>
        </li>
        <li>
            <asp:LinkButton ID="lbtnColorValidation" runat="server" CssClass="disable_Popup_bt">
                <span id="spanColorValidation" runat="server" style="padding-left: 12px;">Color Validation
                </span>
            </asp:LinkButton>
        </li>
        <li class="last">
            <asp:LinkButton ID="lbtnICCProfiling" runat="server" CssClass="disable_Popup_bt">
                <span id="spanICCProfiling" runat="server" style="padding-left: 12px;">ICC Profiling
                </span>
            </asp:LinkButton>
        </li>
    </ul>
</div>
<input id="Hidden_AligmentCurves" type="hidden" runat="server" value="0" />
<input id="Hidden_DLPOptimization" type="hidden" runat="server" value="0" />
<input id="Hidden_InkOptimization" type="hidden" runat="server" value="0" />
<input id="Hidden_ColorValidation" type="hidden" runat="server" value="0" />
<input id="Hidden_ICCProfiling" type="hidden" runat="server" value="0" />
