<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" EnableEventValidation="false"  CodeBehind="SupportManagement.aspx.cs" Inherits="TaskeroWeb.SupportManagement" %>

<%@ Register TagPrefix="custom" Namespace="Taskero.Business" Assembly="Taskero.Business" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="rad" %>
<%@ Register Src="~/Common/AdminMenu.ascx" TagName="AdminMenu" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <table width="940px" border="0" cellspacing="5" cellpadding="0" align="center" class="maintable">
                                        <tr>
                                            <td class="ERROR">
                                                <asp:Literal ID="LblError" runat="server"></asp:Literal>
                                                <asp:Literal ID="LTRL_ID" runat="server" Visible="False">0</asp:Literal>
                                            </td>
                                        </tr>
                                        <tr id="TRGridTable" runat="server">
                                            <td valign="top">
                                                <table cellspacing="1" cellpadding="2" border="0">
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="BTN_AddUser" runat="server" CssClass="BUTTON" OnClick="BTN_AddUser_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" style="margin-left: 40px">
                                                            <rad:RadGrid ID="RadGrid1" runat="server" AllowFilteringByColumn="True" AllowPaging="True"
                                                                AllowSorting="True" AutoGenerateColumns="False" GridLines="None" Width="920px"
                                                                PageSize="25" Skin="Green" OnNeedDataSource="RadGrid1_NeedDataSource" OnSortCommand="RadGrid1_SortCommand"
                                                                OnItemCommand="RadGrid1_ItemCommand" MasterTableView-EditFormSettings-EditFormType="Template"
                                                                OnItemCreated="RadGrid1_ItemCreated" OnItemDataBound="RadGrid1_ItemDataBound">
                                                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                                                <ClientSettings>
                                                                    <Scrolling AllowScroll="true" ScrollHeight="400" />
                                                                </ClientSettings>
                                                                <PagerStyle Position="Top" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <MasterTableView AllowNaturalSort="false">
                                                                    <Columns>
                                                                        <rad:GridBoundColumn DataField="LoginID" HeaderText="Username" UniqueName="column1"
                                                                            SortExpression="LoginID" ItemStyle-Width="130px" HeaderStyle-Width="130px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                        </rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="IsActive" HeaderText="" UniqueName="column10" Visible="false">
                                                                        </rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="LastName" HeaderText="Last Name" UniqueName="column2"
                                                                            SortExpression="LastName" ItemStyle-Width="95px" HeaderStyle-Width="95px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                        </rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="FirstName" HeaderText="First Name" UniqueName="column3"
                                                                            SortExpression="FirstName" ItemStyle-Width="95px" HeaderStyle-Width="95px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                        </rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="Email" HeaderText="Email" UniqueName="column4" SortExpression="Email"
                                                                            ItemStyle-Width="175px" HeaderStyle-Width="175px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                        </rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="CompanyName" HeaderText="Company" UniqueName="column5"
                                                                            SortExpression="CompanyName" ItemStyle-Width="130px" HeaderStyle-Width="130px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                        </rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="BranchName" HeaderText="Branch" UniqueName="column6"
                                                                            SortExpression="BranchName" ItemStyle-Width="135px" HeaderStyle-Width="135px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                        </rad:GridBoundColumn>
                                                                        <rad:GridTemplateColumn AllowFiltering="false" HeaderText="IsActive" ItemStyle-Width="55px"
                                                                            HeaderStyle-Width="55px" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Literal ID="LBL_IsAvtive" runat="server"></asp:Literal>
                                                                            </ItemTemplate>
                                                                        </rad:GridTemplateColumn>
                                                                        <rad:GridTemplateColumn AllowFiltering="false" HeaderText="Edit" ItemStyle-Width="40px"
                                                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="IMGBTN_Edit" runat="server" CommandName="EditUser" ImageUrl="~/Images/Edit-Icon.gif"
                                                                                    ToolTip="Edit" />
                                                                            </ItemTemplate>
                                                                        </rad:GridTemplateColumn>
                                                                        <rad:GridTemplateColumn AllowFiltering="false" HeaderText="Delete" ItemStyle-Width="45px"
                                                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="45px">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="IMGBTN_Delete" runat="server" CommandName="Delete" ImageUrl="~/Images/Delete-Icon.gif"
                                                                                    ToolTip="Delete" />
                                                                            </ItemTemplate>
                                                                        </rad:GridTemplateColumn>
                                                                    </Columns>
                                                                    <PagerStyle AlwaysVisible="True" Position="Top" />
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
                                                                        <div style="float: right">
                                                                            <asp:Button ID="BTN_Reset" TabIndex="48" runat="server" CssClass="BUTTON" CommandName="ResetFilter">
                                                                            </asp:Button>
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
                                        <tr id="TRMainTable" runat="server" align="center">
                                            <td>
                                                <table width="52%" border="0" cellspacing="1" cellpadding="2" class="form-text">
                                                    <tr>
                                                        <td valign="top" align="center">
                                                            <fieldset>
                                                                <legend>
                                                                    <asp:Literal ID="LTRL_SubTitle" runat="server"></asp:Literal>
                                                                </legend>
                                                                <table width="100%" border="0" cellspacing="1" cellpadding="2" class="form-text">
                                                                    <tr>
                                                                        <td align="right" style="width:130px">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="right" class="required-text-small">
                                                                            Fields marked with (<span class="ERROR">*</span>) are Required
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" valign="top">
                                                                            <asp:Literal ID="LTRL_RoleType" runat="server"></asp:Literal><span class="ERROR">*</span>&nbsp;:
                                                                        </td>
                                                                        <td>
                                                                            <asp:Literal ID="LTRL_User_Role" runat="server"></asp:Literal>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" valign="top">
                                                                            <asp:Literal ID="LTRL_CompanyName" runat="server"></asp:Literal><span class="ERROR">*</span>&nbsp;:
                                                                        </td>
                                                                        <td>
                                                                            <asp:Panel ID="PNL_Company" runat="server" ScrollBars="Auto" CssClass="CHECKBOXLIST">
                                                                                <asp:CheckBoxList ID="CHK_Company" runat="server" CssClass="CHECKBOX" AutoPostBack="true"
                                                                                    Width="200px" OnSelectedIndexChanged="DDL_Company_SelectedIndexChanged">
                                                                                </asp:CheckBoxList>
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" valign="top">
                                                                            <asp:Literal ID="LTRL_BranchName" runat="server"></asp:Literal><span class="ERROR">*</span>&nbsp;:
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:Panel ID="PNL_Branch" runat="server" ScrollBars="Auto" CssClass="CHECKBOXLIST">
                                                                                <asp:CheckBoxList ID="CHK_Branch" runat="server" CssClass="CHECKBOX" AutoPostBack="true"
                                                                                    Width="200px" OnSelectedIndexChanged="CHK_Branch_SelectedIndexChanged">
                                                                                </asp:CheckBoxList>
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" valign="top">
                                                                            <asp:Literal ID="LTRL_DongleName" runat="server"></asp:Literal><span class="ERROR">*</span>&nbsp;:
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:Panel ID="PNL_Dongle" runat="server" ScrollBars="Auto" CssClass="CHECKBOXLIST">
                                                                                <asp:CheckBoxList ID="CHK_Dongle" CssClass="CHECKBOX" runat="server" Width="200px">
                                                                                </asp:CheckBoxList>
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Literal ID="LTRL_FirstName" runat="server"></asp:Literal><span class="ERROR">*</span>&nbsp;:
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="TXT_FirstName" runat="server" MaxLength="30" CssClass="TEXTBOX" 
                                                                                Width="150px"></asp:TextBox>
                                                                            <ajax:FilteredTextBoxExtender ID="FIL_FirstName" runat="server" TargetControlID="TXT_FirstName"
                                                                                FilterType="LowercaseLetters,UppercaseLetters,Custom" ValidChars=" '">
                                                                            </ajax:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Literal ID="LTRL_LastName" runat="server"></asp:Literal><span class="ERROR">*</span>&nbsp;:
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="TXT_LastName" runat="server" MaxLength="30" CssClass="TEXTBOX" Width="150px"></asp:TextBox>
                                                                            <ajax:FilteredTextBoxExtender ID="FIL_LastName" runat="server" TargetControlID="TXT_LastName"
                                                                                FilterType="LowercaseLetters,UppercaseLetters,Custom" ValidChars=" '">
                                                                            </ajax:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Literal ID="LTRL_Email" runat="server"></asp:Literal><span class="ERROR">*</span>&nbsp;:
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="TXT_Email" runat="server" MaxLength="50" CssClass="TEXTBOX" Width="150px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" valign="top">
                                                                            <asp:Literal ID="LTRL_LoginID" runat="server"></asp:Literal><span class="ERROR">*</span>&nbsp;:
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="TXT_LoginID" runat="server" MaxLength="100" CssClass="TEXTBOX" Width="150px"></asp:TextBox>
                                                                            <ajax:FilteredTextBoxExtender ID="FIL_Username" runat="server" TargetControlID="TXT_LoginID"
                                                                                FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers">
                                                                            </ajax:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Literal ID="LTRL_LoginPass" runat="server"></asp:Literal><span class="ERROR">*</span>&nbsp;:
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="TXT_LoginPass" runat="server" TextMode="Password" MaxLength="20"
                                                                                CssClass="TEXTBOX" Width="150px"></asp:TextBox>
                                                                            <ajax:FilteredTextBoxExtender ID="FIL_Password" runat="server" TargetControlID="TXT_LoginPass"
                                                                                FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers">
                                                                            </ajax:FilteredTextBoxExtender>
                                                                            <ajax:PasswordStrength ID="PS_Password" runat="server" TargetControlID="TXT_LoginPass"
                                                                                DisplayPosition="RightSide" StrengthIndicatorType="Text" PreferredPasswordLength="15"
                                                                                PrefixText="Strength:" TextStrengthDescriptions="Very Poor;Weak;Average;Strong;Excellent"
                                                                                StrengthStyles="TextIndicator_Strength1;TextIndicator_Strength2;TextIndicator_Strength3;TextIndicator_Strength4;TextIndicator_Strength5"
                                                                                MinimumNumericCharacters="0" MinimumSymbolCharacters="0" RequiresUpperAndLowerCaseCharacters="false" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Literal ID="LTRL_LoginConfirmPassword" runat="server"></asp:Literal><span class="ERROR">
                                                                                *</span>&nbsp;:
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="TXT_LoginConfirmPassword" TextMode="Password" runat="server" MaxLength="20"
                                                                                Width="150px" CssClass="TEXTBOX"></asp:TextBox>
                                                                            <ajax:FilteredTextBoxExtender ID="FIL_ConformPassword" runat="server"
                                                                                TargetControlID="TXT_LoginConfirmPassword" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers">
                                                                            </ajax:FilteredTextBoxExtender>
                                                                            <asp:Button ID="BTN_DisplayPasswordField" runat="server" CssClass="BUTTON" Text="Show Password" ToolTip="Click here to show password field values" OnClick="BTN_DisplayPasswordField_Click" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">Allowed IP&nbsp;:</td>
                                                                        <td>
                                                                            <asp:TextBox ID="TXT_AllowedIP1" runat="server" MaxLength="3" Columns="3" CssClass="TEXTBOX"></asp:TextBox>&nbsp;.&nbsp;
                                                                            <asp:TextBox ID="TXT_AllowedIP2" runat="server" MaxLength="3" Columns="3" CssClass="TEXTBOX"></asp:TextBox>&nbsp;.&nbsp;
                                                                            <asp:TextBox ID="TXT_AllowedIP3" runat="server" MaxLength="3" Columns="3" CssClass="TEXTBOX"></asp:TextBox>&nbsp;.&nbsp;
                                                                            <asp:TextBox ID="TXT_AllowedIP4" runat="server" MaxLength="3" Columns="3" CssClass="TEXTBOX"></asp:TextBox>
                                                                            <ajax:FilteredTextBoxExtender ID="FTBE_AllowedIP1" runat="server" TargetControlID="TXT_AllowedIP1" FilterType="Numbers,Custom" ValidChars="*"></ajax:FilteredTextBoxExtender>
                                                                            <ajax:FilteredTextBoxExtender ID="FTBE_AllowedIP2" runat="server" TargetControlID="TXT_AllowedIP2" FilterType="Numbers,Custom" ValidChars="*"></ajax:FilteredTextBoxExtender>
                                                                            <ajax:FilteredTextBoxExtender ID="FTBE_AllowedIP3" runat="server" TargetControlID="TXT_AllowedIP3" FilterType="Numbers,Custom" ValidChars="*"></ajax:FilteredTextBoxExtender>
                                                                            <ajax:FilteredTextBoxExtender ID="FTBE_AllowedIP4" runat="server" TargetControlID="TXT_AllowedIP4" FilterType="Numbers,Custom" ValidChars="*"></ajax:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Literal ID="LTRL_Active" runat="server"></asp:Literal>
                                                                            &nbsp;:
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="DDL_Active" runat="server" CssClass="DROPDOWNLIST" Width="153px">
                                                                                <asp:ListItem Value="1" Text="Yes" Selected="True"></asp:ListItem>
                                                                                <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            Email when Yellow
                                                                            &nbsp;:
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="DDL_EmailWhenYellow" runat="server" CssClass="DROPDOWNLIST" Width="153px">
                                                                                <asp:ListItem Value="1" Text="Yes" Selected="True"></asp:ListItem>
                                                                                <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            Email when Red
                                                                            &nbsp;:
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="DDL_EmailWhenRed" runat="server" CssClass="DROPDOWNLIST" Width="153px">
                                                                                <asp:ListItem Value="1" Text="Yes" Selected="True"></asp:ListItem>
                                                                                <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" valign="top">
                                                                            Reports
                                                                            &nbsp;:
                                                                        </td>
                                                                        <td>
                                                                           <asp:Panel ID="PNL_Reports" runat="server" ScrollBars="Auto" CssClass="CHECKBOXLIST" Width="200px">
                                                                                <asp:CheckBoxList ID="CHK_Reports" CssClass="CHECKBOX" runat="server" Width="200px"></asp:CheckBoxList>
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2" align="center">
                                                                            <asp:Button ID="BTN_AddSave" runat="server" CssClass="BUTTON" OnClick="BTN_AddSave_Click" />
                                                                            &nbsp;&nbsp;<asp:Button ID="BTN_Cancel" runat="server" CssClass="BUTTON" OnClick="BTN_Cancel_Click" />
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </fieldset>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;
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
    </table>
    <script language="javascript">
    function display()
    {
        var BTN_Disp = document.getElementById('<%=BTN_DisplayPasswordField.ClientID %>');
        var Pass = document.getElementById('<%=TXT_LoginPass.ClientID %>');
        var ConfPass = document.getElementById('<%=TXT_LoginConfirmPassword.ClientID %>');
        if(Pass.getAttribute("type") == "password" && ConfPass.getAttribute("type") == "password") 
        {
            Pass.setAttribute("type","text");
            ConfPass.setAttribute("type","text");
        }
        else if(Pass.getAttribute("type") == "text" && ConfPass.getAttribute("type") == "text")
        {
            Pass.setAttribute("type","password");
            ConfPass.setAttribute("type","password");
        }
        
        if(BTN_Disp.value == "Show")
        {
            BTN_Disp.value = "Hide";
            BTN_Disp.title = "Click to hide password field values";
        }
        else if(BTN_Disp.value == "Hide")
        {
            BTN_Disp.value = "Show";
            BTN_Disp.title = "Click to show password field values";
        }
        return false;
    }
    </script>
</asp:Content>
