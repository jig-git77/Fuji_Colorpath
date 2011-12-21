<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddDestinationProfile.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.AddDestinationProfile" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="uc" %>
<%@ Register Src="~/UserControl/MeasurementStatusbar.ascx" TagName="MeasurementStatusbar" TagPrefix="uc" %>
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
    <uc:MeasurementStatusbar ID="ucMeasurementStatusbar" runat="server" />
    <div class="info_form">
        <uc:Statusbar ID="ucStatusbar1" runat="server" Visible="false" />
    </div>
    <div class="add_form">
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
                    <asp:Label ID="lblDestinationProfileName" runat="server" Text="Name:" CssClass="LabelTitle"></asp:Label>
                    &nbsp;
                </td>
               <td>
                    <asp:TextBox ID="txtDestinationProfile" runat="server" width="178px" Height="19px"></asp:TextBox>
                    <%--<asp:RequiredFieldValidator ID="rfvProjectName" runat="server" ControlToValidate="txtProjectName" ErrorMessage="*" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="SaveProject"></asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr>
                <td class="ProjectListtd1">
                    <asp:Label ID="lblDeviceType" runat="server" Text="Device Type:" CssClass="LabelTitle"></asp:Label>&nbsp;
                </td>
                <td>
                    <asp:DropDownList ID="cmbDeviceType" runat="server" DataValueField="Key" DataTextField="Value"
                        OnSelectedIndexChanged="cmbDeviceType_SelectedIndexChanged" AutoPostBack="true"
                        CssClass="select" Width="180px">
                    </asp:DropDownList>
                </td>
                <td class="ProjectListtd1">
                    <asp:Label ID="lblAlignmentChart" runat="server" Text="Alignment Chart:" CssClass="LabelTitle"></asp:Label>&nbsp;
                </td>
                <td>
                    <asp:DropDownList ID="cmbAlignmentChart" runat="server" DataValueField="Key" DataTextField="Value"
                        CssClass="select " Width="200px" onchange=" ControlVisibility();">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>      

    </div>
    <uc:AddMeasurementCommon ID="ucAddMeasurementCommon" runat="server" Type="DestinationProfile" />
</div>
<div class="contant_bottom">
    &nbsp;</div>
<div class="clear">
</div>
