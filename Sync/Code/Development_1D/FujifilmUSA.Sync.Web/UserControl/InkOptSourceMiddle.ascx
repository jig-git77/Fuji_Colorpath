<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InkOptSourceMiddle.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.InkOptSourceMiddle" %>

<asp:LinkButton ID="lbtnDownloadAndSaveDLP" runat="server" CssClass="button" OnClick="lbtnDownloadAndSaveDLP_Click"
                        OnClientClick="return ChooseRadWindow();">
                        <span>
                            <asp:Image ID="Image2" runat="server" ImageUrl="../Images/export.png" meta:resourcekey="Image2Resource1" />
                            <asp:Localize runat="server" Text="Create DLP" ID="locCreateDLP" meta:resourcekey="locCreateDLPResource1"></asp:Localize>
                        </span>
                    </asp:LinkButton>