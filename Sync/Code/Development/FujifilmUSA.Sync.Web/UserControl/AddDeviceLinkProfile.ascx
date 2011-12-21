<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddDeviceLinkProfile.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.AddDeviceLinkProfile" %>

<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<%@ Register Src="~/UserControl/AddMeasurementCommon.ascx" TagName="AddMeasurementCommon" TagPrefix="uc" %>
<div class="contant_top">
    &nbsp;</div>
<div class="contant_middle">
    <div class="head">
        <h1>
            Add Device Link Profile</h1>
        <asp:LinkButton ID="lbtnDeviceLinkProfileSave" runat="server" CssClass="button">
            <span>
                <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/save_icon.png" />
                Save</span></asp:LinkButton></div>
    <div class="clear">
    </div>
    <div class="info_form">
        <table id="tblMessage" runat="server" width="100%" style="border: 1px solid #0C3D30;
            background-color: #6BB19C; color: Black; height: 45px;" cellpadding="0" cellspacing="0">
            <tr>
                <td style="padding: 2px 2px 2px 5px; font-weight: bold;" valign="top">
                    <asp:Label ID="lblMessage" runat="server" Text="Status : Scanning device is not loaded."></asp:Label>
                </td>
                <td style="padding: 2px 5px 2px 2px; font-weight: bold;" align="right" valign="top">
                    <asp:Label ID="lblPluginMessage" runat="server" Text="loading..."></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="add_form">
        <center>
            <ucStatusbar:Statusbar ID="ucStatusbar1" runat="server" Visible="false" />
        </center>
        <table border="0" cellspacing="5" cellpadding="0" class="Width100percent">
            <tr>
                <td class="ProjectListtd1">
                    <asp:Label ID="lblDeviceLinkProfileName" runat="server" Text="Name" CssClass="LabelTitle"></asp:Label>
                    :
                </td>
               <td  colspan="2">
                   <%-- <telerik:RadTextBox ID="txtProjectName" runat="server" SkinID="width196px">
                    </telerik:RadTextBox><%--<asp:RequiredFieldValidator ID="rfvProjectName" runat="server" ControlToValidate="txtProjectName" ErrorMessage="*" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="SaveProject"></asp:RequiredFieldValidator>--%>
                   <asp:TextBox ID="txtProjectName" runat="server" Width="178px" Height="19px"></asp:TextBox>
                </td>               
                 <%--<td class="float_right">
                    &nbsp;
                </td>--%>
            </tr>
             <tr>
              <td class="ProjectListtd1">
                    <asp:Label ID="lblDeviceType" runat="server" Text="Device Type" CssClass="LabelTitle"></asp:Label>                    :
                </td>
                <td >
                    <%--<telerik:RadComboBox ID="cmbDeviceType" runat="server" DataValueField="Key" DataTextField="Value" AutoPostBack="true" SkinID="width200px" ></telerik:RadComboBox>--%>
                    <asp:DropDownList ID="cmbDeviceType" runat="server" DataValueField="Key" 
                        DataTextField="Value" AutoPostBack="true" width="180px" CssClass="select"
                        onselectedindexchanged="cmbDeviceType_SelectedIndexChanged1">
                    </asp:DropDownList>
                </td>
                <td class="ProjectListtd1">
                    <asp:Label ID="lblAlignmentChart" runat="server" Text="Alignment Chart:" CssClass="LabelTitle"></asp:Label>
                </td>
                <td >                    
                   <%-- <telerik:RadComboBox ID="cmbAlignmentChart" runat="server" DataValueField="Key" DataTextField="Value" SkinID="width200px"></telerik:RadComboBox>--%>
                    <asp:DropDownList ID="cmbAlignmentChart" runat="server" DataValueField="Key" DataTextField="Value"  width="180px" CssClass="select"></asp:DropDownList>
                </td>               
            </tr>           
            <tr>
                <td class="ProjectListtd1"><asp:Label ID="Label1" runat="server" Text="Select Source:" CssClass="LabelTitle"></asp:Label>
                </td>
                <td >
                <%--<telerik:RadComboBox ID="cmbSourceProfile" runat="server" DataValueField="Key" DataTextField="Value" SkinID="width200px"  AutoPostBack="true" ></telerik:RadComboBox>--%>
                <asp:DropDownList ID="cmbSourceProfile" runat="server" DataValueField="Key" DataTextField="Value"  width="180px" CssClass="select"></asp:DropDownList>
                </td>
                <td class="ProjectListtd1"><asp:Label ID="Label2" runat="server" Text="Select Destination:" CssClass="LabelTitle"></asp:Label>
                </td>
                <td>
                <%--<telerik:RadComboBox ID="cmbDestinationProfile" DataValueField="Key" DataTextField="Value" runat="server" SkinID="width200px" AutoPostBack="true" ></telerik:RadComboBox>--%>
                <asp:DropDownList ID="cmbDestinationProfile" runat="server" DataValueField="Key" DataTextField="Value"  width="180px" CssClass="select"></asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>    
    <uc:AddMeasurementCommon ID="ucAddMeasurementCommon" runat="server" />
</div>

<div class="contant_bottom">
    &nbsp;</div>
<div class="clear">
</div>
