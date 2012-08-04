<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="Coriolis._Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>FUJIFILM Graphic Systems U.S.A., Inc.</title>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <meta name="keywords" content="FUJIFILM Graphic Systems U.S.A., Inc." />
    <meta name="description" content="FUJIFILM Graphic Systems U.S.A., Inc." />
   
    <script src="JavaScript/curvycorners.js" type="text/javascript"></script>
     <link  href="CSS/StyleSheet.css" rel="stylesheet" type="text/css" />        
    <link href="CSS/Gridview.css" rel="stylesheet" type="text/css" />
   
</head>
<body>
    <form id="FRM1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
      </br>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center" height="100%">
     <tr>
         <td class="header-top-border"></td>
    </tr>
        <tr>
            <td valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td valign="top" align="center"> 
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                   <td height="76" align="center" valign="bottom">
                                        <table width="940" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td valign="top" align=center>
                                                       <img src="images/logo.png" alt="FUJIFILM" width="124" height="21" 
                                                        border="0" /><span class="title">&nbsp;&nbsp;&nbsp;&nbsp; FUJIFILM North America Corporation - 
                                                       Graphic Systems Division</span>
                                                    
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="550" align="center" valign="top"><table width="934">
                                      <tr>
                                        <td width="146">&nbsp;</td>
                                        <td width="652">&nbsp;</td>
                                      </tr>
                                        <tr>
                                            <td width="146">
                                                &nbsp;</td>
                                            <td width="650" style="height:450px" valign="top">
<DIV id="myDiv" class="myBox">Welcome to&nbsp; FujiFilm Graphic Systems used equipment 
    website<br />
     <br />
    All the equipment <ul>
  <li>Rebuilt and inspected by FUJIFILM trained personnel.</li>
  <li>Comes with one year parts and labor warranty.</li>
  <li>FREE technical support by telephone or email from our in-house staff 
      technicians.</li>
  <li>Price includes pre-site and installation.</li>
