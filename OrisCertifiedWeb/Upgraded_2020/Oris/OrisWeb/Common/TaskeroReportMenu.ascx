<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TaskeroReportMenu.ascx.cs" Inherits="OrisWeb.Common.TaskeroReportMenu" %>
<div id="TopNavTab"<%=_STYLE %>>
<ul>
    <li class="active" id="Main" runat="server"><a href="TaskeroReports.aspx" title="All reports"><span>All</span></a>
    </li>
    <asp:Repeater ID="rptReportType" runat="server" OnItemDataBound="rptReportType_ItemDataBound">
        <ItemTemplate>
            <li id="LIReports" runat="server"><a title='<%#DataBinder.Eval(Container.DataItem, "ReportDesc")%>' href='<%#DataBinder.Eval(Container.DataItem, "AspxPageName")%>'><span><%#DataBinder.Eval(Container.DataItem, "ReportDesc")%></span></a></li>
        </ItemTemplate>
    </asp:Repeater>
</ul>
</div>