<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddBranchManagement.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.AddBranchManagement" %>
<%@ Register Src="~/UserControl/AdminStatusbar.ascx" TagName="AdminStatusbar" TagPrefix="ucAdminStatusbar" %>
<%@ Register Src="~/UserControl/ValidationContainer.ascx" TagName="ValidationContainer" TagPrefix="ucValidationContainer" %>
<script src="JS/CommonFunctions.js?version=4" type="text/javascript"></script>
<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle" style="line-height: 34px;">
    <div class="head">
        <h1>
            Branch Management</h1>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px;">
        <center>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <center>
                        <fieldset style="width: 60%;">
                            <legend>
                                <asp:Label ID="lblHeader" runat="server" Text="Add Branch"></asp:Label>
                            </legend>
                            <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" Visible="false" />
                            <table>
                                 <tr>
                                <td align="right">
                                     <span style="font-weight: bold;">Region<span style="color: Red;">*</span>
                                        </span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <asp:DropDownList ID="cmbRegion" runat="server" DataValueField="Key" DataTextField="Value"
                                            CssClass="select" Width="400px" AutoPostBack="true" OnSelectedIndexChanged="cmbRegion_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvRegion" runat="server" Display="Dynamic" ControlToValidate="cmbRegion"
                                            ForeColor="Red" ValidationGroup="vgAddBranch" InitialValue="0">
                                            <ucValidationContainer:ValidationContainer ID="rfvRegionContainer" runat="server"
                                                ValidationMsg="Please select region." Size="200" />     
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;">Company Name<span style="color: Red;">*</span>
                                        </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;: &nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:DropDownList ID="cmbCompanyName" runat="server" Width="400px" CssClass="select">
                                            </asp:DropDownList>
                                        </div>
                                        <div style="float: left">
                                            <asp:RequiredFieldValidator ID="rfvcompanyName" runat="server" Display="Dynamic"
                                                ControlToValidate="cmbCompanyName" ForeColor="Red" ValidationGroup="vgAddBranch">
                                                <ucValidationContainer:ValidationContainer ID="rfvcCompanyContainer" runat="server"
                                                    ValidationMsg="Please select company." Size="200" /></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;">Branch Name<span style="color: Red;">*</span> </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtBranchName" runat="server" MaxLength="50" Width="400px" Height="19px"></asp:TextBox>
                                        </div>
                                        <div style="float: left">
                                            <asp:RequiredFieldValidator ID="rfvBranchName" runat="server" Display="Dynamic" ControlToValidate="txtBranchName"
                                                ForeColor="Red" ValidationGroup="vgAddBranch">
                                                <ucValidationContainer:ValidationContainer ID="rfvBranchContainer" runat="server"
                                                    ValidationMsg="Please provide branch name." Size="200" /> </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;">Location<span style="color: Red;">*</span> </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtLocation" runat="server" MaxLength="50" Width="400px" Height="19px"></asp:TextBox>
                                        </div>
                                        <div style="float: left">
                                            <asp:RequiredFieldValidator ID="rfvLocation" runat="server" Display="Dynamic" ControlToValidate="txtLocation"
                                                ForeColor="Red" ValidationGroup="vgAddBranch">
                                                <ucValidationContainer:ValidationContainer ID="ValidationContainer1" runat="server"
                                                    ValidationMsg="Please provide location." Size="200" /> </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                  <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;">Support ID</span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtSupportID" runat="server" MaxLength="30" Width="400px" 
                                                Height="19px"></asp:TextBox>
                                        </div>
                                        <div style="float: left">
                                           
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;">Is Active?</span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:CheckBox ID="chkIsActive" runat="server" Checked="true" />
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
                                                <asp:LinkButton ID="lbtnSave" runat="server" CssClass="gray_bt" ValidationGroup="vgAddBranch"
                                                    OnClick="lbtnSave_Click">
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
