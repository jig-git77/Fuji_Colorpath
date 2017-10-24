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
        <div style="float: left; width: 70%;">
            <table class="PatchDetailsTable">
                <tr>
                    <th>Target</th>
                    <th>Predicted</th>
                    <th>Measured</th>
                </tr>
                <tr style="height: 75px;">
                    <td>
                        <div runat="server" id="divTarget" style="height: 75px;"></div>
                    </td>
                    <td>
                        <div runat="server" id="divActual" style="height: 75px;"></div>
                    </td>
                    <td>
                        <div runat="server" id="divMeasured" style="height: 75px;"></div>
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
                                <th>Predicted</th>
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
                    <td>
                        <table>
                            <tr>
                                <th>Measured</th>
                                <th>DeltaL</th>
                                <th>DeltaA</th>
                                <th>DeltaB</th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="Label1" Text="Measured_L*"></asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="Label2" Text="0"></asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="Label7" Text="0"></asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="Label8" Text="0"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="Label3" Text="Measured_a*"></asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="Label4" Text="0"></asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="Label9" Text="0"></asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="Label10" Text="0"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="Label5" Text="Measured_b*"></asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="Label6" Text="0"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="Label11" Text="0"></asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="Label12" Text="0"></asp:Label></td>
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
                    <%-- <td align="center" style="background: url(images/SpotAB_Bg.jpg) no-repeat center; width: 276px; padding-left: 20px; padding-bottom: 14px;">--%>
                    <td align="center" style="width: 178px; padding-bottom: 14px;">
                        <asp:Chart ID="chartPaper1" runat="server" BackImage="~/Images/SpotAB_Bg.gif" BackImageWrapMode="Scaled"
                            ImageType="Jpeg" Width="178px" Height="180px" ImageLocation="~/UploadFiles/temp/SingleMeasurement_Page1_PaperLab1"
                            meta:resourcekey="chartPaper1Resource1">
                            <Legends>
                                <asp:Legend Name="Default" Enabled="False">
                                </asp:Legend>
                            </Legends>
                            <Series>
                                <asp:Series Name="Red" ChartType="Polar"  Color="Red"
                                     MarkerSize="20" MarkerBorderWidth="2" MarkerBorderColor="Red" MarkerStyle="Circle" 
                                     MarkerColor="Red" ShadowOffset="1" ChartArea="Default" Legend="Default">
                                </asp:Series>
                                <asp:Series Name="Green" ChartType="Polar" Color="Green"
                                    MarkerSize="10" MarkerBorderWidth="2" MarkerBorderColor="Green" MarkerStyle="Circle"
                                    MarkerColor="Green" ShadowOffset="1" ChartArea="Default" Legend="Default">
                                </asp:Series>
                            </Series>
                            <BorderSkin PageColor="AliceBlue" />
                            <Titles>
                                <asp:Title Name="Title1">
                                </asp:Title>
                            </Titles>
                            <ChartAreas>
                                <asp:ChartArea BackColor="Transparent" BorderColor="" Name="Default">
                                    <AxisY LineColor="120, 0, 0, 0" TitleAlignment="Near" Maximum="100" Minimum="-100"
                                        LineDashStyle="NotSet">
                                        <MajorGrid Enabled="False" />
                                        <MajorTickMark Enabled="False" />
                                        <LabelStyle Enabled="False" />
                                    </AxisY>
                                    <AxisX IsMarginVisible="False" TitleAlignment="Near" Maximum="100" Minimum="-100" LineDashStyle="NotSet">
                                        <MajorGrid Enabled="False" />
                                        <MajorTickMark Enabled="False" />
                                        <LabelStyle Enabled="False" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                  
                    <td align="center" style="width: 50px; padding-left:22px;">

                        <asp:Chart ID="chartPaper2" runat="server" BackImage="~/Images/SpotL_Bg.jpg" BackImageWrapMode="Scaled"
                            ImageType="Jpeg" Width="50px" Height="180px" ImageLocation="~/UploadFiles/temp/SingleMeasurement_Page1_PaperLab2"
                            meta:resourcekey="chartPaper2Resource1">
                            <Legends>
                                <asp:Legend Name="Default" Enabled="False">
                                </asp:Legend>
                            </Legends>
                            <Series>
                                <asp:Series Name="Red" ChartType="Polar"  Color="Red"
                                     MarkerSize="20" MarkerBorderWidth="2" MarkerBorderColor="Red" MarkerStyle="Circle" 
                                     MarkerColor="Red" ShadowOffset="1" ChartArea="Default" Legend="Default">
                                </asp:Series>
                                <asp:Series Name="Green" ChartType="Polar" Color="Green"
                                    MarkerSize="10" MarkerBorderWidth="2" MarkerBorderColor="Green" MarkerStyle="Circle"
                                    MarkerColor="Green" ShadowOffset="1" ChartArea="Default" Legend="Default">
                                </asp:Series>
                            </Series>
                            <BorderSkin PageColor="AliceBlue" />
                            <Titles>
                                <asp:Title Name="Title1">
                                </asp:Title>
                            </Titles>
                            <ChartAreas>
                                <asp:ChartArea BackColor="Transparent" BorderColor="" Name="Default">
                                    <AxisY LineColor="120, 0, 0, 0" TitleAlignment="Near" Maximum="110" Minimum="-10" LineDashStyle="NotSet">
                                        <MajorGrid Enabled="False" />
                                        <MajorTickMark Enabled="False" />
                                        <LabelStyle Enabled="False" />
                                    </AxisY>
                                    <AxisX IsMarginVisible="False" TitleAlignment="Near" Maximum="100" Minimum="-100" LineDashStyle="NotSet">
                                        <MajorGrid Enabled="False" />
                                        <MajorTickMark Enabled="False" />
                                        <LabelStyle Enabled="False" />
                                    </AxisX>
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


