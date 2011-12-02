<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="FujifilmUSA.Sync.Web.Index"
    MasterPageFile="~/MainMaster.Master" %>

<%@ Register Src="~/UserControl/DeviceList.ascx" TagName="DeviceList" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <uc1:DeviceList ID="DeviceList1" runat="server" />
</asp:Content>
