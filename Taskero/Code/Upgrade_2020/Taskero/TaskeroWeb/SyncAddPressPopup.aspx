<%@ Page Title="" Language="C#" MasterPageFile="~/SyncPopup.Master" AutoEventWireup="true" EnableEventValidation="false"  CodeBehind="SyncAddPressPopup.aspx.cs" Inherits="TaskeroWeb.SyncAddPressPopup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td class="green-title">
                Add Press
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <asp:Label ID="LblError" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table cellpadding="0" cellspacing="4" border="0" width="95%">
                    <tr>
                        <td class="FORM-TEXT-BLACK">
                            Branch :
                        </td>
                        <td>
                            <asp:DropDownList ID="DdlBranchs" runat="server" CssClass="DROPDOWNLIST" 
                                AutoPostBack="True" onselectedindexchanged="DdlBranchs_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                      <tr>
                        <td class="FORM-TEXT-BLACK">
                            Press Name :
                        </td>
                        <td>
                            <asp:TextBox ID="TxtPressName" runat="server" CssClass="TEXTBOX"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="FORM-TEXT-BLACK">
                            Manufacture :
                        </td>
                        <td>
                            <asp:TextBox ID="TxtManufature" runat="server" CssClass="TEXTBOX"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="FORM-TEXT-BLACK">
                            Serial Number :
                        </td>
                        <td>
                            <asp:DropDownList ID="DdlSerialNumber" runat="server" CssClass="DROPDOWNLIST">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="FORM-TEXT-BLACK">
                            Ink Unit:
                        </td>
                        <td>
                            <asp:DropDownList ID="DdlInkUnit" runat="server" CssClass="DROPDOWNLIST" Width="50px">
                             <asp:ListItem  Text="1" Value="1"></asp:ListItem>                                
                                <asp:ListItem  Text="2" Value="2"></asp:ListItem>
                                <asp:ListItem  Text="3" Value="3"></asp:ListItem>
                                <asp:ListItem  Text="4" Value="4"></asp:ListItem>
                                <asp:ListItem  Text="5" Value="5"></asp:ListItem>
                                <asp:ListItem  Text="6" Value="6"></asp:ListItem>
                                <asp:ListItem  Text="7" Value="7"></asp:ListItem>
                                <asp:ListItem  Text="8" Value="8"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="FORM-TEXT-BLACK">
                            Press Type :
                        </td>
                        <td>
                            <asp:DropDownList ID="DdlPressType" runat="server" CssClass="DROPDOWNLIST">
                               
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr><td colspan="2">&nbsp;</td></tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button ID="BTN_SavePress" runat="server" CssClass="BUTTON" 
                                Text="Save Press" onclick="BTN_SavePress_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>