<%@ Page Language="C#" MasterPageFile="~/GlobalMaster.Master" AutoEventWireup="true" CodeBehind="ApplicationError.aspx.cs"
    Inherits="FujifilmUSA.Sync.Web.ApplicationError" %>

<%@ Register Src="~/UserControl/ApplicationError.ascx" TagName="ApplicationError"
    TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <uc:ApplicationError ID="ucApplicationError" runat="server" />
</asp:Content>
