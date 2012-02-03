<%@ Page Language="C#" MasterPageFile="~/Home.master" AutoEventWireup="true" CodeFile="TaskeroReports.aspx.cs"
    Inherits="TaskeroReports" Title="Taskero Reports" %>

<%@ Register Src="Common/ReportMenu.ascx" TagName="ReportMenu" TagPrefix="uc1" %>
<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
        <tr>
            <td>
                <table width="940" border="0" cellspacing="0" cellpadding="0" align="center">
                    <tr>
                        <td>
                            <uc1:ReportMenu ID="ReportMenu1" runat="server" />
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
            <td align="center">
                <table width="940" border="0" cellspacing="0" cellpadding="0" align="center">
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" border="0" class="TABLE1">
                                <tr>
                                    <td class="green-title">
                                        &nbsp; Report List
                                    </td>
                                </tr>
                                <tr>
                                    <td  style="height:20px">
                                        &nbsp; 
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblError" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td>
                                        <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" Width="100%" Style="padding: 0px"                                           >
                                            <ajaxToolkit:TabPanel ID="TabPanPrepress" Width="100%" runat="server" HeaderText="Prepress"
                                                Style="padding: 0px; width: 100%">
                                                <ContentTemplate>
                                                    <table width="940" border="0" cellspacing="0" cellpadding="0" align="center">
                                                        <tr>
                                                            <td>
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                    <tr id="TRMessagePrePress" runat="server" visible="false">
                                                                        <td>
                                                                            <table width="100%" border="0" cellspacing="1" cellpadding="0" class="TABLE1">
                                                                                <tr>
                                                                                    <td style="height: 200px; text-align: center; vertical-align: middle;">
                                                                                        <font style="font-size: 30px; font-weight: bold; color: Red;">Reports are not available
                                                                                            for this type of category.</font>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="TRContentPrePress" runat="server">
                                                                        <td>
                                                                            <div style="overflow: auto; height: 415px">
                                                                                <asp:DataList ID="RPT_Reports_PrePress" runat="server" RepeatColumns="4">
                                                                                    <ItemTemplate>
                                                                                        <td style="width: 182px; text-align: center;">
                                                                                            <table border="0" cellspacing="0" cellpadding="1" align="center">
                                                                                                <tr>
                                                                                                    <td style="text-align: center; vertical-align: top;" colspan="3">
                                                                                                        <a href="<%#Eval("AspxPageName") %>">
                                                                                                            <img src="images/<%#Eval("IconName") %>" style="border: 1px solid #808080;" alt=""
                                                                                                                border="0" /></a>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td align="center" class="report-img-title" colspan="3">
                                                                                                        <%#Eval("ReportDesc")%>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td height="10" colspan="3">
                                                                                                        <img src="images/spacer.gif" alt="fujifilm" border="0" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </ItemTemplate>
                                                                                </asp:DataList>
                                                                            </div>
                                                                            <asp:Label ID="LblErrorPrePress" runat="server" CssClass="ERROR"></asp:Label>
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
                                                </ContentTemplate>
                                            </ajaxToolkit:TabPanel>
                                            <ajaxToolkit:TabPanel ID="TabPanPress" Width="100%" runat="server" HeaderText="Press">
                                                <ContentTemplate>
                                                    <table width="940" border="0" cellspacing="0" cellpadding="0" align="center">
                                                        <tr>
                                                            <td>
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                    <tr id="TRMessagePress" runat="server" visible="false">
                                                                        <td>
                                                                            <table width="100%" border="0" cellspacing="1" cellpadding="0" class="TABLE1">
                                                                                <tr>
                                                                                    <td style="height: 200px; text-align: center; vertical-align: middle;">
                                                                                        <font style="font-size: 30px; font-weight: bold; color: Red;">Reports are not available
                                                                                            for this type of category.</font>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="TRContentPress" runat="server">
                                                                        <td>
                                                                            <div style="overflow: auto; height: 415px">
                                                                                <asp:DataList ID="RPT_Reports_Press" runat="server" RepeatColumns="4">
                                                                                    <ItemTemplate>
                                                                                        <td style="width: 182px; text-align: center;">
                                                                                            <table border="0" cellspacing="0" cellpadding="1" align="center">
                                                                                                <tr>
                                                                                                    <td style="text-align: center; vertical-align: top;" colspan="3">
                                                                                                        <a href="<%#Eval("AspxPageName") %>">
                                                                                                            <img src="images/<%#Eval("IconName") %>" style="border: 1px solid #808080;" alt=""
                                                                                                                border="0" /></a>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td align="center" class="report-img-title" colspan="3">
                                                                                                        <%#Eval("ReportDesc")%>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td height="10" colspan="3">
                                                                                                        <img src="images/spacer.gif" alt="fujifilm" border="0" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </ItemTemplate>
                                                                                </asp:DataList>
                                                                            </div>
                                                                            <asp:Label ID="LblErrorPress" runat="server" CssClass="ERROR"></asp:Label>
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
                                                </ContentTemplate>
                                            </ajaxToolkit:TabPanel>
                                            <ajaxToolkit:TabPanel ID="TabPanMiscelleneaous" Width="100%" runat="server" HeaderText="Miscelleneaous">
                                                <ContentTemplate>
                                                    <table width="940" border="0" cellspacing="0" cellpadding="0" align="center">
                                                        <tr>
                                                            <td>
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                    <tr id="TRMessageMiscelleneaous" runat="server" visible="false">
                                                                        <td>
                                                                            <table width="100%" border="0" cellspacing="1" cellpadding="0" class="TABLE1">
                                                                                <tr>
                                                                                    <td style="height: 200px; text-align: center; vertical-align: middle;">
                                                                                        <font style="font-size: 30px; font-weight: bold; color: Red;">Reports are not available
                                                                                            for this type of category.</font>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="TRContentMiscelleneaous" runat="server">
                                                                        <td>
                                                                            <div style="overflow: auto; height: 415px">
                                                                                <asp:DataList ID="RPT_Reports_Miscelleneaous" runat="server" RepeatColumns="4">
                                                                                    <ItemTemplate>
                                                                                        <td style="width: 182px; text-align: center;">
                                                                                            <table border="0" cellspacing="0" cellpadding="1" align="center">
                                                                                                <tr>
                                                                                                    <td style="text-align: center; vertical-align: top;" colspan="3">
                                                                                                        <a href="<%#Eval("AspxPageName") %>">
                                                                                                            <img src="images/<%#Eval("IconName") %>" style="border: 1px solid #808080;" alt=""
                                                                                                                border="0" /></a>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td align="center" class="report-img-title" colspan="3">
                                                                                                        <%#Eval("ReportDesc")%>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td height="10" colspan="3">
                                                                                                        <img src="images/spacer.gif" alt="fujifilm" border="0" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </ItemTemplate>
                                                                                </asp:DataList>
                                                                            </div>
                                                                            <asp:Label ID="LblErrorMiscelleneaous" runat="server" CssClass="ERROR"></asp:Label>
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
                                                </ContentTemplate>
                                            </ajaxToolkit:TabPanel>
                                        </ajaxToolkit:TabContainer>
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
            </td>
        </tr>
    </table>
</asp:Content>
