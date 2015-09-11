<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PUUP_BOHDDescSolution.aspx.vb" Inherits="Popup_BOPUUP_HDDescSolution" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
        <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <base target="_self"/>
     <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table align="left" height="780" width="860">
            <tr>
                <td valign="top" style="width: 100%; height: 100%">
                    <table align="left" style="width: 100%; height: 100%">
                        <tr>
                            <td align="right" style="width: 860">
                                <a class="LinkButtons" href="#" onclick="window.close();"><strong><span style="font-size: 8pt;
                                    color: #0a365c; font-family: Tahoma">Close</span></strong></a> &nbsp;&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="center" class="heading" valign="top" style="width: 100%">
                                <asp:Label ID="lblHeader" runat="server"></asp:Label></td>
                        </tr>                        
                        <tr>
                            <td style="width:100%;height:100%" valign="top"  class="redborder"   >
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                    <tr>
                                        <td style="width: 100%; height: 2%" align="center"  >
                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%; height: 98%" valign="top"  >
                                            <asp:TextBox ID="txtDescSolution" runat="server"  TextMode="MultiLine" CssClass="textbox" Wrap="true" ReadOnly="True"
                                                Width="99%" Height="752px"></asp:TextBox></td>
                                    </tr>
                                </table>
                                </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
