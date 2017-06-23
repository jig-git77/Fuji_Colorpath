<%@ Page Language="C#" MasterPageFile="~/PopUpMaster.Master" AutoEventWireup="true" CodeBehind="PopupDownloadSoftware.aspx.cs" Inherits="FujifilmUSA.Sync.Web.PopupDownloadSoftware" Title="Download Software" %>
<%@ Register Src="~/UserControl/PopupControl/DownloadSoftware.ascx" TagName="DownloadSoftware" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <uc:DownloadSoftware ID="ucDownloadSoftware"  runat="server"/>
</asp:Content>
