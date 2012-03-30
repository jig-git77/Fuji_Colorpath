<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddSourceProfile.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.AddSourceProfile" %>
<%@ Register Src="~/UserControl/AdminStatusbar.ascx" TagName="AdminStatusbar" TagPrefix="ucAdminStatusbar" %>
<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle" style="line-height: 34px;">
    <div class="head">
        <h1>
            Add Source Profile</h1>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="conditional">
            <ContentTemplate>
                <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" Visible="false" />
                <table cellpadding="0" cellspacing="0" border="0" width="97%">
                    <tr id="trListHeader" runat="server">
                        <td>
                            <asp:Panel ID="panelAddProjectDescription" runat="server" BorderStyle="None">
                                <table>
                                    <tr>
                                        <td>
                                            <span>Company Name: &nbsp<span style="color: Red;">*</span> </span>
                                        </td>
                                        <td>
                                            <div style="float: left;">
                                                <asp:DropDownList ID="cmbCompanyName" runat="server" DataValueField="Key" DataTextField="Value"
                                                    OnSelectedIndexChanged="cmbCompanyName_SelectedIndexChanged" AutoPostBack="true"
                                                    CssClass="select" Width="200px">
                                                </asp:DropDownList>
                                            </div>
                                            <div style="float: left">
                                                <asp:RequiredFieldValidator ID="rfvCompnayName" runat="server" Display="Dynamic"
                                                    ControlToValidate="cmbCompanyName" ForeColor="Red" ValidationGroup="vgSourceProfile">
                                            <div class="tooltip_round_box" style="width: 200px;">
                                            <div class="tooltip_arrow" style="margin-top: -3px;">
                                                &nbsp;</div>
                                            <div class="tooltip_top_left">
                                            </div>
                                            <div class="tooltip_top_right">
                                            </div>
                                            <div class="tooltip_inside">
                                                Please select compnay.
                                            </div>
                                            <div class="tooltip_bottom_left">
                                            </div>
                                            <div class="tooltip_bottom_right">
                                            </div>
                                        </div>
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="clear">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span>Region: &nbsp<span style="color: Red;">*</span> </span>
                                        </td>
                                        <td>
                                            <div style="float: left;">
                                                <asp:DropDownList ID="cmbRegion" runat="server" DataValueField="Key" DataTextField="Value"
                                                    CssClass="select" Width="200px">
                                                </asp:DropDownList>
                                            </div>
                                            <div style="float: left">
                                                <asp:RequiredFieldValidator ID="rfvRegion" runat="server" Display="Dynamic" ControlToValidate="cmbRegion"
                                                    ForeColor="Red" ValidationGroup="vgSourceProfile">
                                            <div class="tooltip_round_box" style="width: 200px;">
                                            <div class="tooltip_arrow" style="margin-top: -3px;">
                                                &nbsp;</div>
                                            <div class="tooltip_top_left">
                                            </div>
                                            <div class="tooltip_top_right">
                                            </div>
                                            <div class="tooltip_inside">
                                                Please select compnay.
                                            </div>
                                            <div class="tooltip_bottom_left">
                                            </div>
                                            <div class="tooltip_bottom_right">
                                            </div>
                                        </div>
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="clear">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span>Profile Name: &nbsp<span style="color: Red;">*</span> </span>
                                        </td>
                                        <td>
                                            <div style="float: left;">
                                                <asp:TextBox ID="txtSourceProfile" runat="server" Width="200px" Height="19px"></asp:TextBox>
                                                <asp:HiddenField ID="hdnSourceProfileID" runat="server" Value="0" />
                                            </div>
                                            <div style="float: left">
                                                <asp:RequiredFieldValidator ID="rfvSourceProfile" runat="server" Display="Dynamic"
                                                    ControlToValidate="txtSourceProfile" ForeColor="Red" ValidationGroup="vgSourceProfile">
                                            <div class="tooltip_round_box" style="width: 200px;">
                                            <div class="tooltip_arrow" style="margin-top: -3px;">
                                                &nbsp;</div>
                                            <div class="tooltip_top_left">
                                            </div>
                                            <div class="tooltip_top_right">
                                            </div>
                                            <div class="tooltip_inside">
                                                Please Source Profile.
                                            </div>
                                            <div class="tooltip_bottom_left">
                                            </div>
                                            <div class="tooltip_bottom_right">
                                            </div>
                                        </div>
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="clear">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span>Profile Description:</span>
                                        </td>
                                        <td>
                                            <div style="float: left;">
                                                <asp:TextBox ID="txtProfileDescription" runat="server" Width="300px" Height="50px"
                                                    TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                            <div style="float: left">
                                            </div>
                                            <div class="clear">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 130px">
                                            <span>Profile: &nbsp<span style="color: Red;">*</span> </span>
                                        </td>
                                        <td style="width: 400px">
                                            <div style="float: left;">
                                                <asp:FileUpload ID="fuProfile" runat="server" />
                                            </div>
                                            <div style="float: left">
                                                <asp:RequiredFieldValidator ID="rfvProfile" runat="server" Display="Dynamic" ControlToValidate="fuProfile"
                                                    ForeColor="Red" ValidationGroup="vgSourceProfile">
                                            <div class="tooltip_round_box" style="width: 200px;">
                                            <div class="tooltip_arrow" style="margin-top: -3px;">
                                                &nbsp;</div>
                                            <div class="tooltip_top_left">
                                            </div>
                                            <div class="tooltip_top_right">
                                            </div>
                                            <div class="tooltip_inside">
                                                Please Profile File.
                                            </div>
                                            <div class="tooltip_bottom_left">
                                            </div>
                                            <div class="tooltip_bottom_right">
                                            </div>
                                        </div>
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="clear">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 130px">
                                            <span>Is Used ?: &nbsp &nbsp</span>
                                        </td>
                                        <td style="width: 400px">
                                            <div style="float: left;">
                                                <asp:CheckBox ID="chkIsActive" runat="server" />
                                            </div>
                                            <div class="clear">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <div class="text">
                                                <div style="padding-left: 3px;">
                                                    <asp:LinkButton ID="lbtnSave" runat="server" CssClass="gray_bt" ValidationGroup="vgSourceProfile"
                                                        OnClick="lbtnSave_Click">
                                                    <span> Save </span>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="gray_bt" PostBackUrl="~/SourceProfileManagement.aspx">
                                                    <span> Cancel </span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="clear">
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="lbtnSave" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</div>
<div class="admin_bottom">
    &nbsp;</div>
<div class="clear">
</div>
