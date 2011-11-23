<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddProduct.aspx.cs" Inherits="FujifilmUSA.Sync.Web.AddProduct"
    MasterPageFile="~/MainMaster.Master" %>

<%@ Register Src="~/UserControl/AddProduct.ascx" TagName="AddProduct" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <uc1:AddProduct ID="AddProduct1" runat="server" />
</asp:Content>
