<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Videos.aspx.cs" Inherits="TaskeroWeb.Videos" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/Common/Menu.ascx" TagName="AdminMenu" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="rad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <table width="940" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="23" height="36" valign="top">
                            <img src="images/menu-curve-left.png" alt="FujiFilm" border="0" />
                        </td>
                        <td valign="top" class="content-menu-top" align="left">
                            <strong>Videos</strong>
                        </td>
                        <td width="23" valign="top">
                            <img src="images/menu-curve-right.png" alt="FujiFilm" border="0" />
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
        <tr>
            <td>
                <%--<asp:UpdatePanel ID="UP_Video" runat="server" OnLoad="UP_Video_Load">
                    <ContentTemplate>--%>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="LBL_Error" runat="server" CssClass="ERROR"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <%--Upper Part(Left And Right)--%>
                            <asp:UpdatePanel ID="upVideoDisplayArea" runat="server" OnLoad="UP_Video_Load">
                                <ContentTemplate>
                                    <%--Manish--%>
                                    <asp:Button runat="server" ID="btnRefreshMainDisplayArea" OnClick="btnRefreshMainDisplayArea_Click"
                                        Style="display: none" />
                                    <%--Manish--%>
                                    <table width="100%" border="0" cellspacing="2" cellpadding="2">
                                        <tr>
                                            <td class="green-form-bg" style="width: 300px" valign="top">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td class="green-title" align="Center">
                                                            <div id="divSearch" runat="server" style="float: left">
                                                                <b>Find Videos</b></div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table border="0" cellpadding="0" cellspacing="3" width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <table cellpadding="1" cellspacing="1" width="100%" border="0">
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <b>Keywords&nbsp;:</b>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="TXT_Search" runat="server"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <b>In</b>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div style="background-color: #FFFFFF; overflow: auto; width: 170px; border: 1px solid #000000">
                                                                            <asp:DataList ID="dlCategoryList" runat="server" RepeatColumns="1" RepeatDirection="Vertical">
                                                                                <ItemTemplate>
                                                                                    <asp:Literal ID="ltrlCategoryID" Visible="false" runat="server" Text='<%# Eval("CategoryID") %>'></asp:Literal>
                                                                                    <asp:CheckBox ID="chkCategory" runat="server" Text='<%# Eval("Description") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:DataList>
                                                                            <telerik:RadTreeView ID="rtv_CategoryList" runat="server" CheckBoxes="true" Visible="false">
                                                                                <Nodes>
                                                                                </Nodes>
                                                                            </telerik:RadTreeView>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr visible="false">
                                                                    <td>
                                                                        <asp:CheckBoxList ID="Chk_CategoryList" runat="server" Visible="false">
                                                                        </asp:CheckBoxList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table cellspacing="0" cellpadding="0">
                                                                            <tr>
                                                                                <td style="padding-right: 2px">
                                                                                    <asp:Button ID="BTN_Search" runat="server" CssClass="BUTTON" Text="Search" OnClick="BTN_Search_Click" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Button ID="BTN_ClearSearch" runat="server" CssClass="BUTTON" Text="Clear Search"
                                                                                        OnClick="BTN_ClearSearch_Click" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button ID="BTN_AdvanceSearch" runat="server" CssClass="BUTTON" Text="Advance Search >> " />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <%--<tr>
                                            <td class="green-title" align="left">
                                                <b>
                                                    <asp:Literal ID="LTRL_MostPopular" runat="server" Text="Most Popular"></asp:Literal></b>
                                            </td>
                                        </tr>--%>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="green-form-bg" valign="top" style="width: 630px">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td class="green-title" align="right">
                                                            <div id="divListGridAction" style="display: block">
                                                                <div style="float: right; padding: 2px 5px 2px 5px">
                                                                    <asp:ImageButton ID="ibtnGridView" runat="server" onmouseover="assignImage(this,'Images/icon_grid_view_over.gif',3)"
                                                                        onmouseout="assignImage(this,'Images/icon_grid_view_off.gif',3)" ImageUrl="~/Images/icon_grid_view_off.gif"
                                                                        OnClick="ibtnGridView_Click" />
                                                                </div>
                                                                <div style="float: right; padding: 2px 0px 2px 5px">
                                                                    <asp:ImageButton ID="ibtnThumbView" runat="server" onmouseover="assignImage(this,'Images/icon_thumb_view_over.gif',2)"
                                                                        onmouseout="assignImage(this,'Images/icon_thumb_view_off.gif',2)" ImageUrl="~/Images/icon_thumb_view_off.gif"
                                                                        OnClick="ibtnThumbView_Click" />
                                                                </div>
                                                                <div style="float: right; padding: 2px 0px 2px 5px">
                                                                    <asp:ImageButton ID="ibtnSlideShowView" runat="server" onmouseover="assignImage(this,'Images/icon_slide_show_over.gif',1)"
                                                                        onmouseout="assignImage(this,'Images/icon_slide_show_off.gif',1)" ImageUrl="~/Images/icon_slide_show_on.gif"
                                                                        OnClick="ibtnSlideShowView_Click" />
                                                                </div>
                                                                <div style="float: right; padding-right: 2px">
                                                                    <b>
                                                                        <asp:Literal ID="LTRL_ViewVideos" runat="server" Text="View:"></asp:Literal>
                                                                    </b>
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div id="divVideoDetailMore" style="display: none; width: 750px;height:372px">
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr>
                                                                        <td colspan="2" class="green-title" align="right">
                                                                            <div style="float: left">
                                                                                Video Details</div>
                                                                            <div style="float: right; vertical-align: middle; padding-top: 2px; padding-right: 2px">
                                                                                <img src="Images/Close2.png" style="cursor: pointer" onclick="return CloseVideoDetailMore();" />
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="top" style="padding: 10px; width: 310px">
                                                                            <div id='strVideoImage' class="div_grid_video">
                                                                            </div>
                                                                        </td>
                                                                        <td valign="top" align="left" style="padding: 10px 10px 10px 0px">
                                                                            <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                                                                <tr>
                                                                                    <td style="border-bottom: 1px solid #D1D1D1; vertical-align: top" align="left">
                                                                                        <b>Title&nbsp;:&nbsp;<b>
                                                                                    </td>
                                                                                    <td align="left" width="100%" style="border-bottom: 1px solid #D1D1D1">
                                                                                        <div id="strVideoTitle" style="padding: 2px">
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left" style="border-bottom: 1px solid #D1D1D1; vertical-align: top">
                                                                                        <b>Description&nbsp;:&nbsp;</b>
                                                                                    </td>
                                                                                    <td align="left" style="border-bottom: 1px solid #D1D1D1; vertical-align: top">
                                                                                        <div id="strVideoDestription" style="padding: 2px">
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left" style="border-bottom: 1px solid #D1D1D1; vertical-align: top">
                                                                                        <b>Keywords :</b>
                                                                                    </td>
                                                                                    <td align="left" style="border-bottom: 1px solid #D1D1D1; vertical-align: top">
                                                                                        <div id="strVideoKeywords" style="padding: 2px">
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="display: none">
                                                                                    <td>
                                                                                    </td>
                                                                                    <td align="right">
                                                                                        <div id="divViewVideoLink" style="padding: 2px">
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            <div id="divListGridView" style="width: 750px">
                                                                <div id="divViewVideos1" runat="server" style="width: 750px;height:350px" visible="true">
                                                                    <center>
                                                                        <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                                            <tr>
                                                                                <td align="center" valign="top">
                                                                                    <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" id="Videos" width="100%"
                                                                                        height="350" codebase="http://fpdownload.macromedia.com/get/flashplayer/current/swflash.cab">
                                                                                        <param name="movie" value="images/VideoRotate.swf" />
                                                                                        <param name="quality" value="high" />
                                                                                        <param name="bgcolor" value="#869ca7" />
                                                                                        <param name="allowScriptAccess" value="sameDomain" />
                                                                                        <param name="FlashVars" value="data=VideoView.aspx" />
                                                                                        <param name="wmode" value="transparent" />
                                                                                        <embed src="images/VideoRotate.swf" quality="high" bgcolor="#869ca7" width="100%"
                                                                                            height="350" name="Investor" wmode="transparent" align="middle" play="true" loop="false"
                                                                                            quality="high" allowscriptaccess="sameDomain" flashvars="data=VideoView.aspx"
                                                                                            type="application/x-shockwave-flash" pluginspage="http://www.adobe.com/go/getflashplayer">
			                                                        </embed>
                                                                                    </object>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <%--<object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0"
                                                            width="700" height="380">
                                                            <param name="movie" value="images/Videos.swf" />
                                                            <param name="wmode" value="transparent">
                                                            <param name="quality" value="high" />
                                                            <embed src="images/Videos.swf" quality="high" wmode="transparent" pluginspage="http://www.macromedia.com/go/getflashplayer"
                                                                type="application/x-shockwave-flash" width="500" height="380"></embed></object>--%>
                                                                    </center>
                                                                </div>
                                                                <div id="divViewVideos2" runat="server" style="width: 750px;height:350px" visible="false">
                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td align="center" style="padding-top: 10px">
                                                                                <div style="width: 100%; height: 300px; overflow: auto;">
                                                                                    <asp:DataList ID="DL_Videos2" runat="server" Width="100%" RepeatColumns="4" RepeatDirection="Horizontal"
                                                                                        OnItemDataBound="DL_Videos2_ItemDataBound">
                                                                                        <ItemTemplate>
                                                                                            <table>
                                                                                                <tr>
                                                                                                    <td valign="top" style="height:110px;width:110px" align="center">
                                                                                                        <div class="div_grid_video" style="float:none">
                                                                                                            <%--<%# "Videos/VideosThumb/" + Convert.ToString(DataBinder.Eval(Container.DataItem, "VideoID")) + ".jpg" %>--%>
                                                                                                            <asp:Image ID="imgVideoFile" AlternateText="Image Not Found" runat="server" ImageUrl='<%# "~/Videos/VideosThumb/" + Convert.ToString(DataBinder.Eval(Container.DataItem, "VideoID")) + ".jpg" %>'
                                                                                                                Width="100px" />
                                                                                                        </div>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                            <div style="z-index: -1; width: 170px;">
                                                                                                <%# DataBinder.Eval(Container.DataItem, "LinkText") %>
                                                                                            </div>
                                                                                            <%--<asp:Image ImageUrl="~/App_Themes/Default/RadControls/ToolTip/Skins/Black/Img/Black.gif"--%>
                                                                                            <telerik:RadToolTip ID="rttVideo" runat="server" Height="250px" Width="400px" IsClientID="true"
                                                                                                OffsetY="4" HideEvent="LeaveToolTip" Animation="Fade" Position="BottomCenter"
                                                                                                RelativeTo="Element" EnableEmbeddedSkins="false" Skin="FujiFilm">
                                                                                                <table cellpadding="5" cellspacing="5" width="100%">
                                                                                                    <tr>
                                                                                                        <td valign="top" align="center" style="height:110px;width:110px" width="150px" rowspan="2">
                                                                                                            <div class="div_grid_video">
                                                                                                                <asp:Image ID="rttVideoImage" AlternateText="Image Not Found" runat="server" ImageUrl='<%# "~/Videos/VideosThumb/" + Convert.ToString(DataBinder.Eval(Container.DataItem, "VideoID")) + ".jpg" %>'
                                                                                                                    Width="150px" />
                                                                                                            </div>
                                                                                                        </td>
                                                                                                        <td style="widows: 5px">
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                        <td valign="top">
                                                                                                            <table width="100%" cellpadding="2" cellspacing="2" height="100%">
                                                                                                                <tr>
                                                                                                                    <td style="width: 30%" valign="top" align="left">
                                                                                                                        <b>Title</b>
                                                                                                                    </td>
                                                                                                                    <td style="width: 1%" valign="top" align="left">
                                                                                                                        <b>:</b>
                                                                                                                    </td>
                                                                                                                    <td style="width: 70%" valign="top" align="left">
                                                                                                                        <%# DataBinder.Eval(Container.DataItem, "LinkText") %>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td style="width: 30%" valign="top" align="left">
                                                                                                                        <b>Description</b>
                                                                                                                    </td>
                                                                                                                    <td style="width: 1%" valign="top" align="left">
                                                                                                                        <b>:</b>
                                                                                                                    </td>
                                                                                                                    <td style="width: 70%" valign="top">
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td colspan="3" valign="top" align="left">
                                                                                                                        <%# GetDescription(Convert.ToString(DataBinder.Eval(Container.DataItem, "Description")))%>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td colspan="3" valign="top" align="right" height="100%" valign="bottom">
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="3" align="right">
                                                                                                            <a href="#" onclick="return OpenMovieFile('PlayVideoFile.aspx?u=1&VideoID=<%# DataBinder.Eval(Container.DataItem, "VideoID") %>');">
                                                                                                                Watch Video</a>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </telerik:RadToolTip>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Center" />
                                                                                    </asp:DataList>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-top: 15px">
                                                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                                                    <tr>
                                                                                        <td align="left" style="width: 20%">
                                                                                            <div style="float: left; padding-left: 10px; padding-bottom: 5px">
                                                                                                <asp:Button ID="BTN_ListPrev" runat="server" CssClass="BUTTON" ToolTip="Previous" Text="<< Prev" OnClick="BTN_ListPrev_Click" />
                                                                                            </div>
                                                                                        </td>
                                                                                        <td align="center" style="width: 60%">
                                                                                            <telerik:RadSlider ID="RadSliderVideo" OnClientValueChange="ChangeThumbnailSize"
                                                                                                OnClientLoaded="RadSliderClientLoaded" runat="server" MinimumValue="100" MaximumValue="160"
                                                                                                Value="100" Visible="true" Skin="Telerik" Width="350px">
                                                                                                <Items>
                                                                                                    <telerik:RadSliderItem />
                                                                                                </Items>
                                                                                            </telerik:RadSlider>
                                                                                        </td>
                                                                                        <td align="right" style="width: 20%">
                                                                                            <div style="float: right; padding-right: 10px; padding-bottom: 5px">
                                                                                                <asp:Button ID="BTN_ListNext" runat="server" ToolTip="Next" CssClass="BUTTON" Text="Next >>" OnClick="BTN_ListNext_Click" />
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <div id="divViewVideos3" runat="server" style="width: 750px;height:350px" visible="false">
                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="height:350px">
                                                                        <tr>
                                                                            <td valign="top" align="center">
                                                                                <asp:DataList ID="DL_Videos3" runat="server" Width="100%" OnItemDataBound="DL_Videos3_ItemDataBound">
                                                                                    <HeaderTemplate>
                                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr class="div_grid_row" style="height:150px">
                                                                                            <td style="width: 100px" valign="top">
                                                                                                <div class="div_grid_video">
                                                                                                    <asp:Image ID="imgVideoFile" AlternateText="Image" runat="server" ImageUrl='<%# "~/Videos/VideosThumb/" + Convert.ToString(DataBinder.Eval(Container.DataItem, "VideoID")) + ".jpg" %>'
                                                                                                        Width="100px" Height="100px" />
                                                                                                </div>
                                                                                            </td>
                                                                                            <td style="vertical-align: top">
                                                                                                <div style="padding-bottom: 5px">
                                                                                                    <b>Title&nbsp;:&nbsp;</b><%# Convert.ToString(DataBinder.Eval(Container.DataItem, "LinkText")) %>
                                                                                                </div>
                                                                                                <div style="padding-bottom: 5px">
                                                                                                    <b>Description :</b>
                                                                                                    <br />
                                                                                                    <%# GetDescription(Convert.ToString(DataBinder.Eval(Container.DataItem, "Description")))%>
                                                                                                </div>
                                                                                            </td>
                                                                                            <td valign="bottom">
                                                                                                <a href="#" onclick="OpenVideoFile('<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "VideoID")) %>','<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Description")) %>','<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "LinkText")) %>','<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Keywords")) %>','<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "VideoID")) %>');">
                                                                                                    More..</a>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        </table>
                                                                                    </FooterTemplate>
                                                                                </asp:DataList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <div style="float: left; padding-left: 10px; padding-bottom: 3px; padding-top: 5px">
                                                                                    <asp:Button ID="BTN_GridPrev" runat="server" ToolTip="Previous" CssClass="BUTTON" Text="<< Prev" OnClick="BTN_GridPrev_Click" />
                                                                                </div>
                                                                                <div style="float: right; padding-right: 10px; padding-bottom: 3px; padding-top: 5px">
                                                                                    <asp:Button ID="BTN_GridNext" runat="server" ToolTip="Next" CssClass="BUTTON" Text="Next >>" OnClick="BTN_GridNext_Click" />
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <div id="divViewVideo4" runat="server" style="width: 100%" visible="false">
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" valign="top">
                            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                <tr>
                                    <td class="green-form-bg" valign="top">
                                        <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                            <tr>
                                                <td class="green-title" align="left">
                                                    <div style="float: left">
                                                        <b>
                                                            <asp:Literal ID="LTRL_RecentlyViewed" runat="server" Text="Recently Viewed"></asp:Literal></b>
                                                    </div>
                                                    <div id="divRecentlyOff" runat="server" style="float: right; padding-top: 5px; padding-right: 5px">
                                                        <asp:ImageButton ID="ibtnOff" runat="server" ImageUrl="~/Images/aerrow-up.gif" OnClientClick="return PanelOnOff();" /></div>
                                                    <div id="divRecentlyOn" runat="server" style="display: none; float: right; padding-top: 5px;
                                                        padding-right: 5px">
                                                        <asp:ImageButton ID="ibtnOn" runat="server" ImageUrl="~/Images/aerrow-down.gif" OnClientClick="return PanelOnOff();" /></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div id="divRecentlyViewed" runat="server">
                                                        <asp:UpdatePanel ID="UPRecentVideo" runat="server">
                                                            <ContentTemplate>
                                                                <asp:Button ID="btnRefreshRecentVideo" Style="display: none" runat="server" OnClick="btnRefreshRecentVideo_Click"
                                                                    Text="Refresh Recent Videos" />
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr>
                                                                        <td style="width: 50px; padding-left: 5px" align="left">
                                                                            <asp:ImageButton ID="ibtn_RecentlyPrevious" ImageUrl="~/images/5.jpg" ToolTip="Previous" runat="server"
                                                                                OnClick="ibtn_RecentlyPrevious_Click" />
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:DataList ID="DL_RecentlyVideos" runat="server" RepeatColumns="7" RepeatDirection="Horizontal"
                                                                                OnItemDataBound="DL_RecentlyVideos_ItemDataBound">
                                                                                <ItemTemplate>
                                                                                    <table border="0">
                                                                                        <tr>
                                                                                            <td style="width:110px; z-index: 1" align="center">
                                                                                                <div  class="div_grid_video">
                                                                                                    <asp:Image ID="imgVideoFile" AlternateText="Image" runat="server" ImageUrl='<%# "~/Videos/VideosThumb/" + Convert.ToString(DataBinder.Eval(Container.DataItem, "VideoID")) + ".jpg" %>'
                                                                                                        Width="100px" />
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </ItemTemplate>
                                                                            </asp:DataList>
                                                                        </td>
                                                                        <td style="width: 50px; padding-right: 5px" align="right">
                                                                            <asp:ImageButton ID="ibtn_RecentlyNext" ToolTip="Next" ImageUrl="~/images/6.jpg" runat="server"
                                                                                OnClick="ibtn_RecentlyNext_Click" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div>
                                <asp:Button runat="server" ID="hiddenTargetControlForModalPopup1" Style="display: none" />
                                <ajax:ModalPopupExtender 
                                    runat="server" 
                                    ID="ModalPopupExtender1" 
                                    BehaviorID="programmaticModalPopupBehavior1"
                                    TargetControlID="hiddenTargetControlForModalPopup1" 
                                    PopupControlID="programmaticPopup1"
                                    BackgroundCssClass="modalBackground" 
                                    DropShadow="false" 
                                    PopupDragHandleControlID="programmaticPopupDragHandle">
                                </ajax:ModalPopupExtender>
                                <asp:Panel runat="server" ID="programmaticPopup1" Style="display: none; height: 500px;
                                    background-color: White; z-index: 1">
                                    <a id="hideModalPopupViaClientButton1" href="#" style="border: none; text-decoration: none;">
                                        <img src="Images/Close2.png" style="position: absolute; border: none; right: 0px;
                                            top: 0px; text-align: right;" />
                                    </a>
                                    <iframe id="FrmPopup" src="" width="620px" height="500" frameborder="0" scrolling="auto">
                                    </iframe>
                                </asp:Panel>
                                <asp:Button runat="server" ID="hiddenTargetControlForModalPopup2" Style="display: none" />
                                <ajax:ModalPopupExtender runat="server" ID="ModalPopupExtender2" BehaviorID="programmaticModalPopupBehavior2"
                                    TargetControlID="hiddenTargetControlForModalPopup2" PopupControlID="programmaticPopup2"
                                    BackgroundCssClass="modalBackgroundPopupVideo" DropShadow="false" PopupDragHandleControlID="programmaticPopupDragHandle">
                                </ajax:ModalPopupExtender>
                                <asp:Panel runat="server" ID="programmaticPopup2" Style="display: none; height: 595px;
                                    background-color: none;">
                                    <iframe allowtransparency="true" id="FrmPopup2" src="" width="655px" height="595px"
                                        frameborder="0" scrolling="no" style="border: 0px solid #006699;"></iframe>
                                </asp:Panel>
                            </div>
                        </td>
                    </tr>
                </table>
                <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
                <%-------------------------------------------------------------------------%>
                <asp:Button runat="server" ID="hiddenTargetControlForModalPopup" Style="display: none" />
                <ajax:ModalPopupExtender runat="server" ID="programmaticModalPopup" BehaviorID="programmaticModalPopupBehavior"
                    TargetControlID="hiddenTargetControlForModalPopup" PopupControlID="programmaticPopup"
                    BackgroundCssClass="modalBackground" DropShadow="false" PopupDragHandleControlID="programmaticPopupDragHandle">
                </ajax:ModalPopupExtender>
                <asp:Panel runat="server" ID="programmaticPopup" Style="display: none; height: 500px;
                    width: 500px; background-color: White; z-index: 1000">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <div style="z-index: 2">
                                    <table cellpadding="0" cellspacing="0" border="0" align="left" width="100%">
                                        <tr>
                                            <td class="ERROR">
                                                <asp:Literal ID="LblError" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                    <tr>
                                                        <td class="green-title">
                                                            <b>
                                                                <asp:Literal ID="LTRL_AdvancedSearch" runat="server" Text="Advanced Search"></asp:Literal></b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table border="0" cellpadding="5" cellspacing="5" width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <b>
                                                                            <asp:Literal ID="LTRL_Category" runat="server" Text="By Category"></asp:Literal></b>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding-left: 35px">
                                                                        <asp:DataList ID="DL_CategoryList" runat="server" RepeatColumns="2" RepeatDirection="Vertical"
                                                                            OnItemDataBound="DL_CategoryList_ItemDataBound" DataKeyField="CategoryID">
                                                                            <HeaderTemplate>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        Chk -
                                                                                                        <asp:CheckBox ID="CHK_MainCategory" runat="server" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="padding-left: 25px">
                                                                                                        <asp:CheckBoxList ID="CHK_ChildCategory" runat="server" RepeatDirection="Vertical"
                                                                                                            RepeatColumns="2">
                                                                                                        </asp:CheckBoxList>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                            </FooterTemplate>
                                                                        </asp:DataList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <hr />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <b>
                                                                            <asp:Literal ID="LTRL_KeyWords" runat="server" Text="By Keywords"></asp:Literal></b>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding-left: 35px">
                                                                        <asp:TextBox ID="TXT_KeyWords" runat="server"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:Button ID="BTN_AdvancedSearch" TabIndex="48" runat="server" CssClass="BUTTON"
                                                                            Text="Search" CausesValidation="false" OnClick="BTN_AdvancedSearch_Click"></asp:Button>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <a id="hideModalPopupViaClientButton" href="#" style="border: none; text-decoration: none;">
                        <img src="Images/Close2.png" style="position: absolute; border: none; right: 0px;
                            top: 0px; text-align: right;" />
                    </a>
                    <%--<iframe id="FrmPopup" src="" width="620px" height="585" frameborder="0" scrolling="auto">
                                            </iframe>--%></asp:Panel>
                <%-------------------------------------------------------------------------------%>
            </td>
        </tr>
    </table>

    <script language="javascript" type="text/javascript">
        // Add click handlers for buttons to show and hide modal popup on pageLoad
        function RadSliderClientLoaded(sender, eventArgs) {
            sender.set_value() = 100;
        }
        function ChangeThumbnailSize(sender, eventArgs) {

            var originalWidth = 100;

            var objDG = document.getElementById("<%=DL_Videos2.ClientID %>");

            //UncheckReviwed(obj);

            if (navigator.appName == "Microsoft Internet Explorer") //check the browser is IE
            {
                obj = objDG.all.tags("img"); //ID of the Datagrid
            }
            else {//for the browser other than IE
                obj = objDG.getElementsByTagName('img');
            }

            for (var i = 0; i < obj.length; i++) // check the all items in the grid
            {
                if (obj[i].id.indexOf('imgVideoFile') != -1) {
                    obj[i].style.width = originalWidth * sender.get_value() / 100 + "px";
                }
            }
        }


        function assignImage(obj, imgUrl, imgNo) {

            var div1 = document.getElementById("<%=divViewVideos1.ClientID %>")
            var div2 = document.getElementById("<%=divViewVideos2.ClientID %>")
            var div3 = document.getElementById("<%=divViewVideos3.ClientID %>")

            if (div1 != null) {
                if (imgNo == 1)
                    obj.src = "Images/icon_slide_show_on.gif";
                else
                    obj.src = imgUrl
            }
            else if (div2 != null) {
                if (imgNo == 2)
                    obj.src = "Images/icon_thumb_view_on.gif";
                else
                    obj.src = imgUrl
            }
            else if (div3 != null) {
                if (imgNo == 3)
                    obj.src = "Images/icon_grid_view_on.gif";
                else
                    obj.src = imgUrl
            }

        }

        function pageLoad() {

            $addHandler($get("hideModalPopupViaClientButton1"), 'click', hideModalPopupViaClient1);
            $addHandler($get("hideModalPopupViaClientButton"), 'click', hideModalPopupViaClient);
        }

        function showModalPopupViaClient(ev) {
            ev.preventDefault();
            var modalPopupBehavior = $find('programmaticModalPopupBehavior');
            modalPopupBehavior.show();
            document.getElementById("FrmPopup").src = "";
        }

        function hideModalPopupViaClient(ev) {
            ev.preventDefault();
            var modalPopupBehavior = $find('programmaticModalPopupBehavior');
            modalPopupBehavior.hide();
            document.getElementById("FrmPopup").src = "";
        }
        function hideModalPopupViaClient1(ev) {
            ev.preventDefault();
            var modalPopupBehavior = $find('programmaticModalPopupBehavior1');
            modalPopupBehavior.hide();
            document.getElementById("FrmPopup").src = "";

            //document.getElementById("<%= btnRefreshMainDisplayArea.ClientID %>").click();
            //__doPostBack("UP_Video", "");
            return false;
        }
        function hidePopup() {
            var modalPopupBehavior = $find('programmaticModalPopupBehavior1');
            modalPopupBehavior.hide();
            document.getElementById("<%= btnRefreshMainDisplayArea.ClientID %>").click();
            //__doPostBack("UP_Video", "");
            return false;}
        function OpenAdvanceSearch(URLStr) {
            document.getElementById("FrmPopup").src = URLStr;
            //var modalPopupBehavior = $find('programmaticModalPopupBehavior');
            var modalPopupBehavior = $find('programmaticModalPopupBehavior1');
            modalPopupBehavior.show();
            return false;
        }


        //        function OpenVideoFile(URLStr)
        //        {           
        //            document.getElementById("FrmPopup").src = URLStr;
        //            var modalPopupBehavior = $find('programmaticModalPopupBehavior1');
        //            modalPopupBehavior.show();
        //            return false;
        //        }
        function OpenVideoFile(img, dec, title, keywords, videoid) {

            img = "Videos/VideosThumb/" + videoid + ".jpg";
            document.getElementById('divListGridView').style.display = "none";
            document.getElementById('divListGridAction').style.display = "none";
            document.getElementById('divVideoDetailMore').style.display = "block";
            document.getElementById('strVideoImage').innerHTML = "<img src='" + img + "' height='300px' width='300px' style='cursor:pointer' onclick=\"return OpenMovieFile('PlayVideoFile.aspx?u=1&VideoID=" + videoid + "');\" />";
            document.getElementById('divViewVideoLink').innerHTML = "<a href='#' id='linkViewVideo'><span onclick=\"return OpenMovieFile('PlayVideoFile.aspx?u=1&VideoID=" + videoid + "');\">View Video</span></a>";
            document.getElementById('strVideoTitle').innerHTML = title;
            document.getElementById('strVideoDestription').innerHTML = dec;
            document.getElementById('strVideoKeywords').innerHTML = keywords;
            return false;
        }
        function PanelOnOff() {
            if (document.getElementById('<%= divRecentlyViewed.ClientID %>').style.display == 'none') {
                document.getElementById('<%= divRecentlyViewed.ClientID %>').style.display = 'block';
                document.getElementById('<%= divRecentlyOff.ClientID %>').style.display = 'block';
                document.getElementById('<%= divRecentlyOn.ClientID %>').style.display = 'none';
            }
            else {
                document.getElementById('<%= divRecentlyViewed.ClientID %>').style.display = 'none';
                document.getElementById('<%= divRecentlyOff.ClientID %>').style.display = 'none';
                document.getElementById('<%= divRecentlyOn.ClientID %>').style.display = 'block';
            }
            return false;
        }
        function ViewVideos1() {
            document.getElementById('<%= divViewVideos1.ClientID %>').style.display = 'block';
            document.getElementById('<%= divViewVideos2.ClientID %>').style.display = 'none';
            document.getElementById('<%= divViewVideos3.ClientID %>').style.display = 'none';
            return false;
        }
        function ViewVideos2() {
            document.getElementById('<%= divViewVideos1.ClientID %>').style.display = 'none';
            document.getElementById('<%= divViewVideos2.ClientID %>').style.display = 'block';
            document.getElementById('<%= divViewVideos3.ClientID %>').style.display = 'none';
            return false;
        }
        function ViewVideos3() {
            document.getElementById('<%= divViewVideos1.ClientID %>').style.display = 'none';
            document.getElementById('<%= divViewVideos2.ClientID %>').style.display = 'none';
            document.getElementById('<%= divViewVideos3.ClientID %>').style.display = 'block';
            return false;
        }
        function OpenMovieFile(URLStr) {
            document.getElementById("FrmPopup2").src = URLStr;
            var modalPopupBehavior = $find('programmaticModalPopupBehavior2');
            modalPopupBehavior.show();
            return false;
        }
        function RefreshRecentlyVideo() {

            document.getElementById("<%= btnRefreshRecentVideo.ClientID %>").click();
            //__doPostBack("UPRecentVideo", "");
            //__doPostBack("UP_Video", "");
        }
        function CloseVideoDetailMore() {
            document.getElementById('divVideoDetailMore').style.display = "none";
            document.getElementById('divListGridAction').style.display = "block";
            document.getElementById('divListGridView').style.display = "block";
        }
    </script>

</asp:Content>