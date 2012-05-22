<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OperationSelection.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.OperationSelection" %>
<div class="contant_top">
    &nbsp;</div>
<div class="contant_middle">
    <div class="head">
        <div>
            <table>
                <tr>
                    <td style="width: 93%">
                        <h1>
                            <asp:Label ID="lblDeviceName" runat="server"></asp:Label>
                        </h1>
                    </td>
                    <td style="width: 50px">
                        <asp:LinkButton ID="LinkButton1" runat="server"><h1>Help</h1></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
        <div class="clear">
        </div>
      
        <div class="ContainerClass">           
            <table cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr>
                    <td>
                       <asp:LinkButton ID="lbtAligmentCurves" runat="server" ValidationGroup="vgDestination"
                            CssClass="disable_big_bt" OnClientClick="return false;" >
                            <span id="spanAligmentCurves" runat="server">
                                Alignment Curves
                            </span>
                        </asp:LinkButton>
                        <input id="Hidden_AligmentCurves" type="hidden" runat="server" value="0"/>
                    </td>
                    <td>
                         <asp:LinkButton ID="lbtnDLPOptimization" runat="server" ValidationGroup="vgDestination"
                            CssClass="disable_big_bt" OnClick="lbtnDLPOptimization_Click" OnClientClick="return false;">
                             <span id="spanDLPOptimization" runat="server">
                                DLP Optimization
                            </span>
                        </asp:LinkButton>
                        <input id="Hidden_DLPOptimization" type="hidden" runat="server" value="0"/>
                    </td>
                    <td>
                        <asp:LinkButton ID="lbtInkOptimization" runat="server" ValidationGroup="vgDestination"
                            CssClass="disable_big_bt" onclick="lbtInkOptimization_Click" OnClientClick="return false;">
                             <span id="spanInkOptimization" runat="server">
                                Ink Optimization
                            </span>
                        </asp:LinkButton>
                        <input id="Hidden_InkOptimization" type="hidden" runat="server" value="0"/>
                    </td>
                </tr>
                 <tr align="center">
                    <td colspan="3">  
                        <br />
                    </td>
                 </tr>
                <tr>
                    <td></td>
                    <td>                       
                        <asp:LinkButton ID="lbtnColorValidation" runat="server" ValidationGroup="vgDestination"
                        CssClass="disable_big_bt" OnClientClick="return false;" >
                            <span id="spanColorValidation" runat="server">
                            Color Validation
                            </span>
                        </asp:LinkButton>       
                        <input id="Hidden_ColorValidation" type="hidden" runat="server" value="0"/>         
                    </td>
                    <td></td>
                </tr>
            </table>           
        </div>
       
    </div>
    <div class="clear">
    </div>
</div>
<div class="contant_bottom">
    &nbsp;</div>
