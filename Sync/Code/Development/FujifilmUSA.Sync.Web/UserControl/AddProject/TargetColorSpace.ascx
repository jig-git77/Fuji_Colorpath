<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TargetColorSpace.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.TargetColorSpace" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<div class="head">
    <h1 id="hTitle" runat="server">
        New Print Condition - Target Colorspace
    </h1>
    <asp:LinkButton ID="lnkbtnTargetColorSpace" runat="server" CssClass="button" OnClick="lnkbtnTargetColorSpace_Click">
        <span>
            <asp:Image ID="imgAdd" runat="server" ImageUrl="~/Images/new_project_icon.png" />
            Profiler Options</span></asp:LinkButton>
</div>
<div class="formDeviceNoLine" style="display: none;">
    <ucStatusbar:Statusbar ID="ucStatusbar1" runat="server" Visible="false" />
    <div class="clear">
    </div>
</div>
<div class="listDevice">
    <table cellpadding="0" cellspacing="0" border="0" width="97%">
        <tr id="trListHeader" runat="server">
            <td>
                <asp:Panel ID="panelAddProjectDescription" runat="server" BorderStyle="None">
                    <table>
                        <tr>
                            <td style="width: 130px">
                                <span>Source Profile:</span>
                            </td>
                            <td style="width: 450px">
                                <asp:DropDownList ID="cmbSourceProfile" runat="server" Width="200px" CssClass="select"
                                    DataValueField="Key" DataTextField="Value" OnSelectedIndexChanged="cmbSourceProfile_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" colspan="2">
                                <span>Profile Description:</span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:TextBox ID="txtProfileDescription" runat="server" Width="115%" Height="200px"
                                    TextMode="MultiLine" Enabled="false" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</div>
