<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportSingleMeasurement.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.ReportSingleMeasurement" %>

<asp:Image ID="i1" runat="server" ImageUrl="../Images/Sync DLP Report_preview.jpg" Visible="false" />
<br />

 <%------------------------ Report Generate Structure -------------------------%>
    <asp:Panel Style="width: 100%" ID="PNL_Result" runat="server" Visible="true">
        <div style="overflow: auto; height:2600px;">
            <table style="width: 100%" cellspacing="0" cellpadding="0" align="center" border="0">
                <tr>
                    <td valign="top">
                        <asp:TextBox ID="txt_MeasurementID" runat="server"></asp:TextBox>
                        <br />
                        Target Versus Actual AB Plot
                        <br />
                        <asp:Chart ID="CHART_TargetVersusActualAB" runat="server" BackImageMode="Scaled"
                            BackImage="~/Images/SingleMeasurement_PointBack.gif" Height="230px" ImageType="Jpeg"
                            Width="218px" ImageLocation="~/UploadFiles/temp/SingleMeasurement_Page1_TargetVersusActualAB">
                            <Legends>
                                <asp:Legend Name="Default" Enabled="False">
                                </asp:Legend>
                            </Legends>
                            <Series>
                                <asp:Series Name="Series0" ChartType="Polar" 
                                    MarkerSize="9" MarkerStyle="Circle" ShadowOffset="1">
                                </asp:Series>
                                <asp:Series Name="Series1" ChartType="Polar" 
                                    MarkerSize="9" MarkerStyle="Circle" ShadowOffset="1">
                                </asp:Series>
                                <asp:Series Name="Series2" ChartType="Polar" 
                                    MarkerSize="9" MarkerStyle="Circle" ShadowOffset="1">
                                </asp:Series>
                                <asp:Series Name="Series3" ChartType="Polar" 
                                    MarkerSize="9" MarkerStyle="Circle" ShadowOffset="1">
                                </asp:Series>
                                <asp:Series Name="Series4" ChartType="Polar" 
                                    MarkerSize="9" MarkerStyle="Circle" ShadowOffset="1">
                                </asp:Series>
                                <asp:Series Name="Series5" ChartType="Polar" 
                                    MarkerSize="9" MarkerStyle="Circle" ShadowOffset="1">
                                </asp:Series>
                                <asp:Series Name="Series6" ChartType="Polar" 
                                    MarkerSize="6" MarkerStyle="Circle" ShadowOffset="1">
                                </asp:Series>
                                <asp:Series Name="Series7" ChartType="Polar" 
                                    MarkerSize="6" MarkerStyle="Circle" ShadowOffset="1">
                                </asp:Series>
                                <asp:Series Name="Series8" ChartType="Polar" 
                                    MarkerSize="6" MarkerStyle="Circle" ShadowOffset="1">
                                </asp:Series>
                                <asp:Series Name="Series9" ChartType="Polar" 
                                    MarkerSize="6" MarkerStyle="Circle" ShadowOffset="1">
                                </asp:Series>
                                <asp:Series Name="Series10" ChartType="Polar" 
                                    MarkerSize="6" MarkerStyle="Circle" ShadowOffset="1">
                                </asp:Series>
                                <asp:Series Name="Series11" ChartType="Polar" 
                                    MarkerSize="6" MarkerStyle="Circle" ShadowOffset="1">
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
                                         LineDashStyle="NotSet" >
                                        <MajorGrid Enabled="False" />
                                        <MajorTickMark Enabled="false" />
                                        <LabelStyle Enabled="False" />
                                    </AxisY>
                                    <AxisX IsMarginVisible="False" TitleAlignment="Near" Maximum="100" Minimum="-100" LineDashStyle="NotSet">
                                        <MajorGrid Enabled="False" />
                                        <MajorTickMark Enabled="false" />
                                        <LabelStyle Enabled="False" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                </tr>
                <tr>
                    <td>
                        Summary Delta Value
                        <br />
                        <asp:Chart ID="ChartResultSummary_Delta" runat="server" Width="340px" Height="290px" BackColor="Transparent"
                            ImageLocation="~/UploadFiles/temp/SingleMeasurement_Page1_ResultSummary_Delta" ImageType="Jpeg">
                            <BorderSkin BackSecondaryColor="White" BorderColor="Black" BorderWidth="2"
                                SkinStyle="Raised" BackColor="White"></BorderSkin>
                        
                           <Series>
                                <asp:Series Name="Series0"  Color="Transparent" ChartArea="ChartArea1">                                              
                                </asp:Series>
                                <asp:Series Name="Series1" BorderWidth="0" BorderColor="Gray" Color="Gray" 
                                                            IsVisibleInLegend="False" ChartArea="ChartArea1">
                                </asp:Series>    
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1" BackColor="Transparent">
                                        <Area3DStyle IsRightAngleAxes="False" WallWidth="0"
                                                                IsClustered="True"></Area3DStyle>
                                          
                                    <AxisX  Enabled="True"  IsReversed="True" IsLabelAutoFit="False" >
                                        <MajorGrid Enabled="False" LineColor="Beige" />
                                        <MajorTickMark Enabled="False" />
                                        <LabelStyle Font="Verdana, 7pt" />
                                    </AxisX>
                                        <AxisY Enabled="True" IsLabelAutoFit="False">
                                        <MajorGrid LineColor="Beige"></MajorGrid>
                                        <LabelStyle Format="###" Font="Verdana, 7pt" />
                                    </AxisY>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                </tr>
                <tr>
                    <td>
                        Summary Delta E Value
                        <br />
                        <asp:Chart ID="ChartResultSummary_DeltaEValue" runat="server" Width="340px" Height="400px" BackColor="Transparent"
                             ImageLocation="~/UploadFiles/temp/SingleMeasurement_Page1_ResultSummary_DeltaEValue"
                            ImageType="Jpeg">
                            <BorderSkin BackSecondaryColor="White" BorderColor="Black" BorderWidth="2"
                                SkinStyle="Raised" BackColor="White"></BorderSkin>                        
                            <Series>
                                <asp:Series Name="Series0"  Color="Transparent" ChartArea="ChartArea1">                                              
                                </asp:Series>
                                <asp:Series Name="Series1" BorderWidth="0" BorderColor="Gray" Color="Gray" 
                                                            IsVisibleInLegend="False" ChartArea="ChartArea1">
                                </asp:Series>    
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1" BackColor="Transparent">
                                        <Area3DStyle IsRightAngleAxes="False" WallWidth="0"
                                                                IsClustered="True"></Area3DStyle>
                                          
                                    <AxisX  Enabled="True"  IsReversed="True" IsLabelAutoFit="False" >
                                        <MajorGrid Enabled="False" LineColor="Beige" />
                                        <MajorTickMark Enabled="False" />
                                        <LabelStyle Font="Verdana, 7pt" />
                                    </AxisX>
                                        <AxisY Enabled="True" IsLabelAutoFit="False">
                                        <MajorGrid LineColor="Beige"></MajorGrid>
                                        <LabelStyle Format="###" Font="Verdana, 7pt" />
                                    </AxisY>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                </tr>
                <tr>
                    <td>
                        Gray L* Deviation Graph
                        <br />
                        <asp:Chart ID="CHRT_GrayDeviation_L" runat="server" Width="600px" Height="160px"
                            ImageType="Jpeg" ImageLocation="~/UploadFiles/temp/SingleMeasurement_Page2_GrayDeviation_L"
                             BackGradientType="Center" BackGradientEndColor="White" BorderLineColor="Gray" >                                                            
                            <BorderSkin BackSecondaryColor="White" BorderColor="Black" BorderWidth="2"
                                SkinStyle="Raised" BackColor="White"></BorderSkin>                            
                             <Titles>
                                    <asp:Title Text="L*" Name="Title1">
                                        <Position X="92" Y="15" Auto="False" />
                                    </asp:Title>
                                </Titles>
                                <Legends>
                                    <asp:Legend Name="Default" Enabled="False">
                                    </asp:Legend>
                                </Legends>
                                                                               
                                <Series>
                                    <asp:Series Name="Series1" LabelFormat="0" IsVisibleInLegend="False"
                                        Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                        MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255" 
                                        ChartArea="Default" Legend="Default">
                                    </asp:Series>
                                    <asp:Series MarkerSize="12" BackSecondaryColor="162, 164, 255" IsVisibleInLegend="False"
                                        Color="Green" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                        BorderWidth="2" Name="Series2" ChartArea="Default" Legend="Default">
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="Default" BorderColor="26, 59, 105" BackSecondaryColor="Gray"
                                        BackColor="White" BackGradientStyle="TopBottom" BorderDashStyle="Solid" ShadowOffset="1">
                                        <Position Height="99" Width="100" Auto="False" />
                                        <AxisY IsLabelAutoFit="False" TitleFont="Verdana, 8pt, style=Bold" Interval="25"
                                            Minimum="0" Maximum="100">
                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                            <LabelStyle Font="Verdana, 6pt" />
                                        </AxisY>
                                        <AxisX IsLabelAutoFit="False" IsMarginVisible="False" Interval="25" Minimum="0" 
                                            Maximum="100">
                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                            <LabelStyle Font="Verdana, 6pt" />
                                        </AxisX>
                                    </asp:ChartArea>
                                </ChartAreas>
                        </asp:Chart>
                    </td>
                </tr>
                <tr>
                    <td>
                        Gray a* Deviation Graph
                        <br />
                        <asp:Chart ID="CHRT_GrayDeviation_A" runat="server" Width="600px" Height="160px"
                            ImageType="Jpeg" ImageLocation="~/UploadFiles/temp/SingleMeasurement_Page2_GrayDeviation_A"
                           BackGradientType="Center" BackGradientEndColor="White" BorderLineColor="Gray" >                             
                            <Legends>
                                <asp:Legend Name="Default" Enabled="False">
                                </asp:Legend>
                            </Legends>
                            <Titles>
                                <asp:Title Text="a*" Name="Title1">
                                    <Position X="92" Y="15" Auto="False" />
                                </asp:Title>
                            </Titles>
                            <Series>
                                <asp:Series Name="Series1" LabelFormat="0" IsVisibleInLegend="False"
                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255" 
                                    ChartArea="Default" Legend="Default">
                                </asp:Series>
                                <asp:Series MarkerSize="12" BackSecondaryColor="162, 164, 255" IsVisibleInLegend="False"
                                    Color="Green" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                    BorderWidth="2" Name="Series2" ChartArea="Default" Legend="Default">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="Default" BorderColor="26, 59, 105" BackSecondaryColor="51, 153, 102"
                                    BackColor="255, 102, 204" BackGradientStyle="TopBottom" 
                                    BorderDashStyle="Solid" ShadowOffset="1">
                                    <Position Height="99" Width="100" Auto="False" />
                                    <AxisY IsLabelAutoFit="False" TitleFont="Verdana, 8pt, style=Bold" Interval="2"
                                        Minimum="-6" Maximum="6">
                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                        <LabelStyle Font="Verdana, 6pt" />
                                    </AxisY>
                                    <AxisX IsLabelAutoFit="False" IsMarginVisible="False" Interval="25" Minimum="0" 
                                        Maximum="100">
                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                        <LabelStyle Font="Verdana, 6pt" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                </tr>
                <tr>
                    <td>
                        Gray b* Deviation Graph
                        <br />
                        <asp:Chart ID="CHRT_GrayDeviation_B" runat="server" Width="600px" Height="160px"
                            ImageType="Jpeg" ImageLocation="~/UploadFiles/temp/SingleMeasurement_Page2_GrayDeviation_B"
                            BackGradientType="Center" BackGradientEndColor="White" BorderLineColor="Gray" >                           
                            <Legends>
                                <asp:Legend Name="Default" Enabled="False">
                                </asp:Legend>
                            </Legends>
                            <Titles>
                                <asp:Title Text="b*" Name="Title1">
                                    <Position X="92" Y="15" Auto="False" />
                                </asp:Title>
                            </Titles>
                                                                                   
                            <Series>
                                <asp:Series Name="Series1" LabelFormat="0" IsVisibleInLegend="False"
                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255" 
                                    ChartArea="Default" Legend="Default">
                                </asp:Series>
                                <asp:Series MarkerSize="12" BackSecondaryColor="162, 164, 255" IsVisibleInLegend="False"
                                    Color="Green" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                    BorderWidth="2" Name="Series2" ChartArea="Default" Legend="Default">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="Default" BorderColor="26, 59, 105" BackSecondaryColor="51, 51, 204"
                                    BackColor="255, 255, 153" BackGradientStyle="TopBottom" 
                                    BorderDashStyle="Solid" ShadowOffset="1">
                                    <Position Height="99" Width="100" Auto="False" />
                                    <AxisY IsLabelAutoFit="False" TitleFont="Verdana, 8pt, style=Bold" Interval="2"
                                        Minimum="-6" Maximum="6">
                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                        <LabelStyle Font="Verdana, 6pt" />
                                    </AxisY>
                                    <AxisX IsLabelAutoFit="False" IsMarginVisible="False" Interval="25" Minimum="0" 
                                        Maximum="100">
                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                        <LabelStyle Font="Verdana, 6pt" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                </tr>
                <tr>
                    <td>
                        Gray NPD
                        <br />
                        <%--<asp:Chart ID="CHRT_GrayNPD" runat="server" Width="450px" Height="385px" ImageType="Jpeg"
                            ImageUrl="~/UploadFiles/temp/SingleMeasurement_Page2_GrayNPD" BackGradientType="Center"
                            Palette="Kindergarten" BackGradientEndColor="White" BorderLineColor="#808080">
                            <Legends>
                                <asp:Legend Name="Default" Enabled="false">
                                </asp:Legend>
                            </Legends>
                            <Titles>
                                <asp:Title Text="Gray NPD" Alignment="TopCenter" Font="Verdana, 8pt, style=Bold">
                                </asp:Title>
                            </Titles>
                            <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2"
                                SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                            <UI>
                                <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                    <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                        FrameBackColor="SteelBlue"></BorderSkin>
                                </Toolbar>
                            </UI>
                            <Series>
                                <asp:Series Name="Series1" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                </asp:Series>
                                <asp:Series MarkerSize="12" BackGradientEndColor="162, 164, 255" ShowInLegend="false"
                                    Color="Green" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                    BorderWidth="2" Name="Series2">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                    BackColor="Gray" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                    <Position Height="80" Width="90" X="0" Y="15" />
                                    <AxisY LabelsAutoFit="False" Title="Density" TitleFont="Verdana, 8pt, style=Bold" Interval="Auto">
                                        <MajorGrid LineColor="Gray"></MajorGrid>                                                                       
                                                                        
                                    </AxisY>
                                    <AxisX LabelsAutoFit="False" Margin="False" Interval="Auto" Title="">
                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                        <LabelStyle FontAngle="0" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>--%>
                    </td>
                </tr>
                <tr>
                    <td>
                        Black NPD
                        <br />
                        <%--<asp:Chart ID="CHRT_BlackNPD" runat="server" Width="450px" Height="385" ImageType="Jpeg"
                            ImageUrl="~/UploadFiles/temp/SingleMeasurement_Page2_BlackNPD" BackGradientType="Center"
                            Palette="Kindergarten" BackGradientEndColor="White" BorderLineColor="#808080">
                            <Titles>
                                <asp:Title Text="Black NPD" Alignment="TopCenter" Font="Verdana, 8pt, style=Bold">
                                </asp:Title>
                            </Titles>
                            <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2"
                                SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                            <UI>
                                <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                    <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                        FrameBackColor="SteelBlue"></BorderSkin>
                                </Toolbar>
                            </UI>
                            <Series>
                                <asp:Series Name="Series1" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                </asp:Series>
                                <asp:Series MarkerSize="12" BackGradientEndColor="162, 164, 255" ShowInLegend="false"
                                    Color="Green" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                    BorderWidth="2" Name="Series2">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                    BackColor="Gray" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                    <Position Height="80" Width="90" X="0" Y="15" />
                                    <AxisY LabelsAutoFit="False" Title="Density" TitleFont="Verdana, 8pt, style=Bold" Interval="Auto">
                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                    </AxisY>
                                    <AxisX LabelsAutoFit="False" Margin="False" Interval="Auto" Title="">
                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                        <LabelStyle FontAngle="0" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>--%>
                    </td>
                </tr>
                <tr>
                    <td>
                        Dotgain Actual
                        <br />
                        <%--<asp:Chart ID="CHRT_DotgainActual" runat="server" Width="900px" Height="250px" ImageType="Jpeg"
                            ImageUrl="~/UploadFiles/temp/SingleMeasurement_Page3_CHRT_DotgainActual" BackGradientType="Center"
                            Palette="Kindergarten" BackGradientEndColor="White" BorderLineColor="#808080">                                                           
                            <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2"
                                SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                            <UI>
                                <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                    <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                        FrameBackColor="SteelBlue"></BorderSkin>
                                </Toolbar>
                            </UI>
                            <Series>
                                <asp:Series Name="Series1" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                    Color="Cyan" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                </asp:Series>
                                <asp:Series Name="Series2" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                    Color="Magenta" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                </asp:Series>
                                <asp:Series Name="Series3" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                    Color="Yellow" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                </asp:Series>
                                <asp:Series Name="Series4" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                </asp:Series>
                                <asp:Series MarkerSize="12" BackGradientEndColor="162, 164, 255" ShowInLegend="false"
                                    Color="Green" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                    BorderWidth="2" Name="Series5">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                    BackColor="Gray" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                    <Position Height="80" Width="90" X="0" Y="15" />
                                    <AxisY LabelsAutoFit="False" Title="" Interval="Auto">
                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                    </AxisY>
                                    <AxisX LabelsAutoFit="False" Margin="False" Interval="Auto" Title="">
                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                        <LabelStyle FontAngle="0" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>--%>
                    </td>
                </tr>
                <tr>
                    <td>
                        Dotgain Deviation<br />
                        <%--<asp:Chart ID="CHART_DotgainDeviation" ImageType="Jpeg" runat="server" Width="380px" Height="450px" 
                                ImageUrl="~/UploadFiles/temp/SingleMeasurement_Page3_DotgainDeviation"
                            BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#FFFFFF"
                            BorderLineColor="#808080">
                            <Legends>
                                <asp:Legend Name="Default">
                                </asp:Legend>
                                <asp:Legend Name="Legends1" Alignment="Center" Docking="Bottom">
                                </asp:Legend>
                            </Legends>                                                           
                            <Series>
                                <asp:Series Name="Series0" Color="Transparent" ChartArea="ChartArea1" BorderWidth="1"
                                    ShowInLegend="false">
                                </asp:Series>
                                <asp:Series Name="Series1" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="1" CustomAttributes="DrawingStyle=Cylinder"
                                    ShowInLegend="false">
                                </asp:Series>
                                <asp:Series Name="Series2" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="2"
                                    ShowInLegend="false">
                                </asp:Series>
                                    <asp:Series Name="Series3" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="1"
                                    ShowInLegend="false">
                                </asp:Series>
                                <asp:Series Name="Series4" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="1"
                                    ShowInLegend="false">
                                </asp:Series>
                            </Series>
                            <BorderSkin SkinStyle="None"></BorderSkin>
                            <UI>
                                <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                    <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                        FrameBackColor="SteelBlue"></BorderSkin>
                                </Toolbar>
                            </UI>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                    BackColor="Gray" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                    <Area3DStyle YAngle="10" XAngle="10" RightAngleAxes="False" WallWidth="0" Clustered="true">
                                    </Area3DStyle>
                                    <Position Height="99" Width="94" X="0" Y="2" />
                                    <AxisY Enabled="True">
                                        <MajorGrid LineColor="Gray" />
                                        <SubAxes>
                                            <asp:SubAxis>
                                            </asp:SubAxis>
                                        </SubAxes>
                                    </AxisY>
                                    <AxisX Enabled="True" Interval="Auto">
                                        <MajorGrid LineColor="Gray" />
                                        <SubAxes>
                                            <asp:SubAxis>
                                            </asp:SubAxis>
                                        </SubAxes>
                                    </AxisX>
                                    <AxisX2 Enabled="True" Interval="Auto" Minimum="0" Maximum="1">
                                        <LabelStyle ShowEndLabels="false" />
                                        <MajorGrid LineColor="Beige" />
                                        <SubAxes>
                                            <asp:SubAxis>
                                            </asp:SubAxis>
                                        </SubAxes>
                                    </AxisX2>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>--%>
                    </td>
                </tr>
                <tr>
                    <td>
                        Density Deviation<br />
                        <%--<asp:Chart ID="CHRT_DensityDeviation" ImageType="Jpeg" runat="server" Width="380px" Height="450px" 
                                ImageUrl="~/UploadFiles/temp/SingleMeasurement_Page3_DensityDeviation"
                            BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#FFFFFF"
                            BorderLineColor="#808080">
                            <Legends>
                                <asp:Legend Name="Default">
                                </asp:Legend>
                                <asp:Legend Name="Legends1" Alignment="Center" Docking="Bottom">
                                </asp:Legend>
                            </Legends>                                                           
                            <Series>
                                <asp:Series Name="Series0" Color="Transparent" ChartArea="ChartArea1" BorderWidth="1"
                                    ShowInLegend="false">
                                </asp:Series>
                                <asp:Series Name="Series1" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="1" CustomAttributes="DrawingStyle=Cylinder"
                                    ShowInLegend="false">
                                </asp:Series>
                                <asp:Series Name="Series2" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="2"
                                    ShowInLegend="false">
                                </asp:Series>
                                <asp:Series Name="Series3" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="1"
                                    ShowInLegend="false">
                                </asp:Series>
                                <asp:Series Name="Series4" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="1"
                                    ShowInLegend="false">
                                </asp:Series>
                            </Series>
                            <BorderSkin SkinStyle="None"></BorderSkin>
                            <UI>
                                <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                    <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                        FrameBackColor="SteelBlue"></BorderSkin>
                                </Toolbar>
                            </UI>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                    BackColor="Gray" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                    <Area3DStyle YAngle="10" XAngle="10" RightAngleAxes="False" WallWidth="0" Clustered="true">
                                    </Area3DStyle>
                                    <Position Height="99" Width="94" X="0" Y="2" />
                                    <AxisY Enabled="True">
                                        <MajorGrid LineColor="Beige" />
                                        <SubAxes>
                                            <asp:SubAxis>
                                            </asp:SubAxis>
                                        </SubAxes>
                                    </AxisY>
                                    <AxisX Enabled="True" Interval="Auto">
                                        <MajorGrid LineColor="Beige" />
                                        <SubAxes>
                                            <asp:SubAxis>
                                            </asp:SubAxis>
                                        </SubAxes>
                                    </AxisX>
                                    <AxisX2 Enabled="True" Interval="Auto" Minimum="0" Maximum="1">
                                        <LabelStyle ShowEndLabels="false" />
                                        <MajorGrid LineColor="Beige" />
                                        <SubAxes>
                                            <asp:SubAxis>
                                            </asp:SubAxis>
                                        </SubAxes>
                                    </AxisX2>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>--%>
                    </td>
                </tr>
                <tr>
                    <td>
                        Print Contrast Deviation<br />
                       <%-- <asp:Chart ID="CHRT_PrintContrastDeviation" ImageType="Jpeg" runat="server" Width="380px" Height="450px" 
                            ImageUrl="~/UploadFiles/temp/SingleMeasurement_Page3_PrintContrastDeviation"
                            BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#FFFFFF"
                            BorderLineColor="#808080">
                            <Legends>
                                <asp:Legend Name="Default">
                                </asp:Legend>
                                <asp:Legend Name="Legends1" Alignment="Center" Docking="Bottom">
                                </asp:Legend>
                            </Legends>                                                          
                            <Series>
                                <asp:Series Name="Series0" Color="Transparent" ChartArea="ChartArea1" BorderWidth="1"
                                    ShowInLegend="false">
                                </asp:Series>
                                <asp:Series Name="Series1" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="1" CustomAttributes="DrawingStyle=Cylinder"
                                    ShowInLegend="false">
                                </asp:Series>
                                <asp:Series Name="Series2" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="2"
                                    ShowInLegend="false">
                                </asp:Series>
                                <asp:Series Name="Series3" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="1"
                                    ShowInLegend="false">
                                </asp:Series>
                                <asp:Series Name="Series4" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="1"
                                    ShowInLegend="false">
                                </asp:Series>
                            </Series>
                            <BorderSkin SkinStyle="None"></BorderSkin>
                            <UI>
                                <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                    <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                        FrameBackColor="SteelBlue"></BorderSkin>
                                </Toolbar>
                            </UI>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                    BackColor="Gray" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                    <Area3DStyle YAngle="10" XAngle="10" RightAngleAxes="False" WallWidth="0" Clustered="true">
                                    </Area3DStyle>
                                    <Position Height="99" Width="94" X="0" Y="2" />
                                    <AxisY Enabled="True">
                                        <MajorGrid LineColor="Beige" />
                                        <SubAxes>
                                            <asp:SubAxis>
                                            </asp:SubAxis>
                                        </SubAxes>
                                    </AxisY>
                                    <AxisX Enabled="True" Interval="Auto">
                                        <MajorGrid LineColor="Beige" />
                                        <SubAxes>
                                            <asp:SubAxis>
                                            </asp:SubAxis>
                                        </SubAxes>
                                    </AxisX>
                                    <AxisX2 Enabled="True" Interval="Auto" Minimum="0" Maximum="1">
                                        <LabelStyle ShowEndLabels="false" />
                                        <MajorGrid LineColor="Beige" />
                                        <SubAxes>
                                            <asp:SubAxis>
                                            </asp:SubAxis>
                                        </SubAxes>
                                    </AxisX2>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>--%>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>