<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="ManageMeasurement.aspx.cs"  EnableEventValidation="false" Inherits="OrisWeb.ManageMeasurement" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<%@ Register TagPrefix="custom" Namespace="Oris.Business" Assembly="Oris.Business" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="rad" %>
<%@ Register Src="~/Common/AdminMenu.ascx" TagName="AdminMenu" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript" src="JS/CommonFunctions.js"></script>

    <script type="text/javascript" type="text/javascript">
    function CheckCheckBoxes() 
    {
      
       var Result = 'false';
	   for(i = 0; i < document.forms[0].elements.length; i++) 
        {
            elm = document.forms[0].elements[i]
            
            if (elm.type == 'checkbox') 
            {
                    if(elm.checked)
                       {Result = 'true'}
            }
        }
        if(Result == 'true')
        {
            if(confirm('The selected Measurements will be deleted. Continue?')) 
               return true;
            else 
               return false;
        }
        else{ alert("Please select measurement(s) to delete?"); return false;}
    }
    function CheckAllDataGridCheckBoxes(aspCheckBoxAllID, aspCheckBoxID) 
    {
	    checkVal = document.getElementById(aspCheckBoxAllID).checked;
        re = new RegExp('_' + aspCheckBoxID )  //generated control name starts with a colon
            
        for(i = 0; i < document.forms[0].elements.length; i++) 
        {
            elm = document.forms[0].elements[i]
            
            if (elm.type == 'checkbox') 
            {
                if (re.test(elm.id)) 
                {
                    elm.checked = checkVal
                }
            }
        }
    }

    </script>

    <style type="text/css">
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
                <table width="940" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <uc1:AdminMenu ID="UCAdminMenu" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="middle">
                            <table width="100%" border="0" cellspacing="1" cellpadding="2">
                                <tr>
                                    <td class="green-title" align="left">
                                        <asp:Literal ID="LTRL_Title" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" class="green-form-bg">
                                        <asp:UpdatePanel ID="UP_Branch" runat="server">
                                            <ContentTemplate>
                                                <table width="940px" border="0" cellspacing="5" cellpadding="0" align="center" class="maintable">
                                                    <tr>
                                                        <td class="ERROR">
                                                            <asp:Literal ID="LblError" runat="server"></asp:Literal>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td runat="server" id="TD_ShowBranch" valign="top">
                                                            <table width="920px" border="0" cellspacing="1" cellpadding="2">
                                                                <tr>
                                                                    <td align="center">
                                                                        <rad:RadGrid ID="GridJobReport" runat="server" AllowFilteringByColumn="True" AllowPaging="True"
                                                                            AllowSorting="True" AutoGenerateColumns="False" EnableAJAX="True" GridLines="None"
                                                                            Width="100%" PageSize="25" OnNeedDataSource="GridJobReport_NeedDataSource" Skin="Green"
                                                                            OnItemDataBound="GridJobReport_ItemDataBound" OnItemCreated="GridJobReport_ItemCreated"
                                                                            OnItemCommand="GridJobReport_ItemCommand">
                                                                            <ItemStyle Wrap="false" />
                                                                            <ClientSettings>
                                                                                <Scrolling AllowScroll="True" ScrollHeight="400"/>
                                                                            </ClientSettings>
                                                                            <PagerStyle Position="Top" />
                                                                            <MasterTableView>
                                                                                <PagerStyle Position="Top" />
                                                                                <RowIndicatorColumn Visible="False">
                                                                                </RowIndicatorColumn>
                                                                                <ExpandCollapseColumn Visible="False" Resizable="False">
                                                                                </ExpandCollapseColumn>
                                                                                <Columns>
                                                                                    <rad:GridTemplateColumn SortDescImageUrl="~/RadControls/Grid/Skins/Green/SortDesc.gif"
                                                                                        AllowFiltering="False" SortAscImageUrl="~/RadControls/Grid/Skins/Green/SortAsc.gif"
                                                                                        FilterImageUrl="~/RadControls/Grid/Skins/Green/Filter.gif" UniqueName="TemplateColumn">
                                                                                        <HeaderTemplate>
                                                                                            <asp:CheckBox ID="cbSelectAll" runat="server"></asp:CheckBox>
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="cbSelect" runat="server"></asp:CheckBox>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle Width="5%"></HeaderStyle>
                                                                                    </rad:GridTemplateColumn>
                                                                                    <rad:GridBoundColumn DataField="CompanyName" HeaderText="CompanyName" UniqueName="CompanyName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="BranchName" HeaderText="Branch Name" UniqueName="BranchName"
                                                                                        HeaderStyle-Width="15%" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="DongleDescription" HeaderText="Dongle" UniqueName="DongleDescription"
                                                                                        HeaderStyle-Width="15%" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="CPDeviceDesc" HeaderText="Device" UniqueName="CPDeviceDesc"
                                                                                        HeaderStyle-Width="15%" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                                    </rad:GridBoundColumn>
                                                                                    <custom:FilterColumnByDate UniqueName="DateTime" DataField="DateTime" HeaderText="Server Time"
                                                                                        SortExpression="DateTime" HeaderStyle-Width="14%">
                                                                                        <HeaderStyle />
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "DateTime")%></ItemTemplate>
                                                                                    </custom:FilterColumnByDate>
                                                                                    <custom:FilterColumnByDate UniqueName="GMTDateTime" DataField="GMTDateTime" HeaderText="GMT Time"
                                                                                        SortExpression="GMTDateTime" HeaderStyle-Width="14%">
                                                                                        <HeaderStyle />
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "GMTDateTime")%></ItemTemplate>
                                                                                    </custom:FilterColumnByDate>
                                                                                    <rad:GridBoundColumn DataField="Date" DataType="System.DateTime" HeaderText="Date"
                                                                                        UniqueName="Date" HeaderStyle-Width="10%" Visible="false">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="TIME" DataType="System.DateTime" HeaderText="Date"
                                                                                        UniqueName="Time" HeaderStyle-Width="10%" Visible="false">
                                                                                    </rad:GridBoundColumn>
                                                                                    <custom:FilteringColumn HeaderText="Status" SortDescImageUrl="~/RadControls/Grid/Skins/Green/SortDesc.gif"
                                                                                        UniqueName="Status" SortAscImageUrl="~/RadControls/Grid/Skins/Green/SortAsc.gif"
                                                                                        SortExpression="Status" FilterImageUrl="~/RadControls/Grid/Skins/Green/Filter.gif"
                                                                                        DataField="Status" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <img id="ibtnStatus" runat="server" style="cursor: pointer" />
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle Width="8%"></HeaderStyle>
                                                                                    </custom:FilteringColumn>
                                                                                    <rad:GridBoundColumn DataField="Status" DataType="System.String" HeaderText="Status"
                                                                                        UniqueName="hdnStatus" Visible="false">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridTemplateColumn HeaderText="Show" AllowFiltering="false" UniqueName="View"
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
                                                                                    <rad:GridBoundColumn DataField="Job" HeaderText="Job No" UniqueName="Job" HeaderStyle-Width="11%"
                                                                                        Visible="false">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="PrintTarget" HeaderText="Print Target" UniqueName="PrintTarget"
                                                                                        Visible="false">
                                                                                    </rad:GridBoundColumn>
                                                                                    <custom:FilteringColumn HeaderText="Device Type" SortDescImageUrl="~/RadControls/Grid/Skins/Green/SortDesc.gif"
                                                                                        UniqueName="DeviceType" SortAscImageUrl="~/RadControls/Grid/Skins/Green/SortAsc.gif"
                                                                                        SortExpression="Status" FilterImageUrl="~/RadControls/Grid/Skins/Green/Filter.gif"
                                                                                        DataField="CPDeviceTypeDesc" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "CPDeviceTypeDesc")%>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle Width="10%"></HeaderStyle>
                                                                                    </custom:FilteringColumn>
                                                                                    <rad:GridBoundColumn DataField="GUID" HeaderText="GUID" UniqueName="GUID" HeaderStyle-Width="250px"
                                                                                        Visible="false">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="CPMeasurementID" HeaderText="CPMeasurementID" UniqueName="CPMeasurementID"
                                                                                        Visible="false">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="CompanyID" HeaderText="CompanyID" UniqueName="CompanyID"
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
                                                                                    <rad:GridBoundColumn DataField="StatusID" HeaderText="CPDeviceTypeID" UniqueName="CPDeviceTypeID"
                                                                                        Visible="false">
                                                                                    </rad:GridBoundColumn>
                                                                                </Columns>
                                                                                <PagerStyle AlwaysVisible="True" />
                                                                                <PagerTemplate>
                                                                                    <div style="float: left">
                                                                                        <asp:Button ID="btnDelete" runat="server" CssClass="BUTTON" Text="Delete" CommandName="Delete">
                                                                                        </asp:Button>
                                                                                        &nbsp;
                                                                                    </div>
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
                                                                                    <div style="float: right">
                                                                                        <asp:Button ID="BTN_Reset" TabIndex="48" runat="server" CssClass="BUTTON" Text="Clear Filter"
                                                                                            CommandName="ResetFilter"></asp:Button>
                                                                                        &nbsp;
                                                                                    </div>
                                                                                </PagerTemplate>
                                                                            </MasterTableView>
                                                                        </rad:RadGrid>
                                                                        <br />
                                                                        <asp:Button runat="server" ID="hiddenTargetControlForModalPopup" Style="display: none" />
                                                                        <ajax:ModalPopupExtender runat="server" ID="programmaticModalPopup" BehaviorID="programmaticModalPopupBehavior"
                                                                            TargetControlID="hiddenTargetControlForModalPopup" PopupControlID="programmaticPopup"
                                                                            BackgroundCssClass="modalBackground" DropShadow="false" PopupDragHandleControlID="programmaticPopupDragHandle">
                                                                        </ajax:ModalPopupExtender>
                                                                        <asp:Panel runat="server" ID="programmaticPopup" Style="display: none; height: 765px;
                                                                            background-color: White;">
                                                                            <a id="hideModalPopupViaClientButton" href="#" style="border: none; text-decoration: none;">
                                                                                <img src="Images/Close2.png" style="position: absolute; border: none; right: 0px;
                                                                                    top: 0px; text-align: right;" />
                                                                            </a>
                                                                            <iframe id="FrmPopup" src="" width="905px" height="100%" frameborder="0" scrolling="no">
                                                                            </iframe>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
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
    function hideModalPopupViaClient(ev) 
    {
            ev.preventDefault();        
            var modalPopupBehavior = $find('programmaticModalPopupBehavior');
            modalPopupBehavior.hide();
            document.getElementById("FrmPopup").src = "";
    }
    function OpenWindow()
    {
        window.open( 'DTLTrend.aspx?CPMeasurementGUID=2D081E05-113E-11DD-89C5-000A958F3FC6&CPMeasurementID=2&CompanyID=1&Company=FujiFilm Graphic Systems&BranchID=1&Branch=Hanover Park&CPDongleID=1&CPDongle=Chirags Dongle&CPDeviceID=147&CPDevice=Final Proof MAC test&Job=&Date=04/23/2008&Time=6:20 AM&CPDevicePrintTargetID=220&PrintTarget=G5 2 LEVEL TOLERANCE&Status=1&Customer=ABC' , 'CPMeasurementReading' , 'width=900px,height=700px,resizable=1,scrollbars=1' );
    }    
    
    function OpenMeasurementWindow(URLStr, IsShow, IsNewWindow , NewId)
    {            
        if(IsShow == true)
        {
            if(IsNewWindow == true)
            {
                window.open(URLStr, NewId , 'width=900px,height=765px,resizable=0,scrollbars=0');
            }
            else
            {
                //========== Settings Maulik ==========//
//                if(screen.height > 1125)
//                    document.getElementById("<%=programmaticPopup.ClientID %>").style.height = "850px";
//                else if( screen.height > 1000 )
//                    document.getElementById("<%=programmaticPopup.ClientID %>").style.height = "775px";
//                else if( screen.height > 825 )
//                    document.getElementById("<%=programmaticPopup.ClientID %>").style.height = "650px";
//                else
//                    document.getElementById("<%=programmaticPopup.ClientID %>").style.height = "650px";
                if( screen.width <= 1024 )
                {            
                    document.getElementById("<%=programmaticPopup.ClientID %>").style.height = "600px";                
                }
                //========== Settings Maulik ==========//
            
                document.getElementById("FrmPopup").src = URLStr +"&ModalDialog=Y";
                var modalPopupBehavior = $find('programmaticModalPopupBehavior');
                modalPopupBehavior.show();
            }
        }
        else
        {
            alert("Facility for device type color, monitor and wide format only!");
        }
    }
    
    </script>

</asp:Content>
