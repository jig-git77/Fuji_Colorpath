<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProgressBar.ascx.cs" Inherits="Common_ProgressBar" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:UpdateProgress runat="server" DynamicLayout="true" ID="UP_Home">
    <ProgressTemplate>
        <div id="DIV_Progress" runat="server" class="PROGRESS" align="center"><img src="Images/Loading2.gif" style="background: transparent;" alt="Loading" /><br /><b>Loading</b></div>
            <cc1:AlwaysVisibleControlExtender ID="AVCE_Home" runat="server" TargetControlID="DIV_Progress"
                VerticalSide="middle" VerticalOffset="10" HorizontalSide="Center" HorizontalOffset="10"
                ScrollEffectDuration=".1">
            </cc1:AlwaysVisibleControlExtender>
    </ProgressTemplate>
</asp:UpdateProgress>
<script type="text/javascript" language="javascript"> 
            
           var imagenames = new Array('Images/Loading2.gif'); 
           var imagesdone=false; 
           var loaded=0, preloader;
           
           //var txt ='<div id="preloader" style="position:relative; font-size:12px; line-height:25px; vertical-align:middle; width:100%; text-align:center; font-family:Arial;"><IMG SRC="Images/loading.gif" /></p>'; 
           var txt = '<div id="preloader" class="PROGRESS" align="center"><img src="Images/Loading2.gif" style="background: transparent;" alt="Loading" /><br /><b>Loading</b></div>';
           document.write(txt); 
            
             function loadimages()
             { 
                 preloader=document.getElementById('preloader'); 
                 for(n=0; n < imagenames.length; n++)
                 { 
                     if (document.images) 
                     {
                            //
                     }
                           
                     setTimeout('checkload('+ n +')' ,n*100); 
                     dispbars();
                 } 
             } 


            function dispbars()
            {   
                loaded++; 
                if(loaded >= imagenames.length) setTimeout('hidepreloader()',800); 
            } 


            function checkload(index){ 
                setTimeout('checkload('+index+')', 100); 
            } 


            function hidepreloader()
            { 
                preloader.style.visibility="hidden"; 
                imagesdone=true; 
            } 
            loadimages(); 
            dispbars();           
     </script>