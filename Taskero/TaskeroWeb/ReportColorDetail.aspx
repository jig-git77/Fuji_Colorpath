<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" EnableEventValidation="false"  CodeBehind="ReportColorDetail.aspx.cs" Inherits="TaskeroWeb.ReportColorDetail" %>

<%@ Register Src="~/Common/ReportMenu.ascx" TagName="ReportMenu" TagPrefix="uc1" %>
<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" align="center" border="0" width="100%">
        <tr>
            <td align="center">
                <table width="940" border="0" cellspacing="0" cellpadding="0" align="center">
                    <tr>
                        <td>
                            <!--Report Menu Conttrol Will Load here-->
                            <uc1:ReportMenu ID="ReportMenu1" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table width="940" border="0" cellspacing="0" cellpadding="0" align="center">
                    <tr>
                        <td>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">                               
                                <tr>
                                    <td style="padding: 5px;" class="TABLE1">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <table style="width: 100%" cellspacing="0" cellpadding="0" align="center" border="0">
                                                    <tr>
                                                        <td class="green-title" valign="middle" align="left" colspan="4">
                                                            <asp:Label ID="lblReportTitle" runat="server" Text="Color Detail Report(Search Criteria)"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" class="green-form-bg" style="border-bottom: none" id="TDReportTitle"
                                                            runat="server">
                                                            <asp:Label ID="LblError" runat="server" CssClass="ERROR"></asp:Label>
                                                            <asp:Literal ID="LTRL_CPMeasurementGUID" runat="server" Visible="false"></asp:Literal></center>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel Style="width: 100%" ID="PNL_Search" runat="server">
                                                    <table style="width: 100%" cellspacing="0" cellpadding="0" align="center" border="0">
                                                        <tr>
                                                            <td align="center" valign="top" class="green-form-bg" style="border-top: none">
                                                                <table width="80%" border="0" cellspacing="2" cellpadding="3" class="form-text">
                                                                    <tr>
                                                                        <td colspan="5">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 20%" class="BOLD" align="center">
                                                                            Company
                                                                        </td>
                                                                        <td style="width: 20%" class="BOLD" align="center">
                                                                            Branch
                                                                        </td>
                                                                        <td style="width: 20%" class="BOLD" align="center">
                                                                            Dongle No.
                                                                        </td>
                                                                        <td style="width: 20%" class="BOLD" align="center">
                                                                            Device
                                                                        </td>
                                                                        <td style="width: 20%" class="BOLD" align="center">
                                                                            Print Target
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center">
                                                                            <asp:DropDownList ID="DDL_Company" runat="server" AutoPostBack="true" OnSelectedIndexChanged="FillBranch"
                                                                                CssClass="DROPDOWNLIST">
                                                                                <asp:ListItem Value="0">--- Select Company ---</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:DropDownList ID="DDL_Branch" runat="server" AutoPostBack="true" OnSelectedIndexChanged="FillDongle"
                                                                                CssClass="DROPDOWNLIST">
                                                                                <asp:ListItem Value="0">--- Select Branch ---</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:DropDownList ID="DDL_DongleNo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="FillDevice"
                                                                                CssClass="DROPDOWNLIST">
                                                                                <asp:ListItem Value="0">--- Select Dongle ---</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:DropDownList ID="DDL_Device" runat="server" AutoPostBack="true" OnSelectedIndexChanged="FillPrintTarget"
                                                                                CssClass="DROPDOWNLIST">
                                                                                <asp:ListItem Value="0">--- Select Device ---</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:DropDownList ID="DDL_PrintTarget" runat="server" AutoPostBack="true" OnSelectedIndexChanged="FillDates"
                                                                                CssClass="DROPDOWNLIST">
                                                                                <asp:ListItem Value="0">--- Select Print Target ---</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" valign="baseline">
                                                                            Search By Date
                                                                        </td>
                                                                        <td class="BOLD" valign="top" colspan="3">
                                                                            From&nbsp;<asp:TextBox ID="TXT_FromDate" runat="server" AutoPostBack="True" CssClass="TEXTBOX"
                                                                                MaxLength="10" Columns="10" OnTextChanged="TXT_DateText" Width="75px"></asp:TextBox>&nbsp;<asp:ImageButton
                                                                                    ID="IBTN_FromDate" runat="server" ImageUrl="Images/calendar-icon.png" ImageAlign="AbsMiddle">
                                                                                </asp:ImageButton><ajaxToolkit:CalendarExtender ID="Cal_FromDate" runat="server"
                                                                                    TargetControlID="TXT_FromDate" PopupButtonID="IBTN_FromDate">
                                                                                </ajaxToolkit:CalendarExtender>
                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; To&nbsp;<asp:TextBox ID="TXT_ToDate" runat="server"
                                                                                AutoPostBack="True" CssClass="TEXTBOX" MaxLength="10" Columns="10" OnTextChanged="TXT_DateText"
                                                                                Width="75px"></asp:TextBox>&nbsp;<asp:ImageButton ID="IBTN_ToDate" runat="server"
                                                                                    ImageUrl="Images/calendar-icon.png" ImageAlign="AbsMiddle"></asp:ImageButton><ajaxToolkit:CalendarExtender
                                                                                        ID="Cal_ToDate" runat="server" TargetControlID="TXT_ToDate" PopupButtonID="IBTN_ToDate">
                                                                                    </ajaxToolkit:CalendarExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            &nbsp;
                                                                        </td>
                                                                        <td class="BOLD" align="center">
                                                                            Field
                                                                        </td>
                                                                        <td class="BOLD" align="center">
                                                                            Operator
                                                                        </td>
                                                                        <td class="BOLD" align="center">
                                                                            Criteria
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="BOLD" align="right">
                                                                            Search Option One
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="DDL_OPT1_Field" runat="server" CssClass="DROPDOWNLIST">
                                                                                <asp:ListItem Value="">---</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement CP1 ON CP1.CPMeasurementGUID = PMD.CPMeasurementGUID AND CP1.[User]">User</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement CP1 ON CP1.CPMeasurementGUID = PMD.CPMeasurementGUID AND CP1.Job">Job</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement CP1 ON CP1.CPMeasurementGUID = PMD.CPMeasurementGUID AND CP1.Info_Text">Info Text</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement CP1 ON CP1.CPMeasurementGUID = PMD.CPMeasurementGUID AND CP1.Customer">Customer</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP1 ON DP1.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP1.Substrate">Substrate Manufacturer</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP1 ON DP1.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP1.Substrate_SKU">Substrate SKU</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP1 ON DP1.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP1.Substrate_Batch">Substrate Batch</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP1 ON DP1.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP1.Ink">Ink Manufacturer</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP1 ON DP1.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP1.Ink_SKU">Ink SKU</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP1 ON DP1.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP1.Ink_Batch">Ink Batch</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP1 ON DP1.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP1.Chemistry">Chemistry Manufacturer</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP1 ON DP1.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP1.Chemistry_Batch">Chemistry Batch</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP1 ON DP1.CPMeasurementGUID = PMD.CPMeasurementGUID AND CONVERT( VARCHAR , DP1.Resolution )">Resolution</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP1 ON DP1.CPMeasurementGUID = PMD.CPMeasurementGUID AND CONVERT( VARCHAR , DP1.Line_Screen )">Dot Shape</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_OD OD1 ON OD1.CPMeasurementGUID = PMD.CPMeasurementGUID AND OD1.Category">Device Category</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_OD OD1 ON OD1.CPMeasurementGUID = PMD.CPMeasurementGUID AND OD1.Manufacturer">Device Manufacturer</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_OD OD1 ON OD1.CPMeasurementGUID = PMD.CPMeasurementGUID AND OD1.Model">Device Model</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_OD OD1 ON OD1.CPMeasurementGUID = PMD.CPMeasurementGUID AND OD1.RIP_Type">Rip Type</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_PT PT1 ON PT1.CPMeasurementGUID = PMD.CPMeasurementGUID INNER JOIN CPDevicePrintTarget CPPT1 ON CPPT1.CPDevicePrintTargetID = PT1.CPDevicePrintTargetID AND CPPT1.PrintTarget">Print Target Name</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_PT PT1 ON PT1.CPMeasurementGUID = PMD.CPMeasurementGUID AND PT1.Proof_Supplier">Print Target Supplier</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_MD MD1 ON MD1.CPMeasurementGUID = PMD.CPMeasurementGUID AND MD1.Instrument">Measurement Device</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_PT PT1 ON PT1.CPMeasurementGUID = PMD.CPMeasurementGUID AND PT1.Target_File">Target Name</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="DDL_OPT1_Operator" runat="server" CssClass="DROPDOWNLIST">
                                                                                <asp:ListItem Value="">---</asp:ListItem>
                                                                                <asp:ListItem Value="= '<<>>'">Equals</asp:ListItem>
                                                                                <asp:ListItem Value="<> '<<>>'">Does Not Equal</asp:ListItem>
                                                                                <asp:ListItem Value="LIKE '%<<>>%'">Contains</asp:ListItem>
                                                                                <asp:ListItem Value="NOT LIKE '%<<>>%'">Does Not Contain</asp:ListItem>
                                                                                <asp:ListItem Value="LIKE '<<>>%'">Start With</asp:ListItem>
                                                                                <asp:ListItem Value="LIKE '%<<>>'">Ends With</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td nowrap>
                                                                            <asp:TextBox ID="TXT_OPT1_KeyWords" runat="server" CssClass="TEXTBOX"></asp:TextBox>&nbsp;<asp:Button
                                                                                ID="BTN_Clear1" runat="server" CssClass="green2-btn" Text="X" OnClick="BTN_Clear_Click"
                                                                                Ind="1"></asp:Button>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="BOLD" align="right">
                                                                            Search Option Two
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="DDL_OPT2_Field" runat="server" CssClass="DROPDOWNLIST">
                                                                                <asp:ListItem Value="">---</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement CP2 ON CP2.CPMeasurementGUID = PMD.CPMeasurementGUID AND CP2.[User]">User</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement CP2 ON CP2.CPMeasurementGUID = PMD.CPMeasurementGUID AND CP2.Job">Job</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement CP2 ON CP2.CPMeasurementGUID = PMD.CPMeasurementGUID AND CP2.Info_Text">Info Text</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement CP2 ON CP2.CPMeasurementGUID = PMD.CPMeasurementGUID AND CP2.Customer">Customer</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP2 ON DP2.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP2.Substrate">Substrate Manufacturer</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP2 ON DP2.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP2.Substrate_SKU">Substrate SKU</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP2 ON DP2.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP2.Substrate_Batch">Substrate Batch</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP2 ON DP2.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP2.Ink">Ink Manufacturer</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP2 ON DP2.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP2.Ink_SKU">Ink SKU</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP2 ON DP2.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP2.Ink_Batch">Ink Batch</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP2 ON DP2.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP2.Chemistry">Chemistry Manufacturer</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP2 ON DP2.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP2.Chemistry_Batch">Chemistry Batch</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP2 ON DP2.CPMeasurementGUID = PMD.CPMeasurementGUID AND CONVERT( VARCHAR , DP2.Resolution )">Resolution</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP2 ON DP2.CPMeasurementGUID = PMD.CPMeasurementGUID AND CONVERT( VARCHAR , DP2.Line_Screen )">Dot Shape</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_OD OD2 ON OD2.CPMeasurementGUID = PMD.CPMeasurementGUID AND OD2.Category">Device Category</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_OD OD2 ON OD2.CPMeasurementGUID = PMD.CPMeasurementGUID AND OD2.Manufacturer">Device Manufacturer</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_OD OD2 ON OD2.CPMeasurementGUID = PMD.CPMeasurementGUID AND OD2.Model">Device Model</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_OD OD2 ON OD2.CPMeasurementGUID = PMD.CPMeasurementGUID AND OD2.RIP_Type">Rip Type</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_PT PT2 ON PT2.CPMeasurementGUID = PMD.CPMeasurementGUID INNER JOIN CPDevicePrintTarget CPPT2 ON CPPT2.CPDevicePrintTargetID = PT2.CPDevicePrintTargetID AND CPPT2.PrintTarget">Print Target Name</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_PT PT2 ON PT2.CPMeasurementGUID = PMD.CPMeasurementGUID AND PT2.Proof_Supplier">Print Target Supplier</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_MD MD2 ON MD2.CPMeasurementGUID = PMD.CPMeasurementGUID AND MD2.Instrument">Measurement Device</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_PT PT2 ON PT2.CPMeasurementGUID = PMD.CPMeasurementGUID AND PT2.Target_File">Target Name</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="DDL_OPT2_Operator" runat="server" CssClass="DROPDOWNLIST">
                                                                                <asp:ListItem Value="">---</asp:ListItem>
                                                                                <asp:ListItem Value="= '<<>>'">Equals</asp:ListItem>
                                                                                <asp:ListItem Value="<> '<<>>'">Does Not Equal</asp:ListItem>
                                                                                <asp:ListItem Value="LIKE '%<<>>%'">Contains</asp:ListItem>
                                                                                <asp:ListItem Value="NOT LIKE '%<<>>%'">Does Not Contain</asp:ListItem>
                                                                                <asp:ListItem Value="LIKE '<<>>%'">Start With</asp:ListItem>
                                                                                <asp:ListItem Value="LIKE '%<<>>'">Ends With</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td nowrap>
                                                                            <asp:TextBox ID="TXT_OPT2_KeyWords" runat="server" CssClass="TEXTBOX"></asp:TextBox>&nbsp;<asp:Button
                                                                                ID="BTN_Clear2" runat="server" CssClass="green2-btn" Text="X" OnClick="BTN_Clear_Click"
                                                                                Ind="2"></asp:Button>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="BOLD" align="right">
                                                                            Search Option Three
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="DDL_OPT3_Field" runat="server" CssClass="DROPDOWNLIST">
                                                                                <asp:ListItem Value="">---</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement CP3 ON CP3.CPMeasurementGUID = PMD.CPMeasurementGUID AND CP3.[User]">User</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement CP3 ON CP3.CPMeasurementGUID = PMD.CPMeasurementGUID AND CP3.Job">Job</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement CP3 ON CP3.CPMeasurementGUID = PMD.CPMeasurementGUID AND CP3.Info_Text">Info Text</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement CP3 ON CP3.CPMeasurementGUID = PMD.CPMeasurementGUID AND CP3.Customer">Customer</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP3 ON DP3.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP3.Substrate">Substrate Manufacturer</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP3 ON DP3.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP3.Substrate_SKU">Substrate SKU</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP3 ON DP3.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP3.Substrate_Batch">Substrate Batch</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP3 ON DP3.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP3.Ink">Ink Manufacturer</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP3 ON DP3.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP3.Ink_SKU">Ink SKU</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP3 ON DP3.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP3.Ink_Batch">Ink Batch</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP3 ON DP3.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP3.Chemistry">Chemistry Manufacturer</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP3 ON DP3.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP3.Chemistry_Batch">Chemistry Batch</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP3 ON DP3.CPMeasurementGUID = PMD.CPMeasurementGUID AND CONVERT( VARCHAR , DP3.Resolution )">Resolution</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP3 ON DP3.CPMeasurementGUID = PMD.CPMeasurementGUID AND CONVERT( VARCHAR , DP3.Line_Screen )">Dot Shape</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_OD OD3 ON OD3.CPMeasurementGUID = PMD.CPMeasurementGUID AND OD3.Category">Device Category</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_OD OD3 ON OD3.CPMeasurementGUID = PMD.CPMeasurementGUID AND OD3.Manufacturer">Device Manufacturer</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_OD OD3 ON OD3.CPMeasurementGUID = PMD.CPMeasurementGUID AND OD3.Model">Device Model</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_OD OD3 ON OD3.CPMeasurementGUID = PMD.CPMeasurementGUID AND OD3.RIP_Type">Rip Type</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_PT PT3 ON PT3.CPMeasurementGUID = PMD.CPMeasurementGUID INNER JOIN CPDevicePrintTarget CPPT3 ON CPPT3.CPDevicePrintTargetID = PT3.CPDevicePrintTargetID AND CPPT3.PrintTarget">Print Target Name</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_PT PT3 ON PT3.CPMeasurementGUID = PMD.CPMeasurementGUID AND PT3.Proof_Supplier">Print Target Supplier</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_MD MD3 ON MD3.CPMeasurementGUID = PMD.CPMeasurementGUID AND MD3.Instrument">Measurement Device</asp:ListItem>
                                                                                <asp:ListItem Value="INNER JOIN CPMeasurement_PT PT3 ON PT3.CPMeasurementGUID = PMD.CPMeasurementGUID AND PT3.Target_File">Target Name</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="DDL_OPT3_Operator" runat="server" CssClass="DROPDOWNLIST">
                                                                                <asp:ListItem Value="">---</asp:ListItem>
                                                                                <asp:ListItem Value="= '<<>>'">Equals</asp:ListItem>
                                                                                <asp:ListItem Value="<> '<<>>'">Does Not Equal</asp:ListItem>
                                                                                <asp:ListItem Value="LIKE '%<<>>%'">Contains</asp:ListItem>
                                                                                <asp:ListItem Value="NOT LIKE '%<<>>%'">Does Not Contain</asp:ListItem>
                                                                                <asp:ListItem Value="LIKE '<<>>%'">Start With</asp:ListItem>
                                                                                <asp:ListItem Value="LIKE '%<<>>'">Ends With</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td nowrap>
                                                                            <asp:TextBox ID="TXT_OPT3_KeyWords" runat="server" CssClass="TEXTBOX"></asp:TextBox>&nbsp;<asp:Button
                                                                                ID="BTN_Clear3" runat="server" CssClass="green2-btn" Text="X" OnClick="BTN_Clear_Click"
                                                                                Ind="3"></asp:Button>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="3">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Button ID="BTN_CreateReport" OnClick="BTN_CreateReport_Click" runat="server"
                                                                                CssClass="green2-btn" Text="Create Report"></asp:Button>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="5" height="10px">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="BTN_CreateReport" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel Style="width: 100%" ID="PNL_Result" runat="server">
                                                    <table style="width: 100%" cellspacing="0" cellpadding="0" align="center" border="0">
                                                        <tr>
                                                            <td class="BOLD" valign="middle" align="left" colspan="3">
                                                                <table style="width: 100%">
                                                                    <tr>
                                                                        <td align="center" width="95%" nowrap>
                                                                            <asp:Literal ID="LTRL_DeviceName" runat="server"></asp:Literal>&nbsp;Report<br />
                                                                            <asp:Literal ID="LTRL_Branch" runat="server"></asp:Literal>&nbsp;<asp:Literal ID="Label4"
                                                                                runat="server">-</asp:Literal>&nbsp;<asp:Literal ID="LTRL_CompanyName" runat="server"></asp:Literal>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="middle" align="left" colspan="3">
                                                                <strong>&nbsp;&nbsp;&nbsp;Date:</strong>
                                                                <asp:Literal ID="LTRL_DateRange" runat="server"></asp:Literal>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 49%" valign="top" align="center">
                                                                <table style="width: 100%" cellspacing="5" cellpadding="0" align="center" border="0">
                                                                    <tr>
                                                                        <td style="width: 10%;" valign="top" align="center">
                                                                            <table cellspacing="0" cellpadding="0" align="center" border="0" class="DATAGRID1"
                                                                                width="100%">
                                                                                <tr>
                                                                                    <td class="HEADERSTYLE1">
                                                                                        Device Information
                                                                                    </td>
                                                                                    <td class="HEADERSTYLE1">
                                                                                        Result
                                                                                    </td>
                                                                                    <td class="HEADERSTYLE1">
                                                                                        Search Field
                                                                                    </td>
                                                                                    <td class="HEADERSTYLE1">
                                                                                        Operator
                                                                                    </td>
                                                                                    <td class="HEADERSTYLE1">
                                                                                        Criteria
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="ITEMSTYLE1">
                                                                                    <td align="left">
                                                                                        &nbsp;No Of Reads
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:Literal ID="LTRL_NoOfReads" runat="server"></asp:Literal>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:Literal ID="LTRL_Field1" runat="server"></asp:Literal>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:Literal ID="LTRL_Operator1" runat="server"></asp:Literal>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:Literal ID="LTRL_Criteria1" runat="server"></asp:Literal>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="ALTERNATINGITEMSTYLE1">
                                                                                    <td align="left">
                                                                                        &nbsp;Avg Delta E
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:Literal ID="LTRL_AvgDeltaE" runat="server"></asp:Literal>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:Literal ID="LTRL_Field2" runat="server"></asp:Literal>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:Literal ID="LTRL_Operator2" runat="server"></asp:Literal>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:Literal ID="LTRL_Criteria2" runat="server"></asp:Literal>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="ITEMSTYLE1">
                                                                                    <td class="BOLD" align="left">
                                                                                        &nbsp;Avg Max Delta E
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:Literal ID="LTRL_AvgMaxDeltaE" runat="server"></asp:Literal>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:Literal ID="LTRL_Field3" runat="server"></asp:Literal>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:Literal ID="LTRL_Operator3" runat="server"></asp:Literal>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:Literal ID="LTRL_Criteria3" runat="server"></asp:Literal>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="ALTERNATINGITEMSTYLE1">
                                                                                    <td class="BOLD" align="left">
                                                                                        &nbsp;Error Ratio
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:Literal ID="LTRL_AvgErrorRate" runat="server"></asp:Literal>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        ---
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        ---
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        ---
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="top">
                                                                            <table class="DATAGRID1" cellspacing="0" cellpadding="0" border="0" align="center"
                                                                                width="100%">
                                                                                <tr>
                                                                                    <td class="HEADERSTYLE1" colspan="4" align="left">
                                                                                        <strong>Environmental</strong>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="HEADERSTYLE1">
                                                                                    <td style="width: 25%" align="center">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                    <td style="width: 25%" align="center">
                                                                                        Average
                                                                                    </td>
                                                                                    <td style="width: 25%" align="center">
                                                                                        Std. Dev
                                                                                    </td>
                                                                                    <td style="width: 25%" align="center">
                                                                                        Min
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="ITEMSTYLE1">
                                                                                    <td style="width: 25%" class="BOLD" align="right">
                                                                                        Temp:
                                                                                    </td>
                                                                                    <td style="width: 25%" align="center">
                                                                                        <asp:Literal ID="LTRL_TempAvg" runat="server"></asp:Literal>
                                                                                    </td>
                                                                                    <td style="width: 25%" align="center">
                                                                                        <asp:Literal ID="LTRL_TempStandard" runat="server"></asp:Literal>
                                                                                    </td>
                                                                                    <td style="width: 25%" align="center">
                                                                                        <asp:Literal ID="LTRL_TempMin" runat="server"></asp:Literal>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="ALTERNATINGITEMSTYLE1">
                                                                                    <td style="width: 25%" class="BOLD" align="right">
                                                                                        Rh:
                                                                                    </td>
                                                                                    <td style="width: 25%" align="center">
                                                                                        <asp:Literal ID="LTRL_RhAvg" runat="server"></asp:Literal>
                                                                                    </td>
                                                                                    <td style="width: 25%" align="center">
                                                                                        <asp:Literal ID="LTRL_RhStandard" runat="server"></asp:Literal>
                                                                                    </td>
                                                                                    <td style="width: 25%" align="center">
                                                                                        <asp:Literal ID="LTRL_RhMin" runat="server"></asp:Literal>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="TR_ConSKUInfo">
                                                                        <td valign="top">
                                                                            <table class="DATAGRID1" cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
                                                                                <tr>
                                                                                    <td class="HEADERSTYLE1" colspan="4" align="left">
                                                                                        <strong>Consumables SKU Information</strong>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:DataGrid ID="DGConSkuInfo" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                                            CellSpacing="1">
                                                                                            <Columns>
                                                                                                <asp:BoundColumn DataField="PAPER_SKU" HeaderText="Paper Sku">
                                                                                                    <ItemStyle HorizontalAlign="Left" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
                                                                                                        Font-Overline="False" Font-Bold="False"></ItemStyle>
                                                                                                    <HeaderStyle Width="25%"></HeaderStyle>
                                                                                                </asp:BoundColumn>
                                                                                                <asp:BoundColumn DataField="MEASUREMENTS" HeaderText="Measurements" ItemStyle-HorizontalAlign="Center">
                                                                                                    <HeaderStyle Width="25%"></HeaderStyle>
                                                                                                </asp:BoundColumn>
                                                                                                <asp:BoundColumn DataField="ErrorRate" HeaderText="Error Ratio" DataFormatString="{0:f2}%"
                                                                                                    ItemStyle-HorizontalAlign="Center">
                                                                                                    <HeaderStyle Width="25%"></HeaderStyle>
                                                                                                </asp:BoundColumn>
                                                                                                <asp:BoundColumn DataField="AVG_DE" HeaderText="Ave Delta E" DataFormatString="{0:f2}"
                                                                                                    ItemStyle-HorizontalAlign="Center">
                                                                                                    <HeaderStyle Width="25%"></HeaderStyle>
                                                                                                </asp:BoundColumn>
                                                                                            </Columns>
                                                                                        </asp:DataGrid>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:DataGrid ID="DGInkSku" runat="server" CssClass="DATAGRID" Width="100%" AutoGenerateColumns="False"
                                                                                            CellSpacing="1">
                                                                                            <Columns>
                                                                                                <asp:BoundColumn DataField="INK_SKU" HeaderText="Ink Sku">
                                                                                                    <ItemStyle HorizontalAlign="Left" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
                                                                                                        Font-Overline="False" Font-Bold="False"></ItemStyle>
                                                                                                    <HeaderStyle Width="25%"></HeaderStyle>
                                                                                                </asp:BoundColumn>
                                                                                                <asp:BoundColumn DataField="MEASUREMENTS" HeaderText="Measurements" ItemStyle-HorizontalAlign="Center">
                                                                                                    <HeaderStyle Width="25%"></HeaderStyle>
                                                                                                </asp:BoundColumn>
                                                                                                <asp:BoundColumn DataField="ErrorRate" HeaderText="Error Ratio" DataFormatString="{0:f2}%"
                                                                                                    ItemStyle-HorizontalAlign="Center">
                                                                                                    <HeaderStyle Width="25%"></HeaderStyle>
                                                                                                </asp:BoundColumn>
                                                                                                <asp:BoundColumn DataField="AVG_DE" HeaderText="Ave Delta E" DataFormatString="{0:f2}"
                                                                                                    ItemStyle-HorizontalAlign="Center">
                                                                                                    <HeaderStyle Width="25%"></HeaderStyle>
                                                                                                </asp:BoundColumn>
                                                                                            </Columns>
                                                                                        </asp:DataGrid>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="TR_ConBatchInfo">
                                                                        <td valign="top" align="center">
                                                                            <table class="DATAGRID1" cellspacing="0" cellpadding="0" border="0" width="100%">
                                                                                <tr>
                                                                                    <td class="HEADERSTYLE1" align="left" colspan="4">
                                                                                        <strong>Consumables Batch Information</strong>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:DataGrid ID="DGConBatchInfo" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                                            CellSpacing="1">
                                                                                            <Columns>
                                                                                                <asp:BoundColumn DataField="Substrate_Batch" HeaderText="Substrate Batch">
                                                                                                    <ItemStyle HorizontalAlign="Left" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
                                                                                                        Font-Overline="False" Font-Bold="False"></ItemStyle>
                                                                                                    <HeaderStyle Width="25%"></HeaderStyle>
                                                                                                </asp:BoundColumn>
                                                                                                <asp:BoundColumn DataField="MEASUREMENTS" HeaderText="Measurements" ItemStyle-HorizontalAlign="Center">
                                                                                                    <HeaderStyle Width="25%"></HeaderStyle>
                                                                                                </asp:BoundColumn>
                                                                                                <asp:BoundColumn DataField="ErrorRate" HeaderText="Error Rate" DataFormatString="{0:f2}%"
                                                                                                    ItemStyle-HorizontalAlign="Center">
                                                                                                    <HeaderStyle Width="25%"></HeaderStyle>
                                                                                                </asp:BoundColumn>
                                                                                                <asp:BoundColumn DataField="AVG_DE" HeaderText="Ave Delta E" DataFormatString="{0:f2}"
                                                                                                    ItemStyle-HorizontalAlign="Center">
                                                                                                    <HeaderStyle Width="25%"></HeaderStyle>
                                                                                                </asp:BoundColumn>
                                                                                            </Columns>
                                                                                        </asp:DataGrid>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:DataGrid ID="DGInkBatch" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                                            CellSpacing="1">
                                                                                            <Columns>
                                                                                                <asp:BoundColumn DataField="Ink_Batch" HeaderText="InkBatch">
                                                                                                    <ItemStyle HorizontalAlign="Left" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
                                                                                                        Font-Overline="False" Font-Bold="False"></ItemStyle>
                                                                                                    <HeaderStyle Width="25%"></HeaderStyle>
                                                                                                </asp:BoundColumn>
                                                                                                <asp:BoundColumn DataField="MEASUREMENTS" HeaderText="Measurements" ItemStyle-HorizontalAlign="Center">
                                                                                                    <HeaderStyle Width="25%"></HeaderStyle>
                                                                                                </asp:BoundColumn>
                                                                                                <asp:BoundColumn DataField="ErrorRate" HeaderText="Error Rate" DataFormatString="{0:f2}%"
                                                                                                    ItemStyle-HorizontalAlign="Center">
                                                                                                    <HeaderStyle Width="25%"></HeaderStyle>
                                                                                                </asp:BoundColumn>
                                                                                                <asp:BoundColumn DataField="AVG_DE" HeaderText="Ave Delta E" DataFormatString="{0:f2}"
                                                                                                    ItemStyle-HorizontalAlign="Center">
                                                                                                    <HeaderStyle Width="25%"></HeaderStyle>
                                                                                                </asp:BoundColumn>
                                                                                            </Columns>
                                                                                        </asp:DataGrid>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="top">
                                                                            <table class="DATAGRID1" cellspacing="0" cellpadding="0" border="0" align="center"
                                                                                width="100%">
                                                                                <tr>
                                                                                    <td class="HEADERSTYLE1" colspan="4" align="left">
                                                                                        Consumables Stability
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="HEADERSTYLE1">
                                                                                        Consumable
                                                                                    </td>
                                                                                    <td class="HEADERSTYLE1">
                                                                                        Ave Delta E
                                                                                    </td>
                                                                                    <td class="HEADERSTYLE1">
                                                                                        Ave Hue Delta
                                                                                    </td>
                                                                                    <td class="HEADERSTYLE1">
                                                                                        Ave L* Delta
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="ITEMSTYLE1">
                                                                                    <td class="BOLD" align="left">
                                                                                        &nbsp;Cyan
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:Literal ID="LTRL_CyanAvgDeltaE" runat="server"></asp:Literal>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:Literal ID="LTRL_CAveHueDelta" runat="server"></asp:Literal>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:Literal ID="LTRL_CAVELDelta" runat="server"></asp:Literal>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="ALTERNATINGITEMSTYLE1">
                                                                                    <td class="BOLD" align="left">
                                                                                        &nbsp;Magenta
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:Literal ID="LTRL_MagentaAvgDeltaE" runat="server"></asp:Literal>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:Literal ID="LTRL_MAveHueDelta" runat="server"></asp:Literal>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:Literal ID="LTRL_MAveLDelta" runat="server"></asp:Literal>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="ITEMSTYLE1">
                                                                                    <td class="BOLD" align="left">
                                                                                        &nbsp;Yellow
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:Literal ID="LTRL_YellowAvgDeltaE" runat="server"></asp:Literal>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:Literal ID="LTRL_YAveHueDelta" runat="server"></asp:Literal>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:Literal ID="LTRL_YAveLDelta" runat="server"></asp:Literal>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="ALTERNATINGITEMSTYLE1">
                                                                                    <td class="BOLD" align="left">
                                                                                        &nbsp;Black
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:Literal ID="LTRL_BlackAvgDeltaE" runat="server"></asp:Literal>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:Literal ID="LTRL_BAveHueDelta" runat="server"></asp:Literal>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:Literal ID="LTRL_BAveLDelta" runat="server"></asp:Literal>
                                                                                    </tdLiteral
                                                                                </tr>
                                                                                <tr class="ITEMSTYLE1">
                                                                                    <td class="BOLD" align="left">
                                                                                        &nbsp;Paper
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:Literal ID="LTRL_PaperAveDelta" runat="server"></asp:Literal>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:Literal ID="LTRL_PaperAveHueDelta" runat="server"></asp:Literal>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:Literal ID="LTRL_PaperAveLDelta" runat="server"></asp:Literal>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="width: 1%">
                                                            </td>
                                                            <td style="width: 50%" valign="top" align="center">
                                                                <table style="width: 100%" cellspacing="5" cellpadding="0" align="center" border="0">
                                                                    <tr>
                                                                        <td style="width: 100%" valign="top" align="center">
                                                                            <table style="width: 100%" cellspacing="0" cellpadding="0" align="center" border="0">
                                                                                <DCWC:Chart 
                                                                                    ID="ChartPie1" 
                                                                                    runat="server" 
                                                                                    ImageUrl="UploadFiles/temp/ColorDetail_1"
                                                                                    Width="450px" 
                                                                                    ImageType="Jpeg" 
                                                                                    Height="250px" 
                                                                                    BackGradientEndColor="#EFEFEF" 
                                                                                    BorderLineColor="#808080" 
                                                                                    BorderLineStyle="Solid" 
                                                                                    Palette="Kindergarten" 
                                                                                    BackGradientType="DiagonalLeft">
                                                                                    <Legends>
                                                                                        <DCWC:Legend Alignment="Center" BackColor="150, 255, 255, 255" BorderColor="LightSlateGray"
                                                                                            Docking="Bottom" Name="Default">
                                                                                        </DCWC:Legend>
                                                                                    </Legends>
                                                                                    <ChartAreas>
                                                                                        <DCWC:ChartArea Name="Default" BackColor="White" BackGradientEndColor="White" BorderColor="#D6E1C1"
                                                                                            BorderStyle="Solid">
                                                                                            <AxisY>
                                                                                                <MajorGrid LineColor="65, 0, 0, 0" />
                                                                                                <MinorGrid LineColor="30, 0, 0, 0" />
                                                                                                <MinorTickMark Size="2" />
                                                                                            </AxisY>
                                                                                            <AxisX>
                                                                                                <MajorGrid LineColor="65, 0, 0, 0" />
                                                                                                <MinorGrid LineColor="30, 0, 0, 0" />
                                                                                                <MinorTickMark Size="2" />
                                                                                            </AxisX>
                                                                                            <Area3DStyle WallWidth="0" />
                                                                                        </DCWC:ChartArea>
                                                                                    </ChartAreas>
                                                                                    <Titles>
                                                                                        <DCWC:Title Name="Title1" Text="Failure Rate">
                                                                                        </DCWC:Title>
                                                                                    </Titles>
                                                                                    <Series>
                                                                                        <DCWC:Series BorderColor="100, 0, 0, 0" Name="Default" ChartType="Pie" YValuesPerPoint="4"
                                                                                            BackGradientEndColor="162, 164, 255" CustomAttributes="PieDrawingStyle=Concave">
                                                                                        </DCWC:Series>
                                                                                    </Series>
                                                                                    <BorderSkin FrameBackColor="white" FrameBackGradientEndColor="white" FrameBorderColor="white"
                                                                                        FrameBorderWidth="2" PageColor="white" SkinStyle="Raised" />
                                                                                    <UI>
                                                                                        <Toolbar BorderColor="#D6E1C1" BackColor="White">
                                                                                            <BorderSkin SkinStyle="Raised" FrameBackColor="SteelBlue" FrameBackGradientEndColor="LightBlue"
                                                                                                PageColor="Transparent" />
                                                                                        </Toolbar>
                                                                                    </UI>
                                                                                </DCWC:Chart>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="middle" align="center">
                                                                            <table class="DATAGRID" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                <tr class="HEADERSTYLE">
                                                                                    <td style="width: 11%" id="TD_Sample" runat="server" align="center">
                                                                                        <asp:LinkButton ID="Lnk_Sample" OnClick="Lnk_Sample_Click" runat="server">Patch</asp:LinkButton>
                                                                                    </td>
                                                                                    <td style="width: 12%" id="TD_C" runat="Server" align="center">
                                                                                        <asp:LinkButton ID="Lnk_Cyan" OnClick="Lnk_Cyan_Click" runat="server">C</asp:LinkButton>
                                                                                    </td>
                                                                                    <td style="width: 12%" id="TD_M" runat="server" align="center">
                                                                                        <asp:LinkButton ID="Lnk_Margenta" OnClick="Lnk_Margenta_Click" runat="server">M </asp:LinkButton>
                                                                                    </td>
                                                                                    <td style="width: 12%" id="TD_Y" runat="server" align="center">
                                                                                        <asp:LinkButton ID="Lnk_Yellow" OnClick="Lnk_Yellow_Click" runat="server">Y</asp:LinkButton>
                                                                                    </td>
                                                                                    <td style="width: 12%" id="Td_B" runat="server" align="center">
                                                                                        <asp:LinkButton ID="Lnk_Black" OnClick="Lnk_Black_Click" runat="server">K</asp:LinkButton>
                                                                                    </td>
                                                                                    <td style="width: 6%" align="center">
                                                                                        <asp:Literal ID="LTRL_Col" runat="server" Text="Color"></asp:Literal>
                                                                                    </td>
                                                                                    <td style="width: 16%" id="TD_Avg_De" runat="server" align="center" class="gridHeaderSortASC">
                                                                                        <asp:LinkButton ID="Lnk_Avg_De" OnClick="Lnk_Avg_De_Click" runat="server">Ave. De</asp:LinkButton>
                                                                                    </td>
                                                                                    <td style="width: 16%" id="TD_Std" runat="server" align="center">
                                                                                        <asp:LinkButton ID="Lnk_Std_Dev" OnClick="Lnk_Std_Dev_Click" runat="server">Std.Dev</asp:LinkButton>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="DATAGRID1" valign="top" align="center">
                                                                            <div style="overflow: auto; height: 400px; width: 100%;">
                                                                                <asp:Literal ID="LTRL_SortExpression1" runat="server" Visible="false">AVG_DE , STDEV_DE</asp:Literal>
                                                                                <asp:Literal ID="LTRL_SortOrder1" runat="server" Visible="false">DESC</asp:Literal>
                                                                                <asp:DataGrid ID="DGPatches" runat="Server" CssClass="DATAGRID" Width="97%" AutoGenerateColumns="False"
                                                                                    CellSpacing="1" OnItemDataBound="DGPatches_ItemDataBound" ShowHeader="False">
                                                                                    <AlternatingItemStyle CssClass="ALTERNATINGSTYLE1"></AlternatingItemStyle>
                                                                                    <ItemStyle CssClass="ITEMSTYLE1"></ItemStyle>
                                                                                    <Columns>
                                                                                        <asp:BoundColumn DataField="Sample_Name">
                                                                                            <ItemStyle Width="13%" HorizontalAlign="Left" Font-Italic="False" Font-Strikeout="False"
                                                                                                Font-Underline="False" Font-Overline="False" Font-Bold="False"></ItemStyle>
                                                                                        </asp:BoundColumn>
                                                                                        <asp:BoundColumn DataField="CMYK_C">
                                                                                            <ItemStyle Width="12%"></ItemStyle>
                                                                                        </asp:BoundColumn>
                                                                                        <asp:BoundColumn DataField="CMYK_M">
                                                                                            <ItemStyle Width="12%"></ItemStyle>
                                                                                        </asp:BoundColumn>
                                                                                        <asp:BoundColumn DataField="CMYK_Y">
                                                                                            <ItemStyle Width="12%"></ItemStyle>
                                                                                        </asp:BoundColumn>
                                                                                        <asp:BoundColumn DataField="CMYK_K">
                                                                                            <ItemStyle Width="12%"></ItemStyle>
                                                                                        </asp:BoundColumn>
                                                                                        <asp:TemplateColumn>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="LBL_Color" runat="server"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateColumn>
                                                                                        <asp:BoundColumn DataField="AVG_DE" DataFormatString="{0:f2}">
                                                                                            <ItemStyle Width="12%"></ItemStyle>
                                                                                        </asp:BoundColumn>
                                                                                        <asp:BoundColumn DataField="STDEV_DE" DataFormatString="{0:f2}">
                                                                                            <ItemStyle Width="12%"></ItemStyle>
                                                                                        </asp:BoundColumn>
                                                                                    </Columns>
                                                                                </asp:DataGrid></div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="3">
                                                                <DCWC:Chart ID="ChartLine1" runat="server" ImageUrl="UploadFiles/temp/ColorDetail_2"
                                                                    Width="900px" ImageType="Jpeg" BackGradientType="DiagonalLeft" Palette="Kindergarten"
                                                                    BackGradientEndColor="#EFEFEF" BorderLineColor="#808080" BorderLineStyle="Solid">
                                                                    <Legends>
                                                                        <DCWC:Legend Name="Default">
                                                                        </DCWC:Legend>
                                                                    </Legends>
                                                                    <Series>
                                                                        <DCWC:Series BorderColor="100, 0, 0, 0" Name="Default" CustomAttributes="PieDrawingStyle=Concave"
                                                                            BackGradientEndColor="162, 164, 255" YValuesPerPoint="4">
                                                                        </DCWC:Series>
                                                                    </Series>
                                                                    <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="White" FrameBorderWidth="2"
                                                                        SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                                                                    <UI>
                                                                        <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                                                            <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                                                                FrameBackColor="SteelBlue"></BorderSkin>
                                                                        </Toolbar>
                                                                    </UI>
                                                                    <Titles>
                                                                        <DCWC:Title Text="Delta E Average" Name="Title1">
                                                                        </DCWC:Title>
                                                                    </Titles>
                                                                    <DCWC:Legend Alignment="Center" Docking="Bottom" BorderColor="Black" Name="Default">
                                                                    </DCWC:Legend>
                                                                    <ChartAreas>
                                                                        <DCWC:ChartArea BackGradientEndColor="White" BorderColor="LightSlateGray" BackColor="White"
                                                                            BorderStyle="Solid" Name="Default">
                                                                            <AxisX>
                                                                                <MinorGrid LineColor="30, 0, 0, 0"></MinorGrid>
                                                                                <MajorGrid LineColor="65, 0, 0, 0"></MajorGrid>
                                                                                <MinorTickMark Size="2"></MinorTickMark>
                                                                            </AxisX>
                                                                            <Area3DStyle WallWidth="0"></Area3DStyle>
                                                                            <AxisY>
                                                                                <MinorGrid LineColor="30, 0, 0, 0"></MinorGrid>
                                                                                <MajorGrid LineColor="65, 0, 0, 0"></MajorGrid>
                                                                                <MinorTickMark Size="2"></MinorTickMark>
                                                                            </AxisY>
                                                                        </DCWC:ChartArea>
                                                                    </ChartAreas>
                                                                </DCWC:Chart>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" colspan="3">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" colspan="3">
                                                                <asp:Button ID="BTN_MakePDF" OnClick="BTN_MakePDF_Click" runat="server" CssClass="green2-btn"
                                                                    Text="Generate PDF"></asp:Button>
                                                                &nbsp;<asp:Button ID="BTN_BackToSearch" OnClick="BTN_BackToSearch_Click" runat="server"
                                                                    CssClass="green2-btn" Text="Back To Search"></asp:Button>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="BTN_MakePDF" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>

