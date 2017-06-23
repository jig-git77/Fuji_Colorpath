<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" 
CodeBehind="SessionEnd.aspx.cs" 
Inherits="FujifilmUSA.Sync.Web.SessionEnd" %>

<%@ Register Src="~/UserControl/SessionEnd.ascx" TagName="SessionEnd" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<!---Code for stopping cache to store in browser for this page in order to limit same error pops up everytime from cache--->
<meta http-equiv="cache-control" content="max-age=0" />
<meta http-equiv="cache-control" content="no-cache" />
<meta http-equiv="expires" content="0" />
<meta http-equiv="expires" content="Tue, 01 Jan 1980 1:00:00 GMT" />
<meta http-equiv="pragma" content="no-cache" />
<!---End Code--->

<uc:SessionEnd ID="ucSessionEnd"  runat="server"/>
</asp:Content>
