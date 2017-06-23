<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddPrintStandardType.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.AddPrintStandardType" %>
<%@ Register Src="~/UserControl/AdminStatusbar.ascx" TagName="AdminStatusbar" TagPrefix="ucAdminStatusbar" %>
<%@ Register Src="~/UserControl/ValidationContainer.ascx" TagName="ValidationContainer"
    TagPrefix="ucValidationContainer" %>
<link rel="stylesheet" href="JQuery/datepicker-themes/base/jquery.ui.all.css">

<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle" style="line-height: 34px;">
    <div class="head">
        <h1><asp:Localize ID="locPrintStandardTypeMgmt" runat="server" Text="PrintStandardType Management" 
                meta:resourcekey="locPrintStandardTypeMgmtResource1"></asp:Localize></h1>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <center>
                    <fieldset style="width: 70%;">
                        <legend>
                            <asp:Label ID="lblHeader" runat="server" Text="Add PrintStandardType" 
                                meta:resourcekey="lblHeaderResource1"></asp:Label>
                        </legend>
                        <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" 
                            Visible="False" />
                        <table width="100%">
                          <tr>
                                <td  align="right">
                                    <span style="font-weight: bold;"><asp:Localize runat="server" 
                                        Text="PrintStandard Name" ID="locPrintStandardType" 
                                        meta:resourcekey="locPrintStandardTypeResource1"></asp:Localize><span style="color: Red;">*</span>
                                    </span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td style="width: 370px">
                                    <div style="float: left;">
                                        <asp:TextBox ID="txtPrintStandardType" runat="server" MaxLength="50" Width="370px"
                                            Height="19px" meta:resourcekey="txtPrintStandardTypeResource1"></asp:TextBox>
                                             <asp:HiddenField ID="hdnPrintStandardTypeID" runat="server" Value="0" />
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvPrintStandardType" runat="server" Display="Dynamic"
                                                ControlToValidate="txtPrintStandardType" ForeColor="Red" 
                                                ValidationGroup="vgAddPrintStandardType" meta:resourcekey="rfvPrintStandardTypeResource1"><ucValidationContainer:ValidationContainer ID="rfvcPrintStandardTypeContainer" runat="server"
                                                    ValidationMsg="Please provide PrintStandard name." Size="200" />
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <span style="font-weight: bold;"><asp:Localize Text="Source Profile" 
                                        ID="locSourceProfile" runat="server" meta:resourcekey="locSourceProfileResource1"></asp:Localize><span style="color: Red;">*</span>
                                    </span>
                                </td>
                                <td style="width:8px;">
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <asp:DropDownList ID="cmbSourceProfile" runat="server" Width="370px" CssClass="select" 
                                            meta:resourcekey="cmbSourceProfileResource1">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvSourceProfile" runat="server" Display="Dynamic" ControlToValidate="cmbSourceProfile"
                                            ForeColor="Red" ValidationGroup="vgAddPrintStandardType" meta:resourcekey="rfvSourceProfileResource1"><ucValidationContainer:ValidationContainer ID="rfvSourceProfileContainer" runat="server"
                                                ValidationMsg="Please select SourceProfile." Size="200" />
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr style="visibility:hidden;">
                                <td  align="right">
                                    <span style="font-weight: bold;"><asp:Localize Text="Is Sync1DType?" runat="server" 
                                        ID="locIsSync1DType" meta:resourcekey="locIsSync1DTypeResource1"></asp:Localize></span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td style="width: 370px">
                                    <div style="float: left;">
                                        <asp:CheckBox ID="chkIsSync1DType" runat="server" 
                                            meta:resourcekey="chkIsSync1DTypeResource1" />
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
                                <td>
                                    <div class="text">
                                        <div style="padding-left: 3px;">
                                            <asp:LinkButton ID="lbtnSave" runat="server" CssClass="gray_bt" ValidationGroup="vgAddPrintStandardType"
                                                OnClick="lbtnSave_Click">
                                                    <span> <asp:Localize Text="Save" runat="server" ID="locSave" 
                                                meta:resourcekey="locSaveResource1"></asp:Localize>
</span>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="gray_bt" 
                                                PostBackUrl="~/PrintStandardTypeManagement.aspx">
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
                    </fieldset>
                </center>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
<div class="admin_bottom">
    &nbsp;</div>
<div class="clear">
</div>
