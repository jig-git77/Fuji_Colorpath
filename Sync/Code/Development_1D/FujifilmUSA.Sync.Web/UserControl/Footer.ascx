<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Footer.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.Footer" %>
<footer>
    <div id="footer">
        <table style="width: 100%;">
            <tr style="height: 80px; vertical-align: central;">
                <td align="center">
                    <img alt="" src="Images/logo_Fujifilm.png" />
                    <br />
                    <br />
                    <b>FUJIFILM Graphic Communications Division </b>
                </td>

            </tr>

            <tr>
                <td style="display: flex; justify-content: space-around; align-items: center; border-top-style: solid; border-width: 1px; border-color: grey;">
                    <a href="https://www.fujifilm.com/us/en/business/graphics-printing/workflow-solutions/xmf-workflow" target="_blank">
                        <img alt="" src="Images/xmf.png" width="210" height="70" />
                    </a>
                    <a href="https://print-us.fujifilm.com/products/toner-digital-presses/" target="_blank">
                        <img alt="" src="Images/revoria.png" width="210" height="70" /></a>
                    <a href="https://print-us.fujifilm.com/products/wide-format-printers/" target="_blank">
                        <img alt="" src="Images/acuity.png" width="210" height="70" />
                    </a>
                    <a href="https://print-us.fujifilm.com/products/jpress-750s/" target="_blank">
                        <img alt="" src="Images/jpress.png" width="210" height="70" />
                    </a>

                </td>

            </tr>

            <tr>
                <td style="display: flex; justify-content: space-around; align-items: center; border-bottom-style: solid; border-width: 1px; border-color: grey;">
                    <a href="https://print-us.fujifilm.com/products/inkjet-printbar-systems/" target="_blank">
                        <img alt="" src="Images/dimatix.png" width="210" height="70" />
                    </a>
                    <a href="https://print-us.fujifilm.com/products/superia-zx-plate/" target="_blank">
                        <img alt="" src="Images/superia.png" width="210" height="70" />
                    </a>
                    <a href="https://print-us.fujifilm.com/products/flenex-water-wash-plates/" target="_blank">
                        <img alt="" src="Images/flenex.png" width="210" height="70" />
                    </a>

                </td>

            </tr>
            <tr>
                <td align="center">
                    <br />
                    <br />

                    <b>© <%= DateTime.Now.Year %> FUJIFILM North America Corporation - Graphics Communications Division | 
                        <a href="Terms/ColorPath Sync TC 2013.pdf" title="Terms of Use" target="_blank">Terms and Conditions</a>
                    </b>
                </td>
            </tr>
        </table>

    </div>
</footer>

