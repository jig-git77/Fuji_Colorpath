<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" 
    CodeBehind="InkOptSourceMiddle.aspx.cs" Inherits="FujifilmUSA.Sync.Web.InkOptSourceMiddle" enableEventValidation="false" %>
<%@ Register Src="~/UserControl/InkOptSourceMiddle.ascx" TagName="InkOptSourceMiddle" TagPrefix="uc" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <uc:InkOptSourceMiddle ID="ucInkOptSourceMiddle" runat="server" />
</asp:Content>
