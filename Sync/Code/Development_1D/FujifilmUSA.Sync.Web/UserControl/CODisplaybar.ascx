<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CODisplaybar.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.CODisplaybar" %>
<div class="contant_top">
    &nbsp;</div>
<div class="contant_middle">
    <div class="head">
        <h1><asp:Label ID="lblHeader" runat="server"></asp:Label></h1>       
    </div>
    <div class="clear"></div>
    <div >       
        <center>

            <div style="height:200px;">
                <div style="font-size: 14px;"><br /><br /><br />
                    <asp:Label ID="lblMessage" runat="server" style="font-size: 18px; color: red;"></asp:Label>
                    <span style="font-size: 18px; color: red;"></span>
                    <br /><br />                    
            </div>
          </div>
       </center>
      
    </div>
</div>
<div class="contant_bottom">
    &nbsp;</div>