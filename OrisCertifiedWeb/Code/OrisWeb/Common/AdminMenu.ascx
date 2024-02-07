<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminMenu.ascx.cs" Inherits="OrisWeb.Common.AdminMenu" %>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td width="23" height="36" valign="top">
            <img src="images/menu-curve-left.png" alt="FujiFilm" border="0" />
        </td>
        <td valign="top" class="content-menu-top" align="left">
            <strong>Admin</strong>
        </td>
        <td valign="top" class="content-menu-top" align="right">
            <asp:DataList ID="DL_AccessType" runat="server" RepeatDirection="Horizontal" OnItemDataBound="DL_AccessType_ItemDataBound">
                <ItemTemplate>
                    <a title='<%#DataBinder.Eval(Container.DataItem, "Desc")%>' runat="server" id="ANC_Tab">
                        <img src='<%#DataBinder.Eval(Container.DataItem, "SmallImage")%>' border="0" alt='<%#DataBinder.Eval(Container.DataItem, "Desc")%>' /></a>
                </ItemTemplate>
            </asp:DataList>
        </td>
        <td width="23" valign="top">
            <img src="images/menu-curve-right.png" alt="FujiFilm" border="0" />
        </td>
    </tr>
</table>