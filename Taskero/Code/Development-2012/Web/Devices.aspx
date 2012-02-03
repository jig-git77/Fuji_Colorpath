<%@ Page Language="C#" MasterPageFile="~/Home.master"  EnableEventValidation="false" AutoEventWireup="true" CodeFile="Devices.aspx.cs"
    Inherits="Devices" %>

<%@ Register Src="Common/HomeMenu.ascx" TagName="HomeMenu" TagPrefix="uc1" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="rad" %>
<%@ Register TagPrefix="custom" Namespace="Fuji.Business" Assembly="Fuji.Business" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
    </style>
    <table width="940px" border="0" cellspacing="0" cellpadding="0" align="center">
        <tr>
            <td>
                <!--Menu Conttrol Will Load here-->
                <uc1:HomeMenu ID="HomeMenu1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Literal ID="LblError" runat="server"></asp:Literal>
                        <table cellpadding="0" cellspacing="0" border="0" class="TABLE1">
                            <tr>
                                <td class="green-title">
                                    &nbsp; Devices
                                </td>
                            </tr>
                            <tr id="TR_Company" runat="server">
                                <td class="RADSEARCHBARCOMPANY" align="center">
                                    <asp:DropDownList ID="DDL_CompanyList" runat="server" CssClass="inputbox" AutoPostBack="True"
                                        OnSelectedIndexChanged="DDL_CompanyList_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="DDL_DongleList" runat="server" CssClass="inputbox" AutoPostBack="True"
                                        OnSelectedIndexChanged="DDL_DongleList_SelectedIndexChanged" Visible="false">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="DDL_BranchList" runat="server" CssClass="inputbox" AutoPostBack="True"
                                        OnSelectedIndexChanged="DDL_BranchList_SelectedIndexChanged" Visible="false">
                                    </asp:DropDownList>
                                    <asp:Literal ID="LTRL_CompanyList" runat="server"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <rad:RadGrid ID="GridDevicesPrepress" runat="server" AllowFilteringByColumn="True"
                                        AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" EnableAJAX="True"
                                        GridLines="None" Width="100%" PageSize="25" OnNeedDataSource="GridDevicesPrepress_NeedDataSource"
                                        Skin="Green" OnItemDataBound="GridDevicesPrepress_ItemDataBound" OnItemCreated="GridDevicesPrepress_ItemCreated"
                                        OnItemCommand="GridDevicesPrepress_ItemCommand">
                                        <ItemStyle Wrap="true" />
                                        <ClientSettings Resizing-AllowColumnResize="true">
                                            <Scrolling AllowScroll="True" ScrollHeight="400" />
                                        </ClientSettings>
                                        <PagerStyle Position="Top" />
                                        <MasterTableView>
                                            <PagerStyle Position="Top" />
                                            <RowIndicatorColumn Visible="False">
                                            </RowIndicatorColumn>
                                            <ExpandCollapseColumn Visible="False" Resizable="False">
                                            </ExpandCollapseColumn>
                                            <Columns>
                                                <custom:FilteringColumn HeaderText="Status" SortDescImageUrl="~/RadControls/Grid/Skins/Green/SortDesc.gif"
                                                    UniqueName="DeviceStatus" SortAscImageUrl="~/RadControls/Grid/Skins/Green/SortAsc.gif"
                                                    SortExpression="Status" FilterImageUrl="~/RadControls/Grid/Skins/Green/Filter.gif"
                                                    DataField="Status" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="70px">
                                                    <ItemTemplate>
                                                        <img id="ibtnStatus" runat="server" style="cursor: pointer" />
                                                    </ItemTemplate>
                                                </custom:FilteringColumn>
                                                <rad:GridBoundColumn DataField="Status" DataType="System.String" HeaderText="Status"
                                                    UniqueName="hdnStatus" Visible="false">
                                                </rad:GridBoundColumn>
                                                <rad:GridTemplateColumn HeaderText="View" AllowFiltering="false" UniqueName="View"
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="60px">
                                                    <ItemTemplate>
                                                        <img id="ImgView" runat="server" src="~/Images/view-icon-1.gif" style="cursor: pointer"
                                                            title="View in same window" />
                                                        &nbsp;
                                                        <img id="ImgView_New" runat="server" src="~/Images/popup.gif" style="cursor: pointer"
                                                            title="View in new window" />
                                                    </ItemTemplate>
                                                </rad:GridTemplateColumn>
                                                <rad:GridBoundColumn DataField="Job" HeaderText="Job No" UniqueName="Job" Visible="false">
                                                </rad:GridBoundColumn>
                                                <rad:GridBoundColumn DataField="PrintTarget" HeaderText="Print Target" UniqueName="PrintTarget"
                                                    Visible="false">
                                                </rad:GridBoundColumn>
                                                <rad:GridBoundColumn DataField="CPDeviceDesc" HeaderText="Device" UniqueName="CPDeviceDesc"
                                                    HeaderStyle-Width="150px" ItemStyle-Width="150px" AutoPostBackOnFilter="true"
                                                    CurrentFilterFunction="Contains">
                                                </rad:GridBoundColumn>
                                                <rad:GridBoundColumn DataField="DongleSerial" HeaderText="Workstation" UniqueName="DongleDescription"
                                                    HeaderStyle-Width="150px" ItemStyle-Width="150px" AutoPostBackOnFilter="true"
                                                    CurrentFilterFunction="Contains">
                                                </rad:GridBoundColumn>
                                                <rad:GridBoundColumn DataField="BranchName" HeaderText="Branch" UniqueName="BranchName"
                                                    Visible="true" HeaderStyle-Width="150px" ItemStyle-Width="150px" AutoPostBackOnFilter="true"
                                                    CurrentFilterFunction="Contains">
                                                </rad:GridBoundColumn>
                                                <custom:FilteringColumn HeaderText="Device Type" SortDescImageUrl="~/RadControls/Grid/Skins/Green/SortDesc.gif"
                                                    UniqueName="DeviceTypeWithoutPress" SortAscImageUrl="~/RadControls/Grid/Skins/Green/SortAsc.gif"
                                                    SortExpression="Status" FilterImageUrl="~/RadControls/Grid/Skins/Green/Filter.gif"
                                                    DataField="CPDeviceTypeDesc" HeaderStyle-Width="150px" ItemStyle-Width="150px">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "CPDeviceTypeDesc") %>
                                                    </ItemTemplate>
                                                </custom:FilteringColumn>
                                                <custom:FilterColumnByDate UniqueName="DateTime" DataField="DateTime" HeaderText="Date"
                                                    HeaderStyle-Width="150px" ItemStyle-Width="150px">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "DateTime")%></ItemTemplate>
                                                </custom:FilterColumnByDate>
                                                <rad:GridBoundColumn DataField="Date" DataType="System.DateTime" HeaderText="Date"
                                                    UniqueName="Date" Visible="false">
                                                </rad:GridBoundColumn>
                                                <rad:GridBoundColumn DataField="TIME" DataType="System.TimeSpan" HeaderText="TIME"
                                                    UniqueName="TIME" Visible="false">
                                                </rad:GridBoundColumn>
                                                <rad:GridBoundColumn DataField="GUID" HeaderText="GUID" UniqueName="GUID" Visible="false">
                                                </rad:GridBoundColumn>
                                                <rad:GridBoundColumn DataField="CPMeasurementID" HeaderText="CPMeasurementID" UniqueName="CPMeasurementID"
                                                    Visible="false">
                                                </rad:GridBoundColumn>
                                                <rad:GridBoundColumn DataField="CompanyID" HeaderText="CompanyID" UniqueName="CompanyID"
                                                    Visible="false">
                                                </rad:GridBoundColumn>
                                                <rad:GridBoundColumn DataField="CompanyName" HeaderText="CompanyName" UniqueName="CompanyName"
                                                    Visible="false">
                                                </rad:GridBoundColumn>
                                                <rad:GridBoundColumn DataField="BranchID" HeaderText="BranchID" UniqueName="BranchID"
                                                    Visible="false">
                                                </rad:GridBoundColumn>
                                                <rad:GridBoundColumn DataField="CPDongleID" HeaderText="DongleID" UniqueName="DongleID"
                                                    Visible="false">
                                                </rad:GridBoundColumn>
                                                <rad:GridBoundColumn DataField="CPDeviceID" HeaderText="DeviceID" UniqueName="DeviceID"
                                                    Visible="false">
                                                </rad:GridBoundColumn>
                                                <rad:GridBoundColumn DataField="CPDevicePrintTargetID" HeaderText="CPDevicePrintTargetID"
                                                    UniqueName="CPDevicePrintTargetID" Visible="false">
                                                </rad:GridBoundColumn>
                                                <rad:GridBoundColumn DataField="Customer" HeaderText="Customer" UniqueName="Customer"
                                                    Visible="false">
                                                </rad:GridBoundColumn>
                                                <rad:GridBoundColumn DataField="CPDeviceTypeID" HeaderText="CPDeviceTypeID" UniqueName="CPDeviceTypeID"
                                                    Visible="false">
                                                </rad:GridBoundColumn>
                                                <rad:GridBoundColumn DataField="FlgOpenMeasurementWindow" HeaderText="FlgOpenMeasurementWindow"
                                                    UniqueName="FlgOpenMeasurementWindow" Visible="false">
                                                </rad:GridBoundColumn>
                                                <rad:GridBoundColumn DataField="CPSerialNUM" Visible="false">
                                                </rad:GridBoundColumn>
                                            </Columns>
                                            <PagerStyle AlwaysVisible="True" />
                                            <PagerTemplate>
                                                <div style="float: left">
                                                    <asp:Literal ID="LTRL_RecordText" runat="server"></asp:Literal>&nbsp;
                                                    <asp:ImageButton ID="IBTN_First" CommandName="Page" CausesValidation="false" CommandArgument="First"
                                                        runat="server" ImageUrl="Images/Nav_FirstPage_off.gif" align="absmiddle"></asp:ImageButton>
                                                    <asp:ImageButton ID="IBTN_Prev" CommandName="Page" CausesValidation="false" CommandArgument="Prev"
                                                        runat="server" ImageUrl="Images/Nav_PreviousPage_off.gif" align="absmiddle">
                                                    </asp:ImageButton>
                                                    <asp:ImageButton ID="IBTN_Next" CommandName="Page" CausesValidation="false" CommandArgument="Next"
                                                        runat="server" ImageUrl="Images/Nav_NextPage_off.gif" align="absmiddle"></asp:ImageButton>
                                                    <asp:ImageButton ID="IBTN_Last" CommandName="Page" CausesValidation="false" CommandArgument="Last"
                                                        runat="server" ImageUrl="Images/Nav_LastPage_off.gif" align="absmiddle"></asp:ImageButton>
                                                    &nbsp;
                                                    <asp:TextBox ID="TXT_PageNumber" runat="server" Columns="3" Text='<%# (int)DataBinder.Eval(Container, "OwnerTableView.CurrentPageIndex") + 1 %>'
                                                        Visible="False" />
                                                    <asp:LinkButton ID="IBTN_Go" runat="server" CommandName="CustomChangePage" Visible="False">Go</asp:LinkButton>
                                                    <font class="text">| Records</font>
                                                    <asp:DropDownList ID="DDL_RecordPage" runat="server" AutoPostBack="true" CommandName="Page"
                                                        class="inputbox">
                                                        <asp:ListItem Value="5">5</asp:ListItem>
                                                        <asp:ListItem Value="10">10</asp:ListItem>
                                                        <asp:ListItem Value="25" Selected>25</asp:ListItem>
                                                        <asp:ListItem Value="50">50</asp:ListItem>
                                                        <asp:ListItem Value="100">100</asp:ListItem>
                                                        <asp:ListItem Value="500">500</asp:ListItem>
                                                        <asp:ListItem Value="1000">1000</asp:ListItem>
                                                    </asp:DropDownList>
                                                    per page
                                                </div>
                                                <div style="float: left; width: 100px;">
                                                    &nbsp;</div>
                                                <div style="float: left;">
                                                    <img id="Img1" runat="server" src="~/Images/Green-Lamp.gif" align="absmiddle" />
                                                    Green &nbsp;
                                                    <img id="Img2" runat="server" src="~/Images/Yellow-Lamp.gif" align="absmiddle" />
                                                    Yellow &nbsp;
                                                    <img id="Img3" runat="server" src="~/Images/Orange-Lamp.gif" align="absmiddle" />
                                                    Orange &nbsp;
                                                    <img id="Img4" runat="server" src="~/Images/Red-Lamp.gif" align="absmiddle" />
                                                    Red
                                                </div>
                                                <div style="float: right">
                                                    <asp:Button ID="BTN_Reset" TabIndex="48" runat="server" CssClass="BUTTON" Text="Clear Filter"
                                                        CommandName="ResetFilter"></asp:Button>
                                                </div>
                                            </PagerTemplate>
                                        </MasterTableView>
                                    </rad:RadGrid>
                                </td>
                            </tr>
                        </table>
                        <asp:Button runat="server" ID="hiddenTargetControlForModalPopup" Style="display: none" />
                        <ajaxtoolkit:ModalPopupExtender runat="server" ID="programmaticModalPopup" Y="50"
                            BehaviorID="programmaticModalPopupBehavior" TargetControlID="hiddenTargetControlForModalPopup"
                            PopupControlID="programmaticPopup" BackgroundCssClass="modalBackground" DropShadow="false"
                            PopupDragHandleControlID="programmaticPopupDragHandle">
                        </ajaxtoolkit:ModalPopupExtender>
                        <asp:Panel runat="server" ID="programmaticPopup" Style="display: none; height: 765px;
                            background-color: White;">
                            <a id="hideModalPopupViaClientButton" href="#" style="border: none; text-decoration: none;">
                                <img src="Images/Close2.png" style="position: absolute; border: none; right: 0px;
                                    top: 0px; text-align: right;" />
                            </a>
                            <iframe id="FrmPopup" src="" width="910px" height="100%" frameborder="0" scrolling="no">
                            </iframe>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>

    <script language="javascript" type="text/javascript">
        function pageLoad() {
            $addHandler($get("hideModalPopupViaClientButton"), 'click', hideModalPopupViaClient);
        }
        function hideModalPopupViaClient(ev) {
            ev.preventDefault();
            var modalPopupBehavior = $find('programmaticModalPopupBehavior');
            modalPopupBehavior.hide();
            document.getElementById("FrmPopup").src = "";
        }
        function OpenMeasurementWindow(URLStr, IsShow, IsNewWindow, NewId) {
            if (IsShow == true) {
                if (IsNewWindow == true) {
                    window.open(URLStr + '&ReportType=D', NewId, 'width=900px,height=765px,resizable=1,scrollbars=0');
                }
                else {
                    if (screen.width <= 1024) {
                        document.getElementById("<%=programmaticPopup.ClientID %>").style.height = "600px";
                    }

                    document.getElementById("FrmPopup").src = URLStr + "&ReportType=D";
                    var modalPopupBehavior = $find('programmaticModalPopupBehavior');
                    modalPopupBehavior.show();
                }
            }
            else {
                alert("Facility for device type color, monitor and wide format only!");
            }
        }
    </script>

</asp:Content>
