<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ShowProduct.aspx.vb" Inherits="Coriolis.ShowProduct" %>
<%@ Register Assembly="Bright.WebControls" Namespace="Bright.WebControls" TagPrefix="bri" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
 <link rel="stylesheet" type="text/css" href="CSS/StyleSheet.css" media="all" />
    <title></title>      
    
</head>
<body>
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>     
    <table   class="showproducttable" height="600px" width="905px">
        <tr>
            <td  colspan="2" class="header_green" align="center"><asp:Label ID="lblModelHeader" runat="server"></asp:Label> - <asp:Label ID="lblDescription" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="showproductTD" valign="middle">
                <div ID="DivImgPreview" runat="server" visible="false">
                    <asp:Image ID="ImgProduct" runat="server" Height="350" width="500" />
                </div>
            </td>
            <td class="showproductTD" valign="middle"  >
                <table width="100%" >
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr><tr> <td>
                    <table class="showproducttable" width="100%">
                     <div class="green-title">
                         <caption class="green-title">
                             Product Information</caption>
                        </div>
                        <tr class="ITEMSTYLE">
                            <td style="width:150px">
                                Serial Number:</td>
                            <td class="showproductTD">
                                <asp:Label ID="lblSerialNumber" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr class="ALTERNATINGITEMSTYLE">
                            <td style="width:150px">
                                Product Type:</td>
                            <td class="showproductTD">
                                <asp:Label ID="lblProductType" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr class="ITEMSTYLE">
                            <td style="width:150px">
                                Model:</td>
                            <td class="showproductTD">
                                <asp:Label ID="lblModel" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr class="ALTERNATINGITEMSTYLE">
                            <td style="width:150px">
                                Year Built:</td>
                            <td class="showproductTD">
                                <asp:Label ID="lblYearBuilt" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr class="ITEMSTYLE">
                            <td style="width:150px">
                                Internal Cost:</td>
                            <td class="showproductTD" >
                                <asp:Label ID="lblListPrice" runat="server" CssClass="redFont"></asp:Label>
                            </td>
                        </tr>
                        <tr class="ALTERNATINGITEMSTYLE">
                            <td style="width:150px">
                                Usage:</td>
                            <td class="showproductTD">
                                <asp:Label ID="lblUsage" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr class="ITEMSTYLE">
                            <td style="width:150px">
                                Current Status:&nbsp;</td>
                            <td class="showproductTD">
                                <asp:Label ID="lblCurrentStatus" runat="server"></asp:Label>
                            </td>
                        </tr> 
                           <tr class="ALTERNATINGITEMSTYLE">
                            <td style="width:150px">
                                Date Available:</td>
                            <td class="showproductTD">
                                <asp:Label ID="lblDateAvailable" runat="server" CssClass="redFont"></asp:Label>
                            </td>
                        </tr> <tr class="ITEMSTYLE">
                            <td style="width:150px">
                                Current Location:</td>
                            <td class="showproductTD">
                                 <asp:Label ID="lblCurrLocation" runat="server"></asp:Label>
                            </td>
                        </tr>
                          </tr> 
                           <tr class="ALTERNATINGITEMSTYLE">
                            <td style="width:150px">
                                Warranty:</td>
                            <td class="showproductTD">
                                <asp:HyperLink ID="lnkWarranty" Target="_blank" runat="server" ></asp:HyperLink>
                            </td>
                        </tr>
                    </table></td></tr><tr><td style="height:30px"></td></tr>
                    <tr>
                        <td>
                           <table class="showproducttable" width="100%">
                     <div class="green-title">
                         <caption class="green-title">
                             Product Specification</caption>
                        </div>
                        <tr class="ALTERNATINGITEMSTYLE">
                            <td style="width:150px">Power:&nbsp;</td>
                            <td class="showproductTD">
                                 <asp:Label ID="lblPower" runat="server"></asp:Label>&nbsp;</td>
                        </tr>
                         <tr class="ITEMSTYLE">
                            <td >
                                Physical Dimension:</td>
                            <td class="showproductTD" >
                                <asp:Label ID="lblPhysicalDimension" runat="server"></asp:Label>&nbsp;
                            </td>
                        </tr>
                          <tr class="ALTERNATINGITEMSTYLE">
                            <td style="width:150px">Print Size:</td>
                            <td class="showproductTD">
                                <asp:Label ID="lblPrintSize" runat="server"></asp:Label>&nbsp;
                              </td>
                        </tr>
                         <tr class="ITEMSTYLE">
                            <td>&nbsp;
                                </td>
                            <td class="showproductTD" ><asp:HyperLink ID="lnkDetailSpec" Target="_blank" runat="server" ></asp:HyperLink>
                                 </td>
                        </tr>
                    </table></td>
                    </tr>
                </table>
            </td>
        </tr><tr>
            
            <td class="showproductTD" valign="top"> <div style="clear:both;">
                <asp:Repeater ID="Repeater1" runat="server" >
                    <ItemTemplate>
                        <span style="padding:2px 10px 2px 10px">
                            <bri:ThumbViewer ID="ThumbViewer1" runat="server" ImageUrl='<%# Eval("Name", "images/" + Me.LTRL_ProductID.Text + "/{0}") %>' Height="75" />
                        </span>
                    </ItemTemplate>
                </asp:Repeater>  
                <br /><td class="showproductTD" width="450" valign="top">  
                <asp:Label ID="lblNotes" runat="server"  ></asp:Label>
            </td>
                <caption>
                    &nbsp;</caption>
            </td>
        </tr>
    </table>
        
        <asp:Literal ID="LTRL_ProductID" runat="server" Visible="false"></asp:Literal>
       </ContentTemplate>  
      </asp:UpdatePanel>
    </form>
</body>
</html>
