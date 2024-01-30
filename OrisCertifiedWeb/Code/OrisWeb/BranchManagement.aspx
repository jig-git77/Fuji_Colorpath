<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true"  EnableEventValidation="false" CodeBehind="BranchManagement.aspx.cs" Inherits="OrisWeb.BranchManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register TagPrefix="custom" Namespace="Oris.Business" Assembly="Oris.Business" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="rad" %>
<%@ Register Src="~/Common/AdminMenu.ascx" TagName="AdminMenu" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript" src="JS/CommonFunctions.js"></script>
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
                        <td valign="middle">
                            <table width="100%" border="0" cellspacing="1" cellpadding="2" align="center">
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
                                                        <td>
                                                            <asp:Label ID="LblError" runat="server" CssClass="ERROR"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td runat="server" id="TD_ShowBranch" valign="top">
                                                            <table width="920px" border="0" cellspacing="1" cellpadding="2">
                                                                <tr>
                                                                    <td align="left" valign="top">
                                                                        <asp:Button CssClass="BUTTON" ID="BTN_AddNewBranch" runat="server" OnClick="BTN_AddNewBranch_Click" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <rad:RadGrid ID="RG_Branch" runat="server" AllowFilteringByColumn="True" AllowPaging="True"
                                                                            AllowSorting="True" AutoGenerateColumns="False" GridLines="None" Width="920px"
                                                                            PageSize="25" Skin="Green" MasterTableView-EditFormSettings-EditFormType="Template"
                                                                            OnItemCommand="RG_Branch_ItemCommand" OnNeedDataSource="RG_Branch_NeedDataSource"
                                                                            OnSortCommand="RG_Branch_SortCommand" OnItemDataBound="RG_Branch_ItemDataBound"
                                                                            OnItemCreated="RG_Branch_ItemCreated">
                                                                            <ItemStyle HorizontalAlign="Left" Wrap="true" Width="920px" />
                                                                            <ClientSettings>
                                                                                <Scrolling AllowScroll="true" ScrollHeight="380" />
                                                                            </ClientSettings>
                                                                            <PagerStyle Position="Top" />
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <MasterTableView AllowSorting="true" AllowNaturalSort="false">
                                                                                <NoRecordsTemplate>
                                                                                    No Data Found.</NoRecordsTemplate>
                                                                                <EditFormSettings>
                                                                                    <EditColumn UniqueName="EditCommandColumn1">
                                                                                    </EditColumn>
                                                                                </EditFormSettings>
                                                                                <PagerStyle Mode="NumericPages" Position="Top" />
                                                                                <RowIndicatorColumn Visible="False">
                                                                                    <HeaderStyle Width="30px"></HeaderStyle>
                                                                                </RowIndicatorColumn>
                                                                                <ExpandCollapseColumn Visible="False" Resizable="False">
                                                                                    <HeaderStyle Width="20px"></HeaderStyle>
                                                                                </ExpandCollapseColumn>
                                                                                <Columns>
                                                                                    <rad:GridBoundColumn DataField="BranchID" HeaderText="ID" Visible="false">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="CompanyName" HeaderText="Company Name" SortExpression="CompanyName"
                                                                                        Visible="false">
                                                                                    </rad:GridBoundColumn>
                                                                                    <custom:FilteringColumn UniqueName="CompanyName" DataField="CompanyName" HeaderText="Company Name"
                                                                                        Visible="true" SortExpression="CompanyName" ItemStyle-Width="150px" HeaderStyle-Width="150px">
                                                                                        <HeaderStyle />
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "CompanyName")%>
                                                                                        </ItemTemplate>
                                                                                    </custom:FilteringColumn>
                                                                                    <rad:GridBoundColumn DataField="BranchName" HeaderText="Branch Name" SortExpression="BranchName"
                                                                                        HeaderStyle-Width="90px" ItemStyle-Width="85px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="BranchLocation" HeaderText="Branch Location" SortExpression="BranchLocation"
                                                                                        HeaderStyle-Width="90px" ItemStyle-Width="85px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="PrimaryContactName" HeaderText="Contact Name" SortExpression="PrimaryContactName"
                                                                                        HeaderStyle-Width="90px" ItemStyle-Width="85px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="Telephone" HeaderText="Telephone" SortExpression="Telephone"
                                                                                        HeaderStyle-Width="90px" ItemStyle-Width="85px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="PrimaryContactEmail" HeaderText="Email" SortExpression="PrimaryContactEmail"
                                                                                        ItemStyle-Wrap="true" HeaderStyle-Width="140px" ItemStyle-Width="125px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="PrimaryContactCell" HeaderText="Primary Contact Cell"
                                                                                        HeaderStyle-Width="90px" ItemStyle-Width="85px" SortExpression="PrimaryContactCell" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="IsActive" HeaderText="Active" SortExpression="IsActive"
                                                                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="90px" ItemStyle-Width="85px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="false">
                                                                                    </rad:GridBoundColumn>
                                                                                   
                                                                                    <rad:GridTemplateColumn HeaderText="Support User" AllowFiltering="false" HeaderStyle-Width="85px"
                                                                                        ItemStyle-Width="85px" HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Center">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="IMGBTN_Assign" runat="server" CommandName="AssignBranch" ImageUrl="~/Images/Icon-Assign-Support-User.gif"
                                                                                                ToolTip="Click to assign dongles to support user" />
                                                                                        </ItemTemplate>
                                                                                    </rad:GridTemplateColumn>
                                                                                    <rad:GridTemplateColumn HeaderText="Edit" AllowFiltering="false" HeaderStyle-Width="38px"
                                                                                        ItemStyle-Width="38px" ItemStyle-HorizontalAlign="Center">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="IMGBTN_Edit" runat="server" CommandName="EditBranch" ImageUrl="~/Images/Edit-Icon.gif"
                                                                                                ToolTip="Edit" />
                                                                                        </ItemTemplate>
                                                                                    </rad:GridTemplateColumn>
                                                                                    <rad:GridTemplateColumn HeaderText="Delete" AllowFiltering="false" HeaderStyle-Width="45px"
                                                                                        ItemStyle-Width="45px" ItemStyle-HorizontalAlign="Center">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="IMGBTN_Delete" runat="server" CommandName="Delete" ImageUrl="~/Images/Delete-Icon.gif"
                                                                                                ToolTip="Delete" />
                                                                                        </ItemTemplate>
                                                                                    </rad:GridTemplateColumn>
                                                                                    <rad:GridBoundColumn DataField="CompanyID" HeaderText="CompanyID" Visible="false">
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
                                                                                        <asp:TextBox ID="tbPageNumber" runat="server" Columns="3" Text='<%# (int)DataBinder.Eval(Container, "OwnerTableView.CurrentPageIndex") + 1 %>'
                                                                                            Visible="False" />
                                                                                        <asp:LinkButton ID="LBTN_Go" runat="server" CommandName="CustomChangePage" Visible="False">Go</asp:LinkButton>
                                                                                        <font class="text">| Records</font>
                                                                                        <asp:DropDownList ID="DDl_RecordPage" runat="server" AutoPostBack="true" CommandName="Page"
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
                                                                                    <div style="float: right">
                                                                                        <asp:Button ID="BTN_Reset" TabIndex="48" runat="server" CssClass="BUTTON" Text="Clear Filter"
                                                                                            CommandName="ResetFilter"></asp:Button>
                                                                                        &nbsp;
                                                                                    </div>
                                                                                </PagerTemplate>
                                                                            </MasterTableView>
                                                                        </rad:RadGrid>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td runat="server" id="TD_AddEditBranch" align="left">
                                                            <table cellpadding="0" cellspacing="0" align="center">
                                                                <tr>
                                                                    <td align="center">
                                                                        <fieldset>
                                                                            <legend class="subtitle">
                                                                                <asp:Literal ID="LTRL_Header" runat="server"></asp:Literal>
                                                                            </legend>
                                                                            <table border="0" cellspacing="1" cellpadding="2" class="form-text" align="center">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        &nbsp;<asp:Literal ID="LTRL_CompanyID" runat="server" Visible="false"></asp:Literal><asp:Literal ID="LTRL_BranchID" runat="server" Visible="false">0</asp:Literal>
                                                                                    </td>
                                                                                    <td align="right" class="required-text-small">
                                                                                        Fields marked with (<span class="required-text-small-red">*</span>) are Required
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <asp:Literal ID="LTRL_CompanyName" runat="server"></asp:Literal>
                                                                                        <span class="required-text-small-red">*</span>&nbsp;:
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList CssClass="DROPDOWNLIST" ID="DDL_CompanyName" runat="server" Width="153px">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <asp:Literal ID="LTRL_BranchName" runat="server"></asp:Literal>
                                                                                        <span class="required-text-small-red">*</span>&nbsp;:
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox CssClass="TEXTBOX" ID="TXT_BranchName" runat="server" MaxLength="60"
                                                                                            Width="150px" onkeypress="CheckAlphaNumeric(event);"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <asp:Literal ID="LTRL_BranchLocation" runat="server"></asp:Literal>
                                                                                        <span class="required-text-small-red">*</span>&nbsp;:
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox CssClass="TEXTBOX" ID="TXT_BranchLocation" runat="server" MaxLength="60"
                                                                                            Width="150px"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <asp:Literal ID="LTRL_PrimaryContactName" runat="server"></asp:Literal>
                                                                                        <span class="required-text-small-red">*</span>&nbsp;:
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox CssClass="TEXTBOX" ID="TXT_PrimaryContactName" runat="server" MaxLength="50"
                                                                                            Width="150px"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <asp:Literal ID="LTRL_CountryCode" runat="server"></asp:Literal>
                                                                                        <span class="required-text-small-red">*</span>&nbsp;:
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox CssClass="TEXTBOX" ID="TXT_Tel_CountryCode" runat="server" Width="30px"
                                                                                            MaxLength="5" onkeypress="CheckNumeric(event);"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <asp:Literal ID="LTRL_Phone" runat="server"></asp:Literal>
                                                                                        <span class="required-text-small-red">*</span>&nbsp;:
                                                                                    </td>
                                                                                    <td>
                                                                                        <%-- Server Side maxlength up to 10 and US phone number validation is removed and maxlength added here up tp 15 --%>
                                                                                        <asp:TextBox CssClass="TEXTBOX" ID="TXT_Tel_PhoneNo" runat="server" MaxLength="15" Width="150px"></asp:TextBox>
                                                                                        <ajax:FilteredTextBoxExtender ID="Filtered_Tel_PhoneNo" runat="server" TargetControlID="TXT_Tel_PhoneNo"
                                                                                            FilterType="Custom" ValidChars="1234567890()-+ ">
                                                                                        </ajax:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <asp:Literal ID="LTRL_Extension" runat="server"></asp:Literal>
                                                                                        &nbsp;:
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox CssClass="TEXTBOX" ID="TXT_Tel_Extension" runat="server" Width="40px"
                                                                                            MaxLength="5" onkeypress="CheckNumeric(event);"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <asp:Literal ID="LTRL_PrimaryContactEmail" runat="server"></asp:Literal>
                                                                                        <span class="required-text-small-red">*</span>&nbsp;:
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox CssClass="TEXTBOX" ID="TXT_PrimaryContactEmail" runat="server" MaxLength="50"
                                                                                            Width="150px"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <asp:Literal ID="LTRL_PrimaryContactCell" runat="server"></asp:Literal>&nbsp;:
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox CssClass="TEXTBOX" ID="TXT_PrimaryContactCell" runat="server" MaxLength="20"
                                                                                            Width="150px"></asp:TextBox>
                                                                                        <ajax:FilteredTextBoxExtender ID="Filtered_PrimaryContactCell" runat="server" TargetControlID="TXT_PrimaryContactCell"
                                                                                            FilterType="Custom" ValidChars="1234567890()-+ ">
                                                                                        </ajax:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <asp:Literal ID="LTRL_Active" runat="server"></asp:Literal>&nbsp;:
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:CheckBox ID="CHK_IsActive" runat="server" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2" align="center">
                                                                                        <asp:Button CssClass="BUTTON" ID="BTN_Save" runat="server" Text="Save" OnClick="btnSave_Click" />
                                                                                        &nbsp;<asp:Button CssClass="BUTTON" ID="BTN_Cancel" runat="server" Text="Cancel"
                                                                                            OnClick="btnCancel_Click" />
                                                                                        &nbsp;</td>
                                                                                </tr>
                                                                            </table>
                                                                        </fieldset>
                                                                    </td>
                                                                    <td style="padding-left: 20px;" valign="top">
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
                    <tr>
                        <td align="center" valign="top">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div>
                                <asp:Button runat="server" ID="hiddenTargetControlForModalPopup" Style="display: none" />
                                <ajax:ModalPopupExtender runat="server" ID="programmaticModalPopup" BehaviorID="programmaticModalPopupBehavior"
                                    TargetControlID="hiddenTargetControlForModalPopup" PopupControlID="programmaticPopup"
                                    BackgroundCssClass="modalBackground" DropShadow="false" PopupDragHandleControlID="programmaticPopupDragHandle">
                                </ajax:ModalPopupExtender>
                                <asp:Panel runat="server" ID="programmaticPopup" Style="display: none; height: 525px;
                                    background-color: White;  ">
                                    <a id="hideModalPopupViaClientButton" href="#" style="border: none; text-decoration: none;">
                                        <img src="Images/Close2.png" style="position: absolute; border: none; right: 0px;
                                            top: 0px; text-align: right;" />
                                    </a>
                                    <iframe id="FrmPopup" src="" width="620px" height="100%" frameborder="0" scrolling="auto"  >
                                    </iframe>
                                </asp:Panel>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <script language="javascript" type="text/javascript">
    function ConfirmDelete()
    {
        if( confirm('Are you sure ? you want to delete') == false )
        return false;
        else return true;
    }
    var styleToSelect;
        function onOk() {
            $get('Paragraph1').className = styleToSelect;
        }
        
        // Add click handlers for buttons to show and hide modal popup on pageLoad
        function pageLoad() {
            //$addHandler($get("showModalPopupClientButton"), 'click', showModalPopupViaClient);
            $addHandler($get("hideModalPopupViaClientButton"), 'click', hideModalPopupViaClient);        
        }
        
        function showModalPopupViaClient(ev) {
            ev.preventDefault();
            var modalPopupBehavior = $find('programmaticModalPopupBehavior');
            modalPopupBehavior.show();
            document.getElementById("FrmPopup").src = "";
        }
        
        function hideModalPopupViaClient(ev) {
            ev.preventDefault();
            var modalPopupBehavior = $find('programmaticModalPopupBehavior');
            modalPopupBehavior.hide();
            document.getElementById("FrmPopup").src = "";
        }
        function ShowSupportUserAndDongleUser()
        {
            var oWindow = window.radopen ("SupportUserBranch.aspx", "WinSupportUser");
            oWindow.SetSize (900, 700);
            oWindow.SetTitle ("Assign Support User Branch & Dongle");
            oWindow.Center();
            return false;
        } 
        function OpenSupportUserWindow(URLStr)
        {
            document.getElementById("FrmPopup").src = URLStr;
            var modalPopupBehavior = $find('programmaticModalPopupBehavior');
            modalPopupBehavior.show();        
        }
        function OpenAssignSupportUserWindow(URLStr)
        {           
            document.getElementById("FrmPopup").src = URLStr;
            var modalPopupBehavior = $find('programmaticModalPopupBehavior');
            modalPopupBehavior.show();
        }
        function OpenAssignVideoAccessToUserWindow(URLStr)
        {           
            document.getElementById("FrmPopup").src = URLStr;
            var modalPopupBehavior = $find('programmaticModalPopupBehavior');
            modalPopupBehavior.show();
        }
    </script>

</asp:Content>

