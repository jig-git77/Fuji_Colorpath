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
                    <th>Target</th>
                    <th>Predicted</th>
                </tr>
                <tr style="height: 75px;">
                    <td>
                        <div runat="server" id="divTargetRun0" style="height: 75px;"></div>
                    </td>
                    <td>
                        <div runat="server" id="divActualRun0" style="height: 75px;"></div>
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
                                    <asp:Label runat="server" ID="lblTargetLRun0"></asp:Label></td>
                            </tr>
                            <tr>
                                <td><b>A</b></td>
                                <td>
                                    <asp:Label runat="server" ID="lblTargetARun0"></asp:Label></td>
                            </tr>
                            <tr>
                                <td><b>B</b></td>
                                <td>
                                    <asp:Label runat="server" ID="lblTargetBRun0"></asp:Label></td>
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
                        <table runat="server">
                            <tr>
                                <th>Predicted</th>
                                <th>Delta</th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblActualLRun0"></asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="lblDeltaL"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblActualARun0"></asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="lblDeltaA"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblActualBRun0"></asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="lblDeltaB">---</asp:Label></td>
                            </tr>
                        </table>                        
                    </td>
            </tr>
            </table>
        </div>
        <div runat="server" id="divPatchDetailsRun"  visible="false" style="float: left; width: 70%;">
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
                        </table>
                    </td>
                    <td>
                        <table runat="server" id="Table1">
                            <tr>
                                <th>Predicted</th>
                                <th>Measured</th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblActualL"></asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="lblMeasuredL"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblActualA"></asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="lblMeasuredA"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblActualB"></asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="lblMeasuredB">---</asp:Label></td>
                            </tr>
                        </table>                        
                    </td>
                    <td>
                        <table>
                            <tr>
                                <th>Delta_T_P</th>
                                <th>Delta_M_T</th>
                                <th>Delta_M_P</th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblTarget_PredictedL" Text="0"></asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="lblMeasured_TargetL" Text="0"></asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="lblMeasured_PredictedL" Text="0"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblTarget_PredictedA" Text="0"></asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="lblMeasured_TargetA" Text="0"></asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="lblMeasured_PredictedA" Text="0"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblTarget_PredictedB" Text="0"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblMeasured_TargetB" Text="0"></asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="lblMeasured_PredictedB" Text="0"></asp:Label></td>
                            </tr>
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
                                <asp:Series Name="Red" ChartType="Polar" Color="Red"
                                    MarkerSize="20" MarkerBorderWidth="2" MarkerBorderColor="Red" MarkerStyle="Circle"
                                    MarkerColor="Red" ShadowOffset="1" ChartArea="Default" Legend="Default">
                                </asp:Series>
                                <asp:Series Name="Green" ChartType="Polar" Color="Green"
                                    MarkerSize="10" MarkerBorderWidth="2" MarkerBorderColor="Green" MarkerStyle="Circle"
                                    MarkerColor="Green" ShadowOffset="1" ChartArea="Default" Legend="Default">
                                </asp:Series>
                                <asp:Series Name="Purple" ChartType="Polar" Color="Green"
                                    MarkerSize="7" MarkerBorderWidth="2" MarkerBorderColor="Purple" MarkerStyle="Circle"
                                    MarkerColor="Purple" ShadowOffset="1" ChartArea="Default" Legend="Default">
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
                                <asp:Series Name="Purple" ChartType="Polar" Color="Purple"
                                    MarkerSize="7" MarkerBorderWidth="2" MarkerBorderColor="Purple" MarkerStyle="Circle"
                                    MarkerColor="Purple" ShadowOffset="1" ChartArea="Default" Legend="Default">
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


