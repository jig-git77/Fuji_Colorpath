<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Footer.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.Footer" %>
 <footer>
    <div id="footer">
        <table style="width:100%;">
            <tr>
                <td align="left" style="width:20%; padding-left:15px;">
                    <img alt="" src="Images/logo_Fujifilm.png" />
                </td>
                <td align="center">
                    <b> <asp:Localize Text="© <%= DateTime.Now.Year %> FUJIFILM North America Corporation - Graphic Systems Division" 
                            runat="server" ID="locFooter" meta:resourcekey="locFooterResource1"></asp:Localize> </b>
                </td>
                <td align="right" style="width:20%;">
                     <img alt="" src="Images/logo_bottom.png" />
                </td>
            </tr>
        </table>
    	
    </div>
 </footer>

