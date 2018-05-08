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
        <div runat="server" id="divPatchDetailsRun0" visible="false" style="float: left; width: 50%;">
            <table class="PatchDetailsTable">
                <tr>
                    <th><asp:Localize runat="server" Text="Target" ID="locTargetHead" meta:resourcekey="locTargetResource"></asp:Localize></th>
                    <th><asp:Localize runat="server" Text="Predicted" ID="locPredictedHead" meta:resourcekey="locPredictedResource"></asp:Localize></th>
                </tr>
                <tr style="height: 75px;">
                    <td style="width:50%;"><div runat="server" id="divTargetRun0" style="height: 75px;"></div></td>
                    <td style="width:50%;"><div runat="server" id="divActualRun0" style="height: 75px;"></div></td>                    
                </tr>
            </table>
            <table class="PatchDetailsTable">
                <tr>
                    <th><asp:Localize runat="server" Text="Metric" ID="locMetrictbl1" meta:resourcekey="locMetricResource"></asp:Localize></th>
                    <th><asp:Localize runat="server" Text="Target" ID="locTargetTbl1" meta:resourcekey="locTargetResource"></asp:Localize></th>
                    <th><asp:Localize runat="server" Text="Predicted" ID="locPredictedTbl1" meta:resourcekey="locPredictedResource"></asp:Localize></th>
                    <th><asp:Localize runat="server" Text="Delta" ID="locDelta" meta:resourcekey="locDeltaResource"></asp:Localize></th>
                </tr>
                <tr>
                    <td><b>L</b></td>
                    <td><asp:Label runat="server" ID="lblTargetLRun0"></asp:Label></td>
                    <td><asp:Label runat="server" ID="lblActualLRun0"></asp:Label></td>
                    <td><asp:Label runat="server" ID="lblDeltaL"></asp:Label></td>
                </tr>
                <tr>
                    <td><b>A</b></td>
                    <td><asp:Label runat="server" ID="lblTargetARun0"></asp:Label></td>
                    <td><asp:Label runat="server" ID="lblActualARun0"></asp:Label></td>
                    <td><asp:Label runat="server" ID="lblDeltaA"></asp:Label></td>
                </tr>
                <tr>
                    <td><b>B</b></td>
                    <td><asp:Label runat="server" ID="lblTargetBRun0"></asp:Label></td>
                    <td><asp:Label runat="server" ID="lblActualBRun0"></asp:Label></td>
                    <td><asp:Label runat="server" ID="lblDeltaB"></asp:Label></td>
                </tr>                       
            </table>
        </div>
        <div runat="server" id="divPatchDetailsRun"  visible="false" style="float: left; width: 70%;">
            <table class="PatchDetailsTable">
                <tr>
                    <th><asp:Localize runat="server" Text="Target" ID="locTargetHead1" meta:resourcekey="locTargetResource"></asp:Localize></th>
                    <th><asp:Localize runat="server" Text="Predicted" ID="locPredictedHead1" meta:resourcekey="locPredictedResource"></asp:Localize></th>
                    <th><asp:Localize runat="server" Text="Measured" ID="locMeasuredHead" meta:resourcekey="locMeasuredResource"></asp:Localize></th>
                </tr>
                <tr style="height: 75px;">
                    <td style="width:33%;"><div runat="server" id="divTarget" style="height: 75px;"></div></td>
                    <td style="width:33%;"><div runat="server" id="divActual" style="height: 75px;"></div></td>
                    <td style="width:33%;"><div runat="server" id="divMeasured" style="height: 75px;"></div></td>
                </tr>
            </table>
            <table class="PatchDetailsTable">
                <tr>
                    <th><asp:Localize runat="server" Text="Metric" ID="locMetricHead1" meta:resourcekey="locMetricResource"></asp:Localize></th>
                    <th><asp:Localize runat="server" Text="Target" ID="locTarget" meta:resourcekey="locTargetResource"></asp:Localize></th>
                    <th><asp:Localize runat="server" Text="Predicted" ID="locPredicted" meta:resourcekey="locPredictedResource"></asp:Localize></th>
                    <th><asp:Localize runat="server" Text="Measured" ID="locMeasured" meta:resourcekey="locMeasuredResource"></asp:Localize></th>
                    <th><asp:Localize runat="server" Text="Delta_T_P" ID="locTP" meta:resourcekey="locT_PResource"></asp:Localize></th>
                    <th><asp:Localize runat="server" Text="Delta_M_T" ID="locMT" meta:resourcekey="locM_TResource"></asp:Localize></th>
                    <th><asp:Localize runat="server" Text="Delta_M_P" ID="locMP" meta:resourcekey="locM_PResource"></asp:Localize></th>
                </tr>
                <tr>
                    <td><b>L</b></td>
                    <td><asp:Label runat="server" ID="lblTargetL"></asp:Label></td>
                    <td><asp:Label runat="server" ID="lblActualL"></asp:Label></td>
                    <td><asp:Label runat="server" ID="lblMeasuredL"></asp:Label></td>
                    <td><asp:Label runat="server" ID="lblTarget_PredictedL" Text="0"></asp:Label></td>
                    <td><asp:Label runat="server" ID="lblMeasured_TargetL" Text="0"></asp:Label></td>
                    <td><asp:Label runat="server" ID="lblMeasured_PredictedL" Text="0"></asp:Label></td>
                </tr>
                <tr>
                    <td><b>A</b></td>
                    <td><asp:Label runat="server" ID="lblTargetA"></asp:Label></td>
                    <td><asp:Label runat="server" ID="lblActualA"></asp:Label></td>
                    <td><asp:Label runat="server" ID="lblMeasuredA"></asp:Label></td>
                    <td><asp:Label runat="server" ID="lblTarget_PredictedA" Text="0"></asp:Label></td>
                    <td><asp:Label runat="server" ID="lblMeasured_TargetA" Text="0"></asp:Label></td>
                    <td><asp:Label runat="server" ID="lblMeasured_PredictedA" Text="0"></asp:Label></td>
                </tr>
                <tr>
                    <td><b>B</b></td>
                    <td><asp:Label runat="server" ID="lblTargetB"></asp:Label></td>
                    <td><asp:Label runat="server" ID="lblActualB"></asp:Label></td>
                    <td><asp:Label runat="server" ID="lblMeasuredB"></asp:Label></td>
                    <td><asp:Label runat="server" ID="lblTarget_PredictedB" Text="0"></asp:Label></td>
                    <td><asp:Label runat="server" ID="lblMeasured_TargetB" Text="0"></asp:Label></td>
                    <td><asp:Label runat="server" ID="lblMeasured_PredictedB" Text="0"></asp:Label></td>
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
                                <asp:Series Name="Red" ChartType="Polar" Color="Red"
                                    MarkerSize="20" MarkerBorderWidth="2" MarkerBorderColor="Red" MarkerStyle="Circle"
                                    MarkerColor="Red" ShadowOffset="1" ChartArea="Default" Legend="Default">
                                </asp:Series>
                                <asp:Series Name="Green" ChartType="Polar" Color="Green"
                                    MarkerSize="10" MarkerBorderWidth="2" MarkerBorderColor="Green" MarkerStyle="Circle"
                                    MarkerColor="Green" ShadowOffset="1" ChartArea="Default" Legend="Default">
                                </asp:Series>
                                <asp:Series Name="Blue" ChartType="Polar" Color="Blue"
                                    MarkerSize="7" MarkerBorderWidth="2" MarkerBorderColor="Blue" MarkerStyle="Circle"
                                    MarkerColor="Blue" ShadowOffset="1" ChartArea="Default" Legend="Default">
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

                    <td align="center" style="width: 50px; padding-left: 22px;">

                        <asp:Chart ID="chartPaper2" runat="server" BackImage="~/Images/SpotL_Bg.jpg" BackImageWrapMode="Scaled"
                            ImageType="Jpeg" Width="50px" Height="180px" ImageLocation="~/UploadFiles/temp/SingleMeasurement_Page1_PaperLab2"
                            meta:resourcekey="chartPaper2Resource1">
                            <Legends>
                                <asp:Legend Name="Default" Enabled="False">
                                </asp:Legend>
                            </Legends>
                            <Series>
                                <asp:Series Name="Red" ChartType="Polar" Color="Red"
                                    MarkerSize="20" MarkerBorderWidth="2" MarkerBorderColor="Red" MarkerStyle="Circle"
                                    MarkerColor="Red" ShadowOffset="1" ChartArea="Default" Legend="Default">
                                </asp:Series>
                                <asp:Series Name="Green" ChartType="Polar" Color="Green"
                                    MarkerSize="10" MarkerBorderWidth="2" MarkerBorderColor="Green" MarkerStyle="Circle"
                                    MarkerColor="Green" ShadowOffset="1" ChartArea="Default" Legend="Default">
                                </asp:Series>
                                <asp:Series Name="Blue" ChartType="Polar" Color="Blue"
                                    MarkerSize="7" MarkerBorderWidth="2" MarkerBorderColor="Blue" MarkerStyle="Circle"
                                    MarkerColor="Blue" ShadowOffset="1" ChartArea="Default" Legend="Default">
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


