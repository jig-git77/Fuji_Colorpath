<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="PlayVideoFile.aspx.cs" Inherits="OrisWeb.PlayVideoFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>FUJIFILM Graphic Systems U.S.A., Inc.</title>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <meta name="keywords" content="FUJIFILM Graphic Systems U.S.A., Inc." />
    <meta name="description" content="FUJIFILM Graphic Systems U.S.A., Inc." />
    <link href="CSS/Default.css" rel="stylesheet" type="text/css" />

    <script type='text/javascript' src='JS/swfobject.js'></script>

</head>
<body style="background-color:transparent;">
    <form id="form1" runat="server" style="background:transparent;">
    <div style="background:none; width:655px; height:505px; z-index:1000; border:0px solid #006699;">
        <div style="float:left; width:650px;"><div style="background:url(images/curvebox-tl.png) no-repeat left top; float:left; width:600px; height:49px;"></div><div style="background:url(images/curvebox-tr.png) no-repeat left top; float:right; width:50px; height:34px; padding-top:15px;"><a href="#" onclick="return ClosePopup();"><img src="images/close-img.png" alt="" border="0" /></a></div></div>
        <div style="background:url(images/curvebox-ml.png) repeat-y left top; width:653px; float:left; clear:both;">
        <div style="background:url(images/curvebox-mr.png) repeat-y right top; width:639px; float:right; clear:both; text-align:center;">
        <div style="background:#fff; width:625px; float:left;">
        <table cellpadding="0" cellspacing="0" border="0" align="center" width="100%">
            <tr>
                <td valign="top" align="center">
                    <div id="playmoviespace">
                    </div>
                </td>
            </tr>
            <tr>
                <td align="center" style="padding-top: 5px">
                    <!--<a href="#" onclick="return ClosePopup();">Close Video</a>-->
                </td>
            </tr>
        </table>
        </div>
        </div>
        </div>
        <div style="float:left; width:650px;"><div style="background:url(images/curvebox-bl.png) no-repeat left top; float:left; width:630px; height:49px;"></div><div style="background:url(images/curvebox-br.png) no-repeat left top; float:right; width:20px; height:49px;">&nbsp;</div></div>
    </div>

    <script language="javascript" type="text/javascript">
        function ClosePopup()
        {
            var i = '<%= IsUser %>';
            if(i == 0)
            {
                var modalPopupBehavior = parent.$find('programmaticModalPopupBehavior');
                modalPopupBehavior.hide();
            }else{
                var modalPopupBehavior = parent.$find('programmaticModalPopupBehavior2');
                modalPopupBehavior.hide();
          
                parent.RefreshRecentlyVideo(); 
            }
        }
    </script>

    <script type='text/javascript'>
	var s0 = new SWFObject('player.swf','ply','600','500','9');
	s0.addParam('allowfullscreen','true');
	s0.addParam('allowscriptaccess','always');
	s0.addParam('wmode','opaque');
	s0.addParam('quality','high');
	s0.addParam('LOOP','false');
	s0.addParam('flashvars','file=<%= VideoFilePath %>&autostart=true');
	s0.write('playmoviespace');
    </script>

    </form>
</body>
</html>

