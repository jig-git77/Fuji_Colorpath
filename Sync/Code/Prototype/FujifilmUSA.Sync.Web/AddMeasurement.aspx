<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddMeasurement.aspx.cs" Inherits="FujifilmUSA.Sync.Web.AddMeasurement"
    MasterPageFile="~/MainMaster.Master" %>

<%@ Register Src="~/UserControl/AddMeasurement.ascx" TagName="AddMeasurement" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
  <uc1:AddMeasurement ID="ucAddMeasurement" runat="server" />
</asp:Content>
