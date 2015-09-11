<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_AirLineOffice.aspx.vb"
    Inherits="Setup_MSUP_AirLineOffice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Add/Modify AirlineOffice</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
     <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">     
    function AirlineOfficeReset()
    {
        document.getElementById("lblError").innerHTML="";
        document.getElementById("cboAirlineName").selectedIndex = 0;
        document.getElementById("txtAddress").value="";        
        document.getElementById("cboAoffice").selectedIndex=0;        
        return true;
    }
    
   function  NewFunction()
   {    
       window.location="MSUP_AirLineOffice.aspx?Action=I";
       return false;
   }

     function CheckMendatoty()
    {
        if (document.getElementById("cboAirlineName").selectedIndex==0)
        {          
                document.getElementById("lblError").innerText="Airline Name is Mandatory"
                document.getElementById("cboAirlineName").focus();
                return false;
        }
        
        if (document.getElementById("txtAddress").value=="")
        {           
                document.getElementById("lblError").innerText="AirlineOffice Address is Mandatory"
                document.getElementById("txtAddress").focus();
                return false;
        }        
        if (document.getElementById("cboAoffice").selectedIndex==0)
        {           
                document.getElementById("lblError").innerText="Please select Aoffice ."
                document.getElementById("cboAoffice").focus();
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
                                <span class="menu">Setup-></span><span class="sub_menu">AirLine Office</span>
                            </td>
                        </tr>
                        <tr>
                        <tr>
                            <td class="heading" align="center" valign="top" style="height: 10px">
                                Manage AirLine Office
                            </td>
                        </tr>
                        <tr>
                            <td width="100%" valign="top">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td width="100%" class="redborder">
                                            <table align="left" border="0" cellpadding="2" cellspacing="0" width="100%">
                                                <tr height="20px">
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 256px; width: 100%" colspan="4" valign="top">
                                                        <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td class="textbold" colspan="6" align="center" height="25px" valign="TOP">
                                                                    <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" ></asp:Label>
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
                                                                <td style="width: 173px; height: 6px">
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
                                                                    Airline Name<span class="Mandatory">*</span></td>
                                                                <td style="height: 2px; width: 312px;">
                                                                    <span class="textbold"><span style="font-size: 9pt; color: #000000; font-family: Tahoma">
                                                                        <asp:DropDownList  onkeyup="gotop(this.id)"  ID="cboAirlineName" runat="server" CssClass="dropdown" TabIndex="1"
                                                                            Width="187px">
                                                                        </asp:DropDownList></span></span></td>
                                                                <td style="height: 2px; width: 173px;">
                                                                </td>
                                                                <!--following td is for displaying all buttons on this page -->
                                                                <td width="18%" style="height: 22px" rowspan="4" valign="top">
                                                                    <table>
                                                                        <tr>
                                                                            <td style="width: 78px">
                                                                                <asp:Button ID="btnSave" runat="server" CssClass="button" TabIndex="2" Text="Save" AccessKey="A" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" style="width: 78px">
                                                                                <asp:Button ID="btnNew" runat="server" CssClass="button" Text="New" TabIndex="2" AccessKey="N" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" style="width: 78px">
                                                                                <asp:Button ID="btnReset" runat="server" CssClass="button" Text="Reset" TabIndex="2" AccessKey="R" />
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
                                                                </td>
                                                                <td style="width: 312px; height: 2px">
                                                                </td>
                                                                <td style="width: 173px; height: 2px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 2px" width="6%">
                                                                </td>
                                                                <td class="textbold" style="width: 441px; height: 2px">
                                                                </td>
                                                                <td style="height: 2px" width="15%">
                                                                </td>
                                                                <td style="width: 312px; height: 2px">
                                                                </td>
                                                                <td style="width: 173px; height: 2px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="width: 441px">
                                                                </td>
                                                                <td width="15%">
                                                                    Airline Address<span class="Mandatory">*</span></td>
                                                                <td style="width: 312px">
                                                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="textfield" TabIndex="1" Height="329%"
                                                                        Rows="5" TextMode="MultiLine" Width="180px" MaxLength="255" /></td>
                                                                <td valign="top" style="width: 173px">
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
                                                                <td align="center" class="textbold" colspan="6" valign="top">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="width: 441px">
                                                                </td>
                                                                <td>
                                                                    Aoffice<span class="Mandatory">*</span></td>
                                                                <td style="width: 312px">
                                                                    <asp:DropDownList  onkeyup="gotop(this.id)"  ID="cboAoffice" runat="server" CssClass="dropdown" TabIndex="1"
                                                                        Width="187px">
                                                                    </asp:DropDownList></td>
                                                                <td style="width: 173px">
                                                                    &nbsp;</td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                </td>
                                                                <td class="textbold" style="width: 441px">
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td style="width: 312px">
                                                                </td>
                                                                <td style="width: 173px">
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                </td>
                                                                <td class="textbold" style="width: 441px">
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td style="width: 312px">
                                                                </td>
                                                                <td style="width: 173px">
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 23px">
                                                                    &nbsp;</td>
                                                                <td colspan="2" class="ErrorMsg">
                                                                </td>
                                                                <td style="height: 23px; width: 312px;">
                                                                    &nbsp;</td>
                                                                <td style="height: 23px; width: 173px;">
                                                                    &nbsp;</td>
                                                                <td style="height: 23px">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                    &nbsp;</td>
                                                                <td colspan="2">
                                                                    <strong><span style="font-size: 8pt"></span></strong>
                                                                </td>
                                                                <td style="width: 312px">
                                                                    &nbsp;</td>
                                                                <td style="width: 173px">
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" height="4">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" height="4">
                                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                    &nbsp; &nbsp; &nbsp;&nbsp; <span class="ErrorMsg">Field Marked * are Mandatory</span></td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" style="height: 12px">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <br />
                                                    </td>
                                                    <td width="18%" rowspan="1" valign="top" style="height: 256px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="5">
                                                    </td>
                                                </tr>
                                                <tr style="font-size: 12pt; font-family: Times New Roman">
                                                    <td colspan="6" height="12">
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
            <tr>
                <td valign="top">
                    &nbsp;</td>
            </tr>
        </table>
    </form>
</body>
</html>
