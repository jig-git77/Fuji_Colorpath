<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="DeviceList.aspx.cs"
    Inherits="DeviceList" %>
    
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="middle_part_page3">
        <div class="title_bar_page3">
            <div class="jobs_btn">
                <asp:ImageButton ID="IbtnDevice" runat="server" ImageUrl="~/images/device.png"
                    OnClick="IbtnDevice_Click" />
                </div>
            <div class="back_arow_btn">
                <a href="Home.aspx">
                    <img src="images/arrow.png" alt="Back" title="Back" /></a></div>
            <div class="logout_span">
                <a href="Logout.aspx" class="logout">Logout</a></div>
            <div class="clear">
            </div>
        </div>
        <div class="top_inner_page3">
            <div class="filter_btn">
                <asp:ImageButton ID="IbtnFilter" runat="server" ImageUrl="~/images/btn_filter.png"
                    OnClientClick="return Filter()" />
            </div>
            <div class="next_back_btn">
                    <div class="back_arow_btn_page3">
                        <asp:ImageButton ID="IbtnDeviceBack" runat="server" ImageUrl="~/images/back.png" OnClick="IbtnDeviceBack_Click" />
                    </div>
                    <div class="next_arow_btn_page3">
                        <asp:ImageButton ID="IbtnDeviceNext" runat="server" ImageUrl="~/images/next.png" OnClick="IbtnDeviceNext_Click" />
                    </div>                    
                </div>
            <div class="clear">
            </div>
        </div>
        <div class="middle_part_page3_conn">
            <div class="inner_conn_page3">
                <asp:Panel ID="panFilter" runat="server" Style="display: none">
                    <div class="page3_line_1">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="form-text">
                            <tr>
                                <td>
                                    Status :
                                </td>
                                <td>
                                    <asp:DropDownList ID="DdlStatus" runat="server" CssClass="TEXTBOX">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Device :
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtDevice" CssClass="TEXTBOX" runat="server" Width="220px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Workstation :
                                </td>
                                <td>
                                     <asp:TextBox ID="TxtWorkstation" CssClass="TEXTBOX" runat="server" Width="220px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Branch :
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtBranch" CssClass="TEXTBOX" runat="server" Width="220px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 2px;">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:ImageButton ID="IbtnSearch" runat="server" ImageUrl="~/images/btn_search.png"
                                        OnClick="IbtnSearch_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 2px;">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <div class="clear">
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="panData" runat="server">
                    <div class="page3_line_1">
                        <ul>
                            <li>
                                <div class="icon_Green">
                                    Green</div>
                            </li>
                            <li>
                                <div class="icon_Yellow">
                                    Yellow</div>
                            </li>
                            <li>
                                <div class="icon_Orange">
                                    Orange</div>
                            </li>
                            <li>
                                <div class="icon_Red">
                                    Red</div>
                            </li>
                        </ul>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="scroll">
                        <%--<div id='mycustomscroll1' class='flexcroll'>--%>
                        <asp:DataGrid ID="DGDevices" runat="server" AllowPaging="true" ShowHeader="false"
                            PageSize="10" Width="100%" AutoGenerateColumns="false" GridLines="None" OnItemDataBound="DGDevices_ItemDataBound"
                            OnPageIndexChanged="DGDevices_PageIndexChanged" PagerStyle-CssClass="PAGERSTYLE" PagerStyle-PageButtonCount="5"
                            PagerStyle-Mode="NumericPages">
                            <Columns>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <ul class="pageitem">
                                            <li><a id="hrefDeviceDetail" runat="server"><span class="arrow"><span class="thumb"><strong>Status</strong><img
                                                id="imgStatus" runat="server" alt="" /></span><span class="text"><strong style="display:block;padding:8px 0px 3px 0px;">Device.</strong><asp:Literal
                                                    ID="litDeviceName" runat="server"></asp:Literal></span></span></a> </li>
                                        </ul>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                        <%--</div>--%>
                    </div>
                </asp:Panel>
            </div>
        </div>
        <div class="clear">
        </div>
        <div class="bottom_cor">
            <div>
            </div>
        </div>
    </div>

    <script language="javascript" type="text/javascript">
        function Filter()
        {
            var obj = document.getElementById('<%=panFilter.ClientID%>');
            if(obj.style.display == "none")
            {
                obj.style.display = "inline";
            }
            else
            {
                obj.style.display = "none";
            }
            return false;
        }
    </script>

</asp:Content>
