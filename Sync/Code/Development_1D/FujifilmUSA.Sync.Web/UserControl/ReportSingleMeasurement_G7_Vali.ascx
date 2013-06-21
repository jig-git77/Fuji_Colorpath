<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportSingleMeasurement_G7_Vali.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.ReportSingleMeasurement_G7_Vali" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="uc" %>
<uc:Statusbar ID="fujiStatusbar" runat="server" />
<asp:ImageButton ID="btn_GeneratePDF" 
    ImageUrl="../Images/Sync DLP Report_preview.jpg" runat="server" 
    onclick="btn_GeneratePDF_Click" ToolTip="Download PDF" />
<br />

 <%------------------------ Report Generate Structure -------------------------%>
    <asp:Panel Style="width: 100%" ID="PNL_Result" runat="server" Visible="false">
        <div style="overflow: auto; height:400px;">
            <table style="width: 100%" cellspacing="0" cellpadding="0" align="center" border="0">
                <tr>
                    <td valign="top">
                        <asp:TextBox ID="txt_MeasurementID" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txt_dTE" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txt_DLPName" runat="server"></asp:TextBox>  
                        <asp:TextBox ID="txt_Vali_Type" runat="server"></asp:TextBox>
                        <br />
                        Target Versus Actual AB Plot
                        <br />
                        <asp:Chart ID="CHART_TargetVersusActualAB" runat="server" BackImageMode="Scaled"
                            BackImage="~/Images/SingleMeasurement_PointBack.gif" Height="234px" ImageType="Jpeg"
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
                        <asp:Chart ID="ChartResultSummary_Delta" runat="server" Width="150px" Height="250px" BackColor="Transparent"
                            ImageLocation="~/UploadFiles/temp/SingleMeasurement_Page1_ResultSummary_Delta" ImageType="Jpeg">
                             <BorderSkin BackSecondaryColor="White" BorderColor="Black" BorderWidth="2"
                                SkinStyle="Raised" BackColor="White"></BorderSkin> 
                        
                           <Series>
                                <asp:Series Name="Series0"  Color="#D4E8FA" ChartArea="ChartArea1" ChartType="Bar" IsVisibleInLegend="False" 
                                        BackGradientStyle="DiagonalRight" >                                              
                                </asp:Series>
                                <asp:Series Name="Series1" BorderWidth="0" BorderColor="Gray" Color="Gray" 
                                                            IsVisibleInLegend="False" ChartArea="ChartArea1" ChartType="Bar">
                                </asp:Series>    
                                 <asp:Series Name="Series2" BorderWidth="0"  Color="Transparent" 
                                                            IsVisibleInLegend="False" ChartArea="ChartArea1" ChartType="Bar" MarkerImage="~/Images/Chart-Red-Image-Vertical.png">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1" BackColor="White">
                                        <Area3DStyle  Enable3D="True" Inclination="0" Rotation="0" IsRightAngleAxes="true" IsClustered="false" >
                                        </Area3DStyle>
                                          
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
                        <asp:Chart ID="ChartResultSummary_DeltaEValue" runat="server" Width="150px" Height="260px" BackColor="Transparent"
                             ImageLocation="~/UploadFiles/temp/SingleMeasurement_Page1_ResultSummary_DeltaEValue"
                            ImageType="Jpeg">
                            <BorderSkin BackSecondaryColor="White" BorderColor="Black" BorderWidth="2"
                                SkinStyle="Raised" BackColor="White"></BorderSkin>                        
                            <Series>
                                <asp:Series Name="Series0"  Color="#D4E8FA" ChartArea="ChartArea1" ChartType="Bar" IsVisibleInLegend="true"  BackGradientStyle="DiagonalRight" >                                              
                                </asp:Series>
                                <asp:Series Name="Series1" BorderWidth="0" BorderColor="Gray" Color="Gray" 
                                                            IsVisibleInLegend="False" ChartArea="ChartArea1" ChartType="Bar">
                                </asp:Series> 
                                <asp:Series Name="Series2" BorderWidth="0"  Color="Transparent" 
                                                           IsVisibleInLegend="False" ChartArea="ChartArea1" ChartType="Bar" MarkerImage="~/Images/Chart-Red-Image-Vertical.png">
                                </asp:Series>    
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1" BackColor="White">
                                        <Area3DStyle  Enable3D="True" Inclination="0" Rotation="0" IsRightAngleAxes="true" IsClustered="false" >
                                        </Area3DStyle>
                                          
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
                        Gray Balance ΔCh
                        <br />
                        <asp:Chart ID="Chart_GrayF" runat="server" Width="300px"  Height="70px" BackGradientType="Center" 
                            BackGradientEndColor="White" BorderLineColor="#808080" EnableViewState="false" 
                            ImageLocation="~/UploadFiles/temp/SingleMeasurement_Page1_DeltaChChart" ImageType="Jpeg">
                            <Legends>
                                <asp:Legend Name="Default" Enabled="False">
                                </asp:Legend>
                            </Legends>
                            <BorderSkin SkinStyle="None"></BorderSkin>                                               
                            <Series>
                                <asp:Series Name="Series1" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                </asp:Series>                                                         
                                <asp:Series Name="Series1Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                </asp:Series>  
                                <asp:Series  Name="Series2" MarkerSize="12" BackSecondaryColor="162, 164, 255" IsVisibleInLegend="false" Color="Red" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                                BorderWidth="2">
                                </asp:Series>  
                                <asp:Series Name="Series2Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                        Color="Red" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                            BackSecondaryColor="162, 164, 255">
                                </asp:Series>                                                       
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="Default" BorderColor="26, 59, 105" BackSecondaryColor="White"
                                    BackColor="Gray" BackGradientStyle="TopBottom" BorderDashStyle="Solid" ShadowOffset="1">
                                    <Position Height="99" Width="94" X="0" Y="2" />
                                    <AxisY IsLabelAutoFit="False" Title="" TitleFont="Verdana, 8pt, style=Bold" Interval="2"
                                        Minimum="0" Maximum="6">
                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                        <LabelStyle Angle="0" Font="Verdana, 6pt" />
                                    </AxisY>
                                    <AxisX IsLabelAutoFit="False" IsMarginVisible="False" Interval="25" Title="" Minimum="0" Maximum="100">
                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                        <LabelStyle Angle="0" Font="Verdana, 6pt" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                </tr>    
                <tr>
                    <td>
                        Black ΔL
                        <br />
                        <asp:Chart ID="Chart_BlackL" runat="server" Width="300px"  Height="70px" BackGradientType="Center" 
                            BackGradientEndColor="White" BorderLineColor="#808080" EnableViewState="false"
                            ImageLocation="~/UploadFiles/temp/SingleMeasurement_Page1_BlackDeltaLChart" ImageType="Jpeg">
                            <Legends>
                                <asp:Legend Name="Default" Enabled="False">
                                </asp:Legend>
                            </Legends>
                            <BorderSkin SkinStyle="None"></BorderSkin>                                               
                            <Series>
                                <asp:Series Name="Series1" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                </asp:Series>                                                         
                                <asp:Series Name="Series1Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                </asp:Series>  
                                <asp:Series  Name="Series2" MarkerSize="12" BackSecondaryColor="162, 164, 255" IsVisibleInLegend="false" Color="Red" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                                BorderWidth="2">
                                </asp:Series>  
                                <asp:Series Name="Series2Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                        Color="Red" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                            BackSecondaryColor="162, 164, 255">
                                </asp:Series>                                                       
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="Default" BorderColor="26, 59, 105" BackSecondaryColor="White"
                                    BackColor="Gray" BackGradientStyle="TopBottom" BorderDashStyle="Solid" ShadowOffset="1">
                                    <Position Height="99" Width="94" X="0" Y="2" />
                                    <AxisY IsLabelAutoFit="False" Title="" TitleFont="Verdana, 8pt, style=Bold" Interval="2"
                                        Minimum="0" Maximum="6">
                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                        <LabelStyle Angle="0" Font="Verdana, 6pt" />
                                    </AxisY>
                                    <AxisX IsLabelAutoFit="False" IsMarginVisible="False" Interval="25" Title="" Minimum="0" Maximum="100">
                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                        <LabelStyle Angle="0" Font="Verdana, 6pt" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                </tr> 
                <tr>
                    <td>
                        Gray ΔL
                        <br />
                        <asp:Chart ID="Chart_GrayL" runat="server" Width="300px"  Height="70px" BackGradientType="Center" 
                            BackGradientEndColor="White" BorderLineColor="#808080" EnableViewState="false"
                            ImageLocation="~/UploadFiles/temp/SingleMeasurement_Page1_GrayDeltaLChart" ImageType="Jpeg">
                            <Legends>
                                <asp:Legend Name="Default" Enabled="False">
                                </asp:Legend>
                            </Legends>
                            <BorderSkin SkinStyle="None"></BorderSkin>                                               
                            <Series>
                                <asp:Series Name="Series1" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                </asp:Series>                                                         
                                <asp:Series Name="Series1Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                </asp:Series>  
                                <asp:Series  Name="Series2" MarkerSize="12" BackSecondaryColor="162, 164, 255" IsVisibleInLegend="false" Color="Red" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                                BorderWidth="2">
                                </asp:Series>  
                                <asp:Series Name="Series2Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                        Color="Red" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                            BackSecondaryColor="162, 164, 255">
                                </asp:Series>                                                       
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="Default" BorderColor="26, 59, 105" BackSecondaryColor="White"
                                    BackColor="Gray" BackGradientStyle="TopBottom" BorderDashStyle="Solid" ShadowOffset="1">
                                    <Position Height="99" Width="94" X="0" Y="2" />
                                    <AxisY IsLabelAutoFit="False" Title="" TitleFont="Verdana, 8pt, style=Bold" Interval="2"
                                        Minimum="0" Maximum="6">
                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                        <LabelStyle Angle="0" Font="Verdana, 6pt" />
                                    </AxisY>
                                    <AxisX IsLabelAutoFit="False" IsMarginVisible="False" Interval="25" Title="" Minimum="0" Maximum="100">
                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                        <LabelStyle Angle="0" Font="Verdana, 6pt" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                </tr> 
                <tr>
                    <td>
                        Gray Balance L*a*b*
                        <br />
                        <asp:Chart ID="chartL" runat="server" Width="300px" Height="70px" 
                            BackGradientType="Center" BackGradientEndColor="White" BorderLineColor="Gray" meta:resourcekey="chartLResource1"
                            ImageLocation="~/UploadFiles/temp/SingleMeasurement_Page1_LChart" ImageType="Jpeg">
                            <Titles>
                                <asp:Title Text="L*" Name="Title1">
                                    <Position X="94" Y="15" Auto="False" />
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
                                        <LabelStyle Font="Verdana, 6pt" ForeColor="Transparent" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                        <br />
                        <asp:Chart ID="chartA" runat="server" Width="300px" Height="70px" BackGradientType="Center" 
                            BackGradientEndColor="White" BorderLineColor="Gray" meta:resourcekey="chartAResource1"
                            ImageLocation="~/UploadFiles/temp/SingleMeasurement_Page1_AChart" ImageType="Jpeg">
                            <Legends>
                                <asp:Legend Name="Default" Enabled="False">
                                </asp:Legend>
                            </Legends>
                            <Titles>
                                <asp:Title Text="a*" Name="Title1">
                                    <Position X="94" Y="15" Auto="False" />
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
                                        <LabelStyle Font="Verdana, 6pt" ForeColor="Transparent" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                        <br />
                        <asp:Chart ID="chartB" runat="server" Width="300px" Height="70px" BackGradientType="Center" 
                            BackGradientEndColor="White" BorderLineColor="Gray" meta:resourcekey="chartBResource1"
                            ImageLocation="~/UploadFiles/temp/SingleMeasurement_Page1_BChart" ImageType="Jpeg">
                            <Legends>
                                <asp:Legend Name="Default" Enabled="False">
                                </asp:Legend>
                            </Legends>
                            <Titles>
                                <asp:Title Text="b*" Name="Title1">
                                    <Position X="94" Y="15" Auto="False" />
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
                        End of File
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />                       
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>