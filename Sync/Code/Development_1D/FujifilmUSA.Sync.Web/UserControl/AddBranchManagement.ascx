<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="AddBranchManagement.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.AddBranchManagement" %>
<%@ Register Src="~/UserControl/AdminStatusbar.ascx" TagName="AdminStatusbar" TagPrefix="ucAdminStatusbar" %>
<%@ Register Src="~/UserControl/ValidationContainer.ascx" TagName="ValidationContainer" TagPrefix="ucValidationContainer" %>
<script src="JS/CommonFunctions.js?version=4" type="text/javascript"></script>
<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle" style="line-height: 34px;">
    <div class="head">
        <h1><asp:Localize ID="locBranchManagement" runat="server" Text="Branch Management" 
                meta:resourcekey="locBranchManagementResource1"></asp:Localize></h1>
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
                                <asp:Label ID="lblHeader" runat="server" Text="Add Branch" 
                                    meta:resourcekey="lblHeaderResource1"></asp:Label>
                            </legend>
                            <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" 
                                Visible="False" />
                            <table>
                                 <tr>
                                <td align="right">
                                     <span style="font-weight: bold;"><asp:Localize ID="locRegion" runat="server" 
                                         Text="Region" meta:resourcekey="locRegionResource1"></asp:Localize><span style="color: Red;">*</span>
                                        </span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <asp:DropDownList ID="cmbRegion" runat="server" DataValueField="Key" DataTextField="Value"
                                            CssClass="select" Width="400px" AutoPostBack="True" 
                                            OnSelectedIndexChanged="cmbRegion_SelectedIndexChanged" 
                                            meta:resourcekey="cmbRegionResource1">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvRegion" runat="server" Display="Dynamic" ControlToValidate="cmbRegion"
                                            ForeColor="Red" ValidationGroup="vgAddBranch" InitialValue="0"><ucValidationContainer:ValidationContainer ID="rfvRegionContainer" runat="server"
                                                ValidationMsg="Please select region." Size="200" />
</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize Text="Company Name" 
                                            ID="locCompanyName" runat="server" meta:resourcekey="locCompanyNameResource1"></asp:Localize><span style="color: Red;">*</span>
                                        </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;: &nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:DropDownList ID="cmbCompanyName" runat="server" Width="400px" 
                                                CssClass="select" meta:resourcekey="cmbCompanyNameResource1">
                                            </asp:DropDownList>
                                        </div>
                                        <div style="float: left">
                                            <asp:RequiredFieldValidator ID="rfvcompanyName" runat="server" Display="Dynamic"
                                                ControlToValidate="cmbCompanyName" ForeColor="Red" 
                                                ValidationGroup="vgAddBranch"><ucValidationContainer:ValidationContainer ID="rfvcCompanyContainer" runat="server"
                                                    ValidationMsg="Please select company." Size="200" />
</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize Text="Branch Name" 
                                            Id="locBranchName" runat="server" meta:resourcekey="locBranchNameResource1"></asp:Localize><span style="color: Red;">*</span> </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtBranchName" runat="server" MaxLength="50" Width="400px" 
                                                Height="19px" meta:resourcekey="txtBranchNameResource1"></asp:TextBox>
                                        </div>
                                        <div style="float: left">
                                            <asp:RequiredFieldValidator ID="rfvBranchName" runat="server" Display="Dynamic" ControlToValidate="txtBranchName"
                                                ForeColor="Red" ValidationGroup="vgAddBranch"><ucValidationContainer:ValidationContainer ID="rfvBranchContainer" runat="server"
                                                    ValidationMsg="Please provide branch name." Size="200" />
</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize Text="Location" ID="locLocation" 
                                            runat="server" meta:resourcekey="locLocationResource1"></asp:Localize><span style="color: Red;">*</span> </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtLocation" runat="server" MaxLength="50" Width="400px" 
                                                Height="19px" meta:resourcekey="txtLocationResource1"></asp:TextBox>
                                        </div>
                                        <div style="float: left">
                                            <asp:RequiredFieldValidator ID="rfvLocation" runat="server" Display="Dynamic" ControlToValidate="txtLocation"
                                                ForeColor="Red" ValidationGroup="vgAddBranch"><ucValidationContainer:ValidationContainer ID="ValidationContainer1" runat="server"
                                                    ValidationMsg="Please provide location." Size="200" />
</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                  <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize Text="Support ID" runat="server" 
                                            ID="locSupportId" meta:resourcekey="locSupportIdResource1"></asp:Localize></span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtSupportID" runat="server" MaxLength="30" Width="400px" 
                                                Height="19px" meta:resourcekey="txtSupportIDResource1"></asp:TextBox>
                                        </div>
                                        <div style="float: left">
                                           
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize ID="locIsActive" runat="server" 
                                            Text="Is Active?" meta:resourcekey="locIsActiveResource1"></asp:Localize></span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" 
                                                meta:resourcekey="chkIsActiveResource1" />
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
                                                    <span> <asp:Localize Text="Save" ID="locSave" runat="server" 
                                                    meta:resourcekey="locSaveResource1"></asp:Localize> </span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="gray_bt" 
                                                    PostBackUrl="~/BranchManagement.aspx">
                                                    <span> <asp:Localize Text="Cancel" ID="locCancel" runat="server" 
                                                    meta:resourcekey="locCancelResource1"></asp:Localize> </span>
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
