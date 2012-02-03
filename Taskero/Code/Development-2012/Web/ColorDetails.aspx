<%@ Page Language="C#" MasterPageFile="~/MasterDeviceTrendLine.master" AutoEventWireup="true" EnableEventValidation="false"
    CodeFile="ColorDetails.aspx.cs" Inherits="ColorDetails" %>
<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>
<%@ Register Assembly="DundasWebGauge" Namespace="Dundas.Gauges.WebControl" TagPrefix="DGWC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
    function Pageopen( CPMeasurementGUID )
    {
        window.open( "DataHeaderColor.aspx?CPMeasurementGUID="+ CPMeasurementGUID , 'DataHeaderColor' , 'width=950px,height=700px,resizable=1,scrollbars=1' );
    }
    </script>
    <table cellpadding="2" cellspacing="0" border="0" class="popup-table" style="margin-top:5px">
        <tr class="popup-top-bar">
            <td style="width: 33%">
                &nbsp;&nbsp;<asp:Button ID="BTN_Header" runat="server" CssClass="popup-button" Text="Measurement File" />
                &nbsp;&nbsp;<asp:Button ID="btnDownloadFile" runat="server" CssClass="popup-button" Text="Download File" OnClick="BTN_DownloadFile_Click" />
            </td>
            <td style="width: 33%">
                &nbsp;
            </td>
            <td style="width: 33%" align="right">
                <asp:Button ID="BTN_DeleteJob" runat="server" CssClass="popup-button" Text="Discard CPV Measurement"
                    OnClick="BTN_DeleteJob_Click" />&nbsp;&nbsp;
            </td>
        </tr>
        <tr class="popup-table-td" valign="top">
            <td style="padding: 5px">
                <div class="green-title">
                    Job Information</div>
                <table cellpadding="0" cellspacing="0" class="DATAGRID" border="0" style="width: 100%">
                    <tr class="ITEMSTYLE">
                        <td align="right" style="width: 50%">
                            <b>OUTPUT DEVICE&nbsp;&nbsp;</b>
                        </td>
                        <td style="width: 50%">
                            &nbsp;&nbsp;
                            <asp:Literal ID="LTRL_OutPutDevice" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr class="ALTERNATINGITEMSTYLE">
                        <td align="right">
                            <b>PRINT TARGET&nbsp;&nbsp;</b>
                        </td>
                        <td>
                            &nbsp;&nbsp;<asp:Literal ID="LTRL_PrintTarget" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr class="ITEMSTYLE">
                        <td align="right">
                            <b>INKS&nbsp;&nbsp;</b>
                        </td>
                        <td>
                            &nbsp;&nbsp;<asp:Literal ID="LTRL_Inks" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr class="ALTERNATINGITEMSTYLE">
                        <td align="right">
                            <b>SUBSTRATE&nbsp;&nbsp;</b>
                        </td>
                        <td>
                            &nbsp;&nbsp;<asp:Literal ID="LTRL_Substrate" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr class="ITEMSTYLE">
                        <td align="right">
                            <b>SPECTRO&nbsp;&nbsp;</b>
                        </td>
                        <td>
                            &nbsp;&nbsp;<asp:Literal ID="LTRL_Spectro" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr class="ALTERNATINGITEMSTYLE">
                        <td align="right">
                            <b>USER&nbsp;&nbsp;</b>
                        </td>
                        <td>
                            &nbsp;&nbsp;<asp:Literal ID="LTRL_User" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr class="ITEMSTYLE">
                        <td align="right">
                            <b>CUSTOMER&nbsp;&nbsp;</b>
                        </td>
                        <td>
                            &nbsp;&nbsp;<asp:Literal ID="LTRL_Customer" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr class="ALTERNATINGITEMSTYLE">
                        <td align="right">
                            <b>INFORMATION&nbsp;&nbsp;</b>
                        </td>
                        <td>
                            &nbsp;&nbsp;<asp:Literal ID="LTRL_Information" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr class="ITEMSTYLE">
                        <td align="right">
                            <b>PRINTER USER&nbsp;&nbsp;</b>
                        </td>
                        <td>
                            &nbsp;&nbsp;<asp:Literal ID="LTRL_PrinterUser" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr class="ALTERNATINGITEMSTYLE">
                        <td align="right">
                            <b>PRINTER HOST&nbsp;&nbsp;</b>
                        </td>
                        <td>
                            &nbsp;&nbsp;<asp:Literal ID="LTRL_PrinterHost" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr class="ITEMSTYLE">
                        <td align="right">
                            <b>OUTPUT TIME&nbsp;&nbsp;</b>
                        </td>
                        <td>
                            &nbsp;&nbsp;<asp:Literal ID="LTRL_OutputTime" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr class="ALTERNATINGITEMSTYLE">
                        <td align="right">
                            <b>PRINTER PAPER&nbsp;&nbsp;</b>
                        </td>
                        <td>
                            &nbsp;&nbsp;<asp:Literal ID="LTRL_PrinterPaper" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr class="ITEMSTYLE">
                        <td align="right">
                            <b>PRINTER CC TABLE&nbsp;&nbsp;</b>
                        </td>
                        <td>
                            &nbsp;&nbsp;<asp:Literal ID="LTRL_PrinterCCTable" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr class="ALTERNATINGITEMSTYLE">
                        <td align="right">
                            <b>PRINTER TARG. ICC&nbsp;&nbsp;</b>
                        </td>
                        <td>
                            &nbsp;&nbsp;<asp:Literal ID="LTRL_PrinterTargetICC" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="padding: 5px" valign="top">
                <div id="DivInkLevelCHRT" runat="server" visible="false"  style="margin-bottom:5px">
                        <div class="green-title">
                            Device Information</div>
                        <table cellpadding="0" cellspacing="3" class="TABLE1" border="0">
                            <tr>
                                <td>
                                    <DCWC:Chart ID="CHRT_InkLevel" runat="server" Width="300px" Height="270px" ImageUrl="~/UploadFiles/temp/CHRT_InkLevel_#SEQ(300,3)"
                                        ImageType="Jpeg" BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#DFDFDF"
                                        BorderLineColor="#808080">
                                        <Legends>
                                            <DCWC:Legend Name="Default">
                                            </DCWC:Legend>
                                        </Legends>
                                        <Titles>
                                            <DCWC:Title Text="Ink Level">
                                            </DCWC:Title>
                                        </Titles>
                                        <Series>
                                            <DCWC:Series Name="Series1" BorderColor="26, 59, 105" BorderWidth="0" Font="Microsoft Sans Serif, 7pt, style=Underline"
                                                Label="" CustomAttributes="DrawingStyle=Cylinder">
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
                                            <DCWC:ChartArea Name="ChartArea1" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                                BackColor="White" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                                <Area3DStyle YAngle="10" XAngle="10" RightAngleAxes="False" WallWidth="0" Clustered="True">
                                                </Area3DStyle>
                                                <AxisX Interval="1" Enabled="true" LabelsAutoFit="False">
                                                    <MajorGrid LineColor="Beige"></MajorGrid>
                                                    <LabelStyle Font="Microsoft Sans Serif, 7pt" FontAngle="45" />
                                                </AxisX>
                                                <AxisY Enabled="True" Interval="10" LabelsAutoFit="False" Maximum="100">
                                                    <MajorGrid LineColor="Beige"></MajorGrid>
                                                    <LabelStyle Format="##0.0" Font="Microsoft Sans Serif, 7pt" />
                                                </AxisY>
                                            </DCWC:ChartArea>
                                        </ChartAreas>
                                    </DCWC:Chart>
                                </td>
                            </tr>
                        </table>
                    </div>
                <div id="DivTempCHART" runat="server" visible="false">
                        <div class="green-title">
                            Environmental Information</div>
                        <table border="0" cellpadding="0" cellspacing="0" class="DATAGRID" style="width: 100%">
                            <tr class="HEADERSTYLE">
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    Trg
                                </td>
                                <td>
                                    ACT
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    Trg
                                </td>
                                <td>
                                    ACT
                                </td>
                            </tr>
                            <tr class="ITEMSTYLE">
                                <td>
                                    Temp
                                </td>
                                <td>
                                    <asp:Literal ID="LTRL_Temp_TRG" runat="server">00.0</asp:Literal>&deg;F
                                </td>
                                <td>
                                    <asp:Literal ID="LTRL_Temp_ACT" runat="server">00.0</asp:Literal>&deg;F
                                </td>
                                <td>
                                    RH
                                </td>
                                <td>
                                    <asp:Literal ID="LTRL_RH_TRG" runat="server">00.0</asp:Literal>%
                                </td>
                                <td>
                                    <asp:Literal ID="LTRL_RH_ACT" runat="server">00.0</asp:Literal>%
                                </td>
                            </tr>
                            <tr class="ITEMSTYLE">
                                <td colspan="3" valign="middle" align="center">
                                    <DGWC:GaugeContainer ID="CHRT_Temp" ImageUrl="~/UploadFiles/temp/CHRT_Temp_#SEQ(300,3)"
                                        AutoLayout="False" runat="server" Width="100px" Height="200px" ShadowIntensity="20"
                                        BackColor="White">
                                        <Labels>
                                            <DGWC:GaugeLabel Name="Label1" TextColor="White" BackGradientEndColor="" TextShadowOffset="1"
                                                Parent="LinearGauges.Default" BackColor="" Text="&#176;F" Font="Microsoft Sans Serif, 12pt">
                                                <Size Height="7" Width="22"></Size>
                                                <Location Y="88.2" X="69"></Location>
                                            </DGWC:GaugeLabel>
                                            <DGWC:GaugeLabel Name="Label2" TextColor="White" BackGradientEndColor="" TextShadowOffset="1"
                                                Parent="LinearGauges.Default" BackColor="" Text="&#176;C" Font="Microsoft Sans Serif, 12pt">
                                                <Size Height="7" Width="22"></Size>
                                                <Location Y="88.2" X="12"></Location>
                                            </DGWC:GaugeLabel>
                                        </Labels>
                                        <BackFrame FrameStyle="None" FrameShape="Rectangular"></BackFrame>
                                        <Values>
                                            <DGWC:InputValue Name="Default">
                                            </DGWC:InputValue>
                                        </Values>
                                        <LinearGauges>
                                            <DGWC:LinearGauge Name="Default">
                                                <Scales>
                                                    <DGWC:LinearScale StartMargin="9" Interval="10" FillGradientType="TopBottom" Maximum="40"
                                                        FillGradientEndColor="Black" Minimum="-30" Width="0" FillColor="Transparent"
                                                        BorderWidth="1" Name="ScaleC" Position="45">
                                                        <LabelStyle Font="Microsoft Sans Serif, 12pt" TextColor="White"></LabelStyle>
                                                        <MajorTickMark Width="2" BorderWidth="0" FillColor="White" Length="7" Placement="Inside">
                                                        </MajorTickMark>
                                                        <MinorTickMark Width="1.7" BorderWidth="0" FillColor="White" Length="4" Placement="Inside">
                                                        </MinorTickMark>
                                                    </DGWC:LinearScale>
                                                    <DGWC:LinearScale StartMargin="9" Interval="20" FillGradientType="TopBottom" Maximum="104"
                                                        FillGradientEndColor="Black" Minimum="-22" Width="0" FillColor="Transparent"
                                                        BorderWidth="1" Name="ScaleF" IntervalOffset="2" Position="55">
                                                        <LabelStyle Font="Microsoft Sans Serif, 12pt" Placement="Outside" TextColor="White"
                                                            ShowEndLabels="False"></LabelStyle>
                                                        <MajorTickMark Width="2" BorderWidth="0" FillColor="White" Length="7" Placement="Outside">
                                                        </MajorTickMark>
                                                        <MinorTickMark Width="1.7" BorderWidth="0" FillColor="White" Length="4" Placement="Outside">
                                                        </MinorTickMark>
                                                    </DGWC:LinearScale>
                                                </Scales>
                                                <BackFrame FrameWidth="10" Style="edged" Shape="RoundedRectangular" FrameGradientType="HorizontalCenter"
                                                    FrameColor="#808080" FrameGradientEndColor="#505050" BackGradientType="HorizontalCenter"
                                                    BackColor="#808080" BackGradientEndColor="#808080"></BackFrame>
                                                <Size Height="100" Width="100"></Size>
                                                <Location Y="0" X="0"></Location>
                                                <Pointers>
                                                    <DGWC:LinearPointer ThermometerBackGradientEndColor="" FillGradientType="LeftRight"
                                                        ThermometerBackColor="White" FillGradientEndColor="DarkRed" Placement="Inside"
                                                        Width="10" Value="60" FillColor="Red" Type="Thermometer" ThermometerBulbOffset="10"
                                                        ScaleName="ScaleF" ThermometerBulbSize="25" ShadowOffset="1" Name="Default">
                                                    </DGWC:LinearPointer>
                                                </Pointers>
                                            </DGWC:LinearGauge>
                                        </LinearGauges>
                                    </DGWC:GaugeContainer>
                                </td>
                                <td colspan="3" valign="middle" align="center">
                                    <DGWC:GaugeContainer ID="CHRT_RH" ImageUrl="~/UploadFiles/temp/CHRT_RH_#SEQ(300,3)"
                                        AutoLayout="False" runat="server" Width="100px" Height="200px" ShadowIntensity="20"
                                        BackColor="White">
                                        <Labels>
                                            <DGWC:GaugeLabel Name="Label1" TextColor="White" BackGradientEndColor="" TextShadowOffset="1"
                                                Parent="LinearGauges.Default" BackColor="" Text="RH" Font="Microsoft Sans Serif">
                                                <Size Height="7" Width="50"></Size>
                                                <Location Y="90" X="40"></Location>
                                            </DGWC:GaugeLabel>
                                        </Labels>
                                        <BackFrame FrameStyle="None" FrameShape="Rectangular"></BackFrame>
                                        <Values>
                                            <DGWC:InputValue Name="Default">
                                            </DGWC:InputValue>
                                        </Values>
                                        <LinearGauges>
                                            <DGWC:LinearGauge Name="Default">
                                                <Scales>
                                                    <DGWC:LinearScale StartMargin="9" Interval="10" FillGradientType="TopBottom" Maximum="100"
                                                        FillGradientEndColor="Black" Minimum="0" Width="0" FillColor="Transparent" BorderWidth="1"
                                                        Name="ScaleC" Position="45">
                                                        <LabelStyle Font="Microsoft Sans Serif, 12pt" TextColor="White"></LabelStyle>
                                                        <MajorTickMark Width="2" BorderWidth="0" FillColor="White" Length="7" Placement="Inside">
                                                        </MajorTickMark>
                                                        <MinorTickMark Width="1.7" BorderWidth="0" FillColor="White" Length="4" Placement="Inside">
                                                        </MinorTickMark>
                                                    </DGWC:LinearScale>
                                                    <DGWC:LinearScale StartMargin="9" Interval="10" FillGradientType="TopBottom" Maximum="100"
                                                        FillGradientEndColor="Black" Minimum="0" Width="0" FillColor="Transparent" BorderWidth="1"
                                                        Name="ScaleF" Position="55">
                                                        <LabelStyle Font="Microsoft Sans Serif, 12pt" Placement="Outside" TextColor="White">
                                                        </LabelStyle>
                                                        <MajorTickMark Width="2" BorderWidth="0" FillColor="White" Length="7" Placement="Outside">
                                                        </MajorTickMark>
                                                        <MinorTickMark Width="1.7" BorderWidth="0" FillColor="White" Length="4" Placement="Outside">
                                                        </MinorTickMark>
                                                    </DGWC:LinearScale>
                                                </Scales>
                                                <BackFrame FrameWidth="10" Style="edged" Shape="RoundedRectangular" FrameGradientType="VerticalCenter"
                                                    FrameColor="#808080" FrameGradientEndColor="#505050" BackGradientType="VerticalCenter"
                                                    BackColor="#808080" BackGradientEndColor="#808080"></BackFrame>
                                                <Size Height="100" Width="100"></Size>
                                                <Location Y="0" X="0"></Location>
                                                <Pointers>
                                                    <DGWC:LinearPointer ThermometerBackGradientEndColor="" FillGradientType="LeftRight"
                                                        ThermometerBackColor="White" FillGradientEndColor="DarkRed" Placement="Inside"
                                                        Width="10" Value="60" FillColor="Red" Type="Thermometer" ThermometerBulbOffset="0"
                                                        ScaleName="ScaleF" ThermometerBulbSize="10" ShadowOffset="0" Name="Default">
                                                    </DGWC:LinearPointer>
                                                </Pointers>
                                            </DGWC:LinearGauge>
                                        </LinearGauges>
                                    </DGWC:GaugeContainer>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="DivJobPreview" runat="server" visible="false"><asp:image ID="Image1" width="300" Height="175" runat="server"/> </div>
            </td>
            <td style="padding: 5px" valign="top">
                <div class="green-title">
                    Delta</div>
                <table class="DATAGRID" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr class="HEADERSTYLE">
                        <td style="width: 40%">
                            Metric
                        </td>
                        <td style="width: 15%">
                            Thrsh
                        </td>
                        <td style="width: 15%">
                            Actual
                        </td>
                        <td style="width: 15%">
                            STD
                        </td>
                    </tr>
                    <tr class="ALTERNATINGITEMSTYLE">
                        <td>
                            <b>Ave DE</b>
                        </td>
                        <td>
                            <asp:Literal ID="LTRL_Thrsh_AveDe" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="LTRL_Actual_AveDe" runat="server"></asp:Literal>
                        </td>
                        <td>
                            
                        </td>
                    </tr>
                    <tr class="ITEMSTYLE">
                        <td>
                            <b>Max DE</b>
                        </td>
                        <td>
                            <asp:Literal ID="LTRL_Thrsh_MaxDe" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="LTRL_Actual_MaxDe" runat="server"></asp:Literal>
                        </td>
                        <td>
                            
                        </td>
                    </tr>
                    <tr class="ALTERNATINGITEMSTYLE">
                        <td>
                            <b>Cyan DE</b>
                        </td>
                        <td>
                            <asp:Literal ID="LTRL_Thrsh_CyanDe" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="LTRL_Actual_CyanDe" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="LTRL_Std_CyanDe" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr class="ITEMSTYLE">
                        <td>
                            <b>Mag. DE</b>
                        </td>
                        <td>
                            <asp:Literal ID="LTRL_Thrsh_MagDe" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="LTRL_Actual_MagDe" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="LTRL_Std_MagDe" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr class="ALTERNATINGITEMSTYLE">
                        <td>
                            <b>Yell. DE</b>
                        </td>
                        <td>
                            <asp:Literal ID="LTRL_Thrsh_YellDe" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="LTRL_Actual_YellDe" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="LTRL_Std_YellDe" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr class="ITEMSTYLE">
                        <td>
                            <b>Black DE</b>
                        </td>
                        <td>
                            <asp:Literal ID="LTRL_Thrsh_BlackDe" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="LTRL_Actual_BlackDe" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="LTRL_Std_BlackDe" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr class="ALTERNATINGITEMSTYLE">
                        <td>
                            <b>Paper DE</b>
                        </td>
                        <td>
                            <asp:Literal ID="LTRL_Thrsh_PaperDe" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="LTRL_Actual_PaperDe" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="LTRL_Std_PaperDe" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr class="ITEMSTYLE">
                        <td>
                            <b>Cyan DH</b>
                        </td>
                        <td>
                            <asp:Literal ID="LTRL_Thrsh_CyanDH" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="LTRL_Actual_CyanDH" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="LTRL_Std_CyanDH" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr class="ALTERNATINGITEMSTYLE">
                        <td>
                            <b>Mag. DH</b>
                        </td>
                        <td>
                            <asp:Literal ID="LTRL_Thrsh_MagDH" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="LTRL_Actual_MagDH" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="LTRL_Std_MagDH" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr class="ITEMSTYLE">
                        <td>
                            <b>Yell. DH</b>
                        </td>
                        <td>
                            <asp:Literal ID="LTRL_Thrsh_YellDH" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="LTRL_Actual_YellDH" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="LTRL_Std_YellDH" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr class="ALTERNATINGITEMSTYLE">
                        <td>
                            <b>Black DH</b>
                        </td>
                        <td>
                            <asp:Literal ID="LTRL_Thrsh_BlackDH" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="LTRL_Actual_BlackDH" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="LTRL_Std_BlackDH" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr class="ITEMSTYLE">
                        <td>
                            <b>Gray Ave DH</b>
                        </td>
                        <td>
                            <asp:Literal ID="LTRL_Thrsh_GrayAveDH" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="LTRL_Actual_GrayAveDH" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="LTRL_Std_GrayAveDH" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>    
    <asp:Literal ID="LblError" runat="server"></asp:Literal>
    <asp:Literal ID="LTRL_CPMeasurementGUID" runat="server" Visible="false"></asp:Literal>
    <asp:Literal ID="LTRL_CPDeviceID" runat="server" Visible="false"></asp:Literal>
    <asp:Literal ID="LTRL_CPDevicePrintTargetID" runat="server" Visible="false"></asp:Literal>
    <asp:Literal ID="LTRL_KeyDetails" runat="server" Visible="false"></asp:Literal>
</asp:Content>
