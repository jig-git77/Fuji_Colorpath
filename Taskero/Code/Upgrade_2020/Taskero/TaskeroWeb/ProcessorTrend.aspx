<%@ Page Title="" Language="C#" MasterPageFile="~/MasterProcessor.Master" AutoEventWireup="true" EnableEventValidation="false"  CodeBehind="ProcessorTrend.aspx.cs" Inherits="TaskeroWeb.ProcessorTrend" %>

<%@ Register Assembly="DundasWebGauge" Namespace="Dundas.Gauges.WebControl" TagPrefix="dgwc" %>
<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="5" border="0" style="width: 900px">
                <tr>
                    <td>
                        <div>
                            Record(s)&nbsp;
                            <asp:Literal ID="LTRL_StartRecord" runat="server">0</asp:Literal>
                            &nbsp;to&nbsp;
                            <asp:Literal ID="LTRL_EndRecord" runat="server">0</asp:Literal>
                            &nbsp;of&nbsp;
                            <asp:Literal ID="LTRL_TotalRec" runat="server">0</asp:Literal>
                            &nbsp;|&nbsp;
                            <asp:ImageButton ID="IBTN_FirstPage" runat="server" ImageUrl="Images/Nav_FirstPage_off.gif"
                                OnClick="IBTN_FirstPage_Click" />
                            &nbsp;
                            <asp:ImageButton ID="IBTN_PreviousPage" runat="server" ImageUrl="Images/Nav_PreviousPage_off.gif"
                                OnClick="IBTN_PreviousPage_Click" />
                            &nbsp;
                            <asp:ImageButton ID="IBTN_NextPage" runat="server" ImageUrl="Images/Nav_NextPage_off.gif"
                                OnClick="IBTN_NextPage_Click" />
                            &nbsp;
                            <asp:ImageButton ID="IBTN_LastPage" runat="server" ImageUrl="Images/Nav_LastPage_off.gif"
                                OnClick="IBTN_LastPage_Click" />
                            &nbsp;|&nbsp;Records&nbsp;
                            <asp:DropDownList ID="DDL_PageSize" runat="server" CssClass="DROPDOWNLIST2" AutoPostBack="true"
                                OnSelectedIndexChanged="DDL_PageSize_SelectedIndexChanged">
                                <asp:ListItem Value="25">25</asp:ListItem>
                                <asp:ListItem Value="48" Selected="True">48</asp:ListItem>
                                <asp:ListItem Value="100">100</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;|&nbsp;Go to page&nbsp;
                            <asp:DropDownList ID="DDL_GoToPage" runat="server" CssClass="DROPDOWNLIST2" AutoPostBack="true"
                                OnSelectedIndexChanged="DDL_GoToPage_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div>
                            <DCWC:Chart ID="CHRT_Date" runat="server" Width="850px" Height="40px" ImageUrl="~/UploadFiles/temp/CHRT_Date_#SEQ(300,3)"
                                ImageType="Jpeg" BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#DFDFDF"
                                BorderLineColor="#808080">
                                <Legends>
                                    <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom">
                                    </DCWC:Legend>
                                </Legends>
                                <Series>
                                    <DCWC:Series Name="Series0" Color="Transparent" ChartArea="ChartArea1" BorderWidth="1"
                                        ShowInLegend="false">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Series1" Color="Transparent" ChartArea="ChartArea1" BorderWidth="1"
                                        ShowInLegend="false">
                                    </DCWC:Series>
                                </Series>
                                <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2"
                                    SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                                <UI>
                                    <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                        <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                            FrameBackColor="SteelBlue"></BorderSkin>
                                    </Toolbar>
                                </UI>
                                <ChartAreas>
                                    <DCWC:ChartArea Name="ChartArea1" BackColor="Transparent">
                                        <Position Height="60" Width="99" X="5" Y="0" />
                                        <AxisY LineColor="Transparent" Enabled="True">
                                            <MinorGrid LineColor="Transparent" Interval="1" />
                                            <MajorGrid LineColor="Transparent" />
                                            <LabelStyle FontColor="Transparent" />
                                        </AxisY>
                                        <AxisY2 LineColor="Transparent" Enabled="True">
                                            <MinorGrid LineColor="Transparent" Interval="1" />
                                            <MajorGrid LineColor="Transparent" />
                                            <LabelStyle FontColor="Transparent" />
                                        </AxisY2>
                                        <AxisX Enabled="True" IntervalAutoMode="VariableCount"  LineColor="Transparent">
                                            <MinorGrid LineColor="Transparent" />
                                            <MajorGrid LineColor="Transparent" />
                                        </AxisX>
                                    </DCWC:ChartArea>
                                </ChartAreas>
                            </DCWC:Chart>
                        </div>
                        <div id="CHART_DIV_SCROLL1">
                            <!-- Trend Conductivity  -->
                            <div class="CHART_DIV">
                                <div class="CHART_DIV_LEFT">
                                    <DCWC:Chart ID="CHRT_TrendDeltaE" runat="server" Width="850px" Height="125px" ImageUrl="~/UploadFiles/temp/CHRT_DeltaE_#SEQ(300,3)"
                                        ImageType="Jpeg" BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#DFDFDF"
                                        BorderLineColor="#808080">
                                        <Legends>
                                            <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom">
                                            </DCWC:Legend>
                                        </Legends>
                                        <Titles>
                                            <DCWC:Title Text="Delta E" Visible="false">
                                            </DCWC:Title>
                                        </Titles>
                                        <Series>
                                            <DCWC:Series Name="Series0" Color="Transparent" ChartArea="ChartArea1" BorderWidth="1"
                                                ShowInLegend="false">
                                            </DCWC:Series>
                                            <DCWC:Series Name="Series1" Color="150, 000, 255, 255" ChartArea="ChartArea1" BorderWidth="1"
                                                ShowInLegend="false">
                                            </DCWC:Series>
                                            <DCWC:Series Name="Series2" Color="150, 255, 000, 255" ChartArea="ChartArea1" BorderWidth="1"
                                                ShowInLegend="false">
                                            </DCWC:Series>
                                            <DCWC:Series Name="Series3" Color="150, 255, 000, 255" ChartArea="ChartArea1" BorderWidth="1"
                                                ShowInLegend="false">
                                            </DCWC:Series>
                                        </Series>
                                        <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2"
                                            SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                                        <UI>
                                            <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                                <BorderSkin FrameBackGradientEndColor="Gray" PageColor="Transparent" SkinStyle="Raised"
                                                    FrameBackColor="SteelBlue"></BorderSkin>
                                            </Toolbar>
                                        </UI>
                                        <ChartAreas>
                                            <DCWC:ChartArea Name="ChartArea1">
                                                <Position Height="100" Width="99" X="0" Y="10" />
                                                <AxisY Enabled="True" LabelsAutoFit="False" StartFromZero="false" LineColor="Blue"
                                                    LineStyle="Solid" LineWidth="2">
                                                    <LabelStyle FontColor="Blue" />
                                                    <MajorGrid LineColor="White" />
                                                    <SubAxes>
                                                        <DCWC:SubAxis Title="Conductivity" TitleColor="Blue">
                                                        </DCWC:SubAxis>
                                                    </SubAxes>
                                                </AxisY>
                                                <AxisY2 Enabled="True" LabelsAutoFit="False" StartFromZero="false" LineColor="BlueViolet"
                                                    LineStyle="Solid" LineWidth="2">
                                                    <LabelStyle FontColor="BlueViolet" />
                                                    <MajorGrid LineColor="White" />
                                                    <SubAxes>
                                                        <DCWC:SubAxis Title="Temperature" TitleColor="BlueViolet">
                                                        </DCWC:SubAxis>
                                                    </SubAxes>
                                                </AxisY2>
                                                <AxisX Enabled="True">
                                                    <LabelStyle FontColor="Transparent" />
                                                    <MajorGrid LineColor="Beige" />
                                                </AxisX>
                                            </DCWC:ChartArea>
                                        </ChartAreas>
                                    </DCWC:Chart>
                                </div>
                            </div>
                            <!-- Trend Plates Per Period  -->
                            <div class="CHART_DIV">
                                <div class="CHART_DIV_LEFT">
                                    <DCWC:Chart ID="CHRT_TrendPlatePerPeriod" runat="server" Width="850px" Height="125px"
                                        ImageUrl="~/UploadFiles/temp/CHRT_DeltaE_#SEQ(300,3)" ImageType="Jpeg" BackGradientType="Center"
                                        Palette="Kindergarten" BackGradientEndColor="#DFDFDF" BorderLineColor="#808080">
                                        <Legends>
                                            <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom">
                                            </DCWC:Legend>
                                        </Legends>
                                        <Titles>
                                            <DCWC:Title Text="Delta E" Visible="false">
                                            </DCWC:Title>
                                        </Titles>
                                        <Series>
                                            <DCWC:Series Name="Series0" Color="Transparent" ChartArea="ChartArea1" BorderWidth="1"
                                                ShowInLegend="false">
                                            </DCWC:Series>
                                            <DCWC:Series Name="Series1" Color="150, 000, 255, 255" ChartArea="ChartArea1" BorderWidth="1"
                                                ShowInLegend="false">
                                            </DCWC:Series>
                                            <DCWC:Series Name="Series2" Color="150, 255, 000, 255" ChartArea="ChartArea1" BorderWidth="1"
                                                ShowInLegend="false">
                                            </DCWC:Series>
                                        </Series>
                                        <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2"
                                            SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                                        <UI>
                                            <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                                <BorderSkin FrameBackGradientEndColor="Gray" PageColor="Transparent" SkinStyle="Raised"
                                                    FrameBackColor="SteelBlue"></BorderSkin>
                                            </Toolbar>
                                        </UI>
                                        <ChartAreas>
                                            <DCWC:ChartArea Name="ChartArea1">
                                                <Position Height="100" Width="90" X="0" Y="10" />
                                                <AxisY Enabled="True" LabelsAutoFit="False" StartFromZero="false" LineColor="Blue"
                                                    LineStyle="Solid" LineWidth="2">
                                                    <LabelStyle FontColor="Blue" />
                                                    <MajorGrid LineColor="White" />
                                                    <SubAxes>
                                                        <DCWC:SubAxis Title="Plates Per Period" IntervalType="Number" TitleColor="Blue">
                                                        </DCWC:SubAxis>
                                                    </SubAxes>
                                                </AxisY>
                                                <AxisY2 Enabled="false" LabelsAutoFit="False">
                                                    <LabelStyle FontColor="BlueViolet" />
                                                    <MajorGrid LineColor="White" />
                                                    <SubAxes>
                                                        <DCWC:SubAxis Title="" TitleColor="Blue">
                                                        </DCWC:SubAxis>
                                                    </SubAxes>
                                                </AxisY2>
                                                <AxisX Enabled="True">
                                                    <LabelStyle FontColor="Transparent" />
                                                    <MajorGrid LineColor="Beige" />
                                                </AxisX>
                                            </DCWC:ChartArea>
                                        </ChartAreas>
                                    </DCWC:Chart>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table id="Table1" cellpadding="0" cellspacing="0" border="0" style="width: 99%;
                            margin-top: 5px" runat="server" class="popup-table">
                            <tr>
                                <td class="popup-top-bar" colspan="3">
                                    Measurement Details
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 35%">
                                    <table style="width: 100%;" cellspacing="5" border="0" cellpadding="0">
                                        <tr>
                                            <td class="green-title">
                                                Rebatch Consumables Usage
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" border="0" class="TABLE1">
                                                    <tr>
                                                        <td class="green-title">
                                                            Developer
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dgwc:GaugeContainer ID="GCDeveloper" runat="server" Height="50px" AutoLayout="False"
                                                                ImageUrl="~/TempImages/GaugePic_#SEQ(300,3)"  BackColor="Gray">
                                                                <Values>
                                                                    <dgwc:InputValue Name="Default">
                                                                    </dgwc:InputValue>
                                                                </Values>
                                                                <BackFrame FrameWidth="1" Style="simple" BackGradientEndColor="0, 0, 0" FrameStyle="None"
                                                                    FrameColor="Gray" BackColor="Black" FrameGradientEndColor="Black" BackGradientType="TopBottom">
                                                                </BackFrame>
                                                                <LinearGauges>
                                                                    <dgwc:LinearGauge Name="Developer">
                                                                        <Scales>
                                                                            <dgwc:LinearScale FillGradientType="TopBottom" FillGradientEndColor="WhiteSmoke"
                                                                                Width="0" FillColor="Transparent" BorderWidth="1" Name="Default" Position="56">
                                                                                <LabelStyle Placement="Inside" TextColor="White" DistanceFromScale="5"></LabelStyle>
                                                                                <MajorTickMark FillColor="White"></MajorTickMark>
                                                                                <MinorTickMark Width="2" FillColor="White"></MinorTickMark>
                                                                            </dgwc:LinearScale>
                                                                        </Scales>
                                                                        <BackFrame FrameWidth="15" Style="edged" Shape="Rectangular" FrameGradientType="TopBottom"
                                                                            FrameColor="Gray" BackColor="Black" FrameGradientEndColor="Black" BackGradientType="TopBottom">
                                                                        </BackFrame>
                                                                        <Size Height="100" Width="79"></Size>
                                                                        <Location Y="0" X="0"></Location>
                                                                        <Pointers>
                                                                            <dgwc:LinearPointer ThermometerBackGradientEndColor="" MarkerLength="20" ThermometerBackColor="ActiveBorder"
                                                                                Placement="Cross" Width="8" MarkerStyle="Rectangle" Name="Default"></dgwc:LinearPointer>
                                                                        </Pointers>
                                                                        <Ranges>
                                                                            <dgwc:LinearRange FillColor="Red" Name="Default" StartValue="0" Placement="Cross"
                                                                                StartWidth="20" DistanceFromScale="0" EndWidth="20"></dgwc:LinearRange>
                                                                        </Ranges>
                                                                    </dgwc:LinearGauge>
                                                                </LinearGauges>
                                                                <NumericIndicators>
                                                                    <dgwc:NumericIndicator ShowDecimalPoint="true" IndicatorStyle="Digital14Segment"
                                                                        Name="Default" FormatString="000.00" DigitColor="Black" BackColor="Olive" Parent="Developer"
                                                                        DampeningEnabled="true" Location-X="100">
                                                                        <Size Width="26" />
                                                                        <Location X="101" Y="0" />
                                                                    </dgwc:NumericIndicator>
                                                                </NumericIndicators>
                                                            </dgwc:GaugeContainer>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" border="0" class="TABLE1">
                                                    <tr>
                                                        <td class="green-title">
                                                            Plate Area
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dgwc:GaugeContainer ID="GCPlateArea" runat="server" Height="50px" AutoLayout="False"
                                                                ImageUrl="~/TempImages/GaugePic_#SEQ(300,3)"  BackColor="Gray">
                                                                <Values>
                                                                    <dgwc:InputValue Name="Default">
                                                                    </dgwc:InputValue>
                                                                </Values>
                                                                <BackFrame FrameWidth="1" Style="simple" BackGradientEndColor="0, 0, 0" FrameStyle="None"
                                                                    FrameColor="Gray" BackColor="Black" FrameGradientEndColor="Black" BackGradientType="TopBottom">
                                                                </BackFrame>
                                                                <LinearGauges>
                                                                    <dgwc:LinearGauge Name="PlateArea">
                                                                        <Scales>
                                                                            <dgwc:LinearScale FillGradientType="TopBottom" FillGradientEndColor="WhiteSmoke"
                                                                                Width="0" FillColor="Transparent" BorderWidth="1" Name="Default" Position="56">
                                                                                <LabelStyle Placement="Inside" TextColor="White" DistanceFromScale="5"></LabelStyle>
                                                                                <MajorTickMark FillColor="White"></MajorTickMark>
                                                                                <MinorTickMark Width="2" FillColor="White"></MinorTickMark>
                                                                            </dgwc:LinearScale>
                                                                        </Scales>
                                                                        <BackFrame FrameWidth="15" Style="edged" Shape="Rectangular" FrameGradientType="TopBottom"
                                                                            FrameColor="Gray" BackColor="Black" FrameGradientEndColor="Black" BackGradientType="TopBottom">
                                                                        </BackFrame>
                                                                        <Size Height="100" Width="79"></Size>
                                                                        <Location Y="0" X="0"></Location>
                                                                        <Pointers>
                                                                            <dgwc:LinearPointer ThermometerBackGradientEndColor="" MarkerLength="20" ThermometerBackColor="ActiveBorder"
                                                                                Placement="Cross" Width="8" MarkerStyle="Rectangle" Name="Default"></dgwc:LinearPointer>
                                                                        </Pointers>
                                                                        <Ranges>
                                                                            <dgwc:LinearRange FillColor="Red" Name="Default" StartValue="0" Placement="Cross"
                                                                                StartWidth="20" DistanceFromScale="0" EndWidth="20"></dgwc:LinearRange>
                                                                        </Ranges>
                                                                    </dgwc:LinearGauge>
                                                                </LinearGauges>
                                                                <NumericIndicators>
                                                                    <dgwc:NumericIndicator ShowDecimalPoint="true" FormatString="000.00" BackColor="Olive"
                                                                        DigitColor="Black" Parent="PlateArea" IndicatorStyle="Digital14Segment" Name="Default"
                                                                        DampeningEnabled="true" Location-X="100">
                                                                        <Size Width="26" />
                                                                        <Location X="101" Y="0" />
                                                                    </dgwc:NumericIndicator>
                                                                </NumericIndicators>
                                                            </dgwc:GaugeContainer>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" border="0" class="TABLE1">
                                                    <tr>
                                                        <td class="green-title">
                                                            Number Of Plates
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dgwc:GaugeContainer ID="GCNumberOfPlate"  runat="server" Height="50px" AutoLayout="False" 
                                                                ImageUrl="~/TempImages/GaugePic_#SEQ(300,3)"  BackColor="Gray">
                                                                <Values>
                                                                    <dgwc:InputValue Name="Default">
                                                                    </dgwc:InputValue>
                                                                </Values>
                                                                <BackFrame FrameWidth="1"   Style="simple" BackGradientEndColor="0, 0, 0" FrameStyle="None"
                                                                    FrameColor="Gray" BackColor="Black" FrameGradientEndColor="Black" BackGradientType="TopBottom">
                                                                </BackFrame>
                                                                <LinearGauges>
                                                                    <dgwc:LinearGauge Name="NumberOfPlate" >
                                                                        <Scales>
                                                                            <dgwc:LinearScale FillGradientType="TopBottom" FillGradientEndColor="WhiteSmoke"
                                                                                Width="0" FillColor="Transparent" BorderWidth="1" Name="Default" Position="56">
                                                                                <LabelStyle Placement="Inside" TextColor="White" DistanceFromScale="5"></LabelStyle>
                                                                                <MajorTickMark FillColor="White"></MajorTickMark>
                                                                                <MinorTickMark Width="2" FillColor="White"></MinorTickMark>
                                                                            </dgwc:LinearScale>
                                                                        </Scales>
                                                                        <BackFrame FrameWidth="15" Style="edged" Shape="Rectangular" FrameGradientType="TopBottom"
                                                                            FrameColor="Gray" BackColor="Black" FrameGradientEndColor="Black" BackGradientType="TopBottom">
                                                                        </BackFrame>
                                                                        <Size Height="100" Width="79"></Size>                                                                        
                                                                        <Location Y="0" X="0"></Location>
                                                                        <Pointers>
                                                                            <dgwc:LinearPointer ThermometerBackGradientEndColor="" MarkerLength="20" ThermometerBackColor="ActiveBorder"
                                                                                Placement="Cross" Width="8" MarkerStyle="Rectangle" Name="Default"></dgwc:LinearPointer>
                                                                        </Pointers>
                                                                        <Ranges>
                                                                            <dgwc:LinearRange FillColor="Red" Name="Default" StartValue="0" Placement="Cross"
                                                                                StartWidth="20" DistanceFromScale="0" EndWidth="20"></dgwc:LinearRange>
                                                                        </Ranges>
                                                                    </dgwc:LinearGauge>
                                                                </LinearGauges>
                                                                <NumericIndicators>
                                                                    <dgwc:NumericIndicator ShowDecimalPoint="true" FormatString="000.00" Name="Default"
                                                                        DigitColor="Black" BackColor="Olive" Parent="NumberOfPlate" IndicatorStyle="Digital14Segment"
                                                                        DampeningEnabled="true" Location-X="100">
                                                                        <Size Width="26" />
                                                                        <Location X="101" Y="0" />
                                                                    </dgwc:NumericIndicator>
                                                                </NumericIndicators>
                                                            </dgwc:GaugeContainer>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td valign="top" style="width: 17%">
                                    <table style="width: 100%;" cellspacing="5" border="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" border="0" class="TABLE1">
                                                    <tr>
                                                        <td class="green-title">
                                                            Time Stamp
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" style="padding-top: 7px; padding-bottom: 7px">
                                                            <asp:Label ID="LBLTimeStamp" runat="server" Style="background-color: #FFF; font-size: 6.50pt;
                                                                color: #80C5B0; font-weight: bold"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" border="0" class="TABLE1">
                                                    <tr>
                                                        <td class="green-title">
                                                            Plates Per Period
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" style="background-color: Gray">
                                                            <dgwc:GaugeContainer ID="GCPlatesPerPeriod" runat="server" Height="33px" Width="120px"
                                                                AutoLayout="False" ImageUrl="~/TempImages/GaugePic_#SEQ(300,3)" 
                                                                BackColor="Gray">
                                                                <Values>
                                                                    <dgwc:InputValue Name="Default">
                                                                    </dgwc:InputValue>
                                                                </Values>
                                                                <BackFrame FrameWidth="1" Style="simple" BackGradientEndColor="0, 0, 0" FrameStyle="None"
                                                                    FrameColor="Gray" BackColor="Black" FrameGradientEndColor="Black" BackGradientType="TopBottom">
                                                                </BackFrame>
                                                                <NumericIndicators>
                                                                    <dgwc:NumericIndicator ShowDecimalPoint="true" FormatString="00000.00" BackColor="Olive"
                                                                        DigitColor="Black" IndicatorStyle="Digital14Segment" Name="Default" DampeningEnabled="true"
                                                                        Location-X="100">
                                                                        <Size Width="100" />
                                                                        <Location X="0" Y="0" />
                                                                    </dgwc:NumericIndicator>
                                                                </NumericIndicators>
                                                            </dgwc:GaugeContainer>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" border="0" class="TABLE1">
                                                    <tr>
                                                        <td class="green-title">
                                                            Lifetime Usage
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0" border="0" class="TABLE1">
                                                                <tr>
                                                                    <td class="green-title">
                                                                        Total Area
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" style="background-color: Gray">
                                                                        <dgwc:GaugeContainer ID="GCTotalArea" runat="server" Height="33px" Width="120px"
                                                                            AutoLayout="False" ImageUrl="~/TempImages/GaugePic_#SEQ(300,3)" 
                                                                            BackColor="Gray">
                                                                            <Values>
                                                                                <dgwc:InputValue Name="Default">
                                                                                </dgwc:InputValue>
                                                                            </Values>
                                                                            <BackFrame FrameWidth="1" Style="simple" BackGradientEndColor="0, 0, 0" FrameStyle="None"
                                                                                FrameColor="Gray" BackColor="Black" FrameGradientEndColor="Black" BackGradientType="TopBottom">
                                                                            </BackFrame>
                                                                            <NumericIndicators>
                                                                                <dgwc:NumericIndicator ShowDecimalPoint="true" FormatString="00000.00" Name="Default"
                                                                                    DigitColor="Black" BackColor="Olive" IndicatorStyle="Digital14Segment" DampeningEnabled="true"
                                                                                    Location-X="100">
                                                                                    <Size Width="100" />
                                                                                    <Location X="0" Y="0" />
                                                                                </dgwc:NumericIndicator>
                                                                            </NumericIndicators>
                                                                        </dgwc:GaugeContainer>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0" border="0" class="TABLE1">
                                                                <tr>
                                                                    <td class="green-title">
                                                                        Total Plates
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" style="background-color: Gray">
                                                                        <dgwc:GaugeContainer ID="GCTotalPlates" runat="server" Height="33px" Width="120px"
                                                                            AutoLayout="False" ImageUrl="~/TempImages/GaugePic_#SEQ(300,3)" 
                                                                            BackColor="Gray">
                                                                            <Values>
                                                                                <dgwc:InputValue Name="Default">
                                                                                </dgwc:InputValue>
                                                                            </Values>
                                                                            <BackFrame FrameWidth="1" Style="simple" BackGradientEndColor="0, 0, 0" FrameStyle="None"
                                                                                FrameColor="Gray" BackColor="Black" FrameGradientEndColor="Black" BackGradientType="TopBottom">
                                                                            </BackFrame>
                                                                            <NumericIndicators>
                                                                                <dgwc:NumericIndicator ShowDecimalPoint="true" FormatString="00000.00" Name="Default"
                                                                                    DigitColor="Black" BackColor="Olive" IndicatorStyle="Digital14Segment" DampeningEnabled="true"
                                                                                    Location-X="100">
                                                                                    <Size Width="100" />
                                                                                    <Location X="0" Y="0" />
                                                                                </dgwc:NumericIndicator>
                                                                            </NumericIndicators>
                                                                        </dgwc:GaugeContainer>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td valign="top" style="width: 48%">
                                    <table cellpadding="0" cellspacing="5"" " border="0" class="TABLE1">
                                        <tr>
                                            <td>
                                                <dgwc:GaugeContainer ID="GCCircular" runat="server" Height="248px" Width="400px"
                                                    AutoLayout="False" ImageUrl="~/TempImages/GaugePic_#SEQ(300,3)" 
                                                    BackColor="White">
                                                    <Values>
                                                        <dgwc:InputValue Name="Default">
                                                        </dgwc:InputValue>
                                                    </Values>
                                                    <BackFrame FrameWidth="1" Style="simple" BackGradientEndColor="126, 126, 126" Shape="Rectangular"
                                                        FrameColor="Gray" BackColor="Gray" FrameGradientEndColor="Black" BackGradientType="TopBottom">
                                                    </BackFrame>
                                                    <CircularGauges>
                                                        <dgwc:CircularGauge Name="Big">
                                                            <Scales>
                                                                <dgwc:CircularScale SweepAngle="240" StartAngle="60" FillGradientType="TopBottom"
                                                                    Minimum="0" Interval="10" Maximum="60" Reversed="true" FillGradientEndColor="Gray"
                                                                    Width="0" FillColor="Transparent" BorderWidth="1" Name="Default">
                                                                    <LabelStyle TextColor="White"></LabelStyle>
                                                                    <MajorTickMark Width="4" Shape="Rectangle"></MajorTickMark>
                                                                </dgwc:CircularScale>
                                                            </Scales>
                                                            <BackFrame FrameWidth="10" Style="edged" BackGradientEndColor="126, 126, 126" Shape="Circular"
                                                                FrameColor="Gray" BackColor="Gray" FrameGradientEndColor="Black" BackGradientType="TopBottom">
                                                            </BackFrame>
                                                            <Size Height="100" Width="51"></Size>
                                                            <PivotPoint Y="50" X="50"></PivotPoint>
                                                            <Location Y="0" X="5"></Location>
                                                            <Pointers>
                                                                <dgwc:CircularPointer FillGradientEndColor="White" CapWidth="30" FillColor="Red"
                                                                    Name="Default"></dgwc:CircularPointer>
                                                            </Pointers>
                                                        </dgwc:CircularGauge>
                                                        <dgwc:CircularGauge Name="SmallTop">
                                                            <Scales>
                                                                <dgwc:CircularScale FillGradientType="TopBottom" FillGradientEndColor="Gray"
                                                                    Width="0" FillColor="Transparent" BorderWidth="1" SweepAngle="240"
                                                                    StartAngle="60" Name="Default">
                                                                    <LabelStyle TextColor="White"></LabelStyle>
                                                                    <MajorTickMark Width="4" Shape="Rectangle"></MajorTickMark>
                                                                </dgwc:CircularScale>
                                                            </Scales>
                                                            <BackFrame FrameWidth="10" Style="edged" BackGradientEndColor="126, 126, 126" Shape="Circular"
                                                                FrameColor="Gray" BackColor="Gray" FrameGradientEndColor="Black" BackGradientType="TopBottom">
                                                            </BackFrame>
                                                            <Size Height="50" Width="40"></Size>
                                                            <PivotPoint Y="50" X="50"></PivotPoint>
                                                            <Location Y="0" X="50"></Location>
                                                            <Pointers>
                                                                <dgwc:CircularPointer FillGradientEndColor="White" CapWidth="30" FillColor="Red"
                                                                    Name="Default"></dgwc:CircularPointer>
                                                                <dgwc:CircularPointer NeedleStyle="NeedleStyle2" FillGradientEndColor="White" CapWidth="30"
                                                                    FillColor="Green" Type="Marker" MarkerStyle="Trapezoid" Name="Average"
                                                                    Placement="Outside" ToolTip="Average"></dgwc:CircularPointer>
                                                            </Pointers>
                                                        </dgwc:CircularGauge>
                                                        <dgwc:CircularGauge Name="SmallBottom">
                                                            <Scales>
                                                                <dgwc:CircularScale FillGradientType="TopBottom"  FillGradientEndColor="Gray"
                                                                    Width="0" FillColor="Transparent" StartAngle="60" BorderWidth="1" SweepAngle="240"
                                                                    Name="Default">
                                                                    <LabelStyle TextColor="White"></LabelStyle>
                                                                    <MajorTickMark Width="4" Shape="Rectangle"></MajorTickMark>
                                                                </dgwc:CircularScale>
                                                            </Scales>
                                                            <BackFrame FrameWidth="10" Style="edged" BackGradientEndColor="126, 126, 126" Shape="Circular"
                                                                FrameColor="Gray" BackColor="Gray" FrameGradientEndColor="Black" BackGradientType="TopBottom">
                                                            </BackFrame>
                                                            <Size Height="50" Width="40"></Size>
                                                            <PivotPoint Y="50" X="50"></PivotPoint>
                                                            <Location Y="50" X="50"></Location>
                                                            <Pointers>
                                                                <dgwc:CircularPointer FillGradientEndColor="White" CapWidth="30"  FillColor="Red"
                                                                    Name="Default"></dgwc:CircularPointer>
                                                            </Pointers>
                                                        </dgwc:CircularGauge>
                                                    </CircularGauges>
                                                    <NumericIndicators>
                                                        <dgwc:NumericIndicator Style="digital7segment" FormatString="000.00" ShowDecimalPoint="True" BackColor="Silver"
                                                            BackGradientType="None" SeparatorColor="White"  DecimalColor="Black"
                                                            Parent="CircularGauges.Big" LedDimColor="177, 177, 177" ShadowOffset="1" Name="NIBig"
                                                            DigitColor="Black">
                                                            <Location Y="75" X="36"></Location>
                                                            <Size Height="12" Width="30"></Size>
                                                        </dgwc:NumericIndicator>
                                                    </NumericIndicators>
                                                    <Labels>
                                                        <dgwc:GaugeLabel Name="LBLBig" TextColor="Black" ResizeMode="None" Font="Trebuchet MS, 9pt"
                                                            BackGradientEndColor="" Parent="CircularGauges.Big" BackColor="" Text="Days Remaining"
                                                            TextAlignment="TopCenter">
                                                            <Size Height="12" Width="45"></Size>
                                                            <Location Y="68" X="28"></Location>
                                                        </dgwc:GaugeLabel>
                                                    </Labels>
                                                    <NumericIndicators>
                                                        <dgwc:NumericIndicator Style="digital7segment" FormatString="000.00" ShowDecimalPoint="True" BackColor="Silver"
                                                            BackGradientType="None" SeparatorColor="White" DecimalColor="Black"
                                                            Parent="CircularGauges.SmallTop" LedDimColor="177, 177, 177" ShadowOffset="1"
                                                            Name="MISmallTop" DigitColor="Black">
                                                            <Location Y="75" X="36"></Location>
                                                            <Size Height="12" Width="30"></Size>
                                                        </dgwc:NumericIndicator>
                                                    </NumericIndicators>
                                                    <Labels>
                                                        <dgwc:GaugeLabel Name="LBLSmallTop" TextColor="Black" ResizeMode="None" Font="Trebuchet MS, 6pt"
                                                            BackGradientEndColor="" Parent="CircularGauges.SmallTop" BackColor="" Text="Conductivity ms"
                                                            TextAlignment="TopCenter">
                                                            <Size Height="12" Width="55"></Size>
                                                            <Location Y="67" X="22"></Location>
                                                        </dgwc:GaugeLabel>
                                                    </Labels>
                                                    <NumericIndicators>
                                                        <dgwc:NumericIndicator Style="digital7segment" FormatString="000.00" ShowDecimalPoint="True" BackColor="Silver"
                                                            BackGradientType="None" SeparatorColor="White" DecimalColor="Black"
                                                            Parent="CircularGauges.SmallBottom" LedDimColor="177, 177, 177" ShadowOffset="1"
                                                            Name="NISmallBottom" DigitColor="Black">
                                                            <Location Y="75" X="36"></Location>
                                                            <Size Height="12" Width="30"></Size>
                                                        </dgwc:NumericIndicator>
                                                    </NumericIndicators>
                                                    <Labels>
                                                        <dgwc:GaugeLabel Name="LBLSmallBottom" TextColor="Black" ResizeMode="None" Font="Trebuchet MS, 6pt"
                                                            BackGradientEndColor="" Parent="CircularGauges.SmallBottom" BackColor="" Text="Temperature C"
                                                            TextAlignment="TopCenter">
                                                            <Size Height="12" Width="55"></Size>
                                                            <Location Y="67" X="22"></Location>
                                                        </dgwc:GaugeLabel>
                                                    </Labels>
                                                </dgwc:GaugeContainer>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

            <script language="javascript">
                if (screen.width <= 1024) {
                    document.getElementById("CHART_DIV_SCROLL1").style.height = "450px";
                }   

            </script>

            <asp:Literal ID="LblError" runat="server"></asp:Literal>
            <asp:Literal ID="LTRL_ZProcID" runat="server" Visible="false"></asp:Literal>
            <asp:Literal ID="LTRL_ZProcIDs" runat="server" Visible="false"></asp:Literal>
            <asp:Literal ID="LTRL_CPDeviceID" runat="server" Visible="false">0</asp:Literal>
            <asp:Literal ID="LTRL_JobID" runat="server" Visible="false">0</asp:Literal>
            <asp:Literal ID="LTRL_DongleID" runat="server" Visible="false">0</asp:Literal>
            <asp:Literal ID="LTRL_PageSize" runat="server" Visible="false">50</asp:Literal>
            <asp:Literal ID="LTRL_CurrentPage" runat="server" Visible="false">-1</asp:Literal>
            <asp:Literal ID="LTRL_StartIndex" runat="server" Visible="false">0</asp:Literal>
            <asp:Literal ID="LTRL_KeyTrend" runat="server" Visible="false">0</asp:Literal>
            <asp:Literal ID="LTRL_KeyJobSheetSummary" runat="server" Visible="false"></asp:Literal>

            <script language="javascript">
                function PageRedirect(Url, ZProcID) {
                    window.location.href = Url;
                }
            </script>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
