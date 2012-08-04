<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="_15Minutes.aspx.cs" Inherits="ProblemSpecs._15Minutes" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="style/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript"> 
        <!-- 
            function clearText(field) { 
                if (field.value == "<Need more data>")
                {
                    field.value = "";
                }
            }           
        // -->         
        </script>  
  
    <style type="text/css">
        .style1
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            border: 1px solid #000000;
            padding: 5px;
            border-collapse: inherit;
            height: 28px;
            width: 273px;
        }
        .style3
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            border: 1px solid #000000;
            padding: 5px;
            border-collapse: inherit;
            height: 50px;
            width: 273px;
        }
        .style4
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            border: 1px solid #000000;
            padding: 5px;
            border-collapse: inherit;
            height: 8px;
            width: 273px;
        }
        .style5
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            border: 1px solid #000000;
            padding: 5px;
            border-collapse: inherit;
            height: 28px;
            width: 272px;
        }
        .style6
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            border: 1px solid #000000;
            padding: 5px;
            border-collapse: inherit;
            height: 42px;
            width: 272px;
        }
        .style7
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            border: 1px solid #000000;
            padding: 5px;
            border-collapse: inherit;
            height: 50px;
            width: 272px;
        }
        .style8
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            border: 1px solid #000000;
            padding: 5px;
            border-collapse: inherit;
            width: 277px;
        }
        .style9
        {
            width: 277px;
        }
    </style>
  
