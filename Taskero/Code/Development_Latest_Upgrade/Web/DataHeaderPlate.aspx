<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DataHeaderPlate.aspx.cs" EnableEventValidation="false"
    Inherits="DataHeaderPlate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FUJIFILM Graphic Systems U.S.A., Inc.</title>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <meta name="keywords" content="FUJIFILM Graphic Systems U.S.A., Inc." />
    <meta name="description" content="FUJIFILM Graphic Systems U.S.A., Inc." />
       <link href="CSS/Default.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
    function Close()
    {
        window.close();
        return false;
    }
    function Print()
    {
        window.print();		
        return false;
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:Literal ID="LtrlCSS" runat="server"></asp:Literal>
    <div>
        <table cellpadding="0" cellspacing="0" border="0" style="width: 750px" class="TABLE5">
            <tr>
                <td class="green-title" nowrap="true">
                    Measurement File <span style="padding-left: 550px;">
                        <asp:Button ID="BTNPRINT" runat="server" Text="Print" CssClass="green2-btn" OnClientClick="return Print();" />
                        <asp:Button ID="BTNClose" runat="server" Text="Close" CssClass="green2-btn" OnClientClick="return Close();" />&nbsp;</span>
                </td>
            </tr>
            <tr class="Display">
                <td>
                    <asp:Label ID="LblError" runat="server" CssClass="ERROR"></asp:Label><asp:Literal ID="LTRL_CPMeasurementGUID"
                        runat="server" Visible="false"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DG1" runat="server">
                        <Columns>
                            <asp:BoundColumn DataField="ID" ItemStyle-Width="3%"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FieldName" ItemStyle-Width="18%"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Value" ItemStyle-Width="69%"></asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <table cellpadding="0" cellspacing="0" border="0" class="TABLE5">
            <tr>
                <td class="green-title">
                    Data
                </td>
            </tr>
            <tr>
                <asp:DataGrid ID="DG2" runat="server">
                </asp:DataGrid>
            </tr>
        </table>
        <br />
    </div>
    </form>
</body>
</html>
