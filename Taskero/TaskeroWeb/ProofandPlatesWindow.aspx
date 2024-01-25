<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false"  CodeBehind="ProofandPlatesWindow.aspx.cs" Inherits="TaskeroWeb.ProofandPlatesWindow" %>


<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="rad" %>
<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FUJIFILM Graphic Systems U.S.A., Inc.</title>
    <link rel="Stylesheet" href="Css/Default.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <table width="100%" border="0" cellspacing="0" cellpadding="1">
                <tr>
                    <td height="10" colspan="2">
                        <img src="images/spacer.gif" alt="fujifilm" border="0" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="LblError" runat="server" CssClass="ERROR"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 90%">
                        <center>
                            <b>Next refresh in&nbsp;</b><b id="BOLD1"></b><b>&nbsp;seconds.</b></center>
                    </td>
                    <td style="width: 10%" align="right">
                        <asp:Button ID="BTN_Reset" runat="server" CssClass="BUTTON" Text="Clear Filter" OnClick="BTN_Reset_Click">
                        </asp:Button>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <rad:RadGrid ID="DG1" runat="server" AutoGenerateColumns="false" OnItemCommand="DG1_ItemCommand"
                            Skin="Green" OnItemDataBound="DG1_ItemDataBound" AllowFilteringByColumn="true"
                            OnNeedDataSource="DG1_NeedDataSource">
                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                            <MasterTableView AllowFilteringByColumn="true">
                                <HeaderStyle HorizontalAlign="Center" />
                                <Columns>
                                    <rad:GridBoundColumn DataField="CPDeviceID" HeaderText="CPDeviceID" Visible="false">
                                    </rad:GridBoundColumn>
                                    <rad:GridBoundColumn DataField="CPDeviceStatusTypeID" HeaderText="CPDeviceStatusTypeID"
                                        Visible="false">
                                    </rad:GridBoundColumn>
                                    <rad:GridTemplateColumn HeaderText="AlertType" ItemStyle-HorizontalAlign="Center"
                                        AllowFiltering="false">
                                        <ItemTemplate>
                                            <img src="Images/<%#DataBinder.Eval(Container,"DataItem.StatusDesc") %>-Lamp.gif"
                                                alt="<%#DataBinder.Eval(Container,"DataItem.StatusDesc") %>" title="<%#DataBinder.Eval(Container,"DataItem.StatusDesc") %>"
                                                align="middle" />
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridBoundColumn DataField="Age" HeaderText="Age(HH:MM)" ItemStyle-HorizontalAlign="center"
                                        AllowFiltering="false">
                                    </rad:GridBoundColumn>
                                    <rad:GridBoundColumn DataField="CompanyName" HeaderText="Company" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains">
                                    </rad:GridBoundColumn>
                                    <rad:GridBoundColumn DataField="BranchName" HeaderText="Branch" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains">
                                    </rad:GridBoundColumn>
                                    <rad:GridBoundColumn DataField="WorkStation" HeaderText="WorkStation" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains">
                                    </rad:GridBoundColumn>
                                    <rad:GridBoundColumn DataField="CPDeviceDesc" HeaderText="Device" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains">
                                    </rad:GridBoundColumn>
                                    <rad:GridTemplateColumn HeaderText="AlertType" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-CssClass="ITEMSTYLE" AllowFiltering="false">
                                        <ItemTemplate>
                                            &nbsp;
                                            <asp:LinkButton ID="LBTN_Reset" runat="server" CommandName="EditProof" Visible="false">&nbsp;Reset&nbsp;</asp:LinkButton>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </rad:RadGrid>
                    </td>
                </tr>
                <tr>
                    <td height="10" colspan="2">
                        <img src="images/spacer.gif" alt="fujifilm" border="0" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
    
    <script language="javascript">
        document.getElementById("BOLD1").innerHTML = "";
        var i = 60 ;
        function ReduceCounter()
        {            
            if( i == 0 )
            { 
                window.location.href = window.location.href ;                  
            }
            else
            {
                if( i < 0 )
                { i = 60; }
                setTimeout( "ReduceCounter()" , 1000 );
                i = i - 1 ;
                document.getElementById("BOLD1").innerHTML = i ;
            }
        }
        ReduceCounter();
    </script>
</body>
</html>
