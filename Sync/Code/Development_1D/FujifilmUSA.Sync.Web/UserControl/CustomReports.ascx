<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomReports.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.CustomReports" %>
<div class="admin_top">
    &nbsp;
</div>
<div class="admin_middle">
    <div class="head">
        <h1>
            <asp:Localize runat="server" Text="Custom Reports" ID="locCustomReports"
                meta:resourcekey="locCustomReportsResource1"></asp:Localize></h1>
        <h1></h1>
    </div>
    <div class="clear">
    </div>
    <table cellspacing="0" border="0" style="width:100%">
        <tr>
            <td style="height: 50px;"></td>
        </tr>
        <tr align="center">
            <td align="center">


                <asp:LinkButton ID="lbtnReport1" runat="server">
                    <div>
                        <asp:Image ID="imgReport1" runat="server" AlternateText="DLP Report" ImageUrl="~/Images/Sync DLP Report_preview.jpg"
                            ToolTip="DLP Report" Width="150" />
                    </div>
                    <asp:Localize Text="DLP Report" ID="locReport1" runat="server" ></asp:Localize>
                </asp:LinkButton>
            </td>
            <td align="center">

                <asp:LinkButton ID="lbtnReport2" runat="server">
                    <div>
                        <asp:Image ID="imgReport2" runat="server" AlternateText="G7 Report" ImageUrl="~/Images/Sync1D Report_preview_G7.jpg"
                            ToolTip="G7 Report" Width="150" />
                    </div>
                    <asp:Localize Text="G7 Report" ID="locReport2" runat="server" ></asp:Localize>
                </asp:LinkButton>

            </td>
            <td align="center">
                <asp:LinkButton ID="lbtnReport3" runat="server">
                    <div>
                        <asp:Image ID="imgReport3" runat="server" AlternateText="Validation Report" ImageUrl="~/Images/Sync1D Report_preview_Vali.jpg"
                            ToolTip="Validation Report" Width="150" />
                    </div>
                    <asp:Localize Text="Validation Report" ID="locReport3" runat="server" ></asp:Localize>
                </asp:LinkButton>
            </td>
            <td align="center">
                <asp:LinkButton ID="lbtnReport4" runat="server">
                    <div>
                        <asp:Image ID="imgReport4" runat="server" AlternateText="Density Report" ImageUrl="~/Images/Sync1D CMYK Density Report_preview.jpg"
                            ToolTip="Density Report" Width="150" />
                    </div>
                    <asp:Localize Text="Density Report" ID="locReport4" runat="server" ></asp:Localize>
                </asp:LinkButton>
            </td>
        </tr>
    </table>
</div>
<div class="admin_bottom">
    &nbsp;
</div>
