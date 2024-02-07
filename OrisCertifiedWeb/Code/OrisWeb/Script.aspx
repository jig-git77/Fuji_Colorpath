<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Script.aspx.cs" Inherits="OrisWeb.Script" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 100%" cellspacing="5" cellpadding="0" border="0">
        <tr>
            <td class="TITLE" colspan="2">
                Create Script</td>
        </tr>
        <tr>
            <td class="SITELINE" colspan="2">
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="LblError" runat="server" CssClass="ERROR"></asp:Label></td>
        </tr>
        <tr>
            <td class="TDLEFT">
                Database Table
            </td>
            <td class="TDRIGHT">
                <asp:DropDownList ID="DDL" runat="server">
                </asp:DropDownList>&nbsp;
                <asp:DropDownList ID="DDL_TYPE" runat="server">
                    <asp:ListItem Value="">ALL</asp:ListItem>
                    <asp:ListItem Value="1">Stored Procedure</asp:ListItem>
                    <asp:ListItem Value="2">Class</asp:ListItem>
                </asp:DropDownList>&nbsp;
                <asp:Button ID="BTN1" runat="server" Text="GENDERATE SCRIPT" CssClass="BUTTON" OnClick="BTN1_Click"></asp:Button></td>
        </tr>
        <tr>
            <td class="TDLEFT" colspan="2">
            </td>
        </tr>
        <tr>
            <td class="SUBTITLE" colspan="2">
                Stored Procedure</td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:TextBox ID="TXT1" runat="server" CssClass="TextBox" TextMode="MultiLine" Rows="10"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="SUBTITLE" colspan="2">
                Class</td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:TextBox runat="server" ID="TXT2" Rows="10" TextMode="MultiLine" CssClass="TextBox"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
