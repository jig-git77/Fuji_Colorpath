<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddBranchManagement.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.AddBranchManagement" %>
<%@ Register Src="~/UserControl/AdminStatusbar.ascx" TagName="AdminStatusbar" TagPrefix="ucAdminStatusbar" %>

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
        <center>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <center>
                        <fieldset style="width: 60%;">
                            <legend><asp:Label ID="lblHeader" runat ="server" Text ="Add Branch" ></asp:Label> </legend>
                            <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" Visible="false" />
                                <table>
                            <tr>
                                <td align="right">
                                    <span style="font-weight:bold;">Company Name<span style="color: Red;">*</span> </span>
                                </td>
                                <td> <b>&nbsp;: &nbsp;</b></td>
                                <td align="left">
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
                                <td align="right">
                                    <span style="font-weight:bold;">Branch Name: &nbsp<span style="color: Red;">*</span> </span>
                                </td>
                                <td> <b>&nbsp;:&nbsp;</b></td>
                                <td align="left">
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
                                <td align="right">
                                    <span style="font-weight:bold;">Location: &nbsp<span style="color: Red;">*</span> </span>
                                </td>
                                <td> <b>&nbsp;:&nbsp;</b></td>
                                <td align="left">
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
                                <td align="right">
                                    <span style="font-weight:bold;">Is Active ?: &nbsp &nbsp</span>
                                </td>
                                <td> <b>&nbsp;:&nbsp;</b></td>
                                <td align="left" >
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
                                <td></td>
                                <td align="left">
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
