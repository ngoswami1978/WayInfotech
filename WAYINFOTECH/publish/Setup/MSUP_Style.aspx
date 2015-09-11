<%@ page language="VB" autoeventwireup="false" inherits="Setup_MSUP_Style, App_Web_msup_style.aspx.4cd3357d" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Add/Modify Style</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/WAY.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">     
    function AirlineOfficeReset()
    {
        document.getElementById("lblError").innerHTML="";
        document.getElementById("txtBarCode").value="";
        document.getElementById("txtStyleName").value="";
        document.getElementById("txtDesignName").value="";
        document.getElementById("txtShadeNo").value="";
        document.getElementById("txtMRP").value="";
        return true;
    }
    
   function  NewFunction()
   {    
       window.location="MSUP_Style.aspx?Action=I";
       return false;
   }

     function CheckMendatoty()
    {
        if (document.getElementById("txtBarCode").value=="")
        {          
                document.getElementById("lblError").innerText="Please enter the BarCode "
                document.getElementById("txtBarCode").focus();
                return false;
        }
        
        if (document.getElementById("txtStyleName").value=="")
        {           
                document.getElementById("lblError").innerText="Please enter the Style "
                document.getElementById("txtStyleName").focus();
                return false;
        }        
        if (document.getElementById("txtDesignName").value=="")
        {           
                document.getElementById("lblError").innerText="Please enter the Style "
                document.getElementById("txtDesignName").focus();
                return false;
        }
    }    
    </script>

