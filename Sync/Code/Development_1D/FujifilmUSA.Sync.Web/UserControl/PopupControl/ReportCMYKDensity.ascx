<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportCMYKDensity.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.PopupControl.ReportCMYKDensity" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<script language="javascript" type="text/javascript">
    function Close() {
        var oWindow = GetMeasurementRadWindow();
        GetMeasurementRadWindow().close();
        return false;
    }

    function GetMeasurementRadWindow() {
        var oWindow = null;
        if (window.radWindow) oWindow = window.radWindow;
        else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
        return oWindow;
    }


</script>
<script src="JS/ChartTooltip.js" type="text/javascript"></script>

<style type="text/css">
   
    .textwidth40
    {
        width: 40px;
    }
    .textwidth30
    {
        width: 30px;
    }
</style>

<div style="padding-left:5px;">


     <table cellpadding="0" cellspacing="10" width="100%">
        <tr>
            <td>
                <ucStatusbar:Statusbar ID="fujiStatusbar" runat="server" Visible="False" />
            </td>
        </tr>
       <tr>
        <td style="width: 48%;" valign="top">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                <tr>
                    <td class="green-title">
                        &nbsp; <asp:Localize Text="Cyan" runat="server" 
                            ID="locCMYKDensityCyan" meta:resourcekey="locCMYKDensityCyanResource1"></asp:Localize>
                    </td>
                </tr>
                <tr>
                    <td valign="top" style="padding: 2px">
                        <asp:Chart ID="Chart_CMYKDensity_C" runat="server" Width="320px"  Height="320px" BackGradientType="Center" 
                            BackGradientEndColor="White" BorderLineColor="#808080" EnableViewState="false" ImageType="Jpeg" ImageLocation="~/UploadFiles/temp/SingleMeasurement_Page1_DeltaChChart">
                            <Legends>
                                <asp:Legend Name="Default" Enabled="False">
                                </asp:Legend>
                            </Legends>
                            <BorderSkin SkinStyle="None"></BorderSkin>                                               
                            <Series>
                                <asp:Series Name="Series1" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                    Color="Blue" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                </asp:Series>                                                         
                                <asp:Series Name="Series1Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                </asp:Series>  
                                <asp:Series Name="Series2" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                    Color="Blue" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                </asp:Series>                                                         
                                <asp:Series Name="Series2Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                </asp:Series>  
                                <asp:Series  Name="Series3" MarkerSize="12" BackSecondaryColor="162, 164, 255" IsVisibleInLegend="false" Color="Red" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                                BorderWidth="2">
                                </asp:Series>  
                                <asp:Series Name="Series3Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                        Color="Red" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                            BackSecondaryColor="162, 164, 255">
                                </asp:Series>                                                       
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="Default" BorderColor="26, 59, 105" BackSecondaryColor="White"
                                    BackColor="Gray" BackGradientStyle="TopBottom" BorderDashStyle="Solid" ShadowOffset="1">
                                    <Position Height="99" Width="94" X="0" Y="2" />
                                    <AxisY IsLabelAutoFit="False" Title="ΔE" Interval="1" Minimum="0" Maximum="10">
                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                        <LabelStyle Angle="0" Font="Verdana, 6pt" />
                                    </AxisY>
                                    <AxisX IsLabelAutoFit="False" IsMarginVisible="False" Interval="0.1" Title="Density" Minimum="1" Maximum="2">
                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                        <LabelStyle Angle="0" Font="Verdana, 6pt" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 48%;" valign="top">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                <tr>
                    <td class="green-title">
                        &nbsp; <asp:Localize Text="Magenta" runat="server" 
                            ID="Localize1" meta:resourcekey="locCMYKDensityCyanResource1"></asp:Localize>
                    </td>
                </tr>
                <tr>
                    <td valign="top" style="padding: 2px">
                         <asp:Chart ID="Chart_CMYKDensity_M" runat="server" Width="320px"  Height="320px" BackGradientType="Center" 
                            BackGradientEndColor="White" BorderLineColor="#808080" EnableViewState="false" ImageType="Jpeg" ImageLocation="~/UploadFiles/temp/SingleMeasurement_Page1_DeltaChChart">
                            <Legends>
                                <asp:Legend Name="Default" Enabled="False">
                                </asp:Legend>
                            </Legends>
                            <BorderSkin SkinStyle="None"></BorderSkin>                                               
                            <Series>
                                <asp:Series Name="Series1" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                    Color="Blue" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                </asp:Series>                                                         
                                <asp:Series Name="Series1Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                </asp:Series>  
                                <asp:Series Name="Series2" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                    Color="Blue" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                </asp:Series>                                                         
                                <asp:Series Name="Series2Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                </asp:Series>  
                                <asp:Series  Name="Series3" MarkerSize="12" BackSecondaryColor="162, 164, 255" IsVisibleInLegend="false" Color="Red" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                                BorderWidth="2">
                                </asp:Series>  
                                <asp:Series Name="Series3Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                        Color="Red" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                            BackSecondaryColor="162, 164, 255">
                                </asp:Series>                                                       
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="Default" BorderColor="26, 59, 105" BackSecondaryColor="White"
                                    BackColor="Gray" BackGradientStyle="TopBottom" BorderDashStyle="Solid" ShadowOffset="1">
                                    <Position Height="99" Width="94" X="0" Y="2" />
                                    <AxisY IsLabelAutoFit="False" Title="ΔE" Interval="1" Minimum="0" Maximum="10">
                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                        <LabelStyle Angle="0" Font="Verdana, 6pt" />
                                    </AxisY>
                                    <AxisX IsLabelAutoFit="False" IsMarginVisible="False" Interval="0.1" Title="Density" Minimum="1" Maximum="2">
                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                        <LabelStyle Angle="0" Font="Verdana, 6pt" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                </tr>
            </table>
        </td>
       </tr>
       <tr>
        <td style="width: 48%;" valign="top">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                <tr>
                    <td class="green-title">
                        &nbsp; <asp:Localize Text="Yellow" runat="server" 
                            ID="Localize2" meta:resourcekey="locCMYKDensityCyanResource1"></asp:Localize>
                    </td>
                </tr>
                <tr>
                    <td valign="top" style="padding: 2px">
                         <asp:Chart ID="Chart_CMYKDensity_Y" runat="server" Width="320px"  Height="320px" BackGradientType="Center" 
                            BackGradientEndColor="White" BorderLineColor="#808080" EnableViewState="false" ImageType="Jpeg" ImageLocation="~/UploadFiles/temp/SingleMeasurement_Page1_DeltaChChart">
                            <Legends>
                                <asp:Legend Name="Default" Enabled="False">
                                </asp:Legend>
                            </Legends>
                            <BorderSkin SkinStyle="None"></BorderSkin>                                               
                            <Series>
                                <asp:Series Name="Series1" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                    Color="Blue" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                </asp:Series>                                                         
                                <asp:Series Name="Series1Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                </asp:Series>  
                                <asp:Series Name="Series2" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                    Color="Blue" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                </asp:Series>                                                         
                                <asp:Series Name="Series2Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                </asp:Series>  
                                <asp:Series  Name="Series3" MarkerSize="12" BackSecondaryColor="162, 164, 255" IsVisibleInLegend="false" Color="Red" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                                BorderWidth="2">
                                </asp:Series>  
                                <asp:Series Name="Series3Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                        Color="Red" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                            BackSecondaryColor="162, 164, 255">
                                </asp:Series>                                                       
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="Default" BorderColor="26, 59, 105" BackSecondaryColor="White"
                                    BackColor="Gray" BackGradientStyle="TopBottom" BorderDashStyle="Solid" ShadowOffset="1">
                                    <Position Height="99" Width="94" X="0" Y="2" />
                                    <AxisY IsLabelAutoFit="False" Title="ΔE" Interval="1" Minimum="0" Maximum="10">
                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                        <LabelStyle Angle="0" Font="Verdana, 6pt" />
                                    </AxisY>
                                    <AxisX IsLabelAutoFit="False" IsMarginVisible="False" Interval="0.1" Title="Density" Minimum="1" Maximum="2">
                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                        <LabelStyle Angle="0" Font="Verdana, 6pt" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 48%;" valign="top">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                <tr>
                    <td class="green-title">
                        &nbsp; <asp:Localize Text="Black" runat="server" 
                            ID="Localize3" meta:resourcekey="locCMYKDensityCyanResource1"></asp:Localize>
                    </td>
                </tr>
                <tr>
                    <td valign="top" style="padding: 2px">
                         <asp:Chart ID="Chart_CMYKDensity_K" runat="server" Width="320px"  Height="320px" BackGradientType="Center" 
                            BackGradientEndColor="White" BorderLineColor="#808080" EnableViewState="false" ImageType="Jpeg" ImageLocation="~/UploadFiles/temp/SingleMeasurement_Page1_DeltaChChart">
                            <Legends>
                                <asp:Legend Name="Default" Enabled="False">
                                </asp:Legend>
                            </Legends>
                            <BorderSkin SkinStyle="None"></BorderSkin>                                               
                            <Series>
                                <asp:Series Name="Series1" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                    Color="Blue" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                </asp:Series>                                                         
                                <asp:Series Name="Series1Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                </asp:Series>  
                                <asp:Series Name="Series2" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                    Color="Blue" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                </asp:Series>                                                         
                                <asp:Series Name="Series2Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                </asp:Series>  
                                <asp:Series  Name="Series3" MarkerSize="12" BackSecondaryColor="162, 164, 255" IsVisibleInLegend="false" Color="Red" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                                BorderWidth="2">
                                </asp:Series>  
                                <asp:Series Name="Series3Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                        Color="Red" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                            BackSecondaryColor="162, 164, 255">
                                </asp:Series>                                                       
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="Default" BorderColor="26, 59, 105" BackSecondaryColor="White"
                                    BackColor="Gray" BackGradientStyle="TopBottom" BorderDashStyle="Solid" ShadowOffset="1">
                                    <Position Height="99" Width="94" X="0" Y="2" />
                                    <AxisY IsLabelAutoFit="False" Title="ΔE" Interval="1" Minimum="0" Maximum="10">
                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                        <LabelStyle Angle="0" Font="Verdana, 6pt" />
                                    </AxisY>
                                    <AxisX IsLabelAutoFit="False" IsMarginVisible="False" Interval="0.1" Title="Density" Minimum="1" Maximum="2">
                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                        <LabelStyle Angle="0" Font="Verdana, 6pt" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                </tr>
            </table>
        </td>
       </tr>
        <tr>
            <td></td>
            <td>
                <div class="buttonright" style="padding-left: 3px">
                    <asp:LinkButton ID="lbtnClose" runat="server" OnClientClick="javascript: Close();"
                        CssClass="gray_bt">
                                        <span> <asp:Localize runat="server" ID="locCancel" 
                        Text="Cancel" meta:resourcekey="locCancelResource1" ></asp:Localize> 
                                
 
                                </span>
                    </asp:LinkButton>
                </div>
             
                <div class="clear">
                </div>
            </td>
        </tr>
    </table>

</div>
<div class="FadingTooltip" id="FADINGTOOLTIP" style="z-index: 999; visibility: hidden;
    position: absolute">
</div>
<div style="visibility:hidden;">
    <asp:TextBox ID="txt_ProjectID" runat="server"></asp:TextBox>
    <asp:TextBox ID="txt_OptimizeRunID" runat="server"></asp:TextBox>
    <asp:TextBox ID="txt_MeasurementID" runat="server"></asp:TextBox>
</div>