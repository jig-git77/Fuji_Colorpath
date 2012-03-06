<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OperationSelection.aspx.cs" Inherits="FujifilmUSA.Sync.Web.OperationSelection"
    MasterPageFile="~/MainMaster.Master" %>

<%@ Register Src="~/UserControl/OperationSelection.ascx" TagName="OperationSelection" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <uc1:OperationSelection ID="MockSelection1" runat="server" />
</asp:Content>
