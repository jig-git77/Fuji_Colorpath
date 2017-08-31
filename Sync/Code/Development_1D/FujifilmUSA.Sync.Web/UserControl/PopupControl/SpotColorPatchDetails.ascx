<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SpotColorPatchDetails.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.PopupControl.SpotColorPatchDetails" %>
<style>
    .PatchDetailsTable {
        line-height: 20px;
        width: 100%;
    }

        .PatchDetailsTable table {
            width: 100%;
        }

        .PatchDetailsTable th {
            background-color: darkgrey;
            color: white;
        }

        .PatchDetailsTable td {
            text-align: center;
        }
</style>
<div style="display: block;" class="contantbox">
    <div>
        <div style="float: left; width: 50%;">
            <table class="PatchDetailsTable">
                <tr>
                    <th>Target</th>
                    <th>Actual</th>
                </tr>
                <tr style="height: 75px;">
                    <td>
                        <div runat="server" id="divTarget" style="height: 75px;"></div>
                    </td>
                    <td>
                        <div runat="server" id="divActual" style="height: 75px;"></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <th>Metric</th>
                                <th>Target</th>
                            </tr>
                            <tr>
                                <td><b>L</b></td>
                                <td>
                                    <asp:Label runat="server" ID="lblTargetL"></asp:Label></td>
                            </tr>
                            <tr>
                                <td><b>A</b></td>
                                <td>
                                    <asp:Label runat="server" ID="lblTargetA"></asp:Label></td>
                            </tr>
                            <tr>
                                <td><b>B</b></td>
                                <td>
                                    <asp:Label runat="server" ID="lblTargetB"></asp:Label></td>
                            </tr>
                            <%--  <tr>
                                <td><b>H</b></td>
                                <td>
                                    <asp:Label runat="server" ID="lblTargetH">---</asp:Label></td>
                            </tr>
                            <tr>
                                <td><b>C</b></td>
                                <td>
                                    <asp:Label runat="server" ID="lblTargetC">---</asp:Label></td>
                            </tr>
                            <tr>
                                <td><b>PC</b></td>
                                <td>
                                    <asp:Label runat="server" ID="lblTargetPC">---</asp:Label></td>
                            </tr>--%>
                        </table>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <th>Actual</th>
                                <th>Delta</th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblActualL"></asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="lblDeltaL"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblActualA"></asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="lblDeltaA"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblActualB"></asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="lblDeltaB">---</asp:Label></td>
                            </tr>
                            <%--<tr>
                                <td>
                                    <asp:Label runat="server" ID="lblActualH"></asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="lblDeltaH">---</asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblActualC"></asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="lblDeltaC">---</asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblActualPC"></asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="lblDeltaPC">---</asp:Label></td>
                            </tr>--%>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table>
                <tr>
                    <td align="center" style="background: url(images/BGISOPaperGraph1_Sync4D.jpg) no-repeat center; width: 276px; padding-left: 20px; padding-bottom: 14px;">
                        <asp:Chart ID="chartPaper1" runat="server" BackImageMode="Scaled" BackImage="~/Images/ISOPaperGraph1_Sync4D.jpg"
                            ImageType="Jpeg" Width="240px" Height="130px" ImageLocation="~/UploadFiles/temp/SingleMeasurement_Page1_PaperLab1"
                            meta:resourcekey="chartPaper1Resource1">
                            <Legends>
                                <asp:Legend Name="Default" Enabled="False">
                                </asp:Legend>
                            </Legends>
                            <Series>
                                <asp:Series Name="Green" ChartType="Polar"
                                    MarkerSize="10" MarkerBorderWidth="2" MarkerBorderColor="Green" MarkerStyle="Circle"
                                    MarkerColor="Green" ShadowOffset="1" ChartArea="Default" Legend="Default">
                                </asp:Series>
                                <asp:Series Name="Paper" ChartType="Polar" MarkerBorderColor="Black" MarkerColor="White"
                                    MarkerSize="10" MarkerBorderWidth="2"
                                    MarkerStyle="Circle" ShadowOffset="1" ChartArea="Default" Legend="Default">
                                </asp:Series>
                            </Series>
                            <BorderSkin PageColor="AliceBlue" />
                            <Titles>
                                <asp:Title Name="Title1">
                                </asp:Title>
                            </Titles>
                            <ChartAreas>
                                <asp:ChartArea BackColor="Transparent" BorderColor="" Name="Default">
                                    <AxisY LineColor="120, 0, 0, 0" TitleAlignment="Near" Maximum="10" Minimum="-10"
                                        LineDashStyle="NotSet">
                                        <MajorGrid Enabled="False" />
                                        <MajorTickMark Enabled="False" />
                                        <LabelStyle Enabled="False" />
                                    </AxisY>
                                    <AxisX IsMarginVisible="False" TitleAlignment="Near" Maximum="20" Minimum="-20" LineDashStyle="NotSet">
                                        <MajorGrid Enabled="False" />
                                        <MajorTickMark Enabled="False" />
                                        <LabelStyle Enabled="False" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                    <td align="center" style="background: url(images/BGISOPaperGraph2_Sync4D.jpg) no-repeat center; width: 104px; height: 169px; padding-left: 22px; padding-bottom: 12px;">


                        <asp:Chart ID="chartPaper2" runat="server" BackImageMode="Scaled" BackImage="~/Images/ISOPaperGraph2_Sync4D.jpg"
                            ImageType="Jpeg" Width="69px" Height="130px" ImageLocation="~/UploadFiles/temp/SingleMeasurement_Page1_PaperLab2"
                            meta:resourcekey="chartPaper2Resource1">
                            <Legends>
                                <asp:Legend Name="Default" Enabled="False">
                                </asp:Legend>
                            </Legends>
                            <Series>
                                <asp:Series Name="Series1" BorderColor="100, 0, 0, 0" ChartArea="Default"
                                    IsVisibleInLegend="False" Legend="Default">
                                </asp:Series>
                                <asp:Series Name="Series2" Color="Green" ChartArea="Default" BorderWidth="2"
                                    IsVisibleInLegend="False" Legend="Default">
                                </asp:Series>
                            </Series>
                            <BorderSkin PageColor="AliceBlue" />
                            <Titles>
                                <asp:Title Name="Title1">
                                </asp:Title>
                            </Titles>
                            <ChartAreas>
                                <asp:ChartArea BackColor="Transparent" BorderColor="" Name="Default">
                                    <AxisY LineColor="120, 0, 0, 0" TitleAlignment="Near" Maximum="10" Minimum="-10"
                                        LineDashStyle="NotSet">
                                        <MajorGrid Enabled="False" />
                                        <MajorTickMark Enabled="False" />
                                        <LabelStyle Enabled="False" />
                                    </AxisY>
                                    <AxisX TitleAlignment="Near" LineDashStyle="NotSet">
                                        <MajorGrid Enabled="False" />
                                        <MajorTickMark Enabled="False" />
                                        <LabelStyle Enabled="False" />
                                    </AxisX>
                                    <AxisX2 Minimum="0" Maximum="1" LineDashStyle="NotSet">
                                        <MajorGrid Enabled="False" />
                                        <MajorTickMark Enabled="False" />
                                        <LabelStyle Enabled="False" />
                                    </AxisX2>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                </tr>
            </table>
            <%--<div style="background: url(images/BGISOPaperGraph1_Sync4D.jpg) no-repeat center; width: 276px; height: 180px; padding-left: 20px; padding-bottom: 14px;">
                
            </div>--%>
        </div>
        <%--<div></div>--%>
    </div>
</div>


