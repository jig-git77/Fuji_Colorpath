<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true"
 CodeBehind="SourceProfileManagement.aspx.cs" Inherits="FujifilmUSA.Sync.Web.SourceProfileManagement" %>
<%@ Register Src="~/UserControl/SourceProfileManagement.ascx" TagName="SourceProfile" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:SourceProfile ID="ucSourceProfile"  runat="server"/>
</asp:Content>
