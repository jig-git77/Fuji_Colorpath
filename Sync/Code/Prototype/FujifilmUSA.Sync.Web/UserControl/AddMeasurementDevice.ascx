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
     <asp:UpdatePanel ID="upPanel" runat="server">
            <ContentTemplate>
    <ucStatusbar:Statusbar ID="ucStatusbar1" runat="server" Visible="false" />
     <table cellpadding="0" cellspacing="5" border="0" width="100%">
            <tr id="trFilter" runat="server">
                <td>
                    <table cellpadding="0" cellspacing="5" border="0" width="100%">
                        <tr>
                            <td style="width: 20%;">
                                <asp:Label ID="lblCompanyName" runat="server" Text="Company Name" ></asp:Label>
                                &nbsp; :
                            </td>
                            <td>                                
                                <asp:DropDownList ID="cmbCompanyName" runat="server" DataValueField="Key" DataTextField="Value" AutoPostBack="true" 
                                 OnSelectedIndexChanged="cmbCompanyName_SelectedIndexChanged" Width="180px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblBranchName" runat="server" Text="Branch Name" ></asp:Label>
                                &nbsp :
                            </td>
                            <td>                               
                                <asp:DropDownList ID="cmbBranch" runat="server" DataValueField="Key" DataTextField="Value" AutoPostBack="true" OnSelectedIndexChanged="cmbBranch_SelectedIndexChanged" Width="180px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblDongleSerialNo" runat="server" Text="Dongle Serial No" ></asp:Label>
                                &nbsp :
                            </td>
                            <td>                               
                                <asp:DropDownList ID="cmbDongleSerialNo" runat="server" DataValueField="Key" DataTextField="Value" AutoPostBack="true" Width="180px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblSerialNo" runat="server" Text="Serial No" ></asp:Label>
                                &nbsp :
                            </td>
                            <td>                               
                                <asp:TextBox ID="txtSerialNo" runat="server" Width="178px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblDeviceType" runat="server" Text="Device Type" ></asp:Label>
                                &nbsp :
                            </td>
                            <td>                               
                                <asp:DropDownList ID="cmbDeviceType" runat="server" DataValueField="Key" DataTextField="Value" AutoPostBack="true" Width="180px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblIsActive" runat="server" Text="Is Active" ></asp:Label>
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
          </ContentTemplate>
            <Triggers>
            <asp:AsyncPostBackTrigger ControlID="cmbCompanyName" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="cmbBranch" EventName="SelectedIndexChanged" />
            </Triggers> 
        </asp:UpdatePanel>
    </div>
</div>
<div class="admin_bottom">&nbsp;</div>
<div class="clear"></div>