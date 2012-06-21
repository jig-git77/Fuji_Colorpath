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
             <table cellpadding="0" cellspacing="0" style="margin-left:8px;">
              <tr style="height:10px;"></tr>
                 <tr align="center">
                    <td>
                        <asp:LinkButton ID="lbtnAligmentCurves" runat="server" CssClass="disable_big_bt" OnClick="lbtn_Click" Width="250px">
                            <span id="spanAligmentCurves" runat="server" style="padding-left: 12px;"></span>
                        </asp:LinkButton>
                         <input id="Hidden_AligmentCurves" type="hidden" runat="server" value="0"/>
                     </td>
                </tr>
                <tr style="height:10px;"></tr>
                <tr align="center">
                    <td>
                        <asp:LinkButton ID="lbtnDLPOptimization" runat="server" CssClass="disable_big_bt" OnClick="lbtn_Click" Width="250px">
                            <span id="spanDLPOptimization" runat="server" style="padding-left: 12px;"></span>
                        </asp:LinkButton>
                         <input id="Hidden_DLPOptimization" type="hidden" runat="server" value="0"/>
                     </td>
                </tr>
                <tr style="height:10px;"></tr>
                <tr align="center">
                    <td>
                        <asp:LinkButton ID="lbtnInkOptimization" runat="server" CssClass="disable_big_bt" OnClick="lbtn_Click" Width="250px">
                            <span id="spanInkOptimization" runat="server" style="padding-left: 12px;"></span>
                        </asp:LinkButton>
                         <input id="Hidden_InkOptimization" type="hidden" runat="server" value="0"/>
                    </td>
                 </tr>
                 <tr style="height:10px;"></tr>
                 <tr align="center">                 
                    <td>
                        <asp:LinkButton ID="lbtnColorValidation" runat="server" CssClass="disable_big_bt" OnClick="lbtn_Click" Width="250px">
                            <span id="spanColorValidation" runat="server" style="padding-left: 12px;"></span>
                        </asp:LinkButton>
                         <input id="Hidden_ColorValidation" type="hidden" runat="server" value="0"/>         
                    </td>
                </tr>
                <tr style="height:10px;"></tr>
                <tr align="center">
                    <td>
                        <asp:LinkButton ID="lbtnICCProfiling" runat="server" CssClass="disable_big_bt" OnClick="lbtn_Click" Width="250px">
                            <span id="spanICCProfiling" runat="server" style="padding-left: 12px;"></span>
                        </asp:LinkButton>
                         <input id="Hidden_ICCProfiling" type="hidden" runat="server" value="0"/>         
                   </td>
                </tr>
                <tr style="height:10px;"></tr>
            </table>               

    </div>
       <%--<div style="margin-top: -17px;  background: url(../images/contentbox2.png) no-repeat top left; margin-left:200px;">
           
             <table cellpadding="0" cellspacing="0"  style="margin-left:65px;">
             <tr style="height:125px;"></tr>

            
                 <tr>
                    <td>
                        <asp:LinkButton ID="LinkButton2" runat="server" CssClass="Disable_Opt" Width="250px" Enabled="false">
                            <span id="span1" runat="server" style="padding-left: 12px; ">
                                Alignment Curves
                            </span>
                        </asp:LinkButton>
                         <input id="Hidden1" type="hidden" runat="server" value="0"/>
                     </td>
                </tr>
                <tr style="height:10px;"></tr>
                <tr>
                    <td>
                        <asp:LinkButton ID="LinkButton3" runat="server" CssClass="Enable_Opt" Width="250px">
                            <span id="span2" runat="server" style="padding-left: 12px; ">
                                DLP Optimization
                            </span>
                        </asp:LinkButton>
                         <input id="Hidden2" type="hidden" runat="server" value="0"/>
                     </td>
                </tr>
                <tr style="height:7px;"></tr>
                <tr>
                    <td>
                        <asp:LinkButton ID="LinkButton4" runat="server" CssClass="Enable_Opt" Width="250px">
                            <span id="span3" runat="server" style="padding-left: 12px;">
                                Ink Optimization
                            </span>
                        </asp:LinkButton>
                         <input id="Hidden3" type="hidden" runat="server" value="0"/>
                    </td>
                 </tr>
                 <tr style="height:4px;"></tr>
                 <tr>                 
                    <td>
                        <asp:LinkButton ID="LinkButton5" runat="server" CssClass="Disable_Opt" Width="250px" Enabled="false">
                            <span id="span4" runat="server" style="padding-left: 12px; ">
                                Color Validation
                            </span>
                        </asp:LinkButton>
                         <input id="Hidden4" type="hidden" runat="server" value="0"/>         
                    </td>
                </tr>
                <tr style="height:10px;"></tr>
                <tr>
                    <td>
                        <asp:LinkButton ID="LinkButton6" runat="server" CssClass="Disable_Opt" Width="250px" Enabled="false">
                            <span id="span5" runat="server" style="padding-left: 12px;">
                                ICC Profiling
                            </span>
                        </asp:LinkButton>
                         <input id="Hidden5" type="hidden" runat="server" value="0"/>         
                   </td>
                </tr>
                <tr style="height:10px;"></tr>
            </table>               
       </div>--%>
    </div>
    <div class="clear">
    </div>
</div>
<div class="contant_bottom">
    &nbsp;</div>
