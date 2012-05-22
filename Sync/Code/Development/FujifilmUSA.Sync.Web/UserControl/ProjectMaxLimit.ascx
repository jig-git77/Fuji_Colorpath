<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectMaxLimit.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.ProjectMaxLimit" %>


<div class="contant_top">
    &nbsp;</div>
<div class="contant_middle">
    <div class="head">
        <h1>
            Project Limit
            </h1>
       
    </div>
 <div class="clear">
    </div>

    <div >       
        <center>

            <div style="height:200px;">
                <div style="font-size: 14px;"><br /><br /><br />
                    <span style="font-size: 18px; color: red;">Opps!!! You reach your maximum limit of projects, so you can't create more projects.</span>
                    <br /><br />
                    Please click here to go back to
                    <asp:LinkButton ID="lnkProjectList" runat="server" 
                        onclick="lnkProjectList_Click"><span style="font-weight:bold;">Project List page.</span></asp:LinkButton>
            </div>
          </div>
       </center>
      
    </div>
</div>
<div class="contant_bottom">
    &nbsp;</div>
