<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OperationSelection.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.OperationSelection" %>
 
       <!--pop up start-->
<div class="popup">
  <ul>
        <asp:Label ID="lbl_Title" runat="server" Text="Label" Visible="false"></asp:Label>       
    <li class="first">
        <asp:LinkButton ID="lbtAligmentCurves" runat="server" 
                            CssClass="disable_Popup_bt" OnClientClick="return false;" >
                            <span id="spanAligmentCurves" runat="server" style="padding-left:12px;">
                                  Alignment Curves
                            </span>
                        </asp:LinkButton>
                        <input id="Hidden_AligmentCurves" type="hidden" runat="server" value="0"/>
    </li>
    <li>
        <asp:LinkButton ID="lbtnDLPOptimization" runat="server" 
                            CssClass="disable_Popup_bt" OnClick="lbtnDLPOptimization_Click"  OnClientClick="return false;">
                             <span id="spanDLPOptimization" runat="server" style="padding-left:12px;">
                                DLP Optimization
                            </span>
                        </asp:LinkButton>
                        <input id="Hidden_DLPOptimization" type="hidden" runat="server" value="0"/>
    </li>
    <li>
        <asp:LinkButton ID="lbtInkOptimization" runat="server" 
                            CssClass="disable_Popup_bt" onclick="lbtInkOptimization_Click" OnClientClick="return false;">
                             <span id="spanInkOptimization" runat="server" style="padding-left:12px;">
                                Ink Optimization
                            </span>
                        </asp:LinkButton>
                        <input id="Hidden_InkOptimization" type="hidden" runat="server" value="0"/>
    </li>
    <li>
        <asp:LinkButton ID="lbtnColorValidation" runat="server"
                        CssClass="disable_Popup_bt" OnClientClick="return false;" >
                            <span id="spanColorValidation" runat="server" style="padding-left:12px;">
                            Color Validation
                            </span>
                        </asp:LinkButton>       
                        <input id="Hidden_ColorValidation" type="hidden" runat="server" value="0"/>         
    </li>
    <li class="last">
         <asp:LinkButton ID="lbtnICCProfiling" runat="server"
                        CssClass="disable_Popup_bt" OnClientClick="return false;" >
                            <span id="spanICCProfiling" runat="server" style="padding-left:12px;">
                            ICC Profiling
                            </span>
                        </asp:LinkButton>       
                        <input id="Hidden_ICCProfiling" type="hidden" runat="server" value="0"/>       
    </li>
  </ul>
</div>

   
