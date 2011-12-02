<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Information.aspx.cs" Inherits="FujifilmUSA.Sync.Web.Information"
    MasterPageFile="~/MainMaster.Master" %>

<%@ Register Src="~/UserControl/Information.ascx" TagName="Information" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
  <uc1:Information ID="ucInformation" runat="server" />
</asp:Content>
