<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true"
    CodeBehind="ProductList.aspx.cs" Inherits="FujifilmUSA.Sync.Web.ProductList" %>

<%@ Register Src="~/UserControl/ProductList.ascx" TagName="ProductList" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ProductList ID="ProductList1" runat="server" />
</asp:Content>
