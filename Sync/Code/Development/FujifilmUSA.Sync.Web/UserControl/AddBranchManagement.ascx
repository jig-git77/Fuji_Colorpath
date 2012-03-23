<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddBranchManagement.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.AddBranchManagement" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>

<script type="text/javascript" language="javascript">
    $(document).ready(function () {

        $('#DropDowlList1').val(1)

    });
</script>

<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle" style="line-height: 34px;">
    <div class="head">
        <h1>
            Add Branch Management</h1>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px;">
       <asp:UpdatePanel runat="server">
       <ContentTemplate>
        <ucStatusbar:Statusbar ID="ucStatusbar" runat="server" Visible="false" />
        <table cellpadding="0" cellspacing="0" border="0" width="97%">
            <tr id="trListHeader" runat="server">
                <td>
                    <asp:Panel ID="panelAddProjectDescription" runat="server" BorderStyle="None">
                        <table>
                            <tr>
                                <td>
                                    <span style="font-weight:bold;">Company Name: &nbsp<span style="color: Red;">*</span> </span>
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <asp:DropDownList ID="cmbCompanyName" runat="server"  Width="400px" CssClass="select">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvCompnayName" runat="server" Display="Dynamic"
                                            ControlToValidate="cmbCompanyName" ForeColor="Red"  ValidationGroup="vgAddBranch">
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
                                <td style="width: 130px">
                                    <span style="font-weight:bold;">Branch Name: &nbsp<span style="color: Red;">*</span> </span>
                                </td>
                                <td style="width: 400px">
                                    <div style="float: left;">
                                        <asp:TextBox ID="txtBranchName" runat="server" MaxLength="50" Width="400px" Height="19px" ></asp:TextBox>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvBranchName" runat="server" Display="Dynamic"
                                            ControlToValidate="txtBranchName" ForeColor="Red" ValidationGroup="vgAddBranch">
                                            <div class="tooltip_round_box" style="width: 200px;">
                                            <div class="tooltip_arrow" style="margin-top: -3px;">
                                                &nbsp;</div>
                                            <div class="tooltip_top_left">
                                            </div>
                                            <div class="tooltip_top_right">
                                            </div>
                                            <div class="tooltip_inside">
                                                Please provide branch name.
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
                                    <span style="font-weight:bold;">Location: &nbsp<span style="color: Red;">*</span> </span>
                                </td>
                                <td style="width: 400px">
                                    <div style="float: left;">
                                        <asp:TextBox ID="txtLocation" runat="server" MaxLength="50" Width="400px" Height="19px" ></asp:TextBox>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvLocation" runat="server" Display="Dynamic"
                                            ControlToValidate="txtLocation" ForeColor="Red" ValidationGroup="vgAddBranch">
                                            <div class="tooltip_round_box" style="width: 200px;">
                                            <div class="tooltip_arrow" style="margin-top: -3px;">
                                                &nbsp;</div>
                                            <div class="tooltip_top_left">
                                            </div>
                                            <div class="tooltip_top_right">
                                            </div>
                                            <div class="tooltip_inside">
                                                Please provide location.
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
                                    <span style="font-weight:bold;">Is Active ?: &nbsp &nbsp</span>
                                </td>
                                <td style="width: 400px">
                                    <div style="float: left;">
                                        <asp:CheckBox ID="chkIsActive" runat ="server" />
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                        <div class="text" >
                                            <div style="padding-left: 3px;">
                                                <asp:LinkButton ID="lbtnSave" 
                                                    runat="server" 
                                                    CssClass="gray_bt"
                                                    ValidationGroup="vgAddBranch" onclick="lbtnSave_Click" 
                                                    >
                                                    <span> Save </span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="gray_bt" PostBackUrl="~/BranchManagement.aspx">
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
        </asp:UpdatePanel>
    </div>
</div>
<div class="admin_bottom">
    &nbsp;</div>
<div class="clear">
</div>
