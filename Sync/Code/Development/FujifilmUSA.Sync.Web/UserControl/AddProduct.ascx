<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddProduct.ascx.cs"
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
                <td style="text-align: right; width: 90px;">
                    <asp:Label ID="lblProjectName" runat="server" Text="Project Name" CssClass="LabelTitle"></asp:Label>
                    :
                </td>
                <td style="padding-left: 13px; width: 200px;">
                    <telerik:RadTextBox ID="txtProjectName" runat="server" SkinID="width196px">
                    </telerik:RadTextBox>
                </td>
                <td style="text-align: right;">
                    <asp:Label ID="lblProjectType" runat="server" Text="Project Type" CssClass="LabelTitle"></asp:Label>
                    :
                </td>
                <td style="padding-left: 13px; width: 200px; text-align: right;">
                    <telerik:RadComboBox ID="cmbProjectType" runat="server" SkinID="width200px">
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 90px;">
                    <asp:Label ID="lblOutputType" runat="server" Text="Output Type" CssClass="LabelTitle"></asp:Label>
                    :
                </td>
                <td style="padding-left: 13px; width: 200px;">
                    <telerik:RadComboBox ID="cmbOutputType" runat="server" SkinID="width200px">
                    </telerik:RadComboBox>
                </td>
                <td style="text-align: right;">
                    &nbsp;
                </td>
                <td style="padding-left: 13px; width: 200px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <div style="margin-top: 10px;">
                        <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                            SelectedIndex="0">
                            <Tabs>
                                <telerik:RadTab Text="DLP" Value="DLP">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Measurement Setup" Value="Measurement Setup">
                                </telerik:RadTab>
                            </Tabs>
                        </telerik:RadTabStrip>
                        <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" BackColor="#ededed">
                            <telerik:RadPageView ID="RadPageView1" runat="server">
                                <table border="0" cellspacing="5" cellpadding="0" class="Width100percent">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Text="Select Source" CssClass="LabelTitle"></asp:Label>
                                            :
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="RadComboBox1" runat="server" SkinID="width200px">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text="Select Source" CssClass="LabelTitle"></asp:Label>
                                            :
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="RadComboBox2" runat="server" SkinID="width200px">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            --Grid Comes Here--
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
