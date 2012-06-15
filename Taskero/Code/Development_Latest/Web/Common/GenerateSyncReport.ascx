<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GenerateSyncReport.ascx.cs"
    Inherits="Common_GenerateSyncReport" %>
<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="rad" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:ImageButton ID="IbtnGenerateReport" ImageUrl="~/Images/SyncReportImg.gif" runat="server"
    ToolTip="Generate PDF Report" OnClick="IbtnGenerateReport_Click" />
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <%------------------------ Report Generate Structure -------------------------%>
        <asp:Panel Style="width: 100%" ID="PNL_Result" runat="server" Visible="false">
            <div style="overflow: auto; height: 400px;">
                <table style="width: 100%" cellspacing="0" cellpadding="0" align="center" border="0">
                    <tr>
                        <td align="center" valign="top">
                            Gray Balance ΔCh Plot
                            <br />
                            <DCWC:Chart ID="Chart_GrayBalanceF" runat="server" Width="395px" Height="180px" ImageType="Jpeg"
                                ImageUrl="~/UploadFiles/temp/CPSPrintStandards_Page2_GrayBalanceF" BackGradientType="Center"
                                Palette="Kindergarten" BackGradientEndColor="White" BorderLineColor="#808080"
                                EnableViewState="false">
                                <Legends>
                                    <DCWC:Legend Name="Default" Enabled="false">
                                    </DCWC:Legend>
                                </Legends>
                                <BorderSkin SkinStyle="None"></BorderSkin>
                                <UI>
                                    <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                        <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                            FrameBackColor="SteelBlue"></BorderSkin>
                                    </Toolbar>
                                </UI>
                                <Series>
                                    <DCWC:Series Name="Series1" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                        Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                        MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                    </DCWC:Series>
                                    <DCWC:Series MarkerSize="12" BackGradientEndColor="162, 164, 255" ShowInLegend="false"
                                        Color="Red" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                        BorderWidth="2" Name="Series2">
                                    </DCWC:Series>
                                </Series>
                                <ChartAreas>
                                    <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                        BackColor="Gray" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                        <Position Height="99" Width="94" X="0" Y="2" />
                                        <AxisY LabelsAutoFit="False" Title="" TitleFont="Verdana, 8pt, style=Bold" Interval="1"
                                            Minimum="0" Maximum="5">
                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                            <LabelStyle FontAngle="0" Font="Verdana, 6pt" />
                                        </AxisY>
                                        <AxisX LabelsAutoFit="False" Margin="False" Interval="25" Title="" Minimum="0" Maximum="100">
                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                            <LabelStyle FontAngle="0" Font="Verdana, 6pt" />
                                        </AxisX>
                                    </DCWC:ChartArea>
                                </ChartAreas>
                            </DCWC:Chart>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top">
                            Black NPD
                            <br />
                            <DCWC:Chart ID="CHRT_BlackNPD" runat="server" Width="365px" Height="250px" ImageType="Jpeg"
                                ImageUrl="~/UploadFiles/temp/CPSPrintStandards_Page2_BlackNPD" BackGradientType="Center"
                                Palette="Kindergarten" BackGradientEndColor="White" BorderLineColor="#808080"
                                EnableViewState="false">
                                <Legends>
                                    <DCWC:Legend Name="Default" Enabled="false">
                                    </DCWC:Legend>
                                </Legends>
                                <BorderSkin SkinStyle="None"></BorderSkin>
                                <UI>
                                    <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                        <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                            FrameBackColor="SteelBlue"></BorderSkin>
                                    </Toolbar>
                                </UI>
                                <Series>
                                    <DCWC:Series Name="Series0" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                        Color="Green" BorderWidth="3" ChartType="Spline" MarkerBorderColor="Green" MarkerStyle="Circle"
                                        MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Series1" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                        Color="Black" BorderWidth="3" ChartType="Spline" MarkerBorderColor="Black" MarkerStyle="Circle"
                                        MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                    </DCWC:Series>
                                </Series>
                                <ChartAreas>
                                    <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                        BackColor="Gray" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                        <Position Height="99" Width="94" X="0" Y="2" />
                                        <AxisY LabelsAutoFit="False" Title="" TitleFont="Verdana, 8pt, style=Bold" Interval="Auto">
                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                        </AxisY>
                                        <AxisX LabelsAutoFit="False" Margin="False" Interval="25" Title="">
                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                            <LabelStyle FontAngle="0" />
                                        </AxisX>
                                    </DCWC:ChartArea>
                                </ChartAreas>
                            </DCWC:Chart>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top">
                            Gray NPD
                            <br />
                            <DCWC:Chart ID="CHRT_GrayNPD" runat="server" Width="365px" Height="250px" ImageType="Jpeg"
                                ImageUrl="~/UploadFiles/temp/CPSPrintStandards_Page2_GrayNPD" BackGradientType="Center"
                                Palette="Kindergarten" BackGradientEndColor="White" BorderLineColor="#808080"
                                EnableViewState="false">
                                <Legends>
                                    <DCWC:Legend Name="Default" Enabled="false">
                                    </DCWC:Legend>
                                </Legends>
                                <BorderSkin SkinStyle="None"></BorderSkin>
                                <UI>
                                    <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                        <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                            FrameBackColor="SteelBlue"></BorderSkin>
                                    </Toolbar>
                                </UI>
                                <Series>
                                    <DCWC:Series Name="Series0" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                        Color="Green" BorderWidth="3" ChartType="Spline" MarkerBorderColor="Green" MarkerStyle="Circle"
                                        MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Series1" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                        Color="Black" BorderWidth="3" ChartType="Spline" MarkerBorderColor="Black" MarkerStyle="Circle"
                                        MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                    </DCWC:Series>
                                </Series>
                                <ChartAreas>
                                    <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                        BackColor="Gray" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                        <Position Height="99" Width="94" X="0" Y="2" />
                                        <AxisY LabelsAutoFit="False" Title="" TitleFont="Verdana, 8pt, style=Bold" Interval="Auto">
                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                        </AxisY>
                                        <AxisX LabelsAutoFit="False" Margin="False" Interval="25" Title="">
                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                            <LabelStyle FontAngle="0" />
                                        </AxisX>
                                    </DCWC:ChartArea>
                                </ChartAreas>
                            </DCWC:Chart>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top">
                            ΔE Values
                            <br />
                            <DCWC:Chart ID="ChartSummary_DeltaE" runat="server" Width="170px" Height="275px"
                                ImageUrl="~/UploadFiles/temp/CPSPrintStandards_Page2_DeltaEValues" ImageType="Jpeg"
                                EnableViewState="false">
                                <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2"
                                    SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                                <UI>
                                    <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                        <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                            FrameBackColor="SteelBlue"></BorderSkin>
                                    </Toolbar>
                                </UI>
                                <Series>
                                    <DCWC:Series Name="Series0" BackGradientType="DiagonalRight" BorderColor="26, 59, 105"
                                        BorderWidth="0" Color="#D4E8FA" ShadowOffset="70" Font="Microsoft Sans Serif, 7pt, style=Underline"
                                        Label="" CustomAttributes="LabelStyle=Top,DrawingStyle=Cylinder" ShowInLegend="true">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Series1" BorderWidth="0" BorderColor="Gray" Color="Gray" CustomAttributes="DrawingStyle=Cylinder"
                                        ShowInLegend="false">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Series2" BorderWidth="0" Color="Transparent" CustomAttributes="DrawingStyle=Cylinder"
                                        ShowInLegend="false" MarkerImage="~/Images/Chart-Red-Image-Vertical.png">
                                    </DCWC:Series>
                                </Series>
                                <ChartAreas>
                                    <DCWC:ChartArea Name="ChartArea1">
                                        <Area3DStyle YAngle="0" XAngle="0" Enable3D="true" RightAngleAxes="False" WallWidth="0"
                                            Clustered="True"></Area3DStyle>
                                        <AxisX Interval="Auto" Enabled="true" LabelsAutoFit="False" Reverse="true">
                                            <MajorGrid Enabled="false" LineColor="Beige"></MajorGrid>
                                            <MajorTickMark Enabled="false" />
                                            <LabelStyle Font="Verdana, 7pt" />
                                        </AxisX>
                                        <AxisY Enabled="True" LabelsAutoFit="False">
                                            <MajorGrid LineColor="Beige"></MajorGrid>
                                            <LabelStyle Format="###" Font="Verdana, 7pt" FontColor="Black" />
                                        </AxisY>
                                    </DCWC:ChartArea>
                                </ChartAreas>
                            </DCWC:Chart>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top">
                            A*B Comparison
                            <br />
                            <DCWC:Chart ID="CHART_ABComparison" runat="server" BackImageMode="Scaled" BackImage="~/Images/CPSyncABComparison.jpg"
                                ImageType="Jpeg" ImageUrl="~/UploadFiles/temp/CPSPrintStandards_Page2_ABComparison"
                                Width="230px" Height="280px" EnableViewState="false">
                                <Legends>
                                    <DCWC:Legend Name="Default" Enabled="False">
                                    </DCWC:Legend>
                                </Legends>
                                <Series>
                                    <DCWC:Series Name="Targets" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                        MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Cyan" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                        MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Magenta" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                        MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Yellow" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                        MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Red" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                        MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Green" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                        MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Blue" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                        MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Paper" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                        MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1">
                                    </DCWC:Series>
                                </Series>
                                <BorderSkin PageColor="AliceBlue" />
                                <Titles>
                                    <DCWC:Title Name="Title1">
                                    </DCWC:Title>
                                </Titles>
                                <ChartAreas>
                                    <DCWC:ChartArea BackColor="Transparent" BorderColor="" Name="Default">
                                        <AxisY LineColor="120, 0, 0, 0" TitleAlignment="Near" Maximum="100" Minimum="-100"
                                            LineStyle="NotSet">
                                            <MajorGrid Enabled="False" />
                                            <MajorTickMark Enabled="false" />
                                            <LabelStyle Enabled="False" />
                                        </AxisY>
                                        <AxisX Margin="False" TitleAlignment="Near" Maximum="100" Minimum="-100" LineStyle="NotSet">
                                            <MajorGrid Enabled="False" />
                                            <MajorTickMark Enabled="false" />
                                            <LabelStyle Enabled="False" />
                                        </AxisX>
                                    </DCWC:ChartArea>
                                </ChartAreas>
                            </DCWC:Chart>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top">
                            Gray Balance L* a* b*
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <DCWC:Chart ID="Chart_L" runat="server" Width="655px" Height="100px" ImageType="Jpeg"
                                            ImageUrl="~/UploadFiles/temp/CPSPrintStandards_Page2_ChartL" BackGradientType="Center"
                                            Palette="Kindergarten" BackGradientEndColor="White" BorderLineColor="#808080"
                                            EnableViewState="false">
                                            <Titles>
                                                <DCWC:Title Color="Black" Text="L*">
                                                    <Position X="90" Y="10" />
                                                </DCWC:Title>
                                            </Titles>
                                            <Legends>
                                                <DCWC:Legend Name="Default" Enabled="false">
                                                </DCWC:Legend>
                                            </Legends>
                                            <BorderSkin SkinStyle="None"></BorderSkin>
                                            <UI>
                                                <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                                    <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                                        FrameBackColor="SteelBlue"></BorderSkin>
                                                </Toolbar>
                                            </UI>
                                            <Series>
                                                <DCWC:Series Name="Series1" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                    MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                                </DCWC:Series>
                                                <DCWC:Series MarkerSize="12" BackGradientEndColor="162, 164, 255" ShowInLegend="false"
                                                    Color="Green" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                                    BorderWidth="2" Name="Series2">
                                                </DCWC:Series>
                                            </Series>
                                            <ChartAreas>
                                                <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="Gray"
                                                    BackColor="White" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                                    <Position Height="99" Width="94" X="0" Y="0" />
                                                    <AxisY LabelsAutoFit="False" Title="" TitleFont="Verdana, 8pt, style=Bold" Interval="25"
                                                        Minimum="0" Maximum="100">
                                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                                        <LabelStyle FontAngle="0" Font="Verdana, 6pt" />
                                                    </AxisY>
                                                    <AxisX LabelsAutoFit="False" Margin="False" Interval="25" Title="" Minimum="0" Maximum="100">
                                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                                        <LabelStyle FontAngle="0" Font="Verdana, 6pt" FontColor="Transparent" />
                                                    </AxisX>
                                                </DCWC:ChartArea>
                                            </ChartAreas>
                                        </DCWC:Chart>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <DCWC:Chart ID="Chart_a" runat="server" Width="655px" Height="100px" ImageType="Jpeg"
                                            ImageUrl="~/UploadFiles/temp/CPSPrintStandards_Page2_ChartA" BackGradientType="Center"
                                            Palette="Kindergarten" BackGradientEndColor="White" BorderLineColor="#808080"
                                            EnableViewState="false">
                                            <Legends>
                                                <DCWC:Legend Name="Default" Enabled="false">
                                                </DCWC:Legend>
                                            </Legends>
                                            <Titles>
                                                <DCWC:Title Color="Black" Text="a*">
                                                    <Position X="90" Y="10" />
                                                </DCWC:Title>
                                            </Titles>
                                            <BorderSkin SkinStyle="None"></BorderSkin>
                                            <UI>
                                                <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                                    <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                                        FrameBackColor="SteelBlue"></BorderSkin>
                                                </Toolbar>
                                            </UI>
                                            <Series>
                                                <DCWC:Series Name="Series1" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                    MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                                </DCWC:Series>
                                                <DCWC:Series MarkerSize="12" BackGradientEndColor="162, 164, 255" ShowInLegend="false"
                                                    Color="Green" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                                    BorderWidth="2" Name="Series2">
                                                </DCWC:Series>
                                            </Series>
                                            <ChartAreas>
                                                <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="#339966"
                                                    BackColor="#ff66cc" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                                    <Position Height="99" Width="94" X="0" Y="0" />
                                                    <AxisY LabelsAutoFit="False" Title="" TitleFont="Verdana, 8pt, style=Bold" Interval="2"
                                                        Minimum="-6" Maximum="6">
                                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                                        <LabelStyle FontAngle="0" Font="Verdana, 6pt" />
                                                    </AxisY>
                                                    <AxisX LabelsAutoFit="False" Margin="False" Title="" Interval="25" Minimum="0" Maximum="100">
                                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                                        <LabelStyle FontAngle="0" Font="Verdana, 6pt" FontColor="Transparent" />
                                                    </AxisX>
                                                </DCWC:ChartArea>
                                            </ChartAreas>
                                        </DCWC:Chart>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <DCWC:Chart ID="Chart_b" runat="server" Width="655px" Height="100px" ImageType="Jpeg"
                                            ImageUrl="~/UploadFiles/temp/CPSPrintStandards_Page2_ChartB" BackGradientType="Center"
                                            Palette="Kindergarten" BackGradientEndColor="White" BorderLineColor="#808080"
                                            EnableViewState="false">
                                            <Legends>
                                                <DCWC:Legend Name="Default" Enabled="false">
                                                </DCWC:Legend>
                                            </Legends>
                                            <Titles>
                                                <DCWC:Title Color="Black" Text="b*">
                                                    <Position X="90" Y="10" />
                                                </DCWC:Title>
                                            </Titles>
                                            <BorderSkin SkinStyle="None"></BorderSkin>
                                            <UI>
                                                <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                                    <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                                        FrameBackColor="SteelBlue"></BorderSkin>
                                                </Toolbar>
                                            </UI>
                                            <Series>
                                                <DCWC:Series Name="Series1" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                    MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                                </DCWC:Series>
                                                <DCWC:Series MarkerSize="12" BackGradientEndColor="162, 164, 255" ShowInLegend="false"
                                                    Color="Green" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                                    BorderWidth="2" Name="Series2">
                                                </DCWC:Series>
                                            </Series>
                                            <ChartAreas>
                                                <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="#3333cc"
                                                    BackColor="#ffff99" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                                    <Position Height="99" Width="94" X="0" Y="0" />
                                                    <AxisY LabelsAutoFit="False" Title="" TitleFont="Verdana, 8pt, style=Bold" Interval="2"
                                                        Minimum="-6" Maximum="6">
                                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                                        <LabelStyle FontAngle="0" Font="Verdana, 6pt" />
                                                    </AxisY>
                                                    <AxisX LabelsAutoFit="False" Margin="False" Interval="25" Minimum="0" Maximum="100"
                                                        Title="">
                                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                                        <LabelStyle FontAngle="0" Font="Verdana, 6pt" />
                                                    </AxisX>
                                                </DCWC:ChartArea>
                                            </ChartAreas>
                                        </DCWC:Chart>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Density<br />
                            <DCWC:Chart ID="CHRT_SummaryDensity" ImageType="Jpeg" runat="server" Width="650px"
                                Height="200px" ImageUrl="~/UploadFiles/temp/CPSPrintStandards_Page2_Density"
                                BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#FFFFFF"
                                BorderLineColor="#808080" EnableViewState="false">
                                <Series>
                                    <DCWC:Series Name="Series0" Color="Transparent" ChartArea="ChartArea1" BorderWidth="1"
                                        ShowInLegend="false">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Series1" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="1"
                                        CustomAttributes="DrawingStyle=Cylinder" ShowInLegend="false">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Series2" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="2"
                                        ShowInLegend="false">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Series3" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="1"
                                        ShowInLegend="false">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Series4" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="1"
                                        ShowInLegend="false">
                                    </DCWC:Series>
                                </Series>
                                <BorderSkin SkinStyle="None"></BorderSkin>
                                <UI>
                                    <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                        <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                            FrameBackColor="SteelBlue"></BorderSkin>
                                    </Toolbar>
                                </UI>
                                <ChartAreas>
                                    <DCWC:ChartArea Name="ChartArea1" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                        BackColor="Gray" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                        <Area3DStyle YAngle="10" XAngle="10" RightAngleAxes="False" WallWidth="0" Clustered="true">
                                        </Area3DStyle>
                                        <Position Height="99" Width="94" X="0" Y="2" />
                                        <AxisY Enabled="True">
                                            <MajorGrid LineColor="Beige" />
                                        </AxisY>
                                        <AxisX Enabled="True" Interval="Auto">
                                            <MajorGrid LineColor="Beige" />
                                        </AxisX>
                                        <AxisX2 Enabled="True" Interval="Auto" Minimum="0" Maximum="1">
                                            <LabelStyle ShowEndLabels="false" />
                                            <MajorGrid LineColor="Beige" />
                                        </AxisX2>
                                    </DCWC:ChartArea>
                                </ChartAreas>
                            </DCWC:Chart>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            DotGain<br />
                            <DCWC:Chart ID="CHRT_SummaryDotgain" runat="server" Width="650px" Height="300px"
                                ImageType="Jpeg" ImageUrl="~/UploadFiles/temp/CPSPrintStandards_Page2_DotGainChart"
                                BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="White"
                                BorderLineColor="#808080" EnableViewState="false">
                                <Series>
                                    <DCWC:Series Name="Series1" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                        Color="Cyan" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                        MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Series2" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                        Color="Magenta" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                        MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Series3" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                        Color="Yellow" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                        MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Series4" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                        Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                        MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                    </DCWC:Series>
                                </Series>
                                <BorderSkin SkinStyle="None"></BorderSkin>
                                <UI>
                                    <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                        <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                            FrameBackColor="SteelBlue"></BorderSkin>
                                    </Toolbar>
                                </UI>
                                <ChartAreas>
                                    <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                        BackColor="Gray" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                        <Position Height="99" Width="94" X="0" Y="2" />
                                        <AxisY LabelsAutoFit="False" Title="" Interval="Auto">
                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                        </AxisY>
                                        <AxisX LabelsAutoFit="False" Margin="False" Interval="25" Title="">
                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                            <LabelStyle FontAngle="0" />
                                        </AxisX>
                                    </DCWC:ChartArea>
                                </ChartAreas>
                            </DCWC:Chart>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <%------------------------ ISO Report Generate Structure -------------------------%>
        <asp:Panel Style="width: 100%" ID="PNL_ResultISO" runat="server" Visible="false">
            <div style="overflow: auto; height: 300px;">
                <table style="width: 100%" cellspacing="0" cellpadding="0" align="center" border="0">
                    <tr>
                        <td align="center" valign="top">
                            a* - b* Comparison<br />
                            <DCWC:Chart ID="CHART_ABISOComparison" runat="server" BackImageMode="Scaled" BackImage="~/Images/CPSyncABComparison.jpg"
                                ImageType="Jpeg" ImageUrl="~/UploadFiles/temp/CPSPrintStandardsISO_Page2_ABComparison"
                                Width="220px" Height="180px" EnableViewState="false">
                                <Legends>
                                    <DCWC:Legend Name="Default" Enabled="False">
                                    </DCWC:Legend>
                                </Legends>
                                <Series>
                                    <DCWC:Series Name="Targets" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                        MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Cyan" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                        MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Magenta" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                        MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Yellow" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                        MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Red" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                        MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Green" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                        MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Blue" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                        MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Paper" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                        MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1">
                                    </DCWC:Series>
                                </Series>
                                <BorderSkin PageColor="AliceBlue" />
                                <Titles>
                                    <DCWC:Title Name="Title1">
                                    </DCWC:Title>
                                </Titles>
                                <ChartAreas>
                                    <DCWC:ChartArea BackColor="Transparent" BorderColor="" Name="Default">
                                        <AxisY LineColor="120, 0, 0, 0" TitleAlignment="Near" Maximum="100" Minimum="-100"
                                            LineStyle="NotSet">
                                            <MajorGrid Enabled="False" />
                                            <MajorTickMark Enabled="false" />
                                            <LabelStyle Enabled="False" />
                                        </AxisY>
                                        <AxisX Margin="False" TitleAlignment="Near" Maximum="100" Minimum="-100" LineStyle="NotSet">
                                            <MajorGrid Enabled="False" />
                                            <MajorTickMark Enabled="false" />
                                            <LabelStyle Enabled="False" />
                                        </AxisX>
                                    </DCWC:ChartArea>
                                </ChartAreas>
                            </DCWC:Chart>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top">
                            L* Comparison<br />
                            <DCWC:Chart ID="CHART_LISOComparison" runat="server" BackImageMode="Scaled" BackImage="~/Images/CPSyncLComparison.jpg"
                                ImageType="Jpeg" ImageUrl="~/UploadFiles/temp/CPSPrintStandardsISO_Page2_LComparison"
                                Width="220px" Height="55px" EnableViewState="false">
                                <Legends>
                                    <DCWC:Legend Name="Default" Enabled="False">
                                    </DCWC:Legend>
                                </Legends>
                                <Series>
                                    <DCWC:Series Name="Series0" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                        MarkerSize="12" MarkerStyle="Circle" MarkerColor="Green" Color="Green" ShadowOffset="1">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Series1" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                        MarkerSize="12" MarkerStyle="Circle" MarkerColor="Green" MarkerBorderWidth="1"
                                        Color="Green" ShadowOffset="1">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Series2" ChartType="FastPoint" CustomAttributes="PolarDrawingStyle=Marker"
                                        MarkerSize="12" MarkerStyle="Circle" MarkerColor="Green" Color="Red" ShadowOffset="1">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Series3" ChartType="FastPoint" CustomAttributes="PolarDrawingStyle=Marker"
                                        MarkerSize="12" MarkerStyle="Circle" ShadowOffset="1">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Series4" ChartType="FastPoint" CustomAttributes="PolarDrawingStyle=Marker"
                                        MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1">
                                    </DCWC:Series>
                                </Series>
                                <BorderSkin PageColor="AliceBlue" />
                                <Titles>
                                    <DCWC:Title Name="Title1">
                                    </DCWC:Title>
                                </Titles>
                                <ChartAreas>
                                    <DCWC:ChartArea BackColor="Transparent" BorderColor="" Name="Default">
                                        <AxisY LineColor="120, 0, 0, 0" TitleAlignment="Near" Maximum="15" Minimum="-15"
                                            LineStyle="NotSet">
                                            <MajorGrid Enabled="False" />
                                            <MajorTickMark Enabled="false" />
                                            <LabelStyle Enabled="False" />
                                        </AxisY>
                                        <AxisX Margin="False" TitleAlignment="Near" Maximum="100" Minimum="-100" LineStyle="NotSet">
                                            <MajorGrid Enabled="False" />
                                            <MajorTickMark Enabled="false" />
                                            <LabelStyle Enabled="False" />
                                        </AxisX>
                                    </DCWC:ChartArea>
                                </ChartAreas>
                            </DCWC:Chart>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top">
                            ΔE Primary Patches
                            <br />
                            <DCWC:Chart ID="ChartISOSummary_DeltaE" runat="server" Width="300px" Height="220px"
                                ImageUrl="~/UploadFiles/temp/CPSPrintStandardsISO_Page2_DeltaE" ImageType="Jpeg"
                                EnableViewState="false">
                                <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2"
                                    SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                                <UI>
                                    <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                        <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                            FrameBackColor="SteelBlue"></BorderSkin>
                                    </Toolbar>
                                </UI>
                                <Series>
                                    <DCWC:Series Name="Series0" BackGradientType="DiagonalRight" BorderColor="26, 59, 105"
                                        BorderWidth="0" Color="#D4E8FA" ShadowOffset="70" Font="Microsoft Sans Serif, 7pt, style=Underline"
                                        Label="" CustomAttributes="LabelStyle=Top,DrawingStyle=Cylinder" ShowInLegend="true">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Series1" BorderWidth="0" BorderColor="Gray" Color="Gray" CustomAttributes="DrawingStyle=Cylinder"
                                        ShowInLegend="false">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Series2" BorderWidth="0" Color="Transparent" CustomAttributes="DrawingStyle=Cylinder"
                                        ShowInLegend="false" MarkerImage="~/Images/Chart-Red-Image-Vertical1.png">
                                    </DCWC:Series>
                                </Series>
                                <ChartAreas>
                                    <DCWC:ChartArea Name="ChartArea1">
                                        <Area3DStyle YAngle="0" XAngle="0" Enable3D="true" RightAngleAxes="False" WallWidth="0"
                                            Clustered="True"></Area3DStyle>
                                        <AxisX Interval="Auto" Enabled="true" LabelsAutoFit="False" Reverse="true">
                                            <MajorGrid Enabled="false" LineColor="Beige"></MajorGrid>
                                            <MajorTickMark Enabled="false" />
                                            <LabelStyle Font="Verdana, 7pt" />
                                        </AxisX>
                                        <AxisY Enabled="True" LabelsAutoFit="False">
                                            <MajorGrid LineColor="Beige"></MajorGrid>
                                            <LabelStyle Format="###" Font="Verdana, 7pt" FontColor="Black" />
                                        </AxisY>
                                    </DCWC:ChartArea>
                                </ChartAreas>
                            </DCWC:Chart>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top">
                            ΔPaper L* a* b*
                            <br />
                            <DCWC:Chart ID="CHRT_ISOPaper1" runat="server" BackImageMode="Scaled" BackImage="~/Images/ISOPaperGraph1.jpg"
                                ImageType="Jpeg" ImageUrl="~/UploadFiles/temp/CPSPrintStandardsISO_Page2_ISOPaper1"
                                Width="240px" Height="120px" EnableViewState="false">
                                <Legends>
                                    <DCWC:Legend Name="Default" Enabled="False">
                                    </DCWC:Legend>
                                </Legends>
                                <Series>
                                    <DCWC:Series Name="Green" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                        MarkerSize="8" MarkerBorderWidth="2" MarkerBorderColor="Green" MarkerStyle="Circle"
                                        MarkerColor="Green" ShadowOffset="1">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Paper" ChartType="Polar" MarkerBorderColor="Black" MarkerColor="White"
                                        CustomAttributes="PolarDrawingStyle=Marker" MarkerSize="8" MarkerBorderWidth="2"
                                        MarkerStyle="Circle" ShadowOffset="1">
                                    </DCWC:Series>
                                    <DCWC:Series YValuesPerPoint="2" XValueType="Double" Name="SeriesTop" ChartType="SplineRange"
                                        Color="80, 65, 140, 140" YValueType="Double">
                                    </DCWC:Series>
                                    <DCWC:Series Name="SeriesRed1" Color="Red" ChartType="Line" BorderWidth="1" ShowInLegend="false">
                                    </DCWC:Series>
                                    <DCWC:Series Name="SeriesRed2" Color="Red" ChartType="Line" BorderWidth="1" ShowInLegend="false">
                                    </DCWC:Series>
                                    <DCWC:Series Name="SeriesRed3" Color="Red" ChartType="Line" BorderWidth="1" ShowInLegend="false">
                                    </DCWC:Series>
                                    <DCWC:Series Name="SeriesRed4" Color="Red" ChartType="Line" BorderWidth="1" ShowInLegend="false">
                                    </DCWC:Series>
                                </Series>
                                <BorderSkin PageColor="AliceBlue" />
                                <Titles>
                                    <DCWC:Title Name="Title1">
                                    </DCWC:Title>
                                </Titles>
                                <ChartAreas>
                                    <DCWC:ChartArea BackColor="Transparent" BorderColor="" Name="Default">
                                        <AxisY LineColor="120, 0, 0, 0" TitleAlignment="Near" Maximum="10" Minimum="-10"
                                            LineStyle="NotSet">
                                            <MajorGrid Enabled="False" />
                                            <MajorTickMark Enabled="false" />
                                            <LabelStyle Enabled="False" />
                                        </AxisY>
                                        <AxisX Margin="False" TitleAlignment="Near" Maximum="20" Minimum="-20" LineStyle="NotSet">
                                            <MajorGrid Enabled="False" />
                                            <MajorTickMark Enabled="false" />
                                            <LabelStyle Enabled="False" />
                                        </AxisX>
                                    </DCWC:ChartArea>
                                </ChartAreas>
                            </DCWC:Chart>
                            <br />
                            <br />
                            <DCWC:Chart ID="CHRT_ISOPaper2" runat="server" BackImageMode="Scaled" BackImage="~/Images/ISOPaperGraph2.jpg"
                                ImageType="Jpeg" ImageUrl="~/UploadFiles/temp/CPSPrintStandardsISO_Page2_ISOPaper2"
                                Width="69px" Height="118px" EnableViewState="false">
                                <Legends>
                                    <DCWC:Legend Name="Default" Enabled="False">
                                    </DCWC:Legend>
                                </Legends>
                                <Series>
                                    <DCWC:Series Name="Series1" BorderColor="100, 0 , 0 , 0" ChartArea="Default" BorderWidth="1"
                                        CustomAttributes="DrawingStyle=Cylinder" ShowInLegend="false">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Series2" Color="Green" ChartArea="Default" BorderWidth="2" ShowInLegend="false">
                                    </DCWC:Series>
                                    <DCWC:Series YValuesPerPoint="2" XValueType="Double" Name="SeriesTop" ChartType="SplineRange"
                                        BorderColor="Red" Color="80, 65, 140, 140" YValueType="Double">
                                    </DCWC:Series>
                                </Series>
                                <BorderSkin PageColor="AliceBlue" />
                                <Titles>
                                    <DCWC:Title Name="Title1">
                                    </DCWC:Title>
                                </Titles>
                                <ChartAreas>
                                    <DCWC:ChartArea BackColor="Transparent" BorderColor="" Name="Default">
                                        <AxisY LineColor="120, 0, 0, 0" TitleAlignment="Near" Maximum="10" Minimum="-10"
                                            LineStyle="NotSet">
                                            <MajorGrid Enabled="False" />
                                            <MajorTickMark Enabled="false" />
                                            <LabelStyle Enabled="False" />
                                        </AxisY>
                                        <AxisX Margin="true" TitleAlignment="Near" LineStyle="NotSet">
                                            <MajorGrid Enabled="False" />
                                            <MajorTickMark Enabled="false" />
                                            <LabelStyle Enabled="False" />
                                        </AxisX>
                                        <AxisX2 Interval="Auto" Minimum="0" Maximum="1" LineStyle="NotSet">
                                            <MajorGrid Enabled="False" />
                                            <MajorTickMark Enabled="false" />
                                            <LabelStyle Enabled="False" />
                                        </AxisX2>
                                    </DCWC:ChartArea>
                                </ChartAreas>
                            </DCWC:Chart>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top">
                            Density
                            <br />
                            <DCWC:Chart ID="CHRT_SummaryISODensity" ImageType="Jpeg" runat="server" Width="276px"
                                Height="142px" ImageUrl="~/UploadFiles/temp/CPSPrintStandardsISO_Page2_ISODensity"
                                BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#FFFFFF"
                                BorderLineColor="#808080" EnableViewState="false">
                                <Series>
                                    <DCWC:Series Name="Series0" BackGradientType="DiagonalRight" BorderColor="26, 59, 105"
                                        BorderWidth="0" Color="Transparent" ShadowOffset="70" Font="Microsoft Sans Serif, 7pt, style=Underline"
                                        Label="" CustomAttributes="LabelStyle=Top,DrawingStyle=Cylinder" ShowInLegend="true">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Series1" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="1"
                                        CustomAttributes="DrawingStyle=Cylinder" ShowInLegend="false">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Series2" BorderWidth="0" Color="Transparent" CustomAttributes="DrawingStyle=Cylinder"
                                        ShowInLegend="false" MarkerImage="~/Images/green1.png">
                                    </DCWC:Series>
                                </Series>
                                <BorderSkin SkinStyle="None"></BorderSkin>
                                <UI>
                                    <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                        <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                            FrameBackColor="SteelBlue"></BorderSkin>
                                    </Toolbar>
                                </UI>
                                <ChartAreas>
                                    <DCWC:ChartArea Name="ChartArea1" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                        BackColor="Gray" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                        <Area3DStyle YAngle="10" Light="Realistic" XAngle="10" Enable3D="true" RightAngleAxes="False"
                                            WallWidth="0" Clustered="true"></Area3DStyle>
                                        <%--Enable3D="true"--%>
                                        <Position Height="99" Width="94" X="0" Y="2" />
                                        <AxisY Enabled="True">
                                            <MajorGrid LineColor="Beige" />
                                        </AxisY>
                                        <AxisX Enabled="True" Interval="Auto">
                                            <MajorGrid LineColor="Beige" />
                                        </AxisX>
                                    </DCWC:ChartArea>
                                </ChartAreas>
                            </DCWC:Chart>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top">
                            TVI Spread
                            <br />
                            <DCWC:Chart ID="CHRT_SummaryISODotgain" runat="server" Width="650px" Height="170px"
                                ImageType="Jpeg" ImageUrl="~/UploadFiles/temp/CPSPrintStandardsISO_Page3_ISOTVISpread"
                                BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="White"
                                BorderLineColor="#808080" EnableViewState="false">
                                <Series>
                                    <DCWC:Series Name="Series1" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                        Color="Cyan" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                        MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Series1Point" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                        Color="Cyan" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                        MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Series2" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                        Color="Magenta" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                        MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Series2Point" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                        Color="Magenta" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                        MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Series3" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                        Color="Yellow" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                        MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Series3Point" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                        Color="Yellow" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                        MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                    </DCWC:Series>
                                    <DCWC:Series YValuesPerPoint="2" ShowInLegend="false" XValueType="String" Name="SeriesTop"
                                        ChartType="SplineRange" BorderColor="220, 65, 140, 140" Color="80, 65, 140, 140"
                                        YValueType="String">
                                    </DCWC:Series>
                                </Series>
                                <BorderSkin SkinStyle="None"></BorderSkin>
                                <UI>
                                    <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                        <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                            FrameBackColor="SteelBlue"></BorderSkin>
                                    </Toolbar>
                                </UI>
                                <ChartAreas>
                                    <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                        BackColor="Gray" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                        <Position Height="99" Width="94" X="0" Y="2" />
                                        <AxisY LabelsAutoFit="False" Title="" Interval="Auto">
                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                        </AxisY>
                                        <AxisX LabelsAutoFit="False" Margin="False" Interval="25" Title="">
                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                        </AxisX>
                                    </DCWC:ChartArea>
                                </ChartAreas>
                            </DCWC:Chart>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top">
                            Cyan TVI<br />
                            <DCWC:Chart ID="CHRT_DotgainISOCyan" runat="server" Width="318px" Height="165px"
                                ImageType="Jpeg" ImageUrl="~/UploadFiles/temp/CPSPrintStandardsISO_Page3_ISOCyanTVI"
                                BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="White"
                                BorderLineColor="#808080">
                                <Legends>
                                    <DCWC:Legend Name="Default" Enabled="false">
                                    </DCWC:Legend>
                                </Legends>
                                <BorderSkin SkinStyle="None"></BorderSkin>
                                <UI>
                                    <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                        <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                            FrameBackColor="SteelBlue"></BorderSkin>
                                    </Toolbar>
                                </UI>
                                <Series>
                                    <DCWC:Series Name="Series0" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                        Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="Black" MarkerStyle="Circle"
                                        MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                    </DCWC:Series>
                                    <DCWC:Series YValuesPerPoint="2" XValueType="Double" Name="SeriesTop" ChartType="SplineRange"
                                        BorderColor="220, 65, 140, 140" Color="80, 65, 140, 140" YValueType="Double">
                                    </DCWC:Series>
                                    <DCWC:Series MarkerSize="12" BackGradientEndColor="162, 164, 255" ShowInLegend="false"
                                        Color="Green" ChartType="Spline" MarkerBorderColor="Green" MarkerStyle="Circle"
                                        BorderWidth="2" Name="Series1">
                                    </DCWC:Series>
                                    <DCWC:Series MarkerSize="5" BackGradientEndColor="162, 164, 255" ShowInLegend="false"
                                        Color="Gray" ChartType="Point" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                        Name="Series2">
                                    </DCWC:Series>
                                </Series>
                                <ChartAreas>
                                    <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                        BackColor="Gray" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                        <Position Height="99" Width="94" X="0" Y="2" />
                                        <AxisY LabelsAutoFit="False" Title="" TitleFont="Verdana, 8pt, style=Bold" Interval="Auto">
                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                        </AxisY>
                                        <AxisX LabelsAutoFit="False" Margin="False" Interval="25" Title="">
                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                            <LabelStyle FontAngle="0" />
                                        </AxisX>
                                    </DCWC:ChartArea>
                                </ChartAreas>
                            </DCWC:Chart>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top">
                            Magenta TVI<br />
                            <DCWC:Chart ID="CHRT_DotgainISOMeganta" runat="server" Width="318px" Height="165px"
                                ImageType="Jpeg" ImageUrl="~/UploadFiles/temp/CPSPrintStandardsISO_Page3_ISOMagentaTVI"
                                BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="White"
                                BorderLineColor="#808080">
                                <Legends>
                                    <DCWC:Legend Name="Default" Enabled="false">
                                    </DCWC:Legend>
                                </Legends>
                                <BorderSkin SkinStyle="None"></BorderSkin>
                                <UI>
                                    <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                        <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                            FrameBackColor="SteelBlue"></BorderSkin>
                                    </Toolbar>
                                </UI>
                                <Series>
                                    <DCWC:Series Name="Series0" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                        Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="Black" MarkerStyle="Circle"
                                        MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                    </DCWC:Series>
                                    <DCWC:Series YValuesPerPoint="2" XValueType="Double" Name="SeriesTop" ChartType="SplineRange"
                                        BorderColor="220, 65, 140, 140" Color="80, 65, 140, 140" YValueType="Double">
                                    </DCWC:Series>
                                    <DCWC:Series MarkerSize="12" BackGradientEndColor="162, 164, 255" ShowInLegend="false"
                                        Color="Green" ChartType="Spline" MarkerBorderColor="Green" MarkerStyle="Circle"
                                        BorderWidth="2" Name="Series1">
                                    </DCWC:Series>
                                    <DCWC:Series MarkerSize="5" BackGradientEndColor="162, 164, 255" ShowInLegend="false"
                                        Color="Gray" ChartType="Point" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                        Name="Series2">
                                    </DCWC:Series>
                                </Series>
                                <ChartAreas>
                                    <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                        BackColor="Gray" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                        <Position Height="99" Width="94" X="0" Y="2" />
                                        <AxisY LabelsAutoFit="False" Title="" TitleFont="Verdana, 8pt, style=Bold" Interval="Auto">
                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                        </AxisY>
                                        <AxisX LabelsAutoFit="False" Margin="False" Interval="25" Title="">
                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                            <LabelStyle FontAngle="0" />
                                        </AxisX>
                                    </DCWC:ChartArea>
                                </ChartAreas>
                            </DCWC:Chart>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top">
                            Yellow TVI<br />
                            <DCWC:Chart ID="CHRT_DotgainISOYellow" runat="server" Width="318px" Height="165px"
                                ImageType="Jpeg" ImageUrl="~/UploadFiles/temp/CPSPrintStandardsISO_Page3_ISOYellowTVI"
                                BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="White"
                                BorderLineColor="#808080">
                                <Legends>
                                    <DCWC:Legend Name="Default" Enabled="false">
                                    </DCWC:Legend>
                                </Legends>
                                <BorderSkin SkinStyle="None"></BorderSkin>
                                <UI>
                                    <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                        <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                            FrameBackColor="SteelBlue"></BorderSkin>
                                    </Toolbar>
                                </UI>
                                <Series>
                                    <DCWC:Series Name="Series0" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                        Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="Black" MarkerStyle="Circle"
                                        MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                    </DCWC:Series>
                                    <DCWC:Series YValuesPerPoint="2" XValueType="Double" Name="SeriesTop" ChartType="SplineRange"
                                        BorderColor="220, 65, 140, 140" Color="80, 65, 140, 140" YValueType="Double">
                                    </DCWC:Series>
                                    <DCWC:Series MarkerSize="12" BackGradientEndColor="162, 164, 255" ShowInLegend="false"
                                        Color="Green" ChartType="Spline" MarkerBorderColor="Green" MarkerStyle="Circle"
                                        BorderWidth="2" Name="Series1">
                                    </DCWC:Series>
                                    <DCWC:Series MarkerSize="5" BackGradientEndColor="162, 164, 255" ShowInLegend="false"
                                        Color="Gray" ChartType="Point" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                        Name="Series2">
                                    </DCWC:Series>
                                </Series>
                                <ChartAreas>
                                    <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                        BackColor="Gray" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                        <Position Height="99" Width="94" X="0" Y="2" />
                                        <AxisY LabelsAutoFit="False" Title="" TitleFont="Verdana, 8pt, style=Bold" Interval="Auto">
                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                        </AxisY>
                                        <AxisX LabelsAutoFit="False" Margin="False" Interval="25" Title="">
                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                            <LabelStyle FontAngle="0" />
                                        </AxisX>
                                    </DCWC:ChartArea>
                                </ChartAreas>
                            </DCWC:Chart>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top">
                            Black TVI<br />
                            <DCWC:Chart ID="CHRT_DotgainISOBlack" runat="server" Width="318px" Height="165px"
                                ImageType="Jpeg" ImageUrl="~/UploadFiles/temp/CPSPrintStandardsISO_Page3_ISOBlackTVI"
                                BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="White"
                                BorderLineColor="#808080">
                                <Legends>
                                    <DCWC:Legend Name="Default" Enabled="false">
                                    </DCWC:Legend>
                                </Legends>
                                <BorderSkin SkinStyle="None"></BorderSkin>
                                <UI>
                                    <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                        <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                            FrameBackColor="SteelBlue"></BorderSkin>
                                    </Toolbar>
                                </UI>
                                <Series>
                                    <DCWC:Series Name="Series0" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                        Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="Black" MarkerStyle="Circle"
                                        MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                    </DCWC:Series>
                                    <DCWC:Series YValuesPerPoint="2" XValueType="Double" Name="SeriesTop" ChartType="SplineRange"
                                        BorderColor="220, 65, 140, 140" Color="80, 65, 140, 140" YValueType="Double">
                                    </DCWC:Series>
                                    <DCWC:Series MarkerSize="12" BackGradientEndColor="162, 164, 255" ShowInLegend="false"
                                        Color="Green" ChartType="Spline" MarkerBorderColor="Green" MarkerStyle="Circle"
                                        BorderWidth="2" Name="Series1">
                                    </DCWC:Series>
                                    <DCWC:Series MarkerSize="5" BackGradientEndColor="162, 164, 255" ShowInLegend="false"
                                        Color="Gray" ChartType="Point" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                        Name="Series2">
                                    </DCWC:Series>
                                </Series>
                                <ChartAreas>
                                    <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                        BackColor="Gray" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                        <Position Height="99" Width="94" X="0" Y="2" />
                                        <AxisY LabelsAutoFit="False" Title="" TitleFont="Verdana, 8pt, style=Bold" Interval="Auto">
                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                        </AxisY>
                                        <AxisX LabelsAutoFit="False" Margin="False" Interval="25" Title="">
                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                            <LabelStyle FontAngle="0" />
                                        </AxisX>
                                    </DCWC:ChartArea>
                                </ChartAreas>
                            </DCWC:Chart>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top">
                            Gray Balance ΔE
                            <br />
                            <DCWC:Chart ID="Chart_GrayBalanceEISO" ImageType="Jpeg" runat="server" Width="650px"
                                Height="130px" ImageUrl="~/UploadFiles/temp/CPSPrintStandardsISO_Page3_ISOGrayBalanceE"
                                BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#FFFFFF"
                                BorderLineColor="#808080" EnableViewState="false">
                                <Titles>
                                    <DCWC:Title Color="Black" Text="&Delta;E">
                                        <Position X="90" Y="25" />
                                    </DCWC:Title>
                                </Titles>
                                <Series>
                                    <DCWC:Series Name="Series0" BackGradientType="DiagonalRight" BorderColor="26, 59, 105"
                                        BorderWidth="0" Color="#D4E8FA" ShadowOffset="70" Font="Microsoft Sans Serif, 7pt, style=Underline"
                                        Label="" CustomAttributes="LabelStyle=Top,DrawingStyle=Cylinder" ShowInLegend="true">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Series1" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="1"
                                        CustomAttributes="DrawingStyle=Cylinder" ShowInLegend="false">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Series2" BorderWidth="0" Color="Transparent" CustomAttributes="DrawingStyle=Cylinder"
                                        ShowInLegend="false" MarkerImage="~/Images/green.png">
                                    </DCWC:Series>
                                </Series>
                                <BorderSkin SkinStyle="None"></BorderSkin>
                                <UI>
                                    <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                        <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                            FrameBackColor="SteelBlue"></BorderSkin>
                                    </Toolbar>
                                </UI>
                                <ChartAreas>
                                    <DCWC:ChartArea Name="ChartArea1" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                        BackColor="Gray" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                        <Area3DStyle YAngle="10" XAngle="10" Enable3D="true" RightAngleAxes="False" WallWidth="0"
                                            Clustered="true"></Area3DStyle>
                                        <%--Enable3D="true"--%>
                                        <Position Height="99" Width="94" X="0" Y="2" />
                                        <AxisY Enabled="True">
                                            <MajorGrid LineColor="Beige" />
                                            <LabelStyle FontAngle="0" Font="Verdana, 6pt" />
                                        </AxisY>
                                        <AxisX Enabled="True" Interval="Auto">
                                            <MajorGrid LineColor="Beige" />
                                            <LabelStyle FontAngle="0" Font="Verdana, 6pt" />
                                        </AxisX>
                                    </DCWC:ChartArea>
                                </ChartAreas>
                            </DCWC:Chart>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top">
                            Gray Balance L* a* b*
                            <br />
                            <DCWC:Chart ID="Chart_LISO" runat="server" Width="655px" Height="130px" ImageType="Jpeg"
                                ImageUrl="~/UploadFiles/temp/CPSPrintStandardsISO_Page3_ISOGrayBalanceL" BackGradientType="Center"
                                Palette="Kindergarten" BackGradientEndColor="White" BorderLineColor="#808080"
                                EnableViewState="false">
                                <Titles>
                                    <DCWC:Title Color="Black" Text="L*">
                                        <Position X="90" Y="15" />
                                    </DCWC:Title>
                                </Titles>
                                <Legends>
                                    <DCWC:Legend Name="Default" Enabled="false">
                                    </DCWC:Legend>
                                </Legends>
                                <BorderSkin SkinStyle="None"></BorderSkin>
                                <UI>
                                    <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                        <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                            FrameBackColor="SteelBlue"></BorderSkin>
                                    </Toolbar>
                                </UI>
                                <Series>
                                    <DCWC:Series Name="Series1" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                        Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                        MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                    </DCWC:Series>
                                    <DCWC:Series MarkerSize="12" BackGradientEndColor="162, 164, 255" ShowInLegend="false"
                                        Color="Green" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                        BorderWidth="2" Name="Series2">
                                    </DCWC:Series>
                                </Series>
                                <ChartAreas>
                                    <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="Gray"
                                        BackColor="White" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                        <Position Height="99" Width="94" X="0" Y="0" />
                                        <AxisY LabelsAutoFit="False" Title="" TitleFont="Verdana, 8pt, style=Bold" Interval="25"
                                            Minimum="0" Maximum="100">
                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                            <LabelStyle FontAngle="0" Font="Verdana, 6pt" />
                                        </AxisY>
                                        <AxisX LabelsAutoFit="False" Margin="False" Interval="25" Title="" Minimum="0" Maximum="100">
                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                            <LabelStyle FontAngle="0" Font="Verdana, 6pt" FontColor="Transparent" />
                                        </AxisX>
                                    </DCWC:ChartArea>
                                </ChartAreas>
                            </DCWC:Chart>
                            <br />
                            <DCWC:Chart ID="Chart_aISO" runat="server" Width="655px" Height="130px" ImageType="Jpeg"
                                ImageUrl="~/UploadFiles/temp/CPSPrintStandardsISO_Page3_ISOGrayBalanceA" BackGradientType="Center"
                                Palette="Kindergarten" BackGradientEndColor="White" BorderLineColor="#808080"
                                EnableViewState="false">
                                <Legends>
                                    <DCWC:Legend Name="Default" Enabled="false">
                                    </DCWC:Legend>
                                </Legends>
                                <Titles>
                                    <DCWC:Title Color="Black" Text="a*">
                                        <Position X="90" Y="15" />
                                    </DCWC:Title>
                                </Titles>
                                <BorderSkin SkinStyle="None"></BorderSkin>
                                <UI>
                                    <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                        <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                            FrameBackColor="SteelBlue"></BorderSkin>
                                    </Toolbar>
                                </UI>
                                <Series>
                                    <DCWC:Series Name="Series1" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                        Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                        MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                    </DCWC:Series>
                                    <DCWC:Series MarkerSize="12" BackGradientEndColor="162, 164, 255" ShowInLegend="false"
                                        Color="Green" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                        BorderWidth="2" Name="Series2">
                                    </DCWC:Series>
                                </Series>
                                <ChartAreas>
                                    <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="#339966"
                                        BackColor="#ff66cc" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                        <Position Height="99" Width="94" X="0" Y="0" />
                                        <AxisY LabelsAutoFit="False" Title="" TitleFont="Verdana, 8pt, style=Bold" Interval="2"
                                            Minimum="-6" Maximum="6">
                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                            <LabelStyle FontAngle="0" Font="Verdana, 6pt" />
                                        </AxisY>
                                        <AxisX LabelsAutoFit="False" Margin="False" Title="" Interval="25" Minimum="0" Maximum="100">
                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                            <LabelStyle FontAngle="0" Font="Verdana, 6pt" FontColor="Transparent" />
                                        </AxisX>
                                    </DCWC:ChartArea>
                                </ChartAreas>
                            </DCWC:Chart>
                            <br />
                            <DCWC:Chart ID="Chart_bISO" runat="server" Width="655px" Height="130px" ImageType="Jpeg"
                                ImageUrl="~/UploadFiles/temp/CPSPrintStandardsISO_Page3_ISOGrayBalanceB" BackGradientType="Center"
                                Palette="Kindergarten" BackGradientEndColor="White" BorderLineColor="#808080"
                                EnableViewState="false">
                                <Legends>
                                    <DCWC:Legend Name="Default" Enabled="false">
                                    </DCWC:Legend>
                                </Legends>
                                <Titles>
                                    <DCWC:Title Color="Black" Text="b*">
                                        <Position X="90" Y="15" />
                                    </DCWC:Title>
                                </Titles>
                                <BorderSkin SkinStyle="None"></BorderSkin>
                                <UI>
                                    <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                        <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                            FrameBackColor="SteelBlue"></BorderSkin>
                                    </Toolbar>
                                </UI>
                                <Series>
                                    <DCWC:Series Name="Series1" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                        Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                        MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                    </DCWC:Series>
                                    <DCWC:Series MarkerSize="12" BackGradientEndColor="162, 164, 255" ShowInLegend="false"
                                        Color="Green" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                        BorderWidth="2" Name="Series2">
                                    </DCWC:Series>
                                </Series>
                                <ChartAreas>
                                    <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="#3333cc"
                                        BackColor="#ffff99" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                        <Position Height="99" Width="94" X="0" Y="0" />
                                        <AxisY LabelsAutoFit="False" Title="" TitleFont="Verdana, 8pt, style=Bold" Interval="2"
                                            Minimum="-6" Maximum="6">
                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                            <LabelStyle FontAngle="0" Font="Verdana, 6pt" />
                                        </AxisY>
                                        <AxisX LabelsAutoFit="False" Margin="False" Interval="25" Minimum="0" Maximum="100"
                                            Title="">
                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                            <LabelStyle FontAngle="0" Font="Verdana, 6pt" />
                                        </AxisX>
                                    </DCWC:ChartArea>
                                </ChartAreas>
                            </DCWC:Chart>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
