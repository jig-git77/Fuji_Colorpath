<%@ Page Language="C#" MasterPageFile="~/Home.master"  EnableEventValidation="false" AutoEventWireup="true" CodeFile="Press.aspx.cs"
    Inherits="Press" Title="Press" %>

<%@ Register Src="Common/HomeMenu.ascx" TagName="HomeMenu" TagPrefix="uc1" %>
<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register Src="Common/StausMenu.ascx" TagName="StausMenu" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
        <tr>
            <td>
                <table width="940" border="0" cellspacing="0" cellpadding="0" align="center">
                    <tr>
                        <td>
                            <!--Report Menu Conttrol Will Load here-->
                            <uc1:HomeMenu ID="HomeMenu1" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table width="940" border="0" cellspacing="0" cellpadding="0" align="center">
                    <tr>
                        <td>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td height="25" id="TopNavTab" style="padding-left: 0px;">
                                        <uc2:StausMenu ID="StausMenu1" runat="server" />
                                    </td>
                                </tr>
                                <tr id="TRContent" runat="server">
                                    <td align="left" valign="top">
                                        <table width="100%" border="0" cellspacing="1" cellpadding="0" class="green-border-white-bg">
                                            <tr>
                                                <td valign="top">
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="1">
                                                        <tr>
                                                            <td height="10">
                                                                <img src="images/spacer.gif" alt="fujifilm" border="0" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="LblError" runat="server" CssClass="ERROR"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                    <tr>
                                                                        <td style="height: 200px; text-align: center; vertical-align: middle;">
                                                                            <font style="font-size: 30px; font-weight: bold; color: Red;">Page Under Construction.</font>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="10">
                                                                <img src="images/spacer.gif" alt="fujifilm" border="0" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
