<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="UpdateChangeLog.aspx.cs" Inherits="FujifilmUSA.Sync.Web.UpdateChangeLog" EnableEventValidation="false" %>

<%@ Register Src="~/UserControl/UpdateChangeLog.ascx" TagName="UpdateChangeLog" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <uc:UpdateChangeLog ID="ucUpdateChangeLog"  runat="server"/>
</asp:Content>