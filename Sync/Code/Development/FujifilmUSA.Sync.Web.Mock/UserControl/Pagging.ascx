<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Pagging.ascx.cs" Inherits="FujifilmUSA.Sync.Web.Mock.UserControl.Pagging" %>
<div class="PaggingHeader">
<table  cellspacing="0" cellpadding="0" border="0">
    <tr>
        <td>
            <asp:DataPager ID="dpPagging" runat="server" PageSize="1">
                <Fields>
                    <asp:NextPreviousPagerField ButtonType="Image" FirstPageImageUrl="~/Images/First.png"
                        ShowFirstPageButton="true" PreviousPageImageUrl="~/Images/Previous.png" ShowLastPageButton="false"
                        ShowNextPageButton="false" />
                    <asp:NumericPagerField ButtonCount="5" />
                    <asp:NextPreviousPagerField ButtonType="Image" ShowLastPageButton="true" ShowNextPageButton="true"
                        ShowPreviousPageButton="false" LastPageImageUrl="~/Images/Last.png" NextPageImageUrl="~/Images/Next.png" />
                </Fields>
            </asp:DataPager>
              &nbsp;Page Size :
            <asp:DropDownList ID="cmbPageSize" runat="server" AutoPostBack="true" CssClass="select" Width="70px">
            </asp:DropDownList>
        </td>
        <td style=" width:200px;text-align:right;">
            <asp:Label ID="lblPageMessage" runat="server" Text=""></asp:Label>
        </td>
    </tr>
</table>
</div>