</ul>
</DIV><table border="0"   cellspacing="0" class="GridviewTable" style="width: 800px;padding:0.5em 0.5em 0.5em 0.5em">
                                                    <tr> 
                                                        <td style="width: 20px;">Details</td>
                                                        <td style="width: 100px;">Serial Number</td>
                                                        <td style="width: 155px;">
                                                            Product Type
                                                            <br />
                                                            <asp:DropDownList ID="ddlProductType" runat="server" AutoPostBack="true" 
                                                                Font-Size="11px" OnSelectedIndexChanged="DDL_ProductType_SelectedIndexChanged" 
                                                                >
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="width: 120px;">Model</td>
                                                        <td style="width: 100px;">Internal Cost</td>
                                                        <td style="width: 110px;">Year Built</td>
                                                        <td style="width: 120px;">Date Available</td>
                                                         
                                                    </tr>
                                                    <tr>
                                                        <td colspan="8">
                                                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                                                                AllowSorting="true" AutoGenerateColumns="False" CssClass="Gridview" 
                                                                OnRowDataBound="Gridview1_RowDataBound" PageSize="10" Width="907px" ShowHeader="false">
                                                                <Columns>
                                                               
                                                                    <asp:TemplateField ItemStyle-Width="35px" HeaderText="Details">
                                                                       
                                                                        <ItemTemplate>
                                                                            <img ID="ImgView_New" runat="server" src="~/Images/popup2.gif" 
                                                                                style="cursor: pointer" title="View in new window" />
                                                                        </ItemTemplate>
                                                                        
                                                                        <ItemStyle Width="35px" />
                                                                        
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="SerialNumber" ItemStyle-HorizontalAlign="Left" 
                                                                        ItemStyle-Width="100px"  >
                                                                      
                                                                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                                      
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ProductType" ItemStyle-Width="155px" >
                                                                       
                                                                        <ItemStyle Width="155px" />
                                                                        
                                                                       
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Model" ItemStyle-Width="120px" >
                                                                        
                                                                        <ItemStyle Width="120px" />
                                                                        
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ListPrice" ItemStyle-Width="100px" >
                                                                        
                                                                        <ItemStyle Width="100px" />
                                                                        
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="YearBuilt" ItemStyle-Width="110px" >
                                                                        
                                                                        <ItemStyle Width="110px" />
                                                                        
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="DateAvailable" ItemStyle-Width="120px" >
                                                                       
                                                                        <ItemStyle Width="120px" />
                                                                       
                                                                    </asp:BoundField>
                                                                         <asp:BoundField ItemStyle-Width="0px" DataField="ProductID" Visible="false"  >
                                                                     
                                                                             <ItemStyle Width="0px" />
                                                                     
                                                                    </asp:BoundField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                      <tr>
                                        <td height="150">&nbsp;</td>
                                        <td valign="middle" align="left">  <div class="GridviewDiv"><strong><a href="#"  onclick="javascript:window.open('guidelines.htm', 'Guidelines', 'width=952px,height=600px,resizable=1,scrollbars=0')">
                                            Used Equipment Sales Guidelines</a></strong>
            </div></td>
                                      </tr>
                                    </table></td>
                              </tr>
                              
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="15" valign="bottom">
               </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="0"  >
                    <tr>
                        <td>
                            <table width="940" align="center" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                         
                                         <div align="center"><span class="title">© <%=System.DateTime.Now.Year %> FUJIFILM 
                                             North America Corporation - Graphic Systems Division                                  </span></div></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </ContentTemplate>
    </asp:UpdatePanel> 
             <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
                <asp:Button runat="server" ID="hiddenTargetControlForModalPopup" Style="display: none" />
                                    <ajaxtoolkit:ModalPopupExtender runat="server" ID="programmaticModalPopup" BehaviorID="programmaticModalPopupBehavior"
                                        Y="20" TargetControlID="hiddenTargetControlForModalPopup" PopupControlID="programmaticPopup"
                                        BackgroundCssClass="modalBackground" DropShadow="false" PopupDragHandleControlID="programmaticPopupDragHandle">
                                    </ajaxtoolkit:ModalPopupExtender>
                                    <asp:Panel runat="server" ID="programmaticPopup" Style="display: none; height: 620px;overflow:auto;
                                        background-color: White; background:  ">
                                        <a id="hideModalPopupViaClientButton" href="#" style="border: none; text-decoration: none;">
                                            <img src="Images/Close2.png" style="position: absolute; border: none; right: 10px;
                                                top: 10px; text-align: right;" />
                                        </a>
                                        <iframe id="FrmPopup" src="" width="952px" height="600px" frameborder="0" marginwidth="0"
                                            scrolling="no" marginheight="0" allowtransparency="true" style="background-color: transparent">
                                        </iframe>
                                    </asp:Panel>
       
        </ContentTemplate>
    </asp:UpdatePanel>   <script language="javascript" type="text/javascript">
                             function pageLoad() {
                                 $addHandler($get("hideModalPopupViaClientButton"), 'click', hideModalPopupViaClient);
                                
                                     //  add our handler to the document's
                                     //  keydown event
                                     $addHandler(document, "keydown", onKeyDown);
                                  
                             }

                            

                             function onKeyDown(e) {
                                 if (e && e.keyCode == Sys.UI.Key.esc) {
                                     // if the key pressed is the escape key, dismiss the dialog
                                     $find('programmaticModalPopupBehavior').hide();
                                 }
                             } 
   
                             function hideModalPopupViaClient(ev) {
                                 ev.preventDefault();
                                 var modalPopupBehavior = $find('programmaticModalPopupBehavior');
                                 modalPopupBehavior.hide();
                                 document.getElementById("FrmPopup").src = "";
                             }

                             function OpenProductWindow(URLStr, IsShow, IsNewWindow, ProductId) {
                                 if (IsShow == true) {
                                     if (IsNewWindow == true) {
                                         //window.open(URLStr, ProductId, 'width=952px,height=600px,resizable=1,scrollbars=0');
                                         //if (screen.width <= 1024) {
                                             document.getElementById("<%=programmaticPopup.ClientID %>").style.height = "600px";
                                         //}
                                         document.getElementById("FrmPopup").src = URLStr;
                                         var modalPopupBehavior = $find('programmaticModalPopupBehavior');
                                         modalPopupBehavior.show();
                                     }
                                     else {
                                         if (screen.width <= 1024) {
                                             document.getElementById("<%=programmaticPopup.ClientID %>").style.height = "80%";
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
    </form>
</body>
</html>
