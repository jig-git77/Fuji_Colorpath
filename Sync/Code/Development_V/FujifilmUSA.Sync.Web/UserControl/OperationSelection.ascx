<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="OperationSelection.ascx.cs"
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
                            <asp:Label ID="lblDeviceName" runat="server" 
                                meta:resourcekey="lblDeviceNameResource2"></asp:Label>
                        </h1>
                    </td>
                    <td style="width: 50px">
                        <asp:LinkButton ID="LinkButton1" runat="server" 
                            meta:resourcekey="LinkButton1Resource1"><h1><asp:Localize ID="locHelp" 
                                runat="server" Text="Help" meta:resourcekey="locHelpResource1"></asp:Localize>
</h1></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
        <div class="clear">
        </div>
      
        <div class="ContainerClass"> 
        <br /><br /><br />
        
             <table cellpadding="0" cellspacing="0" style="margin-left:68px;">
             <tr style="height:20px;"><td></td></tr>
                 <tr>
                    <td>
                        <asp:LinkButton ID="lbtnAligmentCurves" runat="server" 
                            CssClass="disable_big_bt" OnClick="lbtn_Click" Width="250px">
                            <span id="spanAligmentCurves" runat="server" style="padding-left: 12px;"></span>
                        </asp:LinkButton>
                         <input id="Hidden_AligmentCurves" type="hidden" runat="server" value="0"/>
                     </td>
                </tr>
               <%-- <tr style="height:5px;"></tr>--%>
                <tr >
                    <td>
                        <asp:LinkButton ID="lbtnDLPOptimization" runat="server" 
                            CssClass="disable_big_bt" OnClick="lbtn_Click" Width="250px">
                            <span id="spanDLPOptimization" runat="server" style="padding-left: 12px;"></span>
                        </asp:LinkButton>
                         <input id="Hidden_DLPOptimization" type="hidden" runat="server" value="0"/>
                     </td>
                </tr>
               <%-- <tr style="height:3px;"></tr>--%>
                <tr >
                    <td>
                        <asp:LinkButton ID="lbtnInkOptimization" runat="server" 
                            CssClass="disable_big_bt" OnClick="lbtn_Click" Width="250px">
                            <span id="spanInkOptimization" runat="server" style="padding-left: 12px;"></span>
                        </asp:LinkButton>
                         <input id="Hidden_InkOptimization" type="hidden" runat="server" value="0"/>
                    </td>
                 </tr>
                <%-- <tr style="height:3px;"></tr>--%>
                 <tr >                 
                    <td>
                        <asp:LinkButton ID="lbtnColorValidation" runat="server" 
                            CssClass="disable_big_bt" OnClick="lbtn_Click" Width="250px">
                            <span id="spanColorValidation" runat="server" style="padding-left: 12px;"></span>
                        </asp:LinkButton>
                         <input id="Hidden_ColorValidation" type="hidden" runat="server" value="0"/>         
                    </td>
                </tr>
               <%-- <tr style="height:3px;"></tr>--%>
                <tr >
                    <td>
                        <asp:LinkButton ID="lbtnICCProfiling" runat="server" CssClass="disable_big_bt" 
                            OnClick="lbtn_Click" Width="250px">
                            <span id="spanICCProfiling" runat="server" style="padding-left: 12px;"></span>
                        </asp:LinkButton>
                         <input id="Hidden_ICCProfiling" type="hidden" runat="server" value="0"/>         
                   </td>
                </tr>
                <tr style="height:5px;"><td></td></tr>
                </table>               

    </div>
    
    </div>
    <div class="clear">
    </div>
</div>
<div class="contant_bottom">
    &nbsp;</div>
