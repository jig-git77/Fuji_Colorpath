<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="JobList.aspx.cs" Inherits="TaskeroMobile.JobList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="middle_part_page3">
        <div class="title_bar_page3">
            <div class="jobs_btnPrepress">
                <asp:ImageButton ID="IbtnPrepress" runat="server" ImageUrl="~/images/prepress.png"
                    OnClick="IbtnPrepress_Click" /></div>
            <div class="jobs_btnPrepress">
                <asp:ImageButton ID="IbtnPress" ImageUrl="~/images/press.png" runat="server" OnClick="IbtnPress_Click" /></div>
            <div class="back_arow_btn">
                <a href="Home.aspx">
                    <img src="images/arrow.png" alt="Back" title="Back" /></a></div>
            <div class="logout_span">
                <a href="Logout.aspx" class="logout">Logout</a></div>
            <div class="clear">
            </div>
        </div>
        <asp:Panel ID="panPrepress" runat="server">
            <div class="top_inner_page3">
                <div class="filter_btn">
                    <asp:ImageButton ID="IbtnPrepressFilter" runat="server" ImageUrl="~/images/btn_filter.png"
                        OnClientClick="return PrepressFilter()" />
                </div>
                <div class="next_back_btn">
                    <div class="back_arow_btn_page3">
                        <asp:ImageButton ID="IbtnPrepressBack" runat="server" ImageUrl="~/images/back.png"
                            OnClick="IbtnPrepressBack_Click" />
                    </div>
                    <div class="next_arow_btn_page3">
                        <asp:ImageButton ID="IbtnPrepressNext" runat="server" ImageUrl="~/images/next.png"
                            OnClick="IbtnPrepressNext_Click" />
                    </div>
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="middle_part_page3_conn">
                <div class="inner_conn_page3">
                    <asp:Panel ID="panPrepressFilter" runat="server" Style="display: none">
                        <div class="page3_line_1">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%" class="form-text">
                                <tr>
                                    <td>
                                        Status :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DdlPrepressStatus" runat="server" CssClass="TEXTBOX">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Job No :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtPrepressJobNo" CssClass="TEXTBOX" runat="server" Width="220px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Date :
                                    </td>
                                    <td>
                                        <div style="z-index: 1; position: relative">
                                            <asp:TextBox ID="TxtPrepressDate" runat="server" CssClass="TEXTBOX" MaxLength="10"
                                                Columns="10" Width="75px"></asp:TextBox>&nbsp;<asp:ImageButton ID="IBTN_PrepressDate"
                                                    runat="server" ImageUrl="Images/calendar-icon.png" ImageAlign="AbsMiddle">
                                            </asp:ImageButton><ajaxToolkit:CalendarExtender ID="Cal_PrepressDate" runat="server"
                                                TargetControlID="TxtPrepressDate" PopupButtonID="IBTN_PrepressDate">
                                            </ajaxToolkit:CalendarExtender>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Branch :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtPrepressBranch" CssClass="TEXTBOX" runat="server" Width="220px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Device :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtPrepressDevice" CssClass="TEXTBOX" runat="server" Width="220px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 2px;">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:ImageButton ID="IbtnPrepressSearch" runat="server" ImageUrl="~/images/btn_search.png"
                                            OnClick="IbtnPrepressSearch_Click" />
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
                    <asp:Panel ID="panPrepressData" runat="server">
                        <div class="page3_line_1">
                            <ul>
                                <li>
                                    <div class="icon_passed">
                                        Passed</div>
                                </li>
                                <li>
                                    <div class="icon_Failed">
                                        Failed</div>
                                </li>
                                <li>
                                    <div class="icon_Caution">
                                        Caution</div>
                                </li>
                            </ul>
                            <div class="clear">
                            </div>
                        </div>
                        <div class="scroll">
                            <%--<div id='mycustomscroll1' class='flexcroll'>--%>
                            <asp:DataGrid ID="DGPrepress" runat="server" AllowPaging="true" ShowHeader="false"
                                PageSize="10" Width="100%" AutoGenerateColumns="false" GridLines="None" OnItemDataBound="DGPrepress_ItemDataBound"
                                OnPageIndexChanged="DGPrepress_PageIndexChanged" PagerStyle-CssClass="PAGERSTYLE"
                                PagerStyle-PageButtonCount="5" PagerStyle-Mode="NumericPages">
                                <Columns>
                                    <asp:TemplateColumn>
                                        <ItemTemplate>
                                            <ul class="pageitem">
                                                <li><a id="hrefPrepressDetail" runat="server"><span class="arrow"><span class="thumb">
                                                    <strong>Status</strong><img id="imgStatus" runat="server" alt="" src="images/status_passed.png" /></span>
                                                    <span class="text">
                                                        <div> <strong>Job No : </strong><span><asp:Literal ID="litJobName" runat="server"></asp:Literal></span></div>
                                                        <div> <strong>Time Stamp : </strong><asp:Literal ID="litTimeStamp"  runat="server"></asp:Literal></div>
                                                        <div> <strong>Device : </strong><asp:Literal ID="litDevice" runat="server"></asp:Literal></div>
                                                        </span></span></a>
                                                </li>
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
        </asp:Panel>
        <asp:Panel ID="panPress" runat="server">
            <div class="top_inner_page3">
                <div class="filter_btn">
                    <asp:ImageButton ID="IbtnPressFilter" runat="server" ImageUrl="~/images/btn_filter.png"
                        OnClientClick="return PressFilter()" /></div>
                <div class="next_back_btn">
                    <div class="back_arow_btn_page3">
                        <asp:ImageButton ID="IbtnPressBack" runat="server" ImageUrl="~/images/back.png" OnClick="IbtnPressBack_Click" />
                    </div>
                    <div class="next_arow_btn_page3">
                        <asp:ImageButton ID="IbtnPressNext" runat="server" ImageUrl="~/images/next.png" OnClick="IbtnPressNext_Click" />
                    </div>
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="middle_part_page3_conn">
                <div class="inner_conn_page3">
                    <asp:Panel ID="panPressFilter" runat="server" Style="display: none">
                        <div class="page3_line_1">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%" class="form-text">
                                <tr>
                                    <td>
                                        Status :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DdlPressStatus" runat="server" CssClass="TEXTBOX">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Job No :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtPressJobNo" CssClass="TEXTBOX" runat="server" Width="220px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Workstation :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtPressWorkstation" CssClass="TEXTBOX" runat="server" Width="220px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Branch :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtPressBranch" CssClass="TEXTBOX" runat="server" Width="220px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 2px;">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:ImageButton ID="IbtnPressSearch" runat="server" ImageUrl="~/images/btn_search.png"
                                            OnClick="IbtnPressSearch_Click" />
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
                    <asp:Panel ID="panPressData" runat="server">
                        <div class="page3_line_1">
                            <ul>
                                <li>
                                    <div class="icon_Complete">
                                        Complete</div>
                                </li>
                                <li>
                                    <div class="icon_Production">
                                        Production</div>
                                </li>
                                <li>
                                    <div class="icon_MakeReady">
                                        Make Ready</div>
                                </li>
                            </ul>
                            <div class="clear">
                            </div>
                        </div>
                        <div class="scroll">
                            <%-- <div id='mycustomscroll2' class='flexcroll'>--%>
                            <asp:DataGrid ID="DGPress" runat="server" AllowPaging="true" ShowHeader="false" PageSize="10"
                                Width="100%" AutoGenerateColumns="false" GridLines="None" OnItemDataBound="DGPress_ItemDataBound"
                                OnPageIndexChanged="DGPress_PageIndexChanged" PagerStyle-CssClass="PAGERSTYLE"
                                PagerStyle-PageButtonCount="5" PagerStyle-VerticalAlign="Top" PagerStyle-Mode="NumericPages">
                                <Columns>
                                    <asp:TemplateColumn>
                                        <ItemTemplate>
                                            <ul class="pageitem">
                                                <li><a id="hrefPressDetail" runat="server"><span class="arrow"><span class="thumb"><strong>
                                                    Status</strong><img id="imgStatus" runat="server" alt="" src="images/status_passed.png" /></span><span
                                                        class="text"><strong style="display:block;padding:8px 0px 3px 0px;">Job No.</strong><asp:Literal ID="litJobName" runat="server"></asp:Literal></span></span></a>
                                                </li>
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
        </asp:Panel>
        <div class="clear">
        </div>
        <div class="bottom_cor">
            <div>
            </div>
        </div>
    </div>

    <script language="javascript" type="text/javascript">
        function PrepressFilter() {
            var obj = document.getElementById('<%=panPrepressFilter.ClientID%>');
            if (obj.style.display == "none") {
                obj.style.display = "inline";
            }
            else {
                obj.style.display = "none";
            }
            return false;
        }
        function PressFilter() {
            var obj = document.getElementById('<%=panPressFilter.ClientID%>');
            if (obj.style.display == "none") {
                obj.style.display = "inline";
            }
            else {
                obj.style.display = "none";
            }
            return false;
        }
    </script>

</asp:Content>
