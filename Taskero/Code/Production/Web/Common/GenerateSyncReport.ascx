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
                            Gray Balance ΔF* Plot
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
