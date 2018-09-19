<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddMetricTarget.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.AddMetricTarget" %>
<%@ Register Src="~/UserControl/AdminStatusbar.ascx" TagName="AdminStatusbar" TagPrefix="ucAdminStatusbar" %>
<%@ Register Src="~/UserControl/ValidationContainer.ascx" TagName="ValidationContainer" TagPrefix="ucValidationContainer" %>
<script src="JS/CommonFunctions.js?version=4" type="text/javascript"></script>
<div class="admin_top">
    &nbsp;
</div>
<div class="admin_middle" style="line-height: 34px;">
    <div class="head">
        <h1>
            <asp:Localize ID="locMetricTargetPreset" runat="server" Text="Metric Target Management"
                meta:resourcekey="locMetricTargetPresetResource1"></asp:Localize></h1>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px;">
        <center>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <center>
                        <fieldset style="width: 60%;">
                            <legend>
                                <asp:Label ID="lblHeader" runat="server" Text="Add Metric Target Preset"  meta:resourcekey="lblHeaderResource1">
                                  </asp:Label>
                            </legend>
                            <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" 
                                Visible="False" />
                            <table>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize Text="Preset Name" 
                                            Id="locPresetName" runat="server" 
                                            meta:resourcekey="locPresetNameResource1"></asp:Localize><span style="color: Red;">*</span> </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">                                            
                                            <asp:Label  runat="server" ID="lblPresetName"></asp:Label>
                                            <asp:HiddenField runat="server" ID="hdfTargetPresetID" />
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td colspan="3">
                                        <%--<asp:GridView runat="server" ID="grdMetricSum" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:BoundField DataField="MetricSumName" HeaderText="MetricSum Name" />
                                                <asp:TemplateField HeaderText="Target Value">
                                                    <ItemTemplate>
                                                        <asp:HiddenField runat="server" ID="hdfMetricSumId" Value='<%# Bind("MetricSumID") %>' />
                                                        <asp:HiddenField runat="server" ID="hdfTargetMetricId" Value='<%# Bind("MetricTargetId") %>' />
                                                        <asp:TextBox runat="server" ID="txtTargetValue" Width="30"  Text='<%# Bind("TargetValue") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>--%>
                                        <asp:GridView runat="server" ID="grdMetricSum" AutoGenerateColumns="false" ShowFooter="true" OnRowCreated="grdMetricSum_RowCreated">
                                            <Columns>
                                                <%--<asp:BoundField DataField="MetricSumName" HeaderText="MetricSum Name" />--%>
                                                <asp:TemplateField HeaderText="Target Value">
                                                    <ItemTemplate>                                                        
                                                        <asp:DropDownList ID="ddlMetricSum" runat="server" AppendDataBoundItems="true">  
                                                            <asp:ListItem Value="-1">Select</asp:ListItem>  
                                                        </asp:DropDownList>  
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Target Value">
                                                    <ItemTemplate>
                                                        <asp:HiddenField runat="server" ID="hdfMetricSumId" Value='<%# Bind("MetricSumID") %>' />
                                                        <asp:HiddenField runat="server" ID="hdfTargetMetricId" Value='<%# Bind("MetricTargetId") %>' />
                                                        <asp:TextBox runat="server" ID="txtTargetValue" Width="50"  Text='<%# Bind("TargetValue") %>'  />
                                                    </ItemTemplate>
                                                     <FooterTemplate>  
                                                        <asp:Button ID="ButtonAdd" runat="server"   
                                                                             Text="Add New Row"   
                                                                             onclick="ButtonAdd_Click" />  
                                                    </FooterTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField>  
                                                    <ItemTemplate>  
                                                        <asp:LinkButton ID="lnkRemove" runat="server"   
                                                                                onclick="lnkRemove_Click">Remove</asp:LinkButton>  
                                                    </ItemTemplate>  
                                                </asp:TemplateField>  
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td align="left">
                                        <div class="text">
                                            <div style="padding-left: 3px;">
                                                <asp:LinkButton ID="lbtnSave" runat="server" CssClass="gray_bt" ValidationGroup="vgAddMetricTargetPreset"
                                                    OnClick="lbtnSave_Click">
                                                    <span> <asp:Localize Text="Save" ID="locSave" runat="server" 
                                                    meta:resourcekey="locSaveResource1"></asp:Localize> </span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="gray_bt" 
                                                    onclick="lbtnCancel_Click">
                                                    <span> <asp:Localize Text="Cancel" ID="locCancel" runat="server" 
                                                    meta:resourcekey="locCancelResource1"></asp:Localize> </span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                            </table>

                            </fieldset>
                    </center>
                    <br />
                    <br />
                    <center>
                    <fieldset  style="width: 60%;">
                        <legend>
                                <asp:Label ID="Label1" runat="server" Text="Add Metric Sum"  meta:resourcekey="lblHeaderResource1">
                                  </asp:Label>
                            </legend>
                            <ucAdminStatusbar:AdminStatusbar ID="AdminStatusbar1" runat="server" 
                                Visible="False" />
                            <table>
                                                                 
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize Text="Metric Sum" 
                                            Id="Localize1" runat="server" 
                                            meta:resourcekey="locPresetNameResource1"></asp:Localize><span style="color: Red;">*</span> </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtMetricSum" runat="server" MaxLength="50" Width="175px" Height="19px" ></asp:TextBox>
                                        </div>
                                        <div style="float: left">
                                            <asp:RequiredFieldValidator ID="rfvPresetName" runat="server" Display="Dynamic" ControlToValidate="txtMetricSum"
                                                ForeColor="Red" ValidationGroup="vgAddMetricSum">
                                                <ucValidationContainer:ValidationContainer ID="rfvMetricSumContainer" runat="server"
                                                    ValidationMsg="Please provide metric sum name." Size="200"/>
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                                                   
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td align="left">
                                        <div class="text">
                                            <div style="padding-left: 3px;">
                                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="gray_bt" ValidationGroup="vgAddMetricSum"
                                                    OnClick="lbtnMetricSumSave_Click">
                                                    <span> <asp:Localize Text="Save" ID="Localize2" runat="server" 
                                                    meta:resourcekey="locSaveResource1"></asp:Localize> </span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton2" runat="server" CssClass="gray_bt"  PostBackUrl="~/MetricTargetManagement.aspx">
                                                    <span> <asp:Localize Text="Cancel" ID="Localize3" runat="server" 
                                                    meta:resourcekey="locCancelResource1"></asp:Localize> </span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                            </table>
                    </fieldset>
                        </center>
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
</div>
<div class="admin_bottom">
    &nbsp;
</div>
<div class="clear">
</div>
