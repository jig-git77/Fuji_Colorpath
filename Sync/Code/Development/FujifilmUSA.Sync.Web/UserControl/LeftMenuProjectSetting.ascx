<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeftMenuProjectSetting.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.LeftMenuProjectSetting" %>
<ul id="menu2">
    <li>
        <ul class="submenu">
            <li>
                <asp:LinkButton runat="server" ID="lnkDescription" Text="Description" OnClick="lnkItem_Click"></asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lnkTargetColorSpace" Text="Target ColorSpace"
                    OnClick="lnkItem_Click"></asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lnkProfilerOption" Text="Profiler Option" OnClick="lnkItem_Click"></asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lnkMeasurementSetup" Text="Measurement Setup"
                    OnClick="lnkItem_Click"></asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lnkSave" Text="Save" OnClick="lnkItem_Click"></asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lnkMeasure" Text="Measure" OnClick="lnkItem_Click"></asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lnkAnalyze" Text="Analyze" OnClick="lnkItem_Click"></asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lnkDownload" Text="Download" OnClick="lnkItem_Click"></asp:LinkButton>
            </li>
        </ul>
    </li>
</ul>
