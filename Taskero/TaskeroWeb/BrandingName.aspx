<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="BrandingName.aspx.cs" Inherits="TaskeroWeb.BrandingName" %>

<%@ Register TagPrefix="custom" Namespace="Taskero.Business" Assembly="Taskero.Business" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="rad" %>
<%@ Register Src="~/Common/AdminMenu.ascx" TagName="AdminMenu" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="JS/ColorPicker/picker.js"></script>

    <script language="javascript" type="text/javascript">  
   
  //Trim the input text
  function Trim(input)
  {
    var lre = /^\s*/; 
    var rre = /\s*$/; 
    input = input.replace(lre, ""); 
    input = input.replace(rre, ""); 
    return input; 
   }
 
   function CheckUploadFileFile() 
   {
      //Setting the File Name and extension array for diff. type of text files 
        if(document.getElementById('<%=TXT_BrandingName.ClientID%>').value == '')
        {
            document.getElementById('<%=LblError.ClientID%>').innerHTML = "<%= Resources.Message.BrandingNameRequired %>";
            return false;
        }
        else
        {            
            var Res = true;
            if (Res == true)
            {
                file = document.getElementById('<%=FUPLD_HeaderLogo.ClientID%>');
                extArray = new Array(".jpg", ".gif",".png",".jpeg",".tif",".bmp"); 
                Message = "<%= Resources.Message.HeaderLogoRequired %>";
                Message1 = "<%= Resources.Message.JPGJIFFileRequired  %>";
                Res = CheckForTestFile(file,extArray,Message,Message1);
                if (Res == true)
                {
                    file = document.getElementById('<%=FUPLD_LargeImage.ClientID%>');
                    extArray = new Array(".jpg", ".gif",".png",".jpeg",".tif",".bmp"); 
                    Message = "<%= Resources.Message.LargeImageRequired %>";
                    Message1 = "<%= Resources.Message.JPGJIFFileRequired %>";
                    Res = CheckForTestFile(file,extArray,Message,Message1);
                    if (Res == true)
                         return true;
                    else
                         return false;
                }
                else
                    return false;
            }
            else
                return false;
       }
   }
   /*--------------- filter the files before Uploading for text file only    ----------------*/
   function CheckForTestFile(file,extArray,Message,Message1) 
   {
        var fileName=file.value;        
        //Checking for file browsed or not 
        if (Trim(fileName) =='' )
        {
            //document.getElementById('<%=LblError.ClientID%>').innerHTML = Message;
            //file.focus();
            return true;
        }
       //getting the file name
       while (fileName.indexOf("\\") != -1)
         fileName = fileName.slice(fileName.indexOf("\\") + 1);
 
       //Getting the file extension                     
       var ext = fileName.slice(fileName.indexOf(".")).toLowerCase();
 
       //matching extension with our given extensions.
       for (var i = 0; i < extArray.length; i++) 
       {
         if (extArray[i] == ext) 
         { 
           return true;
         }
       }  
         document.getElementById('<%=LblError.ClientID%>').innerHTML = Message1;
         return false;                
   }
    /*--------------- Function to ensure only numeric value with a single . 'DOT' value   ----------------*/
    function CheckHashValue(obj,e)
    {
        key = (window.event) ? e.keyCode : e.which;
        //alert(obj);
        if ((key>=48 && key<=57) || key == 35 || (key>=65 && key<=90) || (key>=97 && key<=122) )
        {
            if (key==35)
            {
                var text;
                text=obj.value;
                //alert(text);
                if(text.toString().indexOf(".")!=-1)
                    RestoreEvent(e)
            }
        }
        else
        {
            RemoveCharKeyValue(e);
        }
    }
    /*--------------- Function to null the current event generated  ----------------*/
    function RestoreEvent(e)
    {
        if (navigator.appName == "Microsoft Internet Explorer") // To Check the browser is IE
            e.returnValue = null;
        else
            e.preventDefault();
    }
    /*--------------- Function used to remove the character and special characters event ----------------------*/
    function RemoveCharKeyValue(e)
    {
        /*find the key value of the key that is pressed*/
        key = (window.event) ? e.keyCode : e.which; 
        /*check whether the key is pressed with the shift key */
        if ( e.shiftKey && ((key>=58 && key<=126)  || (key>=33 && key<=47)))
        {
            RestoreEvent(e);
        }
        /*if the normal keys are pressed*/
        if ((key>=58 && key<=126) || (key>=42 && key<=47) || key==39  || key==32) 
        {
            RestoreEvent(e);
        }
    }
    /*--------------- Function used to check the input value of the color ----------------*/
    function FillColor(objVal,objDisplay)
    {
        if(trim(objVal.value) != "")
        {
            if( objVal.value.length < 7 || objVal.value.toString().indexOf("#")== -1 )
            {
                objVal.focus();
                alert('Please enter valid hex number');
            }
            else document.getElementById(objDisplay).style.backgroundColor=objVal.value;
        }
        else document.getElementById(objDisplay).style.backgroundColor="#FFFFFF";
    }
    /*--------------- Function used to trim the given string ----------------*/
    function trim(stringToTrim) 
    {
        return stringToTrim.replace(/^\s+|\s+$/g,"");
    }
    </script>

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
                            <asp:Label ID="LBL_Title" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="green-form-bg">
                            <table width="940px" border="0" cellspacing="5" cellpadding="0" align="center" class="maintable">
                                <tr>
                                    <td>
                                        <asp:Label ID="LblError" runat="server" CssClass="ERROR"></asp:Label>
                                        <asp:Label ID="LBL_ID" runat="server" Visible="False">0</asp:Label>
                                    </td>
                                </tr>
                                <tr id="TRGridTable" runat="server">
                                    <td valign="top">
                                        <table cellspacing="1" cellpadding="2" border="0">
                                            <tr>
                                                <td>
                                                    <asp:Button ID="BTN_AddBrand" runat="server" CssClass="BUTTON" OnClick="BTN_AddBrand_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <rad:RadGrid ID="GridBrandData" runat="server" AllowFilteringByColumn="True" AllowPaging="True"
                                                        AllowSorting="True" AutoGenerateColumns="False" GridLines="None" Width="920px"
                                                        PageSize="25" Skin="Green" OnNeedDataSource="GridBrandData_NeedDataSource" OnSortCommand="GridBrandData_SortCommand"
                                                        OnItemCommand="GridBrandData_ItemCommand" MasterTableView-EditFormSettings-EditFormType="Template"
                                                        OnItemCreated="GridBrandData_ItemCreated" OnItemDataBound="GridBrandData_ItemDataBound">
                                                        <ItemStyle HorizontalAlign="Left" Wrap="false" Width="920px" />
                                                        <ClientSettings>
                                                            <Scrolling AllowScroll="true" ScrollHeight="400" />
                                                        </ClientSettings>
                                                        <PagerStyle Position="Top" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <MasterTableView AllowNaturalSort="false">
                                                            <Columns>
                                                                <rad:GridBoundColumn DataField="BrandID" HeaderText="BrandID" Visible="false"></rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="BrandName" HeaderText="Brand Name" UniqueName="column2"
                                                                    SortExpression="BrandName" ItemStyle-Width="815px" HeaderStyle-Width="815px"
                                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="Css" Visible="false"></rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="HeaderLogo" Visible="false"></rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="LargeImage" Visible="false"></rad:GridBoundColumn>                                                                
                                                                <rad:GridBoundColumn DataField="TopLine" Visible="false"></rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="MainTabMenuText" Visible="false"></rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="MainTabMenuTextOver" Visible="false"></rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="MainTabMenuBack" Visible="false"></rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="MainTabMenuBackOver" Visible="false"></rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="MainTabMenuBorder" Visible="false"></rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="MainTabMenuBorderOver" Visible="false"></rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="MenuBottomLine" Visible="false"></rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="SubTabMenuText" Visible="false"></rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="SubTabMenuTextOver" Visible="false"></rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="MainForm" Visible="false"></rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="Footer" Visible="false"></rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="FooterText" Visible="false"></rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="CompanyID" Visible="false"></rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="CompanyName" HeaderText="Company" SortExpression="CompanyName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="45%"></rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="PopUpTabMainColor" Visible="false"></rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="PopUpTabText" Visible="false"></rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="PopUpTabSelectedText" Visible="false"></rad:GridBoundColumn>
                                                                <rad:GridTemplateColumn AllowFiltering="false" HeaderText="Edit" ItemStyle-HorizontalAlign="Center"
                                                                    HeaderStyle-Width="50px" ItemStyle-Width="50px">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="IMGBTN_Edit" runat="server" CommandName="Edit" ImageUrl="~/Images/Edit-Icon.gif"
                                                                            ToolTip="Edit" />
                                                                    </ItemTemplate>
                                                                </rad:GridTemplateColumn>
                                                                <rad:GridTemplateColumn AllowFiltering="false" HeaderText="Delete" ItemStyle-Width="50px"
                                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50px">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="IMGBTN_Delete" runat="server" CommandName="Delete" ImageUrl="~/Images/Delete-Icon.gif"
                                                                            ToolTip="Delete" />
                                                                    </ItemTemplate>
                                                                </rad:GridTemplateColumn>
                                                            </Columns>
                                                            <PagerStyle AlwaysVisible="True" Position="Top" />
                                                            <PagerTemplate>
                                                                <div style="float: left">
                                                                    <asp:Label ID="LBL_RecordText" runat="server"></asp:Label>&nbsp;
                                                                    <asp:ImageButton ID="lbFirst" CommandName="Page" CausesValidation="false" CommandArgument="First"
                                                                        runat="server" ImageUrl="Images/Nav_FirstPage_off.gif" align="absmiddle"></asp:ImageButton>
                                                                    <asp:ImageButton ID="lbPrev" CommandName="Page" CausesValidation="false" CommandArgument="Prev"
                                                                        runat="server" ImageUrl="Images/Nav_PreviousPage_off.gif" align="absmiddle">
                                                                    </asp:ImageButton>
                                                                    <asp:ImageButton ID="lbNext" CommandName="Page" CausesValidation="false" CommandArgument="Next"
                                                                        runat="server" ImageUrl="Images/Nav_NextPage_off.gif" align="absmiddle"></asp:ImageButton>
                                                                    <asp:ImageButton ID="lbLast" CommandName="Page" CausesValidation="false" CommandArgument="Last"
                                                                        runat="server" ImageUrl="Images/Nav_LastPage_off.gif" align="absmiddle"></asp:ImageButton>
                                                                    &nbsp;
                                                                    <asp:TextBox ID="tbPageNumber" runat="server" Columns="3" Text='<%# (int)DataBinder.Eval(Container, "OwnerTableView.CurrentPageIndex") + 1 %>'
                                                                        Visible="False" />
                                                                    <asp:LinkButton ID="lbGo" runat="server" CommandName="CustomChangePage" Visible="False">Go</asp:LinkButton>
                                                                    <font class="text">| Records</font>
                                                                    <asp:DropDownList ID="DDlRecordPage" runat="server" AutoPostBack="true" CommandName="Page"
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
                                                                    <asp:Button ID="btnReset" TabIndex="48" runat="server" CssClass="BUTTON" CommandName="ResetFilter">
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
                                <tr id="TRMainTable" runat="server" visible="false">
                                    <td align="center">
                                        <table width="500px" border="0" cellspacing="1" cellpadding="2" class="form-text" align="center">
                                            <tr>
                                                <td valign="top">
                                                    <fieldset>
                                                        <legend><asp:Label ID="LBL_SubTitle" runat="server" Font-Bold="true" Style="color: Black"></asp:Label></legend>
                                                        <table width="100%" border="0" cellspacing="1" cellpadding="2" class="form-text">
                                                            <tr>
                                                                <td style="width: 100px; height: 1px;"></td>
                                                                <td style="width: 200px; height: 1px;"></td>
                                                                <td style="width: 100px; height: 1px;"></td>
                                                                <td style="width: 50px; height: 1px;"></td>
                                                            </tr>
                                                            <tr>
                                                                <td><a href="BrandingHelp.htm" target="0" class="LINKBUTTON2">Need Help?</a></td>
                                                                <td align="right" class="required-text-small" colspan="3">
                                                                    Fields marked with (<span class="ERROR">*</span>) are Required.
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Company</td>
                                                                <td><asp:DropDownList ID="DDL_CompanyID" runat="server" CssClass="DROPDOWNLIST"></asp:DropDownList></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="LBL_BrandingName" runat="server"></asp:Label><span class="ERROR"> *</span>
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:TextBox ID="TXT_BrandingName" runat="server" MaxLength="100" CssClass="TEXTBOX"
                                                                        Width="150px"></asp:TextBox>
                                                                        <asp:Literal ID="LTRL_Css" runat="server" Visible="false"></asp:Literal>
                                                                    <asp:HiddenField ID="HFBrandID" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap>
                                                                    1) Top Line
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:literal id="LTRL_TopLine" runat="server" visible="false"></asp:literal>
                                                                    <asp:textbox id="TXT_TopLine" runat="server" cssclass="TEXTBOX" columns="10" maxlength="7" onkeypress="CheckHashValue(this,event)"></asp:textbox>
                                                                    <a href="javascript:TCP.popup(document.forms[0].elements['<%=TXT_TopLine.ClientID%>'], 3 , document.forms[0].elements['<%=TXT_Color_TopLine.ClientID%>'] )"><img src="JS/ColorPicker/Forecolor.gif" border="0" alt="Click Here to Pick up the color" align="absMiddle"></a>
                                                                    <asp:textbox id="TXT_Color_TopLine" readonly="true" runat="server" CssClass="TEXTBOX2"></asp:textbox>
                                                                    <ajax:filteredtextboxextender id="FTBE_TopLine" runat="server" targetcontrolid="TXT_TopLine" filtertype="Custom,LowercaseLetters,UppercaseLetters,Numbers" validchars="#"></ajax:filteredtextboxextender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap>
                                                                    2) Main Tab Menu Text
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:literal id="LTRL_MainTabMenuText" runat="server" visible="false"></asp:literal>
                                                                    <asp:textbox id="TXT_MainTabMenuText" runat="server" cssclass="TEXTBOX" columns="10" maxlength="7" onkeypress="CheckHashValue(this,event)"></asp:textbox>
                                                                    <a href="javascript:TCP.popup(document.forms[0].elements['<%=TXT_MainTabMenuText.ClientID%>'], 3 , document.forms[0].elements['<%=TXT_Color_MainTabMenuText.ClientID%>'] )"><img src="JS/ColorPicker/Forecolor.gif" border="0" alt="Click Here to Pick up the color" align="absMiddle"></a>
                                                                    <asp:textbox id="TXT_Color_MainTabMenuText" readonly="true" runat="server" CssClass="TEXTBOX2"></asp:textbox>
                                                                    <ajax:filteredtextboxextender id="FTBE_MainTabMenuText" runat="server" targetcontrolid="TXT_MainTabMenuText" filtertype="Custom,LowercaseLetters,UppercaseLetters,Numbers" validchars="#"></ajax:filteredtextboxextender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap>
                                                                    3) Main Tab Menu Text Over
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:literal id="LTRL_MainTabMenuTextOver" runat="server" visible="false"></asp:literal>
                                                                    <asp:textbox id="TXT_MainTabMenuTextOver" runat="server" cssclass="TEXTBOX" columns="10" maxlength="7" onkeypress="CheckHashValue(this,event)"></asp:textbox>
                                                                    <a href="javascript:TCP.popup(document.forms[0].elements['<%=TXT_MainTabMenuTextOver.ClientID%>'], 3 , document.forms[0].elements['<%=TXT_Color_MainTabMenuTextOver.ClientID%>'] )"><img src="JS/ColorPicker/Forecolor.gif" border="0" alt="Click Here to Pick up the color" align="absMiddle"></a>
                                                                    <asp:textbox id="TXT_Color_MainTabMenuTextOver" readonly="true" runat="server" CssClass="TEXTBOX2"></asp:textbox>
                                                                    <ajax:filteredtextboxextender id="FTBE_MainTabMenuTextOver" runat="server" targetcontrolid="TXT_MainTabMenuTextOver" filtertype="Custom,LowercaseLetters,UppercaseLetters,Numbers" validchars="#"></ajax:filteredtextboxextender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap>
                                                                    4) Main Tab Menu Back
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:literal id="LTRL_MainTabMenuBack" runat="server" visible="false"></asp:literal>
                                                                    <asp:textbox id="TXT_MainTabMenuBack" runat="server" cssclass="TEXTBOX" columns="10" maxlength="7" onkeypress="CheckHashValue(this,event)"></asp:textbox>
                                                                    <a href="javascript:TCP.popup(document.forms[0].elements['<%=TXT_MainTabMenuBack.ClientID%>'], 3 , document.forms[0].elements['<%=TXT_Color_MainTabMenuBack.ClientID%>'] )"><img src="JS/ColorPicker/Forecolor.gif" border="0" alt="Click Here to Pick up the color" align="absMiddle"></a>
                                                                    <asp:textbox id="TXT_Color_MainTabMenuBack" readonly="true" runat="server" CssClass="TEXTBOX2"></asp:textbox>
                                                                    <ajax:filteredtextboxextender id="FTBE_MainTabMenuBack" runat="server" targetcontrolid="TXT_MainTabMenuBack" filtertype="Custom,LowercaseLetters,UppercaseLetters,Numbers" validchars="#"></ajax:filteredtextboxextender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap>
                                                                    5) Main Tab Menu Back Over
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:literal id="LTRL_MainTabMenuBackOver" runat="server" visible="false"></asp:literal>
                                                                    <asp:textbox id="TXT_MainTabMenuBackOver" runat="server" cssclass="TEXTBOX" columns="10" maxlength="7" onkeypress="CheckHashValue(this,event)"></asp:textbox>
                                                                    <a href="javascript:TCP.popup(document.forms[0].elements['<%=TXT_MainTabMenuBackOver.ClientID%>'], 3 , document.forms[0].elements['<%=TXT_Color_MainTabMenuBackOver.ClientID%>'] )"><img src="JS/ColorPicker/Forecolor.gif" border="0" alt="Click Here to Pick up the color" align="absMiddle"></a>
                                                                    <asp:textbox id="TXT_Color_MainTabMenuBackOver" readonly="true" runat="server" CssClass="TEXTBOX2"></asp:textbox>
                                                                    <ajax:filteredtextboxextender id="FTBE_MainTabMenuBackOver" runat="server" targetcontrolid="TXT_MainTabMenuBackOver" filtertype="Custom,LowercaseLetters,UppercaseLetters,Numbers" validchars="#"></ajax:filteredtextboxextender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap>
                                                                    6) Main Tab Menu Border
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:literal id="LTRL_MainTabMenuBorder" runat="server" visible="false"></asp:literal>
                                                                    <asp:textbox id="TXT_MainTabMenuBorder" runat="server" cssclass="TEXTBOX" columns="10" maxlength="7" onkeypress="CheckHashValue(this,event)"></asp:textbox>
                                                                    <a href="javascript:TCP.popup(document.forms[0].elements['<%=TXT_MainTabMenuBorder.ClientID%>'], 3 , document.forms[0].elements['<%=TXT_Color_MainTabMenuBorder.ClientID%>'] )"><img src="JS/ColorPicker/Forecolor.gif" border="0" alt="Click Here to Pick up the color" align="absMiddle"></a>
                                                                    <asp:textbox id="TXT_Color_MainTabMenuBorder" readonly="true" runat="server" CssClass="TEXTBOX2"></asp:textbox>
                                                                    <ajax:filteredtextboxextender id="FTBE_MainTabMenuBorder" runat="server" targetcontrolid="TXT_MainTabMenuBorder" filtertype="Custom,LowercaseLetters,UppercaseLetters,Numbers" validchars="#"></ajax:filteredtextboxextender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap>
                                                                    7) Main Tab Menu Border Over
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:literal id="LTRL_MainTabMenuBorderOver" runat="server" visible="false"></asp:literal>
                                                                    <asp:textbox id="TXT_MainTabMenuBorderOver" runat="server" cssclass="TEXTBOX" columns="10" maxlength="7" onkeypress="CheckHashValue(this,event)"></asp:textbox>
                                                                    <a href="javascript:TCP.popup(document.forms[0].elements['<%=TXT_MainTabMenuBorderOver.ClientID%>'], 3 , document.forms[0].elements['<%=TXT_Color_MainTabMenuBorderOver.ClientID%>'] )"><img src="JS/ColorPicker/Forecolor.gif" border="0" alt="Click Here to Pick up the color" align="absMiddle"></a>
                                                                    <asp:textbox id="TXT_Color_MainTabMenuBorderOver" readonly="true" runat="server" CssClass="TEXTBOX2"></asp:textbox>
                                                                    <ajax:filteredtextboxextender id="FTBE_MainTabMenuBorderOver" runat="server" targetcontrolid="TXT_MainTabMenuBorderOver" filtertype="Custom,LowercaseLetters,UppercaseLetters,Numbers" validchars="#"></ajax:filteredtextboxextender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap>
                                                                    8) Menu Bottom Line
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:literal id="LTRL_MenuBottomLine" runat="server" visible="false"></asp:literal>
                                                                    <asp:textbox id="TXT_MenuBottomLine" runat="server" cssclass="TEXTBOX" columns="10" maxlength="7" onkeypress="CheckHashValue(this,event)"></asp:textbox>
                                                                    <a href="javascript:TCP.popup(document.forms[0].elements['<%=TXT_MenuBottomLine.ClientID%>'], 3 , document.forms[0].elements['<%=TXT_Color_MenuBottomLine.ClientID%>'] )"><img src="JS/ColorPicker/Forecolor.gif" border="0" alt="Click Here to Pick up the color" align="absMiddle"></a>
                                                                    <asp:textbox id="TXT_Color_MenuBottomLine" readonly="true" runat="server" CssClass="TEXTBOX2"></asp:textbox>
                                                                    <ajax:filteredtextboxextender id="FTBE_MenuBottomLine" runat="server" targetcontrolid="TXT_MenuBottomLine" filtertype="Custom,LowercaseLetters,UppercaseLetters,Numbers" validchars="#"></ajax:filteredtextboxextender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap>
                                                                    9) Sub Tab Menu Text
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:literal id="LTRL_SubTabMenuText" runat="server" visible="false"></asp:literal>
                                                                    <asp:textbox id="TXT_SubTabMenuText" runat="server" cssclass="TEXTBOX" columns="10" maxlength="7" onkeypress="CheckHashValue(this,event)"></asp:textbox>
                                                                    <a href="javascript:TCP.popup(document.forms[0].elements['<%=TXT_SubTabMenuText.ClientID%>'], 3 , document.forms[0].elements['<%=TXT_Color_SubTabMenuText.ClientID%>'] )"><img src="JS/ColorPicker/Forecolor.gif" border="0" alt="Click Here to Pick up the color" align="absMiddle"></a>
                                                                    <asp:textbox id="TXT_Color_SubTabMenuText" readonly="true" runat="server" CssClass="TEXTBOX2"></asp:textbox>
                                                                    <ajax:filteredtextboxextender id="FTBE_SubTabMenuText" runat="server" targetcontrolid="TXT_SubTabMenuText" filtertype="Custom,LowercaseLetters,UppercaseLetters,Numbers" validchars="#"></ajax:filteredtextboxextender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap>
                                                                    10) Sub Tab Menu Text Over
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:literal id="LTRL_SubTabMenuTextOver" runat="server" visible="false"></asp:literal>
                                                                    <asp:textbox id="TXT_SubTabMenuTextOver" runat="server" cssclass="TEXTBOX" columns="10" maxlength="7" onkeypress="CheckHashValue(this,event)"></asp:textbox>
                                                                    <a href="javascript:TCP.popup(document.forms[0].elements['<%=TXT_SubTabMenuTextOver.ClientID%>'], 3 , document.forms[0].elements['<%=TXT_Color_SubTabMenuTextOver.ClientID%>'] )"><img src="JS/ColorPicker/Forecolor.gif" border="0" alt="Click Here to Pick up the color" align="absMiddle"></a>
                                                                    <asp:textbox id="TXT_Color_SubTabMenuTextOver" readonly="true" runat="server" CssClass="TEXTBOX2"></asp:textbox>
                                                                    <ajax:filteredtextboxextender id="FTBE_SubTabMenuTextOver" runat="server" targetcontrolid="TXT_SubTabMenuTextOver" filtertype="Custom,LowercaseLetters,UppercaseLetters,Numbers" validchars="#"></ajax:filteredtextboxextender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap>
                                                                    11) Main Form
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:literal id="LTRL_MainForm" runat="server" visible="false"></asp:literal>
                                                                    <asp:textbox id="TXT_MainForm" runat="server" cssclass="TEXTBOX" columns="10" maxlength="7" onkeypress="CheckHashValue(this,event)"></asp:textbox>
                                                                    <a href="javascript:TCP.popup(document.forms[0].elements['<%=TXT_MainForm.ClientID%>'], 3 , document.forms[0].elements['<%=TXT_Color_MainForm.ClientID%>'] )"><img src="JS/ColorPicker/Forecolor.gif" border="0" alt="Click Here to Pick up the color" align="absMiddle"></a>
                                                                    <asp:textbox id="TXT_Color_MainForm" readonly="true" runat="server" CssClass="TEXTBOX2"></asp:textbox>
                                                                    <ajax:filteredtextboxextender id="FTBE_MainForm" runat="server" targetcontrolid="TXT_MainForm" filtertype="Custom,LowercaseLetters,UppercaseLetters,Numbers" validchars="#"></ajax:filteredtextboxextender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap>
                                                                    12) Popup Frame Color
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:literal id="LTRL_PopUpTabMainColor" runat="server" visible="false"></asp:literal>
                                                                    <asp:textbox id="TXT_PopUpTabMainColor" runat="server" cssclass="TEXTBOX" columns="10" maxlength="7" onkeypress="CheckHashValue(this,event)"></asp:textbox>
                                                                    <a href="javascript:TCP.popup(document.forms[0].elements['<%=TXT_PopUpTabMainColor.ClientID%>'], 3 , document.forms[0].elements['<%=TXT_Color_PopUpTabMainColor.ClientID%>'] )"><img src="JS/ColorPicker/Forecolor.gif" border="0" alt="Click Here to Pick up the color" align="absMiddle"></a>
                                                                    <asp:textbox id="TXT_Color_PopUpTabMainColor" readonly="true" runat="server" CssClass="TEXTBOX2"></asp:textbox>
                                                                    <ajax:filteredtextboxextender id="FTBE_PopUpTabMainColor" runat="server" targetcontrolid="TXT_PopUpTabMainColor" filtertype="Custom,LowercaseLetters,UppercaseLetters,Numbers" validchars="#"></ajax:filteredtextboxextender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap>
                                                                    13) Popup Menu Text
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:literal id="LTRL_PopUpTabText" runat="server" visible="false"></asp:literal>
                                                                    <asp:textbox id="TXT_PopUpTabText" runat="server" cssclass="TEXTBOX" columns="10" maxlength="7" onkeypress="CheckHashValue(this,event)"></asp:textbox>
                                                                    <a href="javascript:TCP.popup(document.forms[0].elements['<%=TXT_PopUpTabText.ClientID%>'], 3 , document.forms[0].elements['<%=TXT_Color_PopUpTabText.ClientID%>'] )"><img src="JS/ColorPicker/Forecolor.gif" border="0" alt="Click Here to Pick up the color" align="absMiddle"></a>
                                                                    <asp:textbox id="TXT_Color_PopUpTabText" readonly="true" runat="server" CssClass="TEXTBOX2"></asp:textbox>
                                                                    <ajax:filteredtextboxextender id="FTBE_PopUpTabText" runat="server" targetcontrolid="TXT_PopUpTabText" filtertype="Custom,LowercaseLetters,UppercaseLetters,Numbers" validchars="#"></ajax:filteredtextboxextender>
                                                                </td>
                                                            </tr>
                                                             <tr>
                                                                <td nowrap>
                                                                    14) Popup Menu Select Text
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:literal id="LTRL_PopUpTabSelectedText" runat="server" visible="false"></asp:literal>
                                                                    <asp:textbox id="TXT_PopUpTabSelectedText" runat="server" cssclass="TEXTBOX" columns="10" maxlength="7" onkeypress="CheckHashValue(this,event)"></asp:textbox>
                                                                    <a href="javascript:TCP.popup(document.forms[0].elements['<%=TXT_PopUpTabSelectedText.ClientID%>'], 3 , document.forms[0].elements['<%=TXT_Color_PopUpTabSelectedText.ClientID%>'] )"><img src="JS/ColorPicker/Forecolor.gif" border="0" alt="Click Here to Pick up the color" align="absMiddle"></a>
                                                                    <asp:textbox id="TXT_Color_PopUpTabSelectedText" readonly="true" runat="server" CssClass="TEXTBOX2"></asp:textbox>
                                                                    <ajax:filteredtextboxextender id="FTBE_PopUpTabSelectedText" runat="server" targetcontrolid="TXT_PopUpTabSelectedText" filtertype="Custom,LowercaseLetters,UppercaseLetters,Numbers" validchars="#"></ajax:filteredtextboxextender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap>
                                                                    15) Footer
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:literal id="LTRL_Footer" runat="server" visible="false"></asp:literal>
                                                                    <asp:textbox id="TXT_Footer" runat="server" cssclass="TEXTBOX" columns="10" maxlength="7" onkeypress="CheckHashValue(this,event)"></asp:textbox>
                                                                    <a href="javascript:TCP.popup(document.forms[0].elements['<%=TXT_Footer.ClientID%>'], 3 , document.forms[0].elements['<%=TXT_Color_Footer.ClientID%>'] )"><img src="JS/ColorPicker/Forecolor.gif" border="0" alt="Click Here to Pick up the color" align="absMiddle"></a>
                                                                    <asp:textbox id="TXT_Color_Footer" readonly="true" runat="server" CssClass="TEXTBOX2"></asp:textbox>
                                                                    <ajax:filteredtextboxextender id="FTBE_Footer" runat="server" targetcontrolid="TXT_Footer" filtertype="Custom,LowercaseLetters,UppercaseLetters,Numbers" validchars="#"></ajax:filteredtextboxextender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap>
                                                                    16) Footer Text
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:literal id="LTRL_FooterText" runat="server" visible="false"></asp:literal>
                                                                    <asp:textbox id="TXT_FooterText" runat="server" cssclass="TEXTBOX" columns="10" maxlength="7" onkeypress="CheckHashValue(this,event)"></asp:textbox>
                                                                    <a href="javascript:TCP.popup(document.forms[0].elements['<%=TXT_FooterText.ClientID%>'], 3 , document.forms[0].elements['<%=TXT_Color_FooterText.ClientID%>'] )"><img src="JS/ColorPicker/Forecolor.gif" border="0" alt="Click Here to Pick up the color" align="absMiddle"></a>
                                                                    <asp:textbox id="TXT_Color_FooterText" readonly="true" runat="server" CssClass="TEXTBOX2"></asp:textbox>
                                                                    <ajax:filteredtextboxextender id="FTBE_FooterText" runat="server" targetcontrolid="TXT_FooterText" filtertype="Custom,LowercaseLetters,UppercaseLetters,Numbers" validchars="#"></ajax:filteredtextboxextender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" align="center"><asp:CheckBox ID="CHK_ConfirmCssChange" runat="server" Text="Apply new css changes." CssClass="ERROR" /><br /></td>
                                                            </tr>                                                            
                                                            <tr>
                                                                <td colspan="4">
                                                                    <asp:Label ID="LBL_HeaderLogo" runat="server"></asp:Label>&nbsp;:&nbsp;
                                                                    <asp:FileUpload ID="FUPLD_HeaderLogo" runat="server" CssClass="TEXTBOX" Style="width: 200px" />
                                                                    <asp:HiddenField ID="HFHeaderLogo" runat="server" />
                                                                
                                                                    <asp:HyperLink ID="LnkHeaderLogo" Text="Header Logo" runat="server" Target="_blank"
                                                                        Visible="false" CssClass="LINKBUTTON2"></asp:HyperLink>                                                                
                                                                    <asp:CheckBox ID="chkHeaderLogo" runat="server" Visible="false" Checked="false" Text="Remove" /><br /><asp:Literal ID="LTRL_ImagePath_Logo" runat="server"></asp:Literal>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <asp:Label ID="LBL_LargeImage" runat="server"></asp:Label>&nbsp;:&nbsp;
                                                                    <asp:FileUpload ID="FUPLD_LargeImage" runat="server" CssClass="TEXTBOX" Style="width: 200px" />
                                                                    <asp:HiddenField ID="HFLargeImage" runat="server" />
                                                                    <asp:HyperLink ID="LnkLargeImage" Text="Large Image" runat="server" Target="_blank"
                                                                        Visible="false" CssClass="LINKBUTTON2"></asp:HyperLink>                                                                
                                                                    <asp:CheckBox ID="chkLargeImage" runat="server" Visible="false" Checked="false" Text="Remove" /><br /><asp:Literal ID="LTRL_ImagePath_LargeImage" runat="server"></asp:Literal>
                                                                </td>
                                                            </tr>
                                                            <tr id="TRMessage1" runat="server" visible="false">
                                                                <td colspan="4" class="ERROR">
                                                                    Note : Please Select the checkbox for Remove Image File.<br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">&nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </fieldset>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Button ID="BTN_AddSave" runat="server" CssClass="BUTTON" OnClick="BTN_AddSave_Click"
                                                        OnClientClick="return CheckUploadFileFile();" />
                                                    &nbsp;&nbsp;<asp:Button ID="BTN_Cancel" runat="server" CssClass="BUTTON" OnClick="BTN_Cancel_Click" />
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
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
    </table>
</asp:Content>