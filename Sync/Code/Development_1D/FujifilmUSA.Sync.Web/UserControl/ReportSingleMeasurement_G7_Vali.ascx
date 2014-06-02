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