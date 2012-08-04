<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GridviewwithFiltering.aspx.vb"
    Inherits="GridviewwithFiltering"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head profile="http://gmpg.org/xfn/11">
    <link rel="stylesheet" type="text/css" href="gridview.css" media="all" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <h3>Gridview with Filtering</h3>
            <div class="GridviewDiv">
            <table style="width: 540px" border="0" cellpadding="0" cellspacing="1" class="GridviewTable">
                <tr >
                    <td style="width: 40px;">
                        ID
                    </td>
                    <td style="width: 120px;" >
                        First Name
                    </td>
                    <td style="width: 120px;">
                        Last Name
                    </td>
                    <td style="width: 130px;">
                        Department
                    </td>
                    <td style="width: 130px;">
                        Location
                    </td>
                </tr>
                <tr >
                    <td style="width: 40px;">
                    </td>
                    <td style="width: 120px;">
                    </td>
                    <td style="width: 120px;">
                    </td>
                    <td style="width: 130px;">
                        <asp:DropDownList ID="ddldepartment" DataSourceID="dsPopulateDepartment" AutoPostBack="true"
                            DataValueField="department" runat="server" Width="120px" Font-Size="11px" AppendDataBoundItems="true">
                            <asp:ListItem Text="All" Value="%"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width: 130px;">
                        <asp:DropDownList ID="ddlLocation" DataSourceID="dsPopulateLocation" AutoPostBack="true"
                            DataValueField="location" runat="server" Width="120px" Font-Size="11px" AppendDataBoundItems="true">
                            <asp:ListItem Text="All" Value="%"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <asp:GridView ID="Gridview1" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            AllowSorting="true" DataSourceID="dsGridview" Width="540px" PageSize="10" CssClass="Gridview">
                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="Sort" SortExpression="id" ItemStyle-Width="40px"
                                    ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="FirstName" HeaderText="Sort" SortExpression="FirstName"
                                    ItemStyle-Width="120px" />
                                <asp:BoundField DataField="LastName" HeaderText="Sort" SortExpression="LastName"
                                    ItemStyle-Width="120px" />
                                <asp:BoundField DataField="Department" HeaderText="Sort" SortExpression="Department"
                                    ItemStyle-Width="130px" />
                                <asp:BoundField DataField="Location" HeaderText="Sort" SortExpression="Location"
                                    ItemStyle-Width="130px" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            </div>
            <asp:SqlDataSource ID="dsGridview" runat="server" ConnectionString="<%$ ConnectionStrings:EvonetConnectionString %>"
                SelectCommand="SELECT * FROM [T_Employees]" FilterExpression="Department like '{0}%'
                and Location like '{1}%'">
                <FilterParameters>
                    <asp:ControlParameter Name="Department" ControlID="ddldepartment" PropertyName="SelectedValue" />
                    <asp:ControlParameter Name="Location" ControlID="ddllocation" PropertyName="SelectedValue" />
                </FilterParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="dsPopulateDepartment" runat="server" ConnectionString="<%$ ConnectionStrings:EvonetConnectionString %>"
                SelectCommand="SELECT DISTINCT Department from [T_Employees]"></asp:SqlDataSource>
            <asp:SqlDataSource ID="dsPopulateLocation" runat="server" ConnectionString="<%$ ConnectionStrings:EvonetConnectionString %>"
                SelectCommand="SELECT DISTINCT Location FROM [T_Employees]"></asp:SqlDataSource>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>