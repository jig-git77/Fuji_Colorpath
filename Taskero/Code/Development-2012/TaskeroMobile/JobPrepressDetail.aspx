<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="JobPrepressDetail.aspx.cs"
    Inherits="JobPrepressDetail" %>

<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="middle_part_page3">
        <div class="title_bar_page3">
            <div class="jobs_btn">
                <img src="images/prepress.png" alt="Job" title="Job" /></div>
            <div class="back_arow_btn">
                <a href="JobList.aspx">
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
                                        Print Target
                                    </td>
                                    <td>
                                        <asp:Label ID="LblPrintTarget" runat="server"></asp:Label>
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
                    <br />
                    <div class="table_rt">
                        <div class="table_lt">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table">
                                <tr>
                                    <td style="width: 1px;" class="start">
                                    </td>
                                    <td style="width: 80px; padding: 0px 7px 0px 0px;" class="start">
                                    </td>
                                    <td class="start" style="padding-left: 30px">
                                        <b>Tolerance</b>
                                    </td>
                                    <td class="start">
                                        <b>Actual</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <img id="imgdePaper" runat="server" alt="" />
                                    </td>
                                    <td valign="middle" style="padding: 0px 7px 0px 0px;">
                                        <b>de Paper :</b>
                                    </td>
                                    <td style="padding-left: 30px">
                                        <asp:Label ID="lbldePaperTol" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbldePaperAct" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <img id="imgdEAve" runat="server" alt="" />
                                    </td>
                                    <td valign="middle" style="padding: 0px 7px 0px 0px;">
                                        <b>dE Ave :</b>
                                    </td>
                                    <td style="padding-left: 30px">
                                        <asp:Label ID="lbldEAveTol" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbldEAveAct" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <img id="imgdEMax" runat="server" alt="" />
                                    </td>
                                    <td valign="middle" style="padding: 0px 7px 0px 0px;">
                                        <b>dE Max :</b>
                                    </td>
                                    <td style="padding-left: 30px">
                                        <asp:Label ID="lbldEMaxTol" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbldEMaxAct" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <img id="imgdEPrim" runat="server" alt="" />
                                    </td>
                                    <td valign="middle" style="padding: 0px 7px 0px 0px;">
                                        <b>dE Prim :</b>
                                    </td>
                                    <td style="padding-left: 30px">
                                        <asp:Label ID="lbldEPrimTol" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbldEPrimAct" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <img id="imgdHPrim" runat="server" alt="" />
                                    </td>
                                    <td valign="middle" style="padding: 0px 7px 0px 0px;">
                                        <b>dH Prim :</b>
                                    </td>
                                    <td style="padding-left: 30px">
                                        <asp:Label ID="lbldHPrimTol" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbldHPrimAct" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <img id="imgdHGray" runat="server" alt="" />
                                    </td>
                                    <td valign="middle" style="padding: 0px 7px 0px 0px;">
                                        <b>dH Gray :</b>
                                    </td>
                                    <td style="padding-left: 30px">
                                        <asp:Label ID="lbldHGrayTol" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbldHGrayAct" runat="server"></asp:Label>
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
            <div style="background-color: #CDE4DC">
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table">
                    <tr>
                        <td align="right">
                            Show Only Current Print Target Data
                        </td>
                        <td style="width:15px;" align="left">
                            <asp:CheckBox ID="ChkPrintTarget" Text="" runat="server" AutoPostBack="True" 
                                oncheckedchanged="ChkPrintTarget_CheckedChanged"  />
                        </td>
                    </tr>
                </table>
                <div>
                    <DCWC:Chart ID="CHRT_PrepressTargetData" runat="server" Width="480px" Height="150px"
                        BackColor="#CDE4DC" ImageUrl="~/UploadFiles/temp/CHRT_Density_#SEQ(300,3)" ImageType="Jpeg"
                        Palette="None" BackGradientEndColor="#CDE4DC" BorderLineStyle="Solid" BorderLineWidth="1"
                        BorderLineColor="#5CA78C">
                        <Titles>
                            <DCWC:Title Text="Density" Visible="false">
                            </DCWC:Title>
                        </Titles>
                        <Series>
                            <DCWC:Series Name="Series1" ChartArea="ChartArea1" BorderWidth="2" ShowInLegend="false">
                            </DCWC:Series>
                            <DCWC:Series Name="Series2" ChartArea="ChartArea1" BorderWidth="2" ShowInLegend="false">
                            </DCWC:Series>
                        </Series>
                        <UI>
                            <Toolbar BackColor="#DFDFDF" BorderColor="#DFDFDF">
                                <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                    FrameBackColor="SteelBlue"></BorderSkin>
                            </Toolbar>
                        </UI>
                        <ChartAreas>
                            <DCWC:ChartArea Name="ChartArea1" BackColor="#D4EAE2">
                                <Position Auto="true" Height="92" Width="97" X="0" Y="2" />
                                <%--<InnerPlotPosition Auto="true"  Height="97" Width="95" X="8" Y="2" />--%>
                                <AxisY Enabled="True" LineColor="#A4D0C0">
                                    <MajorGrid LineColor="#D4EAE2" />
                                </AxisY>
                                <AxisX Enabled="True" LineColor="#A4D0C0">
                                    <LabelStyle FontColor="Transparent" />
                                    <MajorGrid LineColor="#D4EAE2" />
                                </AxisX>
                            </DCWC:ChartArea>
                        </ChartAreas>
                    </DCWC:Chart>
                </div>
            </div>
        </div>
        <div class="bottom_cor">
            <div>
            </div>
        </div>
    </div>
</asp:Content>