</head>
<body>
    <form id="form1" runat="server">
    <div><table width="750px" class="notestable" style="background-color:#E0E0E0">
  
     <tr>
            <td style="width: 737px">    
                <br />
                <table cellpadding="5" cellspacing="1" width="700px" class="notestable" style="margin-left: auto; margin-right: auto;" >
                    <tr>
                        <td align="right" class="notesTD" style="width: 116px; height: 28px; " valign="top">
                            <asp:Label ID="lblOrderID0" runat="server" CssClass="callabel" Text="Service Call"></asp:Label>
                        </td>
                        <td class="notesTD" style="font-family: Verdana, Arial, Helvetica, sans-serif;
                            height: 28px">
                            <asp:Label ID="lblOrderID" runat="server" CssClass="callabel"></asp:Label></td>
                    </tr>
      <tr>
        <td style="width: 116px; height: 28px" class="notesTD" align="right" valign="top">&nbsp;<asp:Label ID="lblProbStatement" runat="server" CssClass="font_tahomabold" Text="Problem Statement" Width="150px"></asp:Label></td>
        <td style="height: 28px; font-family: Verdana, Arial, Helvetica, sans-serif;" 
              class="notesTD">
            <asp:TextBox ID="txtProblemStatement" runat="server" Width="500px" 
                CssClass="textbox" MaxLength="200" Height="39px" TextMode="MultiLine"></asp:TextBox><span class="errorlabel">*</span></td>
      </tr>
      <tr>
        <td style="width: 116px; height: 20px" class="notesTD" align="right" valign="top">&nbsp;</td>
        <td style="height: 20px; font-family: Verdana, Arial, Helvetica, sans-serif;" 
              class="notesTD">
            <span class="errorlabel">Fields marked with * are needed to be filled out. Others are optional but desired.</span></td>
      </tr>
    </table><br />
            </td>
            <td>
            </td>
        </tr>
    <tr>
      <td style="width: 737px"><fieldset style="border:1px solid #505050 ">
    <legend style="font-size:12pt;font-family: Verdana;margin-left:25px;font-weight: bolder;">What:</legend> <br />
    <table cellpadding="5" cellspacing="1" class="notestable" 
              style="margin-left: auto; margin-right: auto; width: 720px;" >
      <tr>
        <td style="width: 150px; height: 28px" class="notesTD">&nbsp;</td>
        <td style="font-family: Verdana, Arial, Helvetica, sans-serif; " class="notesTD">IS</td>
        <td style="font-family: Verdana, Arial, Helvetica, sans-serif; " class="notesTD">IS NOT</td>
      </tr>
      <tr>
        <td align="right" style="width: 150px; height: 42px" valign="top" class="notesTD"><asp:Label ID="lblObject" runat="server" Text="Object" CssClass="font_verdana"></asp:Label></td>
        <td align="left" valign="top" class="notesTD" ><asp:TextBox ID="txtObject" runat="server" Width="260px" CssClass="textbox" MaxLength="49"></asp:TextBox><span class="errorlabel">*</br></span></td>
        <td valign="top" class="notesTD">
            <asp:TextBox ID="txtIsNotObject" runat="server" TextMode="MultiLine" 
                CssClass="textbox" Height="40px" Width="250px" MaxLength="500">&lt;Need more data&gt;</asp:TextBox><span class="errorlabel">*</span></td>
      </tr>
      <tr>
        <td style="width: 150px; height: 50px;" align="right" valign="top" class="notesTD"><asp:Label ID="lblDeviation" runat="server" CssClass="font_verdana" Text="Deviation"></asp:Label></td>
        <td valign="top" class="notesTD" >
            <asp:TextBox ID="txtProblemDesc" runat="server" TextMode="MultiLine" 
                Width="260px" CssClass="textbox" Height="40px" MaxLength="254"></asp:TextBox><span class="errorlabel">*</span></td>
        <td valign="top" class="notesTD" >
            <asp:TextBox ID="txtIsNotProblemDesc" runat="server" TextMode="MultiLine" 
                Width="250px" CssClass="textbox" Height="40px" MaxLength="500">&lt;Need more data&gt;</asp:TextBox></td>
      </tr>
    </table>
          <br />
     </fieldset>
     </td>
        <td>
        </td>
    </tr>
     <tr>
      <td style="width: 737px"><fieldset style="border:1px solid #505050">
    <legend style="font-size:12pt;font-family: Verdana;margin-left:25px;font-weight: bolder;">Where:</legend> <br />
    <table cellpadding="5" cellspacing="1" width="720px" class="notestable" 
              style="margin-left: auto; margin-right: auto;" >
      <tr>
        <td style="width: 150px; height: 28px" class="notesTD">&nbsp;</td>
        <td style="font-family: Verdana, Arial, Helvetica, sans-serif; "  class="style8">IS</td>
        <td style="height: 28px; font-family: Verdana, Arial, Helvetica, sans-serif; width:275px" class="notesTD">IS NOT</td>
      </tr>
      <tr>
        <td align="right" style="width: 150px; height: 42px" valign="top" class="notesTD"><asp:Label ID="Label1" runat="server" Text="Place/Location" CssClass="font_verdana"></asp:Label></td>
        <td align="left" valign="top" class="style9" >
            <asp:TextBox ID="txtLocation" runat="server" 
                Width="260px" CssClass="textbox" TextMode="MultiLine" MaxLength="500" 
                Height="40px">&lt;Need more data&gt;</asp:TextBox><span class="errorlabel">*</span></td>
        <td style="height: 42px; width: 143px;" valign="top" class="notesTD">
            <asp:TextBox ID="txtIsNotLocation" runat="server" TextMode="MultiLine" 
                CssClass="textbox" Height="40px" Width="250px" MaxLength="500">&lt;Need more data&gt;</asp:TextBox><span class="errorlabel">*</span></td>
      </tr>
      <tr>
        <td style="width: 150px; height: 50px;" align="right" valign="top" class="notesTD"><asp:Label ID="Label3" runat="server" CssClass="font_verdana" Text="On the object"></asp:Label></td>
        <td valign="top" class="style8" >
            <asp:TextBox ID="txtOntheObject" runat="server" TextMode="MultiLine" 
                Width="260px" CssClass="textbox" Height="40px" MaxLength="500">&lt;Need more data&gt;</asp:TextBox><span class="errorlabel">*</span></td>
        <td valign="top" style="height: 50px"  class="notesTD"></td>
      </tr>
    </table>
          <br />
     </fieldset>
     </td>
         <td>
         </td>
    </tr>
    <tr>
      <td style="width: 737px"><fieldset style="border:1px solid #505050">
    <legend style="font-size:12pt;font-family: Verdana;margin-left:25px;font-weight: bolder;">When:</legend> <br />
    <table cellpadding="5" cellspacing="1" width="720px" class="notestable" 
              style="margin-left: auto; margin-right: auto;" >
      <tr>
        <td style="width: 150px; height: 28px" class="notesTD">&nbsp;</td>
        <td style="font-family: Verdana, Arial, Helvetica, sans-serif; " class="style5">IS</td>
        <td style="height: 28px; font-family: Verdana, Arial, Helvetica, sans-serif; width:275px" class="notesTD">IS NOT</td>
      </tr>
      <tr>
        <td align="right" style="width: 150px; height: 42px" valign="top" class="notesTD"><asp:Label ID="Label2" runat="server" Text="First" CssClass="font_verdana"></asp:Label></td>
        <td align="left" valign="top" class="style6">
            <asp:TextBox ID="txtFirst" runat="server" Width="260px" CssClass="textbox"></asp:TextBox><span class="errorlabel">*</span></td>
        <td style="height: 42px;" valign="top" class="notesTD">
            <asp:TextBox ID="txtIsNotFirst" runat="server" TextMode="MultiLine" 
                CssClass="textbox" Height="40px" Width="250px" MaxLength="500">&lt;Need more data&gt;</asp:TextBox><span class="errorlabel">*</span></td>
      </tr>
        <tr>
            <td align="right" class="notesTD" style="width: 150px; height: 42px" valign="top">
                <asp:Label ID="Label9" runat="server"  CssClass="font_verdana" Text="Since (Any Pattern?)</br>e.g. Continuous,Periodic,Sporadic"></asp:Label></td>
            <td align="left" class="style6" valign="top">
                <asp:TextBox ID="txtSince" runat="server" CssClass="textbox" Height="40px" TextMode="MultiLine"
                    Width="260px" MaxLength="500">&lt;Need more data&gt;</asp:TextBox><span class="errorlabel">*</span></td>
            <td class="notesTD" style="width: 143px; height: 42px" valign="top">
            </td>
        </tr>
      <tr>
        <td style="width: 150px; height: 50px;" align="right" valign="top" class="notesTD"><asp:Label ID="Label4" runat="server" CssClass="font_verdana" Text="In the process/cycle?"></asp:Label></td>
        <td valign="top" class="style7" >
            <asp:TextBox ID="txtInHistory" runat="server" TextMode="MultiLine" 
                Width="260px" CssClass="textbox" Height="40px" MaxLength="500">&lt;Need more data&gt;</asp:TextBox><span class="errorlabel">*</span></td>
        <td valign="top" style="height: 50px" class="notesTD" >
            <asp:TextBox ID="txtLifeCycleIsNot" runat="server" TextMode="MultiLine" 
                CssClass="textbox" Height="40px" Width="250px" MaxLength="500">&lt;Need more data&gt;</asp:TextBox></td>
      </tr>
    </table>
          <br />
     </fieldset>
     </td>
        <td>
        </td>
    </tr>
     <tr>
      <td style="width: 750px"><fieldset style="border:1px solid #505050">
    <legend style="font-size:12pt;font-family: Verdana;margin-left:25px;font-weight: bolder;">Extent:</legend> <br />
    <table cellpadding="5" cellspacing="1" width="720px" class="notestable" 
              style="margin-left: auto; margin-right: auto;" >
      <tr>
        <td style="width: 150px; height: 28px" class="notesTD">&nbsp;</td>
        <td style="font-family: Verdana, Arial, Helvetica, sans-serif; " class="style1">IS</td>
        <td style="font-family: Verdana, Arial, Helvetica, sans-serif; " class="notesTD" >IS NOT</td>
      </tr>
      <tr>
        <td align="right" style="width: 150px; height: 42px" valign="top" class="notesTD"><asp:Label ID="Label5" runat="server" Text="How many objects?" CssClass="font_verdana"></asp:Label></td>
        <td align="left" valign="top" class="notesTD">
            <asp:TextBox ID="txtNumOfObjects" runat="server" CssClass="textbox" 
                Height="40px" TextMode="MultiLine"
                Width="260px" MaxLength="50">&lt;Need more data&gt;</asp:TextBox><span class="errorlabel">*</span></td>
        <td valign="top" class="notesTD" ><asp:TextBox ID="txtIsNotNumOFObjects" 
                runat="server" TextMode="MultiLine" CssClass="textbox" Height="40px" 
                Width="250px" MaxLength="500">&lt;Need more data&gt;</asp:TextBox><span class="errorlabel">*</span></td>
      </tr>
      <tr>
        <td style="width: 150px; height: 50px;" align="right" valign="top" class="notesTD"><asp:Label ID="Label6" runat="server" CssClass="font_verdana" Text="Size of Deviation"></asp:Label></td>
        <td valign="top" class="style3" ><asp:TextBox ID="txtDeviationSize" runat="server" 
                TextMode="MultiLine" Width="260px" CssClass="textbox" Height="40px" 
                MaxLength="500">&lt;Need more data&gt;</asp:TextBox><span class="errorlabel">*</span></td>
        <td valign="top" class="notesTD" >&nbsp;</td>
      </tr>
        <tr>
            <td align="right" class="notesTD" style="width: 150px; height: 50px" valign="top">
                <asp:Label ID="Label8" runat="server" CssClass="font_verdana" Text="How many deviations?"></asp:Label></td>
            <td class="style3" valign="top">
                <asp:TextBox ID="txtHowManyDeviations" runat="server" CssClass="textbox" Width="260px" MaxLength="100">&lt;Need more data&gt;</asp:TextBox><span class="errorlabel">*</span></td>
            <td valign="top" class="notesTD">
            </td>
        </tr>
        <tr>
            <td align="right" class="notesTD" style="width: 150px; height: 8px" valign="top">
                <asp:Label ID="Label7" runat="server" CssClass="font_verdana" Text="Trend"></asp:Label></td>
            <td class="style4" valign="top">
                <asp:DropDownList ID="ddlTrend" runat="server" CssClass="dropDownStyle">
                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                    <asp:ListItem Value="1">Increasing</asp:ListItem>
                    <asp:ListItem Value="2">Decreasing</asp:ListItem>
                    <asp:ListItem Value="3">Stable</asp:ListItem>
                </asp:DropDownList><span class="errorlabel">*</span></td>
            <td valign="top" class="notesTD">&nbsp;</td>
        </tr>
    </table>
    <br /> </fieldset>
          
     </td>
         <td>
         </td>
    </tr>
        <tr>
            <td >    
                <br />
                <table cellpadding="5" cellspacing="1" width="700px" class="notestable" style="margin-left: auto; margin-right: auto;" >
      <tr>
        <td style="width: 116px; height: 28px" class="notesTD" align="right" valign="top">&nbsp;<asp:Label ID="Label10" runat="server" CssClass="font_verdana" Text="TRC Notes"></asp:Label></td>
        <td style="height: 28px; font-family: Verdana, Arial, Helvetica, sans-serif;" class="notesTD" colspan="2">
                <asp:TextBox ID="txtTRCNotes" runat="server" CssClass="textbox" Height="114px" TextMode="MultiLine"
                    Width="512px" MaxLength="7999">&lt;Need more data&gt;</asp:TextBox><span class="errorlabel">*</span></td>
      </tr>
                <tr>
                    <td align="right" class="notesTD" style="width: 116px; height: 28px" valign="top">
                    </td>
                    <td class="notesTD" colspan="2" style="font-family: Verdana, Arial, Helvetica, sans-serif;
                        height: 28px">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="Button" OnClick="btnSubmit_Click" />&nbsp;
                        <asp:Label ID="lblErrorMessage" runat="server" Font-Bold="True" 
                            Font-Size="Smaller" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
    </table>
            </td>
            <td>
            </td>
        </tr>
            
    
    
    </table>
     </div> 
    </form>

</body>
</html>

