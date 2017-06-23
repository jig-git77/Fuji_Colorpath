<%@ Page Language="C#"  MasterPageFile="~/GlobalMaster.Master" AutoEventWireup="true" CodeBehind="GlobalActivation.aspx.cs"
    Inherits="FujifilmUSA.Sync.Web.GlobalActivation" %>

<%@ Register Src="UserControl/GlobalActivation.ascx" TagName="GlobalActivation" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <uc1:GlobalActivation ID="ucGlobalActivation" runat="server" />     
</asp:Content>