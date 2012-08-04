<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AssignedOrders.aspx.cs" Inherits="AssignedOrders" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Assigned Orders</title>
    <link href="stylesheet/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form2" runat="server"><asp:ScriptManager ID="ScriptManager1" runat="server"  >
    </asp:ScriptManager> <asp:UpdatePanel ID="UpdatePanelOrders" runat="server">
    <ContentTemplate>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="order_id" Width="820px" CssClass="Grid" OnRowCommand="GridView1_RowCommand"  >
            <Columns>
                <asp:HyperLinkField DataTextField="order_id" HeaderText="Order ID" NavigateUrl="~/OrderInfo.aspx" DataNavigateUrlFields="order_id" DataNavigateUrlFormatString="~/OrderInfo.aspx?order_id={0}" >
                <ItemStyle Width="30px" />
                </asp:HyperLinkField>
                <asp:BoundField DataField="site_company_descr" HeaderText="Site Name" SortExpression="cust_company_id" >
                    <ItemStyle Width="200px" />
                </asp:BoundField>
                <asp:BoundField DataField="bpart_id" HeaderText="Model" SortExpression="bpart_id" >
                    <ItemStyle Width="150px" />
                </asp:BoundField>
                <asp:BoundField DataField="problem_desc" HeaderText="Problem Descr" SortExpression="problem_desc" >
                    <ItemStyle Width="200px" />
                </asp:BoundField>
                <asp:BoundField DataField="resolve_text" HeaderText="Notes"  >
                    <ItemStyle Width="200px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Confirm on site">
                    <ItemTemplate>
                     <asp:Button ID="lnkButton" CommandName="OnSite"
                         CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Order_ID").ToString() %>' Text="On Site"
                         runat="server" Visible='<%# DataBinder.Eval(Container.DataItem,"Status").ToString() == "0" %>' CssClass="OnSiteBtn"  />
                    </ItemTemplate>
                    <ItemStyle Width="20px" />
                </asp:TemplateField>

                <asp:HyperLinkField DataNavigateUrlFields="kt_resolve,order_id" DataNavigateUrlFormatString="http://ftss.fujifilmgs.com/problemspec/_15Minutes.aspx?orderid={1}&SessionID=99912399911" HeaderText="KT Resolve" DataTextField="kt_resolve" DataTextFormatString="<img src='images/icon_{0}.png' alt='{0}' border='0'" Target="_blank" /> 

            </Columns>
            <HeaderStyle CssClass="GridHeader"> </HeaderStyle>
            <AlternatingRowStyle CssClass="GridAtlItem"/>
        </asp:GridView>
        <br />
        <asp:Button ID="ExitButton" runat="server" OnClick="ExitButton_Click" Text="Logout"
            Width="74px" CssClass="SubmitButton" /><br /><asp:HiddenField ID="_UserLocalDT" runat="server" />
        <br />
<asp:Label ID="lblError" runat="server" CssClass="errorlabel_bold"></asp:Label>
</ContentTemplate>
      </asp:UpdatePanel>
    </form>
</body>
</html>
