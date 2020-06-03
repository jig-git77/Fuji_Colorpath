<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DataMonitorWindow.aspx.cs" EnableEventValidation="false"
    Inherits="DataMonitorWindow" %>

<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="rad" %>
<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>FUJIFILM Graphic Systems U.S.A., Inc.</title>
    <link rel="Stylesheet" href="Css/Default.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>

            <script language="javascript">
    function Pageopen( CPMeasurementGUID )
    {
        window.open( "DataHeaderColor.aspx?CPMeasurementGUID="+ CPMeasurementGUID , 'DH' , 'width=950px,height=700px,resizable=1,scrollbars=1' );
    }
            </script>

            <table border="0" cellspacing="0" cellpadding="0" class="TABLE1">
                <tr>
                    <td class="ERROR">
                        <asp:Label ID="LblError" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center" style="padding: 10px;font-weight:bold">
                        Next refresh in&nbsp;<b id="BOLD1"></b>&nbsp;seconds.
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DataGrid ID="DG1" runat="server" OnItemDataBound="DG1_OnItemDataBound">
                            <Columns>
                                <asp:BoundColumn HeaderText="GUID" DataField="CPMeasurementGUID"></asp:BoundColumn>
                                <asp:BoundColumn HeaderText="Dongle No." DataField="DongleSerial" ItemStyle-HorizontalAlign="center">
                                </asp:BoundColumn>
                                <asp:BoundColumn HeaderText="Date" DataField="CreatedDT" ItemStyle-HorizontalAlign="center">
                                </asp:BoundColumn>
                                <asp:BoundColumn HeaderText="ID" DataField="MeasurementID"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Measurement File" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:Literal ID="LTRL_Link" runat="server"></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </td>
                </tr>
            </table>

            <script language="javascript">
        document.getElementById("BOLD1").innerHTML = "";
        var i = 60 ;
        function ReduceCounter()
        {            
            if( i == 0 )
            { window.location.href = window.location.href ;  }
            else
            {
                setTimeout( "ReduceCounter()" , 1000 );
                i = i - 1 ;
                document.getElementById("BOLD1").innerHTML = i ;
            }
        }
        ReduceCounter();
            </script>

        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
