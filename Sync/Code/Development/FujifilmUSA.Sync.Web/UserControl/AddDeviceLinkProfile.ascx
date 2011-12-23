<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddDeviceLinkProfile.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.AddDeviceLinkProfile" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
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
                 <%--<asp:RequiredFieldValidator ID="rfvProjectName" runat="server" ControlToValidate="txtProjectName" ErrorMessage="*" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="SaveProject"></asp:RequiredFieldValidator>--%>
                   <asp:TextBox ID="txtDeviceLinkProfileName" runat="server" Width="178px" Height="19px"></asp:TextBox>
                </td>               
                 <%--<td class="float_right">
                    &nbsp;
                </td>--%>
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
</div>

<div class="contant_bottom">
    &nbsp;</div>
<div class="clear">
</div>
