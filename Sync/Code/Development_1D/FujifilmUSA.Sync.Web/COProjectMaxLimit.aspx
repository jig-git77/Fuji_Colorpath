<%@ Page Title="" Language="C#" MasterPageFile="~/COMaster.Master" AutoEventWireup="true" CodeBehind="COProjectMaxLimit.aspx.cs" Inherits="FujifilmUSA.Sync.Web.COProjectMaxLimit" %>
<%@ Register Src="~/UserControl/COProjectMaxLimit.ascx" TagName="COProjectMaxLimit"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<uc1:COProjectMaxLimit ID="ucCOProjectMaxLimit" runat="server" />
</asp:Content>