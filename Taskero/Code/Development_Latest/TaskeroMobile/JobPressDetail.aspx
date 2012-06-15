<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="JobPressDetail.aspx.cs"
    Inherits="JobPressDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="middle_part_page3">
        <div class="title_bar_page3">
            <div class="jobs_btn">
                <img src="images/press.png" alt="Job" title="Job" /></div>
            <div class="back_arow_btn">
                <a href="JobList.aspx">
                    <img src="images/arrow.png" alt="Back" title="Back" /></a></div>
            <div class="logout_span">
                <a href="Logout.aspx" class="logout">Logout</a></div>
            <div class="clear">
            </div>
        </div>
       <%-- <div class="top_inner_page3">
            <div class="filter_btn">
            </div>
            <div class="next_back_btn">
                <div class="back_arow_btn_page3">
                </div>
                <div class="next_arow_btn_page3">
                </div>
            </div>
            <div class="clear">
            </div>
        </div>--%>
        <div class="middle_part_page3_conn">
            <div class="inner_conn_page3">
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
                <div class="table_height">
                    <div class="table_rt">
                        <div class="table_lt">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table">
                                <tr>
                                    <td style="width: 81px;" class="start">
                                        Status
                                    </td>
                                    <td class="start">
                                        <img id="imgStatus" runat="server" alt="" /> 
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Job No
                                    </td>
                                    <td>
                                        <asp:Label ID="LblJobNo" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        First Sheet
                                    </td>
                                    <td>
                                        <asp:Label ID="LblFirstSheetDate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Last Sheet
                                    </td>
                                    <td>
                                        <asp:Label ID="LblLastSheetDate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Device
                                    </td>
                                    <td>
                                        <asp:Label ID="LblDevice" runat="server"></asp:Label>
                                    </td>
                                </tr>                                
                                <tr>
                                    <td>
                                        Work Station
                                    </td>
                                    <td>
                                        <asp:Label ID="LblWorkStation" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Branch Name
                                    </td>
                                    <td>
                                        <asp:Label ID="LblBranchName" runat="server"></asp:Label>
                                    </td>
                                </tr>                              
                                
                            </table>
                        </div>
                    </div>
                    <div class="table_rb">
                        <div class="table_lb">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="bottom_cor">
            <div>
            </div>
        </div>
    </div>
</asp:Content>
