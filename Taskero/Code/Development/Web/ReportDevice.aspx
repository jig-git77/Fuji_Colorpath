<%@ Page Language="C#" MasterPageFile="~/Home.master"  EnableEventValidation="false" AutoEventWireup="true" CodeFile="ReportDevice.aspx.cs"
    Inherits="ReportDevice" %>
<%@ Register Src="Common/ReportMenu.ascx" TagName="ReportMenu" TagPrefix="uc1" %>
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
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <table style="width: 100%" cellspacing="0" cellpadding="0" align="center" border="0">
                                                    <tr>
                                                        <td class="green-title" valign="middle" align="left" colspan="4">
                                                            <asp:Label ID="lblReportTitle" runat="server" Text="Device Summary Report(Search Criteria)"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" class="green-form-bg" style="border-bottom: none" id="TDReportTitle"
                                                            runat="server">
                                                            <asp:Label ID="LblError" runat="server" CssClass="ERROR"></asp:Label><asp:Literal
                                                                ID="LTRL_CPMeasurementGUID" runat="server" Visible="false"></asp:Literal>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel Style="width: 100%" ID="PNL_Search" runat="server">
                                                    <table cellspacing="0" cellpadding="0" align="center" border="0" style="width: 100%">
                                                        <tbody>
                                                            <tr>
                                                                <td align="center" valign="top" class="green-form-bg" style="border-top: none;">
                                                                    <table width="80%" border="0" cellspacing="2" cellpadding="3" class="form-text">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td colspan="4">
                                                                                    &nbsp;
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 25%" align="center">
                                                                                    Company
                                                                                </td>
                                                                                <td style="width: 25%" align="center">
                                                                                    Branch
                                                                                </td>
                                                                                <td style="width: 25%" align="center">
                                                                                    Dongle No.
                                                                                </td>
                                                                                <td style="width: 25%" align="center">
                                                                                    Device
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center">
                                                                                    <asp:DropDownList ID="DDL_Company" runat="server" OnSelectedIndexChanged="FillBranch"
                                                                                        AutoPostBack="true" CssClass="DROPDOWNLIST">
                                                                                        <asp:ListItem Value="0">--- Select Company ---</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td align="center">
                                                                                    <asp:DropDownList ID="DDL_Branch" runat="server" OnSelectedIndexChanged="FillDongle"
                                                                                        AutoPostBack="true" CssClass="DROPDOWNLIST">
                                                                                        <asp:ListItem Value="0">--- Select Branch ---</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td align="center">
                                                                                    <asp:DropDownList ID="DDL_DongleNo" runat="server" OnSelectedIndexChanged="FillDevice"
                                                                                        AutoPostBack="true" CssClass="DROPDOWNLIST">
                                                                                        <asp:ListItem Value="0">--- Select Dongle ---</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td align="center">
                                                                                    <asp:DropDownList ID="DDL_Device" runat="server" OnSelectedIndexChanged="FillDates"
                                                                                        AutoPostBack="true" CssClass="DROPDOWNLIST">
                                                                                        <asp:ListItem Value="0">--- Select Device ---</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" valign="baseline">
                                                                                    Search By Date
                                                                                </td>
                                                                                <td valign="top" colspan="3">
                                                                                    From&nbsp;<asp:TextBox ID="TXT_FromDate" runat="server" AutoPostBack="True" CssClass="TEXTBOX"
                                                                                        OnTextChanged="TXT_DateText" Columns="10" MaxLength="10" Width="75px"></asp:TextBox>&nbsp;<asp:ImageButton
                                                                                            ID="IBTN_FromDate" runat="server" ImageAlign="AbsMiddle" ImageUrl="Images/calendar-icon.png">
                                                                                        </asp:ImageButton><ajaxToolkit:CalendarExtender ID="Cal_FromDate" runat="server"
                                                                                            PopupButtonID="IBTN_FromDate" TargetControlID="TXT_FromDate">
                                                                                        </ajaxToolkit:CalendarExtender>
                                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; To&nbsp;<asp:TextBox ID="TXT_ToDate" runat="server"
                                                                                        AutoPostBack="True" CssClass="TEXTBOX" OnTextChanged="TXT_DateText" Columns="10"
                                                                                        MaxLength="10" Width="75px"></asp:TextBox>&nbsp;<asp:ImageButton ID="IBTN_ToDate"
                                                                                            runat="server" ImageAlign="AbsMiddle" ImageUrl="Images/calendar-icon.png">
                                                                                    </asp:ImageButton><ajaxToolkit:CalendarExtender ID="Cal_ToDate" runat="server" PopupButtonID="IBTN_ToDate"
                                                                                        TargetControlID="TXT_ToDate">
                                                                                    </ajaxToolkit:CalendarExtender>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td align="center">
                                                                                    Field
                                                                                </td>
                                                                                <td align="center">
                                                                                    Operator
                                                                                </td>
                                                                                <td align="center">
                                                                                    Criteria
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right">
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
                                                                                <td align="right">
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
                                                                                <td align="right">
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
                                                                                    <asp:Button ID="BTN_CreateReport" OnClientClick="Search()" OnClick="BTN_CreateReport_Click"
                                                                                        runat="server" CssClass="green2-btn" Text="Create Report"></asp:Button>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="4" height="10px">
                                                                                    &nbsp;
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </asp:Panel>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="BTN_CreateReport" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel Style="width: 100%" ID="PNL_Result" runat="server">
                                                    <table cellspacing="0" cellpadding="0" align="center" border="0" style="width: 100%">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <%--<img src="../Images/powered-by.gif" hspace="5" vspace="5" align="right" />--%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table cellspacing="2" cellpadding="0" width="100%" align="center" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style="width: 50%" valign="top">
                                                                                    <table cellspacing="5" cellpadding="0" width="100%" align="center" border="0">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td style="width: 100%" valign="top">
                                                                                                    <table class="DATAGRID1" cellspacing="1" cellpadding="0" width="100%">
                                                                                                        <tbody>
                                                                                                            <tr class="HEADERSTYLE1">
                                                                                                                <td colspan="2">
                                                                                                                    &nbsp;Device Report
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr class="ITEMSTYLE1">
                                                                                                                <td style="width: 35%">
                                                                                                                    <b>&nbsp;Date Range</b>
                                                                                                                </td>
                                                                                                                <td style="width: 65%">
                                                                                                                    <asp:Literal ID="LTRL_DateRange" runat="server"></asp:Literal>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr class="ALTERNATINGITEMSTYLE1">
                                                                                                                <td>
                                                                                                                    <b>&nbsp;Device</b>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Literal ID="LTRL_DeviceName" runat="server"></asp:Literal>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr class="ITEMSTYLE1">
                                                                                                                <td>
                                                                                                                    <b>&nbsp;Dongle Number</b>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Literal ID="LTRL_DongleNumber" runat="server"></asp:Literal>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr class="ALTERNATINGITEMSTYLE1">
                                                                                                                <td>
                                                                                                                    <b>&nbsp;Location Name</b>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Literal ID="LTRL_BranchName" runat="server"></asp:Literal>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr class="ITEMSTYLE1">
                                                                                                                <td>
                                                                                                                    <b>&nbsp;Company</b>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Literal ID="LTRL_CompanyName" runat="server"></asp:Literal>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </tbody>
                                                                                                    </table>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="width: 100%" valign="top">
                                                                                                    <table class="DATAGRID1" cellspacing="1" cellpadding="0" width="100%">
                                                                                                        <tbody>
                                                                                                            <tr class="HEADERSTYLE1">
                                                                                                                <td colspan="2">
                                                                                                                    &nbsp;Failure Rate
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr class="ITEMSTYLE1">
                                                                                                                <td style="width: 60%">
                                                                                                                    <b>&nbsp;Number of Measurements</b>
                                                                                                                </td>
                                                                                                                <td style="width: 40%">
                                                                                                                    <asp:Literal ID="LTRL_NumofMeasurements" runat="server"></asp:Literal>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr class="ALTERNATINGITEMSTYLE1">
                                                                                                                <td>
                                                                                                                    <b>&nbsp;Passed</b>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Literal ID="LTRL_Passed" runat="server"></asp:Literal>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr class="ITEMSTYLE1">
                                                                                                                <td>
                                                                                                                    <b>&nbsp;Failed</b>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Literal ID="LTRL_Failed" runat="server"></asp:Literal>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr class="ALTERNATINGITEMSTYLE1">
                                                                                                                <td>
                                                                                                                    <b>&nbsp;Cautions</b>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Literal ID="LTRL_Cautions" runat="server"></asp:Literal>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </tbody>
                                                                                                    </table>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr runat="server" id="TR_Env">
                                                                                                <td style="width: 100%" valign="top">
                                                                                                    <table class="DATAGRID1" cellspacing="0" cellpadding="0" width="100%">
                                                                                                        <tbody>
                                                                                                            <tr class="HEADERSTYLE1">
                                                                                                                <td colspan="2">
                                                                                                                    &nbsp;Environmental
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr class="ITEMSTYLE1">
                                                                                                                <td style="width: 60%">
                                                                                                                    <b>&nbsp;Ave Temp</b>
                                                                                                                </td>
                                                                                                                <td style="width: 40%">
                                                                                                                    <asp:Literal ID="LTRL_AveTemp" runat="server"></asp:Literal>°F
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr class="ALTERNATINGITEMSTYLE1">
                                                                                                                <td>
                                                                                                                    <b>&nbsp;Target Temp</b>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Literal ID="LTRL_TargetTemp" runat="server"></asp:Literal>°F
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr class="ITEMSTYLE1">
                                                                                                                <td>
                                                                                                                    <b>&nbsp;Min Temp</b>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Literal ID="LTRL_MinTemp" runat="server"></asp:Literal>°F
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr class="ALTERNATINGITEMSTYLE1">
                                                                                                                <td>
                                                                                                                    <b>&nbsp;Max Temp</b>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Literal ID="LTRL_MaxTemp" runat="server"></asp:Literal>°F
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr class="ITEMSTYLE1">
                                                                                                                <td>
                                                                                                                    <b>&nbsp;Min RH</b>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Literal ID="LTRL_MinRH" runat="server"></asp:Literal>%
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr class="ALTERNATINGITEMSTYLE1">
                                                                                                                <td>
                                                                                                                    <b>&nbsp;Max RH</b>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Literal ID="LTRL_MaxRH" runat="server"></asp:Literal>%
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </tbody>
                                                                                                    </table>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </td>
                                                                                <td style="width: 50%" valign="top">
                                                                                    <table cellspacing="5" cellpadding="0" width="100%" align="center" border="0">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td style="width: 100%" valign="top" align="center">
                                                                                                    <table class="DATAGRID1" cellspacing="0" cellpadding="0" border="0" width="100%">
                                                                                                        <tbody>
                                                                                                            <tr>
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
                                                                                                                <td align="center">
                                                                                                                    <asp:Literal ID="LTRL_Field1" runat="server"></asp:Literal>
                                                                                                                </td>
                                                                                                                <td align="center">
                                                                                                                    <asp:Literal ID="LTRL_Operator1" runat="server"></asp:Literal>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Literal ID="LTRL_Criteria1" runat="server"></asp:Literal>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr class="ALTERNATINGITEMSTYLE1">
                                                                                                                <td align="center">
                                                                                                                    <asp:Literal ID="LTRL_Field2" runat="server"></asp:Literal>
                                                                                                                </td>
                                                                                                                <td align="center">
                                                                                                                    <asp:Literal ID="LTRL_Operator2" runat="server"></asp:Literal>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Literal ID="LTRL_Criteria2" runat="server"></asp:Literal>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr class="ITEMSTYLE1">
                                                                                                                <td align="center">
                                                                                                                    <asp:Literal ID="LTRL_Field3" runat="server"></asp:Literal>
                                                                                                                </td>
                                                                                                                <td align="center">
                                                                                                                    <asp:Literal ID="LTRL_Operator3" runat="server"></asp:Literal>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Literal ID="LTRL_Criteria3" runat="server"></asp:Literal>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </tbody>
                                                                                                    </table>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="padding-right: 2px; padding-left: 2px; padding-bottom: 2px; padding-top: 2px"
                                                                                                    valign="middle" align="center">
                                                                                                    <DCWC:Chart ID="ChartPie1" runat="server" ImageUrl="UploadFiles/temp/DeviceReport1"
                                                                                                        ImageType="Jpeg" BackGradientType="DiagonalLeft" Palette="Kindergarten" BackGradientEndColor="#EFEFEF"
                                                                                                        BorderLineColor="#808080" BorderLineStyle="Solid" Height="350px" Width="450px">
                                                                                                        <Legends>
                                                                                                            <DCWC:Legend Alignment="Center" BackColor="150, 255, 255, 255" BorderColor="LightSlateGray"
                                                                                                                Docking="Bottom" Name="Default">
                                                                                                            </DCWC:Legend>
                                                                                                        </Legends>
                                                                                                        <ChartAreas>
                                                                                                            <DCWC:ChartArea Name="Default" BackColor="White" BackGradientEndColor="White" BorderColor="LightSlateGray"
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
                                                                                                            <Toolbar BorderColor="LightSlateGray" BackColor="White">
                                                                                                                <BorderSkin SkinStyle="Raised" FrameBackColor="SteelBlue" FrameBackGradientEndColor="LightBlue"
                                                                                                                    PageColor="Transparent" />
                                                                                                            </Toolbar>
                                                                                                        </UI>
                                                                                                    </DCWC:Chart>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr id="LBL_InkConsumables" runat="Server">
                                                                <td align="center" colspan="2" style="padding-left: 5px; padding-right: 5px;">
                                                                    <table class="DATAGRID1" cellspacing="0" cellpadding="0" border="0" width="100%">
                                                                        <tbody>
                                                                            <tr class="HEADERSTYLE1">
                                                                                <td align="left" colspan="6">
                                                                                    &nbsp;Consumables
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="ALTERNATINGITEMSTYLE1">
                                                                                <!-- <td align="right">
                                                                                    &nbsp;
                                                                                </td>-->
                                                                                <td style="width: 15%">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td style="width: 15%" nowrap>
                                                                                    <b>Measurements</b>
                                                                                </td>
                                                                                <td style="width: 15%" nowrap>
                                                                                    <b>Delta E Ave</b>
                                                                                </td>
                                                                                <td style="width: 15%" nowrap>
                                                                                    <b>Delta Max</b>
                                                                                </td>
                                                                                <td style="width: 15%" nowrap>
                                                                                    <b>Density Ave</b>
                                                                                </td>
                                                                                <td style="width: 15%">
                                                                                    <b>Std. Deviation Density</b>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="ITEMSTYLE1">
                                                                               <!-- <td align="right">
                                                                                    &nbsp;
                                                                                </td>-->
                                                                                <td align="right">
                                                                                    <b>Paper</b>
                                                                                </td>
                                                                                <td align="center">
                                                                                    <asp:Literal ID="LTRL_PaperMeasurements" runat="server"></asp:Literal>
                                                                                </td>
                                                                                <td align="center">
                                                                                    <asp:Literal ID="LTRL_PaperDeltaEAve" runat="server"></asp:Literal>
                                                                                </td>
                                                                                <td align="center">
                                                                                    <asp:Literal ID="LTRL_PaperDeltaMax" runat="server"></asp:Literal>
                                                                                </td>
                                                                                <td align="center">
                                                                                    <asp:Literal ID="LTRL_PaperDeltaDensityAve" runat="server"></asp:Literal>
                                                                                </td>
                                                                                <td align="center">
                                                                                    <asp:Literal ID="LTRL_PaperStdDev" runat="server"></asp:Literal>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="ALTERNATINGITEMSTYLE1">
                                                                               <!-- <td align="right">
                                                                                    &nbsp;
                                                                                </td>-->
                                                                                <td align="right">
                                                                                    <b>Ink Cyan</b>
                                                                                </td>
                                                                                <td align="center">
                                                                                    <asp:Literal ID="LTRL_InkCyanMeasurements" runat="server"></asp:Literal>
                                                                                </td>
                                                                                <td align="center">
                                                                                    <asp:Literal ID="LTRL_InkCyanDeltaEAve" runat="server"></asp:Literal>
                                                                                </td>
                                                                                <td align="center">
                                                                                    <asp:Literal ID="LTRL_InkCyanDeltaMax" runat="server"></asp:Literal>
                                                                                </td>
                                                                                <td align="center">
                                                                                    <asp:Literal ID="LTRL_InkCyanDeltaDensityAve" runat="server"></asp:Literal>
                                                                                </td>
                                                                                <td align="center">
                                                                                    <asp:Literal ID="LTRL_InkCyanStdDev" runat="server"></asp:Literal>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="ITEMSTYLE1">
                                                                              <!-- <td align="right">
                                                                                    &nbsp;
                                                                                </td>-->
                                                                                <td align="right">
                                                                                    <b>Ink Magenta</b>
                                                                                </td>
                                                                                <td align="center">
                                                                                    <asp:Literal ID="LTRL_InkMagentaMeasurements" runat="server"></asp:Literal>
                                                                                </td>
                                                                                <td align="center">
                                                                                    <asp:Literal ID="LTRL_InkMagentaDeltaEAve" runat="server"></asp:Literal>
                                                                                </td>
                                                                                <td align="center">
                                                                                    <asp:Literal ID="LTRL_InkMagentaDeltaMax" runat="server"></asp:Literal>
                                                                                </td>
                                                                                <td align="center">
                                                                                    <asp:Literal ID="LTRL_InkMagentaDeltaDensityAve" runat="server"></asp:Literal>
                                                                                </td>
                                                                                <td align="center">
                                                                                    <asp:Literal ID="LTRL_InkMagentaStdDev" runat="server"></asp:Literal>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="ALTERNATINGITEMSTYLE1">
                                                                                <!-- <td align="right">
                                                                                    &nbsp;
                                                                                </td>-->
                                                                                <td align="right">
                                                                                    <b>Ink Yellow</b>
                                                                                </td>
                                                                                <td align="center">
                                                                                    <asp:Literal ID="LTRL_InkYellowMeasurements" runat="server"></asp:Literal>
                                                                                </td>
                                                                                <td align="center">
                                                                                    <asp:Literal ID="LTRL_InkYellowDeltaEAve" runat="server"></asp:Literal>
                                                                                </td>
                                                                                <td align="center">
                                                                                    <asp:Literal ID="LTRL_InkYellowDeltaMax" runat="server"></asp:Literal>
                                                                                </td>
                                                                                <td align="center">
                                                                                    <asp:Literal ID="LTRL_InkYellowDeltaDensityAve" runat="server"></asp:Literal>
                                                                                </td>
                                                                                <td align="center">
                                                                                    <asp:Literal ID="LTRL_InkYellowStdDev" runat="server"></asp:Literal>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="ITEMSTYLE1">
                                                                             <!-- <td align="right">
                                                                                    &nbsp;
                                                                                </td>-->
                                                                                <td align="right">
                                                                                    <b>Ink Black</b>
                                                                                </td>
                                                                                <td align="center">
                                                                                    <asp:Literal ID="LTRL_InkBlackMeasurements" runat="server"></asp:Literal>
                                                                                </td>
                                                                                <td align="center">
                                                                                    <asp:Literal ID="LTRL_InkBlackDeltaEAve" runat="server"></asp:Literal>
                                                                                </td>
                                                                                <td align="center">
                                                                                    <asp:Literal ID="LTRL_InkBlackDeltaMax" runat="server"></asp:Literal>
                                                                                </td>
                                                                                <td align="center">
                                                                                    <asp:Literal ID="LTRL_InkBlackDeltaDensityAve" runat="server"></asp:Literal>
                                                                                </td>
                                                                                <td align="center">
                                                                                    <asp:Literal ID="LTRL_InkBlackStdDev" runat="server"></asp:Literal>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr id="LBL_Consumables" runat="server">
                                                                <td align="center" colspan="2" style="padding-left: 5px; padding-right: 5px;">
                                                                    <table class="DATAGRID1" cellspacing="0" cellpadding="0" border="0" width="100%">
                                                                        <tbody>
                                                                            <tr class="HEADERSTYLE1">
                                                                                <td align="left" colspan="7">
                                                                                    &nbsp;Consumables
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:DataGrid ID="DGConsumable" runat="server" Width="100%" AutoGenerateColumns="False">
                                                                                        <Columns>
                                                                                            <asp:BoundColumn DataField="Plate" HeaderText="Substrate SKU" HeaderStyle-HorizontalAlign="Center">
                                                                                                <HeaderStyle Width="25%"></HeaderStyle>
                                                                                            </asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="TotalReads" HeaderText="Total Reads" ItemStyle-HorizontalAlign="Center"
                                                                                                HeaderStyle-HorizontalAlign="Center">
                                                                                                <HeaderStyle Width="15%"></HeaderStyle>
                                                                                            </asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="AvgNegDev" DataFormatString="{0:f2}" HeaderText="Avg Neg Deviation"
                                                                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                                                                <HeaderStyle Width="15%"></HeaderStyle>
                                                                                            </asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="AvgPosDev" DataFormatString="{0:f2}" HeaderText="Avg Pos Deviation"
                                                                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                                                                <HeaderStyle Width="15%"></HeaderStyle>
                                                                                            </asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="MaxNegDev" DataFormatString="{0:f2}" HeaderText="Max Neg Deviation"
                                                                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                                                                <HeaderStyle Width="15%"></HeaderStyle>
                                                                                            </asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="MaxPosDev" DataFormatString="{0:f2}" HeaderText="Max Pos Deviation"
                                                                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                                                                <HeaderStyle Width="15%"></HeaderStyle>
                                                                                            </asp:BoundColumn>
                                                                                        </Columns>
                                                                                        <AlternatingItemStyle CssClass="ALTERNATINGSTYLE" />
                                                                                        <ItemStyle CssClass="ITEMSTYLE" />
                                                                                        <HeaderStyle CssClass="HEADERSTYLE" />
                                                                                    </asp:DataGrid>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 5px">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" colspan="2" style="padding-left: 5px; padding-right: 5px;">
                                                                    <DCWC:Chart ID="ChartLine1" runat="server" BackGradientType="DiagonalLeft" Palette="Kindergarten"
                                                                        BackGradientEndColor="#EFEFEF" BorderLineColor="#808080" ImageUrl="UploadFiles/temp/DeviceReport2"
                                                                        ImageType="Jpeg" BorderLineStyle="Solid" Width="900px">
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
                                                                            <DCWC:Title Text="Delta E" Name="Title1">
                                                                            </DCWC:Title>
                                                                        </Titles>
                                                                        <DCWC:Legend Alignment="Center" Docking="Bottom" BorderColor="Black" Name="Default">
                                                                        </DCWC:Legend>
                                                                        <ChartAreas>
                                                                            <DCWC:ChartArea BackGradientEndColor="#EFEFEF" BorderColor="#808080" BackColor="White"
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
                                                                <td style="height: 5px" colspan="2">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" colspan="2">
                                                                    <asp:Button ID="BTN_MakePDF" OnClick="BTN_MakePDF_Click" runat="server" CssClass="green2-btn"
                                                                        Text="Generate PDF"></asp:Button>
                                                                    &nbsp;<asp:Button ID="BTN_BackToSearch" OnClientClick="BackToSearch()" OnClick="BTN_BackToSearch_Click"
                                                                        runat="server" CssClass="green2-btn" Text="Back To Search"></asp:Button>&nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 5px" colspan="2">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </tbody>
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
