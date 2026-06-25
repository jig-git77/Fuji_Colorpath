<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="DownloadSoftwares.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.DownloadSoftwares" %>

<%@ Register Src="~/UserControl/AdminStatusbar.ascx" TagName="AdminStatusbar" TagPrefix="ucAdminStatusbar" %>
<style type="text/css">
    div.RadGrid .rgRow {
        background: #F7F7F7;
    }

    div.RadGrid .rgAltRow {
        background: #F7F7F7;
    }

    .rgPager table {
        border-top: 0px;
        border-bottom: 1px solid #000000;
        border-spacing: 1pt;
        width: 100%;
    }

    .RadGrid_Default .rgRow a, .RadGrid_Default .rgAltRow a, .RadGrid_Default .rgEditRow a, .RadGrid_Default tr.rgEditRow a, .RadGrid_Default tr.rgHoveredRow a, .RadGrid_Default tr.rgActiveRow a, .RadGrid_Default .rgFooter a, .RadGrid_Default .rgEditForm a {
        color: #003CA9;
    }

    .RadGrid_Default td.rgPagerCell {
        -moz-border-bottom-colors: none;
        -moz-border-image: none;
        -moz-border-left-colors: none;
        -moz-border-right-colors: none;
        -moz-border-top-colors: none;
        border-color: #828282 #EEEEEE #EEEEEE;
        border-style: solid;
        border-width: 0px 0 0px 0px;
    }

    .RadGrid_Default .rgRow td, .RadGrid_Default .rgAltRow td, .RadGrid_Default .rgEditRow td, .RadGrid_Default .rgFooter td {
        border-style: none;
        border-width: 0 0 1px;
    }

    .RadGrid_Default .rgRow .rgSorted {
        background-color: #F7F7F7;
        border-bottom-color: #F7F7F7;
    }

    .RadGrid_Default .rgAltRow .rgSorted {
        background-color: #F7F7F7;
        border-bottom-color: #F7F7F7;
    }

    .RadGrid_Default th.rgSorted {
        background-color: #F1F1F1;
        background-position: 0px;
    }

    .breakWord200 {
        max-width: 200px !important;
        word-break: break-all !important;
        word-wrap: break-word !important;
        vertical-align: top;
        line-height: 15px;
    }

    .breakWord300 {
        max-width: 300px !important;
        word-break: break-all !important;
        word-wrap: break-word !important;
        vertical-align: top;
        line-height: 15px;
    }


    .Download_list_2 {
        /* margin: 36px 26px 0 23px; commented by Jayesh */
        overflow: hidden;
    }

        .Download_list_2 li {
            background: url(../Images/download_list_bg.png) no-repeat top left;
            width: 660px;
            height: 35px;
            margin-bottom: 11px;
        }

            .Download_list_2 li .left {
                float: left;
                margin: 10px 0 0 23px;
                color: #5e5e5e;
                font-size: 18px;
            }

    .download_list_row {
        padding: 16px;
        display: flex;
    }

    .download_list_row_left {
        display: flex;
        align-items: center;
        width: 43%;
        border-right: solid 1px #999;
        margin-right: 10px;
        margin-left: 5%;
    }

    .download_list_row_right {
        width: 55%;
        margin-right:6%;
    }

    .download_list_linkbutton {
        background-color: #ffffff;
        border: #d5d5d5 solid 1px;
        display: flex;
        padding: 10px 10px;
        box-sizing: border-box;
        border-radius: 10px;
        cursor: pointer;
        margin-bottom: 12px;
        align-items: center;
    }

        .download_list_linkbutton:last-child {
            margin-bottom: 0;
        }
        /*
            .download_list_linkbutton  {
                display: flex;
                align-items: center;
                justify-content: center;
                white-space: nowrap;
                cursor:pointer;
                color: #000;
            }
            */
        .download_list_linkbutton .dl_text {
            font-size: 12pt;
            color: #000;
            margin-bottom: 4px;
        }

        .download_list_linkbutton .dl_detail {
            font-size: 10pt;
            color: #000;
        }

        .download_list_linkbutton img {
            margin-right: 16px;
            width: 20px;
        }

    .download_list_head {
        font-size: 16pt;
        margin-bottom: 8px;
    }

    .download_list_detail {
        font-size: 10pt;
        line-height: 14pt;
    }

    .download_list_icon {
        width: 64px;
        margin-right: 16px;
    }
    .rgMasterTable{
        margin-top:4%;
    }
</style>

<div class="admin_top">
    &nbsp;
</div>

