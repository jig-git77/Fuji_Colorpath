<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddCompanyManagement.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.AddCompanyManagement" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle" style="line-height: 34px;">
    <div class="head">
        <h1>
            New Company Management</h1>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px; margin-right: 15px;">
        <ucStatusbar:Statusbar ID="Statusbar1" runat="server" Visible="false" />
    </div>
    <div style="margin-left: 10px;">
        <table cellpadding="0" cellspacing="0" border="0" width="97%">
            <tr id="trListHeader" runat="server">
                <td>
                    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                        <AjaxSettings>
                            <telerik:AjaxSetting AjaxControlID="rgProjectList">
                                <UpdatedControls>
                                    <telerik:AjaxUpdatedControl ControlID="panelAddProjectDescription" LoadingPanelID="RadAjaxLoadingPanel1" />
                                </UpdatedControls>
                            </telerik:AjaxSetting>
                        </AjaxSettings>
                    </telerik:RadAjaxManager>
                    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
                    <asp:Panel ID="panelAddProjectDescription" runat="server" BorderStyle="None">
                        <table>
                            <tr>
                                <td style="width: 130px">
                                    <span>Company Name: &nbsp<span style="color: Red;">*</span> </span>
                                </td>
                                <td style="width: 400px">
                                    <div style="float: left;">
                                        <asp:TextBox ID="txtCompanyName" runat="server" Width="200px" Height="19px" ></asp:TextBox>
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
                                <td>
                                    <span>Region Name: &nbsp<span style="color: Red;">*</span> </span>
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <asp:DropDownList ID="cmbRegionName" runat="server" Width="200px" CssClass="select">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvRegionName" runat="server" Display="Dynamic"
                                            ControlToValidate="cmbRegionName" ForeColor="Red" ValidationGroup="vgCompanyName">
                                            <div class="tooltip_round_box" style="width: 200px;">
                                            <div class="tooltip_arrow" style="margin-top: -3px;">
                                                &nbsp;</div>
                                            <div class="tooltip_top_left">
                                            </div>
                                            <div class="tooltip_top_right">
                                            </div>
                                            <div class="tooltip_inside">
                                                Please provide region name.
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
                               
                                </td>
                                <td>
                                        <div class="text" >
                                            <div style="padding-left: 3px;">
                                                <asp:LinkButton ID="lbtnSave" 
                                                    runat="server" 
                                                    CssClass="gray_bt"
                                                    ValidationGroup="vgCompanyName" onclick="lbtnSave_Click" 
                                                    >
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
    </div>
</div>
<div class="admin_bottom">
    &nbsp;</div>
<div class="clear">
</div>
