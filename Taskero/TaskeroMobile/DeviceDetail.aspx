<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="DeviceDetail.aspx.cs" Inherits="TaskeroMobile.DeviceDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="middle_part_page3">
        <div class="title_bar_page3">
            <div class="jobs_btn">
                <img src="images/device.png" alt="Job" title="Job" /></div>
            <div class="back_arow_btn">
                <a href="DeviceList.aspx">
                    <img src="images/arrow.png" alt="Back" title="Back" /></a></div>
            <div class="logout_span">
                <a href="Logout.aspx" class="logout">Logout</a></div>
            <div class="clear">
            </div>
        </div>
        <%--<div class="top_inner_page3">
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
                                <tr>
                                    <td>
                                        Device Type
                                    </td>
                                    <td>
                                        <asp:Label ID="LblDeviceType" runat="server"></asp:Label>     
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Date
                                    </td>
                                    <td>
                                        <asp:Label ID="LblDate" runat="server"></asp:Label>     
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
