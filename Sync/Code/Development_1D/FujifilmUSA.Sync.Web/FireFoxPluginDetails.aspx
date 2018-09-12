<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FireFoxPluginDetails.aspx.cs" Inherits="FujifilmUSA.Sync.Web.FireFoxPluginDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FireFox Measurement Plugin Details</title>
    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="CSS/FirefoxPluginStyle.css"/>

</head>
<body>
    <form id="form1" runat="server">

   <div class="col-xs-12" style="background-image: url(images/header_bg1.png)">
		<img src="Images/ColorPath_Avatar_small.png" alt=""/>
	</div>
		<div class="row" style="padding-left:20px; padding-right:20px">
			<div class="col-sm-12" style="max-width:1200px">
				<h1>Oops!</h1>

				<h2>Let's see how we can help.</h2>
				<br/>
				
				<p>If you're seeing this message, it's likely because Firefox just auto-updated to a version that removed support for plugins like our measurement plugin and others like it.<br>
				In order to continue, please choose from one of the options below:</p>
				
				<div class="row">
					<div class="col-md-41">
						<h3>1. Firefox ESR <small>(Extended Support Release) 52</small></h3>
						
						<p>To continue using Firefox, you’ll have to download and install the ESR version 52 and make sure to disable automatic updating.</p>
						<a class="btn btn-default" href= "Terms/ColorPath SYNC Firefox ESR.pdf" target="_blank" role="button">View instructions for installing the Firefox ESR</a>
						
						<br/><br/>
						
                        <a class="btn btn-success" href= "Download/Firefox Setup 52.9.0esr.zip" target="_blank" role="button" style="margin-top:5px">Download Firefox ESR - English</a>
						<a class="btn btn-success" href= "Download/Firefox Setup 52.9.0esr de.zip" target="_blank" role="button" style="margin-top:5px">Download Firefox ESR - German</a>
                        <a class="btn btn-success" href= "Download/Firefox Setup 52.9.0esr ja.zip" target="_blank" role="button" style="margin-top:5px">Download Firefox ESR - Japanese</a>
						
					</div>
					
					<div class="col-md-4">
						<h3>2. Safari <small>(MacOS only)</small></h3>
					    <p>Safari still supports the Fujifilm Color Management plugin. On a Mac, just log in to ColorPath Sync.</p>
					</div>
					
					<div class="col-md-4">
						<h3>3. Internet Explorer <small>(Windows&nbsp;only)</small></h3>
					    <p>Internet Explorer still supports the Fujifilm Color Management plugin. On Windows, just log in to ColorPath Sync.</p>
					</div>
				</div>
                <br />
              
			</div>
        </div>
        <div class="row" style="padding-left:20px; padding-right:20px">
			<div class="col-sm-12" style="max-width:1200px">
                <hr />
                <div>
				    <p>If you just installed the Firefox ESR but you don't have the ColorPath plugin installed, please return to <a href="http://colorpathsync.com">ColorPath Sync</a> and click "cancel" on the popup to log in.</p>
				    <p>If you still need help, call us at 1(800) FLY-FUJI</p>
                </div>
		    </div>
		</div>
		<br/>
		<div class="row">
			<div class="col-md-12"><center><img src="images/FUJIFILM_Slogan_270_100.png" class="img-responsive" /></center></div>
		</div>


    </form>
</body>
</html>
