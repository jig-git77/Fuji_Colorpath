<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ReportPerformanceSummaryPrintBuyer.aspx.cs" Inherits="OrisWeb.ReportPerformanceSummaryPrintBuyer" %>
<%@ Register Src="~/Common/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<%@ Register Src="ReportPerformanceSummaryCtrl.ascx" TagName="ReportPerformanceSummaryCtrl" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" align="center" border="0" width="100%">
        <tr>
            <td align="center">
                <table width="940" border="0" cellspacing="0" cellpadding="0" align="center">
                    <tr>
                        <td>
                            <!--Report Menu Conttrol Will Load here-->
                            <uc1:Menu ID="Menu1" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
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
                                    <td style="padding: 5px;" class="TABLE1">
                                        <uc2:ReportPerformanceSummaryCtrl ID="ReportPerformanceSummaryCtrl1" runat="server" />
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
</asp:Content>

