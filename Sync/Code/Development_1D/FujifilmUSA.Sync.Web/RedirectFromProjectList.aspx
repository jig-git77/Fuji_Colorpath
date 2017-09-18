<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="RedirectFromProjectList.aspx.cs" Inherits="FujifilmUSA.Sync.Web.RedirectFromProjectList" %>
<%@ Register Src="~/UserControl/RedirectFromProjectList.ascx" TagName="RedirectProjectList" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:RedirectProjectList ID="ucProjectList" runat="server" />
</asp:Content>
