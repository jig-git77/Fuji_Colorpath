<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddProject.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.AddProduct" %>
<div class="contant_top">
    &nbsp;</div>
<div class="contant_middle">
    <div class="head">
        <h1>
            Add Project</h1>
        <a href="#" class="button"><span>
            <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/save_icon.png" />
            Save</span></a></div>
    <div class="clear">
    </div>
    <div class="product_list_table">
        <table border="0" cellspacing="5" cellpadding="0" class="Width100percent">
            <tr>
                <td class="ProjectListtd1">
                    <asp:Label ID="lblProjectName" runat="server" Text="Project Name" CssClass="LabelTitle"></asp:Label>
                    :
                </td>
                <td class="ProjectListtd2">
                    <telerik:RadTextBox ID="txtProjectName" runat="server" SkinID="width196px">
                    </telerik:RadTextBox>
                </td>
                <td class="float_right">
                    <asp:Label ID="lblProjectType" runat="server" Text="Project Type" CssClass="LabelTitle"></asp:Label>
                    :
                </td>
                <td class="ProjectListtd4">
                    <telerik:RadComboBox ID="cmbProjectType" runat="server" SkinID="width200px">                      
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="ProjectListtd1">
                    <asp:Label ID="lblOutputType" runat="server" Text="Output Type" CssClass="LabelTitle"></asp:Label>
                    :
                </td>
                <td class="ProjectListtd2">
                    <telerik:RadComboBox ID="cmbOutputType" runat="server" SkinID="width200px">
                    </telerik:RadComboBox>
                </td>
                <td class="float_right">
                    &nbsp;
                </td>
                <td class="ProjectListtd4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <div style="margin-top: 10px;">
                        <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                            SelectedIndex="0" Skin="Black">
                            <Tabs>
                                <telerik:RadTab Text="DLP" Value="DLP">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Measurement Setup" Value="Measurement Setup">
                                </telerik:RadTab>
                                <telerik:RadTab Text="DLP" Value="DLP">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Measurement Setup" Value="Measurement Setup">
                                </telerik:RadTab>
                            </Tabs>
                        </telerik:RadTabStrip>
                        <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" CssClass="RadPageViewBorder">
                            <telerik:RadPageView ID="RadPageView1" runat="server">
                                <table border="0" cellspacing="5" cellpadding="0" class="Width100percent">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Text="Select Source" CssClass="LabelTitle"></asp:Label>
                                            :
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cmbSourceProfile" runat="server" DataValueField="Key" DataTextField="Value" SkinID="width200px">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text="Select Source" CssClass="LabelTitle"></asp:Label>
                                            :
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cmbDestinationProfile" DataValueField="Key" DataTextField="Value" runat="server" SkinID="width200px">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <div style="padding: 10px 5px 5px 5px;">
                                                <telerik:RadGrid ID="grdDLP" runat="server" AutoGenerateColumns="false" OnNeedDataSource="grdDLP_NeedDataSource"
                                                    EnableEmbeddedSkins="false" Skin="Fujifilm" SkinID="CommonGrid">
                                                    <MasterTableView>
                                                        <Columns>
                                                            <telerik:GridTemplateColumn>
                                                                <ItemTemplate>
                                                                    <asp:Image ID="imgBullet" ImageUrl="~/Images/table_bullate.png" runat="server" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn DataField="Source" HeaderText="Source" UniqueName="Source">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Paper" HeaderText="Paper" UniqueName="Paper">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="PrintVariables" HeaderText="Print Variables"
                                                                UniqueName="PrintVariables">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Separations" HeaderText="Separations" UniqueName="Separations">
                                                            </telerik:GridBoundColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                    <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="true">
                                                    </ClientSettings>
                                                </telerik:RadGrid>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="RadPageView2" runat="server">
                                2222<br />
                                2222<br />
                                2222<br />
                                2222<br />
                                2222<br />
                                2222<br />
                                2222<br />
                                2222<br />
                                2222<br />
                                2222<br />
                                2222<br />
                                2222<br />
                                2222<br />
                                2222<br />
                                2222<br />
                                2222<br />
                                2222<br />
                                2222<br />
                                2222<br />
                            </telerik:RadPageView>
                        </telerik:RadMultiPage>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</div>
<div class="contant_bottom">
    &nbsp;</div>
<div class="clear">
</div>
