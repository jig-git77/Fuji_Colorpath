<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ProcessorManagement.aspx.cs" Inherits="OrisWeb.ProcessorManagement" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="rad" %>
<%@ Register TagPrefix="custom" Namespace="Oris.Business" Assembly="Oris.Business" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="940px" border="0" cellspacing="0" cellpadding="0" align="center">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
                    <tr>
                        <td>
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" class="TABLE1">
                                        <tr>
                                            <td class="green-title">
                                                &nbsp; Processors
                                            </td> 
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LblError" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="RADSEARCHBARCOMPANY" align="center">
                                                <asp:DropDownList ID="DDL_CompanyList" runat="server" CssClass="inputbox" AutoPostBack="True"
                                                    OnSelectedIndexChanged="DDL_CompanyList_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:Literal ID="LTRL_UserCompany" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <rad:RadGrid ID="GridJobReportPrepress" runat="server" AllowFilteringByColumn="True"
                                                    AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" EnableAJAX="True"
                                                    GridLines="None" Width="100%" PageSize="25" OnNeedDataSource="GridJobReportPrepress_NeedDataSource"
                                                    Skin="Green" OnItemDataBound="GridJobReportPrepress_ItemDataBound" OnItemCreated="GridJobReportPrepress_ItemCreated"
                                                    OnItemCommand="GridJobReportPrepress_ItemCommand">
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
                                                            <rad:GridTemplateColumn HeaderText="View" AllowFiltering="false" UniqueName="View"
                                                                ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <img id="ImgView" runat="server" src="~/Images/view-icon-1.gif" style="cursor: pointer"
                                                                        title="View in same window" />
                                                                    &nbsp;
                                                                    <img id="ImgView_New" runat="server" src="~/Images/popup.gif" style="cursor: pointer"
                                                                        title="View in new window" />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="7%"></HeaderStyle>
                                                            </rad:GridTemplateColumn>
                                                            <rad:GridBoundColumn DataField="Device" HeaderText="Device" UniqueName="Device" HeaderStyle-Width="15%" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                
                                                            </rad:GridBoundColumn>
                                                            <rad:GridBoundColumn DataField="BranchName" HeaderText="Branch" UniqueName="BranchName" HeaderStyle-Width="8%" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"></rad:GridBoundColumn>
                                                            <rad:GridBoundColumn DataField="NumOfPlatesRel" HeaderText="Rel Plates" UniqueName="NumOfPlatesRel" HeaderStyle-Width="8%" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"></rad:GridBoundColumn>
                                                            <rad:GridBoundColumn DataField="DevPumpedRel" HeaderText="Rel Developer" UniqueName="DevPumpedRel" HeaderStyle-Width="8%" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"></rad:GridBoundColumn>
                                                            <rad:GridBoundColumn DataField="Conductivity" HeaderText="Conductivity" UniqueName="Conductivity" HeaderStyle-Width="8%" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"></rad:GridBoundColumn>
                                                            <rad:GridBoundColumn DataField="DeveloperTemp" HeaderText="Temperature" UniqueName="DeveloperTemp" HeaderStyle-Width="8%" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"></rad:GridBoundColumn>
                                                            <rad:GridBoundColumn DataField="DaystoDevChange" HeaderText="Life Est. Days" UniqueName="DaystoDevChange" HeaderStyle-Width="8%" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"></rad:GridBoundColumn>
                                                            <rad:GridBoundColumn DataField="LastMeasurementDate" HeaderText="Last Measurement" UniqueName="LastMeasurementDate" HeaderStyle-Width="15%" AllowFiltering="false" AllowSorting="false"></rad:GridBoundColumn>
                                                            <rad:GridTemplateColumn Visible="false">
                                                              <ItemTemplate>
                                                                    <asp:Label ID="lblDongleID" runat="server" Text='<%# Eval("CPDongleID") %>'></asp:Label>
                                                                    <asp:Label ID="lblZProcID" runat="server" Text='<%# Eval("ZProcID") %>'></asp:Label>
                                                                    <asp:Label ID="lblLocation" runat="server" Text='<%# Eval("CompanyName") + " - " + Eval("BranchName")  %>'></asp:Label>
                                                                    <asp:Label ID="lblDevice" runat="server" Text='<%# Eval("Device") %>'></asp:Label>
                                                                    
                                                                </ItemTemplate>
                                                            </rad:GridTemplateColumn>
                                                        </Columns>
                                                        <PagerStyle AlwaysVisible="True" />
                                                        <PagerTemplate>
                                                            <div class="RADSEARCHBAR">
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
                                                                <div style="float: left; width: 150px;">
                                                                    &nbsp;</div>
                                                                
                                                                <div style="float: right">
                                                                    <asp:Button ID="BTN_Reset" TabIndex="48" runat="server" CssClass="BUTTON" Text="Clear Filter"
                                                                        CommandName="ResetFilter"></asp:Button>
                                                                    &nbsp;
                                                                </div>
                                                            </div>
                                                        </PagerTemplate>
                                                    </MasterTableView>
                                                </rad:RadGrid>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <asp:Button runat="server" ID="hiddenTargetControlForModalPopup" Style="display: none" />
                                    <ajaxtoolkit:ModalPopupExtender runat="server" ID="programmaticModalPopup" BehaviorID="programmaticModalPopupBehavior"
                                        Y="50" TargetControlID="hiddenTargetControlForModalPopup" PopupControlID="programmaticPopup"
                                        BackgroundCssClass="modalBackground" DropShadow="false" PopupDragHandleControlID="programmaticPopupDragHandle">
                                    </ajaxtoolkit:ModalPopupExtender>
                                    <asp:Panel runat="server" ID="programmaticPopup" Style="display: none; height: 765px;
                                        background-color: Transparent; background: ">
                                        <a id="hideModalPopupViaClientButton" href="#" style="border: none; text-decoration: none;">
                                            <img src="Images/Close2.png" style="position: absolute; border: none; right: 10px;
                                                top: 10px; text-align: right;" />
                                        </a>
                                        <iframe id="FrmPopup" src="" width="952px" height="770px" frameborder="0" marginwidth="0"
                                            scrolling="no" marginheight="0" allowtransparency="true" style="background-color: transparent">
                                        </iframe>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
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
            if (IsShow == true) 
            {
                if (IsNewWindow == true) 
                {
                    window.open(URLStr, NewId, 'width=952px,height=795px,resizable=1,scrollbars=0');
                }
                else 
                {
                    if (screen.width <= 1024) 
                    {
                        document.getElementById("<%=programmaticPopup.ClientID %>").style.height = "630px";
                    }

                    document.getElementById("FrmPopup").src = URLStr;
                    var modalPopupBehavior = $find('programmaticModalPopupBehavior');
                    modalPopupBehavior.show();
                }
            }
            else {
                alert("Facility For Processor Not Available!");
            }
        }
        </script>

</asp:Content>