<div class="admin_middle">

    <div class="head">
        <h1>
            <asp:Localize ID="locDownloadResource2" runat="server" Text="Downloads"></asp:Localize>
        </h1>
    </div>

    <div class="clear"></div>

    <center>

        <div style="margin-left: 10px; width: 95%;">

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar"
                        runat="server"
                        Visible="false" />

                    <div class="RadGrid RadGrid_Default">

                        <table class="rgMasterTable" style="width: 100%;">

                            <tbody>

                                <!-- ========================= -->
                                <!-- Measure 3 -->
                                <!-- ========================= -->
                                <tr class="rgRow">
                                    <td class="download_list_row">

                                        <div class="download_list_row_left">

                                            <img class="download_list_icon"
                                                src="Images/measure3.png"
                                                alt="Measure 3" />

                                            <div>

                                                <div class="download_list_head">
                                                    <asp:Localize ID="locMeasure3Title"
                                                        runat="server"
                                                        Text="Measure 3"
                                                        meta:resourcekey="locMeasure3TitleResource1" />

                                                </div>

                                                <div class="download_list_detail">
                                                    <asp:Localize
                                                        ID="locMeasure3Detail"
                                                        runat="server"
                                                        Mode="PassThrough"
                                                        meta:resourcekey="locMeasure3Detail"
                                                        Text="For i1Pro3 and i1iO3<br />Released May 2026" />
                                                </div>

                                            </div>

                                        </div>

                                        <div class="download_list_row_right">

                                            <!-- Mac Download -->
                                            <a class="download_list_linkbutton"
                                                href="Download/Measure 3 Mac 1.0.dmg"
                                                target="_blank">

                                                <img src="Images/mac.png"
                                                    alt="Download for Mac" />

                                                <div>
                                                    <div class="dl_text">
                                                        <asp:Localize ID="locMeasure3ForMac"
                                                            runat="server"
                                                            Text="Download for Mac"
                                                            meta:resourcekey="locMeasureForMac" />
                                                    </div>

                                                    <div class="dl_detail">
                                                        <asp:Localize ID="locMeasure3ForMacDetail"
                                                            runat="server"
                                                            Text="v1.0 (Apple Silicon)"
                                                            meta:resourcekey="locMeasure3ForMacDetail" />
                                                    </div>
                                                </div>

                                            </a>

                                            <!-- PC Download -->
                                            <a class="download_list_linkbutton"
                                                href="Download/Measure 3 PC 1.0.zip"
                                                target="_blank">

                                                <img src="Images/pc.png"
                                                    alt="Download for PC" />

                                                <div>
                                                    <div class="dl_text">
                                                        <asp:Localize ID="locMeasure3ForPC"
                                                            runat="server"
                                                            Text="Download for PC"
                                                            meta:resourcekey="locMeasureForPC" />
                                                    </div>

                                                    <div class="dl_detail">
                                                        <asp:Localize ID="locMeasure3ForPCDetail"
                                                            runat="server"
                                                            Text="v1.0 (64-bit)"
                                                            meta:resourcekey="locMeasure3ForPCDetail" />
                                                    </div>
                                                </div>

                                            </a>

                                            <!-- PDF -->
                                            <%-- change path before commit--%>
                                            <a class="download_list_linkbutton"
                                                href="Download/ColorPath SYNC Measure 3 Guide.pdf"
                                                target="_blank">

                                                <img src="Images/pdficon.png"
                                                    alt="PDF" />

                                                <div class="dl_detail">
                                                    <asp:Localize ID="Measure3TitleReferencePdf"
                                                        runat="server"
                                                        Text="Measure 3"
                                                        meta:resourcekey="locMeasure3TitleResource1" />
                                                    <asp:Localize ID="LocMeasure3ReferenceGuidePdf"
                                                        runat="server"
                                                        Text="Reference Guide (PDF)"
                                                        meta:resourcekey="LocMeasureReferenceGuidePdf" />
                                                </div>

                                            </a>

                                        </div>

                                    </td>
                                </tr>

                                <!-- ========================= -->
                                <!-- Measure Legacy -->
                                <!-- ========================= -->
                                <tr class="rgRow">
                                    <td class="download_list_row">

                                        <div class="download_list_row_left">

                                            <img class="download_list_icon"
                                                src="Images/measure3.png"
                                                alt="Measure Legacy" />

                                            <div>

                                                <div class="download_list_head">
                                                    <asp:Localize ID="locMeasureLegacyTitle"
                                                        runat="server"
                                                        Text="Measure (Legacy)"
                                                        meta:resourcekey="locMeasureLegacyTitleResource" />
                                                </div>

                                                <div class="download_list_detail">
                                                    <asp:Localize
                                                        ID="locMeasureLegacyDetail"
                                                        runat="server"
                                                        Mode="PassThrough"
                                                        meta:resourcekey="locMeasureLegacyDetail"
                                                        Text="For i1Pro2, i1iO2 and i1iSis2 <br /> For i1Pro3 and i1iO3 <br /> Legacy Support" />
                                                </div>

                                            </div>

                                        </div>

                                        <div class="download_list_row_right">

                                            <!-- Existing Mac Download -->
                                            <a class="download_list_linkbutton"
                                                href="Download/Measure Mac Installer.zip"
                                                target="_blank">

                                                <img src="Images/mac.png"
                                                    alt="Download for Mac" />

                                                <div>

                                                    <div class="dl_text">
                                                        <asp:Localize ID="locMeasureLegacyForMac"
                                                            runat="server"
                                                            Text="Download for Mac"
                                                            meta:resourcekey="locMeasureForMac" />
                                                    </div>

                                                    <div class="dl_detail">
                                                         <asp:Localize ID="locMeasureLegacyForMacDetail"
                                                            runat="server"
                                                            Text="v1.0.61 (Intel Chips)"
                                                            meta:resourcekey="locMeasureLegacyForMacDetail" />
                                                    </div>

                                                </div>

                                            </a>

                                            <!-- Existing PC Download -->
                                            <a class="download_list_linkbutton"
                                                href="Download/Measure PC Installer.zip"
                                                target="_blank">

                                                <img src="Images/pc.png"
                                                    alt="Download for PC" />

                                                <div>

                                                    <div class="dl_text">
                                                        <asp:Localize ID="locMeasureLegacyForPC"
                                                            runat="server"
                                                            Text="Download for PC"
                                                            meta:resourcekey="locMeasureForPC" />
                                                    </div>

                                                    <div class="dl_detail">
                                                        <asp:Localize ID="locMeasureLegacyForPCDetail"
                                                            runat="server"
                                                            Text="v1.0.60 (64-bit)"
                                                            meta:resourcekey="locMeasureLegacyForPCDetail" />
                                                    </div>

                                                </div>

                                            </a>

                                            <!-- Existing PDF -->
                                            <a class="download_list_linkbutton"
                                                href="Download/ColorPath SYNC Measure Guide.pdf"
                                                target="_blank">

                                                <img src="Images/pdficon.png"
                                                    alt="PDF" />

                                                <div class="dl_detail">
                                                    <asp:Localize ID="locMeasureLegacyReferencePdf"
                                                        runat="server"
                                                        Text="Measure (Legacy)"
                                                        meta:resourcekey="locMeasureLegacyTitleResource" />
                                                    <asp:Localize ID="LocMeasureLegacyReferenceGuidePdf"
                                                        runat="server"
                                                        Text="Reference Guide (PDF)"
                                                        meta:resourcekey="LocMeasureReferenceGuidePdf" />
                                                </div>

                                            </a>

                                        </div>

                                    </td>
                                </tr>
                            </tbody>

                        </table>

                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>

        </div>

    </center>

