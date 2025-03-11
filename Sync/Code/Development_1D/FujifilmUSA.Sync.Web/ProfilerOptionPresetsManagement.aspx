<%@ Page Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ProfilerOptionPresetsManagement.aspx.cs" Inherits="FujifilmUSA.Sync.Web.ProfilerOptionPresetsManagement" %>
<%@ Register Src="~/UserControl/ProfilerOptionPresetsManagement.ascx" TagName="ProfilerOptionPresetsManagement" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <uc:ProfilerOptionPresetsManagement  ID="ucProfilerOptionPresetsManagement"  runat="server"/>
</asp:Content>