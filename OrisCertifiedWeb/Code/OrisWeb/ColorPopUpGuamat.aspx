<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ColorPopUpGuamat.aspx.cs"  EnableEventValidation="false" Inherits="OrisWeb.ColorPopUpGuamat" %>



<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<link href="CSS/Default.css" rel="stylesheet" type="text/css" />
<script src="JS/CommonFunctions.js" type="text/javascript"></script>
<head id="Head1" runat="server">
    <title>Gamut</title>
    <script type="text/javascript">
        function PrintChart()
        {
            document.getElementById("DIV1").style.display = "none";
            window.print();
            document.getElementById("DIV1").style.display = "";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" style="float:inherit;width:100%" class="HEADERSTYLE" id="DIV1">
        <img id="ImgPrint" src="Images/Icon-Printer.gif" style="cursor:pointer" onclick="PrintChart();" />
    </div>
        <div id="divGamutImg" align="center" style="float:inherit;width:100%; padding:0px;" class="alternate-td-dark">
        <DCWC:Chart ID="CHART_GAMUT" runat="server" BackImageMode="Scaled" BackImage="~/Images/FarbKreis_LAB2_150.gif"
            Height="300px" Width="300px" ImageUrl="~/UploadFiles/temp/CHRT_GAMUT_#SEQ(300,3)">
            <Legends>
                <DCWC:Legend Name="Default" Enabled="False">
                </DCWC:Legend>
            </Legends>
            <Series>
                <DCWC:Series 
                    ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker" 
                    MarkerSize="8" MarkerStyle="Circle" 
                    MarkerColor="Red" Color="Red" 
                    Name="Series1" ShadowOffset="1">
                </DCWC:Series>
                <DCWC:Series 
                    ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker" 
                    MarkerSize="5" MarkerStyle="Circle" 
                    MarkerColor="Green" Color="Green" 
                    Name="Series2" ShadowOffset="1">
                </DCWC:Series>
                <DCWC:Series ChartType="FastPoint" CustomAttributes="PolarDrawingStyle=Marker" 
                    MarkerSize="8" MarkerStyle="Circle" 
                    MarkerColor="Red" Color="Green" 
                    Name="Series3" ShadowOffset="1">
                </DCWC:Series>
                <DCWC:Series ChartType="FastPoint" CustomAttributes="PolarDrawingStyle=Marker" 
                    MarkerSize="5" MarkerStyle="Circle" 
                    MarkerColor="Green" Color="Green" 
                    Name="Series4" ShadowOffset="1">
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
    </div>
    <asp:Label ID="LblError" runat="server" CssClass="ERROR"></asp:Label>
    <asp:Literal ID="LTRL_KeyColorChars" runat="server" Visible="false">0</asp:Literal>
    </form>
</body>
</html>
