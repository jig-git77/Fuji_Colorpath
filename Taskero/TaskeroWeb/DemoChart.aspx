<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DemoChart.aspx.cs" EnableEventValidation="false" Inherits="TaskeroWeb.DemoChart" %>


<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Dundas Sample Demo</title>    
</head>
<body>
    <form id="form1" runat="server">
    <DCWC:Chart ID="CHRT_ZoneDeltaE" runat="server" Width="890px" Height="300px" BackGradientEndColor="#EFEFEF"
        BorderLineStyle="Solid" BackGradientType="DiagonalLeft" ImageUrl="~/UploadFiles/temp/CHRT_TrendDeltaE_#SEQ(300,3)">
        <Legends>
            <DCWC:Legend Name="Default">
            </DCWC:Legend>
            <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom">
            </DCWC:Legend>
        </Legends>
        <Series>
            <DCWC:Series Name="Series0" Color="200, 0, 0, 0" BorderColor="100, 0 , 0 , 0" BackGradientType="VerticalCenter"
                ChartArea="ChartArea0" BorderWidth="1" ShowInLegend="false">
            </DCWC:Series>
        </Series>
        <ChartAreas>
            <DCWC:ChartArea Name="ChartArea0" BorderColor="26, 59, 105" BackGradientEndColor="White"
                BackColor="White" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1"
                AlignWithChartArea="ChartArea1">
                <Area3DStyle YAngle="75" XAngle="10" RightAngleAxes="False" WallWidth="0"></Area3DStyle>
                <Position Height="75" Width="100" X="0" Y="25" />
                <AxisY Enabled="True">
                    <MajorGrid LineColor="Beige" Interval="1" />
                    <SubAxes>
                        <DCWC:SubAxis Title="Delta E">
                        </DCWC:SubAxis>
                    </SubAxes>
                </AxisY>
                <AxisX Enabled="True" Interval="1">
                    <MajorGrid LineColor="Beige" />
                    <SubAxes>
                        <DCWC:SubAxis Title="Zone Number">
                        </DCWC:SubAxis>
                    </SubAxes>
                </AxisX>
            </DCWC:ChartArea>
            <DCWC:ChartArea Name="ChartArea1" BorderColor="26, 59, 105" BackGradientEndColor="White"
                BackColor="White" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1"
                AlignWithChartArea="ChartArea0">
                <Area3DStyle YAngle="10" XAngle="10" RightAngleAxes="False" WallWidth="0" Clustered="True">
                </Area3DStyle>
                <Position Height="30" Width="100" X="0" Y="0" />
                <AxisY Enabled="True" Minimum="0" Maximum="2" Interval="1">
                    <MajorGrid LineColor="Beige" />
                    <SubAxes>
                        <DCWC:SubAxis Title=" ">
                        </DCWC:SubAxis>
                    </SubAxes>
                </AxisY>
                <AxisX Enabled="True" Interval="1">
                    <MajorGrid LineColor="Beige" />
                    <SubAxes>
                        <DCWC:SubAxis Title="">
                        </DCWC:SubAxis>
                    </SubAxes>
                </AxisX>
            </DCWC:ChartArea>
        </ChartAreas>
    </DCWC:Chart>    
    <asp:Literal ID="LblError" runat="server"></asp:Literal>
    <asp:Literal ID="LTRL_KeyJobZoneSummary" runat="server" Visible="False"></asp:Literal>
    <asp:Literal ID="LTRL_DIVs" runat="server" Visible="false"></asp:Literal>
    <asp:Literal ID="LTRL_Colors" runat="server" Visible="false"></asp:Literal>
    <asp:Literal ID="LTRL_IsEnabledImage" runat="server" Visible="false"></asp:Literal>
    <asp:Literal ID="LTRL_JobID" runat="server" Visible="false"></asp:Literal>
    <asp:Literal ID="LTRL_DongleID" runat="server" Visible="false"></asp:Literal>
    <asp:Literal ID="LTRL_CPDeviceID" runat="server" Visible="false"></asp:Literal>
    </form>
</body>
</html>
