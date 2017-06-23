<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" 
CodeBehind="AddDeviceFamily.aspx.cs" 
Inherits="FujifilmUSA.Sync.Web.AddDeviceFamily" %>
<%@ Register Src="~/UserControl/AddDeviceFamily.ascx" TagName="AddDeviceFamily" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<uc:AddDeviceFamily ID="ucAddDeviceFamily"  runat="server"/>
</asp:Content>
