<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeviceList.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.DeviceList" %>
<div class="contant_top">
    &nbsp;</div>
<div class="contant_middle">
    <div class="head">
        <h1>
            Device List</h1>
        <div class="clear">
        </div>
        <div class="ContainerClass">
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr id="trFilter" runat="server">
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td class="DeviceListFirstTD">
                                    <asp:Label ID="lblCompany" runat="server" Text="Company" CssClass="LabelTitle"></asp:Label>
                                    &nbsp; :
                                </td>
                                <td>
                                   <%-- <telerik:RadComboBox ID="cmbCompany" runat="server" DataValueField="Key" DataTextField="Value"
                                        OnSelectedIndexChanged="cmbCompany_SelectedIndexChanged" AutoPostBack="true">
                                    </telerik:RadComboBox>--%>
                                    <asp:DropDownList ID="cmbCompany" runat="server" DataValueField="Key" DataTextField="Value"
                                        onselectedindexchanged="cmbCompany_SelectedIndexChanged1" AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="DeviceListFirstTD">
                                    <asp:Label ID="lblBranch" runat="server" Text="Branch" CssClass="LabelTitle"></asp:Label>
                                    &nbsp :
                                </td>
                                <td>
                                    <%--<telerik:RadComboBox ID="cmbBranch" runat="server" DataValueField="Key" DataTextField="Value"
                                        OnSelectedIndexChanged="cmbBranch_SelectedIndexChanged" AutoPostBack="true">
                                    </telerik:RadComboBox>--%>
                                     <asp:DropDownList ID="cmbBranch" runat="server" DataValueField="Key" DataTextField="Value"
                                        onselectedindexchanged="cmbBranch_SelectedIndexChanged1" AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" class="Hight30px">
                                    <hr />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td colspan="2">
                                     <telerik:RadListView ID="rlvDeviceList" runat="server" ItemPlaceholderID="ListViewContainer"
                                        OnNeedDataSource="rlvDeviceList_NeedDataSource" OnItemDataBound="rlvDeviceList_ItemDataBound" >
                                        <LayoutTemplate>
                                            <asp:PlaceHolder runat="server" ID="ListViewContainer" />
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <div class="product_list">
                                                <ul>
                                                    <li>
                                                        <div class="left">
                                                            <%# Eval("DeviceName")%>
                                                        </div>
                                                        <div class="right">
                                                            <asp:LinkButton ID="lbtnSelectDevice" runat="server" CssClass="gray_bt" OnClick="lbtnSelectDevice_Click"><span>Optimize</span></asp:LinkButton>
                                                            </div>                                                        
                                                            <div class="right">
                                                            <asp:LinkButton ID="lbtnDLP" runat="server" CssClass="gray_bt" OnClick="lbtnDLP_Click"><span>DLP</span></asp:LinkButton>
                                                            </div>
                                                       <div class="right">
                                                            <asp:LinkButton ID="lbtnDestination" runat="server" CssClass="gray_bt" OnClick="lbtnDestination_Click"><span>Destination</span></asp:LinkButton>
                                                            </div>                                                                                                                
                                                        <div class="clear">
                                                        </div>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="clear">
                                            </div>
                                        </ItemTemplate>
                                    </telerik:RadListView>                                   
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="clear">
    </div>
</div>
<div class="contant_bottom">
    &nbsp;</div>
