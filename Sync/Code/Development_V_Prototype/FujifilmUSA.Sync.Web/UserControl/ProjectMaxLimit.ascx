<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectMaxLimit.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.ProjectMaxLimit" %>


<div class="contant_top">
    &nbsp;</div>
<div class="contant_middle">
    <div class="head">
        <h1><asp:Localize Text="Project Limit" runat="server" ID="locProjectLimit" 
                meta:resourcekey="locProjectLimitResource1"></asp:Localize>
            </h1>
       
    </div>
 <div class="clear">
    </div>

    <div >       
        <center>

            <div style="height:200px;">
                <div style="font-size: 14px;"><br /><br /><br />
                    <span style="font-size: 18px; color: red;">
                    <asp:Localize Text="Error: You reach your maximum limit of projects, so you can't create more projects." 
                        runat="server" ID="locMessage" meta:resourcekey="locMessageResource1"></asp:Localize></span>
                    <br /><br />
                    <asp:Localize Text="Please click here to go back to" runat="server" 
                        ID="locGoBackMessage" meta:resourcekey="locGoBackMessageResource1"></asp:Localize> 
                    <asp:LinkButton ID="lnkProjectList" runat="server" 
                        onclick="lnkProjectList_Click"><span style="font-weight:bold;"><asp:Localize 
                        Text="Project List page." runat="server" ID="locProjectListPage" 
                        meta:resourcekey="locProjectListPageResource1"></asp:Localize>
</span></asp:LinkButton>
            </div>
          </div>
       </center>
      
    </div>
</div>
<div class="contant_bottom">
    &nbsp;</div>
