<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ValidationContainer.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.ValidationContainer" %>
<div class="tooltip_round_box" style="width: '<%=Size %>'px;">
    <div class="tooltip_arrow" style="margin-top: -3px;">
        &nbsp;</div>
    <div class="tooltip_top_left">
    </div>
    <div class="tooltip_top_right">
    </div>
    <div class="tooltip_inside">
        <%= ValidationMsg %>
    </div>
    <div class="tooltip_bottom_left">
    </div>
    <div class="tooltip_bottom_right">
    </div>
</div>
