<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddMeasurementDevice.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.AddMeasurementDevice" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<div class="admin_top">&nbsp;</div>
<div class="admin_middle">
    <div class="head">
       <h1>
            Add Sync Measurement Device</h1>        
        <asp:LinkButton ID="lbtnProjectSave" runat="server" CssClass="button" onclick="lbtnProjectSave_Click" 
           >
        <span>
            <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/save_icon.png" />
            Save</span></asp:LinkButton></div>
    <div class="clear"></div>  
    <div class="add_form">    
    <center><ucStatusbar:Statusbar ID="ucStatusbar1" runat="server" Visible="false" /></center>
     <table cellpadding="0" cellspacing="5" border="0" width="100%">
            <tr id="trFilter" runat="server">
                <td>
                    <table cellpadding="0" cellspacing="5" border="0" width="100%">
                        <tr>
                            <td style="width: 20%;">
                                <asp:Label ID="lblCompanyName" runat="server" Text="Company Name" CssClass="LabelTitle"></asp:Label>
                                &nbsp; :
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbCompanyName" runat="server" DataValueField="Key" DataTextField="Value" AutoPostBack="true" 
                                 OnSelectedIndexChanged="cmbCompanyName_SelectedIndexChanged">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblBranchName" runat="server" Text="Branch Name" CssClass="LabelTitle"></asp:Label>
                                &nbsp :
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbBranch" runat="server" DataValueField="Key" DataTextField="Value" AutoPostBack="true" OnSelectedIndexChanged="cmbBranch_SelectedIndexChanged">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblDongleSerialNo" runat="server" Text="Dongle Serial No" CssClass="LabelTitle"></asp:Label>
                                &nbsp :
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbDongleSerialNo" runat="server" DataValueField="Key" DataTextField="Value">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblSerialNo" runat="server" Text="Serial No" CssClass="LabelTitle"></asp:Label>
                                &nbsp :
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtSerialNo" runat="server">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblDeviceType" runat="server" Text="Device Type" CssClass="LabelTitle"></asp:Label>
                                &nbsp :
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbDeviceType" runat="server" DataValueField="Key" DataTextField="Value" AutoPostBack="true">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblIsActive" runat="server" Text="Is Active" CssClass="LabelTitle"></asp:Label>
                                &nbsp :
                            </td>
                            <td>
                                <asp:CheckBox ID="chkIsActive" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</div>
<div class="admin_bottom">&nbsp;</div>
<div class="clear"></div>