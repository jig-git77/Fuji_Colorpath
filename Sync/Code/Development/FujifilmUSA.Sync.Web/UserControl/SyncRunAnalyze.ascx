<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SyncRunAnalyze.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.SyncRunAnalyze" %>
<div class="contant_top">
    &nbsp;</div>
<div class="contant_middle">
    <div class="head">
        <h1>
            Analyze</h1>
        <div class="Information_divAnalyze">
            <asp:LinkButton ID="btnOptimize" runat="server"  CssClass="button" 
                onclick="btnOptimize_Click">
                <span>
                    <asp:Image ID="Image2" runat="server" ImageUrl="../Images/export.png" />
                   Optimize Profile</span> &nbsp; 
            </asp:LinkButton>
            <asp:LinkButton ID="btnExport" runat="server" OnClick="btnExport_Click" CssClass="button">
                <span>
                    <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/export.png" />
                    Export</span>
            </asp:LinkButton>            
            <asp:LinkButton ID="btnCertify" runat="server" CssClass="button" OnClick="btnCertify_Click">
                <span>
                    <asp:Image ID="Image1" runat="server" ImageUrl="../Images/export.png" />
                    Certify</span>
            </asp:LinkButton> &nbsp;
        
        </div>
    </div>
    <div class="clear">
    </div>
    <div class="ContainerClass">
        <div class="color_pallate">
            <div class="color_pallate_head">
            </div>
            <div class="color_pallate_middle">
                <div class="color_bar">
                    <div id="Canvas" style="position: relative; height: 130px; width: 630px; overflow: visible;">
                        Measurement Selection
                    </div>
                </div>
            </div>
            <div class="color_pallate_bottom">
                &nbsp;</div>
        </div>
        <div class="color_pallate">
            <div class="color_pallate_head">
            </div>
            <div class="color_pallate_middle">
                <div class="color_bar">
                    <div id="Div1" style="position: relative; height: 460px; width: 630px; overflow: visible;">
                        Chart for Analyze
                    </div>
                </div>
            </div>
            <div class="color_pallate_bottom">
                &nbsp;</div>
        </div>
    </div>
</div>
</div>
<div class="contant_bottom">
    &nbsp;</div>
