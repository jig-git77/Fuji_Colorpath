<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadBrandFiles.aspx.cs" Inherits="OrisWeb.UploadBrandFiles" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS/Default.css" rel="stylesheet" type="text/css" />
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

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:Literal ID="LtrlCSS" runat="server"></asp:Literal>        
        <asp:Literal ID="LTRL_Css" runat="server" Visible="false"></asp:Literal>
        <div class="green-form-bg">
            <table width="98%" border="0" cellspacing="1" cellpadding="2" align="center" class="maintable" style="font-weight:bold">
            <tr>
                <td style="width: 100px; height: 1px;"></td>
                <td style="width: 200px; height: 1px;"></td>
                <td style="width: 100px; height: 1px;"></td>
                <td style="width: 50px; height: 1px;"></td>
            </tr>
            <tr><td colspan="4" class="ERROR">&nbsp;<asp:Literal ID="LblError" runat="server"></asp:Literal></td></tr>
            <tr>                
                <td><a href="BrandingHelp.htm" target="0" class="LINKBUTTON2">Need Help?</a></td>
                <td align="right" class="required-text-small" colspan="3">Fields marked with (<span class="ERROR">*</span>) are Required</td>
            </tr>                        
            <tr>
                <td nowrap>
                    &nbsp;1) Top Line
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
                    &nbsp;2) Main Tab Menu Text
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
                    &nbsp;3) Main Tab Menu Text Over
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
                    &nbsp;4) Main Tab Menu Back
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
                    &nbsp;5) Main Tab Menu Back Over
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
                    &nbsp;6) Main Tab Menu Border
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
                    &nbsp;7) Main Tab Menu Border Over
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
                    &nbsp;8) Menu Bottom Line
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
                    &nbsp;9) Sub Tab Menu Text
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
                    &nbsp;10) Sub Tab Menu Text Over
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
                    &nbsp;11) Main Form
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
                    &nbsp;12) Footer
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
                    &nbsp;13) Footer Text
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
                <td colspan="4" align="center"><asp:CheckBox ID="CHK_ConfirmCssChange" runat="server" Text="Apply new css changes." CssClass="ERROR" Checked="true" /></td>
            </tr>            
           <tr>
                <td colspan="4">
                    &nbsp;Header Logo&nbsp;:&nbsp;
                    <asp:FileUpload ID="FUPLD_HeaderLogo" runat="server" CssClass="TEXTBOX" Style="width: 200px" />
                    <asp:HiddenField ID="HFHeaderLogo" runat="server" />
                
                    <asp:HyperLink ID="LnkHeaderLogo" Text="Header Logo" runat="server" Target="_blank"
                        Visible="false" CssClass="LINKBUTTON2"></asp:HyperLink>
                
                    <asp:CheckBox ID="chkHeaderLogo" runat="server" Visible="false" Checked="false" Text="Remove" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;Large Image&nbsp;:&nbsp;
                    <asp:FileUpload ID="FUPLD_LargeImage" runat="server" CssClass="TEXTBOX" Style="width: 200px" />
                    <asp:HiddenField ID="HFLargeImage" runat="server" />
                    <asp:HyperLink ID="LnkLargeImage" Text="Large Image" runat="server" Target="_blank"
                        Visible="false" CssClass="LINKBUTTON2"></asp:HyperLink>                                                                
                    <asp:CheckBox ID="chkLargeImage" runat="server" Visible="false" Checked="false" Text="Remove" />
                </td>
            </tr>
            <tr>
                <td colspan="4" class="ERROR">
                    Note : Please Select the checkbox for Remove Image File.<br />Please click "Save" to "Update" on main form  to update the branding.
                </td>
            </tr>
            <tr><td colspan="4">&nbsp;</td></tr>
            <tr><td colspan="4" align="center"><asp:Button ID="BTN_Save" runat="server" CssClass="BUTTON" Text="Save"  OnClick="BTN_Save_Click" OnClientClick="return CheckUploadFileFile();" /></td></tr>
            <tr><td colspan="4">&nbsp;</td></tr>                            
        </table>
        </div>                    
    </form>
</body>
</html>
