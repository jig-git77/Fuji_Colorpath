<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="VideoManagement.aspx.cs" Inherits="OrisWeb.VideoManagement" %>


<%@ Register Assembly="RadUpload.Net2" Namespace="Telerik.WebControls" TagPrefix="rad" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register TagPrefix="custom" Namespace="Oris.Business" Assembly="Oris.Business" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="rad" %>
<%@ Register Src="~/Common/AdminMenu.ascx" TagName="AdminMenu" TagPrefix="uc1" %>
<%@ Register Assembly="RadComboBox.Net2" Namespace="Telerik.WebControls" TagPrefix="rad" %>
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
                                        <asp:Literal ID="LTRL_Title" runat="server" ></asp:Literal>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td align="center" class="green-form-bg">
                                        
                                                <table width="940px" border="0" cellspacing="0" cellpadding="0" align="center" class="maintable">
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Label ID="LBL_Error" runat="server" CssClass="ERROR"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <ajax:TabContainer ID="TabContainer1" runat="server" Width="100%" style="padding:0px"  AutoPostBack="false">
                                                            
                                                    <ajax:TabPanel 
                                                        ID="TabPanVideos" 
                                                        Width="100%" 
                                                        runat="server" 
                                                        HeaderText="Videos" 
                                                        Style="padding:0px;width:100%">
                                                        
                                                        <ContentTemplate>
                                                        <asp:UpdatePanel ID="UP_Videos" runat="server" >
                                                        <ContentTemplate>
                                                            <div id="divVideo" runat="server" >
                                                                <table width="940px" border="0" cellspacing="0" cellpadding="0">
                                                                    <tr class="green-title" align="left">
                                                                        <td onclick="return PanelOnOffVideo();" style="padding-left:10px">
                                                                            <div style="float:left">
                                                                                <asp:Literal ID="LTRL_Videos" runat="server"></asp:Literal></div>
                                                                            <div id="divVideoOff" runat="server" style="display:none;float:right; padding-top:5px; padding-right:5px"><asp:ImageButton ID="ibtnVideoOff" runat="server" ImageUrl="~/Images/aerrow-up.gif" /></div>
                                                                                        <div id="divVideoOn" runat="server" style="float:right; padding-top:5px; padding-right:5px" ><asp:ImageButton ID="ibtnVideoOn" runat="server" ImageUrl="~/Images/aerrow-down.gif"  /></div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" class="green-form-bg">
                                                                            <div id="divAddVideo" runat="server" style="display:none;border:solid 0px #000">
                                                                                <table border="0" cellspacing="1" cellpadding="2" class="form-text">
                                                                                    <tr style="height:10px">
                                                                                        <td colspan="3"></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="right" valign="top">
                                                                                            <asp:Literal ID="LTRL_VideoCategorySubCategory" runat="server"></asp:Literal>
                                                                                        </td>
                                                                                        <td style="width:5px" align="right" valign="top"><span class="ERROR">*</span>&nbsp;:</td>
                                                                                        <td valign="top">
                                                                                            <asp:Literal ID="LTRL_VideoID" runat="server" Text="" Visible="false"></asp:Literal>
                                                                                            <rad:RadComboBox runat="server" ID="DDL_VideoCategorySubCategory" 
                                                                                                Width="153px">
                                                                                            </rad:RadComboBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="right" valign="top">
                                                                                            <asp:Literal ID="LTRL_LinkText" runat="server"></asp:Literal>
                                                                                        </td>
                                                                                        <td align="right" valign="top"><span class="ERROR">*</span>&nbsp;:</td>
                                                                                        <td valign="top">
                                                                                            <asp:TextBox CssClass="TEXTBOX" ID="TXT_LinkText" runat="server" MaxLength="100"
                                                                                                Width="150px"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="right" valign="top">
                                                                                            <asp:Literal ID="LTRL_UploadTo" runat="server" Text="Upload To"></asp:Literal>
                                                                                        </td>
                                                                                        <td align="right" valign="top">&nbsp;:</td>
                                                                                        <td valign="top" align="left">
                                                                                            <asp:RadioButtonList ID="RDO_UploadTo" runat="server" RepeatDirection="Horizontal">
                                                                                                <asp:ListItem Text="Taskero Server" Value="0"></asp:ListItem>
                                                                                                <asp:ListItem Text="Streamig Server" Value="1"></asp:ListItem>
                                                                                            </asp:RadioButtonList>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="right" valign="top">
                                                                                            <asp:Literal ID="LTRL_VideoDescription" runat="server"></asp:Literal>
                                                                                        </td>
                                                                                        <td align="right" valign="top">:</td>
                                                                                        <td valign="top">
                                                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                                                <tr>
                                                                                                    <td valign="top">
                                                                                                        <asp:TextBox CssClass="TEXTBOX" ID="TXT_VideoDescription" runat="server" MaxLength="2000"
                                                                                                            TextMode="MultiLine" Columns="60" Rows="5" Width="250px" onkeypress="CheckAlphaNumeric(event);"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td valign="bottom">
                                                                                                        <div style="float:left">&nbsp;</div><div id="divDescriptionLimit" runat="server" style="float:left" class="ERROR">2000</div>
                                                                                                        <div style="float:right">&nbsp;<asp:Literal ID="LTRL_DescriptionLimit" runat="server"></asp:Literal></div>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="right" valign="top">
                                                                                            <asp:Literal ID="LTRL_Keywords" runat="server"></asp:Literal>
                                                                                        </td>
                                                                                        <td align="right" valign="top">:</td>
                                                                                        <td valign="top">
                                                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                                                <tr>
                                                                                                    <td valign="top">
                                                                                                        <asp:TextBox CssClass="TEXTBOX" ID="TXT_KeyWords" runat="server" MaxLength="300"
                                                                                                            TextMode="MultiLine" Columns="60" Rows="5" Width="250px" onkeypress="CheckAlphaNumeric(event);"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td valign="bottom">
                                                                                                        <div style="float:left">&nbsp;</div><div id="divKeyWordsLimit" runat="server" style="float:left" class="ERROR">300</div>
                                                                                                        <div style="float:right">&nbsp;<asp:Literal ID="LTRL_KeywordsLimit" runat="server"></asp:Literal></div>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="right" valign="top">
                                                                                            <asp:Literal ID="LTRL_VideoFile" runat="server"></asp:Literal>
                                                                                        </td>
                                                                                        <td align="right" valign="top"><span class="ERROR">*</span>&nbsp;:</td>
                                                                                        <td  valign="top" align="left">
                                                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <rad:RadUpload ID="FU_VideoFile" runat="server" Width="255px" Skin="Inox" OnClientFileSelected="checkExtension" ControlObjectsVisibility="None" MaxFileInputsCount="1" />
                                                                                                    </td>
                                                                                                    <td align="left">&nbsp;<asp:Literal ID="LTRL_VideoFileName" runat="server"></asp:Literal></td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td colspan="2">
                                                                                                        <div id="divFileExtension" class="ERROR"></div>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                            <script type="text/javascript">
                                                                                                function checkExtension(radUpload, eventArgs) {
                                                                                                    var selectedFileName = eventArgs.FileInputField.value.toLowerCase();
                                                                                                    var selectedFileExt = selectedFileName.substring(selectedFileName.lastIndexOf(".") + 1);
                                                                                                    if (selectedFileExt == "mov" || selectedFileExt == "flv" || selectedFileExt == "wmv") {
                                                                                                        
                                                                                                        document.getElementById('divFileExtension').innerHTML = "";
                                                                                                    }
                                                                                                    else {
                                                                                                        clearSelectedFilesForUpload();
                                                                                                        document.getElementById('divFileExtension').innerHTML = "File should be .mov, .flv or .wmv format.";
                                                                                                    }
                                                                                                    return false;
                                                                                                }

                                                                                                function clearSelectedFilesForUpload() {
                                                                                                    var upload = GetRadUpload("<%= FU_VideoFile.ClientID %>");
                                                                                                    var fileInputCount = upload.GetFileInputs().length;

                                                                                                    for (var i = 0; i < fileInputCount; i++) {
                                                                                                        upload.ClearFileInputAt(i);
                                                                                                    }
                                                                                                }
                                                                                            </script>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>&nbsp;</td>
                                                                                        <td>&nbsp;</td>
                                                                                        <td valign="top">
                                                                                            <rad:RadProgressArea 
                                                                                                ID="RadProgressArea1" 
                                                                                                runat="server"
                                                                                                Skin="Inox"
                                                                                                >
                                                                                                </rad:RadProgressArea>
                                                                                            <rad:RadProgressManager ID="RadProgressManager1" runat="server" />
                                                                                            
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>&nbsp;</td>
                                                                                        <td>&nbsp;</td>
                                                                                        <td valign="top">
                                                                                            <asp:Button CssClass="BUTTON" ID="BTN_AddVideo" runat="server" OnClientClick="myStart();" OnClick="BTN_AddVideo_Click" />
                                                                                            <script language="javascript" type="text/javascript">
                                                                                                function myStart() {
                                                                                                    RadProgressManager.StartProgressPolling();
                                                                                                } 
                                                                                            </script>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr style="height:10px">
                                                                                        <td colspan="3">&nbsp;</td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height:5px"></td>
                                                                    </tr>
                                                                    <tr class="green-title" align="left" >
                                                                        <td style="padding-left:10px">
                                                                            <asp:Literal ID="LTRL_VideosList" runat="server" ></asp:Literal>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <div style="border:solid 1px #000">
                                                                            <rad:RadGrid ID="RG_Videos" runat="server" AutoGenerateColumns="false" PagerStyle-Mode="NextPrevAndNumeric"
                                                                                ShowHeader="true" AllowPaging="true" AllowSorting="true" AllowFilteringByColumn="true"
                                                                                Width="940px" OnPageIndexChanged="RG_Videos_PageIndexChanged" OnNeedDataSource="RG_Videos_NeedDataSource"
                                                                                Skin="Green" PageSize="25" OnSortCommand="RG_Videos_SortCommand" OnItemCommand="RG_Videos_ItemCommand"
                                                                                OnItemDataBound="RG_Videos_ItemDataBound" MasterTableView-EditFormSettings-EditFormType="Template"
                                                                                OnItemCreated="RG_Videos_ItemCreated">
                                                                                <ItemStyle HorizontalAlign="Left" Wrap="false" Width="920px" />
                                                                                <ClientSettings>
                                                                                    <Scrolling AllowScroll="true" ScrollHeight="400" />
                                                                                </ClientSettings>
                                                                                <PagerStyle Position="Top" />
                                                                                <HeaderStyle HorizontalAlign="Center" Width="920px" />
                                                                                <MasterTableView>
                                                                                    <PagerStyle Mode="NumericPages" Position="Top" Width="910px" />
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="920px" />
                                                                                    <Columns>
                                                                                        <rad:GridBoundColumn DataField="VideoID" HeaderText="VideoID" Visible="false" UniqueName="VideoID">
                                                                                        </rad:GridBoundColumn>
                                                                                        
                                                                                        <rad:GridBoundColumn 
                                                                                            HeaderText="Category/Sub-Category" 
                                                                                            DataField="CatDescription" 
                                                                                            SortExpression="CatDescription" 
                                                                                            AutoPostBackOnFilter="true" 
                                                                                            CurrentFilterFunction="Contains"
                                                                                            Visible="True" ItemStyle-Width="400px" HeaderStyle-Width="400px">
                                                                                        </rad:GridBoundColumn>
                                                                                        <rad:GridBoundColumn DataField="LinkText" HeaderText="Link Text" UniqueName="LinkText"
                                                                                            SortExpression="LinkText" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                                                            Visible="True" ItemStyle-Width="175px" HeaderStyle-Width="175px">
                                                                                        </rad:GridBoundColumn>
                                                                                        <rad:GridBoundColumn DataField="VideoFile" HeaderText="FileName" UniqueName="VideoFile"
                                                                                            SortExpression="VideoFile" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                                                            Visible="True" ItemStyle-Width="175px" HeaderStyle-Width="175px">
                                                                                        </rad:GridBoundColumn>
                                                                                        <rad:GridBoundColumn DataField="Keywords" HeaderText="Keywords" UniqueName="Keywords"
                                                                                            SortExpression="Keywords" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                                                            Visible="True" ItemStyle-Width="175px" HeaderStyle-Width="175px">
                                                                                        </rad:GridBoundColumn>
                                                                                        <rad:GridBoundColumn DataField="UploadedOn" HeaderText="Uploaded on" DataType="System.DateTime" UniqueName="UploadedOn" DataFormatString="{0:MM/dd/yyyy}"
                                                                                            Visible="false" ItemStyle-Width="125px" HeaderStyle-Width="125px">
                                                                                        </rad:GridBoundColumn>
                                                                                        <rad:GridTemplateColumn HeaderText="Video" AllowFiltering="false" ItemStyle-HorizontalAlign="Center"
                                                                                            ItemStyle-Width="50px" HeaderStyle-Width="50px">
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="IMGBTN_Video" runat="server" CommandName="VideoVideo" ImageUrl="~/Images/View-Icon-1.gif"
                                                                                                    ToolTip="Video" />
                                                                                            </ItemTemplate>
                                                                                        </rad:GridTemplateColumn>
                                                                                        <rad:GridTemplateColumn HeaderText="Edit" AllowFiltering="false" ItemStyle-HorizontalAlign="Center"
                                                                                            ItemStyle-Width="50px" HeaderStyle-Width="50px">
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="IMGBTN_Edit" runat="server" CommandName="EditVideo" ImageUrl="~/Images/Edit-Icon.gif"
                                                                                                    ToolTip="Edit" />
                                                                                            </ItemTemplate>
                                                                                        </rad:GridTemplateColumn>
                                                                                        <rad:GridTemplateColumn HeaderText="Delete" AllowFiltering="false" ItemStyle-HorizontalAlign="Center"
                                                                                            ItemStyle-Width="50px" HeaderStyle-Width="50px">
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="IMGBTN_Delete" runat="server" CommandName="Delete" ImageUrl="~/Images/Delete-Icon.gif"
                                                                                                    ToolTip="Delete" />
                                                                                            </ItemTemplate>
                                                                                        </rad:GridTemplateColumn>
                                                                                    </Columns>
                                                                                    <PagerStyle AlwaysVisible="True" />
                                                                                    <PagerTemplate>
                                                                                        <div style="float: left">
                                                                                            <asp:Literal ID="LTRL_RecordText" runat="server"></asp:Literal>
                                                                                            <asp:ImageButton ID="LBTN_First" CommandName="Page" CausesValidation="false" CommandArgument="First"
                                                                                                runat="server" ImageUrl="Images/Nav_FirstPage_off.gif" align="absmiddle"></asp:ImageButton>
                                                                                            <asp:ImageButton ID="LBTN_Prev" CommandName="Page" CausesValidation="false" CommandArgument="Prev"
                                                                                                runat="server" ImageUrl="Images/Nav_PreviousPage_off.gif" align="absmiddle">
                                                                                            </asp:ImageButton>
                                                                                            <asp:ImageButton ID="LBTN_Next" CommandName="Page" CausesValidation="false" CommandArgument="Next"
                                                                                                runat="server" ImageUrl="Images/Nav_NextPage_off.gif" align="absmiddle"></asp:ImageButton>
                                                                                            <asp:ImageButton ID="LBTN_Last" CommandName="Page" CausesValidation="false" CommandArgument="Last"
                                                                                                runat="server" ImageUrl="Images/Nav_LastPage_off.gif" align="absmiddle"></asp:ImageButton>
                                                                                            &nbsp;
                                                                                            <asp:TextBox ID="tbPageNumber" runat="server" Columns="3" Text='<%# (int)DataBinder.Eval(Container, "OwnerTableView.CurrentPageIndex") + 1 %>'
                                                                                                Visible="False" />
                                                                                            <asp:LinkButton ID="LBTN_Go" runat="server" CommandName="CustomChangePage" Visible="False">Go</asp:LinkButton>
                                                                                            <font class="text">| Records</font>
                                                                                            <asp:DropDownList ID="DDL_RecordPageVideo" runat="server" AutoPostBack="true" CommandName="Page"
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
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:PostBackTrigger ControlID="BTN_AddVideo" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </ContentTemplate>
                                                    </ajax:TabPanel>
                                                    <ajax:TabPanel ID="TabPanCategories" Width="100%" runat="server" HeaderText="Categories" >
                                                        <ContentTemplate>
                                                            <asp:UpdatePanel ID="UP_Categories" runat="server">
                                                                <ContentTemplate>
                                                                    <div id="divCategories" runat="server" >
                                                                <table width="940px" border="0" cellspacing="0" cellpadding="0">
                                                                    <tr class="green-title" align="left">
                                                                        <td onclick="return PanelOnOff();" style="padding-left:10px">
                                                                            <div style="float:left"><asp:Literal ID="LTRL_Categories" runat="server"></asp:Literal></div>
                                                                            <div id="divCategoryOff" runat="server" style="display:none;float:right; padding-top:5px; padding-right:5px"><asp:ImageButton ID="ibtnOff" runat="server" ImageUrl="~/Images/aerrow-up.gif"  /></div>
                                                                                        <div id="divCategoryOn" runat="server" style="float:right; padding-top:5px; padding-right:5px" ><asp:ImageButton ID="ibtnOn" runat="server" ImageUrl="~/Images/aerrow-down.gif" /></div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" class="green-form-bg">
                                                                            <div id="divAddCategory" runat="server" style="display:none;border:solid 0px #000">
                                                                                <table border="0" cellspacing="1" cellpadding="2" class="form-text">
                                                                                    <tr>
                                                                                        <td align="right">
                                                                                            <asp:Literal ID="LTRL_CategorySubCategory" runat="server"></asp:Literal><span class="ERROR">*</span>&nbsp;:
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Literal ID="LTRL_CategoryID" runat="server" Text="" Visible="false"></asp:Literal>
                                                                                            <rad:RadComboBox runat="server" ID="DDL_Categories"  Width="153px">
                                                                                            </rad:RadComboBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="right">
                                                                                            <asp:Literal ID="LTRL_CategoryDescription" runat="server"></asp:Literal><span class="ERROR">*</span>&nbsp;:
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox CssClass="TEXTBOX" ID="TXT_CategoryDescription" runat="server" MaxLength="60"
                                                                                                Width="150px" onkeypress="CheckAlphaNumeric(event);"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="2" align="center">
                                                                                            <asp:Button CssClass="BUTTON" ID="BTN_AddCategory" runat="server"
                                                                                                OnClick="BTN_AddCategory_Click" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height:5px"></td>
                                                                    </tr>
                                                                    <tr class="green-title" align="left">
                                                                        <td style="padding-left:10px">
                                                                            <asp:Literal ID="LTRL_CategoriesList" runat="server" ></asp:Literal>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <div style="border:solid 1px #000">
                                                                            <rad:RadGrid ID="RG_Categories" runat="server" AutoGenerateColumns="false" PagerStyle-Mode="NextPrevAndNumeric"
                                                                                ShowHeader="true" AllowPaging="true" AllowSorting="true" AllowFilteringByColumn="true"
                                                                                Width="940px" OnPageIndexChanged="RG_Categories_PageIndexChanged" OnNeedDataSource="RG_Categories_NeedDataSource"
                                                                                Skin="Green" PageSize="25" OnSortCommand="RG_Categories_SortCommand" OnItemCommand="RG_Categories_ItemCommand"
                                                                                OnItemDataBound="RG_Categories_ItemDataBound" MasterTableView-EditFormSettings-EditFormType="Template"
                                                                                OnItemCreated="RG_Categories_ItemCreated">
                                                                                <ItemStyle HorizontalAlign="Left" Wrap="false" Width="920px" />
                                                                                <ClientSettings>
                                                                                    <Scrolling AllowScroll="true" ScrollHeight="400" />
                                                                                </ClientSettings>
                                                                                <PagerStyle Position="Top" />
                                                                                <HeaderStyle HorizontalAlign="Center" Width="920px" />
                                                                                <MasterTableView>
                                                                                    <PagerStyle Mode="NumericPages" Position="Top" Width="910px" />
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="920px" />
                                                                                    <Columns>
                                                                                        <rad:GridBoundColumn DataField="CategoryID" HeaderText="CategoryID" Visible="false"
                                                                                            UniqueName="CategoryID">
                                                                                        </rad:GridBoundColumn>
                                                                                        <rad:GridBoundColumn HeaderText="Parent Category" 
                                                                                            SortExpression="ParentDescription" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                                                            DataField="ParentDescription" Visible="True">
                                                                                        </rad:GridBoundColumn>
                                                                                        <rad:GridBoundColumn DataField="ParentID" 
                                                                                            HeaderText="ParentID" Visible="false" UniqueName="ParentID">
                                                                                        </rad:GridBoundColumn>
                                                                                        <rad:GridBoundColumn DataField="Description" 
                                                                                            SortExpression="Description" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                                                            HeaderText="Category/Sub-Category" UniqueName="Description"
                                                                                            Visible="True" ItemStyle-Width="350px" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="350px">
                                                                                        </rad:GridBoundColumn>
                                                                                        <rad:GridTemplateColumn HeaderText="Edit" AllowFiltering="false" ItemStyle-HorizontalAlign="Center"
                                                                                            ItemStyle-Width="50px" HeaderStyle-Width="50px">
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="EditCategory" ImageUrl="~/Images/Edit-Icon.gif"
                                                                                                    ToolTip="Edit" />
                                                                                            </ItemTemplate>
                                                                                        </rad:GridTemplateColumn>
                                                                                        <rad:GridTemplateColumn HeaderText="Delete" AllowFiltering="false" ItemStyle-HorizontalAlign="Center"
                                                                                            ItemStyle-Width="50px" HeaderStyle-Width="50px">
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Delete" ImageUrl="~/Images/Delete-Icon.gif"
                                                                                                    ToolTip="Delete" />
                                                                                            </ItemTemplate>
                                                                                        </rad:GridTemplateColumn>
                                                                                        <rad:GridTemplateColumn HeaderText="Permissions" AllowFiltering="false" ItemStyle-HorizontalAlign="Center"
                                                                                            ItemStyle-Width="50px" HeaderStyle-Width="50px">
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="IMGBTN_Permissions" runat="server" CommandName="Permissions" ImageUrl="~/Images/Icon-Assign-Support-User.gif"
                                                                                                    ToolTip="Permissions" />
                                                                                            </ItemTemplate>
                                                                                        </rad:GridTemplateColumn>
                                                                                    </Columns>
                                                                                    <PagerStyle AlwaysVisible="True" />
                                                                                    <PagerTemplate>
                                                                                        <div style="float: left">
                                                                                            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                                                                            <asp:ImageButton ID="ImageButton3" CommandName="Page" CausesValidation="false" CommandArgument="First"
                                                                                                runat="server" ImageUrl="Images/Nav_FirstPage_off.gif" align="absmiddle"></asp:ImageButton>
                                                                                            <asp:ImageButton ID="ImageButton4" CommandName="Page" CausesValidation="false" CommandArgument="Prev"
                                                                                                runat="server" ImageUrl="Images/Nav_PreviousPage_off.gif" align="absmiddle">
                                                                                            </asp:ImageButton>
                                                                                            <asp:ImageButton ID="ImageButton5" CommandName="Page" CausesValidation="false" CommandArgument="Next"
                                                                                                runat="server" ImageUrl="Images/Nav_NextPage_off.gif" align="absmiddle"></asp:ImageButton>
                                                                                            <asp:ImageButton ID="ImageButton6" CommandName="Page" CausesValidation="false" CommandArgument="Last"
                                                                                                runat="server" ImageUrl="Images/Nav_LastPage_off.gif" align="absmiddle"></asp:ImageButton>
                                                                                            &nbsp;
                                                                                            <asp:TextBox ID="TextBox1" runat="server" Columns="3" Text='<%# (int)DataBinder.Eval(Container, "OwnerTableView.CurrentPageIndex") + 1 %>'
                                                                                                Visible="False" />
                                                                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="CustomChangePage" Visible="False">Go</asp:LinkButton>
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
                                                                                            <asp:Button ID="Button1" TabIndex="48" runat="server" CssClass="BUTTON" CommandName="ResetFilter">
                                                                                            </asp:Button>
                                                                                            &nbsp;
                                                                                        </div>
                                                                                    </PagerTemplate>
                                                                                </MasterTableView>
                                                                            </rad:RadGrid>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </ContentTemplate>
                                                    </ajax:TabPanel>
                                                </ajax:TabContainer>
                                                        </td>
                                                    </tr>
                                                    
                                                </table>
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
                                    BackgroundCssClass="modalBackgroundPopupVideo" DropShadow="false" PopupDragHandleControlID="programmaticPopupDragHandle">
                                </ajax:ModalPopupExtender>
                                <asp:Panel runat="server" ID="programmaticPopup" Style="display: none; height: 530px;
                                    background-color: none;">
                                    
                                    <iframe id="FrmPopup" src="" width="655px" height="595px" allowtransparency="true" frameborder="0" scrolling="no" style="border:0px solid #006699;">
                                    </iframe>
                                </asp:Panel>
                                <asp:Button runat="server" ID="hiddenTargetControlForModalPopup1" Style="display: none" />
                                <ajax:ModalPopupExtender runat="server" ID="programmaticModalPopup1" BehaviorID="programmaticModalPopupBehavior1"
                                    TargetControlID="hiddenTargetControlForModalPopup1" PopupControlID="programmaticPopup1"
                                    BackgroundCssClass="modalBackground" DropShadow="false" PopupDragHandleControlID="programmaticPopupDragHandle">
                                </ajax:ModalPopupExtender>
                                <asp:Panel runat="server" ID="programmaticPopup1" Style="display: none; height: 530px;
                                    background-color: White;">
                                    <a id="hideModalPopupViaClientButton1" href="#" style="border: none; text-decoration: none;">
                                                <img src="Images/Close2.png" style="position: absolute; border: none; right: 0px;
                                                    top: 0px; text-align: right;" /></a>
                                    <iframe id="FrmPopup1" src="" width="620px" height="530" frameborder="0" scrolling="no">
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
        function PanelOnOff() {
            
            if(document.getElementById('<%= divAddCategory.ClientID %>').style.display == 'none')
            {
                document.getElementById('<%= divAddCategory.ClientID %>').style.display = 'block';
                document.getElementById('<%= divCategoryOff.ClientID %>').style.display = 'block';
                document.getElementById('<%= divCategoryOn.ClientID %>').style.display = 'none';
            }
            else
            {
                document.getElementById('<%= divAddCategory.ClientID %>').style.display = 'none';
                document.getElementById('<%= divCategoryOff.ClientID %>').style.display = 'none';
                document.getElementById('<%= divCategoryOn.ClientID %>').style.display = 'block';
           }
           return false;
        }
        function PanelOnOffVideo() {
            if(document.getElementById('<%= divAddVideo.ClientID %>').style.display == 'none')
            {
                document.getElementById('<%= divAddVideo.ClientID %>').style.display = 'block';
                document.getElementById('<%= divVideoOff.ClientID %>').style.display = 'block';
                document.getElementById('<%= divVideoOn.ClientID %>').style.display = 'none';
            }
            else
            {
                document.getElementById('<%= divAddVideo.ClientID %>').style.display = 'none';
                document.getElementById('<%= divVideoOff.ClientID %>').style.display = 'none';
                document.getElementById('<%= divVideoOn.ClientID %>').style.display = 'block';
           }
           return false;
        }
        // Add click handlers for buttons to show and hide modal popup on pageLoad
        function pageLoad() {
            $addHandler($get("hideModalPopupViaClientButton1"), 'click', hideModalPopupViaClient1);        
        }
        
        function showModalPopupViaClient(ev) {
            ev.preventDefault();
            var modalPopupBehavior = $find('programmaticModalPopupBehavior1');
            modalPopupBehavior.show();
            document.getElementById("FrmPopup1").src = "";
        }
        
        function hideModalPopupViaClient(ev) {
            ev.preventDefault();
            var modalPopupBehavior = $find('programmaticModalPopupBehavior');
            modalPopupBehavior.hide();
            document.getElementById("FrmPopup").src = "";
        }
        function hideModalPopupViaClient1(ev) {
            ev.preventDefault();
            var modalPopupBehavior = $find('programmaticModalPopupBehavior1');
            modalPopupBehavior.hide();
            document.getElementById("FrmPopup1").src = "";
        }
        function OpenUserPermissionWindow(URLStr)
        {           
            document.getElementById("FrmPopup1").src = URLStr;
            var modalPopupBehavior = $find('programmaticModalPopupBehavior1');
            modalPopupBehavior.show();
            return false;
        }
        function OpenVideoFile(URLStr)
        {           
            document.getElementById("FrmPopup").src = URLStr;
            var modalPopupBehavior = $find('programmaticModalPopupBehavior');
            modalPopupBehavior.show();
            return false;
        }
        function CountCharacter(strTextBox,maxCharacter)
        {
            if(strTextBox == 'Description')
            {
                var remainingCharacter = maxCharacter - document.getElementById('<%= TXT_VideoDescription.ClientID %>').value.length;
                if(remainingCharacter <= 0 )
                    remainingCharacter = 0;
                if(document.getElementById('<%= TXT_VideoDescription.ClientID %>').value.length > maxCharacter)
                    document.getElementById('<%= TXT_VideoDescription.ClientID %>').value =document.getElementById('<%= TXT_VideoDescription.ClientID %>').value.substr(0,maxCharacter);
                document.getElementById('<%= divDescriptionLimit.ClientID %>').innerHTML =  remainingCharacter;
            }else if(strTextBox == 'KeyWords')
            {
                var remainingCharacter = maxCharacter - document.getElementById('<%= TXT_KeyWords.ClientID %>').value.length;
                if(remainingCharacter <= 0 )
                    remainingCharacter = 0;
                if(document.getElementById('<%= TXT_KeyWords.ClientID %>').value.length > maxCharacter)
                    document.getElementById('<%= TXT_KeyWords.ClientID %>').value =document.getElementById('<%= TXT_KeyWords.ClientID %>').value.substr(0,maxCharacter);
                document.getElementById('<%= divKeyWordsLimit.ClientID %>').innerHTML =  remainingCharacter;
            }
            return false;
        }
    </script>

</asp:Content>
