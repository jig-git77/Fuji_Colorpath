<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="DataMonitor.aspx.cs" Inherits="OrisWeb.DataMonitor" %>

<%@ Register Src="~/Common/HomeMenu.ascx" TagName="HomeMenu" TagPrefix="uc1" %>
<%@ Register Src="~/Common/StausMenu.ascx" TagName="StausMenu" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript">
    function Pageopen( CPMeasurementGUID )
    {
        window.open( "DataHeaderColor.aspx?CPMeasurementGUID="+ CPMeasurementGUID , 'DH' , 'width=950px,height=700px,resizable=1,scrollbars=1' );
    }
    </script>

    <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
        <tr>
            <td>
                <table width="940" border="0" cellspacing="0" cellpadding="0" align="center">
                    <tr>
                        <td>
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
                                        <iframe id="FRM_Value" src="DataMonitorWindow.aspx" style="width: 100%; height: 600px"
                                            scrolling="auto" frameborder="0"></iframe>
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