</head>
<body>
    <form id="frmAirLineOfficeUpdate" runat="server">
        <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Setup-></span><span class="sub_menu">Style</span>
                            </td>
                        </tr>
                        <tr>
                        <tr>
                            <td class="heading" align="center" valign="top" style="height: 10px">
                                Manage Style
                            </td>
                        </tr>
                        <tr>
                            <td width="100%" valign="top" style="height: 208px">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td width="100%" class="redborder">
                                            <table align="left" border="0" cellpadding="2" cellspacing="0" width="100%">
                                                <tr height="20px">
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 150px; width: 100%" colspan="4" valign="top">
                                                        <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td class="textbold" colspan="6" align="center" valign="TOP" style="height: 7px">
                                                                    <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 6px" width="6%">
                                                                </td>
                                                                <td class="textbold" style="width: 441px; height: 6px">
                                                                </td>
                                                                <td style="height: 6px" width="15%">
                                                                </td>
                                                                <td style="width: 312px; height: 6px">
                                                                </td>
                                                                <td style="width: 280px; height: 6px">
                                                                </td>
                                                                <td rowspan="1" style="height: 6px" valign="top" width="18%">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="6%" class="textbold" style="height: 2px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="height: 2px; width: 441px;">
                                                                </td>
                                                                <td width="15%" style="height: 2px">
                                                                    BarCode<span class="Mandatory">*</span></td>
                                                                <td style="height: 2px; width: 312px;">
                                                                    <span class="textbold"><span style="font-size: 9pt; color: #000000; font-family: Tahoma">
                                                                        <asp:TextBox ID="txtBarCode" runat="server" CssClass="textfield" MaxLength="6"
                                                                            TabIndex="1" Width="280px"></asp:TextBox></span></span></td>
                                                                <td style="height: 2px; width: 280px;">
                                                                </td>
                                                                <!--following td is for displaying all buttons on this page -->
                                                                <td width="18%" style="height: 22px" rowspan="4" valign="top">
                                                                    <table>
                                                                        <tr>
                                                                            <td style="width: 78px">
                                                                                <asp:Button ID="btnSave" runat="server" CssClass="button" TabIndex="2" Text="Save"
                                                                                    AccessKey="A" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" style="width: 78px">
                                                                                <asp:Button ID="btnNew" runat="server" CssClass="button" Text="New" TabIndex="2"
                                                                                    AccessKey="N" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" style="width: 78px">
                                                                                <asp:Button ID="btnReset" runat="server" CssClass="button" Text="Reset" TabIndex="2"
                                                                                    AccessKey="R" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <%--<tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>--%>
                                                            <tr>
                                                                <td class="textbold" style="height: 2px" width="6%">
                                                                </td>
                                                                <td class="textbold" style="width: 441px; height: 2px">
                                                                </td>
                                                                <td style="height: 2px" width="15%">
                                                                    Quality<strong><span style="font-size: 8pt; color: #de2418; font-family: Verdana">*</span></strong></td>
                                                                <td style="width: 312px; height: 2px">
                                                                    <asp:TextBox ID="txtStyleName" runat="server" CssClass="textfield" MaxLength="200"
                                                                        TabIndex="1" Width="280px"></asp:TextBox></td>
                                                                <td style="width: 280px; height: 2px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 2px" width="6%">
                                                                </td>
                                                                <td class="textbold" style="width: 441px; height: 2px">
                                                                </td>
                                                                <td style="height: 2px" width="15%">
                                                                    Design<strong><span style="font-size: 8pt; color: #de2418; font-family: Verdana">*</span></strong></td>
                                                                <td style="width: 312px; height: 2px">
                                                                    <asp:TextBox ID="txtDesignName" runat="server" CssClass="textfield" MaxLength="200"
                                                                        TabIndex="1" Width="280px"></asp:TextBox></td>
                                                                <td style="width: 280px; height: 2px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="width: 441px">
                                                                </td>
                                                                <td width="15%">
                                                                    ShadeNo</td>
                                                                <td style="width: 312px">
                                                                    <asp:TextBox ID="txtShadeNo" runat="server" CssClass="textfield" MaxLength="200"
                                                                        TabIndex="1" Width="280px"></asp:TextBox></td>
                                                                <td valign="top" style="width: 280px">
                                                                </td>
                                                                <%--<td valign="top" width="18%" style="height: 22px">
                                                                    <!--this is for two button in a single Cell -->
                                                                    <table>
                                                                    
                                                                          <tr>
                                                                                <td align=left>
                                                                               <asp:Button ID="Button1" runat="server" CssClass="button" Text="New" TabIndex="2" />                                                                                                                                         
                                                                                </td>
                                                                          </tr>
                                                                            <tr>
                                                                                <td align=left>
                                                                                    <asp:Button ID="Button2" runat="server" CssClass="button" Text="Reset" TabIndex="2" />                                                                     
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                     </td>--%>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                </td>
                                                                <td class="textbold" style="width: 441px">
                                                                </td>
                                                                <td width="15%">
                                                                    MRP</td>
                                                                <td style="width: 312px">
                                                                    <asp:TextBox ID="txtMRP" runat="server" CssClass="textfield" MaxLength="200" TabIndex="1"
                                                                        Width="280px"></asp:TextBox></td>
                                                                <td style="width: 280px" valign="top">
                                                                </td>
                                                                <td rowspan="1" style="height: 22px" valign="bottom" width="18%">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 1px">
                                                                </td>
                                                                <td class="textbold" style="width: 441px; height: 1px">
                                                                </td>
                                                                <td style="height: 1px" width="15%">
                                                                </td>
                                                                <td style="width: 312px; height: 1px">
                                                                </td>
                                                                <td style="width: 280px; height: 1px" valign="top">
                                                                </td>
                                                                <td rowspan="1" style="height: 1px" valign="bottom" width="18%">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 6px">
                                                                </td>
                                                                <td class="textbold" style="width: 441px; height: 6px">
                                                                </td>
                                                                <td style="height: 6px" width="15%">
                                                                </td>
                                                                <td style="width: 312px; height: 6px">
                                                                </td>
                                                                <td style="width: 280px; height: 6px" valign="top">
                                                                </td>
                                                                <td rowspan="1" style="height: 6px" valign="bottom" width="18%">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 6px">
                                                                </td>
                                                                <td class="textbold" style="width: 441px; height: 6px">
                                                                </td>
                                                                <td style="height: 6px" width="15%">
                                                                </td>
                                                                <td style="width: 312px; height: 6px">
                                                                </td>
                                                                <td style="width: 280px; height: 6px" valign="top">
                                                                </td>
                                                                <td rowspan="1" style="height: 6px" valign="bottom" width="18%">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 6px">
                                                                </td>
                                                                <td class="textbold" style="width: 441px; height: 6px">
                                                                </td>
                                                                <td style="height: 6px" width="15%">
                                                                </td>
                                                                <td style="width: 312px; height: 6px">
                                                                </td>
                                                                <td style="width: 280px; height: 6px" valign="top">
                                                                </td>
                                                                <td rowspan="1" style="height: 6px" valign="bottom" width="18%">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" class="textbold" colspan="6" valign="bottom">
                                                                    <strong><span style="font-size: 8pt; color: #9d0a0a; font-family: Verdana">Field Marked
                                                                        * are Mandatory</span></strong>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6">
                                                                    <span class="ErrorMsg"></span>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <br />
                                                    </td>
                                                    <td width="18%" rowspan="1" >
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
