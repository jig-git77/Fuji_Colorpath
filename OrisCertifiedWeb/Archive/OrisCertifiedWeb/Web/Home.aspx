<%@ Page Language="C#" MasterPageFile="~/Home.master" AutoEventWireup="true" CodeFile="Home.aspx.cs"  EnableEventValidation="false"
    Inherits="Home" %>

<%@ Register Src="Common/HomeMenu.ascx" TagName="HomeMenu" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="940px" border="0" cellspacing="0" cellpadding="0" align="center">
        <tr>
            <td>
                <!--Menu Conttrol Will Load here-->
                <uc1:HomeMenu ID="HomeMenu1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="100" align="center" valign="bottom">
                            <img src="images/select-menu-item-btn.png" alt="Select Menu Item" border="0" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 100%;" align="center">
                            <a href="#" title="Oris Certified Web">
                                <img id="IMG_LargeImage" runat="server" src="images/oris_logo_big.jpg" alt="Oris Certified Web" border="0" vspace="15" /></a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
