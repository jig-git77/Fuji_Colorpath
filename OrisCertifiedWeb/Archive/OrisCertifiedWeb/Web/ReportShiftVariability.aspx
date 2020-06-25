<%@ Page Language="C#" MasterPageFile="~/Home.master" AutoEventWireup="true" CodeFile="ReportShiftVariability.aspx.cs"  EnableEventValidation="false"
    Inherits="ReportShiftVariability" %>
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
                                                            <asp:Label ID="lblReportTitle" runat="server" Text="Shift Variability Report(Search Criteria)"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" class="green-form-bg" style="border-bottom: none" id="TDReportTitle"
                                                            runat="server">
                                                            <asp:Label ID="LblError" runat="server" CssClass="ERROR"></asp:Label><asp:Label ID="LBL_CPMeasurementGUID1"
                                                                runat="server" Visible="false"></asp:Label><asp:Label ID="LBL_CPMeasurementGUID2"
                                                                    runat="server" Visible="false"></asp:Label>
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
                                                                        <td style="width: 25%" align="center">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td style="width: 25%" align="center">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td style="width: 25%" align="center">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td style="width: 25%" align="center">
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
                                                                            <asp:DropDownList ID="DDL_Company" runat="server" CssClass="DROPDOWNLIST" OnSelectedIndexChanged="FillBranch"
                                                                                AutoPostBack="true">
                                                                                <asp:ListItem Value="0">--- Select Company ---</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:DropDownList ID="DDL_Branch" runat="server" CssClass="DROPDOWNLIST" OnSelectedIndexChanged="FillDongle"
                                                                                AutoPostBack="true">
                                                                                <asp:ListItem Value="0">--- Select Branch ---</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:DropDownList ID="DDL_DongleNo" runat="server" CssClass="DROPDOWNLIST" OnSelectedIndexChanged="FillDevice"
                                                                                AutoPostBack="true">
                                                                                <asp:ListItem Value="0">--- Select Dongle ---</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:DropDownList ID="DDL_Device" runat="server" CssClass="DROPDOWNLIST" OnSelectedIndexChanged="FillDates"
                                                                                AutoPostBack="true">
                                                                                <asp:ListItem Value="0">--- Select Device ---</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" valign="baseline">
                                                                            Date(DD/MM/YYYY)
                                                                        </td>
                                                                        <td valign="top" colspan="3" align="left">
                                                                            &nbsp;From&nbsp;<asp:TextBox ID="TXT_FromDate" runat="server" CssClass="TEXTBOX"
                                                                                AutoPostBack="True" OnTextChanged="TXT_DateText" Columns="10" MaxLength="10"
                                                                                Width="75px"></asp:TextBox>&nbsp;<asp:ImageButton ID="IBTN_FromDate" runat="server"
                                                                                    ImageAlign="AbsMiddle" ImageUrl="Images/calendar-icon.png"></asp:ImageButton><ajaxToolkit:CalendarExtender
                                                                                        ID="Cal_FromDate" runat="server" TargetControlID="TXT_FromDate" PopupButtonID="IBTN_FromDate">
                                                                                    </ajaxToolkit:CalendarExtender>
                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; To&nbsp;<asp:TextBox ID="TXT_ToDate" runat="server"
                                                                                CssClass="TEXTBOX" AutoPostBack="True" OnTextChanged="TXT_DateText" Columns="10"
                                                                                MaxLength="10" Width="75px"></asp:TextBox>&nbsp;<asp:ImageButton ID="IBTN_ToDate"
                                                                                    runat="server" ImageAlign="AbsMiddle" ImageUrl="Images/calendar-icon.png">
                                                                            </asp:ImageButton><ajaxToolkit:CalendarExtender ID="Cal_ToDate" runat="server" TargetControlID="TXT_ToDate"
                                                                                PopupButtonID="IBTN_ToDate">
                                                                            </ajaxToolkit:CalendarExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            Shift 1(HH:MM)
                                                                        </td>
                                                                        <td align="left" colspan="3">
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        <b>From</b>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="DDL_Shift1FromHours" runat="server" CssClass="DROPDOWNLIST">
                                                                                            <asp:ListItem Value="00">12</asp:ListItem>
                                                                                            <asp:ListItem Value="01">01</asp:ListItem>
                                                                                            <asp:ListItem Value="02">02</asp:ListItem>
                                                                                            <asp:ListItem Value="03">03</asp:ListItem>
                                                                                            <asp:ListItem Value="04">04</asp:ListItem>
                                                                                            <asp:ListItem Value="05">05</asp:ListItem>
                                                                                            <asp:ListItem Value="06">06</asp:ListItem>
                                                                                            <asp:ListItem Value="07">07</asp:ListItem>
                                                                                            <asp:ListItem Value="08">08</asp:ListItem>
                                                                                            <asp:ListItem Value="09">09</asp:ListItem>
                                                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                                                            <asp:ListItem Value="11">11</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td>
                                                                                        :
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="DDL_Shift1FromMins" runat="server" CssClass="DROPDOWNLIST">
                                                                                            <asp:ListItem Value="00">00</asp:ListItem>
                                                                                            <asp:ListItem Value="01">01</asp:ListItem>
                                                                                            <asp:ListItem Value="02">02</asp:ListItem>
                                                                                            <asp:ListItem Value="03">03</asp:ListItem>
                                                                                            <asp:ListItem Value="04">04</asp:ListItem>
                                                                                            <asp:ListItem Value="05">05</asp:ListItem>
                                                                                            <asp:ListItem Value="06">06</asp:ListItem>
                                                                                            <asp:ListItem Value="07">07</asp:ListItem>
                                                                                            <asp:ListItem Value="08">08</asp:ListItem>
                                                                                            <asp:ListItem Value="09">09</asp:ListItem>
                                                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                                                            <asp:ListItem Value="11">11</asp:ListItem>
                                                                                            <asp:ListItem Value="12">12</asp:ListItem>
                                                                                            <asp:ListItem Value="13">13</asp:ListItem>
                                                                                            <asp:ListItem Value="14">14</asp:ListItem>
                                                                                            <asp:ListItem Value="15">15</asp:ListItem>
                                                                                            <asp:ListItem Value="16">16</asp:ListItem>
                                                                                            <asp:ListItem Value="17">17</asp:ListItem>
                                                                                            <asp:ListItem Value="18">18</asp:ListItem>
                                                                                            <asp:ListItem Value="19">19</asp:ListItem>
                                                                                            <asp:ListItem Value="20">20</asp:ListItem>
                                                                                            <asp:ListItem Value="21">21</asp:ListItem>
                                                                                            <asp:ListItem Value="22">22</asp:ListItem>
                                                                                            <asp:ListItem Value="23">23</asp:ListItem>
                                                                                            <asp:ListItem Value="24">24</asp:ListItem>
                                                                                            <asp:ListItem Value="25">25</asp:ListItem>
                                                                                            <asp:ListItem Value="26">26</asp:ListItem>
                                                                                            <asp:ListItem Value="27">27</asp:ListItem>
                                                                                            <asp:ListItem Value="28">28</asp:ListItem>
                                                                                            <asp:ListItem Value="29">29</asp:ListItem>
                                                                                            <asp:ListItem Value="30">30</asp:ListItem>
                                                                                            <asp:ListItem Value="31">31</asp:ListItem>
                                                                                            <asp:ListItem Value="32">32</asp:ListItem>
                                                                                            <asp:ListItem Value="33">33</asp:ListItem>
                                                                                            <asp:ListItem Value="34">34</asp:ListItem>
                                                                                            <asp:ListItem Value="35">35</asp:ListItem>
                                                                                            <asp:ListItem Value="36">36</asp:ListItem>
                                                                                            <asp:ListItem Value="37">37</asp:ListItem>
                                                                                            <asp:ListItem Value="38">38</asp:ListItem>
                                                                                            <asp:ListItem Value="39">39</asp:ListItem>
                                                                                            <asp:ListItem Value="40">40</asp:ListItem>
                                                                                            <asp:ListItem Value="41">41</asp:ListItem>
                                                                                            <asp:ListItem Value="42">42</asp:ListItem>
                                                                                            <asp:ListItem Value="43">43</asp:ListItem>
                                                                                            <asp:ListItem Value="44">44</asp:ListItem>
                                                                                            <asp:ListItem Value="45">45</asp:ListItem>
                                                                                            <asp:ListItem Value="46">46</asp:ListItem>
                                                                                            <asp:ListItem Value="47">47</asp:ListItem>
                                                                                            <asp:ListItem Value="48">48</asp:ListItem>
                                                                                            <asp:ListItem Value="49">49</asp:ListItem>
                                                                                            <asp:ListItem Value="50">50</asp:ListItem>
                                                                                            <asp:ListItem Value="51">51</asp:ListItem>
                                                                                            <asp:ListItem Value="52">52</asp:ListItem>
                                                                                            <asp:ListItem Value="53">53</asp:ListItem>
                                                                                            <asp:ListItem Value="54">54</asp:ListItem>
                                                                                            <asp:ListItem Value="55">55</asp:ListItem>
                                                                                            <asp:ListItem Value="56">56</asp:ListItem>
                                                                                            <asp:ListItem Value="57">57</asp:ListItem>
                                                                                            <asp:ListItem Value="58">58</asp:ListItem>
                                                                                            <asp:ListItem Value="59">59</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td nowrap="true">
                                                                                        <asp:RadioButtonList ID="RBTN_Shift1FromAMPM" runat="server" CssClass="RADIOBUTTONLIST"
                                                                                            RepeatDirection="horizontal">
                                                                                            <asp:ListItem Value="0" Selected="true">AM</asp:ListItem>
                                                                                            <asp:ListItem Value="12">PM</asp:ListItem>
                                                                                        </asp:RadioButtonList>
                                                                                    </td>
                                                                                    <td>
                                                                                        <b>To</b>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="DDL_Shift1ToHours" runat="server" CssClass="DROPDOWNLIST">
                                                                                            <asp:ListItem Value="00">12</asp:ListItem>
                                                                                            <asp:ListItem Value="01">01</asp:ListItem>
                                                                                            <asp:ListItem Value="02">02</asp:ListItem>
                                                                                            <asp:ListItem Value="03">03</asp:ListItem>
                                                                                            <asp:ListItem Value="04">04</asp:ListItem>
                                                                                            <asp:ListItem Value="05">05</asp:ListItem>
                                                                                            <asp:ListItem Value="06">06</asp:ListItem>
                                                                                            <asp:ListItem Value="07">07</asp:ListItem>
                                                                                            <asp:ListItem Value="08">08</asp:ListItem>
                                                                                            <asp:ListItem Value="09">09</asp:ListItem>
                                                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                                                            <asp:ListItem Value="11" Selected="true">11</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td>
                                                                                        :
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="DDL_Shift1ToMins" runat="server" CssClass="DROPDOWNLIST">
                                                                                            <asp:ListItem Value="00">00</asp:ListItem>
                                                                                            <asp:ListItem Value="01">01</asp:ListItem>
                                                                                            <asp:ListItem Value="02">02</asp:ListItem>
                                                                                            <asp:ListItem Value="03">03</asp:ListItem>
                                                                                            <asp:ListItem Value="04">04</asp:ListItem>
                                                                                            <asp:ListItem Value="05">05</asp:ListItem>
                                                                                            <asp:ListItem Value="06">06</asp:ListItem>
                                                                                            <asp:ListItem Value="07">07</asp:ListItem>
                                                                                            <asp:ListItem Value="08">08</asp:ListItem>
                                                                                            <asp:ListItem Value="09">09</asp:ListItem>
                                                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                                                            <asp:ListItem Value="11">11</asp:ListItem>
                                                                                            <asp:ListItem Value="12">12</asp:ListItem>
                                                                                            <asp:ListItem Value="13">13</asp:ListItem>
                                                                                            <asp:ListItem Value="14">14</asp:ListItem>
                                                                                            <asp:ListItem Value="15">15</asp:ListItem>
                                                                                            <asp:ListItem Value="16">16</asp:ListItem>
                                                                                            <asp:ListItem Value="17">17</asp:ListItem>
                                                                                            <asp:ListItem Value="18">18</asp:ListItem>
                                                                                            <asp:ListItem Value="19">19</asp:ListItem>
                                                                                            <asp:ListItem Value="20">20</asp:ListItem>
                                                                                            <asp:ListItem Value="21">21</asp:ListItem>
                                                                                            <asp:ListItem Value="22">22</asp:ListItem>
                                                                                            <asp:ListItem Value="23">23</asp:ListItem>
                                                                                            <asp:ListItem Value="24">24</asp:ListItem>
                                                                                            <asp:ListItem Value="25">25</asp:ListItem>
                                                                                            <asp:ListItem Value="26">26</asp:ListItem>
                                                                                            <asp:ListItem Value="27">27</asp:ListItem>
                                                                                            <asp:ListItem Value="28">28</asp:ListItem>
                                                                                            <asp:ListItem Value="29">29</asp:ListItem>
                                                                                            <asp:ListItem Value="30">30</asp:ListItem>
                                                                                            <asp:ListItem Value="31">31</asp:ListItem>
                                                                                            <asp:ListItem Value="32">32</asp:ListItem>
                                                                                            <asp:ListItem Value="33">33</asp:ListItem>
                                                                                            <asp:ListItem Value="34">34</asp:ListItem>
                                                                                            <asp:ListItem Value="35">35</asp:ListItem>
                                                                                            <asp:ListItem Value="36">36</asp:ListItem>
                                                                                            <asp:ListItem Value="37">37</asp:ListItem>
                                                                                            <asp:ListItem Value="38">38</asp:ListItem>
                                                                                            <asp:ListItem Value="39">39</asp:ListItem>
                                                                                            <asp:ListItem Value="40">40</asp:ListItem>
                                                                                            <asp:ListItem Value="41">41</asp:ListItem>
                                                                                            <asp:ListItem Value="42">42</asp:ListItem>
                                                                                            <asp:ListItem Value="43">43</asp:ListItem>
                                                                                            <asp:ListItem Value="44">44</asp:ListItem>
                                                                                            <asp:ListItem Value="45">45</asp:ListItem>
                                                                                            <asp:ListItem Value="46">46</asp:ListItem>
                                                                                            <asp:ListItem Value="47">47</asp:ListItem>
                                                                                            <asp:ListItem Value="48">48</asp:ListItem>
                                                                                            <asp:ListItem Value="49">49</asp:ListItem>
                                                                                            <asp:ListItem Value="50">50</asp:ListItem>
                                                                                            <asp:ListItem Value="51">51</asp:ListItem>
                                                                                            <asp:ListItem Value="52">52</asp:ListItem>
                                                                                            <asp:ListItem Value="53">53</asp:ListItem>
                                                                                            <asp:ListItem Value="54">54</asp:ListItem>
                                                                                            <asp:ListItem Value="55">55</asp:ListItem>
                                                                                            <asp:ListItem Value="56">56</asp:ListItem>
                                                                                            <asp:ListItem Value="57">57</asp:ListItem>
                                                                                            <asp:ListItem Value="58">58</asp:ListItem>
                                                                                            <asp:ListItem Value="59" Selected="true">59</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td nowrap="true">
                                                                                        <asp:RadioButtonList ID="RBTN_Shift1ToAMPM" runat="server" CssClass="RADIOBUTTONLIST"
                                                                                            RepeatDirection="horizontal">
                                                                                            <asp:ListItem Value="0" Selected="true">AM</asp:ListItem>
                                                                                            <asp:ListItem Value="12">PM</asp:ListItem>
                                                                                        </asp:RadioButtonList>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            Shift 2(HH:MM)
                                                                        </td>
                                                                        <td align="left" colspan="3">
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        <b>From</b>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="DDL_Shift2FromHours" runat="server" CssClass="DROPDOWNLIST">
                                                                                            <asp:ListItem Value="00">12</asp:ListItem>
                                                                                            <asp:ListItem Value="01">01</asp:ListItem>
                                                                                            <asp:ListItem Value="02">02</asp:ListItem>
                                                                                            <asp:ListItem Value="03">03</asp:ListItem>
                                                                                            <asp:ListItem Value="04">04</asp:ListItem>
                                                                                            <asp:ListItem Value="05">05</asp:ListItem>
                                                                                            <asp:ListItem Value="06">06</asp:ListItem>
                                                                                            <asp:ListItem Value="07">07</asp:ListItem>
                                                                                            <asp:ListItem Value="08">08</asp:ListItem>
                                                                                            <asp:ListItem Value="09">09</asp:ListItem>
                                                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                                                            <asp:ListItem Value="11">11</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td>
                                                                                        :
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="DDL_Shift2FromMins" runat="server" CssClass="DROPDOWNLIST">
                                                                                            <asp:ListItem Value="00">00</asp:ListItem>
                                                                                            <asp:ListItem Value="01">01</asp:ListItem>
                                                                                            <asp:ListItem Value="02">02</asp:ListItem>
                                                                                            <asp:ListItem Value="03">03</asp:ListItem>
                                                                                            <asp:ListItem Value="04">04</asp:ListItem>
                                                                                            <asp:ListItem Value="05">05</asp:ListItem>
                                                                                            <asp:ListItem Value="06">06</asp:ListItem>
                                                                                            <asp:ListItem Value="07">07</asp:ListItem>
                                                                                            <asp:ListItem Value="08">08</asp:ListItem>
                                                                                            <asp:ListItem Value="09">09</asp:ListItem>
                                                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                                                            <asp:ListItem Value="11">11</asp:ListItem>
                                                                                            <asp:ListItem Value="12">12</asp:ListItem>
                                                                                            <asp:ListItem Value="13">13</asp:ListItem>
                                                                                            <asp:ListItem Value="14">14</asp:ListItem>
                                                                                            <asp:ListItem Value="15">15</asp:ListItem>
                                                                                            <asp:ListItem Value="16">16</asp:ListItem>
                                                                                            <asp:ListItem Value="17">17</asp:ListItem>
                                                                                            <asp:ListItem Value="18">18</asp:ListItem>
                                                                                            <asp:ListItem Value="19">19</asp:ListItem>
                                                                                            <asp:ListItem Value="20">20</asp:ListItem>
                                                                                            <asp:ListItem Value="21">21</asp:ListItem>
                                                                                            <asp:ListItem Value="22">22</asp:ListItem>
                                                                                            <asp:ListItem Value="23">23</asp:ListItem>
                                                                                            <asp:ListItem Value="24">24</asp:ListItem>
                                                                                            <asp:ListItem Value="25">25</asp:ListItem>
                                                                                            <asp:ListItem Value="26">26</asp:ListItem>
                                                                                            <asp:ListItem Value="27">27</asp:ListItem>
                                                                                            <asp:ListItem Value="28">28</asp:ListItem>
                                                                                            <asp:ListItem Value="29">29</asp:ListItem>
                                                                                            <asp:ListItem Value="30">30</asp:ListItem>
                                                                                            <asp:ListItem Value="31">31</asp:ListItem>
                                                                                            <asp:ListItem Value="32">32</asp:ListItem>
                                                                                            <asp:ListItem Value="33">33</asp:ListItem>
                                                                                            <asp:ListItem Value="34">34</asp:ListItem>
                                                                                            <asp:ListItem Value="35">35</asp:ListItem>
                                                                                            <asp:ListItem Value="36">36</asp:ListItem>
                                                                                            <asp:ListItem Value="37">37</asp:ListItem>
                                                                                            <asp:ListItem Value="38">38</asp:ListItem>
                                                                                            <asp:ListItem Value="39">39</asp:ListItem>
                                                                                            <asp:ListItem Value="40">40</asp:ListItem>
                                                                                            <asp:ListItem Value="41">41</asp:ListItem>
                                                                                            <asp:ListItem Value="42">42</asp:ListItem>
                                                                                            <asp:ListItem Value="43">43</asp:ListItem>
                                                                                            <asp:ListItem Value="44">44</asp:ListItem>
                                                                                            <asp:ListItem Value="45">45</asp:ListItem>
                                                                                            <asp:ListItem Value="46">46</asp:ListItem>
                                                                                            <asp:ListItem Value="47">47</asp:ListItem>
                                                                                            <asp:ListItem Value="48">48</asp:ListItem>
                                                                                            <asp:ListItem Value="49">49</asp:ListItem>
                                                                                            <asp:ListItem Value="50">50</asp:ListItem>
                                                                                            <asp:ListItem Value="51">51</asp:ListItem>
                                                                                            <asp:ListItem Value="52">52</asp:ListItem>
                                                                                            <asp:ListItem Value="53">53</asp:ListItem>
                                                                                            <asp:ListItem Value="54">54</asp:ListItem>
                                                                                            <asp:ListItem Value="55">55</asp:ListItem>
                                                                                            <asp:ListItem Value="56">56</asp:ListItem>
                                                                                            <asp:ListItem Value="57">57</asp:ListItem>
                                                                                            <asp:ListItem Value="58">58</asp:ListItem>
                                                                                            <asp:ListItem Value="59">59</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td nowrap="true">
                                                                                        <asp:RadioButtonList ID="RBTN_Shift2FromAMPM" runat="server" CssClass="RADIOBUTTONLIST"
                                                                                            RepeatDirection="horizontal">
                                                                                            <asp:ListItem Value="0">AM</asp:ListItem>
                                                                                            <asp:ListItem Value="12" Selected="true">PM</asp:ListItem>
                                                                                        </asp:RadioButtonList>
                                                                                    </td>
                                                                                    <td>
                                                                                        <b>To</b>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="DDL_Shift2ToHours" runat="server" CssClass="DROPDOWNLIST">
                                                                                            <asp:ListItem Value="00">12</asp:ListItem>
                                                                                            <asp:ListItem Value="01">01</asp:ListItem>
                                                                                            <asp:ListItem Value="02">02</asp:ListItem>
                                                                                            <asp:ListItem Value="03">03</asp:ListItem>
                                                                                            <asp:ListItem Value="04">04</asp:ListItem>
                                                                                            <asp:ListItem Value="05">05</asp:ListItem>
                                                                                            <asp:ListItem Value="06">06</asp:ListItem>
                                                                                            <asp:ListItem Value="07">07</asp:ListItem>
                                                                                            <asp:ListItem Value="08">08</asp:ListItem>
                                                                                            <asp:ListItem Value="09">09</asp:ListItem>
                                                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                                                            <asp:ListItem Value="11" Selected="true">11</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td>
                                                                                        :
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="DDL_Shift2ToMins" runat="server" CssClass="DROPDOWNLIST">
                                                                                            <asp:ListItem Value="00">00</asp:ListItem>
                                                                                            <asp:ListItem Value="01">01</asp:ListItem>
                                                                                            <asp:ListItem Value="02">02</asp:ListItem>
                                                                                            <asp:ListItem Value="03">03</asp:ListItem>
                                                                                            <asp:ListItem Value="04">04</asp:ListItem>
                                                                                            <asp:ListItem Value="05">05</asp:ListItem>
                                                                                            <asp:ListItem Value="06">06</asp:ListItem>
                                                                                            <asp:ListItem Value="07">07</asp:ListItem>
                                                                                            <asp:ListItem Value="08">08</asp:ListItem>
                                                                                            <asp:ListItem Value="09">09</asp:ListItem>
                                                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                                                            <asp:ListItem Value="11">11</asp:ListItem>
                                                                                            <asp:ListItem Value="12">12</asp:ListItem>
                                                                                            <asp:ListItem Value="13">13</asp:ListItem>
                                                                                            <asp:ListItem Value="14">14</asp:ListItem>
                                                                                            <asp:ListItem Value="15">15</asp:ListItem>
                                                                                            <asp:ListItem Value="16">16</asp:ListItem>
                                                                                            <asp:ListItem Value="17">17</asp:ListItem>
                                                                                            <asp:ListItem Value="18">18</asp:ListItem>
                                                                                            <asp:ListItem Value="19">19</asp:ListItem>
                                                                                            <asp:ListItem Value="20">20</asp:ListItem>
                                                                                            <asp:ListItem Value="21">21</asp:ListItem>
                                                                                            <asp:ListItem Value="22">22</asp:ListItem>
                                                                                            <asp:ListItem Value="23">23</asp:ListItem>
                                                                                            <asp:ListItem Value="24">24</asp:ListItem>
                                                                                            <asp:ListItem Value="25">25</asp:ListItem>
                                                                                            <asp:ListItem Value="26">26</asp:ListItem>
                                                                                            <asp:ListItem Value="27">27</asp:ListItem>
                                                                                            <asp:ListItem Value="28">28</asp:ListItem>
                                                                                            <asp:ListItem Value="29">29</asp:ListItem>
                                                                                            <asp:ListItem Value="30">30</asp:ListItem>
                                                                                            <asp:ListItem Value="31">31</asp:ListItem>
                                                                                            <asp:ListItem Value="32">32</asp:ListItem>
                                                                                            <asp:ListItem Value="33">33</asp:ListItem>
                                                                                            <asp:ListItem Value="34">34</asp:ListItem>
                                                                                            <asp:ListItem Value="35">35</asp:ListItem>
                                                                                            <asp:ListItem Value="36">36</asp:ListItem>
                                                                                            <asp:ListItem Value="37">37</asp:ListItem>
                                                                                            <asp:ListItem Value="38">38</asp:ListItem>
                                                                                            <asp:ListItem Value="39">39</asp:ListItem>
                                                                                            <asp:ListItem Value="40">40</asp:ListItem>
                                                                                            <asp:ListItem Value="41">41</asp:ListItem>
                                                                                            <asp:ListItem Value="42">42</asp:ListItem>
                                                                                            <asp:ListItem Value="43">43</asp:ListItem>
                                                                                            <asp:ListItem Value="44">44</asp:ListItem>
                                                                                            <asp:ListItem Value="45">45</asp:ListItem>
                                                                                            <asp:ListItem Value="46">46</asp:ListItem>
                                                                                            <asp:ListItem Value="47">47</asp:ListItem>
                                                                                            <asp:ListItem Value="48">48</asp:ListItem>
                                                                                            <asp:ListItem Value="49">49</asp:ListItem>
                                                                                            <asp:ListItem Value="50">50</asp:ListItem>
                                                                                            <asp:ListItem Value="51">51</asp:ListItem>
                                                                                            <asp:ListItem Value="52">52</asp:ListItem>
                                                                                            <asp:ListItem Value="53">53</asp:ListItem>
                                                                                            <asp:ListItem Value="54">54</asp:ListItem>
                                                                                            <asp:ListItem Value="55">55</asp:ListItem>
                                                                                            <asp:ListItem Value="56">56</asp:ListItem>
                                                                                            <asp:ListItem Value="57">57</asp:ListItem>
                                                                                            <asp:ListItem Value="58">58</asp:ListItem>
                                                                                            <asp:ListItem Value="59" Selected="true">59</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td nowrap="true">
                                                                                        <asp:RadioButtonList ID="RBTN_Shift2ToAMPM" runat="server" CssClass="RADIOBUTTONLIST"
                                                                                            RepeatDirection="horizontal">
                                                                                            <asp:ListItem Value="0">AM</asp:ListItem>
                                                                                            <asp:ListItem Value="12" Selected="true">PM</asp:ListItem>
                                                                                        </asp:RadioButtonList>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
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
                                                                            <asp:Button ID="BTN_CreateReport" OnClick="BTN_CreateReport_Click" runat="server"
                                                                                CssClass="green2-btn" Text="Create Report"></asp:Button>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="4" height="10px">
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
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel Style="width: 100%" ID="PNL_Result" runat="server">
                                                    <table class="TABLE2" cellspacing="0" cellpadding="0" align="center" border="0">
                                                        <tr>
                                                            <td>
                                                                <table cellspacing="5" cellpadding="0" align="center" border="0">
                                                                    <tr>
                                                                        <td style="width: 50%" valign="top">
                                                                            <table class="DATAGRID1" cellspacing="1" cellpadding="0" border="0" width="100%">
                                                                                <tr class="HEADERSTYLE1">
                                                                                    <td colspan="2">
                                                                                        &nbsp;Report Parameters
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="ITEMSTYLE1">
                                                                                    <td style="width: 35%">
                                                                                        <b>&nbsp;Date Range</b>
                                                                                    </td>
                                                                                    <td style="width: 65%">
                                                                                        <asp:Label ID="LBL_DateRange" runat="server"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="ALTERNATINGITEMSTYLE1">
                                                                                    <td>
                                                                                        <b>&nbsp;Device</b>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="LBL_DeviceName" runat="server"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="ITEMSTYLE1">
                                                                                    <td>
                                                                                        <b>&nbsp;Dongle Number</b>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="LBL_DongleNumber" runat="server"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="ALTERNATINGITEMSTYLE1">
                                                                                    <td>
                                                                                        <b>&nbsp;Location Name</b>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="LBL_BranchName" runat="server"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="ITEMSTYLE1">
                                                                                    <td>
                                                                                        <b>&nbsp;Company</b>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="LBL_CompanyName" runat="server"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td style="width: 50%" valign="top">
                                                                            <table class="DATAGRID1" cellspacing="1" cellpadding="0" border="0" width="100%">
                                                                                <tr class="HEADERSTYLE1">
                                                                                    <td colspan="2">
                                                                                        Time Sheet
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="ITEMSTYLE1">
                                                                                    <td style="width: 35%" align="right">
                                                                                        <b>Shift 1, Start</b>
                                                                                    </td>
                                                                                    <td style="width: 65%">
                                                                                        <asp:Label ID="LBL_Shift1Start" runat="server"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="ALTERNATINGITEMSTYLE1">
                                                                                    <td align="right">
                                                                                        <b>End</b>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="LBL_Shift1End" runat="server"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="ITEMSTYLE1">
                                                                                    <td align="right">
                                                                                        <b>Shift 2, Start</b>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="LBL_Shift2Start" runat="server"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="ALTERNATINGITEMSTYLE1">
                                                                                    <td align="right">
                                                                                        <b>End</b>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="LBL_Shift2End" runat="server"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="top" align="center" colspan="2">
                                                                            <table class="DATAGRID1" cellspacing="1" cellpadding="0" border="0" width="100%">
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
                                                                                    <td>
                                                                                        <asp:Label ID="LBL_Field1" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="LBL_Operator1" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="LBL_Criteria1" runat="server"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="ALTERNATINGITEMSTYLE1">
                                                                                    <td>
                                                                                        <asp:Label ID="LBL_Field2" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="LBL_Operator2" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="LBL_Criteria2" runat="server"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="ITEMSTYLE1">
                                                                                    <td>
                                                                                        <asp:Label ID="LBL_Field3" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="LBL_Operator3" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="LBL_Criteria3" runat="server"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <table class="DATAGRID1" cellspacing="1" cellpadding="0" align="center" border="0"
                                                                                width="100%">
                                                                                <tr>
                                                                                    <td class="HEADERSTYLE1" colspan="3">
                                                                                        Shift 1
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="ALTERNATINGITEMSTYLE1">
                                                                                    <td>
                                                                                        <span>&nbsp;Error Rate</span>&nbsp;:&nbsp;<asp:Label ID="LBL_Shift1ErrorRatio" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <span>Number Of Reads</span>&nbsp;:&nbsp;<asp:Label ID="LBL_Shift1NoOfReads" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <span>Total Errors</span>&nbsp;:&nbsp;<asp:Label ID="LBL_Shift1TotalErrors" runat="server"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" colspan="2">
                                                                            <DCWC:Chart ID="CHRT_MaxDeltaE1" runat="server" CallbackStateContent="All" BackGradientType="DiagonalLeft"
                                                                                Palette="Kindergarten" BackGradientEndColor="#EFEFEF" BorderLineColor="#808080"
                                                                                BorderLineStyle="Solid" Height="225px" Width="900px" ImageType="Jpeg" ImageUrl="UploadFiles/temp/MaxDeltaE_Shift1">
                                                                                <Titles>
                                                                                    <DCWC:Title Name="Title1" Text="Maximum Delta E Ave">
                                                                                    </DCWC:Title>
                                                                                </Titles>
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
                                                                                <Series>
                                                                                    <DCWC:Series BorderColor="100, 0, 0, 0" Name="Default" YValuesPerPoint="4" BackGradientEndColor="162, 164, 255"
                                                                                        CustomAttributes="PieDrawingStyle=Concave">
                                                                                    </DCWC:Series>
                                                                                </Series>
                                                                                <BorderSkin FrameBackColor="White" FrameBackGradientEndColor="White" FrameBorderColor="White"
                                                                                    FrameBorderWidth="2" SkinStyle="Raised" />
                                                                                <UI>
                                                                                    <Toolbar BorderColor="LightSlateGray" BackColor="White">
                                                                                        <BorderSkin SkinStyle="Raised" FrameBackColor="SteelBlue" FrameBackGradientEndColor="LightBlue"
                                                                                            PageColor="Transparent" />
                                                                                    </Toolbar>
                                                                                </UI>
                                                                                <Legends>
                                                                                    <DCWC:Legend Name="Default">
                                                                                    </DCWC:Legend>
                                                                                </Legends>
                                                                                <DCWC:Legend Alignment="Center" BorderColor="Black" Docking="Bottom" Name="Default">
                                                                                </DCWC:Legend>
                                                                            </DCWC:Chart>
                                                                            <DCWC:Chart ID="CHRT_Density1" runat="server" CallbackStateContent="All" BackGradientType="DiagonalLeft"
                                                                                Palette="Kindergarten" BackGradientEndColor="#EFEFEF" BorderLineColor="#808080"
                                                                                BorderLineStyle="Solid" Height="225px" Width="900px" ImageType="Jpeg" ImageUrl="UploadFiles/temp/Density_Shift1">
                                                                                <Titles>
                                                                                    <DCWC:Title Name="Title1" Text="Density">
                                                                                    </DCWC:Title>
                                                                                </Titles>
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
                                                                                <Series>
                                                                                    <DCWC:Series BorderColor="100, 0, 0, 0" Name="Default" YValuesPerPoint="4" BackGradientEndColor="162, 164, 255"
                                                                                        CustomAttributes="PieDrawingStyle=Concave">
                                                                                    </DCWC:Series>
                                                                                </Series>
                                                                                <BorderSkin FrameBackColor="White" FrameBackGradientEndColor="White" FrameBorderColor="White"
                                                                                    FrameBorderWidth="2" SkinStyle="Raised" />
                                                                                <UI>
                                                                                    <Toolbar BorderColor="LightSlateGray" BackColor="White">
                                                                                        <BorderSkin SkinStyle="Raised" FrameBackColor="SteelBlue" FrameBackGradientEndColor="LightBlue"
                                                                                            PageColor="Transparent" />
                                                                                    </Toolbar>
                                                                                </UI>
                                                                                <Legends>
                                                                                    <DCWC:Legend Name="Default">
                                                                                    </DCWC:Legend>
                                                                                </Legends>
                                                                                <DCWC:Legend Alignment="Center" BorderColor="Black" Docking="Bottom" Name="Default">
                                                                                </DCWC:Legend>
                                                                            </DCWC:Chart>
                                                                            <DCWC:Chart ID="CHRT_DotGain1" runat="server" CallbackStateContent="All" BackGradientType="DiagonalLeft"
                                                                                Palette="Kindergarten" BackGradientEndColor="#EFEFEF" BorderLineColor="#808080"
                                                                                BorderLineStyle="Solid" Height="225px" Width="900px" ImageType="Jpeg" ImageUrl="UploadFiles/temp/DotGain_Shift1">
                                                                                <Titles>
                                                                                    <DCWC:Title Name="Title1" Text="Dotgain">
                                                                                    </DCWC:Title>
                                                                                </Titles>
                                                                                <ChartAreas>
                                                                                    <DCWC:ChartArea Name="ChartArea1" BackColor="White" BackGradientEndColor="White" BorderColor="LightSlateGray"
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
                                                                                <Series>
                                                                                                                                                    <DCWC:Series Name="Series0" Color="Transparent" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                
                                <DCWC:Series Name="Cyan0" Color="150, 000, 255, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Magenta0" Color="150, 255, 000, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Yellow0" Color="150, 255, 225, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Black0" Color="150, 000, 000, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                
                                
                                <DCWC:Series Name="Cyan1" Color="150, 000, 255, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Magenta1" Color="150, 255, 000, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Yellow1" Color="150, 255, 225, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Black1" Color="150, 000, 000, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                
                                <DCWC:Series Name="Cyan2" Color="150, 000, 255, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Magenta2" Color="150, 255, 000, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Yellow2" Color="150, 255, 225, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Black2" Color="150, 000, 000, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                
                                <DCWC:Series Name="Cyan3" Color="150, 000, 255, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Magenta3" Color="150, 255, 000, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Yellow3" Color="150, 255, 225, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Black3" Color="150, 000, 000, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>


                                <DCWC:Series Name="Cyan4" Color="150, 000, 255, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Magenta4" Color="150, 255, 000, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Yellow4" Color="150, 255, 225, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Black4" Color="150, 000, 000, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>

                                <DCWC:Series Name="Cyan5" Color="150, 000, 255, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Magenta5" Color="150, 255, 000, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Yellow5" Color="150, 255, 225, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Black5" Color="150, 000, 000, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>

                                <DCWC:Series Name="Cyan6" Color="150, 000, 255, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Magenta6" Color="150, 255, 000, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Yellow6" Color="150, 255, 225, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Black6" Color="150, 000, 000, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>

                                <DCWC:Series Name="Cyan7" Color="150, 000, 255, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Magenta7" Color="150, 255, 000, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Yellow7" Color="150, 255, 225, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Black7" Color="150, 000, 000, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>

                                                                                </Series>
                                                                                <BorderSkin FrameBackColor="White" FrameBackGradientEndColor="White" FrameBorderColor="White"
                                                                                    FrameBorderWidth="2" SkinStyle="Raised" />
                                                                                <UI>
                                                                                    <Toolbar BorderColor="LightSlateGray" BackColor="White">
                                                                                        <BorderSkin SkinStyle="Raised" FrameBackColor="SteelBlue" FrameBackGradientEndColor="LightBlue"
                                                                                            PageColor="Transparent" />
                                                                                    </Toolbar>
                                                                                </UI>
                                                                                <Legends>
                                                                                    <DCWC:Legend Name="Default">
                                                                                    </DCWC:Legend>
                                                                                </Legends>
                                                                                <DCWC:Legend Alignment="Center" BorderColor="Black" Docking="Bottom" Name="Default">
                                                                                </DCWC:Legend>
                                                                            </DCWC:Chart>
                                                                            <DCWC:Chart ID="CHRT_PaperWhite1" runat="server" CallbackStateContent="All" BackGradientType="DiagonalLeft"
                                                                                Palette="Kindergarten" BackGradientEndColor="#EFEFEF" BorderLineColor="#808080"
                                                                                BorderLineStyle="Solid" Height="225px" Width="900px" ImageType="Jpeg" ImageUrl="UploadFiles/temp/PaperWhite_Shift1">
                                                                                <Titles>
                                                                                    <DCWC:Title Name="Title1" Text="Paper White">
                                                                                    </DCWC:Title>
                                                                                </Titles>
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
                                                                                <Series>
                                                                                    <DCWC:Series BorderColor="100, 0, 0, 0" Name="Default" YValuesPerPoint="4" BackGradientEndColor="162, 164, 255"
                                                                                        CustomAttributes="PieDrawingStyle=Concave">
                                                                                    </DCWC:Series>
                                                                                </Series>
                                                                                <BorderSkin FrameBackColor="White" FrameBackGradientEndColor="White" FrameBorderColor="White"
                                                                                    FrameBorderWidth="2" SkinStyle="Raised" />
                                                                                <UI>
                                                                                    <Toolbar BorderColor="LightSlateGray" BackColor="White">
                                                                                        <BorderSkin SkinStyle="Raised" FrameBackColor="SteelBlue" FrameBackGradientEndColor="LightBlue"
                                                                                            PageColor="Transparent" />
                                                                                    </Toolbar>
                                                                                </UI>
                                                                                <Legends>
                                                                                    <DCWC:Legend Name="Default">
                                                                                    </DCWC:Legend>
                                                                                </Legends>
                                                                                <DCWC:Legend Alignment="Center" BorderColor="Black" Docking="Bottom" Name="Default">
                                                                                </DCWC:Legend>
                                                                            </DCWC:Chart>
                                                                            <DCWC:Chart ID="CHRT_TempHumidity1" runat="server" CallbackStateContent="All" BackGradientType="DiagonalLeft"
                                                                                Palette="Kindergarten" BackGradientEndColor="#EFEFEF" BorderLineColor="#808080"
                                                                                BorderLineStyle="Solid" Height="225px" Width="900px" ImageType="Jpeg" ImageUrl="UploadFiles/temp/TempHumidity_Shift1">
                                                                                <Titles>
                                                                                    <DCWC:Title Name="Title1" Text="Temperature Rel. Humidity">
                                                                                    </DCWC:Title>
                                                                                </Titles>
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
                                                                                <Series>
                                                                                    <DCWC:Series BorderColor="100, 0, 0, 0" Name="Default" YValuesPerPoint="4" BackGradientEndColor="162, 164, 255"
                                                                                        CustomAttributes="PieDrawingStyle=Concave">
                                                                                    </DCWC:Series>
                                                                                </Series>
                                                                                <BorderSkin FrameBackColor="White" FrameBackGradientEndColor="White" FrameBorderColor="White"
                                                                                    FrameBorderWidth="2" SkinStyle="Raised" />
                                                                                <UI>
                                                                                    <Toolbar BorderColor="LightSlateGray" BackColor="White">
                                                                                        <BorderSkin SkinStyle="Raised" FrameBackColor="SteelBlue" FrameBackGradientEndColor="LightBlue"
                                                                                            PageColor="Transparent" />
                                                                                    </Toolbar>
                                                                                </UI>
                                                                                <Legends>
                                                                                    <DCWC:Legend Name="Default">
                                                                                    </DCWC:Legend>
                                                                                </Legends>
                                                                                <DCWC:Legend Alignment="Center" BorderColor="Black" Docking="Bottom" Name="Default">
                                                                                </DCWC:Legend>
                                                                            </DCWC:Chart>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <table class="DATAGRID1" cellspacing="1" cellpadding="0" align="center" border="0"
                                                                                width="100%">
                                                                                <tr>
                                                                                    <td class="HEADERSTYLE1" colspan="3">
                                                                                        Shift 2
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="ALTERNATINGITEMSTYLE1">
                                                                                    <td>
                                                                                        <span>&nbsp;Error Rate</span>&nbsp;:&nbsp;<asp:Label ID="LBL_Shift2ErrorRatio" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <span>Number Of Reads</span>&nbsp;:&nbsp;<asp:Label ID="LBL_Shift2NoOfReads" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <span>Total Errors</span>&nbsp;:&nbsp;<asp:Label ID="LBL_Shift2TotalErrors" runat="server"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" colspan="2">
                                                                            <DCWC:Chart ID="CHRT_MaxDeltaE2" runat="server" CallbackStateContent="All" BackGradientType="DiagonalLeft"
                                                                                Palette="Kindergarten" BackGradientEndColor="#EFEFEF" BorderLineColor="#808080"
                                                                                BorderLineStyle="Solid" Height="225px" Width="900px" ImageType="Jpeg" ImageUrl="UploadFiles/temp/MaxDeltaE_Shift2">
                                                                                <Titles>
                                                                                    <DCWC:Title Name="Title1" Text="Maximum Delta E Ave">
                                                                                    </DCWC:Title>
                                                                                </Titles>
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
                                                                                <Series>
                                                                                    <DCWC:Series BorderColor="100, 0, 0, 0" Name="Default" YValuesPerPoint="4" BackGradientEndColor="162, 164, 255"
                                                                                        CustomAttributes="PieDrawingStyle=Concave">
                                                                                    </DCWC:Series>
                                                                                </Series>
                                                                                <BorderSkin FrameBackColor="White" FrameBackGradientEndColor="White" FrameBorderColor="White"
                                                                                    FrameBorderWidth="2" SkinStyle="Raised" />
                                                                                <UI>
                                                                                    <Toolbar BorderColor="LightSlateGray" BackColor="White">
                                                                                        <BorderSkin SkinStyle="Raised" FrameBackColor="SteelBlue" FrameBackGradientEndColor="LightBlue"
                                                                                            PageColor="Transparent" />
                                                                                    </Toolbar>
                                                                                </UI>
                                                                                <Legends>
                                                                                    <DCWC:Legend Name="Default">
                                                                                    </DCWC:Legend>
                                                                                </Legends>
                                                                                <DCWC:Legend Alignment="Center" BorderColor="Black" Docking="Bottom" Name="Default">
                                                                                </DCWC:Legend>
                                                                            </DCWC:Chart>
                                                                            <DCWC:Chart ID="CHRT_Density2" runat="server" CallbackStateContent="All" BackGradientType="DiagonalLeft"
                                                                                Palette="Kindergarten" BackGradientEndColor="#EFEFEF" BorderLineColor="#808080"
                                                                                BorderLineStyle="Solid" Height="225px" Width="900px" ImageType="Jpeg" ImageUrl="UploadFiles/temp/Density_Shift2">
                                                                                <Titles>
                                                                                    <DCWC:Title Name="Title1" Text="Density">
                                                                                    </DCWC:Title>
                                                                                </Titles>
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
                                                                                <Series>
                                                                                    <DCWC:Series BorderColor="100, 0, 0, 0" Name="Default" YValuesPerPoint="4" BackGradientEndColor="162, 164, 255"
                                                                                        CustomAttributes="PieDrawingStyle=Concave">
                                                                                    </DCWC:Series>
                                                                                </Series>
                                                                                <BorderSkin FrameBackColor="White" FrameBackGradientEndColor="White" FrameBorderColor="White"
                                                                                    FrameBorderWidth="2" SkinStyle="Raised" />
                                                                                <UI>
                                                                                    <Toolbar BorderColor="LightSlateGray" BackColor="White">
                                                                                        <BorderSkin SkinStyle="Raised" FrameBackColor="SteelBlue" FrameBackGradientEndColor="LightBlue"
                                                                                            PageColor="Transparent" />
                                                                                    </Toolbar>
                                                                                </UI>
                                                                                <Legends>
                                                                                    <DCWC:Legend Name="Default">
                                                                                    </DCWC:Legend>
                                                                                </Legends>
                                                                                <DCWC:Legend Alignment="Center" BorderColor="Black" Docking="Bottom" Name="Default">
                                                                                </DCWC:Legend>
                                                                            </DCWC:Chart>
                                                                            <DCWC:Chart ID="CHRT_DotGain2" runat="server" CallbackStateContent="All" BackGradientType="DiagonalLeft"
                                                                                Palette="Kindergarten" BackGradientEndColor="#EFEFEF" BorderLineColor="#808080"
                                                                                BorderLineStyle="Solid" Height="225px" Width="900px" ImageType="Jpeg" ImageUrl="UploadFiles/temp/DotGain_Shift2">
                                                                                <Titles>
                                                                                    <DCWC:Title Name="Title1" Text="Dotgain">
                                                                                    </DCWC:Title>
                                                                                </Titles>
                                                                                <ChartAreas>
                                                                                    <DCWC:ChartArea Name="ChartArea1" BackColor="White" BackGradientEndColor="White" BorderColor="LightSlateGray"
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
                                                                                <Series>
                                                                                                                                                    <DCWC:Series Name="Series0" Color="Transparent" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                
                                <DCWC:Series Name="Cyan0" Color="150, 000, 255, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Magenta0" Color="150, 255, 000, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Yellow0" Color="150, 255, 225, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Black0" Color="150, 000, 000, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                
                                
                                <DCWC:Series Name="Cyan1" Color="150, 000, 255, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Magenta1" Color="150, 255, 000, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Yellow1" Color="150, 255, 225, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Black1" Color="150, 000, 000, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                
                                <DCWC:Series Name="Cyan2" Color="150, 000, 255, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Magenta2" Color="150, 255, 000, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Yellow2" Color="150, 255, 225, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Black2" Color="150, 000, 000, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                
                                <DCWC:Series Name="Cyan3" Color="150, 000, 255, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Magenta3" Color="150, 255, 000, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Yellow3" Color="150, 255, 225, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Black3" Color="150, 000, 000, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>


                                <DCWC:Series Name="Cyan4" Color="150, 000, 255, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Magenta4" Color="150, 255, 000, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Yellow4" Color="150, 255, 225, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Black4" Color="150, 000, 000, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>

                                <DCWC:Series Name="Cyan5" Color="150, 000, 255, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Magenta5" Color="150, 255, 000, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Yellow5" Color="150, 255, 225, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Black5" Color="150, 000, 000, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>

                                <DCWC:Series Name="Cyan6" Color="150, 000, 255, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Magenta6" Color="150, 255, 000, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Yellow6" Color="150, 255, 225, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Black6" Color="150, 000, 000, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>

                                <DCWC:Series Name="Cyan7" Color="150, 000, 255, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Magenta7" Color="150, 255, 000, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Yellow7" Color="150, 255, 225, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Black7" Color="150, 000, 000, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>

                                                                                </Series>
                                                                                <BorderSkin FrameBackColor="White" FrameBackGradientEndColor="White" FrameBorderColor="White"
                                                                                    FrameBorderWidth="2" SkinStyle="Raised" />
                                                                                <UI>
                                                                                    <Toolbar BorderColor="LightSlateGray" BackColor="White">
                                                                                        <BorderSkin SkinStyle="Raised" FrameBackColor="SteelBlue" FrameBackGradientEndColor="LightBlue"
                                                                                            PageColor="Transparent" />
                                                                                    </Toolbar>
                                                                                </UI>
                                                                                <Legends>
                                                                                    <DCWC:Legend Name="Default">
                                                                                    </DCWC:Legend>
                                                                                </Legends>
                                                                                <DCWC:Legend Alignment="Center" BorderColor="Black" Docking="Bottom" Name="Default">
                                                                                </DCWC:Legend>
                                                                            </DCWC:Chart>
                                                                            <DCWC:Chart ID="CHRT_PaperWhite2" runat="server" CallbackStateContent="All" BackGradientType="DiagonalLeft"
                                                                                Palette="Kindergarten" BackGradientEndColor="#EFEFEF" BorderLineColor="#808080"
                                                                                BorderLineStyle="Solid" Height="225px" Width="900px" ImageType="Jpeg" ImageUrl="UploadFiles/temp/PaperWhite_Shift2">
                                                                                <Titles>
                                                                                    <DCWC:Title Name="Title1" Text="Paper White">
                                                                                    </DCWC:Title>
                                                                                </Titles>
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
                                                                                <Series>
                                                                                    <DCWC:Series BorderColor="100, 0, 0, 0" Name="Default" YValuesPerPoint="4" BackGradientEndColor="162, 164, 255"
                                                                                        CustomAttributes="PieDrawingStyle=Concave">
                                                                                    </DCWC:Series>
                                                                                </Series>
                                                                                <BorderSkin FrameBackColor="White" FrameBackGradientEndColor="White" FrameBorderColor="White"
                                                                                    FrameBorderWidth="2" SkinStyle="Raised" />
                                                                                <UI>
                                                                                    <Toolbar BorderColor="LightSlateGray" BackColor="White">
                                                                                        <BorderSkin SkinStyle="Raised" FrameBackColor="SteelBlue" FrameBackGradientEndColor="LightBlue"
                                                                                            PageColor="Transparent" />
                                                                                    </Toolbar>
                                                                                </UI>
                                                                                <Legends>
                                                                                    <DCWC:Legend Name="Default">
                                                                                    </DCWC:Legend>
                                                                                </Legends>
                                                                                <DCWC:Legend Alignment="Center" BorderColor="Black" Docking="Bottom" Name="Default">
                                                                                </DCWC:Legend>
                                                                            </DCWC:Chart>
                                                                            <DCWC:Chart ID="CHRT_TempHumidity2" runat="server" CallbackStateContent="All" BackGradientType="DiagonalLeft"
                                                                                Palette="Kindergarten" BackGradientEndColor="#EFEFEF" BorderLineColor="#808080"
                                                                                BorderLineStyle="Solid" Height="225px" Width="900px" ImageType="Jpeg" ImageUrl="UploadFiles/temp/TempHumidity_Shift2">
                                                                                <Titles>
                                                                                    <DCWC:Title Name="Title1" Text="Temperature Rel. Humidity">
                                                                                    </DCWC:Title>
                                                                                </Titles>
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
                                                                                <Series>
                                                                                    <DCWC:Series BorderColor="100, 0, 0, 0" Name="Default" YValuesPerPoint="4" BackGradientEndColor="162, 164, 255"
                                                                                        CustomAttributes="PieDrawingStyle=Concave">
                                                                                    </DCWC:Series>
                                                                                </Series>
                                                                                <BorderSkin FrameBackColor="White" FrameBackGradientEndColor="White" FrameBorderColor="White"
                                                                                    FrameBorderWidth="2" SkinStyle="Raised" />
                                                                                <UI>
                                                                                    <Toolbar BorderColor="LightSlateGray" BackColor="White">
                                                                                        <BorderSkin SkinStyle="Raised" FrameBackColor="SteelBlue" FrameBackGradientEndColor="LightBlue"
                                                                                            PageColor="Transparent" />
                                                                                    </Toolbar>
                                                                                </UI>
                                                                                <Legends>
                                                                                    <DCWC:Legend Name="Default">
                                                                                    </DCWC:Legend>
                                                                                </Legends>
                                                                                <DCWC:Legend Alignment="Center" BorderColor="Black" Docking="Bottom" Name="Default">
                                                                                </DCWC:Legend>
                                                                            </DCWC:Chart>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" colspan="2">
                                                                            <asp:Button ID="BTN_MakePDF" runat="server" CssClass="green2-btn" OnClick="BTN_MakePDF_Click"
                                                                                Text="Generate PDF"></asp:Button>&nbsp;
                                                                            <asp:Button ID="BTN_BackToSearch" OnClick="BTN_BackToSearch_Click" runat="server"
                                                                                CssClass="green2-btn" Text="Back To Search"></asp:Button>
                                                                        </td>
                                                                    </tr>
                                                                </table>
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
