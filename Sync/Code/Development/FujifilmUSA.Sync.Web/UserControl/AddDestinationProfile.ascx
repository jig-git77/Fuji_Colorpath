<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddDestinationProfile.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.AddDestinationProfile" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<%@ Register Src="~/UserControl/AddMeasurement.ascx" TagName="AddMeasurement" TagPrefix="uc" %>
<div class="contant_top">
    &nbsp;</div>
<div class="contant_middle">
    <div class="head">
        <h1>
            Add Destination Profile</h1>
        <asp:LinkButton ID="lbtnDestinationProfileSave" runat="server" CssClass="button">
            <span>
                <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/save_icon.png" />
                Save</span></asp:LinkButton></div>
    <div class="clear">
    </div>
    <div class="add_form">
        <center>
            <ucStatusbar:Statusbar ID="ucStatusbar1" runat="server" Visible="false" />
        </center>
        <table border="0" cellspacing="5" cellpadding="0" class="Width100percent">
            <tr>
                <td class="ProjectListtd1">
                    <asp:Label ID="lblDestinationProfileName" runat="server" Text="Name" CssClass="LabelTitle"></asp:Label>
                    :
                </td>
               <td class="ProjectListtd2">
                    <telerik:RadTextBox ID="txtProjectName" runat="server" SkinID="width196px">
                    </telerik:RadTextBox><%--<asp:RequiredFieldValidator ID="rfvProjectName" runat="server" ControlToValidate="txtProjectName" ErrorMessage="*" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="SaveProject"></asp:RequiredFieldValidator>--%>
                </td>
                <td class="float_right">
                    <asp:Label ID="lblDeviceType" runat="server" Text="Device Type" CssClass="LabelTitle"></asp:Label>
                    :
                </td>
                <td class="ProjectListtd2">
                    <telerik:RadComboBox ID="cmbDeviceType" runat="server" DataValueField="Key" DataTextField="Value" AutoPostBack="true" SkinID="width200px" ></telerik:RadComboBox>
                </td>
            </tr>
             <tr>
                <td class="ProjectListtd1">
                    <asp:Label ID="lblAlignmentChart" runat="server" Text="Alignment Chart:" CssClass="LabelTitle"></asp:Label>
                </td>
                <td class="ProjectListtd2">                    
                    <telerik:RadComboBox ID="cmbAlignmentChart" runat="server" DataValueField="Key" DataTextField="Value" SkinID="width200px"></telerik:RadComboBox>
                </td>
                <td class="float_right">
                    &nbsp;
                </td>
                <td class="ProjectListtd4">
                    &nbsp;
                </td>
            </tr>           
            <tr>
                <td></td>
            </tr>
        </table>
    </div>    
    <uc:AddMeasurement ID="ucAddMeasurement" runat="server" />
</div>

<div class="contant_bottom">
    &nbsp;</div>
<div class="clear">
</div>
