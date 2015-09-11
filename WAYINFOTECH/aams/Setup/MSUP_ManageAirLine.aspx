<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_ManageAirLine.aspx.vb"
    Inherits="Setup_MSUP_ManageAirLine" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2" runat="server">
    <title>AAMS:Add/Modify Airline</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">        
    function AirlineReset()
    {   
        document.getElementById("lblError").innerHTML="";
        document.getElementById("txtAirlineName").value="";
        document.getElementById("txtAirlineCode").value="";
        document.getElementById("chekOnlineCarrier").checked = false;
        return true;
    }
    
    function CheckMendatoty()
    {
        if (document.getElementById("txtAirlineName").value=="")
        {          
            document.getElementById("lblError").innerText="Please enter Airline Name ";
            document.getElementById("txtAirlineName").focus();
            return false;
        }
        if (document.getElementById("txtAirlineCode").value=="")
        {           
            document.getElementById("lblError").innerText="Please enter Airline Code "
            document.getElementById("txtAirlineCode").focus();
            return false;
        }        
       
       
       
       
        if (IsDataValid(document.getElementById("txtAirlineCode").value,6)==false)
        {   
        var txtVal = document.getElementById("txtAirlineCode").value;
        
         //vAscii = txtVal.charCodeAt(0);
         //alert(vAscii) 
        // vAscii1=txtVal.charCodeAt(1);
               //    alert(vAscii1) 
                   
           document.getElementById("lblError").innerText="Please enter valid Airline Code "
            document.getElementById("txtAirlineCode").focus();
            return false;
        }        
    }
    
    
    function NewFunction()
    {   
        window.location.href="MSUP_ManageAirLine.aspx?Action=I";       
        return false;
    }
    </script>

</head>
<body >
    <form id="frmAirline" runat="server">
        <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Setup-></span><span class="sub_menu">Airline </span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Manage Airline
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                            <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" height="25px" valign="TOP">
                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="6%" class="textbold">
                                                        &nbsp;</td>
                                                    <td width="15%" class="textbold">
                                                        Airline Name<span class="Mandatory">*</span></td>
                                                    <td width="30%">
                                                        <asp:TextBox ID="txtAirlineName" CssClass="textbox" runat="server" TabIndex="1" MaxLength="40"></asp:TextBox></td>
                                                    <td width="12%" class="textbold">
                                                        Airline Code<span class="Mandatory">*</span></td>
                                                    <td width="21%">
                                                        <asp:TextBox ID="txtAirlineCode" CssClass="textbox" runat="server" TabIndex="2" MaxLength="2"></asp:TextBox></td>
                                                    <td width="18%">
                                                        <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="4"  AccessKey="A"/></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 22px">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="height: 22px">
                                                        Online Carrier</td>
                                                    <td style="height: 22px">
                                                        <asp:CheckBox ID="chekOnlineCarrier" runat="server" TabIndex="3" />
                                                    </td>
                                                    <td class="textbold" style="height: 22px">
                                                    </td>
                                                    <td style="height: 22px">
                                                    </td>
                                                    <td style="height: 22px">
                                                        <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="5"  AccessKey="N"/></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold">
                                                        &nbsp;</td>
                                                    <td class="textbold">
                                                    </td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="6" AccessKey="R" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 23px">
                                                        &nbsp;</td>
                                                    <td colspan="2" class="ErrorMsg">
                                                        Field Marked * are Mandatory</td>
                                                    <td style="height: 23px">
                                                        &nbsp;</td>
                                                    <td style="height: 23px">
                                                        &nbsp;</td>
                                                    <td style="height: 23px">
                                                        &nbsp;</td>
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
