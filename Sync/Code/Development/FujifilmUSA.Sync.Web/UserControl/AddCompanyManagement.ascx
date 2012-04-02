<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddCompanyManagement.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.AddCompanyManagement" %>
<%@ Register Src="~/UserControl/AdminStatusbar.ascx" TagName="AdminStatusbar" TagPrefix="ucAdminStatusbar" %>
<style type="text/css" >
#inner {
	width:200px;
	float:left;
	margin-right:2%;
	border:1px solid #7AB6A1;
	background:#FFF;
	padding: 3px;
	font: 12px Verdana, Arial, Helvetica, sans-serif;
    border-radius: 3px 3px 3px 3px;
	-moz-border-radius: 3px 3px 3px 3px;
}
</style>

<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle" style="line-height: 34px;">
    <div class="head">
        <h1>
            Company Management</h1>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px;">
        <center>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <center>
                        <fieldset style="width: 60%;">
                            <legend> <asp:Label ID="lblHeader" runat ="server" Text ="Add Company" ></asp:Label></legend>
                            <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" Visible="false" />
                            <table cellpadding="0" cellspacing="0" border="0" width="97%">
                                <tr id="trListHeader" runat="server">
                                    <td>
                                        <asp:Panel ID="panelAddProjectDescription" runat="server" BorderStyle="None">
                                            <table>
                                                <tr>
                                                    <td align="right">
                                                        <span style="font-weight: bold">Company Name<span style="color: Red;">*</span> </span>
                                                    </td>
                                                    <td>
                                                        <b>&nbsp;:&nbsp;</b>
                                                    </td>
                                                    <td align="left">
                                                        <div style="float: left;">
                                                            <asp:TextBox ID="txtCompanyName" runat="server" Width="400px" MaxLength="50" Height="19px"></asp:TextBox>
                                                        </div>
                                                        <div style="float: left">
                                                            <asp:RequiredFieldValidator ID="rfvCompanyName" runat="server" Display="Dynamic"
                                                                ControlToValidate="txtCompanyName" ForeColor="Red" ValidationGroup="vgCompanyName">
                                            <div class="tooltip_round_box" style="width: 200px;">
                                            <div class="tooltip_arrow" style="margin-top: -3px;">
                                                &nbsp;</div>
                                            <div class="tooltip_top_left">
                                            </div>
                                            <div class="tooltip_top_right">
                                            </div>
                                            <div class="tooltip_inside">
                                                Please provide company name.
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
                                                    <td align="right">
                                                        <span style="font-weight: bold">Region Name<span style="color: Red;">*</span> </span>
                                                    </td>
                                                    <td>
                                                        <b>&nbsp;:&nbsp;</b>
                                                    </td>
                                                    <td align="left">
                                                        <div style="float: left;">
                                                            <asp:DropDownList ID="cmbRegionName" runat="server" Width="200px" CssClass="select">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div style="float: left">
                                                            <asp:RequiredFieldValidator ID="rfvRegionName" runat="server" Display="Dynamic" ControlToValidate="cmbRegionName"
                                                                ForeColor="Red" ValidationGroup="vgCompanyName">
                                            <div class="tooltip_round_box" style="width: 200px;">
                                            <div class="tooltip_arrow" style="margin-top: -3px;">
                                                &nbsp;</div>
                                            <div class="tooltip_top_left">
                                            </div>
                                            <div class="tooltip_top_right">
                                            </div>
                                            <div class="tooltip_inside">
                                                Please select region.
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
                                                    <td align="right">
                                                        <span style="font-weight: bold">Is Active? </span>
                                                    </td>
                                                    <td>
                                                        <b>&nbsp;:&nbsp;</b>
                                                    </td>
                                                    <td align="left">
                                                        <div style="float: left;">
                                                            <asp:CheckBox ID="chkIsActive" runat="server" />
                                                        </div>
                                                        <div style="float: left">
                                                        </div>
                                                        <div class="clear">
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td align="left">
                                                        <div class="text">
                                                            <div style="padding-left: 3px;">
                                                                <asp:LinkButton ID="lbtnSave" runat="server" CssClass="gray_bt" ValidationGroup="vgCompanyName"
                                                                    OnClick="lbtnSave_Click">
                                                    <span> Save </span>
                                                                </asp:LinkButton>
                                                                <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="gray_bt" PostBackUrl="~/CompanyManagement.aspx">
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
                        </fieldset>
                    </center>
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
</div>
<div class="admin_bottom">
    &nbsp;</div>
<div class="clear">
</div>
