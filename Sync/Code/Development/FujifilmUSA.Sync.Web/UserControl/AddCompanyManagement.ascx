<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddCompanyManagement.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.AddCompanyManagement" %>
<%@ Register Src="~/UserControl/AdminStatusbar.ascx" TagName="AdminStatusbar" TagPrefix="ucAdminStatusbar" %>
<%@ Register Src="~/UserControl/ValidationContainer.ascx" TagName="ValidationContainer"
    TagPrefix="ucValidationContainer" %>
<style type="text/css">
    #inner
    {
        width: 200px;
        float: left;
        margin-right: 2%;
        border: 1px solid #7AB6A1;
        background: #FFF;
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
        <h1><asp:Localize Text="Company Management" runat="server" ID="locCompanyMgmt" 
                meta:resourcekey="locCompanyMgmtResource1"></asp:Localize></h1>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px;">
        <center>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <center>
                        <fieldset style="width: 60%;">
                            <legend>
                                <asp:Label ID="lblHeader" runat="server" Text="Add Company" 
                                    meta:resourcekey="lblHeaderResource1"></asp:Label></legend>
                            <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" 
                                Visible="False" />
                            <table cellpadding="0" cellspacing="0" border="0" width="97%">
                                <tr id="trListHeader" runat="server">
                                    <td runat="server">
                                        <asp:Panel ID="panelAddProjectDescription" runat="server" BorderStyle="None" 
                                            meta:resourcekey="panelAddProjectDescriptionResource1">
                                            <table>
                                                <tr>
                                                    <td align="right">
                                                        <span style="font-weight: bold"><asp:Localize Text="Company Name" 
                                                            runat="server" Id="locCompanyName" meta:resourcekey="locCompanyNameResource1"></asp:Localize><span style="color: Red;">*</span> </span>
                                                    </td>
                                                    <td>
                                                        <b>&nbsp;:&nbsp;</b>
                                                    </td>
                                                    <td align="left">
                                                        <div style="float: left;">
                                                            <asp:TextBox ID="txtCompanyName" runat="server" Width="370px" MaxLength="50" 
                                                                Height="19px" meta:resourcekey="txtCompanyNameResource1"></asp:TextBox>
                                                        </div>
                                                        <div style="float: left">
                                                            <asp:RequiredFieldValidator ID="rfvCompanyName" runat="server" Display="Dynamic"
                                                                ControlToValidate="txtCompanyName" ForeColor="Red" 
                                                                ValidationGroup="vgCompanyName"><ucValidationContainer:ValidationContainer ID="rfvcCompanyContainer" runat="server"
                                                                    ValidationMsg="Please provide company name." Size="200" />
</asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="clear">
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span style="font-weight: bold"><asp:Localize Text="Region Name" runat="server" 
                                                            ID="locRegionName" meta:resourcekey="locRegionNameResource1"></asp:Localize><span style="color: Red;">*</span> </span>
                                                    </td>
                                                    <td>
                                                        <b>&nbsp;:&nbsp;</b>
                                                    </td>
                                                    <td align="left">
                                                        <div style="float: left;">
                                                            <asp:DropDownList ID="cmbRegionName" runat="server" Width="200px" 
                                                                CssClass="select" meta:resourcekey="cmbRegionNameResource1">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div style="float: left">
                                                            <asp:RequiredFieldValidator ID="rfvRegionName" runat="server" Display="Dynamic" ControlToValidate="cmbRegionName"
                                                                ForeColor="Red" ValidationGroup="vgCompanyName"><ucValidationContainer:ValidationContainer ID="Validationcontainer1" runat="server"
                                                                    ValidationMsg="Please select region." Size="200" />
</asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="clear">
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span style="font-weight: bold"><asp:Localize Text="Is Active?" runat="server" 
                                                            ID="locIsActive" meta:resourcekey="locIsActiveResource1"></asp:Localize> </span>
                                                    </td>
                                                    <td>
                                                        <b>&nbsp;:&nbsp;</b>
                                                    </td>
                                                    <td align="left">
                                                        <div style="float: left;">
                                                            <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" 
                                                                meta:resourcekey="chkIsActiveResource1" />
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
                                                    <span> <asp:Localize Text="Save" runat="server" ID="locSave" 
                                                                    meta:resourcekey="locSaveResource1"></asp:Localize>

 </span>
                                                                </asp:LinkButton>
                                                                <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="gray_bt" 
                                                                    PostBackUrl="~/CompanyManagement.aspx">
                                                    <span> <asp:Localize Text="Cancel" runat="server" ID="locCancel" 
                                                                    meta:resourcekey="locCancelResource1"></asp:Localize>

 </span>
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
