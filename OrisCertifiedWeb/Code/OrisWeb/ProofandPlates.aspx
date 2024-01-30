<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="ProofandPlates.aspx.cs" Inherits="OrisWeb.ProofandPlates" %>

<%@ Register Src="~/Common/HomeMenu.ascx" TagName="HomeMenu" TagPrefix="uc1" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="rad" %>
<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register Src="~/Common/StausMenu.ascx" TagName="StausMenu" TagPrefix="uc2" %>
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
                                                   <iframe id="FRM_Value" src="ProofandPlatesWindow.aspx" style="width:100%;height:600px" scrolling="auto" frameborder="0"></iframe>
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