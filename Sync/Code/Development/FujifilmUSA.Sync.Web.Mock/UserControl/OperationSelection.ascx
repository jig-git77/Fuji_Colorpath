<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OperationSelection.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.Mock.UserControl.OperationSelection" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
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
            <div>
                <label>Select An Option Below:</label>
                <div class="clear">
                </div>
            </div>
            <div>
                <ucStatusbar:Statusbar ID="ucStatusbar1" runat="server" Visible="false" />
                <div class="formSelection">    
                   <asp:LinkButton ID="lbtnColorValidation" runat="server" ValidationGroup="vgDestination"
                    CssClass="disable_big_bt" OnClientClick="return false;" >
                    <span>
                        <asp:Image ID="imgColorValidtion" runat="server" />
                        Color Validation </span>
                </asp:LinkButton>
                </div>
                <div class="formSelection">
                <asp:LinkButton ID="lbtnDLPOptimization" runat="server" ValidationGroup="vgDestination"
                    CssClass="big_bt" OnClick="lbtnDLPOptimization_Click">
                    <span>
                        <asp:Image ID="Image1" runat="server" />
                        DLP Optimization </span>
                </asp:LinkButton>
                </div>
                <div class="formSelection">
                <asp:LinkButton ID="lbtAligmentCurves" runat="server" ValidationGroup="vgDestination"
                    CssClass="disable_big_bt" OnClientClick="return false;" >
                    <span>
                        <asp:Image ID="Image2" runat="server" />
                        Aligment Curves </span>
                </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    <div class="clear">
    </div>
</div>
<div class="contant_bottom">
    &nbsp;</div>
