<%@ Page Title="" Language="C#" MasterPageFile="~/SyncPopup.master" AutoEventWireup="true"
    CodeFile="SyncCertifyPressRunPopup.aspx.cs" Inherits="SyncCertifyPressRunPopup" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="rad" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="Common/GenerateSyncReport.ascx" TagName="GenerateSyncReport" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table id="TblGamut" runat="server" cellpadding="0" cellspacing="0" border="0" width="100%"
        class="SYNCTABLE">
        <tr>
            <td class="green-title">
                Measurement List
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <asp:Label ID="LblError" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">
                <table style="width: 98%; border: 1PX SOLID #C1C1C1" cellspacing="0" cellpadding="0"
                    border="0">
                    <tr>
                        <td style="width: 10%; border-right: 1PX SOLID #C1C1C1;" class="BGRIDTITLE" align="center">
                            Used
                        </td>
                        <td style="width: 50%; border-right: 1PX SOLID #C1C1C1;" class="BGRIDTITLE" align="center">
                            Measurement Name
                        </td>
                        <td style="width: 40%; border-right: 1PX SOLID #C1C1C1;" class="BGRIDTITLE" align="center">
                            Upload Date Time
                        </td>
                    </tr>
                </table>
                <rad:RadGrid ID="RG_SyncMeasurement" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                    AllowSorting="true" AllowFilteringByColumn="false" Width="100%" Skin="Green">
                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                    <ClientSettings>
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" ScrollHeight="250" />
                    </ClientSettings>
                    <PagerStyle Position="Top" />
                    <MasterTableView>
                        <Columns>
                            <rad:GridTemplateColumn DataField="IsUsed" HeaderText="Used" ItemStyle-Width="10%"
                                HeaderStyle-Width="10%" AllowFiltering="false" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <img src='<%# UsedImageName(Eval("IsUsed").ToString()) %>' height="10px" width="12px" />
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn DataField="MeasurementName" HeaderText="MeasurementName"
                                ItemStyle-Width="50%" HeaderStyle-Width="50%" AllowFiltering="false" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="Lbl_Measurement" runat="server" Text='<%# OriginalFileNameReturn(Eval("MeasurementName").ToString()) %>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn DataField="UploadDateTime" DataType="System.DateTime" UniqueName="UploadDateTime"
                                AllowFiltering="false" Visible="true" HeaderText="Upload Date Time" ItemStyle-Width="40%"
                                HeaderStyle-Width="40%" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "UploadDateTime").ToString().Replace(":000", " ") %>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </rad:RadGrid>
            </td>
        </tr>        
        <tr><td style="padding:8px;"><uc1:GenerateSyncReport ID="GenerateSyncReport1" runat="server" /></td></tr>
        <tr><td style="padding:8px;"><asp:Button ID="BtnCertifyPressRun" OnClientClick="return GetClientDate();" OnClick=" BtnCertifyPressRun_Click" runat="server" CssClass="BUTTON" Text="Certify Press Run"/>&nbsp;&nbsp;&nbsp;        
        <asp:Button ID="BtnBackToAnalyzeMeasurement" OnClick=" BtnBackToAnalyzeMeasurement_Click" runat="server" CssClass="BUTTON" Text="Back To Analyze Measurement"/>
         </td></tr>
    </table>
    <asp:HiddenField ID="HDClientDatatime" runat="server" />
    <script language="javascript" type="text/javascript">
        function GetClientDate() {
            var dt = new Date();
            var tzo = dt.toDateString() + ' ' + dt.getHours() + ':' + dt.getMinutes() + ':' + dt.getSeconds();
            document.getElementById('<%= HDClientDatatime.ClientID %>').value = tzo.toString();
            return true;
        }
    </script>
</asp:Content>
