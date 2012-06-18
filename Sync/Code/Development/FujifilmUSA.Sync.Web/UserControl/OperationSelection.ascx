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
      <%-- <div>
            <asp:Image ID="ImageMenu" runat="server" ImageUrl="~/Images/contentbox.png" />
       </div>--%>
    </div>
    <div class="clear">
    </div>
</div>
<div class="contant_bottom">
    &nbsp;</div>
