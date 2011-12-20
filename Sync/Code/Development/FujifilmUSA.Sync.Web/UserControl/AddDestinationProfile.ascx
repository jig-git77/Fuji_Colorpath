<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddDestinationProfile.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.AddDestinationProfile" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<%@ Register Src="~/UserControl/AddMeasurementCommon.ascx" TagName="AddMeasurementCommon"
    TagPrefix="uc" %>
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
    <div class="info_form">
        <ucStatusbar:Statusbar ID="ucStatusbar1" runat="server" Visible="false" />
    </div>
    <div class="info_form">
        <%--<div class="float_l InformationtxtDivWidth" style="margin-bottom: 1px;">
            <asp:Label ID="lblDestinationProfileName" runat="server" Text="Name" CssClass="LabelTitle"></asp:Label>
            &nbsp;
            <asp:TextBox ID="txtDestinationProfile" runat="server" SkinID="width250px"></asp:TextBox>
           <asp:RequiredFieldValidator ID="rfvProjectName" runat="server" ControlToValidate="txtProjectName" ErrorMessage="*" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="SaveProject"></asp:RequiredFieldValidator>
        </div>
        <div class="float_l" style="margin-bottom: 3px;">
            <asp:Label ID="lblDeviceType" runat="server" Text="Device Type" CssClass="LabelTitle"></asp:Label>&nbsp;
            <asp:DropDownList ID="cmbDeviceType" runat="server" DataValueField="Key" DataTextField="Value"
                OnSelectedIndexChanged="cmbDeviceType_SelectedIndexChanged" AutoPostBack="true" CssClass="select"
                Width="200px">
            </asp:DropDownList>
        </div>
        <div class="float_r">
            <asp:Label ID="lblAlignmentChart" runat="server" Text="Alignment Chart:" CssClass="LabelTitle"></asp:Label>&nbsp;
            <asp:DropDownList ID="cmbAlignmentChart" runat="server" DataValueField="Key" DataTextField="Value" CssClass="select "
                Width="200px">
            </asp:DropDownList>
        </div>--%>
           <table border="0" cellspacing="5" cellpadding="0" class="Width100percent">
            <tr>
                <td class="ProjectListtd1">
                    <asp:Label ID="lblDestinationProfileName" runat="server" Text="Name" CssClass="LabelTitle"></asp:Label>
                    &nbsp;
                </td>
               <td class="ProjectListtd2">
                    <asp:TextBox ID="txtDestinationProfile" runat="server" SkinID="width250px"></asp:TextBox>
                    <%--<asp:RequiredFieldValidator ID="rfvProjectName" runat="server" ControlToValidate="txtProjectName" ErrorMessage="*" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="SaveProject"></asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr>
                <td class="ProjectListtd1">
                    <asp:Label ID="lblDeviceType" runat="server" Text="Device Type" CssClass="LabelTitle"></asp:Label>&nbsp;
                </td>
                <td class="ProjectListtd2">
                    <asp:DropDownList ID="cmbDeviceType" runat="server" DataValueField="Key" DataTextField="Value"
                        OnSelectedIndexChanged="cmbDeviceType_SelectedIndexChanged" AutoPostBack="true"
                        CssClass="select" Width="200px">
                    </asp:DropDownList>
                </td>
                <td class="ProjectListtd1">
                    <asp:Label ID="lblAlignmentChart" runat="server" Text="Alignment Chart:" CssClass="LabelTitle"></asp:Label>&nbsp;
                </td>
                <td class="ProjectListtd2">
                    <asp:DropDownList ID="cmbAlignmentChart" runat="server" DataValueField="Key" DataTextField="Value"
                        CssClass="select " Width="200px">
                    </asp:DropDownList>
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