</div>

<div class="admin_bottom">
    &nbsp;
</div>

<div class="clear">
</div>

<%--<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle">
    <div class="head">
        <h1><asp:Localize runat="server" Text="Download" ID="locDownload" 
                meta:resourcekey="locDownloadResource1"></asp:Localize></h1>       
    </div>
    <div class="clear">
    </div>
    <center>
        <div style="margin-left: 10px;" >
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" 
                    Visible="False" />
                <telerik:RadGrid ID="rgDownload" AllowSorting="True"
                    AllowPaging="True" PageSize="5" runat="server" AutoGenerateColumns="False" EnableLinqExpressions="False"
                    OnNeedDataSource="rgDownload_NeedDataSource" Width="75%" CellSpacing="0" 
                    GridLines="None">
                    <HeaderStyle Font-Bold="True" />
                    <PagerStyle Position="Top" Font-Size="Small" />
                    <MasterTableView DataKeyNames="DownloadId,DownloadName">
                        <CommandItemSettings ExportToPdfText="Export to PDF" />
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" 
                            Visible="True">
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
                            Visible="True">
                        </ExpandCollapseColumn>
                        <Columns>                             
                            <telerik:GridTemplateColumn DataField="DownloadName" HeaderText="Software Name"
                                   UniqueName="DownloadName" SortExpression="DownloadName" 
                                FilterControlAltText="Filter DownloadName column" 
                                meta:resourcekey="GridTemplateColumnResource1">
                                        <ItemTemplate>
                                            <div class="Download_list">
                                                <ul>
                                                    <li>
                                                        <div class="left">
                                                            <a href='<%# Eval("DownloadPath") %>' target="_blank">
                                                                <asp:Label ID="lblDownloadName" runat="server" 
                                                                Text='<%# Eval("DownloadName") %>' 
                                                                meta:resourcekey="lblDownloadNameResource1" />
                                                            </a>
                                                        </div>
                                                        <div class="clear">
                                                        </div>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="clear">
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="breakWord200" />
                                </telerik:GridTemplateColumn>                                                  
                        </Columns>
                        
                    </MasterTableView>
             
                </telerik:RadGrid>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </center>
</div>
<div class="admin_bottom">
    &nbsp;</div>
<div class="clear">
</div>--%>
